Imports System.Runtime.InteropServices

Public Class BitmapAPI
    Private Enum BitmapCompressionMode As UInteger
        BI_RGB
        BI_RLE8
        BI_RLE4
        BI_BITFIELDS
        BI_JPEG
        BI_PNG
    End Enum

    <StructLayout(LayoutKind.Sequential)>
    Private Structure BITMAPINFOHEADER
        Public biSize As Int32
        Public biWidth As Int32
        Public biHeight As Int32
        Public biPlanes As Int16
        Public biBitCount As Int16
        Public biCompression As BitmapCompressionMode
        Public biSizeImage As Int32
        Public biXPelsperMeter As Int32
        Public biYPelsPerMeter As Int32
        Public biClrUsed As Int32
        Public biClrImportant As Int32
    End Structure

    Public Enum ColorModes
        WhiteOnBlack = 0
        BlackOnWhite = 1
    End Enum

    Private Const CBM_INIT = &H4

    Private Const DIB_RGB_COLORS = 0
    Private Const DIB_PAL_COLORS = 1

    <DllImport("gdi32.dll")>
    Private Shared Function CreateDIBitmap(hdc As IntPtr, ByRef lpbmih As BITMAPINFOHEADER,
                                           fdwInit As UInt32, lpbInit As Byte(), lpbmi As Byte(),
                                           fuUsage As UInt32) As IntPtr
    End Function

    Private Shared mem As IO.MemoryStream
    Private Shared bw As IO.BinaryWriter
    Private Shared bmiHeader As New BITMAPINFOHEADER()

    Public Shared Sub Init(bmpSize As Size, colorMode As ColorModes)
        If mem IsNot Nothing Then
            bw.Close()
            bw.Dispose()

            mem.Close()
            mem.Dispose()
        End If

        mem = New IO.MemoryStream()
        bw = New IO.BinaryWriter(mem)

        bmiHeader.biSize = 40
        bmiHeader.biWidth = bmpSize.Width
        bmiHeader.biHeight = bmpSize.Height
        bmiHeader.biPlanes = 1
        bmiHeader.biBitCount = 8
        bmiHeader.biCompression = BitmapCompressionMode.BI_RGB

        bw.Write(bmiHeader.biSize)
        bw.Write(bmiHeader.biWidth)
        bw.Write(bmiHeader.biHeight)
        bw.Write(bmiHeader.biPlanes)
        bw.Write(bmiHeader.biBitCount)
        bw.Write(bmiHeader.biCompression)
        bw.Write(bmiHeader.biSizeImage)
        bw.Write(bmiHeader.biXPelsperMeter)
        bw.Write(bmiHeader.biYPelsPerMeter)
        bw.Write(bmiHeader.biClrUsed)
        bw.Write(bmiHeader.biClrImportant)

        'Const nullByte = 0
        For i As Integer = 0 To 256 - 1
            Dim b As Byte = If(colorMode = ColorModes.BlackOnWhite, 255 - i, i)
            bw.Write(b)
            bw.Write(b)
            bw.Write(b)
            bw.Write(b)
        Next
    End Sub

    Public Shared Function GetBitmap(hdc As IntPtr, data() As Byte) As Bitmap
        If data IsNot Nothing Then
            Return Bitmap.FromHbitmap(CreateDIBitmap(hdc, bmiHeader, CBM_INIT, data, mem.ToArray(), DIB_RGB_COLORS))
        Else
            Return Bitmap.FromHbitmap(CreateDIBitmap(hdc, bmiHeader, 0, Nothing, mem.ToArray(), DIB_RGB_COLORS))
        End If
    End Function
End Class
