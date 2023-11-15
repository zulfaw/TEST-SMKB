Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Public Class MataWang
    Inherits System.Web.UI.Page
    Dim dbconn As New DBKewConn
    Dim dt As New DataTable
    Dim strSql As String
    Dim ds As New DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not IsPostBack Then
                fLoadSenarai()
                sBindNegara()
            End If
        Catch ex As Exception

        End Try

        fLoadSenarai()
        sBindNegara()
    End Sub

    Private Sub fLoadSenarai()
        Try
            Dim intRec As Integer
            Dim strSql As String


            Dim dt As New DataTable
            dt.Columns.Add("Id", GetType(String))
            dt.Columns.Add("Negara", GetType(String))
            dt.Columns.Add("Matawang", GetType(String))
            dt.Columns.Add("Simbol", GetType(String))

            strSql = "SELECT  Id, Negara, Matawang, case when Simbol is NULL then '' else Simbol end as Simbol
                        FROM  SMKB_CLM_Matawang ORDER BY Negara ASC"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            Dim strId, strNegara, strMatawang, strSimbol As String


            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        strId = ds.Tables(0).Rows(i)("Id")
                        strNegara = ds.Tables(0).Rows(i)("Negara")
                        strMatawang = ds.Tables(0).Rows(i)("Matawang")
                        strSimbol = ds.Tables(0).Rows(i)("Simbol")


                        dt.Rows.Add(strId, strNegara, strMatawang, strSimbol)
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
    Private Sub sBindNegara()
        Try
            Dim strSql As String = "SELECT  Kod_Negara, Butiran as Negara FROM SMKB_Negara ORDER BY Butiran ASC "

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            ddlNegara.DataSource = ds
            ddlNegara.DataTextField = "Negara"
            ddlNegara.DataValueField = "Negara"
            ddlNegara.DataBind()

            ddlNegara.Items.Insert(0, New ListItem("- Sila Pilih -", "0"))
            ddlNegara.SelectedIndex = 0

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
                strSql = $"SELECT  Id, Negara, Matawang, case when Simbol is NULL then '' else Simbol end as Simbol
                        FROM  SMKB_CLM_Matawang WHERE Id = '{Id}' "

                Dim dt As New DataTable
                dt = dbconn.fSelectCommandDt(strSql)
                If dt.Rows.Count > 0 Then
                    sBindNegara()
                    txtId.Text = dt.Rows(0)("Id")
                    ddlNegara.SelectedValue = dt.Rows(0)("Negara")
                    txtMataWang.Text = dt.Rows(0)("Matawang")
                    txtSimbol.Text = dt.Rows(0)("Simbol")

                End If
                ViewState("SaveMode") = 2 '--tuk edit data

                ClientScript.RegisterStartupScript([GetType](), "alert", "ShowPopup('1');", True)

            ElseIf e.CommandName = "OpenModal" Then

                ClientScript.RegisterStartupScript([GetType](), "alert", "ShowPopup('3');", True)

            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lbtnSimpan_Click(sender As Object, e As EventArgs) Handles lbtnSimpan.Click

        Dim dbconn As New DBKewConn
        Dim strConnString As String = dbKewConnStr()
        Dim strSql1 As String
        Dim strSql As String
        Dim strHidID As String = Trim(txtId.Text)
        Dim strNegara As String = Trim(ddlNegara.SelectedValue)
        Dim strMataWang As String = Trim(txtMataWang.Text.TrimEnd)
        Dim strSimbol As String = Trim(txtSimbol.Text.TrimEnd)
        Dim bil As Integer

        Try
            'If ViewState("SaveMode") = 1 Then
            '--Edit--
            'INSERT

            'SELECT  Id, Negara, Matawang, case when Simbol is NULL then '' else Simbol end as Simbol   FROM  SMKB_CLM_Matawang WHERE Id
            strSql = $"SELECT Id  FROM SMKB_CLM_Matawang WHERE Id = '{strHidID}'"
            If fCheckRec(strSql) = 0 Then
                strSql1 = " SELECT TOP 1 Id  FROM SMKB_CLM_Matawang ORDER BY Id DESC "
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
                strSql = "INSERT INTO SMKB_CLM_Matawang (Id, Negara, Matawang, Simbol) values 
                                 (@strHidID, @strNegara, @strMataWang, @strSimbol)"

                Using connection As New SqlConnection(strConnString)
                    Dim command As New SqlCommand(strSql, connection)
                    command.Parameters.AddWithValue("@strHidID", strHidID)
                    command.Parameters.AddWithValue("@strNegara", strNegara)
                    command.Parameters.AddWithValue("@strMataWang", strMataWang)
                    command.Parameters.AddWithValue("@strSimbol", strSimbol)


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
                strSql = "UPDATE SMKB_CLM_Matawang set Negara = @strNegara, Matawang = @strMataWang, Simbol = @strSimbol WHERE Id = @strHidID"

                Using connection As New SqlConnection(strConnString)
                    Dim command As New SqlCommand(strSql, connection)
                    command.Parameters.Add("@strHidID", SqlDbType.VarChar)
                    command.Parameters("@strHidID").Value = strHidID
                    command.Parameters.AddWithValue("@strNegara", strNegara)
                    command.Parameters.AddWithValue("@strMataWang", strMataWang)
                    command.Parameters.AddWithValue("@strSimbol", strSimbol)

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

        ddlNegara.SelectedIndex = 0
        txtMataWang.Text = ""
        txtSimbol.Text = ""
        txtId.Text = ""


    End Sub

    'Protected Sub lbtnTutup_Click(sender As Object, e As EventArgs) Handles lbtnTutup.Click
    '    ClearFill()
    'End Sub

End Class