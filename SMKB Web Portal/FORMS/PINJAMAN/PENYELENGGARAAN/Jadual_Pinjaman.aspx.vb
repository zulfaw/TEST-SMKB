Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports System.Globalization

Public Class Jadual_Pinjaman
    Inherits System.Web.UI.Page
    Dim dbconn As New DBKewConn

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                fBindddlKatPinj()
                ddlKatPinj.Items.Insert(0, New ListItem("-SILA PILIH-", String.Empty))
                ddlKatPinj.SelectedIndex = 0

                lbtnKemaskini.Visible = False
                lbtnUndo.Visible = False
                fBindgvJadual()
            End If
        Catch ex As Exception
            'fErrorLog("Pendaftaran_Menu.aspx(Page_Load())- " & ex.Message.ToString)
        End Try
    End Sub

    Private Sub fBindddlKatPinj()
        'select all except Modul Pentadbiran Sistem
        Dim strSql As String = "select kod, Butiran from PJM_KatPinj order by Kod"

        Using dt = dbconn.fSelectCommandDt(strSql)
            ddlKatPinj.DataSource = dt
            ddlKatPinj.DataTextField = "Butiran"
            ddlKatPinj.DataValueField = "Kod"
            ddlKatPinj.DataBind()
        End Using
    End Sub

    Private Sub fBindgvJadual()

        Dim strSql As String = "select KategoriPinj, tempoh, KadarTempoh, Amaun, Ansuran from PJM_Jadual"

        Dim dt = fCreateDt(strSql)

        If dt.Rows.Count > 0 Then
            gvJadual.DataSource = dt
            gvJadual.DataBind()
        Else
            fGlobalAlert("Tiada Rekod Dijumpai", Me.Page, Me.GetType())
        End If
    End Sub

    Private Function fCreateDt(str As String) As DataTable


        Using dt = dbconn.fSelectCommandDt(str)
            Return dt
        End Using

    End Function

    Protected Sub lbtnSimpan_Click(sender As Object, e As EventArgs) Handles lbtnSimpan.Click
        'Try
        '    Page.Validate()
        '    If Page.IsValid Then
        '        Dim kodSubmenu = ddlKodSubMenu.SelectedValue

        '        Dim strSql = $"select count(*) from MK_Kawalan where KodSubMenu = '{kodSubmenu}'"
        '        dbconn = New DBKewConn

        '        If dbconn.fSelectCount(strSql) > 0 Then
        '            fGlobalAlert("Rekod sudah ada!. Tidak boleh buat penambahan rekod yang sama", Me.Page, Me.[GetType]())
        '        Else

        '            strSql = "INSERT INTO MK_Kawalan VALUES (@KodSubMenus, @TrkMulaa, @TrkHinggaa, @Perkaraa)"

        '            Dim tarikhMula As Date = Date.ParseExact(txtTarikhMula.Text, "dd/MM/yyyy", New CultureInfo("en-US"))
        '            Dim tarikhHingga As Date = Date.ParseExact(txtTarikhHingga.Text, "dd/MM/yyyy", New CultureInfo("en-US"))

        '            Dim paramSql() As SqlParameter = {
        '                New SqlParameter("@KodSubMenus", kodSubmenu),
        '                New SqlParameter("@TrkMulaa", tarikhMula),
        '                New SqlParameter("@TrkHinggaa", tarikhHingga),
        '                New SqlParameter("@Perkaraa", txtPerkara.Text)
        '            }

        '            If dbconn.fInsertCommand(strSql, paramSql) > 0 Then
        '                fGlobalAlert("Rekod baru telah disimpan!", Me.Page, Me.[GetType]())
        '                lbtnReset_Click(sender, e)
        '                fBindgvJadual()
        '            Else
        '                fGlobalAlert("Rekod tidak berjaya disimpan!", Me.Page, Me.[GetType]())
        '            End If
        '        End If
        '    End If
        'Catch ex As Exception
        '    'fErrorLog("Jenis_Permohonan.aspx(btnSave_Click)- " & ex.Message.ToString)
        'End Try
    End Sub

    Protected Sub lbtnReset_Click(sender As Object, e As EventArgs) Handles lbtnReset.Click


        fBindddlKatPinj()
        ddlKatPinj.Items.Insert(0, New ListItem("-SILA PILIH-", String.Empty))
        ddlKatPinj.SelectedIndex = 0


    End Sub


    Protected Sub gvJadual_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvJadual.SelectedIndexChanged
        For Each row As GridViewRow In gvJadual.Rows
            If row.RowIndex = gvJadual.SelectedIndex Then

                row.ForeColor = ColorTranslator.FromHtml("#0000FF")
                lbtnUndo.Visible = True
                lbtnKemaskini.Visible = True
                lbtnSimpan.Visible = False

                Dim KodSubmenu As String = CType(row.FindControl("lblKodSubmenu"), Label).Text
                Dim Perkara As String = CType((row.FindControl("lblPerkara")), Label).Text
                Dim TarikhMula As String = CType((row.FindControl("lblTarikhMula")), Label).Text
                Dim TarikhHingga As String = CType((row.FindControl("lblTarikhHingga")), Label).Text


                Dim KodSubModul = KodSubmenu.Substring(0, 4)
                Dim KodModul = KodSubmenu.Substring(0, 2)

                fBindddlKatPinj()
                ddlKatPinj.Items.FindByValue(KodModul).Selected = True

            Else
                row.ForeColor = ColorTranslator.FromHtml("#000000")
            End If
        Next
    End Sub


    Protected Sub gvJadual_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvJadual.RowDeleting
        Try
            Dim row As GridViewRow = DirectCast(gvJadual.Rows(e.RowIndex), GridViewRow)
            Dim KodSubmenu = CType(row.FindControl("lblKodSubmenu"), Label).Text 'Trim(row.Cells(1).Text.ToString.TrimEnd)
            Dim dbconn As New DBKewConn

            If KodSubmenu IsNot String.Empty Then
                Dim strSql = $"delete from MK_Kawalan where KodSubMenu='{KodSubmenu}'"
                If dbconn.fUpdateCommand(strSql) > 0 Then
                    fGlobalAlert("Rekod Butiran telah dipadam!", Me.Page, Me.[GetType]())
                End If
            End If

            fBindgvJadual()

        Catch ex As Exception

        End Try
    End Sub


    Protected Sub lbtnUndo_Click(sender As Object, e As EventArgs) Handles lbtnUndo.Click
        lbtnSimpan.Visible = True
        lbtnKemaskini.Visible = False
        lbtnUndo.Visible = False

        lbtnReset_Click(sender, e)
    End Sub

    Protected Sub lbtnKemaskini_Click(sender As Object, e As EventArgs) Handles lbtnKemaskini.Click
        'Try
        '    Page.Validate()
        '    If Page.IsValid Then
        '        Dim kodSubmenu = ddlKodSubMenu.SelectedValue

        '        Dim dbconn As New DBKewConn
        '        '"select a.KodSubMenu, b.NamaSubMenu, a.Perkara, a.TkhMula, a.TkhHingga From MK_Kawalan a INNER Join MK_USubMenu b On a.KodSubMenu = b.KodSubMenu order by a.KodSubMenu"

        '        Dim strSql = "Update MK_Kawalan SET TkhMula=@TrkMulaa, TkhHingga=@TrkHinggaa, Perkara=@Perkaraa WHERE KodSubMenu= @KodSubMenus"

        '        Dim tarikhMula As Date = Date.ParseExact(txtTarikhMula.Text, "dd/MM/yyyy", New CultureInfo("en-US"))
        '        Dim tarikhHingga As Date = Date.ParseExact(txtTarikhHingga.Text, "dd/MM/yyyy", New CultureInfo("en-US"))
        '        'txtTarikh.Text = Date.Today.ToString("dd/MM/yyyy")

        '        Dim paramSql() As SqlParameter = {
        '                New SqlParameter("@KodSubMenus", kodSubmenu),
        '                New SqlParameter("@TrkMulaa", tarikhMula),
        '                New SqlParameter("@TrkHinggaa", tarikhHingga),
        '                New SqlParameter("@Perkaraa", txtPerkara.Text)
        '            }

        '        If dbconn.fInsertCommand(strSql, paramSql) > 0 Then
        '            fGlobalAlert("Rekod baru telah dikemaskini!", Me.Page, Me.[GetType]())
        '            lbtnReset_Click(sender, e)
        '            fBindgvJadual()
        '            lbtnUndo.Visible = False
        '            lbtnKemaskini.Visible = False
        '            lbtnSimpan.Visible = True
        '        Else
        '            fGlobalAlert("Rekod tidak berjaya dikemaskini!", Me.Page, Me.[GetType]())
        '        End If
        '    End If
        'Catch ex As Exception
        '    'fErrorLog("Jenis_Permohonan.aspx(btnSave_Click)- " & ex.Message.ToString)
        'End Try
    End Sub

End Class