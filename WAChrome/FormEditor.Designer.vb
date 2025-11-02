<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormEditor
    Inherits System.Windows.Forms.Form

    'Form esegue l'override del metodo Dispose per pulire l'elenco dei componenti.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Richiesto da Progettazione Windows Form
    Private components As System.ComponentModel.IContainer

    'NOTA: la procedura che segue è richiesta da Progettazione Windows Form
    'Può essere modificata in Progettazione Windows Form.  
    'Non modificarla mediante l'editor del codice.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        ComboBoxMessaggi = New ComboBox()
        TextBoxMessaggio = New TextBox()
        FlowLayoutPanelEmoji = New FlowLayoutPanel()
        ButtonSalva = New Button()
        Label1 = New Label()
        Label2 = New Label()
        Label3 = New Label()
        btnClose = New Button()
        ComboBoxCategorieEmoji = New ComboBox()
        ButtonAggiornaEmoji = New Button()
        Label4 = New Label()
        PanelKeyboard = New Panel()
        Label5 = New Label()
        SuspendLayout()
        ' 
        ' ComboBoxMessaggi
        ' 
        ComboBoxMessaggi.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBoxMessaggi.FormattingEnabled = True
        ComboBoxMessaggi.Location = New Point(171, 9)
        ComboBoxMessaggi.Name = "ComboBoxMessaggi"
        ComboBoxMessaggi.Size = New Size(282, 25)
        ComboBoxMessaggi.TabIndex = 0
        ' 
        ' TextBoxMessaggio
        ' 
        TextBoxMessaggio.Location = New Point(171, 54)
        TextBoxMessaggio.Multiline = True
        TextBoxMessaggio.Name = "TextBoxMessaggio"
        TextBoxMessaggio.Size = New Size(697, 86)
        TextBoxMessaggio.TabIndex = 1
        ' 
        ' FlowLayoutPanelEmoji
        ' 
        FlowLayoutPanelEmoji.AutoScroll = True
        FlowLayoutPanelEmoji.Font = New Font("Segoe UI Emoji", 11.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        FlowLayoutPanelEmoji.Location = New Point(171, 187)
        FlowLayoutPanelEmoji.Name = "FlowLayoutPanelEmoji"
        FlowLayoutPanelEmoji.Size = New Size(697, 128)
        FlowLayoutPanelEmoji.TabIndex = 2
        ' 
        ' ButtonSalva
        ' 
        ButtonSalva.BackColor = Color.WhiteSmoke
        ButtonSalva.ForeColor = Color.Black
        ButtonSalva.Location = New Point(687, 9)
        ButtonSalva.Name = "ButtonSalva"
        ButtonSalva.Size = New Size(86, 29)
        ButtonSalva.TabIndex = 3
        ButtonSalva.Text = "Salva"
        ButtonSalva.UseVisualStyleBackColor = False
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI Semibold", 9.75F, FontStyle.Bold)
        Label1.ForeColor = Color.White
        Label1.Location = New Point(14, 9)
        Label1.Name = "Label1"
        Label1.Size = New Size(133, 17)
        Label1.TabIndex = 4
        Label1.Text = "Seleziona Messaggio"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI Semibold", 9.75F, FontStyle.Bold)
        Label2.ForeColor = Color.White
        Label2.Location = New Point(41, 54)
        Label2.Name = "Label2"
        Label2.Size = New Size(109, 17)
        Label2.TabIndex = 5
        Label2.Text = "Testo Messaggio"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Segoe UI Semibold", 9.75F, FontStyle.Bold)
        Label3.ForeColor = Color.White
        Label3.Location = New Point(106, 187)
        Label3.Name = "Label3"
        Label3.Size = New Size(41, 17)
        Label3.TabIndex = 6
        Label3.Text = "Emoji"
        ' 
        ' btnClose
        ' 
        btnClose.BackColor = Color.WhiteSmoke
        btnClose.ForeColor = Color.Black
        btnClose.Location = New Point(779, 9)
        btnClose.Name = "btnClose"
        btnClose.Size = New Size(86, 29)
        btnClose.TabIndex = 7
        btnClose.Text = "Esci"
        btnClose.UseVisualStyleBackColor = False
        ' 
        ' ComboBoxCategorieEmoji
        ' 
        ComboBoxCategorieEmoji.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBoxCategorieEmoji.FormattingEnabled = True
        ComboBoxCategorieEmoji.Location = New Point(171, 156)
        ComboBoxCategorieEmoji.Name = "ComboBoxCategorieEmoji"
        ComboBoxCategorieEmoji.Size = New Size(200, 25)
        ComboBoxCategorieEmoji.TabIndex = 8
        ' 
        ' ButtonAggiornaEmoji
        ' 
        ButtonAggiornaEmoji.ForeColor = Color.Black
        ButtonAggiornaEmoji.Location = New Point(743, 156)
        ButtonAggiornaEmoji.Name = "ButtonAggiornaEmoji"
        ButtonAggiornaEmoji.Size = New Size(125, 29)
        ButtonAggiornaEmoji.TabIndex = 9
        ButtonAggiornaEmoji.Text = "🔄 Ricarica emoji"
        ButtonAggiornaEmoji.UseVisualStyleBackColor = True
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Font = New Font("Segoe UI Semibold", 9.75F, FontStyle.Bold)
        Label4.ForeColor = Color.White
        Label4.Location = New Point(47, 156)
        Label4.Name = "Label4"
        Label4.Size = New Size(103, 17)
        Label4.TabIndex = 10
        Label4.Text = "Categoria Emoji"
        ' 
        ' PanelKeyboard
        ' 
        PanelKeyboard.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        PanelKeyboard.BackColor = Color.Transparent
        PanelKeyboard.Location = New Point(171, 321)
        PanelKeyboard.Name = "PanelKeyboard"
        PanelKeyboard.Size = New Size(694, 396)
        PanelKeyboard.TabIndex = 11
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Font = New Font("Segoe UI Semibold", 9.75F, FontStyle.Bold)
        Label5.ForeColor = Color.White
        Label5.Location = New Point(96, 321)
        Label5.Name = "Label5"
        Label5.Size = New Size(54, 34)
        Label5.TabIndex = 12
        Label5.Text = "Tastiera" & vbCrLf & "  editor"
        ' 
        ' FormEditor
        ' 
        AutoScaleDimensions = New SizeF(8F, 17F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(25), CByte(25), CByte(25))
        ClientSize = New Size(934, 729)
        Controls.Add(Label5)
        Controls.Add(PanelKeyboard)
        Controls.Add(Label4)
        Controls.Add(ButtonAggiornaEmoji)
        Controls.Add(ComboBoxCategorieEmoji)
        Controls.Add(btnClose)
        Controls.Add(ButtonSalva)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Controls.Add(FlowLayoutPanelEmoji)
        Controls.Add(TextBoxMessaggio)
        Controls.Add(ComboBoxMessaggi)
        Font = New Font("Segoe UI Semibold", 9.75F, FontStyle.Bold)
        ForeColor = Color.White
        Name = "FormEditor"
        StartPosition = FormStartPosition.CenterScreen
        Text = "FormEditor"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents ComboBoxMessaggi As ComboBox
    Friend WithEvents TextBoxMessaggio As TextBox
    Friend WithEvents FlowLayoutPanelEmoji As FlowLayoutPanel
    Friend WithEvents ButtonSalva As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents btnClose As Button
    Friend WithEvents ComboBoxCategorieEmoji As ComboBox
    Friend WithEvents ButtonAggiornaEmoji As Button
    Friend WithEvents Label4 As Label
    Friend WithEvents PanelKeyboard As Panel
    Friend WithEvents Label5 As Label
End Class
