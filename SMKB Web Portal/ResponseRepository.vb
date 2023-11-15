Imports System.Web.Services.Description

Public Class ResponseRepository
    Private response_ As Response
    '00 success
    '01 failed

    Public Sub New()
        response_ = New Response
        response_.Code = ""
        response_.Payload = Nothing
        Failed("")
    End Sub

    Public Sub Failed(message As String, Optional kod As String = "01")
        response_.Code = kod
        response_.Status = False
        response_.Message = message
    End Sub

    Public Sub Success(message As String, Optional kod As String = "00", Optional obj As Object = Nothing)
        response_.Code = kod
        response_.Status = True
        response_.Message = message
        response_.Payload = obj
    End Sub

    Public Sub SuccessPayload(obj As Object)
        response_.Code = "00"
        response_.Status = True
        response_.Message = "Request has been successfully."
        response_.Payload = obj
    End Sub

    Public Function GetResult() As Response
        Return response_
    End Function
End Class


