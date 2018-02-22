
#Region " declarações iniciais"
Imports System
Imports System.IO
Imports System.Windows.Forms.Form
Imports System.IO.Ports
Imports System.Math
Imports System.Data
Imports System.Threading
#End Region


Public Class Controle

    Private addDataRunner As Thread
    Public Delegate Sub AddDataDelegate()
    Public addDataDel As AddDataDelegate
    Public ponto_atual As Integer

    Public ponto_volatil_Seg As Integer
    Public ponto_volatil_Min As Integer
    Public ponto_fixo_Min As Integer
    Public ponto_volatil_Hora As Integer
    Public ponto_fixo_Hora As Integer
    Public ponto_volatil_Dia As Integer
    Public ponto_fixo_Dia As Integer
    Public adc_atual As Integer

    Public TimeAtual() As Date

    

    Public mm_temp_int(4) As Double
    Public mm_temp_ext(4) As Double
    Public mm_ph(4) As Double
    Public mm_metano(4) As Double
    Public mm_dioxDeCarb(4) As Double
    Public mm_hidrogenio(4) As Double
    Public mm_sulfeto(4) As Double

    Public somaSeg As Double
    Public somaMin As Double
    Public somaHora As Double
    Public somaDia As Double

    Public pH_volt As Double
    Public a_pH As Double
    Public b_pH As Double

    Public Rs_atual_CH4 As Double
    Public sensor_volt_CH4 As Double
    Public ppm_log_CH4 As Double
    Public Ro_CH4 As Double
    Public a_CH4 As Double
    Public b_CH4 As Double

    Public Rs_atual_CO2 As Double
    Public sensor_volt_CO2 As Double
    Public ppm_log_CO2 As Double
    Public Ro_CO2 As Double
    Public a_CO2 As Double
    Public b_CO2 As Double

    Public Rs_atual_H2 As Double
    Public sensor_volt_H2 As Double
    Public ppm_log_H2 As Double
    Public Ro_H2 As Double
    Public a_H2 As Double
    Public b_H2 As Double

    Public Rs_atual_H2S As Double
    Public sensor_volt_H2S As Double
    Public ppm_log_H2S As Double
    Public Ro_H2S As Double
    Public a_H2S As Double
    Public b_H2S As Double



    Public Shared on_off_experimento As Boolean = True
    Public phApresent() As Double
    Public tempExtApresent() As Double
    Public tempIntApresent() As Double
    Public metanoApresent() As Double
    Public dioxApresent() As Double
    Public hidrogApresent() As Double
    Public sulfApresent() As Double
    Public Shared pode_enviar_agitador As Boolean
    Public Shared pode_enviar_resistencia As Boolean
    Public Shared pode_enviar_bomba_dagua As Boolean
    Public Shared pode_enviar_seg_resistencia As Boolean
    Public Shared enviar_agitador As Boolean
    Public Shared enviar_resistencia As Boolean
    Public Shared enviar_bomba_dagua As Boolean
    Public Shared enviar_seg_resistencia As Boolean

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim addDataThreadStart As New ThreadStart(AddressOf AddDataThreadLoop)
        addDataRunner = New Thread(addDataThreadStart)
        addDataDel = New AddDataDelegate(AddressOf AddData)


    End Sub

