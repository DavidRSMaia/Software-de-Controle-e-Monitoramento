#Region " Descrição"

' procedimentos:
' gráfico
' arquivo
' webcam
' mov em tempo de execução

' váriaveis de processo:
' Temp interna (na amostra)
' temp externa para controle
' PH
' setpoint temp operação
' intervalo de coleta 

' Variáveis de atuadores: 
' agitador (delta-t de entre ligações e delta-T em funcionamento
' aquecimento( controlado)
' bomba dagua

#End Region

#Region " declarações iniciais"
Imports System
Imports System.IO
Imports System.Windows.Forms.Form
Imports System.IO.Ports
Imports System.Math
Imports System.Text.RegularExpressions
Imports System.Windows.Forms.DataVisualization.Charting
Imports System.Data
Imports System.Threading
#End Region

Public Class Principal

#Region "variaveis"
    Public botao_conf_parametro As Boolean = True
    Public tempfunca As String
    Public inttempfunca As String
    Public tempfuncb As String
    Public inttempfuncb As String
    Public setpoint As String
#End Region

#Region "váriaveis de controle dos atuadores"

    

    Public Shared setpoint_on_off As Double
    Public mm_temp_int(10) As Double
    Public mm_temp_ext(10) As Double
    Public mm_ph(10) As Double
    Public temp As Double
    Public setpoint_seg As Double
    Public tempo_agitacao As Integer = 30
    Public intervalo_entre_agitacoes As Integer = 180

    Public tempo_ciclagem_agua As Integer = 30
    Public intervalo_entre_ciclagem_agua As Integer = 180



#End Region

#Region " salvar arquivo"
    Public Shared savePath As String
    Public Shared savediretório_data As String
    Public n_linhas_por_arquivo As Integer = 1000
    'Public array_dados_concatenados_2D(99, 0) As String
    Public array_dados_concatenados() As String
    Public ind_file As Integer = 0 'divide o arquivo

#End Region


#Region " mov em execução"



    Private on_off_mov As Boolean = False


    Public Const WM_NCLBUTTONDOWN = &HA1
    Public Const HTCAPTION = 2
