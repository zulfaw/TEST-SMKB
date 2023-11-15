Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Public Class Penyediaan_Bajet_PTJ
    Inherits System.Web.UI.Page
    Dim ds As New DataSet
    Public Shared dvPTJ As New DataView
    Public Shared dvPermohonan As New DataView
    Public Shared dv As New DataView
    Public Shared strObjekAM As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Dim dt As New DataTable

                fFindRec()
                dt = fCreateDt("00", "00", "00")
                gvStatusPermohonan.DataSource = dt
                gvStatusPermohonan.DataBind()

                Dim dt2 As DataTable = dvPTJ.ToTable(True, "Bahagian", "KodBah")
                ddlBahagian.DataSource = dt2
                ddlBahagian.DataTextField = "Bahagian"
                ddlBahagian.DataValueField = "KodBah"
                ddlBahagian.DataBind()

                ddlBahagian.Items.Insert(0, New ListItem("-KESELURUHAN-", "00"))
                ddlBahagian.SelectedIndex = 0

                ddlUnit.Items.Insert(0, New ListItem("-KESELURUHAN-", "00"))
                ddlUnit.SelectedIndex = 0

                fBindDdlKodOperasi()
                ddlKodOperasi.Items.Insert(0, New ListItem("-KESELURUHAN-", "00"))
                ddlKodOperasi.SelectedIndex = 0

                'gvStatusPermohonan.DataSource = New List(Of String)
                'gvStatusPermohonan.DataBind()

                'gvChartOfAccount.DataSource = New List(Of String)
                'gvChartOfAccount.DataBind()

            End If
        Catch ex As Exception
            'fErrorLog("Pendaftaran_Menu.aspx(Page_Load())- " & ex.Message.ToString)
        End Try
    End Sub

    Private Function fFindRec() As DataTable
        Try
            Dim strSql As String

            strSql = "SELECT a.KodPTJ,a.Butiran,a.Singkatan,a.Status AS StatusPTJ,(a.KodPTJ + ' - ' + a.Butiran) as PTJ,
                                           b.KodBah, b.NamaBahagian, b.Status AS StatusBhgn,(b.KodBah + ' - ' + b.NamaBahagian) as Bahagian,
                                           c.KodUnit, c.NamaUnit, c.Status AS StatusUnit,(c.KodUnit + ' - ' + c.NamaUnit) as Unit
                                    FROM MK_PTJ a Left OUTER JOIN MK_BahagianPTJ b ON b.KodPtj = a.KodPTJ
                                    LEFT OUTER JOIN MK_UnitPTJ AS c ON b.KodBah = c.KodBah
                                    where a.Status=1 and
                                    a.KodPTJ=41"

            ds = BindSQL(strSql)
            dvPTJ = New DataView(ds.Tables(0))
            ds = Nothing

            strSql = "SELECT a.BG20_NoMohon, a.BG20_Program, b.Butiran,a.BG20_AmaunMohon,c.BG_Butiran,
                             a.BG20_TarikhMohon, a.KodPtj, a.KodBahagian, a.KodUnitPtj, 
                             a.BG20_TahunBajet, a.KodDasar, a.KodOperasi, a.BG20_Status
                         
                            FROM  BG20_Mohon  a INNER JOIN BG_KodOperasi b ON a.KodOperasi = b.KodOperasi
                            INNER JOIN BG_StatusDok c ON a.StatusDok = c.BG_KodStatus
                            WHERE (StatusDok = '03') OR
                            (StatusDok = '08')"
            '    "SELECT a.BG20_NoMohon, a.BG20_Program,f.Butiran ,d.Total,a.BG20_Status,
            '                   a.KodOperasi,a.BG20_TarikhMohon, a.KodPtj, a.KodBahagian, a.KodUnitPtj,
            '                   a.KodMaksud, a.KodKorporat, a.BG20_Justifikasi, a.BG20_TahunBajet,
            '                   a.KodDasar,   a.StaffIDPemohon, a.BG20_StatusDok,
            '                   a.StaffIDPenyokong, a.StaffIDPengesah, a.StaffIDPelulus,
            '                   b.BG20_NoButiran, b.BG20_Butiran, 
            '                   b.BG20_Kuantiti, b.BG20_AngHrgUnit, b.BG20_AngJumlah, b.BG20_Status AS StatusButiran, 
            '                   b.KodVotSebagai, b.KodVotLanjut

            '                   From BG20_Mohon a INNER Join BG20_MohonDt b On a.BG20_NoMohon = b.BG20_NoMohon
            '                   LEFT OUTER JOIN
            '                       (SELECT c.BG20_NoMohon, SUM(c.BG20_AngJumlah) AS Total
            '                        FROM  BG20_MohonDt c
            '                        GROUP BY c.BG20_NoMohon) d ON d.BG20_NoMohon = b.BG20_NoMohon
            'LEFT OUTER JOIN
            '                       (SELECT e.KodOperasi,e.Butiran
            '                        FROM  BG_KodOperasi e
            '                        ) f ON a.KodOperasi = f.KodOperasi
            '                  WHERE  a.BG20_Status=1 and a.KodPTJ=41"

            ds = BindSQL(strSql)
            dvPermohonan = New DataView(ds.Tables(0))
            ds = Nothing

        Catch ex As Exception
            'fErrorLog("Pendaftaran_Menu.aspx(fCreateDt())- " & ex.Message.ToString())
        End Try
    End Function

    Public Function ToTable(distinct As Boolean, ParamArray columnNames As String()) As DataTable

    End Function

    Private Sub ddlBahagian_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBahagian.SelectedIndexChanged
        Try
            Dim strBahagian As String = ddlBahagian.SelectedValue.ToString
            dvPTJ.RowFilter = [String].Empty
            dvPTJ.RowFilter = "KodBah= '" & strBahagian & "'"
            'Dim dt2 As DataTable = dv.ToTable(True, "NamaUnit", "Unit")
            ddlUnit.DataSource = dvPTJ
            ddlUnit.DataTextField = "Unit"
            ddlUnit.DataValueField = "KodUnit"
            ddlUnit.DataBind()
            ddlUnit.Items.Insert(0, New ListItem("KESELURUHAN", ""))
            ddlUnit.SelectedIndex = 0

        Catch ex As Exception
            'fErrorLog("Pendaftaran_Menu.aspx(ddlKodModul_SelectedIndexChanged)- " & ex.Message.ToString)
        End Try
    End Sub

    Private Sub fBindDdlKodOperasi()
        Try
            'Dim strSql As String = $"Select KodOperasi, (KodOperasi + ' - ' + Butiran) AS Butiran2 from BG_KodOperasi"

            'Dim ds As New DataSet
            'Dim dbconn As New DBKewConn
            'ds = dbconn.fselectCommand(strSql)
            ds = BindSQL($"Select KodOperasi, (KodOperasi + ' - ' + Butiran) AS Butiran2 from BG_KodOperasi")

            ddlKodOperasi.DataSource = ds
            ddlKodOperasi.DataTextField = "Butiran2"
            ddlKodOperasi.DataValueField = "KodOperasi"
            ddlKodOperasi.DataBind()
        Catch ex As Exception
            'fErrorLog("Peruntukan_KW.aspx(fBindDdlKW)- " & ex.Message.ToString)
        End Try
    End Sub
    Public Sub OnConfirm(sender As Object, e As EventArgs) Handles btnPapar.Click
        Dim dt As New DataTable
        Try
            Dim strUnit As String = ddlUnit.SelectedValue.ToString
            Dim strBahagian As String = ddlBahagian.SelectedValue.ToString
            Dim strKodOperasi As String = ddlKodOperasi.SelectedValue.ToString

            dt = fCreateDt(strUnit, strBahagian, strKodOperasi)
            gvStatusPermohonan.DataSource = dt
            gvStatusPermohonan.DataBind()
        Catch ex As Exception
            'fErrorLog("Pendaftaran_Menu.aspx(Page_Load())- " & ex.Message.ToString)
        End Try

    End Sub

    Private Function fCreateDt(ByVal strUnit As String, ByVal strBahagian As String, ByVal strKod As String) As DataTable

        Try
            Dim dt As New DataTable

            'dt.Columns.Add("Pilih", GetType(Boolean))
            dt.Columns.Add("Bil", GetType(Integer))
            dt.Columns.Add("NoPermohonan", GetType(String))
            dt.Columns.Add("Program", GetType(String))
            dt.Columns.Add("KodOperasi", GetType(String))
            dt.Columns.Add("Anggaran", GetType(Double))
            dt.Columns.Add("Status", GetType(String))

            'Dim blnPilih As Boolean
            Dim intBil As Integer
            Dim strNoPermohonan As String
            Dim strProgram As String
            Dim strKodOperasi As String
            Dim dblAnggaran As Double
            Dim strStatus As String

            If strBahagian = "00" Then
                dvPermohonan.RowFilter = [String].Empty
            Else
                dvPermohonan.RowFilter = "KodBahagian= '" & strBahagian & "'"
                If strUnit <> "00" Then
                    dvPermohonan.RowFilter = "KodUnit= '" & strUnit & "'"
                End If
            End If

            If strKod <> "00" Then
                dvPermohonan.RowFilter = "KodOperasi= '" & strKod & "'"
            End If

            'For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
            For i As Integer = 0 To dvPermohonan.Count - 1
                'blnPilih = False
                intBil = i + 1
                strNoPermohonan = dvPermohonan(i)(0).ToString
                strProgram = dvPermohonan(i)(1).ToString
                strKodOperasi = dvPermohonan(i)(2).ToString
                dblAnggaran = dvPermohonan(i)(3).ToString
                strStatus = dvPermohonan(i)(4).ToString
                dt.Rows.Add(intBil, strNoPermohonan, strProgram, strKodOperasi, dblAnggaran, strStatus)
            Next

            Return dt

        Catch ex As Exception
            'fErrorLog("Pendaftaran_Menu.aspx(fCreateDt())- " & ex.Message.ToString())
        End Try

    End Function

    Protected Sub gvStatusPermohonan_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvStatusPermohonan.RowCommand
        ' If multiple ButtonField column fields are used, use the
        ' CommandName property to determine which button was clicked.
        If e.CommandName = "Select" Then

            ' Convert the row index stored in the CommandArgument
            ' property to an Integer.
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)

            ' Get the last name of the selected author from the appropriate
            ' cell in the GridView control.
            Dim selectedRow As GridViewRow = gvStatusPermohonan.Rows(index)
            Dim NoMohon As String = selectedRow.Cells(1).Text

            ' Display the selected author.
            'Message.Text = "You selected " & contact & "."

            'Open other page.
            Response.Redirect($"~/FORMS/BAJET/PERMOHONAN BAJET/MaklumatPenyediaanBajetPTJ.aspx?no={NoMohon}")
        End If
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

    Protected Sub lbtnDrafABM_Click(sender As Object, e As EventArgs) Handles lbtnDrafABM.Click
        Response.Redirect("~/FORMS/BAJET/PERMOHONAN BAJET/Draf_ABM.aspx?no=0")
    End Sub



    Private Sub gvStatusPermohonan_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvStatusPermohonan.PageIndexChanging
        Try

            gvStatusPermohonan.PageIndex = e.NewPageIndex

            If Session("SortedView") IsNot Nothing Then
                gvStatusPermohonan.DataSource = Session("SortedView")
                gvStatusPermohonan.DataBind()
            Else

                'Dim dt As New DataTable
                'dt = fCreateDt2()
                'RaiseEvent onconfirm()
                'btnPapar.Click()
                'gvStatusPermohonan.DataSource = dvButiran
                'gvStatusPermohonan.DataBind()


                Dim dt As New DataTable
                'Try
                Dim strUnit As String = ddlUnit.SelectedValue.ToString
                    Dim strBahagian As String = ddlBahagian.SelectedValue.ToString
                    Dim strKodOperasi As String = ddlKodOperasi.SelectedValue.ToString

                    dt = fCreateDt(strUnit, strBahagian, strKodOperasi)
                    gvStatusPermohonan.DataSource = dt
                    gvStatusPermohonan.DataBind()
                'Catch ex As Exception
                'fErrorLog("Pendaftaran_Menu.aspx(Page_Load())- " & ex.Message.ToString)
                'End Try

            End If

        Catch ex As Exception
            'fErrorLog(ex.Message.ToString)
        End Try
    End Sub
    Public Property direction() As SortDirection
        Get
            If ViewState("directionState") Is Nothing Then
                ViewState("directionState") = SortDirection.Ascending
            End If
            Return DirectCast(ViewState("directionState"), SortDirection)
        End Get
        Set
            ViewState("directionState") = Value
        End Set
    End Property




    'Protected Sub lbtnMohonBaru_Click(sender As Object, e As EventArgs) Handles lbtnMohonBaru.Click
    '    Response.Redirect("~/FORMS/BAJET/PERMOHONAN BAJET/MaklumatPermohonanBajet.aspx?no=0")
    'End Sub
    'Private Sub gvStatusPermohonan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvStatusPermohonan.SelectedIndexChanged
    '    Try
    '        Dim dt As New DataTable

    '        dt.Columns.Add("Bil", GetType(Integer))
    '        dt.Columns.Add("Butiran", GetType(String))
    '        dt.Columns.Add("KW", GetType(String))
    '        dt.Columns.Add("ObjectAM", GetType(String))
    '        dt.Columns.Add("ObjectSebagai", GetType(String))
    '        dt.Columns.Add("ObjectLanjut", GetType(String))
    '        dt.Columns.Add("Anggaran", GetType(Double))

    '        For Each row As GridViewRow In gvStatusPermohonan.Rows
    '            If row.RowIndex = gvStatusPermohonan.SelectedIndex Then
    '                Dim strNoPermohonan As String = gvStatusPermohonan.Rows(row.RowIndex).Cells(1).Text
    '                Select Case strNoPermohonan
    '                    Case = "201741010101"
    '                        dt.Rows.Add(1, "Sewaan", "01", "", "", " ", 10000)
    '                        dt.Rows.Add(1, "Latihan", "01", "", "", " ", 5000)
    '                End Select

    '                gvChartOfAccount.DataSource = dt
    '                gvChartOfAccount.DataBind()
    '            Else
    '                row.ForeColor = ColorTranslator.FromHtml("#000000")
    '                row.ToolTip = "Klik untuk pilih rekod ini."
    '            End If
    '        Next
    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Private Sub gvChartOfAccount_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvChartOfAccount.RowDataBound
    '    If (e.Row.RowType = DataControlRowType.DataRow) Then

    '        'Find the DropDownList in the Row
    '        Dim ddlKW As DropDownList = CType(e.Row.FindControl("ddlKW"), DropDownList)

    '        ds = BindSQL("Select KodKw, Butiran, (KodKw + ' - ' + Butiran) as Butiran2 From MK_Kw ORDER BY KodKw")
    '        ddlKW.DataSource = ds
    '        ddlKW.DataTextField = "Butiran2"
    '        ddlKW.DataValueField = "KodKw"
    '        ddlKW.DataBind()

    '        ddlKW.Items.Insert(0, New ListItem(String.Empty, String.Empty))
    '        ddlKW.SelectedIndex = 0


    '        ds = BindSQL("SELECT KodVot, Butiran, Klasifikasi,(KodVot + ' - ' + Butiran) as Butiran2, 
    '        LEFT(KodVot, 1) AS KodObjekAm,
    '        LEFT(KodVot, 2) AS KodObjekSebagai
    '        FROM MK_Vot ORDER BY KodVot")
    '        dv = New DataView(ds.Tables(0))

    '        Dim ddlObjekAM As DropDownList = CType(e.Row.FindControl("ddlObjekAM"), DropDownList)
    '        dv.RowFilter = [String].Empty
    '        dv.RowFilter = "Klasifikasi= 'H1'"
    '        ddlObjekAM.DataSource = dv
    '        ddlObjekAM.DataTextField = "Butiran2"
    '        ddlObjekAM.DataValueField = "KodVot"
    '        ddlObjekAM.DataBind()

    '        ddlObjekAM.Items.Insert(0, New ListItem(String.Empty, String.Empty))
    '        ddlObjekAM.SelectedIndex = 0

    '    End If
    'End Sub

    '

    '' Public Sub ddlObjekAM_SelectedIndexChanged(sender As Object, e As GridViewRowEventArgs)
    'Public Sub ddlObjekAM_SelectedIndexChanged(sender As Object, e As EventArgs)

    '    Try
    '        ' Dim ddlModuls As DropDownList = CType(e.Row.FindControl("ddlModul"), DropDownList)
    '        Dim ddl As DropDownList = DirectCast(sender, DropDownList)
    '        Dim strVal As String = ddl.SelectedValue
    '        Dim strObjekAM As String = strVal.Substring(0, 1)
    '        'dv.RowFilter = strVal  ' [String].Empty
    '        'dv.RowFilter = "Klasifikasi= 'H2' and KodObjekAm=left('" & strObjekAM & "',1)"
    '        dv.RowFilter = "Klasifikasi= 'H2' and KodObjekAm= '" & strObjekAM & "'"

    '        'Dim ddlObjekSebagai As DropDownList = CType(e.Row.FindControl("ddlObjekSebagai"), DropDownList)

    '        'Dim ddl As DropDownList = DirectCast(sender, DropDownList)
    '        Dim row As GridViewRow = DirectCast(ddl.Parent.Parent, GridViewRow)
    '        Dim idx As Integer = row.RowIndex

    '        Dim ddlObjekSebagai As DropDownList = DirectCast(row.Cells(0).FindControl("ddlObjekSebagai"), DropDownList)
    '        ddlObjekSebagai.DataSource = dv
    '        ddlObjekSebagai.DataTextField = "Butiran2"
    '        ddlObjekSebagai.DataValueField = "KodVot"
    '        ddlObjekSebagai.DataBind()


    '    Catch ex As Exception

    '    End Try

    '    'Dim ddlObjekAM As DropDownList = DirectCast(sender, DropDownList)
    '    'Dim grdrDropDownRow As GridViewRow = DirectCast(ddlObjekAM.Parent.Parent, GridViewRow)
    '    'Dim lblCurrentStatus As Label = DirectCast(grdrDropDownRow.FindControl("lblOrderStatus"), Label)
    '    'If lblCurrentStatus IsNot Nothing Then
    '    '    lblCurrentStatus.Text = ddlObjekAM.SelectedItem.Text
    '    'End If



    '    'Dim strObjekAm As String = ddlObjekAM.SelectedValue.ToString
    '    'Dim ddlObjekSebagai As DropDownList = CType(e.Row.FindControl("ddlObjekSebagai"), DropDownList)
    '    'dv.RowFilter = [String].Empty
    '    'dv.RowFilter = "Klasifikasi= 'H2' and KodObjekAm=left('" & strObjekAM & "',1)"
    '    'ddlObjekSebagai.DataSource = dv
    '    'ddlObjekSebagai.DataTextField = "Butiran2"
    '    'ddlObjekSebagai.DataValueField = "KodVot"
    '    'ddlObjekSebagai.DataBind()

    '    'ddlObjekSebagai.Items.Insert(0, New ListItem(String.Empty, String.Empty))
    '    'ddlObjekSebagai.SelectedIndex = 0


    'End Sub
    'Public Sub ddlObjekSebagai_SelectedIndexChanged(sender As Object, e As EventArgs)
    '    Try

    '        Dim ddl As DropDownList = DirectCast(sender, DropDownList)
    '        Dim strVal As String = ddl.SelectedValue
    '        Dim strObjekSebagai As String = strVal.Substring(0, 2)

    '        dv.RowFilter = "Klasifikasi= 'H3' and KodObjekSebagai= '" & strObjekSebagai & "'"

    '        Dim row As GridViewRow = DirectCast(ddl.Parent.Parent, GridViewRow)
    '        Dim idx As Integer = row.RowIndex

    '        Dim ddlObjekLanjut As DropDownList = DirectCast(row.Cells(0).FindControl("ddlObjekLanjut"), DropDownList)
    '        ddlObjekLanjut.DataSource = dv
    '        ddlObjekLanjut.DataTextField = "Butiran2"
    '        ddlObjekLanjut.DataValueField = "KodVot"
    '        ddlObjekLanjut.DataBind()


    '    Catch ex As Exception

    '    End Try

    'End Sub
End Class