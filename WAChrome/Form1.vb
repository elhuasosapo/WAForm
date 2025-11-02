Imports System.IO
Imports System.Text
Imports Microsoft.Web.WebView2.Core

Public Class Form1

    Private Config As ConfigManager.ConfigData


    Private Async Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' Gestione argomenti da riga di comando
            Dim args() As String = Environment.GetCommandLineArgs()
            If args.Length > 1 Then
                TxtNumero.Text = args(1) ' il primo argomento dopo l'eseguibile
            End If

            Dim configPath As String = Path.Combine(Application.StartupPath, "messaggi.json")
            Config = ConfigManager.CaricaOPrepara(configPath)

            Await WebView2.EnsureCoreWebView2Async(Nothing)
            WebView2.CoreWebView2.Navigate("https://web.whatsapp.com/")
            LblStato.Text = "Carica WhatsApp Web e accedi al tuo account..."

            GeneraPulsantiExtra()

        Catch ex As Exception
            LblStato.Text = "Errore: " & ex.Message
        End Try
    End Sub

    Private Async Sub BtnConferma_Click(sender As Object, e As EventArgs) Handles BtnConferma.Click
        Await InviaMessaggioAsync(TxtNumero.Text.Trim(), Config.messaggi.conferma)
    End Sub

    Private Async Sub BtnRifiuta_Click(sender As Object, e As EventArgs) Handles BtnRifiuta.Click
        Await InviaMessaggioAsync(TxtNumero.Text.Trim(), Config.messaggi.rifiuta)
    End Sub


    ' Escape sicuro per testo JavaScript
    Private Function JsEscape(s As String) As String
        If s Is Nothing Then Return ""
        Return s.Replace("\", "\\").Replace("""", "\""").Replace(vbCrLf, "\n").Replace(vbLf, "\n")
    End Function

    Private Async Function InviaMessaggioAsync(numero As String, testo As String) As Task
        Try
            Dim usaNavigazione As Boolean = Not String.IsNullOrWhiteSpace(numero)

            If usaNavigazione Then
                LblStato.Text = $"Apertura chat per {numero}..."
                ' WhatsApp aggiunge già il testo con ?text=
                Dim url As String = $"https://web.whatsapp.com/send?phone={numero}&text={Uri.EscapeDataString(testo)}"
                WebView2.CoreWebView2.Navigate(url)
                Await Task.Delay(5000)
            Else
                LblStato.Text = "Uso chat corrente..."
            End If

            ' Attendi che la chat sia pronta
            LblStato.Text = "Attendo caricamento campo messaggi..."
            Dim chatPronta = Await AttendiElementoAsync("div[role='textbox']", 30000)
            If Not chatPronta Then
                LblStato.Text = "❌ Chat non caricata. Riprova."
                Exit Function
            End If

            LblStato.Text = "✅ Campo messaggio individuato."

            ' Prepara il testo da inserire nel JS
            Dim safeText As String = JsEscape(testo)

            ' Costruisci script JS
            Dim script As New StringBuilder()
            script.AppendLine("(async function(){")
            script.AppendLine("function sleep(ms){return new Promise(r=>setTimeout(r,ms));}")

            If usaNavigazione Then
                ' Caso 1: chat aperta con numero -> testo già inserito da WhatsApp
                script.AppendLine("await sleep(800);")
                script.AppendLine("const sendBtn = document.querySelector('[data-testid=""send""]') || document.querySelector('[aria-label=""Invia""]');")
                script.AppendLine("if (sendBtn){ sendBtn.click(); return 'SENT_CLICK'; }")
                script.AppendLine("const box = document.querySelector('div[role=""textbox""]');")
                script.AppendLine("if (box){")
                script.AppendLine("  box.dispatchEvent(new KeyboardEvent('keydown',{key:'Enter',code:'Enter',bubbles:true}));")
                script.AppendLine("  box.dispatchEvent(new KeyboardEvent('keyup',{key:'Enter',code:'Enter',bubbles:true}));")
                script.AppendLine("  return 'SENT_ENTER';")
                script.AppendLine("}")
                script.AppendLine("return 'NO_INPUT';")
            Else
                ' Caso 2: chat corrente -> incolla testo manualmente
                script.AppendLine("const box = Array.from(document.querySelectorAll('div[role=""textbox""]'))")
                script.AppendLine("  .find(el => el.offsetParent !== null && !((el.getAttribute('aria-label')||'').toLowerCase().includes('cerca')));")
                script.AppendLine("if(!box) return 'NO_INPUT';")
                script.AppendLine("box.focus();")
                script.AppendLine("try {")
                script.AppendLine("  const dt = new DataTransfer();")
                script.AppendLine($"  dt.setData('text', '{safeText}');")
                script.AppendLine("  const ev = new ClipboardEvent('paste',{bubbles:true,cancelable:true,clipboardData:dt});")
                script.AppendLine("  box.dispatchEvent(ev);")
                script.AppendLine("} catch(e) {}")
                script.AppendLine("await sleep(600);")
                script.AppendLine("const sendBtn = document.querySelector('[data-testid=""send""]') || document.querySelector('[aria-label=""Invia""]');")
                script.AppendLine("if (sendBtn){ sendBtn.click(); return 'SENT_CLICK'; }")
                script.AppendLine("box.dispatchEvent(new KeyboardEvent('keydown',{key:'Enter',code:'Enter',bubbles:true}));")
                script.AppendLine("box.dispatchEvent(new KeyboardEvent('keyup',{key:'Enter',code:'Enter',bubbles:true}));")
                script.AppendLine("return 'SENT_ENTER';")
            End If

            script.AppendLine("})();")

            ' Esegui script
            Dim result = Await WebView2.CoreWebView2.ExecuteScriptAsync(script.ToString())

            ' Gestione risultato
            If result.Contains("SENT_CLICK") OrElse result.Contains("SENT_ENTER") Then
                LblStato.Text = "✅ Messaggio inviato."
            ElseIf result.Contains("NO_INPUT") Then
                LblStato.Text = "⚠️ Campo messaggi non disponibile. Apri la chat e riprova."
            Else
                LblStato.Text = "❌ Non sono riuscito a inviare il messaggio."
            End If

        Catch ex As Exception
            LblStato.Text = "❌ Errore durante invio: " & ex.Message
        End Try
    End Function



    Private Async Function AttendiElementoAsync(selector As String, timeoutMs As Integer) As Task(Of Boolean)
        Dim elapsed As Integer = 0
        Dim delay As Integer = 1000

        While elapsed < timeoutMs
            ' cerca anche dentro shadow roots (senza evidenziazioni)
            Dim script As String = "
        (function() {
            function findAllInShadow(root, selector) {
                let result = [];
                if (!root) return result;
                if (root.querySelectorAll) {
                    result = Array.from(root.querySelectorAll(selector));
                }
                const elements = root.querySelectorAll('*');
                for (const el of elements) {
                    if (el.shadowRoot) {
                        result = result.concat(findAllInShadow(el.shadowRoot, selector));
                    }
                }
                return result;
            }
            const matches = findAllInShadow(document, 'div[role=""textbox""]');
            return matches.length;
        })();
        "

            Dim result = Await WebView2.CoreWebView2.ExecuteScriptAsync(script)
            Dim count As Integer
            Integer.TryParse(New String(result.Where(AddressOf Char.IsDigit).ToArray()), count)

            If count > 0 Then
                Return True
            End If

            LblStato.Text = $"Attesa campo di testo... ({elapsed \ 1000}s)"
            Await Task.Delay(delay)
            elapsed += delay
        End While

        ' non mostrare MessageBox qui, solo ritorniamo False
        Return False
    End Function

    Private Sub GeneraPulsantiExtra()
        If Config?.messaggi?.extra Is Nothing OrElse Config.messaggi.extra.Count = 0 Then Exit Sub

        ' 🔹 Pulisce solo le prime due celle della prima riga
        For col = 0 To 1
            Dim controlToRemove = TableLayoutPanel2.GetControlFromPosition(col, 0)
            If controlToRemove IsNot Nothing Then
                TableLayoutPanel2.Controls.Remove(controlToRemove)
                controlToRemove.Dispose()
            End If
        Next

        ' 🔹 Aggiunge fino a 2 pulsanti extra nella prima riga
        Dim maxPulsanti As Integer = Math.Min(2, Config.messaggi.extra.Count)

        For i = 0 To maxPulsanti - 1
            Dim msg = Config.messaggi.extra(i)

            Dim btn As New Button()
            btn.Name = $"BtnExtra{i}"
            btn.Text = msg.titolo
            btn.Dock = DockStyle.Fill
            btn.Margin = New Padding(5)
            btn.Tag = msg.testo
            btn.Height = 35

            AddHandler btn.Click, AddressOf BtnExtra_Click

            TableLayoutPanel2.Controls.Add(btn, i, 0)
        Next
    End Sub


    Private Async Sub BtnExtra_Click(sender As Object, e As EventArgs)
        Dim btn = TryCast(sender, Button)
        If btn Is Nothing Then Exit Sub

        Dim testoMessaggio As String = TryCast(btn.Tag, String)
        If String.IsNullOrEmpty(testoMessaggio) Then Exit Sub

        Await InviaMessaggioAsync(TxtNumero.Text.Trim(), testoMessaggio)
    End Sub

    Private Sub BtnRicaricaConfig_Click(sender As Object, e As EventArgs) Handles BtnRicaricaConfig.Click
        RicaricaConfigurazione()
    End Sub

    Private Async Sub EvidenziaStatoAsync()
        Dim originalColor = LblStato.ForeColor
        LblStato.ForeColor = Color.Green
        Await Task.Delay(1000)
        LblStato.ForeColor = originalColor
    End Sub

    Private Sub RicaricaConfigurazione()
        Try
            Dim configPath As String = Path.Combine(Application.StartupPath, "messaggi.json")

            ' 🔹 Ricarica il file JSON aggiornato
            Config = ConfigManager.CaricaOPrepara(configPath)

            ' 🔹 Rigenera i pulsanti extra nella TableLayoutPanel2
            GeneraPulsantiExtra()

            LblStato.Text = "✅ Configurazione ricaricata con successo."
        Catch ex As Exception
            LblStato.Text = "❌ Errore durante la ricarica configurazione: " & ex.Message
        End Try
        EvidenziaStatoAsync()
    End Sub

    Private Sub btnMessaggi_Click(sender As Object, e As EventArgs) Handles btnMessaggi.Click
        Dim editor As New FormEditor()
        editor.ShowDialog()
    End Sub
End Class
