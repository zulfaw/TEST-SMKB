Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports System.Globalization
Imports AjaxControlToolkit
Imports System.Data.Entity.SqlServer
Imports System
Imports System.Windows.Forms
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Tab


Public Class Elaun
    Inherits System.Web.UI.Page
    Private sqlCon As SqlConnection
    Private strConn As String = "Data Source=devmis12.utem.edu.my;Initial Catalog=dbKewanganV4;Persist Security Info=True;User ID=smkb;Password=Smkb@Dev2012"
    Private strConnStaf As String = "Data Source=devmis11.utem.edu.my;Initial Catalog=dbStaf;Persist Security Info=True;User ID=smkb;Password=Smkb@Dev2012"
    'Private oDB As IDatabase
    Dim dbconn As New DBKewConn
    Dim dbconnstaf As New DBSMConn
    Dim SQL As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Not IsPostBack Then
            Dim kodsubmenu As String = Request.QueryString("kodsubmenu")
            If kodsubmenu <> "" Then
                Session("Kod_Sub_Menu") = kodsubmenu
            End If

            BindListViewControls()

            fBindDdlVotTetap(ddlVotTetap)
            fBindDdlVotXTetap(ddlVotBknTetap)
            fBindDdlVotTetap(ddlVotE)
            fBindDdlVotXTetap(ddlVotxE)
            ViewState("savemode") = 1
        End If

    End Sub

    Private Sub BindListViewControls()

        Dim query As String = "Select * from SMKB_Gaji_Elaun order by kod"
        Dim da As SqlDataAdapter = New SqlDataAdapter(query, strConn)
        Dim table As DataTable = New DataTable()

        da.Fill(table)
        lvElaun.DataSource = table
        lvElaun.DataBind()
    End Sub

    Private Sub fBindDdlVotTetap(ddl As DropDownList)
        Dim strSql As String = "select kod_vot,kod_vot + ' - ' + butiran as butirans,butiran,Kod_klasifikasi from SMKB_vot where Kod_klasifikasi = 'D' order by kod_vot"
        Using dt = dbconn.fSelectCommandDt(strSql)
            ddl.DataSource = dt
            ddl.DataTextField = "butirans"
            ddl.DataValueField = "kod_vot"
            ddl.DataBind()

            ddl.Items.Insert(0, New ListItem("- SILA PILIH - ", ""))
            ddl.SelectedIndex = 0

        End Using
    End Sub
    Private Sub fBindDdlVotXTetap(ddl As DropDownList)
        Dim strSql As String = "select kod_vot,kod_vot + ' - ' + butiran as butirans,butiran,Kod_klasifikasi from SMKB_vot where Kod_klasifikasi = 'D' order by kod_vot"
        Using dt = dbconn.fSelectCommandDt(strSql)


            ddl.DataSource = dt
            ddl.DataTextField = "butirans"
            ddl.DataValueField = "kod_vot"
            ddl.DataBind()

            ddl.Items.Insert(0, New ListItem("- SILA PILIH - ", ""))
            ddl.SelectedIndex = 0

        End Using
    End Sub

    Protected Sub lvElaun_PagePropertiesChanging(sender As Object, e As PagePropertiesChangingEventArgs)

        TryCast(lvElaun.FindControl("DataPager1"), DataPager).SetPageProperties(e.StartRowIndex, e.MaximumRows, False)
        'lvElaun.DataBind()
        BindListViewControls()
    End Sub
    Private Function fCheckKodElaun(ByVal strKod As String) As Boolean
        Dim strSql = "select count(*) from smkb_gaji_elaun  where Kod = '" & strKod & "'"
        Dim intCnt As Integer = dbconn.fSelectCount(strSql)
        If intCnt < 1 Then
            Return False
        Else
            Return True
        End If

    End Function
    Private Function fCheckMaster(ByVal strKod As String) As Boolean
        Dim strSql = "select count(*) from smkb_gaji_master  where Kod_trans = '" & strKod & "'"
        Dim intCnt As Integer = dbconn.fSelectCount(strSql)
        If intCnt < 1 Then
            Return False
        Else
            Return True
        End If

    End Function
    Private Function fInsertElaun() As Boolean
        Try
            Dim strSql As String
            Dim strKodElaun As String = Trim(txtKodElaun.Text.TrimEnd)
            Dim strNamaElaun As String = Trim(txtNamaElaun.Text.ToUpper.TrimEnd)
            Dim strVotTetap As String = ddlVotTetap.SelectedValue
            Dim strVotXTetap As String = ddlVotBknTetap.SelectedValue
            Dim strKiraKwsp As Byte
            Dim strKiraPerkeso As Byte
            Dim strKiraCukai As Byte
            Dim strKiraPencen As Byte

            If rbKWSP.SelectedValue = "1" Then
                strKiraKwsp = True
            Else
                strKiraKwsp = False
            End If
            If rbPerkeso.SelectedValue = "1" Then
                strKiraPerkeso = True
            Else
                strKiraPerkeso = False
            End If
            If rbCukai.SelectedValue = "1" Then
                strKiraCukai = True
            Else
                strKiraCukai = False
            End If
            If rbPencen.SelectedValue = "1" Then
                strKiraPencen = True
            Else
                strKiraPencen = False
            End If

            strSql = "insert into SMKB_Gaji_ELAUN (kod, Butiran, Vot_Tetap, Vot_Bukan_Tetap, Kira_Kwsp, Kira_Perkeso, Kira_Cukai, Kira_Pencen) values (@KodElaun,@KodNamaElaun,@VotTetap,@VotBknTetap, @KiraKwsp, @KiraPerkeso, @KiraCukai, @KiraPencen)"

            Dim paramSql() As SqlParameter = {
                    New SqlParameter("@KodElaun", strKodElaun),
                    New SqlParameter("@KodNamaElaun", strNamaElaun),
                    New SqlParameter("@VotTetap", strVotTetap),
                    New SqlParameter("@VotBknTetap", strVotXTetap),
                   New SqlParameter("@KiraKwsp", strKiraKwsp),
                    New SqlParameter("@KiraPerkeso", strKiraPerkeso),
                     New SqlParameter("@KiraCukai", strKiraCukai),
                     New SqlParameter("@KiraPencen", strKiraPencen)
                }

            dbconn = New DBKewConn
            dbconn.sConnBeginTrans()
            If dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                dbconn.sConnCommitTrans()
                Return True
            Else
                dbconn.sConnRollbackTrans()
                Return False
            End If


        Catch ex As Exception
            Return False
        End Try
    End Function
    Protected Sub lbtnSimpan_Click(sender As Object, e As EventArgs) Handles lbtnSimpan.Click

        Dim strKod As String = Trim(txtKodElaun.Text.TrimEnd)
        Dim strButir As String = Trim(txtNamaElaun.Text.TrimEnd)

        ViewState("SaveMode") = "1"

        If fCheckKodElaun(strKod) = False Then
            If fInsertElaun() = True Then
                Alert("Rekod baru telah ditambah!")
                BindListViewControls()
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "alert", "closeModal();", True)
                'fResetElaun()
            Else
                Alert("Rekod baru gagal ditambah!")
            End If
        Else
            Alert("Kod Elaun yang dimasukkan telah wujud! Sila masukkan Kod Elaun lain.")
        End If


    End Sub
    Private Sub Alert(msg As String)
        Dim sb As New StringBuilder()
        sb.Append("alert('")
        sb.Append(msg.Replace(vbLf, "\n").Replace(vbCr, "").Replace("'", "\'"))
        sb.Append("');")
        ScriptManager.RegisterStartupScript(Me.Page, Me.[GetType](), "showalert", sb.ToString(), True)
    End Sub

    Protected Sub lvElaun_ItemDataBound(sender As Object, e As ListViewItemEventArgs)
        If lvElaun.EditIndex = DirectCast(e.Item, ListViewDataItem).DataItemIndex Then
            Dim dt As New DataTable()

            Dim ddlVot As DropDownList = TryCast(e.Item.FindControl("ddlVotE"), DropDownList)

            fBindDdlVotTetap(ddlVot)

            'Dim lblCountry As Label = TryCast(e.Item., Label)
            'ddlVot.Items.FindByValue(lblCountry.Text).Selected = True
        End If
    End Sub

    Protected Sub lvElaun_ItemCanceling(sender As Object, e As ListViewCancelEventArgs)
        lvElaun.EditIndex = -1
        BindListViewControls()
    End Sub

    Protected Sub lvElaun_ItemDeleting(sender As Object, e As ListViewDeleteEventArgs)

        Dim strSql As String
        dbconn = New DBKewConn
        Dim strKodElaun As String = Trim(txtKodElaun.Text.ToString.TrimEnd)
        Dim item As ListViewDataItem = lvElaun.Items(e.ItemIndex)
        Dim hfKod As HiddenField = CType(item.FindControl("hfKod"), HiddenField)

        ViewState("SaveMode") = "3"

        If fCheckMaster(hfKod.Value) = False Then
            strSql = "delete from smkb_gaji_elaun where Kod = '" & hfKod.Value & "'"
            dbconn.sConnBeginTrans()
            If dbconn.fUpdateCommand(strSql) > 0 Then
                dbconn.sConnCommitTrans()
                Alert("Rekod telah dipadam!")
                BindListViewControls()
            Else
                dbconn.sConnRollbackTrans()
                Alert("Rekod gagal dipadam!")
            End If

        Else
            Alert("Rekod tidak dapat dipadam! Terdapat rekod elaun dalam transaksi gaji.")
        End If

    End Sub

    Protected Sub lvElaun_ItemUpdating(sender As Object, e As ListViewUpdateEventArgs)
        lvElaun.EditIndex = -1
        BindListViewControls()
    End Sub

    Protected Sub lvElaun_ItemEditing(sender As Object, e As ListViewEditEventArgs)
        lvElaun.EditIndex = e.NewEditIndex
        BindListViewControls()
    End Sub


    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs)
        Dim btn As LinkButton = sender
        Dim kod = btn.Attributes("data-id")
        ' MsgBox(btn.Attributes("data-id"))
        txtKodE.Text = kod
        Dim str3 = $"Select * from SMKB_Gaji_Elaun  WHERE kod = '{kod}' order by kod"
        Using dt2 = dbconn.fSelectCommandDt(str3)
            Me.txtNmElaunU.Text = dt2.Rows(0)("butiran")
            Me.ddlVotE.SelectedValue = dt2.Rows(0)("vot_tetap")
            Me.ddlVotxE.SelectedValue = dt2.Rows(0)("vot_bukan_tetap")
            If dt2.Rows(0)("kira_kwsp") = True Then
                rbKwspE.SelectedValue = "1"
            Else
                rbKwspE.SelectedValue = "2"
            End If
            If dt2.Rows(0)("kira_perkeso") = True Then
                rbPerkesoE.SelectedValue = "1"
            Else
                rbPerkesoE.SelectedValue = "2"
            End If
            If dt2.Rows(0)("kira_cukai") = True Then
                rbCukaiE.SelectedValue = "1"
            Else
                rbCukaiE.SelectedValue = "2"
            End If
            If dt2.Rows(0)("kira_pencen") = True Then
                rbPencenE.SelectedValue = "1"
            Else
                rbPencenE.SelectedValue = "2"
            End If
        End Using

        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "Pop", "openModal();", True)
    End Sub
    Protected Sub lbtnUpdate_Click(sender As Object, e As EventArgs) Handles lbtnUpdate.Click

        Dim strSql As String
        Dim strKodElaun As String = Trim(txtKodE.Text.TrimEnd)
        Dim strNamaElaun As String = Trim(txtNmElaunU.Text.ToUpper.TrimEnd)
        Dim strVotTetap As String = ddlVotE.SelectedValue
        Dim strVotXTetap As String = ddlVotxE.SelectedValue
        Dim strKiraKwsp As Byte
        Dim strKiraPerkeso As Byte
        Dim strKiraCukai As Byte
        Dim strKiraPencen As Byte

        ViewState("SaveMode") = "2"

        If rbKwspE.SelectedValue = "1" Then
            strKiraKwsp = True
        Else
            strKiraKwsp = False
        End If
        If rbPerkesoE.SelectedValue = "1" Then
            strKiraPerkeso = True
        Else
            strKiraPerkeso = False
        End If
        If rbCukaiE.SelectedValue = "1" Then
            strKiraCukai = True
        Else
            strKiraCukai = False
        End If
        If rbPencenE.SelectedValue = "1" Then
            strKiraPencen = True
        Else
            strKiraPencen = False
        End If

        strSql = "UPDATE SMKB_Gaji_ELAUN SET Butiran=@KodNamaElaun, Vot_Tetap=@VotTetap, Vot_Bukan_Tetap=@VotBknTetap, Kira_Kwsp=@KiraKwsp,
            Kira_Perkeso=@KiraPerkeso, Kira_Cukai=@KiraCukai, Kira_Pencen=@KiraPencen WHERE kod=@KodElaun"

        Dim paramSql() As SqlParameter = {
                    New SqlParameter("@KodNamaElaun", strNamaElaun),
                    New SqlParameter("@VotTetap", strVotTetap),
                    New SqlParameter("@VotBknTetap", strVotXTetap),
                   New SqlParameter("@KiraKwsp", strKiraKwsp),
                    New SqlParameter("@KiraPerkeso", strKiraPerkeso),
                     New SqlParameter("@KiraCukai", strKiraCukai),
                     New SqlParameter("@KiraPencen", strKiraPencen),
                     New SqlParameter("@KodElaun", strKodElaun)
                }

        dbconn = New DBKewConn
        dbconn.sConnBeginTrans()
        If dbconn.fInsertCommand(strSql, paramSql) > 0 Then
            dbconn.sConnCommitTrans()
            Alert("Rekod telah dikemaskini!")
            BindListViewControls()

        Else
            dbconn.sConnRollbackTrans()
            Alert("Rekod gagal dikemaskini!")

        End If
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "alert", "closeModal();", True)
    End Sub

    Protected Sub btnHapus_Click(sender As Object, e As EventArgs)

    End Sub

    Protected Sub TextBox1_TextChanged(sender As Object, e As EventArgs)
        ' MsgBox("masuk")
        Dim query As String = "Select * from SMKB_Gaji_Elaun where kod like '%" & TextBox1.Text & "%' Or butiran Like '%" & TextBox1.Text & "%'  order by kod"
        Dim da As SqlDataAdapter = New SqlDataAdapter(query, strConn)
        Dim table As DataTable = New DataTable()

        da.Fill(table)
        lvElaun.DataSource = table
        lvElaun.DataBind()

    End Sub


    'Protected Sub rptSubMenu_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)

    '    If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
    '        'Reference the Repeater Item.
    '        Dim item As RepeaterItem = e.Item

    '        'Reference the Controls.
    '        Dim lnk As LinkButton = CType(e.Item.FindControl("LinkButton1"), LinkButton)
    '        If lnk.CommandArgument = Session("Kod_Sub_Menu") Then
    '            lnk.CssClass = "tablinks active"
    '        End If
    '    End If

    'End Sub

    'Protected Sub rptSubMenu_ItemCommand(source As Object, e As RepeaterCommandEventArgs)

    '    Dim uNmFail As String


    '    For Each i As RepeaterItem In (CType(source, Repeater)).Items

    '        Dim btn As LinkButton = CType(i.FindControl("LinkButton1"), LinkButton)
    '        btn.CssClass = "tablinks"
    '    Next

    '    If e.CommandName = "Pilih" Then
    '        Dim lnk As LinkButton = CType(e.Item.FindControl("LinkButton1"), LinkButton)
    '        Dim a As String = e.CommandArgument.ToString()
    '        'Dim kod As String = lnk.Attributes("data-id")
    '        lnk.CssClass = "tablinks active"

    '        dbconnNew = New SqlConnection(strCon)
    '        dbconnNew.Open()

    '        SQL = "Select Kod_Sub_Menu, Nama_Sub_Menu, isnull(Nama_Fail,'-')  Nama_Fail from SMKB_Sub_Menu where Kod_Sub_Menu ='" & a & "'"
    '        dbcomm = New SqlCommand(SQL, dbconnNew)
    '        dbread = dbcomm.ExecuteReader()
    '        dbread.Read()
    '        If dbread.HasRows Then
    '            uNmFail = dbread("Nama_Fail")
    '            Response.Redirect("~/" + uNmFail + "?kodsubmenu=" + dbread("Kod_Sub_Menu"))

    '        End If
    '        dbread.Close()


    '    End If
    'End Sub
End Class