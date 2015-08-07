'------------------------------------------------------------------------------------------------
' Create GodexPrinter.vb by Jeffrey 2014/07/14
'------------------------------------------------------------------------------------------------

Imports System
Imports System.Collections
Imports System.Text
Imports System.Drawing.Printing

#Region "Enum Definition"

Public Enum HalftoneMode
    None
    ClusterDithering
    DispersedDithering
    DiffisionDithering
End Enum

Public Enum BarCodeType

    Code39_Extended             ' BA
    Code39_Extended_CheckDidit  ' BA2
    Code39                      ' BA3
    Code39_CheckDidit           ' BA4
    EAN8                        ' BB
    EAN8_Add2                   ' BC
    EAN8_Add5                   ' BD
    EAN13                       ' BE
    EAN13_Add2                  ' BF
    EAN13_Add5                  ' BG
    UPCA                        ' BH
    UPCA_Add2                   ' BI
    UPCA_Add5                   ' BJ
    UPCE                        ' BK
    UPCE_Add2                   ' BL
    UPCE_Add5                   ' BM
    I2of5                       ' BN
    I2of5_CheckDigit            ' BN2
    Codabar                     ' BO
    Code93                      ' BP
    Code128_Auto                ' BQ
    Code128_Subset              ' BQ2
    UCC_128                     ' BR
    PostNET                     ' BS
    ITF14                       ' BT
    EAN128                      ' BU
    RPS128                      ' BV
    HIBC                        ' BX
    MSI_1MOD10                  ' BY
    MSI_2MOD10                  ' BY2
    MSI_1MOD1110                ' BY3
    MSI_NoDigitCheck            ' BY4
    I2of5_ShippingBearerBars    ' BZ
    UCC_EAN128_KMART            ' B1
    UCC_EAN128_RANDOM           ' B2
    Telepen                     ' B3
    FIM                         ' B4
    Plessey                     ' B7


End Enum

Public Enum PortType
    LPT1
    COM1
    COM2
    COM3
    COM4
    LPT2
    USB
End Enum

Public Enum PaperMode
    GapLabel
    PlainPaperLabel
End Enum

Public Enum RotateMode
    Angle_0
    Angle_90 = 90
    Angle_180 = 180
    Angle_270 = 270
End Enum

Public Enum FontWeight
    FW_100_THIN = 100
    FW_200_EXTRALIGHT = 200
    FW_300_LIGHT = 300
    FW_400_NORMAL = 400
    FW_500_MEDIUM = 500
    FW_600_FW_SEMIBOLD = 600
    FW_700_BOLD = 700
    FW_800_EXTRABOLD = 800
    FW_900_HEAVY = 900
End Enum

Public Enum Italic_State
    S_OFF
    S_ON
End Enum

Public Enum Underline_State
    S_OFF
    S_ON
End Enum

Public Enum Strikeout_State
    S_OFF
    S_ON
End Enum

Public Enum Inverse_State
    S_OFF
    S_ON
End Enum

Public Enum Image_Type
    BMP
    PCX
End Enum

#End Region

