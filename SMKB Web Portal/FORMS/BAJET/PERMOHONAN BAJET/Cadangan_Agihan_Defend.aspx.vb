Imports System.Drawing
Imports System.Data.SqlClient
Imports System.Web
Public Class Cadangan_Agihan_Defend
    Inherits System.Web.UI.Page

    Protected strMsg, strNotice As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not IsPostBack Then

                Dim dateNext As Date = Now.AddYears(+1)
                txtYear.Text = dateNext.Year

                fBindgvAbmMaster()

            End If
        Catch ex As Exception
            sErrLog(ex.HResult, ex.Message, fGetFormName(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session("ssusrID"), Session("PcIP"), Session("PcName"), Session("SysVer"))
        End Try

    End Sub

    Private Sub fBindgvAbmMaster()
        Try
            Dim ds As New DataSet

            Dim strSql As String = " select  (KodPTJ + '0000') as KodPTJ ,Butiran, '-' as 'N1', '-' as 'N2' , '-' as 'N3' , '-' as 'N4', '0.00' as Total from MK_PTJ_Baru where Status = '1' order by KodPTJ "

            Dim dbconn As New DBKewConn

            ds = dbconn.fSelectCommand(strSql)
            Dim dt As DataTable = ds.Tables(0)
            gvAbmMaster.DataSource = dt
            gvAbmMaster.DataBind()

        Catch ex As Exception
            sErrLog(ex.HResult, ex.Message, fGetFormName(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session("ssusrID"), Session("PcIP"), Session("PcName"), Session("SysVer"))
        End Try
    End Sub

    Protected Sub gvAbmMaster_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvAbmMaster.RowCommand

        ' CommandName property to determine which button was clicked.
        If e.CommandName = "Select" Then

            ' Convert the row index stored in the CommandArgument
            ' property to an Integer.
            'Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            'Dim KodSubMenu = Request.QueryString("KodSubMenu")

            ' Get the last name of the selected author from the appropriate
            ' cell in the GridView control.
            'Dim selectedRow As GridViewRow = gvAbmMaster.Rows(index)
            'Dim NoMohon As String = selectedRow.Cells(2).Text

            ' Display the selected author.
            'Message.Text = "You selected " & contact & "."

            'Open other page.
            Response.Redirect($"~/FORMS/BAJET/PERMOHONAN BAJET/MaklumatDefendBend.aspx")
        End If
    End Sub

End Class

