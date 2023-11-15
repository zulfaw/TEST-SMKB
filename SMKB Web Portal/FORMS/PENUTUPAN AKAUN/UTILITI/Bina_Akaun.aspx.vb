Imports System.Data.SqlClient
Imports System.Drawing

Public Class Bina_Akaun
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            fBindDdlKW()
            fBindDdlPTj()
            fBindDdlKO()
            fBindDdlKP()
            fBindDdlVAm()

            sClearGvObjSbg()
            sClearGvObjSbg2()

        End If

    End Sub

    Private Sub fBindDdlKW()

        Try

            Dim strSql As String = "Select KodKw,(KodKw + ' - ' + Butiran ) as Butiran from MK_Kw order by KodKw"
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn

            ds = dbconn.fSelectCommand(strSql)

            ddlKW1.DataSource = ds
            ddlKW1.DataTextField = "Butiran"
            ddlKW1.DataValueField = "KodKw"
            ddlKW1.DataBind()
            ddlKW1.Items.Insert(0, New ListItem("- SILA PILIH -", "0"))
            ddlKW1.SelectedIndex = 0

        Catch ex As Exception

        End Try

    End Sub
    Private Function fBindDdlVAm()

        Try

            Dim strSql As String = "select KodVot, (KodVot +' - '+ Butiran ) as Butiran from MK_Vot where Klasifikasi = 'H1' order by KodVot"
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn

            ds = dbconn.fSelectCommand(strSql)

            ddlVotAm.DataSource = ds
            ddlVotAm.DataTextField = "Butiran"
            ddlVotAm.DataValueField = "KodVot"
            ddlVotAm.DataBind()

            ddlVotAm.Items.Insert(0, New ListItem("- SILA PILIH -", "0"))
            ddlVotAm.SelectedIndex = 0

        Catch ex As Exception

        End Try

    End Function


    Private Sub fLoadObjSbg1(ByVal strObjAM As String)
        Dim dt As New DataTable
        Try
            sClearGvObjSbg()
            dt.Columns.AddRange(New DataColumn() {
                                New DataColumn("KodVot", GetType(String)),
                                New DataColumn("Butiran", GetType(String))
                                })

            Dim strSql As String = "select KodVot,(KodVot + ' - ' + Butiran) as Butiran from MK_Vot where LEFT (KodVot , 1) = '" & strObjAM & "' and Klasifikasi = 'H2' order by KodVot"
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn

            ds = dbconn.fSelectCommand(strSql)

            'If Not ds Is Nothing Then
            '    If ds.Tables(0).Rows.Count > 0 Then
            '        gvObjSbg1.DataSource = ds
            '        gvObjSbg1.DataBind()
            '    End If
            'End If

            ddlVotSbg.DataSource = ds
            ddlVotSbg.DataTextField = "Butiran"
            ddlVotSbg.DataValueField = "KodVot"
            ddlVotSbg.DataBind()

            ddlVotSbg.Items.Insert(0, New ListItem("- SILA PILIH -", "0"))
            ddlVotSbg.SelectedIndex = 0


        Catch ex As Exception

        End Try
    End Sub

    Private Sub sClearGvObjSbg()
        gvObjSbg1.DataSource = New List(Of String)
        gvObjSbg1.DataBind()
    End Sub

    Private Sub sClearGvObjSbg2()
        gvObjSbg2.DataSource = New List(Of String)
        gvObjSbg2.DataBind()
        ViewState("dtProses") = Nothing
    End Sub



    Private Function fBindDdlPTj()

        Try

            Dim strSql As String = "select KodPTJ , (KodPTJ + ' - ' + Butiran ) as Butiran from mk_ptj where kodptj <> '-'  order by kodptj"
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn

            ds = dbconn.fSelectCommand(strSql)

            ddlPTj1.DataSource = ds
            ddlPTj1.DataTextField = "Butiran"
            ddlPTj1.DataValueField = "KodPTJ"
            ddlPTj1.DataBind()

            ddlPTj1.Items.Insert(0, New ListItem("- SILA PILIH -", "0"))
            ddlPTj1.SelectedIndex = 0



        Catch ex As Exception

        End Try

    End Function

    Private Sub fBindDdlKO()
        Try
            Dim strSql As String = "Select KodKO, (KodKO + ' - ' + Butiran) AS Butiran from MK_KodOperasi order by KodKO"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            ddlKO1.DataSource = ds
            ddlKO1.DataTextField = "Butiran"
            ddlKO1.DataValueField = "KodKO"
            ddlKO1.DataBind()
            ddlKO1.Items.Insert(0, New ListItem("- SILA PILIH -", "0"))
            ddlKO1.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Private Sub fBindDdlKP()

        Try
            Dim strSql As String = "select KodProjek , (KodProjek + ' - ' + Butiran) as Butiran from mk_KodProjek"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            ddlKP1.DataSource = ds
            ddlKP1.DataTextField = "Butiran"
            ddlKP1.DataValueField = "KodProjek"
            ddlKP1.DataBind()
            ddlKP1.Items.Insert(0, New ListItem("- SILA PILIH -", "0"))
            ddlKP1.SelectedIndex = 0


        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnProses_Click(sender As Object, e As EventArgs) Handles btnProses.Click
        Try

            If gvObjSbg2.Rows.Count = 0 Then
                fGlobalAlert("Pilih Objek Sebagai yang ingin dibina dalam COA!", Me.Page, Me.[GetType]())
                Exit Sub
            End If


            btnProses.Enabled = False
            System.Threading.Thread.Sleep(5000)
            'fLoadKW()
            'fLoadKO()
            'fLoadPTj()
            'fLoadKP()
            'fLoadVot()
            fCreateCoA()

            btnProses.Enabled = True
        Catch ex As Exception

        End Try
    End Sub
    '  Private Sub fLoadKW()
    '      Try
    '          Dim ds As New DataSet
    '          Dim dt As New DataTable
    '          Dim strSql As String = "Select KodKw, Butiran from MK_Kw
    'WHERE KodKw >='" & ddlKW1.SelectedValue & "' and KodKw <= '" & ddlKW2.SelectedValue & "'
    '   ORDER BY KodKw ASC"

    '          Dim dbconn As New DBKewConn
    '          ds = dbconn.fselectCommand(strSql)
    '          dt = ds.Tables(0)
    '          ViewState("dtKW") = dt
    '      Catch ex As Exception

    '      End Try

    '  End Sub

    '  Private Sub fLoadKO()
    '      Try
    '          Dim ds As New DataSet
    '          Dim dt As New DataTable

    '          Dim strSql As String = "Select KodKO, Butiran from MK_KodOperasi
    'where KodKO  > = '" & ddlKO1.SelectedValue & "' and KodKO <= '" & ddlKO2.SelectedValue & "' order by KodKO asc"

    '          Dim dbconn As New DBKewConn
    '          ds = dbconn.fselectCommand(strSql)
    '          dt = ds.Tables(0)
    '          ViewState("dtKO") = dt
    '      Catch ex As Exception

    '      End Try

    '  End Sub

    '  Private Sub fLoadPTj()
    '      Try
    '          Dim ds As New DataSet
    '          Dim dt As New DataTable
    '          Dim strSql As String = "Select KodPTJ, Butiran from mk_ptj
    'where KodPTJ  > = '" & ddlPTj1.SelectedValue & "' and KodPTJ <= '" & ddlPTj2.SelectedValue & "' order by KodPTJ asc"

    '          Dim dbconn As New DBKewConn
    '          ds = dbconn.fselectCommand(strSql)
    '          dt = ds.Tables(0)
    '          ViewState("dtPTj") = dt
    '      Catch ex As Exception

    '      End Try

    '  End Sub

    '  Private Sub fLoadKP()
    '      Try
    '          Dim ds As New DataSet
    '          Dim dt As New DataTable
    '          Dim strSql As String = "Select KodProjek, Butiran from mk_KodProjek
    'where KodProjek  > = '" & ddlKP1.SelectedValue & "' and KodProjek <= '" & ddlKP2.SelectedValue & "' order by KodProjek asc"

    '          Dim dbconn As New DBKewConn
    '          ds = dbconn.fselectCommand(strSql)
    '          dt = ds.Tables(0)
    '          ViewState("dtKP") = dt
    '      Catch ex As Exception

    '      End Try

    '  End Sub

    'Private Sub fLoadVot()
    '    Try
    '        Dim ds As New DataSet
    '        Dim dt As New DataTable
    '        Dim strSql As String = "Select KodVot, Butiran from MK_Vot 
    ' WHERE KodVot >='" & Mid(ddlVot1.SelectedValue, 1, 5) & "' and KodVot <= '" & Mid(ddlVot2.SelectedValue, 1, 5) & "'
    ' ORDER BY KodVot ASC"

    '        Dim dbconn As New DBKewConn
    '        ds = dbconn.fselectCommand(strSql)
    '        dt = ds.Tables(0)
    '        ViewState("dtVot") = dt
    '    Catch ex As Exception

    '    End Try

    'End Sub

    Private Function fCreateCoA() As String
        Try
            Dim strKodKW As String
            Dim strKodKO As String
            Dim strKodPTj As String
            Dim strKodKP As String
            Dim strKodVot As String
            Dim strNoStaf As String = Session("ssusrID")

            Dim dt As New DataTable
            dt.Columns.Add("KodKW", GetType(String))
            dt.Columns.Add("KodKO", GetType(String))
            dt.Columns.Add("KodPTj", GetType(String))
            dt.Columns.Add("KodKP", GetType(String))
            dt.Columns.Add("KodVot", GetType(String))
            dt.Columns.Add("NoStaf", GetType(String))


            'Dim dtKW As New DataTable
            'Dim dtKO As New DataTable
            'Dim dtPTj As New DataTable
            'Dim dtKP As New DataTable
            'Dim dtVot As New DataTable

            'dtKW = ViewState("dtKW")
            'dtKO = ViewState("dtKO")
            'dtPTj = ViewState("dtPTj")
            'dtKP = ViewState("dtKP")
            'dtVot = ViewState("dtVot")

            'For i = 0 To dtKW.Rows.Count - 1
            '    For j = 0 To dtKO.Rows.Count - 1
            '        For k = 0 To dtPTj.Rows.Count - 1
            '            For l = 0 To dtKP.Rows.Count - 1
            '                For m = 0 To dtVot.Rows.Count - 1

            '                    strKodKW = dtKW.Rows(i).Item("KodKw")
            '                    strKodKO = dtKO.Rows(j).Item("KodKO")
            '                    strKodPTj = dtPTj.Rows(k).Item("KodPTJ")
            '                    strKodKP = dtKP.Rows(l).Item("KodProjek")
            '                    strKodVot = dtVot.Rows(m).Item("KodVot")

            '                    dt.Rows.Add(strKodKW, strKodKO, strKodPTj, strKodKP, strKodVot, strNoStaf)
            '                Next
            '            Next
            '        Next
            '    Next
            'Next

            For Each gvRow As GridViewRow In gvObjSbg2.Rows
                strKodKW = Trim(ddlKW1.SelectedValue.TrimEnd)
                strKodKO = Trim(ddlKO1.SelectedValue.TrimEnd)
                strKodPTj = Trim(ddlPTj1.SelectedValue.TrimEnd)
                strKodKP = Trim(ddlKP1.SelectedValue.TrimEnd)
                strKodVot = TryCast(gvRow.FindControl("lblKodVot"), Label).Text
                dt.Rows.Add(strKodKW, strKodKO, strKodPTj, strKodKP, strKodVot, strNoStaf)
            Next

            If fProses(dt) Then
                fGlobalAlert("Carta akaun telah dibina!", Me.Page, Me.[GetType]())
                sClear()
            Else
                fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
            End If

            ''Bina akaun guna stored proc
            'fClearRec(strNoStaf)
            'If fInsertRec(dt) = True Then
            '    Dim strMsg As String = fExecSP()

            '    If strMsg = "1" Then
            '        clsAlert.Alert(lblMsg, "Carta akaun telah dibina!", clsAlert.AlertType.Success, True)
            '        sClear()
            '    Else
            '        clsAlert.Alert(lblMsg, strMsg, clsAlert.AlertType.Err, False)
            '    End If

            'Else
            '    clsAlert.Alert(lblMsg, "Ralat!", clsAlert.AlertType.Err, False)
            'End If

        Catch ex As Exception

        End Try
    End Function

    Private Sub sClear()
        sClearGvObjSbg()
        sClearGvObjSbg2()

        ddlKW1.SelectedIndex = 0
        ddlKO1.SelectedIndex = 0
        ddlPTj1.SelectedIndex = 0
        ddlKP1.SelectedIndex = 0
        ddlVotAm.SelectedIndex = 0
        ddlVotSbg.SelectedIndex = 0
    End Sub

    Private Function fProses(ByVal dt As DataTable) As Boolean
        Dim strSql As String
        Dim dbconn As New DBKewConn
        Dim strTahun As String = Now.Year
        Dim strBulan As String = Now.Month
        Dim blnSuccess As Boolean = True

        Try
            dbconn.sConnBeginTrans()
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim strKodKW As String = dt.Rows(i)("KodKw").ToString
                Dim strKodKO As String = dt.Rows(i)("KodKO").ToString
                Dim strKodPTj As String = dt.Rows(i)("KodPTJ").ToString
                Dim strKodKP As String = dt.Rows(i)("KodKP").ToString
                Dim strKodVot As String = dt.Rows(i)("KodVot").ToString

                'kes pilih vot lanjut
                If strKodVot.Substring(2, 1) <> "0" Then
                    'simpan dlm MK03_AmTahun & MK04_AmBulan

                    strSql = "select count(*) from MK03_AmTahun with (Nolock) 
                WHERE KodKw = '" & strKodKW & "' AND KodKO = '" & strKodKO & "' AND KodPTJ = '" & strKodPTj & "' AND KodKP = '" & strKodKP & "' AND KodVot = '" & strKodVot & "' AND MK03_Tahun = '" & strTahun & "'"

                    If fCheckRec(strSql) = 0 Then
                        'INSERT INTO MK03_AmTahun
                        strSql = "INSERT INTO MK03_AmTahun (KodKw,KodKO, KodPTJ,KodKP, KodVot, MK03_Tahun, MK03_Debit, MK03_Kredit)
                				VALUES(@KodKw,@KodKO, @KodPTJ,@KodKP, @KodVot, @iTahun, 0, 0)"

                        Dim paramSql3() As SqlParameter = {
                                    New SqlParameter("@KodKw", strKodKW),
                                    New SqlParameter("@KodKO", strKodKO),
                                    New SqlParameter("@KodPTJ", strKodPTj),
                                    New SqlParameter("@KodKP", strKodKP),
                                    New SqlParameter("@KodVot", strKodVot),
                                    New SqlParameter("@iTahun", strTahun)
                                    }

                        If Not dbconn.fInsertCommand(strSql, paramSql3) > 0 Then
                            blnSuccess = False
                            Exit Try
                        End If
                    End If

                    strSql = "select count(*) from MK04_AmBulan with (Nolock) 
                WHERE KodKw = '" & strKodKW & "' AND KodKO = '" & strKodKO & "' AND KodPTJ = '" & strKodPTj & "' AND KodKP = '" & strKodKP & "' AND KodVot = '" & strKodVot & "' AND MK04_Tahun = '" & strTahun & "' and MK04_Bulan= '" & strBulan & "'"

                    If fCheckRec(strSql) = 0 Then
                        'INSERT INTO MK04_AmBulan
                        strSql = "INSERT INTO MK04_AmBulan (KodKw,KodKO, KodPTJ,KodKP, KodVot, MK04_Tahun, MK04_Bulan, MK04_Debit, MK04_Kredit)
                				VALUES(@KodKw,@KodKO, @KodPTJ,@KodKP, @KodVot, @iTahun, @iBulan, 0, 0)"

                        Dim paramSql4() As SqlParameter = {
                                    New SqlParameter("@KodKw", strKodKW),
                                    New SqlParameter("@KodKO", strKodKO),
                                    New SqlParameter("@KodPTJ", strKodPTj),
                                    New SqlParameter("@KodKP", strKodKP),
                                    New SqlParameter("@KodVot", strKodVot),
                                    New SqlParameter("@iTahun", strTahun),
                                    New SqlParameter("@iBulan", strBulan)
                                    }

                        If Not dbconn.fInsertCommand(strSql, paramSql4) > 0 Then
                            blnSuccess = False
                            Exit Try
                        End If
                    End If



                    '--SIMPAN DLM MK01_VOTTahun bg kes user pilih detail vot shj

                    '-->H3
                    Dim strKodVotH3 = strKodVot.Substring(0, 3) + "00"

                    strSql = "select count(*) from MK03_AmTahun with (Nolock) 
                WHERE KodKw = '" & strKodKW & "' AND KodKO = '" & strKodKO & "' AND KodPTJ = '" & strKodPTj & "' AND KodKP = '" & strKodKP & "' AND KodVot = '" & strKodVotH3 & "' AND MK03_Tahun = '" & strTahun & "'"

                    If fCheckRec(strSql) = 0 Then
                        'INSERT INTO MK03_AmTahun-->H3
                        strSql = "INSERT INTO MK03_AmTahun (KodKw, KodKO, KodPTJ,KodKP, KodVot, MK03_Tahun, MK03_Debit, MK03_Kredit)
                					VALUES(@KodKw,@KodKO, @KodPTJ,@KodKP, @KodVotHdr, @iTahun, 0, 0)"

                        Dim paramSql5() As SqlParameter = {
                                    New SqlParameter("@KodKw", strKodKW),
                                    New SqlParameter("@KodKO", strKodKO),
                                    New SqlParameter("@KodPTJ", strKodPTj),
                                    New SqlParameter("@KodKP", strKodKP),
                                    New SqlParameter("@KodVotHdr", strKodVotH3),
                                    New SqlParameter("@iTahun", strTahun)
                                    }

                        If Not dbconn.fInsertCommand(strSql, paramSql5) > 0 Then
                            blnSuccess = False
                            Exit Try
                        End If
                    End If


                    strSql = "select count(*) from MK04_AmBulan with (Nolock) 
                WHERE KodKw = '" & strKodKW & "' AND KodKO = '" & strKodKO & "' AND KodPTJ = '" & strKodPTj & "' AND KodKP = '" & strKodKP & "' AND KodVot = '" & strKodVotH3 & "' AND MK04_Tahun = '" & strTahun & "' and MK04_Bulan='" & strBulan & "'"

                    If fCheckRec(strSql) = 0 Then
                        'INSERT INTO MK04_AmBulan-->H3
                        strSql = "INSERT INTO MK04_AmBulan (KodKw,KodKO, KodPTJ,KodKP, KodVot, MK04_Tahun, MK04_Bulan, MK04_Debit, MK04_Kredit)
                					VALUES(@KodKw,@KodKO, @KodPTJ,@KodKP, @KodVotHdr, @iTahun, @iBulan, 0, 0)"

                        Dim paramSql6() As SqlParameter = {
                                    New SqlParameter("@KodKw", strKodKW),
                                    New SqlParameter("@KodKO", strKodKO),
                                    New SqlParameter("@KodPTJ", strKodPTj),
                                    New SqlParameter("@KodKP", strKodKP),
                                    New SqlParameter("@KodVotHdr", strKodVotH3),
                                    New SqlParameter("@iTahun", strTahun),
                                    New SqlParameter("@iBulan", strBulan)
                                    }

                        If Not dbconn.fInsertCommand(strSql, paramSql6) > 0 Then
                            blnSuccess = False
                            Exit Try
                        End If
                    End If


                    '-->H2
                    Dim strKodVotH2 As String = strKodVot.Substring(0, 2) + "000"

                    strSql = "select count(*) from MK01_VotTahun with (Nolock) 
                WHERE KodKw = '" & strKodKW & "' AND KodKO = '" & strKodKO & "' AND KodPTJ = '" & strKodPTj & "' AND KodKP = '" & strKodKP & "' AND KodVot = '" & strKodVotH2 & "' AND MK01_Tahun = '" & strTahun & "'"

                    If fCheckRec(strSql) = 0 Then
                        '--MK01_VotTahun-->H2
                        strSql = "INSERT INTO MK01_VotTahun (KodKw,KodKO, KodPTJ,KodKP, KodVot, MK01_Tahun, MK01_Perutk, MK01_BljYtd, 
                					MK01_TngYtd, MK01_BakiTng, MK01_PemLulus, MK01_BakiSms, MK01_BakiPerutk, MK01_BakiSlpsPemLulus,
                					MK01_VirMasuk, MK01_VirKeluar, MK01_Status)
                					VALUES(@KodKw,@KodKO, @KodPTJ,@KodKP, @KodVotHdr, @iTahun, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1)"

                        Dim paramSql7() As SqlParameter = {
                                    New SqlParameter("@KodKw", strKodKW),
                                    New SqlParameter("@KodKO", strKodKO),
                                    New SqlParameter("@KodPTJ", strKodPTj),
                                    New SqlParameter("@KodKP", strKodKP),
                                    New SqlParameter("@KodVotHdr", strKodVotH2),
                                    New SqlParameter("@iTahun", strTahun)
                                    }

                        If Not dbconn.fInsertCommand(strSql, paramSql7) > 0 Then
                            blnSuccess = False
                            Exit Try
                        End If
                    End If

                    strSql = "select count(*) from MK02_VotBulan with (Nolock) 
                WHERE KodKw = '" & strKodKW & "' AND KodKO = '" & strKodKO & "' AND KodPTJ = '" & strKodPTj & "' AND KodKP = '" & strKodKP & "' AND KodVot = '" & strKodVotH2 & "' AND MK02_Tahun = '" & strTahun & "' and MK02_Bulan='" & strBulan & "'"

                    If fCheckRec(strSql) = 0 Then
                        '--MK02_VotBulan-->H2
                        strSql = "INSERT INTO MK02_VotBulan (KodKw,KodKO, KodPTJ,KodKP, KodVot, MK02_Bulan, MK02_Tahun, MK02_Perutk, 
                					MK02_BljYtd, MK02_BljMtd, MK02_TngYtd, MK02_TngMtd, MK02_BakiTng, MK02_BakiSms, MK02_BakiPerutk)
                					VALUES(@KodKw,@KodKO, @KodPTJ,@KodKP, @KodVotHdr, @iBulan, @iTahun, 0, 0, 0, 0, 0, 0, 0, 0)"

                        Dim paramSql8() As SqlParameter = {
                                    New SqlParameter("@KodKw", strKodKW),
                                    New SqlParameter("@KodKO", strKodKO),
                                    New SqlParameter("@KodPTJ", strKodPTj),
                                    New SqlParameter("@KodKP", strKodKP),
                                    New SqlParameter("@KodVotHdr", strKodVotH2),
                                    New SqlParameter("@iTahun", strTahun),
                                    New SqlParameter("@iBulan", strBulan)
                                    }

                        If Not dbconn.fInsertCommand(strSql, paramSql8) > 0 Then
                            blnSuccess = False
                            Exit Try
                        End If
                    End If




                    '-->H1
                    Dim strKodVotH1 As String = strKodVot.Substring(0, 1) + "0000"

                    If strKodVotH1.Substring(0, 1) <> "1" Then
                        Dim a
                        a = "aaa"
                    End If

                    strSql = "select count(*) from MK01_VotTahun with (Nolock) 
                WHERE KodKw = '" & strKodKW & "' AND KodKO = '" & strKodKO & "' AND KodPTJ = '" & strKodPTj & "' AND KodKP = '" & strKodKP & "' AND KodVot = '" & strKodVotH1 & "' AND MK01_Tahun = '" & strTahun & "'"

                    If fCheckRec(strSql) = 0 Then
                        '--MK01_VotTahun-->H1
                        strSql = "INSERT INTO MK01_VotTahun (KodKw,KodKO, KodPTJ,KodKP, KodVot, MK01_Tahun, MK01_Perutk, MK01_BljYtd, 
                					MK01_TngYtd, MK01_BakiTng, MK01_PemLulus, MK01_BakiSms, MK01_BakiPerutk, MK01_BakiSlpsPemLulus,
                					MK01_VirMasuk, MK01_VirKeluar, MK01_Status)
                					VALUES(@KodKw,@KodKO, @KodPTJ,@KodKP, @KodVotHdr, @iTahun, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1)"

                        Dim paramSql9() As SqlParameter = {
                                    New SqlParameter("@KodKw", strKodKW),
                                    New SqlParameter("@KodKO", strKodKO),
                                    New SqlParameter("@KodPTJ", strKodPTj),
                                    New SqlParameter("@KodKP", strKodKP),
                                    New SqlParameter("@KodVotHdr", strKodVotH1),
                                    New SqlParameter("@iTahun", strTahun)
                                    }

                        If Not dbconn.fInsertCommand(strSql, paramSql9) > 0 Then
                            blnSuccess = False
                            Exit Try
                        End If
                    End If

                    strSql = "select count(*) from MK02_VotBulan with (Nolock) 
                WHERE KodKw = '" & strKodKW & "' AND KodKO = '" & strKodKO & "' AND KodPTJ = '" & strKodPTj & "' AND KodKP = '" & strKodKP & "' AND KodVot = '" & strKodVotH1 & "' AND MK02_Tahun = '" & strTahun & "' and MK02_Bulan='" & strBulan & "'"

                    If fCheckRec(strSql) = 0 Then
                        '--MK02_VotBulan-->H1
                        strSql = "INSERT INTO MK02_VotBulan (KodKw,KodKO, KodPTJ,KodKP, KodVot, MK02_Bulan, MK02_Tahun, MK02_Perutk, 
                					MK02_BljYtd, MK02_BljMtd, MK02_TngYtd, MK02_TngMtd, MK02_BakiTng, MK02_BakiSms, MK02_BakiPerutk)
                					VALUES(@KodKw,@KodKO, @KodPTJ,@KodKP, @KodVotHdr, @iBulan, @iTahun, 0, 0, 0, 0, 0, 0, 0, 0)"

                        Dim paramSql10() As SqlParameter = {
                                    New SqlParameter("@KodKw", strKodKW),
                                    New SqlParameter("@KodKO", strKodKO),
                                    New SqlParameter("@KodPTJ", strKodPTj),
                                    New SqlParameter("@KodKP", strKodKP),
                                    New SqlParameter("@KodVotHdr", strKodVotH1),
                                    New SqlParameter("@iTahun", strTahun),
                                    New SqlParameter("@iBulan", strBulan)
                                    }

                        If Not dbconn.fInsertCommand(strSql, paramSql10) > 0 Then
                            blnSuccess = False
                            Exit Try
                        End If
                    End If


                End If
            Next

        Catch ex As Exception
            blnSuccess = False
        End Try


        If blnSuccess Then
            dbconn.sConnCommitTrans()
            Return True
        Else
            dbconn.sConnRollbackTrans()
            Return False
        End If


    End Function




    Private Sub fClearRec(ByVal strNoStaf As String)
        Try
            Dim strSql As String = "DELETE FROM TMP_BINA_AKAUN WHERE NOSTAF = '" & strNoStaf & "'"
            Dim dbConn As New DBKewConn
            dbConn.sConnBeginTrans()
            If dbConn.fUpdateCommand(strSql) > 0 Then
                dbConn.sConnCommitTrans()
            Else
                dbConn.sConnRollbackTrans()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Function fInsertRec(ByVal dt As DataTable) As Boolean
        Try

            'Dim dbConn As New DBKewConn
            'If dbConn.fInsertBulk("dbo.TMP_Bina_Akaun", dt) = True Then
            '    Return True
            'Else
            '    Return False
            'End If
            Dim strKodKW, strKodKO, strKodPTj, strKodKP, strkodvot, strNoStaf As String
            Dim strsql As String
            Dim dbconn As New DBKewConn
            For i As Integer = 0 To dt.Rows.Count - 1
                strKodKW = dt.Rows(i).Item("KodKW")
                strKodKO = dt.Rows(i).Item("KodKO")
                strKodPTj = dt.Rows(i).Item("KodPTj")
                strKodKP = dt.Rows(i).Item("KodKP")
                strkodvot = dt.Rows(i).Item("KodVot")
                strNoStaf = dt.Rows(i).Item("NoStaf")

                strsql = "insert into TMP_Bina_Akaun (KodKW ,KodKO , KodPTj , KodKP , KodVot , NoStaf ) " &
                    "values (@KodKW, @KodKO, @KodPTj, @KodKP, @KodVot, @NoStaf)"

                Dim paramSql() As SqlParameter = {
                    New SqlParameter("@KodKW", strKodKW),
                    New SqlParameter("@KodKO", strKodKO),
                    New SqlParameter("@KodPTj", strKodPTj),
                    New SqlParameter("@KodKP", strKodKP),
                    New SqlParameter("@KodVot", strkodvot),
                    New SqlParameter("@NoStaf", strNoStaf)}

                dbconn.sConnBeginTrans()
                If dbconn.fInsertCommand(strsql, paramSql) > 0 Then
                    dbconn.sConnCommitTrans()
                Else
                    dbconn.sConnRollbackTrans()
                    Return False
                End If
            Next

            Return True

        Catch ex As Exception

        End Try
    End Function

    Private Function fExecSP() As String
        Dim dbconn As New DBKewConn

        Dim strNoStaf As String = Session("ssusrID")
        Dim dtBulan As Integer = DateTime.Now.Month()
        Dim dtTahun As String = DateTime.Now.Year
        Dim strOutput As String
        Try
            Dim paramSql() As SqlParameter = {
                                New SqlParameter("@iBulan", dtBulan),
                New SqlParameter("@iTahun", dtTahun),
                New SqlParameter("@NoStaf", strNoStaf),
                 New SqlParameter("@Err", "")
                                }

            If dbconn.fSP_Insert("USP_BINA_AKAUN", paramSql, strOutput) > 0 Then
                If strOutput = "1" Then
                    Return "1"
                Else
                    Return "Kod Akaun Telah wujud!"
                End If
            Else
                Return "Proses bina Kod Akaun gagal!"
            End If

        Catch ex As Exception

        End Try
    End Function



    ''' <summary>
    ''' Load kodvot yang telah wujud dalam COA
    ''' </summary>
    ''' <param name="strVot"></param>
    Private Sub sLoadVot(ByVal strVot As String)
        Try
            'Semak vot yang sudah wujud dalam COA
            'Dim strSql As String = "select KodVot from MK01_VotTahun where MK01_Tahun = '" & Now.Year & "' and KodKw = '" & Trim(ddlKW1.SelectedValue.TrimEnd) & "' and KodKO = '" & Trim(ddlKO1.SelectedValue.TrimEnd) & "' and KodPTJ = '" & Trim(ddlPTj1.SelectedValue.TrimEnd) & "' and KodKP = '" & Trim(ddlKP1.SelectedValue.TrimEnd) & "' and LEFT (KodVot , 1) = '" & strVot & "' and MK01_Status = 1"

            Dim strSql As String = "select MK03_Tahun as Tahun, kodkw, KodPTJ, KodVot  from MK03_AmTahun where MK03_Tahun = '" & Now.Year & "' and kodkw = '" & Trim(ddlKW1.SelectedValue.TrimEnd) & "' and KodKO = '" & Trim(ddlKO1.SelectedValue.TrimEnd) & "' and KodPTJ = '" & Trim(ddlPTj1.SelectedValue.TrimEnd) & "' and KodKP = '" & Trim(ddlKP1.SelectedValue.TrimEnd) & "' and LEFT (KodVot , 2) = '" & strVot & "'"

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn

            ds = dbconn.fSelectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    For Each gvRow As GridViewRow In gvObjSbg1.Rows
                        Dim chkSel As CheckBox = DirectCast(gvRow.FindControl("cbSelect"), CheckBox)
                        Dim btnChecked As LinkButton = DirectCast(gvRow.FindControl("btnChecked"), LinkButton)
                        Dim strKodVot As String = TryCast(gvRow.FindControl("lblKodVot"), Label).Text

                        Dim answer As String = ""
                        Dim foundRows() As Data.DataRow

                        foundRows = ds.Tables(0).Select("KodVot = '" & strKodVot & "' ")
                        answer = ""

                        If foundRows.Count > 0 Then
                            chkSel.Visible = False
                            chkSel.Checked = False
                            btnChecked.Visible = True
                            gvRow.ForeColor = ColorTranslator.FromHtml("#5a5a5a") ' Drawing.Color.Blue
                        Else
                            chkSel.Visible = True
                            btnChecked.Visible = False
                            gvRow.ForeColor = Drawing.Color.Black
                        End If
                    Next
                End If
            End If

            'Semak vot yang sudah dipilih dalam senarai
            If gvObjSbg2.Rows.Count > 0 Then
                For Each gvRow As GridViewRow In gvObjSbg1.Rows
                    Dim strKodvot1 As String = TryCast(gvRow.FindControl("lblKodVot"), Label).Text
                    Dim chkSel As CheckBox = DirectCast(gvRow.FindControl("cbSelect"), CheckBox)

                    For Each gvRow2 As GridViewRow In gvObjSbg2.Rows
                        Dim strKodvot2 As String = TryCast(gvRow2.FindControl("lblKodVot"), Label).Text

                        If strKodvot1 = strKodvot2 Then
                            chkSel.Enabled = False
                            chkSel.Checked = True
                            gvRow.ForeColor = Drawing.Color.Blue
                        End If
                    Next
                Next
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub lBtnAdd_Click(sender As Object, e As EventArgs) Handles lBtnAdd.Click
        Try
            Dim dt As New DataTable
            If ViewState("dtProses") Is Nothing Then
                dt = fSetDt()
            Else
                dt = TryCast(ViewState("dtProses"), DataTable)
            End If

            For Each gvRow As GridViewRow In gvObjSbg1.Rows
                Dim chkSel As CheckBox = DirectCast(gvRow.FindControl("cbSelect"), CheckBox)
                If chkSel.Checked = True Then
                    Dim strKodvot As String = TryCast(gvRow.FindControl("lblKodVot"), Label).Text
                    Dim strButiran As String = Server.HtmlDecode(TryCast(gvRow.FindControl("lblButiran"), Label).Text)

                    Dim foundRows As DataRow()
                    foundRows = dt.Select("KodVot='" & strKodvot & "'")

                    If foundRows.Count = 0 Then
                        dt.Rows.Add(strKodvot, strButiran)

                        chkSel.Enabled = False
                        gvRow.ForeColor = Drawing.Color.Blue
                    End If
                End If
            Next

            Dim dataView As New DataView(dt)
            dataView.Sort = " KodVot ASC"
            dt = dataView.ToTable()

            gvObjSbg2.DataSource = dt
            gvObjSbg2.DataBind()
            ViewState("dtProses") = dt
        Catch ex As Exception

        End Try
    End Sub

    Private Function fSetDt() As DataTable
        Try
            Dim dt As New DataTable
            dt.Columns.AddRange(New DataColumn() {
                            New DataColumn("KodVot", GetType(String)),
                            New DataColumn("Butiran", GetType(String))
                                })
            Return dt
        Catch ex As Exception

        End Try

    End Function


    Private Sub gvObjSbg2_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvObjSbg2.RowDeleting
        Try
            Dim index As Integer = Convert.ToInt32(e.RowIndex)
            Dim selectedRow As GridViewRow = gvObjSbg2.Rows(index)

            Dim strKodvot1 As String = Trim(CType(selectedRow.FindControl("lblKodVot"), Label).Text.TrimEnd)

            Dim dt As New DataTable
            If Not ViewState("dtProses") Is Nothing Then
                dt = TryCast(ViewState("dtProses"), DataTable)
            Else
                Exit Sub
            End If

            Dim foundRows As DataRow()
            foundRows = dt.Select("KodVot='" & strKodvot1 & "'")
            If foundRows.Count > 0 Then
                foundRows(0).Delete()

                For Each gvRow As GridViewRow In gvObjSbg1.Rows
                    Dim chkSel As CheckBox = DirectCast(gvRow.FindControl("cbSelect"), CheckBox)
                    If chkSel.Checked = True Then
                        Dim strKodvot2 As String = TryCast(gvRow.FindControl("lblKodVot"), Label).Text

                        If strKodvot2 = strKodvot1 Then
                            chkSel.Enabled = True
                            chkSel.Checked = False
                            gvRow.ForeColor = Drawing.Color.Black
                        End If
                    End If
                Next

                gvObjSbg2.DataSource = dt
                gvObjSbg2.DataBind()

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ddlKW1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKW1.SelectedIndexChanged
        ddlKO1.SelectedIndex = 0
        ddlPTj1.SelectedIndex = 0
        ddlKP1.SelectedIndex = 0
        ddlVotAm.SelectedIndex = 0
        sClearGvObjSbg2()
        sClearGvObjSbg()
    End Sub

    Private Sub ddlKO1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKO1.SelectedIndexChanged

        ddlPTj1.SelectedIndex = 0
        ddlKP1.SelectedIndex = 0
        ddlVotAm.SelectedIndex = 0
        sClearGvObjSbg2()
        sClearGvObjSbg()
    End Sub

    Private Sub ddlKP1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKP1.SelectedIndexChanged
        ddlVotAm.SelectedIndex = 0
        sClearGvObjSbg2()
        sClearGvObjSbg()
    End Sub

    Private Sub ddlPTj1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPTj1.SelectedIndexChanged
        ddlKP1.SelectedIndex = 0
        ddlVotAm.SelectedIndex = 0
        sClearGvObjSbg2()
        sClearGvObjSbg()
    End Sub

    Private Sub ddlVotAm_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlVotAm.SelectedIndexChanged
        Dim strVot As String = Trim(ddlVotAm.SelectedValue.Substring(0, 1))
        sClearGvObjSbg()
        fLoadObjSbg1(strVot)
        'sLoadVot(strVot)
    End Sub

    Private Function fBindDdlVSbg()

        Try

            Dim strSql As String = "select KodVot, (KodVot +' - '+ Butiran ) as Butiran from MK_Vot where Klasifikasi = 'H1' order by KodVot"
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn

            ds = dbconn.fSelectCommand(strSql)

            ddlVotAm.DataSource = ds
            ddlVotAm.DataTextField = "Butiran"
            ddlVotAm.DataValueField = "KodVot"
            ddlVotAm.DataBind()

            ddlVotAm.Items.Insert(0, New ListItem("- SILA PILIH -", "0"))
            ddlVotAm.SelectedIndex = 0

        Catch ex As Exception

        End Try

    End Function

    Private Sub fLoadObjLanjut(ByVal strObjSbg As String)
        Dim dt As New DataTable
        Try
            sClearGvObjSbg()
            dt.Columns.AddRange(New DataColumn() {
                                New DataColumn("KodVot", GetType(String)),
                                New DataColumn("Butiran", GetType(String))
                                })

            Dim strSql As String = "select KodVot,(KodVot + ' - ' + Butiran) as Butiran from MK_Vot where LEFT (KodVot , 2) = '" & strObjSbg & "' and Klasifikasi = 'D' order by KodVot"
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn

            ds = dbconn.fSelectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    gvObjSbg1.DataSource = ds
                    gvObjSbg1.DataBind()
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ddlVotSbg_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlVotSbg.SelectedIndexChanged

        Dim strVot As String = Trim(ddlVotSbg.SelectedValue.Substring(0, 2))
        sClearGvObjSbg()
        fLoadObjLanjut(strVot)
        sLoadVot(strVot)
    End Sub
End Class