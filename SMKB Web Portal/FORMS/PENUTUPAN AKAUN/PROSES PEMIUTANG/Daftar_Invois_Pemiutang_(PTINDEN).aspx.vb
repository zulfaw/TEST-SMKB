Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Globalization

Public Class Daftar_Invois_Pemiutang__PTINDEN_
    Inherits System.Web.UI.Page
    '---PEMBOLEHUBAH UTK AUDITLOG---
    Dim sLogBaru As String
    Dim sLogLama As String
    Dim sLogUserID As String
    Dim sLogNostaf As String
    Dim sLogSubModul As String
    Dim sLogField As String
    Dim sLogLevel As String
    Dim sLogTable As String
    Dim sLogRef As String
    Dim sLogMedan As String
    Dim sLogRefNo As String
    Dim sLogDesc As String
    Dim sLogStatus As String
    Dim lsLogerror As String
    Dim lsLogDate As String
    Dim lsLogTime As String
    Dim lsLogUsrIP As String
    Dim lsLogUsrPC As String
    Dim RefDateTime As Date


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        RefDateTime = Now()
        If Not IsPostBack Then

            txtIdInv.Text = ""
            sBindDDlKat()
            sBindDdlJenInv()
            sBindDdlNegeri()
            sBindDdlNegara()
            sBindDdlBank()
            sClearGvTransPT()
            sClearGvTransLain()

            txtIdKat.Text = "SY"
            txtKat.Text = "SYARIKAT"

            fLoadKW()
            fLoadKO()
            fLoadPTj()
            fLoadKP()
            fLoadVot()

            sSetInv()

        End If
    End Sub

    Private Sub ddlJenInv_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlJenInv.SelectedIndexChanged
        sSetInv()
    End Sub

    Private Sub sSetInv()
        Try
            Dim strJenInv As String = Trim(ddlJenInv.SelectedValue.TrimEnd)
            Select Case strJenInv
                Case "ILL"
                    trPT.Visible = False
                    trKat.Visible = True

                    divGvPT.Visible = False
                    divGvLain.Visible = True

                    btnShowMpeNom.Visible = True

                    sInitGvTrans()
                    fAddRow()
                Case Else

                    If strJenInv = "IPT" Then
                        lblNoPt.Text = "No. PT/PB"
                        lblNoInv.Text = "No. Invois"
                        lblTkhInv.Text = "Tarikh Invois"
                        lblTTkhInv.Text = "Tarikh Terima Invois"

                    ElseIf strJenInv = "IND" Then
                        lblNoPt.Text = "No. Inden"
                        lblNoInv.Text = "No. Interim"
                        lblTkhInv.Text = "Tarikh Interim"
                        lblTTkhInv.Text = "Tarikh Terima Interim"
                    End If

                    trPT.Visible = True
                    trKat.Visible = False

                    divGvPT.Visible = True
                    divGvLain.Visible = False

                    btnShowMpeNom.Visible = False
                    sClearGvTransPT()
            End Select

            txtIdKat.Text = "SY"
            txtKat.Text = "SYARIKAT"

            txtJenByr.Text = "BAYARAN PENUH"
            hidJenByr.Value = "BP"
        Catch ex As Exception

        End Try
    End Sub

    Private Sub fCheckJenInv(ByVal strJenInv As String)
        If strJenInv = "ILL" Then
            trPT.Visible = False
            trKat.Visible = False

            divGvPT.Visible = False
            divGvLain.Visible = True
        Else
            trPT.Visible = True
            trKat.Visible = True

            divGvPT.Visible = True
            divGvLain.Visible = False
        End If
    End Sub


    Private Sub sBindDdlJenInv()
        Try
            Dim strSql As String = "select kod, (kod + ' - ' + Butiran) as Butiran  from AP_JenInv Order By Kod"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            ddlJenInv.DataSource = ds
            ddlJenInv.DataTextField = "Butiran"
            ddlJenInv.DataValueField = "kod"
            ddlJenInv.DataBind()

            ddlTapisan.DataSource = ds
            ddlTapisan.DataTextField = "Butiran"
            ddlTapisan.DataValueField = "kod"
            ddlTapisan.DataBind()

            ddlTapisan.Items.Insert(0, New ListItem("- KESELURUHAN -", "0"))
            ddlTapisan.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Private Sub sBindDDlKat()
        Try

            Dim strSql As String = "Select KodKat , Butiran  from AP_KatPenerima where KodKat In ('SY','ST','OA')"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            ddlKat.DataSource = ds
            ddlKat.DataTextField = "Butiran"
            ddlKat.DataValueField = "KodKat"
            ddlKat.DataBind()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlKW_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try

            Dim gvr As GridViewRow = CType(((CType(sender, Control)).NamingContainer), GridViewRow)
            Dim ddlKW As DropDownList = CType(gvr.FindControl("ddlKW"), DropDownList)
            Dim strKodKW As String = ddlKW.SelectedItem.Value

            'Load ddlKO
            Dim ddlKO = DirectCast(gvr.FindControl("ddlKO"), DropDownList)
            ddlKO.DataSource = fGetKO(strKodKW)   'ViewState("dsKO")
            ddlKO.DataTextField = "ButiranKO"
            ddlKO.DataValueField = "KodKO"
            ddlKO.DataBind()
            ddlKO.Items.Insert(0, New ListItem("- SILA PILIH KW -", 0))
            ddlKO.SelectedIndex = 0

            'Set ddlPTj kepada default
            Dim ddlPTj = DirectCast(gvr.FindControl("ddlPTj"), DropDownList)
            ddlPTj.Items.Clear()
            ddlPTj.Items.Insert(0, New ListItem("- SILA PILIH KO -", 0))
            ddlPTj.SelectedIndex = 0

            'Set ddlKP kepada default
            Dim ddlKP = DirectCast(gvr.FindControl("ddlKP"), DropDownList)
            ddlKP.Items.Clear()
            ddlKP.Items.Insert(0, New ListItem("- SILA PILIH PTj -", 0))
            ddlKP.SelectedIndex = 0

            'Set ddlVot kepada default
            Dim ddlVot = DirectCast(gvr.FindControl("ddlVot"), DropDownList)
            ddlVot.Items.Clear()
            ddlVot.Items.Insert(0, New ListItem("- SILA PILIH KP -", 0))
            ddlVot.SelectedIndex = 0



        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlKO_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try

            Dim gvr As GridViewRow = CType(((CType(sender, Control)).NamingContainer), GridViewRow)
            Dim ddlKW As DropDownList = CType(gvr.FindControl("ddlKW"), DropDownList)
            Dim strKodKW As String = ddlKW.SelectedItem.Value

            Dim ddlKO As DropDownList = CType(gvr.FindControl("ddlKO"), DropDownList)
            Dim strKodKO As String = ddlKO.SelectedItem.Value

            'Load ddlPTj
            Dim ddlPTj = DirectCast(gvr.FindControl("ddlPTj"), DropDownList)
            ddlPTj.DataSource = fGetPTj(strKodKW, strKodKO)
            ddlPTj.DataTextField = "ButiranPTj"
            ddlPTj.DataValueField = "KodPTj"
            ddlPTj.DataBind()
            ddlPTj.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
            ddlPTj.SelectedIndex = 0

            'set ddlKP kepada default
            Dim ddlKP = DirectCast(gvr.FindControl("ddlKP"), DropDownList)
            ddlKP.Items.Clear()
            ddlKP.Items.Insert(0, New ListItem("- SILA PILIH PTj -", 0))
            ddlKP.SelectedIndex = 0

            'set ddlVot kepada default
            Dim ddlVot = DirectCast(gvr.FindControl("ddlVot"), DropDownList)
            ddlVot.Items.Clear()
            ddlVot.Items.Insert(0, New ListItem("- SILA PILIH KP -", 0))
            ddlVot.SelectedIndex = 0


        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlPTj_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try

            Dim gvr As GridViewRow = CType(((CType(sender, Control)).NamingContainer), GridViewRow)
            Dim ddlKW As DropDownList = CType(gvr.FindControl("ddlKW"), DropDownList)
            Dim strKodKW As String = ddlKW.SelectedItem.Value

            Dim ddlKO As DropDownList = CType(gvr.FindControl("ddlKO"), DropDownList)
            Dim strKodKO As String = ddlKO.SelectedItem.Value

            Dim ddlPTj As DropDownList = CType(gvr.FindControl("ddlPTj"), DropDownList)
            Dim strKodPTj As String = ddlPTj.SelectedItem.Value

            'Load ddlKP
            Dim ddlKP = DirectCast(gvr.FindControl("ddlKP"), DropDownList)
            ddlKP.DataSource = fGetKP(strKodKW, strKodKO, strKodPTj)
            ddlKP.DataTextField = "Butiran"
            ddlKP.DataValueField = "KodProjek"
            ddlKP.DataBind()
            ddlKP.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
            ddlKP.SelectedIndex = 0

            'set ddlVot kepada default
            Dim ddlVot = DirectCast(gvr.FindControl("ddlVot"), DropDownList)
            ddlVot.Items.Clear()
            ddlVot.Items.Insert(0, New ListItem("- SILA PILIH KP -", 0))
            ddlVot.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlKP_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try

            Dim gvr As GridViewRow = CType(((CType(sender, Control)).NamingContainer), GridViewRow)
            Dim ddlKW As DropDownList = CType(gvr.FindControl("ddlKW"), DropDownList)
            Dim strKodKW As String = ddlKW.SelectedItem.Value

            Dim ddlKO As DropDownList = CType(gvr.FindControl("ddlKO"), DropDownList)
            Dim strKodKO As String = ddlKO.SelectedItem.Value

            Dim ddlPTj As DropDownList = CType(gvr.FindControl("ddlPTj"), DropDownList)
            Dim strKodPTj As String = ddlPTj.SelectedItem.Value

            Dim ddlKP As DropDownList = CType(gvr.FindControl("ddlKP"), DropDownList)
            Dim strKodKP As String = ddlKP.SelectedItem.Value

            'Load ddlVot
            Dim ddlVot = DirectCast(gvr.FindControl("ddlVot"), DropDownList)
            ddlVot.DataSource = fGetVot(strKodKW, strKodKO, strKodPTj, strKodKP)
            ddlVot.DataTextField = "Butiran"
            ddlVot.DataValueField = "KodVot"
            ddlVot.DataBind()
            ddlVot.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
            ddlVot.SelectedIndex = 0
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnShowMpePT_ServerClick(sender As Object, e As EventArgs) Handles btnShowMpePT.ServerClick
        fClearGvPT()
        txtCarian.Text = ""
        fBindDdlCarian()
        mpePnlNoPT.Show()
    End Sub


    Private Sub fClearGvSY()
        gvSY.DataSource = New List(Of String)
        gvSY.DataBind()
    End Sub

    Private Sub fClearGvST()
        gvST.DataSource = New List(Of String)
        gvST.DataBind()
    End Sub
    Private Sub fClearGvPT()
        gvPT.DataSource = New List(Of String)
        gvPT.DataBind()
    End Sub

    Private Sub sClearGvTransPT()
        gvTransPT.DataSource = New List(Of String)
        gvTransPT.DataBind()
    End Sub

    Private Sub sClearGvTransLain()
        gvTrans.DataSource = New List(Of String)
        gvTrans.DataBind()
    End Sub

    Private Sub fBindDdlCarian()

        Try
            ddlCarian.Items.Clear()
            ddlCarian.Items.Add(New ListItem("No. PT/PB/Inden", "1"))
            ddlCarian.Items.Add(New ListItem("Nama Penerima", "2"))
            ddlCarian.Items.Add(New ListItem("Nama Pembekal", "3"))

        Catch ex As Exception

        End Try

    End Sub

    Private Sub btnCariPt_ServerClick(sender As Object, e As EventArgs) Handles btnCariPt.ServerClick
        If txtCarian.Text <> "" Then
            fBindGvPT()
        End If
        mpePnlNoPT.Show()
    End Sub

    Private Sub fBindGvPT()
        Dim strSql As String
        Dim intRec As Integer
        Dim strFilter As String
        Dim strJenBrg As String
        fClearGvPT()
        Try

            Dim dt As New DataTable
            dt.Columns.Add("PtID", GetType(Integer))
            dt.Columns.Add("NoPT", GetType(String))
            dt.Columns.Add("TkhPT", GetType(String))
            dt.Columns.Add("IDSya", GetType(String))
            dt.Columns.Add("NamaSya", GetType(String))
            dt.Columns.Add("JumAsal", GetType(String))
            dt.Columns.Add("JumBB", GetType(String))
            dt.Columns.Add("FlagFIR", GetType(Integer))

            If ddlCarian.SelectedValue = 1 Then  'Carian no PT
                strFilter = "A.PO19_NoPt like '%" & Trim(txtCarian.Text.TrimEnd) & "%'"

            ElseIf ddlCarian.SelectedValue = 2 Then 'Carian penerima
                strFilter = "A.PO19_BekalKepada like '%" & Trim(txtCarian.Text.TrimEnd) & "%'"

            ElseIf ddlCarian.SelectedValue = 3 Then 'Carian pembekal
                strFilter = "C.ROC01_NamaSya like '%" & Trim(txtCarian.Text.TrimEnd) & "%'"
            End If

            If ddlJenInv.SelectedValue = "IPT" Then
                strJenBrg = "B"
            ElseIf ddlJenInv.SelectedValue = "IND" Then
                strJenBrg = "K"
            End If

            strSql = "SELECT A.PO19_PtID, A.PO19_NoPt, A.PO19_TkhPt,  B.PO01_JenisBrg , A.PO19_FlagFIR AS FlagFIR, A.ROC01_IdSya, C.ROC01_NoSya, C.ROC01_NamaSya, A.PO19_JumSebenar , A.PO19_JumBlmByr, A.PO19_StatusPP, B.PO01_NoMohonSem 
