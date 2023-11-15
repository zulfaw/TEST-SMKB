

Imports System.Data.SqlClient
Imports System.Web.Services
Imports Microsoft.Ajax.Utilities
''' 
''' This class handles database queries with functionality to rollback and log query
''' Exception will be logged but not handled in this class
''' query method will return false on error
''' 
Public Class Query

    'Default connection string 
    Public Shared connString As String = "Data Source=devmis12.utem.edu.my\sql_ins02;Initial Catalog=DbKewanganV4;Persist Security Info=True;User ID=Smkb;Password=Smkb@Dev2012"

    'sql connection object
    Private conn As SqlConnection

    'boolean flag which marks if the connection object initialized by this class or received from paramater
    Private isInnerCon As Boolean = True

    'object holding sqltransaction data to allow rollbacks
    Private transaction As SqlTransaction

    Private prevVals As String

    Private newVals As String

    Private operation As String

    Private table As String



    ''' execute insert query with sqlcommand as parameter
    ''' 
    ''' Return integer number of affected rows, -1 if fails/error 
    '''  
    Public Function execute(cmd As SqlCommand) As Integer
        Try
            initialize(cmd)


            Return cmd.ExecuteNonQuery()
        Catch ex As Exception
            logError("Insert Data", ex)
            Return -1

        End Try
    End Function

    'execute insert query with sql string as paramater
    Public Function execute(sqlString As String, param As List(Of SqlParameter)) As Integer
        Dim ncmd As SqlCommand = New SqlCommand()
        ncmd.CommandText = sqlString
        ncmd.Parameters.Add(param)
        Return execute(ncmd)
    End Function

    ''' 
    ''' Call this method to rollback any sql transaction done using the object of this class
    ''' Connection will be closed if it is not connection passed by caller 
    '''  
    Public Sub rollback()
        transaction.Rollback()
        If isInnerCon Then
            conn.Close()
        End If
    End Sub


    ''' 
    ''' Call this method to apply the changes made by sql queries through object of this class
    ''' Connection will be closed if it is not connection passed by caller 
    '''  
    Public Sub finish()
        transaction.Commit()
        logTransaction()
        If isInnerCon Then
            conn.Close()
        End If
    End Sub


    '''  
    ''' This method intialize the sql command object passed
    ''' if connection already exist it will be used 
    '''    if existing connection is passed inside the object, this class will not handle its closure
    '''    
    ''' if no existing connection default connecton string will be used to create a new connection
    '''  
    '''  
    Private Sub initialize(cmd As SqlCommand)
        If cmd.Connection Is Nothing Then
            conn = New SqlConnection(Query.connString)
            conn.Open()
            isInnerCon = True

        Else
            conn = cmd.Connection

            If conn.State <> ConnectionState.Open Then
                conn.Open()
            End If

            isInnerCon = False
        End If
        transaction = conn.BeginTransaction()
        cmd.Connection = conn
        cmd.Transaction = transaction
    End Sub

    Public Sub logError(process As String, exc As Exception)
        Dim stacks As New StackTrace(True)
        Dim fname As String = stacks.GetFrame(0).GetFileName()
        Dim source As String = ""
        Dim i As Integer = 0
        For Each st As StackFrame In stacks.GetFrames()
            If Not String.IsNullOrEmpty(st.GetFileName()) AndAlso Not st.GetFileName().Equals(fname) Then
                source += " [" + i.ToString() + "] Method: " + st.GetMethod().Name + " in : " + st.GetFileName() + " at line " + st.GetFileLineNumber().ToString()
                i += 1
            End If
        Next
        Dim type As String = exc.GetType().ToString()
        Dim desc As String = exc.Message
        Dim userID As String = HttpContext.Current.Session("ssusrID")
        'log error here to db later
    End Sub

    Private Sub logTransaction()

    End Sub

    Private Sub parseQuery(ByVal query As String)
        Dim parsed As List(Of String) = query.Split(" "c).ToList()
        operation = parsed.ElementAt(0).Trim().ToLower()

        If operation.Equals("insert") Or operation.Equals("delete") Then
            'insert into table   || delete from table
            ' 0       1    2          0      1    2
            table = parsed.ElementAt(2).Trim().ToLower()
        ElseIf operation.Equals("update") Then
            'update table set
            ' 0       1
            table = parsed.ElementAt(1).Trim().ToLower()
        End If
    End Sub

    Private Sub loadPrevious()

    End Sub
End Class
