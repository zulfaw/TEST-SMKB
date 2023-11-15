Imports System.Data.SqlClient
Imports System.Globalization
Imports System.IO
Imports System.Reflection
Imports System.Web
Imports System.Web.Configuration

Module ModFunction

    ''' <summary>
    ''' function get User Info
    ''' </summary>
    ''' <param name="strStaffID">staf id</param>
    ''' <returns></returns>
    Public Function fGetUserInfo(strStaffID) As DataTable
        Dim dbconn As New DBSMConn
        Dim strSql As String = $"select a.MS01_Nama, c.JawGiliran , e.Pejabat, e.KodPejabat, d.MS08_Bahagian, d.MS08_Unit, a.MS01_Email, c.JawGiliran,
                               b.MS02_GredGajiS as GredGajiS,a.MS01_NoTelBimbit as NoTel,b.MS02_JumlahGajiS as GajiS     
                                    FROM MS01_Peribadi a, ms02_perjawatan b, MS_Jawatan c ,MS08_Penempatan d, MS_Pejabat e
                                    WHERE a.MS01_NoStaf = '{strStaffID}'
                                    AND b.MS01_NoStaf = a.MS01_NoStaf 
                                    AND b.ms02_jawsandang = c.KodJawatan  
                                    AND e.KodPejabat = d.MS08_Pejabat 
                                    AND d.MS01_NoStaf = a.MS01_NoStaf
                                    AND d.MS08_StaTerkini = 1;"

        Using dt = dbconn.fselectCommandDt(strSql)
            Return dt
        End Using
    End Function

    Public Function fPowerUser(nostaf, KodSubMenu)
        Dim strPU = "SELECT COUNT(KodSubMenu) AS BIL FROM MK_UPowerUser WHERE NoStaf = '" & nostaf & "' AND KodSubMenu = '" & KodSubMenu & "'"
        Dim dbconn As New DBKewConn
        If dbconn.fSelectCount(strPU) > 0 Then
            Return True
        End If

        Return False   'POWER USER
    End Function

    ''' <summary>
    ''' UTK KENALPASTI POWER USER 
    ''' KEGUNAAN TAPISAN DATA PTJ
    ''' </summary>
    ''' <param name="nostaf"></param>
    ''' <param name="KodSubMenu"></param>
    ''' <returns>
    ''' RETURN TRUE JIKA POWER USER
    ''' RETURN FALSE JIKA BUKAN POWER USER
    ''' </returns>
    Public Function fPowerUserExtra(nostaf, KodSubMenu)
        Dim strPU = "SELECT COUNT(KodSubMenu) AS BIL FROM MK_UPowerUser WHERE NoStaf = '" & nostaf & "' AND KodSubMenu = '" & KodSubMenu & "'"
        Dim dbconn As New DBKewConn

        If dbconn.fSelectCount(strPU) > 0 Then
            Return True   'POWER USER
        End If

        Dim strKodThp As String = ""
        strPU = "SELECT KODTAHAP FROM MK_UTahapDt WHERE NOSTAF = '" & nostaf & "' AND KODTAHAP  <> 'SMKB1'"
        dbconn.sSelectCommand(strPU, strKodThp)
        If Not String.IsNullOrEmpty(strKodThp) Then
            If Left(strKodThp, 1) = "A" Or Left(strKodThp, 1) = "B" Then
                Return True    'POWER USER
            End If
        End If

        Return False

    End Function

    ''' <summary>
    ''' Get CLM info
    ''' </summary>
    ''' <param name="strStaffID"></param>
    ''' <returns></returns>
    Public Function fGetUserCLM(strStaffID) As DataTable
        Dim dbconn As New DBClmConn
        Dim strSql As String = " select CLM_Tahap, CLM_LastLogin from CLM_Pengguna where CLM_loginID = '" & strStaffID & "';"
        Using ds = dbconn.fselectCommand(strSql)
            Return ds.Tables(0)
        End Using
    End Function


    ''' <summary>
    ''' Get user tahap
    ''' </summary>
    ''' <param name="strStaffID"></param>
    ''' <returns></returns>
    Public Function fGetUserTahap(strStaffID) As DataTable
        Dim dbconn As New DBKewConn
        'Dim strSql As String = "SELECT MK_UTahap.JenTahap, MK_UTahapDT.NoStaf, MK_UTahapDT.KodTahap FROM MK_UTahapDT INNER JOIN MK_UTahap ON MK_UTahapDT.KodTahap = MK_UTahap.KodTahap WHERE MK_UTahapDT.NoStaf = '" & strStaffID & "'"
        Dim strSql As String = "SELECT no_staf, kod_tahap from SMKB_UTahapDT where No_Staf  = '" & strStaffID & "'"
        'Select Case no_staf, kod_tahap from SMKB_UTahapDT where No_Staf = '01662'
        Using dt = dbconn.fSelectCommandDt(strSql)
            Return dt
        End Using
    End Function

    ''' <summary>
    ''' UTK KENALPASTI POWER USER 
    ''' KEGUNAAN TAPISAN DATA PTJ
    ''' </summary>
    ''' <param name="nostaf"></param>
    ''' <param name="KodSubMenu"></param>
    ''' <returns>
    ''' RETURN TRUE JIKA POWER USER
    ''' RETURN FALSE JIKA BUKAN POWER USER
    ''' </returns>
    Public Function fCheckPowerUser(nostaf, KodSubMenu)
        Dim strPU = "SELECT COUNT(KodSubMenu) AS BIL FROM MK_UPowerUser WHERE NoStaf = '" & nostaf & "' AND KodSubMenu = '" & KodSubMenu & "'"
        Dim dbconn As New DBKewConn

        If dbconn.fSelectCount(strPU) > 0 Then
            Return True   'POWER USER
        End If

        Dim strKodThp As String = ""
        strPU = "SELECT KODTAHAP FROM MK_UTahapDt WHERE NOSTAF = '" & nostaf & "' AND KODTAHAP  <> 'SMKB1'"
        dbconn.sSelectCommand(strPU, strKodThp)
        If Not String.IsNullOrEmpty(strKodThp) Then
            If Left(strKodThp, 1) = "A" Or Left(strKodThp, 1) = "B" Then
                Return True    'POWER USER
            End If
        End If

        Return False

    End Function


    Public Function fPopBulan() As DataSet

        Try
            Dim ds As New DataSet
            Dim dt As New DataTable
            Dim dcinBulan = New DataColumn("Id", GetType(String))
            Dim dcstrBulan = New DataColumn("Bulan", GetType(String))


            dt.Columns.Add(dcinBulan)
            dt.Columns.Add(dcstrBulan)

            dt.Rows.Add("01", "JANUARI")
            dt.Rows.Add("02", "FEBRUARI")
            dt.Rows.Add("03", "MAC")
            dt.Rows.Add("04", "APRIL")
            dt.Rows.Add("05", "MEI")
            dt.Rows.Add("06", "JUN")
            dt.Rows.Add("07", "JULAI")
            dt.Rows.Add("08", "OGOS")
            dt.Rows.Add("09", "SEPTEMBER")
            dt.Rows.Add("10", "OKTOBER")
            dt.Rows.Add("11", "NOVEMBER")
            dt.Rows.Add("12", "DISEMBER")

            ds = New DataSet()
            ds.Tables.Add(dt)
            Return ds

        Catch ex As Exception

        End Try

    End Function


    Public Function fPopTahun(ByVal intYearCnt As Integer) As DataSet
        Try
            Dim ds As New DataSet
            Dim dt As New DataTable
            Dim dcinBulan = New DataColumn("Val", GetType(Int32))
            Dim dcstrBulan = New DataColumn("Tahun", GetType(String))

            dt.Columns.Add(dcinBulan)
            dt.Columns.Add(dcstrBulan)

            Dim dtPrevYear As Date
            Dim StrPrevYear As String
            For i As Integer = 0 To intYearCnt - 1
                dtPrevYear = Now.AddYears(-i)
                StrPrevYear = dtPrevYear.Year()
                dt.Rows.Add(StrPrevYear, StrPrevYear)
            Next

            ds = New DataSet()
            ds.Tables.Add(dt)
            Return ds

        Catch ex As Exception

        End Try
    End Function

    ''' <summary>
    ''' Alert message
    ''' </summary>
    ''' <param name="msg"></param>
    ''' <param name="pg"></param>
    ''' <param name="tp"></param>
    Public Sub fGlobalAlert(msg As String, pg As Page, tp As Type)
        Dim sb As New StringBuilder()
        sb.Append("alert('")
        sb.Append(msg.Replace(vbLf, "\n").Replace(vbCr, "").Replace("'", "\'"))
        sb.Append("');")
        ScriptManager.RegisterStartupScript(pg, tp, "showalert", sb.ToString, True)
    End Sub

    ''' <summary>
    ''' Alert message and redirect page to new target URL
    ''' </summary>
    ''' <param name="msg"></param>
    ''' <param name="pg"></param>
    ''' <param name="tp"></param>
    ''' <param name="targetURL"></param>
    Public Sub fGlobalAlert(msg As String, pg As Page, tp As Type, targetURL As String)
        Dim sb As New StringBuilder()
        sb.Append("alert('")
        sb.Append(msg.Replace(vbLf, "\n").Replace(vbCr, "").Replace("'", "\'"))
        sb.Append($"');window.location='{targetURL}';")
        ScriptManager.RegisterStartupScript(pg, tp, "showalert", sb.ToString, True)
    End Sub
    'window.location.reload();

    Public Sub fGlobalAlertReload(msg As String, pg As Page, tp As Type)
        Dim sb As New StringBuilder()
        sb.Append("alert('")
        sb.Append(msg.Replace(vbLf, "\n").Replace(vbCr, "").Replace("'", "\'"))
        sb.Append($"');window.location.reload();")
        ScriptManager.RegisterStartupScript(pg, tp, "showalert", sb.ToString, True)
    End Sub

    ''' <summary>
    ''' Confirm message
    ''' Changed By: Hazrin
    ''' Date: 14/02/2018
    ''' </summary>
    ''' <param name="msg"></param>
    ''' <param name="pg"></param>
    ''' <param name="tp"></param>
    ''' <param name="fName">Function Name</param>
    Public Sub fGlobalConfirm(msg As String, pg As Page, tp As Type, fName As String)
        Dim jsFunction As New StringBuilder()
        jsFunction.Append($"{fName}('")
        jsFunction.Append(msg.Replace(vbLf, "\n").Replace(vbCr, "").Replace("'", "\'"))
        jsFunction.Append("');")
        'Dim jsFunction = $"{fName} ('{msg}');"
        ScriptManager.RegisterStartupScript(pg, tp, "confirm", jsFunction.ToString, True)

    End Sub



    '    Public Sub sAuditLog(UserID, SubMenuID, NamaTable, TransID, TransButiran, strPcIP, strPcName, strSysVer)
    '        Dim dbconn As New DBKewConn
    '        Try


    '            Dim strSql As String
    '            strSql = "insert into MK_AuditLog (UserID , SubMenuID , NamaTable , TransID , TransButiran , PcIP , PCName, SysVer )
    'values (@UserID,@SubMenuID,@NamaTable,@TransID,@TransButiran,@PcIP,@PCName, @SysVer)"

    '            Dim paramSql() As SqlParameter = {
    '                New SqlParameter("@UserID", UserID),
    '                New SqlParameter("@SubMenuID", SubMenuID),
    '                New SqlParameter("@NamaTable", NamaTable),
    '                 New SqlParameter("@TransID", TransID),
    '                New SqlParameter("@TransButiran", TransButiran),
    '                New SqlParameter("@PcIP", strPcIP),
    '                New SqlParameter("@PCName", strPcName),
    '                New SqlParameter("@SysVer", strSysVer)
    '            }

    '            dbconn.sConnBeginTrans()
    '            If dbconn.fInsertCommand(strSql, paramSql) > 0 Then
    '                dbconn.sConnCommitTrans()
    '            Else
    '                dbconn.sConnRollbackTrans()
    '            End If

    '        Catch ex As Exception
    '        Finally
    '            dbconn.sCloseConnection()
    '        End Try
    '    End Sub



    ''' <summary>
    ''' Arip,02526,07/11/2019 
    ''' Log transaction for audit log
    ''' </summary>
    ''' <param name="UserID"></param>
    ''' <param name="UserGroup"></param>
    ''' <param name="UserUbah"></param>
    ''' <param name="SubMenu"></param>
    ''' <param name="Keterangan"></param>
    ''' <param name="InfoTable"></param>
    ''' <param name="RefKey"></param>
    ''' <param name="RefNo"></param>
    ''' <param name="InfoMedan"></param>
    ''' <param name="InfoBaru"></param>
    ''' <param name="InfoLama"></param>
    ''' <param name="lsLogUsrIP"></param>
    ''' <param name="lsLogUsrPC"></param>
    Public Sub sAuditLog(UserID, UserGroup, UserUbah, SubMenu, Keterangan, InfoTable, RefKey, RefNo, InfoMedan, InfoBaru, InfoLama, lsLogUsrIP, lsLogUsrPC)
        Dim dbconn As New DBKewConn
        Try

            Dim strSql As String
            strSql = "insert into MK_AuditLog (UserID, UserGroup, UserUbah, SubMenu, Keterangan, InfoTable, RefKey, RefNo, InfoMedan, InfoBaru, " _
                    & " InfoLama, UserIP, PCName, Tarikh) " _
                    & " values (@UserID,@UserGroup,@UserUbah,@SubMenu,@Keterangan,@InfoTable,@RefKey, @RefNo,@InfoMedan,@InfoBaru,@InfoLama, " _
                    & " @UserIP,@PCName,@Tarikh)"

            Dim paramSql() As SqlParameter = {
                New SqlParameter("@UserID", UserID),
                New SqlParameter("@UserGroup", UserGroup),
                New SqlParameter("@UserUbah", UserUbah),
                 New SqlParameter("@SubMenu", SubMenu),
                New SqlParameter("@Keterangan", Keterangan),
                New SqlParameter("@InfoTable", InfoTable),
                New SqlParameter("@RefKey", RefKey),
                New SqlParameter("@RefNo", RefNo),
                New SqlParameter("@InfoMedan", InfoMedan),
                New SqlParameter("@InfoBaru", InfoBaru),
                New SqlParameter("@InfoLama", InfoLama),
                New SqlParameter("@UserIP", lsLogUsrIP),
                New SqlParameter("@PCName", lsLogUsrPC),
                New SqlParameter("@Tarikh", DateTime.Now)
            }

            dbconn.sConnBeginTrans()
            If dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                dbconn.sConnCommitTrans()
            Else
                dbconn.sConnRollbackTrans()
            End If

        Catch ex As Exception
        Finally
            dbconn.sCloseConnection()
        End Try
    End Sub


    Public Sub sErrLog(strErrNumber, strErrDesc, strErrForm, strErrEvent, strErrUID, strErrPcIP, strErrPcName, strErrSysVer)
        Dim dbconn As New DBKewConn
        Try
            Dim strSql As String
            strSql = "insert into [MK_UErrRpt] (ErrNumber , ErrDesc , ErrForm, ErrEvent ,ErrUID ,ErrPcIP,ErrPcName   ,ErrSysVer )
