Imports System.Data.SqlClient
Imports System.Threading

Public Class Bina_Akaun_T
    Inherits System.Web.UI.Page

    Dim dbconn As New DBKewConn
    Dim dtMK01 As New DataTable
    Dim dtMK02 As New DataTable
    Dim dtMK03 As New DataTable
    Dim dtMK04 As New DataTable
    Dim Tahun = Now.Year
    Dim Bulan = Now.Month

    Dim strSqlKP = "select KodProjek , Butiran from mk_KodProjek order by KodProjek;"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            fBindDdl()
        End If
    End Sub

    Private Sub fBindDdl()
        Dim strSqlKW = "Select KodKw,(KodKw + ' - ' + Butiran ) as Butiran from MK_Kw order by KodKw;"
        Dim strSqlKO = "Select KodKO, (KodKO + ' - ' + Butiran) AS Butiran from MK_KodOperasi order by KodKO;"
        Dim strSqlPTJ = "select KodPTJ , (KodPTJ + ' - ' + Butiran ) as Butiran from mk_ptj where kodptj <> '-'  order by kodptj;"
        Dim strSqlVOT = "select KodVot, (KodVot +' - '+ Butiran ) as Butiran from MK_Vot order by KodVot;"

        Using ds = dbconn.fSelectCommand(strSqlKW + strSqlKO + strSqlPTJ + strSqlKP + strSqlVOT)
            'Kw
            Using dt = ds.Tables(0)
                ddlKW1.DataSource = dt
                ddlKW1.DataTextField = "Butiran"
                ddlKW1.DataValueField = "KodKw"
                ddlKW1.DataBind()
                ddlKW1.Items.Insert(0, New ListItem("-SILA PILIH-", "0"))
                ddlKW1.SelectedIndex = 0

                ddlKW2.DataSource = dt
                ddlKW2.DataTextField = "Butiran"
                ddlKW2.DataValueField = "KodKw"
                ddlKW2.DataBind()
                ddlKW2.Items.Insert(0, New ListItem("-SILA PILIH-", "0"))
                ddlKW2.SelectedIndex = 0
            End Using

            'Ko
            Using dt = ds.Tables(1)
                ddlKO1.DataSource = dt
                ddlKO1.DataTextField = "Butiran"
                ddlKO1.DataValueField = "KodKO"
                ddlKO1.DataBind()
                ddlKO1.Items.Insert(0, New ListItem("-SILA PILIH-", "0"))
                ddlKO1.SelectedIndex = 0

                ddlKO2.DataSource = dt
                ddlKO2.DataTextField = "Butiran"
                ddlKO2.DataValueField = "KodKO"
                ddlKO2.DataBind()
                ddlKO2.Items.Insert(0, New ListItem("-SILA PILIH-", "0"))
                ddlKO2.SelectedIndex = 0
            End Using

            'PTJ
            Using dt = ds.Tables(2)
                ddlPTj1.DataSource = dt
                ddlPTj1.DataTextField = "Butiran"
                ddlPTj1.DataValueField = "KodPTJ"
                ddlPTj1.DataBind()

                ddlPTj1.Items.Insert(0, New ListItem("-SILA PILIH-", "0"))
                ddlPTj1.SelectedIndex = 0

                ddlPTj2.DataSource = dt
                ddlPTj2.DataTextField = "Butiran"
                ddlPTj2.DataValueField = "KodPTJ"
                ddlPTj2.DataBind()

                ddlPTj2.Items.Insert(0, New ListItem("-SILA PILIH-", "0"))
                ddlPTj2.SelectedIndex = 0
            End Using

            'Kp
            Using dt = ds.Tables(3)
                'ddlKP1.DataSource = dt
                'ddlKP1.DataTextField = "Butiran"
                'ddlKP1.DataValueField = "KodProjek"
                'ddlKP1.DataBind()
                'ddlKP1.Items.Insert(0, New ListItem("-SILA PILIH-", "0"))
                'ddlKP1.SelectedIndex = 0

                gvKP1.DataSource = dt
                gvKP1.DataBind()

                dt.AcceptChanges()
                ViewState("dtKP1") = dt
                ViewState("dtKP2") = dt.Clone

                fBindGvKP1()
                fBindGvKP2()
            End Using

            'Vot
            Using dt = ds.Tables(4)
                ddlVot1.DataSource = dt
                ddlVot1.DataTextField = "Butiran"
                ddlVot1.DataValueField = "KodVot"
                ddlVot1.DataBind()

                ddlVot1.Items.Insert(0, New ListItem("-SILA PILIH-", "0"))
                ddlVot1.SelectedIndex = 0

                ddlVot2.DataSource = dt
                ddlVot2.DataTextField = "Butiran"
                ddlVot2.DataValueField = "KodVot"
                ddlVot2.DataBind()

                ddlVot2.Items.Insert(0, New ListItem("-SILA PILIH-", "0"))
                ddlVot2.SelectedIndex = 0
            End Using
        End Using
    End Sub


    Private Sub btnProses_Click(sender As Object, e As EventArgs) Handles btnProses.Click
        btnProses.Enabled = False
        'Thread.Sleep(2000)

        fLoadALL()
        btnProses.Enabled = True
    End Sub
    Private Sub fLoadALL()
        Try
            Dim strMK01 = $"SELECT [ID],[KodKw],[KodKO],[KodPTJ],[KodKP],[KodVot],[MK01_Tahun] FROM [MK01_VotTahun] WHERE MK01_Tahun = {Tahun};"
            Dim strMK02 = $"SELECT [ID],[KodKw],[KodKO],[KodPTJ],[KodKP],[KodVot],[MK02_Bulan],[MK02_Tahun] FROM [MK02_VotBulan] WHERE MK02_Tahun={Tahun} AND MK02_Bulan={Bulan};"
            Dim strMK03 = $"SELECT [ID],[KodKw],[KodKO],[KodPTJ],[KodKP],[KodVot],[MK03_Tahun] FROM [MK03_AmTahun] WHERE MK03_Tahun={Tahun};"
            Dim strMK04 = $"SELECT [ID],[KodKw],[KodKO],[KodPTJ],[KodKP],[KodVot],[MK04_Bulan],[MK04_Tahun] FROM [MK04_AmBulan] WHERE MK04_Tahun={Tahun};"

            Using dsMK = dbconn.fSelectCommand(strMK01 + strMK02 + strMK03 + strMK04)
                dtMK01 = dsMK.Tables(0)
                dtMK02 = dsMK.Tables(1)
                dtMK03 = dsMK.Tables(2)
                dtMK04 = dsMK.Tables(3)
                dtMK01.AcceptChanges()
                dtMK02.AcceptChanges()
                dtMK03.AcceptChanges()
                dtMK04.AcceptChanges()
            End Using

            Dim strSqlKW As String = $"Select KodKw from MK_Kw WHERE KodKw >='{ ddlKW1.SelectedValue }' and KodKw <= '{ ddlKW2.SelectedValue }' ORDER BY KodKw ASC;"
            Dim strSqlKO As String = $"Select KodKO from MK_KodOperasi where KodKO  >= '{ ddlKO1.SelectedValue }' and KodKO <= '{ ddlKO2.SelectedValue}' order by KodKO asc;"
            Dim strSqlPTJ As String = $"Select KodPTJ from mk_ptj where KodPTJ  >= '{ ddlPTj1.SelectedValue }' and KodPTJ <= '{ ddlPTj2.SelectedValue }' order by KodPTJ asc;"

            Dim strKPBuilder As New StringBuilder
            Dim indx As Integer = 0
            Using dtKp2 = CType(ViewState("dtKP2"), DataTable)
                For Each drdt In dtKp2.Rows
                    indx += 1
                    strKPBuilder.Append("'" + drdt("KodProjek") + "'" + ",")

                    'las row, remove comma ','
                    If indx = dtKp2.Rows.Count Then
                        strKPBuilder.Remove(strKPBuilder.Length - 1, 1)
                    End If

                Next
            End Using

            Dim strSqlKP As String = $"Select KodProjek from mk_KodProjek where KodProjek  IN ({ strKPBuilder.ToString }) order by KodProjek asc;"
            Dim strSqlVot As String = $"Select KodVot from MK_Vot WHERE KodVot >='{ddlVot1.SelectedValue}' and KodVot <='{ ddlVot2.SelectedValue }' ORDER BY KodVot ASC;"

            Using dtNew As New DataTable
                dtNew.Columns.AddRange(New DataColumn() {
                                    New DataColumn("KodKW", GetType(String)),
                                    New DataColumn("KodKO", GetType(String)),
                                    New DataColumn("KodPTj", GetType(String)),
                                    New DataColumn("KodKP", GetType(String)),
                                    New DataColumn("KodVot", GetType(String))
                })

                Using ds = dbconn.fSelectCommand(strSqlKW + strSqlKO + strSqlPTJ + strSqlKP + strSqlVot)
                    Using dtkw = ds.Tables(0)
                        Using dtko = ds.Tables(1)
                            Using dtptj = ds.Tables(2)
                                Using dtkp = ds.Tables(3)
                                    Using dtvot = ds.Tables(4)
                                        For Each rkw In dtkw.Rows
                                            For Each rko In dtko.Rows
                                                For Each rptj In dtptj.Rows
                                                    For Each rkp In dtkp.Rows
                                                        For Each rvot In dtvot.Rows
                                                            dtNew.Rows.Add(rkw("KodKw"), rko("KodKO"), rptj("KodPTJ"), rkp("KodProjek"), rvot("KodVot"))
                                                        Next
                                                    Next
                                                Next
                                            Next
                                        Next
                                    End Using
                                End Using
                            End Using
                        End Using
                    End Using
                End Using

                Dim OldKodVotH1 = ""
                Dim OldKodVotH2 = ""
                Dim OldKodVotH3 = ""
                'loop dt to store inside table
                For Each drNew In dtNew.Rows
                    Dim KW = drNew("KodKW"), KO = drNew("KodKO"), PTJ = drNew("KodPTj"), KP = drNew("KodKP"), VOT = drNew("KodVot")

                    'If Vot is H1 or H2
                    If VOT.Substring(2, 1) = "0" Then
                        'simpan dlm MK01_VotTahun & MK02_VotBulan

                        'MK01: if not found any, insert record into dt
                        If Not dtMK01.Select($"KodKw = '{KW}' And KodKO = '{KO}' And KodPTJ = '{PTJ}' And KodKP = '{KP}' And KodVot = '{VOT}'").Any Then
                            Dim dr As DataRow = dtMK01.NewRow
                            dr("KodKw") = KW
                            dr("KodKO") = KO
                            dr("KodPTJ") = PTJ
                            dr("KodKP") = KP
                            dr("KodVot") = VOT
                            dtMK01.Rows.Add(dr)
                        End If

                        'MK02: if not found any, insert record into dt
                        If Not dtMK02.Select($"KodKw = '{KW}' And KodKO = '{KO}' And KodPTJ = '{PTJ}' And KodKP = '{KP}' And KodVot = '{VOT}'").Any Then
                            Dim dr As DataRow = dtMK02.NewRow
                            dr("KodKw") = KW
                            dr("KodKO") = KO
                            dr("KodPTJ") = PTJ
                            dr("KodKP") = KP
                            dr("KodVot") = VOT
                            dtMK02.Rows.Add(dr)
                        End If
                    Else
                        'If Vot is H3 or Vot Detail
                        'Simpan dlm MK03_AmTahun & MK04_AmBulan

                        'MK03: if not found any, insert record into dt
                        If Not dtMK03.Select($"KodKw = '{KW}' And KodKO = '{KO}' And KodPTJ = '{PTJ}' And KodKP = '{KP}' And KodVot = '{VOT}'").Any Then
                            Dim dr As DataRow = dtMK03.NewRow
                            dr("KodKw") = KW
                            dr("KodKO") = KO
                            dr("KodPTJ") = PTJ
                            dr("KodKP") = KP
                            dr("KodVot") = VOT
                            dtMK03.Rows.Add(dr)
                        End If

                        'MK04: if not found any, insert record into dt
                        If Not dtMK04.Select($"KodKw = '{KW}' And KodKO = '{KO}' And KodPTJ = '{PTJ}' And KodKP='{KP}' And KodVot='{VOT}'").Any Then
                            Dim dr As DataRow = dtMK04.NewRow
                            dr("KodKw") = KW
                            dr("KodKO") = KO
                            dr("KodPTJ") = PTJ
                            dr("KodKP") = KP
                            dr("KodVot") = VOT
                            dtMK04.Rows.Add(dr)
                        End If

                        'Bagi kes user pilih detail vot shj
                        'Simpan di MK03 dan MK04 untuk H3
                        'Simpan di MK01 dan MK02 bagi H1 & H2
                        Dim NewKodVotH1 = drNew("KodVot").Substring(0, 1) + "0000"
                        Dim NewKodVotH2 = drNew("KodVot").Substring(0, 2) + "000"
                        Dim NewKodVotH3 = drNew("KodVot").Substring(0, 3) + "00"

                        If Not OldKodVotH3.Equals(NewKodVotH3) Then
                            OldKodVotH3 = NewKodVotH3

                            'MK03 for H3: if not found any, insert record into dt
                            If Not dtMK03.Select($"KodKw = '{KW}' And KodKO = '{KO}' And KodPTJ = '{PTJ}' And KodKP = '{KP}' And KodVot = '{NewKodVotH3}'").Any Then
                                Dim dr As DataRow = dtMK03.NewRow
                                dr("KodKw") = KW
                                dr("KodKO") = KO
                                dr("KodPTJ") = PTJ
                                dr("KodKP") = KP
                                dr("KodVot") = NewKodVotH3
                                dtMK03.Rows.Add(dr)
                            End If

                            'MK04 for H3: if not found any, insert record into dt
                            If Not dtMK04.Select($"KodKw = '{KW}' And KodKO = '{KO}' And KodPTJ = '{PTJ}' And KodKP = '{KP}' And KodVot = '{NewKodVotH3}'").Any Then
                                Dim dr As DataRow = dtMK04.NewRow
                                dr("KodKw") = KW
                                dr("KodKO") = KO
                                dr("KodPTJ") = PTJ
                                dr("KodKP") = KP
                                dr("KodVot") = NewKodVotH3
                                dtMK04.Rows.Add(dr)
                            End If
                        End If

                        If Not OldKodVotH2.Equals(NewKodVotH2) Then
                            OldKodVotH2 = NewKodVotH2
                            'MK01 for H2: if not found any, insert record into dt
                            If Not dtMK01.Select($"KodKw = '{KW}' And KodKO = '{KO}' And KodPTJ = '{PTJ}' And KodKP = '{KP}' And KodVot = '{NewKodVotH2}'").Any Then
                                Dim dr As DataRow = dtMK01.NewRow
                                dr("KodKw") = KW
                                dr("KodKO") = KO
                                dr("KodPTJ") = PTJ
                                dr("KodKP") = KP
                                dr("KodVot") = NewKodVotH2
                                dtMK01.Rows.Add(dr)
                            End If

                            'MK02 for H2: if not found any, insert record into dt
                            If Not dtMK02.Select($"KodKw = '{KW}' And KodKO = '{KO}' And KodPTJ = '{PTJ}' And KodKP = '{KP}' And KodVot = '{NewKodVotH2}'").Any Then
                                Dim dr As DataRow = dtMK02.NewRow
                                dr("KodKw") = KW
                                dr("KodKO") = KO
                                dr("KodPTJ") = PTJ
                                dr("KodKP") = KP
                                dr("KodVot") = NewKodVotH2
                                dtMK02.Rows.Add(dr)
                            End If
                        End If

                        If Not OldKodVotH1.Equals(NewKodVotH1) Then
                            OldKodVotH1 = NewKodVotH1
                            'MK01 for H1: if not found any, insert record into dt
                            If Not dtMK01.Select($"KodKw = '{KW}' And KodKO = '{KO}' And KodPTJ = '{PTJ}' And KodKP = '{KP}' And KodVot = '{NewKodVotH1}'").Any Then
                                Dim dr As DataRow = dtMK01.NewRow
                                dr("KodKw") = KW
                                dr("KodKO") = KO
                                dr("KodPTJ") = PTJ
                                dr("KodKP") = KP
                                dr("KodVot") = NewKodVotH1
                                dtMK01.Rows.Add(dr)
                            End If

                            'MK02 for H1: if not found any, insert record into dt
                            If Not dtMK02.Select($"KodKw = '{KW}' And KodKO = '{KO}' And KodPTJ = '{PTJ}' And KodKP = '{KP}' And KodVot = '{NewKodVotH1}'").Any Then
                                Dim dr As DataRow = dtMK02.NewRow
                                dr("KodKw") = KW
                                dr("KodKO") = KO
                                dr("KodPTJ") = PTJ
                                dr("KodKP") = KP
                                dr("KodVot") = NewKodVotH1
                                dtMK02.Rows.Add(dr)
                            End If
                        End If
                    End If
                Next
            End Using

            'Simpan all records
            fSimpan()

        Catch ex As Exception
            fGlobalAlert("Proses bina Kod Akaun gagal!" + ex.Message, Me.Page, Me.GetType())
            divErr.Visible = True
            lblErr.Text = "Proses bina Kod Akaun gagal!"
        End Try

    End Sub


    Private Sub fSimpan()
        dbconn = New DBKewConn

        Dim resultUpdate = False, resultCommit = False, norecordToSave = False
        Dim dr As DataRow
        Dim strMK01 = $"SELECT [ID],[KodKw],[KodKO],[KodPTJ],[KodKP],[KodVot],[MK01_Tahun],[MK01_Perutk],[MK01_BljYtd],[MK01_TngYtd]
