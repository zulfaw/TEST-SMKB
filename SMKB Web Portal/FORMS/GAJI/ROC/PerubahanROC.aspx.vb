Imports System.Web.Services
Imports Newtonsoft.Json

Imports System.Net
Imports System.Net.Mail
Imports System.Web.Configuration

Public Class PerubahanROC
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Public Class ItemList
        Public Property text As String
        Public Property value As String

        Public Sub New()

        End Sub

        Public Sub New(text_, val_)
            text = text_
            value = val_
        End Sub
    End Class
End Class