values (@ErrNumber, @ErrDesc, @errForm,@ErrEvent, @ErrUID,  @ErrPcIP, @ErrPcName, @ErrSysVer)"

            Dim paramSql() As SqlParameter = {
                New SqlParameter("@ErrNumber", strErrNumber),
                New SqlParameter("@ErrDesc", strErrDesc),
                New SqlParameter("@errForm", strErrForm),
                New SqlParameter("@ErrEvent", strErrEvent),
                 New SqlParameter("@ErrUID", strErrUID),
                New SqlParameter("@ErrPcIP", strErrPcIP),
                New SqlParameter("@ErrPcName", strErrPcName),
                New SqlParameter("@ErrSysVer", strErrSysVer)
            }

            dbconn.sConnBeginTrans()
            If dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                dbconn.sConnCommitTrans()
            Else
                dbconn.sConnRollbackTrans()
            End If

        Catch ex As Exception

        Finally
            dbconn.sCloseConnection()
        End Try
    End Sub

    Public Function fGetFormName() As String
        Try
            Dim Path As String = System.Web.HttpContext.Current.Request.Url.AbsolutePath
            Dim Info As System.IO.FileInfo = New System.IO.FileInfo(Path)
            Dim pageName As String = Info.Name
            Return pageName
        Catch ex As Exception

        End Try
    End Function

    Public Function fGetUserIP() As String
        Try
            Dim localIp As String
            For Each address As System.Net.IPAddress In System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName).AddressList
                If address.AddressFamily = Net.Sockets.AddressFamily.InterNetwork Then
                    localIp = address.ToString()
                    Return localIp
                End If
            Next

        Catch ex As Exception

        End Try
    End Function

    Public Function fGetBakiSebenar(ByVal year As Integer, ByVal tarikh As String, ByVal kw As String, ByVal ko As String, ByVal ptj As String, ByVal kp As String, ByVal vot As String) As Decimal
        Dim dbconn As New DBKewConn
        Dim bakiSebenar As Decimal = 0.00
        Try
            Dim param1 As SqlParameter = New SqlParameter("@arg_tahun", SqlDbType.Int)
            param1.Value = year
            param1.Direction = ParameterDirection.Input
            param1.IsNullable = False

            Dim param2 As SqlParameter = New SqlParameter("@arg_tarikh", SqlDbType.VarChar)
            param2.Value = tarikh
            param2.Direction = ParameterDirection.Input
            param2.IsNullable = False

            Dim param3 As SqlParameter = New SqlParameter("@arg_kw", SqlDbType.VarChar)
            param3.Value = kw
            param3.Direction = ParameterDirection.Input
            param3.IsNullable = False

            Dim param4 As SqlParameter = New SqlParameter("@arg_KO", SqlDbType.VarChar)
            param4.Value = ko
            param4.Direction = ParameterDirection.Input
            param4.IsNullable = False

            Dim param5 As SqlParameter = New SqlParameter("@arg_jbt", SqlDbType.VarChar)
            param5.Value = ptj
            param5.Direction = ParameterDirection.Input
            param5.IsNullable = False

            Dim param6 As SqlParameter = New SqlParameter("@arg_KP", SqlDbType.VarChar)
            param6.Value = kp
            param6.Direction = ParameterDirection.Input
            param6.IsNullable = True

            Dim param7 As SqlParameter = New SqlParameter("@arg_vot", SqlDbType.VarChar)
            param7.Value = vot
            param7.Direction = ParameterDirection.Input
            param7.IsNullable = False

            Dim param8 As SqlParameter = New SqlParameter("@l_bakisbnr", SqlDbType.Decimal)
            param8.Value = bakiSebenar
            param8.Direction = ParameterDirection.Output
            param8.IsNullable = False

            Dim paramSql() As SqlParameter = {param1, param2, param3, param4, param5, param6, param7, param8}

            Dim count = dbconn.fExecuteSP("USP_BAKISBNR", paramSql, param8, bakiSebenar)

        Catch ex As Exception

        End Try
        Return bakiSebenar
    End Function

    ''' <summary>
    ''' Get StatusDok for Perolehan
    ''' </summary>
    ''' <param name="kod"></param>
    ''' <returns></returns>
    Public Function fGetStatusDokPO(kod As String) As Dictionary(Of String, String)
        Dim strSql As String = $"Select Kod, Butiran from PO_StatusPP where Kod='{kod}'"

        Dim ds As New DataSet
        Dim dbconn As New DBKewConn
        ds = dbconn.fSelectCommand(strSql)

        Dim dt = ds.Tables(0)

        Dim dictStatusDok As New Dictionary(Of String, String)
        Dim NamaStatus = ""
        Dim KodStatusDok = ""

        If dt.Rows.Count > 0 Then
            NamaStatus = dt.Rows(0)("Butiran").ToString
            KodStatusDok = dt.Rows(0)("Kod").ToString
            dictStatusDok.Add(KodStatusDok, NamaStatus)
        End If

        Return dictStatusDok
    End Function

    ''' <summary>
    ''' Get Ulasan from every transaction of changing statusdok
    ''' </summary>
    ''' <param name="kodStatus"></param>
    ''' <param name="NoMohon"></param>
    ''' <param name="year"></param>
    ''' <returns></returns>
    Public Function fGetUlasanStatusPO(kodStatus As String, NoMohon As String, year As Integer) As String
        Dim strSql As String = $"select PO10_Ulasan from PO10_StatusDok where YEAR(PO10_Tkh) = {year} And PO_StatusPP='{kodStatus}' 