,[MK01_BakiTng],[MK01_PemLulus],[MK01_BakiSms],[MK01_BakiPerutk],[MK01_BakiSlpsPemLulus],[MK01_VirMasuk],[MK01_VirKeluar],[MK01_Status] FROM [MK01_VotTahun] WHERE MK01_Tahun = {Tahun};"
        Dim strMK02 = $"SELECT [ID],[KodKw],[KodKO],[KodPTJ],[KodKP],[KodVot],[MK02_Bulan],[MK02_Tahun],[MK02_Perutk],[MK02_BljYtd]
,[MK02_BljMtd],[MK02_TngYtd],[MK02_TngMtd],[MK02_BakiTng],[MK02_BakiSms],[MK02_BakiPerutk],[MK02_PemLulus],[MK02_BakiSlpsPemLulus],[MK02_VirMasuk],[MK02_VirKeluar]
,[MK02_Status]  FROM [MK02_VotBulan] WHERE MK02_Tahun={Tahun} AND MK02_Bulan={Bulan};"
        Dim strMK03 = $"SELECT [ID],[KodKw],[KodKO],[KodPTJ],[KodKP],[KodVot],[MK03_Tahun],[MK03_Debit],[MK03_Kredit] FROM [MK03_AmTahun] WHERE MK03_Tahun={Tahun};"
        Dim strMK04 = $"SELECT [ID],[KodKw],[KodKO],[KodPTJ],[KodKP],[KodVot],[MK04_Bulan],[MK04_Tahun],[MK04_Debit],[MK04_Kredit] FROM [MK04_AmBulan] WHERE MK04_Tahun={Tahun};"

        dbconn.sConnBeginTrans()
        Using dsMK01 = dbconn.fSelectCommand(strMK01, "MK01", True)
            Dim changedRecordsTable As DataTable = dtMK01.GetChanges()
            If changedRecordsTable IsNot Nothing Then
                For Each drRow As DataRow In changedRecordsTable.Rows
                    dr = dsMK01.Tables(0).NewRow
                    dr("KodKw") = drRow("KodKw")
                    dr("KodKO") = drRow("KodKO")
                    dr("KodPTJ") = drRow("KodPTJ")
                    dr("KodKP") = drRow("KodKP")
                    dr("KodVot") = drRow("KodVot")
                    dr("MK01_Tahun") = Tahun
                    dr("MK01_Perutk") = 0
                    dr("MK01_BljYtd") = 0
                    dr("MK01_TngYtd") = 0
                    dr("MK01_BakiTng") = 0
                    dr("MK01_PemLulus") = 0
                    dr("MK01_BakiSms") = 0
                    dr("MK01_BakiPerutk") = 0
                    dr("MK01_BakiSlpsPemLulus") = 0
                    dr("MK01_VirMasuk") = 0
                    dr("MK01_VirKeluar") = 0
                    dr("MK01_Status") = 1
                    dsMK01.Tables(0).Rows.Add(dr)
                Next
                dbconn.sUpdateCommand(dsMK01, strMK01, resultUpdate, False)
                norecordToSave = False
            Else
                norecordToSave = True
            End If
        End Using


        Using dsMK02 = dbconn.fSelectCommand(strMK02, "MK02", True)
                Dim changedRecordsTable As DataTable = dtMK02.GetChanges()
            If changedRecordsTable IsNot Nothing Then
                For Each drRow As DataRow In changedRecordsTable.Rows
                    dr = dsMK02.Tables(0).NewRow
                    dr("KodKw") = drRow("KodKw")
                    dr("KodKO") = drRow("KodKO")
                    dr("KodPTJ") = drRow("KodPTJ")
                    dr("KodKP") = drRow("KodKP")
                    dr("KodVot") = drRow("KodVot")
                    dr("MK02_Bulan") = Bulan
                    dr("MK02_Tahun") = Tahun
                    dr("MK02_Perutk") = 0
                    dr("MK02_BljYtd") = 0
                    dr("MK02_BljMtd") = 0
                    dr("MK02_TngYtd") = 0
                    dr("MK02_TngMtd") = 0
                    dr("MK02_BakiTng") = 0
                    dr("MK02_BakiSms") = 0
                    dr("MK02_BakiPerutk") = 0
                    dsMK02.Tables(0).Rows.Add(dr)
                Next
                dbconn.sUpdateCommand(dsMK02, strMK02, resultUpdate, False)
                norecordToSave = False
            Else
                norecordToSave = True
            End If
            End Using

        Using dsMK03 = dbconn.fSelectCommand(strMK03, "MK03", True)
            Dim changedRecordsTable As DataTable = dtMK03.GetChanges()
            If changedRecordsTable IsNot Nothing Then
                For Each drRow As DataRow In changedRecordsTable.Rows
                    dr = dsMK03.Tables(0).NewRow
                    dr("KodKw") = drRow("KodKw")
                    dr("KodKO") = drRow("KodKO")
                    dr("KodPTJ") = drRow("KodPTJ")
                    dr("KodKP") = drRow("KodKP")
                    dr("KodVot") = drRow("KodVot")
                    dr("MK03_Tahun") = Tahun
                    dr("MK03_Debit") = 0
                    dr("MK03_Kredit") = 0
                    dsMK03.Tables(0).Rows.Add(dr)
                Next
                dbconn.sUpdateCommand(dsMK03, strMK03, resultUpdate, False)
                norecordToSave = False
            Else
                norecordToSave = True
            End If
        End Using

        Using dsMK04 = dbconn.fSelectCommand(strMK04, "MK04", True)
            Dim changedRecordsTable As DataTable = dtMK04.GetChanges()
            If changedRecordsTable IsNot Nothing Then
                For Each drRow As DataRow In changedRecordsTable.Rows
                    dr = dsMK04.Tables(0).NewRow
                    dr("KodKw") = drRow("KodKw")
                    dr("KodKO") = drRow("KodKO")
                    dr("KodPTJ") = drRow("KodPTJ")
                    dr("KodKP") = drRow("KodKP")
                    dr("KodVot") = drRow("KodVot")
                    dr("MK04_Tahun") = Tahun
                    dr("MK04_Bulan") = Bulan
                    dr("MK04_Debit") = 0
                    dr("MK04_Kredit") = 0
                    dsMK04.Tables(0).Rows.Add(dr)
                Next
                dbconn.sUpdateCommand(dsMK04, strMK04, resultUpdate, False)
                norecordToSave = False
            Else
                norecordToSave = True
            End If
        End Using

        If resultUpdate Then
            dbconn.sConnCommitTrans()
            resultCommit = True
        End If

        If resultCommit Then
            fGlobalAlert("Proses bina Kod Akaun berjaya!", Me.Page, Me.GetType())
            divSucc.Visible = True
            btnProses.Enabled = True
        Else
            If norecordToSave Then
                fGlobalAlert("Rekod lama telah wujud. Tiada kemasukan data!", Me.Page, Me.GetType())
                lblErr.Text = "Rekod lama telah wujud. Tiada kemasukan data!"
            Else
                fGlobalAlert("Proses bina Kod Akaun gagal!", Me.Page, Me.GetType())
                lblErr.Text = "Proses bina Kod Akaun gagal!"
            End If

            divErr.Visible = True

            btnProses.Enabled = True
        End If
    End Sub

    Protected Sub gvKP1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvKP1.SelectedIndexChanged
        Dim row = gvKP1.SelectedRow
        Dim noKP As String = row.Cells(1).Text.Trim
        Dim namaKP As String = row.Cells(2).Text.Trim

        Using dtKp1 = CType(ViewState("dtKP1"), DataTable)
            Dim rowDr = dtKp1.Select($"KodProjek = '{noKP}'").FirstOrDefault()
            dtKp1.Rows.Remove(rowDr)

            ViewState("dtKP1") = dtKp1
            fBindGvKP1()
        End Using

        Using dtKp2 = CType(ViewState("dtKP2"), DataTable)
            dtKp2.Rows.Add(noKP, namaKP)
            ViewState("dtKP2") = dtKp2
            fBindGvKP2()
        End Using
    End Sub

    Protected Sub gvKP1_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvKP1.Sorting
        Dim sortedView As New DataView(fCreateDt1())
        sortedView.Sort = e.SortExpression & " " & GetSortDirection(e.SortExpression)
        Session("SortedView") = sortedView
        gvKP1.DataSource = sortedView
        gvKP1.DataBind()
    End Sub

    Protected Sub gvKP1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvKP1.PageIndexChanging
        gvKP1.PageIndex = e.NewPageIndex

        If Session("SortedView") IsNot Nothing Then
            gvKP1.DataSource = Session("SortedView")
            gvKP1.DataBind()
        Else
            fBindGvKP1()
        End If
    End Sub

    Protected Sub gvKp2_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvKp2.RowDeleting
        Dim row = gvKp2.Rows(e.RowIndex)
        Dim noKP As String = row.Cells(1).Text.Trim
        Dim namaKP As String = row.Cells(2).Text.Trim

        Using dtKp2 = CType(ViewState("dtKP2"), DataTable)
            Dim rowDr = dtKp2.Select($"KodProjek = '{noKP}'").FirstOrDefault()
            dtKp2.Rows.Remove(rowDr)

            ViewState("dtKP2") = dtKp2
            fBindGvKP2()
        End Using

        Using dtKp1 = CType(ViewState("dtKP1"), DataTable)
            dtKp1.Rows.Add(noKP, namaKP)
            ViewState("dtKP1") = dtKp1
            fBindGvKP1()
        End Using
    End Sub


    Protected Sub ddlSaizRekod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSaizRekod.SelectedIndexChanged
        gvKP1.PageSize = CInt(ddlSaizRekod.SelectedValue)
        fBindGvKP1()
    End Sub

    Private Sub fBindGvKP1()
        Using dt = fCreateDt1()
            dt.DefaultView.Sort = "KodProjek ASC"
            Dim dt1 = dt.DefaultView.ToTable()
            gvKP1.DataSource = dt1
            gvKP1.DataBind()
        End Using
    End Sub

    Private Function fCreateDt1() As DataTable
        Using dt = CType(ViewState("dtKP1"), DataTable)
            lblJumRekodS.Text = dt.Rows.Count
            Return dt
        End Using
    End Function

    Private Sub fBindGvKP2()
        Using dt = fCreateDt2()
            dt.DefaultView.Sort = "KodProjek ASC"
            Dim dt1 = dt.DefaultView.ToTable()
            gvKp2.DataSource = dt1
            gvKp2.DataBind()
        End Using
    End Sub

    Private Function fCreateDt2() As DataTable
        Using dt = CType(ViewState("dtKP2"), DataTable)
            lblJumRekodS2.Text = dt.Rows.Count
            Return dt
        End Using
    End Function

    Private Function GetSortDirection(ByVal column As String) As String

        ' By default, set the sort direction to ascending.
        Dim sortDirection = "ASC"

        ' Retrieve the last column that was sorted.
        Dim sortExpression = TryCast(ViewState("SortExpression"), String)

        If sortExpression IsNot Nothing Then
            ' Check if the same column is being sorted.
            ' Otherwise, the default value can be returned.
            If sortExpression = column Then
                Dim lastDirection = TryCast(ViewState("SortDirection"), String)
                If lastDirection IsNot Nothing AndAlso lastDirection = "ASC" Then
                    sortDirection = "DESC"
                End If
            End If
        End If

        ' Save new values in ViewState.
        ViewState("SortDirection") = sortDirection
        ViewState("SortExpression") = column

        Return sortDirection

    End Function

    Protected Sub lbtnCari_Click(sender As Object, e As EventArgs) Handles lbtnCari.Click
        'Using dt = CType(ViewState("dtKP1"), DataTable)
        'Dim drs As DataRow()
        Dim str = "select KodProjek , Butiran from mk_KodProjek order by KodProjek"
        If ddlCari.SelectedValue = 1 Then
            'keseluruhan
            str = $"select KodProjek , Butiran from mk_KodProjek order by KodProjek"
        ElseIf ddlCari.SelectedValue = 2 Then
            'cari kod
            'drs = dt.Select($"KodProjek Like '{txtCari.Text}%'")
            str = $"select KodProjek , Butiran from mk_KodProjek WHERE KodProjek Like '{txtCari.Text}%' order by KodProjek"
        Else
            'cari nama
            'drs = dt.Select($"Butiran Like '%{txtCari.Text}%'")
            str = $"select KodProjek , Butiran from mk_KodProjek WHERE Butiran Like '%{txtCari.Text}%' order by KodProjek"
        End If
        Using dt = dbconn.fSelectCommandDt(str)
            ViewState("dtKP1") = dt
            fBindGvKP1()
        End Using
        'End Using
    End Sub
End Class