FROM PO19_Pt A 
INNER JOIN PO01_PPembelian B ON A.PO01_NoMohon = B.PO01_NoMohon  
INNER JOIN ROC01_Syarikat C ON A.ROC01_IdSya = C.ROC01_IDSya 
Where (A.PO19_StatusPP in ('074','094','084')) AND A.PO19_StatusCF <> 1 AND A.PO19_JumBlmByr > 0 AND A.PO19_Status = 'A' AND A.PO19_JenisBrg = '" & strJenBrg & "'
AND " & strFilter &
" Group BY A.PO19_PtID, A.PO19_NoPt, A.PO19_TkhPt,  B.PO01_JenisBrg , A.PO19_FlagFIR, A.ROC01_IdSya, C.ROC01_NamaSya, C.ROC01_NoSya, A.PO19_JumSebenar , A.PO19_JumBlmByr, A.PO19_StatusPP, B.PO01_NoMohonSem 
ORDER BY A.PO19_NoPt, A.PO19_TkhPt"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            Dim intPtID As Integer
            Dim strNoPT, strTkhPT, strIDSya, strNamaSya, strJumAsl, strJumBB As String

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        intPtID = ds.Tables(0).Rows(i)("PO19_PtID")
                        strNoPT = ds.Tables(0).Rows(i)("PO19_NoPt")
                        strTkhPT = ds.Tables(0).Rows(i)("PO19_TkhPt")
                        strIDSya = ds.Tables(0).Rows(i)("ROC01_IdSya")
                        strNamaSya = ds.Tables(0).Rows(i)("ROC01_NamaSya")
                        strJumAsl = FormatNumber(ds.Tables(0).Rows(i)("PO19_JumSebenar"), 2)
                        strJumBB = FormatNumber(ds.Tables(0).Rows(i)("PO19_JumBlmByr"), 2)
                        dt.Rows.Add(intPtID, strNoPT, strTkhPT, strIDSya, strNamaSya, strJumAsl, strJumBB)
                    Next
                    gvPT.DataSource = dt
                    gvPT.DataBind()
                    intRec = dt.Rows.Count
                    ViewState("dtPT") = dt
                End If

            End If

            If intRec = 0 Then
                fGlobalAlert("Tiada rekod!", Me.Page, Me.[GetType]())
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub gvPT_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvPT.RowCommand
        Try
            If e.CommandName = "Select" Then
                'sNewRec()
                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim selectedRow As GridViewRow = gvPT.Rows(index)

                Dim intNoPtId As String = CType(selectedRow.FindControl("lblPtID"), Label).Text.TrimEnd
                Dim strNoPT As String = CType(selectedRow.FindControl("lblNoPT"), Label).Text.TrimEnd
                Dim strJumInv As String = CType(selectedRow.FindControl("lblJumBB"), Label).Text.TrimEnd
                txtNoPT.Text = strNoPT
                fLoadPtDt(intNoPtId)
                fLoadPenerima(strNoPT)
                fClearGvPT()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Function fLoadPtDt(ByVal intNoPtId As Integer)
        Try
            Dim dt As New DataTable
            dt = fGetDtTransIPT()

            Dim strSql As String = "select PO19_PtDtId, KodKw, KodKO, KodPtj, KodKP, KodVot, PO19_Butiran, PO19_Kuantiti as QtyAsal, PO19_KadarHarga as KadarAsal, PO19_JumKadar as JumAsal, PO19_QBlmByr as QtyBlmByr , PO19_JumBlmByr as JumBlmByr from PO19_PtDt 