AND PO10_NoMohon ='{NoMohon}' order by PO10_Tkh desc"

        Dim dbconn As New DBKewConn
        Dim ds = dbconn.fSelectCommand(strSql)
        Return ds.Tables(0).Rows(0)(0).ToString 'dbconn.sSelectCommand(strSql, Ulasan)

    End Function

    Public Function fGetUlasanStatusJK(kodStatus As String, NoMohon As String, year As Integer) As String
        Dim strSql As String = $"select PO22_Ulasan from PO22_MesyPTeknikalDt where PO01_NoMohon ='{NoMohon}'"

        Dim dbconn As New DBKewConn
        Dim ds = dbconn.fSelectCommand(strSql)
        Return ds.Tables(0).Rows(0)(0).ToString 'dbconn.sSelectCommand(strSql, Ulasan)

    End Function

    Public Function fGetCheckJK(kodStatus As String, NoMohon As String, year As Integer) As String
        Dim strSql As String = $"select PO22_FlagSemakanJK from PO22_MesyPTeknikalDt where PO01_NoMohon ='{NoMohon}'"

        Dim dbconn As New DBKewConn
        Dim ds = dbconn.fSelectCommand(strSql)
        Return ds.Tables(0).Rows(0)(0).ToString 'dbconn.sSelectCommand(strSql, Ulasan)

    End Function

    ''' <summary>
    ''' Get Butiran KW
    ''' </summary>
    ''' <param name="strKodKW"></param>
    ''' <param name="strButiran"></param>
    ''' <returns></returns>
    Public Function fFindKW(ByVal strKodKW As String, ByRef strButiran As String)
        Try
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn

            Dim strSql As String = "select KodKw , Butiran  from MK_Kw where KodKw = '" & strKodKW & "'"
            ds = dbconn.fSelectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    strButiran = ds.Tables(0).Rows(0)("Butiran").ToString
                End If
            End If
        Catch ex As Exception

        End Try
    End Function

    ''' <summary>
    ''' Get butiran KO
    ''' </summary>
    ''' <param name="strKodKo"></param>
    ''' <param name="strButiran"></param>
    ''' <returns></returns>
    Public Function fFindKO(ByVal strKodKo As String, ByRef strButiran As String)
        Try
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn

            Dim strSql As String = "select KodKO , Butiran  from MK_KodOperasi where KodKO = '" & strKodKo & "'"
            ds = dbconn.fSelectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    strButiran = ds.Tables(0).Rows(0)("Butiran").ToString
                End If
            End If
        Catch ex As Exception

        End Try
    End Function

    ''' <summary>
    ''' get Butiran KP
    ''' </summary>
    ''' <param name="strKodKp"></param>
    ''' <param name="strButiran"></param>
    ''' <returns></returns>
    Public Function fFindKP(ByVal strKodKp As String, ByRef strButiran As String)
        Try
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn

            Dim strSql As String = "select KodProjek , Butiran  from MK_KodProjek where KodProjek = '" & strKodKp & "'"
            ds = dbconn.fSelectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    strButiran = ds.Tables(0).Rows(0)("Butiran").ToString
                End If
            End If
        Catch ex As Exception

        End Try
    End Function

    ''' <summary>
    ''' get Butiran Vot
    ''' </summary>
    ''' <param name="strKodVot"></param>
    ''' <returns></returns>
    Public Function fFindVot(ByVal strKodVot As String) As String
        Try
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim strButiran As String

            Dim strSql As String = "select Butiran  from mk_vot where KodVot = '" & strKodVot & "'"
            ds = dbconn.fSelectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    strButiran = ds.Tables(0).Rows(0)("Butiran").ToString
                    Return strButiran
                End If
            End If
        Catch ex As Exception

        End Try
    End Function

    ''' <summary>
    ''' get ButiranDasar
    ''' </summary>
    ''' <param name="strKodDasar"></param>
    ''' <param name="strButiran"></param>
    ''' <returns></returns>
    Public Function fFindDasar(ByVal strKodDasar As String, ByRef strButiran As String)
        Try
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn

            Dim strSql As String = "select BG_KodDasar , Butiran from BG_Dasar where BG_KodDasar  = '" & strKodDasar & "'"
            ds = dbconn.fSelectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    strButiran = ds.Tables(0).Rows(0)("Butiran").ToString
                End If
            End If
        Catch ex As Exception

        End Try
    End Function

    ''' <summary>
    ''' Get Butiran PTj
    ''' </summary>
    ''' <param name="strKodPTj"></param>
    ''' <param name="strButiran"></param>
    ''' <returns></returns>
    Public Function fFindPTj(ByVal strKodPTj As String, ByRef strButiran As String)
        Try
            Dim ds As New DataSet
            Dim dbconn As New DBKewConn

            Dim strSql As String = "select Butiran  from MK_PTJ where KodPTJ = '" & strKodPTj & "'"
            ds = dbconn.fSelectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    strButiran = ds.Tables(0).Rows(0)("Butiran").ToString
                End If
            End If
        Catch ex As Exception

        End Try
    End Function



    ''' <summary>
    ''' jana running number No Siri bg04_indkw,bg05_indptj,bg06_indobjam, bg07_indobjsbg, BG08_IndDasar
    ''' </summary>
    ''' <param name="pObjek"></param>
    ''' <param name="strTahun"></param>
    ''' <returns></returns>
    Public Function fGetNoSiri(ByVal pObjek As String, ByVal strTahun As String) As String
        Try
            Dim strSql As String
            Dim strLastIdx, strIdx As String
            Dim intIdx As Integer
            Dim strCol As String


            Dim strPrefix As String
            Select Case pObjek
                Case "KW"
                    strSql = "Select TOP 1 bg04_indkw from BG04_AgihKw WITH (NOLOCK) where bg04_tahun='" & Trim(strTahun) & "' order by bg04_indkw desc"
                    strCol = "bg04_indkw"
                    strPrefix = "K"

                Case "PTj"
                    strSql = "select TOP 1 bg05_indptj from BG05_AgihPTJ WITH (NOLOCK) where bg05_tahun='" & Trim(strTahun) & "' order by bg05_indptj desc"
                    strCol = "bg05_indptj"
                    strPrefix = "P"

                Case "OBJAM"
                    strSql = "select TOP 1 bg06_indobjam from BG06_AgihObjAm WITH (NOLOCK) where bg06_tahun='" & Trim(strTahun) & "' order by bg06_indobjam desc"
                    strCol = "bg06_indobjam"
                    strPrefix = "A"

                Case "OBJSBG"
                    strSql = "select TOP 1 bg07_indobjsbg from BG07_AgihObjSbg WITH (NOLOCK) where bg07_tahun='" & Trim(strTahun) & "' order by bg07_indobjsbg desc"
                    strCol = "bg07_indobjsbg"
                    strPrefix = "S"
                Case "DASAR"
                    strSql = "SELECT TOP 1 BG08_IndDasar  FROM BG08_AgihDasar WITH (NOLOCK) where BG08_Tahun ='" & Trim(strTahun) & "' ORDER BY BG08_IndDasar desc"
                    strCol = "BG08_IndDasar"
                    strPrefix = "D"
            End Select


            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            ds = dbconn.fSelectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    strLastIdx = ds.Tables(0).Rows(0)(strCol).ToString
                Else
                    strLastIdx = ""
                End If
            Else
                strLastIdx = ""
            End If

            If strLastIdx = "" Then
                strIdx = strTahun & strPrefix & "00001"
            Else
                strIdx = strLastIdx.Substring(strLastIdx.Length - 5)
                intIdx = CInt(strIdx)
                intIdx = intIdx + 1
                strIdx = strTahun & strPrefix & intIdx.ToString("D5")

            End If

            Return strIdx
        Catch ex As Exception

        End Try
    End Function

    Function fgetPerutk(intTahun As Integer, strTarikh As String, arg_kw As String, arg_ko As String, arg_ptj As String, arg_kp As String, arg_vot As String)
        Try

            Dim bakiSebenar As Decimal = 0.00
            Dim l_perutk As Double
            Dim l_net As Double
            Dim l_vir_masuk As Double
            Dim l_vir_keluar As Double
            Dim PTJ As String
            Dim l_bajetbf As Double
            Dim date1 = DateTime.ParseExact(strTarikh, "dd/MM/yyyy", New CultureInfo("en-US"))
            Dim tarikh = date1.ToString("yyyy-MM-dd") 'Format(strTarikh, "yyyy-MM-dd") 'DateTime.ParseExact(strTarikh, "dd/MM/yyyy", New CultureInfo("en-US"))

            PTJ = arg_ptj

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            Dim strsql As String = "select sum(mk09_debit) as jum from mk09_bajetbf where kodkw ='" & arg_kw & "' and KodKO ='" & arg_ko & "' and kodptj ='" & arg_ptj & "' and KodKP ='" & arg_kp & "' and kodvot ='" & arg_vot & "' and mk09_tahun = '" & intTahun & "'"
            ds = dbconn.fSelectCommand(strsql)

            'strsql = "select sum(mk09_debit) as jum from mk09_bajetbf where kodkw ='" & arg_kw & "' and kodptj = '" & arg_jbt & "' and kodvot ='' and kodakt ='' and mk09_tahun = ''"
            'Set rs = myDB("select sum(mk09_debit) as jum from mk09_bajetbf where kodkw ='" & arg_kw & "' and kodptj = '" & arg_jbt & "' and kodvot ='" & arg_vot & "' and kodakt ='" & arg_akt & "' and mk09_tahun = '" & arg_tahun & "'", adLockReadOnly)
            ds = dbconn.fSelectCommand(strsql)
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    l_bajetbf = IIf(IsDBNull(ds.Tables(0).Rows(0)("jum")), 0.00, ds.Tables(0).Rows(0)("jum"))
                    'Return True
                Else
                    'Return False
                    l_bajetbf = 0
                End If
                fgetPerutk = (l_perutk + l_bajetbf + l_net + l_vir_masuk - l_vir_keluar)
                'getPerutk = FormatNumber(l_perutk + l_bajetbf + l_net + l_vir_masuk - l_vir_keluar, 2)
            End If



            strsql = "select sum(BG07_Amaun) as jum from BG07_AgihObjSbg where KodKw  ='" & arg_kw & "' 
                        and KodKO ='" & arg_ko & "' and kodptj = '" & arg_ptj & "' and KodKP ='" & arg_kp & "' and kodvot ='" & arg_vot & "' 
                        and kodagih = 'AL' and BG07_TkhAgih <= Convert(DateTime,'" & tarikh & "', 102) and BG07_StatLulus='1'"

            'BG07_TkhAgih <= Convert(DateTime,'" & Format(arg_tarikh, "yyyy-mm-dd") & "', 102)
            ds = dbconn.fSelectCommand(strsql)
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    l_perutk = IIf(IsDBNull(ds.Tables(0).Rows(0)("jum")), 0.00, ds.Tables(0).Rows(0)("jum"))
                    'IIf(IsDBNull(ds.Tables(0).Rows(0)("jum")), 0.00, ds.Tables(0).Rows(0)("jum"))
                    'Return True
                Else
                    'Return False
                    l_perutk = 0
                End If

            End If

            strsql = "select sum(BG07_Amaun) as jum from BG07_AgihObjSbg where KodKw  ='" & arg_kw & "' and KodKO ='" & arg_ko & "' 
                        and kodptj = '" & arg_ptj & "' and KodKP ='" & arg_kp & "' and kodvot ='" & arg_vot & "' and kodagih = 'TB' 
                        and BG07_TkhAgih  =" & tarikh & " and BG07_StatLulus='1'"
            ds = dbconn.fSelectCommand(strsql)
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    l_net = IIf(IsDBNull(ds.Tables(0).Rows(0)("jum")), 0.00, ds.Tables(0).Rows(0)("jum"))
                    'Return True
                Else
                    'Return False
                    l_net = 0
                End If

            End If

            strsql = "SELECT SUM(BG10_Viremen.BG10_Amaun ) AS jum FROM BG10_Viremen INNER JOIN BG10_ViremenDT ON BG10_Viremen.BG10_NoViremen  = BG10_ViremenDT.BG10_NoViremen
                        INNER JOIN BG12_StatusDok  ON BG10_Viremen.BG10_NoViremen = BG12_StatusDok.BG10_NoViremen 
                        WHERE BG10_ViremenDT.KodKw = '" & arg_kw & "' AND BG10_ViremenDT.KodKO  = '" & arg_ko & "' and BG10_ViremenDT.KodPTJ = '" & arg_ptj & "' AND BG10_ViremenDT.KodKP  = '" & arg_kp & "' 
                        AND BG10_ViremenDT.KodVot = '" & arg_vot & "' AND BG10_ViremenDT.KodVir = 'M' AND BG10_Viremen.BG10_StatusKJ  = '1' AND BG10_Viremen.BG10_StatusBen  = '1' and year(BG12_StatusDok.BG12_TkhProses ) = '" & intTahun & "' 
                        and BG12_StatusDok.BG12_TkhProses  ='" & tarikh & "' and BG12_StatusDok.kodstatusdok='05'"

            ds = dbconn.fSelectCommand(strsql)
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    l_vir_masuk = IIf(IsDBNull(ds.Tables(0).Rows(0)("jum")), 0.00, ds.Tables(0).Rows(0)("jum"))
                    'Return True
                Else
                    'Return False
                    l_vir_masuk = 0
                End If

            End If

            strsql = "SELECT SUM(BG10_Viremen.BG10_Amaun ) AS jum FROM BG10_Viremen INNER JOIN BG10_ViremenDT ON BG10_Viremen.BG10_NoViremen  = BG10_ViremenDT.BG10_NoViremen
                        INNER JOIN BG12_StatusDok  ON BG10_Viremen.BG10_NoViremen = BG12_StatusDok.BG10_NoViremen 
                        WHERE BG10_ViremenDT.KodKw = '" & arg_kw & "' AND BG10_ViremenDT.KodKO  = '" & arg_ko & "' and BG10_ViremenDT.KodPTJ = '" & arg_ptj & "' AND BG10_ViremenDT.KodKP  = '" & arg_kp & "' 
                        AND BG10_ViremenDT.KodVot = '" & arg_vot & "' AND BG10_ViremenDT.KodVir = 'K' AND BG10_Viremen.BG10_StatusKJ  = '1' AND BG10_Viremen.BG10_StatusBen  = '1' and year(BG12_StatusDok.BG12_TkhProses ) = '" & intTahun & "' 
                        and BG12_StatusDok.BG12_TkhProses  ='" & tarikh & "' and BG12_StatusDok.kodstatusdok='05'"

            ds = dbconn.fSelectCommand(strsql)
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    l_vir_keluar = IIf(IsDBNull(ds.Tables(0).Rows(0)("jum")), 0.00, ds.Tables(0).Rows(0)("jum"))
                    'Return True
                Else
                    'Return False
                    l_vir_masuk = 0
                End If

            End If
        Catch ex As Exception
            Return False
        End Try

    End Function
    Function fgetBlj(intTahun As Integer, strTarikh As String, arg_kw As String, arg_ko As String, arg_ptj As String, arg_kp As String, arg_vot As String)
        Try
            Dim date1 = DateTime.ParseExact(strTarikh, "dd/MM/yyyy", New CultureInfo("en-US"))
            Dim tarikh = date1.ToString("yyyy-MM-dd")
            Dim l_jrnl_debit As Double
            Dim l_jrnl_kredit As Double

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            Dim strSql As String

            strSql = "select sum(mk06_debit) as jumdebit from mk06_transaksi where mk06_tkhtran ='" & tarikh & "' and year(mk06_tkhtran) = '" & intTahun & "' and kodkw = '" & arg_kw & "' and KodKO ='" & arg_ko & "' and kodptj ='" & arg_ptj & "' and KodKP ='" & arg_kp & "'and substring(kodvot,1,2) ='" & arg_vot & "'  and substring(MK06_RUJUKAN,1,2) in ('JK','RT','JP','BK','RS','BP')"
            '"select sum(mk06_debit) as jumdebit from mk06_transaksi where mk06_tkhtran <= CONVERT(DATETIME,'" & Format(arg_tarikh, "yyyy-mm-dd") & "', 102) and year(mk06_tkhtran) = '" & arg_tahun & "' and kodkw = '" & arg_kw & "' and kodptj ='" & arg_ptj & "' and substring(kodvot,1,2) ='" & refvot & "' and kodakt='" & arg_akt & "' and substring(MK06_RUJUKAN,1,2) in ('JK','RT','JP','BK','RS','BP')"
            ds = dbconn.fSelectCommand(strSql)
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    l_jrnl_debit = IIf(IsDBNull(ds.Tables(0).Rows(0)("jumdebit")), 0.00, ds.Tables(0).Rows(0)("jumdebit"))                    'Return True
                Else
                    'Return False
                    l_jrnl_debit = 0

                End If
                'Else
                'Return False
            End If

            strSql = "select sum(mk06_kredit) as jumkredit from mk06_transaksi where mk06_tkhtran= '" & tarikh & "' and year(mk06_tkhtran) = '" & intTahun & "' and kodkw = '" & arg_kw & "' and KodKO ='" & arg_ko & "' and kodptj ='" & arg_ptj & "' and KodKP ='" & arg_kp & "' and substring(kodvot,1,2) ='" & arg_vot & "'  and substring(MK06_RUJUKAN,1,2) in ('JK','RT','JP','BK','RS','BP')"
            'strSql = ("select sum(mk06_kredit) as jumkredit from mk06_transaksi where mk06_tkhtran <= CONVERT(DATETIME,'" & Format(arg_tarikh, "yyyy-mm-dd") & "', 102) and year(mk06_tkhtran) = '" & arg_tahun & "' and kodkw = '" & arg_kw & "' and kodptj ='" & arg_ptj & "' and substring(kodvot,1,2) ='" & refvot & "' and kodakt='" & arg_akt & "' and substring(MK06_RUJUKAN,1,2) in ('JK','RT','JP','BK','RS','BP')", adLockReadOnly)
            ds = dbconn.fSelectCommand(strSql)
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    l_jrnl_kredit = IIf(IsDBNull(ds.Tables(0).Rows(0)("jumkredit")), 0.00, ds.Tables(0).Rows(0)("jumkredit"))
                    'l_jrnl_debit = 0
                    'Return True
                Else

                    l_jrnl_kredit = 0

                End If

                'Return False
            End If

            fgetBlj = l_jrnl_debit - l_jrnl_kredit

        Catch ex As Exception
            'Return False
        End Try


    End Function
    Function fgetLstTng(intTahun As Integer, strTarikh As String, arg_kw As String, arg_ko As String, arg_ptj As String, arg_kp As String, arg_vot As String)
        Dim tng_debit As Double
        Dim tng_kredit As Double

        Dim ds As New DataSet
        Dim dbconn As New DBKewConn
        Dim strsql As String


        strsql = "SELECT sum(a.MK06_Debit) as jumdebit FROM MK06_Transaksi a,PO19_PT b WHERE a.MK06_NoDok = b.PO19_NoPT And b.PO19_StatusCF = '1' 
                    and a.KodDok in ('CF_LO','CF_INDEN') And a.KodKw = '" & arg_kw & "' and a.KodKO ='" & arg_ko & "' AND a.KodPTJ = '" & arg_ptj & "' and a.KodKP ='" & arg_kp & "' 
                    AND substring(a.KodVot,1,2) = '" & arg_vot & "' And Year(a.MK06_TkhTran) = '" & intTahun & "'"
        ds = dbconn.fSelectCommand(strsql)
        If Not ds Is Nothing Then
            If ds.Tables(0).Rows.Count > 0 Then
                tng_debit = IIf(IsDBNull(ds.Tables(0).Rows(0)("jumdebit")), 0.00, ds.Tables(0).Rows(0)("jumdebit"))
                'l_jrnl_debit = 0
                'Return True
            Else

                tng_debit = 0

            End If

            'Return False
        End If


        strsql = "SELECT sum(a.MK06_kredit) as jumkredit FROM MK06_Transaksi a,PO19_PT b WHERE a.MK06_NoDok = b.PO19_NoPT And b.PO19_StatusCF = '1' 
                    and a.KodDok in ('CF_LO','CF_INDEN') And a.KodKw = '" & arg_kw & "' and a.KodKO ='" & arg_ko & "' AND a.KodPTJ = '" & arg_ptj & "' and a.KodKP ='" & arg_kp & "' 
                    AND substring(a.KodVot,1,2) = '" & arg_vot & "' And Year(a.MK06_TkhTran) = '" & intTahun & "'"

        ds = dbconn.fSelectCommand(strsql)
        If Not ds Is Nothing Then
            If ds.Tables(0).Rows.Count > 0 Then
                tng_kredit = IIf(IsDBNull(ds.Tables(0).Rows(0)("jumkredit")), 0.00, ds.Tables(0).Rows(0)("jumkredit"))
                'l_jrnl_debit = 0
                'Return True
            Else

                tng_kredit = 0

            End If

            'Return False
        End If

        fgetLstTng = tng_debit - tng_kredit

    End Function
    Function fgetBakiTng(intTahun As Integer, strTarikh As String, arg_kw As String, arg_ko As String, arg_ptj As String, arg_kp As String, arg_vot As String)
        Dim l_jrnl_debit As Double
        Dim l_jrnl_kredit As Double
        Dim l_net_tng As Double
        Dim l_sipiutang As Double
        Dim l_blj As Double
        Dim l_cek As Double
        Dim l_btlbaucer As Double

        Dim l_net_inden As Double
        Dim l_jumgl As Double
        Dim l_jrnl_debitlo As Double
        Dim l_jrnl_kreditlo As Double

        Dim ds As New DataSet
        Dim dbconn As New DBKewConn
        Dim strsql As String

        strsql = "select sum(mk06_debit) as jumdebit from mk06_transaksi where year(mk06_transaksi.mk06_tkhtran) = '" & intTahun & "' and mk06_tkhtran ='" & strTarikh & "'
                and mk06_transaksi.kodkw = '" & arg_kw & "' and mk06_transaksi.KodKO  = '" & arg_ko & "' and mk06_transaksi.kodptj ='" & arg_ptj & "' and mk06_transaksi.KodKP  = '" & arg_kp & "' and substring(mk06_transaksi.kodvot,1,2) ='" & arg_vot & "' 
                and (koddok ='LO' or koddok = 'ADJ_LO')"

        ds = dbconn.fSelectCommand(strsql)
        If Not ds Is Nothing Then
            If ds.Tables(0).Rows.Count > 0 Then
                l_jrnl_debit = IIf(IsDBNull(ds.Tables(0).Rows(0)("jumdebit")), 0.00, ds.Tables(0).Rows(0)("jumdebit"))
                'l_jrnl_debit = 0
                'Return True
            Else

                l_jrnl_debit = 0

            End If

            'Return False
        End If

        strsql = "select sum(mk06_kredit) as jumkredit from mk06_transaksi where year(mk06_transaksi.mk06_tkhtran) = '" & intTahun & "' and mk06_tkhtran ='" & strTarikh & "'
                and mk06_transaksi.kodkw = '" & arg_kw & "' and mk06_transaksi.KodKO  = '" & arg_ko & "' and mk06_transaksi.kodptj ='" & arg_ptj & "' and mk06_transaksi.KodKP  = '" & arg_kp & "' and substring(mk06_transaksi.kodvot,1,2) ='" & arg_vot & "' 
                and (koddok ='LO' or koddok = 'ADJ_LO')"

        ds = dbconn.fSelectCommand(strsql)
        If Not ds Is Nothing Then
            If ds.Tables(0).Rows.Count > 0 Then
                l_jrnl_kredit = IIf(IsDBNull(ds.Tables(0).Rows(0)("jumkredit")), 0.00, ds.Tables(0).Rows(0)("jumkredit"))
                'l_jrnl_debit = 0
                'Return True
            Else

                l_jrnl_kredit = 0

            End If

            'Return False
        End If
        l_net_tng = l_jrnl_debit - l_jrnl_kredit

        strsql = "select sum(mk06_debit)-sum(mk06_kredit) as JumInden from mk06_transaksi where year(mk06_transaksi.mk06_tkhtran) = '" & intTahun & "' 
                    and mk06_tkhtran ='" & strTarikh & "' and mk06_transaksi.kodkw = '" & arg_kw & "' and mk06_transaksi.KodKO  = '" & arg_ko & "' 
                    and mk06_transaksi.KodPTJ  ='" & arg_ptj & "' and mk06_transaksi.KodKP  = '" & arg_kp & "'  and substring(mk06_transaksi.kodvot,1,2) ='" & arg_vot & "' 
                    and (koddok ='INDEN' or koddok = 'ADJ_INDEN')"

        ds = dbconn.fSelectCommand(strsql)
        If Not ds Is Nothing Then
            If ds.Tables(0).Rows.Count > 0 Then
                l_net_inden = IIf(IsDBNull(ds.Tables(0).Rows(0)("JumInden")), 0.00, ds.Tables(0).Rows(0)("JumInden"))
                'l_jrnl_debit = 0
                'Return True
            Else

                l_net_inden = 0

            End If

            'Return False
        End If

        strsql = "select sum(mk06_debit) as JumPiutang from mk06_transaksi where year(mk06_transaksi.mk06_tkhtran) = '" & intTahun & "'
                    and mk06_tkhtran ='" & strTarikh & "' and mk06_transaksi.kodkw = '" & arg_kw & "' and mk06_transaksi.KodKO  = '" & arg_ko & "' 
                    and mk06_transaksi.kodptj ='" & arg_ptj & "' and mk06_transaksi.KodKP  = '" & arg_kp & "' and substring(mk06_transaksi.kodvot,1,2) ='" & arg_vot & "' 
                    and koddok ='GL-TP'"

        ds = dbconn.fSelectCommand(strsql)
        If Not ds Is Nothing Then
            If ds.Tables(0).Rows.Count > 0 Then
                l_sipiutang = IIf(IsDBNull(ds.Tables(0).Rows(0)("JumPiutang")), 0.00, ds.Tables(0).Rows(0)("JumPiutang"))
                'l_jrnl_debit = 0
                'Return True
            Else

                l_sipiutang = 0

            End If

            'Return False
        End If

        strsql = "select sum(mk06_debit) as JumBlj From mk06_transaksi,ap04_baucar Where mk06_transaksi.mk06_rujukan = ap04_baucar.ap04_nobaucar
                    and ap04_baucar.ap02_JENIS in ('J01','J09')  and (ap04_baucar.ap04_NOBAUCAR is not null or ap04_baucar.ap04_NOBAUCAR <> '')
                    and mk06_transaksi.koddok = 'AP-CQ' and mk06_transaksi.kodKW = '" & arg_kw & " ' and mk06_transaksi.KodKO  = '" & arg_ko & "' and mk06_transaksi.kodPTJ = '" & arg_ptj & "' and mk06_transaksi.KodKP  = '" & arg_kp & "'
                    and substring(mk06_transaksi.kodVOT,1,2) = '" & arg_vot & "'  and year(mk06_transaksi.mk06_TKHTRAN) = '" & intTahun & "' and mk06_transaksi.mk06_TKHTRAN ='" & strTarikh & "'"

        ds = dbconn.fSelectCommand(strsql)
        If Not ds Is Nothing Then
            If ds.Tables(0).Rows.Count > 0 Then
                l_blj = IIf(IsDBNull(ds.Tables(0).Rows(0)("JumBlj")), 0.00, ds.Tables(0).Rows(0)("JumBlj"))
                'l_jrnl_debit = 0
                'Return True
            Else

                l_blj = 0

            End If

            'Return False
        End If

        'Jumlah Batal Cek
        strsql = "select isnull(sum(mk06_kredit),0) as JumCek From mk06_transaksi, ap04_baucar, ap05_baucarnominees
        Where ap04_baucar.ap04_nodraf = ap05_baucarnominees.ap04_nodraf And mk06_transaksi.mk06_nodok = ap05_baucarnominees.ap05_nocek
        And year(mk06_transaksi.mk06_TKHTRAN) = year(ap05_baucarnominees.ap05_TKHBATALCEK)
        And substring(mk06_transaksi.mk06_rujukan,1,2) = 'JK' and ap04_baucar.ap02_jenis in ('J01','J09')
        And (ap04_baucar.ap04_NOBAUCAR Is Not null Or ap04_baucar.ap04_NOBAUCAR <> '')  and mk06_transaksi.koddok = 'CEK'
        And mk06_transaksi.kodKW = '" & arg_kw & "' and mk06_transaksi.KodKO  = '" & arg_ko & "' and mk06_transaksi.kodPTJ = '" & arg_ptj & "' and mk06_transaksi.KodKP  = '" & arg_kp & "'
        And substring(mk06_transaksi.kodVOT,1,2) = '" & arg_vot & "' and year(mk06_transaksi.mk06_TKHTRAN) = '" & intTahun & "' And mk06_transaksi.mk06_TKhTRAN ='" & strTarikh & "'"

        ds = dbconn.fSelectCommand(strsql)
        If Not ds Is Nothing Then
            If ds.Tables(0).Rows.Count > 0 Then
                l_cek = IIf(IsDBNull(ds.Tables(0).Rows(0)("JumCek")), 0.00, ds.Tables(0).Rows(0)("JumCek"))

                'Return True
            Else

                l_cek = 0

            End If

            'Return False
        End If


        'Jumlah Batal Baucar
        strsql = "select isnull(sum(mk06_kredit),0) as JumBaucer From mk06_transaksi, ap04_baucar 
                    Where mk06_transaksi.mk06_nodok = ap04_baucar.ap04_nobaucar 
                    and ap04_baucar.ap02_JENIS in ('J01','J09') and (ap04_baucar.ap04_NOBAUCAR is not null or ap04_baucar.ap04_NOBAUCAR <> '')
                    and mk06_transaksi.koddok = 'AP-CV' and mk06_transaksi.kodKW = '" & arg_kw & "' and mk06_transaksi.KodKO  = '" & arg_ko & "' and mk06_transaksi.kodPTJ = '" & arg_ptj & "' and mk06_transaksi.KodKP = '" & arg_kp & "'
                    and substring(mk06_transaksi.kodVOT,1,2)  = '" & arg_vot & "'  and year(mk06_transaksi.mk06_TKHTRAN) = '" & intTahun & "'
                    and mk06_transaksi.mk06_TKHTRAN ='" & strTarikh & "' and substring(mk06_transaksi.mk06_rujukan,1,2) = 'JK'"

        ds = dbconn.fSelectCommand(strsql)
        If Not ds Is Nothing Then
            If ds.Tables(0).Rows.Count > 0 Then
                l_btlbaucer = IIf(IsDBNull(ds.Tables(0).Rows(0)("JumBaucer")), 0.00, ds.Tables(0).Rows(0)("JumBaucer"))

                'Return True
            Else

                l_btlbaucer = 0

            End If

            'Return False
        End If





        'JUMLAH JRNL LO
        strsql = "select isnull(sum(mk06_debit),0) as jumdt from mk06_transaksi d,gl01_jrnltran Where D.mk06_rujukan = gl01_jrnltran.gl01_nojrnl
                    and substring(d.koddok,1,2) = 'GL' and d.mk06_kodap in ('J01','J09')
                    and substring(d.mk06_rujukan,1,2) in ('JK','JP') and d.mk06_debit > 0 and d.kodKW = '" & arg_kw & "' and d.KodKO ='" & arg_ko & "' and d.KodPTJ ='" & arg_ptj & "' and d.KodKP ='" & arg_kp & "'
                    and substring(d.kodvot,1,2) = '" & arg_vot & "'  and year(d.mk06_TKHTRAN)  = '" & intTahun & "'
                    and d.mk06_TKHTRAN ='" & strTarikh & "' and (gl01_jrnltran.gl01_statuslejar is null or gl01_jrnltran.gl01_statuslejar=0)"


        ds = dbconn.fSelectCommand(strsql)
        If Not ds Is Nothing Then
            If ds.Tables(0).Rows.Count > 0 Then
                l_jrnl_debitlo = IIf(IsDBNull(ds.Tables(0).Rows(0)("jumdt")), 0.00, ds.Tables(0).Rows(0)("jumdt"))

                'Return True
            Else

                l_jrnl_debitlo = 0

            End If

            'Return False
        End If

        strsql = "select isnull(sum(mk06_kredit),0) as jumkt from mk06_transaksi d,gl01_jrnltran Where D.mk06_rujukan = gl01_jrnltran.gl01_nojrnl
                    and substring(d.koddok,1,2) = 'GL' and d.mk06_kodap in ('J01','J09')
                    and substring(d.mk06_rujukan,1,2) in ('JK','JP') and d.mk06_kredit > 0 and d.kodKW = '" & arg_vot & "' and d.KodKO ='" & arg_ko & "' and d.KodPTJ ='" & arg_ptj & "' and d.KodKP ='" & arg_kp & "'
                    and substring(d.kodvot,1,2) = '" & arg_vot & "'  and year(d.mk06_TKHTRAN)  = '" & intTahun & "'
                    and d.mk06_TKHTRAN ='" & strTarikh & "'  and (gl01_jrnltran.gl01_statuslejar is null or gl01_jrnltran.gl01_statuslejar=0)"

        ds = dbconn.fSelectCommand(strsql)
        If Not ds Is Nothing Then
            If ds.Tables(0).Rows.Count > 0 Then
                l_jrnl_kreditlo = IIf(IsDBNull(ds.Tables(0).Rows(0)("jumkt")), 0.00, ds.Tables(0).Rows(0)("jumkt"))

                'Return True
            Else

                l_jrnl_kreditlo = 0

            End If

            'Return False
        End If

        fgetBakiTng = (l_net_tng + l_net_inden) - ((l_blj + l_sipiutang) - l_cek - l_btlbaucer) - (l_jrnl_debitlo - l_jrnl_kreditlo)

    End Function

    Public Function fCheckRec(ByVal strSql As String) As Int16
        Dim dbConn2 As New DBKewConn
        Try
            Return dbConn2.fSelectCount(strSql)
        Catch ex As Exception
        Finally
            dbConn2.sCloseConnection()
        End Try
    End Function

    Public Function fGetRec(strSql As String) As DataSet
        Dim ds As New DataSet
        Dim dbConn3 As New DBKewConn
        Try
            ds = dbConn3.fSelectCommand(strSql)
            Return ds
        Catch ex As Exception

        Finally
            dbConn3.sCloseConnection()

        End Try
    End Function

    ''' <summary>
    ''' Get single email
    ''' </summary>
    ''' <param name="userID"></param>
    ''' <returns></returns>
    Public Function fGetEmail(userID As String) As String
        Dim strSqlEmailPemohon = $"select MS01_Email FROM MS01_Peribadi WHERE MS01_NoStaf = '{userID}';"
        Dim userEmail As String = ""
        Dim dbConn As New DBSMConn
        dbConn.sSelectCommand(strSqlEmailPemohon, userEmail)
        Return userEmail
    End Function

    ''' <summary>
    ''' Get email for one user from kod tahap for certain PTJ
    ''' </summary>
    ''' <param name="KodTahap"></param>
    ''' <returns></returns>
    Public Function fGetEmailKumpTahapPTJ(KodTahap As String, KodPTJ As String) As String
        Dim strSqlNoStaf = $"select a.MS01_email FROM {DBStaf}MS01_Peribadi a, 
{DBStaf}MS08_Penempatan d, {DBStaf}MS_Pejabat e, MK_UTahapDT b
WHERE a.MS01_NoStaf = b.NoStaf
AND e.KodPejabat = d.MS08_Pejabat 
AND d.MS01_NoStaf = a.MS01_NoStaf
AND d.MS08_StaTerkini = 1
AND e.KodPejabat='{Left(KodPTJ, 2)}'
AND b.KodTahap = '{KodTahap}';"
        Dim usersEmail As String = ""
        Dim dbConn As New DBKewConn
        dbConn.sSelectCommand(strSqlNoStaf, usersEmail)
        Return usersEmail
    End Function

    ''' <summary>
    ''' Get all emails from all users that assigns from certain tahap.
    ''' </summary>
    ''' <param name="KodTahap"></param>
    ''' <returns></returns>
    Public Function fGetEmailKumpTahapBendahari(KodTahap As String) As String
        Dim strSql = $"select a.MS01_email FROM {DBStaf}MS01_Peribadi a, MK_UTahapDT b
