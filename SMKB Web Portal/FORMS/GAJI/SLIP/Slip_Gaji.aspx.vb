Imports System.Data.SqlClient
Imports System.Web.Services
Imports Newtonsoft.Json

Public Class Slip_Gaji
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ' Fetch data from the database
            Dim stafData As DataTable = GetStafList()
            ' Bind the data to the dropdown list
            DropDownList1.DataSource = stafData
            DropDownList1.DataBind()








        End If
    End Sub
    Private Function GetStafList() As DataTable
        ' Create a connection to your database
        Dim connectionString As String = "server=devmis12.utem.edu.my;database=DbKewanganV4;uid=Smkb;pwd=Smkb@Dev2012;"
        Dim connection As New SqlConnection(connectionString)

        ' Create a SQL query to fetch the month data
        Dim query As String = "SELECT A.MS01_NOSTAF, A.MS01_NAMA, A.MS01_NOSTAF + ' - ' +  A.MS01_NAMA as descnm
		From [qa11].dbstaf.dbo.MS01_PERIBADI A INNER JOIN [qa11].dbstaf.dbo.MS08_PENEMPATAN B ON (A.MS01_NOSTAF = B.MS01_NOSTAF )
		WHERE B.MS08_StaTerkini = '1' and A.MS01_STATUS='1' 
		 order by A.MS01_NAMA"

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


End Class