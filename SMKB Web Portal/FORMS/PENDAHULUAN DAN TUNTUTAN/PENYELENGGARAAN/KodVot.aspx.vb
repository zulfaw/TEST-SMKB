Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing

Public Class KodVot
    Inherits System.Web.UI.Page
    Dim dbconn As New DBKewConn
    Dim dt As New DataTable
    Dim strSql As String
    Dim ds As New DataSet


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                fLoadSenarai()
                sBindJnsTugas()
            End If
        Catch ex As Exception

        End Try

        fLoadSenarai()
        'sBindJnsTugas()
    End Sub

    Private Sub fLoadSenarai()
        Try
            Dim intRec As Integer
            Dim strSql As String


            Dim dt As New DataTable
            dt.Columns.Add("VotID", GetType(String))
            dt.Columns.Add("Kod", GetType(String))
            dt.Columns.Add("Butiran", GetType(String))
            dt.Columns.Add("KodVot", GetType(String))
            dt.Columns.Add("ButiranTugas", GetType(String))

            strSql = "SELECT  VotID, Kod, Butiran, KodVot, KwsnTugas, CASE WHEN KwsnTugas ='LN' THEN 'LN - LUAR NEGARA' WHEN KwsnTugas='DN' THEN 'DN - DALAM NEGERI' END AS ButiranTugas
                        FROM  SMKB_CLM_Vot ORDER BY VotID ASC"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            Dim strHid, strKod, strButiran, strVot, strButiranTugas As String


            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        strKod = ds.Tables(0).Rows(i)("Kod")
                        strButiran = ds.Tables(0).Rows(i)("Butiran")
                        strVot = ds.Tables(0).Rows(i)("KodVot")
                        strButiranTugas = ds.Tables(0).Rows(i)("ButiranTugas")
                        strHid = ds.Tables(0).Rows(i)("VotID")


                        dt.Rows.Add(strHid, strKod, strButiran, strVot, strButiranTugas)
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

    Private Sub sBindJnsTugas()
        Try
            Dim strSql As String = "SELECT DISTINCT KwsnTugas, CASE WHEN KwsnTugas ='LN' THEN 'LN - LUAR NEGARA' WHEN KwsnTugas='DN' THEN 'DN - DALAM NEGERI' END AS Butiran2
                                    FROM  SMKB_CLM_Vot "

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            ddlJnsTugas.DataSource = ds
            ddlJnsTugas.DataTextField = "Butiran2"
            ddlJnsTugas.DataValueField = "KwsnTugas"
            ddlJnsTugas.DataBind()

            ddlJnsTugas.Items.Insert(0, "--Sila Pilih--")


        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lbtnSimpan_Click(sender As Object, e As EventArgs) Handles lbtnSimpan.Click
        Dim strSql As String
        Dim dbconn As New DBKewConn
        Dim strConnString As String = dbKewConnStr()
        Dim strSql1 As String
        Dim strKod As String = Trim(txtKod.Text.TrimEnd)
        Dim strButiran As String = Trim(txtButiran.Text.ToUpper.TrimEnd)
        Dim strKodVot As String = Trim(txtKodVot.Text.TrimEnd)
        Dim strJnsTugas As String = ddlJnsTugas.SelectedValue
        Dim bil As Integer
        Dim strHidID As String = Trim(hidKodID.Value.TrimEnd)

        Try
            'If ViewState("SaveMode") = 1 Then
            '--Edit--
            'INSERT

            strSql = $"SELECT  VotID, Kod, Butiran, KodVot, KwsnTugas FROM  SMKB_CLM_Vot WHERE VotID = '{strHidID}' "
            If fCheckRec(strSql) = 0 Then
                strSql1 = " select TOP 1 VotID from SMKB_CLM_Vot ORDER BY VotID DESC "
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
                strSql = "insert into SMKB_CLM_Vot (VotID, Kod, Butiran, KodVot, KwsnTugas) values (@strHidID, @strKod, @strButiran, @strKodVot, @strJnsTugas)"
                Using connection As New SqlConnection(strConnString)
                    Dim command As New SqlCommand(strSql, connection)
                    command.Parameters.AddWithValue("@strHidID", strHidID)
                    command.Parameters.AddWithValue("@strKod", strKod)
                    command.Parameters.AddWithValue("@strButiran", strButiran)
                    command.Parameters.AddWithValue("@strKodVot", strKodVot)
                    command.Parameters.AddWithValue("@strJnsTugas", strJnsTugas)

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
                strSql = "UPDATE SMKB_CLM_Vot set Butiran = @strButiran, KwsnTugas = @strJnsTugas, KodVot = @strKodVot where Kod = @strKod"
                Using connection As New SqlConnection(strConnString)
                    Dim command As New SqlCommand(strSql, connection)
                    command.Parameters.Add("@strKod", SqlDbType.VarChar)
                    command.Parameters("@strKod").Value = strKod
                    command.Parameters.AddWithValue("@strButiran", strButiran)
                    command.Parameters.AddWithValue("@strJnsTugas", strJnsTugas)
                    command.Parameters.AddWithValue("@strKodVot", strKodVot)


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

        End Try
    End Sub

    Function clearFill()
        txtKod.Text = ""
        txtButiran.Text = ""
        txtKodVot.Text = ""
        ddlJnsTugas.SelectedIndex = 0
        hidKodID.Value = ""
    End Function

    Protected Sub gvSenarai_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvSenarai.RowCommand
        Try
            If e.CommandName = "Select" Then

                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim selectedRow As GridViewRow = gvSenarai.Rows(index)
                Dim lnkbtn As LinkButton = CType(selectedRow.FindControl("lnkButton"), LinkButton)
                Dim Id As String = lnkbtn.Attributes("data-id")
                ' Dim strKod As String = selectedRow.Cells(0).Text.ToString


                'Call sql
                strSql = $"SELECT VotID, Kod, Butiran, KodVot, KwsnTugas FROM  SMKB_CLM_Vot WHERE VotID = '{Id}'"

                Dim dt As New DataTable
                dt = dbconn.fSelectCommandDt(strSql)
                If dt.Rows.Count > 0 Then
                    txtKod.Text = dt.Rows(0)("Kod")
                    txtButiran.Text = dt.Rows(0)("Butiran")
                    txtKodVot.Text = dt.Rows(0)("KodVot")
                    hidKodID.Value = dt.Rows(0)("VotID")
                    sBindJnsTugas()
                    ddlJnsTugas.SelectedValue = dt.Rows(0)("KwsnTugas")


                End If
                ViewState("SaveMode") = 2 '--tuk edit data

                ClientScript.RegisterStartupScript([GetType](), "alert", "ShowPopup('1');", True)

            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlJnsTugas_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            Dim strSql As String = "SELECT DISTINCT KwsnTugas, CASE WHEN KwsnTugas ='LN' THEN 'LN - LUAR NEGARA' WHEN KwsnTugas='DN' THEN 'DN - DALAM NEGERI' END AS Butiran2
                                    FROM  SMKB_CLM_Vot WHERE KwsnTugas = '" & Me.ddlJnsTugas.SelectedValue & "' "

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            ddlJnsTugas.DataSource = ds
            ddlJnsTugas.DataTextField = "Butiran2"
            ddlJnsTugas.DataValueField = "KwsnTugas"
            ddlJnsTugas.DataBind()




        Catch ex As Exception

        End Try
    End Sub
End Class