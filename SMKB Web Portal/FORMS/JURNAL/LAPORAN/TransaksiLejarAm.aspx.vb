Imports System.Data.SqlClient
Imports System.Web.Services
Imports Newtonsoft.Json

Public Class TransaksiLejarAm
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ' Fetch data from the database
            Dim ptjData As DataTable = GetPtjList()
            ' Bind the data to the dropdown list
            ptj.DataSource = ptjData
            ptj.DataBind()

            Dim corporate As DataTable = GetCorporate()
            ' Bind the data to the dropdown list
            syarikat.DataSource = corporate
            syarikat.DataBind()

            Dim kodvot As DataTable = GetVot()
            'Bind the data to the dropdown list
            votlist.DataSource = kodvot
            votlist.DataBind()

            Dim kodkw As DataTable = GetKW()
            'Bind the data to the dropdown list
            kodkwlist.DataSource = kodkw
            kodkwlist.DataBind()

        End If
    End Sub



    Private Function GetPtjList() As DataTable
        ' Create a connection to your database
        Dim connectionString As String = "server=devmis12.utem.edu.my;database=DbKewanganV4;uid=Smkb;pwd=Smkb@Dev2012;"
        Dim connection As New SqlConnection(connectionString)

        ' Create a SQL query to fetch the month data
        Dim query As String = " select '00' AS KodPejabat, 'KESELURUHAN' AS Pejabat UNION ALL
        select concat(KodPejabat,'0000') as KodPejabat , Pejabat from [devmis\SQL_INS01].dbStaf.dbo.MS_PEJABAT where Status = 1"

        ' Create a SqlCommand object to execute the query
        Dim command As New SqlCommand(query, connection)

        ' Create a DataTable to store the results
        Dim ptjData As New DataTable()

        ' Open the database connection
        connection.Open()

        ' Execute the query and fill the DataTable with the results
        Using adapter As New SqlDataAdapter(command)
            adapter.Fill(ptjData)
        End Using

        ' Close the database connection
        connection.Close()

        ' Return the DataTable with the month data
        Return ptjData
    End Function

    Private Function GetCorporate() As DataTable
        ' Create a connection to your database
        Dim connectionString As String = "server=devmis12.utem.edu.my;database=DbKewanganV4;uid=Smkb;pwd=Smkb@Dev2012;"
        Dim connection As New SqlConnection(connectionString)

        ' Create a SQL query to fetch the month data
        Dim query As String = "select Nama_Sing, Nama from SMKB_Korporat"

        ' Create a SqlCommand object to execute the query
        Dim command As New SqlCommand(query, connection)

        ' Create a DataTable to store the results
        Dim corporate As New DataTable()

        ' Open the database connection
        connection.Open()

        ' Execute the query and fill the DataTable with the results
        Using adapter As New SqlDataAdapter(command)
            adapter.Fill(corporate)
        End Using

        ' Close the database connection
        connection.Close()


        Return corporate
    End Function

    Private Function GetVot() As DataTable
        ' Create a connection to your database
        Dim connectionString As String = "server=devmis12.utem.edu.my;database=DbKewanganV4;uid=Smkb;pwd=Smkb@Dev2012;"
        Dim connection As New SqlConnection(connectionString)

        ' Create a SQL query to fetch the month data
        Dim query As String = "SELECT * FROM (	        Select SMKB_Kod_Jenis_Vot as Kod, SMKB_Butiran as butiran, SMKB_Kod_Jenis_Vot	        from SMKB_Jenis_Vot 	        union all	        SELECT '#' as Kod, 'KESELURUHAN' as butiran, '#' as SMKB_Kod_Jenis_VOt	        ) mainTbl        order by SMKB_Kod_Jenis_Vot"

        ' Create a SqlCommand object to execute the query
        Dim command As New SqlCommand(query, connection)

        ' Create a DataTable to store the results
        Dim kodvot As New DataTable()

        ' Open the database connection
        connection.Open()

        ' Execute the query and fill the DataTable with the results
        Using adapter As New SqlDataAdapter(command)
            adapter.Fill(kodvot)
        End Using

        ' Close the database connection
        connection.Close()


        Return kodvot
    End Function

    Private Function GetKW() As DataTable
        ' Create a connection to your database
        Dim connectionString As String = "server=devmis12.utem.edu.my;database=DbKewanganV4;uid=Smkb;pwd=Smkb@Dev2012;"
        Dim connection As New SqlConnection(connectionString)

        ' Create a SQL query to fetch the month data
        Dim query As String = "select                                 CONCAT(Kod_Kump_Wang, ' - ', Butiran) AS Kod_Kw, Kod_Kump_Wang, Butiran                                from SMKB_Kump_Wang                                Order by Kod_Kump_Wang"

        ' Create a SqlCommand object to execute the query
        Dim command As New SqlCommand(query, connection)

        ' Create a DataTable to store the results
        Dim kodkw As New DataTable()

        ' Open the database connection
        connection.Open()

        ' Execute the query and fill the DataTable with the results
        Using adapter As New SqlDataAdapter(command)
            adapter.Fill(kodkw)
        End Using

        ' Close the database connection
        connection.Close()

        Return kodkw
    End Function

End Class