Public Class GoDexPrinterCommand

    Private Shared BarcodeTypeHash As Hashtable = New Hashtable()

    Private Shared Sub GetBarcodeTypeHash()

        If GoDexPrinterCommand.BarcodeTypeHash.Count <= 0 Then
            GoDexPrinterCommand.BarcodeTypeHash(BarCodeType.Code39_Extended) = "A"
            GoDexPrinterCommand.BarcodeTypeHash(BarCodeType.Code39_Extended_CheckDidit) = "A2"
            GoDexPrinterCommand.BarcodeTypeHash(BarCodeType.Code39) = "A3"
            GoDexPrinterCommand.BarcodeTypeHash(BarCodeType.Code39_CheckDidit) = "A4"
            GoDexPrinterCommand.BarcodeTypeHash(BarCodeType.EAN8) = "B"
            GoDexPrinterCommand.BarcodeTypeHash(BarCodeType.EAN8_Add2) = "C"
            GoDexPrinterCommand.BarcodeTypeHash(BarCodeType.EAN8_Add5) = "D"
            GoDexPrinterCommand.BarcodeTypeHash(BarCodeType.EAN13) = "E"
            GoDexPrinterCommand.BarcodeTypeHash(BarCodeType.EAN13_Add2) = "F"
            GoDexPrinterCommand.BarcodeTypeHash(BarCodeType.EAN13_Add5) = "G"
            GoDexPrinterCommand.BarcodeTypeHash(BarCodeType.UPCA) = "H"
            GoDexPrinterCommand.BarcodeTypeHash(BarCodeType.UPCA_Add2) = "I"
            GoDexPrinterCommand.BarcodeTypeHash(BarCodeType.UPCA_Add5) = "J"
            GoDexPrinterCommand.BarcodeTypeHash(BarCodeType.UPCE) = "K"
            GoDexPrinterCommand.BarcodeTypeHash(BarCodeType.UPCE_Add2) = "L"
            GoDexPrinterCommand.BarcodeTypeHash(BarCodeType.UPCE_Add5) = "M"
            GoDexPrinterCommand.BarcodeTypeHash(BarCodeType.I2of5) = "N"
            GoDexPrinterCommand.BarcodeTypeHash(BarCodeType.I2of5_CheckDigit) = "N2"
            GoDexPrinterCommand.BarcodeTypeHash(BarCodeType.Codabar) = "O"
            GoDexPrinterCommand.BarcodeTypeHash(BarCodeType.Code93) = "P"
            GoDexPrinterCommand.BarcodeTypeHash(BarCodeType.Code128_Auto) = "Q"
            GoDexPrinterCommand.BarcodeTypeHash(BarCodeType.Code128_Subset) = "Q2"
            GoDexPrinterCommand.BarcodeTypeHash(BarCodeType.UCC_128) = "R"
            GoDexPrinterCommand.BarcodeTypeHash(BarCodeType.PostNET) = "S"
            GoDexPrinterCommand.BarcodeTypeHash(BarCodeType.ITF14) = "T"
            GoDexPrinterCommand.BarcodeTypeHash(BarCodeType.EAN128) = "U"
            GoDexPrinterCommand.BarcodeTypeHash(BarCodeType.RPS128) = "V"
            GoDexPrinterCommand.BarcodeTypeHash(BarCodeType.HIBC) = "X"
            GoDexPrinterCommand.BarcodeTypeHash(BarCodeType.MSI_1MOD10) = "Y"
            GoDexPrinterCommand.BarcodeTypeHash(BarCodeType.MSI_2MOD10) = "Y2"
            GoDexPrinterCommand.BarcodeTypeHash(BarCodeType.MSI_1MOD1110) = "Y3"
            GoDexPrinterCommand.BarcodeTypeHash(BarCodeType.MSI_NoDigitCheck) = "Y4"
            GoDexPrinterCommand.BarcodeTypeHash(BarCodeType.I2of5_ShippingBearerBars) = "Z"
            GoDexPrinterCommand.BarcodeTypeHash(BarCodeType.UCC_EAN128_KMART) = "1"
            GoDexPrinterCommand.BarcodeTypeHash(BarCodeType.UCC_EAN128_RANDOM) = "2"
            GoDexPrinterCommand.BarcodeTypeHash(BarCodeType.Telepen) = "3"
            GoDexPrinterCommand.BarcodeTypeHash(BarCodeType.FIM) = "4"
            GoDexPrinterCommand.BarcodeTypeHash(BarCodeType.Plessey) = "7"

        End If

    End Sub

    Public Sub JobStart()
        EZioApi.sendcommand("^L")
    End Sub

    Public Sub JobEnd()
        EZioApi.sendcommand("E")
    End Sub

    Public Function PrintText(ByVal PosX As Integer, ByVal PosY As Integer, ByVal FontHeight As Integer, ByVal FontName As String, ByVal Data As String) As Integer
        Return EZioApi.ecTextOut(PosX, PosY, FontHeight, FontName, Data)
    End Function

    Public Function PrintText(ByVal PosX As Integer, ByVal PosY As Integer, ByVal FontHeight As Integer, ByVal FontName As String, ByVal Data As String, ByVal TextWidth As Integer, ByVal Dark As FontWeight, ByVal Rotate As RotateMode) As Integer
        Return EZioApi.ecTextOutR(PosX, PosY, FontHeight, FontName, Data, TextWidth, CInt(Dark), CInt(Rotate))
    End Function

    Public Function PrintText(ByVal PosX As Integer, ByVal PosY As Integer, ByVal FontHeight As Integer, ByVal FontName As String, ByVal Data As String, ByVal TextWidth As Integer, ByVal Dark As FontWeight, ByVal Rotate As RotateMode, ByVal Italic As Italic_State, ByVal Underline As Underline_State, ByVal Strikeout As Strikeout_State, ByVal Inverse As Inverse_State) As Integer
        Return EZioApi.ecTextOutFine(PosX, PosY, FontHeight, FontName, Data, TextWidth, CInt(Dark), CInt(Rotate), CInt(Italic), CInt(Underline), CInt(Strikeout), CInt(Inverse))
    End Function

    Public Function PrintText_Unicode(ByVal PosX As Integer, ByVal PosY As Integer, ByVal FontHeight As Integer, ByVal FontName As String, ByVal Data As String) As Integer
        Dim bytes As Byte() = Encoding.Unicode.GetBytes(Data)
        Return EZioApi.ecTextOutW(PosX, PosY, FontHeight, FontName, bytes, Data.Length)
    End Function

    Public Function PrintText_Unicode(ByVal PosX As Integer, ByVal PosY As Integer, ByVal FontHeight As Integer, ByVal FontName As String, ByVal Data As String, ByVal TextWidth As Integer, ByVal Dark As FontWeight, ByVal Rotate As RotateMode) As Integer
        Dim bytes As Byte() = Encoding.Unicode.GetBytes(Data)
        Return EZioApi.ecTextOutRW(PosX, PosY, FontHeight, FontName, bytes, TextWidth, CInt(Dark), CInt(Rotate), Data.Length)
    End Function

    Public Function PrintText_Unicode(ByVal PosX As Integer, ByVal PosY As Integer, ByVal FontHeight As Integer, ByVal FontName As String, ByVal Data As String, ByVal TextWidth As Integer, ByVal Dark As FontWeight, ByVal Rotate As RotateMode, ByVal Italic As Italic_State, ByVal Underline As Underline_State, ByVal Strikeout As Strikeout_State, ByVal Inverse As Inverse_State) As Integer
        Dim bytes As Byte() = Encoding.Unicode.GetBytes(Data)
        Return EZioApi.ecTextOutFineW(PosX, PosY, FontHeight, FontName, bytes, TextWidth, CInt(Dark), CInt(Rotate), CInt(Italic), CInt(Underline), CInt(Strikeout), CInt(Inverse), Data.Length)
    End Function

    Public Function PrintText_EZPL_Internal(ByVal FontType As String, ByVal PosX As Integer, ByVal PosY As Integer, ByVal Mul_X As Integer, ByVal Mul_Y As Integer, ByVal Gap As Integer, ByVal RotationInverse As String, ByVal Data As String) As Integer
        Return EZioApi.InternalFont_TextOut(FontType, PosX, PosY, Mul_X, Mul_Y, Gap, RotationInverse, Data)
    End Function

    Public Function PrintText_EZPL_Internal(ByVal FontType As String, ByVal PosX As Integer, ByVal PosY As Integer, ByVal Data As String) As Integer
        Return EZioApi.InternalFont_TextOut_S(FontType, PosX, PosY, Data)
    End Function

    Public Function PrintText_EZPL_Bitmapped(ByVal FontName As String, ByVal PosX As Integer, ByVal PosY As Integer, ByVal Mul_X As Integer, ByVal Mul_Y As Integer, ByVal Gap As Integer, ByVal RotationInverse As String, ByVal Data As String) As Integer
        Return EZioApi.DownloadFont_TextOut(FontName, PosX, PosY, Mul_X, Mul_Y, Gap, RotationInverse, Data)
    End Function

    Public Function PrintText_EZPL_Bitmapped(ByVal FontName As String, ByVal PosX As Integer, ByVal PosY As Integer, ByVal Data As String) As Integer
        Return EZioApi.DownloadFont_TextOut_S(FontName, PosX, PosY, Data)
    End Function

    Public Function PrintText_EZPL_TTF(ByVal FontName As String, ByVal PosX As Integer, ByVal PosY As Integer, ByVal Font_W As Integer, ByVal Font_H As Integer, ByVal SpaceChar As Integer, ByVal RotationInverse As String, ByVal TTFTable As String, ByVal WidthMode As Integer, ByVal Data As String) As Integer
        Return EZioApi.TrueTypeFont_TextOut(FontName, PosX, PosY, Font_W, Font_H, SpaceChar, RotationInverse, TTFTable, WidthMode, Data)
    End Function

    Public Function PrintText_EZPL_TTF(ByVal FontName As String, ByVal PosX As Integer, ByVal PosY As Integer, ByVal Data As String) As Integer
        Return EZioApi.TrueTypeFont_TextOut_S(FontName, PosX, PosY, Data)
    End Function

    Public Sub UploadImage_Int(ByVal Filename As String, ByVal DisplayName As String, ByVal mType As Image_Type)
        Dim imageType As String
        If mType = Image_Type.BMP Then
            imageType = "bmp"
        Else
            imageType = "pcx"
        End If
        EZioApi.sendcommand("~MDELG," + DisplayName)
        EZioApi.intloadimage(Filename, DisplayName, imageType)
    End Sub

    Public Sub UploadImage_Ext(ByVal Filename As String, ByVal DisplayName As String, ByVal mType As Image_Type)
        Dim imageType As String
        If mType = Image_Type.BMP Then
            imageType = "bmp"
        Else
            imageType = "pcx"
        End If
        EZioApi.sendcommand("~MDELG," + DisplayName)
        EZioApi.extloadimage(Filename, DisplayName, imageType)
    End Sub

    Public Sub UploadImage_FullColor(ByVal Filename As String, ByVal DisplayName As String, ByVal nRotate As Integer)
        EZioApi.sendcommand("~MDELG," + DisplayName)
        EZioApi.downloadimage(Filename, nRotate, DisplayName)
    End Sub

    Public Sub UploadText(ByVal FontHeight As Integer, ByVal FontName As String, ByVal Data As String, ByVal TextWidth As Integer, ByVal Dark As FontWeight, ByVal Rotate As RotateMode, ByVal Name As String)
        EZioApi.sendcommand("~MDELG," + Name)
        EZioApi.ecTextDownLoad(FontHeight, FontName, Data, TextWidth, CInt(Dark), CInt(Rotate), Name)
    End Sub

    Public Sub PrintImageByName(ByVal DisplayName As String, ByVal PosX As Integer, ByVal PosY As Integer)
        EZioApi.sendcommand("Y" & PosX.ToString() & "," & PosY.ToString() & "," & DisplayName)
    End Sub

    Public Function PrintImage(ByVal PosX As Integer, ByVal PosY As Integer, ByVal FileName As String, ByVal mRotation As Integer) As Integer
        Return EZioApi.putimage(PosX, PosY, FileName, mRotation)
    End Function

    Public Sub AutoSensing()
        EZioApi.sendcommand("~S,SENSOR,0")
    End Sub

    Public Function Send(ByVal Cmd As String) As Integer
        Return EZioApi.sendcommand(Cmd)
    End Function

    Public Sub SendByte(ByVal ByteArray As Byte())
        EZioApi.sendbuf(ByteArray, ByteArray.Length)
    End Sub

    Public Sub SendByte(ByVal ByteArray As Byte(), ByVal nLen As Integer)
        EZioApi.sendbuf(ByteArray, nLen)
    End Sub

    Public Function Read() As String

        Dim MyArray(2048) As Byte
        Dim RetData As String = ""
        Dim RetryNo As Integer = 3
        Dim CurNo As Integer = 0

        While (True)

            If (EZioApi.RcvBuf(MyArray, MyArray.Length) = 0) Then
                CurNo = CurNo + 1
            Else
                CurNo = 0
                Dim Temp() As String = Encoding.Default.GetString(MyArray).Split(ControlChars.NullChar)
                RetData = RetData & Temp(0)
                Array.Clear(MyArray, 0, MyArray.Length)
            End If

            If (CurNo >= RetryNo) Then
                Exit While
            End If

        End While

        Return RetData

    End Function

    Public Function PrintBarCode(ByVal mBarCodeType As BarCodeType, ByVal PosX As Integer, ByVal PosY As Integer, ByVal Data As String) As Integer
        GoDexPrinterCommand.GetBarcodeTypeHash()
        Return EZioApi.Bar_S(GoDexPrinterCommand.BarcodeTypeHash(mBarCodeType).ToString(), PosX, PosY, Data)
    End Function

    Public Function PrintBarCode(ByVal mBarCodeType As BarCodeType, ByVal PosX As Integer, ByVal PosY As Integer, ByVal Narrow As Integer, ByVal Wide As Integer, ByVal Height As Integer, ByVal Rotation As Integer, ByVal Raedable As Integer, ByVal Data As String) As Integer
        GoDexPrinterCommand.GetBarcodeTypeHash()
        Return EZioApi.Bar(GoDexPrinterCommand.BarcodeTypeHash(mBarCodeType).ToString(), PosX, PosY, Narrow, Wide, Height, Rotation, Raedable, Data)
    End Function

    Public Function PrintQRCode(ByVal PosX As Integer, ByVal PosY As Integer, ByVal Data As String) As Integer
        Return EZioApi.Bar_QRcode_S(PosX, PosY, Encoding.[Default].GetByteCount(Data), Data)
    End Function

    Public Function PrintQRCode(ByVal PosX As Integer, ByVal PosY As Integer, ByVal Data As String, ByVal mEncoding As Encoding) As Integer
        Dim ByteData() As Byte = mEncoding.GetBytes(Data)
        Return EZioApi.Bar_QRcode_S(PosX, PosY, mEncoding.GetByteCount(Data), Data)
    End Function

    Public Function PrintQRCode(ByVal PosX As Integer, ByVal PosY As Integer, ByVal Mode As Integer, ByVal Type As Integer, ByVal ErrorLavel As String, ByVal Mask As Integer, ByVal Mul As Integer, ByVal Rotation As Integer, ByVal Data As String) As Integer
        Return EZioApi.Bar_QRcode(PosX, PosY, Mode, Type, ErrorLavel, Mask, Mul, Encoding.[Default].GetByteCount(Data), Rotation, Data)
    End Function

    Public Function PrintQRCode_UTF8(ByVal PosX As Integer, ByVal PosY As Integer, ByVal Data As String) As Integer
        Dim bytes As Byte() = Encoding.UTF8.GetBytes(Data)
        Return EZioApi.Bar_QRcode_S(PosX, PosY, Encoding.UTF8.GetByteCount(Data), bytes)
    End Function

    Public Function PrintQRCode_UTF8(ByVal PosX As Integer, ByVal PosY As Integer, ByVal Mode As Integer, ByVal Type As Integer, ByVal ErrorLavel As String, ByVal Mask As Integer, ByVal Mul As Integer, ByVal Rotation As Integer, ByVal Data As String) As Integer
        Dim bytes As Byte() = Encoding.UTF8.GetBytes(Data)
        Return EZioApi.Bar_QRcode(PosX, PosY, Mode, Type, ErrorLavel, Mask, Mul, Encoding.UTF8.GetByteCount(Data), Rotation, bytes)
    End Function

    Public Function PrintPDF417(ByVal PosX As Integer, ByVal PosY As Integer, ByVal Data As String) As Integer
        Return EZioApi.Bar_PDF417_S(PosX, PosY, Encoding.[Default].GetByteCount(Data), Data)
    End Function

    Public Function PrintPDF417(ByVal PosX As Integer, ByVal PosY As Integer, ByVal Width As Integer, ByVal Height As Integer, ByVal Row As Integer, ByVal Col As Integer, ByVal ErrorLevel As Integer, ByVal Rotation As Integer, ByVal Data As String) As Integer
        Return EZioApi.Bar_PDF417(PosX, PosY, Width, Height, Row, Col, ErrorLevel, Encoding.[Default].GetByteCount(Data), Rotation, Data)
    End Function

    Public Function PrintAztec(ByVal PosX As Integer, ByVal PosY As Integer, ByVal Data As String) As Integer
        Return EZioApi.Bar_Aztec_S(PosX, PosY, Encoding.[Default].GetByteCount(Data), Data)
    End Function

    Public Function PrintAztec(ByVal PosX As Integer, ByVal PosY As Integer, ByVal Rotation As Integer, ByVal Mul As Integer, ByVal ECICs As String, ByVal MenuSymbol As String, ByVal Type As Integer, ByVal Data As String) As Integer
        Return EZioApi.Bar_Aztec(PosX, PosY, Rotation, Mul, ECICs, Type, MenuSymbol, Encoding.[Default].GetByteCount(Data), Data)
    End Function

    Public Function PrintMaxiCode(ByVal PosX As Integer, ByVal PosY As Integer, ByVal CountryCode As String, ByVal PostalCode As String, ByVal nClass As String, ByVal Data As String) As Integer
        Return EZioApi.Bar_Maxicode_S(PosX, PosY, CountryCode, PostalCode, nClass, 0, Data)
    End Function

    Public Function PrintMaxiCode(ByVal PosX As Integer, ByVal PosY As Integer, ByVal SymbolNo As Integer, ByVal SetNo As Integer, ByVal Mode As Integer, ByVal CountryCode As String, ByVal PostalCode As String, ByVal nClass As String, ByVal Rotate As Integer, ByVal Data As String) As Integer
        Return EZioApi.Bar_Maxicode(PosX, PosY, SymbolNo, SetNo, Mode, CountryCode, PostalCode, nClass, Rotate, Data)
    End Function

    Public Function PrintDataMatrix(ByVal PosX As Integer, ByVal PosY As Integer, ByVal Data As String) As Integer
        Return EZioApi.Bar_DataMatrix_S(PosX, PosY, Encoding.[Default].GetByteCount(Data), Data)
    End Function

    Public Function PrintDataMatrix(ByVal PosX As Integer, ByVal PosY As Integer, ByVal Enlarge As Integer, ByVal RotationR As String, ByVal Data As String) As Integer
        Return EZioApi.Bar_DataMatrix(PosX, PosY, Enlarge, RotationR, Encoding.[Default].GetByteCount(Data), Data)
    End Function

    Public Function DrawHorLine(ByVal PosX As Integer, ByVal PosY As Integer, ByVal Length As Integer, ByVal Thick As Integer) As Integer
        Return EZioApi.DrawHorLine(PosX, PosY, Length, Thick)
    End Function

    Public Function DrawVerLine(ByVal PosX As Integer, ByVal PosY As Integer, ByVal Length As Integer, ByVal Thick As Integer) As Integer
        Return EZioApi.DrawVerLine(PosX, PosY, Length, Thick)
    End Function

    Public Function FillRec(ByVal PosX As Integer, ByVal PosY As Integer, ByVal Rec_W As Integer, ByVal Rec_H As Integer) As Integer
        Return EZioApi.FillRec(PosX, PosY, Rec_W, Rec_H)
    End Function

    Public Function DrawRec(ByVal PosX As Integer, ByVal PosY As Integer, ByVal Rec_W As Integer, ByVal Rec_H As Integer, ByVal lrw As Integer, ByVal ubw As Integer) As Integer
        Return EZioApi.DrawRec(PosX, PosY, Rec_W, Rec_H, lrw, ubw)
    End Function

    Public Function DrawOblique(ByVal PosX1 As Integer, ByVal PosY1 As Integer, ByVal Thick As Integer, ByVal PosX2 As Integer, ByVal PosY2 As Integer) As Integer
        Return EZioApi.DrawOblique(PosX1, PosY1, Thick, PosX2, PosY2)
    End Function

    Public Function DrawEllipse(ByVal PosX As Integer, ByVal PosY As Integer, ByVal Ellipse_W As Integer, ByVal Ellipse_H As Integer, ByVal PenWidth As Integer) As Integer
        Return EZioApi.DrawEllipse(PosX, PosY, Ellipse_W, Ellipse_H, PenWidth)
    End Function

    Public Function DrawRoundRec(ByVal PosX As Integer, ByVal PosY As Integer, ByVal Rec_W As Integer, ByVal Rec_H As Integer, ByVal Arc_W As Integer, ByVal Arc_H As Integer, ByVal PenWidth As Integer) As Integer
        Return EZioApi.DrawRoundRec(PosX, PosY, Rec_W, Rec_H, Arc_W, Arc_H, PenWidth)
    End Function

    Public Function DrawTriangle(ByVal PosX1 As Integer, ByVal PosY1 As Integer, ByVal PosX2 As Integer, ByVal PosY2 As Integer, ByVal PosX3 As Integer, ByVal PosY3 As Integer, ByVal PenWidth As Integer) As Integer
        Return EZioApi.DrawTriangle(PosX1, PosY1, PosX2, PosY2, PosX3, PosY3, PenWidth)
    End Function

    Public Function DrawDiamond(ByVal PosX As Integer, ByVal PosY As Integer, ByVal Rec_W As Integer, ByVal Rec_H As Integer, ByVal PenWidth As Integer) As Integer
        Return EZioApi.DrawDiamond(PosX, PosY, Rec_W, Rec_H, PenWidth)
    End Function

    Public Function PrintHalftoneImage(ByVal PosX As Integer, ByVal PosY As Integer, ByVal FileName As String, ByVal mRotation As Integer, ByVal nHalftoneMode As HalftoneMode) As Integer
        Return EZioApi.putimage_Halftone(PosX, PosY, FileName, mRotation, CInt(nHalftoneMode))
    End Function

