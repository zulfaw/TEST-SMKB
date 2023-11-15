Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports System.Globalization
Imports AjaxControlToolkit
Imports System.Data.Entity.SqlServer

Public Class Perubahan_ROC
    Inherits System.Web.UI.Page
    Public Shared dvJenisCek As New DataView
    Public Shared dsJenisCek As New DataSet
    Private sqlCon As SqlConnection
    Private strConn As String = "Data Source=devmis12.utem.edu.my;Initial Catalog=dbKewanganV4;Persist Security Info=True;User ID=smkb;Password=Smkb@Dev2012"
    Private strConnStaf As String = "Data Source=devmis11.utem.edu.my;Initial Catalog=dbStaf;Persist Security Info=True;User ID=smkb;Password=Smkb@Dev2012"
    'Private oDB As IDatabase
    Dim dbconn As New DBKewConn
    Dim dbconnstaf As New DBSMConn
    Public dsKod As New DataSet
    Public dvKodKW As New DataView
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                'fBindDdlBulan()
                fLoadBlnGaji()
                'fBindGvROC()
                'BindListViewControls()
                fBindGv()
                ViewState("savemode") = 1


            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub fLoadBlnGaji()

        Dim sqlROC01 As String = $"select * from SMKB_Gaji_bulan;"
        Dim ds = dbconn.fSelectCommand(sqlROC01)
        Dim list As New ListItem()
        Using dt = ds.Tables(0)
            If dt.Rows.Count > 0 Then
                ViewState("BulanGj") = dt.Rows(0)("Bulan").ToString
                ViewState("TahunGj") = dt.Rows(0)("Tahun").ToString
                Me.ddlBulan.Text = dt.Rows(0)("Bulan").ToString & "/" & dt.Rows(0)("Tahun").ToString

                list.Text = dt.Rows(0)("Bulan").ToString & "/" & dt.Rows(0)("Tahun").ToString
                list.Value = dt.Rows(0)("Bulan").ToString & "/" & dt.Rows(0)("Tahun").ToString
                ddlBulan.Items.Add(list)
            End If
        End Using
    End Sub
    Private Sub fBindDdlBulan()
        Dim Date1 As New DateTime(ViewState("TahunGj"), ViewState("BulanGj"), 1)
        Dim Date2 As New DateTime(ViewState("TahunGj"), 12, 31)
        Dim bulan As String
        Dim NumOfMonths = DateDiff("m", Date1, Date2)
        Dim JumBulan = NumOfMonths + 1
        Dim blngj As Integer = ViewState("BulanGj")
        Dim thngj As Integer = ViewState("TahunGj")
        'Dim Date1 As DateTime = Convert.ToDateTime("1/1/2000")
        For i As Integer = 1 To JumBulan

            Dim list As New ListItem()
            If blngj <= 12 Then
                blngj = blngj
                thngj = thngj
            Else
                blngj = Math.Round(blngj / 12)
                thngj = thngj + 1
            End If
            list.Text = blngj.ToString() + "/" + thngj.ToString()
            list.Value = blngj.ToString() + "/" + thngj.ToString()
            ddlBulan.Items.Add(list)

            blngj = blngj + 1
        Next
    End Sub
    Private Function fBindGv()


        Try
            Dim dt As New DataTable
            dt = fCreateDtKW()

            If dt.Rows.Count = 0 Then
                gvKod.DataSource = New List(Of String)
            Else
                gvKod.DataSource = dt
            End If
            gvKod.DataBind()
            'lblJumRekod.InnerText = dt.Rows.Count


            'Required for jQuery DataTables to work.
            gvKod.UseAccessibleHeader = True
            gvKod.HeaderRow.TableSection = TableRowSection.TableHeader

        Catch ex As Exception

        End Try
    End Function

    Private Function fCreateDtKW() As DataTable
        Dim blnStatus As Boolean
        Dim strStatus As String
        Try

            Dim dt As New DataTable
            dt.Columns.Add("no_staf", GetType(String))
            dt.Columns.Add("ms01_nama", GetType(String))
            dt.Columns.Add("kod_trans", GetType(String))
            dt.Columns.Add("jumlama", GetType(String))
            dt.Columns.Add("jumbaru", GetType(String))
            dt.Columns.Add("MS15_Keterangan", GetType(String))
            dt.Columns.Add("MS15_NoRoc", GetType(String))

            Dim strNoStaf As String
            Dim strNama As String
            Dim NoRoc As String
            Dim Keterangan As String
            Dim kodtrans As String
            Dim jumlama As String
            Dim jumbaru As String

            'Dim strSql As String = "select R1NoStaf, R1TkhDisahkan, R1NoRujSurat, R1NoRoc, R1Keterangan  from [qa11].dbstaf.dbo.VMS15_ROC Where R1StaBendahari Is NOT NULL" &
            '" And YEAR(R1TkhDisahkan) = '2018'" &
            '" AND MONTH(R1TkhDisahkan) = '1' And R1KodROC Not In (Select KOD_ROC FROM SMKB_Gaji_ROC)" &
            '" ORDER BY R1NoStaf"

            'Dim strSql As String = "Select z.R1NoStaf,z.R1NoRoc,z.R1Keterangan,z.kodtrans,z.roc01_amaunakandibayar as jumbaru," &
            '" (select isnull(amaun,0) from SMKB_Gaji_Lejar_His c where no_staf = R1NoStaf And bulan=5 And tahun=2023 And c.Kod_Trans=z.kodtrans ) As jumlama" &
            '" from(" &
            '" Select distinct b.ROC01_KumpButiran, R1NoStaf, R1TkhDisahkan, R1NoRujSurat, R1NoRoc, R1Keterangan, IsNull(b.ROC01_AmaunAkanDibayarB, b.roc01_amaunakandibayar) As roc01_amaunakandibayar," &
            '" b.ROC01_Butiran, Case When b.ROC01_KumpButiran = '1' then (select kod_smkb from SMKB_Gaji_Kod_Convert where kod_smsm= b.ROC01_Butiran)" &
            '" when b.ROC01_KumpButiran = '2' then (select kod_smkb from SMKB_Gaji_Kod_Convert where kod_smsm= left(b.ROC01_Butiran,5)) end as kodtrans" &
            '" From [qa11].dbstaf.dbo.VMS15_ROC a, [qa11].dbstaf.dbo.ROC01_BUTIR b Where" &
            '" a.R1NoRoc = b.MS15_NoROC And R1StaBendahari Is Not NULL And YEAR(R1TkhDisahkan) = '" & ViewState("TahunGj") & "' " &
            '" And MONTH(R1TkhDisahkan) = '" & ViewState("BulanGj") & "' And R1KodROC Not In (Select KOD_ROC FROM SMKB_Gaji_ROC) and b.ROC01_KumpButiran in ('1','2'))z " &
            '" ORDER BY R1NoStaf"


            Dim strSql As String = "Select a.no_staf,e.ms01_nama,kod_trans + ' | ' + (select c.Butiran from SMKB_Gaji_Kod_Trans c where c.Kod_Trans=a.Kod_Trans) as kod_trans," &
            " (select amaun from smkb_gaji_lejar b where a.kod_trans = b.Kod_Trans And b.bulan=5 And b.tahun=2023 And b.No_Staf=a.No_Staf) as jumlama ,amaun as jumbaru,d.MS15_Keterangan,d.MS15_NoRoc" &
            " From smkb_gaji_master a inner Join [qa11].dbstaf.dbo.MS15_ROC d on  a.no_roc = d.MS15_NoRoc " &
            " inner Join [qa11].dbstaf.dbo.ms01_peribadi e on d.ms01_nostaf=e.ms01_nostaf" &
            " Where MS15_StaBendahari Is Not NULL" &
            " And YEAR(MS15_TkhDisahkan) = '2023' " &
            " And MONTH(MS15_TkhDisahkan) = '6' And MS15_KodROC Not In (Select KOD_ROC FROM SMKB_Gaji_ROC)"

            Dim dbconn As New DBKewConn
            dsKod = dbconn.fSelectCommand(strSql)
            dvKodKW = New DataView(dsKod.Tables(0))

            For i As Integer = 0 To dsKod.Tables(0).Rows.Count - 1

                strNoStaf = dsKod.Tables(0).Rows(i)(0).ToString
                strNama = dsKod.Tables(0).Rows(i)(1).ToString
                kodtrans = dsKod.Tables(0).Rows(i)(2).ToString
                jumlama = dsKod.Tables(0).Rows(i)(3).ToString
                jumbaru = dsKod.Tables(0).Rows(i)(4).ToString
                Keterangan = dsKod.Tables(0).Rows(i)(5).ToString
                NoRoc = dsKod.Tables(0).Rows(i)(6).ToString

                dt.Rows.Add(strNoStaf, strNama, kodtrans, jumlama, jumbaru, Keterangan, NoRoc)
            Next

            Return dt

        Catch ex As Exception

        End Try

    End Function



    Private Function fCreateDtROC() As DataTable

        'Dim strSql As String = "SELECT * From VMS15_ROC WHERE R1StaBendahari IS NULL ORDER BY R1NoStaf"
        'Dim strSql As String = "Select * From VMS15_ROC Where R1StaBendahari Is NULL" &
        '" And YEAR(R1TkhDisahkan) = '" & ViewState("BulanGj") & "'" &
        '" AND MONTH(R1TkhDisahkan) = '" & ViewState("TahunGj") & "'" &
        '" AND R1KodROC NOT IN (SELECT KODROC FROM [devmis12].dbKewangan.dbo.GJ_ROC)" &
        '" ORDER BY R1NoStaf"

        Dim strSql As String = "Select * From VMS15_ROC Where R1StaBendahari Is NULL" &
        " And YEAR(R1TkhDisahkan) = 2018 AND MONTH(R1TkhDisahkan) = 1 ORDER BY R1NoStaf"
        Using dt = dbconnstaf.fselectCommandDt(strSql)
            Return dt
        End Using
    End Function
    Private Sub gvKod_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvKod.PageIndexChanging
        gvKod.PageIndex = e.NewPageIndex
        fBindGv()
    End Sub
    Private Sub gvKod_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvKod.RowCommand
        Try
            If e.CommandName = "Select" Then


                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim selectedRow As GridViewRow = gvKod.Rows(index)

                'txtKodKW.Value = selectedRow.Cells(0).Text.ToString

                'txtButiran.Value = selectedRow.Cells(1).Text.ToString
                'Dim myHiddenField As LinkButton = CType(selectedRow.FindControl("lnkButton"), LinkButton)
                'Dim value As String = myHiddenField.Text
                'txtButiran.Value = value


                'Dim statusAktif = selectedRow.Cells(3).Text.ToString
                'If statusAktif = "Aktif" Then
                '    statusAktif = 1
                'Else
                '    statusAktif = 0
                'End If
                ''rblStatus.SelectedValue = statusAktif
                ' ClientScript.RegisterStartupScript([GetType](), "alert", "ShowPopup('1');", True)
                Dim KodSubMenu = "080101"
                Dim KodSub = Request.QueryString("KodSub")
                Dim nostaf As String = selectedRow.Cells(0).Text.ToString
                Dim noroc As String = selectedRow.Cells(4).Text.ToString
                Response.Redirect($"~/FORMS/GAJI/ROC/Maklumat_ROC.aspx?KodSub={KodSub}&KodSubMenu={KodSubMenu}&nostaf={nostaf}&noroc={noroc}")


            End If
        Catch ex As Exception

        End Try
    End Sub



End Class