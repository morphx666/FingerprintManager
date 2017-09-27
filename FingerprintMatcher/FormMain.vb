Imports ScanAPIHelper
Imports System.Threading
Imports PatternRecognition.FingerprintRecognition.Core
Imports PatternRecognition.FingerprintRecognition.FeatureExtractors
Imports PatternRecognition.FingerprintRecognition.FeatureDisplay
Imports PatternRecognition.FingerprintRecognition.FeatureRepresentation

Public Class FormMain
    Private Enum ScannerErrorCodes
        FTR_ERROR_EMPTY_FRAME = 4306 'ERROR_EMPTY
        FTR_ERROR_MOVABLE_FINGER = &H20000001
        FTR_ERROR_NO_FRAME = &H20000002
        FTR_ERROR_USER_CANCELED = &H20000003
        FTR_ERROR_HARDWARE_INCOMPATIBLE = &H20000004
        FTR_ERROR_FIRMWARE_INCOMPATIBLE = &H20000005
        FTR_ERROR_INVALID_AUTHORIZATION_CODE = &H20000006

        ' Other return codes are Windows-compatible
        ERROR_NO_MORE_ITEMS = 259                ' ERROR_NO_MORE_ITEMS
        ERROR_NOT_ENOUGH_MEMORY = 8              ' ERROR_NOT_ENOUGH_MEMORY
        ERROR_NO_SYSTEM_RESOURCES = 1450         ' ERROR_NO_SYSTEM_RESOURCES
        ERROR_TIMEOUT = 1460                     ' ERROR_TIMEOUT
        ERROR_NOT_READY = 21                     ' ERROR_NOT_READY
        ERROR_BAD_CONFIGURATION = 1610           ' ERROR_BAD_CONFIGURATION
        ERROR_INVALID_PARAMETER = 87             ' ERROR_INVALID_PARAMETER
        ERROR_CALL_NOT_IMPLEMENTED = 120         ' ERROR_CALL_NOT_IMPLEMENTED
        ERROR_NOT_SUPPORTED = 50                 ' ERROR_NOT_SUPPORTED
        ERROR_WRITE_PROTECT = 19                 ' ERROR_WRITE_PROTECT
        ERROR_MESSAGE_EXCEEDS_MAX_SIZE = 4336    ' ERROR_MESSAGE_EXCEEDS_MAX_SIZE
    End Enum

    Private Class ScannerItem
        Private mName As String
        Private mInterfaceNumber As Integer
        Private mScanner As Device

        Public Sub New(interfaceNumber As Integer)
            mInterfaceNumber = interfaceNumber

            OpenScanner()
            mName = GetCompatibilityString(mScanner.Information.DeviceCompatibility) + ": " + Scanner.VersionInformation.HardwareVersion.ToString()
            CloseScanner()
        End Sub

        Public ReadOnly Property Name As String
            Get
                Return mName
            End Get
        End Property

        Public Sub OpenScanner()
            If mScanner IsNot Nothing Then CloseScanner()
            Device.BaseInterface = mInterfaceNumber
            mScanner = New Device()
            mScanner.Open()

            mScanner.FastFingerDetectMethod = True
        End Sub

        Public Sub CloseScanner()
            If mScanner IsNot Nothing Then
                mScanner.SetDiodesStatus(DiodesStatus.turn_off, DiodesStatus.turn_off)
                mScanner.Close()
                mScanner.Dispose()
                mScanner = Nothing
            End If
        End Sub

        Public ReadOnly Property Scanner As Device
            Get
                Return mScanner
            End Get
        End Property

        Private Function GetCompatibilityString(compatibilityId As Integer) As String
            Select Case compatibilityId
                Case "0" : Return "USB 1.1 device"
                Case "1" : Return "USB 2.0 device (SOI966)"
                Case "2" : Return """Sweep"" scanner"
                Case "3" : Return """BlackFin"" scanner"
                Case "4" : Return "USB 2.0 device (SOI968)"
                Case "5" : Return "USB 2.0 device (FS88 compatible - SOI968)"
                Case "6" : Return "USB 2.0 device (FS90 compatible - PAS202)"
                Case "7" : Return "USB 2.0 device (FS50 compatible)"
                Case "8" : Return "USB 2.0 device (FS60 compatible)"
                Case "9" : Return "USB 2.0 device (FS25 compatible)"
                Case "11" : Return "USB 2.0 device (FS80 compatible - SOI968 + Prism w/o Lens)"
                Case "12" : Return "USB 2.0 device (FS90 compatible - GC0303 w/o PIV)"
                Case "210" : Return "USB 2.0 device (FS98 compatible)"
                Case Else : Return "Unknown Device"
            End Select
        End Function

        Public Overrides Function ToString() As String
            Return mName
        End Function
    End Class

    Private selectedScanner As ScannerItem

    Private scannerThread As Thread
    Private cancelAllThreads As Boolean
    Private emptyBitmap As Bitmap

    Private featureExtractor As JYFeatureExtractor
    Private featureDisplay1 As MTripletsDisplay

    Private Sub FormMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        cancelAllThreads = True

        Do
            Application.DoEvents()
        Loop While scannerThread.IsAlive

        If selectedScanner IsNot Nothing Then selectedScanner.CloseScanner()
    End Sub

    Private Sub FormMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LabelInfo.Visible = False

        LoadAvailableScanners()

        scannerThread = New Thread(AddressOf ScannerLoop) With {
            .IsBackground = True
        }
        scannerThread.Start()

        featureExtractor = New JYFeatureExtractor With {
            .MtiaExtractor = New Ratha1995MinutiaeExtractor()
        }
        featureDisplay1 = New MTripletsDisplay()
    End Sub

    Private Sub LoadAvailableScanners()
        Dim status() As FTRSCAN_INTERFACE_STATUS = Device.GetInterfaces()

        For interfaceNumber = 0 To status.Length - 1
            If status(interfaceNumber) = FTRSCAN_INTERFACE_STATUS.FTRSCAN_INTERFACE_STATUS_CONNECTED Then
                ComboBoxScanners.Items.Add(New ScannerItem(interfaceNumber))
            End If
        Next

        If ComboBoxScanners.Items.Count > 0 Then ComboBoxScanners.SelectedIndex = 0
    End Sub

    Private Sub ComboBoxScanners_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxScanners.SelectedIndexChanged
        If selectedScanner IsNot Nothing Then selectedScanner.CloseScanner()
        selectedScanner = ComboBoxScanners.SelectedItem
        If selectedScanner IsNot Nothing Then
            selectedScanner.OpenScanner()
            BitmapAPI.Init(selectedScanner.Scanner.ImageSize, BitmapAPI.ColorModes.BlackOnWhite)
            If emptyBitmap IsNot Nothing Then emptyBitmap.Dispose()

            emptyBitmap = New Bitmap(selectedScanner.Scanner.ImageSize.Width, selectedScanner.Scanner.ImageSize.Height)
            Using g As Graphics = Graphics.FromImage(emptyBitmap)
                g.Clear(Color.White)
            End Using
        End If
    End Sub

    Private Sub ScannerLoop()
        Dim validatorCounter As Integer = 0

        Do
            If selectedScanner Is Nothing OrElse selectedScanner.Scanner Is Nothing Then
                If selectedScanner IsNot Nothing Then selectedScanner.Scanner.SetDiodesStatus(DiodesStatus.turn_off, DiodesStatus.turn_on_permanent)
                PictureBoxFingerprint.Image = emptyBitmap
                validatorCounter = 0
                Thread.Sleep(250)
            Else
                If selectedScanner.Scanner.IsFingerPresent Then
                    Me.Invoke(New MethodInvoker(Sub() UpdateFrame()))
                    Thread.Sleep(15)

                    validatorCounter += 1
                    If validatorCounter >= 2 Then
                        Dim found As Boolean
                        Me.Invoke(New MethodInvoker(Sub() found = SearchFingerPrint()))
                        If found Then
                            validatorCounter = 0
                            selectedScanner.Scanner.SetDiodesStatus(DiodesStatus.turn_on_permanent, DiodesStatus.turn_off)
                            Thread.Sleep(3000)
                        End If
                    End If
                Else
                    selectedScanner.Scanner.SetDiodesStatus(DiodesStatus.turn_off, DiodesStatus.turn_on_permanent)
                    PictureBoxFingerprint.Image = emptyBitmap
                    validatorCounter = 0
                    Thread.Sleep(120)
                End If
            End If
        Loop Until cancelAllThreads
    End Sub

    Private Sub UpdateFrame()
        Dim frame() As Byte = Nothing
        Const r As Double = 1

        Try
            frame = selectedScanner.Scanner.GetFrame()
        Catch ex As ScanAPIException
            'Me.Text = GetScannerAPIError(ex)
            PictureBoxFingerprint.Image = emptyBitmap
        End Try

        If frame IsNot Nothing AndAlso frame.Length > 0 Then
            Using g As Graphics = PictureBoxFingerprint.CreateGraphics()
                Dim bmp As Bitmap = ResizeImage(BitmapAPI.GetBitmap(g.GetHdc(), frame), selectedScanner.Scanner.ImageSize.Width \ r,
                                                                                        selectedScanner.Scanner.ImageSize.Height \ r)
                bmp.RotateFlip(RotateFlipType.Rotate180FlipX)
                PictureBoxFingerprint.Image = bmp
            End Using
        End If
    End Sub

    Private Function ResizeImage(img As Bitmap, w As Integer, h As Integer) As Bitmap
        Return img

        'Dim img2 As Bitmap = New Bitmap(w, h)
        'Using g = Graphics.FromImage(img2)
        '    g.DrawImage(img, 0, 0, w, h)
        'End Using

        'Return img2
    End Function

    Private Function GetScannerAPIError(ex As ScanAPIException) As String
        Select Case ex.ErrorCode
            Case ScannerErrorCodes.FTR_ERROR_EMPTY_FRAME
                Return "Empty Frame"

            Case ScannerErrorCodes.FTR_ERROR_MOVABLE_FINGER
                Return "Unstable Finger"

            Case ScannerErrorCodes.FTR_ERROR_NO_FRAME
                Return "Fake Finger"

            Case ScannerErrorCodes.FTR_ERROR_USER_CANCELED
                Return "Error code FTR_ERROR_USER_CANCELED"

            Case ScannerErrorCodes.FTR_ERROR_HARDWARE_INCOMPATIBLE
                Return "Feature Not Supported"

            Case ScannerErrorCodes.FTR_ERROR_FIRMWARE_INCOMPATIBLE
                Return "Feature Not Supported"

            Case ScannerErrorCodes.FTR_ERROR_INVALID_AUTHORIZATION_CODE
                Return "Invalid Authorization Code"

            Case ScannerErrorCodes.ERROR_NO_MORE_ITEMS
                Return "Error code ERROR_NO_MORE_ITEMS"

            Case ScannerErrorCodes.ERROR_NOT_ENOUGH_MEMORY
                Return "Error code ERROR_NOT_ENOUGH_MEMORY"

            Case ScannerErrorCodes.ERROR_NO_SYSTEM_RESOURCES
                Return "Error code ERROR_NO_SYSTEM_RESOURCES"

            Case ScannerErrorCodes.ERROR_TIMEOUT
                Return "Error code ERROR_TIMEOUT"

            Case ScannerErrorCodes.ERROR_NOT_READY
                Return "Error code ERROR_NOT_READY"

            Case ScannerErrorCodes.ERROR_BAD_CONFIGURATION
                Return "Error code ERROR_BAD_CONFIGURATION"

            Case ScannerErrorCodes.ERROR_INVALID_PARAMETER
                Return "Error code ERROR_INVALID_PARAMETER"

            Case ScannerErrorCodes.ERROR_CALL_NOT_IMPLEMENTED
                Return "Error code ERROR_CALL_NOT_IMPLEMENTED"

            Case ScannerErrorCodes.ERROR_NOT_SUPPORTED
                Return "Error code ERROR_NOT_SUPPORTED"

            Case ScannerErrorCodes.ERROR_WRITE_PROTECT
                Return "Error code ERROR_WRITE_PROTECT"

            Case ScannerErrorCodes.ERROR_MESSAGE_EXCEEDS_MAX_SIZE
                Return "Error code ERROR_MESSAGE_EXCEEDS_MAX_SIZE"

            Case Else
                Return String.Format("Error code: {0}", ex.ErrorCode)
        End Select
    End Function

    Private Function SearchFingerPrint() As Boolean
        Dim srcFeatures As JYFeatures
        Dim trgFeatures As JYFeatures
        Dim result As New List(Of MinutiaPair)
        Dim matcher As New PatternRecognition.FingerprintRecognition.Matchers.JY
        Dim score As Double
        Dim sw As New Stopwatch()

        Static isBusy As Boolean
        If isBusy Then Return False
        isBusy = True

        sw.Start()

        selectedScanner.Scanner.SetDiodesStatus(DiodesStatus.turn_off, DiodesStatus.turn_on_period)
        LabelInfo.Visible = True
        TextBoxName.Text = ""
        Application.DoEvents()

        Dim img As Bitmap = CType(PictureBoxFingerprint.Image.Clone(), Bitmap)
        Dim bestMatch As Double = 0
        Dim name As String = ""
        Try
            srcFeatures = featureExtractor.ExtractFeatures(img)

            For Each file In (New IO.DirectoryInfo(My.Application.Info.DirectoryPath)).GetFiles("*.fpd")
                trgFeatures = BinarySerializer.Deserialize(file.FullName)

                score = matcher.Match(trgFeatures, srcFeatures, result)

                If score >= 40 AndAlso score > bestMatch Then
                    bestMatch = score
                    name = String.Format("{0} [{1:F2}%]", file.Name.Replace(file.Extension, ""), score)
                End If
            Next

            TextBoxName.Text = name

            If TextBoxName.Text = "" Then TextBoxName.Text = "Not Found!"
        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            TextBoxName.Text = "Error!"
        End Try

        LabelInfo.Visible = False
        LabelTime.Text = String.Format("{0:N0}ms", sw.ElapsedMilliseconds)

        isBusy = False

        Return bestMatch <> 0
    End Function
End Class
