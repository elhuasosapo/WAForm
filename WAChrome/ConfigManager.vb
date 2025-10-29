Imports System.IO
Imports System.Text.Json
Imports System.Text.Json.Serialization

Public Class ConfigManager

    Public Class Messaggi
        Public Property conferma As String
        Public Property rifiuta As String

        ' Elenco di messaggi extra, ognuno con titolo e testo
        Public Property extra As List(Of MessaggioExtra)
    End Class

    Public Class MessaggioExtra
        Public Property titolo As String
        Public Property testo As String
    End Class



    Public Class ConfigData
        Public Property messaggi As Messaggi
    End Class

    Private Shared ReadOnly DefaultConfig As New ConfigData With {
    .messaggi = New Messaggi With {
        .conferma = "Ciao, la tua prenotazione è confermata ✅",
        .rifiuta = "Ci dispiace, non possiamo accettare la tua prenotazione. ❌",
        .extra = New List(Of MessaggioExtra) From {
            New MessaggioExtra With {.titolo = "Promemoria", .testo = "Non dimenticare il tuo appuntamento 📅"},
            New MessaggioExtra With {.titolo = "Ringraziamento", .testo = "Grazie per averci scelto 🙏"}
        }
    }
}


    ''' <summary>
    ''' Carica il file di configurazione; se non esiste, lo crea con i valori di default.
    ''' </summary>
    Public Shared Function CaricaOPrepara(filePath As String) As ConfigData
        Try
            If Not File.Exists(filePath) Then
                ' Crea directory se non esiste
                Dim dir As String = System.IO.Path.GetDirectoryName(filePath)
                If Not String.IsNullOrEmpty(dir) AndAlso Not Directory.Exists(dir) Then
                    Directory.CreateDirectory(dir)
                End If

                ' Serializza il JSON con indentazione leggibile
                Dim options As New JsonSerializerOptions With {
                    .WriteIndented = True
                }
                Dim json As String = JsonSerializer.Serialize(DefaultConfig, options)
                File.WriteAllText(filePath, json)
            End If

            ' Leggi e deserializza il JSON
            Dim jsonText As String = File.ReadAllText(filePath)
            Dim cfg = JsonSerializer.Deserialize(Of ConfigData)(jsonText)

            If cfg Is Nothing Then Return DefaultConfig
            Return cfg

        Catch ex As Exception
            ' Se c'è un errore (file corrotto, permessi, ecc.), ritorna config di default
            Return DefaultConfig
        End Try
    End Function

End Class