where PO19_PtID = " & intNoPtId

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            Dim intPtDtID As Integer
            Dim strKW, strKO, strPTj, strKP, strVot, strItem, strKadarAsl As String
            Dim intQtyAsl, intQtyBlm, intQtyAkan As Integer
            Dim strJumAsl, strJumBlm, strJumAkan As String
            Dim strBaki As String

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        intPtDtID = ds.Tables(0).Rows(i)("PO19_PtDtId")
                        strKW = ds.Tables(0).Rows(i)("KodKw")
                        strKO = ds.Tables(0).Rows(i)("KodKO")
                        strPTj = ds.Tables(0).Rows(i)("KodPtj")
                        strKP = ds.Tables(0).Rows(i)("KodKP")
                        strVot = ds.Tables(0).Rows(i)("KodVot")
                        Dim strVotSbg As String = strVot.Substring(0, 5)
                        strItem = ds.Tables(0).Rows(i)("PO19_Butiran")

                        intQtyAsl = CInt(ds.Tables(0).Rows(i)("QtyAsal"))
                        strKadarAsl = FormatNumber(ds.Tables(0).Rows(i)("KadarAsal"), 2)
                        strJumAsl = FormatNumber(ds.Tables(0).Rows(i)("JumAsal"), 2)

                        intQtyBlm = CInt(ds.Tables(0).Rows(i)("QtyBlmByr"))
                        strJumBlm = FormatNumber(ds.Tables(0).Rows(i)("JumBlmByr"), 2)

                        intQtyAkan = CInt(ds.Tables(0).Rows(i)("QtyBlmByr"))
                        strJumAkan = FormatNumber(ds.Tables(0).Rows(i)("JumBlmByr"), 2)

                        If fGetVotBelanja.Contains(strVot.Substring(0, 1) & "0000") Then
                            strVotSbg = strVot.Substring(0, 2) & "000"
                            strBaki = FormatNumber(CDec(fGetBakiSebenar(Now.Year, Now.ToString("yyyy-MM-dd"), strKW, strKO, strPTj, strKP, strVotSbg)))
                        Else
                            strBaki = ""
                        End If


                        dt.Rows.Add(intPtDtID, strKW, strKO, strPTj, strKP, strVot, strItem,
                                    intQtyAsl, strKadarAsl, strJumAsl, intQtyBlm, strJumBlm, intQtyAkan, strJumAkan, strBaki)
                    Next
                End If
            End If

            gvTransPT.DataSource = dt
            gvTransPT.DataBind()

        Catch ex As Exception

        End Try
    End Function

    Private Function fGetDtTransIPT() As DataTable
        Try
            Dim dt As New DataTable
            dt.Columns.Add("PtDtId", GetType(Integer))
            dt.Columns.Add("KW", GetType(String))
            dt.Columns.Add("KO", GetType(String))
            dt.Columns.Add("PTj", GetType(String))
            dt.Columns.Add("KP", GetType(String))
            dt.Columns.Add("Vot", GetType(String))
            dt.Columns.Add("Item", GetType(String))
            dt.Columns.Add("QtyAsl", GetType(String))
            dt.Columns.Add("HargaAsl", GetType(String))
            dt.Columns.Add("JumAsl", GetType(String))
            dt.Columns.Add("QtyBlm", GetType(String))
            dt.Columns.Add("JumBlm", GetType(String))
            dt.Columns.Add("QtyAkan", GetType(String))
            dt.Columns.Add("JumAkan", GetType(String))
            dt.Columns.Add("Baki", GetType(String))

            Return dt
        Catch ex As Exception

        End Try
    End Function

    Private Function fGetDtTransILL() As DataTable
        Try
            Dim dt As New DataTable
            dt.Columns.Add("PtDtId", GetType(Integer))
            dt.Columns.Add("KW", GetType(String))
            dt.Columns.Add("KO", GetType(String))
            dt.Columns.Add("PTj", GetType(String))
            dt.Columns.Add("KP", GetType(String))
            dt.Columns.Add("Vot", GetType(String))
            dt.Columns.Add("Item", GetType(String))
            dt.Columns.Add("QtyAsl", GetType(String))
            dt.Columns.Add("HargaAsl", GetType(String))
            dt.Columns.Add("JumAsl", GetType(String))
            dt.Columns.Add("QtyBlm", GetType(String))
            dt.Columns.Add("JumBlm", GetType(String))
            dt.Columns.Add("QtyAkan", GetType(String))
            dt.Columns.Add("JumAkan", GetType(String))

            Return dt
        Catch ex As Exception

        End Try
    End Function

    Private Sub fLoadPenerima(ByVal strNoPT As String)

        Try
            Dim ds As New DataSet
            If fCheckFIR(strNoPT, ds) = False Then
                ds = fGetSya(strNoPT)
            End If

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    txtIdKat.Text = "SY"
                    txtKat.Text = "SYARIKAT"
                    txtIDPenerima.Text = ds.Tables(0).Rows(0)("IdSya").ToString
                    txtNmPenerima.Text = ds.Tables(0).Rows(0)("NamaSya").ToString
                    txtJumInv.Text = FormatNumber(ds.Tables(0).Rows(0)("Jumbayar").ToString(), 2)
                    ' txtBank.Text = ds.Tables(0).Rows(0)("NamaBank").ToString
                    ddlBank.SelectedValue = ds.Tables(0).Rows(0)("KodBank").ToString
                    'hidKodBank.Value = ds.Tables(0).Rows(0)("KodBank").ToString
                    txtNoAkaun.Text = ds.Tables(0).Rows(0)("NoAkaun").ToString
                    txtAlmt1.Text = ds.Tables(0).Rows(0)("AlmtP1").ToString
                    txtAlmt2.Text = ds.Tables(0).Rows(0)("AlmtP2").ToString
                    txtBandar.Text = ds.Tables(0).Rows(0)("BandarP").ToString
                    txtPoskod.Text = ds.Tables(0).Rows(0)("PoskodP").ToString
                    ddlNegeri.SelectedValue = ds.Tables(0).Rows(0)("KodNegeriP").ToString
                    ddlNegara.SelectedValue = ds.Tables(0).Rows(0)("KodNegaraP").ToString
                    txtEmel.Text = ds.Tables(0).Rows(0)("Emel").ToString
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub sBindDdlNegeri()
        Try
            Dim strSql As String = "select kodnegeri, Butiran from MK_Negeri where KodNegeri <> '-'"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            ddlNegeri.DataSource = ds
            ddlNegeri.DataTextField = "Butiran"
            ddlNegeri.DataValueField = "kodnegeri"
            ddlNegeri.DataBind()

            ddlNegeri.Items.Insert(0, New ListItem("- SILA PILIH - ", "0"))
            ddlNegeri.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Private Sub sBindDdlNegara()
        Try
            Dim strSql As String = "select KodNegara , Butiran  from MK_Negara where KodNegara <> '-'"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            ddlNegara.DataSource = ds
            ddlNegara.DataTextField = "Butiran"
            ddlNegara.DataValueField = "KodNegara"
            ddlNegara.DataBind()

            ddlNegara.Items.Insert(0, New ListItem("- SILA PILIH - ", "0"))
            ddlNegara.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlKaedah_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            Dim ddlKaedah As DropDownList = CType(sender, DropDownList)
            Dim gvRow As GridViewRow = (CType(ddlKaedah.Parent.Parent, GridViewRow))
            Dim txtPeratus As TextBox = CType(gvRow.FindControl("txtPeratus"), TextBox)
            Dim txtJumAkan As TextBox = CType(gvRow.FindControl("txtJumAkan"), TextBox)

            Dim intVal As Integer = ddlKaedah.SelectedValue

            If intVal = 0 Then
                txtPeratus.Enabled = False
                txtPeratus.Text = ""
                txtJumAkan.Enabled = True
            Else
                txtPeratus.Enabled = True
                txtJumAkan.Enabled = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub sBindDdlBank()
        Try
            Dim strSql As String = "select Kod, (Kod + ' - ' + Nama) as NamaBank from MK_BankCreditOnline where Kod <> '-'"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            ddlBank.DataSource = ds
            ddlBank.DataTextField = "NamaBank"
            ddlBank.DataValueField = "Kod"
            ddlBank.DataBind()
            ddlBank.Items.Insert(0, New ListItem("- SILA PILIH - ", "0"))
            ddlBank.SelectedIndex = 0
        Catch ex As Exception

        End Try
    End Sub

    Private Sub lbtnSimpan_Click(sender As Object, e As EventArgs) Handles lbtnSimpan.Click

        If Page.IsValid Then
            Dim decJumlah As Decimal
            If ddlJenInv.SelectedValue = "ILL" Then
                Dim footerTrans = gvTrans.FooterRow
                decJumlah = CDec(CType(footerTrans.FindControl("lblTotJum"), Label).Text)
            Else
                Dim footerTrans = gvTransPT.FooterRow
                decJumlah = CDec(CType(footerTrans.FindControl("lblGJumByr"), Label).Text)
            End If

            If decJumlah = 0 Then
                Exit Sub
            End If

            Try
                If txtIdInv.Text = "" Then
                    If fSimpan() = True Then
                        fGlobalAlert("Maklumat invois telah disimpan!", Me.Page, Me.[GetType]())
                        sNewRec()
                    Else
                        fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
                    End If
                Else
                    If fUpdate() = True Then
                        fGlobalAlert("Maklumat invois telah dikemas kini!", Me.Page, Me.[GetType]())
                        sNewRec()
                    Else
                        fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
                    End If
                End If

            Catch ex As Exception

            End Try
        End If

    End Sub

    Private Function fSimpan() As Boolean
        Dim intTrackNo As Integer
        Dim strErrMsg As String
        Dim blnSuccess As Boolean = True
        Dim dbconn As New DBKewConn
        Dim strNoStaf As String = Session("ssusrID")
        Dim strSql As String
        Dim strTahun As String
        Dim strStatTng As String


        Try
            intTrackNo = 1
            '1- INSERT AP01_Invois
            Dim strIdInv, strNoInv, strNoDO, strJenByr, strStatusDok As String
            Dim strKodJenByrn As String
            Dim intStatus As Integer

            strTahun = Now.Year
            strNoInv = Trim(txtNoInv.Text.TrimEnd)
            strNoDO = Trim(txtNoDO.Text.TrimEnd)

            intTrackNo = 111
            Dim dtTkhInv As Date = DateTime.ParseExact(Trim(txtTkhInv.Text.TrimEnd), "dd/MM/yyyy", CultureInfo.CurrentCulture)
            Dim strTkhInv As String = dtTkhInv.ToString("yyyy-MM-dd")

            intTrackNo = 112
            Dim dtTkhTInv As Date = DateTime.ParseExact(Trim(txtTkhTInv.Text.TrimEnd), "dd/MM/yyyy", CultureInfo.CurrentCulture)
            Dim strTkhTInv As String = dtTkhTInv.ToString("yyyy-MM-dd")

            intTrackNo = 113
            Dim dtTkhDO As Date = DateTime.ParseExact(Trim(txtTkhDO.Text.TrimEnd), "dd/MM/yyyy", CultureInfo.CurrentCulture)
            Dim strTkhDO As String = dtTkhDO.ToString("yyyy-MM-dd")

            intTrackNo = 114
            Dim dtTkhTDO As Date = DateTime.ParseExact(Trim(txtTkhTDO.Text.TrimEnd), "dd/MM/yyyy", CultureInfo.CurrentCulture)
            Dim strTkhTDO As String = dtTkhTDO.ToString("yyyy-MM-dd")

            intTrackNo = 115

            Dim dtTkhDafTInv As Date = Now.ToString
            Dim strTkhDaf As String = dtTkhDafTInv.ToString("yyyy-MM-dd")

            Dim strJenInv As String = Trim(ddlJenInv.SelectedValue.TrimEnd)

            strStatusDok = "01"
            intStatus = 1

            Dim strIdPenerima As String
            Dim strPenerima As String
            Dim decJumlah As Decimal
            Dim strIdSya As String
            Dim strNoPT As String

            intTrackNo = 2
            Select Case ddlJenInv.SelectedValue
                Case "IPT", "IND"
                    intTrackNo = 3
                    strIdPenerima = Trim(txtIDPenerima.Text.TrimEnd)
                    strPenerima = Trim(txtNmPenerima.Text.TrimEnd)
                    strIdSya = strIdPenerima

                    Dim footerTrans = gvTransPT.FooterRow
                    decJumlah = CDec(CType(footerTrans.FindControl("lblGJumByr"), Label).Text)

                    strNoPT = Trim(txtNoPT.Text.TrimEnd)

                Case "IPB"
                    intTrackNo = 4
                    strIdPenerima = Trim(txtIDPenerima.Text.TrimEnd)
                    strPenerima = Trim(txtNmPenerima.Text.TrimEnd)
                    strIdSya = strIdPenerima
                    strNoPT = "-"
                    Dim footerTrans = gvTransPT.FooterRow
                    decJumlah = CDec(CType(footerTrans.FindControl("lblGJumByr"), Label).Text)

                Case "ILL"

                    Select Case ddlKat.SelectedValue
                        Case "OA"
                            intTrackNo = 5
                            strIdPenerima = Trim(txtKPOA.Text.TrimEnd)
                            strPenerima = Trim(txtNamaOA.Text.TrimEnd)
                            strIdSya = "-"

                        Case Else
                            intTrackNo = 6
                            strIdPenerima = Trim(txtIDPenerima.Text.TrimEnd)
                            strPenerima = Trim(txtNmPenerima.Text.TrimEnd)
                            strIdSya = strIdPenerima

                    End Select

                    strNoPT = "-"

                    intTrackNo = 7
                    Dim footerTrans = gvTrans.FooterRow
                    decJumlah = CDec(CType(footerTrans.FindControl("lblTotJum"), Label).Text)
                Case Else

            End Select

            intTrackNo = 9
            strIdInv = fGetRunNo(strJenInv, Session("ssusrKodPTj"), 6)

            If strIdInv = String.Empty Then
                intTrackNo = 10
                blnSuccess = False
                Exit Try
            End If

            strKodJenByrn = 1
            strStatTng = 1
            strJenByr = hidJenByr.Value


            strSql = "INSERT INTO AP01_Invois (AP01_NoId, PO19_NoPt, AP01_NoInv, AP01_TkhInv, AP01_TkhTInv, AP01_NoDo, AP01_TkhDO ,AP01_TkhTDO, AP01_TkhDaftar, AP01_Tahun, AP01_JenByr, AP01_JenInv, AP01_Jumlah, AP09_StatusDok, AP01_Status, ROC01_IdSya, AP01_Padan,AP01_StatusTng) " &
                "VALUES (@IdInv, @NoPT, @NoInvois, @TkhInv, @TkhTInv, @NoDO, @TkhDO, @TkhTDO,  @TkhDaf, @Tahun, @JenByr, @JenInv, @Jumlah, @StatusDok, @Status, @IdSya, @Padan,@StatTng)"
            Dim paramSql() As SqlParameter = {
                   New SqlParameter("@IdInv", strIdInv),
                    New SqlParameter("@NoPT", strNoPT),
                    New SqlParameter("@NoInvois", strNoInv),
                    New SqlParameter("@TkhInv", strTkhInv),
                    New SqlParameter("@TkhTInv", strTkhTInv),
                    New SqlParameter("@NoDO", strNoDO),
                    New SqlParameter("@TkhDO", strTkhDO),
                    New SqlParameter("@TkhTDO", strTkhTDO),
                    New SqlParameter("@TkhDaf", strTkhDaf),
                    New SqlParameter("@Tahun", strTahun),
                    New SqlParameter("@JenByr", strJenByr),
                New SqlParameter("@JenInv", strJenInv),
                 New SqlParameter("@Jumlah", decJumlah),
                New SqlParameter("@StatusDok", strStatusDok),
                New SqlParameter("@Status", intStatus),
                New SqlParameter("@IdSya", strIdSya),
                 New SqlParameter("@Padan", 0),
                 New SqlParameter("@StatTng", strStatTng)
                }

            dbconn.sConnBeginTrans()
            If Not dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                intTrackNo = 11
                blnSuccess = False
                Exit Try
            Else
                sLogMedan = "AP01_Invois|PO19_NoPt|AP01_NoInv|AP01_TkhInv|AP01_TkhInv|AP01_NoDo|AP01_TkhTDO|AP01_TkhTDO|AP01_TkhDaftar|AP01_Tahun|AP01_JenByr|AP01_JenInv|AP01_Jumlah|AP09_StatusDok|ROC01_IdSya||"
                sLogBaru = strIdInv & "|" & strNoPT & "|" & strNoInv & "|" & strTkhInv & "|" & strTkhTInv & "|" & strNoDO & "|" & strTkhDO & "|" & strTkhTDO & "|" & strTkhDaf & "|" & strTahun & "|" & strJenByr & "|" & strJenInv & "|" & decJumlah & "|" & strStatusDok & "|" & strIdSya & "||"
                sLogLama = "-"
                Audit("SIMPAN", "", "AP01_Invois", sLogMedan, sLogLama, sLogBaru)
            End If

            Select Case ddlJenInv.SelectedValue
                Case "IPT", "IND"
                    intTrackNo = 12
                    '2- INSERT AP01_InvoisDt
                    Dim intBil As Int16 = 0
                    Dim intPtDtId As Integer
                    Dim strKW, strKO, strPTj, strKP, strVot, strButiran As String

                    Dim decKdrHarga, decAmtByr As Decimal
                    Dim intKttAkanByr As Integer

                    For Each gvRow As GridViewRow In gvTransPT.Rows
                        intTrackNo = 13
                        intBil = intBil + 1
                        intPtDtId = Trim(TryCast(gvRow.FindControl("lblPtDtID"), Label).Text.TrimEnd)
                        strKW = Trim(TryCast(gvRow.FindControl("lblKW"), Label).Text.TrimEnd)
                        strKO = Trim(TryCast(gvRow.FindControl("lblKO"), Label).Text.TrimEnd)
                        strPTj = Trim(TryCast(gvRow.FindControl("lblPTj"), Label).Text.TrimEnd)
                        strKP = Trim(TryCast(gvRow.FindControl("lblKP"), Label).Text.TrimEnd)
                        strVot = Trim(TryCast(gvRow.FindControl("lblVot"), Label).Text.TrimEnd)
                        strButiran = Trim(TryCast(gvRow.FindControl("lblItem"), Label).Text.TrimEnd)
                        decKdrHarga = CDec(TryCast(gvRow.FindControl("lblHargaAsl"), Label).Text)
                        intKttAkanByr = CInt(TryCast(gvRow.FindControl("txtQtyByr"), TextBox).Text)
                        decAmtByr = CDec(TryCast(gvRow.FindControl("txtJumAkan"), TextBox).Text)

                        If decAmtByr > 0 Then
                            strSql = "insert into AP01_InvoisDt (AP01_NoId, PO19_PtDtId, AP01_Bil, KodKw , KodKO , KodPtj , KodKP , KodVot , AP01_Butiran ,AP01_KadarHarga, AP01_KuantitiAkanByr, AP01_AmaunAkanByr) 
                                        values (@IdInv, @PtDtId, @Bil, @KodKW, @KodKO, @KodPtj, @KodKP, @KodVot, @Butiran, @KadarHarga, @KttAkanByr, @AmaunAkanByr )"
                            Dim paramSql2() As SqlParameter = {
                                    New SqlParameter("@IdInv", strIdInv),
                                    New SqlParameter("@Bil", intBil),
                                    New SqlParameter("@PtDtId", intPtDtId),
                                    New SqlParameter("@KodKW", strKW),
                                    New SqlParameter("@KodKO", strKO),
                                    New SqlParameter("@KodPtj", strPTj),
                                    New SqlParameter("@KodKP", strKP),
                                    New SqlParameter("@KodVot", strVot),
                                    New SqlParameter("@Butiran", strButiran),
                                    New SqlParameter("@KadarHarga", decKdrHarga),
                                    New SqlParameter("@KttAkanByr", intKttAkanByr),
                                    New SqlParameter("@AmaunAkanByr", decAmtByr)
                                }

                            If Not dbconn.fInsertCommand(strSql, paramSql2) > 0 Then
                                intTrackNo = 14
                                blnSuccess = False
                                Exit Try
                            Else
                                sLogMedan = "AP01_NoId|PO19_PtDtId|AP01_Bil|KodKw|KodKO|KodPt|KodKP|KodVot|AP01_Butiran|AP01_KadarHarga|AP01_KuantitiAkanByr|AP01_AmaunAkanByr||"
                                sLogBaru = strIdInv & "|" & intBil & "|" & intPtDtId & "|" & strKW & "|" & strKO & "|" & strPTj & "|" & strKP & "|" & strVot & "|" & strButiran & "|" & decKdrHarga & "|" & intKttAkanByr & "|" & decAmtByr & "||"
                                sLogLama = "-"
                                Audit("SIMPAN", "", "AP01_InvoisDt", sLogMedan, sLogLama, sLogBaru)
                            End If

                        End If
                    Next
                Case "IPB"

                Case "ILL"
                    intTrackNo = 15
                    '2- INSERT AP01_InvoisDt
                    Dim intBil As Int16 = 0
                    Dim intPtDtId As Integer
                    Dim strKW, strKO, strPTj, strKP, strVot, strButiran As String

                    Dim decKdrHarga, decAmtByr As Decimal
                    Dim intKttAkanByr As Integer

                    For Each gvRow As GridViewRow In gvTrans.Rows
                        intTrackNo = 16
                        intBil = intBil + 1
                        strKW = CType(gvRow.FindControl("ddlKW"), DropDownList).SelectedValue.TrimEnd
                        strKO = CType(gvRow.FindControl("ddlKO"), DropDownList).SelectedValue.TrimEnd
                        strPTj = CType(gvRow.FindControl("ddlPTj"), DropDownList).SelectedValue.TrimEnd
                        strKP = CType(gvRow.FindControl("ddlKP"), DropDownList).SelectedValue.TrimEnd
                        strVot = CType(gvRow.FindControl("ddlVot"), DropDownList).SelectedValue.TrimEnd
                        strButiran = CType(gvRow.FindControl("txtButiran"), TextBox).Text.TrimEnd
                        decKdrHarga = CDec(CType(gvRow.FindControl("txtHarga"), TextBox).Text.TrimEnd)
                        intKttAkanByr = CType(gvRow.FindControl("txtKttByr"), TextBox).Text.TrimEnd
                        decAmtByr = CDec(CType(gvRow.FindControl("lblJumlah"), Label).Text.TrimEnd)

                        strSql = "insert into AP01_InvoisDt (AP01_NoId, AP01_Bil, KodKw , KodKO , KodPtj , KodKP , KodVot , AP01_Butiran ,AP01_KadarHarga, AP01_KuantitiAkanByr, AP01_AmaunAkanByr) 
                                        values (@IdInv, @Bil, @KodKW, @KodKO, @KodPtj, @KodKP, @KodVot, @Butiran, @KadarHarga, @KttAkanByr, @AmaunAkanByr )"
                        Dim paramSql2() As SqlParameter = {
                                New SqlParameter("@IdInv", strIdInv),
                                New SqlParameter("@Bil", intBil),
                                New SqlParameter("@KodKW", strKW),
                                New SqlParameter("@KodKO", strKO),
                                New SqlParameter("@KodPtj", strPTj),
                                New SqlParameter("@KodKP", strKP),
                                New SqlParameter("@KodVot", strVot),
                                New SqlParameter("@Butiran", strButiran),
                                New SqlParameter("@KadarHarga", decKdrHarga),
                                New SqlParameter("@KttAkanByr", intKttAkanByr),
                                New SqlParameter("@AmaunAkanByr", decAmtByr)
                            }

                        If Not dbconn.fInsertCommand(strSql, paramSql2) > 0 Then
                            intTrackNo = 17
                            blnSuccess = False

                            Exit Try
                        Else
                            sLogMedan = "AP01_NoId|PO19_PtDtId|AP01_Bil|KodKw|KodKO|KodPt|KodKP|KodVot|AP01_Butiran|AP01_KadarHarga|AP01_KuantitiAkanByr|AP01_AmaunAkanByr||"
                            sLogBaru = strIdInv & "|" & intBil & "|" & intPtDtId & "|" & strKW & "|" & strKO & "|" & strPTj & "|" & strKP & "|" & strVot & "|" & strButiran & "|" & decKdrHarga & "|" & intKttAkanByr & "|" & decAmtByr & "||"
                            sLogLama = "-"
                            Audit("SIMPAN", "", "AP01_InvoisDt", sLogMedan, sLogLama, sLogBaru)
                        End If
                    Next

            End Select


            intTrackNo = 18
            '3- INSERT INTO AP01_InvoisNominees
            Dim strKatPenerima As String = Trim(txtIdKat.Text.TrimEnd)

            Dim decJumlahP As Decimal = decJumlah
            Dim strAlamat1 As String = Trim(txtAlmt1.Text.TrimEnd)
            Dim strAlamat2 As String = Trim(txtAlmt2.Text.TrimEnd)
            Dim strBandar As String = Trim(txtBandar.Text.TrimEnd)
            Dim strPoskod As String = Trim(txtPoskod.Text.TrimEnd)
            Dim strKoNegeri As String = Trim(ddlNegeri.SelectedValue.TrimEnd)
            Dim strKodNegara As String = Trim(ddlNegara.SelectedValue.TrimEnd)
            Dim strNoAkaun As String = Trim(txtNoAkaun.Text.TrimEnd)
            Dim strKodBank As String = Trim(ddlBank.Text.Trim)
            Dim strEmail As String = Trim(txtEmel.Text.TrimEnd)

            strSql = "INSERT INTO AP01_InvoisNominees (AP01_NoId, AP01_Bil, KategoriPenerima , AP01_IdPenerima, AP01_Penerima , AP01_Jumlah, AP01_Alamat1 , AP01_Alamat2 , AP01_Bandar , AP01_Poskod , KodNegeri , KodNegara , AP01_NoAkaun , AP01_KodBank , AP01_Email  )
                    VALUES (@IdInv, @Bil, @KategoriPenerima, @IdPenerima, @Penerima, @Jumlah, @Alamat1, @Alamat2, @Bandar, @Poskod, @KodNegeri, @KodNegara, @NoAkaun, @KodBank, @Email)"
            Dim paramSql3() As SqlParameter = {
                    New SqlParameter("@IdInv", strIdInv),
                    New SqlParameter("@Bil", 1),
                    New SqlParameter("@KategoriPenerima", strKatPenerima),
                    New SqlParameter("@IdPenerima", strIdPenerima),
                    New SqlParameter("@Penerima", strPenerima),
                    New SqlParameter("@Jumlah", decJumlahP),
                    New SqlParameter("@Alamat1", strAlamat1),
                    New SqlParameter("@Alamat2", strAlamat2),
                    New SqlParameter("@Bandar", strBandar),
                    New SqlParameter("@Poskod", strPoskod),
                    New SqlParameter("@KodNegeri", strKoNegeri),
                    New SqlParameter("@KodNegara", strKodNegara),
                    New SqlParameter("@NoAkaun", strNoAkaun),
                    New SqlParameter("@KodBank", strKodBank),
                    New SqlParameter("@Email", strEmail)
                }

            If Not dbconn.fInsertCommand(strSql, paramSql3) > 0 Then
                intTrackNo = 19
                blnSuccess = False
                Exit Try
            Else
                sLogMedan = "AP01_NoId|AP01_Bil|KategoriPenerima|AP01_IdPenerima|AP01_Penerima|AP01_Jumlah|AP01_Alamat1|AP01_Alamat2|AP01_Bandar|AP01_Poskod|KodNegeri|KodNegara|AP01_NoAkaun|AP01_KodBank|AP01_Email||"
                sLogBaru = strIdInv & "|" & 1 & "|" & strKatPenerima & "|" & strIdPenerima & "|" & strPenerima & "|" & decJumlahP & "|" & strAlamat1 & "|" & strAlamat2 & "|" & strBandar & "|" & strPoskod & "|" & strKoNegeri & "|" & strKodNegara & "|" & strNoAkaun & "|" & strKodBank & "|" & strEmail & "||"
                sLogLama = "-"
                Audit("SIMPAN", "", "AP01_InvoisNominees", sLogMedan, sLogLama, sLogBaru)
            End If

            'INSERT INTO AP09_StatusDok
            strSql = "INSERT INTO AP09_StatusDok (AP02_NoPP, AP04_NoBaucar, AP01_NoId, AP09_StatusDok, AP09_NoStaf, AP09_Tkh, AP09_Ulasan) " &
                "VALUES (@NoPP, @NoBaucar, @NoID, @StatusDok, @NoStaf, @Tkh, @Ulasan)"
            Dim paramSql4() As SqlParameter = {
                    New SqlParameter("@NoPP", "-"),
                    New SqlParameter("@NoBaucar", "-"),
                    New SqlParameter("@NoID", strIdInv),
                    New SqlParameter("@StatusDok", "01"),
                    New SqlParameter("@NoStaf", strNoStaf),
                    New SqlParameter("@Tkh", Now.ToString("yyyy-MM-dd")),
                    New SqlParameter("@Ulasan", "-")
                }

            If Not dbconn.fInsertCommand(strSql, paramSql4) > 0 Then
                intTrackNo = 20
                blnSuccess = False
                Exit Try
            Else
                sLogMedan = "AP02_NoPP|AP04_NoBaucar|AP01_NoId|AP09_StatusDok|AP09_NoStaf|AP09_Tkh|AP09_Ulasan||"
                sLogBaru = "-|-|" & strIdInv & "|01|" & strNoStaf & "|" & Now.ToString("yyyy-MM-dd") & "|-||"
                sLogLama = "-"
                Audit("SIMPAN", "", "AP09_StatusDok", sLogMedan, sLogLama, sLogBaru)
            End If

        Catch ex As Exception
            strErrMsg = ex.Message.ToString
            blnSuccess = False
        End Try

        If blnSuccess = True Then
            dbconn.sConnCommitTrans()
            Return True
        ElseIf blnSuccess = False Then
            dbconn.sConnRollbackTrans()
            fErrorLog("DAFTAR_INVOIS(fSimpan);TrackNo - " & intTrackNo & "; ErrMsg - " & strErrMsg)
            Return False
        End If
    End Function

    Private Function fUpdate() As Boolean
        Dim blnSuccess As Boolean = True
        Dim dbconn As New DBKewConn
        Dim strNoStaf As String = Session("ssusrID")
        'Dim intNoAkhir As Integer
        Dim strSql As String
        Dim strTahun As String = Now.Year
        Dim decJumlah As Decimal

        Try
            '1- INSERT AP01_Invois
            Dim strIdInv = Trim(txtIdInv.Text.TrimEnd)
            Dim strNoInv = Trim(txtNoInv.Text.TrimEnd)
            Dim strNoDO = Trim(txtNoDO.Text.TrimEnd)
            Dim dtTkhInv As DateTime = CDate(Trim(txtTkhInv.Text.TrimEnd))
            Dim strTkhInv = dtTkhInv.ToString("yyyy-MM-dd")
            Dim dtTkhTInv As DateTime = CDate(Trim(txtTkhTInv.Text.TrimEnd))
            Dim strTkhTInv = dtTkhTInv.ToString("yyyy-MM-dd")
            Dim dtTkhDO As Date = Trim(txtTkhDO.Text.TrimEnd)
            Dim strTkhDO As String = dtTkhDO.ToString("yyyy-MM-dd")
            Dim dtTkhTDO As Date = Trim(txtTkhTDO.Text.TrimEnd)
            Dim strTkhTDO As String = dtTkhTDO.ToString("yyyy-MM-dd")

            Dim strJenByr = hidJenByr.Value

            Select Case ddlJenInv.SelectedValue
                Case "IPT"
                    Dim footerTrans = gvTransPT.FooterRow
                    decJumlah = CDec(CType(footerTrans.FindControl("lblGJumByr"), Label).Text)
                Case "IPB"
                    Dim footerTrans = gvTransPT.FooterRow
                    decJumlah = CDec(CType(footerTrans.FindControl("lblGJumByr"), Label).Text)

                Case "ILL"
                    Dim footerTrans = gvTrans.FooterRow
                    decJumlah = CDec(CType(footerTrans.FindControl("lblTotJum"), Label).Text)
                Case Else

            End Select


            strSql = "update AP01_Invois set AP01_NoInv = @NoInv, AP01_TkhInv = @TkhInv, AP01_TkhTInv = @TkhTInv, AP01_Jumlah = @Jumlah, AP01_NoDo = @NoDO, AP01_TkhDO = @TkhDO, AP01_TkhTDO = @TkhTDO, AP01_JenByr = @JenByr where AP01_NoId = @IdInv"

            Dim paramSql() As SqlParameter = {
                    New SqlParameter("@IdInv", strIdInv),
                    New SqlParameter("@NoInv", strNoInv),
                    New SqlParameter("@TkhInv", strTkhInv),
                    New SqlParameter("@TkhTInv", strTkhTInv),
                    New SqlParameter("@Jumlah", decJumlah),
                    New SqlParameter("@NoDO", strNoDO),
                    New SqlParameter("@TkhDO", strTkhDO),
                    New SqlParameter("@TkhTDO", strTkhTDO),
                    New SqlParameter("@JenByr", strJenByr)
                    }

            dbconn.sConnBeginTrans()
            If Not dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
                blnSuccess = False
                Exit Try
            Else
                sLogMedan = "AP01_NoInv|AP01_TkhInv|AP01_TkhTInv|AP01_Jumlah|AP01_NoDo|AP01_TkhDO|AP01_TkhTDO|AP01_JenByr||"
                sLogBaru = strNoInv & "|" & strTkhInv & "|" & strTkhTInv & "|" & decJumlah & "|" & strNoDO & "|" & strTkhDO & "|" & strTkhTDO & "|" & strJenByr & "||"
                sLogLama = "-"
                Audit("UPDATE", "", "AP01_Invois", sLogMedan, sLogLama, sLogBaru)
            End If

            '2- UPDATE AP01_InvoisDt
            Dim intPtDtId, intKttAkanByr As Integer
            Dim decAmtByr As Decimal

            Select Case ddlJenInv.SelectedValue
                Case "IPT"
                    For Each gvRow As GridViewRow In gvTransPT.Rows
                        intPtDtId = Trim(TryCast(gvRow.FindControl("lblPtDtID"), Label).Text.TrimEnd)
                        intKttAkanByr = CInt(TryCast(gvRow.FindControl("txtQtyByr"), TextBox).Text)
                        decAmtByr = CDec(TryCast(gvRow.FindControl("txtJumAkan"), TextBox).Text)

                        If decAmtByr > 0 Then
                            strSql = "update AP01_InvoisDt set AP01_KuantitiAkanByr = @KttAkanByr, AP01_AmaunAkanByr = @AmaunAkanByr where PO19_PtDtId  = @PtDtId"

                            Dim paramSql2() As SqlParameter = {
                                New SqlParameter("@PtDtId", intPtDtId),
                                New SqlParameter("@KttAkanByr", intKttAkanByr),
                                New SqlParameter("@AmaunAkanByr", decAmtByr)
                                }

                            If Not dbconn.fUpdateCommand(strSql, paramSql2) > 0 Then
                                blnSuccess = False
                                Exit Try
                            Else
                                sLogMedan = "AP01_KuantitiAkanByr|AP01_AmaunAkanByr|PO19_PtDtId||"
                                sLogBaru = decAmtByr & "|" & intKttAkanByr & "|" & intPtDtId & "||"
                                Audit("UPDATE", "", "AP01_InvoisDt", sLogMedan, sLogLama, sLogBaru)
                            End If
                        End If
                    Next

                Case "IPB"

                Case "ILL"

                    strSql = "delete from AP01_InvoisDt where AP01_NoId = '" & strIdInv & "'"
                    dbconn.sConnBeginTrans()
                    If Not dbconn.fUpdateCommand(strSql) > 0 Then
                        blnSuccess = False
                        Exit Try
                    Else
                        sLogMedan = "AP01_NoId||"
                        sLogBaru = strIdInv & "||"
                        sLogLama = "-"
                        Audit("DELETE", "", "AP01_InvoisDt", sLogMedan, sLogLama, sLogBaru)
                    End If

                    '2- INSERT AP01_InvoisDt
                    Dim intBil As Int16 = 0
                    Dim strKW, strKO, strPTj, strKP, strVot, strButiran As String
                    Dim decKdrHarga As Decimal

                    For Each gvRow As GridViewRow In gvTrans.Rows
                        intBil = intBil + 1
                        strKW = CType(gvRow.FindControl("ddlKW"), DropDownList).SelectedValue.TrimEnd
                        strKO = CType(gvRow.FindControl("ddlKO"), DropDownList).SelectedValue.TrimEnd
                        strPTj = CType(gvRow.FindControl("ddlPTj"), DropDownList).SelectedValue.TrimEnd
                        strKP = CType(gvRow.FindControl("ddlKP"), DropDownList).SelectedValue.TrimEnd
                        strVot = CType(gvRow.FindControl("ddlVot"), DropDownList).SelectedValue.TrimEnd
                        strButiran = CType(gvRow.FindControl("txtButiran"), TextBox).Text.TrimEnd
                        decKdrHarga = CDec(CType(gvRow.FindControl("txtHarga"), TextBox).Text.TrimEnd)
                        intKttAkanByr = CType(gvRow.FindControl("txtKttByr"), TextBox).Text.TrimEnd
                        decAmtByr = CDec(CType(gvRow.FindControl("lblJumlah"), Label).Text.TrimEnd)

                        strSql = "insert into AP01_InvoisDt (AP01_NoId, AP01_Bil, KodKw , KodKO , KodPtj , KodKP , KodVot , AP01_Butiran ,AP01_KadarHarga, AP01_KuantitiAkanByr, AP01_AmaunAkanByr) 
                                        values (@IdInv, @Bil, @KodKW, @KodKO, @KodPtj, @KodKP, @KodVot, @Butiran, @KadarHarga, @KttAkanByr, @AmaunAkanByr )"
                        Dim paramSql2() As SqlParameter = {
                                New SqlParameter("@IdInv", strIdInv),
                                New SqlParameter("@Bil", intBil),
                                New SqlParameter("@KodKW", strKW),
                                New SqlParameter("@KodKO", strKO),
                                New SqlParameter("@KodPtj", strPTj),
                                New SqlParameter("@KodKP", strKP),
                                New SqlParameter("@KodVot", strVot),
                                New SqlParameter("@Butiran", strButiran),
                                New SqlParameter("@KadarHarga", decKdrHarga),
                                New SqlParameter("@KttAkanByr", intKttAkanByr),
                                New SqlParameter("@AmaunAkanByr", decAmtByr)
                            }

                        If Not dbconn.fInsertCommand(strSql, paramSql2) > 0 Then
                            blnSuccess = False
                            Exit Try
                        Else
                            sLogMedan = "AP01_NoId|PO19_PtDtId|AP01_Bil|KodKw|KodKO|KodPt|KodKP|KodVot|AP01_Butiran|AP01_KadarHarga|AP01_KuantitiAkanByr|AP01_AmaunAkanByr||"
                            sLogBaru = strIdInv & "|" & intBil & "|" & intPtDtId & "|" & strKW & "|" & strKO & "|" & strPTj & "|" & strKP & "|" & strVot & "|" & strButiran & "|" & decKdrHarga & "|" & intKttAkanByr & "|" & decAmtByr & "||"
                            sLogLama = "-"
                            Audit("SIMPAN", "", "AP01_InvoisDt", sLogMedan, sLogLama, sLogBaru)
                        End If
                    Next

            End Select

            'UPDATE AP01_InvoisNominees
            strSql = "update AP01_InvoisNominees set AP01_Jumlah = @Jumlah where AP01_NoId = @IdInv"

            Dim paramSql3() As SqlParameter = {
                            New SqlParameter("@Jumlah", decJumlah),
                            New SqlParameter("@IdInv", strIdInv)
                            }

            If Not dbconn.fUpdateCommand(strSql, paramSql3) > 0 Then
                blnSuccess = False
                Exit Try
            Else
                sLogMedan = "AP01_Jumlah|AP01_NoId||"
                sLogBaru = decJumlah & "|" & strIdInv & "||"
                Audit("UPDATE", "", "AP01_InvoisNominees", sLogMedan, sLogLama, sLogBaru)
            End If


            'INSERT INTO AP09_StatusDok
            strSql = "INSERT INTO AP09_StatusDok (AP02_NoPP, AP04_NoBaucar, AP01_NoId, AP09_StatusDok, AP09_NoStaf, AP09_Tkh, AP09_Ulasan) " &
                "VALUES (@NoPP, @NoBaucar, @NoID, @StatusDok, @NoStaf, @Tkh, @Ulasan)"
            Dim paramSql4() As SqlParameter = {
                    New SqlParameter("@NoPP", "-"),
                    New SqlParameter("@NoBaucar", "-"),
                    New SqlParameter("@NoID", strIdInv),
                    New SqlParameter("@StatusDok", "01"),
                    New SqlParameter("@NoStaf", strNoStaf),
                    New SqlParameter("@Tkh", Now.ToString("yyyy-MM-dd")),
                    New SqlParameter("@Ulasan", "-")
                }

            If Not dbconn.fInsertCommand(strSql, paramSql4) > 0 Then
                blnSuccess = False
                Exit Try
            Else
                sLogMedan = "AP02_NoPP|AP04_NoBaucar|AP01_NoId|AP09_StatusDok|AP09_NoStaf|AP09_Tkh|AP09_Ulasan||"
                sLogBaru = "-|-|" & strIdInv & "|01|" & strNoStaf & "|" & Now.ToString("yyyy-MM-dd") & "|-||"
                sLogLama = "-"
                Audit("SIMPAN", "", "AP09_StatusDok", sLogMedan, sLogLama, sLogBaru)
            End If

        Catch ex As Exception
            blnSuccess = False
        End Try

        If blnSuccess = True Then
            dbconn.sConnCommitTrans()
            Return True
        ElseIf blnSuccess = False Then
            dbconn.sConnRollbackTrans()
            Return False
        End If
    End Function

    Private Sub sNewRec()

        ddlJenInv.SelectedIndex = 0
        ddlKat.SelectedIndex = 0
        txtNoPT.Text = ""
        txtNoInv.Text = ""
        txtTkhInv.Text = ""
        txtTkhTInv.Text = ""
        txtNoDO.Text = ""
        txtTkhDO.Text = ""
        txtTkhTDO.Text = ""
        txtJenByr.Text = ""
        txtIdKat.Text = ""
        txtKat.Text = ""
        txtIDPenerima.Text = ""
        txtNmPenerima.Text = ""
        txtNamaOA.Text = ""
        txtKPOA.Text = ""
        txtAlmt1.Text = ""
        txtAlmt2.Text = ""
        txtBandar.Text = ""
        txtPoskod.Text = ""
        txtEmel.Text = ""
        txtNoAkaun.Text = ""
        txtJumInv.Text = ""
        ddlNegeri.SelectedIndex = 0
        ddlNegara.SelectedIndex = 0
        ddlBank.SelectedIndex = 0

        fClearGvTrans()
        sClearGvTransPT()
        sClearGvTransLain()
        txtIdInv.Text = ""
        btnShowMpeNom.Visible = True
        lbtnHapus.Visible = False
        lbtnSimpan.Visible = True
        sSetInv()
    End Sub

    Private Sub fClearGvTrans()
        Try
            gvTrans.DataSource = New List(Of String)
            gvTrans.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ddlKat_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKat.SelectedIndexChanged
        Select Case ddlKat.SelectedValue
            Case "SY"
                trPPT.Visible = True
                trPOA.Visible = False
            Case "ST"
                trPPT.Visible = True
                trPOA.Visible = False
            Case "OA"
                trPPT.Visible = False
                trPOA.Visible = True

        End Select
        sClearPenerima()
        txtIdKat.Text = ddlKat.SelectedValue
        txtKat.Text = ddlKat.SelectedItem.Text


    End Sub

    Private Sub sClearPenerima()
        Try
            txtIDPenerima.Text = ""
            txtNmPenerima.Text = ""
            txtNamaOA.Text = ""
            txtKPOA.Text = ""
            txtAlmt1.Text = ""
            txtAlmt2.Text = ""
            txtBandar.Text = ""
            txtPoskod.Text = ""
            ddlNegeri.SelectedValue = 0
            ddlNegara.Text = ""
            txtEmel.Text = ""
            ddlBank.SelectedValue = 0
            txtNoAkaun.Text = ""


        Catch ex As Exception

        End Try
    End Sub




    Private Sub fBindDdlST()

        Try
            ddlCariST.Items.Clear()
            ddlCariST.Items.Add(New ListItem("No. Staf", "1"))
            ddlCariST.Items.Add(New ListItem("Nama Staf", "2"))

        Catch ex As Exception

        End Try

    End Sub

    Private Sub btnShowMpeNom_ServerClick(sender As Object, e As EventArgs) Handles btnShowMpeNom.ServerClick
        Select Case ddlKat.SelectedValue
            Case "SY"
                txtCariSY.Text = ""
                fClearGvSY()
                tblSY.Visible = True
                tblST.Visible = False
                lblTitle.Text = "Senarai Syarikat"
                ddlCariSY.SelectedValue = 0
            Case "ST"
                txtCariST.Text = ""
                fBindDdlST()
                fClearGvST()
                tblSY.Visible = False
                tblST.Visible = True
                lblTitle.Text = "Senarai Staf"
        End Select

        mpePnlListNom.Show()
    End Sub

    Private Sub gvTransPT_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvTransPT.RowCreated
        Try
            If e.Row.RowType = DataControlRowType.Header Then
                Dim gvHeader As GridView = CType(sender, GridView)
                Dim gvHeaderRow As GridViewRow = New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert)
                Dim tc1 As TableCell = New TableCell()
                tc1.Text = ""
                tc1.ColumnSpan = 7
                tc1.BackColor = System.Drawing.Color.White
                gvHeaderRow.Cells.Add(tc1)

                tc1 = New TableCell()
                tc1.Text = "ASAL"
                tc1.Font.Bold = True
                tc1.HorizontalAlign = HorizontalAlign.Center
                tc1.ColumnSpan = 3
                tc1.BackColor = ColorTranslator.FromHtml("#f4f5f7")
                gvHeaderRow.Cells.Add(tc1)

                tc1 = New TableCell()
                tc1.Text = "BELUM BAYAR"
                tc1.Font.Bold = True
                tc1.HorizontalAlign = HorizontalAlign.Center
                tc1.ColumnSpan = 2
                tc1.BackColor = ColorTranslator.FromHtml("#f4f5f7")
                gvHeaderRow.Cells.Add(tc1)

                tc1 = New TableCell()
                tc1.Text = "AKAN BAYAR"
                tc1.Font.Bold = True
                tc1.HorizontalAlign = HorizontalAlign.Center
                tc1.ColumnSpan = 4
                tc1.BackColor = ColorTranslator.FromHtml("#e8a196")
                gvHeaderRow.Cells.Add(tc1)

                tc1 = New TableCell()
                tc1.Text = ""
                tc1.ColumnSpan = 1
                tc1.BackColor = System.Drawing.Color.White
                gvHeaderRow.Cells.Add(tc1)

                gvHeader.Controls(0).Controls.AddAt(0, gvHeaderRow)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Dim decTotJumByr, decTotJumAsl, decTotJumBaki As Decimal
    Dim strGJumByr, strGJumAsl, strGJumBaki As String

    Private Sub gvTransPT_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvTransPT.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim decJumAkan As Decimal

                Dim ddlKaedah = DirectCast(e.Row.FindControl("ddlKaedah"), DropDownList)
                ddlKaedah.Items.Add(New ListItem("Amaun", "0"))
                ddlKaedah.Items.Add(New ListItem("Peratus", "1"))

                Dim txtPeratus = DirectCast(e.Row.FindControl("txtPeratus"), TextBox)
                txtPeratus.Enabled = False

                'jumlah asal
                Dim lblJumAsl As Label = CType(e.Row.FindControl("lblJumAsl"), Label)
                decTotJumAsl += Decimal.Parse(lblJumAsl.Text)
                strGJumAsl = FormatNumber(decTotJumAsl)

                'Jumlah bayar
                Dim txtJumAkan As TextBox = CType(e.Row.FindControl("txtJumAkan"), TextBox)
                If txtJumAkan.Text = "" Then decJumAkan = 0 Else decJumAkan = CDec(txtJumAkan.Text)
                decTotJumByr += decJumAkan
                strGJumByr = FormatNumber(decTotJumByr)

                'Jumlah baki
                Dim lblJumBlm As Label = CType(e.Row.FindControl("lblJumBlm"), Label)
                decTotJumBaki += Decimal.Parse(lblJumBlm.Text)
                strGJumBaki = FormatNumber(decTotJumBaki)

            ElseIf e.Row.RowType = DataControlRowType.Footer Then
                Dim lblGJumByr As Label = CType(e.Row.FindControl("lblGJumByr"), Label)
                lblGJumByr.Text = strGJumByr.ToString()
            End If

            If Not strGJumByr Is Nothing AndAlso Not strGJumAsl Is Nothing AndAlso Not strGJumBaki Is Nothing Then
                If strGJumByr = strGJumAsl Then
                    txtJenByr.Text = "BAYARAN PENUH"
                    hidJenByr.Value = "BP"
                ElseIf strGJumByr < strGJumAsl AndAlso strGJumByr < strGJumBaki Then
                    txtJenByr.Text = "BAYARAN SEPARA"
                    hidJenByr.Value = "BS"
                ElseIf strGJumByr < strGJumAsl AndAlso strGJumByr = strGJumBaki Then
                    txtJenByr.Text = "BAYARAN AKHIR"
                    hidJenByr.Value = "BA"
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnCariSY_ServerClick(sender As Object, e As EventArgs) Handles btnCariSY.ServerClick
        Try
            If txtCariSY.Text <> "" Then
                fBindGvSY()
            End If
            mpePnlListNom.Show()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub fBindGvSY()
        Dim strSql As String
        Dim intRec As Integer = 0
        Dim strFilter As String
        Try

            Dim dt As New DataTable
            dt.Columns.Add("IDSya", GetType(String))
            dt.Columns.Add("NamaSya", GetType(String))

            If ddlCariSY.SelectedValue = 0 Then  'Carian ID Syarikat
                strFilter = "IDSyarikat like '%" & Trim(txtCariSY.Text.TrimEnd.TrimStart) & "%'"
            ElseIf ddlCariSY.SelectedValue = 1 Then 'Carian Nama Pembekal
                strFilter = "NamaSyarikat like '%" & Trim(txtCariSY.Text.TrimEnd.TrimStart) & "%'"
            End If

            strSql = "select IDSyarikat, NoSyarikat , NamaSyarikat, AlmtP1 , AlmtP2, BandarP , PoskodP , NegeriP , ButiranNegeri , NegaraP , ButiranNegara , Emel, KodBank, NamaBank, NoAkaun 
