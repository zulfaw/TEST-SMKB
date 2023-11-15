Public Class Laporan_Buku_Vot
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            fBindDdlKW()
            fBindDdlPTj()
            fBindDdlVot()

            txtStartDate.Text = DateTime.Now.ToString("dd/MM/yyyy")
            txtEndDate.Text = DateTime.Now.ToString("dd/MM/yyyy")
        End If
    End Sub

    Private Sub fBindDdlKW()

        Try

            Dim strSql As String = "Select KodKW, butiran, (KodKW + ' - ' + butiran) as Butiran2  from mk_kw ORDER BY Kodkw"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fselectCommand(strSql)

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

            Dim strSql As String = "select KodPTJ ,Butiran, (KodPTJ + ' - '+ Butiran) as Butiran2  from mk_ptj where kodptj <> '-' ORDER BY Kodptj"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fselectCommand(strSql)

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

            Dim strSql As String = "select KodVot, Butiran, (KodVot + ' - ' + Butiran) as Butiran2 from mk_vot where klasifikasi = 'H2' order by kodvot"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fselectCommand(strSql)

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


End Class