End Class

Public Class clsPrinterConfig

    Public Sub Setup(ByVal LabelLength As Integer, ByVal Darkness As Integer, ByVal Speed As Integer, ByVal LabelMode As Integer, ByVal LabelGap As Integer, ByVal BlackTop As Integer)
        EZioApi.setup(LabelLength, Darkness, Speed, LabelMode, LabelGap, BlackTop)
    End Sub

    Public Sub LabelMode(ByVal nMode As PaperMode, ByVal nLabelHeight As Integer, ByVal nGapFeed As Integer)
        If nMode = PaperMode.GapLabel Then
            EZioApi.sendcommand("^Q" + nLabelHeight.ToString() + "," + nGapFeed.ToString())
        Else
            EZioApi.sendcommand("^Q" + nLabelHeight.ToString() + ",0," + nGapFeed.ToString())
        End If
    End Sub

    Public Sub LabelWidth(ByVal nWidth As Integer)
        EZioApi.sendcommand("^W" + nWidth.ToString())
    End Sub

    Public Sub Dark(ByVal nDark As Integer)
        EZioApi.sendcommand("^H" + nDark.ToString())
    End Sub

    Public Sub Speed(ByVal nSpeed As Integer)
        EZioApi.sendcommand("^S" + nSpeed.ToString())
    End Sub

    Public Sub PageNo(ByVal nPageNo As Integer)
        EZioApi.sendcommand("^P" + nPageNo.ToString())
    End Sub

    Public Sub CopyNo(ByVal nCopyNo As Integer)
        EZioApi.sendcommand("^C" + nCopyNo.ToString())
    End Sub

