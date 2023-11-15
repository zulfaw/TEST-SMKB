Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports Newtonsoft.Json
Imports System.Web.Script.Services
Imports System.Web.Script.Serialization
'Imports System.Web.Http
Imports System.Data.SqlClient
Imports System

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()>
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class AkaunPenghutangWS
    Inherits System.Web.Services.WebService

    'SQL to fetch top 10 result of staf list for dropdown
    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetNoAkaunPenghutangList(ByVal q As String) As String
        Dim tmpDT As DataTable = GetNoAkaunPenghutang(q)
        Return JsonConvert.SerializeObject(tmpDT)
    End Function
    Private Function GetNoAkaunPenghutang(kod As String) As DataTable
        Dim db = New DBKewConn

        Dim query As String = "SELECT TOP(10) Kod_Penghutang as kod, Nama_Penghutang as nama, No_Rujukan as id FROM SMKB_Penghutang_Master"
        Dim param As New List(Of SqlParameter)
        If (kod <> "") Then
            query = query & " WHERE LOWER(Nama_Penghutang) LIKE '%' + @kod + '%' OR Kod_Penghutang LIKE '%' + @kod + '%' OR No_Rujukan LIKE '%' + @kod + '%'"
            param.Add(New SqlParameter("@kod", kod))
        End If
        query &= " ORDER BY Kod_Penghutang"
        Return db.Read(query, param)
    End Function
End Class