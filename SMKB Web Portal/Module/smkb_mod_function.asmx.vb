Imports System.ComponentModel
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports Newtonsoft.Json
Imports System.Web.Script.Services
Imports System.Web.Script.Serialization
Imports System.Web.Http
Imports System.Data.SqlClient

<System.Web.Script.Services.ScriptService()>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class smkb_mod_function
    Inherits System.Web.Services.WebService
    Dim dbconn As New DBKewConn
    Dim ds As New DataSet
    Dim param As New List(Of SqlParameter)
    Dim whereSql As String = ""

    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function getListMasterLookup(q As String) As String
        Dim paramSql() As SqlParameter

        If q <> "" Then
            whereSql = " and Description LIKE '%' + @desc + '%'"
            paramSql = {
                    New SqlParameter("@desc", q)
            }
        End If

        dbconnNew = New SqlConnection(strCon)
        dbconnNew.Open()

        Dim strSql As String = $"select Master_Reference_code as value, Description as text from smkb_lookup_master WHERE 1 =1 " & whereSql & " order by Master_Reference_code"

        ds = NewGetDataPB(strSql, paramSql, dbconnNew)

        If (ds.Tables.Count = 0) Then
            Exit Function
        End If

        Return JsonConvert.SerializeObject(ds.Tables(0))
    End Function
    <System.Web.Services.WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function getListNamaUser(q As String) As String
        Dim paramSql() As SqlParameter

        If q <> "" Then
            whereSql = " and MS01_Nama LIKE '%' + @parNama + '%'"
            paramSql = {
                    New SqlParameter("@parNama", q)
            }
        End If

        dbconnNew = New SqlConnection(strCon)
        dbconnNew.Open()



        Dim strSql As String = $"Select MS01_NoStaf, MS01_Nama FROM [DEVMIS\SQL_INS01].dbStaf.dbo.MS01_Peribadi
                    WHERE MS01_Status = 1
                    AND MS01_NoStaf IN (select CLM_loginID FROM [DEVMIS\SQL_INS04].dbCLM.dbo.CLM_PenggunaSis
                    WHERE 1 = 1 " & whereSql & "
                    AND CLM_SisKod = 'SMKB'
                    AND CLM_SisStatus = 'AKTIF')
                    ORDER BY MS01_Nama"


        ds = NewGetDataPB(strSql, paramSql, dbconnNew)

        If (ds.Tables.Count = 0) Then
            Exit Function
        End If

        Return JsonConvert.SerializeObject(ds.Tables(0))
    End Function


End Class

