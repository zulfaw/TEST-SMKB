
Imports Microsoft.Reporting.WebForms

Public Class Laporan_Peruntukan_KW
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            fBindDdlKW()
            fBindDdlPTj()
            fBindDdlVot()

            ddlPTjTo.Enabled = False
            ddlPTjFrom.Enabled = False
            ddlVotTo.Enabled = False
            ddlVotFrom.Enabled = False

            txtStartDate.Text = DateTime.Now.ToString("dd/MM/yyyy")
            txtEndDate.Text = DateTime.Now.ToString("dd/MM/yyyy")

        End If


    End Sub



    Private Sub fBindDdlKW()

        Try
            Dim strKodKw As String
            Dim strButiran As String


            Dim strSql As String = "Select KodKW, butiran, (KodKW + ' - ' + butiran) as Butiran2  from mk_kw ORDER BY Kodkw"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strSql)

            ddlKwFrom.DataSource = ds
            ddlKwFrom.DataTextField = "Butiran2"
            ddlKwFrom.DataValueField = "KodKW"
            ddlKwFrom.DataBind()

            ddlKWTo.DataSource = ds
            ddlKWTo.DataTextField = "Butiran2"
            ddlKWTo.DataValueField = "KodKW"
            ddlKWTo.DataBind()

        Catch ex As Exception

        End Try


    End Sub

    Private Sub fBindDdlPTj()

        Try
            Dim strKodKw As String
            Dim strButiran As String

            Dim strSql As String = "select KodPTJ ,Butiran, (KodPTJ + ' - '+ Butiran) as Butiran2  from mk_ptj where kodptj <> '-' ORDER BY Kodptj"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strSql)

            ddlPTjFrom.DataSource = ds
            ddlPTjFrom.DataTextField = "Butiran2"
            ddlPTjFrom.DataValueField = "KodPTJ"
            ddlPTjFrom.DataBind()

            ddlPTjTo.DataSource = ds
            ddlPTjTo.DataTextField = "Butiran2"
            ddlPTjTo.DataValueField = "KodPTJ"
            ddlPTjTo.DataBind()

        Catch ex As Exception

        End Try


    End Sub

    Private Sub fBindDdlVot()

        Try
            Dim strKodKw As String
            Dim strButiran As String


            Dim strSql As String = "select KodVot, Butiran, (KodVot + ' - ' + Butiran) as Butiran2 from mk_vot where klasifikasi = 'H2' order by kodvot"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strSql)

            ddlVotFrom.DataSource = ds
            ddlVotFrom.DataTextField = "Butiran2"
            ddlVotFrom.DataValueField = "KodVot"
            ddlVotFrom.DataBind()

            ddlVotTo.DataSource = ds
            ddlVotTo.DataTextField = "Butiran2"
            ddlVotTo.DataValueField = "KodVot"
            ddlVotTo.DataBind()

        Catch ex As Exception

        End Try


    End Sub

    Protected Sub btnPapar0_Click(sender As Object, e As EventArgs) Handles btnPapar0.Click

        Try

            Dim strStartDate As String
            Dim strEndDate As String
            Dim strKWFrom As String
            Dim strKWTo As String

            strStartDate = txtStartDate.Text.ToString
            strEndDate = txtEndDate.Text.ToString
            strKWFrom = ddlKwFrom.SelectedValue.ToString
            strKWTo = ddlKWTo.SelectedValue.ToString

            Session("StartDate") = strStartDate
            Session("strEndDate") = strEndDate
            Session("strKWFrom") = strKWFrom
            Session("strKWTo") = strKWTo

            Dim url As String = "Vwr_Peruntukan_KW.aspx?"
            Dim fullURL As String = (Convert.ToString("window.open('") & url) + "', '_blank', 'height=1000,width=750,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=yes,titlebar=no' );"
            ScriptManager.RegisterStartupScript(Me, GetType(String), "OPEN_WINDOW", fullURL, True)
        Catch ex As Exception

        End Try

    End Sub

End Class