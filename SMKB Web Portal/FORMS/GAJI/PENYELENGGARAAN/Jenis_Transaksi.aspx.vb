Imports System
Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.Net.NetworkInformation


Public Class Jenis_Transaksi
    Inherits System.Web.UI.Page
    Dim dbconn As New DBKewConn
    Dim dbconnSM As New DBSMConn
    Public dsKod As New DataSet
    Public dvKodKW As New DataView

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            fBindGvJenis()

        End If
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
            dt.Columns.Add("Jenis_Trans", GetType(String))
            dt.Columns.Add("Butiran", GetType(String))
            dt.Columns.Add("Daripada", GetType(String))


            Dim jenis As String
            Dim butir As String
            Dim drpd As String



            Dim strSql As String = "SELECT Jenis_Trans,butiran,daripada from smkb_gaji_jenis_trans order by jenis_trans"
            Dim dbconn As New DBKewConn
            dsKod = dbconn.fSelectCommand(strSql)
            dvKodKW = New DataView(dsKod.Tables(0))

            For i As Integer = 0 To dsKod.Tables(0).Rows.Count - 1

                jenis = dsKod.Tables(0).Rows(i)(0).ToString
                butir = dsKod.Tables(0).Rows(i)(1).ToString
                drpd = dsKod.Tables(0).Rows(i)(2).ToString

                dt.Rows.Add(jenis, butir, drpd)
            Next

            Return dt

        Catch ex As Exception

        End Try

    End Function
    Private Sub gvJenis_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvJenis.RowCommand
        Try
            Dim strSql As String
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim selectedRow As GridViewRow = gvJenis.Rows(index)
            If e.CommandName = "EditRow" Then

                'Dim kod As String = ViewState("kod")



                txtJenis.Text = selectedRow.Cells(0).Text.ToString
                txtButir.Text = selectedRow.Cells(1).Text.ToString
                txtDaripada.Text = selectedRow.Cells(2).Text.ToString
                ViewState("SaveMode") = "2"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Pop", "ShowPopup('2');", True)

            ElseIf e.CommandName = "DeleteRow" Then
                ViewState("SaveMode") = "3"
                Dim jenis As String = selectedRow.Cells(0).Text.ToString

                If fCheckMaster(jenis) = False Then
                    strSql = "delete from SMKB_Gaji_Jenis_Trans where jenis_trans = '" & jenis & "'"
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
    Private Sub lbtnSave_Click(sender As Object, e As EventArgs) Handles lbtnSave.ServerClick
        Dim strSql As String
        Dim dbconn As New DBKewConn
        Try
            Dim strKod As String = Trim(txtJenis.Text.TrimEnd)
            Dim strButir As String = Trim(txtButir.Text.ToUpper.TrimEnd)
            Dim strDaripada As String = Trim(txtDaripada.Text.ToUpper.TrimEnd)

            strSql = "select count(*) from SMKB_Gaji_Jenis_Trans where Jenis_Trans = '" & strKod & "'"
            If fCheckRec(strSql) = 0 Then

                strSql = "insert into SMKB_Gaji_Jenis_Trans (Jenis_trans , Butiran, Daripada) values (@Kod,@Butiran,@Drpd)"
                Dim paramSql() As SqlParameter = {
                    New SqlParameter("@Kod", strKod),
                    New SqlParameter("@Butiran", strButir),
                    New SqlParameter("@Drpd", strDaripada)
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
                'lblModalMessaage.Text = "Ralat!Jenis trans telah wujud!" 'message di modal
                'ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
                strSql = "UPDATE SMKB_Gaji_Jenis_Trans SET Butiran=@Butir, Daripada=@Drpd WHERE Jenis_Trans=@Kod"

                Dim paramSql() As SqlParameter = {
                            New SqlParameter("@Butir", strButir),
                            New SqlParameter("@Drpd", strDaripada),
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

    Private Function fCheckMaster(ByVal strKod As String) As Boolean
        Dim strSql = "select count(*) from smkb_gaji_master  where jenis_trans = '" & strKod & "'"
        Dim intCnt As Integer = dbconn.fSelectCount(strSql)
        If intCnt < 1 Then
            Return False
        Else
            Return True
        End If

    End Function
End Class