from VAP_SenaraiPembekal where " & strFilter

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            Dim IDSya, NamaSya As String

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        IDSya = ds.Tables(0).Rows(i)("IDSyarikat").ToString
                        NamaSya = ds.Tables(0).Rows(i)("NamaSyarikat").ToString

                        dt.Rows.Add(IDSya, NamaSya)
                    Next
                    ViewState("dsSya") = ds
                    ViewState("dtST") = dt
                    gvSY.DataSource = dt
                    gvSY.DataBind()

                    intRec = dt.Rows.Count
                End If
            End If

            If intRec = 0 Then
                fGlobalAlert("Tiada rekod!", Me.Page, Me.[GetType]())
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub gvSY_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvSY.RowCommand
        Try
            If e.CommandName = "Select" Then
                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim selectedRow As GridViewRow = gvSY.Rows(index)

                Dim IDSya As String = CType(selectedRow.FindControl("lblIDSya"), Label).Text.TrimEnd

                Dim dsSya As DataSet = TryCast(ViewState("dsSya"), DataSet)
                Dim dvSya As New DataView()

                dvSya = dsSya.Tables(0).DefaultView
                dvSya.RowFilter = "IDSyarikat = '" & IDSya & "'"

                txtIDPenerima.Text = dvSya.Item(0)("IDSyarikat").ToString
                txtNmPenerima.Text = dvSya.Item(0)("NamaSyarikat").ToString
                txtAlmt1.Text = dvSya.Item(0)("AlmtP1").ToString
                txtAlmt2.Text = dvSya.Item(0)("AlmtP2").ToString
                txtBandar.Text = dvSya.Item(0)("BandarP").ToString
                txtPoskod.Text = dvSya.Item(0)("PoskodP").ToString
                ddlNegeri.SelectedValue = dvSya.Item(0)("NegeriP").ToString
                ddlNegara.SelectedValue = dvSya.Item(0)("NegaraP").ToString
                txtEmel.Text = dvSya.Item(0)("Emel").ToString
                ddlBank.SelectedValue = dvSya.Item(0)("KodBank").ToString
                txtNoAkaun.Text = dvSya.Item(0)("NoAkaun").ToString

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnCariST_ServerClick(sender As Object, e As EventArgs) Handles btnCariST.ServerClick
        Try
            If txtCariST.Text <> "" Then
                fBindGvST()
            End If

            mpePnlListNom.Show()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub fBindGvST()

        Dim intRec As Integer = 0
        Dim strFilter As String

        Try

            Dim dt As New DataTable
            dt.Columns.Add("NoStaf", GetType(String))
            dt.Columns.Add("NamaStaf", GetType(String))
            dt.Columns.Add("Pejabat", GetType(String))
            dt.Columns.Add("Email", GetType(String))
            dt.Columns.Add("Bank", GetType(String))
            dt.Columns.Add("NoAkaun", GetType(String))

            If ddlCariST.SelectedValue = 1 Then  'Carian No Staf
                strFilter = "A.MS01_NoStaf = '" & Trim(txtCariST.Text.TrimEnd) & "'"
            ElseIf ddlCariST.SelectedValue = 3 Then 'Carian Nama Staf
                strFilter = "A.MS01_Nama like '%" & Trim(txtCariST.Text.TrimEnd) & "%'"
            End If

            Dim ds As New DataSet
            Dim dbconn As New DBSMConn

            Dim strSql As String = "select A.MS01_NoStaf, A.MS01_Nama, C.Pejabat, A.MS01_Email, D.NamaBank, MS01_NoAkaun from MS01_Peribadi A
									inner join MS08_Penempatan B on B.MS01_NoStaf = A.MS01_NoStaf 
									inner join  MS_Pejabat C on C.KodPejabat = B.MS08_Pejabat
									inner join MS_Bank D on D.KodBank  = A.MS01_KodBank   
									where  A.MS01_Status = 1 and C.Status = 1 and " & strFilter & "
									order by A.MS01_Nama"

            ds = dbconn.fselectCommand(strSql)

            Dim NoStaf, NamaStaf, Pejabat, Email, strBank, strNoAkaun As String

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        NoStaf = ds.Tables(0).Rows(i)("MS01_NoStaf").ToString
                        NamaStaf = ds.Tables(0).Rows(i)("MS01_Nama").ToString
                        Pejabat = ds.Tables(0).Rows(i)("Pejabat").ToString
                        Email = ds.Tables(0).Rows(i)("MS01_Email").ToString
                        strBank = ds.Tables(0).Rows(i)("NamaBank").ToString
                        strNoAkaun = ds.Tables(0).Rows(i)("MS01_NoAkaun").ToString

                        dt.Rows.Add(NoStaf, NamaStaf, Pejabat, Email, strBank, strNoAkaun)
                    Next


                    ViewState("dsST") = ds
                    gvST.DataSource = dt
                    gvST.DataBind()
                    ViewState("dtST") = dt
                    intRec = dt.Rows.Count
                End If
            End If

            If intRec = 0 Then
                fGlobalAlert("Tiada rekod!", Me.Page, Me.[GetType]())
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub gvST_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvST.RowCommand
        Try
            If e.CommandName = "Select" Then
                Dim strKodBank As String
                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim selectedRow As GridViewRow = gvST.Rows(index)

                txtIDPenerima.Text = CType(selectedRow.FindControl("lblnostaf"), Label).Text.TrimEnd
                txtNmPenerima.Text = CType(selectedRow.FindControl("lblnamastaf"), Label).Text.TrimEnd
                txtAlmt1.Text = CType(selectedRow.FindControl("lblpejabat"), Label).Text.TrimEnd
                txtEmel.Text = CType(selectedRow.FindControl("lblemail"), Label).Text.TrimEnd

                'Dim strBank As String = CType(selectedRow.FindControl("lblBank"), Label).Text.TrimEnd

                'Select Case strBank
                '    Case "BUMIPUTRA COMMERCE BERHAD"
                '        strKodBank = "CIBBMYKL"
                '    Case "AFFIN BANK BERHAD"
                '        strKodBank = "PHBMMYKL"
                '    Case "ALLIANCE BANK BERHAD"
                '        strKodBank = "MFBBMYKL"
                '    Case "MAYBANK BERHAD"
                '        strKodBank = ""
                '    Case "BANK ISLAM MALAYSIA BERHAD"
                '        strKodBank = ""
                '    Case "BANK MUAMALAT MALAYSIA BERHAD"
                '        strKodBank = ""
                '    Case "PUBLIC BANK BERHAD"
                '        strKodBank = ""
                '    Case "RHB BANK BERHAD"
                '        strKodBank = ""
                '    Case "EON BANK BERHAD"
                '        strKodBank = ""
                '    Case "HONG LEONG BANK BERHAD"
                '        strKodBank = ""
                '    Case "OCBC BANK BERHAD"
                '        strKodBank = ""
                'End Select

                'txtNoAkaun.Text = CType(selectedRow.FindControl("lblNoAkaun"), Label).Text.TrimEnd



                sLoadAddrUtem()
                txtJumInv.Text = ""
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub sLoadAddrUtem()
        Try
            Dim ds As New DataSet
            ds = fGetAddrUtem()

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    txtAlmt2.Text = ds.Tables(0).Rows(0)("Nama").ToString
                    txtBandar.Text = ds.Tables(0).Rows(0)("Bandar").ToString
                    txtPoskod.Text = ds.Tables(0).Rows(0)("Poskod").ToString
                    ddlNegeri.SelectedValue = ds.Tables(0).Rows(0)("KodNegeri").ToString
                    ddlNegara.SelectedValue = ds.Tables(0).Rows(0)("KodNegara").ToString
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvST_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvST.PageIndexChanging
        Try
            gvST.PageIndex = e.NewPageIndex
            If ViewState("dtST") IsNot Nothing Then
                gvST.DataSource = ViewState("dtST")
                gvST.DataBind()
            Else
                fBindGvST()
            End If
            mpePnlListNom.Show()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvSY_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvSY.PageIndexChanging
        Try
            gvSY.PageIndex = e.NewPageIndex
            If ViewState("dtST") IsNot Nothing Then
                gvSY.DataSource = ViewState("dtST")
                gvSY.DataBind()
            Else
                fBindGvSY()
            End If
            mpePnlListNom.Show()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvPT_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvPT.PageIndexChanging
        Try
            gvPT.PageIndex = e.NewPageIndex
            If ViewState("dtPT") IsNot Nothing Then
                gvPT.DataSource = ViewState("dtPT")
                gvPT.DataBind()
            Else
                fBindGvPT()
            End If
            mpePnlListNom.Show()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub sInitGvTrans()
        Try
            Dim dt As DataTable = New DataTable()
            Dim dr As DataRow = Nothing
            dt.Columns.Add(New DataColumn("KW", GetType(String)))
            dt.Columns.Add(New DataColumn("KO", GetType(String)))
            dt.Columns.Add(New DataColumn("PTj", GetType(String)))
            dt.Columns.Add(New DataColumn("KP", GetType(String)))
            dt.Columns.Add(New DataColumn("Vot", GetType(String)))
            dt.Columns.Add(New DataColumn("Butiran", GetType(String)))
            dt.Columns.Add(New DataColumn("Harga", GetType(String)))
            dt.Columns.Add(New DataColumn("Kuantiti", GetType(String)))
            dt.Columns.Add(New DataColumn("Jumlah", GetType(String)))

            'dr = dt.NewRow()
            'dr("KW") = 0
            'dr("KO") = 0
            'dr("PTj") = 0
            'dr("KP") = 0
            'dr("Vot") = 0
            'dr("Butiran") = String.Empty
            'dr("Harga") = "0.00"
            'dr("Kuantiti") = 0
            'dr("Jumlah") = "0.00"

            'dt.Rows.Add(dr)
            ViewState("dtTrans") = dt
            gvTrans.DataSource = dt
            gvTrans.DataBind()
        Catch ex As Exception

        End Try

    End Sub

    Dim decTotJumTrans As Decimal
    Dim strTotJumByr As String
    Private Sub gvTrans_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvTrans.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim strJumlah As String = CType(e.Row.FindControl("lblJumlah"), Label).Text
                If strJumlah = "" Then strJumlah = "0.00"
                decTotJumTrans += CDec(strJumlah)


                Dim ddlKW = DirectCast(e.Row.FindControl("ddlKW"), DropDownList)
                ddlKW.DataSource = ViewState("dsKW")
                ddlKW.DataTextField = "ButiranKW"
                ddlKW.DataValueField = "KodKw"
                ddlKW.DataBind()
                ddlKW.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
                ddlKW.SelectedValue = TryCast(e.Row.FindControl("hidKW"), HiddenField).Value

                Dim ddlKO = DirectCast(e.Row.FindControl("ddlKO"), DropDownList)
                Dim strSelKO As String = TryCast(e.Row.FindControl("hidKO"), HiddenField).Value

                If strSelKO = "0" OrElse strSelKO = "" Then
                    ddlKO.Items.Insert(0, New ListItem("- SILA PILIH KW -", 0))
                    ddlKO.SelectedValue = strSelKO

                Else
                    ddlKO.DataSource = ViewState("dsKO")
                    ddlKO.DataTextField = "Butiran"
                    ddlKO.DataValueField = "KodKO"
                    ddlKO.DataBind()

                    ddlKO.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
                    ddlKO.SelectedValue = TryCast(e.Row.FindControl("hidKO"), HiddenField).Value
                End If

                Dim ddlPTj = DirectCast(e.Row.FindControl("ddlPTj"), DropDownList)

                Dim strSelPTj As String = TryCast(e.Row.FindControl("hidPTj"), HiddenField).Value

                If strSelPTj = "0" OrElse strSelPTj = "" Then
                    ddlPTj.Items.Insert(0, New ListItem("- SILA PILIH KO -", 0))
                    ddlPTj.SelectedValue = strSelPTj
                Else
                    ddlPTj.DataSource = ViewState("dsPTj")
                    ddlPTj.DataTextField = "Butiran"
                    ddlPTj.DataValueField = "KodPTj"
                    ddlPTj.DataBind()
                    ddlPTj.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
                    ddlPTj.SelectedValue = strSelPTj
                End If

                Dim ddlKP = DirectCast(e.Row.FindControl("ddlKP"), DropDownList)
                Dim strSelKP As String = TryCast(e.Row.FindControl("hidKP"), HiddenField).Value

                If strSelKP = "0" OrElse strSelKP = "" Then
                    ddlKP.Items.Insert(0, New ListItem("- SILA PILIH PTj -", 0))
                    ddlKP.SelectedValue = strSelKP
                Else
                    ddlKP.DataSource = ViewState("dsKP")
                    ddlKP.DataTextField = "Butiran"
                    ddlKP.DataValueField = "KodKP"
                    ddlKP.DataBind()

                    ddlKP.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
                    ddlKP.SelectedValue = strSelKP
                End If

                Dim ddlVot = DirectCast(e.Row.FindControl("ddlVot"), DropDownList)
                Dim strSelVot As String = TryCast(e.Row.FindControl("hidVot"), HiddenField).Value

                If strSelVot = "0" OrElse strSelVot = "" Then
                    ddlVot.Items.Insert(0, New ListItem("- SILA PILIH KP -", 0))
                    ddlVot.SelectedValue = strSelVot
                Else
                    ddlVot.DataSource = ViewState("dsVot")
                    ddlVot.DataTextField = "Butiran"
                    ddlVot.DataValueField = "KodVot"
                    ddlVot.DataBind()

                    ddlVot.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
                    ddlVot.SelectedValue = strSelVot
                End If

            ElseIf e.Row.RowType = DataControlRowType.Footer Then
                Dim lblTotJum As Label = CType(e.Row.FindControl("lblTotJum"), Label)
                lblTotJum.Text = FormatNumber(decTotJumTrans)

                txtJumInv.Text = FormatNumber(decTotJumTrans)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub fLoadKW()
        Try

            Dim strSql As String = "Select KodKw,Butiran,(KodKw + ' - ' + Butiran ) as ButiranKW from MK_Kw order by KodKw"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strSql)

            ViewState("dsKW") = ds

        Catch ex As Exception

        End Try
    End Sub

    Private Sub fLoadKO()
        Try
            Dim strSql As String = "SELECT DISTINCT TOP 100 PERCENT dbo.MK_kodOperasi.KodKO, (dbo.MK_kodOperasi.KodKO + ' - ' + dbo.MK_kodOperasi.Butiran) as Butiran " &
                "FROM dbo.MK_kodOperasi INNER JOIN dbo.MK01_VotTahun ON dbo.MK_kodOperasi.KodKO = dbo.MK01_VotTahun.KodKO " &
                "where dbo.MK01_VotTahun.MK01_Tahun = YEAR(GETDATE()) ORDER BY dbo.MK_kodOperasi.Kodko"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strSql)
            ViewState("dsKO") = ds

        Catch ex As Exception

        End Try
    End Sub

    Private Sub fLoadPTj()
        Try
            Dim strSql As String = "SELECT DISTINCT   MK_PTJ.KodPTJ  , (MK_PTJ.KodPTJ   + ' - ' + MK_PTJ.Butiran  ) as Butiran  
                    From MK_PTJ INNER Join MK01_VotTahun ON MK_PTJ.KodPTJ   = MK01_VotTahun.KodPTJ   
                    where MK01_VotTahun.MK01_Tahun = YEAR(GETDATE()) and MK_PTJ.KodPTJ <> '-' and MK_PTJ.KodKategoriPTJ = 'P' and MK_PTJ.status = 1 
