Imports System.IO
Imports System.Web.Configuration
Imports System.Data.Entity.Core.EntityClient
Module ModMain

#Region "Declaration"
    Public strErrorLogPath As String = HttpContext.Current.Server.MapPath("~/App_Data/Log/ErrorLog")
    Public strDebugLogPath As String = HttpContext.Current.Server.MapPath("~/App_Data/Log/DebugLog")

    Public strConn As String

    'DB Kewangan
    Public strDbServer As String
    Public strDbName As String
    Public strDBPassword As String
    Public strDBUser As String

    'DB Sumber Manusia
    Public strDbSMServer As String
    Public strDbSMName As String
    Public strDBSMPassword As String
    Public strDBSMUser As String

    'DB CLM
    Public strDbCLMServer As String
    Public strDbCLMName As String
    Public strDBCLMPassword As String
    Public strDBCLMUser As String

    'DB Student
    Public strDbSTServer As String
    Public strDbSTName As String
    Public strDBSTPassword As String
    Public strDBSTUser As String

    Public strSesUserName As String
    Public strSesUserPost As String
    Public struserPtj As String
    Public strSesUserKodPtj As String
    Public struserKodBahagian As String
    Public struserKodUnit As String
    Public strDateLastLogin As String

    'general
    'Public strSesStaffID As String
    'Public strSessId As String
    'Public blnLogin As Boolean
    'Public strUsrTahap As String
    'Public strUsrPcIP As String
    'Public strUsrPCName As String

    'laporan
    Public strRptServerUrl As String = WebConfigurationManager.AppSettings("RptServerUrl")
    Public strRptServerPath As String = WebConfigurationManager.AppSettings("RptServerPath")

    'Need to change when moving to production.
    Public Const DBStaf As String = "QA11.DBStaf.dbo."

#End Region
    Sub New()
        sReadConfig()
    End Sub

    Public Class UserInfo
        Public Shared strSesStaffID As String
        Public Shared strSessId As String

        Public Shared strUsrTahap As String
        Public Shared strUsrPcIP As String
        Public Shared strUsrPCName As String
    End Class

    Public Class UserLogin
        Public Shared status As Boolean
    End Class

    'Public Function ShowMessage(ByVal Message As String, ByVal type As WarningType)
    '    Try
    '        Dim master As SiteMaster
    '        Dim PanelMessage As Panel = TryCast(master.FindControl("Message"), Panel)
    '        Dim labelMessage As Label = TryCast(PanelMessage.FindControl("labelMessage"), Label)
    '        labelMessage.Text = Message
    '        PanelMessage.CssClass = String.Format("alert alert-{0} alert-dismissable", type.ToString().ToLower())
    '        PanelMessage.Attributes.Add("role", "alert")
    '        PanelMessage.Visible = True
    '    Catch ex As Exception

    '    End Try

    'End Function
    Public Sub sReadConfig()
        'strConn = "metadata=res://*/TrainingModel.csdl|res://*/TrainingModel.ssdl|res://*/TrainingModel.msl;provider=System.Data.SqlClient;provider connection string='data source=(local);initial catalog=Training;integrated security=True;multipleactiveresultsets=True;application name=EntityFramework';"
        Try

            strDbServer = WebConfigurationManager.AppSettings("DBKewServerName")
            strDbName = WebConfigurationManager.AppSettings("DBKewName")
            strDBUser = WebConfigurationManager.AppSettings("DBKewUserID")
            strDBPassword = WebConfigurationManager.AppSettings("DBKewPassword")

            Dim strDeDBPassword
            Dim clsCrypto As New clsCrypto
            strDeDBPassword = clsCrypto.fDecrypt(strDBPassword)

            Dim ConnectionString = "data source=" & strDbServer & ";initial catalog=" & strDbName & ";Uid=" & strDBUser & ";Pwd=" & strDBPassword & ";integrated security=True;"

            Dim csb As EntityConnectionStringBuilder = New EntityConnectionStringBuilder()
            csb.Metadata = "res://*/TrainingModel.csdl|res://*/TrainingModel.ssdl|res://*/TrainingModel.msl"
            csb.Provider = "System.Data.SqlClient"
            csb.ProviderConnectionString = ConnectionString '"data source=(local);initial catalog=Training;integrated security=True"
            strConn = csb.ToString()
        Catch ex As Exception

        End Try

    End Sub
    Public Sub fErrorLog(ByVal strErrMsg As String)

        Try
            Dim logWriter As StreamWriter

            Dim strDateToday As String = Now.ToString("yyyyMMdd")

            If Directory.Exists(strErrorLogPath) = False Then
                Directory.CreateDirectory(strErrorLogPath)
            End If

            Dim strErrLogPath As String = strErrorLogPath & "\Error_" & strDateToday & ".log"
            If File.Exists(strErrLogPath) Then
                logWriter = File.AppendText(strErrLogPath)
                logWriter.WriteLine(Now & " " & strErrMsg)
            Else
                logWriter = File.CreateText(strErrLogPath)
                logWriter.WriteLine(Now & " " & strErrMsg)
            End If
            If Not logWriter Is Nothing Then
                logWriter.Close()
            End If
        Catch ex As Exception

        End Try

    End Sub

    Public Sub fDebugLog(ByVal strErrMsg As String)

        Try
            Dim logWriter As StreamWriter

            Dim strDateToday As String = Now.ToString("yyyyMMdd")

            If Directory.Exists(strDebugLogPath) = False Then
                Directory.CreateDirectory(strDebugLogPath)
            End If

            Dim strDebLogPath As String = strDebugLogPath & "\Debug_" & strDateToday & ".log"
            If File.Exists(strDebLogPath) Then
                logWriter = File.AppendText(strDebLogPath)
                logWriter.WriteLine(Now & " " & strErrMsg)
            Else
                logWriter = File.CreateText(strDebLogPath)
                logWriter.WriteLine(Now & " " & strErrMsg)
            End If
            If Not logWriter Is Nothing Then
                logWriter.Close()
            End If
        Catch ex As Exception

        End Try

    End Sub
    Public Function ConvertString(strString) As String
        Dim input As String = strString
        Dim pattern As String = "\b(\w|['-])+\b"
        ' With lambda support:
        input = input.ToLower()
        Dim result As String = Regex.Replace(input, pattern,
            Function(m) m.Value(0).ToString().ToUpper() & m.Value.Substring(1))

        Return result
    End Function
    'Initialize the Connection String based on the DB Type
    Public Function dbKewConnStr() As String

        Dim strDeDBPassword
        Dim clsCrypto As New clsCrypto
        strDeDBPassword = clsCrypto.fDecrypt(strDBPassword)

        dbKewConnStr = "Server=" & strDbServer & ";Database=" & strDbName & ";Uid=" & strDBUser & ";Pwd=" & strDeDBPassword & ";Pooling=False;"
        Return dbKewConnStr
    End Function

    'DB Student EQ
    Public strDbEQServer As String
    Public strDbEQName As String
    Public strDBEQUser As String
    Public strDBEQPassword As String



End Module
