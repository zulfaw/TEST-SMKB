Imports System.Data.SqlClient

Public Class NestedMasterPage2
    Inherits System.Web.UI.MasterPage
    Dim SQL As String
    Public isNewPage As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As eventargs) Handles Me.load
        If Not ispostback Then
            Dim kodsub As String = Request.QueryString("kodsub")
            If kodsub <> "" Then
                Session("Kod_Sub") = kodsub
            End If

            If Request.QueryString("kodsubmenu") <> "" Then
                hidSubMenu2.Value = Request.QueryString("kodsubmenu")
            End If

            Dim strUsrId As String = Session("ssusrID")

            'Session("ssusrID") = Trim(Request.QueryString("usrLogin"))
            'Session("sessionID") = Trim(Request.QueryString("usession"))

            If Request.QueryString("rpt") <> "true" Then
                dbconnNew = New SqlConnection(strCon)
                dbconnNew.Open()

                SQL = "select Nama_Sub from SMKB_Sub_Modul where Kod_Sub ='" & Session("Kod_Sub") & "'"
                dbcomm = New SqlCommand(SQL, dbconnNew)
                dbread = dbcomm.ExecuteReader()
                dbread.Read()
                If dbread.HasRows Then
                    namaSubMenu.Text = dbread("Nama_Sub")
                End If
                dbread.Close()


                If CStr(Session("Kod_Sub")) <> "" Then
                    SQL = "select Kod_Sub_Menu, Nama_Sub_Menu, isnull(Nama_Fail,'-')  Nama_Fail from SMKB_Sub_Menu where Kod_Sub = '" & Session("Kod_Sub") & "' and laporan <> '1' and status = 1
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
        End If
    End Sub

    Protected Sub rptSubMenu_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)

    End Sub
End Class