ORDER BY MK_PTJ.KodPTJ "

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strSql)
            ViewState("dsPTj") = ds

        Catch ex As Exception

        End Try
    End Sub

    Private Sub fLoadKP()
        Try

            Dim strSql As String = "SELECT DISTINCT TOP 100 PERCENT MK_KodProjek.KodProjek as KodKP, (MK_KodProjek .KodProjek  + ' - ' + MK_KodProjek.Butiran ) as Butiran  
                    From MK_KodProjek INNER Join MK01_VotTahun ON MK_KodProjek.KodProjek  = MK01_VotTahun.KodKP
                    where MK01_VotTahun.MK01_Tahun = YEAR(GETDATE())  ORDER BY MK_KodProjek.KodProjek"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strSql)
            ViewState("dsKP") = ds

        Catch ex As Exception

        End Try
    End Sub

    Private Sub fLoadVot()
        Try

            Dim strSql As String = "SELECT DISTINCT A.KodVot, (A.KODVOT + ' - ' + B.BUTIRAN) as Butiran FROM MK03_AmTahun A INNER JOIN MK_VOT B ON A.KODVOT = B.KODVOT 
Where a.MK03_TAHUN = YEAR(GETDATE()) AND  RIGHT(A.KODVOT,2) <> '00' ORDER BY A.KODVOT"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strSql)
            ViewState("dsVot") = ds


        Catch ex As Exception

        End Try
    End Sub

    Private Sub sGetTotal()
        Try
            Dim decTotJumAkan, decTotJumAsl, decTotJumBlm As Decimal
            For Each gvRow As GridViewRow In gvTransPT.Rows
                Dim decJumAkan, decJumAsl, decJumBlm As Decimal

                Dim strJumAkan As String = Trim(CType(gvRow.FindControl("txtJumAkan"), TextBox).Text.TrimEnd)
                If strJumAkan = "" Then decJumAkan = 0 Else decJumAkan = CDec(strJumAkan)


                Dim strJumAsl As String = Trim(CType(gvRow.FindControl("lblJumAsl"), Label).Text.TrimEnd)
                If strJumAsl = "" Then decJumAsl = 0 Else decJumAsl = CDec(strJumAsl)


                Dim strJumBlm As String = Trim(CType(gvRow.FindControl("lblJumBlm"), Label).Text.TrimEnd)
                If strJumBlm = "" Then decJumBlm = 0 Else decJumBlm = CDec(strJumBlm)

                decTotJumAkan += decJumAkan
                decTotJumAsl += decJumAsl
                decTotJumBlm += decJumBlm
            Next

            Dim strJumByr As String = FormatNumber(decTotJumAkan, 2)

            Dim footerRow = gvTransPT.FooterRow
            CType(footerRow.FindControl("lblGJumByr"), Label).Text = strJumByr

            txtJumInv.Text = strJumByr


            If decTotJumAkan = decTotJumAsl Then
                txtJenByr.Text = "BAYARAN PENUH"
                hidJenByr.Value = "BP"
            ElseIf decTotJumAkan < decTotJumAsl AndAlso decTotJumAkan < decTotJumBlm Then
                txtJenByr.Text = "BAYARAN SEPARA"
                hidJenByr.Value = "BS"
            ElseIf decTotJumAkan < decTotJumAsl AndAlso decTotJumAkan = decTotJumBlm Then
                txtJenByr.Text = "BAYARAN AKHIR"
                hidJenByr.Value = "BA"
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub txtQtyByr_TextChanged(sender As Object, e As EventArgs)
        Try
            Dim intQtyByr As Integer

            Dim txtQtyByr As TextBox = CType(sender, TextBox)
            Dim gvr As GridViewRow = CType(txtQtyByr.Parent.Parent, GridViewRow)
            Dim rowindex As Integer = gvr.RowIndex
            Dim selectedRow As GridViewRow = gvTransPT.Rows(gvr.RowIndex)

            Dim intQtyBlm As Integer = CInt(CType(selectedRow.FindControl("lblQtyBlm"), Label).Text.TrimEnd)

            If txtQtyByr.Text = "" Then
                txtQtyByr.Text = intQtyBlm
            End If
            intQtyByr = CInt(txtQtyByr.Text)
            If intQtyByr > intQtyBlm Then
                fGlobalAlert("Kuantiti yang dimasukkan melebihi kuantiti belum bayar! Sila masukkan kuantiti yang sama atau kurang.", Me.Page, Me.[GetType]())
                txtQtyByr.Text = intQtyBlm
                Exit Sub
            End If


            Dim decHrgAsl As Decimal = CDec(CType(selectedRow.FindControl("lblHargaAsl"), Label).Text.TrimEnd)

            Dim decJumByr As Decimal = (decHrgAsl * intQtyByr)
            CType(selectedRow.FindControl("txtJumAkan"), TextBox).Text = decJumByr

            sGetTotal()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub txtPeratus_TextChanged(sender As Object, e As EventArgs)
        Try
            Dim txtPeratus As TextBox = CType(sender, TextBox)
            Dim gvr As GridViewRow = CType(txtPeratus.Parent.Parent, GridViewRow)
            Dim rowindex As Integer = gvr.RowIndex
            Dim selectedRow As GridViewRow = gvTransPT.Rows(gvr.RowIndex)

            Dim decJumBlm As Decimal = CDec(CType(selectedRow.FindControl("lblJumBlm"), Label).Text.TrimEnd)
            Dim decPeratus As Decimal = CDec(txtPeratus.Text)

            Dim decJumByr As Decimal = (decPeratus * decJumBlm) / 100
            CType(selectedRow.FindControl("txtJumAkan"), TextBox).Text = decJumByr

            sGetTotal()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub txtJumAkan_TextChanged(sender As Object, e As EventArgs)

        Try
            Dim txtJumAkan As TextBox = CType(sender, TextBox)
            Dim gvr As GridViewRow = CType(txtJumAkan.Parent.Parent, GridViewRow)
            Dim rowindex As Integer = gvr.RowIndex
            Dim selectedRow As GridViewRow = gvTransPT.Rows(gvr.RowIndex)

            Dim decJumBlm As String = CDec(CType(selectedRow.FindControl("lblJumBlm"), Label).Text.TrimEnd)

            If txtJumAkan.Text = "" Then
                txtJumAkan.Text = decJumBlm
            End If

            Dim decJumAkan As Decimal = CDec(txtJumAkan.Text)
            If decJumAkan > decJumBlm Then
                fGlobalAlert("Jumlah Akan Bayar yang dimasukkan melebihi Jumlah Belum Bayar! Sila masukkan jumlah yang sama atau kurang.", Me.Page, Me.[GetType]())
                txtJumAkan.Text = decJumBlm
                Exit Sub
            End If

            txtJumAkan.Text = FormatNumber(decJumAkan, 2)

            sGetTotal()

        Catch ex As Exception

        End Try


    End Sub





    Private Sub fAddRow()
        Try
            Dim dtTrans As DataTable = fLoadDtTrans()
            Dim drCurrentRow As DataRow = Nothing
            drCurrentRow = dtTrans.NewRow()
            dtTrans.Rows.Add(drCurrentRow)
            gvTrans.DataSource = dtTrans
            gvTrans.DataBind()
            SetPreviousData(dtTrans)

        Catch ex As Exception

        End Try
    End Sub

    Private Function fLoadDtTrans() As DataTable
        Try
            Dim strJumlah As String
            Dim rowIndex As Integer = 0
            Dim dtTrans As DataTable = CType(ViewState("dtTrans"), DataTable)
            For i As Integer = 0 To dtTrans.Rows.Count - 1
                Dim gvRow As GridViewRow = gvTrans.Rows(rowIndex)

                dtTrans.Rows(i)("KW") = CType(gvRow.FindControl("ddlKW"), DropDownList).SelectedValue.TrimEnd
                dtTrans.Rows(i)("KO") = CType(gvRow.FindControl("ddlKO"), DropDownList).SelectedValue.TrimEnd
                dtTrans.Rows(i)("PTj") = CType(gvRow.FindControl("ddlPTj"), DropDownList).SelectedValue.TrimEnd
                dtTrans.Rows(i)("KP") = CType(gvRow.FindControl("ddlKP"), DropDownList).SelectedValue.TrimEnd
                dtTrans.Rows(i)("Vot") = CType(gvRow.FindControl("ddlVot"), DropDownList).SelectedValue.TrimEnd
                dtTrans.Rows(i)("Butiran") = CType(gvRow.FindControl("txtButiran"), TextBox).Text.TrimEnd
                dtTrans.Rows(i)("Harga") = CType(gvRow.FindControl("txtHarga"), TextBox).Text.TrimEnd
                dtTrans.Rows(i)("Kuantiti") = CType(gvRow.FindControl("txtKttByr"), TextBox).Text.TrimEnd
                dtTrans.Rows(i)("Jumlah") = CType(gvRow.FindControl("lblJumlah"), Label).Text.TrimEnd

                strJumlah = CType(gvRow.FindControl("lblJumlah"), Label).Text.TrimEnd
                If strJumlah = "" Then strJumlah = "0.00"
                dtTrans.Rows(i)("Jumlah") = CDec(strJumlah)

                rowIndex += 1
            Next
            Return dtTrans
        Catch ex As Exception

        End Try
    End Function

    Private Sub SetPreviousData(ByVal dtTrans As DataTable)
        Try
            Dim rowIndex As Integer = 0
            If dtTrans.Rows.Count > 0 Then
                For i As Integer = 0 To dtTrans.Rows.Count - 1
                    Dim ddl1 As DropDownList = CType(gvTrans.Rows(rowIndex).Cells(1).FindControl("ddlKW"), DropDownList)
                    Dim ddl2 As DropDownList = CType(gvTrans.Rows(rowIndex).Cells(2).FindControl("ddlKO"), DropDownList)
                    Dim ddl3 As DropDownList = CType(gvTrans.Rows(rowIndex).Cells(1).FindControl("ddlPTj"), DropDownList)
                    Dim ddl4 As DropDownList = CType(gvTrans.Rows(rowIndex).Cells(2).FindControl("ddlKP"), DropDownList)
                    Dim ddl5 As DropDownList = CType(gvTrans.Rows(rowIndex).Cells(1).FindControl("ddlVot"), DropDownList)
                    Dim box1 As TextBox = CType(gvTrans.Rows(rowIndex).Cells(3).FindControl("txtButiran"), TextBox)
                    Dim box2 As TextBox = CType(gvTrans.Rows(rowIndex).Cells(3).FindControl("txtHarga"), TextBox)
                    Dim box3 As TextBox = CType(gvTrans.Rows(rowIndex).Cells(3).FindControl("txtKttByr"), TextBox) ' CType(gvTrans.FindControl("txtKttByr"), TextBox).Text.TrimEnd
                    Dim box4 As Label = CType(gvTrans.Rows(rowIndex).Cells(3).FindControl("lblJumlah"), Label)

                    ddl1.SelectedValue = dtTrans.Rows(i)("KW").ToString()
                    ddl2.SelectedValue = dtTrans.Rows(i)("KO").ToString()
                    ddl3.SelectedValue = dtTrans.Rows(i)("PTj").ToString()
                    ddl4.SelectedValue = dtTrans.Rows(i)("KP").ToString()
                    ddl5.SelectedValue = dtTrans.Rows(i)("Vot").ToString()
                    box1.Text = dtTrans.Rows(i)("Butiran").ToString()
                    box2.Text = dtTrans.Rows(i)("Harga").ToString()
                    box3.Text = dtTrans.Rows(i)("Kuantiti").ToString()
                    box4.Text = dtTrans.Rows(i)("Jumlah").ToString()
                    rowIndex += 1
                Next
                ViewState("dtTrans") = dtTrans
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Function fGetKO(ByVal strKodKW As String) As DataSet
        Try
            Dim strSql As String = "SELECT DISTINCT TOP 100 PERCENT dbo.MK_kodOperasi.KodKO, (dbo.MK_kodOperasi.KodKO + ' - ' + dbo.MK_kodOperasi.Butiran) as ButiranKO " &
                "FROM dbo.MK_kodOperasi INNER JOIN dbo.MK01_VotTahun ON dbo.MK_kodOperasi.KodKO = dbo.MK01_VotTahun.KodKO " &
                "where dbo.MK01_VotTahun.MK01_Tahun = YEAR(GETDATE()) and dbo.mk01_votTahun.KodKw = '" & strKodKW & "' ORDER BY dbo.MK_kodOperasi.Kodko"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strSql)

            Return ds

        Catch ex As Exception

        End Try
    End Function


    Protected Sub lbtnTambah_Click(sender As Object, e As EventArgs)
        fAddRow()
    End Sub

    Private Function fGetPTj(ByVal strKodKW As String, ByVal strKodKO As String) As DataSet
        Try


            Dim strSql As String = "SELECT DISTINCT   MK_PTJ.KodPTJ  , (MK_PTJ.KodPTJ   + ' - ' + MK_PTJ.Butiran  ) as ButiranPTj  
                    From MK_PTJ INNER Join MK01_VotTahun ON MK_PTJ.KodPTJ   = MK01_VotTahun.KodPTJ   
                    where MK01_VotTahun.MK01_Tahun = YEAR(GETDATE()) and MK01_VotTahun.KodKw = '" & strKodKW & "' and MK01_VotTahun.KodKO = '" & strKodKO & "' and MK_PTJ.KodPTJ <> '-' and MK_PTJ.KodKategoriPTJ = 'P' and MK_PTJ.status = 1 
