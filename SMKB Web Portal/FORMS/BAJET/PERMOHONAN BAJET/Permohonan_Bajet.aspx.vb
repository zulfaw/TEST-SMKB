﻿Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Globalization

Imports System.Net
Imports System.Net.Mail
Imports System.Web.Configuration


Public Class Permohonan_Bajet
    Inherits System.Web.UI.Page


    Private myStatusDok As New StatusDokModel
    Private dicStatusDok As New Dictionary(Of String, String)
    Private dsJurnalDt As New DataSet("JurnalDtDs")
    Private dbconnEQ As New DBEQConn
    Private dbconn As New DBKewConn

    Private countButiran As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not IsPostBack Then

                fBindDdlStatus()

                fBindDdlKW()
                fBindDdlKodOperasi()
                fBindDdlTahun()

                'Session("ssusrID") = "00245"

                Dim strSql_eqsa = $"Select staf.att15 As nostaf, Replace(ptj.kodpejabat,' ','') as kodpejabat, ptj.name as ptj, dept.dept_kod, dept.dept_name, Unit.unit_kod, Unit.unit_name From
                                    [eqcas].[dbo].live_user_staf as staf
                                    Left Join [eqcas].[dbo].live_group as ptj on staf.att1 = ptj.kodpejabat
                                    left join [eqcas].[dbo].live_dept as dept on staf.att10 = dept.dept_kod
                                    left join [eqcas].[dbo].live_unit as unit on staf.att11 = unit.unit_kod
                                    where att15 = '{Session("ssusrID")}'"                                                                                                                                                                                           '

                Dim dsMohon_View As New DataSet
                Dim dtData As New DataTable
                Dim dt = TryCast(ViewState("vsSenaraiBaucarCek"), DataTable)

                dsMohon_View = dbconnEQ.fselectCommand(strSql_eqsa)
                Dim ds_ = dbconnEQ.fselectCommand(strSql_eqsa)
                If ds_.Tables.Count > 0 Then
                    If ds_.Tables(0).Rows.Count = 1 Then
                        Dim ptj = ds_.Tables(0).Rows(0)("kodpejabat").ToString
                        Dim bahgian = ds_.Tables(0).Rows(0)("dept_kod").ToString

                        lblPTJ.Text = ds_.Tables(0).Rows(0)("kodpejabat").ToString
                        lblBahagian.Text = ds_.Tables(0).Rows(0)("dept_kod").ToString

                        fBindGVMohonBajet("01", ptj, bahgian)

                        Try
                            Dim strSql As String = $"select  a.MS01_NoStaf as nostaf, c.MS01_Nama as nama from 
                            MS02_Perjawatan as a ,
                            MS08_Penempatan as b,
                            MS01_Peribadi as c
                            where  a.MS01_NoStaf = b.MS01_NoStaf 
                            and a.MS01_NoStaf = c.MS01_NoStaf
                            and RIGHT(MS02_GredGajiS,2) >= '{ptj}'
                            and a.MS01_NoStaf not in ('{Session("ssusrID")}')
                            and b.MS08_Pejabat = '{ptj}'
                            and RIGHT(MS02_GredGajiS,2) not in ('U7','K6','K7','K5','U5')
                            and c.MS01_Status = '1' and b.MS08_StaTerkini = '1'
                            Order by  nama"

                            Dim ds As New DataSet
                            Dim dbconn As New DBSMConn
                            ds = dbconn.fselectCommand(strSql)

                            ddlPenyokong.DataSource = ds
                            ddlPenyokong.DataTextField = "nama"
                            ddlPenyokong.DataValueField = "nostaf"
                            ddlPenyokong.DataBind()
                            ddlPenyokong.Items.Insert(0, New ListItem("-SILA PILIH-", ""))
                            ddlPenyokong.SelectedIndex = 0


                        Catch ex As Exception
                            'fErrorLog("Peruntukan_KW.aspx(fBindDdlKW)- " & ex.Message.ToString)
                        End Try

                    End If
                End If


            End If
        Catch ex As Exception

        End Try
    End Sub



    '''' <summary>
    '''' Bind data BG_StatusDokBaru and load to dropdownlist
    '''' </summary>
    Private Sub fBindDdlStatus()
        Try
            Dim strSql As String = "Select KodStatusDok, Butiran from BG_StatusDok WHERE KodStatusDok <= '07' OR KodStatusDok >= '17' AND KodStatusDok <= '22' ORDER BY Urutan ASC"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            ddlStatus.DataSource = ds
            ddlStatus.DataTextField = "Butiran"
            ddlStatus.DataValueField = "KodStatusDok"
            ddlStatus.DataBind()

            ddlStatus.Items.Insert(0, New ListItem("- KESELURUHAN -", ""))
            ddlStatus.SelectedIndex = 1
        Catch ex As Exception
            'fErrorLog("Peruntukan_KW.aspx(fBindDdlKW)- " & ex.Message.ToString)
        End Try
    End Sub

    Protected Sub lbtnMohonBaru_Click(sender As Object, e As EventArgs) Handles lbtnMohonBaru.Click
        Dim KodSubMenu = Request.QueryString("KodSubMenu")
        Response.Redirect($"~/FORMS/BAJET/PERMOHONAN BAJET/MaklumatPermohonanBajet.aspx?KodSubMenu={KodSubMenu}&no=0")
    End Sub

    Private Sub fBindGVMohonBajet(KodStatus As String, ptj As String, bahagian As String)
        Try
            Dim strSql As String = ""
            Dim strSql_count As String = ""
            Dim carianStatus = ddlStatus.SelectedValue
            Dim kodkw = ddlKW.SelectedValue
            Dim KodKO = ddlKodOperasi.SelectedValue

            Dim ayatSql As String

            If (carianStatus <> "") Then
                ayatSql = ayatSql + $" and StatusDok='{carianStatus}' "
            End If

            If (kodkw <> "") Then
                ayatSql = ayatSql + $" and KodKW = '{ddlKW.SelectedValue}' "
            End If

            If (KodKO <> "") Then
                ayatSql = ayatSql + $" and KodOperasi = '{ddlKodOperasi.SelectedValue}' "
            End If


            If KodStatus = String.Empty Then
                strSql = $"select DISTINCT a.BG20_NoMohon, BG20_TarikhMohon, REPLACE(BG20_Program,'\','''')  as BG20_Program, BG20_AmaunMohon, StatusDok, KodKW, KodOperasi,  REPLACE(BG20_Justifikasi,'\','''')  as BG20_Justifikasi from BG01_Mohon as a WHERE BG20_StaffIDPemohon is not null and a.BG20_StaffIDPemohon = '{Session("ssusrID")}' " + ayatSql
                strSql_count = $"select count(DISTINCT a.BG20_NoMohon) as bil from BG01_Mohon as a WHERE BG20_StaffIDPemohon is not null and a.BG20_StaffIDPemohon = '{Session("ssusrID")}' " + ayatSql
            Else
                strSql = $"select DISTINCT a.BG20_NoMohon, BG20_TarikhMohon, REPLACE(BG20_Program,'\','''')  as BG20_Program, BG20_AmaunMohon, StatusDok, KodKW, KodOperasi, REPLACE(BG20_Justifikasi,'\','''')  as BG20_Justifikasi from BG01_Mohon as a where  BG20_StaffIDPemohon is not null and a.BG20_StaffIDPemohon = '{Session("ssusrID")}' " + ayatSql
                strSql_count = $"select count(DISTINCT a.BG20_NoMohon) as bil from BG01_Mohon as a where  BG20_StaffIDPemohon is not null and a.BG20_StaffIDPemohon = '{Session("ssusrID")}' " + ayatSql
            End If

            Dim ds = dbconn.fSelectCommand(strSql_count)
            If ds.Tables.Count > 0 Then
                lblRekod.Text = ds.Tables(0).Rows(0)("bil")
            Else
                lblRekod.Text = "0"
            End If


            Dim dt = fCreateDt(strSql)
            Dim total As Decimal = dt.AsEnumerable().Sum(Function(row) row.Field(Of Decimal)("BG20_AmaunMohon"))
            ViewState("TotalAmount") = total

            gvMohonBajet.DataSource = dt
            gvMohonBajet.DataBind()

        Catch ex As Exception

        End Try
    End Sub



    Private Function fCreateDt(strSql As String) As DataTable
        Dim dbconn As New DBKewConn

        'pass as reference 
        Dim ds = dbconn.fSelectCommand(strSql)

        'Convert dataset to datatable
        Dim dt = ds.Tables(0)

        If dt.Rows.Count > 0 Then
            Dim strDate As DateTime = Nothing
            dt.Columns.Add("TarikhMohon", GetType(String))
            dt.Columns.Add("Status", GetType(String))
            dt.Columns.Add("AngJumBesar", GetType(Decimal))

            For Each row As DataRow In dt.Rows
                'strDate = row("BG20_TarikhMohon").ToString
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



    Protected Function Limit(ByVal desc As Object, ByVal maxLength As Integer) As String
        Dim description = CStr(desc)

        If String.IsNullOrEmpty(description) Then
            Return description
        End If

        Return If(description.Length <= maxLength, description, description.Substring(0, maxLength) & "...")
    End Function


    Protected Sub gvMohonBajet_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvMohonBajet.RowCommand

        ' CommandName property to determine which button was clicked.

        ' Convert the row index stored in the CommandArgument
        ' property to an Integer.
        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim KodSubMenu = Request.QueryString("KodSubMenu")

        ' Get the last name of the selected author from the appropriate
        ' cell in the GridView control.
        Dim selectedRow As GridViewRow = gvMohonBajet.Rows(index)
        Dim NoMohon As String = selectedRow.Cells(5).Text

        ' Display the selected author.
        'Message.Text = "You selected " & contact & "."

        If e.CommandName = "Select" Then
            'Open other page.
            Response.Redirect($"~/FORMS/BAJET/PERMOHONAN BAJET/MaklumatPermohonanBajet.aspx?KodSubMenu={KodSubMenu}&no={NoMohon}")

        ElseIf e.CommandName = "Cetak" Then

            Dim namaLaporan = "BG_Permohonan"
            Dim url = namaLaporan
            Dim strSVR = $"https://reporting.utem.edu.my/ReportServer/Pages/ReportViewer.aspx?%2fS2%2fS2D%2f{url}"
            Dim fullURL As String = $"window.open('{strSVR}', '_blank', 'width=' + (parseInt(window.innerWidth) * 0.3) + ',height=' + (parseInt(window.innerHeight) * .3) + ',status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=yes,titlebar=no' );"
            ScriptManager.RegisterStartupScript(Me, GetType(String), "OPEN_WINDOW", fullURL, True)
        End If
    End Sub

    Protected Sub gvMohonBajet_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvMohonBajet.Sorting
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
            If kodStatus = String.Empty Then
                strSql = $"select BG20_NoMohon, BG20_Program, BG20_TarikhMohon, BG20_AmaunMohon, StatusDok from BG01_Mohon where BG20_StaffIDPemohon is not null"
            Else
                strSql = $"select BG20_NoMohon, BG20_Program, BG20_TarikhMohon, BG20_AmaunMohon, StatusDok from BG01_Mohon WHERE StatusDok='{kodStatus}' and BG20_StaffIDPemohon is not null"
            End If
            Dim dt = fCreateDt(strSql)
            If Not dt Is Nothing Then
                Using dv As New DataView(dt)
                    dv.Sort = e.SortExpression & " " & sortingDirection
                    Session("SortedView") = dv
                    gvMohonBajet.DataSource = dv
                    gvMohonBajet.DataBind()
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



    Protected Sub fBindDdlKW()
        Try
            Dim strSql As String = "Select KodKW, butiran, (KodKW +' - ' + butiran) as Butiran2 from mk_kw ORDER BY Kodkw "

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn

            ds = dbconn.fSelectCommand(strSql)
            ddlKW.DataSource = ds
            ddlKW.DataTextField = "Butiran2"
            ddlKW.DataValueField = "KodKW"
            ddlKW.DataBind()
            ddlKW.Items.Insert(0, New ListItem("- KESELURUHAN -", ""))
            ddlKW.SelectedIndex = 0
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub fbindDdlTahun()
        Try
            Dim strSql As String = "select TahunBajet from BG_TahunMohonBajet Where status = 1 "

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn

            ds = dbconn.fSelectCommand(strSql)
            ddlTahun.DataSource = ds
            ddlTahun.DataTextField = "TahunBajet"
            ddlTahun.DataValueField = "TahunBajet"
            ddlTahun.DataBind()
            'ddlTahun.Items.Insert(0, New ListItem("- KESELURUHAN -", ""))
            ddlTahun.SelectedIndex = 0
        Catch ex As Exception

        End Try
    End Sub

    Private Sub fBindDdlKodOperasi()
        Try
            Dim strSql As String = $"Select KodKO, (KodKO + ' - ' + Butiran) AS Butiran2 from MK_KodOperasi where Status = '1'"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            ddlKodOperasi.DataSource = ds
            ddlKodOperasi.DataTextField = "Butiran2"
            ddlKodOperasi.DataValueField = "KodKO"
            ddlKodOperasi.DataBind()
            'ddlKodOperasi.Items.Insert(0, "- SILA PILIH -")
            'ddlKodOperasi.Items.Insert(1, "- KESELURUHAN -")
            ddlKodOperasi.Items.Insert(0, New ListItem("- KESELURUHAN -", ""))
            ddlKodOperasi.SelectedIndex = 0

        Catch ex As Exception
            'fErrorLog("Peruntukan_KW.aspx(fBindDdlKW)- " & ex.Message.ToString)
        End Try
    End Sub



    Protected Sub lbtnHantar_Click(sender As Object, e As EventArgs) Handles lbtnHantar.Click

        If ddlPenyokong.SelectedValue <> "" Then

            Dim strbuildr As New StringBuilder(String.Empty)
            Dim rows = gvMohonBajet.Rows
            Dim chk As CheckBox
            Dim dbconn As New DBKewConn
            Dim dsMohon As New DataSet
            Dim dsMohon_Status As New DataSet
            Dim resultUpdate = False
            Dim resultCommit = False
            Dim strSqlButiran = ""
            Dim strSql_ = ""
            Dim paramSqlBtrn() As SqlParameter = Nothing
            'Dim tarikhMohon As DateTime

            'tarikhMohon = String.Format("{0:dd/MM/yyyy}", DateTime.Now)

            Dim dt = Date.Today.ToString("dd/MM/yyyy").ToString
            Dim dtTkhM As Date = DateTime.ParseExact(dt, "dd/MM/yyyy", CultureInfo.CurrentCulture)
            Dim tarikhMohon As String = dtTkhM.ToString("yyyy-MM-dd")
            
            Dim paramSql() As SqlParameter = {}

            Dim noMohon As String

            dbconn.sConnBeginTrans()

            countButiran = 0

            For Each row As GridViewRow In rows
                chk = DirectCast(row.Cells(9).FindControl("CheckBox1"), CheckBox)
                If chk.Checked Then
                    noMohon = row.Cells(5).Text

                    strSqlButiran = "Update BG01_Mohon set StatusDok = @statusDok, BG20_StaffIDPenyokong =@stafSokong, BG20_FlagPemohon =@flagPemohon WHERE BG20_Status = @status and BG20_NoMohon = @NoMohon"
                    paramSqlBtrn = {
                        New SqlParameter("@NoMohon", noMohon),
                        New SqlParameter("@status", True),
                        New SqlParameter("@stafSokong", ddlPenyokong.SelectedValue),
                        New SqlParameter("@statusDok", "02"),
                        New SqlParameter("@flagPemohon", "1")
                    }

                    Dim dbconn2 As New DBKewConn
                    'kira bil. status dok untuk no permohonan tersebut
                    strSql_ = $"select count(BG01_NoPermohonan)+1 as bil from BG12_StatusDok where BG01_NoPermohonan ='{noMohon}'"
                    dsMohon_Status = dbconn2.fSelectCommand(strSql_)
                    'If dsMohon_Status.Tables.Count > 0 Then
                    Dim bilCount = dsMohon_Status.Tables(0).Rows(0)("bil").ToString
                    'End If

                    Dim strSQLStatusDok = "INSERT INTO BG12_StatusDok (BG01_NoPermohonan, KodStatusDok, BG12_TkhProses, BG12_NoStaf, BG12_Ulasan, BG12_Bil) " &
                           " VALUES (@NoMohon, @StatusKod, @Tarikh, @NoStaf,@Ulasan, @bil)"

                    Dim paramSql2() As SqlParameter =
                      {
                         New SqlParameter("@NoMohon", noMohon),
                         New SqlParameter("@StatusKod", "02"),
                         New SqlParameter("@Tarikh", tarikhMohon),
                         New SqlParameter("@NoStaf", Session("ssusrID")),
                         New SqlParameter("@Ulasan", "-"),
                         New SqlParameter("@bil", bilCount)
                         }

                    If dbconn.fUpdateCommand(strSqlButiran, paramSqlBtrn) > 0 Then
                            countButiran = countButiran + 1

                            dbconn.fInsertCommand(strSQLStatusDok, paramSql2)
                            dbconn.sConnCommitTrans()
                            'sAuditLog(Session("ssusrID"), Request.QueryString("KodSubMenu"), "BG20_Mohon", "1", "Simpan Button")
                        End If

                    End If
            Next

            If countButiran > 0 Then

                Dim message As String = String.Empty
                Dim strNamaPenyokong As String
                Dim strEmel As String

                strNamaPenyokong = "Syafiqah"
                strEmel = "syafiqah@utem.edu.my"

                Dim Subject = "Pengesahan Permohonan Bajet"
                Dim body As String = "Assalamualaikum Wrh. Wbt. dan salam sejahtera,"
                body += "<br /><br />Prof. / Prof. Madya/ Dr./ Tuan / Puan"
                body += "<br /><br />Mohon log masuk sistem SMKB untuk mengesahkan permohonan bajet."
                'body += "<br /><a href = '" + Request.Url.AbsoluteUri.Replace("Register", Convert.ToString("EmailActivation.aspx?ActivationCode=") & activationCode) + "'>Klik di sini untuk pengaktifan akaun tuan.</a>"
                body += "<br /><br />Terima kasih"
                Dim sent = clsSharedMails.sendEmail(strEmel, Subject, body)
                If sent Then
                    'message = $"Pendaftaran berjaya. Kod pengaktifan telah dihantar melalui email: {strEmel}."
                    'fGlobalAlert($"Permohonan berjaya hantar.", Me.Page, Me.[GetType](), "../../BAJET/PERMOHONAN BAJET/Permohonan_Bajet.aspx")
                End If
                fGlobalAlert($"Permohonan berjaya dihantar untuk semakan ketua bahagian.", Me.Page, Me.[GetType](), "../../BAJET/PERMOHONAN BAJET/Permohonan_Bajet.aspx")

            Else
                fGlobalAlert("Sila pilih permohonan yang ingin dihantar sekurang-kurangnya satu (1) permohonan.", Me.Page, Me.[GetType]())
            End If

        Else
            fGlobalAlert("Sila buat pilihan penyokong sebelum hantar permohonan.", Me.Page, Me.[GetType]())

        End If
    End Sub

    Private Sub firstBindgvJurnal()
        Try
            Dim dt As New DataTable()
            dt.Columns.AddRange(New DataColumn() {
                 New DataColumn("KodKw", GetType(String))
           })

            ViewState("vsJurnal") = dt

            'Dim dtHapus As New DataTable
            'dtHapus.Columns.Add("Bil", GetType(Integer))
            'ViewState("vsHapus") = dtHapus
        Catch ex As Exception

        End Try
    End Sub

    Private Function fBindGVMohonBajetxx(KodStatus As String)
        Try
            Dim ds As DataSet
            Dim dt As New DataTable
            Dim dtData As New DataTable
            Dim dbconn As New DBKewConn

            dt = TryCast(ViewState("vsJurnal"), DataTable)
            dt.Rows.Clear()
            'dt.item.Clear()


            Dim strSql As String = ""
            'Dim kodStatus = ddlStatus.SelectedValue

            If KodStatus = String.Empty Then
                strSql = $"select DISTINCT BG20_NoMohon, BG20_TarikhMohon, BG20_Program, BG20_AmaunMohon, StatusDok, KodKW, KodOperasi, BG20_Justifikasi from BG01_Mohon where BG20_StaffIDPemohon is not null ORDER BY BG20_NoMohon DESC,  BG20_TarikhMohon DESC"
            Else
                strSql = $"select DISTINCT BG20_NoMohon, BG20_TarikhMohon, BG20_Program, BG20_AmaunMohon, StatusDok, KodKW, KodOperasi, BG20_Justifikasi from BG01_Mohon WHERE StatusDok='{KodStatus}' and BG20_StaffIDPemohon is not null ORDER BY BG20_NoMohon DESC,  BG20_TarikhMohon DESC"
            End If

            ds = dbconn.fSelectCommand(strSql)
            dtData = ds.Tables(0)

            Dim decTotDbt As Decimal = 0
            Dim decTotKbt As Decimal = 0

            Dim tableRow As DataRow = Nothing
            For Each rowdt In dtData.Rows
                'add each row into data table
                tableRow = dt.NewRow()
                tableRow("KodKW") = rowdt("KodKW")
                tableRow("KodOperasi") = rowdt("KodOperasi")
                tableRow("BG20_Program") = rowdt("BG20_Program")
                tableRow("BG20_Justifikasi") = rowdt("BG20_Justifikasi")
                tableRow("BG20_NoMohon") = rowdt("BG20_NoMohon")
                dt.Rows.Add(tableRow)
            Next

            dt.AcceptChanges()


        Catch ex As Exception

        End Try
    End Function



    Protected Sub OnDataBound(sender As Object, e As EventArgs)
        For i As Integer = gvMohonBajet.Rows.Count - 1 To 1 Step -1
            Dim row As GridViewRow = gvMohonBajet.Rows(i)
            Dim previousRow As GridViewRow = gvMohonBajet.Rows(i - 1)
            For j As Integer = 0 To row.Cells.Count - 1
                If row.Cells(j).Text = previousRow.Cells(j).Text Then
                    If previousRow.Cells(j).RowSpan = 0 Then
                        If row.Cells(j).RowSpan = 0 Then
                            previousRow.Cells(j).RowSpan += 2
                        Else
                            previousRow.Cells(j).RowSpan = row.Cells(j).RowSpan + 1
                        End If
                        row.Cells(j).Visible = False
                    End If
                End If
            Next
        Next
    End Sub

    Dim previousCellValue As String = ""
    Dim previousCellCount As Integer = 1

    Protected Sub OnRowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim row As DataRowView = TryCast(e.Row.DataItem, DataRowView)

            If previousCellValue = row("NoMohon").ToString() Then
                previousCellCount += 1
            Else

                If previousCellCount > 1 Then
                    gvMohonBajet.Rows(e.Row.RowIndex - previousCellCount).Cells(0).RowSpan = previousCellCount
                    gvMohonBajet.Rows(e.Row.RowIndex - previousCellCount).Cells(1).RowSpan = previousCellCount

                    For i As Integer = 1 To previousCellCount - 1
                        gvMohonBajet.Rows((e.Row.RowIndex - previousCellCount) + i).Cells(0).Visible = False
                        gvMohonBajet.Rows((e.Row.RowIndex - previousCellCount) + i).Cells(1).Visible = False
                    Next
                End If

                previousCellValue = row("Id").ToString()
                previousCellCount = 1
            End If
        ElseIf e.Row.RowType = DataControlRowType.Footer Then

            If previousCellCount > 1 Then
                gvMohonBajet.Rows(gvMohonBajet.Rows.Count - previousCellCount).Cells(0).RowSpan = previousCellCount
                gvMohonBajet.Rows(gvMohonBajet.Rows.Count - previousCellCount).Cells(1).RowSpan = previousCellCount

                For i As Integer = 1 To previousCellCount - 1
                    gvMohonBajet.Rows((gvMohonBajet.Rows.Count - previousCellCount) + i).Cells(0).Visible = False
                    gvMohonBajet.Rows((gvMohonBajet.Rows.Count - previousCellCount) + i).Cells(1).Visible = False
                Next
            End If
        End If
    End Sub

    Protected Sub OnRowDataBoundxx(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(3).ToolTip = TryCast(e.Row.DataItem, DataRowView)("BG20_Program").ToString()
            e.Row.Cells(4).ToolTip = TryCast(e.Row.DataItem, DataRowView)("BG20_Justifikasi").ToString()
        End If

    End Sub

    Protected Sub ddlKW_Click(sender As Object, e As EventArgs) Handles ddlKW.SelectedIndexChanged
        fBindView()
    End Sub

    Protected Sub ddlKodOperasi_Click(sender As Object, e As EventArgs) Handles ddlKodOperasi.SelectedIndexChanged
        fBindView()
    End Sub

    Protected Sub ddlStatus_Click(sender As Object, e As EventArgs) Handles ddlStatus.SelectedIndexChanged
        fBindView()
    End Sub

    Private Sub fBindView()
        Dim PTJ = lblPTJ.Text
        Dim bahagian = lblBahagian.Text
        Dim status = ddlStatus.SelectedValue

        fBindGVMohonBajet(status, PTJ, bahagian)
    End Sub

    Private Sub gvMohonBajet_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvMohonBajet.PageIndexChanging
        gvMohonBajet.PageIndex = e.NewPageIndex
        fBindView()
    End Sub


End Class