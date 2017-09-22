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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ComboBoxScanners = New System.Windows.Forms.ComboBox()
        Me.PictureBoxFingerprint = New System.Windows.Forms.PictureBox()
        Me.PictureBoxFeatures = New System.Windows.Forms.PictureBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxName = New System.Windows.Forms.TextBox()
        Me.ButtonSave = New System.Windows.Forms.Button()
        Me.LabelInfo = New System.Windows.Forms.Label()
        CType(Me.PictureBoxFingerprint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxFeatures, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(54, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Scanner"
        '
        'ComboBoxScanners
        '
        Me.ComboBoxScanners.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxScanners.FormattingEnabled = True
        Me.ComboBoxScanners.Location = New System.Drawing.Point(72, 12)
        Me.ComboBoxScanners.Name = "ComboBoxScanners"
        Me.ComboBoxScanners.Size = New System.Drawing.Size(475, 25)
        Me.ComboBoxScanners.TabIndex = 1
        '
        'PictureBoxFingerprint
        '
        Me.PictureBoxFingerprint.BackColor = System.Drawing.Color.White
        Me.PictureBoxFingerprint.Location = New System.Drawing.Point(15, 43)
        Me.PictureBoxFingerprint.Name = "PictureBoxFingerprint"
        Me.PictureBoxFingerprint.Size = New System.Drawing.Size(183, 247)
        Me.PictureBoxFingerprint.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBoxFingerprint.TabIndex = 2
        Me.PictureBoxFingerprint.TabStop = False
        '
        'PictureBoxFeatures
        '
        Me.PictureBoxFeatures.BackColor = System.Drawing.Color.White
        Me.PictureBoxFeatures.Location = New System.Drawing.Point(204, 43)
        Me.PictureBoxFeatures.Name = "PictureBoxFeatures"
        Me.PictureBoxFeatures.Size = New System.Drawing.Size(183, 247)
        Me.PictureBoxFeatures.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBoxFeatures.TabIndex = 3
        Me.PictureBoxFeatures.TabStop = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(393, 43)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(43, 17)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Name"
        '
        'TextBoxName
        '
        Me.TextBoxName.Location = New System.Drawing.Point(393, 63)
        Me.TextBoxName.Name = "TextBoxName"
        Me.TextBoxName.Size = New System.Drawing.Size(154, 25)
        Me.TextBoxName.TabIndex = 5
        '
        'ButtonSave
        '
        Me.ButtonSave.Location = New System.Drawing.Point(472, 94)
        Me.ButtonSave.Name = "ButtonSave"
        Me.ButtonSave.Size = New System.Drawing.Size(75, 26)
        Me.ButtonSave.TabIndex = 6
        Me.ButtonSave.Text = "Save"
        Me.ButtonSave.UseVisualStyleBackColor = True
        '
        'LabelInfo
        '
        Me.LabelInfo.AutoSize = True
        Me.LabelInfo.Location = New System.Drawing.Point(35, 293)
        Me.LabelInfo.Name = "LabelInfo"
        Me.LabelInfo.Size = New System.Drawing.Size(143, 17)
        Me.LabelInfo.TabIndex = 7
        Me.LabelInfo.Text = "Press SPACE to analyze"
        '
        'FormMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(555, 316)
        Me.Controls.Add(Me.LabelInfo)
        Me.Controls.Add(Me.ButtonSave)
        Me.Controls.Add(Me.TextBoxName)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.PictureBoxFeatures)
        Me.Controls.Add(Me.PictureBoxFingerprint)
        Me.Controls.Add(Me.ComboBoxScanners)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.Name = "FormMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Fingerprint Manager"
        CType(Me.PictureBoxFingerprint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxFeatures, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxScanners As System.Windows.Forms.ComboBox
    Friend WithEvents PictureBoxFingerprint As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBoxFeatures As System.Windows.Forms.PictureBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBoxName As System.Windows.Forms.TextBox
    Friend WithEvents ButtonSave As System.Windows.Forms.Button
    Friend WithEvents LabelInfo As System.Windows.Forms.Label

End Class
