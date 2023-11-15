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

Public Class Potongan
    Inherits System.Web.UI.Page
    Private sqlCon As SqlConnection
    Private strConn As String = "Data Source=devmis12.utem.edu.my;Initial Catalog=dbKewanganV4;Persist Security Info=True;User ID=smkb;Password=Smkb@Dev2012"
    Private strConnStaf As String = "Data Source=devmis11.utem.edu.my;Initial Catalog=dbStaf;Persist Security Info=True;User ID=smkb;Password=Smkb@Dev2012"
    Dim dbconn As New DBKewConn
    Dim dbconnstaf As New DBSMConn

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
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
                fBindDdlJenisPot()
                fBindDdlAgensi()
                ViewState("savemode") = 1


            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub BindListViewControls()

        Dim query As String = "Select * from SMKB_Gaji_Potongan order by kod"
        Dim da As SqlDataAdapter = New SqlDataAdapter(query, strConn)
        Dim table As DataTable = New DataTable()
        da.Fill(table)

        lvPotongan.DataSource = table
        lvPotongan.DataBind()
    End Sub
    Private Sub fBindDdlVotTetap(ddl As DropDownList)
        Dim strSql As String = "select kod_vot,kod_vot + ' - ' + butiran as butirans,butiran,Kod_klasifikasi from SMKB_vot where Kod_klasifikasi = 'D' order by kod_vot"
        Using dt = dbconn.fSelectCommandDt(strSql)
            ddl.DataSource = dt
            ddl.DataTextField = "butirans"
            ddl.DataValueField = "kod_vot"
            ddl.DataBind()

            ddl.Items.Insert(0, New ListItem("- SILA PILIH - ", "-"))
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

            ddl.Items.Insert(0, New ListItem("- SILA PILIH - ", "-"))
            ddl.SelectedIndex = 0

        End Using
    End Sub
    Private Sub fBindDdlAgensi()
        Dim strSql As String = "select kod_kerajaan,nama from SMKB_gaji_kerajaan order by kod_kerajaan"
        Using dt = dbconn.fSelectCommandDt(strSql)
            ddlAgensi.DataSource = dt
            ddlAgensi.DataTextField = "nama"
            ddlAgensi.DataValueField = "kod_kerajaan"
            ddlAgensi.DataBind()

            ddlAgensi.Items.Insert(0, New ListItem("- SILA PILIH - ", "-"))
            ddlAgensi.SelectedIndex = 0

            ddlAgensiE.DataSource = dt
            ddlAgensiE.DataTextField = "nama"
            ddlAgensiE.DataValueField = "kod_kerajaan"
            ddlAgensiE.DataBind()

            ddlAgensiE.Items.Insert(0, New ListItem("- SILA PILIH - ", "-"))
            ddlAgensiE.SelectedIndex = 0


        End Using
    End Sub
    Private Sub fBindDdlJenisPot()


        ddlJnsPot.Items.Insert(0, New ListItem("- SILA PILIH - ", "-"))
        ddlJnsPot.Items.Insert(1, New ListItem("KWSP Pekerja", "KP"))
        ddlJnsPot.Items.Insert(1, New ListItem("KWSP Majikan", "KM"))
        ddlJnsPot.Items.Insert(1, New ListItem("Perkeso Pekerja", "SP"))
        ddlJnsPot.Items.Insert(1, New ListItem("Perkeso Majikan", "SM"))
        ddlJnsPot.Items.Insert(1, New ListItem("Pencen", "PC"))
        ddlJnsPot.Items.Insert(1, New ListItem("Cukai", "TX"))
        ddlJnsPot.Items.Insert(1, New ListItem("Potongan", "PT"))
        ddlJnsPot.SelectedIndex = 0

        ddlJnsPotE.Items.Insert(0, New ListItem("- SILA PILIH - ", "-"))
        ddlJnsPotE.Items.Insert(1, New ListItem("KWSP Pekerja", "KP"))
        ddlJnsPotE.Items.Insert(1, New ListItem("KWSP Majikan", "KM"))
        ddlJnsPotE.Items.Insert(1, New ListItem("Perkeso Pekerja", "SP"))
        ddlJnsPotE.Items.Insert(1, New ListItem("Perkeso Majikan", "SM"))
        ddlJnsPotE.Items.Insert(1, New ListItem("Pencen", "PC"))
        ddlJnsPotE.Items.Insert(1, New ListItem("Cukai", "TX"))
        ddlJnsPotE.Items.Insert(1, New ListItem("Potongan", "PT"))
        ddlJnsPotE.SelectedIndex = 0
    End Sub
    Protected Sub lvPotongan_ItemDataBound(sender As Object, e As ListViewItemEventArgs)

    End Sub
    Private Function fCheckMaster(ByVal strKod As String) As Boolean
        Dim strSql = "select count(*) from smkb_gaji_master  where Kod_trans = '" & strKod & "'"
        Dim intCnt As Integer = dbconn.fSelectCount(strSql)
        If intCnt < 1 Then
            Return False
        Else
            Return True
        End If

    End Function
    Private Function fCheckKodPot(ByVal strKod As String) As Boolean
        Dim strSql = "select count(*) from SMKB_Gaji_Potongan  where Kod = '" & strKod & "'"
        Dim intCnt As Integer = dbconn.fSelectCount(strSql)
        If intCnt < 1 Then
            Return False
        Else
            Return True
        End If

    End Function
    Private Function fInsertPot() As Boolean
        Try
            Dim strSql As String
            Dim strKodPot As String = Trim(txtKodPot.Text.TrimEnd)
            Dim strNmPot As String = Trim(txtNmPot.Text.ToUpper.TrimEnd)
            Dim strVotTetap As String = ddlVotTetap.SelectedValue
            Dim strVotXTetap As String = ddlVotBknTetap.SelectedValue
            Dim strKiraKwsp As Byte
            Dim strKiraPerkeso As Byte
            Dim strKiraCukai As Byte
            Dim strKiraPencen As Byte
            Dim strKiraAP As Byte
            Dim strJenis As String = ddlJnsPot.SelectedValue
            Dim strGov As String = ddlAgensi.SelectedValue

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
            If rbAP.SelectedValue = "1" Then
                strKiraAP = True
            Else
                strKiraAP = False
            End If

            strSql = "insert into SMKB_Gaji_Potongan (kod, Butiran, Vot_Tetap, Vot_Bukan_Tetap, Kira_Kwsp, Kira_Perkeso, Kira_Cukai, Kira_Pencen, Masuk_AP, Jenis, Kod_Kerajaan) values (@KodPot,@NamaPot,@VotTetap,@VotBknTetap, @KiraKwsp, @KiraPerkeso, @KiraCukai, @KiraPencen,@KiraAP,@Jenis,@KodGov)"

            Dim paramSql() As SqlParameter = {
                     New SqlParameter("@KodPot", strKodPot),
                    New SqlParameter("@NamaPot", strNmPot),
                    New SqlParameter("@VotTetap", strVotTetap),
                    New SqlParameter("@VotBknTetap", strVotXTetap),
                   New SqlParameter("@KiraKwsp", strKiraKwsp),
                    New SqlParameter("@KiraPerkeso", strKiraPerkeso),
                     New SqlParameter("@KiraCukai", strKiraCukai),
                     New SqlParameter("@KiraPencen", strKiraPencen),
                     New SqlParameter("@KiraAP", strKiraAP),
                     New SqlParameter("@Jenis", strJenis),
                     New SqlParameter("@KodGov", strGov)
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
    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs)
        Dim btn As LinkButton = sender
        Dim kod = btn.Attributes("data-id")
        ' MsgBox(btn.Attributes("data-id"))
        Me.txtKodE.Text = kod
        Dim str3 = $"Select * from SMKB_Gaji_Potongan  WHERE kod = '{kod}' order by kod"
        Using dt2 = dbconn.fSelectCommandDt(str3)
            Me.txtNamaE.Text = dt2.Rows(0)("butiran")
            Me.ddlJnsPotE.Text = dt2.Rows(0)("jenis")
            If IsDBNull(dt2.Rows(0)("vot_tetap")) = False Then
                Me.ddlVotE.SelectedValue = dt2.Rows(0)("vot_tetap")
            Else
                Me.ddlVotE.SelectedValue = "-"
            End If
            If IsDBNull(dt2.Rows(0)("vot_bukan_tetap")) = False Then
                Me.ddlVotxE.SelectedValue = dt2.Rows(0)("vot_bukan_tetap")
            Else
                Me.ddlVotxE.SelectedValue = "-"
            End If
            If IsDBNull(dt2.Rows(0)("kod_kerajaan")) = False Then
                Me.ddlAgensiE.SelectedValue = dt2.Rows(0)("kod_kerajaan")
            Else
                Me.ddlAgensiE.SelectedValue = "-"
            End If

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
            If dt2.Rows(0)("Masuk_AP") = True Then
                rbAPU.SelectedValue = "1"
            Else
                rbAPU.SelectedValue = "2"
            End If
        End Using
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "Pop", "openModal();", True)

    End Sub
    Private Sub Alert(msg As String)
        Dim sb As New StringBuilder()
        sb.Append("alert('")
        sb.Append(msg.Replace(vbLf, "\n").Replace(vbCr, "").Replace("'", "\'"))
        sb.Append("');")
        ScriptManager.RegisterStartupScript(Me.Page, Me.[GetType](), "showalert", sb.ToString(), True)
    End Sub
    Protected Sub lvPotongan_ItemDeleting(sender As Object, e As ListViewDeleteEventArgs)
        Dim strSql As String
        Dim strKodElaun As String = Trim(txtKodPot.Text.ToString.TrimEnd)
        Dim item As ListViewDataItem = lvPotongan.Items(e.ItemIndex)
        Dim hfKod As HiddenField = CType(item.FindControl("hfKod"), HiddenField)

        ViewState("SaveMode") = "3"

        If fCheckMaster(hfKod.Value) = False Then
            strSql = "delete from smkb_gaji_potongan where Kod = '" & hfKod.Value & "'"
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
            Alert("Rekod tidak dapat dipadam! Terdapat rekod potongan ini dalam transaksi gaji.")
        End If
    End Sub

    Protected Sub lbtnSimpan_Click(sender As Object, e As EventArgs)
        Dim strKod As String = Trim(txtKodPot.Text.TrimEnd)
        Dim strButir As String = Trim(txtNmPot.Text.TrimEnd)

        ViewState("SaveMode") = "1"

        If fCheckKodPot(strKod) = False Then
            If fInsertPot() = True Then
                Alert("Rekod baru telah ditambah!")
                BindListViewControls()

                'fResetElaun()
            Else
                Alert("Rekod baru gagal ditambah!")
            End If
        Else
            Alert("Kod potongan yang dimasukkan telah wujud! Sila masukkan Kod potongan lain.")
        End If
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "alert", "closeModal();", True)
    End Sub

    Protected Sub lbtnUpdate_Click(sender As Object, e As EventArgs)
        Dim strSql As String
        Dim strKodElaun As String = Trim(txtKodE.Text.TrimEnd)
        Dim strNamaElaun As String = Trim(txtNamaE.Text.ToUpper.TrimEnd)
        Dim strVotTetap As String = ddlVotE.SelectedValue
        Dim strVotXTetap As String = ddlVotxE.SelectedValue
        Dim strKiraKwsp As Byte
        Dim strKiraPerkeso As Byte
        Dim strKiraCukai As Byte
        Dim strKiraPencen As Byte
        Dim bKiraAP As Byte
        Dim strJenis As String = ddlJnsPotE.SelectedValue
        Dim strGov As String = ddlAgensiE.SelectedValue

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

        If rbAPU.SelectedValue = "1" Then
            bKiraAP = 0
        Else
            bKiraAP = 1
        End If

        strSql = "UPDATE SMKB_Gaji_Potongan SET Butiran=@strNamaElaun, Vot_Tetap=@VotTetap, Vot_Bukan_Tetap=@VotBknTetap, Kira_Kwsp=@KiraKwsp,
            Kira_Perkeso=@KiraPerkeso, Kira_Cukai=@KiraCukai, Kira_Pencen=@KiraPencen, Masuk_AP=@KiraAP, Jenis=@Jenis, Kod_Kerajaan=@KodGov WHERE kod = @KodElaun"

        Dim paramSql() As SqlParameter = {
                    New SqlParameter("@strNamaElaun", strNamaElaun),
                    New SqlParameter("@VotTetap", strVotTetap),
                    New SqlParameter("@VotBknTetap", strVotXTetap),
                   New SqlParameter("@KiraKwsp", strKiraKwsp),
                    New SqlParameter("@KiraPerkeso", strKiraPerkeso),
                     New SqlParameter("@KiraCukai", strKiraCukai),
                     New SqlParameter("@KiraPencen", strKiraPencen),
                     New SqlParameter("@KiraAP", bKiraAP),
                     New SqlParameter("@Jenis", strJenis),
                     New SqlParameter("@KodGov", strGov),
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

    Protected Sub lvPotongan_PagePropertiesChanging(sender As Object, e As PagePropertiesChangingEventArgs)

        TryCast(lvPotongan.FindControl("DataPager1"), DataPager).SetPageProperties(e.StartRowIndex, e.MaximumRows, False)
        'lvElaun.DataBind()
        BindListViewControls()
    End Sub

    Protected Sub TextBox1_TextChanged(sender As Object, e As EventArgs)
        Dim query As String = "Select * from SMKB_Gaji_Potongan where kod like '%" & TextBox1.Text & "%' Or butiran Like '%" & TextBox1.Text & "%'  order by kod"
        Dim da As SqlDataAdapter = New SqlDataAdapter(query, strConn)
        Dim table As DataTable = New DataTable()

        da.Fill(table)
        lvPotongan.DataSource = table
        lvPotongan.DataBind()
    End Sub
End Class