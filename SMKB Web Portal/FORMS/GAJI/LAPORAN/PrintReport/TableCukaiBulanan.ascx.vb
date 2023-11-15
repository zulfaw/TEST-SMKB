Imports System
Imports System.Data.SqlClient
Imports System.Diagnostics.Eventing.Reader
Imports System.Web.UI.HtmlControls
Imports System.Web
Imports System.Web.UI.WebControls

Public Class TableCukaiBulanan
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'lblTajuk.InnerHtml = Tajuk
            Dim dataTable As New DataTable()
            dataTable.Columns.Add("bulan", GetType(String))
            dataTable.Columns.Add("tahun", GetType(String))
            dataTable.Columns.Add("ptj", GetType(String))
            dataTable.Rows.Add(Session("bulan"), Session("tahun"), Session("ptj"))

            rptone.DataSource = dataTable
            rptone.DataBind()
        End If
    End Sub

    Private Function LoadDataTable(bulan As String, tahun As String, ptj As String) As DataTable
        Dim query As String

        If Session("ptj") = "00" Then
            query = "SELECT A.No_Staf,C.MS01_Nama AS Nama,B.No_Cukai AS No_Cukai,C.MS01_KpB AS No_KP, B.Kategori_Cukai,A.Amaun,A.Jenis_Trans
                     FROM SMKB_Gaji_Lejar A
                     LEFT JOIN SMKB_Gaji_Staf B ON B.No_Staf = A.No_Staf
                     LEFT JOIN [DEVMIS\SQL_INS01].dbStaf.dbo.MS01_Peribadi AS C ON C.MS01_NoStaf = A.No_Staf
                     WHERE A.Jenis_Trans = 'T' AND A.Bulan = @bulan AND A.Tahun = @tahun"
        ElseIf Session("ptj") = "-0000" Then
            query = "SELECT A.No_Staf,C.MS01_Nama AS Nama,B.No_Cukai AS No_Cukai,C.MS01_KpB AS No_KP, B.Kategori_Cukai,A.Amaun,A.Jenis_Trans
                     FROM SMKB_Gaji_Lejar A
                     LEFT JOIN SMKB_Gaji_Staf B ON B.No_Staf = A.No_Staf
                     LEFT JOIN [DEVMIS\SQL_INS01].dbStaf.dbo.MS01_Peribadi AS C ON C.MS01_NoStaf = A.No_Staf
                     WHERE A.Jenis_Trans = 'T' AND A.Bulan = @bulan AND A.Tahun = @tahun AND A.Kod_PTJ= '-'"
        Else
            query = "SELECT A.No_Staf,C.MS01_Nama AS Nama,B.No_Cukai AS No_Cukai,C.MS01_KpB AS No_KP, B.Kategori_Cukai,A.Amaun,A.Jenis_Trans
                     FROM SMKB_Gaji_Lejar A
                     LEFT JOIN SMKB_Gaji_Staf B ON B.No_Staf = A.No_Staf
                     LEFT JOIN [DEVMIS\SQL_INS01].dbStaf.dbo.MS01_Peribadi AS C ON C.MS01_NoStaf = A.No_Staf
                     WHERE A.Jenis_Trans = 'T' AND A.Bulan = @bulan AND A.Tahun = @tahun AND A.Kod_PTJ= @ptj"
        End If

        Dim dataTable As New DataTable()
        Using connection As New SqlConnection(strCon)
            Using command As New SqlCommand(query, connection)
                command.Parameters.Add(New SqlParameter("@bulan", bulan))
                command.Parameters.Add(New SqlParameter("@tahun", tahun))
                command.Parameters.Add(New SqlParameter("@ptj", ptj))

                connection.Open()
                dataTable.Load(command.ExecuteReader())
            End Using
        End Using
        Return dataTable
    End Function

    Protected Sub rptone_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
        Dim rptItem As RepeaterItem = TryCast(e.Item, RepeaterItem)
        Dim bulan As String = rptItem.DataItem("bulan")
        Dim tahun As String = rptItem.DataItem("tahun")
        Dim ptj As String = rptItem.DataItem("ptj")

        Dim rptTwo As Repeater = TryCast(rptItem.FindControl("repeaterDetail"), Repeater)
        'rptTwo.DataSource = LoadDataTable(Session("bulan"), Session("tahun"), Session("ptj"))
        rptTwo.DataSource = LoadDataTable(bulan, tahun, ptj)
        rptTwo.DataBind()

    End Sub
End Class