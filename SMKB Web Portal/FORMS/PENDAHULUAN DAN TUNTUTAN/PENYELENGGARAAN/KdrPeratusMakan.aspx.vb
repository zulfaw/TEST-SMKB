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

Public Class KdrPeratusMakan
    Inherits System.Web.UI.Page
    Dim dbconn As New DBKewConn
    Dim dt As New DataTable
    Dim strSql As String
    Dim ds As New DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                fLoadSenarai()
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
            dt.Columns.Add("PeratusMknID", GetType(String))
            dt.Columns.Add("Kod", GetType(String))
            dt.Columns.Add("Butiran", GetType(String))
            dt.Columns.Add("Kadar", GetType(String))



            strSql = "SELECT PeratusMknID, Kod, Butiran, Kadar FROM SMKB_CLM_PeratusMkn"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            Dim strId, strKod, strButiran, strKadar As String


            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1

                        strId = ds.Tables(0).Rows(i)("PeratusMknID")
                        strKod = ds.Tables(0).Rows(i)("Kod")
                        strButiran = ds.Tables(0).Rows(i)("Butiran")
                        strKadar = ds.Tables(0).Rows(i)("Kadar")

                        dt.Rows.Add(strId, strKod, strButiran, strKadar)
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

    Protected Sub gvSenarai_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvSenarai.RowCommand
        Try
            If e.CommandName = "Select" Then

                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                'Dim index As Integer = gvSenarai.SelectedRow.RowIndex
                Dim selectedRow As GridViewRow = gvSenarai.Rows(index)
                Dim lnkbtn As LinkButton = CType(selectedRow.FindControl("lnkButton"), LinkButton)
                Dim Id As String = lnkbtn.Attributes("data-id")


                'Call sql
                strSql = $"SELECT PeratusMknID, Kod, Butiran, Kadar FROM SMKB_CLM_PeratusMkn
                                    Where PeratusMknID = '{Id}' "

                Dim dt As New DataTable
                dt = dbconn.fSelectCommandDt(strSql)
                If dt.Rows.Count > 0 Then
                    txtKod1.Text = dt.Rows(0)("Kod")
                    txtButiran.Text = dt.Rows(0)("Butiran")
                    txtKdr.text = dt.Rows(0)("Kadar")
                    HidID.Value = dt.Rows(0)("PeratusMknID")

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
        Dim strKadar As String = Trim(txtKdr.Text.TrimEnd)
        Dim strKod As String = Trim(txtKod1.Text.TrimEnd)
        Dim strButiran As String = txtButiran.text
        Dim strID As String = hidID.Value
        Dim bil As Integer


        Try
            'If ViewState("SaveMode") = 1 Then
            '--Edit--
            'INSERT

            strSql = $"SELECT PeratusMknID, Kod, Butiran, Kadar FROM SMKB_CLM_PeratusMkn WHERE PeratusMknID = '{strID}' "
            If fCheckRec(strSql) = 0 Then
                strSql1 = " select TOP 1 PeratusMknID from SMKB_CLM_PeratusMkn ORDER BY PeratusMknID DESC "
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
                strSql = "insert into SMKB_CLM_PeratusMkn (PeratusMknID, Kod, Butiran, Kadar) values (@strID, @strJnsKend, @strKM, @strKadar)"
                Using connection As New SqlConnection(strConnString)
                    Dim command As New SqlCommand(strSql, connection)
                    command.Parameters.AddWithValue("@strID", strID)
                    command.Parameters.AddWithValue("@strKod", strKod)
                    command.Parameters.AddWithValue("@strButiran", strButiran)
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
                strSql = "UPDATE SMKB_CLM_PeratusMkn set Kod = @strKod, Butiran = @strButiran, Kadar = @strKadar where PeratusMknID = @strID"
                Using connection As New SqlConnection(strConnString)
                    Dim command As New SqlCommand(strSql, connection)
                        command.Parameters.Add("@strID", SqlDbType.Int)
                        command.Parameters("@strID").Value = strID
                        command.Parameters.AddWithValue("@strKod", strKod)
                    command.Parameters.AddWithValue("@strButiran", strButiran)
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

        txtKod1.Text = ""
        txtButiran.Text = ""
        txtKdr.text = ""
        hidID.Value = ""

    End Function
End Class