Imports System.Data.SqlClient
Imports System.Drawing

Public Class Penentuan_Peringkat_PTJ
    Inherits System.Web.UI.Page
    Protected strNotice As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            txtPTJ.Text = Session("ssusrPTj")
            hidTxtKodpTj.Text = Session("ssusrKodPTj")
            txtTahunBajet.Text = Date.Now.Year.ToString
            fSetPeringkat()

        End If

    End Sub

    Private Sub fSetPeringkat()
        Try
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            Dim dsPeringkat As New DataSet

            Dim strSql As String
            strSql = "select KodPTj, KodPeringkat, Tahun from BG09_PeringkatAgihan where kodptj = '" & Trim(hidTxtKodpTj.Text.TrimEnd) & "' and tahun = '" & Trim(txtTahunBajet.Text.TrimEnd) & "'"
            dsPeringkat = dbconn.fselectCommand(strSql)
            If Not dsPeringkat Is Nothing Then
                If dsPeringkat.Tables(0).Rows.Count > 0 Then
                    rbPeringkat.SelectedValue = dsPeringkat.Tables(0).Rows(0)(1).ToString
                Else
                    rbPeringkat.SelectedValue = 1
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lbtnSimpan_Click(sender As Object, e As EventArgs) Handles lbtnSimpan.Click
        Try
            Dim strsql As String
            Dim strKodPTj As String
            Dim strTahun As String
            Dim strKodPeringkat As String

            strKodPTj = Trim(hidTxtKodpTj.Text.TrimEnd)
            strKodPeringkat = Trim(rbPeringkat.SelectedValue.TrimEnd)
            strTahun = Trim(txtTahunBajet.Text.TrimEnd)

            Dim isExist As Boolean = fCheckExist(strKodPTj)
            If isExist = False Then
                strsql = "insert into BG09_PeringkatAgihan(KodpTj, KodPeringkat, Tahun) values (@KodpTj, @KodPeringkat, @Tahun)"

                Dim paramSql() As SqlParameter = {
                    New SqlParameter("@KodpTj", strKodPTj),
                    New SqlParameter("@KodPeringkat", strKodPeringkat),
                    New SqlParameter("@Tahun", strTahun)
                }

                Dim dbconn As New DBKewConn
                dbconn.sConnBeginTrans()
                If dbconn.fInsertCommand(strsql, paramSql) > 0 Then
                    dbconn.sConnCommitTrans()
                    fGlobalAlert("Peringkat Agihan Bajet telah ditetapkan!", Me.Page, Me.[GetType]())
                Else
                    dbconn.sConnRollbackTrans()
                    fGlobalAlert("Penetapan gagal!", Me.Page, Me.[GetType]())
                End If





            Else

                Dim dbconn As New DBKewConn
                strsql = "Update BG09_PeringkatAgihan set KodPeringkat = @KodPeringkat where KodpTj = @KodpTj and Tahun = @Tahun"
                Dim paramSql() As SqlParameter = {
                            New SqlParameter("@KodpTj", strKodPTj),
                            New SqlParameter("@KodPeringkat", strKodPeringkat),
                            New SqlParameter("@Tahun", strTahun)
                            }
                dbconn.sConnBeginTrans()
                If dbconn.fUpdateCommand(strsql, paramSql) > 0 Then
                    dbconn.sConnCommitTrans()
                    fGlobalAlert("Tetapan peringkat Agihan Bajet telah dikemas kini!", Me.Page, Me.[GetType]())
                Else
                    dbconn.sConnRollbackTrans()
                    fGlobalAlert("Kemas kini gagal!", Me.Page, Me.[GetType]())
                End If



            End If




        Catch ex As Exception

        End Try
    End Sub

    Private Function fCheckExist(ByVal strKodPTj As String) As Boolean
        Try
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            Dim strSql As String
            strSql = "select count(*) from BG09_PeringkatAgihan where kodptj = '" & strKodPTj & "' and tahun = '" & Trim(txtTahunBajet.Text.TrimEnd) & "'"
            Dim intCnt As Integer = dbconn.fSelectCount(strSql)

            If intCnt > 0 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception

        End Try
    End Function






End Class