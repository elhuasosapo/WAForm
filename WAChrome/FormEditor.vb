Imports System.IO
Imports System.Text
Imports System.Text.Json
Imports System.Text.Json.Nodes

Public Class FormEditor

    Private jsonPath As String = Path.Combine(Application.StartupPath, "messaggi.json")
    Private messaggi As JsonObject
    Private currentKey As String
    Private jsonPathEmoji As String = Path.Combine(Application.StartupPath, "emojis.json")
    Private emojisData As JsonObject
    ' Flag per maiuscole/minuscole
    Private isShiftActive As Boolean = True


    Private Sub FormEditor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CaricaJson()
        PopolaCombo()
        CaricaEmojiDaFile()
        PopolaCategorieEmoji()
        CreaTastieraVirtuale()
    End Sub

    Private Sub CaricaJson()
        Dim jsonText As String = File.ReadAllText(jsonPath, Encoding.UTF8)
        Dim root = JsonNode.Parse(jsonText)
        messaggi = CType(root("messaggi"), JsonObject)
    End Sub

    Private Sub PopolaCombo()
        ComboBoxMessaggi.Items.Clear()
        ComboBoxMessaggi.Items.Add("conferma")
        ComboBoxMessaggi.Items.Add("rifiuta")

        ' aggiungi extra
        Dim extra = CType(messaggi("extra"), JsonArray)
        For Each item In extra
            ComboBoxMessaggi.Items.Add(CType(item("titolo"), String))
        Next
        ComboBoxMessaggi.SelectedIndex = 0
    End Sub

    Private Sub ComboBoxMessaggi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxMessaggi.SelectedIndexChanged
        Dim sel = ComboBoxMessaggi.SelectedItem.ToString()
        currentKey = sel

        If sel = "conferma" OrElse sel = "rifiuta" Then
            TextBoxMessaggio.Text = messaggi(sel).ToString()
        Else
            ' Cerca nei "extra"
            Dim extra = CType(messaggi("extra"), JsonArray)
            For Each item As JsonNode In extra
                If item("titolo").ToString() = sel Then
                    TextBoxMessaggio.Text = item("testo").ToString()
                    Exit For
                End If
            Next
        End If
    End Sub

    ' 🔹 Carica le emoji dal file JSON
    Private Sub CaricaEmojiDaFile()
        Try
            ' Se il file non esiste, lo creiamo con un contenuto di base
            If Not File.Exists(jsonPathEmoji) Then
                Dim defaultEmojis As New Dictionary(Of String, String()) From {
                {"Facce", New String() {"😀", "😁", "😂", "🤣", "😊", "😍", "😎", "😢", "😡", "😱"}},
                {"Simboli", New String() {"✅", "❌", "⭐", "🔥", "💡", "🎉", "💖", "📣", "⚡"}},
                {"Oggetti", New String() {"📅", "📞", "💻", "📱", "🎁", "☕", "🍀", "🚀", "🏠", "✈️"}},
                {"Gesti", New String() {"👍", "👎", "🙏", "👏", "🤝", "💪"}}
            }

                Dim jsonTextDefault As String = JsonSerializer.Serialize(defaultEmojis, New JsonSerializerOptions With {
                .WriteIndented = True,
                .Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            })

                File.WriteAllText(jsonPathEmoji, jsonTextDefault, Encoding.UTF8)
            End If

            ' Ora carichiamo il file (sia che fosse già presente, sia che l'abbiamo appena creato)
            Dim jsonText As String = File.ReadAllText(jsonPathEmoji, Encoding.UTF8)
            emojisData = CType(JsonNode.Parse(jsonText), JsonObject)

        Catch ex As Exception
            MessageBox.Show("Errore durante il caricamento/creazione delle emoji: " & ex.Message,
                        "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    ' 🔹 Popola la ComboBox con i nomi delle categorie
    Private Sub PopolaCategorieEmoji()
        ComboBoxCategorieEmoji.Items.Clear()

        If emojisData Is Nothing Then Return

        For Each kvp In emojisData
            ComboBoxCategorieEmoji.Items.Add(kvp.Key)
        Next

        If ComboBoxCategorieEmoji.Items.Count > 0 Then
            ComboBoxCategorieEmoji.SelectedIndex = 0
        End If
    End Sub


    ' 🔹 Evento cambio categoria
    Private Sub ComboBoxCategorieEmoji_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxCategorieEmoji.SelectedIndexChanged
        Dim categoria = ComboBoxCategorieEmoji.SelectedItem.ToString()
        MostraEmojiPerCategoria(categoria)
    End Sub

    ' 🔹 Mostra le emoji della categoria scelta
    Private Sub MostraEmojiPerCategoria(categoria As String)
        FlowLayoutPanelEmoji.Controls.Clear()

        If emojisData Is Nothing OrElse Not emojisData.ContainsKey(categoria) Then Exit Sub

        Dim emojiArray = CType(emojisData(categoria), JsonArray)

        For Each emojiNode In emojiArray
            Dim emoji As String = emojiNode.ToString()
            Dim btn As New Button()
            btn.Text = emoji
            btn.Font = New Font("Segoe UI Emoji", 14, FontStyle.Regular)
            btn.Width = 40
            btn.Height = 40
            btn.Margin = New Padding(3)
            btn.FlatStyle = FlatStyle.Flat
            AddHandler btn.Click, Sub(s, e)
                                      TextBoxMessaggio.SelectedText = emoji
                                  End Sub
            FlowLayoutPanelEmoji.Controls.Add(btn)
        Next
    End Sub

    ' 🔹 Pulsante opzionale per ricaricare il file JSON
    Private Sub ButtonAggiornaEmoji_Click(sender As Object, e As EventArgs) Handles ButtonAggiornaEmoji.Click
        CaricaEmojiDaFile()
        PopolaCategorieEmoji()
    End Sub

    Private Sub ButtonSalva_Click(sender As Object, e As EventArgs) Handles ButtonSalva.Click
        Dim testo = TextBoxMessaggio.Text.Trim

        If currentKey = "conferma" OrElse currentKey = "rifiuta" Then
            messaggi(currentKey) = testo
        Else
            Dim extra = CType(messaggi("extra"), JsonArray)
            For Each item In extra
                If item("titolo").ToString = currentKey Then
                    item("testo") = testo
                End If
            Next
        End If

        ' ✅ Scrivi direttamente il nodo "messaggi"
        Dim root As New JsonObject()
        root("messaggi") = messaggi.DeepClone() ' <-- 🔥 clone per evitare "parent already set"

        File.WriteAllText(
        jsonPath,
        root.ToJsonString(New JsonSerializerOptions With {
            .WriteIndented = True,
            .Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        }),
        Encoding.UTF8
    )

        MessageBox.Show("Messaggio salvato ✅", "Salvato", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub


    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub CreaTastieraVirtuale()
        ' Pulisce il pannello
        PanelKeyboard.Controls.Clear()
        PanelKeyboard.AutoScroll = True
        PanelKeyboard.BackColor = Color.FromArgb(45, 45, 45)

        ' Layout tastiera
        Dim righe As String() = {
        "1234567890",
        "QWERTYUIOP",
        "ASDFGHJKL",
        "ZXCVBNM"
    }

        Dim y As Integer = 10
        Dim buttonWidth As Integer = 50
        Dim buttonHeight As Integer = 50
        Dim spacing As Integer = 5

        ' 🎹 Genera le righe lettere/numeri
        For Each riga As String In righe
            Dim numButtons As Integer = riga.Length
            Dim totalRowWidth As Integer = (numButtons * buttonWidth) + ((numButtons - 1) * spacing)
            Dim startRowX As Integer = Math.Max(10, (PanelKeyboard.ClientSize.Width - totalRowWidth) \ 2)

            Dim x As Integer = startRowX
            For Each c As Char In riga
                Dim displayChar As String = If(isShiftActive, c.ToString().ToUpper(), c.ToString().ToLower())

                Dim btn As New Button()
                btn.Text = displayChar
                btn.Font = New Font("Segoe UI", 14, FontStyle.Bold)
                btn.Width = buttonWidth
                btn.Height = buttonHeight
                btn.Left = x
                btn.Top = y
                btn.FlatStyle = FlatStyle.Flat
                btn.BackColor = Color.Black
                btn.ForeColor = Color.White
                btn.FlatAppearance.BorderColor = Color.Gray
                btn.FlatAppearance.BorderSize = 1

                Dim localBtn = btn
                AddHandler localBtn.Click, Sub() InserisciCarattere(localBtn.Text)

                PanelKeyboard.Controls.Add(localBtn)
                x += buttonWidth + spacing
            Next

            y += buttonHeight + spacing
        Next

        ' 🇮🇹 Riga caratteri speciali italiani
        Dim specialChars As String() = {"""", "?", "ì", "é", "ò", "à", "ù", ",", ".", "-", "@"}
        Dim numSpecial As Integer = specialChars.Length
        Dim totalSpecialRowWidth As Integer = (numSpecial * buttonWidth) + ((numSpecial - 1) * spacing)
        Dim startSpecialRowX As Integer = Math.Max(10, (PanelKeyboard.ClientSize.Width - totalSpecialRowWidth) \ 2)

        Dim xSpec As Integer = startSpecialRowX
        For Each ch As String In specialChars
            Dim btn As New Button()
            btn.Text = ch
            btn.Font = New Font("Segoe UI", 14, FontStyle.Bold)
            btn.Width = buttonWidth
            btn.Height = buttonHeight
            btn.Left = xSpec
            btn.Top = y
            btn.FlatStyle = FlatStyle.Flat
            btn.BackColor = Color.Black
            btn.ForeColor = Color.White
            btn.FlatAppearance.BorderColor = Color.Gray
            btn.FlatAppearance.BorderSize = 1

            Dim localBtn = btn
            AddHandler localBtn.Click, Sub() InserisciCarattere(localBtn.Text)

            PanelKeyboard.Controls.Add(localBtn)
            xSpec += buttonWidth + spacing
        Next

        y += buttonHeight + spacing

        ' 🔘 Tasti speciali (Shift, Spazio, Backspace, Invio)
        Dim spaceBtnWidth As Integer = 220
        Dim otherBtnWidth As Integer = 100
        Dim totalSpecialButtonsWidth As Integer = otherBtnWidth + spaceBtnWidth + otherBtnWidth + otherBtnWidth + (3 * spacing)
        Dim startSpecialButtonsX As Integer = Math.Max(10, (PanelKeyboard.ClientSize.Width - totalSpecialButtonsWidth) \ 2)

        Dim xButtons As Integer = startSpecialButtonsX

        ' Tasto Shift
        Dim btnShift As New Button()
        btnShift.Text = "Shift"
        btnShift.Tag = "SHIFT"
        btnShift.Font = New Font("Segoe UI", 12, FontStyle.Bold)
        btnShift.Width = otherBtnWidth
        btnShift.Height = buttonHeight
        btnShift.Left = xButtons
        btnShift.Top = y + 10
        btnShift.BackColor = If(isShiftActive, Color.DimGray, Color.Black)
        btnShift.ForeColor = Color.White
        btnShift.FlatStyle = FlatStyle.Flat
        btnShift.FlatAppearance.BorderColor = Color.Gray
        btnShift.FlatAppearance.BorderSize = 1

        Dim localShift = btnShift
        AddHandler localShift.Click, Sub()
                                         isShiftActive = Not isShiftActive
                                         CreaTastieraVirtuale()
                                     End Sub
        PanelKeyboard.Controls.Add(localShift)
        xButtons += btnShift.Width + spacing

        ' Spazio
        Dim btnSpace As New Button()
        btnSpace.Text = "Spazio"
        btnSpace.Tag = " "
        btnSpace.Font = New Font("Segoe UI", 12, FontStyle.Regular)
        btnSpace.Width = spaceBtnWidth
        btnSpace.Height = buttonHeight
        btnSpace.Left = xButtons
        btnSpace.Top = y + 10
        btnSpace.BackColor = Color.Black
        btnSpace.ForeColor = Color.White
        btnSpace.FlatStyle = FlatStyle.Flat
        btnSpace.FlatAppearance.BorderColor = Color.Gray
        btnSpace.FlatAppearance.BorderSize = 1
        Dim localSpace = btnSpace
        AddHandler localSpace.Click, Sub() InserisciCarattere(localSpace.Tag.ToString())
        PanelKeyboard.Controls.Add(localSpace)
        xButtons += btnSpace.Width + spacing

        ' Backspace
        Dim btnBack As New Button()
        btnBack.Text = "Backspace"
        btnBack.Tag = "{BACKSPACE}"
        btnBack.Font = New Font("Segoe UI", 12, FontStyle.Regular)
        btnBack.Width = otherBtnWidth
        btnBack.Height = buttonHeight
        btnBack.Left = xButtons
        btnBack.Top = y + 10
        btnBack.BackColor = Color.Black
        btnBack.ForeColor = Color.White
        btnBack.FlatStyle = FlatStyle.Flat
        btnBack.FlatAppearance.BorderColor = Color.Gray
        btnBack.FlatAppearance.BorderSize = 1
        Dim localBack = btnBack
        AddHandler localBack.Click, Sub() InserisciCarattere(localBack.Tag.ToString())
        PanelKeyboard.Controls.Add(localBack)
        xButtons += btnBack.Width + spacing

        ' Invio
        Dim btnEnter As New Button()
        btnEnter.Text = "Invio"
        btnEnter.Tag = "{ENTER}"
        btnEnter.Font = New Font("Segoe UI", 12, FontStyle.Regular)
        btnEnter.Width = otherBtnWidth
        btnEnter.Height = buttonHeight
        btnEnter.Left = xButtons
        btnEnter.Top = y + 10
        btnEnter.BackColor = Color.Black
        btnEnter.ForeColor = Color.White
        btnEnter.FlatStyle = FlatStyle.Flat
        btnEnter.FlatAppearance.BorderColor = Color.Gray
        btnEnter.FlatAppearance.BorderSize = 1
        Dim localEnter = btnEnter
        AddHandler localEnter.Click, Sub() InserisciCarattere(localEnter.Tag.ToString())
        PanelKeyboard.Controls.Add(localEnter)
    End Sub

    Private Sub InserisciCarattere(valore As String)
        ' Inserisce nel controllo TextBox/MaskedTextBox attivo se è di tipo TextBoxBase
        Dim focusControl = Me.ActiveControl
        ' Se il controllo attivo non è un TextBox, prova a puntare su TextBoxMessaggio
        If Not (TypeOf focusControl Is TextBoxBase) Then
            focusControl = TextBoxMessaggio
        End If

        If TypeOf focusControl Is TextBoxBase Then
            Dim txt = CType(focusControl, TextBoxBase)

            If valore = "{BACKSPACE}" Then
                If txt.SelectionStart > 0 Then
                    Dim pos = txt.SelectionStart
                    txt.Text = txt.Text.Remove(pos - 1, 1)
                    txt.SelectionStart = Math.Max(0, pos - 1)
                End If
            ElseIf valore = "{ENTER}" Then
                txt.SelectedText = vbCrLf
            Else
                txt.SelectedText = valore
            End If

            txt.Focus()
        End If
    End Sub

End Class