ORDER BY MK_PTJ.KodPTJ "

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strSql)

            Return ds

        Catch ex As Exception

        End Try
    End Function

    Private Function fGetKP(ByVal strKodKW As String, ByVal strKodKO As String, ByVal strKodPTj As String) As DataSet
        Try

            Dim strSql As String = "SELECT DISTINCT TOP 100 PERCENT MK_KodProjek.KodProjek , (MK_KodProjek .KodProjek  + ' - ' + MK_KodProjek.Butiran ) as Butiran  
                    From MK_KodProjek INNER Join MK01_VotTahun ON MK_KodProjek.KodProjek  = MK01_VotTahun.KodKP
                    where MK01_VotTahun.MK01_Tahun = YEAR(GETDATE()) and MK01_VotTahun.KodKw = '" & strKodKW & "' and MK01_VotTahun.KodKO = '" & strKodKO & "' and MK01_VotTahun .KodPTJ = '" & strKodPTj & "' ORDER BY MK_KodProjek.KodProjek"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strSql)
            'ViewState("dsKP") = ds
            Return ds

        Catch ex As Exception

        End Try
    End Function

    Private Function fGetVot(ByVal strKodKW As String, ByVal strKodKO As String, ByVal strKodPTj As String, ByVal strKodKP As String) As DataSet
        Try

            Dim strSql As String = "SELECT DISTINCT A.KodVot, (A.KODVOT + ' - ' + B.BUTIRAN) as Butiran FROM MK03_AmTahun A INNER JOIN MK_VOT B ON A.KODVOT = B.KODVOT 
