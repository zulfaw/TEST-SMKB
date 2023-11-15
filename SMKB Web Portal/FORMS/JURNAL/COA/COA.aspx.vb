Imports Microsoft.AspNet.Identity
Imports System.Globalization
Imports System.Threading
Imports System.Configuration
Imports System.Web.Configuration
Imports System.Collections.Specialized
Imports System.Reflection
Imports System
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports System.Net.NetworkInformation
Imports System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder

Public Class COA
    Inherits System.Web.UI.Page

    Public dsKod As New DataSet
    Public dvKodKW As New DataView
    Dim dbconn As New DBKewConn


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            fCariPTJ()
            fBindKW("")
            fBindKo("")
            fBindKP("")
            fBindPTJ("")
            'fBindVot("")
            ' fBindKlasifikasiVot("")
            fBindGv()
            'fBindVot()

        End If
    End Sub


    Private Function fBindGv()

        Try
            Dim dt As New DataTable
            dt = fCreateDtKW()

            If dt.Rows.Count = 0 Then
                gvKod.DataSource = New List(Of String)
            Else
                gvKod.DataSource = dt
            End If
            gvKod.DataBind()
            'lblJumRekod.InnerText = dt.Rows.Count


            'Required for jQuery DataTables to work.
            gvKod.UseAccessibleHeader = True
            gvKod.HeaderRow.TableSection = TableRowSection.TableHeader

        Catch ex As Exception

        End Try
    End Function

    Private Function fCreateDtKW() As DataTable
        Dim blnStatus As Boolean
        Dim strStatus As String
        Try

            Dim dt As New DataTable
            dt.Columns.Add("Kod_KW", GetType(String))
            dt.Columns.Add("Kod_KO", GetType(String))
            dt.Columns.Add("Kod_KP", GetType(String))
            dt.Columns.Add("Kod_PTJ", GetType(String))
            dt.Columns.Add("Kod_VOT", GetType(String))
            dt.Columns.Add("Status", GetType(String))
            dt.Columns.Add("Butir_KW", GetType(String))
            dt.Columns.Add("Butir_KO", GetType(String))
            dt.Columns.Add("Butir_KP", GetType(String))
            dt.Columns.Add("Butir_PTJ", GetType(String))

            Dim strKodKW, strKodKO, strKodKP, strKodPTJ, strKodVOT As String
            Dim strButirKW, strButirKO, strButirKodKP, strButirPTJ As String
            Dim strButiran As String
            Dim kod_klasifikasi As String
            Dim Kod_Jenis As String
            Dim Kod_Vot_Saga As String

            Dim searchDDL = ddlCari.SelectedValue & "0000"

            Dim strSql As String = $"SELECT Kod_Kump_Wang, Kod_Operasi, Kod_Projek, Kod_PTJ, Kod_Vot, Status FROM SMKB_COA_Master where Kod_PTJ = '{searchDDL}'"


            dsKod = dbconn.fSelectCommand(strSql)
            dvKodKW = New DataView(dsKod.Tables(0))

            For i As Integer = 0 To dsKod.Tables(0).Rows.Count - 1

                strKodKW = dsKod.Tables(0).Rows(i)(0).ToString
                strKodKO = dsKod.Tables(0).Rows(i)(1).ToString
                strKodKP = dsKod.Tables(0).Rows(i)(2).ToString
                strKodPTJ = dsKod.Tables(0).Rows(i)(3).ToString
                strKodVOT = dsKod.Tables(0).Rows(i)(4).ToString
                blnStatus = dsKod.Tables(0).Rows(i)(5).ToString

                strButirKW = fGetKW(dsKod.Tables(0).Rows(i)(0).ToString)
                strButirKO = fGetKO(dsKod.Tables(0).Rows(i)(1).ToString)
                strButirKodKP = fGetKP(dsKod.Tables(0).Rows(i)(2).ToString)
                strButirPTJ = fGetPTJ(dsKod.Tables(0).Rows(i)(3).ToString)

                If blnStatus = True Then
                    strStatus = "Aktif"
                Else
                    strStatus = "Tidak Aktif"
                End If

                dt.Rows.Add(strKodKW, strKodKO, strKodKP, strKodPTJ, strKodVOT, strStatus, strButirKW, strButirKO, strButirKodKP, strButirPTJ)
            Next

            Return dt

        Catch ex As Exception

        End Try

    End Function

    Private Sub fBindKodVot(kodKlasifikasi As String)
        Try
            Dim strsql As String


            strsql = $"Select Kod_Vot as Kod, concat(Kod_Vot,' - ',Butiran) as Butiran from SMKB_Vot  order by Kod_Vot"

            Dim ds As New DataSet
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strsql)
            ' Dim strKod = Trim(ds.Tables(0).Rows(0)("Kod").ToString)

            ddlVot.DataSource = ds
            ddlVot.DataTextField = "butiran"
            ddlVot.DataValueField = "Kod"
            ddlVot.DataBind()


            ddlVot.Items.Insert(0, New ListItem("- SILA PILIH -", 0))

            If kodKlasifikasi = "" Then
                ddlVot.SelectedIndex = 0
            Else
                ddlVot.Items.FindByValue(kodKlasifikasi.Trim()).Selected = True
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Function fBindVot()

        Try
            Dim dt As New DataTable
            dt = fCreateDtVot()

            If dt.Rows.Count = 0 Then
                gvVot.DataSource = New List(Of String)
            Else
                gvVot.DataSource = dt
            End If
            gvVot.DataBind()
            'lblJumRekod.InnerText = dt.Rows.Count


            'Required for jQuery DataTables to work.
            gvVot.UseAccessibleHeader = True
            gvVot.HeaderRow.TableSection = TableRowSection.TableHeader

        Catch ex As Exception

        End Try
    End Function

    Private Function fCreateDtVot() As DataTable
        Dim blnStatus As Boolean
        Dim strStatus As String
        Try

            Dim dt As New DataTable
            dt.Columns.Add("Kod_Vot", GetType(String))
            dt.Columns.Add("Butiran", GetType(String))
            'dt.Columns.Add("Status", GetType(String))


            Dim strKodVOT As String
            Dim strButirVot As String

            'If action edit
            Dim strKodkw As String = ddlKw.SelectedValue
            Dim strKodko As String = ddlKO.SelectedValue
            Dim strKodkp As String = ddlKP.SelectedValue
            Dim strKodptj As String = ddlPTJ.SelectedValue & "0000"


            Dim strSql As String = $"select a.Kod_Vot , 
                    a.Butiran  from SMKB_Vot a where a.Status = 1  and a.Kod_Klasifikasi ='D' 
                    and a.Kod_Vot not in (SELECT Kod_Vot FROM SMKB_COA_Master 
                    where Kod_Kump_Wang = '{strKodkw}'
                    AND Kod_Operasi = '{strKodko}'
                    AND Kod_Projek = '{strKodkp}'
                    AND Kod_PTJ = '{strKodptj}'
                    AND Kod_Vot = a.Kod_Vot )
                    order by a.Kod_Vot
                   
                    "

            dsKod = dbconn.fSelectCommand(strSql)

            For i As Integer = 0 To dsKod.Tables(0).Rows.Count - 1

                strKodVOT = dsKod.Tables(0).Rows(i)(0).ToString
                strButirVot = dsKod.Tables(0).Rows(i)(1).ToString
                'strStatus = dsKod.Tables(0).Rows(i)(2).ToString 'fGetStatus(strKodkw, strKodko, strKodkp, strKodptj, strKodVOT)
                'check if vot status aktif atau tidak

                dt.Rows.Add(strKodVOT, strButirVot)
            Next

            Return dt

        Catch ex As Exception

        End Try

    End Function


    Private Sub lbtnSimpan_Click(sender As Object, e As EventArgs) Handles lbtnSimpan.ServerClick
        Dim strSql As String
        Dim dbconn As New DBKewConn
        Try
            Dim strKodkw As String = ddlKw.SelectedValue
            Dim strKodko As String = ddlKO.SelectedValue
            Dim strKodkp As String = ddlKP.SelectedValue
            Dim strKodptj As String = ddlPTJ.SelectedValue & "0000"

            Dim strStatus = "" 'rblStatus.SelectedValue


            'If ViewState("SaveMode") = 1 Then
            'INSERT

            Dim rows = gvVot.Rows
            Dim chk As CheckBox
            Dim strKodvot As Label
            Dim strVot As String
            Dim countItem = 0

            For Each row As GridViewRow In rows
                chk = DirectCast(row.FindControl("chkBox"), CheckBox)
                strKodvot = DirectCast(row.FindControl("lblKodVot"), Label)
                strVot = strKodvot.Text
                If chk.Checked Then
                    strStatus = 1 'Status set to True
                    strSql = $"SELECT count(*) from SMKB_COA_Master  
                            WHERE Kod_Kump_Wang = '{strKodkw}'
                            AND Kod_Operasi = '{strKodko}'
                            AND Kod_Projek = '{strKodkp}'
                            AND Kod_PTJ = '{strKodptj}'
                            AND Kod_Vot = '{strVot}'"

                    If fCheckRec(strSql) = 0 Then

                        strSql = "insert into SMKB_COA_Master (Kod_Kump_Wang, Kod_Operasi, Kod_Projek, Kod_PTJ, Kod_Vot,Status)    
                         values (@KodKW ,@KodKO, @KodKP, @KodPTJ, @KodVOT, @Status )"

                        Dim paramSql() As SqlParameter = {
                            New SqlParameter("@KodKW", strKodkw),
                            New SqlParameter("@KodKO", strKodko),
                            New SqlParameter("@KodKP", strKodkp),
                            New SqlParameter("@KodPTJ", strKodptj),
                            New SqlParameter("@KodVOT", strVot),
                            New SqlParameter("@Status", strStatus)
                        }

                        dbconn.sConnBeginTrans()
                        If dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                            dbconn.sConnCommitTrans()
                            ''fGlobalAlert("Rekod baru telah disimpan!", Me.Page, Me.[GetType]())
                            'lblModalMessaage.Text = "Rekod baru telah disimpan" 'message di modal
                            'ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
                            'fBindGv()
                            'fReset()

                            countItem = 1
                        Else
                            dbconn.sConnRollbackTrans()
                            ''fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
                            'lblModalMessaage.Text = "Ralat!" 'message di modal
                            'ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
                        End If

                    Else
                        'UPDATE
                        strSql = "update SMKB_COA_Master set Status =@Status
                                                    WHERE Kod_Kump_Wang = @KodKW
                                                        AND Kod_Operasi = @KodKO
                                                        AND Kod_Projek = @KodKP
                                                        AND Kod_PTJ = @KodPTJ
                                                        AND Kod_Vot = @KodVOT"
                        Dim paramSql() As SqlParameter = {
                                New SqlParameter("@KodKW", strKodkw),
                        New SqlParameter("@KodKO", strKodko),
                        New SqlParameter("@KodKP", strKodkp),
                        New SqlParameter("@KodPTJ", strKodptj),
                        New SqlParameter("@KodVOT", strVot),
                        New SqlParameter("@Status", strStatus)
                                }
                        dbconn.sConnBeginTrans()
                        If dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
                            dbconn.sConnCommitTrans()
                            'fGlobalAlert("Rekod telah dikemaskini!", Me.Page, Me.[GetType]())
                            'lblModalMessaage.Text = "Rekod telah dikemaskini." 'message di modal
                            'ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
                            'fBindGv()
                            'fReset()
                            countItem = 1
                        Else
                            dbconn.sConnRollbackTrans()
                            'fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
                            'lblModalMessaage.Text = "Ralat!" 'message di modal
                            'ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
                        End If
                    End If

                End If
            Next

            If countItem = 1 Then
                lblModalMessaage.Text = "Rekod telah disimpan" 'message di modal
                ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
                fBindGv()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub lbtnKemaskini_Click(sender As Object, e As EventArgs) Handles lbtnKemaskini.ServerClick
        Dim strSql As String
        Dim dbconn As New DBKewConn
        Try
            Dim strKodkw As String = ddlKw.SelectedValue
            Dim strKodko As String = ddlKO.SelectedValue
            Dim strKodkp As String = ddlKP.SelectedValue
            Dim strKodptj As String = ddlPTJ.SelectedValue & "0000"
            Dim strKodvot As String = ddlVot.SelectedValue
            Dim strStatus = rblStatus.SelectedValue


            'If ViewState("SaveMode") = 1 Then
            'INSERT

            strSql = $"SELECT count(*) from SMKB_COA_Master  
                            WHERE Kod_Kump_Wang = '{strKodkw}'
                            AND Kod_Operasi = '{strKodko}'
                            AND Kod_Projek = '{strKodkp}'
                            AND Kod_PTJ = '{strKodptj}'
                            AND Kod_Vot = '{strKodvot}'"

            If fCheckRec(strSql) = 0 Then

                strSql = "insert into SMKB_COA_Master (Kod_Kump_Wang, Kod_Operasi, Kod_Projek, Kod_PTJ, Kod_Vot,Status)    
                         values (@KodKW ,@KodKO, @KodKP, @KodPTJ, @KodVOT, @Status )"

                Dim paramSql() As SqlParameter = {
                    New SqlParameter("@KodKW", strKodkw),
                    New SqlParameter("@KodKO", strKodko),
                    New SqlParameter("@KodKP", strKodkp),
                    New SqlParameter("@KodPTJ", strKodptj),
                    New SqlParameter("@KodVOT", strKodvot),
                    New SqlParameter("@Status", strStatus)
                }

                dbconn.sConnBeginTrans()
                If dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                    dbconn.sConnCommitTrans()
                    'fGlobalAlert("Rekod baru telah disimpan!", Me.Page, Me.[GetType]())
                    lblModalMessaage.Text = "Rekod baru telah disimpan" 'message di modal
                    ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
                    fBindGv()
                    'fReset()
                Else
                    dbconn.sConnRollbackTrans()
                    'fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
                    lblModalMessaage.Text = "Ralat!" 'message di modal
                    ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
                End If

            Else
                'UPDATE
                strSql = "update SMKB_COA_Master set Status =@Status
                             WHERE Kod_Kump_Wang = @KodKW
                                                        AND Kod_Operasi = @KodKO
                                                        AND Kod_Projek = @KodKP
                                                        AND Kod_PTJ = @KodPTJ
                                                        AND Kod_Vot = @KodVOT"
                Dim paramSql() As SqlParameter = {
                        New SqlParameter("@KodKW", strKodkw),
                New SqlParameter("@KodKO", strKodko),
                New SqlParameter("@KodKP", strKodkp),
                New SqlParameter("@KodPTJ", strKodptj),
                New SqlParameter("@KodVOT", strKodvot),
                New SqlParameter("@Status", strStatus)
                        }
                dbconn.sConnBeginTrans()
                If dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
                    dbconn.sConnCommitTrans()
                    'fGlobalAlert("Rekod telah dikemaskini!", Me.Page, Me.[GetType]())
                    lblModalMessaage.Text = "Rekod telah dikemaskini." 'message di modal
                    ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
                    fBindGv()
                    'fReset()
                Else
                    dbconn.sConnRollbackTrans()
                    'fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
                    lblModalMessaage.Text = "Ralat!" 'message di modal
                    ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub


    Private Sub gvKod_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvKod.RowCommand
        Try
            If e.CommandName = "Select" Then


                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim selectedRow As GridViewRow = gvKod.Rows(index)

                Dim strKodkw As String = selectedRow.Cells(0).Text.ToString
                Dim strKodko As String = selectedRow.Cells(1).Text.ToString
                Dim strKodkp As String = selectedRow.Cells(2).Text.ToString
                Dim strKodptj As String = selectedRow.Cells(3).Text.ToString
                Dim strKodvot As String = selectedRow.Cells(4).Text.ToString


                'Call sql
                Dim strSql As String = $"SELECT Kod_Kump_Wang, Kod_Operasi, Kod_Projek, Kod_PTJ, Kod_Vot, Status FROM SMKB_COA_Master 
                                        where Kod_Kump_Wang = '{strKodkw}'
                                        AND Kod_Operasi = '{strKodko}'
                                        AND Kod_Projek = '{strKodkp}'
                                        AND Kod_PTJ = '{strKodptj}'
                                        AND Kod_Vot = '{strKodvot}'
                                        "
                Dim dt As New DataTable
                dt = dbconn.fSelectCommandDt(strSql)
                If dt.Rows.Count > 0 Then
                    Dim strKW = dt.Rows(0)("Kod_Kump_Wang")
                    fBindKW(strKW)

                    Dim strKo = dt.Rows(0)("Kod_Operasi")
                    fBindKo(strKo)

                    Dim strKP = dt.Rows(0)("Kod_Projek")
                    fBindKP(strKP)

                    Dim strVot = dt.Rows(0)("Kod_Vot")
                    fBindKodVot(strVot)

                    Dim strPTJ = dt.Rows(0)("Kod_PTJ")
                    fBindPTJ(strPTJ)

                    Dim statusAktif = dt.Rows(0)("Status")
                    rblStatus.SelectedValue = statusAktif


                End If

                'fBindVot()
                divVot.Visible = False
                lbtnSimpan.Visible = False
                lbtnKemaskini.Visible = True
                dvCheck.Visible = True

                ClientScript.RegisterStartupScript([GetType](), "alert", "ShowPopup('1');", True)


            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub gvVot_RowDataBound(ByVal sender As Object, e As GridViewRowEventArgs)

        Dim strKodkw As String = ddlKw.SelectedValue
        Dim strKodko As String = ddlKO.SelectedValue
        Dim strKodkp As String = ddlKP.SelectedValue
        Dim strKodptj As String = ddlPTJ.SelectedValue & "0000"

        Dim strStatus As Label
        Dim chk As CheckBox
        Dim strStat As String
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                chk = DirectCast(e.Row.FindControl("chkBox"), CheckBox)
                strStatus = DirectCast(e.Row.FindControl("lblStatus"), Label)
                'strStat = strStatus.Text

                'If strStat = 1 Then
                '    chk.Checked = True
                'Else
                '    chk.Checked = False
                'End If


            End If

        Catch ex As Exception
            sErrLog(ex.HResult, ex.Message, fGetFormName(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session("ssusrID"), Session("PcIP"), Session("PcName"), Session("SysVer"))
        End Try
    End Sub

    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.ServerClick
        divVot.Visible = True
        fBindVot()
        lbtnSimpan.Visible = True
        lbtnKemaskini.Visible = False
        dvCheck.Visible = False

        ClientScript.RegisterStartupScript([GetType](), "alert", "ShowPopup('2');", True)
    End Sub
    Private Sub gvKod_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvKod.PageIndexChanging
        gvKod.PageIndex = e.NewPageIndex
        fBindGv()
    End Sub



    Private Sub fBindKW(kodKlasifikasi As String)
        Try
            Dim strsql As String


            strsql = $"Select Kod_Kump_Wang as Kod, Butiran from SMKB_Kump_Wang  order by Kod_Kump_Wang"

            Dim ds As New DataSet
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strsql)
            ' Dim strKod = Trim(ds.Tables(0).Rows(0)("Kod").ToString)

            ddlKw.DataSource = ds
            ddlKw.DataTextField = "butiran"
            ddlKw.DataValueField = "Kod"
            ddlKw.DataBind()


            ddlKw.Items.Insert(0, New ListItem("- SILA PILIH -", 0))

            If kodKlasifikasi = "" Then
                ddlKw.SelectedIndex = 0
            Else
                ddlKw.Items.FindByValue(kodKlasifikasi.Trim()).Selected = True
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub fBindKo(kodKlasifikasi As String)
        Try
            Dim strsql As String


            strsql = $"Select Kod_Operasi as Kod, Butiran from SMKB_Operasi  order by Kod_Operasi"

            Dim ds As New DataSet
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strsql)
            ' Dim strKod = Trim(ds.Tables(0).Rows(0)("Kod").ToString)

            ddlKO.DataSource = ds
            ddlKO.DataTextField = "butiran"
            ddlKO.DataValueField = "Kod"
            ddlKO.DataBind()


            ddlKO.Items.Insert(0, New ListItem("- SILA PILIH -", 0))

            If kodKlasifikasi = "" Then
                ddlKO.SelectedIndex = 0
            Else
                ddlKO.Items.FindByValue(kodKlasifikasi.Trim()).Selected = True
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub fBindKP(kodKlasifikasi As String)
        Try
            Dim strsql As String


            strsql = $"Select Kod_Projek as Kod, Butiran from SMKB_Projek  order by Kod_Projek"

            Dim ds As New DataSet
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strsql)
            ' Dim strKod = Trim(ds.Tables(0).Rows(0)("Kod").ToString)

            ddlKP.DataSource = ds
            ddlKP.DataTextField = "butiran"
            ddlKP.DataValueField = "Kod"
            ddlKP.DataBind()


            ddlKP.Items.Insert(0, New ListItem("- SILA PILIH -", 0))

            If kodKlasifikasi = "" Then
                ddlKP.SelectedIndex = 0
            Else
                ddlKP.Items.FindByValue(kodKlasifikasi.Trim()).Selected = True
            End If

        Catch ex As Exception

        End Try
    End Sub


    Private Function fGetKW(Kod As String) As String
        'Dim ds As New DataSet
        Dim Butiran = ""
        Try
            Dim strSql = $"Select Butiran from SMKB_Kump_Wang WHERE Kod_Kump_Wang='{Kod}'"

            Dim dbconn As New DBKewConn
            dbconn.sSelectCommand(strSql, Butiran)

            Return Butiran
        Catch ex As Exception
            'fErrorLog("Peruntukan_KW.aspx(fBindDdlKW)- " & ex.Message.ToString)
        End Try
        Return Butiran
    End Function

    Private Function fGetKO(Kod As String) As String
        'Dim ds As New DataSet
        Dim Butiran = ""
        Try
            Dim strSql = $"Select Butiran from SMKB_Operasi WHERE Kod_Operasi ='{Kod}'"

            Dim dbconn As New DBKewConn
            dbconn.sSelectCommand(strSql, Butiran)

            Return Butiran
        Catch ex As Exception
            'fErrorLog("Peruntukan_KW.aspx(fBindDdlKW)- " & ex.Message.ToString)
        End Try
        Return Butiran
    End Function

    Private Function fGetKP(Kod As String) As String
        'Dim ds As New DataSet
        Dim Butiran = ""
        Try
            Dim strSql = $"Select Butiran from SMKB_Projek WHERE Kod_Projek ='{Kod}'"

            Dim dbconn As New DBKewConn
            dbconn.sSelectCommand(strSql, Butiran)

            Return Butiran
        Catch ex As Exception
            'fErrorLog("Peruntukan_KW.aspx(fBindDdlKW)- " & ex.Message.ToString)
        End Try
        Return Butiran
    End Function

    Private Function fGetVot(Kod As String) As String
        'Dim ds As New DataSet
        Dim Butiran = ""
        Try
            Dim strSql = $"Select Butiran from SMKB_Vot WHERE Kod_Vot ='{Kod}'"

            Dim dbconn As New DBKewConn
            dbconn.sSelectCommand(strSql, Butiran)

            Return Butiran
        Catch ex As Exception
            'fErrorLog("Peruntukan_KW.aspx(fBindDdlKW)- " & ex.Message.ToString)
        End Try
        Return Butiran
    End Function

    Private Function fGetPTJ(Kod As String) As String

        Dim Butiran = ""
        Try
            Dim strSql = $"select  Pejabat  from MS_Pejabat WHERE Status = 1 and KodPejabat = Left('{Kod}',2)"

            Dim dbconn As New DBSMConn
            dbconn.sSelectCommand(strSql, Butiran)

            Return Butiran
        Catch ex As Exception
            'fErrorLog("Peruntukan_KW.aspx(fBindDdlKW)- " & ex.Message.ToString)
        End Try
        Return Butiran

    End Function

    Private Sub fBindPTJ(kodKlasifikasi As String)
        Try
            Dim dbconn As New DBSMConn
            Dim strsql As String

            kodKlasifikasi = Left(kodKlasifikasi, 2)

            strsql = $"select KodPejabat AS kod, Pejabat as butiran, Singkatan from MS_Pejabat WHERE Status = 1  order by Pejabat"
            Dim ds As New DataSet
            Dim dt As New DataTable
            ds = dbconn.fselectCommand(strsql)
            ' Dim strKod = Trim(ds.Tables(0).Rows(0)("Kod").ToString)

            ddlPTJ.DataSource = ds
            ddlPTJ.DataTextField = "butiran"
            ddlPTJ.DataValueField = "Kod"
            ddlPTJ.DataBind()


            ddlPTJ.Items.Insert(0, New ListItem("- SILA PILIH -", 0))

            If kodKlasifikasi = "" Then
                ddlPTJ.SelectedIndex = 0
            Else
                ddlPTJ.Items.FindByValue(kodKlasifikasi.Trim()).Selected = True
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub fCariPTJ()
        Try
            Dim dbconn As New DBSMConn
            Dim strsql As String


            strsql = $"select KodPejabat AS kod, Pejabat as butiran, Singkatan from MS_Pejabat WHERE Status = 1 order by Pejabat"

            Dim ds As New DataSet
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strsql)
            ' Dim strKod = Trim(ds.Tables(0).Rows(0)("Kod").ToString)

            ddlCari.DataSource = ds
            ddlCari.DataTextField = "butiran"
            ddlCari.DataValueField = "Kod"
            ddlCari.DataBind()


            ddlCari.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
            ddlCari.SelectedIndex = 0


        Catch ex As Exception

        End Try
    End Sub


    Private Sub lbtnCari_Click(sender As Object, e As EventArgs) Handles lbtnCari.ServerClick
        fBindGv()
    End Sub

    Protected Sub ddlVot_IndexChanged(ByVal sender As Object, ByVal e As EventArgs)

        ClientScript.RegisterStartupScript(Me.GetType(), "Popup", "$('#permohonan').modal('show')", True)
        fBindVot()

    End Sub
End Class