Public Class Form1
    Private Const BNA_CONFIGFILE As String = "config.xml"

    Private Xml As XMLReadWrite
    Private filepath As String
    Private printername As String
    Private templatename As String
    Private labelwidth As Integer
    Private labelheight As Integer
    Private Printer As GodexPrinter

    Private Enum ORDERTEXTINDEX
        ORDER_NO
        SHIPTO_NAME
        SHIPTO_ADDRESS0
        SHIPTO_ADDRESS1
        SHIPTO_ADDRESS2
        SHIPTO_ADDRESS3
        ZIPCODE
        PHONE
        SHIPTO_TYPE
    End Enum

#Region "DECLARE PRINTER VARIABLE CODE"
    Dim CodePage_Code() As Integer = {0, _
              864, 708, 720, 28596, 10004, 1256, 775, 28594, 1257, 852, _
              28592, 10029, 1250, 51936, 54936, 936, 20936, 52936, 50227, 10008, _
              950, 20000, 20002, 10002, 10082, 866, 28595, 20866, 21866, 10007, _
              1251, 28603, 29001, 863, 20106, 737, 28597, 10006, 1253, 869, _
              862, 38598, 28598, 10005, 1255, 20420, 20880, 21025, 20277, 1142, _
              20278, 1143, 20297, 1147, 20273, 1141, 875, 20423, 20424, 20871, _
              1149, 500, 1148, 20280, 1144, 20290, 20833, 870, 20284, 1145, _
              20838, 1026, 20905, 20285, 1146, 37, 1140, 1047, 20924, 20003, _
              861, 10079, 57006, 57003, 57002, 57010, 57008, 57009, 57007, 57011, _
              57004, 57005, 20269, 51932, 20932, 50220, 50222, 50221, 10001, 932, _
              949, 51949, 50225, 1361, 10003, 20949, 28593, 28605, 865, 20108, _
              855, 858, 437, 860, 10010, 20107, 20261, 20001, 20004, 10021, _
              874, 857, 28599, 10081, 1254, 10017, 1200, 1201, 12001, 12000, _
              65000, 65001, 20127, 1258, 20005, 850, 20105, 28591, 10000, 1252}

    Dim CodePage_Name() As String = {"System Default", _
            "IBM864", "ASMO-708", "DOS-720", "iso-8859-6", "x-mac-arabic", "windows-1256", "ibm775", "iso-8859-4", "windows-1257", "ibm852", _
            "iso-8859-2", "x-mac-ce", "windows-1250", "EUC-CN", "GB18030", "gb2312", "x-cp20936", "hz-gb-2312", "x-cp50227", "x-mac-chinesesimp", _
            "big5", "x-Chinese-CNS", "x-Chinese-Eten", "x-mac-chinesetrad", "x-mac-croatian", "cp866", "iso-8859-5", "koi8-r", "koi8-u", "x-mac-cyrillic", _
            "windows-1251", "iso-8859-13", "x-Europa", "IBM863", "x-IA5-German", "ibm737", "iso-8859-7", "x-mac-greek", "windows-1253", "ibm869", _
            "DOS-862", "iso-8859-8-i", "iso-8859-8", "x-mac-hebrew", "windows-1255", "IBM420", "IBM880", "cp1025", "IBM277", "IBM01142", _
            "IBM278", "IBM01143", "IBM297", "IBM01147", "IBM273", "IBM01141", "cp875", "IBM423", "IBM424", "IBM871", _
            "IBM01149", "IBM500", "IBM01148", "IBM280", "IBM01144", "IBM290", "x-EBCDIC-KoreanExtended", "IBM870", "IBM284", "IBM01145", _
            "IBM-Thai", "IBM1026", "IBM905", "IBM285", "IBM01146", "IBM037", "IBM01140", "IBM01047", "IBM00924", "x-cp20003", _
            "ibm861", "x-mac-icelandic", "x-iscii-as", "x-iscii-be", "x-iscii-de", "x-iscii-gu", "x-iscii-ka", "x-iscii-ma", "x-iscii-or", "x-iscii-pa", _
            "x-iscii-ta", "x-iscii-te", "x-cp20269", "euc-jp", "EUC-JP", "iso-2022-jp", "iso-2022-jp", "csISO2022JP", "x-mac-japanese", "shift_jis", _
            "ks_c_5601-1987", "euc-kr", "iso-2022-kr", "Johab", "x-mac-korean", "x-cp20949", "iso-8859-3", "iso-8859-15", "IBM865", "x-IA5-Norwegian", _
            "IBM855", "IBM00858", "IBM437", "IBM860", "x-mac-romanian", "x-IA5-Swedish", "x-cp20261", "x-cp20001", "x-cp20004", "x-mac-thai", _
            "windows-874", "ibm857", "iso-8859-9", "x-mac-turkish", "windows-1254", "x-mac-ukrainian", "utf-16", "unicodeFFFE", "utf-32BE", "utf-32", _
            "utf-7", "utf-8", "us-ascii", "windows-1258", "x-cp20005", "ibm850", "x-IA5", "iso-8859-1", "macintosh", "Windows-1252"}

    Dim CodePage_DispName() As String = {"System Default", _
            "Arabic (864)", "Arabic (ASMO 708)", "Arabic (DOS)", "Arabic (ISO)", "Arabic (Mac)", _
            "Arabic (Windows)", "Baltic (DOS)", "Baltic (ISO)", "Baltic (Windows)", "Central European (DOS)", _
            "Central European (ISO)", "Central European (Mac)", "Central European (Windows)", "Chinese Simplified (EUC)", "Chinese Simplified (GB18030)", _
            "Chinese Simplified (GB2312)", "Chinese Simplified (GB2312-80)", "Chinese Simplified (HZ)", "Chinese Simplified (ISO-2022)", "Chinese Simplified (Mac)", _
            "Chinese Traditional (Big5)", "Chinese Traditional (CNS)", "Chinese Traditional (Eten)", "Chinese Traditional (Mac)", "Croatian (Mac)", _
            "Cyrillic (DOS)", "Cyrillic (ISO)", "Cyrillic (KOI8-R)", "Cyrillic (KOI8-U)", "Cyrillic (Mac)", _
            "Cyrillic (Windows)", "Estonian (ISO)", "Europa", "French Canadian (DOS)", "German (IA5)", _
            "Greek (DOS)", "Greek (ISO)", "Greek (Mac)", "Greek (Windows)", "Greek, Modern (DOS)", _
            "Hebrew (DOS)", "Hebrew (ISO-Logical)", "Hebrew (ISO-Visual)", "Hebrew (Mac)", "Hebrew (Windows)", _
            "IBM EBCDIC (Arabic)", "IBM EBCDIC (Cyrillic Russian)", "IBM EBCDIC (Cyrillic Serbian-Bulgarian)", "IBM EBCDIC (Denmark-Norway)", "IBM EBCDIC (Denmark-Norway-Euro)", _
            "IBM EBCDIC (Finland-Sweden)", "IBM EBCDIC (Finland-Sweden-Euro)", "IBM EBCDIC (France)", "IBM EBCDIC (France-Euro)", "IBM EBCDIC (Germany)", _
            "IBM EBCDIC (Germany-Euro)", "IBM EBCDIC (Greek Modern)", "IBM EBCDIC (Greek)", "IBM EBCDIC (Hebrew)", "IBM EBCDIC (Icelandic)", _
            "IBM EBCDIC (Icelandic-Euro)", "IBM EBCDIC (International)", "IBM EBCDIC (International-Euro)", "IBM EBCDIC (Italy)", "IBM EBCDIC (Italy-Euro)", _
            "IBM EBCDIC (Japanese katakana)", "IBM EBCDIC (Korean Extended)", "IBM EBCDIC (Multilingual Latin-2)", "IBM EBCDIC (Spain)", "IBM EBCDIC (Spain-Euro)", _
            "IBM EBCDIC (Thai)", "IBM EBCDIC (Turkish Latin-5)", "IBM EBCDIC (Turkish)", "IBM EBCDIC (UK)", "IBM EBCDIC (UK-Euro)", _
            "IBM EBCDIC (US-Canada)", "IBM EBCDIC (US-Canada-Euro)", "IBM Latin-1", "IBM Latin-1", "IBM5550 Taiwan", _
            "Icelandic (DOS)", "Icelandic (Mac)", "ISCII Assamese", "ISCII Bengali", "ISCII Devanagari", _
            "ISCII Gujarati", "ISCII Kannada", "ISCII Malayalam", "ISCII Oriya", "ISCII Punjabi", _
            "ISCII Tamil", "ISCII Telugu", "ISO-6937", "Japanese (EUC)", "Japanese (JIS 0208-1990 and 0212-1990)", _
            "Japanese (JIS)", "Japanese (JIS-Allow 1 byte Kana - SO/SI)", "Japanese (JIS-Allow 1 byte Kana)", "Japanese (Mac)", "Japanese (Shift-JIS)", _
            "Korean", "Korean (EUC)", "Korean (ISO)", "Korean (Johab)", "Korean (Mac)", "Korean Wansung", _
            "Latin 3 (ISO)", "Latin 9 (ISO)", "Nordic (DOS)", "Norwegian (IA5)", _
            "OEM Cyrillic", "OEM Multilingual Latin I", "OEM United States", "Portuguese (DOS)", "Romanian (Mac)", _
            "Swedish (IA5)", "T.61", "TCA Taiwan", "TeleText Taiwan", "Thai (Mac)", _
            "Thai (Windows)", "Turkish (DOS)", "Turkish (ISO)", "Turkish (Mac)", "Turkish (Windows)", _
            "Ukrainian (Mac)", "Unicode", "Unicode (Big endian)", "Unicode (UTF-32 Big endian)", "Unicode (UTF-32)", _
            "Unicode (UTF-7)", "Unicode (UTF-8)", "US-ASCII", "Vietnamese (Windows)", "Wang Taiwan", _
            "Western European (DOS)", "Western European (IA5)", "Western European (ISO)", "Western European (Mac)", "Western European (Windows)"}
