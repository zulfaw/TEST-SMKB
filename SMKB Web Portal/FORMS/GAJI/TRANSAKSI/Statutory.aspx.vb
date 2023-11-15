Imports System
Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.Net.NetworkInformation

Public Class Statutory
    Inherits System.Web.UI.Page
    Dim dbconn As New DBKewConn
    Dim dbconnSM As New DBSMConn
    Public dsKod As New DataSet
    Public dvKodKW As New DataView

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            'fBindGvJenis()

        End If
    End Sub
    Private Sub fBindGvJenis()


        Try
            Dim dt As New DataTable
            dt = fCreateDtJenis()

            If dt.Rows.Count = 0 Then
                gvJenis.DataSource = New List(Of String)
            Else
                gvJenis.DataSource = dt
            End If
            gvJenis.DataBind()
            'lblJumRekod.InnerText = dt.Rows.Count


            'Required for jQuery DataTables to work.
            gvJenis.UseAccessibleHeader = True
            gvJenis.HeaderRow.TableSection = TableRowSection.TableHeader

        Catch ex As Exception

        End Try
    End Sub
    Private Function fCreateDtJenis() As DataTable

        Try

            Dim dt As New DataTable
            dt.Columns.Add("no_staf", GetType(String))
            dt.Columns.Add("ms01_nama", GetType(String))
            dt.Columns.Add("gaji", GetType(String))
            dt.Columns.Add("elaun", GetType(String))
            dt.Columns.Add("potongan", GetType(String))
            dt.Columns.Add("kwsp", GetType(String))
            dt.Columns.Add("perkeso", GetType(String))
            dt.Columns.Add("cukai", GetType(String))


            Dim nostaf As String
            Dim nama As String
            Dim gaji As String
            Dim elaun As String
            Dim potongan As String
            Dim kwsp As String
            Dim perkeso As String
            Dim cukai As String

            Dim strSql As String = "select z.No_Staf,tblStaf.ms01_nama , sum(z.gaji) as gaji, sum(z.elaun) as elaun,sum(z.potongan) as potongan,
            0 as kwsp,0 as perkeso,0 as cukai
            from (
            select no_staf, isnull(sum(amaun),0) as gaji,0 as elaun,0 as potongan
            from SMKB_Gaji_Master 
            where  Jenis_Trans in ('G') 
            and status='A' and  ('2023-05-31' between Tkh_Mula and  Tkh_Tamat)
            group by no_staf
            union all
            select no_staf, 0 as gaji, isnull(sum(amaun),0) as elaun,0 as potongan
            from SMKB_Gaji_Master 
            where  Jenis_Trans in ('E')
            and status='A' and  ('2023-05-31' between Tkh_Mula and  Tkh_Tamat)
            group by no_staf
            union all
            select no_staf, 0 as gaji,0 as elaun,isnull(sum(amaun),0) as potongan
            from SMKB_Gaji_Master 
            where  Jenis_Trans in ('P') 
            and status='A' and  ('2023-05-31' between Tkh_Mula and  Tkh_Tamat) 
            group by no_staf
            ) z, [qa11].dbstaf.dbo.ms01_peribadi as tblStaf where z.no_staf=tblStaf.ms01_nostaf
            group by z.no_staf,tblStaf.ms01_nama"
            Dim dbconn As New DBKewConn
            dsKod = dbconn.fSelectCommand(strSql)
            dvKodKW = New DataView(dsKod.Tables(0))

            For i As Integer = 0 To dsKod.Tables(0).Rows.Count - 1

                nostaf = dsKod.Tables(0).Rows(i)(0).ToString
                nama = dsKod.Tables(0).Rows(i)(1).ToString
                gaji = dsKod.Tables(0).Rows(i)(2).ToString
                elaun = dsKod.Tables(0).Rows(i)(3).ToString
                potongan = dsKod.Tables(0).Rows(i)(4).ToString
                kwsp = dsKod.Tables(0).Rows(i)(5).ToString
                perkeso = dsKod.Tables(0).Rows(i)(6).ToString
                cukai = dsKod.Tables(0).Rows(i)(7).ToString

                dt.Rows.Add(nostaf, nama, gaji, elaun, potongan, kwsp, perkeso, cukai)
            Next

            Return dt

        Catch ex As Exception

        End Try

    End Function
End Class