Where a.MK03_TAHUN = YEAR(GETDATE()) AND A.KodKw = '" & strKodKW & "' and A.KodKO = '" & strKodKO & "' AND A.KODPTJ = '" & strKodPTj & "' AND A.KodKP  = '" & strKodKP & "' AND  RIGHT(A.KODVOT,2) <> '00' ORDER BY A.KODVOT"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strSql)
            'ViewState("dsKP") = ds
            Return ds

        Catch ex As Exception

        End Try
    End Function

    Private Sub gvTrans_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvTrans.RowDeleting
        Try
            Dim index As Integer = Convert.ToInt32(e.RowIndex)
            If index > 0 Then
                Dim dtTrans As DataTable = fLoadDtTrans()
                dtTrans.Rows(index).Delete()

                If dtTrans.Rows.Count = 0 Then
                    gvTrans.DataSource = fInitDtTrans()
                    gvTrans.DataBind()
                Else
                    gvTrans.DataSource = dtTrans
                    gvTrans.DataBind()

                    SetPreviousData(dtTrans)
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub fClearGvLstInv()
        gvLstInv.DataSource = New List(Of String)
        gvLstInv.DataBind()
    End Sub

    Private Sub gvLstInv_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvLstInv.PageIndexChanging
        Try
            gvLstInv.PageIndex = e.NewPageIndex
            If ViewState("dtList") IsNot Nothing Then
                gvLstInv.DataSource = ViewState("dtList")
                gvLstInv.DataBind()
            End If

        Catch ex As Exception

        End Try
    End Sub



    Private Sub fLoadLstInv()
        Dim intRec As Integer
        Dim strFilter As String
        Try
            fClearGvLstInv()

            Dim dt As New DataTable
            dt.Columns.Add("IdInv", GetType(String))
            dt.Columns.Add("NoInv", GetType(String))
            dt.Columns.Add("NmNom", GetType(String))
            dt.Columns.Add("NoPT", GetType(String))
            dt.Columns.Add("NoDO", GetType(String))
            dt.Columns.Add("JenInv", GetType(String))
            dt.Columns.Add("Jumlah", GetType(String))
            dt.Columns.Add("Padan", GetType(Boolean))

            If ddlTapisan.SelectedIndex = 0 Then
                strFilter = ""
            Else
                strFilter = "And AP01_JenInv  = '" & ddlTapisan.SelectedValue & "'"
            End If

            Dim strSql As String = "Select AP01_NoId, AP01_NoInv, PO19_NoPt, AP01_IdPenerima, AP01_Penerima, AP01_Jumlah, AP01_TkhInv, AP01_TkhTInv, AP01_NoDo, AP01_TkhDO, AP01_TkhTDO , AP01_Padan , AP01_JenInv , KategoriPenerima ,AP01_JenByr ,ButJenByr ,AP09_StatusDok , ButStatDok 
From VAP_Invois Where AP09_StatusDok = '01' AND AP01_StatusTng = 1 " & strFilter

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            Dim strIdInv, strNoInv, strNoPT, strNoDO, strJumlah As String
            Dim decAmaun As Decimal
            Dim blnPadan As Boolean
            Dim strIdNom, strNmNom As String
            Dim strJenInv As String

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        strIdInv = ds.Tables(0).Rows(i)("AP01_NoId")
                        strNoInv = ds.Tables(0).Rows(i)("AP01_NoInv")
                        strIdNom = ds.Tables(0).Rows(i)("AP01_IdPenerima")
                        strNmNom = ds.Tables(0).Rows(i)("AP01_Penerima")
                        strNoPT = ds.Tables(0).Rows(i)("PO19_NoPt")
                        strNoDO = ds.Tables(0).Rows(i)("AP01_NoDo")

                        decAmaun = CDec(ds.Tables(0).Rows(i)("AP01_Jumlah"))
                        strJumlah = FormatNumber(decAmaun, 2)
                        blnPadan = CBool(ds.Tables(0).Rows(i)("AP01_Padan"))
                        strJenInv = ds.Tables(0).Rows(i)("AP01_JenInv")

                        dt.Rows.Add(strIdInv, strNoInv, strNmNom, strNoPT, strNoDO, strJenInv, strJumlah, blnPadan)
                    Next
                    gvLstInv.DataSource = dt
                    gvLstInv.DataBind()
                    ViewState("dtList") = dt
                    intRec = ds.Tables(0).Rows.Count

                    Dim dvList = New DataView(ds.Tables(0))
                    ViewState("dvList") = dvList.Table
                End If
            End If

            If intRec = 0 Then
                fGlobalAlert("Tiada rekod!", Me.Page, Me.[GetType]())
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Function fGetNamaSya(ByVal strIdSya As String) As String
        Dim strNamaSya As String
        Try
            Dim strSql As String = "Select ROC01_NamaSya From ROC01_Syarikat Where ROC01_IDSya = '" & strIdSya & "'"
            Dim dbconn As New DBKewConn
            Dim ds As New DataSet
            ds = dbconn.fSelectCommand(strSql)
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    strNamaSya = ds.Tables(0).Rows(0)("ROC01_NamaSya").ToString
                Else
                    strNamaSya = "-"
                End If
            Else
                strNamaSya = "-"
            End If
            Return strNamaSya
        Catch ex As Exception

        End Try
    End Function
    Private Sub gvLstInv_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvLstInv.RowCommand
        Try
            If e.CommandName = "Select" Then
                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim selectedRow As GridViewRow = gvLstInv.Rows(index)

                Dim strIdInv As String = Trim(CType(selectedRow.FindControl("lblIdInv"), Label).Text.TrimEnd)
                Dim blnPadan As Boolean = CType(selectedRow.FindControl("lblPadan"), Label).Text

                Dim dv As DataView = New DataView(ViewState("dvList"))
                dv.RowFilter = "AP01_NoId = '" & strIdInv & "'"

                Dim strJenInv As String = dv.Item(0)("AP01_JenInv").ToString
                Dim strKat As String = dv.Item(0)("KategoriPenerima").ToString
                Dim strNoPT As String = dv.Item(0)("PO19_NoPt").ToString
                Dim strNoInv As String = dv.Item(0)("AP01_NoInv").ToString
                Dim dtTkhInv As Date = dv.Item(0)("AP01_TkhInv").ToString
                Dim dtTkhTInv As Date = dv.Item(0)("AP01_TkhTInv").ToString
                Dim strNoDO As String = dv.Item(0)("AP01_NoDo").ToString

                Dim strTkhDO As String = dv.Item(0)("AP01_TkhDO")
                Dim dtTkhDO As Date
                If strTkhDO <> "" Then
                    dtTkhDO = CDate(strTkhDO)
                    strTkhDO = dtTkhDO.ToString("dd/MM/yyy")
                End If

                Dim strTkhTDO As String = dv.Item(0)("AP01_TkhTDO").ToString
                Dim dtTkhTDO As Date
                If strTkhTDO <> "" Then
                    dtTkhTDO = CDate(strTkhTDO)
                    strTkhTDO = dtTkhTDO.ToString("dd/MM/yyy")
                End If

                Dim strJenByr As String = dv.Item(0)("ButJenByr").ToString

                txtIdInv.Text = strIdInv
                ddlJenInv.SelectedValue = strJenInv
                ddlKat.SelectedValue = strKat
                txtNoPT.Text = strNoPT
                txtNoInv.Text = strNoInv
                txtTkhInv.Text = dtTkhInv.ToString("dd/MM/yyy")
                txtTkhTInv.Text = dtTkhTInv.ToString("dd/MM/yyy")
                txtNoDO.Text = strNoDO
                txtTkhDO.Text = strTkhDO
                txtTkhTDO.Text = strTkhTDO
                txtJenByr.Text = strJenByr

                'fLoadInv(strIdInv)
                fLoadNominees(strIdInv)

                sSetInv()

                If strJenInv = "ILL" Then
                    fLoadInvDtILL(strIdInv)
                Else
                    fLoadInvDtIPT(strIdInv)
                End If

                If blnPadan = True Then
                    lbtnSimpan.Visible = False
                Else
                    lbtnSimpan.Visible = True
                End If

                btnShowMpeNom.Visible = False
                lbtnHapus.Visible = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub txtKttByr_TextChanged(sender As Object, e As EventArgs)
        Try

            Dim txtKtt As TextBox = CType(sender, TextBox)
            Dim gvr As GridViewRow = CType(txtKtt.Parent.Parent, GridViewRow)
            Dim rowindex As Integer = gvr.RowIndex
            Dim selectedRow As GridViewRow = gvTrans.Rows(gvr.RowIndex)
            Dim strHarga As String = CDec(CType(selectedRow.FindControl("txtHarga"), TextBox).Text.TrimEnd)
            If strHarga = "" Then strHarga = 0.00
            Dim decHarga As Decimal = CDec(strHarga)

            Dim intKtt As Integer = CInt(txtKtt.Text)

            Dim decJumlah As Decimal = CDec(decHarga * intKtt)

            CType(selectedRow.FindControl("lblJumlah"), Label).Text = FormatNumber(decJumlah, 2)
            fSetFooterTrans()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub fSetFooterTrans()
        Try
            Dim decJumTrans As Decimal
            For Each gvRow As GridViewRow In gvTrans.Rows
                Dim decJumlah As Decimal = Trim(CType(gvRow.FindControl("lblJumlah"), Label).Text.TrimEnd)
                decJumTrans += decJumlah
            Next

            Dim strTotJumlah As String = FormatNumber(decJumTrans, 2)

            Dim footerRow = gvTrans.FooterRow
            CType(footerRow.FindControl("lblTotJum"), Label).Text = strTotJumlah

            '  txtJumlah.Text = FormatNumber(txtJumlah.Text, 2)
            txtJumInv.Text = strTotJumlah
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub txtHarga_TextChanged(sender As Object, e As EventArgs)
        Try

            Dim txtHarga As TextBox = CType(sender, TextBox)
            Dim gvr As GridViewRow = CType(txtHarga.Parent.Parent, GridViewRow)
            Dim rowindex As Integer = gvr.RowIndex
            Dim selectedRow As GridViewRow = gvTrans.Rows(gvr.RowIndex)
            Dim strKtt As String = CType(selectedRow.FindControl("txtKttByr"), TextBox).Text.TrimEnd
            If strKtt = "" Then strKtt = 0
            Dim intKtt As Integer = CInt(strKtt)
            Dim decHarga As Decimal = CDec(txtHarga.Text)
            Dim decJumlah As Decimal = CDec(decHarga * intKtt)
            txtHarga.Text = FormatNumber(decHarga, 2)
            CType(selectedRow.FindControl("lblJumlah"), Label).Text = FormatNumber(decJumlah, 2)
            fSetFooterTrans()

        Catch ex As Exception

        End Try
    End Sub

    Private Function fLoadInvDtIPT(ByVal strIdInv As String)
        Try
            Dim dt As New DataTable
            dt = fGetDtTransIPT()

            Dim strSql As String = "select A.PO19_PtDtId, A.KodKw, A.KodKO , A.KodPtj , A.KodKP , A.KodVot , A.AP01_Butiran, B.PO19_Kuantiti as QtyAsal, A.AP01_KadarHarga as KadarAsal, B.PO19_JumKadar as JumAsal, B.PO19_QBlmByr as QtyBlmByr , B.PO19_JumBlmByr as JumBlmByr, A.AP01_KuantitiAkanByr as KttAkanByr, A.AP01_AmaunAkanByr as AmtAkanByr     
