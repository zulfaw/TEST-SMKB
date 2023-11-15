Imports System.CodeDom
Imports System.Data.SqlClient

Public Class SiteMaster
    Inherits MasterPage
    Dim Sql As String
    Dim uKodModul As String
    Dim nostaf As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        uKodModul = Session("KodModul")
        nostaf = Session("LOGINID")

        If Page.IsPostBack = False Then

            '    Dim nostaf As String = Request.QueryString("UsrLogin")
            Dim uSession As String = Request.QueryString("usession")


            Dim statsRpt As String = Request.QueryString("rpt")
            Dim kodReport As String = Request.QueryString("kod")
            Dim kodSub As String = Request.QueryString("kodsub")

            If kodSub <> "" Then
                Session("activeReport") = ""
            End If


            If statsRpt = "true" Then
                Session("activeReport") = kodReport
            End If

            '---SEMAK---
            dbconnNew = New SqlConnection(strCon)
            dbconnNew.Open()


            If uKodModul <> "" Then


                Sql = "select Kod_Sub, Nama_Sub, Nama_Icon from SMKB_Sub_Modul where status = 1 and Kod_Modul = '" & uKodModul & "'
                order by Urutan"
                dbcomm = New SqlCommand(Sql, dbconnNew)

                Dim paramSql() As SqlParameter = {
                            }
                Dim ds As DataSet = NewGetDataPB(Sql, paramSql, dbconnNew)

                If (ds.Tables.Count > 0) Then
                    rptMenu.DataSource = ds
                    rptMenu.DataBind()
                End If

                dbconnNew.Close()
                dbconnNew.Dispose()

                'Response.Redirect("~/Header.aspx")
            End If

            'Response.Redirect("~/Header.aspx")
        End If
    End Sub

    Public Function CheckReportActive(KodSub)
        If KodSub = Session("activeReport") Then
            Return "show"
        End If
        Return ""
    End Function

    Public Function GetListOfReports(KodSub As String, obj As RepeaterItem, btn As LinkButton)
        '---SEMAK---
        dbconnNew = New SqlConnection(strCon)
        dbconnNew.Open()


        If KodSub <> "" Then
            Dim rpt As Repeater = TryCast(obj.FindControl("rptMenuDetails"), Repeater)

            Sql = "SELECT *, ISNULL(Nama_Fail, '-') as URL  FROM SMKB_Sub_Menu
            WHERE Kod_Sub ='" + KodSub + "' and laporan = 1 order by urutan"

            dbcomm = New SqlCommand(Sql, dbconnNew)

            Dim paramSql() As SqlParameter = {
                            }
            Dim ds As DataSet = NewGetDataPB(Sql, paramSql, dbconnNew)

            If (ds.Tables(0).Rows.Count > 0) Then
                rpt.DataSource = ds
                rpt.DataBind()
                btn.Attributes("href") = "#toggle" + KodSub
            Else
                btn.Attributes("data-toggle") = ""
                btn.Attributes("class") = ""
            End If

            dbconnNew.Close()
            dbconnNew.Dispose()

            'Response.Redirect("~/Header.aspx")
        End If

    End Function

    Protected Sub rptMenu_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
        Dim rptItem As RepeaterItem = e.Item
        Dim hdn As HiddenField = TryCast(rptItem.FindControl("hidKodSub"), HiddenField)
        Dim btn As LinkButton = TryCast(rptItem.FindControl("btnSubMenu2"), LinkButton)

        GetListOfReports(hdn.Value, rptItem, btn)
    End Sub
End Class