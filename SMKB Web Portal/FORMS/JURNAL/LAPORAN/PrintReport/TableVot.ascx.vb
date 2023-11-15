Imports System
Imports System.Data.SqlClient
Imports System.Diagnostics.Eventing.Reader
Imports System.Reflection.Emit
Imports System.Web.UI.HtmlControls

Public Class TableVot
    Inherits System.Web.UI.UserControl
    Public Property KodVotFrom As Decimal
    Public Property KodVotTo As Decimal
    Public Property Tajuk As String
    Public Property TotalAmaun As Decimal
    Public Property TotalAmaun2 As Decimal
    Public Property CustomClass As String
    Public Property Bulan As String
    Public Property Tahun As String
    Public Property PTj As String
    Public Property Syarikat As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblTajuk.innerHtml = Tajuk
        tableData.Attributes("class") += " " + CustomClass
        LoadDataTable(KodVotFrom, KodVotTo, "tableData")
    End Sub

    Private Sub LoadDataTable(kodVotFrom As String, kodVotTO As String, namaTable As String)
        ' Retrieve data from the database

        'Dim connectionString As String = "server=v-sql12.utem.edu.my;database=DbKewangan;uid=smkb;pwd=smkb*pwd;"
        Dim connectionString As String = "server=devmis12.utem.edu.my;database=DbKewanganV4;uid=smkb;pwd=Smkb@Dev2012;"

        Dim query As String

        If Session("ptj") = "00" Then
            query = "SELECT * FROM 
                        (
                        SELECT substring(A.Kod_Vot, 1, 2) + '000' AS kodvot,
	                        (select Butiran from SMKB_Vot where Kod_Vot = substring(A.Kod_Vot, 1, 2) + '000') as ButiranDetail,
	                        substring(A.Kod_Vot, 1, 1) + '0000' AS kodvotH,
	                        (select Butiran from SMKB_Vot where Kod_Vot = substring(A.Kod_Vot, 1, 1) + '0000') as ButiranHeader,
	                        B.Kod_Jenis, 
	                        REPLACE(SUM(
		                        CASE 
		                        WHEN @bulan = 1 THEN (A.Dr_1 - A.Cr_1)
		                        WHEN @bulan = 2 THEN (A.Dr_1 + A.Dr_2) - (A.Cr_1 + A.Cr_2)
		                        WHEN @bulan = 3 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3) - (A.Cr_1 + A.Cr_2 + A.Cr_3)
		                        WHEN @bulan = 4 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4) - (A.Cr_1 + A.Cr_2 + A.Cr_2 + A.Cr_4)
		                        WHEN @bulan = 5 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5)
		                        WHEN @bulan = 6 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6)
		                        WHEN @bulan = 7 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7)
		                        WHEN @bulan = 8 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8)	
		                        WHEN @bulan = 9 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9)
		                        WHEN @bulan = 10 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10)
		                        WHEN @bulan = 11 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11)
		                        WHEN @bulan = 12 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)
		                        ELSE 0 
		                        END
	                        ), '-', '') AS amaun,
	                        REPLACE(SUM(CASE 
	                        WHEN @bulan = 1 THEN (A.Dr_1 - A.Cr_1)
	                        WHEN @bulan = 2 THEN (A.Dr_2 - A.Cr_2) 
	                        WHEN @bulan = 3 THEN (A.Dr_3 - A.Cr_3) 
                            WHEN @bulan = 4 THEN (A.Dr_4 - A.Cr_4) 
	                        WHEN @bulan = 5 THEN (A.Dr_5 - A.Cr_5)
                            WHEN @bulan = 6 THEN (A.Dr_6 - A.Cr_6)
	                        WHEN @bulan = 7 THEN (A.Dr_7 - A.Cr_7)
	                        WHEN @bulan = 8 THEN (A.Dr_8 - A.Cr_8)
	                        WHEN @bulan = 9 THEN (A.Dr_9 - A.Cr_9)
	                        WHEN @bulan = 10 THEN (A.Dr_10 - A.Cr_10)
	                        WHEN @bulan = 11 THEN (A.Dr_11 - A.Cr_11)
                            WHEN @bulan = 12 THEN (A.Dr_12 - A.Cr_12)

	                        ELSE 0 
	                        END), '-','') AS amaun2
                        FROM
                        SMKB_Lejar_Am A
                        JOIN SMKB_Vot B ON A.Kod_Vot=B.Kod_Vot
                        WHERE
                        A.Tahun = @tahun 
                        And B.Kod_Jenis In ('I','E')
                        And A.Kod_Kump_Wang >= '01'
                        And A.Kod_Kump_Wang <= '11'
                        And A.Kod_PTJ >= '010000'
                        And A.Kod_PTJ <='Y00005'
                        And A.Kod_Vot >= @kodVotFrom AND A.Kod_Vot <= @kodVotTo
                        --AND Kod_Syarikat = 'UTeM'
                        GROUP BY
                        B.Kod_Jenis,
                        substring(A.Kod_Vot, 1, 2) + '000',
                        substring(A.Kod_Vot, 1, 1) + '0000'
                        )mainTbl WHERE (CAST(amaun as decimal)  <> 0.00 or CAST(amaun2 as decimal) <> 0.00)
                        ORDER BY
                        Kod_Jenis DESC, kodvot"
        Else
            query = "SELECT * FROM 
                        (
                        SELECT substring(A.Kod_Vot, 1, 2) + '000' AS kodvot,
	                        (select Butiran from SMKB_Vot where Kod_Vot = substring(A.Kod_Vot, 1, 2) + '000') as ButiranDetail,
	                        substring(A.Kod_Vot, 1, 1) + '0000' AS kodvotH,
	                        (select Butiran from SMKB_Vot where Kod_Vot = substring(A.Kod_Vot, 1, 1) + '0000') as ButiranHeader,
	                        B.Kod_Jenis, 
	                        REPLACE(SUM(
		                        CASE 
		                        WHEN @bulan = 1 THEN (A.Dr_1 - A.Cr_1)
		                        WHEN @bulan = 2 THEN (A.Dr_1 + A.Dr_2) - (A.Cr_1 + A.Cr_2)
		                        WHEN @bulan = 3 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3) - (A.Cr_1 + A.Cr_2 + A.Cr_3)
		                        WHEN @bulan = 4 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4) - (A.Cr_1 + A.Cr_2 + A.Cr_2 + A.Cr_4)
		                        WHEN @bulan = 5 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5)
		                        WHEN @bulan = 6 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6)
		                        WHEN @bulan = 7 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7)
		                        WHEN @bulan = 8 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8)	
		                        WHEN @bulan = 9 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9)
		                        WHEN @bulan = 10 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10)
		                        WHEN @bulan = 11 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11)
		                        WHEN @bulan = 12 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4 + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)
		                        ELSE 0 
		                        END
	                        ), '-', '') AS amaun,
	                        REPLACE(SUM(CASE 
	                        WHEN @bulan = 1 THEN (A.Dr_1 - A.Cr_1)
	                        WHEN @bulan = 2 THEN (A.Dr_2 - A.Cr_2) 
	                        WHEN @bulan = 3 THEN (A.Dr_3 - A.Cr_3) 
                            WHEN @bulan = 4 THEN (A.Dr_4 - A.Cr_4) 
	                        WHEN @bulan = 5 THEN (A.Dr_5 - A.Cr_5)
                            WHEN @bulan = 6 THEN (A.Dr_6 - A.Cr_6)
	                        WHEN @bulan = 7 THEN (A.Dr_7 - A.Cr_7)
	                        WHEN @bulan = 8 THEN (A.Dr_8 - A.Cr_8)
	                        WHEN @bulan = 9 THEN (A.Dr_9 - A.Cr_9)
	                        WHEN @bulan = 10 THEN (A.Dr_10 - A.Cr_10)
	                        WHEN @bulan = 11 THEN (A.Dr_11 - A.Cr_11)
                            WHEN @bulan = 12 THEN (A.Dr_12 - A.Cr_12)

	                        ELSE 0 
	                        END), '-','') AS amaun2
                        FROM
                        SMKB_Lejar_Am A
                        JOIN SMKB_Vot B ON A.Kod_Vot=B.Kod_Vot
                        WHERE
                        A.Tahun = @tahun 
                        And B.Kod_Jenis In ('I','E')
                        And A.Kod_Kump_Wang >= '01'
                        And A.Kod_Kump_Wang <= '11'
                        And A.Kod_PTJ >= @ptj
                        And A.Kod_PTJ <= @ptj
                        And A.Kod_Vot >= @kodVotFrom AND A.Kod_Vot <= @kodVotTo
                        --AND Kod_Syarikat = 'UTeM'
                        GROUP BY
                        B.Kod_Jenis,
                        substring(A.Kod_Vot, 1, 2) + '000',
                        substring(A.Kod_Vot, 1, 1) + '0000'
                        )mainTbl WHERE (CAST(amaun as decimal)  <> 0.00 or CAST(amaun2 as decimal) <> 0.00)
                        ORDER BY
                        Kod_Jenis DESC, kodvot "
        End If


        Dim dataTable As New DataTable()



        If Session("bulan") = "" Then
            Session("bulan") = "-"
        End If

        If Session("tahun") = "" Then
            Session("tahun") = "-"
        End If

        If Session("ptj") = "" Then
            Session("ptj") = "-"
        End If

        Using connection As New SqlConnection(connectionString)
            Using command As New SqlCommand(query, connection)
                command.Parameters.Add(New SqlParameter("@kodVotFrom", kodVotFrom))
                command.Parameters.Add(New SqlParameter("@kodVotTo", kodVotTO))
                command.Parameters.Add(New SqlParameter("@bulan", session("bulan")))
                command.Parameters.Add(New SqlParameter("@tahun", Session("tahun")))
                command.Parameters.Add(New SqlParameter("@ptj", Session("ptj")))
                connection.Open()
                dataTable.Load(command.ExecuteReader())
            End Using
        End Using

        ' Bind the data to the HTML table
        'BindDataToTable2(dataTable, totalAmaun, totalAmaun2)
        BindDataToTableDynamic(dataTable, namaTable)
    End Sub

    Private Sub BindDataToTableDynamic(ByVal dataTable As DataTable, namaTable As String)
        Dim table As HtmlTable = CType(FindControl(namaTable), HtmlTable)
        Dim totalAmaun As Decimal = 0 ' Variable to store the total amaun
        Dim totalAmaun2 As Decimal = 0 ' Variable to store the total amaun2
        ' Loop through the DataTable rows and populate the HTML table dynamically
        For Each row As DataRow In dataTable.Rows
            Dim newRow As New HtmlTableRow()

            ' Add columns to the new row
            Dim cell1 As New HtmlTableCell()
            cell1.InnerText = row("kodvot").ToString() ' Replace "kodvot" with the actual column name in your database
            cell1.Attributes("class") = "headerkiri"
            newRow.Cells.Add(cell1)

            Dim cell2 As New HtmlTableCell()
            cell2.InnerText = row("ButiranDetail").ToString() ' Replace "ButiranDetail" with the actual column name in your database
            cell2.Attributes("class") = "align-left"
            newRow.Cells.Add(cell2)

            Dim cell3 As New HtmlTableCell()
            cell3.InnerText = Convert.ToDecimal(row("amaun")).ToString("N") ' Replace "amaun" with the actual column name in your database
            cell3.Attributes("class") = "valuekanan"
            newRow.Cells.Add(cell3)

            Dim cell4 As New HtmlTableCell()
            cell4.InnerText = Convert.ToDecimal(row("amaun2")).ToString("N") ' Replace "amaun2" with the actual column name in your database
            cell4.Attributes("class") = "valuekanan"
            newRow.Cells.Add(cell4)

            ' Calculate the total amaun and amaun2
            Dim amaun As Decimal = Convert.ToDecimal(row("amaun")) ' Convert the amaun value to decimal
            Dim amaun2 As Decimal = Convert.ToDecimal(row("amaun2")) ' Convert the amaun2 value to decimal
            totalAmaun += amaun ' Accumulate the amaun value to the totalAmaun variable
            totalAmaun2 += amaun2 ' Accumulate the amaun2 value to the totalAmaun2 variable

            ' Add the new row to the table
            table.Rows.Add(newRow)
        Next

        ' Add the last row with total amounts
        Dim totalRow As New HtmlTableRow()

        Dim emptyCell As New HtmlTableCell()
        totalRow.Cells.Add(emptyCell)

        Dim emptyCell2 As New HtmlTableCell()
        totalRow.Cells.Add(emptyCell2)

        Dim totalAmaunCell As New HtmlTableCell()
        Dim lbl As New System.Web.UI.WebControls.Label()
        lbl.Attributes("class") = "top-border lblamaun"
        lbl.Text = totalAmaun.ToString("N")

        'totalAmaunCell.InnerText = totalAmaun.ToString("N")
        totalAmaunCell.Attributes("class") = "valuekanan bold "
        totalAmaunCell.Attributes("id") = "totalAmaun" & namaTable
        totalAmaunCell.controls.add(lbl)
        totalRow.Cells.Add(totalAmaunCell)
        'totalAmaun = totalAmaunCell.InnerText

        Dim totalAmaun2Cell As New HtmlTableCell()
        Dim lbl2 As New System.Web.UI.WebControls.Label()
        lbl2.id = "lbl2" & namaTable
        lbl2.attributes("class") = "top-border lblamaun"
        lbl2.Text = totalAmaun2.ToString("N")

        'totalAmaun2Cell.InnerText = totalAmaun2.ToString("N")
        totalAmaun2Cell.Attributes("class") = "valuekanan bold"
        totalAmaun2Cell.Attributes("id") = "totalAmaun2" & namaTable
        totalAmaun2Cell.controls.add(lbl2)
        totalRow.Cells.Add(totalAmaun2Cell)
        'totalAmaun2 = totalAmaun2Cell.InnerText

        ' Add the last row to the table
        table.Rows.Add(totalRow)

        ' Find the HTML elements by their IDs
        Dim td1 As HtmlTableCell = CType(FindControl("amaunEmo"), HtmlTableCell)
        Dim td2 As HtmlTableCell = CType(FindControl("amaun2Emo"), HtmlTableCell)
    End Sub

End Class