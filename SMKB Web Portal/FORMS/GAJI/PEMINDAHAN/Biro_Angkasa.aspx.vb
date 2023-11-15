Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.IO
Imports System.Data.OleDb
Imports System.Runtime.Remoting.Messaging
Imports Newtonsoft.Json
Imports System
Imports System.Security.Policy

Public Class Biro_Angkasa
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

    End Sub
    Protected Sub btnExport_Click(sender As Object, e As EventArgs)
        Dim strConnx As String = "Data Source=devmis12.utem.edu.my;Initial Catalog=dbKewanganV4;Persist Security Info=True;User ID=smkb;Password=Smkb@Dev2012"
        Dim DBCon As New SqlConnection(strConnx)
        Dim cmd = New SqlCommand
        Dim cmd1 = New SqlCommand
        Dim cmd2 = New SqlCommand
        Dim dr, dr1, dr2 As SqlDataReader
        Dim nama, nocukai, kodisteri, act_cukai, nostaf, negara, kplama, kpbaru, nopasport, jumpcb, jumcp As String
        Dim stotalpcb As String
        Dim success As Integer
        Dim sblnthn, sbulan, stahun As String
        Dim sjumrekod As String
        Dim sjumpcb As String
        Dim strWrite As String
        Dim ls_firstKPL, S_hash As String
        Dim HASH, jumhash As Long
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



        If Len(year) >= 4 Then
            year = Right(year, 4)
        Else
            year = year.PadLeft(4, "0"c)
        End If

        Dim strSql = "select count(*) as bil from smkb_gaji_lejar  where Kod_Trans='TH01' and Bulan='" & month & "' and Tahun='" & year & "'"
        Dim intCnt As Integer = dbconn.fSelectCount(strSql)
        If intCnt = 0 Then
            lblModalMessaage.Text = "Tiada rekod untuk diproses!"
            ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
            Exit Sub
        End If

        'ClientScript.RegisterStartupScript([GetType](), "alert", "call_loader();", True)


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

        'ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "Pop", "show_loader();", True)

        filePath = Server.MapPath("~/Forms/Gaji/Pemindahan/File/Tabung_Haji.txt")

        If System.IO.File.Exists(filePath) Then
            System.IO.File.Delete(filePath)
        End If

        w = File.AppendText(filePath)

        strWrite = "H1UTEM010801SW0030" + month + year

        w.WriteLine(strWrite)

        cmd = New SqlCommand("select distinct a.no_staf, isnull(b.ms01_nama,'') ms01_nama,isnull(c.No_Trans,'') No_Trans,b.ms01_kpb,b.ms01_kpl,b.ms01_nopasport,a.amaun amaun
        from smkb_gaji_lejar a inner Join [qa11].dbstaf.dbo.ms01_peribadi_1 b on a.no_staf=b.ms01_nostaf inner join
		SMKB_Gaji_Master c on a.No_Staf = c.No_Staf and a.Kod_Trans = c.Kod_Trans
        where bulan=8 and tahun =2023 and a.Kod_Trans='TH01'", DBCon)

        dr = cmd.ExecuteReader

        While dr.Read()
            nama = dr(1)

            If Len(nama) >= 40 Then
                nama = Left(nama, 40)
            Else
                nama = nama + Space(40 - Len(nama))
            End If

            kplama = dr(5)
            If Len(kplama) >= 12 Then
                kplama = Left(kplama, 12)
            Else
                kplama = kplama + Space(12 - Len(kplama))
            End If


            kpbaru = dr(4).ToString.Trim()

            If Len(kpbaru) >= 12 Then
                kpbaru = Left(kpbaru, 12)
            Else
                kpbaru = kpbaru + Space(12 - Len(kpbaru))
            End If


            If kpbaru <> "" Then
                ls_firstKPL = ""
                ls_firstKPL = Left(kpbaru, 1)
                If IsNumeric(ls_firstKPL) = True Then
                    If Len(kpbaru) >= 14 Then
                        kpbaru = Right(kpbaru, 14)
                    Else
                        kpbaru = kpbaru + Space(14 - Len(kpbaru))
                    End If
                Else
                    If Len(kpbaru) > 1 Then
                        kpbaru = Mid(kpbaru, 2)
                        If Len(kpbaru) >= 14 Then
                            kpbaru = ls_firstKPL + Right(kpbaru, 14)
                        Else
                            kpbaru = ls_firstKPL + Space(14 - Len(kpbaru)) + kpbaru
                        End If
                    Else
                        kpbaru = ls_firstKPL + Space(14)
                    End If
                End If
            Else
                kpbaru = " " + Space(12)
            End If

            nopasport = dr(5)
            If Len(nopasport) >= 12 Then
                nopasport = Left(nopasport, 12)
            Else
                nopasport = nopasport + Space(12 - Len(nopasport))
            End If

            nostaf = dr(0)
            If Len(nostaf) >= 20 Then
                nostaf = Left(nostaf, 20)
            Else
                nostaf = nostaf + Space(20 - Len(nostaf))
            End If

            negara = Space(2)

            nocukai = dr(2)
            If nocukai = "" Or nocukai = "-" Then
                nocukai = ""
                HASH = 0
            Else
                HASH = CLng(Right(nocukai, 4))
            End If
            jumhash = jumhash + HASH
            kodisteri = ""
            act_cukai = ""

            If Len(nocukai) >= 15 Then
                nocukai = Right(nocukai, 15)
            Else
                nocukai = nocukai.PadLeft(15, "0"c)
            End If

            kodisteri = "0"

            jumpcb = dr(6).ToString()

            sjumpcb = jumpcb.ToString.Replace("."c, "")

            If Len(sjumpcb) >= 11 Then
                sjumpcb = Right(sjumpcb, 11)
            Else
                sjumpcb = sjumpcb.PadRight(11, "0"c)
            End If

            strWrite = "0" + nocukai + kpbaru + nama + sjumpcb + nostaf

            ' nocukai = Trim(rs("nocukai"))
            w.WriteLine(strWrite)

            success = 1
        End While
        dr.Close()

        cmd1 = New SqlCommand("select distinct sum(a.amaun) jumth,count(a.no_staf) as jumstaf 
        from smkb_gaji_lejar a inner Join [qa11].dbstaf.dbo.ms01_peribadi_1 b on a.no_staf=b.ms01_nostaf 
        where bulan='8' and tahun ='2023' and Kod_Trans='TH01'", DBCon)
        dr1 = cmd1.ExecuteReader
        While dr1.Read()

            sjumrekod = dr1(1)

            If Len(sjumrekod) >= 6 Then
                sjumrekod = Right(sjumrekod, 6)
            Else
                sjumrekod = sjumrekod.PadLeft(6, "0"c)
            End If

            's_header = "H" + s_act_kerajaan + s_act_kerajaan + s_year + GetMonth(s_mth) + CStr(s_sum_pcb) + CStr(s_count_pcb) + CStr(s_sum_cp38) + CStr(s_count_cp38)

            stotalpcb = dr1(0).ToString()

            stotalpcb = stotalpcb.ToString.Replace("."c, "")

            If Len(stotalpcb) >= 13 Then
                stotalpcb = Right(stotalpcb, 13)
            Else
                stotalpcb = stotalpcb.PadLeft(13, "0"c)
            End If

            S_hash = CStr(jumhash)
            If Len(S_hash) >= 20 Then
                S_hash = Left(S_hash, 20)
            Else
                S_hash = S_hash.PadLeft(20, "0"c)
            End If

            strWrite = "FF" + sjumrekod + stotalpcb + S_hash
            ' nocukai = Trim(rs("nocukai"))
            w.WriteLine(strWrite)
        End While
        dr1.Close()

        'If success = 1 Then
        '    lblModalMessaage.Text = "Data selesai dieksport."
        '    ClientScript.RegisterStartupScript([GetType](), "alert", "close_loader();", True)
        'End If

        ScriptManager.RegisterClientScriptBlock(Me.Page, GetType(String), "MyJSFunction", "close_loader();", True)

        w.Close()


        Dim response As System.Web.HttpResponse = System.Web.HttpContext.Current.Response
        response.ClearContent()
        response.Clear()
        response.ContentType = "text/plain"
        response.AddHeader("Content-Disposition", "attachment; filename=Tabung_Haji.txt;")
        response.TransmitFile(filePath)
        response.Flush()
        response.[End]()

        DBCon.Close()
        DBCon.Dispose()


    End Sub
    Function GetMonth(bln)
        If bln < 10 Then
            GetMonth = "0" & bln
        Else
            GetMonth = bln
        End If
    End Function
End Class