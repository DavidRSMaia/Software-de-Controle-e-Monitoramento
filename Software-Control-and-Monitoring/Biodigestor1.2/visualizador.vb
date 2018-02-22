Imports System.Runtime.InteropServices

Public Class visualizador

#Region " funções das APIs usadas e as constantes relacionadas"
    'constantes usadas na DLL
    Const WM_CAP As Short = &H400S

    Const WM_CAP_DRIVER_CONNECT As Integer = WM_CAP + 10
    Const WM_CAP_DRIVER_DISCONNECT As Integer = WM_CAP + 11
    Const WM_CAP_EDIT_COPY As Integer = WM_CAP + 30

    Const WM_CAP_SET_PREVIEW As Integer = WM_CAP + 50
    Const WM_CAP_SET_PREVIEWRATE As Integer = WM_CAP + 52
    Const WM_CAP_SET_SCALE As Integer = WM_CAP + 53
    Const WS_CHILD As Integer = &H40000000
    Const WS_VISIBLE As Integer = &H10000000
    Const SWP_NOMOVE As Short = &H2S
    Const SWP_NOSIZE As Short = 1
    Const SWP_NOZORDER As Short = &H4S
    Const HWND_BOTTOM As Short = 1

    Dim iDevice As Integer = 0 ' ID do dispositivo atual
    Dim hHwnd As Integer ' manipulador da janela do visualizador
    Dim sfdImage As Object

    Declare Function SendMessage Lib "user32" Alias "SendMessageA" _
    (ByVal hwnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, <MarshalAs(UnmanagedType.AsAny)> ByVal lParam As Object) As Integer

    Declare Function SetWindowPos Lib "user32" Alias "SetWindowPos" (ByVal hwnd As Integer, _
    ByVal hWndInsertAfter As Integer, ByVal x As Integer, ByVal y As Integer, ByVal cx As Integer, ByVal cy As Integer, ByVal wFlags As Integer) As Integer

    'A função DestroyWindow destroi a janela especificada.
    'Envia as mensagens WM_DESTROY e WM_NCDESTROY para a 
    'janela para destivá-la e remove o foco do teclado da mesma 
    'Library - User32
    'Parametros - hWnd - (identica a janela a ser destruida)
    'Retorna um valor diferente de zero se for executada com sucesso, 'caso contrario retorna zero
    Declare Function DestroyWindow Lib "user32" (ByVal hndw As Integer) As Boolean


    Declare Function capCreateCaptureWindowA Lib "avicap32.dll" (ByVal lpszWindowName As String, ByVal dwStyle As Integer, _
    ByVal x As Integer, ByVal y As Integer, ByVal nWidth As Integer, ByVal nHeight As Short, ByVal hWndParent As Integer, ByVal nID As Integer) As Integer

    Declare Function capGetDriverDescriptionA Lib "avicap32.dll" (ByVal wDriver As Short, _
    ByVal lpszName As String, ByVal cbName As Integer, ByVal lpszVer As String, ByVal cbVer As Integer) As Boolean
#End Region

    Private Sub visualizador_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        'verifica e carrega os dispositivos
        carregaDispositivos()

        ' se encontrou dispostivos instalados então exibe
        If lstDispositivos.Items.Count > 0 Then
            btniniciar.Enabled = True
            lstDispositivos.SelectedIndex = 0
            btniniciar.Enabled = True
        Else
            lstDispositivos.Items.Add("Não dispositivo de captura instalado.")
            btniniciar.Enabled = False
        End If

        btnparar.Enabled = False
        btnsalvar.Enabled = False
        picCaptura.SizeMode = PictureBoxSizeMode.StretchImage
    End Sub

    Private Sub carregaDispositivos()
        Dim strNome As String = Space(100)
        Dim strVer As String = Space(100)
        Dim bRetorna As Boolean
        Dim x As Integer = 0

        '' Carrega os dispositivos em lstDevices
        Do

            ' Obtem o nome e a versão Driver
            bRetorna = capGetDriverDescriptionA(x, strNome, 100, strVer, 100)

            ' se existir um dispositivo inclui o nome da lista
            If bRetorna Then lstDispositivos.Items.Add(strNome.Trim)
            x += 1
        Loop Until bRetorna = False
    End Sub

    Private Sub abreJanelaVisualizacao()
        Dim iHeight As Integer = picCaptura.Height
        Dim iWidth As Integer = picCaptura.Width

        ' Abre a janela de visualização no picturebox
        hHwnd = capCreateCaptureWindowA(iDevice, WS_VISIBLE Or WS_CHILD, 0, 0, 640, _
        480, picCaptura.Handle.ToInt32, 0)

        ' Conecta com o drive
        If SendMessage(hHwnd, WM_CAP_DRIVER_CONNECT, iDevice, 0) Then
            '
            'Define a escala de previsão
            SendMessage(hHwnd, WM_CAP_SET_SCALE, True, 0)

            'Define a taxa de visualização em milisegundos
            SendMessage(hHwnd, WM_CAP_SET_PREVIEWRATE, 66, 0)

            'Iniciar a visualização da imagem a partir da camara
            SendMessage(hHwnd, WM_CAP_SET_PREVIEW, True, 0)

            ' Redimensiona a janela para se ajustar no picturebox
            SetWindowPos(hHwnd, HWND_BOTTOM, 0, 0, picCaptura.Width, picCaptura.Height, SWP_NOMOVE Or SWP_NOZORDER)

            btnsalvar.Enabled = True
            btnparar.Enabled = True
            btniniciar.Enabled = False
        Else
            '
            ' Erro de conexão fecha a janela de dispostivos
            DestroyWindow(hHwnd)

            btnsalvar.Enabled = False
        End If
    End Sub

    Private Sub fechaJanelaVisualizacao()
        ' Desconecta do dispositivo
        SendMessage(hHwnd, WM_CAP_DRIVER_DISCONNECT, iDevice, 0)

        ' fecha a chama a janela
        DestroyWindow(hHwnd)
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub PictureBox1_Click_1(sender As Object, e As EventArgs) Handles picCaptura.Click

    End Sub

    Private Sub btniniciar_Click(sender As Object, e As EventArgs) Handles btniniciar.Click
        iDevice = lstDispositivos.SelectedIndex
        abreJanelaVisualizacao()
    End Sub

    Private Sub btnparar_Click(sender As Object, e As EventArgs) Handles btnparar.Click
        fechaJanelaVisualizacao()
        btnsalvar.Enabled = False
        btniniciar.Enabled = True
        btnparar.Enabled = False
    End Sub

    Private Sub btnsalvar_Click(sender As Object, e As EventArgs) Handles btnsalvar.Click
        Dim dados As IDataObject
        Dim bmap As Image

        ' Copia a imagem para o clipboard
        SendMessage(hHwnd, WM_CAP_EDIT_COPY, 0, 0)

        ' Obtem a imagem do clipboard e converte para bitmap
        dados = Clipboard.GetDataObject()

        If dados.GetDataPresent(GetType(System.Drawing.Bitmap)) Then
            bmap = CType(dados.GetData(GetType(System.Drawing.Bitmap)), Image)
            picCaptura.Image = bmap
            fechaJanelaVisualizacao()
            btnsalvar.Enabled = False
            btnparar.Enabled = False
            btniniciar.Enabled = True

            If sfdImage.ShowDialog = System.Windows.Forms.DialogResult.OK Then
                bmap.Save(sfdImage.FileName, Imaging.ImageFormat.Bmp)
            End If

        End If
    End Sub

    Private Sub visualizador_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If btnparar.Enabled Then
            fechaJanelaVisualizacao()
        End If
    End Sub
End Class