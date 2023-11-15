Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Public Class Pindah_Akaun
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            fBindDdlKW()
            fBindDdlKP()
            fBindTahun()

            div1.Visible = False
        End If

    End Sub
    Private Sub fBindTahun()
        Try
            Dim yNow As DateTime = Now.AddYears(-1)
            Dim pYear As String = yNow.Year

            Dim yNow1 As DateTime = Now.AddYears(+1)
            Dim pYear1 As String = yNow1.Year

            'txtDrpd.Text = pYear
            txtDrpd.Text = Now.Year

            txtKpd.Text = pYear1
            ' txtKpd.Text = Now.Year

        Catch ex As Exception

        End Try
    End Sub
    Private Sub fBindDdlKW()
        Try
            Dim strSql As String = "Select KodKw,(KodKw + ' - ' + Butiran ) as Butiran from MK_Kw order by KodKw"
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn

            ds = dbconn.fselectCommand(strSql)

            ddlKW.DataSource = ds
            ddlKW.DataTextField = "Butiran"
            ddlKW.DataValueField = "KodKw"
            ddlKW.DataBind()
            ddlKW.Items.Insert(0, New ListItem("-SILA PILIH-", "0"))
            ddlKW.SelectedIndex = 0
        Catch ex As Exception

        End Try
    End Sub
    Private Sub fBindDdlKP()
        Try
            Dim strSql As String = "select KodProjek, (KodProjek + '-' + Butiran) as Butiran from MK_KodProjek where status= 1 order by kodprojek"
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn

            ds = dbconn.fselectCommand(strSql)

            ddlKP.DataSource = ds
            ddlKP.DataTextField = "Butiran"
            ddlKP.DataValueField = "KodProjek"
            ddlKP.DataBind()
            ddlKP.Items.Insert(0, New ListItem("-SILA PILIH-", "0"))
            ddlKP.SelectedIndex = 0
        Catch ex As Exception

        End Try
    End Sub

    'Private Function clearfield()
    '    txtDrpd.Text = ""
    '    txtKpd.Text = ""
    'End Function


    Protected Sub rbOption_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rbOption.SelectedIndexChanged
        If rbOption.SelectedValue = 0 Then
            div1.Visible = False
            Panel1.Visible = False

        ElseIf rbOption.SelectedValue = 1 Then
            div1.Visible = True
            Panel1.Visible = True
        End If
    End Sub

    Private Sub btnProses_Click(sender As Object, e As EventArgs) Handles btnProses.Click

        Dim strKP As String

        If rbOption.SelectedValue = 0 Then
            strKP = "0000000"

        ElseIf rbOption.SelectedValue = 1 Then
            If ddlKW.SelectedValue = 0 Or ddlKP.SelectedValue = "0" Then
                fGlobalAlert("Sila pastikan tiada ruang yang dibiarkan kosong", Me.Page, Me.[GetType]())
                Exit Sub
            End If
            strKP = ddlKP.SelectedValue
        End If

        Dim dbconn As New DBKewConn
        Dim blnSuccess As Boolean = True
        Try
            Dim strSql As String
            ' Dim ds As New DataSet

            fBindDdlKW()
            fBindDdlKP()
            System.Threading.Thread.Sleep(5000)

            Dim strThnDrpd As String = Trim(txtDrpd.Text.TrimEnd)
            Dim strThnKpd As String = Trim(txtKpd.Text.TrimEnd)

            '----Double check jika tahun sumber tiada
            strSql = "select count (mk01_tahun) from mk01_vottahun where MK01_Tahun ='" & strThnDrpd & "' and kodkp ='" & strKP & "' "

            If fCheckRec(strSql) = 0 Then

                fGlobalAlert("Carta akaun bagi tahun " & strThnDrpd & " dan kod projek " & strKP & " tiada ", Me.Page, Me.[GetType]())
                Exit Sub
            Else
            End If

            '----Double check jika user klik button proses 2 kali
            strSql = "Select count (mk01_tahun)  from mk01_vottahun where MK01_Tahun ='" & strThnKpd & "' and kodkp = '" & strKP & "'"

            If fCheckRec(strSql) <> 0 Then

                fGlobalAlert("Carta akaun bagi tahun " & strThnKpd & " dan kod projek " & strKP & " telah dibina ", Me.Page, Me.[GetType]())
                Exit Sub
            Else
            End If



            '----MASUKKAN DATA BARU

            strSql = "INSERT INTO MK01_VotTahun (KodKW, KodKO, KodPTJ, KodKP , kodVot, MK01_Status, MK01_Tahun)
            Select KodKW, KodKO, KodPTJ, KodKP, kodVot,MK01_Status, '" & strThnKpd & "' FROM MK01_VotTahun WHERE MK01_Tahun='" & strThnDrpd & "' and kodkp = '" & strKP & "'"

            dbconn.sConnBeginTrans()
            If Not dbconn.fUpdateCommand(strSql) > 0 Then
                blnSuccess = False
                Exit Try
            End If

            '----DELETE DATA LAMA

            strSql = " Select count(*) from MK03_AmTahun where MK03_Tahun = '" & strThnKpd & "' and kodkp = '" & strKP & "'"

            If fCheckRec(strSql) > 0 Then
                strSql = "DELETE From MK03_AmTahun WHERE MK03_Tahun = @Tahun and kodkp = @KodKP"

                Dim paramSql() As SqlParameter =
                {
                     New SqlParameter("@Tahun", strThnKpd),
                     New SqlParameter("@KodKP", strKP)
                     }
                dbconn.sConnBeginTrans()
                If dbconn.fUpdateCommand(strSql, paramSql) > 0 Then
                    dbconn.sConnCommitTrans()
                    fGlobalAlert("Rekod telah dipadam!", Me.Page, Me.[GetType]())

                Else
                    dbconn.sConnRollbackTrans()
                    fGlobalAlert("Rekod gagal dipadam!", Me.Page, Me.[GetType]())
                    Exit Sub
                End If

            End If

            Dim strKodKW As String
            Dim ds1 As New DataSet


            strSql = "select kodkw from MK_Kw"
            ds1 = fGetRec(strSql)

            If Not ds1 Is Nothing Then
                If ds1.Tables(0).Rows.Count > 0 Then
                    For i As Integer = 0 To ds1.Tables(0).Rows.Count - 1
                        strKodKW = ds1.Tables(0).Rows(i)("kodkw").ToString


                        strSql = "SELECT DISTINCT KODPTJ FROM MK03_AmTahun with (nolock) WHERE MK03_Tahun= '" & strThnDrpd & "' and KodKW = '" & strKodKW & "' and kodkp = '" & strKP & "' ORDER BY KODPTJ"
                        Dim ds2 As New DataSet
                        Dim strKodPTJ As String
                        ds2 = fGetRec(strSql)

                        If Not ds2 Is Nothing Then
                            If ds2.Tables(0).Rows.Count > 0 Then
                                For j As Integer = 0 To ds2.Tables(0).Rows.Count - 1
                                    strKodPTJ = ds2.Tables(0).Rows(j)("KODPTJ").ToString

                                    strSql = "INSERT INTO MK03_AmTahun (KodKW, kodko, KodPTJ, kodkp, kodVot, MK03_Tahun) 
                                                SELECT KodKW, KodKO , KodPTJ, KodKP , kodVot, '" & strThnKpd & "' as MK03_Tahun FROM MK03_AmTahun WHERE MK03_Tahun= '" & strThnDrpd & "' AND kodkw = '" & strKodKW & "' AND KODPTJ = '" & strKodPTJ & "' AND kodkp = '" & strKP & "'"

                                    dbconn.sConnBeginTrans()
                                    If Not dbconn.fInsertCommand(strSql) > 0 Then
                                        blnSuccess = False
                                        Exit Try
                                    End If
                                Next
                            End If
                        End If
                    Next
                End If
            End If
        Catch ex As Exception

            blnSuccess = False
        End Try

        If blnSuccess = False Then
            dbconn.sConnRollbackTrans()
        Else
            dbconn.sConnCommitTrans()
            fGlobalAlert("Pindah akaun berjaya!", Me.Page, Me.[GetType]())
        End If



    End Sub

End Class