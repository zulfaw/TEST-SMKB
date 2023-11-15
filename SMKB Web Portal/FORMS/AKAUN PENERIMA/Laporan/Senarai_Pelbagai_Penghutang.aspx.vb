Imports System.Data.SqlClient

Public Class Senarai_Pelbagai_Penghutang
    Inherits System.Web.UI.Page
    Dim dbconn As New DBKewConn
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            fCariTahun()
        End If
    End Sub

    Private Sub fCariTahun()
        Try
            Dim strsql As String


            strsql = $"SELECT DISTINCT YEAR(Tkh_Mohon) as tahun
                        FROM SMKB_Bil_Hdr A
                        LEFT JOIN SMKB_Terima_Hdr B ON A.No_Bil=B.No_Rujukan
                        INNER JOIN SMKB_Penghutang_Master C ON A.Kod_Penghutang=C.Kod_Penghutang;"

            Dim ds As New DataSet
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strsql)

            ddlTahun.DataSource = ds
            ddlTahun.DataTextField = "tahun"
            ddlTahun.DataValueField = "tahun"
            ddlTahun.DataBind()

            ddlTahun.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
            ddlTahun.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub
End Class