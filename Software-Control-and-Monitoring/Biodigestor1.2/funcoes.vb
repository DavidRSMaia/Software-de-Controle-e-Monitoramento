#Region " declarações iniciais"
Imports System.Runtime.InteropServices ' webcam
Imports System
Imports System.IO
Imports System.Windows.Forms.Form
Imports System.IO.Ports
Imports System.Math
Imports System.Text.RegularExpressions ' essa system pode dar problema de versão do windows 
'Imports System.Windows.Forms.DataVisualization.Charting
Imports System.Data
Imports System.Threading 'vindo do grafico
#End Region

Public Class funcoes


    'Public Shared savePath As String
    'Public Shared savediretório_data As String
    'Public n_linhas_por_arquivo As Double = 1000
    ''Public array_dados_concatenados_2D(99, 0) As String
    'Public array_dados_concatenados() As String
    'Public ind_file As Integer = 0 'divide o arquivo


    'sub salva arquivos 
    Public Shared Sub salvafile()
        Dim ind_file_cont As Integer

        Dim path_data As String = principal.SaveFileDialog1.FileName & ".txt"
        'Dim path_data As String = savediretório_data & SaveFileDialog1.FileName & ".txt"

        If controle.ponto_atual <= Principal.n_linhas_por_arquivo Then


            ReDim Principal.array_dados_concatenados(controle.ponto_atual)
            'concatenar variáveis 
            For i As Integer = 1 To controle.ponto_atual 'adc_0.Length - 1


                ' arrumar txt   
                'For z As Integer = 0 To 3 - _
                '   Convert.ToString(vatual(i)).Length
                '    If Convert.ToString(vatual(i)).Length >= 3 Then Exit For
                '    array_dados_concatenados(i) = _
                '    String.Concat(array_dados_concatenados(i), "0")

                'Next



                Principal.array_dados_concatenados(i) = String.Concat(Principal.array_dados_concatenados(i), _
                           Convert.ToString(controle.vatual(i)))

                Principal.array_dados_concatenados(i) = String.Concat(Principal.array_dados_concatenados(i), "   ")

                'Principal.array_dados_concatenados(i) = String.Concat(Principal.array_dados_concatenados(i), _
                'Convert.ToString(controle.ph(i)))

                Principal.array_dados_concatenados(i) = String.Concat(Principal.array_dados_concatenados(i), "   ")

                Principal.array_dados_concatenados(i) = String.Concat(Principal.array_dados_concatenados(i), _
                            Convert.ToString(controle.temp_externa(i)))

                Principal.array_dados_concatenados(i) = String.Concat(Principal.array_dados_concatenados(i), "   ")

                Principal.array_dados_concatenados(i) = String.Concat(Principal.array_dados_concatenados(i), _
                            Convert.ToString(controle.temp_interna(i)))
                'array_dados_concatenados(i) = String.Concat(array_dados_concatenados(i), "   ")

                ''array_dados_concatenados(i) = String.Concat(array_dados_concatenados(i), _
                ''            Convert.ToString(v4(i)))
                ''array_dados_concatenados(i) = String.Concat(array_dados_concatenados(i), "   ")

                ''array_dados_concatenados(i) = String.Concat(array_dados_concatenados(i), _
                ''            Convert.ToString(v5(i)))
                ''array_dados_concatenados(i) = String.Concat(array_dados_concatenados(i), "   ")

                'array_dados_concatenados(i) = String.Concat(array_dados_concatenados(i), _
                '            Convert.ToString(v6(i)))
                'array_dados_concatenados(i) = String.Concat(array_dados_concatenados(i), "   ")

                'array_dados_concatenados(i) = String.Concat(array_dados_concatenados(i), _
                '           Convert.ToString(v7(i)))
                'array_dados_concatenados(i) = String.Concat(array_dados_concatenados(i), "   ")

                'array_dados_concatenados(i) = String.Concat(array_dados_concatenados(i), _
                '            Convert.ToString(v8(i)))


            Next





            If Principal.SaveFileDialog1.FileName = "" Then Principal.SaveFileDialog1.FileName = path_data

            File.WriteAllLines(Principal.FolderBrowserDialog1.SelectedPath & "\" & ".txt", Principal.array_dados_concatenados) ' esse comando pode dar problema de versão do windows



        Else




            Principal.ind_file = controle.ponto_atual \ Principal.n_linhas_por_arquivo


            'segurança de gravação do arquivo dados 
            If Principal.ind_file >= 20 Then
                MessageBox.Show("Arquivos excedendo o núnero de dados")
                Principal.ind_file = 98

            End If




            For i As Integer = 0 To Principal.ind_file
                If Principal.ind_file = 0 Then Principal.ind_file = 1

                ReDim Principal.array_dados_concatenados(controle.ponto_atual \ Principal.ind_file)
                ind_file_cont += 1


                'concatenar variáveis 
                For n As Integer = 0 To Principal.n_linhas_por_arquivo - 1


                    Principal.array_dados_concatenados(n) = String.Concat(Principal.array_dados_concatenados(n), _
                         Convert.ToString(controle.vatual(n + (i * Principal.n_linhas_por_arquivo))))


                    Principal.array_dados_concatenados(n) = String.Concat(Principal.array_dados_concatenados(n), "   ")

                    'Principal.array_dados_concatenados(n) = String.Concat(Principal.array_dados_concatenados(n), _
                    'Convert.ToString(controle.ph(n + (i * Principal.n_linhas_por_arquivo))))

                    Principal.array_dados_concatenados(n) = String.Concat(Principal.array_dados_concatenados(n), "   ")

                    Principal.array_dados_concatenados(n) = String.Concat(Principal.array_dados_concatenados(n), _
                         Convert.ToString(controle.temp_externa(n + (i * Principal.n_linhas_por_arquivo))))

                    Principal.array_dados_concatenados(n) = String.Concat(Principal.array_dados_concatenados(n), "   ")

                    Principal.array_dados_concatenados(n) = String.Concat(Principal.array_dados_concatenados(n), _
                         Convert.ToString(controle.temp_interna(n + (i * Principal.n_linhas_por_arquivo))))

                    'array_dados_concatenados(n) = String.Concat(array_dados_concatenados(n), "   ")

                    ''array_dados_concatenados(n) = String.Concat(array_dados_concatenados(n), _
                    ''     Convert.ToString(v4(n + (i * n_linhas_por_arquivo))))

                    ''array_dados_concatenados(n) = String.Concat(array_dados_concatenados(n), "   ")

                    ''array_dados_concatenados(n) = String.Concat(array_dados_concatenados(n), _
                    ''     Convert.ToString(v5(n + (i * n_linhas_por_arquivo))))

                    ''array_dados_concatenados(n) = String.Concat(array_dados_concatenados(n), "   ")

                    'array_dados_concatenados(n) = String.Concat(array_dados_concatenados(n), _
                    '     Convert.ToString(v6(n + (i * n_linhas_por_arquivo))))

                    'array_dados_concatenados(n) = String.Concat(array_dados_concatenados(n), "   ")

                    'array_dados_concatenados(n) = String.Concat(array_dados_concatenados(n), _
                    '     Convert.ToString(v7(n + (i * n_linhas_por_arquivo))))

                    'array_dados_concatenados(n) = String.Concat(array_dados_concatenados(n), "   ")

                    'array_dados_concatenados(n) = String.Concat(array_dados_concatenados(n), _
                    '     Convert.ToString(v8(n + (i * n_linhas_por_arquivo))))





                Next

                ' If saveFileDialog1.FileName = "" Then saveFileDialog1.FileName = path_data & Convert.ToString(ind_file) & ".txt"
                If Principal.SaveFileDialog1.FileName = "" Then Principal.SaveFileDialog1.FileName = path_data

                File.WriteAllLines(Principal.FolderBrowserDialog1.SelectedPath & "\" & Convert.ToString(ind_file_cont) & ".txt", Principal.array_dados_concatenados) ' esse comando pode dar problema de versão do windows

            Next




        End If







    End Sub


    Public Shared Function SoLETRAS(ByVal KeyAscii As Integer) As Integer
        'Transformar letras minusculas em Maiúsculas
        KeyAscii = Asc(UCase(Chr(KeyAscii)))
        ' Intercepta um código ASCII recebido e admite somente letras
        If InStr("AÃÁBCÇDEÉÊFGHIÍJKLMNOPQRSTUÚVWXYZ", Chr(KeyAscii)) = 0 Then
            SoLETRAS = 0
        Else
            SoLETRAS = KeyAscii
        End If

        ' teclas adicionais permitidas
        If KeyAscii = 8 Then SoLETRAS = KeyAscii ' Backspace
        If KeyAscii = 13 Then SoLETRAS = KeyAscii ' Enter
        If KeyAscii = 32 Then SoLETRAS = KeyAscii ' Espace
    End Function


    Public Shared Function SoNumeros(ByVal Keyascii As Short) As Short
        If InStr("1234567890", Chr(Keyascii)) = 0 Then
            SoNumeros = 0
        Else
            SoNumeros = Keyascii
        End If

        Select Case Keyascii
            Case 8
                SoNumeros = Keyascii
            Case 13
                SoNumeros = Keyascii
            Case 32
                SoNumeros = Keyascii
        End Select
    End Function


    'exemplo de só letras
    'Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
    '    Dim KeyAscii As Short = CShort(Asc(e.KeyChar))

    '    KeyAscii = CShort(SoLETRAS(KeyAscii))

    '    If KeyAscii = 0 Then

    '        e.Handled = True

    '    End If

    'End Sub


    'exemplo de só numeros 
    'Private Sub TextBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
    '    Dim KeyAscii As Short = CShort(Asc(e.KeyChar))

    '    KeyAscii = CShort(SoNumeros(KeyAscii))

    '    If KeyAscii = 0 Then

    '        e.Handled = True

    '    End If

    'End Sub




    '#Region "tratamneto de dados"
    ' função média movel 
    '    Public Function tratamento_dados(ByRef comd As Integer, ByVal qtd_pontos_media_movel As Integer, Optional ByVal var_fc As Double = 0) As Integer

    '        Dim x As Integer
    '        Dim outputarray(1000000) As Integer
    '        outputarray(0) = 0

    '        Select Case comd



    '            Case 0  ' média móvel 
    '                outputarray(0) = 0
    '                If qtd_pontos_media_movel >= MDIParent1.ponto_atual Then qtd_pontos_media_movel = MDIParent1.ponto_atual
    '                If qtd_pontos_media_movel = 0 Then qtd_pontos_media_movel = 1


    '                If qtd_pontos_media_movel > MDIParent1.ponto_atual Then
    '                    qtd_pontos_media_movel = MDIParent1.ponto_atual

    '                    Select Case Sel_adc


    '                        Case -1
    '                            MessageBox.Show("Tem que selecinar um adc!")
    '                            Exit Select
    '                        Case 0

    '                            x = MDIParent1.v1(MDIParent1.ponto_atual)



    '                            Exit Select

    '                        Case 1
    '                            x = MDIParent1.v2(MDIParent1.ponto_atual)

    '                            Exit Select

    '                        Case 2

    '                            x = MDIParent1.v3(MDIParent1.ponto_atual)
    '                            Exit Select

    '                        Case 3
    '                            x = MDIParent1.v4(MDIParent1.ponto_atual)

    '                            Exit Select

    '                        Case 4
    '                            x = MDIParent1.v5(MDIParent1.ponto_atual)

    '                            Exit Select


    '                    End Select


    '                Else
    '                    For i As Integer = 1 To qtd_pontos_media_movel

    '                        Select Case Sel_adc


    '                            Case -1
    '                                MessageBox.Show("Tem que selecinar um adc!")
    '                                Exit Select
    '                            Case 0
    '                                If i = 0 Then
    '                                    outputarray(1) = MDIParent1.v1(MDIParent1.ponto_atual)
    '                                Else : outputarray(i) = MDIParent1.v1(MDIParent1.ponto_atual - i)
    '                                End If



    '                                Exit Select

    '                            Case 1
    '                                If i = 0 Then
    '                                    outputarray(1) = MDIParent1.v1(MDIParent1.ponto_atual)
    '                                Else : outputarray(i) = MDIParent1.v2(MDIParent1.ponto_atual - i)
    '                                End If

    '                                Exit Select

    '                            Case 2

    '                                If i = 0 Then
    '                                    outputarray(1) = MDIParent1.v1(MDIParent1.ponto_atual)
    '                                Else : outputarray(i) = MDIParent1.v3(MDIParent1.ponto_atual - i)
    '                                End If

    '                                Exit Select

    '                            Case 3
    '                                If i = 0 Then
    '                                    outputarray(1) = MDIParent1.v1(MDIParent1.ponto_atual)
    '                                Else : outputarray(i) = MDIParent1.v4(MDIParent1.ponto_atual - i)
    '                                End If


    '                                Exit Select

    '                            Case 4
    '                                If i = 0 Then
    '                                    outputarray(1) = MDIParent1.v1(MDIParent1.ponto_atual)
    '                                Else : outputarray(i) = MDIParent1.v5(MDIParent1.ponto_atual - i)
    '                                End If


    '                                Exit Select


    '                        End Select


    '                    Next


    '                End If

    '                If qtd_pontos_media_movel = 0 Then qtd_pontos_media_movel = 1


    '                For i As Integer = 1 To qtd_pontos_media_movel
    '                    If qtd_pontos_media_movel - i >= 1 Then outputarray(10) = outputarray(10) + (outputarray(qtd_pontos_media_movel - i)) '+ outputarray(qtd_pontos_media_movel - i) + outputarray(qtd_pontos_media_movel - i) + outputarray(qtd_pontos_media_movel - 3) + outputarray(qtd_pontos_media_movel - 4) / qtd_pontos_media_movel) \ 1
    '                Next



    '                If qtd_pontos_media_movel > 1 Then x = outputarray(10) / (qtd_pontos_media_movel - 1)
    '                If qtd_pontos_media_movel = 1 Then x = outputarray(1)




    '                For i As Integer = 0 To 10
    '                    outputarray(i) = 0
    '                Next









    '            Case 1
    '                Dim kernel(32) As Double
    '                Dim PI As Double = 3.14159265

    '                Dim M As Integer
    '                Dim sum As Integer
    '                'LOW-PASS WINDOWED-SINC FILTER
    '                'copyright © 1997-1999 by California Technical Publishing
    '                'published with  permission from Steven W Smith, www.dspguide.com
    '                'GUI by logix4u , www.logix4u.net
    '                'modified by logix4u, www.logix4.net
    '                'This program filters 5000 samples with a 101 point windowed-sinc filter,
    '                'resulting in 4900 samples of filtered data.
    '                '
    '                'DIM X[4999] 'X[ ] holds the input signal
    '                'DIM Y[4999] 'Y[ ] holds the output signal
    '                'DIM H[100] 'H[ ] holds the filter kernel
    '                '
    '                'PI = 3.14159265
    '                'FC = Val(Text1.Text)  'Set the cutoff frequency (between 0 and 0.5)
    '                M% = 32 'Set filter length (101 points)
    '                '
    '                'GoSub XXXX 'Mythical subroutine to load X[ ]
    '                '
    '                ' 'Calculate the low-pass filter kernel via Eq. 16-4
    '                For i% = 0 To 31
    '                    If (i% - M% / 2) = 0 Then kernel(i%) = 2 * PI * var_fc
    '                    If (i% - M% / 2) <> 0 Then kernel(i%) = Sin(2 * PI * var_fc * (i% - M% / 2)) / (i% - M% / 2)
    '                    kernel(i%) = kernel(i%) * (0.54 - 0.46 * Cos(2 * PI * i% / M%))
    '                Next i%
    '                '

    '                sum = 0 'Normalize the low-pass filter kernel for
    '                For i% = 0 To 31 'unity gain at DC
    '                    sum = sum + kernel(i%)
    '                Next i%
    '                '
    '                For i% = 0 To 31
    '                    kernel(i%) = kernel(i%) / sum
    '                Next i%
    '                '
    '                For j% = 31 To MDIParent1.ponto_atual 'Convolve the input signal & filter kernel
    '                    outputarray(j%) = 0 'array para plot deste filtro  
    '                    For i% = 0 To 31


    '                        ' outputarray(j%) = outputarray(j%) + FuncGen.Samples(j% - I%) * kernel(I%)

    '                        Select Case Sel_adc


    '                            Case -1
    '                                MessageBox.Show("Tem que selecinar um adc!")
    '                                Exit Select
    '                            Case 0
    '                                outputarray(j%) = outputarray(j%) + MDIParent1.v1(j% - i%) * kernel(i%)


    '                                x = outputarray(MDIParent1.ponto_atual)


    '                                Exit Select

    '                            Case 1
    '                                outputarray(j%) = outputarray(j%) + MDIParent1.v2(j% - i%) * kernel(i%)


    '                                x = outputarray(MDIParent1.ponto_atual)



    '                                Exit Select

    '                            Case 2

    '                                outputarray(j%) = outputarray(j%) + MDIParent1.v3(j% - i%) * kernel(i%)


    '                                x = outputarray(MDIParent1.ponto_atual)


    '                                Exit Select

    '                            Case 3
    '                                outputarray(j%) = outputarray(j%) + MDIParent1.v4(j% - i%) * kernel(i%)


    '                                x = outputarray(MDIParent1.ponto_atual)


    '                                Exit Select

    '                            Case 4
    '                                outputarray(j%) = outputarray(j%) + MDIParent1.v5(j% - i%) * kernel(i%)


    '                                x = outputarray(MDIParent1.ponto_atual)


    '                                Exit Select


    '                        End Select



    '                    Next i%
    '                Next j%
    '                '
    '                'End










    '            Case 2

    '                'codigos para implementar  passa baixa  The "weight" variable controls the filter's filtering charactersitics. 
    '                'outputarray(0) = MDIParent1.v1(0)
    '                For ii As Integer = 1 To MDIParent1.ponto_atual '( ii=1; ii<count; ii++ )
    '                    'outputarray(ii) = weight * MDIParent1.v1(ii) + (1.0 - weight) * outputarray(ii - 1)


    '                    'outputarray(ii) = var_fc * MDIParent1.v1(ii) + (1.0 - var_fc) * outputarray(ii - 1)

    '                    'x = outputarray(MDIParent1.ponto_atual)

    '                    Select Case Sel_adc


    '                        Case -1
    '                            MessageBox.Show("Tem que selecinar um adc!")
    '                            Exit Select
    '                        Case 0
    '                            outputarray(ii) = var_fc * MDIParent1.v1(ii) + (1.0 - var_fc) * outputarray(ii - 1)

    '                            x = outputarray(MDIParent1.ponto_atual)


    '                            Exit Select

    '                        Case 1
    '                            outputarray(ii) = var_fc * MDIParent1.v2(ii) + (1.0 - var_fc) * outputarray(ii - 1)

    '                            x = outputarray(MDIParent1.ponto_atual)


    '                            Exit Select

    '                        Case 2

    '                            outputarray(ii) = var_fc * MDIParent1.v3(ii) + (1.0 - var_fc) * outputarray(ii - 1)

    '                            x = outputarray(MDIParent1.ponto_atual)


    '                            Exit Select

    '                        Case 3
    '                            outputarray(ii) = var_fc * MDIParent1.v4(ii) + (1.0 - var_fc) * outputarray(ii - 1)

    '                            x = outputarray(MDIParent1.ponto_atual)


    '                            Exit Select

    '                        Case 4
    '                            outputarray(ii) = var_fc * MDIParent1.v5(ii) + (1.0 - var_fc) * outputarray(ii - 1)

    '                            x = outputarray(MDIParent1.ponto_atual)


    '                            Exit Select


    '                    End Select












    '                Next






    '            Case 3


    '                Dim α As Double


    '                'α = RC / (RC + Me.tmr_auto.Interval)
    '                α = var_fc / (var_fc + (Me.tmr_auto.Interval * 1000))
    '                'outputarray(0) = MDIParent1.v1(0)





    '                For ii As Integer = 1 To MDIParent1.ponto_atual
    '                    Select Case Sel_adc


    '                        Case -1
    '                            MessageBox.Show("Tem que selecinar um adc!")
    '                            Exit Select
    '                        Case 0
    '                            outputarray(ii) = α * outputarray(ii - 1) + α * (MDIParent1.v1(ii) - MDIParent1.v1(ii - 1))

    '                            x = outputarray(MDIParent1.ponto_atual)


    '                            Exit Select

    '                        Case 1
    '                            outputarray(ii) = α * outputarray(ii - 1) + α * (MDIParent1.v2(ii) - MDIParent1.v2(ii - 1))

    '                            x = outputarray(MDIParent1.ponto_atual)


    '                            Exit Select

    '                        Case 2

    '                            outputarray(ii) = α * outputarray(ii - 1) + α * (MDIParent1.v3(ii) - MDIParent1.v3(ii - 1))

    '                            x = outputarray(MDIParent1.ponto_atual)


    '                            Exit Select

    '                        Case 3
    '                            outputarray(ii) = α * outputarray(ii - 1) + α * (MDIParent1.v4(ii) - MDIParent1.v4(ii - 1))

    '                            x = outputarray(MDIParent1.ponto_atual)


    '                            Exit Select

    '                        Case 4
    '                            outputarray(ii) = α * outputarray(ii - 1) + α * (MDIParent1.v5(ii) - MDIParent1.v5(ii - 1))

    '                            x = outputarray(MDIParent1.ponto_atual)


    '                            Exit Select


    '                    End Select


    '                    outputarray(ii) = α * outputarray(ii - 1) + α * (MDIParent1.v1(ii) - MDIParent1.v1(ii - 1))




    '                Next


    '                '   // Return RC high-pass filter output samples, given input samples,
    '                '// time interval dt , and time constant RC
    '                'function highpass( real[0..n] x, real dt, real RC)
    '                '  var real[0..n] y
    '                '  var real α := RC / (RC + dt)
    '                '  y[0] := x[0]
    '                '  for i from 1 to n
    '                '    y[i] := α * y[i-1] + α * (x[i] - x[i-1])
    '                '          Return y





    '            Case 4


    '                Dim α As Double


    '                'α = RC / (RC + Me.tmr_auto.Interval)
    '                α = var_fc / (var_fc + (Me.tmr_auto.Interval * 1000))
    '                'outputarray(0) = MDIParent1.v1(0)





    '                For ii As Integer = 1 To MDIParent1.ponto_atual
    '                    Select Case Sel_adc


    '                        Case -1
    '                            MessageBox.Show("Tem que selecinar um adc!")
    '                            Exit Select
    '                        Case 0

    '                            'y[i] := α * x[i] + (1-α) * y[i-1]
    '                            outputarray(ii) = α * MDIParent1.v1(ii) + (1 - α) * outputarray(ii - 1)

    '                            x = outputarray(MDIParent1.ponto_atual)


    '                            Exit Select

    '                        Case 1
    '                            outputarray(ii) = α * MDIParent1.v2(ii) + (1 - α) * outputarray(ii - 1)

    '                            x = outputarray(MDIParent1.ponto_atual)


    '                            Exit Select

    '                        Case 2

    '                            outputarray(ii) = α * MDIParent1.v3(ii) + (1 - α) * outputarray(ii - 1)

    '                            x = outputarray(MDIParent1.ponto_atual)


    '                            Exit Select

    '                        Case 3
    '                            outputarray(ii) = α * MDIParent1.v4(ii) + (1 - α) * outputarray(ii - 1)

    '                            x = outputarray(MDIParent1.ponto_atual)


    '                            Exit Select

    '                        Case 4
    '                            outputarray(ii) = α * MDIParent1.v5(ii) + (1 - α) * outputarray(ii - 1)

    '                            x = outputarray(MDIParent1.ponto_atual)


    '                            Exit Select


    '                    End Select


    '                    'outputarray(ii) = α * outputarray(ii - 1) + α * (MDIParent1.v1(ii) - MDIParent1.v1(ii - 1))




    '                Next










    '            Case 5
    '            Case 6
    '            Case 7


    '        End Select


    '        Return x


    '    End Function



    '#End Region



    '    Public Function tipo_controlePV(ByRef cmd As Integer, ByVal tmr_interval As Integer, Optional ByVal var_fc As Double = 0) As Integer
    '        Dim x As Integer


    '        Select Case cmd
    '            Case 1
    '                'definir pv
    '                tmr_tipo_controle.Interval = tempo_timer


    '                PV = array_tratamento(cont_array)


    '                dfilter = 10 ' Filter value to scale down derivative effect.
    '                inputd = PV + (inputlast - PV) * (Rate / 60)
    '                inputlast = PV
    '                inputdf = inputdf + (inputd - inputdf) * dfilter / 60
    '                output = (SP - inputdf) * (gain / 100) + feedback


    '                If output > 250 Then ' clamp output valve between 0 and 100%
    '                    output = 250
    '                End If
    '                If output < 0 Then
    '                    output = 0
    '                End If

    '                feedback = feedback - (feedback - output) * reset / 60


    '                Exit Select


    '            Case 2
    '                'dac_1(ponto_atual) = outdac1ant * (1 + ((sp1 - adc_0(ponto_atual)) / sp1) * 0.8)

    '                output = array_tratamento(cont_array - 1) * (1 + ((SP - array_tratamento(cont_array)) / SP) * 0.8)




    '                Exit Select




    '            Case 3
    '            Case 4
    '            Case 5
    '            Case 6
    '                Exit Select
    '            Case 7



    '        End Select
    '        x = output

    '        Return x


    '    End Function







End Class
