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

Public Class KadarKenderaan
    Inherits System.Web.UI.Page
    Dim dbconn As New DBKewConn
    Dim dt As New DataTable
    Dim strSql As String
    Dim ds As New DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                fLoadSenarai()
                sBindKend()
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
            dt.Columns.Add("ID", GetType(String))
            dt.Columns.Add("KodKend", GetType(String))
            dt.Columns.Add("ButiranKend", GetType(String))
            dt.Columns.Add("KM", GetType(String))
            dt.Columns.Add("Kadar", GetType(String))



            strSql = "Select c.ID, c.KodKend, b.Butiran As ButiranKend,c.KM,c.Kadar
                        From SMKB_Lookup_Master As a INNER Join
                        SMKB_Lookup_Detail As b On a.Kod = b.Kod INNER Join
                        SMKB_CLM_KdrKenderaan As c On c.KodKend = b.Kod_Detail
                        Where (b.Kod = 'AC09') AND (a.Status = 1)"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            Dim strId, strKodKen, strButirKend, strKM, strKadar As String


            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1

                        strId = ds.Tables(0).Rows(i)("ID")
                        strKodKen = ds.Tables(0).Rows(i)("KodKend")
                        strButirKend = ds.Tables(0).Rows(i)("ButiranKend")
                        strKM = ds.Tables(0).Rows(i)("KM")
                        strKadar = ds.Tables(0).Rows(i)("Kadar")

                        dt.Rows.Add(strId, strKodKen, strButirKend, strKM, strKadar)
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

    Private Sub sBindKend()
        Try
            Dim strSql As String = "SELECT b.Butiran as ButiranKend, c.KodKend
                                    FROM            SMKB_Lookup_Master AS a INNER JOIN
                                    SMKB_Lookup_Detail AS b ON a.Kod = b.Kod INNER JOIN
                                    SMKB_CLM_KdrKenderaan AS c ON c.KodKend = b.Kod_Detail
                                    WHERE        (b.Kod = 'AC09') AND (a.Status = 1)"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            ddlKend.DataSource = ds
            ddlKend.DataTextField = "ButiranKend"
            ddlKend.DataValueField = "KodKend"
            ddlKend.DataBind()

            ddlKend.Items.Insert(0, New ListItem("- Sila Pilih -", "0"))
            ddlKend.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    'Function clearFill()

    '    HidID.Text = ""
    '    txtKM.Text = ""
    '    txtKadar.Text = ""
    '    ddlKend.SelectedIndex = 0

    'End Function

    Protected Sub gvSenarai_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvSenarai.RowCommand
        Try
            If e.CommandName = "Select" Then

                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                'Dim index As Integer = gvSenarai.SelectedRow.RowIndex
                Dim selectedRow As GridViewRow = gvSenarai.Rows(index)
                Dim lnkbtn As LinkButton = CType(selectedRow.FindControl("lnkButton"), LinkButton)
                Dim Id As String = lnkbtn.Attributes("data-id")


                'Call sql
                strSql = $"Select b.Butiran As ButiranKend, c.KodKend, c.KM, c.Kadar, c.ID
                                    From SMKB_Lookup_Master As a INNER Join
                                    SMKB_Lookup_Detail As b On a.Kod = b.Kod INNER Join
                                    SMKB_CLM_KdrKenderaan As c On c.KodKend = b.Kod_Detail
                                    Where (b.Kod = 'AC09')  AND  c.ID = '{Id}' "

                Dim dt As New DataTable
                dt = dbconn.fSelectCommandDt(strSql)
                If dt.Rows.Count > 0 Then
                    sBindKend()
                    txtKM.Text = dt.Rows(0)("KM")
                    txtKadar.Text = dt.Rows(0)("Kadar")
                    ddlKend.SelectedValue = dt.Rows(0)("KodKend")
                    HidID.Value = dt.Rows(0)("ID")

                End If
                ViewState("SaveMode") = 2 '--tuk edit data

                ClientScript.RegisterStartupScript([GetType](), "alert", "ShowPopup('1');", True)

            ElseIf e.CommandName = "OpenModal" Then

                ClientScript.RegisterStartupScript([GetType](), "alert", "ShowPopup('3');", True)

            End If
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub lbtnSimpan_Click(sender As Object, e As EventArgs) Handles lbtnSimpan.ServerClick
        Dim strSql As String
        Dim dbconn As New DBKewConn
        Dim strConnString As String = dbKewConnStr()
        Dim strSql1 As String
        Dim strKadar As String = Trim(txtKadar.Text.TrimEnd)
        Dim strKM As String = Trim(txtKM.Text.TrimEnd)
        Dim strJnsKend As String = ddlKend.SelectedValue
        Dim strID As String = hidID.Value
        Dim bil As Integer


        Try
            'If ViewState("SaveMode") = 1 Then
            '--Edit--
            'INSERT

            strSql = $"SELECT  ID, KodKend, KM, Kadar FROM SMKB_CLM_KdrKenderaan WHERE ID = '{strID}' "
            If fCheckRec(strSql) = 0 Then
                strSql1 = " select TOP 1 ID from SMKB_CLM_KdrKenderaan ORDER BY ID DESC "
                ds = dbconn.fSelectCommand(strSql1)
                If Not ds Is Nothing Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        bil = ds.Tables(0).Rows(0)(0).ToString
                        bil = bil + 1
                    End If
                Else
                    bil = 1
                End If

                strID = bil
                strSql = "insert into SMKB_CLM_KdrKenderaan (ID, KodKend, KM, Kadar) values (@strID, @strJnsKend, @strKM, @strKadar)"
                Using connection As New SqlConnection(strConnString)
                    Dim command As New SqlCommand(strSql, connection)
                    command.Parameters.AddWithValue("@strID", strID)
                    command.Parameters.AddWithValue("@strJnsKend", strJnsKend)
                    command.Parameters.AddWithValue("@strKM", strKM)
                    command.Parameters.AddWithValue("@strKadar", strKadar)

                    Try
                        connection.Open()
                        Dim rowsAffected As Integer = command.ExecuteNonQuery()

                        If rowsAffected > 0 Then
                            dbconn.sConnCommitTrans()
                            'fGlobalAlert("Rekod baru telah disimpan!", Me.Page, Me.[GetType]())
                            lblModalMessaage.Text = "Rekod baru telah disimpan" 'message di modal
                            ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
                            fLoadSenarai()
                            clearFill()
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
                strSql = "UPDATE SMKB_CLM_KdrKenderaan set KodKend = @strJnsKend, KM = @strKM, Kadar = @strKadar where ID = @strID"
                Using connection As New SqlConnection(strConnString)
                    Dim command As New SqlCommand(strSql, connection)
                    command.Parameters.Add("@strID", SqlDbType.Int)
                    command.Parameters("@strID").Value = strID
                    command.Parameters.AddWithValue("@strJnsKend", strJnsKend)
                    command.Parameters.AddWithValue("@strKM", strKM)
                    command.Parameters.AddWithValue("@strKadar", strKadar)


                    Try
                        connection.Open()
                        Dim rowsAffected As Integer = command.ExecuteNonQuery()

                        If rowsAffected > 0 Then
                            lblModalMessaage.Text = "Rekod telah dikemaskini." 'message di modal
                            ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
                            fLoadSenarai()
                            clearFill()
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
            clearFill()
        End Try
    End Sub

    Function clearFill()

        txtKM.Text = ""
        txtKadar.Text = ""
        ddlKend.SelectedIndex = 0
        hidID.Value = ""

    End Function

End Class