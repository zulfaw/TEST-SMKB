Imports System
Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Data
Imports System.Globalization
Imports System.IO
Imports System.Collections
Imports System.Web.HttpFileCollection

Public Class CetakLaporanPemprosesanAkhirTahun
    Inherits System.Web.UI.Page
    Dim dbconn As New DBKewConn
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim tahun As String = Request.QueryString("year")
        If tahun <> "" Then
            fBindTransaksiTahun(tahun)
        End If
    End Sub
    Protected Sub gvPenghutang_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        ' Check if the row is a DataRow (not header/footer, etc.)
        If e.Row.RowType = DataControlRowType.DataRow Then
            ' Get the value of the first cell in the row (Month column)
            Dim month As String = e.Row.Cells(0).Text

            ' Check if it's the "Jumlah" row and apply styling
            If month = "Jumlah" Then
                e.Row.Font.Bold = True ' Make the text bold
                ' make the text center only for the first cell
                'e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Center
                e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Left
                ' make the text right aligned for the second and third cells
                e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Right
            Else
                '' Merge the cells for the "Jumlah" row
                'e.Row.Cells(0).RowSpan = 2
                'e.Row.Cells(1).Visible = False

                ' If it's not the "Jumlah" row, apply styling to the second and third cells
                e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Right
            End If
        End If
    End Sub
    Private Sub fBindTransaksiTahun(tahun As String)
        If tahun <> "" Then
            Dim strSql As String
            strSql = $"
                    SELECT 
                        PL.Kod_Penghutang,
                        PM.Nama_Penghutang,
                        PL.Tahun,
                        SUM(PL.Dr_1) + SUM(PL.Dr_2) + SUM(PL.Dr_3) + SUM(PL.Dr_4) + SUM(PL.Dr_5) +
                        SUM(PL.Dr_6) + SUM(PL.Dr_7) + SUM(PL.Dr_8) + SUM(PL.Dr_9) + SUM(PL.Dr_10) +
                        SUM(PL.Dr_11) + SUM(PL.Dr_12) + SUM(PL.Dr_13) AS Total_Dr,
                        SUM(PL.Cr_1) + SUM(PL.Cr_2) + SUM(PL.Cr_3) + SUM(PL.Cr_4) + SUM(PL.Cr_5) +
                        SUM(PL.Cr_6) + SUM(PL.Cr_7) + SUM(PL.Cr_8) + SUM(PL.Cr_9) + SUM(PL.Cr_10) +
                        SUM(PL.Cr_11) + SUM(PL.Cr_12) + SUM(PL.Cr_13) AS Total_Cr
                    FROM SMKB_Lejar_Penghutang PL
                    JOIN SMKB_Penghutang_Master PM ON PL.Kod_Penghutang = PM.Kod_Penghutang
                    WHERE PL.Tahun = '{tahun}'
                    GROUP BY PL.Kod_Penghutang, PM.Nama_Penghutang, PL.Tahun;"
            Dim ds = dbconn.fSelectCommand(strSql)

            If ds.Tables(0).Rows.Count > 0 Then
                Dim dt As New DataTable()
                dt.Columns.Add("NoPenghutang", GetType(String))
                dt.Columns.Add("Nama", GetType(String))
                dt.Columns.Add("Debit", GetType(Decimal))
                dt.Columns.Add("Credit", GetType(Decimal))

                Dim totalDr As Decimal = 0D
                Dim totalCr As Decimal = 0D

                ' loop through the rows of the SQL result and add monthly data to the new DataTable.
                For Each row As DataRow In ds.Tables(0).Rows
                    dt.Rows.Add(row.Field(Of String)("Kod_Penghutang"), row.Field(Of String)("Nama_Penghutang"), row.Field(Of Decimal)("Total_Dr"), row.Field(Of Decimal)("Total_Cr"))
                    totalDr += row.Field(Of Decimal)("Total_Dr")
                    totalCr += row.Field(Of Decimal)("Total_Cr")
                Next

                ' Add the total sum row to the DataTable
                Dim newRow As DataRow = dt.Rows.Add("", "Jumlah", totalDr, totalCr)

                ' Add a new column for the merged cell
                Dim mergedColumn As DataColumn = New DataColumn("Jumlah", GetType(String))
                dt.Columns.Add(mergedColumn)

                ' Set the value for the merged column
                newRow("Jumlah") = "Jumlah"

                ' Hide the original columns
                dt.Columns("NoPenghutang").ColumnMapping = MappingType.Hidden
                dt.Columns("Nama").ColumnMapping = MappingType.Hidden

                ' Bind the new DataTable to the GridView.
                gvPenghutangTahun.DataSource = dt
                gvPenghutangTahun.DataBind()

                txtTahun.InnerText = tahun

                txtJumlahCredit.InnerText = totalCr.ToString("N2")
                txtJumlahDebit.InnerText = totalDr.ToString("N2")

                'always return positive value for JumlahBaki
                txtJumlahBaki.InnerText = (Math.Abs(totalDr - totalCr)).ToString("N2")
            End If
        End If
    End Sub
End Class