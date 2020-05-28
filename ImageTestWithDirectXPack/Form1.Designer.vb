<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
      Me.pImages = New System.Windows.Forms.Panel()
      Me.Panel2 = New System.Windows.Forms.Panel()
      Me.Button2 = New System.Windows.Forms.Button()
      Me.Button1 = New System.Windows.Forms.Button()
      Me.ListBox1 = New System.Windows.Forms.ListBox()
      Me.bUpdate = New System.Windows.Forms.Button()
      Me.Panel2.SuspendLayout
      Me.SuspendLayout
      '
      'pImages
      '
      Me.pImages.Dock = System.Windows.Forms.DockStyle.Fill
      Me.pImages.Location = New System.Drawing.Point(200, 0)
      Me.pImages.Name = "pImages"
      Me.pImages.Size = New System.Drawing.Size(600, 450)
      Me.pImages.TabIndex = 0
      '
      'Panel2
      '
      Me.Panel2.Controls.Add(Me.ListBox1)
      Me.Panel2.Controls.Add(Me.Button2)
      Me.Panel2.Controls.Add(Me.Button1)
      Me.Panel2.Controls.Add(Me.bUpdate)
      Me.Panel2.Dock = System.Windows.Forms.DockStyle.Left
      Me.Panel2.Location = New System.Drawing.Point(0, 0)
      Me.Panel2.Name = "Panel2"
      Me.Panel2.Size = New System.Drawing.Size(200, 450)
      Me.Panel2.TabIndex = 1
      '
      'Button2
      '
      Me.Button2.Dock = System.Windows.Forms.DockStyle.Top
      Me.Button2.Location = New System.Drawing.Point(0, 46)
      Me.Button2.Name = "Button2"
      Me.Button2.Size = New System.Drawing.Size(200, 23)
      Me.Button2.TabIndex = 2
      Me.Button2.Text = "- px"
      Me.Button2.UseVisualStyleBackColor = true
      '
      'Button1
      '
      Me.Button1.Dock = System.Windows.Forms.DockStyle.Top
      Me.Button1.Location = New System.Drawing.Point(0, 23)
      Me.Button1.Name = "Button1"
      Me.Button1.Size = New System.Drawing.Size(200, 23)
      Me.Button1.TabIndex = 0
      Me.Button1.Text = "+ px"
      Me.Button1.UseVisualStyleBackColor = true
      '
      'ListBox1
      '
      Me.ListBox1.Dock = System.Windows.Forms.DockStyle.Fill
      Me.ListBox1.FormattingEnabled = true
      Me.ListBox1.Location = New System.Drawing.Point(0, 69)
      Me.ListBox1.Name = "ListBox1"
      Me.ListBox1.Size = New System.Drawing.Size(200, 381)
      Me.ListBox1.TabIndex = 1
      '
      'bUpdate
      '
      Me.bUpdate.Dock = System.Windows.Forms.DockStyle.Top
      Me.bUpdate.Location = New System.Drawing.Point(0, 0)
      Me.bUpdate.Name = "bUpdate"
      Me.bUpdate.Size = New System.Drawing.Size(200, 23)
      Me.bUpdate.TabIndex = 0
      Me.bUpdate.Text = "Update"
      Me.bUpdate.UseVisualStyleBackColor = true
      '
      'Form1
      '
      Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
      Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
      Me.ClientSize = New System.Drawing.Size(800, 450)
      Me.Controls.Add(Me.pImages)
      Me.Controls.Add(Me.Panel2)
      Me.Name = "Form1"
      Me.Text = "Form1"
      Me.Panel2.ResumeLayout(false)
      Me.ResumeLayout(false)

End Sub

    Friend WithEvents pImages As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents ListBox1 As ListBox
    Friend WithEvents bUpdate As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button1 As Button
End Class