#Region "implementação do thread"

    Private Sub AddDataThreadLoop()
        While True
            addDataDel()
        End While
    End Sub

    Public Sub AddData()
        ponto_atual += 1



        If Principal.SerialPort1.IsOpen Then

            For i As Integer = 5 To 11

                Dim origin As String

                If Not Principal.SerialPort1.IsOpen Then Exit For

                Principal.SerialPort1.DiscardInBuffer()
                OutData(i, 0)
                System.Threading.Thread.Sleep(100)

                If Principal.SerialPort1.IsOpen Then
                    origin = Principal.SerialPort1.ReadExisting

                Else : origin = "0"
                End If

                If (IsNumeric(origin)) And adc_atual < 1024 Then
                    If Principal.SerialPort1.IsOpen = True Then Double.TryParse(origin, adc_atual)
                Else
                    origin = "0"
                    Double.TryParse(origin, adc_atual)
                End If

                If i = 5 Then
                    Double.TryParse(origin, adc_atual)
                    pH_volt = ((adc_atual * 5) / 1023)
                    mm_ph(4) = mm_ph(3)
                    mm_ph(3) = mm_ph(2)
                    mm_ph(2) = mm_ph(1)
                    mm_ph(1) = mm_ph(0)
                    mm_ph(0) = ((a_pH * pH_volt) + b_pH)

                    phApresent(ponto_atual) = (mm_ph(0) + mm_ph(1) + mm_ph(2) + mm_ph(3) + mm_ph(4)) / 5

                    If phApresent(ponto_atual) <> 0 Then
                        lblph.Text = Math.Round(phApresent(ponto_atual), 2)
                    End If
                End If

                If i = 6 Then

                    mm_temp_ext(4) = mm_temp_ext(3)
                    mm_temp_ext(3) = mm_temp_ext(2)
                    mm_temp_ext(2) = mm_temp_ext(1)
                    mm_temp_ext(1) = mm_temp_ext(0)
                    mm_temp_ext(0) = ((adc_atual * 150) / 307)

                    tempExtApresent(ponto_atual) = (mm_temp_ext(0) + mm_temp_ext(1) + mm_temp_ext(2) + mm_temp_ext(3) + mm_temp_ext(4)) / 5

                    If tempExtApresent(ponto_atual) <> 0 Then
                        Label27.Text = Math.Round(tempExtApresent(ponto_atual), 1)
                    End If

                End If


                If i = 7 Then

                    mm_temp_int(4) = mm_temp_int(3)
                    mm_temp_int(3) = mm_temp_int(2)
                    mm_temp_int(2) = mm_temp_int(1)
                    mm_temp_int(1) = mm_temp_int(0)
                    mm_temp_int(0) = ((adc_atual * 150) / 307)

                    tempIntApresent(ponto_atual) = (mm_temp_int(0) + mm_temp_int(1) + mm_temp_int(2) + mm_temp_int(3) + mm_temp_int(4)) / 5

                    If tempIntApresent(ponto_atual) <> 0 Then
                        Label18.Text = Math.Round(tempIntApresent(ponto_atual), 1)
                    End If

                End If

                If i = 8 Then
                    Double.TryParse(origin, adc_atual)
                    sensor_volt_CH4 = ((adc_atual * 5) / 1023)
                    Rs_atual_CH4 = (((5 * 10.0) / sensor_volt_CH4) - 10.0)
                    ppm_log_CH4 = ((Log10(Rs_atual_CH4 / Ro_CH4) - b_CH4) / a_CH4)
                    mm_metano(4) = mm_metano(3)
                    mm_metano(3) = mm_metano(2)
                    mm_metano(2) = mm_metano(1)
                    mm_metano(1) = mm_metano(0)
                    mm_metano(0) = Math.Pow(10, ppm_log_CH4)

                    metanoApresent(ponto_atual) = (mm_metano(0) + mm_metano(1) + mm_metano(2) + mm_metano(3) + mm_metano(4)) / 5

                    If metanoApresent(ponto_atual) <> 0 Then
                        lblmetano.Text = Math.Round(metanoApresent(ponto_atual), 0)
                    End If

                End If

                If i = 9 Then
                    Double.TryParse(origin, adc_atual)
                    sensor_volt_CO2 = ((adc_atual * 5) / 1023)
                    Rs_atual_CO2 = (((5 * 10.0) / sensor_volt_CO2) - 10.0)
                    ppm_log_CO2 = ((Log10(Rs_atual_CO2 / Ro_CO2) - b_CO2) / a_CO2)
                    mm_dioxDeCarb(4) = mm_dioxDeCarb(3)
                    mm_dioxDeCarb(3) = mm_dioxDeCarb(2)
                    mm_dioxDeCarb(2) = mm_dioxDeCarb(1)
                    mm_dioxDeCarb(1) = mm_dioxDeCarb(0)
                    mm_dioxDeCarb(0) = Math.Pow(10, ppm_log_CO2)

                    dioxApresent(ponto_atual) = (mm_dioxDeCarb(0) + mm_dioxDeCarb(1) + mm_dioxDeCarb(2) + mm_dioxDeCarb(3) + mm_dioxDeCarb(4)) / 5

                    If dioxApresent(ponto_atual) <> 0 Then
                        lbldioxidoDeCarbono.Text = Math.Round(dioxApresent(ponto_atual), 0)
                    End If

                End If

                If i = 10 Then
                    Double.TryParse(origin, adc_atual)
                    sensor_volt_H2 = ((adc_atual * 5) / 1023)
                    Rs_atual_H2 = (((5 * 10.0) / sensor_volt_H2) - 10.0)
                    ppm_log_H2 = ((Log10(Rs_atual_H2 / Ro_H2) - b_H2) / a_H2)
                    mm_hidrogenio(4) = mm_hidrogenio(3)
                    mm_hidrogenio(3) = mm_hidrogenio(2)
                    mm_hidrogenio(2) = mm_hidrogenio(1)
                    mm_hidrogenio(1) = mm_hidrogenio(0)
                    mm_hidrogenio(0) = Math.Pow(10, ppm_log_H2)

                    hidrogApresent(ponto_atual) = (mm_hidrogenio(0) + mm_hidrogenio(1) + mm_hidrogenio(2) + mm_hidrogenio(3) + mm_hidrogenio(4)) / 5
                    If hidrogApresent(ponto_atual) <> 0 Then
                        lblhidrogenio.Text = Math.Round(hidrogApresent(ponto_atual), 0)
                    End If
                End If

                If i = 11 Then
                    Double.TryParse(origin, adc_atual)
                    sensor_volt_H2S = ((adc_atual * 5) / 1023)
                    Rs_atual_H2S = (((5 * 10.0) / sensor_volt_H2S) - 10.0)
                    ppm_log_H2S = ((Log10(Rs_atual_H2S / Ro_H2S) - b_H2S) / a_H2S)
                    mm_sulfeto(4) = mm_sulfeto(3)
                    mm_sulfeto(3) = mm_sulfeto(2)
                    mm_sulfeto(2) = mm_sulfeto(1)
                    mm_sulfeto(1) = mm_sulfeto(0)
                    mm_sulfeto(0) = Math.Pow(10, ppm_log_H2S)

                    sulfApresent(ponto_atual) = (mm_sulfeto(0) + mm_sulfeto(1) + mm_sulfeto(2) + mm_sulfeto(3) + mm_sulfeto(4)) / 5
                    If sulfApresent(ponto_atual) <> 0 Then
                        lblsulfeto.Text = Math.Round(sulfApresent(ponto_atual), 0)
                    End If
                End If

            Next

            For i As Integer = 1 To 4

                If i = 1 Then
                    If pode_enviar_agitador = True Then
                        Principal.SerialPort1.DiscardInBuffer()
                        If enviar_agitador = True Then
                            OutData(i, 1)
                        Else
                            OutData(i, 0)
                        End If
                        System.Threading.Thread.Sleep(50)
                    Else
                        System.Threading.Thread.Sleep(50)
                    End If

                End If

                If i = 2 Then
                    If pode_enviar_bomba_dagua = True Then
                        Principal.SerialPort1.DiscardInBuffer()
                        If enviar_bomba_dagua = True Then
                            OutData(i, 1)
                        Else
                            OutData(i, 0)
                        End If
                        System.Threading.Thread.Sleep(50)
                    Else
                        System.Threading.Thread.Sleep(50)
                    End If

                End If

                If i = 3 Then
                    If pode_enviar_resistencia = True Then

                        Dim resultado As Double
                        resultado = (Principal.setpoint_on_off * 0.01)
                        'If enviar_resistencia = True Then
                        If (tempIntApresent(ponto_atual)) <= (Principal.setpoint_on_off - resultado) Then
                            OutData(i, 1)


                        ElseIf (tempIntApresent(ponto_atual)) >= (Principal.setpoint_on_off + resultado) Then
                            OutData(i, 0)


                        ElseIf (tempIntApresent(ponto_atual)) >= (Principal.setpoint_on_off - resultado) And (tempIntApresent(ponto_atual) \ 1) <= (Principal.setpoint_on_off + resultado) Then



                        Else
                        End If
                    End If
                    System.Threading.Thread.Sleep(50)
                Else
                    System.Threading.Thread.Sleep(50)
                End If


                If i = 4 Then
                    If pode_enviar_seg_resistencia = True Then
                        Principal.SerialPort1.DiscardInBuffer()
                        Dim resultado_seg As Double
                        resultado_seg = (Principal.setpoint_seg * 0.01)
                        If enviar_resistencia = True Then
                            If (tempIntApresent(ponto_atual)) >= (Principal.setpoint_seg + resultado_seg) Then
                                OutData(i, 1)


                            ElseIf (tempIntApresent(ponto_atual)) <= (Principal.setpoint_seg - resultado_seg) Then
                                OutData(i, 0)


                            ElseIf (tempIntApresent(ponto_atual)) >= (Principal.setpoint_seg - resultado_seg) And (tempIntApresent(ponto_atual)) <= (Principal.setpoint_seg + resultado_seg) Then

                            End If

                            System.Threading.Thread.Sleep(50)
                        Else
                            System.Threading.Thread.Sleep(50)
                        End If

                    End If
                End If
            Next

        End If

    End Sub

    Public Sub OutData(ByVal comd As Integer, ByRef valor As Integer)


        If Principal.SerialPort1.IsOpen Then
            Select Case comd

                Case 1

                    Dim dac0s As String
                    dac0s = Convert.ToString(valor)
                    Principal.SerialPort1.Write("0,1," & dac0s & vbCr)

                Case 2

                    Dim dac1s As String
                    dac1s = Convert.ToString(valor)
                    Principal.SerialPort1.Write("0,2," & dac1s & vbCr)

                Case 3

                    Dim dac2s As String
                    dac2s = Convert.ToString(valor)
                    Principal.SerialPort1.Write("0,3," & dac2s & vbCr)

                Case 4

                    Dim dac3s As String
                    dac3s = Convert.ToString(valor)
                    Principal.SerialPort1.Write("0,4," & dac3s & vbCr)

                Case 5

                    Principal.SerialPort1.Write("0,5,0" & vbCr)

                Case 6

                    Principal.SerialPort1.Write("0,5,1" & vbCr)

                Case 7

                    Principal.SerialPort1.Write("0,5,2" & vbCr)

                Case 8

                    Principal.SerialPort1.Write("0,5,3" & vbCr)

                Case 9

                    Principal.SerialPort1.Write("0,5,4" & vbCr)

                Case 10

                    Principal.SerialPort1.Write("0,5,5" & vbCr)

                Case 11

                    Principal.SerialPort1.Write("0,5,6" & vbCr)

                Case Else

            End Select

        End If

    End Sub

#End Region

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles BtnExperimento.Click

        If on_off_experimento Then

            on_off_experimento = False
            BtnExperimento.Text = "Finalizar"

            If Not Principal.SerialPort1.IsOpen Then Principal.SerialPort1.Open()

            If addDataRunner.IsAlive = True Then
                addDataRunner.Resume()
            Else
                addDataRunner.Start()
            End If
            Timer1.Start()
        Else

            If addDataRunner.IsAlive = True Then
                addDataRunner.Suspend()
            End If

            on_off_experimento = True
            BtnExperimento.Text = "Iniciar"
            Timer1.Stop()

        End If

    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        addDataDel()
    End Sub
End Class