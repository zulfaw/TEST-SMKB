Imports System
Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.Net.NetworkInformation

Public Class Kod_Transaksi
    Inherits System.Web.UI.Page
    Dim dbconn As New DBKewConn
    Dim dbconnSM As New DBSMConn
    Public dsKod As New DataSet
    Public dvKodKW As New DataView

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim kod As String = ddlJenis.SelectedValue
            fBindDdlJenis(ddlJenis)
            fBindDdlVotTetap(ddlVotTetap)
            fBindDdlVotTetap(ddlVotBknTetap)
            fBindDdlJenisPot(ddlJnsPot)
            fBindDdlAgensi(ddlAgensi)
            fBindGvJenis()

        End If
    End Sub
    Private Sub fBindDdlVotTetap(ddl As DropDownList)
        Dim strSql As String = "select kod_vot,kod_vot + ' - ' + butiran as butirans,butiran,Kod_klasifikasi from SMKB_vot where Kod_klasifikasi = 'D' order by kod_vot"
        Using dt = dbconn.fSelectCommandDt(strSql)
            ddl.DataSource = dt
            ddl.DataTextField = "butirans"
            ddl.DataValueField = "kod_vot"
            ddl.DataBind()

            ddl.Items.Insert(0, New ListItem("- SILA PILIH - ", "-"))
            ddl.SelectedIndex = 0

        End Using
    End Sub
    Private Sub fBindDdlJenis(ddl As DropDownList)
        Dim strSql As String = "select jenis_trans,butiran from SMKB_gaji_jenis_trans order by jenis_trans"
        Using dt = dbconn.fSelectCommandDt(strSql)
            ddl.DataSource = dt
            ddl.DataTextField = "butiran"
            ddl.DataValueField = "jenis_trans"
            ddl.DataBind()

            ddl.Items.Insert(0, New ListItem("- SILA PILIH - ", "-"))
            ddl.SelectedIndex = 0

        End Using
    End Sub
    Private Sub fBindDdlAgensi(ddl As DropDownList)
        Dim strSql As String = "select kod_kerajaan,nama from SMKB_gaji_kerajaan order by kod_kerajaan"
        Using dt = dbconn.fSelectCommandDt(strSql)
            ddl.DataSource = dt
            ddl.DataTextField = "nama"
            ddl.DataValueField = "kod_kerajaan"
            ddl.DataBind()

            ddl.Items.Insert(0, New ListItem("- SILA PILIH - ", "-"))
            ddl.SelectedIndex = 0

        End Using
    End Sub
    Private Sub fBindDdlJenisPot(ddl As DropDownList)

        ddl.Items.Insert(0, New ListItem("- SILA PILIH - ", "-"))
        ddl.Items.Insert(1, New ListItem("KWSP Pekerja", "KP"))
        ddl.Items.Insert(2, New ListItem("KWSP Majikan", "KM"))
        ddl.Items.Insert(3, New ListItem("Perkeso Pekerja", "SP"))
        ddl.Items.Insert(4, New ListItem("Perkeso Majikan", "SM"))
        ddl.Items.Insert(5, New ListItem("Pencen", "PC"))
        ddl.Items.Insert(6, New ListItem("Cukai", "TX"))
        ddl.Items.Insert(7, New ListItem("Potongan", "PT"))

        ddl.SelectedIndex = 0

    End Sub
    Private Sub fBindGvJenis()


        Try
            Dim dt As New DataTable
            dt = fCreateDtJenis()

            If dt.Rows.Count = 0 Then
                gvJenis.DataSource = New List(Of String)
            Else
                gvJenis.DataSource = dt
            End If
            gvJenis.DataBind()
            'lblJumRekod.InnerText = dt.Rows.Count


            'Required for jQuery DataTables to work.
            gvJenis.UseAccessibleHeader = True
            gvJenis.HeaderRow.TableSection = TableRowSection.TableHeader

        Catch ex As Exception

        End Try
    End Sub
    Private Function fCreateDtJenis() As DataTable

        Try

            Dim dt As New DataTable
            dt.Columns.Add("Kod_Trans", GetType(String))
            dt.Columns.Add("Butiran", GetType(String))
            dt.Columns.Add("Kira_Kwsp", GetType(String))
            dt.Columns.Add("Kira_Perkeso", GetType(String))
            dt.Columns.Add("Kira_Cukai", GetType(String))
            dt.Columns.Add("Kira_Pencen", GetType(String))
            dt.Columns.Add("Vot_Tetap", GetType(String))
            dt.Columns.Add("Vot_Bukan_Tetap", GetType(String))
            dt.Columns.Add("masuk_ap", GetType(String))
            dt.Columns.Add("jenis", GetType(String))
            dt.Columns.Add("kod_kerajaan", GetType(String))
            dt.Columns.Add("jenis_trans", GetType(String))


            Dim kod As String
            Dim butir As String
            Dim kwsp As String
            Dim perkeso As String
            Dim cukai As String
            Dim pencen As String
            Dim vottetap As String
            Dim votxtetap As String
            Dim jenis As String
            Dim agensi As String
            Dim strSql As String
            Dim jenistrans As String

            Dim searchDDL = ddlJenis.SelectedValue

            If searchDDL = "-" Then
                strSql = "SELECT kod_Trans,butiran,Kira_Kwsp,Kira_Perkeso,Kira_Cukai,Kira_Pencen,isnull(Vot_Tetap,'-') as vot_tetap,isnull(Vot_Bukan_Tetap,'-') as Vot_Bukan_Tetap,masuk_ap,jenis,kod_kerajaan,jenis_trans from smkb_gaji_kod_trans order by kod_trans"

            Else
                strSql = "SELECT kod_Trans,butiran,Kira_Kwsp,Kira_Perkeso,Kira_Cukai,Kira_Pencen,isnull(Vot_Tetap,'-') as vot_tetap,isnull(Vot_Bukan_Tetap,'-') as Vot_Bukan_Tetap,masuk_ap,jenis,kod_kerajaan,jenis_trans from smkb_gaji_kod_trans where jenis_trans = '" & searchDDL & "' order by kod_trans"

            End If

            Dim dbconn As New DBKewConn
            dsKod = dbconn.fSelectCommand(strSql)
            dvKodKW = New DataView(dsKod.Tables(0))

            For i As Integer = 0 To dsKod.Tables(0).Rows.Count - 1

                kod = dsKod.Tables(0).Rows(i)(0).ToString
                butir = dsKod.Tables(0).Rows(i)(1).ToString
                kwsp = dsKod.Tables(0).Rows(i)(2).ToString
                perkeso = dsKod.Tables(0).Rows(i)(3).ToString
                cukai = dsKod.Tables(0).Rows(i)(4).ToString
                pencen = dsKod.Tables(0).Rows(i)(5).ToString
                vottetap = dsKod.Tables(0).Rows(i)(6).ToString
                votxtetap = dsKod.Tables(0).Rows(i)(7).ToString
                jenis = dsKod.Tables(0).Rows(i)(8).ToString
                agensi = dsKod.Tables(0).Rows(i)(9).ToString
                jenistrans = dsKod.Tables(0).Rows(i)(10).ToString


                dt.Rows.Add(kod, butir, kwsp, perkeso, cukai, pencen, vottetap, votxtetap, jenis, agensi, jenistrans)
            Next

            Return dt

        Catch ex As Exception

        End Try

    End Function

    Protected Sub lbtnCari_Click(sender As Object, e As EventArgs)
        Dim kod As String = ddlJenis.SelectedValue

        fBindGvJenis()
    End Sub
    Private Sub gvJenis_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvJenis.RowCommand
        Try
            Dim strSql As String
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim selectedRow As GridViewRow = gvJenis.Rows(index)
            Dim kod As String = ddlJenis.SelectedValue

            If e.CommandName = "EditRow" Then

                'Dim kod As String = ViewState("kod")
                txtJenis.Text = selectedRow.Cells(0).Text.ToString
                txtButir.Text = selectedRow.Cells(1).Text.ToString
                ddlVotTetap.SelectedValue = selectedRow.Cells(6).Text.ToString
                ddlVotBknTetap.SelectedValue = selectedRow.Cells(7).Text.ToString

                If selectedRow.Cells(2).Text.ToString = "True" Then
                    rbKWSP.SelectedValue = "1"
                Else
                    rbKWSP.SelectedValue = "2"
                End If
                If selectedRow.Cells(3).Text.ToString = "True" Then
                    rbPerkeso.SelectedValue = "1"
                Else
                    rbPerkeso.SelectedValue = "2"
                End If
                If selectedRow.Cells(4).Text.ToString = "True" Then
                    rbCukai.SelectedValue = "1"
                Else
                    rbCukai.SelectedValue = "2"
                End If
                If selectedRow.Cells(5).Text.ToString = "True" Then
                    rbPencen.SelectedValue = "1"
                Else
                    rbPencen.SelectedValue = "2"
                End If
                'If selectedRow.Cells(10).Text.ToString = "P" Then
                '    rbAP.Visible = True

                '    'jpot.Style.Add("display", "none")
                'Else
                '    rbAP.Visible = False

                'End If

                ViewState("SaveMode") = "2"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Pop", "ShowPopup('2');", True)

            ElseIf e.CommandName = "DeleteRow" Then
                ViewState("SaveMode") = "3"
                Dim jenis As String = selectedRow.Cells(0).Text.ToString

                If fCheckMaster(jenis) = False Then
                    strSql = "delete from SMKB_Gaji_kod_Trans where kod_trans = '" & jenis & "'"
                    dbconn.sConnBeginTrans()
                    If dbconn.fUpdateCommand(strSql) > 0 Then
                        dbconn.sConnCommitTrans()

                        lblModalMessaage.Text = "Rekod telah dipadam!" 'message di modal
                        ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
                        fBindGvJenis()
                    Else
                        dbconn.sConnRollbackTrans()
                        lblModalMessaage.Text = "Rekod gagal dipadam!" 'message di modal
                        ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
                    End If

                Else

                    lblModalMessaage.Text = "Rekod tidak dapat dipadam! Terdapat rekod potongan ini dalam master gaji!" 'message di modal
                    ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Function fCheckMaster(ByVal strKod As String) As Boolean
        Dim strSql = "select count(*) from smkb_gaji_master  where kod_trans = '" & strKod & "'"
        Dim intCnt As Integer = dbconn.fSelectCount(strSql)
        If intCnt < 1 Then
            Return False
        Else
            Return True
        End If

    End Function
    Protected Sub lbtnSave_Click(sender As Object, e As EventArgs) Handles lbtnSave.ServerClick

        Dim strSql As String
        Dim dbconn As New DBKewConn
        Try
            Dim strJenis As String = Trim(ddlJenis.SelectedValue.TrimEnd)
            Dim strKod As String = Trim(txtJenis.Text.TrimEnd)
            Dim strButir As String = Trim(txtButir.Text.ToUpper.TrimEnd)
            Dim vottetap As String = Trim(ddlVotTetap.SelectedValue.TrimEnd)
            Dim votxtetap As String = Trim(ddlVotTetap.SelectedValue.TrimEnd)
            Dim agensi As String = Trim(ddlAgensi.SelectedValue.TrimEnd)
            Dim jenis As String = Trim(ddlJnsPot.SelectedValue.TrimEnd)
            'Dim strDaripada As String = Trim(txtDaripada.Text.ToUpper.TrimEnd)
            'Dim valSave As String = hdnSimpan.Value
            Dim kod As String = ddlJenis.SelectedValue
            Dim kwsp As Byte
            Dim socso As Byte
            Dim cukai As Byte
            Dim pencen As Byte
            Dim masukap As Byte

            If rbKWSP.SelectedValue = "1" Then
                kwsp = 1
            Else
                kwsp = 0
            End If
            If rbPerkeso.SelectedValue = "1" Then
                socso = 1
            Else
                socso = 0
            End If
            If rbCukai.SelectedValue = "1" Then
                cukai = 1
            Else
                cukai = 0
            End If
            If rbPencen.SelectedValue = "1" Then
                pencen = 1
            Else
                pencen = 0
            End If
            If rbAP.SelectedValue = "1" Then
                masukap = 1
            Else
                masukap = 0
            End If

            strSql = "select count(*) from SMKB_Gaji_kod_Trans where kod_Trans = '" & strKod & "'"
            If fCheckRec(strSql) = 0 Then

                strSql = "insert into SMKB_Gaji_kod_Trans (kod_trans,Butiran,Kira_Kwsp,Kira_Perkeso,Kira_Cukai,Kira_Pencen,Vot_Tetap,Vot_Bukan_Tetap,Masuk_AP,Kod_Kerajaan,Jenis) values (@Kod,@Butir,@kwsp,@socso,@cukai,@pencen,@vottetap,@votxtetap,@masukap,@agensi,@jenis)"
                Dim paramSql() As SqlParameter = {
                    New SqlParameter("@Kod", strKod),
                    New SqlParameter("@Butir", strButir),
                        New SqlParameter("@kwsp", kwsp),
                        New SqlParameter("@socso", socso),
                        New SqlParameter("@cukai", cukai),
                        New SqlParameter("@pencen", pencen),
                        New SqlParameter("@vottetap", vottetap),
                        New SqlParameter("@votxtetap", votxtetap),
                        New SqlParameter("@masukap", masukap),
                        New SqlParameter("@agensi", agensi),
                        New SqlParameter("@jenis", jenis)
                }

                dbconn.sConnBeginTrans()
                If dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                    dbconn.sConnCommitTrans()
                    'fGlobalAlert("Rekod baru telah disimpan!", Me.Page, Me.[GetType]())
                    fBindGvJenis()
                    'fReset()
                    lblModalMessaage.Text = "Rekod baru telah disimpan." 'message di modal
                    ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
                Else
                    dbconn.sConnRollbackTrans()
                    'fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
                    lblModalMessaage.Text = "Ralat!" 'message di modal
                    ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
                End If
            Else
                strSql = "UPDATE SMKB_Gaji_kod_Trans SET Butiran=@Butir,Kira_Kwsp=@kwsp,Kira_Perkeso=@socso,Kira_Cukai=@cukai,Kira_Pencen=@pencen,Vot_Tetap=@vottetap,Vot_Bukan_Tetap=@votxtetap,Masuk_AP=@masukap,Kod_Kerajaan=@agensi,Jenis=@jenis WHERE kod_Trans=@Kod"

                Dim paramSql() As SqlParameter = {
                        New SqlParameter("@Butir", strButir),
                        New SqlParameter("@kwsp", kwsp),
                        New SqlParameter("@socso", socso),
                        New SqlParameter("@cukai", cukai),
                        New SqlParameter("@pencen", pencen),
                        New SqlParameter("@vottetap", vottetap),
                        New SqlParameter("@votxtetap", votxtetap),
                        New SqlParameter("@masukap", masukap),
                        New SqlParameter("@agensi", agensi),
                        New SqlParameter("@jenis", jenis),
                        New SqlParameter("@Kod", strKod)
                    }

                dbconn = New DBKewConn
                dbconn.sConnBeginTrans()
                If dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                    dbconn.sConnCommitTrans()
                    'Alert("Rekod telah dikemaskini!")
                    fBindGvJenis()
                    lblModalMessaage.Text = "Rekod telah dikemaskini." 'message di modal
                    ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
                Else
                    dbconn.sConnRollbackTrans()
                    'Alert("Rekod gagal dikemaskini!")
                    lblModalMessaage.Text = "Ralat!" 'message di modal
                End If
            End If


        Catch ex As Exception

        End Try


    End Sub
End Class