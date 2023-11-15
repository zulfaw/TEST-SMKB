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

Public Class checklist
    Inherits System.Web.UI.Page
    Dim dbconn As New DBKewConn
    Dim dt As New DataTable
    Dim strSql As String
    Dim ds As New DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not IsPostBack Then

                fLoadSenarai()
                sBindPendahuluan()
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
            dt.Columns.Add("ID_CheckList", GetType(String))
            dt.Columns.Add("ID_JenisADV", GetType(String))
            dt.Columns.Add("ButirADV", GetType(String))
            dt.Columns.Add("Butiran", GetType(String))
            dt.Columns.Add("Status", GetType(Boolean))



            strSql = "SELECT   a.ID_CheckList, a.ID_JenisADV,b.Butiran AS ButirADV, a.Butiran, a.Status
                FROM  SMKB_Pendahuluan_ID_CheckList AS a INNER JOIN
                SMKB_Pendahuluan_Jenis_ADV AS b ON a.ID_JenisADV = b.ID_JenisADV "

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            Dim strId, strJnsPend, strButirPend, strLampiran As String
            Dim strStatus As Boolean

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        strId = ds.Tables(0).Rows(i)("ID_CheckList")
                        strJnsPend = ds.Tables(0).Rows(i)("ID_JenisADV")
                        strButirPend = ds.Tables(0).Rows(i)("ButirADV")
                        strLampiran = ds.Tables(0).Rows(i)("Butiran")
                        strStatus = CBool(ds.Tables(0).Rows(i)("Status"))

                        dt.Rows.Add(strId, strJnsPend, strButirPend, strLampiran, strStatus)
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


    Private Sub sBindPendahuluan()
        Try
            Dim strSql As String = "SELECT DISTINCT  a.ID_JenisADV,b.Butiran AS ButirADV
            FROM SMKB_Pendahuluan_ID_CheckList AS a INNER JOIN
            SMKB_Pendahuluan_Jenis_ADV AS b ON a.ID_JenisADV = b.ID_JenisADV"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            ddlPendahuluan.DataSource = ds
            ddlPendahuluan.DataTextField = "ButirADV"
            ddlPendahuluan.DataValueField = "ID_JenisADV"
            ddlPendahuluan.DataBind()

            ddlPendahuluan.Items.Insert(0, New ListItem("- Sila Pilih -", "0"))
            ddlPendahuluan.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Private Sub sBindStatus()
        Try
            Dim strSql As String = "SELECT DISTINCT Status, CASE WHEN Status = 0 then 'Tidak Aktif' When Status = 1 Then 'Aktif' END AS Butiran FROM  SMKB_Pendahuluan_ID_CheckList"

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

    Protected Sub gvSenarai_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvSenarai.RowCommand
        Try
            If e.CommandName = "Select" Then

                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                'Dim index As Integer = gvSenarai.SelectedRow.RowIndex
                Dim selectedRow As GridViewRow = gvSenarai.Rows(index)
                Dim strStatus As String = selectedRow.Cells(2).Text.ToString
                Dim lnkbtn As LinkButton = CType(selectedRow.FindControl("lnkButton"), LinkButton)
                Dim strHidID As String = lnkbtn.Attributes("data-id")


                'Call sql
                strSql = $"SELECT   a.ID_CheckList, a.ID_JenisADV,b.Butiran AS ButirADV, a.Butiran, a.Status
                FROM  SMKB_Pendahuluan_ID_CheckList AS a INNER JOIN
                SMKB_Pendahuluan_Jenis_ADV AS b ON a.ID_JenisADV = b.ID_JenisADV WHERE a.ID_CheckList = '{strHidID}' "

                Dim dt As New DataTable
                dt = dbconn.fSelectCommandDt(strSql)
                If dt.Rows.Count > 0 Then
                    sBindPendahuluan()
                    sBindStatus()
                    txtLampiran.Text = dt.Rows(0)("Butiran")
                    ddlPendahuluan.SelectedValue = dt.Rows(0)("ID_JenisADV")
                    strStatus = dt.Rows(0)("Status")
                    If strStatus = "False" Then
                        ddlStatus.SelectedValue = dt.Rows(0)("Status")
                        'ddlStatus.SelectedItem.Text = "Tidak Aktif"
                    Else
                        'ddlStatus.SelectedItem.Value = 1
                        ddlStatus.SelectedValue = dt.Rows(0)("Status")
                    End If

                    hidID.Value = dt.Rows(0)("ID_CheckList")
                End If
                ViewState("SaveMode") = 2 '--tuk edit data

                ClientScript.RegisterStartupScript([GetType](), "alert", "ShowPopup('1');", True)

            ElseIf e.CommandName = "OpenModal" Then

                ClientScript.RegisterStartupScript([GetType](), "alert", "ShowPopup('3');", True)

            End If
        Catch ex As Exception

        End Try
    End Sub
    'Protected Sub lbtnTutup_Click(sender As Object, e As EventArgs) Handles lbtnTutup.Click
    '    ClearFill()
    'End Sub

    Protected Sub lbtnSimpan_Click(sender As Object, e As EventArgs) Handles lbtnSimpan.ServerClick

        Dim dbconn As New DBKewConn
        Dim strConnString As String = dbKewConnStr()
        Dim strSql1 As String
        Dim strSql As String
        Dim strHidID As String = Trim(hidID.Value)
        Dim strJnsPend As String = Trim(ddlPendahuluan.SelectedValue)
        Dim strButiran As String = Trim(txtLampiran.Text.TrimEnd)
        Dim strStatus As String = Trim(ddlStatus.SelectedValue)
        Dim bil As Integer


        Try
            'If ViewState("SaveMode") = 1 Then
            '--Edit--
            'INSERT
            ''SELECT        ID_CheckList, ID_JenisADV, Butiran, Status FROM            SMKB_Pendahuluan_ID_CheckList

            strSql = $"SELECT ID_CheckList  FROM SMKB_Pendahuluan_ID_CheckList WHERE ID_CheckList = '{strHidID}'"
            If fCheckRec(strSql) = 0 Then
                strSql1 = " SELECT TOP 1 ID_CheckList  FROM SMKB_Pendahuluan_ID_CheckList ORDER BY ID_CheckList DESC "
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
                strSql = "INSERT INTO SMKB_Pendahuluan_ID_CheckList (ID_CheckList, ID_JenisADV, Butiran, Status) values 
                                 (@strHidID, @strJnsPend, @strButiran, @strStatus)"

                Using connection As New SqlConnection(strConnString)
                    Dim command As New SqlCommand(strSql, connection)
                    command.Parameters.AddWithValue("@strHidID", strHidID)
                    command.Parameters.AddWithValue("@strJnsPend", strJnsPend)
                    command.Parameters.AddWithValue("@strButiran", strButiran)
                    command.Parameters.AddWithValue("@strStatus", strStatus)

                    Try
                        connection.Open()
                        Dim rowsAffected As Integer = command.ExecuteNonQuery()

                        If rowsAffected > 0 Then
                            dbconn.sConnCommitTrans()
                            'fGlobalAlert("Rekod baru telah disimpan!", Me.Page, Me.[GetType]())
                            lblModalMessaage.Text = "Rekod baru telah disimpan" 'message di modal
                            ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
                            fLoadSenarai()
                            ClearFill()
                        End If

                    Catch ex As Exception
                        dbconn.sConnRollbackTrans()
                        'fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
                        lblModalMessaage.Text = "Ralat!" 'message di modal
                        ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
                    End Try
                End Using

            Else
                '---UPDATE  
                strSql = "UPDATE SMKB_Pendahuluan_ID_CheckList set ID_JenisADV = @strJnsPend, Butiran = @strButiran, Status = @strStatus WHERE ID_CheckList = @strHidID"

                Using connection As New SqlConnection(strConnString)
                    Dim command As New SqlCommand(strSql, connection)
                    command.Parameters.Add("@strHidID", SqlDbType.VarChar)
                    command.Parameters("@strHidID").Value = strHidID
                    command.Parameters.AddWithValue("@strJnsPend", strJnsPend)
                    command.Parameters.AddWithValue("@strButiran", strButiran)
                    command.Parameters.AddWithValue("@strStatus", strStatus)


                    Try
                        connection.Open()
                        Dim rowsAffected As Integer = command.ExecuteNonQuery()

                        If rowsAffected > 0 Then
                            lblModalMessaage.Text = "Rekod telah dikemaskini." 'message di modal
                            ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
                            fLoadSenarai()
                            ClearFill()
                        End If

                    Catch ex As Exception
                        Console.WriteLine(ex.Message)
                        dbconn.sConnRollbackTrans()
                        'fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
                        lblModalMessaage.Text = "Ralat!" 'message di modal
                        ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
                    End Try
                End Using
            End If

        Catch ex As Exception
            ClearFill()
        End Try
    End Sub



    Sub ClearFill()
        ddlPendahuluan.SelectedIndex = 0
        ddlStatus.SelectedIndex = 0
        txtLampiran.Text = ""
        hidID.Value = ""
    End Sub


End Class