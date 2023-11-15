Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Public Class Maklumat_PTJ
    Inherits System.Web.UI.Page
    Dim ds As New DataSet
    Public Shared dv As New DataView
    Dim strKategori As String
    Dim strKodBahagian As String
    Dim strKodPtj As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                ddlPTJ.Visible = False
                ddlBahagian.Visible = False
                fFindRec()

                strKategori = "PTJ"
                fFilterNama(strKategori, "", "")

            End If
        Catch ex As Exception
            'fErrorLog("Pendaftaran_Menu.aspx(Page_Load())- " & ex.Message.ToString)
        End Try
    End Sub
    Private Function fFindRec() As DataTable
        Try
            Dim strSql As String = "SELECT a.KodPTJ,a.Butiran,a.Singkatan,a.Status AS StatusPTJ,(a.KodPTJ + ' - ' + a.Butiran) as PTJ,
                                           b.KodBah, b.NamaBahagian, b.Status AS StatusBhgn,(b.KodBah + ' - ' + b.NamaBahagian) as Bahagian,
                                           c.KodUnit, c.NamaUnit, c.Status AS StatusUnit,(c.KodUnit + ' - ' + c.NamaUnit) as Unit
                                    FROM MK_PTJ a Left OUTER JOIN MK_BahagianPTJ b ON b.KodPtj = a.KodPTJ
                                    LEFT OUTER JOIN MK_UnitPTJ AS c ON b.KodBah = c.KodBah AND b.KodPtj = c.KodPTJ
                                    where a.Status=1"

            '            "SELECT        a.KodPTJ, a.Butiran, a.Singkatan, a.Status AS StatusPTJ, b.KodBah, b.NamaBahagian, b.Status AS StatusBhgn, c.KodUnit, c.NamaUnit, c.Status
            'FROM            MK_PTJ AS a LEFT OUTER JOIN
            '                         MK_BahagianPTJ AS b ON a.KodPTJ = b.KodPtj LEFT OUTER JOIN
            '                         MK_UnitPTJ AS c ON b.KodBah = c.KodBah AND b.KodPtj = c.KodPTJ"
            ds = BindSQL(strSql)
            dv = New DataView(ds.Tables(0))

        Catch ex As Exception
            'fErrorLog("Pendaftaran_Menu.aspx(fCreateDt())- " & ex.Message.ToString())
        End Try
    End Function

    Private Function fClearItem()
        ddlPTJ.Items.Clear()
        ddlBahagian.Items.Clear()
        ddlNama.Items.Clear()
        'txtNamaSingkatan.Text = ""
        'txtKod.Text = ""

    End Function

    Private Sub ddlKategori_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKategori.SelectedIndexChanged
        fClearItem()


        If ddlKategori.SelectedValue.ToString = "PTJ" Then
            ddlPTJ.Visible = False
            ddlBahagian.Visible = False
            strKategori = "PTJ"
            fFilterNama(strKategori, "", "")

        ElseIf ddlKategori.SelectedValue.ToString = "Bahagian" Then
            ddlPTJ.Visible = True
            ddlBahagian.Visible = False
            dv.RowFilter = [String].Empty
            Dim dt As DataTable = dv.ToTable(True, "Butiran", "KodPTJ")
            ddlPTJ.DataSource = dt
            ddlPTJ.DataTextField = "PTJ"
            ddlPTJ.DataValueField = "KodPTJ"
            ddlPTJ.DataBind()

        ElseIf ddlKategori.SelectedValue.ToString = "Unit" Then
            ddlPTJ.Visible = True
            ddlBahagian.Visible = True

            dv.RowFilter = [String].Empty
            Dim dt As DataTable = dv.ToTable(True, "Butiran", "KodPTJ")
            ddlPTJ.DataSource = dt
            ddlPTJ.DataTextField = "PTJ"
            ddlPTJ.DataValueField = "KodPTJ"
            ddlPTJ.DataBind()
        End If
    End Sub

    Private Sub ddlPTJ_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPTJ.SelectedIndexChanged
        ddlNama.Items.Clear()
        If ddlKategori.SelectedValue.ToString = "Bahagian" Then
            strKategori = "Bahagian"
            strKodPtj = ddlPTJ.SelectedValue.ToString
            strKodBahagian = ddlBahagian.SelectedValue.ToString
            fFilterNama(strKategori, strKodPtj, "")
        ElseIf ddlKategori.SelectedValue.ToString = "Unit" Then
            dv.RowFilter = [String].Empty
            dv.RowFilter = "KodPTJ= '" & strKodPtj & "'"
            Dim dt As DataTable = dv.ToTable(True, "NamaBahagian", "KodBah")
            ddlBahagian.DataSource = dt
            ddlBahagian.DataTextField = "Bahagian"
            ddlBahagian.DataValueField = "KodBah"
            ddlBahagian.DataBind()
        End If
    End Sub

    Private Sub ddlBahagian_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBahagian.SelectedIndexChanged
        ddlNama.Items.Clear()

        strKategori = "Unit"
        strKodPtj = ddlPTJ.SelectedValue.ToString
        strKodBahagian = ddlBahagian.SelectedValue.ToString
        fFilterNama(strKategori, strKodPtj, strKodBahagian)


    End Sub


    Public Function ToTable(
    distinct As Boolean,
    ParamArray columnNames As String()
) As DataTable

    End Function
    Private Sub fFilterNama(ByVal strKategori As String, ByVal strKodPTJ As String, ByVal strKodBahagian As String)
        Try

            dv.RowFilter = [String].Empty

            If strKategori = "PTJ" Then
                Dim dt As DataTable = dv.ToTable(True, "PTJ", "KodPTJ")
                ddlNama.DataSource = dt
                ddlNama.DataTextField = "PTJ"
                ddlNama.DataValueField = "KodPTJ"

            ElseIf strKategori = "Bahagian" Then

                dv.RowFilter = "KodPTJ= '" & strKodPTJ & "'"
                ddlNama.DataSource = dv
                ddlNama.DataTextField = "Bahagian"
                ddlNama.DataValueField = "KodBah"

            ElseIf strKategori = "Unit" Then
                dv.RowFilter = "KodPTJ= '" & strKodPTJ & "' and KodBah='" & strKodBahagian & "'"
                ddlNama.DataSource = dv
                ddlNama.DataTextField = "Unit"
                ddlNama.DataValueField = "KodUnit"
            End If

            ddlNama.DataBind()

        Catch ex As Exception
            'fErrorLog("Pendaftaran_Menu.aspx(fBindDdlKodModul)- " & ex.Message.ToString)
        End Try
    End Sub

    Private Function BindSQL(ByVal strSql As String) As DataSet
        Try
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fselectCommand(strSql)
            Return ds
        Catch ex As Exception
            'fErrorLog(ex.Message.ToString)
        End Try
    End Function

    'Private Sub ddlNama_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlNama.SelectedIndexChanged

    '    Dim strNamaSingkatan As String
    '    Dim strKod As String

    '    If ddlKategori.SelectedValue.ToString = "PTJ" Then
    '        dv.RowFilter = "KodPTJ= '" & ddlNama.SelectedValue.ToString & "'"
    '        strNamaSingkatan = dv(0)(2).ToString
    '        strKod = dv(0)(0).ToString

    '    ElseIf ddlKategori.SelectedValue.ToString = "Bahagian" Then

    '        dv.RowFilter = "KodPTJ= '" & ddlPTJ.SelectedValue.ToString & "' and KodBah= '" & ddlNama.SelectedValue.ToString & "'"

    '        strNamaSingkatan = dv(0)(4).ToString
    '        strKod = dv(0)(3).ToString

    '    ElseIf ddlKategori.SelectedValue.ToString = "Unit" Then
    '        dv.RowFilter = "KodPTJ= '" & ddlPTJ.DataValueField & "' and KodBah= '" & ddlBahagian.DataValueField & "' and KodUnit= '" & ddlNama.DataValueField & " ' "
    '        strNamaSingkatan = dv(0)(2).ToString
    '        strKod = dv(0)(0).ToString
    '    End If
    '    txtNamaSingkatan.Text = strNamaSingkatan
    '    txtKod.Text = strKod
    'End Sub
End Class