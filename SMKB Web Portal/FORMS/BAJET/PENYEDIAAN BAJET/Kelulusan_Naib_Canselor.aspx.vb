Imports System.Drawing

Public Class Kelulusan_Naib_Canselor
    Inherits System.Web.UI.Page
    Dim decTotPerbelanjaan2016 As Decimal = 0.00
    Dim decTotPeruntukan2017 As Decimal = 0.00
    Dim decTotAnggaran2018PTj As Decimal = 0.00
    Dim decTotAnggaran2018Bendahari As Decimal = 0.00
    Dim decTotAnggaran2018NC As Decimal = 0.00
    Private Shared dvButiran As New DataView
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("LoggedIn") Then
                fBindGvRingkasan()
                fBindGvPermohonan()

            Else
                Response.Redirect("../../Logout.aspx")
            End If
        End If
    End Sub

    Private Sub gvRingkasan_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvRingkasan.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim decPerbelanjaan2016 As Decimal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Perbelanjaan2016").ToString())
                Dim decPeruntukan2017 As Decimal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Peruntukan2017").ToString())
                Dim decAnggaran2018PTj As Decimal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Anggaran2018PTj").ToString())
                Dim decAnggaran2018Bendahari As Decimal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Anggaran2018Bendahari").ToString())
                Dim decAnggaran2018NC As Decimal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Anggaran2018NC").ToString())
                decTotPerbelanjaan2016 += decPerbelanjaan2016
                decTotPeruntukan2017 += decPeruntukan2017
                decTotAnggaran2018PTj += decAnggaran2018PTj
                decTotAnggaran2018Bendahari += decAnggaran2018Bendahari
                decTotAnggaran2018NC += decAnggaran2018NC

            ElseIf e.Row.RowType = DataControlRowType.Footer Then

                e.Row.Cells(0).Text = decTotPerbelanjaan2016.ToString("#,##0.00")
                e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(0).Font.Bold = True
                e.Row.Cells(1).Text = decTotPeruntukan2017.ToString("#,##0.00")
                e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(1).Font.Bold = True
                e.Row.Cells(2).Text = decTotAnggaran2018PTj.ToString("#,##0.00")
                e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(2).Font.Bold = True
                e.Row.Cells(3).Text = decTotAnggaran2018Bendahari.ToString("#,##0.00")
                e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(3).Font.Bold = True
                e.Row.Cells(4).Text = decTotAnggaran2018NC.ToString("#,##0.00")
                e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(4).Font.Bold = True
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Function fBindGvRingkasan()
        Try
            Dim dt As New DataTable
            dt.Columns.Add("Objek", GetType(String))
            dt.Columns.Add("Deskripsi", GetType(String))
            dt.Columns.Add("Perbelanjaan2016", GetType(String))
            dt.Columns.Add("Peruntukan2017", GetType(String))
            dt.Columns.Add("Anggaran2018PTj", GetType(String))
            dt.Columns.Add("Anggaran2018Bendahari", GetType(String))
            dt.Columns.Add("Anggaran2018NC", GetType(String))

            dt.Rows.Add("10000", "EMOLUMEN", "0.00", "0.00", "0.00", "0.00", "0.00")
            dt.Rows.Add("11000", "Gaji dan Upahan", "0.00", "0.00", "0.00", "0.00", "0.00")
            dt.Rows.Add("12000", "Elaun Tetap", "0.00", "0.00", "0.00", "0.00", "0.00")

            dt.Rows.Add("20000", "PERKHIDMATAN DAN BEKALAN", "8,154,968.19", "8,377,000.00", "8,957,104.43", "8,600,000", "8,600,000")
            dt.Rows.Add("21000", "Perbelanjaan Perjalanan dan Sara Hidup", "8,154,968.19", "8,377,000.00", "8,957,104.43", "8,600,000", "8,600,000")
            dt.Rows.Add("22000", "Pengangkutan barang", "8,154,968.19", "8,377,000.00", "8,957,104.43", "8,600,000", "8,600,000")
            dt.Rows.Add("23000", "Perhubungan dan Utiliti", "8,154,968.19", "8,377,000.00", "8,957,104.43", "8,600,000", "8,600,000")
            dt.Rows.Add("24000", "Sewaan", "8,154,968.19", "8,377,000.00", "8,957,104.43", "8,600,000", "8,600,000")

            dt.Rows.Add("30000", "ASET", "250,452.00", "35,586.00", "40,000.00", "40,000.00", "40,000.00")
            dt.Rows.Add("31000", "Tanah dan pembaikan Tanah", "250,452.00", "35,586.00", "40,000.00", "40,000.00", "40,000.00")
            dt.Rows.Add("32000", "Bangunan & Pembaikan Bangunan", "250,452.00", "35,586.00", "40,000.00", "40,000.00", "40,000.00")

            dt.Rows.Add("40000", "PEMBERIAN & BAYARAN TETAP", "0.00", "0.00", "0.00", "0.00", "0.00")
            dt.Rows.Add("41000", "Biasiswa dan Bantuan pelajaran", "0.00", "0.00", "0.00", "0.00", "0.00")

            dt.Rows.Add("50000", "PERBELANJAAN LAIN", "0.00", "0.00", "0.00", "0.00", "0.00")
            dt.Rows.Add("51000", "Pulangan Balik & Hapus Kira", "0.00", "0.00", "0.00", "0.00", "0.00")

            gvRingkasan.DataSource = dt
            gvRingkasan.DataBind()
        Catch ex As Exception

        End Try
    End Function

    Private Sub gvRingkasan_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvRingkasan.RowCreated
        Try
            If e.Row.RowType = DataControlRowType.Footer Then
                ' First cell is used for specifying the Total text
                Dim intNoOfMergeCol As Integer = 3
                'except last column 
                For intCellCol As Integer = 1 To intNoOfMergeCol - 1
                    e.Row.Cells.RemoveAt(1)
                Next
                e.Row.Cells(0).ColumnSpan = intNoOfMergeCol
                e.Row.Cells(0).Text = "Jumlah Besar (RM)"
                e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(0).Font.Bold = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Function fBindGvPermohonan()
        Try
            Dim dt As New DataTable
            dt.Columns.Add("Bil", GetType(Integer))
            dt.Columns.Add("NoPermohonan", GetType(String))
            dt.Columns.Add("PTj", GetType(String))
            dt.Columns.Add("Anggaran", GetType(String))

            dt.Rows.Add(1, "BG20174101", "41 - PUSAT PERKHIDMATAN PENGETAHUAN DAN KOMUNIKASI", "1,500,00")
            dt.Rows.Add(2, "BG20170301", "03 - PEJABAT BENDAHARI", "2,300,00")
            dt.Rows.Add(3, "BG20170201", "02 - PEJABAT PENDAFTAR", "1,800,00")
            dt.Rows.Add(4, "BG20171001", "10 - PENERBIT UNIVERSITI", "3,100,00")
            dt.Rows.Add(4, "BG20170501", "05 - PEJABAT PEMBANGUNAN ", "25,000,00")

            gvPermohonan.DataSource = dt
            gvPermohonan.DataBind()
        Catch ex As Exception

        End Try
    End Function

    Private Sub gvPermohonan_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvPermohonan.RowDataBound
        Try

            If e.Row.RowType = DataControlRowType.DataRow Then

                'e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gvPermohonan, "Select$" & e.Row.RowIndex)
                'e.Row.ToolTip = "Klik untuk pilih rekod ini."
                'e.Row.ForeColor = ColorTranslator.FromHtml("#000000")

                'Dim KodModul As String = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "KodModul"))
                'Dim gvChild As GridView = DirectCast(e.Row.FindControl("nestedGridView"), GridView)
                'Dim gvChildSource As New SqlDataSource()
                'gvChildSource.ConnectionString = "Server=devmis12.utem.edu.my;Database=DbKewangan;Uid=smkb;Pwd=Smkb@Dev2012;Pooling=False;"
                'gvChildSource.SelectCommand = "select KodSub,NamaSub from MK_USubModul  where KodModul = '" + KodModul + "'"
                'gvChild.DataSource = gvChildSource
                'gvChild.DataBind()

                Dim strNoPermohonan As String = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "NoPermohonan"))
                Dim gvChild As GridView = DirectCast(e.Row.FindControl("nestedGridView"), GridView)

                Dim dt As New DataTable
                dt.Columns.Add("Bil", GetType(Integer))
                dt.Columns.Add("NoPermohonan", GetType(String))
                dt.Columns.Add("Bahagian", GetType(String))
                dt.Columns.Add("Unit", GetType(String))
                dt.Columns.Add("KO", GetType(String))
                dt.Columns.Add("Anggaran", GetType(String))
                dt.Columns.Add("Justifikasi", GetType(String))

                If strNoPermohonan = "BG20174101" Then
                    dt.Rows.Add(1, "BG2017410102", "01 - Infostruktur", "02 - Sistem Maklumat Kewangan", "00", "700,000", "")
                    dt.Rows.Add(2, "BG2017410202", "02 - Rangkaian", "02 - Pusat Data", "00", "600,000", "")

                ElseIf strNoPermohonan = "BG20170301" Then
                    dt.Rows.Add(1, "BG2017030102", "01 - Bahagian A", "02 - Unit A", "00", "1,500,00", "")
                    dt.Rows.Add(2, "BG2017030202", "02 - Bahagian B", "02 - Unit B", "00", "800,000", "")

                ElseIf strNoPermohonan = "BG20170201" Then
                    dt.Rows.Add(1, "BG2017020102", "01 - Bahagian A", "02 - Unit A", "00", "900,000", "")
                    dt.Rows.Add(2, "BG2017020203", "02 - Bahagian B", "03 - Unit B", "00", "900,000", "")

                ElseIf strNoPermohonan = "BG20171001" Then
                    dt.Rows.Add(1, "BG2017100401", "04 - Bahagian A", "01 - Unit A", "00", "600,000", "")
                    dt.Rows.Add(2, "BG2017100402", "04 - Bahagian A", "02 - Unit B", "00", "1,200,000", "")
                    dt.Rows.Add(3, "BG2017100602", "06 - Bahagian C", "02 - Unit B", "00", "1,300,000", "")

                ElseIf strNoPermohonan = "BG20170501" Then
                    dt.Rows.Add(1, "BG2017050102", "01 - Bahagian A", "02 - Unit B", "00", "15,000,000", "")
                    dt.Rows.Add(2, "BG2017050201", "02 - Bahagian B", "01 - Unit A", "00", "10,000,000", "")
                End If

                gvChild.DataSource = dt
                gvChild.DataBind()

            End If
        Catch ex As Exception

        End Try
    End Sub





End Class