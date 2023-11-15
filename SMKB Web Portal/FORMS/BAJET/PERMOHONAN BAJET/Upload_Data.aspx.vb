Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Data.OleDb
Imports System.Configuration
Imports System.IO

Public Class Upload_Data
    Inherits System.Web.UI.Page

    Private conKew As New DBKewConn

    Dim Econ As OleDbConnection
    Dim con As SqlConnection
    Dim constr, Query, sqlconn As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

            End If
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs)

        ' Dim objHttpPostedFile As HttpPostedFile = FileUpload1.PostedFile
        'Dim CurrentFilePath = FileUpload1.PostedFile.FileName\\\\\

        'Dim CurrentFilePath As String = FileUpload1.PostedFile.FileName 'Path.GetFullPath(FileUpload1.PostedFile.FileName)
        'InsertExcelRecords(CurrentFilePath)

        Dim MyConnection As System.Data.OleDb.OleDbConnection
        Dim DtSet As System.Data.DataSet
        Dim MyCommand As System.Data.OleDb.OleDbDataAdapter
        MyConnection = New System.Data.OleDb.OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0;Data Source='C:\Users\02634\Desktop\code\test.xlsx';Extended Properties=Excel 8.0;")
        MyCommand = New System.Data.OleDb.OleDbDataAdapter("select * from [Sheet1$]", MyConnection)
        MyCommand.TableMappings.Add("Table", "Net-informations.com")
        DtSet = New System.Data.DataSet
        MyCommand.Fill(DtSet)
        'DataGridView1.DataSource = DtSet.Tables(0)
        MyConnection.Close()

    End Sub

    Private Sub InsertExcelRecords(ByVal FilePath As String)
        ExcelConn(FilePath)
        Query = String.Format("Select [Name],[City],[Address],[Designation] FROM [{0}]", "Sheet1$")
        Dim Ecom As OleDbCommand = New OleDbCommand(Query, Econ)
        Econ.Open()
        Dim ds As DataSet = New DataSet()
        Dim oda As OleDbDataAdapter = New OleDbDataAdapter(Query, Econ)
        Econ.Close()
        oda.Fill(ds)
        Dim Exceldt As DataTable = ds.Tables(0)
        connection()
        Dim objbulk As SqlBulkCopy = New SqlBulkCopy(con)
        objbulk.DestinationTableName = "Employee"
        objbulk.ColumnMappings.Add("Name", "Name")
        objbulk.ColumnMappings.Add("City", "City")
        objbulk.ColumnMappings.Add("Address", "Address")
        objbulk.ColumnMappings.Add("Designation", "Designation")
        'conKew.Open()
        'objbulk.WriteToServer(Exceldt)
        'conKew.Close()
    End Sub

    Private Sub ExcelConn(ByVal FilePath As String)
        constr = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0 Xml;HDR=YES;""", FilePath)
        Econ = New OleDbConnection(constr)
    End Sub

    Private Sub connection()
        sqlconn = ConfigurationManager.ConnectionStrings("SqlCom").ConnectionString
        con = New SqlConnection(sqlconn)
    End Sub



End Class