WHERE a.MS01_NoStaf = b.NoStaf
AND b.KodTahap = '{KodTahap}';"
        Dim SendEmail As New StringBuilder(String.Empty)
        Dim numberRow = 0
        Dim dbConn As New DBKewConn
        Using dt = dbConn.fSelectCommandDt(strSql)
            For Each dr In dt.Rows
                numberRow += 1
                If numberRow > 1 Then
                    SendEmail.Append(", ")
                End If
                SendEmail.Append(dr("MS01_email"))
            Next
        End Using
        Return SendEmail.ToString
    End Function

    Public Function fGetStafPTJ(kodPtj) As DataTable
        Dim dbconn As New DBSMConn

        Dim strSql As String = $"select a.MS01_NoStaf as NoStaf, a.MS01_Nama, c.JawGiliran , e.Pejabat, e.KodPejabat, d.MS08_Bahagian, d.MS08_Unit,   
                                    (a.MS01_NoStaf + ' - ' + a.MS01_Nama) As Butiran    
                                    FROM MS01_Peribadi a, ms02_perjawatan b, MS_Jawatan c ,MS08_Penempatan d, MS_Pejabat e
                                    WHERE e.KodPejabat = '{Left(kodPtj, 2)}'
                                    AND b.MS01_NoStaf = a.MS01_NoStaf 
                                    AND b.ms02_jawsandang = c.KodJawatan  
                                    AND e.KodPejabat = d.MS08_Pejabat 
                                    AND d.MS01_NoStaf = a.MS01_NoStaf
                                    AND d.MS08_StaTerkini = 1
                                    ORDER BY a.MS01_Nama;"

        Dim dt = dbconn.fselectCommandDt(strSql)
        Return dt
    End Function

    Public Function fGetUserInfo2(strStaffID) As DataSet

        Try
            Dim ds As New DataSet
            Dim dbconn As New DBSMConn
            Dim dt As New DataTable
            Dim strSql As String = $"select a.MS01_Nama, c.JawGiliran , e.Pejabat, e.KodPejabat, d.MS08_Bahagian, d.MS08_Unit       
                                    FROM MS01_Peribadi a, ms02_perjawatan b, MS_Jawatan c ,MS08_Penempatan d, MS_Pejabat e
                                    WHERE a.MS01_NoStaf = '{strStaffID}'
                                    AND b.MS01_NoStaf = a.MS01_NoStaf 
                                    AND b.ms02_jawsandang = c.KodJawatan  
                                    AND e.KodPejabat = d.MS08_Pejabat 
                                    AND d.MS01_NoStaf = a.MS01_NoStaf
                                    AND d.MS08_StaTerkini = 1"

            ds = dbconn.fselectCommand(strSql)
            Return ds



        Catch ex As Exception
            'fErrorLog(ex.Message.ToString)
        End Try

    End Function

    Public Function fGetUserName(strStaffID) As String
        Dim strSqlStaf = $"select MS01_Nama FROM MS01_Peribadi WHERE MS01_NoStaf = '{strStaffID}';"

        Dim dbSMconn As New DBSMConn
        Dim namaPemohon As String = ""
        dbSMconn.sSelectCommand(strSqlStaf, namaPemohon)

        Return namaPemohon
    End Function

    Public Function fGetUserJawatan(strStaffID) As String
        Dim strSqlStaf = $"SELECT  c.JawGiliran
