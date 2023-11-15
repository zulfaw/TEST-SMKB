Public Class Class1
    Public Property NamaXX As String
    Private umur As String
    Public Sub New(Optional ByVal val As String = "")
        Set_Nama(val)
    End Sub

    Public Shared Sub Test()

    End Sub

    Protected Sub Set_Nama(val As String)
        NamaXX = val
    End Sub

End Class

Public Class Class3
    Property alamat As String
End Class


Public Class Class2
    Inherits Class1

    Public Sub New()
        MyBase.New()
    End Sub
End Class



