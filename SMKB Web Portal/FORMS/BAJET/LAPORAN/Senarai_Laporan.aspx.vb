Imports System.Drawing
Imports System.Configuration
Imports System.Web.Configuration
Imports Microsoft.Reporting.WebForms
Imports System.Globalization

Public Class Senarai_Laporan
    Inherits System.Web.UI.Page
    Dim dbconn As New DBKewConn

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not IsPostBack Then
                If Session("LoggedIn") Then
                    fBindGvLaporan()
                Else
                    Response.Redirect("../../Logout.aspx")
                End If
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub fBindGvLaporan()
        Dim strSql As String = "select * from MK_ULaporan where KodModul = '01' and Status = 1 order by Urutan"

        Using dt = dbconn.fSelectCommandDt(strSql)
            gvLaporan.DataSource = dt
            gvLaporan.DataBind()
        End Using

    End Sub


    Private Sub gvLaporan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvLaporan.SelectedIndexChanged

        Dim kod As String = gvLaporan.DataKeys(gvLaporan.SelectedIndex).Value 'CType(gvLaporan.SelectedRow.FindControl("lblKodLprn"), Label).Text
        ViewState("KodLaporan") = kod

        txtEndDate.Visible = True
        lbltkhHgg.Visible = True
        ddlKOFrom.Visible = True
        ddlKOTo.Visible = True
        ddlPTjFrom.Visible = True
        ddlPTjTo.Visible = True
        ddlKPFrom.Visible = True
        ddlKPTo.Visible = True
        ddlVotFrom.Visible = True
        ddlVotTo.Visible = True

        lblKOFrom.Visible = True
        lblKOTo.Visible = True
        lblPTJFrom.Visible = True
        lblPTJTo.Visible = True
        lblKPFrom.Visible = True
        lblKPTo.Visible = True
        lblVotFrom.Visible = True
        lblVotTo.Visible = True

        rfvddlKOFrom.Enabled = True
        rfvddlKOTo.Enabled = True
        rfvddlPTJFrom.Enabled = True
        rfvddlPTJTo.Enabled = True
        rfvddlKPFrom.Enabled = True
        rfvddlKPTo.Enabled = True
        rfvddlVotFrom.Enabled = True
        rfvddlVotTo.Enabled = True


        If kod = "0102" OrElse kod = "0103" Then
            'Buku Vot
            If kod = "0103" Then
                txtEndDate.Visible = False
                lbltkhHgg.Visible = False
                rfvtxtEndDate.Enabled = False
                lbtnEndDate.Visible = False
            Else
                txtEndDate.Visible = True
                lbltkhHgg.Visible = True
                rfvtxtEndDate.Enabled = True
                lbtnEndDate.Visible = True
            End If

        ElseIf kod = "0116" Then
            'Peruntukan KW
            ddlKOFrom.Visible = False
            ddlKOTo.Visible = False
            ddlPTjFrom.Visible = False
            ddlPTjTo.Visible = False
            ddlKPFrom.Visible = False
            ddlKPTo.Visible = False
            ddlVotFrom.Visible = False
            ddlVotTo.Visible = False

            lblKOFrom.Visible = False
            lblKOTo.Visible = False
            lblPTJFrom.Visible = False
            lblPTJTo.Visible = False
            lblKPFrom.Visible = False
            lblKPTo.Visible = False
            lblVotFrom.Visible = False
            lblVotTo.Visible = False

            rfvddlKOFrom.Enabled = False
            rfvddlKOTo.Enabled = False
            rfvddlPTJFrom.Enabled = False
            rfvddlPTJTo.Enabled = False
            rfvddlKPFrom.Enabled = False
            rfvddlKPTo.Enabled = False
            rfvddlVotFrom.Enabled = False
            rfvddlVotTo.Enabled = False
        End If

        fBindDll()
        MPEPilihanCarian1.Show()
    End Sub

    Private Sub fBindDll()
        Dim strSql As String = "Select KodKW, butiran, (KodKW + ' - ' + butiran) as Butiran2  from mk_kw ORDER BY Kodkw;