from AP01_InvoisDt A
inner join PO19_PtDt B on B.PO19_PtDtId = A.PO19_PtDtId 
 where AP01_NoId = '" & strIdInv & "'"
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            Dim intPtDtID As Integer
            Dim strKodKW, strKodKO, strKodPTj, strKodKP, strKodVot, strItem, strKadarAsl As String
            Dim intQtyAsl, intQtyBlm, intQtyAkan As Integer
            Dim strJumAsl, strJumBlm, strJumAkan As String

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        intPtDtID = ds.Tables(0).Rows(i)("PO19_PtDtId")
                        strKodKW = ds.Tables(0).Rows(i)("KodKw")
                        strKodKO = ds.Tables(0).Rows(i)("KodKO")
                        strKodPTj = ds.Tables(0).Rows(i)("KodPtj")
                        strKodKP = ds.Tables(0).Rows(i)("KodKP")
                        strKodVot = ds.Tables(0).Rows(i)("KodVot")
                        strItem = ds.Tables(0).Rows(i)("AP01_Butiran")

                        intQtyAsl = CInt(ds.Tables(0).Rows(i)("QtyAsal"))
                        strKadarAsl = FormatNumber(ds.Tables(0).Rows(i)("KadarAsal"), 2)
                        strJumAsl = FormatNumber(ds.Tables(0).Rows(i)("JumAsal"), 2)

                        intQtyBlm = CInt(ds.Tables(0).Rows(i)("QtyBlmByr"))
                        strJumBlm = FormatNumber(ds.Tables(0).Rows(i)("JumBlmByr"), 2)

                        intQtyAkan = CInt(ds.Tables(0).Rows(i)("KttAkanByr"))
                        strJumAkan = FormatNumber(ds.Tables(0).Rows(i)("AmtAkanByr"), 2)

                        dt.Rows.Add(intPtDtID, strKodKW, strKodKO, strKodPTj, strKodKP, strKodVot, strItem,
                                    intQtyAsl, strKadarAsl, strJumAsl, intQtyBlm, strJumBlm, intQtyAkan, strJumAkan)
                    Next
                End If
            End If

            gvTransPT.DataSource = dt
            gvTransPT.DataBind()
        Catch ex As Exception

        End Try
    End Function

    Protected Sub lnkBtnCari_Click(sender As Object, e As EventArgs) Handles lnkBtnCari.Click
        fLoadLstInv()
        mpeLstInv.Show()
    End Sub

    Protected Sub lbtnHapus_Click(sender As Object, e As EventArgs) Handles lbtnHapus.Click
        Dim strSql As String
        Dim dbconn As New DBKewConn
        Dim blnSuccess As Boolean = True
        Try
            Dim strIdInv As String = Trim(txtIdInv.Text.TrimEnd)
            Dim strStatDok As String = 20
            strSql = "update AP01_Invois set AP09_StatusDok = @StatusDok where AP01_NoID = @IdInv"

            Dim paramSql() As SqlParameter = {
                    New SqlParameter("@IdInv", strIdInv),
                    New SqlParameter("@StatusDok", strStatDok)
                    }

            dbconn.sConnBeginTrans()
            If Not dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
                blnSuccess = False
            End If

        Catch ex As Exception
            blnSuccess = False
        End Try

        If blnSuccess Then
            dbconn.sConnCommitTrans()
            fGlobalAlert("Rekod telah dihapuskan!", Me.Page, Me.[GetType]())
            sNewRec()
        Else
            dbconn.sConnRollbackTrans()
            fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
        End If

    End Sub

    Private Sub fLoadInvDtILL(ByVal strIdInv As String)
        Try
            sInitGvTrans()
            Dim dtTrans As DataTable = CType(ViewState("dtTrans"), DataTable)
            Dim strSql As String = "select KodKw, KodKO, KodPtj, KodKP, KodVot ,AP01_Butiran, AP01_KadarHarga, AP01_KuantitiAkanByr, AP01_AmaunAkanByr
from AP01_InvoisDt where AP01_NoId = '" & strIdInv & "'"
            Dim dbconn As New DBKewConn
            Dim ds As New DataSet
            ds = dbconn.fSelectCommand(strSql)

            Dim strKW, strKO, strPTj, strKP, strVot, strButiran, strHarga, strKuantiti, strJumlah As String

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        strKW = ds.Tables(0).Rows(i)("KodKw").ToString
                        strKO = ds.Tables(0).Rows(i)("KodKO").ToString
                        strPTj = ds.Tables(0).Rows(i)("KodPtj").ToString
                        strKP = ds.Tables(0).Rows(i)("KodKP").ToString
                        strVot = ds.Tables(0).Rows(i)("KodVot").ToString
                        strButiran = ds.Tables(0).Rows(i)("AP01_Butiran").ToString
                        strHarga = ds.Tables(0).Rows(i)("AP01_KadarHarga").ToString
                        strKuantiti = ds.Tables(0).Rows(i)("AP01_KuantitiAkanByr").ToString
                        strJumlah = ds.Tables(0).Rows(i)("AP01_AmaunAkanByr").ToString

                        dtTrans.Rows.Add(strKW, strKO, strPTj, strKP, strVot, strButiran, strHarga, strKuantiti, strJumlah)
                    Next
                End If
            End If

            gvTrans.DataSource = dtTrans
            gvTrans.DataBind()

        Catch ex As Exception

        End Try
    End Sub

    Private Function fLoadNominees(ByVal strIdInv As String)
        Dim strSql As String
        Dim ds As New DataSet
        Dim dbconn As New DBKewConn
        Try
            strSql = "select KategoriPenerima, AP01_IdPenerima, AP01_Penerima, AP01_Jumlah, NamaBank, AP01_KodBank, AP01_NoAkaun, AP01_Alamat1, AP01_Alamat2, AP01_Bandar, AP01_Poskod, KodNegeri, NegeriP, KodNegara, NegaraP, AP01_Email from 
VAP_Nominees where AP01_NoId = '" & strIdInv & "'"

            ds = dbconn.fSelectCommand(strSql)
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    txtIdKat.Text = "SY"
                    txtKat.Text = "SYARIKAT"
                    txtIDPenerima.Text = ds.Tables(0).Rows(0)("AP01_IdPenerima").ToString
                    txtNmPenerima.Text = ds.Tables(0).Rows(0)("AP01_Penerima").ToString
                    txtJumInv.Text = FormatNumber(ds.Tables(0).Rows(0)("AP01_Jumlah").ToString(), 2)
                    ddlBank.SelectedValue = ds.Tables(0).Rows(0)("AP01_KodBank").ToString
                    txtNoAkaun.Text = ds.Tables(0).Rows(0)("AP01_NoAkaun").ToString
                    txtAlmt1.Text = ds.Tables(0).Rows(0)("AP01_Alamat1").ToString
                    txtAlmt2.Text = ds.Tables(0).Rows(0)("AP01_Alamat2").ToString
                    txtBandar.Text = ds.Tables(0).Rows(0)("AP01_Bandar").ToString
                    txtPoskod.Text = ds.Tables(0).Rows(0)("AP01_Poskod").ToString
                    ddlNegeri.Text = ds.Tables(0).Rows(0)("KodNegeri").ToString
                    ddlNegara.Text = ds.Tables(0).Rows(0)("KodNegara").ToString
                    txtEmel.Text = ds.Tables(0).Rows(0)("AP01_Email").ToString
                End If
            End If
        Catch ex As Exception

        End Try

    End Function

    Private Sub lbtnRekBaru_Click(sender As Object, e As EventArgs) Handles lbtnRekBaru.Click
        sNewRec()
    End Sub

    Private Sub lbtnListInv_Click(sender As Object, e As EventArgs) Handles lbtnListInv.Click
        ddlTapisan.SelectedIndex = 0
        fClearGvLstInv()
        mpeLstInv.Show()
    End Sub

    Private Sub Button4_ServerClick(sender As Object, e As EventArgs) Handles Button4.ServerClick
        mpePnlListNom.Hide()
    End Sub

    Protected Sub gvTrans_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvTrans.SelectedIndexChanged

    End Sub

    Private Sub Button3_ServerClick(sender As Object, e As EventArgs) Handles Button3.ServerClick
        mpePnlNoPT.Hide()
    End Sub

    Private Sub Button2_ServerClick(sender As Object, e As EventArgs) Handles Button2.ServerClick
        mpeLstInv.Hide()
    End Sub

    Sub Audit(AuditType As String, TransID As String, table As String, sLogMedan As String,sLogLama As String, sLogBaru As String)

        'Dim strTkhPemulangan As Date = RefDateTime.ToString("dd/MM/yyyy")
        Dim Sql As String
        Dim SubMenuID As String
        Dim dbconn As New DBKewConn


        sLogUserID = Session("ssusrID")
        sLogLevel = Session("ssUsrTahap")
        'sLogNostaf = Session("RefID")
        sLogSubModul = Request.QueryString("KodSubMenu")
        Select Case AuditType
            '---SIMPAN---
            Case "SIMPAN"
                sLogDesc = "INSERT"
                sLogField = table
                lsLogUsrIP = Session("PcIP")
                lsLogUsrPC = Session("PcName")
                'sLogLama = "-"
                CheckLog()
                sAuditLog(sLogUserID, sLogLevel, "-", sLogSubModul, sLogDesc, sLogField, "-", "-", sLogMedan, sLogBaru, sLogLama, lsLogUsrIP, lsLogUsrPC)

            Case "UPDATE"
                sLogDesc = "UPDATE"
                sLogField = table
                lsLogUsrIP = Session("PcIP")
                lsLogUsrPC = Session("PcName")
                'sLogLama = "-"
                CheckLog()
                sAuditLog(sLogUserID, sLogLevel, "-", sLogSubModul, sLogDesc, sLogField, "-", "-", sLogMedan, sLogBaru, sLogLama, lsLogUsrIP, lsLogUsrPC)

            Case "DELETE"
                sLogDesc = "DELETE"
                sLogField = table
                lsLogUsrIP = Session("PcIP")
                lsLogUsrPC = Session("PcName")
                'sLogLama = "-"
                CheckLog()
                sAuditLog(sLogUserID, sLogLevel, "-", sLogSubModul, sLogDesc, sLogField, "-", "-", sLogMedan, sLogBaru, sLogLama, lsLogUsrIP, lsLogUsrPC)
        End Select

    End Sub

    Sub CheckLog()
        If sLogBaru = "" Then
            sLogBaru = "-"
        End If
        If sLogLama = "" Then
            sLogLama = "-"
        End If
        If sLogNostaf = "" Then
            sLogNostaf = "-"
        End If
        If sLogSubModul = "" Then
            sLogSubModul = "-"
        End If
        If sLogField = "" Then
            sLogField = "-"
        End If
        If sLogTable = "" Then
            sLogTable = "-"
        End If
        If sLogRef = "" Then
            sLogRef = "-"
        End If
        If sLogRefNo = "" Then
            sLogRefNo = "-"
        End If
        If sLogDesc = "" Then
            sLogDesc = "-"
        End If
        If sLogStatus = "" Then
            sLogStatus = "-"
        End If

        '   lsLogDate = RefDateTime.ToString("yyyy-MM-dd")
        '  lsLogTime = RefDateTime.ToString("hh:mm:ss")
    End Sub


End Class