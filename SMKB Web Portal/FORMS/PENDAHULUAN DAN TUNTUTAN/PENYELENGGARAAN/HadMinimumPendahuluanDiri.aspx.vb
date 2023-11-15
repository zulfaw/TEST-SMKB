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

Public Class HadMinimumPendahuluanDiri
    Inherits System.Web.UI.Page
    Dim dbconn As New DBKewConn
    Dim dt As New DataTable
    Dim strSql As String
    Dim ds As New DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                fLoadSenarai()
                sBindKump()
                sBindStatus()
            End If

            fLoadSenarai()
        Catch ex As Exception

        End Try

        fLoadSenarai()
    End Sub

    Private Sub fLoadSenarai()
        Try
            Dim intRec As Integer
            Dim strSql As String

            'fClearGvSenarai()


            Dim dt As New DataTable
            dt.Columns.Add("ID_HadMin", GetType(String))
            dt.Columns.Add("Dari", GetType(String))
            dt.Columns.Add("Hingga", GetType(String))
            dt.Columns.Add("Kump", GetType(String))
            dt.Columns.Add("HadMin", GetType(String))
            dt.Columns.Add("Status", GetType(Boolean))
            dt.Columns.Add("ButiranStatus", GetType(String))



            strSql = "SELECT ID_HadMin, Dari, Hingga, Kump, HadMin, Status, case when Status =1 then 'Aktif' ELSE 'Tidak Aktif' End as ButiranStatus FROM SMKB_Pendahuluan_Had_Min ORDER BY Dari"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            Dim strId, strDari, strHingga, strKump, strHadMin, strButiran As String
            Dim strStatus As Boolean

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        strId = ds.Tables(0).Rows(i)("ID_HadMin")
                        strDari = ds.Tables(0).Rows(i)("Dari")
                        strHingga = ds.Tables(0).Rows(i)("Hingga")
                        strKump = ds.Tables(0).Rows(i)("Kump")
                        strHadMin = ds.Tables(0).Rows(i)("HadMin")
                        strStatus = CBool(ds.Tables(0).Rows(i)("Status"))
                        strButiran = ds.Tables(0).Rows(i)("ButiranStatus")

                        dt.Rows.Add(strId, strDari, strHingga, strKump, strHadMin, strStatus, strButiran)
                    Next
                    gvSenarai.DataSource = dt
                    gvSenarai.DataBind()
                    ViewState("dtSenarai") = dt
                    intRec = ds.Tables(0).Rows.Count
                End If
            End If

            'lblJumRekod.InnerText = intRec

        Catch ex As Exception

        End Try
    End Sub


    Private Sub sBindKump()
        Try
            Dim strSql As String = "SELECT DISTINCT Kump FROM  SMKB_Pendahuluan_Had_Min ORDER BY Kump ASC"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            ddlKumpulan.DataSource = ds
            ddlKumpulan.DataTextField = "Kump"
            ddlKumpulan.DataValueField = "Kump"
            ddlKumpulan.DataBind()

            ddlKumpulan.Items.Insert(0, New ListItem("- Sila Pilih -", "0"))
            ddlKumpulan.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Private Sub sBindStatus()
        Try
            Dim strSql As String = "SELECT DISTINCT Status, CASE WHEN Status = 0 then 'Tidak Aktif' When Status = 1 Then 'Aktif' END AS Butiran FROM  SMKB_Pendahuluan_Had_Min "

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

    'Protected Sub lbtnSimpan_Click(sender As Object, e As EventArgs) Handles lbtnSimpan.Click
    Private Sub lbtnSimpan_Click(sender As Object, e As EventArgs) Handles lbtnSimpan.ServerClick
        Dim strSql1 As String
        Dim strSql As String
        Dim strKod As String = Trim(hidIDP.Value)
        Dim strGredDari As String = Trim(txtGredDari.Text)
        Dim strGredKe As String = Trim(txtGredKe.Text)
        Dim strKump As String = Trim(ddlKumpulan.SelectedValue)
        Dim strHadMin As String = Trim(txtHadMin.Text)
        Dim strStatus As String = Trim(ddlStatus.SelectedValue)
        Dim strHidID As String
        Dim strConnString As String = dbKewConnStr()
        Dim ds As New DataSet
        Dim dbconn As New DBKewConn
        Dim bil As Integer = 0

        Try
            'If ViewState("SaveMode") = 1 Then
            '--Edit--
            'INSERT
            strSql = $"SELECT ID_HadMin FROM  SMKB_Pendahuluan_Had_Min WHERE ID_HadMin = '{strKod}' "
            If fCheckRec(strSql) = 0 Then
                strSql1 = " SELECT TOP 1  ID_HadMin FROM SMKB_Pendahuluan_Had_Min ORDER BY ID_HadMin DESC "
                ds = dbconn.fSelectCommand(strSql1)
                If Not ds Is Nothing Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        bil = ds.Tables(0).Rows(0)(0).ToString
                        bil = bil + 1
                    End If
                Else
                    bil = 1
                End If

                strHidID = bil
                strSql = "INSERT INTO SMKB_Pendahuluan_Had_Min (ID_HadMin,Dari, Hingga, Kump, HadMin, Status ) values (@strHidID, @strGredDari, @strGredKe, @strKump, @strHadMin, @strStatus) "

                'Using connection As New SqlConnection(strConnString)
                '    Dim command As New SqlCommand(strSql, connection)
                '    command.Parameters.AddWithValue("@strHidID", strHidID)
                '    command.Parameters.AddWithValue("@strGredDari", strGredDari)
                '    command.Parameters.AddWithValue("@strGredKe", strGredKe)
                '    command.Parameters.AddWithValue("@strKump", strKump)
                '    command.Parameters.AddWithValue("@strHadMin", strHadMin)
                '    command.Parameters.AddWithValue("@strStatus", strStatus)

                Dim paramSql() As SqlParameter = {
                    New SqlParameter("@strHidID", strHidID),
                    New SqlParameter("@strGredDari", strGredDari),
                    New SqlParameter("@strGredKe", strGredKe),
                    New SqlParameter("@strKump", strKump),
                    New SqlParameter("@strHadMin", strHadMin),
                    New SqlParameter("@strStatus", strStatus)
                }

                    dbconn.sConnBeginTrans()
                    If dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                        dbconn.sConnCommitTrans()
                        'fGlobalAlert("Rekod baru telah disimpan!", Me.Page, Me.[GetType]())
                        lblModalMessaage.Text = "Rekod baru telah disimpan" 'message di modal
                    ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
                    fLoadSenarai()
                    clearFill()
                Else
                        dbconn.sConnRollbackTrans()
                        'fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
                        lblModalMessaage.Text = "Ralat!" 'message di modal
                        ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
                    End If


                '    Try
                '        connection.Open()
                '        Dim rowsAffected As Integer = command.ExecuteNonQuery()

                '        If rowsAffected > 0 Then
                '            dbconn.sConnCommitTrans()
                '            'fGlobalAlert("Rekod baru telah disimpan!", Me.Page, Me.[GetType]())
                '            lblModalMessaage.Text = "Rekod baru telah disimpan" 'message di modal
                '            ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
                '            fLoadSenarai()
                '            clearFill()
                '        End If

                '    Catch ex As Exception
                '        dbconn.sConnRollbackTrans()
                '        'fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
                '        lblModalMessaage.Text = "Ralat!" 'message di modal
                '        ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
                '    End Try
                'End Using

            Else

                '---UPDATE  
                strSql = "UPDATE SMKB_Pendahuluan_Had_Min set Dari = @strGredDari, Hingga = @strGredKe, Kump = @strKump, HadMin = @strHadMin, Status = @strStatus WHERE ID_HadMin = @strKod"
                'Using connection As New SqlConnection(strConnString)
                '    Dim command As New SqlCommand(strSql, connection)
                '    command.Parameters.Add("@strKod", SqlDbType.VarChar)
                '    command.Parameters("@strKod").Value = strKod
                '    command.Parameters.AddWithValue("@strGredDari", strGredDari)
                '    command.Parameters.AddWithValue("@strGredKe", strGredKe)
                '    command.Parameters.AddWithValue("@strKump", strKump)
                '    command.Parameters.AddWithValue("@strHadMin", strHadMin)
                '    command.Parameters.AddWithValue("@strStatus", strStatus)


                Dim paramSql() As SqlParameter = {
                                New SqlParameter("@strKod", strKod),
                                New SqlParameter("@strGredDari", strGredDari),
                                New SqlParameter("@strGredKe", strGredKe),
                                New SqlParameter("@strKump", strKump),
                                New SqlParameter("@strHadMin", strHadMin),
                                New SqlParameter("@strStatus", strStatus)
                            }
                dbconn.sConnBeginTrans()
                If dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
                    dbconn.sConnCommitTrans()
                    'fGlobalAlert("Rekod telah dikemaskini!", Me.Page, Me.[GetType]())
                    lblModalMessaage.Text = "Rekod telah dikemaskini!" 'message di modal
                    ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
                    fLoadSenarai()
                    clearFill()
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

    Function clearFill()

        txtGredDari.Text = ""
        txtGredKe.Text = ""
        txtHadMin.Text = ""
        ddlKumpulan.SelectedIndex = 0
        ddlStatus.SelectedIndex = 0
        hidIDP.Value = ""


    End Function



    Protected Sub gvSenarai_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvSenarai.RowCommand
        Try
            If e.CommandName = "Select" Then

                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                'Dim index As Integer = gvSenarai.SelectedRow.RowIndex
                Dim selectedRow As GridViewRow = gvSenarai.Rows(index)
                Dim strStatus As String = selectedRow.Cells(4).Text.ToString
                Dim lnkbtn As LinkButton = CType(selectedRow.FindControl("lnkButton"), LinkButton)
                Dim Id As String = lnkbtn.Attributes("data-id")


                'Call sql
                strSql = $"SELECT ID_HadMin, Dari, Hingga, Kump, HadMin, Status, case when Status =1 then 'Aktif' ELSE 'Tidak Aktif' End as ButiranStatus FROM SMKB_Pendahuluan_Had_Min WHERE ID_HadMin = '{Id}' "

                Dim dt As New DataTable
                dt = dbconn.fSelectCommandDt(strSql)
                If dt.Rows.Count > 0 Then
                    sBindKump()
                    sBindStatus()
                    txtGredDari.Text = dt.Rows(0)("Dari")
                    txtGredKe.Text = dt.Rows(0)("Hingga")
                    ddlKumpulan.SelectedValue = dt.Rows(0)("Kump")
                    txtHadMin.Text = dt.Rows(0)("HadMin")
                    strStatus = dt.Rows(0)("Status")
                    If strStatus = "False" Then
                        ddlStatus.SelectedValue = dt.Rows(0)("Status")
                        'ddlStatus.SelectedItem.Text = "Tidak Aktif"
                    Else
                        'ddlStatus.SelectedItem.Value = 1
                        ddlStatus.SelectedValue = dt.Rows(0)("Status")
                    End If

                    hidIDP.Value = dt.Rows(0)("ID_HadMin")
                End If
                ViewState("SaveMode") = 2 '--tuk edit data

                ClientScript.RegisterStartupScript([GetType](), "alert", "ShowPopup('1');", True)

            ElseIf e.CommandName = "OpenModal" Then

                ClientScript.RegisterStartupScript([GetType](), "alert", "ShowPopup('3');", True)

            End If
        Catch ex As Exception

        End Try
    End Sub

    'Protected Sub lbtnTutup_Click(sender As Object, e As EventArgs) Handles lbtnTutup.serverclick

    '    clearFill()
    'End Sub


    'Protected Sub gvSenarai_RowDataBound(sender As Object, e As GridViewRowEventArgs)
    '    If e.Row.RowType <> DataControlRowType.DataRow Then
    '        Exit Sub
    '    End If

    '    Dim gvr As GridViewRow = e.Row
    '    Dim btn As Button = CType(gvr.FindControl("btnEdit"), Button)

    'Dim strId As String = selectedRow.Cells(6).Text.ToString
    'e.CommandArgument =

    'Dim strId As Button = CType(gvSenarai.FindControl("btnEdit"), Button)


    '    'Call sql
    '    strSql = $"SELECT ID_HadMin, Dari, Hingga, Kump, HadMin, Status FROM SMKB_Pendahuluan_Had_Min WHERE ID_HadMin = '{btn}' "

    '    Dim dt As New DataTable
    '    dt = dbconn.fSelectCommandDt(strSql)
    '    If dt.Rows.Count > 0 Then
    '        sBindKump()
    '        txtGredDari.Text = dt.Rows(0)("Dari")
    '        txtGredKe.Text = dt.Rows(0)("Hingga")
    '        ddlKumpulan.SelectedValue = dt.Rows(0)("Kump")
    '        txtHadMin.Text = dt.Rows(0)("HadMin")


    '        hidID.Value = dt.Rows(0)("ID_HadMin")

    '    End If


    ' End Sub



    'Protected Sub lbtnTutup_Click(sender As Object, e As EventArgs) Handles lbtnTutup.Click
    '    clearFill()
    'End Sub
End Class