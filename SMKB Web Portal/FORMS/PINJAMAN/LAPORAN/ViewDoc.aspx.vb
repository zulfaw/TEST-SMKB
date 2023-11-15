Imports Microsoft.AspNet.Identity
Imports System.Globalization
Imports System.Threading
Imports System.Configuration
Imports System.Web.Configuration
Imports System.Collections.Specialized
Imports System.Reflection
Imports System
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports System.Net.NetworkInformation
Imports System.Diagnostics.Eventing
Imports System.Runtime.InteropServices.ComTypes
Imports System.Drawing.Printing
Imports System.IO

Public Class ViewDoc
    Inherits System.Web.UI.Page
    Dim Sql As String
    '    Dim dbconn As New DBKewConn

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim File As String

        Me.txtNoPinj.Text = Session("noPinj")

        Using dbconnNew As New SqlConnection(strCon)
            dbconnNew.Open()


            Sql = "select a.No_Pinj, a.No_Staf, c.MS01_Nama,  e.pejabat
                from SMKB_Pinjaman_Master AS a, [devmis\sql_ins01].dbstaf.dbo.MS01_Peribadi AS c,
                [devmis\sql_ins01].dbstaf.dbo.MS08_Penempatan AS d, [devmis\sql_ins01].dbstaf.dbo.MS_pejabat AS e
                where 1 = 1
                and a.No_Pinj = '" & Session("noPinj") & "'
                and a.No_Staf = c.MS01_NoStaf
                and a.No_Staf = d.MS01_NoStaf
                and d.MS08_StaTerkini = 1
                and SUBSTRING(d.MS08_Unit,1,2) = e.kodpejabat"

            'SMP07_NamaMP LIKE '%PROJEK SARJANA MUDA II%'"
            dbcomm = New SqlCommand(Sql, dbconnNew)
            dbread = dbcomm.ExecuteReader()
            dbread.Read()

            If dbread.HasRows Then
                Me.txtNama.Text = dbread("MS01_Nama")
                Me.txtNamaPtj.Text = dbread("pejabat")
            End If
            dbread.Close()
            dbcomm = Nothing
        End Using


        File = Session("FileDoc")
        'MsgBox(File)
        File = File
        ShowF.Attributes.Add("src", File)
    End Sub

End Class