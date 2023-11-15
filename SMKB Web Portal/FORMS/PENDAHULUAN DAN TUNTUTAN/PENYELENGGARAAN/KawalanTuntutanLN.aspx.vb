Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing

Public Class KawalanTuntutanLN
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
        sBindKump()
        sBindJnsTugas()

    End Sub

    Private Sub fLoadSenarai()
        Try
            Dim intRec As Integer
            Dim strSql As String


            Dim dt As New DataTable
            dt.Columns.Add("ID_KdrLN", GetType(String))
            dt.Columns.Add("Kategori", GetType(String))
            dt.Columns.Add("JenisTugas", GetType(String))
            dt.Columns.Add("ElnMkn1", GetType(String))
            dt.Columns.Add("ElnMkn2", GetType(String))
            dt.Columns.Add("ElnMkn3", GetType(String))
            dt.Columns.Add("ElnMkn4", GetType(String))
            dt.Columns.Add("ElnMkn5", GetType(String))
            dt.Columns.Add("SewaHotel", GetType(String))
            dt.Columns.Add("ElnLojing", GetType(String))
            dt.Columns.Add("ElnHLjgKPT", GetType(String))



            strSql = "SELECT ID_KdrLN, Kategori, JenisTugas, ElnMkn1, ElnMkn2, ElnMkn3, ElnMkn4, ElnMkn5, SewaHotel, ElnLojing, ElnHLjgKPT
                        FROM SMKB_CLM_KdrLuarNegara ORDER BY ID_KdrLN"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            Dim strId, strKategori, strJenis, strMkn1, strMkn2, strMkn3, strMkn4, strMkn5, strSewaHotel, strSewaLojing, strKdrMkn As String


            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        strId = ds.Tables(0).Rows(i)("ID_KdrLN")
                        strKategori = ds.Tables(0).Rows(i)("Kategori")
                        strJenis = ds.Tables(0).Rows(i)("JenisTugas")
                        strMkn1 = ds.Tables(0).Rows(i)("ElnMkn1")
                        strMkn2 = ds.Tables(0).Rows(i)("ElnMkn2")
                        strMkn3 = ds.Tables(0).Rows(i)("ElnMkn3")
                        strMkn4 = ds.Tables(0).Rows(i)("ElnMkn4")
                        strMkn5 = ds.Tables(0).Rows(i)("ElnMkn5")
                        strSewaHotel = ds.Tables(0).Rows(i)("SewaHotel")
                        strSewaLojing = ds.Tables(0).Rows(i)("ElnLojing")
                        strKdrMkn = ds.Tables(0).Rows(i)("ElnHLjgKPT")


                        dt.Rows.Add(strId, strKategori, strJenis, strMkn1, strMkn2, strMkn3, strMkn4, strMkn5, strSewaHotel, strSewaLojing, strKdrMkn)
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
            Dim strSql As String = "SELECT Distinct Kategori FROM SMKB_CLM_KdrLuarNegara "

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            ddlKategori.DataSource = ds
            ddlKategori.DataTextField = "Kategori"
            ddlKategori.DataValueField = "Kategori"
            ddlKategori.DataBind()

            ddlKategori.Items.Insert(0, New ListItem("- Sila Pilih -", "0"))
            ddlKategori.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Private Sub sBindJnsTugas()
        Try
            Dim strSql As String = "SELECT DISTINCT JenisTugas, Case When JenisTugas='K' THEN 'K - KURSUS' WHEN JenisTugas ='R' THEN 'R - RASMI' END AS Butiran2 
            FROM  SMKB_CLM_KdrLuarNegara"

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
                strSql = $"SELECT ID_KdrLN, Kategori, JenisTugas, ElnMkn1, ElnMkn2, ElnMkn3, ElnMkn4, ElnMkn5, SewaHotel, ElnLojing, ElnHLjgKPT
                        FROM SMKB_CLM_KdrLuarNegara  WHERE ID_KdrLN = '{Id}'"

                Dim dt As New DataTable
                dt = dbconn.fSelectCommandDt(strSql)
                If dt.Rows.Count > 0 Then
                    sBindKump()
                    sBindJnsTugas()
                    ddlKategori.SelectedValue = dt.Rows(0)("Kategori")
                    ddlJnsTugas.SelectedValue = dt.Rows(0)("JenisTugas")
                    txtMakan1.Text = dt.Rows(0)("ElnMkn1")
                    txtMakan2.Text = dt.Rows(0)("ElnMkn2")
                    txtMakan3.Text = dt.Rows(0)("ElnMkn3")
                    txtMakan4.Text = dt.Rows(0)("ElnMkn4")
                    txtMakan5.Text = dt.Rows(0)("ElnMkn5")
                    txtSewaHotel.Text = dt.Rows(0)("SewaHotel")
                    txtSewaLojing.Text = dt.Rows(0)("ElnLojing")
                    txtKadarMakan.Text = dt.Rows(0)("ElnHLjgKPT")
                    hidJnsTugas = dt.Rows(0)("JenisTugas")
                    hidID.Value = dt.Rows(0)("ID_KdrLN")

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

    Protected Sub lbtnSimpan_Click(sender As Object, e As EventArgs) Handles lbtnSimpan.Click

        Dim dbconn As New DBKewConn
        Dim strConnString As String = dbKewConnStr()
        Dim strSql1 As String
        Dim strSql As String
        Dim strHidID As String = Trim(hidID.Value)
        Dim strKategori As String = Trim(ddlKategori.SelectedValue)
        Dim strJnsTugas As String = Trim(ddlJnsTugas.SelectedValue)
        Dim strMakan1 As String = Trim(txtMakan1.Text.TrimEnd)
        Dim strMakan2 As String = Trim(txtMakan2.Text.TrimEnd)
        Dim strMakan3 As String = Trim(txtMakan3.Text.TrimEnd)
        Dim strMakan4 As String = Trim(txtMakan4.Text.TrimEnd)
        Dim strMakan5 As String = Trim(txtMakan5.Text.TrimEnd)
        Dim strSewaHotel As String = Trim(txtSewaHotel.Text.TrimEnd)
        Dim strSewaLojing As String = Trim(txtSewaLojing.Text.TrimEnd)
        Dim strKadarMkn As String = Trim(txtKadarMakan.Text.TrimEnd)
        Dim bil As Integer


        Try
            'If ViewState("SaveMode") = 1 Then
            '--Edit--
            'INSERT
            'SELECT 

            strSql = $"SELECT ID_KdrLN  FROM SMKB_CLM_KdrLuarNegara WHERE ID_Kdr = '{strHidID}'"
            If fCheckRec(strSql) = 0 Then
                strSql1 = " SELECT TOP 1 ID_KdrLN  FROM SMKB_CLM_KdrLuarNegara ORDER BY ID_KdrLN DESC "
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
                strSql = "INSERT INTO SMKB_CLM_KdrLuarNegara (ID_KdrLN, Kategori, JenisTugas, ElnMkn1, ElnMkn2, ElnMkn3, ElnMkn4, ElnMkn5, SewaHotel, ElnLojing, ElnHLjgKPT) values 
                                 (@strHidID, @strKategori, @strJnsTugas, @strMakan1, @strMakan2, @strMakan3, @strMakan4,@strMakan5, @strSewaHotel, @strSewaLojing, @strKadarMkn )"

                Using connection As New SqlConnection(strConnString)
                    Dim command As New SqlCommand(strSql, connection)
                    command.Parameters.AddWithValue("@strHidID", strHidID)
                    command.Parameters.AddWithValue("@strKategori", strKategori)
                    command.Parameters.AddWithValue("@strJnsTugas", strJnsTugas)
                    command.Parameters.AddWithValue("@strMakan1", strMakan1)
                    command.Parameters.AddWithValue("@strMakan2", strMakan2)
                    command.Parameters.AddWithValue("@strMakan3", strMakan3)
                    command.Parameters.AddWithValue("@strMakan4", strMakan4)
                    command.Parameters.AddWithValue("@strMakan5", strMakan5)
                    command.Parameters.AddWithValue("@strSewaHotel", strSewaHotel)
                    command.Parameters.AddWithValue("@strSewaLojing", strSewaLojing)
                    command.Parameters.AddWithValue("@strKadarMkn", strKadarMkn)

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
                strSql = "UPDATE SMKB_CLM_KdrLuarNegara set Kategori = @strKategori, JenisTugas = @strJnsTugas, ElnMkn1 = @strMakan1, ElnMkn2 = @strMakan2,
                        ElnMkn3 = @strMakan3, ElnMkn4 = @strMakan4, ElnMkn5 = @strMakan5, SewaHotel = @strSewaHotel,
                        ElnLojing = @strSewaLojing, ElnHLjgKPT = @strKadarMkn  WHERE ID_KdrLN = @strHidID"

                Using connection As New SqlConnection(strConnString)
                    Dim command As New SqlCommand(strSql, connection)
                    command.Parameters.Add("@strHidID", SqlDbType.VarChar)
                    command.Parameters("@strHidID").Value = strHidID
                    command.Parameters.AddWithValue("@strKategori", strKategori)
                    command.Parameters.AddWithValue("@strJnsTugas", strJnsTugas)
                    command.Parameters.AddWithValue("@strMakan1", strMakan1)
                    command.Parameters.AddWithValue("@strMakan2", strMakan2)
                    command.Parameters.AddWithValue("@strMakan3", strMakan3)
                    command.Parameters.AddWithValue("@strMakan4", strMakan4)
                    command.Parameters.AddWithValue("@strMakan5", strMakan5)
                    command.Parameters.AddWithValue("@strSewaHotel", strSewaHotel)
                    command.Parameters.AddWithValue("@strSewaLojing", strSewaLojing)
                    command.Parameters.AddWithValue("@strKadarMkn", strKadarMkn)


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
        ddlKategori.SelectedIndex = 0
        ddlJnsTugas.SelectedIndex = 0
        txtMakan1.Text = ""
        txtMakan2.Text = ""
        txtMakan3.Text = ""
        txtMakan4.Text = ""
        txtMakan5.Text = ""
        txtSewaHotel.Text = ""
        txtSewaLojing.Text = ""
        txtKadarMakan.Text = ""
        hidID.Value = ""
    End Sub

    'Protected Sub lbtnTutup_Click(sender As Object, e As EventArgs) Handles lbtnTutup.Click
    '    ClearFill()
    'End Sub


End Class