#End Region

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            Printer = New GodexPrinter
            Me.LoadSetting()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub LoadSetting()
        Try
            Dim fileconfig As String = Application.StartupPath & "\" & BNA_CONFIGFILE

            If Not System.IO.File.Exists(fileconfig) Then
                MessageBox.Show("There are no config file. Unable to create xml configuration", "File not found", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
                Exit Sub
            End If

            Xml = New XMLReadWrite

            filepath = Xml.Read_XML_Value(fileconfig, "/BNAConfiguration", "DataFiles")
            printername = Xml.Read_XML_Value(fileconfig, "/BNAConfiguration", "PrinterName")
            labelwidth = Xml.Read_XML_Value(fileconfig, "/BNAConfiguration", "LabelWidth")
            labelheight = Xml.Read_XML_Value(fileconfig, "/BNAConfiguration", "LabelHeight")
            templatename = Xml.Read_XML_Value(fileconfig, "/BNAConfiguration", "TemplateName")

            Xml = Nothing
        Catch ex As Exception

        End Try

    End Sub

    Private Sub ReadCSVFile(pCSVPath As String)
        Dim objTempText As List(Of CartonProperty)
        Dim objReader As System.IO.StreamReader = Nothing
        Dim objText As System.Text.StringBuilder
        Dim strText As String
        Dim i As Int16
        Try
            objTempText = New List(Of CartonProperty)
            objReader = New System.IO.StreamReader(pCSVPath, System.Text.Encoding.Default)
            objText = New System.Text.StringBuilder
            i = 1
            Do While Not objReader.EndOfStream
                strText = objReader.ReadLine
                If strText.Length > 50 Then
                    If i > 1 Then
                        Me.PrintToPrinter(LoadPrintDataCSV(strText))
                    End If
                End If
                i += 1
            Loop
        Catch ex As Exception

        End Try
    End Sub

    Private Function LoadPrintDataCSV(sTextLine As String) As CartonProperty
        Dim _objLoadText As CartonProperty = Nothing
        Dim delimiter1 As String = "อำเภอ"
        Dim delimiter2 As String = "tel."
        Try

            Dim strArray As Array = Split(sTextLine, ",")

            _objLoadText = New CartonProperty
            With _objLoadText
                .OrderNumber = strArray(ORDERTEXTINDEX.ORDER_NO)
                .ShiptoName = strArray(ORDERTEXTINDEX.SHIPTO_NAME)
                .ShiptoAddress0 = strArray(ORDERTEXTINDEX.SHIPTO_ADDRESS0)
                .ShiptoAddress1 = strArray(ORDERTEXTINDEX.SHIPTO_ADDRESS1)
                .ShiptoAddress2 = strArray(ORDERTEXTINDEX.SHIPTO_ADDRESS2)
                .ShiptoAddress3 = strArray(ORDERTEXTINDEX.SHIPTO_ADDRESS3)
                .ZipCode = strArray(ORDERTEXTINDEX.ZIPCODE)
                .Phone = strArray(ORDERTEXTINDEX.PHONE)
                .ShiptoType = strArray(ORDERTEXTINDEX.SHIPTO_TYPE)
            End With

            Return _objLoadText
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub PrintToPrinter(pDataPrint As CartonProperty)
        Try
            ConnectPrinter(printername) 'เชื่อมต่อ USB
            LabelSetup()
            Printer.Command.JobStart()

            '################ SAMPLE ###############################
            'Printer.Command.PrintText(10, 190, fontHeight, "Arial", "GoDEX EZio DLL Test", 0, FontWeight.FW_900_HEAVY, RotateMode.Angle_180)
            'Printer.Command.PrintText(10, 240, fontHeight, "Arial", "GoDEX EZio DLL Test", 0, FontWeight.FW_700_BOLD, RotateMode.Angle_0, Italic_State.S_OFF, Underline_State.S_OFF, Strikeout_State.S_OFF, Inverse_State.S_ON)
            'Printer.Command.PrintText_Unicode(7 + _MaginX, 90 + _MaginY, FontHeight - 5, "Arial", "FROM :", 0, FontWeight.FW_800_EXTRABOLD, RotateMode.Angle_0)
            'Printer.Command.PrintText_Unicode(7, 120, 5, "Arial", "(ผู้จัดส่ง):", 0, FontWeight.FW_800_EXTRABOLD, RotateMode.Angle_0)
            'Printer.Command.PrintBarCode(BarCodeType.EAN128, 160, 580, 3, 15, 80, 0, 1, "data") '================== BARCODE ==============
            'Printer.Command.PrintImage(200 + _MaginX, 100, _IMAGE_LOGO_LABELPRINT, 0) ' ============== IMAGE ==================
            'Printer.Command.DrawHorLine(27 + _MaginX, 250, 750, 2) ' ============== LINE ==================
            'Printer.Command.DrawHorLine(27, 290 + _MaginY, 750, 2) ' ============== LINE ==================
            '#######################################################
            Me.ReadTemplate(Printer)


            Printer.Command.JobEnd()
            DisconnectPrinter()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ReadTemplate(pPrinter As GodexPrinter)
        Dim objReader As System.IO.StreamReader = Nothing
        Dim objText As System.Text.StringBuilder
        Dim strText As String
        Dim TextObj As TextProperty
        Dim BarcodeObj As BarcodeProperty
        Try
            objReader = New System.IO.StreamReader(templatename, System.Text.Encoding.Default)
            objText = New System.Text.StringBuilder
            Do While Not objReader.EndOfStream
                strText = objReader.ReadLine

                Dim textSplited = strText.Split(",")
                If IsArray(textSplited) AndAlso textSplited.Length > 0 Then
                    Select Case textSplited(0)
                        Case "TEXT"
                            TextObj = New TextProperty
                            TextObj.Xpos = textSplited(1)
                            TextObj.Ypos = textSplited(2)
                            TextObj.fontHeight = textSplited(3)
                            TextObj.fontName = textSplited(4)
                            TextObj.Data = textSplited(5)
                            TextObj.TextWidth = textSplited(6)
                            TextObj.Dark = textSplited(7)
                            TextObj.Rotate = textSplited(8)

                            pPrinter.Command.PrintText_Unicode(TextObj.Xpos, _
                                                               TextObj.Ypos, _
                                                               TextObj.fontHeight, _
                                                               TextObj.fontName, _
                                                               TextObj.Data, _
                                                               TextObj.TextWidth, _
                                                               TextObj.Dark, _
                                                               TextObj.Rotate)
                        Case "BARCODE"

                            BarcodeObj = New BarcodeProperty
                            BarcodeObj.BarcodeType = textSplited(1)
                            BarcodeObj.Xpos = textSplited(2)
                            BarcodeObj.Ypos = textSplited(3)
                            BarcodeObj.Narrow = textSplited(4)
                            BarcodeObj.Wide = textSplited(5)
                            BarcodeObj.Height = textSplited(6)
                            BarcodeObj.Rotation = textSplited(7)
                            BarcodeObj.Readable = textSplited(8)
                            BarcodeObj.Data = textSplited(9)

                            pPrinter.Command.PrintBarCode(BarcodeObj.BarcodeType, _
                                                            BarcodeObj.Xpos, _
                                                            BarcodeObj.Ypos, _
                                                            BarcodeObj.Narrow, _
                                                            BarcodeObj.Wide, _
                                                            BarcodeObj.Height, _
                                                            BarcodeObj.Rotation, _
                                                            BarcodeObj.Readable, _
                                                            BarcodeObj.Data)
                        Case Else

                    End Select
                End If
            Loop
        Catch ex As Exception

        End Try
    End Sub

#Region "Printer Configuration"
    '------------------------------------------------------------------------
    ' Connect Printer
    '------------------------------------------------------------------------
    Private Sub ConnectPrinter(PrinterDriverName As String)
        Try
            'If RBtn_USB.Checked Then
            'Printer.Open(PortType.USB)
            'ElseIf RBtn_COM.Checked Then
            '    If Cbo_COM.SelectedItem IsNot Nothing Then
            '        Printer.Open(Cbo_COM.SelectedItem.ToString())
            '        Printer.SetBaudrate(CInt(Txt_Baud.Text))
            '    End If
            'ElseIf RBtn_LPT.Checked Then
            '    If Cbo_LPT.SelectedIndex = 0 Then
            '        Printer.Open(PortType.LPT1)
            '    Else
            '        Printer.Open(PortType.LPT2)
            '    End If
            'ElseIf RBtn_Driver.Checked Then

            Printer.Open(PrinterDriverName)


            'ElseIf RBtn_NET.Checked Then
            '    Printer.Open(Txt_IP.Text, Integer.Parse(Txt_NetPort.Text))
            'End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    '------------------------------------------------------------------------
    ' Disconnect Printer
    '------------------------------------------------------------------------
    Private Sub DisconnectPrinter()
        Printer.Close()
    End Sub

    '------------------------------------------------------------------------
    ' Label Setup
    '------------------------------------------------------------------------
    Private Sub LabelSetup()

        Dim paperType As PaperMode = PaperMode.GapLabel
        Dim LabelHeight, LabelWidth, LabelGap As Integer
        Dim Printer_Dark, Printer_Speed, NumberOfPage, NumberOfCopy As Integer

        paperType = PaperMode.GapLabel

        LabelGap = 2
        LabelHeight = LabelHeight
        LabelWidth = LabelWidth

        Printer_Dark = 10
        Printer_Speed = 4
        NumberOfCopy = 1
        NumberOfPage = 1

        Printer.Config.LabelMode(paperType, LabelHeight, LabelGap)
        Printer.Config.LabelWidth(LabelWidth)

        Printer.Config.Dark(Printer_Dark)
        Printer.Config.Speed(Printer_Speed)
        Printer.Config.PageNo(NumberOfPage)
        Printer.Config.CopyNo(NumberOfCopy)

    End Sub
#End Region

    Private Class TextProperty
        Property Xpos As Integer
        Property Ypos As Integer
        Property fontHeight As Integer
        Property fontName As String
        Property Data As String
        Property TextWidth As Integer
        Property Dark As Integer
        Property Rotate As Integer
    End Class

    Private Class BarcodeProperty
        Property BarcodeType As Integer
        Property Xpos As Integer
        Property Ypos As Integer
        Property Narrow As Integer
        Property Wide As Integer
        Property Height As Integer
        Property Rotation As Integer
        Property Readable As Integer
        Property Data As String
    End Class
End Class

Public Class CartonProperty
    Property OrderNumber As String
    Property ShiptoName As String
    Property ShiptoAddress0 As String
    Property ShiptoAddress1 As String
    Property ShiptoAddress2 As String
    Property ShiptoAddress3 As String
    Property ZipCode As String
    Property Phone As String
    Property ShiptoType As String
End Class
