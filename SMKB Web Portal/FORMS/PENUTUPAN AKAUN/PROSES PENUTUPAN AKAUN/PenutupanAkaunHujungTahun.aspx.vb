Imports System.Data.SqlClient
Imports System.Web.Services
Imports Newtonsoft.Json

Public Class PenutupanAkaunHujungTahun
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            PopulateYearsDropDownList()
            'CheckBackupData()
            'If tahun.SelectedValue <> "" Then
            '    CheckBackupData()

            'Else
            btnBackup.Disabled = True
                btnProcess.Disabled = True
                btnRestore.Disabled = True
            'End If

        Else
            CheckBackupData()
        End If
    End Sub

    Private Sub PopulateYearsDropDownList()
        Dim currentYear As Integer = DateTime.Now.Year
        Dim yearList As New List(Of Integer)()

        For i As Integer = 1 To 1
            Dim year As Integer = currentYear '- i
            yearList.Add(year)
        Next

        tahun.DataSource = yearList
        tahun.DataBind()

        ' Optionally, add a default/select option
        tahun.Items.Insert(0, New ListItem("-- Sila Pilih --", String.Empty))
    End Sub

    Protected Sub btnBackup_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBackup.ServerClick
        If tahun.SelectedValue = "" Then
            Response.Write("<script>alert('Sila pilih tahun.');</script>")
            Return
        End If

        BackupProcess()
    End Sub

    ' Process Backup Data
    Private Sub BackupProcess()

        ' Get the selected year
        Dim selectedYear As String = tahun.SelectedValue

        ' Convert the selected year to an integer
        Dim yearAsInteger As Integer
        If Not Integer.TryParse(selectedYear, yearAsInteger) Then
            ' Handle the case where selectedYear cannot be converted to an integer
            Response.Write("<script>alert('Invalid selected year.');</script>")
            Return
        End If

        lblReturnValue.Text = "loading.."

        Dim queryAm As String = "MERGE INTO SMKB_Lejar_Am_Backup AS Target
                                            USING (SELECT * FROM SMKB_Lejar_Am_Temp WHERE Tahun = @Tahun) AS Source
                                            ON (Target.Kod_PTJ = Source.Kod_PTJ)
                                            WHEN NOT MATCHED BY TARGET THEN
                                                INSERT (Tahun, Kod_Kump_Wang, Kod_Operasi, Kod_PTJ, Kod_Vot, Kod_Projek, Kod_Syarikat, Dr_1, Cr_1, Dr_2, Cr_2, Dr_3, Cr_3, Dr_4, Cr_4, Dr_5, Cr_5, Dr_6, Cr_6, Dr_7, Cr_7, Dr_8, Cr_8, Dr_9, Cr_9, Dr_10, Cr_10, Dr_11, Cr_11, Dr_12, Cr_12, Dr_13, Cr_13, Status)
                                                VALUES (
                                                    Source.Tahun, Source.Kod_Kump_Wang, Source.Kod_Operasi, Source.Kod_PTJ,
                                                    Source.Kod_Vot, Source.Kod_Projek, Source.Kod_Syarikat,
                                                    Source.Dr_1, Source.Cr_1, Source.Dr_2, Source.Cr_2, Source.Dr_3, Source.Cr_3,
                                                    Source.Dr_4, Source.Cr_4, Source.Dr_5, Source.Cr_5, Source.Dr_6, Source.Cr_6,
                                                    Source.Dr_7, Source.Cr_7, Source.Dr_8, Source.Cr_8, Source.Dr_9, Source.Cr_9,
                                                    Source.Dr_10, Source.Cr_10, Source.Dr_11, Source.Cr_11, Source.Dr_12, Source.Cr_12,
                                                    Source.Dr_13, Source.Cr_13, Source.Status
                                                );"

        Dim queryPemiutang As String = "MERGE INTO SMKB_Lejar_Pemiutang_Backup AS Target
                                            USING (SELECT * FROM SMKB_Lejar_Pemiutang_Temp WHERE Tahun = @Tahun) AS Source
                                            ON (Target.Kod_Pemiutang = Source.Kod_Pemiutang)
                                            WHEN NOT MATCHED BY TARGET THEN
                                                INSERT (Kod_Pemiutang, Tahun, Kod_Kump_Wang, Kod_Operasi, Kod_Projek, Kod_PTJ, Kod_Vot, Dr_1, Cr_1, Dr_2, Cr_2, Dr_3, Cr_3, Dr_4, Cr_4, Dr_5, Cr_5, Dr_6, Cr_6, Dr_7, Cr_7, Dr_8, Cr_8, Dr_9, Cr_9, Dr_10, Cr_10, Dr_11, Cr_11, Dr_12, Cr_12, Dr_13, Cr_13, Status)
                                                VALUES (
                                                    Source.Kod_Pemiutang, Source.Tahun, Source.Kod_Kump_Wang, Source.Kod_Operasi,
                                                    Source.Kod_Projek, Source.Kod_PTJ, Source.Kod_Vot,
                                                    Source.Dr_1, Source.Cr_1, Source.Dr_2, Source.Cr_2, Source.Dr_3, Source.Cr_3,
                                                    Source.Dr_4, Source.Cr_4, Source.Dr_5, Source.Cr_5, Source.Dr_6, Source.Cr_6,
                                                    Source.Dr_7, Source.Cr_7, Source.Dr_8, Source.Cr_8, Source.Dr_9, Source.Cr_9,
                                                    Source.Dr_10, Source.Cr_10, Source.Dr_11, Source.Cr_11, Source.Dr_12, Source.Cr_12,
                                                    Source.Dr_13, Source.Cr_13, Source.Status
                                                );"

        Dim queryPenghutang As String = "MERGE INTO SMKB_Lejar_Penghutang_Backup AS Target
                                            USING (SELECT * FROM SMKB_Lejar_Penghutang_Temp WHERE Tahun = @Tahun) AS Source
                                            ON (Target.Kod_Penghutang = Source.Kod_Penghutang)
                                            WHEN NOT MATCHED BY TARGET THEN
                                                INSERT (Kod_Penghutang, Tahun, Kod_Kump_Wang, Kod_Operasi, Kod_Projek, Kod_PTJ, Kod_Vot, Dr_1, Cr_1, Dr_2, Cr_2, Dr_3, Cr_3, Dr_4, Cr_4, Dr_5, Cr_5, Dr_6, Cr_6, Dr_7, Cr_7, Dr_8, Cr_8, Dr_9, Cr_9, Dr_10, Cr_10, Dr_11, Cr_11, Dr_12, Cr_12, Dr_13, Cr_13, Status)
                                                VALUES (
                                                    Source.Kod_Penghutang, Source.Tahun, Source.Kod_Kump_Wang, Source.Kod_Operasi,
                                                    Source.Kod_Projek, Source.Kod_PTJ, Source.Kod_Vot,
                                                    Source.Dr_1, Source.Cr_1, Source.Dr_2, Source.Cr_2, Source.Dr_3, Source.Cr_3,
                                                    Source.Dr_4, Source.Cr_4, Source.Dr_5, Source.Cr_5, Source.Dr_6, Source.Cr_6,
                                                    Source.Dr_7, Source.Cr_7, Source.Dr_8, Source.Cr_8, Source.Dr_9, Source.Cr_9,
                                                    Source.Dr_10, Source.Cr_10, Source.Dr_11, Source.Cr_11, Source.Dr_12, Source.Cr_12,
                                                    Source.Dr_13, Source.Cr_13, Source.Status
                                                );"



        Dim queryString As String = queryAm & queryPemiutang & queryPenghutang
        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@Tahun", selectedYear))

        Dim db As New DBKewConn
        Dim result As String = db.Process(queryString, param)

        If result <> "OK" Then
            CheckBackupData()
            ' Set the value to be sent back
            lblReturnValue.Text = "Backup success"
        Else
            CheckBackupData()
            lblReturnValue.Text = "Backup failed"
        End If

    End Sub

    Protected Sub btnProcess_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnProcess.ServerClick
        If tahun.SelectedValue = "" Then
            Response.Write("<script>alert('Sila pilih tahun.');</script>")
            Return
        End If

        YearEndProcess()
    End Sub

    ' Process Year End
    Private Sub YearEndProcess()

        ' Get the selected year
        Dim selectedYear As String = tahun.SelectedValue

        Dim nextYear As Integer = selectedYear + 1

        ' Convert the selected year to an integer
        Dim yearAsInteger As Integer
        If Not Integer.TryParse(selectedYear, yearAsInteger) Then
            ' Handle the case where selectedYear cannot be converted to an integer
            Response.Write("<script>alert('Invalid selected year.');</script>")
            Return
        End If

        lblReturnValue.Text = "loading.."


        ' Update SMKB_Lejar_Am ; coulmn Dr_13 (sum Dr_1 -> Dr_12 );  Cr_13  ( sum Cr_1 -> Cr_12 ) for each row
        Dim queryString As String = "
        UPDATE SMKB_Lejar_Am_Temp
        SET Dr_13 = ISNULL(Dr_1, 0) + ISNULL(Dr_2, 0) + ISNULL(Dr_3, 0) + ISNULL(Dr_4, 0) + ISNULL(Dr_5, 0) +
                    ISNULL(Dr_6, 0) + ISNULL(Dr_7, 0) + ISNULL(Dr_8, 0) + ISNULL(Dr_9, 0) + ISNULL(Dr_10, 0) +
                    ISNULL(Dr_11, 0) + ISNULL(Dr_12, 0),
            Cr_13 = ISNULL(Cr_1, 0) + ISNULL(Cr_2, 0) + ISNULL(Cr_3, 0) + ISNULL(Cr_4, 0) + ISNULL(Cr_5, 0) +
                    ISNULL(Cr_6, 0) + ISNULL(Cr_7, 0) + ISNULL(Cr_8, 0) + ISNULL(Cr_9, 0) + ISNULL(Cr_10, 0) +
                    ISNULL(Cr_11, 0) + ISNULL(Cr_12, 0)
        WHERE Tahun = @Tahun;"

        ' Insert into SMKB_Lejar_Am (select * from SMKB_Lejar_Am where Tahun ='2023' and (debit_13 <> 0 or credit_13 <> 0)) ; change Tahun to nextYear dan all Cr Dr is 0.00
        queryString += "
            INSERT INTO SMKB_Lejar_Am_Temp
                (Tahun, Kod_Kump_Wang, Kod_Operasi, Kod_PTJ, Kod_Vot, Kod_Projek, Kod_Syarikat,
	            Dr_1, Cr_1, Dr_2, Cr_2, Dr_3, Cr_3, Dr_4, Cr_4, Dr_5, Cr_5, Dr_6, Cr_6, Dr_7, Cr_7, Dr_8, Cr_8, Dr_9, Cr_9, Dr_10, Cr_10, Dr_11, Cr_11, Dr_12, Cr_12, Dr_13, Cr_13, Status)
            SELECT
                @NextYear, Kod_Kump_Wang, Kod_Operasi, Kod_PTJ, Kod_Vot, Kod_Projek, Kod_Syarikat,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, Status
            FROM SMKB_Lejar_Am_Temp
            WHERE Tahun = @Tahun AND (Dr_13 <> 0 OR Cr_13 <> 0);"

        ' -- Delete SMKB_Lejar_Pemiutang and Delete SMKB_Lejar_Penghutang
        'queryString += "
        '    DELETE FROM SMKB_Lejar_Pemiutang_Temp;
        '    DELETE FROM SMKB_Lejar_Penghutang_Temp;"

        queryString += "
        UPDATE SMKB_Lejar_Pemiutang_Temp
        SET Dr_13 = ISNULL(Dr_1, 0) + ISNULL(Dr_2, 0) + ISNULL(Dr_3, 0) + ISNULL(Dr_4, 0) + ISNULL(Dr_5, 0) +
                    ISNULL(Dr_6, 0) + ISNULL(Dr_7, 0) + ISNULL(Dr_8, 0) + ISNULL(Dr_9, 0) + ISNULL(Dr_10, 0) +
                    ISNULL(Dr_11, 0) + ISNULL(Dr_12, 0),
            Cr_13 = ISNULL(Cr_1, 0) + ISNULL(Cr_2, 0) + ISNULL(Cr_3, 0) + ISNULL(Cr_4, 0) + ISNULL(Cr_5, 0) +
                    ISNULL(Cr_6, 0) + ISNULL(Cr_7, 0) + ISNULL(Cr_8, 0) + ISNULL(Cr_9, 0) + ISNULL(Cr_10, 0) +
                    ISNULL(Cr_11, 0) + ISNULL(Cr_12, 0)
        WHERE Tahun = @Tahun;" '

        queryString += "
            INSERT INTO SMKB_Lejar_Pemiutang_Temp
                (Kod_Pemiutang , Tahun, Kod_Kump_Wang, Kod_Operasi, Kod_Projek, Kod_PTJ, Kod_Vot, 
	            Dr_1, Cr_1, Dr_2, Cr_2, Dr_3, Cr_3, Dr_4, Cr_4, Dr_5, Cr_5, Dr_6, Cr_6, Dr_7, Cr_7, Dr_8, Cr_8, Dr_9, Cr_9, Dr_10, Cr_10, Dr_11, Cr_11, Dr_12, Cr_12, Dr_13, Cr_13, Status)
            SELECT
                Kod_Pemiutang,@NextYear, Kod_Kump_Wang, Kod_Operasi, Kod_Projek , Kod_PTJ, Kod_Vot, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, Status
            FROM SMKB_Lejar_Pemiutang_Temp
            WHERE Tahun = @Tahun AND (Dr_13 <> 0 OR Cr_13 <> 0);"

        queryString += "
        UPDATE SMKB_Lejar_Penghutang_Temp
        SET Dr_13 = ISNULL(Dr_1, 0) + ISNULL(Dr_2, 0) + ISNULL(Dr_3, 0) + ISNULL(Dr_4, 0) + ISNULL(Dr_5, 0) +
                    ISNULL(Dr_6, 0) + ISNULL(Dr_7, 0) + ISNULL(Dr_8, 0) + ISNULL(Dr_9, 0) + ISNULL(Dr_10, 0) +
                    ISNULL(Dr_11, 0) + ISNULL(Dr_12, 0),
            Cr_13 = ISNULL(Cr_1, 0) + ISNULL(Cr_2, 0) + ISNULL(Cr_3, 0) + ISNULL(Cr_4, 0) + ISNULL(Cr_5, 0) +
                    ISNULL(Cr_6, 0) + ISNULL(Cr_7, 0) + ISNULL(Cr_8, 0) + ISNULL(Cr_9, 0) + ISNULL(Cr_10, 0) +
                    ISNULL(Cr_11, 0) + ISNULL(Cr_12, 0)
        WHERE Tahun = @Tahun;"

        queryString += "
             INSERT INTO SMKB_Lejar_Penghutang_Temp
                (Kod_Penghutang, Tahun, Kod_Kump_Wang, Kod_Operasi, Kod_Projek , Kod_PTJ, Kod_Vot, 
	            Dr_1, Cr_1, Dr_2, Cr_2, Dr_3, Cr_3, Dr_4, Cr_4, Dr_5, Cr_5, Dr_6, Cr_6, Dr_7, Cr_7, Dr_8, Cr_8, Dr_9, Cr_9, Dr_10, Cr_10, Dr_11, Cr_11, Dr_12, Cr_12, Dr_13, Cr_13, Status)
            SELECT
                Kod_Penghutang ,@NextYear, Kod_Kump_Wang, Kod_Operasi, Kod_Projek ,Kod_PTJ, Kod_Vot, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, Status
            FROM SMKB_Lejar_Penghutang_Temp
            WHERE Tahun = @Tahun AND (Dr_13 <> 0 OR Cr_13 <> 0);"

        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@Tahun", selectedYear))
        param.Add(New SqlParameter("@NextYear", nextYear))

        Dim db As New DBKewConn
        Dim result As String = db.Process(queryString, param)

        If result <> "OK" Then
            ' Set the value to be sent back
            'lblReturnValue.Text = "Process success"

            lblBackupStatus.Text = "Proses Akhir Tahun Selesai"
            lblBackupStatusIndicator.Attributes("class") = "fa fa-check-circle text-success ml-2"

            btnProcess.Disabled = True
            btnRestore.Disabled = False
        Else
            lblReturnValue.Text = "Process failed"
        End If
    End Sub

    Protected Sub btnRestore_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRestore.ServerClick
        If tahun.SelectedValue = "" Then
            Response.Write("<script>alert('Sila pilih tahun.');</script>")
            Return
        End If

        RestoreProcess()
    End Sub

    ' Process Restore
    Private Sub RestoreProcess()

        ' Get the selected year
        Dim selectedYear As String = tahun.SelectedValue

        Dim nextYear As Integer = selectedYear + 1

        ' Convert the selected year to an integer
        Dim yearAsInteger As Integer
        If Not Integer.TryParse(selectedYear, yearAsInteger) Then
            ' Handle the case where selectedYear cannot be converted to an integer
            Response.Write("<script>alert('Invalid selected year.');</script>")
            Return
        End If

        lblReturnValue.Text = "loading.."

        ' Update SMKB_Lejar_Am_Temp tahun 2023 back; make column Dr_13, Cr_13 to 0.00 ; Delete SMKB_Lejar_Am_Temp tahun 2024...
        Dim queryString As String = "
            UPDATE SMKB_Lejar_Am_Temp
            SET Dr_13 = 0, Cr_13 = 0
            WHERE Tahun = @Tahun;"

        queryString += "
            DELETE FROM SMKB_Lejar_Am_Temp
            WHERE Tahun = @NextYear;"

        queryString += "
            DELETE FROM SMKB_Lejar_Am_Backup
            WHERE Tahun = @Tahun;"

        ' get SMKB_Lejar_Pemiutang_Backup ke SMKB_Lejar_Pemiutang_temp ; delete SMKB_Lejar_Pemiutang_Backup
        'queryString += "
        '    INSERT INTO SMKB_Lejar_Pemiutang_Temp SELECT * FROM SMKB_Lejar_Pemiutang_Backup;
        '    DELETE FROM SMKB_Lejar_Pemiutang_Backup;"

        queryString += "
            UPDATE SMKB_Lejar_Pemiutang_Temp
            SET Dr_13 = 0, Cr_13 = 0
            WHERE Tahun = @Tahun;"

        queryString += "
            DELETE FROM SMKB_Lejar_Pemiutang_Temp
            WHERE Tahun = @NextYear;"

        queryString += "
            DELETE FROM SMKB_Lejar_Pemiutang_Backup
            WHERE Tahun = @Tahun;"

        ' get SMKB_Lejar_Penghutang_Backup ke SMKB_Lejar_Penghutang_temp ; delete SMKB_Lejar_Penghutang_Backup
        'queryString += "
        '    INSERT INTO SMKB_Lejar_Penghutang_Temp SELECT * FROM SMKB_Lejar_Penghutang_Backup;
        '    DELETE FROM SMKB_Lejar_Penghutang_Backup;"

        queryString += "
            UPDATE SMKB_Lejar_Penghutang_Temp
            SET Dr_13 = 0, Cr_13 = 0
            WHERE Tahun = @Tahun;"

        queryString += "
            DELETE FROM SMKB_Lejar_Penghutang_Temp
            WHERE Tahun = @NextYear;"

        queryString += "
            DELETE FROM SMKB_Lejar_Penghutang_Backup
            WHERE Tahun = @Tahun;"

        Dim param As New List(Of SqlParameter)

        param.Add(New SqlParameter("@Tahun", selectedYear))
        param.Add(New SqlParameter("@NextYear", nextYear))

        Dim db As New DBKewConn
        Dim result As String = db.Process(queryString, param)

        If result <> "OK" Then
            ' Set the value to be sent back
            lblReturnValue.Text = "Restore success"
            CheckBackupData()
        Else
            lblReturnValue.Text = "Restore failed"
            CheckBackupData()
        End If
    End Sub

    Private Sub CheckBackupData()

        Dim selectedYear As String = tahun.SelectedValue
        Dim nextYear As Integer = selectedYear + 1
        Dim queryString As String = $"
            DECLARE @CountAmTemp INT, @CountAmBackup INT;
            DECLARE @CountPemiutangTemp INT, @CountPemiutangBackup INT;
            DECLARE @CountPenghutangTemp INT, @CountPenghutangBackup INT;
            DECLARE @Tahun Varchar(4)
            
            SET @Tahun =  '{selectedYear}'

            SELECT @CountAmTemp = COUNT(Tahun) FROM [DbKewanganV4].[dbo].[SMKB_Lejar_Am_Temp] Where Tahun = @Tahun;
            SELECT @CountAmBackup = COUNT(Tahun) FROM [DbKewanganV4].[dbo].[SMKB_Lejar_Am_Backup] Where Tahun = @Tahun;
            SELECT @CountPemiutangTemp = COUNT(Tahun) FROM [DbKewanganV4].[dbo].[SMKB_Lejar_Pemiutang_Temp] Where Tahun = @Tahun;
            SELECT @CountPemiutangBackup = COUNT(Tahun) FROM [DbKewanganV4].[dbo].[SMKB_Lejar_Pemiutang_Backup] Where Tahun = @Tahun;
            SELECT @CountPenghutangTemp = COUNT(Tahun) FROM [DbKewanganV4].[dbo].[SMKB_Lejar_Penghutang_Temp] Where Tahun = @Tahun;
            SELECT @CountPenghutangBackup = COUNT(Tahun) FROM [DbKewanganV4].[dbo].[SMKB_Lejar_Penghutang_Backup] Where Tahun = @Tahun;

            SELECT 
                CASE WHEN @CountAmTemp = @CountAmBackup THEN 1 ELSE 0 END AS ResultAm,
                CASE WHEN @CountPemiutangTemp = @CountPemiutangBackup THEN 1 ELSE 0 END AS ResultPemiutang,
                CASE WHEN @CountPenghutangTemp = @CountPenghutangBackup THEN 1 ELSE 0 END AS ResultPenghutang;"

        Dim param As New List(Of SqlParameter)
        param.Add(New SqlParameter("@ddlyear", selectedYear))

        Dim db As New DBKewConn
        Dim result As DataTable = db.Read(queryString)

        If result.Rows(0)("ResultAm") = 1 And result.Rows(0)("ResultPemiutang") = 1 And result.Rows(0)("ResultPenghutang") = 1 Then

            'check if process dah buat..
            Dim queryStringPT As String = $"
            DECLARE @CountAmTemp INT, @CountAmBackup INT;
            DECLARE @CountPemiutangTemp INT, @CountPemiutangBackup INT;
            DECLARE @CountPenghutangTemp INT, @CountPenghutangBackup INT;
            DECLARE @Tahun Varchar(4)
            
            SET @Tahun =  '{nextYear}'

            SELECT @CountAmTemp = COUNT(Tahun) FROM [DbKewanganV4].[dbo].[SMKB_Lejar_Am_Temp] Where Tahun = @Tahun;           
            SELECT @CountPemiutangTemp = COUNT(Tahun) FROM [DbKewanganV4].[dbo].[SMKB_Lejar_Pemiutang_Temp] Where Tahun = @Tahun;          
            SELECT @CountPenghutangTemp = COUNT(Tahun) FROM [DbKewanganV4].[dbo].[SMKB_Lejar_Penghutang_Temp] Where Tahun = @Tahun;
          

            SELECT 
                CASE WHEN @CountAmTemp >= 1 THEN 1 ELSE 0 END AS ResultAm,
                CASE WHEN @CountPemiutangTemp >= 1 THEN 1 ELSE 0 END AS ResultPemiutang,
                CASE WHEN @CountPenghutangTemp >= 1 THEN 1 ELSE 0 END AS ResultPenghutang;"

            Dim paramPT As New List(Of SqlParameter)
            paramPT.Add(New SqlParameter("@ddlyear", selectedYear))

            Dim dbPT As New DBKewConn
            Dim resultPT As DataTable = dbPT.Read(queryStringPT)

            If resultPT.Rows(0)("ResultAm") = 1 And resultPT.Rows(0)("ResultPemiutang") = 1 And resultPT.Rows(0)("ResultPenghutang") = 1 Then

                lblBackupStatus.Text = "Proses Akhir Tahun Selesai"
                lblBackupStatusIndicator.Attributes("class") = "fa fa-check-circle text-success ml-2"

                btnBackup.Disabled = True
                btnProcess.Disabled = True
                btnRestore.Disabled = False


            Else
                lblBackupStatus.Text = "Backup Selesai"
                lblBackupStatusIndicator.Attributes("class") = "fa fa-check-circle text-success ml-2"

                btnBackup.Disabled = True
                btnProcess.Disabled = False
                btnRestore.Disabled = False

            End If
            'tamat check process if dah buat..
        Else
            lblBackupStatus.Text = "Backup Belum Selesai"
            lblBackupStatusIndicator.Attributes("class") = "fa fa-times-circle text-danger ml-2"

            btnBackup.Disabled = False
            btnProcess.Disabled = True
            btnRestore.Disabled = True


        End If
    End Sub

End Class