#End Region


    Private Sub TextBox1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress

        If Not (Char.IsDigit(e.KeyChar) OrElse Char.IsControl(e.KeyChar)) Then

            e.Handled = True

        End If

    End Sub

    Private Sub TextBox2_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress

        If Not (Char.IsDigit(e.KeyChar) OrElse Char.IsControl(e.KeyChar)) Then

            e.Handled = True

        End If

    End Sub

    Private Sub TextBox3_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox3.KeyPress

        If Not (Char.IsDigit(e.KeyChar) OrElse Char.IsControl(e.KeyChar)) Then

            e.Handled = True

        End If

    End Sub

    Private Sub TextBox4_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox4.KeyPress

        If Not (Char.IsDigit(e.KeyChar) OrElse Char.IsControl(e.KeyChar)) Then

            e.Handled = True

        End If

    End Sub

    Private Sub TextBox5_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox5.KeyPress

        If Not (Char.IsDigit(e.KeyChar) OrElse Char.IsControl(e.KeyChar)) Then

            e.Handled = True

        End If

    End Sub

    Private Sub Principal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'o codigo a seguir ja abre o form do tamanho da tela.
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub VisualizarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VisualizarToolStripMenuItem.Click
        visualizador.Show()

    End Sub

    Private Sub HScrollBar1_Scroll(sender As Object, e As ScrollEventArgs) Handles HScrollBar1.Scroll
        TextBox1.Text = HScrollBar1.Value
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub GroupBox2_Enter(sender As Object, e As EventArgs) Handles GroupBox2.Enter

    End Sub

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles TextBox5.TextChanged

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            CheckBox2.Checked = False
            TextBox2.Enabled = False
            TextBox3.Enabled = False
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            CheckBox1.Checked = False
            TextBox2.Enabled = True
            TextBox3.Enabled = True
        End If

    End Sub

    Private Sub CheckBox4_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox4.CheckedChanged
        If CheckBox4.Checked = True Then
            CheckBox3.Checked = False
            TextBox4.Enabled = False
            TextBox5.Enabled = False
        End If
    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked = True Then
            CheckBox4.Checked = False
            TextBox4.Enabled = True
            TextBox5.Enabled = True
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        If botao_conf_parametro Then

            If Double.TryParse(TextBox1.Text, setpoint_on_off) Then
                controle.pode_enviar_resistencia = True

            End If

            If CheckBox2.Checked Then ' automatico 

                controle.enviar_agitador = False
                controle.pode_enviar_agitador = True

                Integer.TryParse(TextBox2.Text, tempo_agitacao)
                Integer.TryParse(TextBox3.Text, intervalo_entre_agitacoes)

                Controle.Timer2.Interval = 1000
                controle.Timer2.Start()

            End If

            If CheckBox3.Checked Then


                controle.enviar_bomba_dagua = False
                controle.pode_enviar_bomba_dagua = True

                Integer.TryParse(TextBox5.Text, tempo_ciclagem_agua)
                Integer.TryParse(TextBox4.Text, intervalo_entre_ciclagem_agua)


                Controle.Timer3.Interval = 1000
                controle.Timer3.Start()

            End If

        End If


        controle.Show()

        If CheckBox1.Checked Then
            controle.Label2.Text = "Manual"
            controle.Label3.Visible = False
            controle.Label4.Visible = False
            controle.Label5.Visible = False
            controle.Label6.Visible = False
            controle.Label7.Visible = False
            controle.Label8.Visible = False
            controle.Button1.Visible = True
            controle.Button5.Visible = False

        End If

        If CheckBox2.Checked Then
            controle.Label2.Text = "Automático"
            tempfunca = TextBox2.Text
            controle.Label4.Text = tempfunca
            inttempfunca = TextBox3.Text
            controle.Label7.Text = inttempfunca
            controle.Label3.Visible = True
            controle.Label4.Visible = True
            controle.Label5.Visible = True
            controle.Label6.Visible = True
            controle.Label7.Visible = True
            controle.Label8.Visible = True
            controle.Button1.Visible = False
            controle.Button5.Visible = True

        End If

        If CheckBox4.Checked Then
            controle.Label15.Text = "Manual"
            controle.Label9.Visible = False
            controle.Label10.Visible = False
            controle.Label11.Visible = False
            controle.Label12.Visible = False
            controle.Label13.Visible = False
            controle.Label14.Visible = False
            controle.Button2.Visible = True
            controle.Button6.Visible = False
        End If

        If CheckBox3.Checked Then
            controle.Label15.Text = "Automático"
            tempfuncb = TextBox5.Text
            controle.Label13.Text = tempfuncb
            inttempfuncb = TextBox4.Text
            controle.Label10.Text = inttempfuncb
            controle.Label9.Visible = True
            controle.Label10.Visible = True
            controle.Label11.Visible = True
            controle.Label12.Visible = True
            controle.Label13.Visible = True
            controle.Label14.Visible = True
            controle.Button2.Visible = False
            controle.Button6.Visible = True
        End If

        setpoint = TextBox1.Text
        controle.Label21.Text = setpoint
        setpoint = TextBox1.Text
        controle.Label24.Text = setpoint


    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        HScrollBar1.Value = TextBox1.Text
    End Sub

    Private Sub TabelaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TabelaToolStripMenuItem.Click
        tabela.Show()
    End Sub

    Private Sub TempoRealToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TempoRealToolStripMenuItem.Click
        grafico.Show()
    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click

    End Sub

    Private Sub SalvarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SalvarToolStripMenuItem.Click
        funcoes.salvafile()
    End Sub

    Private Sub LocalOndeSalvarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LocalOndeSalvarToolStripMenuItem.Click


        FolderBrowserDialog1.Description = "Selecione uma pasta para realizar o Backup"
        FolderBrowserDialog1.RootFolder = Environment.SpecialFolder.MyComputer
        FolderBrowserDialog1.ShowNewFolderButton = True


        If FolderBrowserDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then

            

        End If
        
    End Sub

    Private Sub SaveFileDialog1_FileOk(sender As Object, e As ComponentModel.CancelEventArgs) Handles SaveFileDialog1.FileOk

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

    End Sub

    Private Sub GráficoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GráficoToolStripMenuItem.Click

    End Sub

    Private Sub Label15_Click(sender As Object, e As EventArgs) Handles Label15.Click

    End Sub

    Private Sub SairToolStripMenuItem_Click(sender As Object, e As EventArgs)

    End Sub
End Class