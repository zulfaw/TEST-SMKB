Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing

Public Class Senarai_Invois
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                'fBindDdlJenis()
                'fBindDdlTahun()

                fLoadSenarai()
                'divList.Visible = True
                'divDt.Visible = False

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub fLoadSenarai()
        Try
            Dim intRec As Integer
            Dim strSql As String

            'fClearGvSenarai()


            Dim dt As New DataTable
            dt.Columns.Add("IdInv", GetType(String))
            dt.Columns.Add("NoInv", GetType(String))
            dt.Columns.Add("NPembekal", GetType(String))
            dt.Columns.Add("NoPT", GetType(String))
            dt.Columns.Add("NoDO", GetType(String))
            dt.Columns.Add("BekalKpd", GetType(String))
            dt.Columns.Add("Jumlah", GetType(String))
            dt.Columns.Add("Padan", GetType(Boolean))


            ' strSql = "select a.AP01_NoId, a.AP01_NoInv, (select b.ROC01_NamaSya  from ROC01_Syarikat b where b.ROC01_IDSya = a.ROC01_IdSya ) as NPembekal,  a.PO19_NoPt , AP01_NoDo, (select c.PO19_BekalKepada from PO19_Pt c where c.PO19_NoPt = a.PO19_NoPt) as BekalKpd, AP01_Jumlah, AP01_Padan " &
            '       "from AP01_Invois a where a.AP01_Status = 1 "

            strSql = "select  * 
                        from SMKB_Invois_Hdr a
                        inner join  SMKB_Syarikat_Hdr b on b.No_Syarikat = a.No_Syarikat
                        inner join PO19_Pt c on c.PO19_NoPt = a.No_PTempatan
                        where a.Status = 1"
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            Dim strIdInv, strNoInv, strNPembekal, strNoPT, strNoDO, strBekalKpd, strJumlah As String
            Dim decAmaun As Decimal
            Dim blnPadan As Boolean

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        strIdInv = ds.Tables(0).Rows(i)("ID_Rujukan")
                        strNoInv = ds.Tables(0).Rows(i)("No_Invois")
                        strNPembekal = ds.Tables(0).Rows(i)("No_Syarikat")
                        strNoPT = ds.Tables(0).Rows(i)("PO19_NoPt")
                        strNoDO = ds.Tables(0).Rows(i)("No_Do")
                        strBekalKpd = ds.Tables(0).Rows(i)("No_Syarikat")
                        decAmaun = CDec(ds.Tables(0).Rows(i)("Jumlah_Sebenar"))
                        strJumlah = FormatNumber(decAmaun, 2)
                        blnPadan = CBool(ds.Tables(0).Rows(i)("Padan"))

                        dt.Rows.Add(strIdInv, strNoInv, strNPembekal, strNoPT, strNoDO, strBekalKpd, strJumlah, blnPadan)
                    Next
                    gvSenarai.DataSource = dt
                    gvSenarai.DataBind()
                    ViewState("dtSenarai") = dt
                    intRec = ds.Tables(0).Rows.Count
                End If
            End If

            'lblJumRekod.InnerText = intRec

        Catch ex As Exception

        End Try
    End Sub

    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        ' Verifies that the control is rendered
    End Sub

End Class