<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.TextBoxName = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.PictureBoxFingerprint = New System.Windows.Forms.PictureBox()
        Me.ComboBoxScanners = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LabelInfo = New System.Windows.Forms.Label()
        Me.LabelTime = New System.Windows.Forms.Label()
        CType(Me.PictureBoxFingerprint, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TextBoxName
        '
        Me.TextBoxName.BackColor = System.Drawing.SystemColors.Window
        Me.TextBoxName.Location = New System.Drawing.Point(200, 63)
        Me.TextBoxName.Name = "TextBoxName"
        Me.TextBoxName.ReadOnly = True
        Me.TextBoxName.Size = New System.Drawing.Size(154, 25)
        Me.TextBoxName.TabIndex = 11
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(200, 43)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(43, 17)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "Name"
        '
        'PictureBoxFingerprint
        '
        Me.PictureBoxFingerprint.BackColor = System.Drawing.Color.White
        Me.PictureBoxFingerprint.Location = New System.Drawing.Point(11, 43)
        Me.PictureBoxFingerprint.Name = "PictureBoxFingerprint"
        Me.PictureBoxFingerprint.Size = New System.Drawing.Size(183, 247)
        Me.PictureBoxFingerprint.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBoxFingerprint.TabIndex = 8
        Me.PictureBoxFingerprint.TabStop = False
        '
        'ComboBoxScanners
        '
        Me.ComboBoxScanners.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxScanners.FormattingEnabled = True
        Me.ComboBoxScanners.Location = New System.Drawing.Point(68, 12)
        Me.ComboBoxScanners.Name = "ComboBoxScanners"
        Me.ComboBoxScanners.Size = New System.Drawing.Size(475, 25)
        Me.ComboBoxScanners.TabIndex = 7
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(54, 17)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Scanner"
        '
        'LabelInfo
        '
        Me.LabelInfo.AutoSize = True
        Me.LabelInfo.ForeColor = System.Drawing.Color.Red
        Me.LabelInfo.Location = New System.Drawing.Point(200, 91)
        Me.LabelInfo.Name = "LabelInfo"
        Me.LabelInfo.Size = New System.Drawing.Size(69, 17)
        Me.LabelInfo.TabIndex = 12
        Me.LabelInfo.Text = "Scanning..."
        '
        'LabelTime
        '
        Me.LabelTime.AutoSize = True
        Me.LabelTime.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelTime.Location = New System.Drawing.Point(360, 68)
        Me.LabelTime.Name = "LabelTime"
        Me.LabelTime.Size = New System.Drawing.Size(28, 15)
        Me.LabelTime.TabIndex = 13
        Me.LabelTime.Text = "n/a"
        '
        'FormMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(555, 301)
        Me.Controls.Add(Me.LabelTime)
        Me.Controls.Add(Me.LabelInfo)
        Me.Controls.Add(Me.TextBoxName)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.PictureBoxFingerprint)
        Me.Controls.Add(Me.ComboBoxScanners)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.Name = "FormMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Fingerprint Matcher"
        CType(Me.PictureBoxFingerprint, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TextBoxName As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents PictureBoxFingerprint As System.Windows.Forms.PictureBox
    Friend WithEvents ComboBoxScanners As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents LabelInfo As System.Windows.Forms.Label
    Friend WithEvents LabelTime As System.Windows.Forms.Label

End Class
