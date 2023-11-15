Imports System.Data.Entity.Migrations
Imports System.Data.SqlClient

Public Class _9704
    Inherits System.Web.UI.Page
    Dim SQL As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim kodsub As String = Request.QueryString("kodsub")
            Session("Kod_Sub") = kodsub
            Dim strUsrId As String = Session("ssusrID")

            Session("ssusrID") = Trim(Request.QueryString("usrLogin"))
            Session("sessionID") = Trim(Request.QueryString("usession"))

            dbconnNew = New SqlConnection(strCon)
            dbconnNew.Open()

            SQL = "select Nama_Sub from SMKB_Sub_Modul where Kod_Sub ='" & Session("Kod_Sub") & "'"
            dbcomm = New SqlCommand(SQL, dbconnNew)
            dbread = dbcomm.ExecuteReader()
            dbread.Read()
            If dbread.HasRows Then
                NamaSubMenu.Text = dbread("Nama_Sub")
            End If
            dbread.Close()


            If CStr(Session("Kod_Sub")) <> "" Then
                SQL = "select Kod_Sub_Menu, Nama_Sub_Menu, isnull(Nama_Fail,'-')  Nama_Fail from SMKB_Sub_Menu where Kod_Sub = '" & Session("Kod_Sub") & "' and status = 1
                order by Urutan"
                dbcomm = New SqlCommand(SQL, dbconnNew)

                Dim paramSql() As SqlParameter = {
                            }
                Dim ds As DataSet = NewGetDataPB(SQL, paramSql, dbconnNew)

                If (ds.Tables.Count > 0) Then
                    rptSubMenu.DataSource = ds
                    rptSubMenu.DataBind()
                End If

                dbconnNew.Close()
                dbconnNew.Dispose()


            End If
        End If
    End Sub




End Class