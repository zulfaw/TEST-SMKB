Imports System.Data.Entity.SqlServer
Imports System.Data.SqlClient
Imports AjaxControlToolkit

Public Class Proses_Gaji
    Inherits System.Web.UI.Page
    Public dsKod As New DataSet
    Public dvKodKW As New DataView
    Dim dbconn As New DBKewConn
    Private sqlCon As SqlConnection
    Private strConn As String = "Data Source=devmis12.utem.edu.my;Initial Catalog=dbKewanganV4;Persist Security Info=True;User ID=smkb;Password=Smkb@Dev2012"
    Private strConnStaf As String = "Data Source=devmis11.utem.edu.my;Initial Catalog=dbStaf;Persist Security Info=True;User ID=smkb;Password=Smkb@Dev2012"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            fLoadBlnGaji()
            fBindPTJ()
            fBindStaf()

        End If
    End Sub
    Private Sub fLoadBlnGaji()

        Dim sqlROC01 As String = $"select * from SMKB_Gaji_bulan;"
        Dim ds = dbconn.fSelectCommand(sqlROC01)
        Dim list As New ListItem()
        Using dt = ds.Tables(0)
            If dt.Rows.Count > 0 Then
                ViewState("BulanGj") = dt.Rows(0)("Bulan").ToString
                ViewState("TahunGj") = dt.Rows(0)("Tahun").ToString
            End If
        End Using
    End Sub
    Private Sub fBindPTJ()
        Try
            Dim dbconn As New DBSMConn
            Dim strsql As String
            Dim bilptj As Integer


            strsql = $"select KodPejabat AS kod, Pejabat as butiran, Singkatan,KodPejabat + ' - ' + Pejabat as butirptj from MS_Pejabat_prod WHERE Status = 1 and KodPejabat not in ('50','-') order by KodPejabat"
            'Dim ds As New DataSet
            'Dim dt As New DataTable
            Dim ds = dbconn.fselectCommand(strsql)
            Using dt = ds.Tables(0)
                bilptj = dt.Rows.Count
            End Using
            ' Dim strKod = Trim(ds.Tables(0).Rows(0)("Kod").ToString)

            ddlPtjDr.DataSource = ds
            ddlPtjDr.DataTextField = "butirptj"
            ddlPtjDr.DataValueField = "Kod"
            ddlPtjDr.DataBind()


            ddlPtjDr.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
            ddlPtjDr.SelectedIndex = 1


            ddlPtjHg.DataSource = ds
            ddlPtjHg.DataTextField = "butirptj"
            ddlPtjHg.DataValueField = "Kod"
            ddlPtjHg.DataBind()


            ddlPtjHg.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
            ddlPtjHg.SelectedIndex = bilptj
            'If kodKlasifikasi = "" Then
            '    ddlPTJ.SelectedIndex = 0
            'Else
            '    ddlPTJ.Items.FindByValue(kodKlasifikasi.Trim()).Selected = True
            'End If

        Catch ex As Exception

        End Try
    End Sub
    Private Sub fBindStaf()
        Try
            Dim dbconn As New DBSMConn
            Dim strsql As String
            Dim bilstaf As Integer


            strsql = $"select ms01_nostaf,ms01_nama,ms01_nostaf + ' - ' + ms01_nama as nmstaf from ms01_peribadi order by ms01_nostaf"
            'Dim ds As New DataSet
            'Dim dt As New DataTable
            Dim ds = dbconn.fselectCommand(strsql)
            Using dt = ds.Tables(0)
                bilstaf = dt.Rows.Count
            End Using
            ' Dim strKod = Trim(ds.Tables(0).Rows(0)("Kod").ToString)

            ddlStafDr.DataSource = ds
            ddlStafDr.DataTextField = "nmstaf"
            ddlStafDr.DataValueField = "ms01_nostaf"
            ddlStafDr.DataBind()


            ddlStafDr.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
            ddlStafDr.SelectedIndex = 1

            ddlStafHg.DataSource = ds
            ddlStafHg.DataTextField = "nmstaf"
            ddlStafHg.DataValueField = "ms01_nostaf"
            ddlStafHg.DataBind()


            ddlStafHg.Items.Insert(0, New ListItem("- SILA PILIH -", 0))
            ddlStafHg.SelectedIndex = bilstaf
            'If kodKlasifikasi = "" Then
            '    ddlPTJ.SelectedIndex = 0
            'Else
            '    ddlPTJ.Items.FindByValue(kodKlasifikasi.Trim()).Selected = True
            'End If

        Catch ex As Exception

        End Try
    End Sub
    Private Sub ProsesGaji()
        Try
            Dim strSql As String = $"SELECT TarikhGaji FROM SMKB_Gaji_Tarikh_Gaji WHERE bulan='{ViewState("BulanGj")}' and tahun= '{ ViewState("TahunGj")}'"
            Dim dt As New DataTable
            dt = dbconn.fSelectCommandDt(strSql)
            If dt.Rows.Count = 0 Then
                'lblModalMessaage.Text = "Proses gaji tidak boleh dilakukan lagi kerana Tarikh Gaji tidak lengkap!" 'message di modal
                'ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
                Exit Sub
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs)
        System.Threading.Thread.Sleep(5000)
        ' ModalPopupExtender1.Show()
    End Sub
    'Private Sub lbtnSimpan_Click(sender As Object, e As EventArgs) Handles lbtnSimpan.ServerClick
    '    Dim dbconn As New DBKewConn
    '    Dim cmd = New SqlCommand
    '    Dim sukses As Integer = 0
    '    Dim sqlComm As New SqlCommand
    '    Dim X As Long

    '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Pop", "ShowPopup('2');", True)

    '    sqlCon = New SqlConnection(strConn)
    '    Using (sqlCon)

    '        Dim strPtjDr As String = ddlPtjDr.SelectedValue
    '        Dim strPtjHg As String = ddlPtjHg.SelectedValue
    '        Dim strStafDr As String = ddlStafDr.SelectedValue
    '        Dim strStafHg As String = ddlStafHg.SelectedValue

    '        sqlComm.Connection = sqlCon

    '        sqlComm.CommandText = "USP_PROSESGAJI"
    '        sqlComm.CommandType = CommandType.StoredProcedure

    '        sqlComm.Parameters.AddWithValue("@iBulan", 6)
    '        sqlComm.Parameters.AddWithValue("@iTahun", 2023)
    '        sqlComm.Parameters.AddWithValue("@strStafDr", strStafDr)
    '        sqlComm.Parameters.AddWithValue("@strStafHg", strStafHg)
    '        sqlComm.Parameters.AddWithValue("@strPtjDr", strPtjDr)
    '        sqlComm.Parameters.AddWithValue("@strPtjHg", strPtjHg)


    '        sqlCon.Open()

    '        'sqlComm.ExecuteNonQuery()
    '        'Dim rowsAffected As Integer = sqlComm.ExecuteNonQuery()
    '        X = sqlComm.ExecuteNonQuery()


    '        ' MsgBox(X)
    '        If X > 0 Then
    '            'ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "alert", "closeModal();", True)
    '            lblModalMessaage.Text = "Rekod telah dikemaskini." 'message di modal
    '            ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
    '        Else
    '            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "alert", "closeModal();", True)
    '            lblModalMessaage.Text = "Ralat!" 'message di modal
    '            ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
    '        End If


    '    End Using
    'End Sub
End Class