Imports System
Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Data
Imports System.Globalization
Imports System.IO
Imports System.Collections
Imports System.Web.HttpFileCollection

Public Class CetakPenyataLanjutAkaunPenghutang
    Inherits System.Web.UI.Page
    Dim dbconn As New DBKewConn
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim noAkaun As String = Request.QueryString("noAkaun")
        Dim bulan As String = Request.QueryString("month") 'format 01-12
        Dim tahun As String = Request.QueryString("year")

        'make month array
        Dim monthArray() As String = {"Januari", "Februari", "Mac", "April", "Mei", "Jun", "Julai", "Ogos", "September", "Oktober", "November", "Disember"}

        'get month name
        Dim bulanName As String = monthArray(Convert.ToInt32(bulan) - 1)

        txtBulan.InnerText = bulanName
        txtTahun.InnerText = tahun

        creditBulan.InnerText = bulanName
        debitBulan.InnerText = bulanName

        If noAkaun <> "" And bulan <> "" And tahun <> "" Then
            fBindTransaksi(noAkaun, bulan, tahun)
        End If
    End Sub

    Private Sub fBindTransaksi(noAkaun As String, bulan As String, tahun As String)
        If noAkaun <> "" Then
            Dim strSql As String
            strSql = $"
                    SELECT
                        CONVERT(varchar, a.Tkh_Mohon, 103) as tarikh, a.Tujuan as tujuan, a.Jumlah as jumlah,
                            b.Nama_Penghutang, b.Tel_Bimbit, b.Alamat_1, b.Alamat_2, b.Poskod, 
                            d1.Butiran AS ButiranBandar, b.Bandar, 
                            d2.Butiran AS ButiranKodNegeri, b.Kod_Negeri,
	                        d3.Butiran AS ButiranKodNegara, b.Kod_Negara
                        FROM SMKB_Bil_Hdr a
                        INNER JOIN SMKB_Penghutang_Master b ON a.Kod_Penghutang = b.Kod_Penghutang
                        LEFT JOIN (
                            SELECT Kod_Detail, Butiran
                            FROM SMKB_Lookup_Detail
                            WHERE Kod = '0003'
                        ) d1 ON b.Bandar = d1.Kod_Detail
                        LEFT JOIN (
                            SELECT Kod_Detail, Butiran
                            FROM SMKB_Lookup_Detail
                            WHERE Kod = '0002'
                        ) d2 ON b.Kod_Negeri = d2.Kod_Detail
                        LEFT JOIN (
                            SELECT Kod_Detail, Butiran
                            FROM SMKB_Lookup_Detail
                            WHERE Kod = '0001'
                        ) d3 ON b.Kod_Negara = d3.Kod_Detail
						WHERE a.Kod_Status_Dok = '03'
                        AND MONTH(a.Tkh_Mohon)='{bulan}'
                        AND YEAR(a.Tkh_Mohon)='{tahun}'
                        AND a.Kod_Penghutang='{noAkaun}'"

            Dim ds = dbconn.fSelectCommand(strSql)

            If ds.Tables(0).Rows.Count > 0 Then
                txtNama.InnerText = ds.Tables(0).Rows(0)("Nama_Penghutang").ToString
                txtAlamat1.InnerText = ds.Tables(0).Rows(0)("Alamat_1").ToString
                txtAlamat2.InnerText = ds.Tables(0).Rows(0)("Alamat_2").ToString
                txtAlamat3.InnerText = ds.Tables(0).Rows(0)("Poskod").ToString + ", " + ds.Tables(0).Rows(0)("ButiranBandar").ToString
                txtAlamat4.InnerText = ds.Tables(0).Rows(0)("ButiranKodNegeri").ToString + ", " + ds.Tables(0).Rows(0)("ButiranKodNegara").ToString

                ' Create a new DataTable to store the monthly Debit (Dr) and Credit (Cr) data.
                Dim dt As New DataTable()
                dt.Columns.Add("Tarikh", GetType(String))
                dt.Columns.Add("Tujuan", GetType(String))
                dt.Columns.Add("Jumlah", GetType(Decimal))

                Dim total As Decimal = 0D

                ' loop through the rows of the SQL result and add monthly data to the new DataTable.
                For Each row As DataRow In ds.Tables(0).Rows
                    dt.Rows.Add(row.Field(Of String)("tarikh"), row.Field(Of String)("tujuan"), row.Field(Of Decimal)("jumlah"))
                    total += row.Field(Of Decimal)("jumlah")
                Next

                ' Bind the new DataTable to the GridView.
                gvPenghutang.DataSource = dt
                gvPenghutang.DataBind()

                txtJumlahCredit.InnerText = total.ToString("N2")
                txtJumlahDebit.InnerText = total.ToString("N2")

            End If
        End If
    End Sub
    Protected Sub gvPenghutang_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        ' Check if the row is a DataRow (not header/footer, etc.)
        If e.Row.RowType = DataControlRowType.DataRow Then

            e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
        End If
    End Sub
    Private Function GetBandarValueFromKod(kod As String) As DataTable
        Dim db = New DBKewConn
        Dim query As String = "SELECT Kod_Detail as value, Butiran as text FROM SMKB_Lookup_Detail WHERE Kod='0003'"
        Dim param As New List(Of SqlParameter)
        If (kod <> "") Then
            query = query & " AND Kod_Detail=@kod"
            param.Add(New SqlParameter("@kod", kod))
        End If
        Return db.Read(query, param)
    End Function
End Class