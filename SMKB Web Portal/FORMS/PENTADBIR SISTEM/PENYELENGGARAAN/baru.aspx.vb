Imports Microsoft.AspNet.Identity
Imports System.Globalization
Imports System.Threading
Imports System.Configuration
Imports System.Web.Configuration
Imports System.Collections.Specialized
Imports System.Reflection
Imports System
Imports System.Data.SqlClient
Public Class baru
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                'fBindDdlJenis()
                'fBindDdlTahun()

                fLoadSenarai()
                'divList.Visible = True
                'divDt.Visible = False

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub fLoadSenarai()
        Try
            dbconnNew = New SqlConnection(strCon)
            dbconnNew.Open()

            Dim Sql = "SELECT [Kod_Sub], [Nama_Sub], [Dis_Sub], [Nama_Icon], [Icon_Location], [Urutan], [Status]
                    FROM SMKB_Sub_Modul
                    WHERE Status= 1
                    AND Kod_Modul = @KodModul
                    order by Urutan"

            Dim paramSql() As SqlParameter = {New SqlParameter("@KodModul", "97")}
            Dim ds As DataSet = NewGetDataPB(Sql, paramSql, dbconnNew)


            Dim rowIndex As Integer = 0

            If (ds.Tables.Count > 0) Then

                Dim dt As New DataTable()
                dt.Columns.Add("rowid", GetType(Integer))
                dt.Columns.Add("Kod_Sub", GetType(String))
                dt.Columns.Add("Nama_Sub", GetType(String))
                dt.Columns.Add("Dis_Sub", GetType(String))
                dt.Columns.Add("Nama_Icon", GetType(String))
                dt.Columns.Add("Icon_Location", GetType(String))
                dt.Columns.Add("Urutan", GetType(String))
                For i = 0 To ds.Tables(0).Rows.Count - 1

                    Dim dr As DataRow = dt.NewRow()
                    rowIndex += 1
                    dr("rowid") = rowIndex
                    dr("Kod_Sub") = ds.Tables(0).Rows(i).Item(0)
                    dr("Nama_Sub") = ds.Tables(0).Rows(i).Item(1)
                    'dr("Dis_Sub") = ds.Tables(0).Rows(i).Item(2)
                    dt.Rows.Add(dr)

                Next
                gvSenarai.DataSource = dt
                gvSenarai.DataBind()
            End If
            dbread.Close()
            dbcomm = Nothing
        Catch ex As Exception
        End Try
    End Sub

End Class