FROM MS01_Peribadi a, MS02_Perjawatan AS b, MS_Jawatan AS c 
WHERE a.MS01_NoStaf=b.MS01_NoStaf And c.KodJawatan = b.MS02_JawSandang AND a.MS01_NoStaf = '{strStaffID}';"

        Dim dbSMconn As New DBSMConn
        Dim namaJawatan As String = ""
        dbSMconn.sSelectCommand(strSqlStaf, namaJawatan)

        Return namaJawatan
    End Function

    Public Function fGetUserPTJ(strStaffID) As String
        Dim strSqlStaf = $"select e.Pejabat
FROM MS01_Peribadi a, MS08_Penempatan d, MS_Pejabat e
WHERE a.MS01_NoStaf = '{strStaffID}'
AND e.KodPejabat = d.MS08_Pejabat 
AND d.MS01_NoStaf = a.MS01_NoStaf
AND d.MS08_StaTerkini = 1;"

        Dim dbSMconn As New DBSMConn
        Dim namaJawatan As String = ""
        dbSMconn.sSelectCommand(strSqlStaf, namaJawatan)

        Return namaJawatan
    End Function

    Public Sub fGetZonning(idStaf As String, ByRef Zon As String, ByRef NamaZon As String)
        Dim str = $"select a.Zon, NamaZon from RC_ZonStaf a, RC_Zon b Where b.Zon = a.Zon AND a.NoStaf ='{idStaf}';"
        Dim dbconn As New DBKewConn
        Zon = ""
        NamaZon = ""
        Using ds = dbconn.fSelectCommand(str)
            If ds IsNot Nothing And ds.Tables(0).Rows.Count > 0 Then
                Zon = ds.Tables(0).Rows(0)("Zon")
                NamaZon = ds.Tables(0).Rows(0)("NamaZon")
            End If
        End Using
    End Sub

    Public Function fGetKodJen(ByVal strKodVot As String, ByRef strKodJen As String, ByRef strKodjenlanjut As String) As String
        Try
            Dim strSql As String
            Dim dbconn As New DBKewConn

            strSql = "select kodjen,kodjenlanjut from mk_vot with (nolock) where kodvot='" & strKodVot & "'"
            Dim ds As New DataSet
            ds = dbconn.fSelectCommand(strSql)
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    strKodJen = ds.Tables(0).Rows(0)("kodjen").ToString
                    strKodjenlanjut = ds.Tables(0).Rows(0)("kodjenlanjut").ToString
                End If
            End If
        Catch ex As Exception

        End Try
    End Function

    Public Function fgetBakiTabung(arg_tahun As Integer, arg_tarikh As String, arg_kw As String, arg_ptj As String) As Decimal
        Dim dbconn As New DBKewConn

        Dim bakiTabung As Decimal = 0.00
        Try
            Dim param1 As SqlParameter = New SqlParameter("@arg_tahun", SqlDbType.Int)
            param1.Value = arg_tahun
            param1.Direction = ParameterDirection.Input
            param1.IsNullable = False

            Dim param2 As SqlParameter = New SqlParameter("@arg_tarikh", SqlDbType.VarChar)
            param2.Value = arg_tarikh
            param2.Direction = ParameterDirection.Input
            param2.IsNullable = False

            Dim param3 As SqlParameter = New SqlParameter("@arg_kw", SqlDbType.VarChar)
            param3.Value = arg_kw
            param3.Direction = ParameterDirection.Input
            param3.IsNullable = False

            Dim param4 As SqlParameter = New SqlParameter("@arg_jbt", SqlDbType.VarChar)
            param4.Value = arg_ptj
            param4.Direction = ParameterDirection.Input
            param4.IsNullable = False

            Dim param5 As SqlParameter = New SqlParameter("@l_bakitabung", SqlDbType.Decimal)
            param5.Value = bakiTabung
            param5.Direction = ParameterDirection.Output
            param5.IsNullable = False

            Dim paramSql() As SqlParameter = {param1, param2, param3, param4, param5}

            Dim count = dbconn.fExecuteSP("USP_BAKITABUNG", paramSql, param5, bakiTabung)

        Catch ex As Exception

        End Try
        Return bakiTabung
    End Function

    Public Function fgetBakiSMS(arg_tahun As Integer, arg_tarikh As String, arg_kw As String, arg_ko As String, arg_ptj As String, arg_kp As String, arg_vot As String) As Decimal
        Dim dbconn As New DBKewConn

        Dim bakiSemasa As Decimal = 0.00
        Try
            Dim param1 As SqlParameter = New SqlParameter("@arg_tahun", SqlDbType.Int)
            param1.Value = arg_tahun
            param1.Direction = ParameterDirection.Input
            param1.IsNullable = False

            Dim param2 As SqlParameter = New SqlParameter("@arg_tarikh", SqlDbType.VarChar)
            param2.Value = arg_tarikh
            param2.Direction = ParameterDirection.Input
            param2.IsNullable = False

            Dim param3 As SqlParameter = New SqlParameter("@arg_kw", SqlDbType.VarChar)
            param3.Value = arg_kw
            param3.Direction = ParameterDirection.Input
            param3.IsNullable = False

            Dim param4 As SqlParameter = New SqlParameter("@arg_KO", SqlDbType.VarChar)
            param4.Value = arg_ko
            param4.Direction = ParameterDirection.Input
            param4.IsNullable = False

            Dim param5 As SqlParameter = New SqlParameter("@arg_jbt", SqlDbType.VarChar)
            param5.Value = arg_ptj
            param5.Direction = ParameterDirection.Input
            param5.IsNullable = False

            Dim param6 As SqlParameter = New SqlParameter("@arg_KP", SqlDbType.VarChar)
            param6.Value = arg_kp
            param6.Direction = ParameterDirection.Input
            param6.IsNullable = True

            Dim param7 As SqlParameter = New SqlParameter("@arg_vot", SqlDbType.VarChar)
            param7.Value = arg_vot
            param7.Direction = ParameterDirection.Input
            param7.IsNullable = False

            Dim param8 As SqlParameter = New SqlParameter("@l_bakisbnr", SqlDbType.Decimal)
            param8.Value = bakiSemasa
            param8.Direction = ParameterDirection.Output
            param8.IsNullable = False

            Dim paramSql() As SqlParameter = {param1, param2, param3, param4, param5, param6, param7, param8}

            Dim count = dbconn.fExecuteSP("USP_BAKISMS", paramSql, param8, bakiSemasa)

        Catch ex As Exception

        End Try
        Return bakiSemasa

    End Function

    Public Function fBakiPerutukanMK01(kw As String, Ko As String, PTJ As String, Kp As String, Vot As String, tahun As Integer)
        Dim str = $"Select MK01_BakiPerutk from MK01_VotTahun where KodKw = '{kw}'
