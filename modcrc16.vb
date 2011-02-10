Module modcrc16
    Const X25_INIT_CRC As Integer = &HFFFF
    Const X25_VALIDATE_CRC As Integer = &HF0B8

    Private Function crc_accumulate(ByVal b As Byte, ByRef crc As UShort) As UShort
        Dim ch As Byte
        ch = CByte(b Xor CByte(crc And &HFF))
        ch = CByte(ch Xor (ch << 4))
        crc_accumulate = ((CInt(crc) >> 8) Xor (CInt(ch) << 8) Xor (CInt(ch) << 3) Xor (CInt(ch) >> 4))
    End Function

    Public Function crc_calculate(ByVal InputString As String) As String
        Dim crcTmp As UShort
        Dim i As Integer

        crcTmp = X25_INIT_CRC

        For i = 2 To Len(InputString)
            crcTmp = crc_accumulate(Asc(Mid(InputString, i, 1)), crcTmp)
        Next

        crc_calculate = Hex(crcTmp).PadLeft(4, "0")
        'crc_calculate = AddCharacter("&h" & Mid(crc_calculate, 3, 2)) & AddCharacter("&h" & Mid(crc_calculate, 1, 2))
        crc_calculate = Chr("&h" & Mid(crc_calculate, 3, 2)) & Chr("&h" & Mid(crc_calculate, 1, 2))
    End Function
End Module