End Class

Public Class GodexPrinter

    Public Command As GoDexPrinterCommand = New GoDexPrinterCommand()
    Public Config As clsPrinterConfig = New clsPrinterConfig()

    Public Shared Function GetDriverPrinter(Optional ByVal FilterName As String = "GoDEX") As String()

        Dim array() As String = Nothing
        Dim arrayList As ArrayList = New ArrayList()
        For Each text As String In PrinterSettings.InstalledPrinters
            If text.ToUpper().Contains(FilterName.ToUpper()) Then
                arrayList.Add(text)
            End If
        Next

        If arrayList.Count > 0 Then
            ReDim array(arrayList.Count - 1)
            For i As Integer = 0 To array.Length - 1
                array(i) = arrayList(i).ToString()
            Next
        End If
        Return array

    End Function

    Public Sub Open(ByVal mPortType As PortType)
        Dim num As Integer = CInt(mPortType)
        EZioApi.openport(num.ToString())
    End Sub

    Public Sub Open(ByVal PortName As String)
        If PortName.Contains("COM") Then
            EZioApi.OpenUSB(PortName)
        Else
            EZioApi.OpenDriver(PortName)
        End If
    End Sub

    Public Sub Open(ByVal strIP As String, ByVal nPort As Integer)
        EZioApi.OpenNet(strIP, nPort.ToString())
    End Sub

    Public Sub SetBaudrate(ByVal nBaud As Integer)
        EZioApi.setbaudrate(nBaud)
    End Sub

    Public Sub Close()
        EZioApi.closeport()
    End Sub

    Public Function GetDllVersion() As String
        Dim Array(100) As Byte
        System.Array.Clear(Array, 0, Array.Length)
        EZioApi.GetDllVersion(Array)
        Return Encoding.ASCII.GetString(Array)
    End Function

End Class


