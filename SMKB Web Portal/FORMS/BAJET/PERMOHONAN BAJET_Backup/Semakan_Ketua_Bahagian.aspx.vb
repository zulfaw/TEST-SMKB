Imports System.Drawing
Imports System.Data.SqlClient
Imports System.Globalization
Public Class Semakan_Ketua_Bahagian

    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            'Dim KodPTJ = Session("ssusrKodPTj")
            If Not IsPostBack Then
                'btnSimpan.Text = "Simpan"
                fBindDdlStatus()
                fBindDdlUnitPTj()
                fBindGVSemakanBajet("02", "")

            End If
        Catch ex As Exception

        End Try
    End Sub




    ''' <summary>
    ''' Bind data BG_StatusDokBaru and load to dropdownlist
    ''' </summary>
    Private Sub fBindDdlStatus()
        Try
            Dim strSql As String = "Select BG_KodStatus, BG_Butiran from BG_StatusDok where BG_ProsesID=1 ORDER BY BG_KodStatus ASC"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fselectCommand(strSql)

            ddlStatus.DataSource = ds
            ddlStatus.DataTextField = "BG_Butiran"
            ddlStatus.DataValueField = "BG_KodStatus"
            ddlStatus.DataBind()

            ddlStatus.Items.Insert(0, New ListItem("-KESELURUHAN-", ""))

            'select Proses Ketua Bahagian
            ddlStatus.SelectedIndex = 2
        Catch ex As Exception
            'fErrorLog("Peruntukan_KW.aspx(fBindDdlKW)- " & ex.Message.ToString)
        End Try
    End Sub

    Private Sub fBindDdlUnitPTj()
        Try
            Dim strSql As String = $"Select KodUnit, NamaUnit from MK_UnitPTJ where Status=1 AND KodBah='{Session("ssusrKodBahagian")}' ORDER BY KodUnit ASC"

            Dim dbconn As New DBKewConn
            Dim ds As New DataSet
            ds = dbconn.fselectCommand(strSql)

            ddlUnit.DataSource = ds
            ddlUnit.DataTextField = "NamaUnit"
            ddlUnit.DataValueField = "KodUnit"
            ddlUnit.DataBind()

            ddlUnit.Items.Insert(0, New ListItem("-KESELURUHAN-", ""))
            ddlUnit.SelectedIndex = 0
        Catch ex As Exception
            'fErrorLog("Peruntukan_KW.aspx(fBindDdlKW)- " & ex.Message.ToString)
        End Try
    End Sub


    Protected Sub ddlStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStatus.SelectedIndexChanged
        fBindGVSemakanBajet(ddlStatus.SelectedValue, ddlUnit.SelectedValue)
    End Sub

    Protected Sub ddlUnit_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlUnit.SelectedIndexChanged
        fBindGVSemakanBajet(ddlStatus.SelectedValue, ddlUnit.SelectedValue)
    End Sub


    Private Sub fBindGVSemakanBajet(KodStatus As String, KodUnit As String)
        Try
            Dim strSql As String = ""
            If KodStatus Is String.Empty And KodUnit Is String.Empty Then
                strSql = $"select BG20_NoMohon, BG20_TarikhMohon, BG20_Program, BG20_AmaunMohon, StatusDok from BG20_Mohon WHERE KodBahagian='{Session("ssusrKodBahagian")}'"
            ElseIf KodStatus IsNot String.Empty And KodUnit Is String.Empty Then
                strSql = $"select BG20_NoMohon, BG20_TarikhMohon, BG20_Program, BG20_AmaunMohon, StatusDok from BG20_Mohon WHERE StatusDok='{KodStatus}' AND KodBahagian='{Session("ssusrKodBahagian")}'"
            ElseIf KodStatus Is String.Empty And KodUnit IsNot String.Empty Then
                strSql = $"select BG20_NoMohon, BG20_TarikhMohon, BG20_Program, BG20_AmaunMohon, StatusDok from BG20_Mohon WHERE KodUnitPtj='{KodUnit}'"
            ElseIf KodStatus IsNot String.Empty And KodUnit IsNot String.Empty Then
                strSql = $"select BG20_NoMohon, BG20_TarikhMohon, BG20_Program, BG20_AmaunMohon, StatusDok from BG20_Mohon WHERE StatusDok='{KodStatus}' AND KodUnitPtj='{KodUnit}'"

            End If

            Dim dt = fCreateDt(strSql)
            Dim total As Decimal = dt.AsEnumerable().Sum(Function(row) row.Field(Of Decimal)("BG20_AmaunMohon"))
            ViewState("TotalAmount") = total

            gvSemakanBajet.DataSource = dt
            gvSemakanBajet.DataBind()

        Catch ex As Exception

        End Try
    End Sub


    Private Function fCreateDt(strSql As String) As DataTable
        Dim dbconn As New DBKewConn

        'pass as reference 
        Dim ds = dbconn.fselectCommand(strSql)

        'Convert dataset to datatable
        Dim dt = ds.Tables(0)

        If dt.Rows.Count > 0 Then
            Dim strDate As DateTime = Nothing
            dt.Columns.Add("TarikhMohon", GetType(String))
            dt.Columns.Add("Status", GetType(String))
            dt.Columns.Add("AngJumBesar", GetType(Decimal))

            For Each row As DataRow In dt.Rows
                strDate = row("BG20_TarikhMohon").ToString
                row("TarikhMohon") = strDate.ToString("dd/MM/yyyy")
                Dim DicStatusDok As Dictionary(Of String, String) = fGetStatusDok(row("StatusDok").ToString)
                Dim status = DicStatusDok.FirstOrDefault.Value
                row("Status") = fGetStatusDok(row("StatusDok").ToString).FirstOrDefault.Value
                row("AngJumBesar") = CDec(row("BG20_AmaunMohon")) '.ToString("#,##0.00")
                'fGetJumlahBesarAnggaran(row("BG20_NoMohon").ToString)
            Next
        End If

        Return dt
    End Function


    Private Function fGetJumlahBesarAnggaran(NoPermohonanBajet As String)
        Dim strSql As String = $"Select BG20_AngJumlah from BG20_MohonDt where BG20_NoMohon='{NoPermohonanBajet}' and BG20_Status='1'"

        Dim ds As New DataSet
        Dim dbconn As New DBKewConn
        ds = dbconn.fselectCommand(strSql)

        'Convert dataset to datatable
        Dim dt = ds.Tables(0)

        Dim Anggaran As Decimal = 0.0
        If dt.Rows.Count > 0 Then
            For Each row As DataRow In dt.Rows
                Anggaran += row("BG20_AngJumlah")
            Next
        End If
        Return Anggaran.ToString("#,##0.00")

    End Function



    Protected Sub gvMohonBajet_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvSemakanBajet.RowDataBound
        Dim intLog As Integer

        Try

            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim Status As String = DataBinder.Eval(e.Row.DataItem, "Status").ToString()

                If Status.Equals("PERMOHONAN BARU") Then
                    Dim btnDetail = CType(e.Row.FindControl("lbtnDetail"), LinkButton)
                    btnDetail.Visible = False
                End If

            End If


        Catch ex As Exception
            Dim strErr As String = ex.ToString & ", intLog - " & intLog
        End Try
    End Sub



    Protected Sub gvMohonBajet_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvSemakanBajet.RowCommand

        ' CommandName property to determine which button was clicked.
        If e.CommandName = "Select" Then

            ' Convert the row index stored in the CommandArgument
            ' property to an Integer.
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)

            ' Get the last name of the selected author from the appropriate
            ' cell in the GridView control.
            Dim selectedRow As GridViewRow = gvSemakanBajet.Rows(index)
            Dim NoMohon As String = selectedRow.Cells(2).Text

            Dim KodSubMenu = Request.QueryString("KodSubMenu")

            'Open other page.
            Response.Redirect($"~/FORMS/BAJET/PERMOHONAN BAJET/MaklumatSemakanBajet.aspx?KodSubMenu={KodSubMenu}&no={NoMohon}")
        End If
    End Sub

    Protected Sub lbtnHantar_Click(sender As Object, e As EventArgs) Handles lbtnHantar.Click
        Dim chkAll As CheckBox = DirectCast(gvSemakanBajet.HeaderRow.Cells(0).FindControl("checkAll"), CheckBox)
        Dim noMohon As String = ""
        Dim program As String = ""
        Dim tarikh As String = ""
        Dim jumlah As String = ""
        Dim status As String = ""
        Dim listPermohonan As New List(Of Mohon)

        For i As Integer = 0 To gvSemakanBajet.Rows.Count - 1
            noMohon = gvSemakanBajet.Rows(i).Cells(2).Text.ToString()
            program = gvSemakanBajet.Rows(i).Cells(3).Text.ToString()
            tarikh = gvSemakanBajet.Rows(i).Cells(4).Text.ToString()
            jumlah = gvSemakanBajet.Rows(i).Cells(5).Text.ToString()
            status = gvSemakanBajet.Rows(i).Cells(6).Text.ToString()

            If status.Equals("PROSES SOKONGAN KETUA BAHAGIAN") Then
                If chkAll.Checked Then
                    'Add all rows
                    listPermohonan.Add(
                        New Mohon() With {
                            .NoMohon = noMohon,
                            .Program = program,
                            .Tarikh = tarikh,
                            .Jumlah = jumlah,
                            .Status = status
                        })
                Else
                    Dim chk As CheckBox = DirectCast(gvSemakanBajet.Rows(i).Cells(0).FindControl("CheckBox1"), CheckBox)

                    If chk.Checked Then
                        listPermohonan.Add(New Mohon() With {
                            .NoMohon = noMohon,
                            .Program = program,
                            .Tarikh = tarikh,
                            .Jumlah = jumlah,
                            .Status = status
                        })
                    End If
                End If
            End If
        Next

        If listPermohonan.Count > 0 Then
            gvHantar.DataSource = listPermohonan
            gvHantar.DataBind()

            Me.ModalPopupExtender3.Show()
            Session("ListPermohonan") = listPermohonan
        Else
            fGlobalAlert("Sila pilih sekurangnya satu rekod berstatus 'PROSES SOKONGAN KETUA BAHAGIAN'", Me.Page, Me.[GetType]())
        End If
    End Sub

    'Protected Sub lbtnMohonBaru_Click(sender As Object, e As EventArgs) Handles lbtnMohonBaru.Click
    '    Response.Redirect("~/FORMS/BAJET/PERMOHONAN BAJET/MaklumatPermohonanBajet.aspx?no=0")
    'End Sub

    Protected Sub gvMohonBajet_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvSemakanBajet.Sorting
        Try
            Dim sortingDirection As String = String.Empty
            If direction = SortDirection.Ascending Then
                direction = SortDirection.Descending
                sortingDirection = "Desc"
            Else
                direction = SortDirection.Ascending
                sortingDirection = "Asc"
            End If

            Dim strSql As String = ""
            Dim kodStatus = ddlStatus.SelectedValue
            Dim kodUnit = ddlUnit.SelectedValue
            If kodStatus Is String.Empty And KodUnit Is String.Empty Then
                strSql = $"select BG20_NoMohon, BG20_TarikhMohon, BG20_Program, BG20_AmaunMohon, StatusDok from BG20_Mohon WHERE KodBahagian='{Session("ssusrKodBahagian")}'"
            ElseIf kodStatus IsNot String.Empty And KodUnit Is String.Empty Then
                strSql = $"select BG20_NoMohon, BG20_TarikhMohon, BG20_Program, BG20_AmaunMohon, StatusDok from BG20_Mohon WHERE StatusDok='{kodStatus}' AND KodBahagian='{Session("ssusrKodBahagian")}'"
            ElseIf kodStatus Is String.Empty And KodUnit IsNot String.Empty Then
                strSql = $"select BG20_NoMohon, BG20_TarikhMohon, BG20_Program, BG20_AmaunMohon, StatusDok from BG20_Mohon WHERE KodUnitPtj='{KodUnit}'"
            ElseIf kodStatus IsNot String.Empty And KodUnit IsNot String.Empty Then
                strSql = $"select BG20_NoMohon, BG20_TarikhMohon, BG20_Program, BG20_AmaunMohon, StatusDok from BG20_Mohon WHERE StatusDok='{kodStatus}' AND KodUnitPtj='{KodUnit}'"

            End If
            Dim dt = fCreateDt(strSql)
            If Not dt Is Nothing Then
                Using dv As New DataView(dt)
                    dv.Sort = e.SortExpression & " " & sortingDirection
                    Session("SortedView") = dv
                    gvSemakanBajet.DataSource = dv
                    gvSemakanBajet.DataBind()
                End Using
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

    Protected Sub lbYes_Click(sender As Object, e As EventArgs) Handles lbYes.Click
        Dim ListMohon = TryCast(Session("ListPermohonan"), List(Of Mohon))

        If ListMohon.Count > 0 Then
            Dim strSql = ""
            Dim noMohon = ""
            Dim StatusHantar = "03"
            Dim paramSql() As SqlParameter
            Dim dbconn As New DBKewConn
            For Each item In ListMohon
                noMohon = item.NoMohon
                strSql = $"UPDATE BG20_Mohon SET StatusDok=@StatusDokk WHERE BG20_NoMohon='{noMohon}'"
                paramSql = {
                    New SqlParameter("@StatusDokk", StatusHantar)
                }
                dbconn.sUpdateCommand(strSql, paramSql)
            Next
            fGlobalAlert($"Semua rekod telah dihantar!", Me.Page, Me.[GetType](), "../../BAJET/PERMOHONAN BAJET/Semakan_Ketua_Bahagian.aspx")
        End If
        Session.Remove("ListPermohonan")
    End Sub

    Protected Sub gvMohonBajet_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvSemakanBajet.RowCreated
        Try
            If e.Row.RowType = DataControlRowType.Footer Then

                'First cell Is used for specifying the Total text
                Dim intNoOfMergeCol = e.Row.Cells.Count - 3 ' /*except last column */
                For intCellCol As Integer = 1 To intNoOfMergeCol - 1
                    e.Row.Cells.RemoveAt(intCellCol)
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
        Catch ex As Exception
        End Try


    End Sub


End Class
