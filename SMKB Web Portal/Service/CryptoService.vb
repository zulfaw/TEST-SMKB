Imports System
Imports System.IO
Imports System.Security.Cryptography
Imports System.Text

Public Class CryptoService

    Shared Function EnecryptStringAES_notuser(cipherText As String, user As String)
        Dim keybytes() As Byte
        Dim iv() As Byte

        keybytes = Encoding.UTF8.GetBytes("m0801#40208we070")
        iv = Encoding.UTF8.GetBytes("&^801040208020ta")
        If (user.Length = 5) Then
            keybytes = Encoding.UTF8.GetBytes("o0801#402ty02070")
            iv = Encoding.UTF8.GetBytes("k^801040" + user + "0ua")

        ElseIf (user.Length = 10) Then
            keybytes = Encoding.UTF8.GetBytes("20801#4zr0802070")
            iv = Encoding.UTF8.GetBytes("q^x" + user + "0#a")
        Else

        End If

        Dim decriptedFromJavascript As Byte() = EncryptStringToBytes(cipherText, keybytes, iv)
        Dim b64ciphertext As String = Convert.ToBase64String(decriptedFromJavascript)
        Return b64ciphertext

    End Function

    Public Function DecryptStringFromBytes(cipherText As Byte(), key As Byte(), iv As Byte()) As String
        ' Check arguments.  
        If cipherText Is Nothing OrElse cipherText.Length <= 0 Then
            Throw New ArgumentNullException("cipherText")
        End If
        If key Is Nothing OrElse key.Length <= 0 Then
            Throw New ArgumentNullException("key")
        End If
        If iv Is Nothing OrElse iv.Length <= 0 Then
            Throw New ArgumentNullException("key")
        End If

        ' Declare the string used to hold  
        ' the decrypted text.  
        Dim plaintext As String = Nothing

        ' Create an RijndaelManaged object  
        ' with the specified key and IV.  
        Using rijAlg As New RijndaelManaged()
            ' Settings  
            rijAlg.Mode = CipherMode.CBC
            rijAlg.Padding = PaddingMode.PKCS7
            rijAlg.FeedbackSize = 128

            rijAlg.Key = key
            rijAlg.IV = iv

            ' Create a decrytor to perform the stream transform.  
            Dim decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV)

            Try
                ' Create the streams used for decryption.  
                Using msDecrypt = New MemoryStream(cipherText)
                    Using csDecrypt = New CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read)
                        Using srDecrypt = New StreamReader(csDecrypt)
                            ' Read the decrypted bytes from the decrypting stream  
                            ' and place them in a string.  
                            plaintext = srDecrypt.ReadToEnd()
                        End Using
                    End Using
                End Using
            Catch
                plaintext = "keyError"
            End Try
        End Using

        Return plaintext
    End Function

    Public Shared Function GenerateRandomString(length As Integer) As String
        Dim allowedChars As String = "+_)(*&^%$#@!abcde=fghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789"
        Dim chars As Char() = New Char(length - 1) {}
        Dim rd As New Random()

        For i As Integer = 0 To length - 1
            chars(i) = allowedChars(rd.Next(0, allowedChars.Length))
        Next

        Return New String(chars)
    End Function

    Private Shared Function EncryptStringToBytes(plainText As String, key As Byte(), iv As Byte()) As Byte()
        ' Check arguments.  
        If plainText Is Nothing OrElse plainText.Length <= 0 Then
            Throw New ArgumentNullException("plainText")
        End If
        If key Is Nothing OrElse key.Length <= 0 Then
            Throw New ArgumentNullException("key")
        End If
        If iv Is Nothing OrElse iv.Length <= 0 Then
            Throw New ArgumentNullException("key")
        End If
        Dim encrypted As Byte()

        ' Create a RijndaelManaged object  
        ' with the specified key and IV.  
        Using rijAlg As New RijndaelManaged()
            rijAlg.Mode = CipherMode.CBC
            rijAlg.Padding = PaddingMode.PKCS7
            rijAlg.FeedbackSize = 128

            rijAlg.Key = key
            rijAlg.IV = iv

            ' Create an encryptor to perform the stream transform.  
            Dim encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV)

            ' Create the streams used for encryption.  
            Using msEncrypt = New MemoryStream()
                Using csEncrypt = New CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write)
                    Using swEncrypt = New StreamWriter(csEncrypt)
                        ' Write all data to the stream.  
                        swEncrypt.Write(plainText)
                    End Using
                    encrypted = msEncrypt.ToArray()
                End Using
            End Using
        End Using


        ' Return the encrypted bytes from the memory stream.  
        Return encrypted
    End Function



End Class
