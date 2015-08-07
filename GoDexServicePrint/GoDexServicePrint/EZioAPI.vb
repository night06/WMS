'------------------------------------------------------------------------------------------------
' Create EZioApi.vb by Jeffrey 2014/07/14
'------------------------------------------------------------------------------------------------

Imports System.Runtime.InteropServices

Public Class EZioApi

    Public Declare Function openport Lib "DLL\Ezio32.dll" (ByVal Port As Integer) As Boolean

    Public Declare Function openport Lib "DLL\Ezio32.dll" (<MarshalAs(UnmanagedType.LPStr)> ByVal DeviceName As String) As Boolean

    Public Declare Function OpenDriver Lib "DLL\Ezio32.dll" (<MarshalAs(UnmanagedType.LPStr)> ByVal DeviceName As String) As Boolean

    Public Declare Function OpenNet Lib "DLL\Ezio32.dll" (<MarshalAs(UnmanagedType.LPStr)> ByVal IP As String, <MarshalAs(UnmanagedType.LPStr)> ByVal Port As String) As Boolean

    Public Declare Function setup Lib "DLL\Ezio32.dll" (ByVal LabelLength As Integer, ByVal Darkness As Integer, ByVal Speed As Integer, ByVal LabelMode As Integer, ByVal LabelGap As Integer, ByVal BlackTop As Integer) As Integer

    Public Declare Function setbaudrate Lib "DLL\Ezio32.dll" (ByVal BaudRte As Integer) As Integer

    Public Declare Sub closeport Lib "DLL\Ezio32.dll" ()

    Public Declare Sub sendbuf Lib "DLL\Ezio32.dll" (<MarshalAs(UnmanagedType.LPArray)> ByVal command As Byte(), ByVal length As Integer)

    Public Declare Function sendcommand Lib "DLL\Ezio32.dll" (<MarshalAs(UnmanagedType.LPStr)> ByVal command As String) As Integer

    Public Declare Function intloadimage Lib "DLL\Ezio32.dll" (<MarshalAs(UnmanagedType.LPStr)> ByVal Filename As String, <MarshalAs(UnmanagedType.LPStr)> ByVal ImageName As String, <MarshalAs(UnmanagedType.LPStr)> ByVal ImageType As String) As Integer

    Public Declare Function extloadimage Lib "DLL\Ezio32.dll" (<MarshalAs(UnmanagedType.LPStr)> ByVal Filename As String, <MarshalAs(UnmanagedType.LPStr)> ByVal ImageName As String, <MarshalAs(UnmanagedType.LPStr)> ByVal ImageType As String) As Integer

    Public Declare Function ecTextOut Lib "DLL\Ezio32.dll" (ByVal PosX As Integer, ByVal PosY As Integer, ByVal FontHeight As Integer, <MarshalAs(UnmanagedType.LPStr)> ByVal FontName As String, <MarshalAs(UnmanagedType.LPStr)> ByVal Data As String) As Integer

    Public Declare Function ecTextOutR Lib "DLL\Ezio32.dll" (ByVal PosX As Integer, ByVal PosY As Integer, ByVal FontHeight As Integer, <MarshalAs(UnmanagedType.LPStr)> ByVal FontName As String, <MarshalAs(UnmanagedType.LPStr)> ByVal Data As String, ByVal TextWidth As Integer, ByVal Dark As Integer, ByVal Rotate As Integer) As Integer

    Public Declare Function ecTextOutFine Lib "DLL\Ezio32.dll" (ByVal PosX As Integer, ByVal PosY As Integer, ByVal FontHeight As Integer, <MarshalAs(UnmanagedType.LPStr)> ByVal FontName As String, <MarshalAs(UnmanagedType.LPStr)> ByVal Data As String, ByVal TextWidth As Integer, ByVal Dark As Integer, ByVal Rotate As Integer, ByVal Italic As Integer, ByVal Underline As Integer, ByVal Strikeout As Integer, ByVal Inverse As Integer) As Integer

    Public Declare Function ecTextDownLoad Lib "DLL\Ezio32.dll" (ByVal FontHeight As Integer, ByVal FontName As String, ByVal Data As String, ByVal TextWidth As Integer, ByVal Dark As Integer, ByVal Rotate As Integer, <MarshalAs(UnmanagedType.LPStr)> ByVal ObjectName As String) As Integer

    Public Declare Function putimage Lib "DLL\Ezio32.dll" (ByVal PosX As Integer, ByVal PosY As Integer, <MarshalAs(UnmanagedType.LPStr)> ByVal Filename As String, ByVal Degree As Integer) As Integer

    Public Declare Function putimage_Halftone Lib "DLL\Ezio32.dll" (ByVal PosX As Integer, ByVal PosY As Integer, <MarshalAs(UnmanagedType.LPStr)> ByVal Filename As String, ByVal Degree As Integer, ByVal Halftone As Integer) As Integer

    Public Declare Function downloadimage Lib "DLL\Ezio32.dll" (<MarshalAs(UnmanagedType.LPStr)> ByVal Filename As String, ByVal Degree As Integer, <MarshalAs(UnmanagedType.LPStr)> ByVal ObjectName As String) As Integer

    Public Declare Function FindFirstUSB Lib "DLL\Ezio32.dll" (<MarshalAs(UnmanagedType.LPArray)> ByVal UsbID As Byte()) As Integer

    Public Declare Function RcvBuf Lib "DLL\Ezio32.dll" (ByVal ByteArray As Byte(), ByVal ByteArraySize As Integer) As Integer

    Public Declare Function FindNextUSB Lib "DLL\Ezio32.dll" (<MarshalAs(UnmanagedType.LPArray)> ByVal UsbID As Byte()) As Integer

    Public Declare Function OpenUSB Lib "DLL\Ezio32.dll" (<MarshalAs(UnmanagedType.LPStr)> ByVal UsbID As String) As Integer

    Public Declare Sub GetDllVersion Lib "DLL\Ezio32.dll" (<MarshalAs(UnmanagedType.LPArray)> ByVal Version As Byte())

    Public Declare Function Bar Lib "DLL\Ezio32.dll" (<MarshalAs(UnmanagedType.LPStr)> ByVal BarcodeType As String, ByVal PosX As Integer, ByVal PosY As Integer, ByVal Narrow As Integer, ByVal Wide As Integer, ByVal Height As Integer, ByVal Rotation As Integer, ByVal Readable As Integer, <MarshalAs(UnmanagedType.LPStr)> ByVal data As String) As Integer

    Public Declare Function Bar_S Lib "DLL\Ezio32.dll" (<MarshalAs(UnmanagedType.LPStr)> ByVal BarcodeType As String, ByVal PosX As Integer, ByVal PosY As Integer, <MarshalAs(UnmanagedType.LPStr)> ByVal data As String) As Integer

    Public Declare Function Bar_GS1DataBar Lib "DLL\Ezio32.dll" (<MarshalAs(UnmanagedType.LPStr)> ByVal BarcodeType As String, ByVal PosX As Integer, ByVal PosY As Integer, ByVal Narrow As Integer, ByVal Segment As Integer, ByVal Height As Integer, ByVal Rotation As Integer, ByVal Readable As Integer, <MarshalAs(UnmanagedType.LPStr)> ByVal data As String) As Integer

    Public Declare Function Bar_GS1DataBar_S Lib "DLL\Ezio32.dll" (<MarshalAs(UnmanagedType.LPStr)> ByVal BarcodeType As String, ByVal PosX As Integer, ByVal PosY As Integer, <MarshalAs(UnmanagedType.LPStr)> ByVal data As String) As Integer

    Public Declare Function Bar_PDF417 Lib "DLL\Ezio32.dll" (ByVal PosX As Integer, ByVal PosY As Integer, ByVal Width As Integer, ByVal Height As Integer, ByVal Row As Integer, ByVal Column As Integer, ByVal ErrorLevel As Integer, ByVal Len As Integer, ByVal Rotation As Integer, <MarshalAs(UnmanagedType.LPStr)> ByVal data As String) As Integer

    Public Declare Function Bar_PDF417_S Lib "DLL\Ezio32.dll" (ByVal PosX As Integer, ByVal PosY As Integer, ByVal Len As Integer, <MarshalAs(UnmanagedType.LPStr)> ByVal data As String) As Integer

    Public Declare Function Bar_MicroPDF417 Lib "DLL\Ezio32.dll" (ByVal PosX As Integer, ByVal PosY As Integer, ByVal Width As Integer, ByVal Height As Integer, ByVal Mode As Integer, ByVal Len As Integer, ByVal Rotation As Integer, <MarshalAs(UnmanagedType.LPStr)> ByVal data As String) As Integer

    Public Declare Function Bar_MicroPDF417_S Lib "DLL\Ezio32.dll" (ByVal PosX As Integer, ByVal PosY As Integer, ByVal Len As Integer, <MarshalAs(UnmanagedType.LPStr)> ByVal data As String) As Integer

    Public Declare Function Bar_Maxicode Lib "DLL\Ezio32.dll" (ByVal PosX As Integer, ByVal PosY As Integer, ByVal SymbolNo As Integer, ByVal SetNo As Integer, ByVal Mode As Integer, <MarshalAs(UnmanagedType.LPStr)> ByVal CountryCode As String, <MarshalAs(UnmanagedType.LPStr)> ByVal PostalCode As String, <MarshalAs(UnmanagedType.LPStr)> ByVal [Class] As String, ByVal Rotation As Integer, <MarshalAs(UnmanagedType.LPStr)> ByVal data As String) As Integer

    Public Declare Function Bar_Maxicode_S Lib "DLL\Ezio32.dll" (ByVal PosX As Integer, ByVal PosY As Integer, <MarshalAs(UnmanagedType.LPStr)> ByVal CountryCode As String, <MarshalAs(UnmanagedType.LPStr)> ByVal PostalCode As String, <MarshalAs(UnmanagedType.LPStr)> ByVal [Class] As String, ByVal Rotation As Integer, <MarshalAs(UnmanagedType.LPStr)> ByVal data As String) As Integer

    Public Declare Function Bar_DataMatrix Lib "DLL\Ezio32.dll" (ByVal PosX As Integer, ByVal PosY As Integer, ByVal Enlarge As Integer, <MarshalAs(UnmanagedType.LPStr)> ByVal RotationR As String, ByVal Len As Integer, <MarshalAs(UnmanagedType.LPStr)> ByVal data As String) As Integer

    Public Declare Function Bar_DataMatrix_S Lib "DLL\Ezio32.dll" (ByVal PosX As Integer, ByVal PosY As Integer, ByVal Len As Integer, <MarshalAs(UnmanagedType.LPStr)> ByVal data As String) As Integer

    Public Declare Function Bar_QRcode Lib "DLL\Ezio32.dll" (ByVal PosX As Integer, ByVal PosY As Integer, ByVal Mode As Integer, ByVal Type As Integer, <MarshalAs(UnmanagedType.LPStr)> ByVal ErrorLevel As String, ByVal Mask As Integer, ByVal Mul As Integer, ByVal Len As Integer, ByVal Rotation As Integer, <MarshalAs(UnmanagedType.LPStr)> ByVal data As String) As Integer

    Public Declare Function Bar_QRcode Lib "DLL\Ezio32.dll" (ByVal PosX As Integer, ByVal PosY As Integer, ByVal Mode As Integer, ByVal Type As Integer, <MarshalAs(UnmanagedType.LPStr)> ByVal ErrorLevel As String, ByVal Mask As Integer, ByVal Mul As Integer, ByVal Len As Integer, ByVal Rotation As Integer, <MarshalAs(UnmanagedType.LPArray)> ByVal data As Byte()) As Integer

    Public Declare Function Bar_QRcode_S Lib "DLL\Ezio32.dll" (ByVal PosX As Integer, ByVal PosY As Integer, ByVal Len As Integer, <MarshalAs(UnmanagedType.LPStr)> ByVal data As String) As Integer

    Public Declare Function Bar_QRcode_S Lib "DLL\Ezio32.dll" (ByVal PosX As Integer, ByVal PosY As Integer, ByVal Len As Integer, <MarshalAs(UnmanagedType.LPArray)> ByVal data As Byte()) As Integer

    Public Declare Function Bar_Aztec Lib "DLL\Ezio32.dll" (ByVal PosX As Integer, ByVal PosY As Integer, ByVal Rotation As Integer, ByVal Mul As Integer, <MarshalAs(UnmanagedType.LPStr)> ByVal ECICs As String, ByVal Type As Integer, <MarshalAs(UnmanagedType.LPStr)> ByVal MenuSymbol As String, ByVal Len As Integer, <MarshalAs(UnmanagedType.LPStr)> ByVal data As String) As Integer

    Public Declare Function Bar_Aztec_S Lib "DLL\Ezio32.dll" (ByVal PosX As Integer, ByVal PosY As Integer, ByVal Len As Integer, <MarshalAs(UnmanagedType.LPStr)> ByVal data As String) As Integer

    Public Declare Function DrawHorLine Lib "DLL\Ezio32.dll" (ByVal PosX As Integer, ByVal PosY As Integer, ByVal Length As Integer, ByVal Thick As Integer) As Integer

    Public Declare Function DrawVerLine Lib "DLL\Ezio32.dll" (ByVal PosX As Integer, ByVal PosY As Integer, ByVal Length As Integer, ByVal Thick As Integer) As Integer

    Public Declare Function FillRec Lib "DLL\Ezio32.dll" (ByVal PosX As Integer, ByVal PosY As Integer, ByVal Rec_W As Integer, ByVal Rec_H As Integer) As Integer

    Public Declare Function DrawRec Lib "DLL\Ezio32.dll" (ByVal PosX As Integer, ByVal PosY As Integer, ByVal Rec_W As Integer, ByVal Rec_H As Integer, ByVal lrw As Integer, ByVal ubw As Integer) As Integer

    Public Declare Function DrawOblique Lib "DLL\Ezio32.dll" (ByVal PosX1 As Integer, ByVal PosY1 As Integer, ByVal Thick As Integer, ByVal PosX2 As Integer, ByVal PosY2 As Integer) As Integer

    Public Declare Function DrawEllipse Lib "DLL\Ezio32.dll" (ByVal PosX As Integer, ByVal PosY As Integer, ByVal Ellipse_W As Integer, ByVal Ellipse_H As Integer, ByVal PenWidth As Integer) As Integer

    Public Declare Function DrawRoundRec Lib "DLL\Ezio32.dll" (ByVal PosX As Integer, ByVal PosY As Integer, ByVal Rec_W As Integer, ByVal Rec_H As Integer, ByVal Arc_W As Integer, ByVal Arc_H As Integer, ByVal PenWidth As Integer) As Integer

    Public Declare Function DrawTriangle Lib "DLL\Ezio32.dll" (ByVal PosX1 As Integer, ByVal PosY1 As Integer, ByVal PosX2 As Integer, ByVal PosY2 As Integer, ByVal PosX3 As Integer, ByVal PosY3 As Integer, ByVal Thick As Integer) As Integer

    Public Declare Function DrawDiamond Lib "DLL\Ezio32.dll" (ByVal PosX As Integer, ByVal PosY As Integer, ByVal Diamand_W As Integer, ByVal Diamand_H As Integer, ByVal Thick As Integer) As Integer

    Public Declare Function InternalFont_TextOut Lib "DLL\Ezio32.dll" (<MarshalAs(UnmanagedType.LPStr)> ByVal FontType As String, ByVal PosX As Integer, ByVal PosY As Integer, ByVal Mul_X As Integer, ByVal Mul_Y As Integer, ByVal Gap As Integer, <MarshalAs(UnmanagedType.LPStr)> ByVal RotationInverse As String, <MarshalAs(UnmanagedType.LPStr)> ByVal Data As String) As Integer

    Public Declare Function InternalFont_TextOut_S Lib "DLL\Ezio32.dll" (<MarshalAs(UnmanagedType.LPStr)> ByVal FontType As String, ByVal PosX As Integer, ByVal PosY As Integer, <MarshalAs(UnmanagedType.LPStr)> ByVal Data As String) As Integer

    Public Declare Function DownloadFont_TextOut Lib "DLL\Ezio32.dll" (<MarshalAs(UnmanagedType.LPStr)> ByVal FontName As String, ByVal PosX As Integer, ByVal PosY As Integer, ByVal Mul_X As Integer, ByVal Mul_Y As Integer, ByVal Gap As Integer, <MarshalAs(UnmanagedType.LPStr)> ByVal RotationInverse As String, <MarshalAs(UnmanagedType.LPStr)> ByVal Data As String) As Integer

    Public Declare Function DownloadFont_TextOut_S Lib "DLL\Ezio32.dll" (<MarshalAs(UnmanagedType.LPStr)> ByVal FontName As String, ByVal PosX As Integer, ByVal PosY As Integer, <MarshalAs(UnmanagedType.LPStr)> ByVal Data As String) As Integer

    Public Declare Function TrueTypeFont_TextOut Lib "DLL\Ezio32.dll" (<MarshalAs(UnmanagedType.LPStr)> ByVal FontName As String, ByVal PosX As Integer, ByVal PosY As Integer, ByVal Font_W As Integer, ByVal Font_H As Integer, ByVal SpaceChar As Integer, <MarshalAs(UnmanagedType.LPStr)> ByVal RotationInverse As String, <MarshalAs(UnmanagedType.LPStr)> ByVal TTFTable As String, ByVal WidthMode As Integer, <MarshalAs(UnmanagedType.LPStr)> ByVal Data As String) As Integer

    Public Declare Function TrueTypeFont_TextOut_S Lib "DLL\Ezio32.dll" (<MarshalAs(UnmanagedType.LPStr)> ByVal FontName As String, ByVal PosX As Integer, ByVal PosY As Integer, <MarshalAs(UnmanagedType.LPStr)> ByVal Data As String) As Integer


    <DllImport("DLL\Ezio32.dll", CallingConvention:=CallingConvention.StdCall, CharSet:=CharSet.Unicode, SetLastError:=True)> _
    Public Shared Function ecTextOutW(ByVal PosX As Integer, _
                               ByVal PosY As Integer, _
                               ByVal FontSize As Integer, _
                               <MarshalAs(UnmanagedType.LPStr)> ByVal FontName As String, _
                               <MarshalAs(UnmanagedType.LPArray)> ByVal DataText() As Byte, _
                               ByVal DataLength As Integer) As Boolean
    End Function

    <DllImport("DLL\Ezio32.dll", CallingConvention:=CallingConvention.StdCall, CharSet:=CharSet.Unicode, SetLastError:=True)> _
    Public Shared Function ecTextOutRW(ByVal PosX As Integer, _
                                ByVal PosY As Integer, _
                                ByVal FontSize As Integer, _
                                <MarshalAs(UnmanagedType.LPStr)> ByVal FontName As String, _
                                <MarshalAs(UnmanagedType.LPArray)> ByVal DataText() As Byte, _
                                ByVal FontWidth As Integer, _
                                ByVal Dark As Integer, _
                                ByVal Rotate As Integer, _
                                ByVal DataLength As Integer) As Boolean
    End Function

    <DllImport("DLL\Ezio32.dll", CallingConvention:=CallingConvention.StdCall, CharSet:=CharSet.Unicode, SetLastError:=True)> _
    Public Shared Function ecTextOutFineW(ByVal PosX As Integer, _
                                   ByVal PosY As Integer, _
                                   ByVal FontSize As Integer, _
                                   <MarshalAs(UnmanagedType.LPStr)> ByVal FontName As String, _
                                   <MarshalAs(UnmanagedType.LPArray)> ByVal DataText() As Byte, _
                                   ByVal FontWidth As Integer, _
                                   ByVal Dark As Integer, _
                                   ByVal Rotate As Integer, _
                                   ByVal Italic As Integer, _
                                   ByVal Underline As Integer, _
                                   ByVal Strickout As Integer, _
                                   ByVal Inverse As Integer, _
                                   ByVal DataLength As Integer) As Boolean
    End Function

End Class
