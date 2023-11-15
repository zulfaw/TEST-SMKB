Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing

Public Class KadarElaunMakan
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
                sBindJnsTugas()
            End If
        Catch ex As Exception

        End Try

        fLoadSenarai()
        'sBindKump()
        'sBindJnsTugas()
    End Sub

    Private Sub fLoadSenarai()
        Try
            Dim intRec As Integer
            Dim strSql As String


            Dim dt As New DataTable
            dt.Columns.Add("KumpGred", GetType(String))
            dt.Columns.Add("GredDari", GetType(String))
            dt.Columns.Add("GredKe", GetType(String))
            dt.Columns.Add("Tempat", GetType(String))
            dt.Columns.Add("JenisTugas", GetType(String))
            dt.Columns.Add("KadarMkn", GetType(String))
            dt.Columns.Add("KadarHotel", GetType(String))
            dt.Columns.Add("KadarLojing", GetType(String))
            dt.Columns.Add("ID_Kdr", GetType(String))

            strSql = "SELECT ID_Kdr, JenisTugas, Tempat, GredDari, GredKe, KadarMkn, KadarHotel, KadarLojing, KumpGred 
                        FROM SMKB_CLM_KdrMknHtlLjg ORDER BY KumpGred ASC"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            Dim strKump, strGredDari, strGredKe, strTempat, strJnsTugas, strKdrMkn, strKdrHotel, strKdrLojing, strHidID As String


            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        strKump = ds.Tables(0).Rows(i)("KumpGred")
                        strGredDari = ds.Tables(0).Rows(i)("GredDari")
                        strGredKe = ds.Tables(0).Rows(i)("GredKe")
                        strTempat = ds.Tables(0).Rows(i)("Tempat")
                        strJnsTugas = ds.Tables(0).Rows(i)("JenisTugas")
                        strKdrMkn = ds.Tables(0).Rows(i)("KadarMkn")
                        strKdrHotel = ds.Tables(0).Rows(i)("KadarHotel")
                        strKdrLojing = ds.Tables(0).Rows(i)("KadarLojing")
                        strHidID = ds.Tables(0).Rows(i)("ID_Kdr")


                        dt.Rows.Add(strKump, strGredDari, strGredKe, strTempat, strJnsTugas, strKdrMkn, strKdrHotel, strKdrLojing, strHidID)
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
            Dim strSql As String = "SELECT DISTINCT KumpGred FROM SMKB_CLM_KdrMknHtlLjg"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            ddlKumpulan.DataSource = ds
            ddlKumpulan.DataTextField = "KumpGred"
            ddlKumpulan.DataValueField = "KumpGred"
            ddlKumpulan.DataBind()

            ddlKumpulan.Items.Insert(0, New ListItem("- Sila Pilih -", "0"))
            ddlKumpulan.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub


    Private Sub sBindJnsTugas()
        Try
            Dim strSql As String = "SELECT DISTINCT JenisTugas, Case When JenisTugas='K' THEN 'K - KURSUS' WHEN JenisTugas ='R' THEN 'R - RASMI' END AS Butiran2 FROM  SMKB_CLM_KdrMknHtlLjg"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            ddlJnsTugas.DataSource = ds
            ddlJnsTugas.DataTextField = "Butiran2"
            ddlJnsTugas.DataValueField = "JenisTugas"
            ddlJnsTugas.DataBind()

            ddlJnsTugas.Items.Insert(0, New ListItem("- Sila Pilih -", "0"))
            ddlJnsTugas.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub gvSenarai_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvSenarai.RowCommand
        Try
            If e.CommandName = "Select" Then

                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim selectedRow As GridViewRow = gvSenarai.Rows(index)
                Dim hidJnsTugas As String
                Dim lnkbtn As LinkButton = CType(selectedRow.FindControl("lnkButton"), LinkButton)
                Dim Id As String = lnkbtn.Attributes("data-id")



                '--Call sql---
                strSql = $"SELECT ID_Kdr, JenisTugas, Tempat, GredDari, GredKe, KadarMkn, KadarHotel, KadarLojing, KumpGred 
                        FROM SMKB_CLM_KdrMknHtlLjg WHERE ID_Kdr = '{Id}'"

                Dim dt As New DataTable
                dt = dbconn.fSelectCommandDt(strSql)
                If dt.Rows.Count > 0 Then
                    sBindKump()
                    sBindJnsTugas()
                    ddlKumpulan.SelectedValue = dt.Rows(0)("KumpGred")
                    txtGredDari.Text = dt.Rows(0)("GredDari")
                    txtGredKe.Text = dt.Rows(0)("GredKe")
                    txtKdrHotel.Text = dt.Rows(0)("KadarHotel")
                    txtKdrLojing.Text = dt.Rows(0)("KadarLojing")
                    txtTempat.Text = dt.Rows(0)("Tempat")
                    txtMakan.Text = dt.Rows(0)("KadarMkn")
                    hidJnsTugas = dt.Rows(0)("JenisTugas")
                    hidKodID.Value = dt.Rows(0)("ID_Kdr")

                    Dim strSql2 As String = $"SELECT DISTINCT JenisTugas, Case When JenisTugas='K' THEN 'K - KURSUS' WHEN JenisTugas ='R' THEN 'R - RASMI' END AS Butiran2 
                            FROM  SMKB_CLM_KdrMknHtlLjg WHERE JenisTugas = '{hidJnsTugas}' "
                    dt = dbconn.fSelectCommandDt(strSql2)
                    If dt.Rows.Count > 0 Then
                        ddlJnsTugas.SelectedValue = dt.Rows(0)("JenisTugas")
                    End If


                End If
                ViewState("SaveMode") = 2 '--tuk edit data

                ClientScript.RegisterStartupScript([GetType](), "alert", "ShowPopup('1');", True)

            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lbtnTutup_Click(sender As Object, e As EventArgs) Handles lbtnTutup.Click
        clearFill()
    End Sub

    Sub ClearFill()
        ddlKumpulan.SelectedIndex = 0
        txtGredDari.Text = ""
        txtGredKe.Text = ""
        txtKdrHotel.Text = ""
        txtKdrLojing.Text = ""
        txtTempat.Text = ""
        txtMakan.Text = ""
        ddlJnsTugas.SelectedIndex = 0
        hidKodID.Value = ""
    End Sub

    Protected Sub lbtnSimpan_Click(sender As Object, e As EventArgs) Handles lbtnSimpan.Click

        Dim dbconn As New DBKewConn
        Dim strConnString As String = dbKewConnStr()
        Dim strSql1 As String
        Dim strSql As String
        Dim strKod As String = Trim(hidKodID.Value)
        Dim strKump As String = Trim(ddlKumpulan.SelectedValue)
        Dim strKdrMkn As String = Trim(txtMakan.Text.TrimEnd)
        Dim strGredDari As String = Trim(txtGredKe.Text.TrimEnd)
        Dim strGredKe As String = Trim(txtGredKe.Text.TrimEnd)
        Dim strKdrHotel As String = Trim(txtKdrHotel.Text.TrimEnd)
        Dim strKdrLojing As String = Trim(txtKdrLojing.Text.TrimEnd)
        Dim strTempat As String = Trim(txtTempat.Text.TrimEnd)
        Dim strJnsTugas As String = Trim(ddlJnsTugas.SelectedValue)
        Dim bil As Integer
        Dim strHidID As String

        Try
            'If ViewState("SaveMode") = 1 Then
            '--Edit--
            'INSERT

            strSql = $"SELECT ID_Kdr  FROM CL_KdrMknHtlLjg WHERE SMKB_CLM_KdrMknHtlLjg = '{strKod}'"
            If fCheckRec(strSql) = 0 Then
                strSql1 = " SELECT TOP 1 ID_Kdr  FROM SMKB_CLM_KdrMknHtlLjg ORDER BY ID_Kdr DESC "
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
                strSql = "INSERT INTO SMKB_CLM_KdrMknHtlLjg (ID_Kdr, KumpGred,  GredDari, GredKe, Tempat, JenisTugas,  KadarMkn, KadarHotel, KadarLojing) values 
                                 (@strHidID,@strKump, @strGredDari, @strGredKe, @strTempat, @strJnsTugas, @strKdrMkn, @strKdrHotel, @strKdrLojing)"

                Using connection As New SqlConnection(strConnString)
                    Dim command As New SqlCommand(strSql, connection)
                    command.Parameters.AddWithValue("@strHidID", strHidID)
                    command.Parameters.AddWithValue("@strKump", strKump)
                    command.Parameters.AddWithValue("@strGredDari", strGredDari)
                    command.Parameters.AddWithValue("@strGredKe", strGredKe)
                    command.Parameters.AddWithValue("@strTempat", strTempat)
                    command.Parameters.AddWithValue("@strJnsTugas", strJnsTugas)
                    command.Parameters.AddWithValue("@strKdrMkn", strKdrMkn)
                    command.Parameters.AddWithValue("@strKdrHotel", strKdrHotel)
                    command.Parameters.AddWithValue("@strKdrLojing", strKdrLojing)

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
                strSql = "UPDATE SMKB_CLM_KdrMknHtlLjg set KumpGred = @strKump, GredDari = @strGredDari, GredKe = @strGredKe, Tempat = @strTempat, 
                        JenisTugas = @strJnsTugas, KadarMkn = @strKdrMkn, KadarHotel = @strKdrHotel, KadarLojing = @strKdrLojing   WHERE ID_Kdr = @strKod"

                Using connection As New SqlConnection(strConnString)
                    Dim command As New SqlCommand(strSql, connection)
                    command.Parameters.Add("@strKod", SqlDbType.VarChar)
                    command.Parameters("@strKod").Value = strKod
                    command.Parameters.AddWithValue("@strKump", strKump)
                    command.Parameters.AddWithValue("@strGredDari", strGredDari)
                    command.Parameters.AddWithValue("@strGredKe", strGredKe)
                    command.Parameters.AddWithValue("@strTempat", strTempat)
                    command.Parameters.AddWithValue("@strJnsTugas", strJnsTugas)
                    command.Parameters.AddWithValue("@strKdrMkn", strKdrMkn)
                    command.Parameters.AddWithValue("@strKdrHotel", strKdrHotel)
                    command.Parameters.AddWithValue("@strKdrLojing", strKdrLojing)


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
End Class