AND KodKo = '{Ko}'  AND KodPTJ = '{PTJ}' AND KodKP = '{Kp}'
AND LEFT(KodVot,2) = '{Left(Vot, 2)}'
AND MK01_Tahun = '{tahun}'"
        Dim baki As Decimal = 0.00
        Dim dbconn As New DBKewConn
        dbconn.sSelectCommand(str, baki)

        Return baki
    End Function

    Public Function fBakiMohon(jenis As String, kw As String, Ko As String, PTJ As String, Kp As String, HdrVot As String, NoDraf As String) As Decimal
        'DAPATKAN BAKI PERUNTUKAN SEMASA YANG TELAH DITOLAK JUMLAH PERMOHONAN

        Dim dbconn As New DBKewConn
        Dim dblBaki As Decimal = 0
        Dim dblJumMohon As Decimal = 0
        Dim dblJum As Decimal = 0
        Dim dblBakiSbnr As Decimal = 0

        Dim intTahun As Integer = Now.Year
        Dim strTarikh As String = Now.ToString("yyyy-MM-dd")


        If kw = "08" Then
            dblBaki = fgetBakiTabung(intTahun, strTarikh, kw, PTJ)
        Else
            If jenis = "AP-PT" Then    'PP JENIS PT & INDEN
                dblBaki = fgetBakiSMS(intTahun, strTarikh, kw, Ko, PTJ, Kp, HdrVot & "000")
            Else
                '            31/01/2011 - Dapatkan drp function Chenol
                dblBaki = fGetBakiSebenar(intTahun, strTarikh, kw, Ko, PTJ, Kp, HdrVot & "000")
            End If
        End If


        If jenis = "AP" Then    'PEMBAYARAN (JUM BELANJA)

            'PERMOHONAN PERINGKAT AP
            Dim str = $"SELECT ISNULL(SUM(B.AP02_JumBayarDt),0) AS JUM 
        FROM AP02_PP A INNER JOIN AP02_PPDt B 
        ON A.AP02_NoPP = B.AP02_NoPP 
        WHERE A.AP02_StatusDok IN ('03','04','05','06','07','17') 
        AND B.KodKw = '{kw}' And B.KodKp = '{Kp}' 
        And B.KodPtj = '{PTJ}' And B.KodKo = '{Ko}' 
        AND LEFT(B.KodVot,2) = '{ Left(HdrVot, 2) }'  
        AND A.AP02_JENIS <> 'J01'"

            dbconn.sSelectCommand(str, dblJum)
            dblJumMohon = dblJum

            'PERMOHONAN PERINGKAT BD : SEBELUM LULUS1
            str = $"SELECT ISNULL(SUM(AP04_DEBIT)-SUM(AP04_KREDIT),0) AS JUM 
        FROM AP04_BAUCAR A INNER JOIN AP04_BAUCARDT B On A.AP04_NOBAUCAR = B.AP04_NOBAUCAR 
        WHERE A.AP04_StatusDok In ('08','09','10','16','18','19') 
        And B.KodKw = '{kw }' And B.KodKp = '{Kp}' 
        AND B.KodPtj = '{ PTJ }' And B.KodKo = '{Ko}' 
        And LEFT(B.KodVot,2) = '{ Left(HdrVot, 2) }' 
        And A.AP04_JUMLAH < 10000 
        AND a.AP04_NoDraf NOT IN ('{ NoDraf }')
        And A.AP02_JENIS <> 'J01'"
            dbconn.sSelectCommand(str, dblJum)
            dblJumMohon = dblJumMohon + dblJum

            'PERMOHONAN PERINGKAT BD : SEBELUM LULUS2
            str = $"SELECT ISNULL(SUM(AP04_DEBIT)-SUM(AP04_KREDIT),0) AS JUM 
        FROM AP04_BAUCAR A INNER JOIN AP04_BAUCARDT B On A.AP04_NOBAUCAR = B.AP04_NOBAUCAR 
        WHERE A.AP04_StatusDok IN ('08','09','10','11','16','18','19') 
         And B.KodKw = '{kw }' And B.KodKp = '{Kp}' 
        AND B.KodPtj = '{ PTJ }' And B.KodKo = '{Ko}'
        And LEFT(B.KodVot,2) = '{ Left(HdrVot, 2) }' 
        And A.AP04_JUMLAH >= 10000 
        And a.AP04_NoDraf NOT IN ('{ NoDraf }')
        AND A.AP02_JENIS <> 'J01'"
            dbconn.sSelectCommand(str, dblJum)
            dblJumMohon = dblJumMohon + dblJum

        ElseIf jenis = "PO" Then    'PEMBELIAN

            Dim str = $"SELECT ISNULL(SUM(B.PO01_JumKadar),0) AS JUM 
        FROM PO01_PPembelian A INNER JOIN PO01_PPembelianDt B 
        On A.PO01_NoMohon = B.PO01_NoMohon 
        WHERE A.PO01_StatusDok In ('01','02','03','04','05','06','07','14','15','23','27') 
         And B.KodKw = '{kw }' And B.KodKp = '{Kp}' 
        AND B.KodPtj = '{ PTJ }' And B.KodKo = '{Ko}'
        AND LEFT(B.KodVot,2) = '{ Left(HdrVot, 2)}'"
            dbconn.sSelectCommand(str, dblJum)
            dblJumMohon = dblJum


        ElseIf jenis = "BG" Then    'BAJET

            'VIREMEN
            Dim str = $"SELECT ISNULL(SUM(BG07_AMAUN),0) AS JUM FROM BG07_VIREMEN A 
        INNER JOIN BG07_ViremenDT B 
        ON A.BG07_NOVIREMEN = B.BG07_NOVIREMEN 
        WHERE A.KODSTATUSDOK IN ('01','02','03','04','09') 
        AND B.KODVIR = 'K' 
        And B.KodKw = '{kw }' And B.KodKp = '{Kp}' 
        AND B.KodPtj = '{ PTJ }' And B.KodKo = '{Ko}' 
        And LEFT(B.KodVot,2) = '{ Left(HdrVot, 2) }'"
            dbconn.sSelectCommand(str, dblJum)
            dblJumMohon = dblJum

            'PENGURANGAN BAJET
            str = $"SELECT ISNULL(SUM(BG06_AMAUN),0) AS JUM FROM BG06_AGIHOBJSBG 
        WHERE KODAGIH = 'KG' AND BG06_STATLULUS = 0 
        And B.KodKw = '{kw }' And B.KodKp = '{Kp}' 
        AND B.KodPtj = '{ PTJ }' And B.KodKo = '{Ko}' 
        AND LEFT(KodVot,2) = '{ Left(HdrVot, 2) }'"
            dbconn.sSelectCommand(str, dblJum)
            dblJumMohon = dblJumMohon + dblJum

        End If

        dblBakiSbnr = dblBaki - dblJumMohon

        Return dblBakiSbnr

    End Function

    Public Function VotSMP(butir As String)
        Dim kodVot As String = ""
        Dim str = $"Select KodVotSMP from RC_VotSMP where Butiran = '{ Trim(butir)}'"
        Dim dbconn As New DBKewConn
        dbconn.sSelectCommand(str, kodVot)
        Return kodVot
    End Function

    ''' <summary>
    ''' Read file content sebagai byte
    ''' Hanafi 30/01/2019
    ''' </summary>
    ''' <param name="FilePath"></param>
    ''' <returns></returns>
    Public Function fReadFile(ByVal FilePath As String) As Byte()
        Dim fs As FileStream
        Try
            ' Read file and return contents           
            fs = File.Open(FilePath, FileMode.Open, FileAccess.Read)
            Dim lngLen As Long = fs.Length
            Dim abytBuffer(CInt(lngLen - 1)) As Byte
            fs.Read(abytBuffer, 0, CInt(lngLen))
            Return abytBuffer
        Catch exp As Exception
            fErrorLog("fReadFile-" & exp.Message)
            Return Nothing
        Finally
            If Not fs Is Nothing Then
                fs.Close()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Decrypt dan Create File
    ''' Hanafi 30/01/2019
    ''' </summary>
    ''' <param name="FileNamePath"></param>
    ''' <param name="bytContent"></param>
    ''' <returns></returns>
    Public Function fDecFile(ByVal FileNamePath As String, ByVal bytContent As Byte()) As Boolean
        Dim objFstream As FileStream
        Dim clsCrypto As New clsCrypto
        Dim decContent As Byte()
        Try
            objFstream = File.Open(FileNamePath, FileMode.Create, FileAccess.Write)
            decContent = clsCrypto.fDecryptFile(bytContent)    '// Encrypt file 31/03/2008
            Dim lngLen As Long = decContent.Length
            objFstream.Write(decContent, 0, CInt(lngLen))
            objFstream.Flush()
            Return True
        Catch exp As Exception
            Console.WriteLine("Exception: " & exp.ToString())
            Return False
        Finally
            objFstream.Close()
        End Try

    End Function
    ''' <summary>
    ''' Encrypt dan Create File
    ''' Hanafi 30/01/2019
    ''' </summary>
    ''' <param name="SrcPath"></param>
    ''' <param name="bytContent"></param>
    ''' <returns></returns>
    Public Function fEncFile(ByVal SrcPath As String, ByVal bytContent As Byte()) As Boolean
        Dim objFstream As FileStream
        Dim encContent As Byte()
        Dim clsCrypto As New clsCrypto
        Try
            objFstream = File.Open(SrcPath, FileMode.Create, FileAccess.Write)
            encContent = clsCrypto.fEncryptFile(bytContent)
            Dim lngLen As Long = encContent.Length
            objFstream.Write(encContent, 0, CInt(lngLen))
            objFstream.Flush()
            objFstream.Close()
            Return True
        Catch ex As Exception
            Return False
        Finally
            objFstream.Close()
        End Try
    End Function

    ''' <summary>
    ''' return senarai ptj
    ''' </summary>
    ''' <returns></returns>
    Public Function fLoadPTj() As DataSet
        Try
            Dim strSql As String = "SELECT DISTINCT MK_PTJ.KodPTJ, (MK_PTJ.KodPTJ + ' - ' + MK_PTJ.Butiran) as Butiran  
