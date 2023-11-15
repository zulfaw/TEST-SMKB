Imports System.Data.SqlClient
Imports System.Web.Services
Imports Newtonsoft.Json

Public Class BukuVot
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            Dim ptjData As DataTable = GetPtjList()
            DropDownList1.DataSource = ptjData
            DropDownList1.DataBind()


            Dim corporate As DataTable = GetCorporate()
            DropDownList2.DataSource = corporate
            DropDownList2.DataBind()

            Dim kewangan As DataTable = GetKW()
            kodkw.DataSource = kewangan
            kodkw.DataBind()

            LoadHeader()
        End If

    End Sub

    Private Function GetPtjList() As DataTable

        Dim connection As New SqlConnection(strCon)
        Dim query As String = " select '00' AS KodPejabat, 'KESELURUHAN' AS Pejabat UNION ALL
        select concat(KodPejabat,'0000') as KodPejabat , Pejabat from [devmis\SQL_INS01].dbStaf.dbo.MS_PEJABAT where Status = 1"

        Dim command As New SqlCommand(query, connection)

        Dim ptjData As New DataTable()

        connection.Open()

        Using adapter As New SqlDataAdapter(command)
            adapter.Fill(ptjData)
        End Using

        connection.Close()

        Return ptjData
    End Function

    Private Function GetCorporate() As DataTable
        Dim connection As New SqlConnection(strCon)
        Dim query As String = "select Nama_Sing, Nama from SMKB_Korporat"
        Dim command As New SqlCommand(query, connection)

        Dim corporate As New DataTable()

        connection.Open()

        Using adapter As New SqlDataAdapter(command)
            adapter.Fill(corporate)
        End Using

        connection.Close()
        Return corporate

    End Function

    Private Function GetKW() As DataTable

        Dim connection As New SqlConnection(strCon)
        Dim query As String = "SELECT '00' AS Kod_Kump_Wang, 'KESELURUHAN' AS Butiran UNION ALL
                            SELECT Kod_Kump_Wang, CONCAT(Kod_Kump_Wang, ' - ', Butiran) AS Butiran
                            FROM SMKB_Kump_Wang
                            ORDER BY Kod_Kump_Wang"

        Dim command As New SqlCommand(query, connection)
        Dim kewangan As New DataTable()
        connection.Open()

        ' Execute the query and fill the DataTable with the results
        Using adapter As New SqlDataAdapter(command)
            adapter.Fill(kewangan)
        End Using

        ' Close the database connection
        connection.Close()

        ' Return the DataTable with the month data
        Return kewangan
    End Function

    Function ToTitleCase(input As String) As String
        Dim words As String() = input.Split(" "c)
        For i As Integer = 0 To words.Length - 1
            If words(i).Length > 0 Then
                words(i) = Char.ToUpper(words(i)(0)) + words(i).Substring(1).ToLower()
            End If
        Next
        Return String.Join(" ", words)
    End Function

    Private Sub LoadHeader()

        Dim strSql As String = "select a.Nama_Sing, a.Nama, a.Almt1, a.Almt2, a.Bandar, a.Poskod, b.Butiran as Negeri, a.Kod_Negara, a.No_Tel1, a.No_Tel2, 
                            a.No_Faks1, a.No_Faks2, a.Laman_Web, a.Logo, a.Emel, a.Kategori, a.No_GST
                            from SMKB_Korporat a, SMKB_Lookup_Detail b
                            where b.Kod_Detail = a.Kod_Negeri
                            and b.Kod = '0002'
                            and a.status = 1"

        Dim connection As New SqlConnection(strCon)
        connection.Open()

        Dim command As New SqlCommand(strSql, connection)
        Dim dbread As SqlDataReader = command.ExecuteReader()

        If dbread.Read() Then
            Dim imageData As Byte() = DirectCast(dbread("Logo"), Byte())
            Dim base64String As String = Convert.ToBase64String(imageData)
            imgMyImage.ImageUrl = String.Format("data:image/jpg;base64,{0}", base64String)

            lblNamaKorporat.Text = dbread("Nama")
            'lblAlamatKorporat.Text = dbread("Almt1") & ", " & dbread("Poskod") & ", " & dbread("Bandar") & ", " & dbread("Negeri")
            lblAlamatKorporat.Text = ToTitleCase(dbread("Almt1")) & ", " & ToTitleCase(dbread("Poskod")) & ", " & ToTitleCase(dbread("Bandar")) & ", " & ToTitleCase(dbread("Negeri"))
            lblNoTelFaks.Text = "No Tel: " & dbread("No_Tel1") & " Fax: " & dbread("No_Faks1")
            lblEmailKorporat.Text = dbread("Emel")
        End If

        dbread.Close()
        connection.Close()
    End Sub

End Class
