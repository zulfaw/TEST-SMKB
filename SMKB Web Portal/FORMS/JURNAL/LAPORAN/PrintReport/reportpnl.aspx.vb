Imports System
Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.Diagnostics.Eventing.Reader
Imports System.Web.UI.HtmlControls

Public Class reportbaru4
    Inherits System.Web.UI.Page
    Dim listBulan As String() = {"Januari", "Februari", "Mac", "April", "Mei", "Jun", "Julai", "Ogos", "September", "Oktober", "November", "Disember"}
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            tahun.Text = Session("tahun")
            Label1.Text = Session("tahun")
            Label2.Text = Session("tahun")
            bulan.Text = listBulan(Session("bulan") - 1)
        End If
    End Sub

    'Private Sub LoadDataTable(kodVotFrom As String, kodVotTO As String, namaTable As String)
    '    ' Retrieve data from the database

    '    'Dim connectionString As String = "server=v-sql12.utem.edu.my;database=DbKewangan;uid=smkb;pwd=smkb*pwd;"
    '    Dim connectionString As String = "server=devmis12.utem.edu.my;database=DbKewanganV4;uid=smkb;pwd=Smkb@Dev2012;"

    '    Dim query As String = "SELECT
    'substring(mk06_transaksi.kodvot, 1, 2) + '000' AS kodvot,
    '                          (select Butiran from MK_Vot where KodVot = substring(mk06_transaksi.kodvot, 1, 2) + '000') as ButiranDetail,
    '                          substring(mk06_transaksi.kodvot, 1, 1) + '0000' AS kodvotH,
    '                          (select Butiran from MK_Vot where KodVot = substring(mk06_transaksi.kodvot, 1, 1) + '0000') as ButiranHeader,

    '                          mk06_transaksi.kodjen,
    '                          replace(sum(CASE WHEN Month(mk06_transaksi.mk06_tkhtran) <= '5' THEN isnull(mk06_transaksi.mk06_debit, 0) - isnull(mk06_transaksi.mk06_kredit, 0) ELSE 0 END),'-','') AS amaun,
    '                          replace(sum(CASE WHEN Month(mk06_transaksi.mk06_tkhtran) = '5' THEN isnull(mk06_transaksi.mk06_debit, 0) - isnull(mk06_transaksi.mk06_kredit, 0) ELSE 0 END),'-','') AS amaun2
    '                        FROM
    '                          mk06_transaksi
    'WHERE
    'year(mk06_transaksi.mk06_tkhtran) = '2021'
    '                          And mk06_transaksi.kodjen In ('I','E')
    '                          And mk06_transaksi.kodkw >= '01'
    '                          And mk06_transaksi.kodkw <= '11'
    '                          And mk06_transaksi.kodptj >= '010000'
    '                          And mk06_transaksi.kodptj <= 'Y00005'
    '                          And mk06_transaksi.koddok Not IN ('LO', 'ADJ_LO', 'CF_LO', 'BJTKURANG', 'BJTTAMBAH', 'VIRKELUAR', 'VIRMASUK')
    '                          And mk06_transaksi.mk06_status IN (0, 1)
    '                          And mk06_transaksi.kodvot >= @kodVotFrom AND mk06_transaksi.kodvot <= @kodVotTo
    '                        GROUP BY
    '                          mk06_transaksi.kodjen,
    '                          substring(mk06_transaksi.kodvot, 1, 2) + '000',
    '                          substring(mk06_transaksi.kodvot, 1, 1) + '0000'
    '                        ORDER BY
    '                          mk06_transaksi.kodjen DESC,
    '                          substring(mk06_transaksi.kodvot, 1, 2) + '000'"

    '    Dim dataTable As New DataTable()


    '    Using connection As New SqlConnection(connectionString)
    '        Using command As New SqlCommand(query, connection)
    '            command.Parameters.Add(New SqlParameter("@kodVotFrom", kodVotFrom))
    '            command.Parameters.Add(New SqlParameter("@kodVotTo", kodVotTO))
    '            connection.Open()
    '            dataTable.Load(command.ExecuteReader())
    '        End Using
    '    End Using


    '    BindDataToTableDynamic(dataTable, namaTable)
    'End Sub

    'Private Sub BindDataToTableDynamic(ByVal dataTable As DataTable, namaTable As String)
    '    Dim table As HtmlTable = CType(FindControl(namaTable), HtmlTable)
    '    Dim totalAmaun As Decimal = 0 ' Variable to store the total amaun
    '    Dim totalAmaun2 As Decimal = 0 ' Variable to store the total amaun2
    '    ' Loop through the DataTable rows and populate the HTML table dynamically
    '    For Each row As DataRow In dataTable.Rows
    '        Dim newRow As New HtmlTableRow()

    '        ' Add columns to the new row
    '        Dim cell1 As New HtmlTableCell()
    '        cell1.InnerText = row("kodvot").ToString() ' Replace "kodvot" with the actual column name in your database
    '        cell1.Attributes("class") = "headerkiri"
    '        newRow.Cells.Add(cell1)

    '        Dim cell2 As New HtmlTableCell()
    '        cell2.InnerText = row("ButiranDetail").ToString() ' Replace "ButiranDetail" with the actual column name in your database
    '        cell2.Attributes("class") = "align-left"
    '        newRow.Cells.Add(cell2)

    '        Dim cell3 As New HtmlTableCell()
    '        cell3.InnerText = Convert.ToDecimal(row("amaun")).ToString("N") ' Replace "amaun" with the actual column name in your database
    '        cell3.Attributes("class") = "valuekanan"
    '        newRow.Cells.Add(cell3)

    '        Dim cell4 As New HtmlTableCell()
    '        cell4.InnerText = Convert.ToDecimal(row("amaun2")).ToString("N") ' Replace "amaun2" with the actual column name in your database
    '        cell4.Attributes("class") = "valuekanan"
    '        newRow.Cells.Add(cell4)

    '        ' Calculate the total amaun and amaun2
    '        Dim amaun As Decimal = Convert.ToDecimal(row("amaun")) ' Convert the amaun value to decimal
    '        Dim amaun2 As Decimal = Convert.ToDecimal(row("amaun2")) ' Convert the amaun2 value to decimal
    '        totalAmaun += amaun ' Accumulate the amaun value to the totalAmaun variable
    '        totalAmaun2 += amaun2 ' Accumulate the amaun2 value to the totalAmaun2 variable

    '        ' Add the new row to the table
    '        table.Rows.Add(newRow)
    '    Next

    '    ' Add the last row with total amounts
    '    Dim totalRow As New HtmlTableRow()

    '    Dim emptyCell As New HtmlTableCell()
    '    totalRow.Cells.Add(emptyCell)

    '    Dim emptyCell2 As New HtmlTableCell()
    '    totalRow.Cells.Add(emptyCell2)

    '    Dim totalAmaunCell As New HtmlTableCell()
    '    totalAmaunCell.InnerText = totalAmaun.ToString("N")
    '    totalAmaunCell.Attributes("class") = "valuekanan bold"
    '    totalAmaunCell.Attributes("id") = "totalAmaun" & namaTable
    '    totalRow.Cells.Add(totalAmaunCell)
    '    ViewState("totalAmaunCell") = totalAmaunCell.InnerText

    '    Dim totalAmaun2Cell As New HtmlTableCell()
    '    totalAmaun2Cell.InnerText = totalAmaun2.ToString("N")
    '    totalAmaun2Cell.Attributes("class") = "valuekanan bold"
    '    totalAmaun2Cell.Attributes("id") = "totalAmaun2" & namaTable
    '    totalAmaunCell.Attributes("runat") = "server"
    '    totalRow.Cells.Add(totalAmaun2Cell)
    '    ViewState("totalAmaun2Cell") = totalAmaun2Cell.InnerText

    '    ' Add the last row to the table
    '    table.Rows.Add(totalRow)

    '    ' Find the HTML elements by their IDs
    '    Dim td1 As HtmlTableCell = CType(FindControl("amaunEmo"), HtmlTableCell)
    '    Dim td2 As HtmlTableCell = CType(FindControl("amaun2Emo"), HtmlTableCell)

    'End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs)

    End Sub
End Class