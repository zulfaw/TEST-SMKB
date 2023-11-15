Imports System
Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.Diagnostics.Eventing.Reader
Imports System.Web.UI.HtmlControls

Public Class reportPotonganBulanan
    Inherits System.Web.UI.Page
    Dim listBulan As String() = {"Januari", "Februari", "Mac", "April", "Mei", "Jun", "Julai", "Ogos", "September", "Oktober", "November", "Disember"}

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ptj.Text = GetDescriptionPTJByCode(Session("ptj"))
            tahun.Text = DateTime.Now.Year.ToString()
            bulan.Text = listBulan(DateTime.Now.Month - 1)
            LoadLogoUtem()
        End If
    End Sub

    Protected Function GetDescriptionPTJByCode(ByVal code As String) As String
        Dim connectionString As String = "server=devmis12.utem.edu.my;database=DbKewanganV4;uid=Smkb;pwd=Smkb@Dev2012;"
        Dim description As String = ""
        Dim trimmedCode As String = ""
        If code.Length >= 4 Then
            trimmedCode = code.Substring(0, code.Length - 4)
            Using connection As New SqlConnection(connectionString)
                connection.Open()

                Dim query As String = "SELECT Pejabat from [devmis\SQL_INS01].dbStaf.dbo.MS_PEJABAT WHERE KodPejabat = @ptj"

                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@ptj", trimmedCode)
                    description = DirectCast(command.ExecuteScalar(), String)
                End Using
            End Using
        Else
            description = "KESELURUHAN"
        End If
        Return description
    End Function

    Private Sub LoadLogoUtem()
        Dim strSql As String = "Select Logo from SMKB_Korporat"

        Dim connection As New SqlConnection(strCon)
        connection.Open()

        Dim command As New SqlCommand(strSql, connection)
        Dim reader As SqlDataReader = command.ExecuteReader()

        If reader.Read() Then
            Dim imageData As Byte() = DirectCast(reader("Logo"), Byte())
            Dim base64String As String = Convert.ToBase64String(imageData)
            imgMyImage.ImageUrl = String.Format("data:image/jpg;base64,{0}", base64String)
        End If

        reader.Close()
        connection.Close()
    End Sub
    Protected Sub Button1_Click(sender As Object, e As EventArgs)

    End Sub
End Class