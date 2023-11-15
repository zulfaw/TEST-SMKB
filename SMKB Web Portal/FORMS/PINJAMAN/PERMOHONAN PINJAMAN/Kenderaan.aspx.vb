Imports System.Data.SqlClient
Imports System.Globalization

'''<summary>
'''Nama Developer: Muhammad Hazrin Bin Othman
'''Tarikh Develop: 26/06/2018
'''Tujuan: Pemohonan Perolehan oleh pemohon
'''</summary>
Public Class Kenderaan
    Inherits System.Web.UI.Page

    Dim dbconn As New DBKewConn

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                fBindDdlStatus()
                fBindgvMohonPinj("0")
                txtTahun.Text = Date.Now.Year.ToString
            End If
        Catch ex As Exception

        End Try
    End Sub

    ''' <summary>
    ''' Bind data BG_StatusDokBaru and load to dropdownlist
    ''' </summary>
    Private Sub fBindDdlStatus()

        Dim strSql As String = "select kod, Butiran from PJM_StatusDok"

        Dim ds As New DataSet
        Dim dbconn As New DBKewConn
        Using dt = dbconn.fSelectCommandDt(strSql)

            ddlStatus.DataSource = dt
            ddlStatus.DataTextField = "Butiran"
            ddlStatus.DataValueField = "Kod"
            ddlStatus.DataBind()

            ddlStatus.Items.Insert(0, New ListItem("-SILA PILIH-", "0"))
            ddlStatus.Items.Insert(1, New ListItem("-KESELURUHAN-", "1"))
            ddlStatus.SelectedIndex = 0
        End Using

    End Sub

    Private Sub fBindgvMohonPinj(KodStatus As String)
        Try
            Dim strSql As String
            '            If KodStatus = "0" Then
            '                strSql = $"select b.PO01_NoMohonSem,b.PO01_NoMohon, PO01_TkhMohonSem, PO01_Tujuan, (Select SUM(PO01_JumKadar) From PO01_PPembelianDt a Where a.PO01_NoMohonSem = b.PO01_NoMohonSem) AS AnggaranBelanja, 
            'PO01_JenisBrg, PO01_StatusPP, c.Butiran 
            'from PO01_PPembelian b, PO_StatusPP c 
            'WHERE PO01_Status='A' AND c.Kod = b.PO01_StatusPP AND (PO01_StatusPP = '001' OR PO01_StatusPP = '002' OR PO01_StatusPP = '011' OR PO01_StatusPP = '023')
            'AND YEAR(PO01_TkhMohonSem)={Now.Year} AND PO01_IdPemohon='{Session("ssusrID")}' Order BY PO01_TkhMohonSem"
            '            ElseIf KodStatus = "1" Then
            '                strSql = $"select b.PO01_NoMohonSem,b.PO01_NoMohon, PO01_TkhMohonSem, PO01_Tujuan, (Select SUM(PO01_JumKadar) From PO01_PPembelianDt a Where a.PO01_NoMohonSem = b.PO01_NoMohonSem) AS AnggaranBelanja, 
            'PO01_JenisBrg, PO01_StatusPP, c.Butiran 
            'from PO01_PPembelian b, PO_StatusPP c 
            'WHERE c.Kod = b.PO01_StatusPP
            'AND YEAR(PO01_TkhMohonSem)={Now.Year} AND PO01_IdPemohon='{Session("ssusrID")}' Order BY PO01_TkhMohonSem"
            '            Else
            strSql = "SELECT a.PJM01_TkhMohon, a.PJM01_NoPinj, a.PJM01_NoStaf, c.Butiran as JenPinj, b.Butiran as StatusDok, a.PJM01_StatusDok, a.PJM01_Amaun
                        FROM PJM01_Daftar AS a, PJM_StatusDok AS b, PJM_JenisPinj as c
                        WHERE 1 = 1
                        AND a.PJM01_StatusDok = b.Kod
                        AND a.PJM01_KatPinj = 'K00001'
                        AND a.PJM01_Status = 'A'
                        AND a.PJM01_JenisPinj NOT IN ('J00005')
                        AND a.PJM01_JenisPinj = c.Kod
                        AND a.PJM01_NoStaf = '01613'"
            'End If
            'Dim dt as datatable 

            Using dt = dbconn.fSelectCommandDt(strSql)

                'End Using
                'Using dt = fCreateDt(strSql)
                'Dim total As Decimal = dt.AsEnumerable().Sum(Function(row) row.Field(Of Decimal?)("AnggaranBelanja"))
                'ViewState("TotalAmount") = total

                gvMohonPinj.DataSource = dt
                gvMohonPinj.DataBind()
            End Using
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvMohonPinj_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvMohonPinj.PageIndexChanging
        gvMohonPinj.PageIndex = e.NewPageIndex
        Dim kod = ddlStatus.SelectedValue
        fBindgvMohonPinj(kod)
    End Sub

    'Private Function fCreateDt(strSql As String) As DataTable

    '    'pass as reference 
    '    Dim dt As New DataTable
    '    Using dt = dbconn.fSelectCommandDt(strSql)
    '        Return dt
    '    End Using
    'End Function



    Protected Sub ddlStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStatus.SelectedIndexChanged
        Dim kod = ddlStatus.SelectedValue
        fBindgvMohonPinj(kod)

        'If kod.Equals("001") Then
        '    btnHantar.Visible = True
        'Else
        '    btnHantar.Visible = False
        'End If
    End Sub

    Protected Sub btnMohonBaru_Click(sender As Object, e As EventArgs) Handles btnMohonBaru.Click
        Dim KodSub = Request.QueryString("KodSub")
        Dim KodSubMenu = Request.QueryString("KodSubMenu")
        Response.Redirect($"~/FORMS/Pinjaman/Permohonan Pinjaman/frm_PinjKend.aspx?KodSub={KodSub}&KodSubMenu={KodSubMenu}&no=0")
    End Sub

    'Protected Sub btnHantar_Click(sender As Object, e As EventArgs) Handles btnHantar.Click
    '    Dim chkAll As CheckBox = DirectCast(gvMohonPinj.HeaderRow.Cells(0).FindControl("checkAll"), CheckBox)
    '    Dim noMohon As String = ""
    '    Dim tujuan As String = ""
    '    Dim tarikh As String = ""
    '    Dim Kategori As String = ""
    '    Dim AngJumlah As String = ""
    '    Dim status As String = ""
    '    Dim listPermohonan As New List(Of MohonPO)

    '    Dim rows = gvMohonPinj.Rows
    '    For i As Integer = 0 To rows.Count - 1
    '        noMohon = gvMohonPinj.DataKeys(i).Value
    '        'CType(rows(i).FindControl("lblNoPoSem"), Label).Text 'rows(i).Cells(2).Text.ToString()
    '        tujuan = rows(i).Cells(3).Text.ToString()
    '        tarikh = rows(i).Cells(5).Text.ToString()
    '        Kategori = rows(i).Cells(4).Text.ToString()
    '        AngJumlah = rows(i).Cells(6).Text.ToString()
    '        status = rows(i).Cells(7).Text.ToString()

    '        If status.StartsWith("PERMOHONAN") Then
    '            If chkAll.Checked Then
    '                'Add all rows
    '                listPermohonan.Add(
    '                    New MohonPO() With {
    '                        .NoMohon = noMohon,
    '                        .Program = tujuan,
    '                        .Tarikh = tarikh,
    '                        .Kategori = Kategori,
    '                        .AngJumlah = AngJumlah,
    '                        .Status = status
    '                    })
    '            Else
    '                Dim chk As CheckBox = DirectCast(rows(i).Cells(0).FindControl("CheckBox1"), CheckBox)

    '                If chk.Checked Then
    '                    listPermohonan.Add(New MohonPO() With {
    '                        .NoMohon = noMohon,
    '                        .Program = tujuan,
    '                        .Tarikh = tarikh,
    '                        .Kategori = Kategori,
    '                        .AngJumlah = AngJumlah,
    '                        .Status = status
    '                    })
    '                End If
    '            End If
    '        End If
    '    Next

    '    If listPermohonan.Count > 0 Then
    '        gvHantar.DataSource = listPermohonan
    '        gvHantar.DataBind()

    '        Me.ModalPopupExtender3.Show()
    '        Session("ListPermohonan") = listPermohonan
    '    Else
    '        fGlobalAlert("Sila pilih sekurangnya satu rekod berstatus 'Permohonan Perolehan'", Me.Page, Me.[GetType]())
    '    End If
    'End Sub

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

    'Protected Sub lbYes_Click(sender As Object, e As EventArgs) Handles lbYes.Click
    '    Dim ListMohon = TryCast(Session("ListPermohonan"), List(Of MohonPO))

    '    If ListMohon.Count > 0 Then
    '        Dim strSql = ""
    '        Dim strSql2 = ""
    '        Dim noMohonSem = ""
    '        Dim StatusHantar = "003" '"05"
    '        Dim paramSql() As SqlParameter
    '        Dim tarikh = DateTime.Now 'ParseExact(Date.Today, "dd/MM/yyyy", New CultureInfo("en-US"))

    '        For Each item In ListMohon
    '            noMohonSem = item.NoMohon
    '            strSql = strSql + $"UPDATE PO01_PPembelian SET PO01_StatusPP = @StatusPP WHERE PO01_NoMohonSem='{noMohonSem}';"
    '            strSql2 = $"INSERT INTO PO10_StatusDok VALUES ('{noMohonSem}','-','-',@StatusPP,@noStaff,@tarikh,@ulasan);"
    '        Next
    '        paramSql = {
    '            New SqlParameter("@StatusPP", StatusHantar),
    '            New SqlParameter("@noStaff", Session("ssusrID")),
    '            New SqlParameter("@tarikh", tarikh),
    '            New SqlParameter("@ulasan", "PERMOHONAN PEMBELIAN")
    '            }
    '        dbconn.sUpdateCommand(strSql + strSql2, paramSql)
    '        Me.ModalPopupExtender3.Hide()
    '        Dim KodSub = Request.QueryString("KodSub")
    '        Dim KodSubMenu = Request.QueryString("KodSubMenu")
    '        fGlobalAlert($"Semua rekod telah dihantar!", Me.Page, Me.[GetType](), $"../../Perolehan/Permohonan Perolehan/Permohonan_Perolehan.aspx?KodSub={KodSub}&KodSubMenu={KodSubMenu}")
    '    End If
    '    Session.Remove("ListPermohonan")
    'End Sub

    Protected Sub gvMohonPinj_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvMohonPinj.RowCreated

        If e.Row.RowType = DataControlRowType.Footer Then

            'First cell Is used for specifying the Total text
            Dim intNoOfMergeCol = e.Row.Cells.Count - 3 ' /*except last column */
            For intCellCol As Integer = 1 To intNoOfMergeCol - 1
                e.Row.Cells.RemoveAt(1)
            Next

            e.Row.Cells(0).ColumnSpan = intNoOfMergeCol
            e.Row.Cells(0).Text = "Jumlah Besar (RM)"
            e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(0).Font.Bold = True

            Dim total As Decimal = ViewState("TotalAmount")
            e.Row.Cells(1).Text = total.ToString("#,##0.00")
            e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(1).Font.Bold = True
        End If

    End Sub

    Protected Sub gvMohonPinj_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvMohonPinj.Sorting

    End Sub

    'Protected Sub gvMohonPinj_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvMohonPinj.RowCommand
    '    ' CommandName property to determine which button was clicked.
    '    If e.CommandName = "Select" Then

    '        ' Convert the row index stored in the CommandArgument
    '        ' property to an Integer.
    '        'Dim index As Integer = Convert.ToInt32(e.CommandArgument)
    '        Dim KodSubMenu = Request.QueryString("KodSubMenu")

    '        ' Get the last name of the selected author from the appropriate
    '        ' cell in the GridView control.
    '        'Dim selectedRow As GridViewRow = gvMohonPinj.Rows(index)
    '        Dim NoMohonSem As String = gvMohonPinj.SelectedDataKey.Value.ToString 'CType(selectedRow.FindControl("lblNoPoSem"), Label).Text 'selectedRow.Cells(2).Text


    '        ' Display the selected author.
    '        'Message.Text = "You selected " & contact & "."

    '        'Open other page.
    '        Response.Redirect($"~/FORMS/Perolehan/Permohonan Perolehan/MaklumatPermohonanPO.aspx?KodSubMenu={KodSubMenu}&no={NoMohonSem}")
    '    End If
    'End Sub

    Protected Sub gvMohonPinj_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvMohonPinj.SelectedIndexChanged
        Dim KodSub = Request.QueryString("KodSub")
        Dim KodSubMenu = Request.QueryString("KodSubMenu")
        Dim NoMohonSem As String = gvMohonPinj.SelectedDataKey.Value.ToString 'CType(selectedRow.FindControl("lblNoPoSem"), Label).Text 'selectedRow.Cells(2).Text

        'Open other page.
        Response.Redirect($"~/FORMS/Perolehan/Permohonan Perolehan/MaklumatPermohonanPO.aspx?KodSub={KodSub}&KodSubMenu={KodSubMenu}&no={NoMohonSem}")
    End Sub
End Class