select kodko, (kodko + ' - ' + butiran) As Butiran2 from MK_KodOperasi order by Kodko;
select KodPTJ ,Butiran, (KodPTJ + ' - '+ Butiran) as Butiran2  from mk_ptj where kodptj <> '-' ORDER BY Kodptj;
select KodProjek, (KodProjek + ' - ' + Butiran) as Butiran2 from MK_KodProjek Order by KodProjek;
select KodVot, Butiran, (KodVot + ' - ' + Butiran) as Butiran2 from mk_vot where klasifikasi = 'H2' order by kodvot"

        Using ds = dbconn.fSelectCommand(strSql)
            Using dt = ds.Tables(0)
                ddlKwFrom.DataSource = dt
                ddlKwFrom.DataTextField = "Butiran2"
                ddlKwFrom.DataValueField = "KodKW"
                ddlKwFrom.DataBind()

                ddlKWTo.DataSource = dt
                ddlKWTo.DataTextField = "Butiran2"
                ddlKWTo.DataValueField = "KodKW"
                ddlKWTo.DataBind()
            End Using

            Using dt = ds.Tables(1)
                ddlKOFrom.DataSource = dt
                ddlKOFrom.DataTextField = "Butiran2"
                ddlKOFrom.DataValueField = "kodko"
                ddlKOFrom.DataBind()

                ddlKOTo.DataSource = dt
                ddlKOTo.DataTextField = "Butiran2"
                ddlKOTo.DataValueField = "kodko"
                ddlKOTo.DataBind()
            End Using

            Using dt = ds.Tables(2)
                ddlPTjFrom.DataSource = dt
                ddlPTjFrom.DataTextField = "Butiran2"
                ddlPTjFrom.DataValueField = "KodPTJ"
                ddlPTjFrom.DataBind()

                ddlPTjTo.DataSource = dt
                ddlPTjTo.DataTextField = "Butiran2"
                ddlPTjTo.DataValueField = "KodPTJ"
                ddlPTjTo.DataBind()
            End Using

            Using dt = ds.Tables(3)
                ddlKPFrom.DataSource = dt
                ddlKPFrom.DataTextField = "Butiran2"
                ddlKPFrom.DataValueField = "KodProjek"
                ddlKPFrom.DataBind()

                ddlKPTo.DataSource = dt
                ddlKPTo.DataTextField = "Butiran2"
                ddlKPTo.DataValueField = "KodProjek"
                ddlKPTo.DataBind()
            End Using

            Using dt = ds.Tables(4)
                ddlVotFrom.DataSource = dt
                ddlVotFrom.DataTextField = "Butiran2"
                ddlVotFrom.DataValueField = "KodVot"
                ddlVotFrom.DataBind()

                ddlVotTo.DataSource = dt
                ddlVotTo.DataTextField = "Butiran2"
                ddlVotTo.DataValueField = "KodVot"
                ddlVotTo.DataBind()
            End Using
        End Using
    End Sub


    Protected Sub lbtnPaparCarian1_Click(sender As Object, e As EventArgs) Handles lbtnPaparCarian1.Click
        Try
            Dim namaLaporan = ""
            Dim url As String = ""


            Dim KWFrom = ddlKwFrom.SelectedValue
            Dim KWTo = ddlKWTo.SelectedValue
            Dim KpFrom = ddlKPFrom.SelectedValue
            Dim KpTo = ddlKPTo.SelectedValue
            Dim KoFrom = ddlKOFrom.SelectedValue
            Dim KoTo = ddlKOTo.SelectedValue
            Dim PtjFrom = ddlPTjFrom.SelectedValue
            Dim PtjTo = ddlPTjTo.SelectedValue
            Dim votFrom = ddlVotFrom.SelectedValue
            Dim votTo = ddlVotTo.SelectedValue


            'Dim tahun = StartDate.Year
            'Dim StartDate As Date = CDate(txtStartDate.Text)
            'Dim tarikhMula As String = StartDate.ToString("MM/dd/yyyy")


            Dim StartDate As Date = DateTime.ParseExact(Trim(txtStartDate.Text.TrimEnd), "dd/MM/yyyy", CultureInfo.CurrentCulture)
            Dim tarikhMula As String = StartDate.ToString("yyyy-MM-dd")
            Dim tahun = StartDate.Year


            If ViewState("KodLaporan") = "0102" Then
                'Buku Vot

                'Dim EndDate As Date = CDate(txtEndDate.Text)
                'Dim tarikhHingga As String = EndDate.ToString("MM/dd/yyyy")

                Dim EndDate As Date = DateTime.ParseExact(Trim(txtEndDate.Text.TrimEnd), "dd/MM/yyyy", CultureInfo.CurrentCulture)
                Dim tarikhHingga As String = EndDate.ToString("yyyy-MM-dd")



                namaLaporan = "BG_BukuVot"
                url = $"{namaLaporan}&rs:Command=Render&StafId={Session("ssusrID").ToString}&tkhdr={tarikhMula}&tkhke={tarikhHingga}&ai_tahun={tahun}&kwdr={KWFrom}&kwke={KWTo}&kodr={KoFrom}&koke={KoTo}&ptjdr={PtjFrom}&ptjke={PtjTo}&kpdr={KpFrom}&kpke={KpTo}&votdr={votFrom}&votke={votTo}"
            ElseIf ViewState("KodLaporan") = "0103" Then
                'Baki Vot
                namaLaporan = "BG_BakiVot"
                url = $"{namaLaporan}&rs:Command=Render&StafID={Session("ssusrID").ToString}&ai_tahun={tahun}&kwdr={KWFrom}&kwke={KWTo}&kodr={KoFrom}&koke={KoTo}&ptjdr={PtjFrom}&ptjke={PtjTo}&kpdr={KpFrom}&kpke={KpTo}&votdr={votFrom}&votke={votTo}"
            ElseIf ViewState("KodLaporan") = "0116" Then
                'Baki Vot
                namaLaporan = "rpt_Peruntukan_KW"
                'url = $"{namaLaporan}&rs:Command=Render&StartDate={tarikhMula}&EndDate={tarikhHingga}&KWFrom={KWFrom}&KWTo={KWTo}"
            ElseIf ViewState("KodLaporan") = "0117" Then
                'Baki Vot
                namaLaporan = "BG_ABM7"
                'url = $"{namaLaporan}&rs:Command=Render&StartDate={tarikhMula}&EndDate={tarikhHingga}&KWFrom={KWFrom}&KWTo={KWTo}"
            End If


            Dim strSVR = $"https://reporting.utem.edu.my/ReportServer/Pages/ReportViewer.aspx?%2fS2%2fS2D%2f{url}"
            Dim fullURL As String = $"window.open('{strSVR}', '_blank', 'width=' + (parseInt(window.innerWidth) * 0.4) + ',height=' + (parseInt(window.innerHeight) * 1.0) + ',status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=yes,titlebar=no' );"
            ScriptManager.RegisterStartupScript(Me, GetType(String), "OPEN_WINDOW", fullURL, True)
        Catch ex As Exception

        End Try
    End Sub
End Class