Imports System
Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Data
Imports System.Globalization
Imports System.IO
Imports System.Collections
Imports System.Web.HttpFileCollection

Public Class CetakPenyataAkaunPenghutang
    Inherits System.Web.UI.Page
    Dim dbconn As New DBKewConn
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim noAkaun As String = Request.QueryString("noAkaun")
        If noAkaun <> "" Then
            fBindTransaksi(noAkaun)
            fBindTransaksiLepas(noAkaun)
        End If
    End Sub

    Private Sub fBindTransaksi(noAkaun As String)

        ' get current Year
        Dim currentYear As String = DateTime.Now.Year.ToString

        Dim totalDrLepas As Decimal = 0D
        Dim totalCrLepas As Decimal = 0D

        Dim totalDr As Decimal = 0D
        Dim totalCr As Decimal = 0D

        ' append data for jumlah lepas
        If noAkaun <> "" Then
            Dim strSql As String
            strSql = $"
                    SELECT 
                        Kod_Penghutang,
                        Tahun,
                        SUM(Dr_1 + Dr_2 + Dr_3 + Dr_4 + Dr_5 + Dr_6 + Dr_7 + Dr_8 + Dr_9 + Dr_10 + Dr_11 + Dr_12 + Dr_13) AS Dr_Total,
                        SUM(Cr_1 + Cr_2 + Cr_3 + Cr_4 + Cr_5 + Cr_6 + Cr_7 + Cr_8 + Cr_9 + Cr_10 + Cr_11 + Cr_12 + Cr_13) AS Cr_Total
                    FROM SMKB_Lejar_Penghutang
                    WHERE Kod_Penghutang = '{noAkaun}' AND Tahun <> '{currentYear}'
                    GROUP BY Kod_Penghutang, Tahun;"

            Dim ds = dbconn.fSelectCommand(strSql)

            If ds.Tables(0).Rows.Count > 0 Then
                For Each row As DataRow In ds.Tables(0).Rows
                    totalDrLepas += row.Field(Of Decimal)("Dr_Total")
                    totalCrLepas += row.Field(Of Decimal)("Cr_Total")
                Next
            End If

        End If

        ' append the data for penghutang details and table
        If noAkaun <> "" Then
            Dim strSql As String
            strSql = $"
                    SELECT 
                        PL.Kod_Penghutang,
                        PM.Nama_Penghutang,
                        PL.Tahun,
                        PM.Alamat_1,
                        PM.Alamat_2,
                        PM.Poskod,
	                    B.Butiran AS ButiranBandar,
	                    C.Butiran AS ButiranKodNegeri,
	                    D.Butiran AS ButiranKodNegara,
                        SUM(PL.Dr_1) AS Dr_1_Sum,
                        SUM(PL.Cr_1) AS Cr_1_Sum,
                        SUM(PL.Dr_2) AS Dr_2_Sum,
                        SUM(PL.Cr_2) AS Cr_2_Sum,
                        SUM(PL.Dr_3) AS Dr_3_Sum,
                        SUM(PL.Cr_3) AS Cr_3_Sum,
                        SUM(PL.Dr_4) AS Dr_4_Sum,
                        SUM(PL.Cr_4) AS Cr_4_Sum,
                        SUM(PL.Dr_5) AS Dr_5_Sum,
                        SUM(PL.Cr_5) AS Cr_5_Sum,
                        SUM(PL.Dr_6) AS Dr_6_Sum,
                        SUM(PL.Cr_6) AS Cr_6_Sum,
                        SUM(PL.Dr_7) AS Dr_7_Sum,
                        SUM(PL.Cr_7) AS Cr_7_Sum,
                        SUM(PL.Dr_8) AS Dr_8_Sum,
                        SUM(PL.Cr_8) AS Cr_8_Sum,
                        SUM(PL.Dr_9) AS Dr_9_Sum,
                        SUM(PL.Cr_9) AS Cr_9_Sum,
                        SUM(PL.Dr_10) AS Dr_10_Sum,
                        SUM(PL.Cr_10) AS Cr_10_Sum,
                        SUM(PL.Dr_11) AS Dr_11_Sum,
                        SUM(PL.Cr_11) AS Cr_11_Sum,
                        SUM(PL.Dr_12) AS Dr_12_Sum,
                        SUM(PL.Cr_12) AS Cr_12_Sum,
                        SUM(PL.Dr_13) AS Dr_13_Sum,
                        SUM(PL.Cr_13) AS Cr_13_Sum
                    FROM SMKB_Lejar_Penghutang PL
                    JOIN SMKB_Penghutang_Master PM ON PL.Kod_Penghutang = PM.Kod_Penghutang
                    LEFT JOIN SMKB_Lookup_Detail B ON PM.Bandar = B.Kod_Detail AND B.Kod = '0003'
                    LEFT JOIN SMKB_Lookup_Detail C ON PM.Kod_Negeri = C.Kod_Detail AND C.Kod = '0002'
                    LEFT JOIN SMKB_Lookup_Detail D ON PM.Kod_Negara = D.Kod_Detail AND D.Kod = '0001'
                    WHERE PL.Kod_Penghutang = '{noAkaun}' AND PL.Tahun = '{currentYear}'
                    GROUP BY PL.Kod_Penghutang, PM.Nama_Penghutang, PM.Alamat_1, PM.Alamat_2, PM.Poskod, PL.Tahun, B.Butiran, C.Butiran, D.Butiran;"
            Dim ds = dbconn.fSelectCommand(strSql)

            If ds.Tables(0).Rows.Count > 0 Then
                txtNama.InnerText = ds.Tables(0).Rows(0)("Nama_Penghutang").ToString
                txtAlamat1.InnerText = ds.Tables(0).Rows(0)("Alamat_1").ToString
                txtAlamat2.InnerText = ds.Tables(0).Rows(0)("Alamat_2").ToString
                txtAlamat3.InnerText = ds.Tables(0).Rows(0)("Poskod").ToString + ", " + ds.Tables(0).Rows(0)("ButiranBandar").ToString
                txtAlamat4.InnerText = ds.Tables(0).Rows(0)("ButiranKodNegeri").ToString + ", " + ds.Tables(0).Rows(0)("ButiranKodNegara").ToString

                ' Create a new DataTable to store the monthly Debit (Dr) and Credit (Cr) data.
                Dim dt As New DataTable()
                dt.Columns.Add("Month", GetType(String))
                dt.Columns.Add("Debit", GetType(Decimal))
                dt.Columns.Add("Credit", GetType(Decimal))

                ' Get the data row from the SQL result
                Dim row As DataRow = ds.Tables(0).Rows(0)

                ' Iterate through the rows of the SQL result and add monthly data to the new DataTable.
                dt.Rows.Add("Januari", row.Field(Of Decimal)("Dr_1_Sum"), row.Field(Of Decimal)("Cr_1_Sum"))
                dt.Rows.Add("Februari", row.Field(Of Decimal)("Dr_2_Sum"), row.Field(Of Decimal)("Cr_2_Sum"))
                dt.Rows.Add("Mac", row.Field(Of Decimal)("Dr_3_Sum"), row.Field(Of Decimal)("Cr_3_Sum"))
                dt.Rows.Add("April", row.Field(Of Decimal)("Dr_4_Sum"), row.Field(Of Decimal)("Cr_4_Sum"))
                dt.Rows.Add("Mei", row.Field(Of Decimal)("Dr_5_Sum"), row.Field(Of Decimal)("Cr_5_Sum"))
                dt.Rows.Add("Jun", row.Field(Of Decimal)("Dr_6_Sum"), row.Field(Of Decimal)("Cr_6_Sum"))
                dt.Rows.Add("Julai", row.Field(Of Decimal)("Dr_7_Sum"), row.Field(Of Decimal)("Cr_7_Sum"))
                dt.Rows.Add("Ogos", row.Field(Of Decimal)("Dr_8_Sum"), row.Field(Of Decimal)("Cr_8_Sum"))
                dt.Rows.Add("September", row.Field(Of Decimal)("Dr_9_Sum"), row.Field(Of Decimal)("Cr_9_Sum"))
                dt.Rows.Add("Oktober", row.Field(Of Decimal)("Dr_10_Sum"), row.Field(Of Decimal)("Cr_10_Sum"))
                dt.Rows.Add("November", row.Field(Of Decimal)("Dr_11_Sum"), row.Field(Of Decimal)("Cr_11_Sum"))
                dt.Rows.Add("Disember", row.Field(Of Decimal)("Dr_12_Sum"), row.Field(Of Decimal)("Cr_12_Sum"))
                'dt.Rows.Add("Jumlah", row("Dr_13"), row("Cr_13"))

                ' Calculate the total sum for the year by summing the values directly
                totalDr = row.Field(Of Decimal)("Dr_1_Sum") + row.Field(Of Decimal)("Dr_2_Sum") + row.Field(Of Decimal)("Dr_3_Sum") + row.Field(Of Decimal)("Dr_4_Sum") +
                                     row.Field(Of Decimal)("Dr_5_Sum") + row.Field(Of Decimal)("Dr_6_Sum") + row.Field(Of Decimal)("Dr_7_Sum") + row.Field(Of Decimal)("Dr_8_Sum") +
                                     row.Field(Of Decimal)("Dr_9_Sum") + row.Field(Of Decimal)("Dr_10_Sum") + row.Field(Of Decimal)("Dr_11_Sum") + row.Field(Of Decimal)("Dr_12_Sum")

                totalCr = row.Field(Of Decimal)("Cr_1_Sum") + row.Field(Of Decimal)("Cr_2_Sum") + row.Field(Of Decimal)("Cr_3_Sum") + row.Field(Of Decimal)("Cr_4_Sum") +
                                     row.Field(Of Decimal)("Cr_5_Sum") + row.Field(Of Decimal)("Cr_6_Sum") + row.Field(Of Decimal)("Cr_7_Sum") + row.Field(Of Decimal)("Cr_8_Sum") +
                                     row.Field(Of Decimal)("Cr_9_Sum") + row.Field(Of Decimal)("Cr_10_Sum") + row.Field(Of Decimal)("Cr_11_Sum") + row.Field(Of Decimal)("Cr_12_Sum")

                ' Add the total sum row to the DataTable
                dt.Rows.Add("Jumlah", totalDr, totalCr)

                ' Bind the new DataTable to the GridView.
                gvPenghutang.DataSource = dt
                gvPenghutang.DataBind()

                ' plus with value from past years
                totalCr += totalCrLepas
                totalDr += totalDrLepas

            End If
        End If

        txtJumlahDebitLepas.InnerText = totalDrLepas.ToString("N2")
        txtJumlahCreditLepas.InnerText = totalCrLepas.ToString("N2")

        txtJumlahCredit.InnerText = totalCr.ToString("N2")
        txtJumlahDebit.InnerText = totalDr.ToString("N2")

        'always return positive value for JumlahBaki
        txtJumlahBaki.InnerText = (Math.Abs(totalDr - totalCr)).ToString("N2")

    End Sub
    Private Sub fBindTransaksiLepas(noAkaun As String)

        ' get current Year
        Dim currentYear As String = DateTime.Now.Year.ToString

        Dim totalDr As Decimal = 0D
        Dim totalCr As Decimal = 0D

        If noAkaun <> "" Then
            Dim strSql As String
            strSql = $"
                    SELECT 
                        Kod_Penghutang,
                        Tahun,
                        SUM(Dr_1 + Dr_2 + Dr_3 + Dr_4 + Dr_5 + Dr_6 + Dr_7 + Dr_8 + Dr_9 + Dr_10 + Dr_11 + Dr_12 + Dr_13) AS Dr_Total,
                        SUM(Cr_1 + Cr_2 + Cr_3 + Cr_4 + Cr_5 + Cr_6 + Cr_7 + Cr_8 + Cr_9 + Cr_10 + Cr_11 + Cr_12 + Cr_13) AS Cr_Total
                    FROM SMKB_Lejar_Penghutang
                    WHERE Kod_Penghutang = '{noAkaun}' AND Tahun <> '{currentYear}'
                    GROUP BY Kod_Penghutang, Tahun;"

            Dim ds = dbconn.fSelectCommand(strSql)

            If ds.Tables(0).Rows.Count > 0 Then
                For Each row As DataRow In ds.Tables(0).Rows
                    totalDr += row.Field(Of Decimal)("Dr_Total")
                    totalCr += row.Field(Of Decimal)("Cr_Total")
                Next
            End If

            txtJumlahCreditLepas.InnerText = totalCr.ToString("N2")
            txtJumlahDebitLepas.InnerText = totalDr.ToString("N2")
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
                e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Center
                ' make the text right aligned for the second and third cells
                e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
            Else
                ' If it's not the "Jumlah" row, apply styling to the second and third cells
                e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
            End If
        End If
    End Sub
End Class