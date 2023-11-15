Imports System.Data.SqlClient
Imports System.Drawing

Public Class Senarai_Objek_Am
    Inherits System.Web.UI.Page

    Private Shared dsObjAm As New DataSet
    Private Shared dvObjAm As New DataView

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            ViewState("SaveMode") = 1
            fBindDdlKodKW()
            fBindDdlObjekAm()
            fBindGvObjAM()

        End If
    End Sub

    Private Sub fBindDdlKodKW()
        Try
            Dim strSql As String = "select KodKW,(KodKw + ' - ' + Butiran ) as Butiran from mk_kw Where Status = 1 order by kodkw"
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fselectCommand(strSql)

            ddlKodKW.DataSource = ds
            ddlKodKW.DataTextField = "Butiran"
            ddlKodKW.DataValueField = "KodKW"
            ddlKodKW.DataBind()

            ddlKodKW.Items.Insert(0, New ListItem("-SILA PILIH-", "0"))
            ddlKodKW.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Private Sub fBindDdlObjekAm()
        Try
            Dim strSql As String = "SELECT DISTINCT TOP 100 PERCENT dbo.MK_Vot.KodVot, (dbo.MK_Vot.KodVot +' - '+ dbo.MK_Vot.Butiran) as Butiran " &
            "FROM dbo.MK_Vot INNER JOIN dbo.MK01_VotTahun ON dbo.MK_Vot.KodVot = dbo.MK01_VotTahun.KodVot WHERE (dbo.MK01_VotTahun.MK01_Status = '1') and (dbo.MK_Vot.Klasifikasi  = 'H1') " &
            "order by dbo.MK_Vot.KodVot"
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fselectCommand(strSql)

            ddlObjekAm.DataSource = ds
            ddlObjekAm.DataTextField = "Butiran"
            ddlObjekAm.DataValueField = "KodVot"
            ddlObjekAm.DataBind()

            ddlObjekAm.Items.Insert(0, New ListItem("-SILA PILIH-", "0"))
            ddlObjekAm.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Private Function fBindGvObjAM()
        Try
            Dim dt As New DataTable
            dt = fCreateDtObjAm()

            If dt.Rows.Count = 0 Then
                gvObjekAm.DataSource = New List(Of String)
            Else
                gvObjekAm.DataSource = dt
            End If

            gvObjekAm.DataBind()
            lblJumRekod.InnerText = dt.Rows.Count
        Catch ex As Exception

        End Try
    End Function

    Private Function fCreateDtObjAm() As DataTable
        Try
            Dim dt As New DataTable
            dt.Columns.Add("KodKW", GetType(String))
            dt.Columns.Add("ObjAm", GetType(String))
            dt.Columns.Add("Status", GetType(String))

            Dim strKodKW, strObjAm, strStatus As String
            Dim blnStatus As Boolean
            Dim strSql As String = "select s.KodKW, (s.KodVot +' - ' + (select v.butiran from mk_vot v where v.kodvot = s.KodVot ))  as ObjAm, Status from bg_setupobjam s order by s.kodkw,ObjAm"

            Dim dbconn As New DBKewConn
            dsObjAm = dbconn.fselectCommand(strSql)
            dvObjAm = New DataView(dsObjAm.Tables(0))

            For i As Integer = 0 To dsObjAm.Tables(0).Rows.Count - 1

                strKodKW = dsObjAm.Tables(0).Rows(i)("KodKW").ToString
                strObjAm = dsObjAm.Tables(0).Rows(i)("ObjAm").ToString
                blnStatus = dsObjAm.Tables(0).Rows(i)("Status").ToString

                If blnStatus = True Then strStatus = "Aktif" Else strStatus = "Tidak Aktif"
                dt.Rows.Add(strKodKW, strObjAm, strStatus)
            Next
            ViewState("dtObjAm") = dt
            Return dt

        Catch ex As Exception

        End Try
    End Function


    Private Sub gvObjekAm_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvObjekAm.SelectedIndexChanged
        Try
            For Each row As GridViewRow In gvObjekAm.Rows
                If row.RowIndex = gvObjekAm.SelectedIndex Then

                    row.ForeColor = ColorTranslator.FromHtml("#0000FF")
                Else
                    row.ForeColor = ColorTranslator.FromHtml("#000000")
                End If
            Next
            ViewState("SaveMode") = 2 'New
        Catch ex As Exception

        End Try
    End Sub



    Protected Sub gvObjekAm_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvObjekAm.PageIndexChanging
        Try
            gvObjekAm.PageIndex = e.NewPageIndex
            If ViewState("dtObjAm") IsNot Nothing Then
                gvObjekAm.DataSource = ViewState("dtObjAm")
                gvObjekAm.DataBind()
            Else
                Dim dt As New DataTable
                dt = fCreateDtObjAm()
                gvObjekAm.DataSource = dt
                gvObjekAm.DataBind()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub fReset()
        Try
            ddlKodKW.SelectedIndex = 0
            ddlObjekAm.SelectedIndex = 0
            rbStatus.SelectedIndex = 0
            lbtnHapus.Visible = False
            For Each row As GridViewRow In gvObjekAm.Rows
                row.ForeColor = ColorTranslator.FromHtml("#000000")
            Next

            ddlKodKW.Enabled = True
            ddlObjekAm.Enabled = True
        Catch ex As Exception

        End Try
    End Sub

    Private Function fCheckTransaksi(ByVal strYear As String, ByVal strKodKW As String, ByVal strKodVot As String)
        Try
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            Dim strSql As String


            strKodVot = strKodVot.Substring(0, 1)
            strSql = "select count(*) from mk06_transaksi where mk06_tahun='" & strYear & "' and kodkw='" & strKodKW & "' and left (kodvot, 1)  ='" & strKodVot & "'"
            Dim intCnt As Integer = dbconn.fSelectCount(strSql)

            Return intCnt

        Catch ex As Exception

        End Try
    End Function

    Private Function fCheckAgihKW(ByVal strYear As String, ByVal strKodKW As String)
        Try
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            Dim strSql As String

            strSql = "select count(*) from bg02_agihkw where bg02_tahun='" & strYear & "' and kodkw='" & strKodKW & "'"
            Dim intCnt As Integer = dbconn.fSelectCount(strSql)

            Return intCnt

        Catch ex As Exception

        End Try
    End Function


    Private Sub gvObjekAm_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvObjekAm.RowCommand

        Try
            If e.CommandName = "Select" Then
                Dim intStatus As Integer

                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim selectedRow As GridViewRow = gvObjekAm.Rows(index)

                Dim strKW As String = selectedRow.Cells(1).Text
                Dim strObjAm As String = selectedRow.Cells(2).Text
                strObjAm = strObjAm.Substring(0, 5)

                Dim strStatus As String = selectedRow.Cells(3).Text
                If strStatus = "Aktif" Then intStatus = 1 Else intStatus = 0

                ddlKodKW.SelectedValue = strKW
                ddlObjekAm.SelectedValue = strObjAm
                rbStatus.SelectedValue = intStatus

                ViewState("SaveMode") = 2 'Edit
                lbtnHapus.Visible = True

                ddlKodKW.Enabled = False
                ddlObjekAm.Enabled = False

            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub lbtnSimpan_Click(sender As Object, e As EventArgs) Handles lbtnSimpan.Click

        Dim strSql As String
        Dim strKodKW, strKodObjAm As String
        Dim intStatus As Integer
        Dim dbconn As New DBKewConn
        Try
            strKodKW = Trim(ddlKodKW.SelectedValue.TrimEnd)
            strKodObjAm = Trim(ddlObjekAm.SelectedValue.TrimEnd)
            intStatus = rbStatus.SelectedValue
            If ViewState("SaveMode") = 1 Then
                'INSERT

                strSql = "select count(*) from bg_setupobjam where KodKw = '" & strKodKW & "' and KodVot = '" & strKodObjAm & "'"
                If fCheckRec(strSql) = 0 Then

                    strSql = "INSERT INTO bg_setupobjam (KodKw,KodVot,Status) VALUES (@KodKW,@KodObjAm,@Status)"

                    Dim paramSql() As SqlParameter = {
                    New SqlParameter("@KodKW", strKodKW),
                    New SqlParameter("@KodObjAm", strKodObjAm),
                    New SqlParameter("@Status", intStatus)
                }

                    dbconn.sConnBeginTrans()
                    If dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                        dbconn.sConnCommitTrans()
                        fGlobalAlert("Rekod telah disimpan!", Me.Page, Me.[GetType]())
                        fBindGvObjAM()
                        fReset()
                    Else
                        dbconn.sConnRollbackTrans()
                        fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
                    End If

                Else
                    fGlobalAlert("Rekod telah wujud!", Me.Page, Me.[GetType]())
                End If
            ElseIf ViewState("SaveMode") = 2 Then
                'UPDATE
                strSql = "update bg_setupobjam set Status = @Status where KodKw = @KodKw and KodVot = @KodObjAm"
                Dim paramSql() As SqlParameter = {
                        New SqlParameter("@KodKW", strKodKW),
                        New SqlParameter("@KodObjAm", strKodObjAm),
                        New SqlParameter("@Status", intStatus)
                        }
                dbconn.sConnBeginTrans()
                If dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
                    dbconn.sConnCommitTrans()
                    fGlobalAlert("Rekod telah dikemas kini!", Me.Page, Me.[GetType]())
                    fBindGvObjAM()
                    fReset()
                Else
                    dbconn.sConnRollbackTrans()
                    fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub lbtnBaru_Click(sender As Object, e As EventArgs) Handles lbtnBaru.Click
        fReset()
        ViewState("SaveMode") = 1 'New
    End Sub

    Private Sub lbtnHapus_Click(sender As Object, e As EventArgs) Handles lbtnHapus.Click
        Try
            Dim strSql As String
            Dim strKodKW As String
            Dim strKodObjAm As String


            strKodKW = ddlKodKW.SelectedValue ' Trim(row.Cells(1).Text.ToString.TrimEnd)
            strKodObjAm = ddlObjekAm.SelectedValue ' (Trim(row.Cells(2).Text.ToString.TrimEnd)).Substring(0, 5)

            Dim strYear As String
            strYear = Now.Year

            Dim intCnt1 As Integer = fCheckTransaksi(strYear, strKodKW, strKodObjAm)
            If intCnt1 > 0 Then
                fGlobalAlert("Rekod tidak boleh dihapuskan! Terdapat transaksi pada tahun semasa yang menggunakan Kod KW tersebut.", Me.Page, Me.[GetType]())
                Exit Sub
            Else
                Dim intCnt2 As Integer = fCheckAgihKW(strYear, strKodKW)
                If intCnt2 > 0 Then
                    fGlobalAlert("Rekod tidak boleh dihapuskan! Terdapat agihan pada tahun semasa yang menggunakan Kod KW tersebut.", Me.Page, Me.[GetType]())
                    Exit Sub
                Else
                    strSql = "delete from bg_setupobjam where kodkw='" & strKodKW & "' and kodvot='" & strKodObjAm & "'"
                    Dim dbconn As New DBKewConn
                    If dbconn.fUpdateCommand(strSql) > 0 Then
                        fGlobalAlert("Rekod telah dihapuskan!", Me.Page, Me.[GetType]())
                        fBindGvObjAM()
                        fReset()
                    End If
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub
End Class