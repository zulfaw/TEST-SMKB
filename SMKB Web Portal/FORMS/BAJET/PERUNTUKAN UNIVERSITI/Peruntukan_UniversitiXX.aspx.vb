Imports System.Web.Configuration

Public Class Peruntukan_UniversitiXX
    Inherits System.Web.UI.Page
    Dim clsMail As New clsMail.Mail

    Dim strSMTPServer As String = WebConfigurationManager.AppSettings("SMTPServer")
    Dim strSMTPPort As String = WebConfigurationManager.AppSettings("SMTPPort")
    Dim strSenderAdr As String = WebConfigurationManager.AppSettings("SenderAddr")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If (Not Page.IsPostBack) Then
            fBindGV()
            Dim strNoStaf As String = Session("ssusrID")
        End If

    End Sub

    Private Sub gvPeruntukan_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvPeruntukan.PageIndexChanging

        Try

            gvPeruntukan.PageIndex = e.NewPageIndex
            If Session("SortedView") IsNot Nothing Then
                gvPeruntukan.DataSource = Session("SortedView")
                gvPeruntukan.DataBind()
            Else
                Dim dt As New DataTable
                dt = fCreateDt()
                gvPeruntukan.DataSource = dt
                gvPeruntukan.DataBind()

            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Function fCreateDt()
        Try
            Dim ds As New DataSet
            Dim dt As New DataTable
            dt.Columns.Add("BG03_Tahun", GetType(String))
            dt.Columns.Add("BG03_Asal", GetType(String))
            dt.Columns.Add("BG03_Tambahan", GetType(String))
            dt.Columns.Add("BG03_Kurangan", GetType(String))
            dt.Columns.Add("jumbf", GetType(String))
            dt.Columns.Add("BG03_JumBesar", GetType(String))

            Dim strTahun As String
            Dim decJumAsal As Decimal
            Dim strJumAsal As String

            Dim decJumTB As Decimal
            Dim strJumTB As String

            Dim decJumKG As Decimal
            Dim strJumKG As String

            Dim decJumBF As Decimal
            Dim strJumBF As String

            Dim decJumBesar As Decimal
            Dim strJumBesar As String

            ds = fLoadDsTahun()
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                strTahun = ds.Tables(0).Rows(i)(0).ToString

                decJumAsal = fGetAmaun(strTahun, "AL")
                strJumAsal = FormatNumber(decJumAsal, 2)

                decJumTB = fGetAmaun(strTahun, "TB")
                strJumTB = FormatNumber(decJumTB, 2)

                decJumKG = fGetAmaun(strTahun, "KG")
                strJumKG = FormatNumber(decJumKG)

                decJumBF = fGetBjBF(strTahun)
                strJumBF = FormatNumber(decJumBF, 2)

                decJumBesar = decJumAsal + decJumTB + decJumBF - decJumKG
                strJumBesar = FormatNumber(decJumBesar, 2)

                dt.Rows.Add(strTahun, strJumAsal, strJumTB, strJumKG, strJumBF, strJumBesar)
            Next
            Return dt
        Catch ex As Exception

        End Try
    End Function

    Private Function fLoadDsTahun() As DataSet

        Try

            Dim strSql As String
            ' strSql = "select bg03_tahun from bg03_bjtuniv order by bg03_tahun"
            strSql = "select distinct BG04_Tahun  from BG04_AgihKw where BG04_Status = 1 order by BG04_Tahun"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strSql)
            Return ds
        Catch ex As Exception

        End Try
    End Function

    Private Function fGetAmaun(ByVal strTahun As String, ByVal strKodAgih As String) As Decimal

        Try

            Dim strSql As String
            Dim strAmaun As Decimal
            strSql = "select sum(bg04_amaun) as amaun from bg04_agihkw where bg04_tahun= '" & strTahun & "' and kodagih='" & strKodAgih & "' and BG04_Status = 1"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    strAmaun = IIf(IsDBNull(ds.Tables(0).Rows(0)("amaun")), 0.00, ds.Tables(0).Rows(0)("amaun"))
                End If
            End If

            Return strAmaun

        Catch ex As Exception

        End Try
    End Function

    Private Function fGetBjBF(ByVal strTahun As String) As Decimal

        Try

            Dim strSql As String
            Dim strBjBF As Decimal
            strSql = "select sum(mk09_debit) as jumbf from mk09_bajetbf where mk09_tahun='" & strTahun & "' "

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    strBjBF = IIf(IsDBNull(ds.Tables(0).Rows(0)("jumbf")), 0.00, ds.Tables(0).Rows(0)("jumbf"))
                End If
            End If

            Return strBjBF

        Catch ex As Exception

        End Try
    End Function

    Private Sub gvPeruntukan_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvPeruntukan.Sorting

        Try

            Dim sortingDirection As String = String.Empty
            If direction = SortDirection.Ascending Then
                direction = SortDirection.Descending
                sortingDirection = "Desc"
            Else
                direction = SortDirection.Ascending
                sortingDirection = "Asc"
            End If

            Dim sortedView As New DataView(fCreateDt())
            sortedView.Sort = Convert.ToString(e.SortExpression + " ") & sortingDirection
            Session("SortedView") = sortedView
            gvPeruntukan.DataSource = sortedView
            gvPeruntukan.DataBind()

        Catch ex As Exception

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

    Private Function fBindGV()

        Try

            Dim dt As New DataTable
            dt = fCreateDt()

            lblJumRekod.InnerText = dt.Rows.Count

            gvPeruntukan.DataSource = dt
            gvPeruntukan.DataBind()

        Catch ex As Exception

        End Try

    End Function



    Private Function ConvertSortDirectionToSql(sortDirection__1 As SortDirection) As String
        Dim newSortDirection As String = [String].Empty

        Select Case sortDirection__1
            Case SortDirection.Ascending
                newSortDirection = "ASC"
                Exit Select

            Case SortDirection.Descending
                newSortDirection = "DESC"
                Exit Select
        End Select

        Return newSortDirection
    End Function

    Protected Sub btnSendMail_Click(sender As Object, e As EventArgs) Handles btnSendMail.Click
        Dim boolSend As Boolean
        Try
            boolSend = fSendEmail()
        Catch ex As Exception

        End Try
    End Sub

    Private Function fSendEmail() As Boolean
        Dim strMsg As String = ""
        Try

            Dim strTo As String
            Dim strFrom As String
            Dim strsubject As String
            Dim strbody As String = ""
            Dim strAtt As String

            strsubject = "TEST EMAIL"
            strbody += "<br><b> Ini email testing dari SMKB Net."
            strbody += "<br>Email ini tidak perlu dibalas."

            strFrom = strSenderAdr  ' "smkbTest@utem.edu.my"
            strTo = "hanafi.mohtar@utem.edu.my"  'Put semicolon(;) for multiple receipients i.e: "aaa@utem.edu.my;bbb@gmail.com"

            strSMTPServer = strSMTPServer  ' "smtp01.utem.edu.my"
            strSMTPPort = strSMTPPort ' "25"
            strAtt = "test.pdf;test2.pdf;test.txt;test.bmp;test.docx" 'Put semicolon(;) for multiple attachment i.e: "aaa.pdf;bbb.txt"
            strMsg = clsMail.fSendMail(strFrom, strTo, strsubject, strbody, strSMTPServer, strSMTPPort, strAtt)
            If strMsg = "1" Then
                Return True
            Else

                Return False
            End If
        Catch ex As Exception
            'clsSearch.sWriteErrorLog(Session("dwspath"), "(Check_In.aspx)fSendNotification() --> " & ex.Message)
        End Try
    End Function

    'Private Sub gvPeruntukan_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvPeruntukan.RowCreated
    '    Try
    '        If e.Row.RowType = DataControlRowType.Header Then
    '            Dim imageUrl As String = (If(gvPeruntukan.SortDirection = SortDirection.Ascending, "..\..\..\Images\sort.png", "..\..\..\Images\sort.png"))
    '            For i As Integer = 0 To gvPeruntukan.Columns.Count - 1
    '                Dim columnExpression As String = gvPeruntukan.Columns(i).SortExpression
    '                ' If columnExpression <> "" AndAlso columnExpression = gvPeruntukan.SortExpression Then
    '                If columnExpression <> "" Then
    '                    Dim img As New Image()
    '                    img.ImageUrl = imageUrl
    '                    e.Row.Cells(0).Controls.Add(New LiteralControl(" "))
    '                    e.Row.Cells(i).Controls.Add(img)
    '                End If
    '            Next
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub
End Class