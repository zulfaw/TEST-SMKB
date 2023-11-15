Imports System
Imports System.Data.SqlClient
Imports System.Diagnostics.Eventing.Reader
Imports System.Web.UI.HtmlControls
Imports System.Web
Imports System.Web.UI.WebControls

Public Class TablePinjamanBulanan
    Inherits System.Web.UI.UserControl

	Dim totalPinjamanAll As Decimal = 0
	Dim totalPotBulananAll As Decimal = 0
	Dim totalPokokAll As Decimal = 0
	Dim totalUntungAll As Decimal = 0

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'lblTajuk.InnerHtml = Tajuk
            Dim dataTable As New DataTable()

			dataTable.Columns.Add("ptj", GetType(String))
			dataTable.Columns.Add("kodpinjaman", GetType(String))
			dataTable.Rows.Add(Session("ptj"), Session("kodpinjaman"))

			rptone.DataSource = dataTable
            rptone.DataBind()
        End If
    End Sub

	Private Function LoadDataTable(ptj As String, kodpinjaman As String) As DataTable
		Dim query As String
		Dim optWhere As String

		If ptj = "00" Then
			ptj = "%"
		ElseIf ptj = "-0000" Then
			ptj = "-"
		Else
			ptj = ptj + "%"
		End If

		query = "SELECT A.Kod_Trans,A.Bulan, A.Tahun,B.No_Trans,A.No_Staf, E.MS01_Nama AS Nama, C.Amaun AS Pinjaman,A.Amaun AS Pot_bulanan, D.Pokok, (A.Amaun - D.Pokok) AS Untung
				FROM SMKB_Gaji_Lejar A
				INNER JOIN SMKB_Gaji_Master B ON B.No_Staf = A.No_Staf AND B.Kod_Trans = @kodpinjaman AND B.Tkh_Tamat > GETDATE()
				INNER JOIN SMKB_Pinjaman_Master C ON C.No_Pinj = B.No_Trans 
				INNER JOIN SMKB_Pinjaman_Jadual_Bayar_Balik D ON D.No_Pinj = B.No_Trans AND RIGHT(D.Bln_GJ, 2) = MONTH(GETDATE()) AND LEFT(D.Bln_GJ,4) = YEAR(GETDATE())
				LEFT JOIN [DEVMIS\SQL_INS01].dbStaf.dbo.MS01_Peribadi AS E ON E.MS01_NoStaf = A.No_Staf
				WHERE A.Tahun = YEAR(GETDATE()) AND A.Bulan = MONTH(GETDATE()) AND A.Kod_PTJ LIKE @ptj AND A.Kod_Trans = @kodpinjaman"

		'query = "SELECT A.Kod_Trans,A.Bulan, A.Tahun,B.No_Trans,A.No_Staf, E.MS01_Nama AS Nama, C.Amaun AS Pinjaman,A.Amaun AS Pot_bulanan, D.Pokok, (A.Amaun - D.Pokok) AS Untung
		'		FROM SMKB_Gaji_Lejar A
		'		INNER JOIN SMKB_Gaji_Master B ON B.No_Staf = A.No_Staf AND B.Kod_Trans = @kodpinjaman AND B.Tkh_Tamat > GETDATE()
		'		INNER JOIN SMKB_Pinjaman_Master C ON C.No_Pinj = B.No_Trans 
		'		INNER JOIN SMKB_Pinjaman_Jadual_Bayar_Balik D ON D.No_Pinj = B.No_Trans AND RIGHT(D.Bln_GJ, 2) = MONTH('2022-10-30 15:39:14.297') AND LEFT(D.Bln_GJ,4) = YEAR('2022-10-30 15:39:14.297')
		'		LEFT JOIN [DEVMIS\SQL_INS01].dbStaf.dbo.MS01_Peribadi AS E ON E.MS01_NoStaf = A.No_Staf
		'		WHERE A.Tahun = YEAR('2022-10-30 15:39:14.297') AND A.Bulan = MONTH('2022-10-30 15:39:14.297') AND A.Kod_PTJ LIKE @ptj AND A.Kod_Trans = @kodpinjaman"

		Dim dataTable As New DataTable()
		Using connection As New SqlConnection(strCon)
			Using command As New SqlCommand(query, connection)
				command.Parameters.Add(New SqlParameter("@ptj", ptj))
				command.Parameters.Add(New SqlParameter("@kodpinjaman", kodpinjaman))

				connection.Open()
				dataTable.Load(command.ExecuteReader())
			End Using
		End Using
		Return dataTable
	End Function

	Protected Sub rptone_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
		Dim rptItem As RepeaterItem = TryCast(e.Item, RepeaterItem)
		Dim ptj As String = rptItem.DataItem("ptj")
		Dim kodpinjaman As String = rptItem.DataItem("kodpinjaman")

		Dim rptTwo As Repeater = TryCast(rptItem.FindControl("repeaterDetail"), Repeater)
		rptTwo.DataSource = LoadDataTable(ptj, kodpinjaman)
		rptTwo.DataBind()

		Dim jumlahPinjamanControl As HtmlTableCell = DirectCast(e.Item.FindControl("jumlahPinjaman"), HtmlTableCell)
		Dim jumlahPotBulananControl As HtmlTableCell = DirectCast(e.Item.FindControl("jumlahPot_bulanan"), HtmlTableCell)
		Dim jumlahPokokAllControl As HtmlTableCell = DirectCast(e.Item.FindControl("jumlahPokok"), HtmlTableCell)
		Dim jumlahUntungAllControl As HtmlTableCell = DirectCast(e.Item.FindControl("jumlahUntung"), HtmlTableCell)

		If jumlahPinjamanControl IsNot Nothing Then
			jumlahPinjamanControl.InnerText = totalPinjamanAll.ToString("N2") ' Format as a decimal with two decimal places
		End If
		If jumlahPotBulananControl IsNot Nothing Then
			jumlahPotBulananControl.InnerText = totalPotBulananAll.ToString("N2") ' Format as a decimal with two decimal places
		End If
		If jumlahPokokAllControl IsNot Nothing Then
			jumlahPokokAllControl.InnerText = totalPokokAll.ToString("N2") ' Format as a decimal with two decimal places
		End If
		If jumlahUntungAllControl IsNot Nothing Then
			jumlahUntungAllControl.InnerText = totalUntungAll.ToString("N2") ' Format as a decimal with two decimal places
		End If

	End Sub

	Protected Sub repeaterDetail_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
		If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
			' Retrieve the Amaun value from the data source
			Dim Pinjaman As Decimal = Convert.ToDecimal(DataBinder.Eval(e.Item.DataItem, "Pinjaman"))
			Dim Pot_bulanan As Decimal = Convert.ToDecimal(DataBinder.Eval(e.Item.DataItem, "Pot_bulanan"))
			Dim Pokok As Decimal = Convert.ToDecimal(DataBinder.Eval(e.Item.DataItem, "Pokok"))
			Dim Untung As Decimal = Convert.ToDecimal(DataBinder.Eval(e.Item.DataItem, "Untung"))

			totalPinjamanAll += Pinjaman
			totalPotBulananAll += Pot_bulanan
			totalPokokAll += Pokok
			totalUntungAll += Untung
		End If
	End Sub

End Class