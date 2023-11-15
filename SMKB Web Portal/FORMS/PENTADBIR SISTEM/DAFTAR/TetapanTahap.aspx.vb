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
Imports System.Diagnostics.Eventing
Imports System.Runtime.InteropServices.ComTypes

Public Class TetapanTahap
    Inherits System.Web.UI.Page

    Public dsTahapDT As New DataSet
    Public dvTahapDT As New DataView
    Dim dbconn As New DBKewConn
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            fCariTahapDT()
            'fBindJenisVot("")
            fBindTahapDT("")
            fBindGv()
            fBindPenggunaSis("")
        End If
    End Sub
    Private Function fBindGv()

        Try

            Dim dt As New DataTable
            dt = fCreateDtTahapDT()

            If dt.Rows.Count = 0 Then
                gvTahapDT.DataSource = New List(Of String)
            Else
                gvTahapDT.DataSource = dt
            End If
            gvTahapDT.DataBind()

            'Required for jQuery DataTables to work.
            gvTahapDT.UseAccessibleHeader = True
            gvTahapDT.HeaderRow.TableSection = TableRowSection.TableHeader

        Catch ex As Exception

        End Try
    End Function

    Private Function fCreateDtTahapDT() As DataTable

        Try

            Dim dt As New DataTable
            dt.Columns.Add("Bil", GetType(String))
            dt.Columns.Add("No_Staf", GetType(String))
            dt.Columns.Add("ms01_nama", GetType(String))
            dt.Columns.Add("tahap", GetType(String))
            dt.Columns.Add("pejabat", GetType(String))
            dt.Columns.Add("status", GetType(String))

            Dim intBil As Integer
            Dim strNoStaf As String
            Dim strNama As String
            Dim strTahap As String
            Dim strPejabat As String
            Dim strStatus As String

            Dim dataCarian = ddlCariTahapDT.SelectedValue

            Dim strSql As String = $"SELECT a.No_Staf, b.ms01_nama,  e.Jen_Tahap, d.pejabat, a.Status
                        FROM SMKB_UTahapDT as a, [DEVMIS\SQL_INS01].dbStaf.dbo.MS01_Peribadi as b, [DEVMIS\SQL_INS01].dbStaf.dbo.MS08_Penempatan as c, 
                        [DEVMIS\SQL_INS01].dbStaf.dbo.MS_Pejabat d, SMKB_UTahap e
                        WHERE a.No_Staf = b.ms01_nostaf
                        AND a.No_Staf  = c.ms01_nostaf
                        AND c.ms08_staterkini = 1
                        AND c.ms08_pejabat = d.kodpejabat
                        AND a.Kod_Tahap = e.Kod_Tahap
                        AND a.Kod_Tahap = '{dataCarian}'
                        ORDER BY b.ms01_nama"

            dsTahapDT = dbconn.fSelectCommand(strSql)
            dvTahapDT = New DataView(dsTahapDT.Tables(0))
            intBil = 1
            For i As Integer = 0 To dsTahapDT.Tables(0).Rows.Count - 1

                intBil = i + 1
                strNoStaf = dsTahapDT.Tables(0).Rows(i)(0).ToString
                strNama = dsTahapDT.Tables(0).Rows(i)(1).ToString
                strTahap = dsTahapDT.Tables(0).Rows(i)(2).ToString
                strPejabat = dsTahapDT.Tables(0).Rows(i)(3).ToString

                Dim status = dsTahapDT.Tables(0).Rows(i)(4).ToString
                If status = 1 Then
                    strStatus = "AKTIF"
                Else
                    strStatus = "TIDAK AKTIF"
                End If

                dt.Rows.Add(intBil, strNoStaf, strNama, strTahap, strPejabat, strStatus)
            Next

            Return dt

        Catch ex As Exception

        End Try

    End Function

    Private Sub lbtnSimpan_Click(sender As Object, e As EventArgs) Handles lbtnSimpan.ServerClick
        Dim strSql As String
        Dim dbconn As New DBKewConn
        Try
            Dim strKodTahap As String = ddlTahapDT.SelectedValue
            Dim strNoStaf As String = ddlPenggunaSis.SelectedValue
            Dim intStatus As Integer = rblStatus.SelectedValue

            strSql = "select count(*) from SMKB_UTahapDT where Kod_Tahap = '" & strKodTahap & "' AND No_Staf = '" & strNoStaf & "'"
            If fCheckRec(strSql) = 0 Then

                strSql = "insert into SMKB_UTahapDT (No_Staf, Kod_Tahap, Status ) 
                    values (@No_Staf, @Kod_Tahap, @Status)"
                Dim paramSql() As SqlParameter = {
                    New SqlParameter("@Kod_Tahap", strKodTahap),
                    New SqlParameter("@No_Staf", strNoStaf),
                    New SqlParameter("@Status", intStatus)
                }

                dbconn.sConnBeginTrans()
                If dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                    dbconn.sConnCommitTrans()
                    'fGlobalAlert("Rekod baru telah disimpan!", Me.Page, Me.[GetType]())
                    lblModalMessaage.Text = "Rekod baru telah disimpan" 'message di modal
                    ScriptManager.RegisterStartupScript(Me.Page, [GetType](), "alert", "SaveSucces();", True)
                    'ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
                    fBindGv()
                    'fReset()
                Else
                    dbconn.sConnRollbackTrans()
                    'fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
                    lblModalMessaage.Text = "Ralat!" 'message di modal
                    ScriptManager.RegisterStartupScript(Me.Page, [GetType](), "alert", "SaveSucces();", True)
                    'ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
                End If

            Else
                'UPDATE
                strSql = "update SMKB_UTahapDT set status = @status
                where Kod_Tahap = @Kod_Tahap AND No_Staf =  @No_Staf"
                Dim paramSql() As SqlParameter = {
                    New SqlParameter("@Kod_Tahap", strKodTahap),
                    New SqlParameter("@No_Staf", strNoStaf),
                    New SqlParameter("@Status", intStatus)
                            }
                dbconn.sConnBeginTrans()
                If dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
                    dbconn.sConnCommitTrans()
                    'fGlobalAlert("Rekod telah dikemaskini!", Me.Page, Me.[GetType]())
                    lblModalMessaage.Text = "Rekod telah dikemaskini." 'message di modal

                    ScriptManager.RegisterStartupScript(Me.Page, [GetType](), "alert", "SaveSucces();", True)
                    fBindGv()
                    'fReset()
                Else
                    dbconn.sConnRollbackTrans()
                    'fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
                    lblModalMessaage.Text = "Ralat!" 'message di modal
                    ScriptManager.RegisterStartupScript(Me.Page, [GetType](), "alert", "SaveSucces();", True)
                    'ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub


    Private Sub gvTahapDT_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvTahapDT.RowCommand
        Try
            If e.CommandName = "Select" Then

                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim selectedRow As GridViewRow = gvTahapDT.Rows(index)

                Dim strKod As String = selectedRow.Cells(1).Text.ToString
                'txtNama.Value = selectedRow.Cells(1).Text.ToString


                'Call sql
                Dim strSql As String = $"SELECT a.Kod_Tahap, a.No_Staf, b.ms01_nama,  d.pejabat, a.Status
                        FROM SMKB_UTahapDT as a, [DEVMIS\SQL_INS01].dbStaf.dbo.MS01_Peribadi as b, [DEVMIS\SQL_INS01].dbStaf.dbo.MS08_Penempatan as c, [DEVMIS\SQL_INS01].dbStaf.dbo.MS_Pejabat d
                        WHERE a.No_Staf = b.ms01_nostaf
                        AND a.No_Staf  = c.ms01_nostaf
                        AND c.ms08_staterkini = 1
                        AND c.ms08_pejabat = d.kodpejabat
                        AND a.No_Staf = '{strKod}'
                        AND a.Kod_Tahap = '" & ddlCariTahapDT.SelectedValue & "'"

                Dim dt As New DataTable
                dt = dbconn.fSelectCommandDt(strSql)
                If dt.Rows.Count > 0 Then

                    Dim strKodTahap As String = dt.Rows(0)("Kod_Tahap")
                    fBindTahapDT(strKodTahap)

                    txtNoStaf.Value = dt.Rows(0)("No_Staf")
                    fBindPenggunaSis(txtNoStaf.Value)

                    'txtNama.Value = dt.Rows(0)("ms01_nama")
                    txtPejabat.Value = dt.Rows(0)("pejabat")

                    Dim status = dt.Rows(0)("status")
                    rblStatus.SelectedValue = status

                End If


                ClientScript.RegisterStartupScript([GetType](), "alert3", "ShowPopup('1');", True)


            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvTahapDT_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvTahapDT.PageIndexChanging
        gvTahapDT.PageIndex = e.NewPageIndex
        fBindGv()
    End Sub


    Private Sub fBindTahapDT(kodKlasifikasi As String)
        Try
            Dim strsql As String
            Dim dataCarian = ddlCariTahapDT.SelectedValue

            If kodKlasifikasi = "" Then
                strsql = $"SELECT Kod_Tahap, Jen_Tahap, Butiran, Status FROM SMKB_UTahap WHERE status = 1 ORDER BY Jen_Tahap"
            Else
                strsql = $"SELECT Kod_Tahap, Jen_Tahap, Butiran, Status FROM SMKB_UTahap WHERE status = 1 and " & kodKlasifikasi & "ORDER BY Jen_Tahap"
            End If


            Dim ds As New DataSet
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strsql)
            ' Dim strKod = Trim(ds.Tables(0).Rows(0)("Kod").ToString)

            ddlTahapDT.DataSource = ds
            ddlTahapDT.DataTextField = "Jen_Tahap"
            ddlTahapDT.DataValueField = "Kod_Tahap"
            ddlTahapDT.DataBind()


            ddlTahapDT.Items.Insert(0, New ListItem("- SILA PILIH -", 0))

            If kodKlasifikasi = "" Then
                ddlTahapDT.SelectedIndex = 0
            Else
                ddlTahapDT.Items.FindByValue(kodKlasifikasi).Selected = True
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub fBindPenggunaSis(kodKlasifikasi As String)
        Try
            Dim strsql As String
            Dim dataCarian = ddlCariTahapDT.SelectedValue

            strsql = $"SELECT MS01_NoStaf, MS01_Nama FROM [DEVMIS\SQL_INS01].dbStaf.dbo.MS01_Peribadi
                    WHERE MS01_Status = 1
                    AND MS01_NoStaf IN (select CLM_loginID from [DEVMIS\SQL_INS04].dbCLM.dbo.CLM_PenggunaSis
                    where CLM_SisKod = 'SMKB'
                    AND CLM_SisStatus = 'AKTIF')
                    ORDER BY MS01_Nama"

            Dim ds As New DataSet
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strsql)
            ' Dim strKod = Trim(ds.Tables(0).Rows(0)("Kod").ToString)

            ddlPenggunaSis.DataSource = ds
            ddlPenggunaSis.DataTextField = "MS01_Nama"
            ddlPenggunaSis.DataValueField = "MS01_NoStaf"
            ddlPenggunaSis.DataBind()


            ddlPenggunaSis.Items.Insert(0, New ListItem("- SILA PILIH -", 0))

            If kodKlasifikasi = "" Then
                ddlPenggunaSis.SelectedIndex = 0
            Else
                ddlPenggunaSis.Items.FindByValue(kodKlasifikasi).Selected = True
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub fCariTahapDT()
        Try
            Dim strsql As String


            strsql = $"SELECT Kod_Tahap, Jen_Tahap, Butiran, Status FROM SMKB_UTahap WHERE status = 1 ORDER BY Jen_Tahap"

            Dim ds As New DataSet
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strsql)
            'Dim strKod = Trim(ds.Tables(0).Rows(0)("Kod").ToString)

            ddlCariTahapDT.DataSource = ds
            ddlCariTahapDT.DataTextField = "Jen_Tahap"
            ddlCariTahapDT.DataValueField = "Kod_Tahap"
            ddlCariTahapDT.DataBind()

            ddlCariTahapDT.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
            ddlCariTahapDT.SelectedIndex = 0


        Catch ex As Exception

        End Try
    End Sub

    Private Sub lbtnCari_Click(sender As Object, e As EventArgs) Handles lbtnCari.ServerClick
        fBindGv()
        fBindTahapDT(ddlCariTahapDT.SelectedValue)
    End Sub

    Protected Sub ddlPenggunaSis_SelectedIndexChanged(sender As Object, e As EventArgs)
        txtNoStaf.Value = ddlPenggunaSis.SelectedValue
        txtPejabat.Value = getPejabat(txtNoStaf.Value)
    End Sub

    Function getPejabat(parNoStaf As String) As String
        Dim sql As String
        Using dbconn As New SqlConnection(strCon)
            dbconn.Open()
            Dim namaPejabat As String = ""
            '----Dapatkan Penyelia
            sql = "SELECT b.pejabat
                FROM [DEVMIS\SQL_INS01].dbStaf.dbo.MS08_Penempatan as a, [DEVMIS\SQL_INS01].dbStaf.dbo.MS_Pejabat b
                WHERE a.ms08_staterkini = 1
                AND a.ms08_pejabat = b.kodpejabat
                AND a.ms01_nostaf = '" & parNoStaf & "'"

            dbcomm = New SqlCommand(sql, dbconn)
            dbread = dbcomm.ExecuteReader()
            dbread.Read()

            If dbread.HasRows Then
                namaPejabat = dbread("pejabat")

            End If
            dbread.Close()

            Return namaPejabat
        End Using

    End Function
End Class