
Imports System.Security
Imports System.Security.Cryptography
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Text.RegularExpressions
Imports System.Text

Public Class clsCrypto

    Dim passPhrase As String = "SMKB@2017"
    Dim saltValue As String = "z1X2c3V4b5N6m"

    Dim initVector As String = "e1F2g9H8@1B3c8D6"        ' must be 16 bytes
    Dim initVectorBytes As Byte()
    'Dim saltValue As String = "s@1TV@lue"                ' can be any string
    'Dim passPhrase As String = "P@sSpr@se"                   ' can be any string
    Dim hashAlgorithm As String = "MD5"                 ' can be "MD5"
    Dim passwordIterations As Integer = 8                   ' can be any number
    Dim keySize As Integer = 256                           ' can be 192 or 128
    Dim secretKey As String = "0000001626BIUTMHBY200218"

    ''' <summary>
    ''' Encrypt string
    ''' </summary>
    ''' <param name="plainText">string value</param>
    ''' <returns></returns>
    Public Function fEncrypt(ByVal plainText As String) As String

        Dim hashAlgorithm As String = "SHA1"

        Dim passwordIterations As Integer = 2
        Dim initVector As String = "@1B2c3D4e5F6g7H8"
        Dim keySize As Integer = 256

        Dim initVectorBytes As Byte() = Encoding.ASCII.GetBytes(initVector)
        Dim saltValueBytes As Byte() = Encoding.ASCII.GetBytes(saltValue)

        Dim plainTextBytes As Byte() = Encoding.UTF8.GetBytes(plainText)

        Dim password As New PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations)

        Dim keyBytes As Byte() = password.GetBytes(keySize \ 8)
        Dim symmetricKey As New RijndaelManaged()

        symmetricKey.Mode = CipherMode.CBC

        Dim encryptor As ICryptoTransform = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes)

        Dim memoryStream As New MemoryStream()
        Dim cryptoStream As New CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write)

        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length)
        cryptoStream.FlushFinalBlock()
        Dim cipherTextBytes As Byte() = memoryStream.ToArray()
        memoryStream.Close()
        cryptoStream.Close()
        Dim cipherText As String = Convert.ToBase64String(cipherTextBytes)
        Return cipherText
    End Function
    ''' <summary>
    ''' Decrypt string
    ''' </summary>
    ''' <param name="cipherText">string value</param>
    ''' <returns></returns>
    Public Function fDecrypt(ByVal cipherText As String) As String

        Dim hashAlgorithm As String = "SHA1"

        Dim passwordIterations As Integer = 2
        Dim initVector As String = "@1B2c3D4e5F6g7H8"
        Dim keySize As Integer = 256
        ' Convert strings defining encryption key characteristics into byte
        ' arrays. Let us assume that strings only contain ASCII codes.
        ' If strings include Unicode characters, use Unicode, UTF7, or UTF8
        ' encoding.
        Dim initVectorBytes As Byte() = Encoding.ASCII.GetBytes(initVector)
        Dim saltValueBytes As Byte() = Encoding.ASCII.GetBytes(saltValue)

        ' Convert our ciphertext into a byte array.
        Dim cipherTextBytes As Byte() = Convert.FromBase64String(cipherText)

        ' First, we must create a password, from which the key will be 
        ' derived. This password will be generated from the specified 
        ' passphrase and salt value. The password will be created using
        ' the specified hash algorithm. Password creation can be done in
        ' several iterations.
        Dim password As New PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations)

        ' Use the password to generate pseudo-random bytes for the encryption
        ' key. Specify the size of the key in bytes (instead of bits).
        Dim keyBytes As Byte() = password.GetBytes(keySize \ 8)

        ' Create uninitialized Rijndael encryption object.
        Dim symmetricKey As New RijndaelManaged()

        ' It is reasonable to set encryption mode to Cipher Block Chaining
        ' (CBC). Use default options for other symmetric key parameters.
        symmetricKey.Mode = CipherMode.CBC

        ' Generate decryptor from the existing key bytes and initialization 
        ' vector. Key size will be defined based on the number of the key 
        ' bytes.
        Dim decryptor As ICryptoTransform = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes)

        ' Define memory stream which will be used to hold encrypted data.
        Dim memoryStream As New MemoryStream(cipherTextBytes)

        ' Define cryptographic stream (always use Read mode for encryption).
        Dim cryptoStream As New CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read)

        ' Since at this point we don't know what the size of decrypted data
        ' will be, allocate the buffer long enough to hold ciphertext;
        ' plaintext is never longer than ciphertext.
        Dim plainTextBytes As Byte() = New Byte(cipherTextBytes.Length - 1) {}

        ' Start decrypting.
        Dim decryptedByteCount As Integer = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length)

        ' Close both streams.
        memoryStream.Close()
        cryptoStream.Close()

        ' Convert decrypted data into a string. 
        ' Let us assume that the original plaintext string was UTF8-encoded.
        Dim plainText As String = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount)

        ' Return decrypted string.   
        Return plainText
    End Function
    'Example:
    '    Dim strEncryptedText As String
    'strEncryptedText  =  Encrypt("yourEncryptionText")

    'Dim strDecrptedText As String
    'strDecrptedText = Decrypt(strEncryptedText)
    ''' <summary>
    ''' Decrypt File Content
    ''' Hanafi 30/01/2019
    ''' </summary>
    ''' <param name="cipherText"></param>
    ''' <returns></returns>
    Public Function fDecryptFile(ByVal cipherText() As Byte) As Byte()

        initVectorBytes = Encoding.ASCII.GetBytes(initVector)

        Dim saltValueBytes As Byte()
        saltValueBytes = Encoding.ASCII.GetBytes(saltValue)


        ' First, we must create a password, from which the key will be 
        ' derived. This password will be generated from the specified 
        ' passphrase and salt value. The password will be created using
        ' the specified hash algorithm. Password creation can be done in
        ' several iterations.
        Dim password As PasswordDeriveBytes
        password = New PasswordDeriveBytes(passPhrase,
                                           saltValueBytes,
                                           hashAlgorithm,
                                           passwordIterations)

        ' Use the password to generate pseudo-random bytes for the encryption
        ' key. Specify the size of the key in bytes (instead of bits).
        Dim keyBytes As Byte()
        keyBytes = password.GetBytes(keySize / 8)

        ' Create uninitialized Rijndael encryption object.
        Dim symmetricKey As RijndaelManaged
        symmetricKey = New RijndaelManaged

        ' It is reasonable to set encryption mode to Cipher Block Chaining
        ' (CBC). Use default options for other symmetric key parameters.
        symmetricKey.Mode = CipherMode.CBC

        ' Generate decryptor from the existing key bytes and initialization 
        ' vector. Key size will be defined based on the number of the key 
        ' bytes.
        Dim decryptor As ICryptoTransform
        decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes)

        ' Define memory stream which will be used to hold encrypted data.
        Dim memoryStream As MemoryStream
        memoryStream = New MemoryStream(cipherText)

        ' Define memory stream which will be used to hold encrypted data.
        Dim cryptoStream As CryptoStream
        cryptoStream = New CryptoStream(memoryStream,
                                        decryptor,
                                        CryptoStreamMode.Read)

        ' Since at this point we don't know what the size of decrypted data
        ' will be, allocate the buffer long enough to hold ciphertext;
        ' plaintext is never longer than ciphertext.
        Dim plainTextBytes As Byte()
        ReDim plainTextBytes(cipherText.Length)

        ' Start decrypting.
        Dim decryptedByteCount As Integer
        decryptedByteCount = cryptoStream.Read(plainTextBytes,
                                               0,
                                               plainTextBytes.Length)


        ' Close both streams.
        memoryStream.Close()
        cryptoStream.Close()

        ' Return decrypted Bytes.
        fDecryptFile = plainTextBytes
    End Function

    ''' <summary>
    ''' Encrypt File Content
    ''' Hanafi 30/01/2019
    ''' </summary>
    ''' <param name="plainTextBytes"></param>
    ''' <returns></returns>
    Public Function fEncryptFile(ByVal plainTextBytes() As Byte) As Byte()
        'ByVal passPhrase As String, _
        'ByVal saltValue As String, _
        'ByVal hashAlgorithm As String, _
        'ByVal passwordIterations As Integer, _
        'ByVal initVector As String, _
        'ByVal keySize As Integer
        ' Convert strings into byte arrays.
        ' Let us assume that strings only contain ASCII codes.
        ' If strings include Unicode characters, use Unicode, UTF7, or UTF8 
        ' encoding.


        initVectorBytes = Encoding.ASCII.GetBytes(initVector)

        Dim saltValueBytes As Byte()
        saltValueBytes = Encoding.ASCII.GetBytes(saltValue)

        ' First, we must create a password, from which the key will be derived.
        ' This password will be generated from the specified passphrase and 
        ' salt value. The password will be created using the specified hash 
        ' algorithm. Password creation can be done in several iterations.
        Dim password As PasswordDeriveBytes
        password = New PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations)

        ' Use the password to generate pseudo-random bytes for the encryption
        ' key. Specify the size of the key in bytes (instead of bits).
        Dim keyBytes As Byte()
        keyBytes = password.GetBytes(keySize / 8)

        ' Create uninitialized Rijndael encryption object.
        Dim symmetricKey As RijndaelManaged
        symmetricKey = New RijndaelManaged

        ' It is reasonable to set encryption mode to Cipher Block Chaining
        ' (CBC). Use default options for other symmetric key parameters.
        symmetricKey.Mode = CipherMode.CBC

        ' Generate encryptor from the existing key bytes and initialization 
        ' vector. Key size will be defined based on the number of the key 
        ' bytes.
        Dim encryptor As ICryptoTransform
        encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes)

        ' Define memory stream which will be used to hold encrypted data.
        Dim memoryStream As MemoryStream
        memoryStream = New MemoryStream

        ' Define cryptographic stream (always use Write mode for encryption).
        Dim cryptoStream As CryptoStream
        cryptoStream = New CryptoStream(memoryStream,
                                        encryptor,
                                        CryptoStreamMode.Write)
        ' Start encrypting.
        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length)

        ' Finish encrypting.
        cryptoStream.FlushFinalBlock()

        ' Convert our encrypted data from a memory stream into a byte array.
        Dim cipherTextBytes As Byte()
        cipherTextBytes = memoryStream.ToArray()

        ' Close both streams.
        memoryStream.Close()
        cryptoStream.Close()

        ' Return encrypted Bytes.
        fEncryptFile = cipherTextBytes
    End Function

    Public Function Encrypt(ByVal clearText As String) As String
        Dim keyArray As Byte() = ASCIIEncoding.ASCII.GetBytes(secretKey)
        Dim clearTextArray As Byte() = ASCIIEncoding.ASCII.GetBytes(clearText)
        Dim tdes As TripleDESCryptoServiceProvider = New TripleDESCryptoServiceProvider()
        Dim tmpKeyArray As Byte() = New Byte(23) {}

        For i As Integer = 0 To keyArray.Length - 1
            tmpKeyArray(i) = keyArray(i)
        Next

        tdes.Key = keyArray
        tdes.Mode = CipherMode.ECB
        tdes.Padding = PaddingMode.PKCS7
        Dim cTransform As ICryptoTransform = tdes.CreateEncryptor()
        Dim encryptedTextArray As Byte() = cTransform.TransformFinalBlock(clearTextArray, 0, clearTextArray.Length)
        tdes.Clear()
        Return Convert.ToBase64String(encryptedTextArray, 0, encryptedTextArray.Length)
    End Function

    Public Function Decrypt(ByVal encryptedText As String) As String
        Dim keyArray As Byte() = ASCIIEncoding.ASCII.GetBytes(secretKey)
        Dim encryptedTextArray As Byte() = Convert.FromBase64String(encryptedText)
        Dim tdes As TripleDESCryptoServiceProvider = New TripleDESCryptoServiceProvider()
        tdes.Key = keyArray
        tdes.Mode = CipherMode.ECB
        tdes.Padding = PaddingMode.PKCS7
        Dim cTransform As ICryptoTransform = tdes.CreateDecryptor()
        Dim clearTextArray As Byte() = cTransform.TransformFinalBlock(encryptedTextArray, 0, encryptedTextArray.Length)
        tdes.Clear()
        Return ASCIIEncoding.ASCII.GetString(clearTextArray)
    End Function

End Class
