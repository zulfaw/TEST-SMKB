Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Public Class Pindah_Bajet
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                gvPindahBajet.DataSource = New List(Of String)
                gvPindahBajet.DataBind()

                fBindDdlTahun()
                If fCheckSetupKW Then
                    fBindDddlKW()
                    alert.Visible = False
                Else
                    alert.Visible = True
                    'fGlobalAlert("Sila pastikan kaedah proses bawa bajet telah ditetapkan!", Me.Page, Me.[GetType]())
                End If

                sClearDdlPTJ()

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub sClearDdlPTJ()
        Try
            ddlPTJ.Items.Clear()
            ddlPTJ.Items.Insert(0, New ListItem(" - SILA PILIH PTj -", 0))
            ddlPTJ.SelectedIndex = 0
        Catch ex As Exception

        End Try
    End Sub

    Private Function fCheckSetupKW() As Boolean
        Try
            Dim strSql As String = "select count(*) as recCount from mk08_pindahanbajet where mk08_tahun = '" & Trim(ddlTahun.SelectedValue.TrimEnd) & "'"
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fselectCommand(strSql)
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    Dim intCount As Integer = CInt(ds.Tables(0).Rows(0)("recCount"))
                    If intCount > 0 Then
                        Return True
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Sub fBindDdlTahun()

        Try
            Dim yNow As DateTime = Now.AddYears(-1)
            Dim pYear As String = yNow.Year
            ddlTahun.Items.Add(pYear)
            ddlTahun.Items.Add(Now.Year)

            ddlKW.Items.Insert(0, New ListItem("-SILA PILIH-", "0"))
            ddlKW.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub
    Private Sub fBindDddlKW()

        Try
            Dim strSql As String = "select a.kodkw,(a.kodkw + ' - ' + b.butiran) as ButiranKW from MK08_PindahanBajet a,MK_Kw b Where a.kodkw=b.kodkw and mk08_tahun = '" & Trim(ddlTahun.SelectedValue.TrimEnd) & "' and MK08_Cara ='BAKI PERUNTUKAN' order by a.kodkw asc"
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fselectCommand(strSql)

            ddlKW.DataSource = ds
            ddlKW.DataTextField = "ButiranKW"
            ddlKW.DataValueField = "KodKw"
            ddlKW.DataBind()

            ddlKW.Items.Insert(0, New ListItem("-SILA PILIH-", "0"))
            ddlKW.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Private Sub fBindDddlPTj()
        Try
            Dim strSql As String = "select distinct(a.kodptj) as kodptj,(a.kodptj + ' - '+ b.butiran)   as butiranPTj From mk01_vottahun a,mk_ptj b " &
            "Where a.KodPtj = b.KodPtj And a.mk01_tahun='" & ddlTahun.SelectedValue & "' and a.kodkw='" & ddlKW.SelectedValue & "' and substring(a.kodvot,2,1) <> 0 and a.kodvot >= '10000' and a.kodvot < '60000' and b.status =1 and b.kodkategoriptj <> '-'"
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fselectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    ddlPTJ.DataSource = ds
                    ddlPTJ.DataTextField = "butiranPTj"
                    ddlPTJ.DataValueField = "KodPTJ"
                    ddlPTJ.DataBind()

                    ddlPTJ.Items.Insert(0, New ListItem("KESELURUHAN", "000000"))
                    ddlPTJ.SelectedIndex = 0
                End If
            End If



        Catch ex As Exception

        End Try
    End Sub

    Private Sub ddlKW_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKW.SelectedIndexChanged
        Try
            fBindDddlPTj()
        Catch ex As Exception

        End Try
    End Sub



    Private Sub fBindGv(ByVal strKodPTj As String)
        Try
            Dim blnFound As Boolean = False
            Dim strtahun As String = Trim(ddlTahun.SelectedValue.TrimEnd)
            Dim strTkhHingga As String = "31/12/" & strtahun
            Dim intRec As Integer

            Dim dbConn As New DBKewConn
            Dim strFilter As String

            If strKodPTj <> "000000" Then
                strFilter = " and a.kodptj= '" & Trim(ddlPTJ.SelectedValue.TrimEnd) & "'"
            End If

            Dim strSql As String = "select a.KodKW, a.KodKO ,a.KodPTj, a.KodKP ,a.KodVot, a.mk01_bakiperutk " &
         "From mk01_vottahun a, mk_ptj b " &
         "Where a.KodPtj = b.KodPtj And a.mk01_tahun='" & ddlTahun.SelectedValue & "' And a.kodkw='" & ddlKW.SelectedValue & "' " & strFilter & " And substring(a.kodvot,2,1) <> 0 and a.kodvot >= '10000' " &
         "And a.kodvot < '60000' And b.status =1 and b.kodkategoriptj <> '-' " &
         "order by a.KodKW, a.KodKO ,a.KodPTj, a.KodKP ,a.KodVot"

            Dim ds As New DataSet
            ds = dbConn.fselectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    blnFound = True
                End If
            End If

            If blnFound = True Then

                Dim dt As New DataTable
                dt.Columns.Add("kodKW", GetType(String))
                dt.Columns.Add("kodKO", GetType(String))
                dt.Columns.Add("kodPTj", GetType(String))
                dt.Columns.Add("kodKP", GetType(String))
                dt.Columns.Add("Kodvot", GetType(String))
                dt.Columns.Add("Baki", GetType(String))
                dt.Columns.Add("Bf", GetType(String))

                Dim strKW, strKO, strPTj, strKP, strKodvot As String
                Dim decBakiPerutk, decBakiSbnr As Decimal
                Dim strBakiPerutk, strBakiSbnr As String

                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    strKW = ds.Tables(0).Rows(i)("KodKW")
                    strKO = ds.Tables(0).Rows(i)("KodKO")
                    strPTj = ds.Tables(0).Rows(i)("KodPTj")
                    strKP = ds.Tables(0).Rows(i)("KodKP")
                    strKodvot = ds.Tables(0).Rows(i)("KodVot")

                    decBakiPerutk = CDec(ds.Tables(0).Rows(i)("mk01_bakiperutk"))
                    strBakiPerutk = decBakiPerutk.ToString("#,##0.00")

                    decBakiSbnr = CDec(fGetBakiSebenar(strtahun, strTkhHingga, strKW, strKO, strPTj, strKP, strKodvot))
                    strBakiSbnr = decBakiSbnr.ToString("#,##0.00")

                    dt.Rows.Add(strKW, strKO, strPTj, strKP, strKodvot, strBakiPerutk, strBakiSbnr)
                Next
                gvPindahBajet.DataSource = dt
                gvPindahBajet.DataBind()

                intRec = ds.Tables(0).Rows.Count
                ViewState("dtBajet") = dt

            Else
                fGlobalAlert("Tiada Rekod Bajet!", Me.Page, Me.[GetType]())
                intRec = 0
            End If

            lblJumRekod.InnerText = intRec

        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvPindahBajet_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvPindahBajet.PageIndexChanging
        Try

            gvPindahBajet.PageIndex = e.NewPageIndex
            If Session("SortedView") IsNot Nothing Then
                gvPindahBajet.DataSource = Session("SortedView")
                gvPindahBajet.DataBind()
            Else
                fBindGv(Trim(ddlPTJ.SelectedValue.TrimEnd))
            End If

        Catch ex As Exception

        End Try
    End Sub



    Private Sub lbtnPapar_Click(sender As Object, e As EventArgs) Handles lbtnPapar.Click
        Try
            Dim strKodPTj As String = ddlPTJ.SelectedValue
            fBindGv(strKodPTj)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lbtnSimpan_Click(sender As Object, e As EventArgs) Handles lbtnSimpan.Click
        Try
            Dim blnFound As Boolean = False
            Dim strTahun As String = Trim(ddlTahun.SelectedValue.TrimEnd)
            Dim intNxTahun As Integer = CInt(strTahun) + 1

            Dim dbconn As New DBKewConn
            Dim strSql As String = "select distinct mk01_tahun from mk01_vottahun where mk01_tahun = '" & Trim(ddlTahun.SelectedValue.TrimEnd) & "' and kodkw='" & Trim(ddlKW.SelectedValue.TrimEnd) & "' "

            Dim ds As New DataSet
            ds = dbconn.fSelectCommand(strSql)
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    blnFound = True
                End If
            End If

            If blnFound = False Then
                fGlobalAlert("Sila pastikan carta akaun telah dibina!", Me.Page, Me.[GetType]())
                Exit Sub
            End If


            Dim dt As New DataTable
            dt = CType(ViewState("dtBajet"), DataTable)

            For Each row As DataRow In dt.Rows
                Dim strKodKW As String = dt.Rows(0)("kodKW")
                Dim strKodKO As String = dt.Rows(0)("kodKO")
                Dim strKodPTj As String = dt.Rows(0)("kodPTj")
                Dim strKodKP As String = dt.Rows(0)("kodKP")
                Dim strKodvotH2 As String = dt.Rows(0)("Kodvot")
                Dim decBF As Decimal = CDec(dt.Rows(0)("Bf"))

                If decBF <> 0 Then
                    'KEMAS KINI MK01_VOTTAHUN H2
                    strSql = "SELECT MK01_Perutk,MK01_BakiSms,MK01_BakiPerutk,MK01_BakiSlpsPemLulus From MK01_VotTahun " &
                            "WHERE KodKw = '" & strKodKW & "' and KodKO = '" & strKodKO & "' And KodPTJ = '" & strKodPTj & "' and KodKP = '" & strKodKP & "' AND KodVot = '" & strKodvotH2 & "' AND MK01_Tahun = '" & intNxTahun & "'"

                    Dim ds2 As New DataSet
                    ds2 = dbconn.fSelectCommand(strSql)

                    If Not ds2 Is Nothing Then
                        If ds2.Tables(0).Rows.Count > 0 Then
                            Dim decPerutk As Decimal = CDec(ds2.Tables(0).Rows(0)("MK01_Perutk")) + decBF
                            Dim decBakiSms As Decimal = CDec(ds2.Tables(0).Rows(0)("MK01_BakiSms")) + decBF
                            Dim decBakiPerutk As Decimal = CDec(ds2.Tables(0).Rows(0)("MK01_BakiPerutk")) + decBF
                            Dim decBakiSlpsPemLulus As Decimal = CDec(ds2.Tables(0).Rows(0)("MK01_BakiSlpsPemLulus")) + decBF

                            strSql = "update MK01_VotTahun set MK01_Perutk = @Perutk, MK01_BakiSms = @BakiSms, MK01_BakiPerutk = @BakiPerutk, MK01_BakiSlpsPemLulus = @BakiSlpsPemLulus " &
             "where KodKw = @KodKW and KodKO = @KodKO And KodPTJ = @KodPTj and KodKP = @KodKP AND KodVot = @Kodvot AND MK01_Tahun = @Tahun"
                            Dim paramSql() As SqlParameter = {
                            New SqlParameter("@Perutk", decPerutk),
                            New SqlParameter("@BakiSms", decBakiSms),
                            New SqlParameter("@BakiPerutk", decBakiPerutk),
                            New SqlParameter("@BakiSlpsPemLulus", decBakiSlpsPemLulus),
                            New SqlParameter("@KodKW", strKodKW),
                            New SqlParameter("@KodKO", strKodKO),
                            New SqlParameter("@KodPTj", strKodPTj),
                            New SqlParameter("@KodKP", strKodKP),
                            New SqlParameter("@Kodvot", strKodvotH2),
                            New SqlParameter("@Tahun", intNxTahun)
                            }

                            dbconn.sConnBeginTrans()
                            If dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
                                dbconn.sConnCommitTrans()
                            Else
                                dbconn.sConnRollbackTrans()
                                Exit Sub
                            End If
                        End If
                    End If

                    'KEMAS KINI VOT TAHUN H1
                    Dim strkodvotH1 As String = Trim(strKodvotH2.TrimEnd.Substring(0, 1)) + "0000"
                    strSql = "SELECT MK01_Perutk,MK01_BakiSms,MK01_BakiPerutk,MK01_BakiSlpsPemLulus From MK01_VotTahun " &
                            "WHERE KodKw = '" & strKodKW & "' and KodKO = '" & strKodKO & "' And KodPTJ = '" & strKodPTj & "' and KodKP = '" & strKodKP & "' AND KodVot = '" & strkodvotH1 & "' AND MK01_Tahun = '" & intNxTahun & "'"

                    Dim ds3 As New DataSet
                    ds3 = dbconn.fSelectCommand(strSql)

                    If Not ds3 Is Nothing Then
                        If ds3.Tables(0).Rows.Count > 0 Then
                            Dim decPerutk As Decimal = CDec(ds3.Tables(0).Rows(0)("MK01_Perutk")) + decBF
                            Dim decBakiSms As Decimal = CDec(ds3.Tables(0).Rows(0)("MK01_BakiSms")) + decBF
                            Dim decBakiPerutk As Decimal = CDec(ds3.Tables(0).Rows(0)("MK01_BakiPerutk")) + decBF
                            Dim decBakiSlpsPemLulus As Decimal = CDec(ds3.Tables(0).Rows(0)("MK01_BakiSlpsPemLulus")) + decBF

                            strSql = "update MK01_VotTahun set MK01_Perutk = @Perutk, MK01_BakiSms = @BakiSms, MK01_BakiPerutk = @BakiPerutk, MK01_BakiSlpsPemLulus = @BakiSlpsPemLulus " &
                 "where KodKw = @KodKW and KodKO = @KodKO And KodPTJ = @KodPTj and KodKP = @KodKP AND KodVot = @Kodvot AND MK01_Tahun = @Tahun"
                            Dim paramSql() As SqlParameter = {
                                New SqlParameter("@Perutk", decPerutk),
                                New SqlParameter("@BakiSms", decBakiSms),
                                New SqlParameter("@BakiPerutk", decBakiPerutk),
                                New SqlParameter("@BakiSlpsPemLulus", decBakiSlpsPemLulus),
                                New SqlParameter("@KodKW", strKodKW),
                                New SqlParameter("@KodKO", strKodKO),
                                New SqlParameter("@KodPTj", strKodPTj),
                                New SqlParameter("@KodKP", strKodKP),
                                New SqlParameter("@Kodvot", strkodvotH1),
                                New SqlParameter("@Tahun", intNxTahun)
                                }

                            dbconn.sConnBeginTrans()
                            If dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
                                dbconn.sConnCommitTrans()
                            Else
                                dbconn.sConnRollbackTrans()
                                Exit Sub
                            End If
                        End If
                    End If

                    'INSERT MK09_BAJETBF
                    If strKodvotH2.TrimEnd.Substring(1, 4) <> "0000" Then
                        'delete existing
                        strSql = "DELETE FROM MK09_BajetBF WHERE KodKw = @KodKW and kodko = @KodKO and KodPTJ = @KodPTj and kodkp = @KodKP and KodVot = @Kodvot and MK09_Tahun = @Tahun"
                        Dim paramSql() As SqlParameter = {
                                New SqlParameter("@KodKW", strKodKW),
                                New SqlParameter("@KodKO", strKodKO),
                                New SqlParameter("@KodPTj", strKodPTj),
                                New SqlParameter("@KodKP", strKodKP),
                                New SqlParameter("@Kodvot", strKodvotH2),
                                New SqlParameter("@Tahun", intNxTahun)
                                }

                        dbconn.sConnBeginTrans()
                        If dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
                            dbconn.sConnCommitTrans()
                        Else
                            dbconn.sConnRollbackTrans()
                            Exit Sub
                        End If

                        'insert new
                        strSql = "INSERT INTO MK09_BajetBF (KodKw, KodPTJ, KodKO,KodKP, KodVot, MK09_Tahun, MK09_Debit, MK09_Kredit) " &
                                    "VALUES(@KodKW, @KodPTj, @KodKO, @KodKP, @Kodvot, @Tahun,@Debit, @Kredit)"

                        Dim paramSql2() As SqlParameter = {
                            New SqlParameter("@KodKw", strKodKW),
                            New SqlParameter("@KodPTJ", strKodPTj),
                            New SqlParameter("@KodKO", strKodKO),
                            New SqlParameter("@KodKP", strKodKP),
                            New SqlParameter("@Kodvot", strKodvotH2),
                            New SqlParameter("@Tahun", intNxTahun),
                            New SqlParameter("@Debit", decBF),
                            New SqlParameter("@Kredit", 0)}

                        dbconn.sConnBeginTrans()
                        If dbconn.fInsertCommand(strSql, paramSql2) > 0 Then
                            dbconn.sConnCommitTrans()
                        Else
                            dbconn.sConnRollbackTrans()
                        End If
                    End If
                End If
            Next


        Catch ex As Exception

        End Try
    End Sub
End Class