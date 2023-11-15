Imports System
Imports System.Data.SqlClient

Public Class tableBayarBalik
    Inherits System.Web.UI.UserControl
    Public Property Tajuk As String

    Public Property noPinj As String

    Public Property CustomClass As String
    Dim dbconn As New DBKewConn

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblTajuk.innerHtml = Tajuk
        tableData.Attributes("class") += " " + CustomClass
        LoadDataTable(noPinj, "tableData")
    End Sub
    Private Sub LoadDataTable(pNoPinj As String, namaTable As String)
        ' Retrieve data from the database

        Dim query As String

        query = "select  Bil_Byr, Ansuran, Faedah, Pokok, Ansuran, Baki_Pokok
                from SMKB_Pinjaman_Jadual_Bayar_Balik
                where No_Pinj = @NoPinjaman"

        Dim dataTable As New DataTable()

        Using dbconnNew As New SqlConnection(strCon)

            Using command As New SqlCommand(query, dbconnNew)
                command.Parameters.Add(New SqlParameter("@NoPinjaman", pNoPinj))
                dbconnNew.Open()
                dataTable.Load(command.ExecuteReader())
            End Using
        End Using

        BindDataToTableDynamic(dataTable, namaTable)
    End Sub

    Private Sub BindDataToTableDynamic(ByVal dataTable As DataTable, namaTable As String)
        Dim table As HtmlTable = CType(FindControl(namaTable), HtmlTable)

        ' Loop through the DataTable rows and populate the HTML table dynamically
        For Each row As DataRow In dataTable.Rows
            Dim newRow As New HtmlTableRow()

            ' Add columns to the new row
            Dim cell1 As New HtmlTableCell()
            cell1.InnerText = row("Bil_Byr").ToString()
            cell1.Attributes("class") = "headerkiri"
            newRow.Cells.Add(cell1)

            Dim cell2 As New HtmlTableCell()
            cell2.InnerText = Convert.ToDecimal(row("Ansuran")).ToString("N")
            cell2.Attributes("class") = "align-left"
            newRow.Cells.Add(cell2)

            Dim cell3 As New HtmlTableCell()
            cell3.InnerText = Convert.ToDecimal(row("Faedah")).ToString("N")
            cell3.Attributes("class") = "valuekanan"
            newRow.Cells.Add(cell3)

            Dim cell4 As New HtmlTableCell()
            cell4.InnerText = Convert.ToDecimal(row("Pokok")).ToString("N")
            cell4.Attributes("class") = "valuekanan"
            newRow.Cells.Add(cell4)

            Dim cell5 As New HtmlTableCell()
            cell5.InnerText = ""
            cell5.Attributes("class") = "valuekanan"
            newRow.Cells.Add(cell5)

            Dim cell6 As New HtmlTableCell()
            cell6.InnerText = ""
            cell6.Attributes("class") = "valuekanan"
            newRow.Cells.Add(cell6)

            Dim cell7 As New HtmlTableCell()
            cell7.InnerText = Convert.ToDecimal(row("Baki_Pokok")).ToString("N")
            cell7.Attributes("class") = "valuekanan"
            newRow.Cells.Add(cell7)


            ' Add the new row to the table
            table.Rows.Add(newRow)
        Next



    End Sub
End Class