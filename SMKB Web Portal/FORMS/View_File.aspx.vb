Imports System.IO
Imports System.Security.Cryptography

Public Class View_File
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim clsCrypto As New clsCrypto
        Dim strKodModul As String = Request.QueryString("KodModul")
        Dim intIdBil As Integer = CInt(Request.QueryString("intIdBil"))
        Dim intId As Integer = CInt(Request.QueryString("Id"))
        Dim strFileName = intIdBil & "-" & intId

        If Not IsPostBack Then
            Dim ds As New DataSet
            Dim UserID As String = Session("ssusrID")
            Dim FileContent As Byte()
            Try
                Dim folderPath As String = "~/Upload/Document/" & strKodModul & "/" & intIdBil
                Dim SrcPath = folderPath & "\" & strFileName

                If File.Exists(Server.MapPath(SrcPath)) Then
                    FileContent = fReadFile(Server.MapPath(SrcPath))

                    If FileContent.Length > 0 Then
                        Dim strTempPath = Server.MapPath(folderPath & "/temp/" & Trim(UserID.TrimEnd))
                        Dim strTmpFileName = strFileName & ".pdf"

                        If Not Directory.Exists(strTempPath) Then
                            Directory.CreateDirectory(strTempPath)
                        End If

                        If File.Exists(strTempPath & "\" & strTmpFileName) Then
                            File.Delete(strTempPath & "\" & strTmpFileName)
                        End If

                        If fDecFile(strTempPath & "\" & strTmpFileName, FileContent) Then
                            Response.Redirect(folderPath & "/temp/02554/" & strTmpFileName, False)
                        End If

                    End If
                End If

            Catch ex As Exception

            Finally
                GC.Collect()
            End Try
        End If

    End Sub

End Class