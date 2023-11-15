Imports System
Imports System.Data.SqlClient
Imports System.Diagnostics.Eventing.Reader
Imports System.Web.UI.HtmlControls
Imports System.Web
Imports System.Web.UI.WebControls

Public Class TableTransaksi
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
		Dim optWhere As String

		If ptj = "00" Then
			optWhere &= ""
		ElseIf ptj = "-0000" Then
			optWhere &= "AND A.Kod_PTJ = '-'"
		Else
			optWhere &= "AND A.Kod_PTJ = @ptj"
		End If

		query = "SELECT No_Staf,Nama,KP,
					COALESCE([G],0.00) AS Gaji_Pokok,
					COALESCE([B],0.00) AS Bonus,
					COALESCE([E],0.00) AS Elaun,
					COALESCE([O],0.00) AS OT,
					(COALESCE([G],0.00)+COALESCE([B],0.00)+COALESCE([E],0.00)+COALESCE([O],0.00)) AS Gaji_Kasar,
					COALESCE([P],0.00) AS Potongan,
					COALESCE([C],0.00) AS Cuti,No_KWSP,
					COALESCE([KWSP],0.00) AS KWSP,
					COALESCE([KWSM],0.00) AS KWSM,
					No_Cukai, Kategori_Cukai,
					COALESCE([T],0.00) AS Cukai,
					(
						(COALESCE([G],0.00)+COALESCE([B],0.00)+COALESCE([E],0.00)+COALESCE([O],0.00)) -
						(COALESCE([P],0.00)+COALESCE([C],0.00)+COALESCE([KWSP],0.00)+COALESCE([SOCP],0.00)+COALESCE([T],0.00))
					) AS Gaji_Bersih,
					No_Perkeso,
					COALESCE([SOCP],0.00) AS SOCP,
					COALESCE([SOCM],0.00) AS SOCM,No_Pencen,
					COALESCE([N],0.00) AS Pencen
				FROM(
					SELECT A.No_Staf, C.MS01_Nama AS Nama, C.MS01_KpB AS KP, A.Jenis_Trans,A.Amaun,KWSM.KWSM,KWSP.KWSP,SOCM.SOCM,SOCP.SOCP, B.No_Cukai, B.Kategori_Cukai, C.MS01_NoPencen AS No_Pencen, C.MS01_NoKWSP AS No_KWSP, B.No_Perkeso
					FROM SMKB_Gaji_Lejar A
					LEFT JOIN SMKB_Gaji_Staf B ON B.No_Staf = A.No_Staf
					LEFT JOIN [DEVMIS\SQL_INS01].dbStaf.dbo.MS01_Peribadi AS C ON C.MS01_NoStaf = A.No_Staf
					LEFT JOIN (SELECT No_Staf, Amaun AS KWSM FROM SMKB_Gaji_Lejar WHERE Bulan = @bulan AND Tahun = @tahun AND Kod_Trans = 'KWSM') AS KWSM ON KWSM.No_Staf = A.No_Staf
					LEFT JOIN (SELECT No_Staf, Amaun AS KWSP FROM SMKB_Gaji_Lejar WHERE Bulan = @bulan AND Tahun = @tahun AND Kod_Trans = 'KWSP') AS KWSP ON KWSP.No_Staf = A.No_Staf
					LEFT JOIN (SELECT No_Staf, Amaun AS SOCM FROM SMKB_Gaji_Lejar WHERE Bulan = @bulan AND Tahun = @tahun AND Kod_Trans = 'SOCM') AS SOCM ON SOCM.No_Staf = A.No_Staf
					LEFT JOIN (SELECT No_Staf, Amaun AS SOCP FROM SMKB_Gaji_Lejar WHERE Bulan = @bulan AND Tahun = @tahun AND Kod_Trans = 'SOCP') AS SOCP ON SOCP.No_Staf = A.No_Staf
					WHERE A.Bulan = @bulan AND A.Tahun = @tahun " + optWhere + "
				)AS SourceTable
				PIVOT(
				Sum(Amaun)
				FOR Jenis_Trans IN ([B],[C],[E],[G],[O],[P],[T],[N])
				) AS PivotTable;"

		'query = "SELECT No_Staf,Nama,KP,No_Cukai, Kategori_Cukai,
		'				COALESCE([T],0.00) AS Cukai,
		'				COALESCE([G],0.00) AS Gaji,
		'				COALESCE([O],0.00) AS OT, No_Pencen,
		'				COALESCE([N],0.00) AS Pencen,No_KWSP,
		'				COALESCE([KWSP],0.00) AS KWSP,
		'				COALESCE([KWSM],0.00) AS KWSM,No_Perkeso,
		'				COALESCE([SOCP],0.00) AS SOCP,
		'				COALESCE([SOCM],0.00) AS SOCM
		'			FROM(
		'				SELECT A.No_Staf, C.MS01_Nama AS Nama, C.MS01_KpB AS KP, A.Jenis_Trans,A.Amaun,KWSM.KWSM,KWSP.KWSP,SOCM.SOCM,SOCP.SOCP, B.No_Cukai, B.Kategori_Cukai, C.MS01_NoPencen AS No_Pencen, C.MS01_NoKWSP AS No_KWSP, B.No_Perkeso
		'				FROM SMKB_Gaji_Lejar A
		'				LEFT JOIN SMKB_Gaji_Staf B ON B.No_Staf = A.No_Staf
		'				LEFT JOIN [DEVMIS\SQL_INS01].dbStaf.dbo.MS01_Peribadi AS C ON C.MS01_NoStaf = A.No_Staf
		'				LEFT JOIN (SELECT No_Staf, Amaun AS KWSM FROM SMKB_Gaji_Lejar WHERE Bulan = @bulan AND Tahun = @tahun AND Kod_Trans = 'KWSM') AS KWSM ON KWSM.No_Staf = A.No_Staf
		'				LEFT JOIN (SELECT No_Staf, Amaun AS KWSP FROM SMKB_Gaji_Lejar WHERE Bulan = @bulan AND Tahun = @tahun AND Kod_Trans = 'KWSP') AS KWSP ON KWSP.No_Staf = A.No_Staf
		'				LEFT JOIN (SELECT No_Staf, Amaun AS SOCM FROM SMKB_Gaji_Lejar WHERE Bulan = @bulan AND Tahun = @tahun AND Kod_Trans = 'SOCM') AS SOCM ON SOCM.No_Staf = A.No_Staf
		'				LEFT JOIN (SELECT No_Staf, Amaun AS SOCP FROM SMKB_Gaji_Lejar WHERE Bulan = @bulan AND Tahun = @tahun AND Kod_Trans = 'SOCP') AS SOCP ON SOCP.No_Staf = A.No_Staf
		'				WHERE A.Bulan = @bulan AND A.Tahun = @tahun " + optWhere + "
		'			)AS SourceTable
		'			PIVOT(
		'			Sum(Amaun)
		'			FOR Jenis_Trans IN ([T],[G],[O],[N])
		'			) AS PivotTable;
		'			"

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