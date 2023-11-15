Imports System.Data.SqlClient
Imports System.Web.Services

Public Class TableTransaksiPotongan
    Inherits System.Web.UI.UserControl
    Dim totalAmaun As Decimal = 0
    Dim totalStaff As Decimal = 0
    Public Property totalAmaunAll As Decimal
    Public Property totalStaffAll As Decimal
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'lblTajuk.InnerHtml = Tajuk
            Dim bulan As String = Session("bulan")
            Dim tahun As String = Session("tahun")
            Dim ptj As String = Session("ptj")
            Dim kodpotongan As String = Session("kodpotongan")
            rptone.DataSource = GetPotongan(bulan, tahun, ptj, kodpotongan)
            rptone.DataBind()
        End If
    End Sub

    Private Function GetPotongan(bulan As String, tahun As String, ptj As String, kodpotongan As String) As DataTable
        Dim connection As New SqlConnection(strCon)
        'Dim kodpotongan As String = ""

        If kodpotongan = "00" Then
            kodpotongan = "%"
        Else
            kodpotongan = kodpotongan + "%"
        End If

        If ptj = "00" Then
            ptj = "%"
        ElseIf ptj = "-0000" Then
            ptj = "-"
        Else
            ptj = Session("ptj") + "%"
        End If

        Dim query As String = "SELECT DISTINCT A.Kod_Trans, CONCAT(A.Kod_Trans, ' - ', B.Butiran) AS Butiran
                            FROM SMKB_Gaji_Lejar A
                            INNER JOIN SMKB_Gaji_Potongan B ON B.Kod = A.Kod_Trans
                            WHERE A.Jenis_Trans = 'P' AND Bulan = @bulan AND Tahun = @tahun AND Kod_PTJ LIKE @ptj AND Kod_Trans LIKE @kodpotongan
                            ORDER BY A.Kod_Trans"


        Dim command As New SqlCommand(query, connection)
        command.Parameters.Add(New SqlParameter("@tahun", tahun))
        command.Parameters.Add(New SqlParameter("@bulan", bulan))
        command.Parameters.Add(New SqlParameter("@ptj", ptj))
        command.Parameters.Add(New SqlParameter("@kodpotongan", kodpotongan))
        Dim potongan As New DataTable()
        connection.Open()

        ' Execute the query and fill the DataTable with the results
        Using adapter As New SqlDataAdapter(command)
            adapter.Fill(potongan)
        End Using

        ' Close the database connection
        connection.Close()

        ' Return the DataTable with the month data
        Return potongan
    End Function

    Private Function LoadDataTable(KodPotongan As String) As DataTable
        Dim query As String
        Dim ptj As String

        If Session("ptj") = "00" Then
            ptj = "%"
        ElseIf Session("ptj") = "-0000" Then
            ptj = "-"
        Else
            ptj = Session("ptj") + "%"
        End If

        query = "SELECT ROW_NUMBER() OVER (ORDER BY A.No_Staf) AS CountNumber, A.No_Staf AS No, B.MS01_Nama AS Nama, Amaun
                FROM SMKB_Gaji_Lejar A
                LEFT JOIN [DEVMIS\SQL_INS01].dbStaf.dbo.MS01_Peribadi AS B ON B.MS01_NoStaf = A.No_Staf
                WHERE Bulan = @bulan AND Tahun = @tahun AND Kod_PTJ LIKE @ptj AND Kod_Trans LIKE @kodpotongan"

        Dim dataTable As New DataTable()

        Using connection As New SqlConnection(strCon)
            Using command As New SqlCommand(query, connection)
                command.Parameters.Add(New SqlParameter("@tahun", Session("tahun")))
                command.Parameters.Add(New SqlParameter("@bulan", Session("bulan")))
                command.Parameters.Add(New SqlParameter("@ptj", ptj))
                command.Parameters.Add(New SqlParameter("@KodPotongan", KodPotongan))

                connection.Open()
                dataTable.Load(command.ExecuteReader())
            End Using
        End Using
        Return dataTable
    End Function

    <WebMethod(EnableSession:=True)>
    Protected Sub rptone_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
        Dim rptItem As RepeaterItem = TryCast(e.Item, RepeaterItem)
        Dim kodPotongan As String = rptItem.DataItem("Kod_Trans")

        Dim rptTwo As Repeater = TryCast(rptItem.FindControl("repeaterDetail"), Repeater)
        rptTwo.DataSource = LoadDataTable(kodPotongan)
        totalAmaun = 0
        totalStaff = 0
        rptTwo.DataBind()

        Dim jumlahAmaunControl As HtmlTableCell = DirectCast(e.Item.FindControl("jumlahAmaun"), HtmlTableCell)

        If jumlahAmaunControl IsNot Nothing Then
            jumlahAmaunControl.InnerText = "Jumlah Amaun: " + totalAmaun.ToString("N2") ' Format as a decimal with two decimal places
        End If

        Dim jumlahPekerjaControl As HtmlTableCell = DirectCast(e.Item.FindControl("jumlahPekerja"), HtmlTableCell)

        If jumlahPekerjaControl IsNot Nothing Then
            jumlahPekerjaControl.InnerText = "Jumlah Pekerja: " + totalStaff.ToString()
        End If

        Session("jumlahBesarPekerja") = totalStaffAll
        Session("jumlahBesarAmaun") = totalAmaunAll.ToString("N2")
    End Sub

    Protected Sub repeaterDetail_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            ' Retrieve the Amaun value from the data source
            Dim amaun As Decimal = Convert.ToDecimal(DataBinder.Eval(e.Item.DataItem, "Amaun"))
            ' Accumulate the total Amaun
            totalAmaun += amaun
            totalStaff += 1
            totalAmaunAll += amaun
            totalStaffAll += 1
        End If
    End Sub
End Class