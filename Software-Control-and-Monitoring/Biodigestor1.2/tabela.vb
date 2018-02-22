Public Class tabela

    Dim largura As Integer
    Dim pont_dif As Integer
    Dim point_ant As Integer = 0

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        For i As Integer = point_ant + 1 To controle.ponto_atual


            Dim item As New DataGridViewRow
            item.CreateCells(DataGridView1)

            item.Cells(1).Value = Controle.tempIntApresent(i)
            item.Cells(2).Value = Controle.tempExtApresent(i)
            item.Cells(3).Value = Controle.phApresent(i)
            item.Cells(4).Value = Controle.metanoApresent(i)
            item.Cells(5).Value = Controle.dioxApresent(i)
            item.Cells(6).Value = Controle.hidrogApresent(i)
            item.Cells(7).Value = Controle.sulfApresent(i)

            DataGridView1.Rows.Add(item)

            
        Next
        point_ant = controle.ponto_atual
    End Sub

    Private Sub tabela_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class