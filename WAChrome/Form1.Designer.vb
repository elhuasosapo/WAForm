<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        WebView2 = New Microsoft.Web.WebView2.WinForms.WebView2()
        Panel = New Panel()
        TableLayoutPanel1 = New TableLayoutPanel()
        TableLayoutPanel2 = New TableLayoutPanel()
        Label2 = New Label()
        Label1 = New Label()
        LblStato = New Label()
        TxtNumero = New TextBox()
        BtnConferma = New Button()
        BtnRifiuta = New Button()
        CType(WebView2, ComponentModel.ISupportInitialize).BeginInit()
        Panel.SuspendLayout()
        TableLayoutPanel1.SuspendLayout()
        TableLayoutPanel2.SuspendLayout()
        SuspendLayout()
        ' 
        ' WebView2
        ' 
        WebView2.AllowExternalDrop = True
        WebView2.CreationProperties = Nothing
        WebView2.DefaultBackgroundColor = Color.White
        WebView2.Dock = DockStyle.Fill
        WebView2.Location = New Point(3, 3)
        WebView2.Name = "WebView2"
        WebView2.Size = New Size(1178, 775)
        WebView2.TabIndex = 0
        WebView2.ZoomFactor = 1R
        ' 
        ' Panel
        ' 
        Panel.Controls.Add(TableLayoutPanel1)
        Panel.Controls.Add(TableLayoutPanel2)
        Panel.Dock = DockStyle.Fill
        Panel.Location = New Point(0, 0)
        Panel.Name = "Panel"
        Panel.Size = New Size(1184, 861)
        Panel.TabIndex = 1
        ' 
        ' TableLayoutPanel1
        ' 
        TableLayoutPanel1.ColumnCount = 1
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        TableLayoutPanel1.Controls.Add(WebView2, 0, 0)
        TableLayoutPanel1.Dock = DockStyle.Fill
        TableLayoutPanel1.Location = New Point(0, 80)
        TableLayoutPanel1.Name = "TableLayoutPanel1"
        TableLayoutPanel1.RowCount = 1
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Absolute, 20F))
        TableLayoutPanel1.Size = New Size(1184, 781)
        TableLayoutPanel1.TabIndex = 1
        ' 
        ' TableLayoutPanel2
        ' 
        TableLayoutPanel2.BackColor = Color.Transparent
        TableLayoutPanel2.ColumnCount = 4
        TableLayoutPanel2.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 13.33333F))
        TableLayoutPanel2.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 13.333334F))
        TableLayoutPanel2.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 33.3333359F))
        TableLayoutPanel2.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 40.0000038F))
        TableLayoutPanel2.Controls.Add(Label2, 3, 0)
        TableLayoutPanel2.Controls.Add(Label1, 2, 0)
        TableLayoutPanel2.Controls.Add(LblStato, 3, 1)
        TableLayoutPanel2.Controls.Add(TxtNumero, 2, 1)
        TableLayoutPanel2.Controls.Add(BtnConferma, 0, 1)
        TableLayoutPanel2.Controls.Add(BtnRifiuta, 1, 1)
        TableLayoutPanel2.Dock = DockStyle.Top
        TableLayoutPanel2.Location = New Point(0, 0)
        TableLayoutPanel2.MinimumSize = New Size(0, 80)
        TableLayoutPanel2.Name = "TableLayoutPanel2"
        TableLayoutPanel2.RowCount = 2
        TableLayoutPanel2.RowStyles.Add(New RowStyle(SizeType.Percent, 50F))
        TableLayoutPanel2.RowStyles.Add(New RowStyle(SizeType.Percent, 50F))
        TableLayoutPanel2.Size = New Size(1184, 80)
        TableLayoutPanel2.TabIndex = 1
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Dock = DockStyle.Fill
        Label2.ForeColor = Color.Gainsboro
        Label2.Location = New Point(711, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(470, 40)
        Label2.TabIndex = 5
        Label2.Text = "Situazione/stato invio messaggio"
        Label2.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Dock = DockStyle.Fill
        Label1.ForeColor = Color.Gainsboro
        Label1.Location = New Point(317, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(388, 40)
        Label1.TabIndex = 3
        Label1.Text = "Numero destinatario (es. 393401234567)"
        Label1.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LblStato
        ' 
        LblStato.AutoSize = True
        LblStato.Dock = DockStyle.Fill
        LblStato.ForeColor = Color.White
        LblStato.Location = New Point(711, 40)
        LblStato.Name = "LblStato"
        LblStato.Size = New Size(470, 40)
        LblStato.TabIndex = 4
        LblStato.Text = "Label2"
        LblStato.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' TxtNumero
        ' 
        TxtNumero.BackColor = Color.White
        TxtNumero.BorderStyle = BorderStyle.FixedSingle
        TxtNumero.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        TxtNumero.Location = New Point(317, 43)
        TxtNumero.Name = "TxtNumero"
        TxtNumero.Size = New Size(388, 29)
        TxtNumero.TabIndex = 2
        TxtNumero.TextAlign = HorizontalAlignment.Center
        ' 
        ' BtnConferma
        ' 
        BtnConferma.BackColor = Color.Transparent
        BtnConferma.FlatAppearance.BorderColor = Color.FromArgb(CByte(0), CByte(192), CByte(0))
        BtnConferma.FlatStyle = FlatStyle.Flat
        BtnConferma.ForeColor = Color.Gainsboro
        BtnConferma.Location = New Point(3, 43)
        BtnConferma.Name = "BtnConferma"
        BtnConferma.Size = New Size(151, 34)
        BtnConferma.TabIndex = 0
        BtnConferma.Text = "Conferma"
        BtnConferma.UseVisualStyleBackColor = False
        ' 
        ' BtnRifiuta
        ' 
        BtnRifiuta.FlatAppearance.BorderColor = Color.Red
        BtnRifiuta.FlatStyle = FlatStyle.Flat
        BtnRifiuta.ForeColor = Color.Gainsboro
        BtnRifiuta.Location = New Point(160, 43)
        BtnRifiuta.Name = "BtnRifiuta"
        BtnRifiuta.Size = New Size(151, 34)
        BtnRifiuta.TabIndex = 1
        BtnRifiuta.Text = "Rifiuta"
        BtnRifiuta.UseVisualStyleBackColor = True
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(7F, 17F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(33), CByte(33), CByte(33))
        ClientSize = New Size(1184, 861)
        Controls.Add(Panel)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Name = "Form1"
        StartPosition = FormStartPosition.CenterScreen
        Text = "WAForm"
        CType(WebView2, ComponentModel.ISupportInitialize).EndInit()
        Panel.ResumeLayout(False)
        TableLayoutPanel1.ResumeLayout(False)
        TableLayoutPanel2.ResumeLayout(False)
        TableLayoutPanel2.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents WebView2 As Microsoft.Web.WebView2.WinForms.WebView2
    Friend WithEvents Panel As Panel
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents BtnConferma As Button
    Friend WithEvents BtnRifiuta As Button
    Friend WithEvents TxtNumero As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents LblStato As Label
    Friend WithEvents Label2 As Label

End Class
