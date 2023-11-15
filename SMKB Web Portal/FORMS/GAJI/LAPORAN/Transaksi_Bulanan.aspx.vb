Imports System.Data.SqlClient
Imports System.Web.Services
Imports Newtonsoft.Json

Public Class Transaksi_Bulanan
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ' Fetch data from the database
            Dim ptjData As DataTable = GetPtjList()
            ' Bind the data to the dropdown list
            ptjFilter.DataSource = ptjData
            ptjFilter.DataBind()


            Dim corporate As DataTable = GetCorporate()
            ' Bind the data to the dropdown list
            syarikatFilter.DataSource = corporate
            syarikatFilter.DataBind()

            'Dim jenisTransData As DataTable = GetJenisTransList()
            '' Bind the data to the dropdown list
            'jenisTransFilter.DataSource = jenisTransData
            'jenisTransFilter.DataBind()

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

        ' Return the DataTable with the month data
        Return corporate
    End Function

    Private Function GetJenisTransList() As DataTable
        ' Create a connection to your database
        Dim connectionString As String = "server=devmis12.utem.edu.my;database=DbKewanganV4;uid=Smkb;pwd=Smkb@Dev2012;"
        Dim connection As New SqlConnection(connectionString)

        ' Create a SQL query to fetch the month data
        Dim query As String = "SELECT '' AS JenisTrans, '-- Sila Pilih --' AS NamaJenisTrans UNION ALL
                               SELECT DISTINCT Jenis_Trans AS JenisTrans, Butiran AS NamaJenisTrans
                               FROM SMKB_Gaji_Jenis_Trans 
                               WHERE Jenis_Trans IN ('E','K','N','T','O')"

        ' Create a SqlCommand object to execute the query
        Dim command As New SqlCommand(query, connection)

        ' Create a DataTable to store the results
        Dim jenisTransData As New DataTable()

        ' Open the database connection
        connection.Open()

        ' Execute the query and fill the DataTable with the results
        Using adapter As New SqlDataAdapter(command)
            adapter.Fill(jenisTransData)
        End Using

        ' Close the database connection
        connection.Close()

        ' Return the DataTable with the month data
        Return jenisTransData
    End Function

    'Protected Sub ptjFilter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ptjFilter.SelectedIndexChanged
    '    Dim selectedPtjValue As String = ptjFilter.SelectedValue

    '    ' Perform a database query to fetch data for staffFilter based on selectedPtjValue
    '    Dim connectionString As String = "server=devmis12.utem.edu.my;database=DbKewanganV4;uid=Smkb;pwd=Smkb@Dev2012;" ' Replace with your database connection string
    '    Dim query As String = "SELECT '00' AS No_Staf, 'KESELURUHAN' AS MS01_Nama UNION ALL
    '                            SELECT DISTINCT A.No_Staf, B.MS01_Nama
    '                            FROM SMKB_Gaji_Lejar A
    '                            LEFT JOIN [DEVMIS\SQL_INS01].dbStaf.dbo.MS01_Peribadi AS B ON B.MS01_NoStaf = A.No_Staf
    '                            WHERE A.Kod_PTJ = @SelectedPtj"

    '    Using connection As New SqlConnection(connectionString)
    '        Using command As New SqlCommand(query, connection)
    '            command.Parameters.AddWithValue("@SelectedPtj", selectedPtjValue)
    '            connection.Open()
    '            Dim reader As SqlDataReader = command.ExecuteReader()

    '            ' Clear existing items in staffFilter
    '            staffFilter.Items.Clear()

    '            ' Populate staffFilter with data from the database
    '            While reader.Read()
    '                Dim NoStaf As String = reader("No_Staf").ToString()
    '                Dim NamaStaf As String = reader("MS01_Nama").ToString()
    '                staffFilter.Items.Add(New ListItem(NamaStaf, NoStaf))
    '            End While

    '            reader.Close()
    '        End Using
    '    End Using
    'End Sub
    'Protected Sub jenisTransFilter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ptjFilter.SelectedIndexChanged
    '    Dim selectedJenisTransValue As String = jenisTransFilter.SelectedValue

    '    Dim trimValue As String

    '    If selectedJenisTransValue.Length >= 4 Then
    '        trimValue = selectedJenisTransValue.Substring(0, selectedJenisTransValue.Length - 4)
    '    End If
    '    ' Perform a database query to fetch data for staffFilter based on selectedPtjValue
    '    Dim connectionString As String = "server=devmis12.utem.edu.my;database=DbKewanganV4;uid=Smkb;pwd=Smkb@Dev2012;" ' Replace with your database connection string
    '    Dim query As String = "SELECT '' AS Kod_Trans, '-- Sila Pilih --' AS Butiran UNION ALL
    '                           SELECT Kod_Trans, Butiran
    '                           FROM SMKB_Gaji_Kod_Trans
    '                           WHERE Jenis_Trans = @SelectedJenisTrans"

    '    Using connection As New SqlConnection(connectionString)
    '        Using command As New SqlCommand(query, connection)
    '            command.Parameters.AddWithValue("@SelectedJenisTrans", selectedJenisTransValue)
    '            connection.Open()
    '            Dim reader As SqlDataReader = command.ExecuteReader()

    '            ' Clear existing items in staffFilter
    '            kodTransFilter.Items.Clear()

    '            ' Populate staffFilter with data from the database
    '            While reader.Read()
    '                Dim JenisTrans As String = reader("Kod_Trans").ToString()
    '                Dim NamaJenisTrans As String = reader("Butiran").ToString()
    '                kodTransFilter.Items.Add(New ListItem(NamaJenisTrans, JenisTrans))
    '            End While

    '            reader.Close()
    '        End Using
    '    End Using
    'End Sub

    Protected Sub Check_Clicked(sender As Object, e As EventArgs)
        ' Your code here

    End Sub

End Class