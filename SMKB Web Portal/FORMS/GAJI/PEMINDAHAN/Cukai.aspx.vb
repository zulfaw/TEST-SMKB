Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.IO
Imports System.Data.OleDb
Imports System.Runtime.Remoting.Messaging
Imports Newtonsoft.Json

Public Class Cukai
    Inherits System.Web.UI.Page
    Dim dbconn As New DBKewConn
    Dim dbconnSM As New DBSMConn
    Public dsKod As New DataSet
    Public dvKodKW As New DataView
    Dim sqlcmd As SqlCommand
    Dim sqlcon As SqlConnection
    Dim sqlread As SqlDataReader
    Dim dt As DataTable
    Dim w As StreamWriter

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'fBindGvJenis()
            Me.txtTarikh.Value = CStr(Year(Now)) + "-" + CStr(Month(Now))

        End If
    End Sub

    Protected Sub btnExport_Click(sender As Object, e As EventArgs)
        Dim strConnx As String = "Data Source=devmis12.utem.edu.my;Initial Catalog=dbKewanganV4;Persist Security Info=True;User ID=smkb;Password=Smkb@Dev2012"
        Dim DBCon As New SqlConnection(strConnx)
        Dim cmd2 = New SqlCommand
        Dim dr2 As SqlDataReader
        Dim stotalpcb As String
        Dim success As Integer
        Dim sblnthn As String
        Dim sjumrekod As String
        Dim sjumpcb As String
        Dim blnthn As DateTime = Convert.ToDateTime(Me.txtTarikh.Value.Trim())
        Dim month As String = blnthn.ToString("MM")
        Dim year As String = blnthn.ToString("yyyy")


        DBCon.Open()
        cmd2 = New SqlCommand("select sum(z.pcb) as pcb,sum(z.cp38) as cp,sum(z.bilpcb) as bilpcb,sum(z.bilcp) as bilcp,(select no_cukai from SMKB_Gaji_No_Majikan) as nocukai from (
        select sum(a.amaun) pcb,
        isnull((select sum(c.amaun) from smkb_gaji_lejar c where c.No_Staf=a.No_Staf and c.Jenis_Trans='T' and c.Kod_Trans='CP38' and c.Bulan='" & month & "' and c.Tahun='" & year & "'),0) as cp38,
        count(a.no_staf) as bilpcb, isnull((select count(c.no_staf) from smkb_gaji_lejar c where c.No_Staf=a.No_Staf and c.Jenis_Trans='T' and c.Kod_Trans='CP38' and c.Bulan='" & month & "' and c.Tahun='" & year & "'),0) as bilcp
        from smkb_gaji_lejar a inner Join [qa11].dbstaf.dbo.ms01_peribadi_1 b on a.no_staf=b.ms01_nostaf 
        where bulan='" & month & "' and tahun ='" & year & "' and Jenis_Trans='T' group by a.no_staf)z", DBCon)
        dr2 = cmd2.ExecuteReader
        While dr2.Read()

            sjumrekod = dr2(2)


            Me.jumrekod.Text = sjumrekod.ToString()


            stotalpcb = dr2(0).ToString()
            Me.jumpcb.Text = stotalpcb.ToString()

        End While
        dr2.Close()

        eksport()
    End Sub
    Function GetMonth(bln)
        If bln < 10 Then
            GetMonth = "0" & bln
        Else
            GetMonth = bln
        End If
    End Function
    Private Sub eksport()
        Dim strConnx As String = "Data Source=devmis12.utem.edu.my;Initial Catalog=dbKewanganV4;Persist Security Info=True;User ID=smkb;Password=Smkb@Dev2012"
        Dim DBCon As New SqlConnection(strConnx)
        Dim cmd = New SqlCommand
        Dim cmd1 = New SqlCommand

        Dim dr, dr1, dr2 As SqlDataReader
        Dim nama, nocukai, kodisteri, act_cukai, nostaf, negara, kplama, kpbaru, nopasport, jumpcb As String
        Dim stotalpcb As String
        Dim success As Integer
        Dim sblnthn As String
        Dim sjumrekod As String
        Dim sjumpcb As String
        Dim strWrite As String
        'Open the connection.

        Dim db = New DBKewConn
        Dim thngj As String = ""
        Dim blngj As String = ""
        Dim blnsblm As Integer = 0
        Dim thnnsblm As Integer = 0
        Dim filePath As String = ""

        success = 0
        sblnthn = Me.txtTarikh.Value
        If sblnthn = "" Then
            lblModalMessaage.Text = "Sila masukkan pilihan bulan dan tahun!"
            ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
            Exit Sub
        End If

        Dim blnthn As DateTime = Convert.ToDateTime(Me.txtTarikh.Value.Trim())
        Dim month As String = blnthn.ToString("MM")
        Dim year As String = blnthn.ToString("yyyy")

        Dim yrmth As String = Me.txtTarikh.Value



        If Len(year) >= 4 Then
            year = Right(year, 4)
        Else
            year = year.PadLeft(4, "0"c)
        End If

        Dim strSql = "select count(*) as bil from smkb_gaji_lejar  where (Jenis_Trans='T' or Kod_Trans='CP38') and Bulan='" & month & "' and Tahun='" & year & "'"
        Dim intCnt As Integer = dbconn.fSelectCount(strSql)
        If intCnt = 0 Then
            lblModalMessaage.Text = "Tiada rekod untuk diproses!"
            ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
            Exit Sub
        End If

        'ClientScript.RegisterStartupScript([GetType](), "alert", "call_loader();", True)







        'ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "Pop", "show_loader();", True)

        filePath = Server.MapPath("~/Forms/Gaji/Pemindahan/File/Cukai.txt")

        If System.IO.File.Exists(filePath) Then
            System.IO.File.Delete(filePath)
        End If

        w = File.AppendText(filePath)
        DBCon.Open()
        cmd1 = New SqlCommand("select sum(z.pcb) as pcb,sum(z.cp38) as cp,sum(z.bilpcb) as bilpcb,sum(z.bilcp) as bilcp,(select no_cukai from SMKB_Gaji_No_Majikan) as nocukai from (
        select sum(a.amaun) pcb,
        isnull((select sum(c.amaun) from smkb_gaji_lejar c where c.No_Staf=a.No_Staf and c.Jenis_Trans='T' and c.Kod_Trans='CP38' and c.Bulan='" & month & "' and c.Tahun='" & year & "'),0) as cp38,
        count(a.no_staf) as bilpcb, isnull((select count(c.no_staf) from smkb_gaji_lejar c where c.No_Staf=a.No_Staf and c.Jenis_Trans='T' and c.Kod_Trans='CP38' and c.Bulan='" & month & "' and c.Tahun='" & year & "'),0) as bilcp
        from smkb_gaji_lejar a inner Join [qa11].dbstaf.dbo.ms01_peribadi_1 b on a.no_staf=b.ms01_nostaf 
        where bulan='" & month & "' and tahun ='" & year & "' and Jenis_Trans='T' group by a.no_staf)z", DBCon)
        dr1 = cmd1.ExecuteReader
        While dr1.Read()
            nocukai = dr1(4)
            sjumrekod = dr1(2)
            If Len(nocukai) > 0 Then
                For j = 1 To Len(nocukai)
                    If IsNumeric(Mid(nocukai, j, 1)) = True Then
                        nocukai = nocukai + Mid(nocukai, j, 1)
                    End If
                Next
            End If



            If Len(nocukai) >= 10 Then
                nocukai = Right(nocukai, 10)
            Else
                nocukai = nocukai.PadLeft(10, "0"c)
            End If
            's_header = "H" + s_act_kerajaan + s_act_kerajaan + s_year + GetMonth(s_mth) + CStr(s_sum_pcb) + CStr(s_count_pcb) + CStr(s_sum_cp38) + CStr(s_count_cp38)

            stotalpcb = dr1(0).ToString()

            stotalpcb = stotalpcb.ToString.Replace("."c, "")

            If Len(stotalpcb) >= 11 Then
                stotalpcb = Right(stotalpcb, 11)
            Else
                stotalpcb = stotalpcb.PadLeft(11, "0"c)
            End If
            strWrite = "H" + nocukai + year + stotalpcb
            ' nocukai = Trim(rs("nocukai"))
            w.WriteLine(strWrite)
        End While
        dr1.Close()

        cmd = New SqlCommand("select a.no_staf, b.ms01_nama,isnull(b.ms01_nocukai,'') as ms01_nocukai,b.ms01_kpb,b.ms01_kpl,b.ms01_nopasport,sum(a.amaun) pcb,
        isnull((select sum(c.amaun) from smkb_gaji_lejar c where c.No_Staf=a.No_Staf and c.Jenis_Trans='T' and c.Kod_Trans='CP38' and c.Bulan=8 and c.Tahun=2023),0) as cp38
        from smkb_gaji_lejar a inner Join [qa11].dbstaf.dbo.ms01_peribadi_1 b on a.no_staf=b.ms01_nostaf 
        where bulan=8 and tahun =2023 and Jenis_Trans='T' group by a.no_staf, b.ms01_nama,b.ms01_nocukai,b.ms01_kpb,b.ms01_kpl,b.ms01_nopasport", DBCon)

        dr = cmd.ExecuteReader

        While dr.Read()
            nama = dr(1)
            'If IsNull(nama) = True Or nama = "" Then
            '    s_nama_staf = ""
            'End If
            If Len(nama) >= 60 Then
                nama = Left(nama, 60)
            Else
                nama = nama + Space(60 - Len(nama))
            End If

            kplama = dr(5)
            If Len(kplama) >= 12 Then
                kplama = Left(kplama, 12)
            Else
                kplama = kplama + Space(12 - Len(kplama))
            End If


            kpbaru = dr(4)
            If Len(kpbaru) >= 12 Then
                kpbaru = Left(kpbaru, 12)
            Else
                kpbaru = kpbaru + Space(12 - Len(kpbaru))
            End If

            nopasport = dr(5)
            If Len(nopasport) >= 12 Then
                nopasport = Left(nopasport, 12)
            Else
                nopasport = nopasport + Space(12 - Len(nopasport))
            End If

            nostaf = dr(0)
            If Len(nostaf) >= 10 Then
                nostaf = Left(nostaf, 10)
            Else
                nostaf = nostaf + Space(10 - Len(nostaf))
            End If

            negara = Space(2)

            nocukai = dr(2)
            If nocukai = "" Or nocukai = "-" Then
                nocukai = ""
            End If

            kodisteri = ""
            act_cukai = ""

            If Len(nocukai) > 0 Then
                For j = 1 To Len(nocukai)
                    If IsNumeric(Mid(nocukai, j, 1)) = True Then
                        act_cukai = act_cukai + Mid(nocukai, j, 1)
                    ElseIf InStr(Mid(nocukai, j, 1), "(") <> 0 Then
                        kodisteri = Mid(nocukai, j + 1, 1)
                        Exit For
                    End If
                Next
            End If

            If Len(act_cukai) >= 10 Then
                act_cukai = Right(act_cukai, 10)
            Else
                act_cukai = act_cukai.PadLeft(10, "0"c)
            End If

            kodisteri = "0"

            jumpcb = dr(6).ToString()

            sjumpcb = jumpcb.ToString.Replace("."c, "")

            'If Len(sjumpcb) >= 9 Then
            '    sjumpcb = Right(sjumpcb, 9)
            'Else
            '    sjumpcb = 9 - Len(sjumpcb), "0") & CStr(sjumpcb)
            'End If

            strWrite = "D" + act_cukai + kodisteri + nama + kplama + kpbaru + nopasport + negara + sjumpcb + nostaf
            ' nocukai = Trim(rs("nocukai"))
            w.WriteLine(strWrite)

            success = 1
        End While
        dr.Close()
        'If success = 1 Then
        '    lblModalMessaage.Text = "Data selesai dieksport."
        '    ClientScript.RegisterStartupScript([GetType](), "alert", "close_loader();", True)
        'End If

        ScriptManager.RegisterClientScriptBlock(Me.Page, GetType(String), "MyJSFunction", "close_loader();", True)

        w.Close()

        'jumrekod.Text = "10101"
        Dim response As System.Web.HttpResponse = System.Web.HttpContext.Current.Response
        response.ClearContent()
        response.Clear()
        response.ContentType = "text/plain"
        response.AddHeader("Content-Disposition", "attachment; filename=Cukai.txt;")
        response.TransmitFile(filePath)
        response.Flush()
        response.[End]()

        DBCon.Close()
        DBCon.Dispose()

    End Sub
End Class