From MK_PTJ INNER Join MK01_VotTahun ON MK_PTJ.KodPTJ   = MK01_VotTahun.KodPTJ   
Where MK_PTJ.KodPTJ <> '-' and MK_PTJ.KodKategoriPTJ = 'P' and MK_PTJ.status = 1 
ORDER BY MK_PTJ.KodPTJ "

            Dim ds As New DataSet
            Dim dbconn As New DBKewConn
            Dim dt As New DataTable
            ds = dbconn.fSelectCommand(strSql)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    Return ds
                Else
                    Return Nothing
                End If
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function fGetVotBelanja() As List(Of String)

        Try
            Dim lstVotBelanja = New List(Of String) From {"10000", "20000", "30000", "40000", "50000", "60000"}

            Return lstVotBelanja
        Catch ex As Exception

        End Try

    End Function

    'ARIP START
    Function NoJurnalSem(ByVal KodModul As String, ByVal Prefix As String, ByVal thnProses As Integer) As Integer
        Try
            'RefDateTime = ServerDate()

            Dim dbconn As New DBKewConn
            'Dim rs As String
            'Dim rs2 As String
            Dim blnFound As Boolean = False
            'Dim NoJurnalF As String
            'Dim thnProses As Integer
            Dim intNoAkhir As Integer
            Dim strSql As String
            'If Me.Thn1.Checked = True Then
            '    thnProses = Year(DateTime.Today)
            'End If

            'If Me.Thn.Checked = True Then
            '    thnProses = Year(DateTime.Today) - 1
            'End If

            Dim rs As String = "SELECT NoAkhir From MK_NoAkhir WHERE KodModul = '" & KodModul & "' AND " _
        & " Prefix = '" & Prefix & "' AND Tahun = '" & thnProses & "'"

            Dim ds2 As New DataSet
            ds2 = dbconn.fSelectCommand(rs)

            If Not ds2 Is Nothing Then
                If ds2.Tables(0).Rows.Count > 0 Then
                    intNoAkhir = CInt(ds2.Tables(0).Rows(0)("NoAKhir").ToString)
                    blnFound = True
                Else
                    intNoAkhir = 0
                End If
            Else
                intNoAkhir = 0
            End If
            intNoAkhir = intNoAkhir + 1


            If blnFound = True Then
                strSql = "update MK_NoAkhir set NoAkhir =@noakhir  WHERE KodModul = @kodmodul AND  Prefix = @prefix AND Tahun = @tahun "

                Dim paramSql2() As SqlParameter = {
                            New SqlParameter("@noakhir", intNoAkhir),
                            New SqlParameter("@tahun", thnProses),
                            New SqlParameter("@kodmodul", KodModul),
                            New SqlParameter("@prefix", Prefix)
                            }

                dbconn.sConnBeginTrans()
                If dbconn.fUpdateCommand(strSql, paramSql2) > 0 Then
                    dbconn.sConnCommitTrans()
                Else
                    dbconn.sConnRollbackTrans()
                    Return String.Empty
                End If
            Else
                strSql = "insert into MK_NoAkhir (KodModul , Prefix , NoAkhir , Tahun , Butiran , KodPTJ ) " &
                "values (@KodModul, @Prefix, @NoAkhir, @Tahun, @Butiran, @KodPTJ)"
                Dim paramSql() As SqlParameter = {
                    New SqlParameter("@KodModul", KodModul),
                    New SqlParameter("@Prefix", Prefix),
                    New SqlParameter("@NoAkhir", 1),
                    New SqlParameter("@Tahun", thnProses),
                    New SqlParameter("@Butiran", "-"),
                    New SqlParameter("@KodPTJ", "-")
                }

                dbconn = New DBKewConn
                dbconn.sConnBeginTrans()
                If dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                    dbconn.sConnCommitTrans()
                Else
                    dbconn.sConnRollbackTrans()
                    Return String.Empty
                End If
            End If

            If Len(intNoAkhir) = 1 Then
                NoJurnalSem = Format(intNoAkhir, "00000#")
            ElseIf Len(intNoAkhir) = 2 Then
                NoJurnalSem = Format(intNoAkhir, "0000##")
            ElseIf Len(intNoAkhir) = 3 Then
                NoJurnalSem = Format(intNoAkhir, "000###")
            ElseIf Len(intNoAkhir) = 4 Then
                NoJurnalSem = Format(intNoAkhir, "00####")
            ElseIf Len(intNoAkhir) = 5 Then
                NoJurnalSem = Format(intNoAkhir, "0#####")
            ElseIf Len(intNoAkhir) = 6 Then
                NoJurnalSem = Format(intNoAkhir, "######")
            End If


        Catch ex As Exception
            Return String.Empty
        End Try

        Return NoJurnalSem

    End Function


    'DAIM
    'Hantar tarikh dalam bentuk DD/MM/YYYY
    'Die akan return string tarikh dalam bentuk MM/DD/YYYY
    Function date_conversion(ByVal tarikh As String, Optional type As String = "VIEW") As String
        If tarikh <> String.Empty Then
            Dim newdate = Split(tarikh, "/")
            If type = "VIEW" Then
                tarikh = newdate(1) & "/" & newdate(0) & "/" & newdate(2)
            End If
        End If

        Return tarikh


    End Function

    Sub Audit(AuditType As String, TransID As String, table As String, sLogMedan As String, sLogLama As String, sLogBaru As String, sLogUserID As String, sLogLevel As String, lsLogUsrIP As String, lsLogUsrPC As String, sLogSubModul As String)
        Dim sLogField As String
        Dim sLogDesc As String
        Dim dbconn As New DBKewConn


        'sLogUserID = Session("ssusrID")
        'sLogLevel = Session("ssUsrTahap")
        'sLogNostaf = Session("RefID")
        'sLogSubModul = Request.QueryString("KodSubMenu")

        sLogField = table

        If sLogBaru = "" Then sLogBaru = "-"

        If sLogLama = "" Then sLogLama = "-"

        ' If sLogNostaf = "" Then  sLogNostaf = "-"

        If sLogSubModul = "" Then sLogSubModul = "-"

        If sLogField = "" Then sLogField = "-"

        ' If sLogTable = "" Then  sLogTable = "-"

        ' If sLogRef = "" Then sLogRef = "-"

        ' If sLogRefNo = "" Then sLogRefNo = "-"

        If sLogDesc = "" Then sLogDesc = "-"

        ' If sLogStatus = "" Then  sLogStatus = "-"


        Select Case AuditType
            '---SIMPAN---
            Case "SIMPAN"
                sLogDesc = "INSERT"

                sAuditLog(sLogUserID, sLogLevel, "-", sLogSubModul, sLogDesc, sLogField, "-", "-", sLogMedan, sLogBaru, sLogLama, lsLogUsrIP, lsLogUsrPC)

            Case "UPDATE"
                sLogDesc = "UPDATE"

                sAuditLog(sLogUserID, sLogLevel, "-", sLogSubModul, sLogDesc, sLogField, "-", "-", sLogMedan, sLogBaru, sLogLama, lsLogUsrIP, lsLogUsrPC)

            Case "DELETE"
                sLogDesc = "DELETE"

                sAuditLog(sLogUserID, sLogLevel, "-", sLogSubModul, sLogDesc, sLogField, "-", "-", sLogMedan, sLogBaru, sLogLama, lsLogUsrIP, lsLogUsrPC)
        End Select

    End Sub

End Module


