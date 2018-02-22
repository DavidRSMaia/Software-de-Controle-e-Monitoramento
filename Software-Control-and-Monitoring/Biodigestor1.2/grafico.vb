Public Class grafico

    Private Sub Chart1_Click(sender As Object, e As EventArgs) Handles Chart1.Click

    End Sub

    Private Sub grafico_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        
        Chart1.ChartAreas("ChartArea1").CursorX.IsUserSelectionEnabled = True

        Chart1.ChartAreas("ChartArea1").AxisX.ScaleView.Zoomable = True

        Chart1.ChartAreas("ChartArea1").CursorY.IsUserSelectionEnabled = True
        Chart1.ChartAreas("ChartArea1").AxisY.ScaleView.Zoomable = True

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        Static ponto_atual As Integer
        Static dif_pontos_atual As Integer


        Dim numberOfPointsInChart As Integer = 200
        Dim numberOfPointsAfterRemoval As Integer = 200 * 95 \ 100


        ponto_atual += 1

        dif_pontos_atual += 1

        Chart1.Series("Temperatura da amostra").Points.AddXY(Controle.ponto_atual, (Controle.tempIntApresent(ponto_atual) \ 1) + 5)
        Chart1.Series("Temperatura de controle").Points.AddXY(Controle.ponto_atual, (Controle.tempExtApresent(ponto_atual) \ 1) + 5)
        Chart1.Series("pH").Points.AddXY(Controle.ponto_atual, (Controle.phApresent(ponto_atual)))
        Chart1.Series("Metano").Points.AddXY(Controle.ponto_atual, (Controle.metanoApresent(ponto_atual)))
        Chart1.Series("Dióxido de Carbono").Points.AddXY(Controle.ponto_atual, (Controle.dioxApresent(ponto_atual)))
        Chart1.Series("Hidrogênio").Points.AddXY(Controle.ponto_atual, (Controle.hidrogApresent(ponto_atual)))
        Chart1.Series("H2S").Points.AddXY(Controle.ponto_atual, (Controle.sulfApresent(ponto_atual)))
        Chart1.ResetAutoValues()


        While Chart1.Series(0).Points.Count > numberOfPointsInChart

            While Chart1.Series(0).Points.Count > numberOfPointsAfterRemoval
                Chart1.Series(0).Points.RemoveAt(0)
            End While
        End While

        While Chart1.Series(1).Points.Count > numberOfPointsInChart
            While Chart1.Series(1).Points.Count > numberOfPointsAfterRemoval
                Chart1.Series(1).Points.RemoveAt(1)
            End While
        End While

        

        If dif_pontos_atual + 1 > numberOfPointsInChart Then
            Chart1.ChartAreas("ChartArea1").AxisX.Minimum = Controle.ponto_atual - numberOfPointsAfterRemoval
            Chart1.ChartAreas("ChartArea1").AxisX.Maximum = Chart1.ChartAreas("ChartArea1").AxisX.Minimum + numberOfPointsInChart
        End If

    End Sub

End Class