<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class visualizador
    Inherits System.Windows.Forms.Form

    'Descartar substituições de formulário para limpar a lista de componentes.
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

    'Exigido pelo Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'OBSERVAÇÃO: O procedimento a seguir é exigido pelo Windows Form Designer
    'Ele pode ser modificado usando o Windows Form Designer.  
    'Não o modifique usando o editor de códigos.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btniniciar = New System.Windows.Forms.Button()
        Me.btnparar = New System.Windows.Forms.Button()
        Me.btnsalvar = New System.Windows.Forms.Button()
        Me.lstDispositivos = New System.Windows.Forms.ListBox()
        Me.picCaptura = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.picCaptura, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btniniciar
        '
        Me.btniniciar.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btniniciar.Location = New System.Drawing.Point(25, 536)
        Me.btniniciar.Name = "btniniciar"
        Me.btniniciar.Size = New System.Drawing.Size(75, 43)
        Me.btniniciar.TabIndex = 1
        Me.btniniciar.Text = "Iniciar Vizualização"
        Me.btniniciar.UseVisualStyleBackColor = True
        '
        'btnparar
        '
        Me.btnparar.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnparar.Location = New System.Drawing.Point(122, 536)
        Me.btnparar.Name = "btnparar"
        Me.btnparar.Size = New System.Drawing.Size(75, 43)
        Me.btnparar.TabIndex = 2
        Me.btnparar.Text = "Parar Vizualização"
        Me.btnparar.UseVisualStyleBackColor = True
        '
        'btnsalvar
        '
        Me.btnsalvar.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnsalvar.Location = New System.Drawing.Point(222, 536)
        Me.btnsalvar.Name = "btnsalvar"
        Me.btnsalvar.Size = New System.Drawing.Size(75, 43)
        Me.btnsalvar.TabIndex = 3
        Me.btnsalvar.Text = "Salvar Imagem"
        Me.btnsalvar.UseVisualStyleBackColor = True
        '
        'lstDispositivos
        '
        Me.lstDispositivos.FormattingEnabled = True
        Me.lstDispositivos.Location = New System.Drawing.Point(639, 536)
        Me.lstDispositivos.Name = "lstDispositivos"
        Me.lstDispositivos.Size = New System.Drawing.Size(237, 43)
        Me.lstDispositivos.TabIndex = 4
        '
        'picCaptura
        '
        Me.picCaptura.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.picCaptura.BackColor = System.Drawing.SystemColors.ControlLight
        Me.picCaptura.Location = New System.Drawing.Point(5, 37)
        Me.picCaptura.Name = "picCaptura"
        Me.picCaptura.Size = New System.Drawing.Size(879, 482)
        Me.picCaptura.TabIndex = 5
        Me.picCaptura.TabStop = False
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(365, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(145, 22)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Biodigestor 02"
        '
        'visualizador
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.HotTrack
        Me.ClientSize = New System.Drawing.Size(888, 591)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.picCaptura)
        Me.Controls.Add(Me.lstDispositivos)
        Me.Controls.Add(Me.btnsalvar)
        Me.Controls.Add(Me.btnparar)
        Me.Controls.Add(Me.btniniciar)
        Me.Name = "visualizador"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "vizualizador"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.picCaptura, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

End Sub
    Friend WithEvents btniniciar As System.Windows.Forms.Button
    Friend WithEvents btnparar As System.Windows.Forms.Button
    Friend WithEvents btnsalvar As System.Windows.Forms.Button
    Friend WithEvents lstDispositivos As System.Windows.Forms.ListBox
    Friend WithEvents picCaptura As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
