Imports System
Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.Net.NetworkInformation

Public Class SelenggaraKenderaan
    Inherits System.Web.UI.Page
    Dim dbconn As New DBKewConn
    Dim dt As New DataTable
    Dim strSql As String
    Dim ds As New DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                fLoadJenis()
                BindDropdown()
                sBindStatus()
                ViewState("savemode") = 1
            End If

            fLoadJenis()
        Catch ex As Exception

        End Try
        fLoadJenis()

    End Sub

    Private Sub fLoadJenis()
        Try
            Dim intRec As Integer
            Dim strSql As String

            'fClearGvSenarai()


            Dim dt As New DataTable
            dt.Columns.Add("No_Staf", GetType(String))
            dt.Columns.Add("No_Kenderaan", GetType(String))
            dt.Columns.Add("Jenis_Kenderaan", GetType(String))
            dt.Columns.Add("Model_Kenderaan", GetType(String))
            dt.Columns.Add("Status", GetType(Boolean))
            dt.Columns.Add("ButiranStatus", GetType(String))



            strSql = $"SELECT a.No_Staf, a.No_Kenderaan, a.Jenis_Kenderaan, b.Butiran, a.Model_Kenderaan, a.Status, case when a.Status =1 then 'Aktif' ELSE 'Tidak Aktif' End as ButiranStatus From SMKB_Tuntutan_Dftr_Kenderaan a, SMKB_Lookup_Detail b Where a.Jenis_Kenderaan = b.Kod_Detail
                     And b.Kod = 'AC09' and No_Staf ='{Session("ssusrID")}' ORDER BY No_Kenderaan"


            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            Dim strStaf, strKenderaan, strJenis, strModelK, strButiran As String
            Dim strStatus As Boolean

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        strStaf = ds.Tables(0).Rows(i)("No_Staf")
                        strKenderaan = ds.Tables(0).Rows(i)("No_Kenderaan")
                        strJenis = ds.Tables(0).Rows(i)("Butiran")
                        strModelK = ds.Tables(0).Rows(i)("Model_Kenderaan")
                        strStatus = CBool(ds.Tables(0).Rows(i)("Status"))
                        strButiran = ds.Tables(0).Rows(i)("ButiranStatus")

                        dt.Rows.Add(strStaf, strKenderaan, strJenis, strModelK, strStatus, strButiran)
                    Next
                    gvJenis.DataSource = dt
                    gvJenis.DataBind()
                    ViewState("dtJenis") = dt
                    intRec = ds.Tables(0).Rows.Count
                End If
            End If

            'lblJumRekod.InnerText = intRec

        Catch ex As Exception

        End Try
    End Sub

    Private Sub BindDropdown()
        Try

            Dim strSql As String = "SELECT Kod_Detail, Butiran FROM SMKB_Lookup_Detail WHERE Kod = 'ac09' AND Kod_Detail IN ('02', '07')"


            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            ddlJKenderaan.DataSource = ds
            ddlJKenderaan.DataTextField = "Butiran"
            ddlJKenderaan.DataValueField = "Kod_Detail"
            ddlJKenderaan.DataBind()

            ddlJKenderaan.Items.Insert(0, New ListItem("- Sila Pilih -"))
            ddlJKenderaan.SelectedIndex = 0

        Catch ex As Exception
            ' Handle the exception
        End Try
    End Sub

    Private Sub sBindStatus()
        Try
            Dim strSql As String = $"SELECT DISTINCT Status, CASE WHEN Status = 1 then 'Aktif' When Status = 0 Then 'Tidak Aktif' END AS Butiran FROM  SMKB_Tuntutan_Dftr_Kenderaan "

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            ddlStatus.DataSource = ds
            ddlStatus.DataTextField = "Butiran"
            ddlStatus.DataValueField = "Status"
            ddlStatus.DataBind()

            ddlStatus.Items.Insert(0, New ListItem("- Sila Pilih -", "0"))
            ddlStatus.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub gvJenis_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvJenis.RowCommand
        Try
            If e.CommandName = "EditRow" Then

                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                'Dim index As Integer = gvSenarai.SelectedRow.RowIndex
                Dim selectedRow As GridViewRow = gvJenis.Rows(index)
                Dim NoKend As String = selectedRow.Cells(2).Text.ToString
                Dim strStatus As String = selectedRow.Cells(4).Text.ToString
                Dim NOstaf As String = Session("ssusrID")


                'Call sql
                strSql = $"Select a.No_Staf, a.No_Kenderaan, a.Jenis_Kenderaan, b.Butiran, a.Model_Kenderaan, a.Status, Case When a.Status =1 Then 'Aktif' ELSE 'Tidak Aktif' End as Status From SMKB_Tuntutan_Dftr_Kenderaan a, SMKB_Lookup_Detail b Where a.Jenis_Kenderaan = b.Kod_Detail
                     And b.Kod = 'AC09' AND No_Kenderaan = '{NoKend}' "

                Dim dt As New DataTable
                dt = dbconn.fSelectCommandDt(strSql)
                If dt.Rows.Count > 0 Then
                    BindDropdown()
                    sBindStatus()
                    txtNoPend_Kenderaan.Text = dt.Rows(0)("No_Kenderaan")
                    ddlJKenderaan.SelectedValue = dt.Rows(0)("Jenis_Kenderaan")
                    txtModelKenderaan.Text = dt.Rows(0)("Model_Kenderaan")
                    strStatus = dt.Rows(0)("Status")
                    If strStatus = "False" Then
                        ddlStatus.SelectedValue = dt.Rows(0)("Status")
                        'ddlStatus.SelectedItem.Text = "Tidak Aktif"
                    Else
                        'ddlStatus.SelectedItem.Value = 1
                        ddlStatus.SelectedValue = dt.Rows(0)("Status")
                    End If

                    hidID.Value = dt.Rows(0)("No_Staf")
                End If
                ViewState("SaveMode") = 2 '--tuk edit data

                ClientScript.RegisterStartupScript([GetType](), "alert", "ShowPopup('1');", True)

            ElseIf e.CommandName = "OpenModal" Then

                ClientScript.RegisterStartupScript([GetType](), "alert", "ShowPopup('2');", True)

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub lbtnSimpan_Click(sender As Object, e As EventArgs) Handles lbtnSimpan.ServerClick

        Dim strSql As String
        Dim strKod As String = Trim(hidID.Value)
        Dim strKenderaan As String = Trim(txtNoPend_Kenderaan.Text)
        Dim strJenis As String = Trim(ddlJKenderaan.SelectedValue)
        Dim strModelK As String = Trim(txtModelKenderaan.Text)
        Dim strStatus As String = Trim(ddlStatus.SelectedValue)
        Dim strConnString As String = dbKewConnStr()
        Dim ds As New DataSet
        Dim dbconn As New DBKewConn
        Dim bil As Integer = 0
        Dim NOstaf As String = Session("ssusrID")


        Try
            'If ViewState("SaveMode") = 1 Then
            '--Edit--
            'INSERT
            strSql = $"SELECT No_Staf FROM  SMKB_Tuntutan_Dftr_Kenderaan WHERE No_Staf = '{NOstaf}' and No_Kenderaan ='{strKenderaan}'"
            If fCheckRec(strSql) = 0 Then

                strSql = "INSERT INTO SMKB_Tuntutan_Dftr_Kenderaan (No_Staf, No_Kenderaan, Jenis_Kenderaan, Model_Kenderaan, Status ) values (@strNOstaf, @strKenderaan, @strJenis, @strModelK,  @strStatus) "

                'Using connection As New SqlConnection(strConnString)
                '    Dim command As New SqlCommand(strSql, connection)
                '    command.Parameters.AddWithValue("@strHidID", strHidID)
                '    command.Parameters.AddWithValue("@strKenderaan", strKenderaan)
                '    command.Parameters.AddWithValue("@strJenis", strJenis)
                '    command.Parameters.AddWithValue("@strModelK", strModelK)
                '    command.Parameters.AddWithValue("@strStatus", strStatus)

                Dim paramSql() As SqlParameter = {
                    New SqlParameter("@strNOstaf", NOstaf),
                    New SqlParameter("@strKenderaan", strKenderaan),
                    New SqlParameter("@strJenis", strJenis),
                    New SqlParameter("@strModelK", strModelK),
                    New SqlParameter("@strStatus", strStatus)
                }
                dbconn = New DBKewConn
                dbconn.sConnBeginTrans()
                If dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                    dbconn.sConnCommitTrans()
                    'fGlobalAlert("Rekod baru telah disimpan!", Me.Page, Me.[GetType]())
                    lblModalMessage.Text = "Rekod baru telah disimpan" 'message di modal
                    ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
                    fLoadJenis()
                    clearFill()
                Else
                    dbconn.sConnRollbackTrans()
                    'fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
                    lblModalMessage.Text = "Ralat!" 'message di modal
                    ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
                End If


                '    Try
                '        connection.Open()
                '        Dim rowsAffected As Integer = command.ExecuteNonQuery()

                '        If rowsAffected > 0 Then
                '            dbconn.sConnCommitTrans()
                '            'fGlobalAlert("Rekod baru telah disimpan!", Me.Page, Me.[GetType]())
                '            lblModalMessage.Text = "Rekod baru telah disimpan" 'message di modal
                '            ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
                '            fLoadJenis()
                '            clearFill()
                '        End If

                '    Catch ex As Exception
                '        dbconn.sConnRollbackTrans()
                '        'fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
                '        lblModalMessage.Text = "Ralat!" 'message di modal
                '        ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
                '    End Try
                'End Using

            Else

                '---UPDATE  
                strSql = "UPDATE SMKB_Tuntutan_Dftr_Kenderaan set Jenis_Kenderaan = @strJenis, Model_Kenderaan = @strModelK, Status = @strStatus WHERE No_Staf = @strNOstaf AND No_Kenderaan = @strKenderaan"
                'Using connection As New SqlConnection(strConnString)
                '    Dim command As New SqlCommand(strSql, connection)
                '    command.Parameters.Add("@strKod", SqlDbType.VarChar)
                '    command.Parameters("@strKod").Value = strKod
                '    command.Parameters.AddWithValue("@strKenderaan", strKenderaan)
                '    command.Parameters.AddWithValue("@strJenis", strJenis)
                '    command.Parameters.AddWithValue("@strModelK", strModelK)
                '    command.Parameters.AddWithValue("@strStatus", strStatus)


                Dim paramSql() As SqlParameter = {
                    New SqlParameter("@strNOstaf", NOstaf),
                    New SqlParameter("@strKenderaan", strKenderaan),
                    New SqlParameter("@strJenis", strJenis),
                    New SqlParameter("@strModelK", strModelK),
                    New SqlParameter("@strStatus", strStatus)
                }

                dbconn.sConnBeginTrans()
                If dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
                    dbconn.sConnCommitTrans()
                    'fGlobalAlert("Rekod telah dikemaskini!", Me.Page, Me.[GetType]())
                    lblModalMessage.Text = "Rekod telah dikemaskini!" 'message di modal
                    ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
                    fLoadJenis()
                    clearFill()
                Else
                    dbconn.sConnRollbackTrans()
                    'fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
                    lblModalMessage.Text = "Ralat!" 'message di modal
                    ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
                End If
            End If

        Catch ex As Exception

        End Try


    End Sub

    Function clearFill()

        txtNoPend_Kenderaan.Text = ""
        ddlJKenderaan.SelectedIndex = 0
        txtModelKenderaan.Text = ""
        ddlStatus.SelectedIndex = 0
        hidID.Value = ""


    End Function


    Protected Sub ddlJKenderaan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlJKenderaan.SelectedIndexChanged

    End Sub
End Class