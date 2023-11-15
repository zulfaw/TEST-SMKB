Imports System.Data.Odbc
Imports System.Data.SqlClient
Imports System.Web.Configuration

Public Class DBKewConn

#Region "Declares"
    Private dbConn As SqlConnection
    Private dbComm As SqlCommand
    Private dbAdapter As SqlDataAdapter
    Private dbSQLTxn As SqlTransaction
    'Private dbDataset As DataSet
    'Private strSQL As String
    Public ConnectionString As String
    Private dbcmd As SqlCommand
    Private status_ As String
    Public dt As DataTable
    Private dr As SqlDataReader
    Dim cmd_status As Boolean = False
    ' --- kzana tambah
    Private conn_ As String = ""


#End Region

    Public Sub New(Optional conn As String = "")
        sReadConfig()
        sInitializeConnStr()
        sCreateConnection()
        ' ---- kzana tambah
        conn_ = conn
    End Sub

    Public Sub sReadConfig()
        If conn_ = "smsm" Then     'KZANA tambah
            strDbServer = WebConfigurationManager.AppSettings("DBSMServerName")
            strDbName = WebConfigurationManager.AppSettings("DBSMName")
            strDBPassword = WebConfigurationManager.AppSettings("DBSMPassword")
            strDBUser = WebConfigurationManager.AppSettings("DBSMUserID")
        Else
            strDbServer = WebConfigurationManager.AppSettings("DBKewServerName")
            strDbName = WebConfigurationManager.AppSettings("DBKewName")
            strDBPassword = WebConfigurationManager.AppSettings("DBKewPassword")
            strDBUser = WebConfigurationManager.AppSettings("DBKewUserID")
        End If
    End Sub
    Public Property ProcStatus As String
        Get
            Return status_
        End Get
        Set(value As String)
            status_ = value
        End Set
    End Property

    Public Function ReadDB(query As Object, Optional ByVal param As List(Of SqlParameter) = Nothing) As DataTable
        RunQueryDB(query, param, 0)
        Return dt
    End Function
    Private Sub RunQuery(query As Object, param As List(Of SqlParameter), type As CommandType)
        Try
            Using mycon As New SqlConnection()
                mycon.ConnectionString = strCon
                mycon.Open()
                dbcmd = New SqlCommand(query, mycon)
                dbcmd.CommandType = CommandType.Text
                If param IsNot Nothing Then
                    param.ForEach(Function(x)
                                      dbcmd.Parameters.Add(x)
                                  End Function)
                End If

                If type = 1 Then
                    If dbcmd.ExecuteNonQuery().Equals(1) Then
                        'dbcmd = New SqlCommand("SELECT SCOPE_IDENTITY();", mycon)
                        'current_id = dbcmd.ExecuteScalar()
                        ProcStatus = "OK"
                    Else
                        ProcStatus = "X"
                    End If
                ElseIf type = 0 Then
                    dt = New DataTable
                    dr = dbcmd.ExecuteReader
                    dt.Load(dr)
                    dr.Close()
                End If
                mycon.Close()
            End Using
        Catch ex As SqlException
            ProcStatus = ex.Message.ToString
        End Try
    End Sub

    Public Function Process(cmd As SqlCommand, ByVal param As List(Of SqlParameter), Optional cmdtype As CommandType = CommandType.Text) As Boolean
        Using mycon As New SqlConnection
            mycon.ConnectionString = strCon
            cmd.Connection = mycon
            cmd.CommandType = cmdtype
            mycon.Open()

            If cmd.ExecuteNonQuery() Then
                cmd_status = True
            End If
        End Using
        Return cmd_status
    End Function

    Public Function Process(query As String, ByVal param As List(Of SqlParameter)) As String
        RunQuery(query, param, 1)
        Return Status()
    End Function

    Public Function Read(query As Object, Optional ByVal param As List(Of SqlParameter) = Nothing) As DataTable
        RunQuery(query, param, 0)
        Return dt
    End Function

    Public Function Status() As String
        Return ProcStatus
    End Function
    ''' <summary>
    ''' Select sql to retrieve the velue of a specific column.
    ''' Please write one Column name in the sql string.
    ''' </summary>
    ''' <param name="strSQL">Get one column in the string</param>
    ''' <param name="returnValue">Datatype of column</param>
    Public Sub sSelectCommand(ByVal strSQL As String, ByRef returnValue As Object)
        Dim SQLReader As SqlDataReader = Nothing

        Try
            If dbConn.State = ConnectionState.Closed Then
                sCreateConnection()
            End If
            dbComm = New SqlCommand(strSQL, dbConn, dbSQLTxn)
            SQLReader = dbComm.ExecuteReader

            If SQLReader.HasRows Then
                While SQLReader.Read()
                    If TypeOf returnValue Is Integer Then
                        returnValue = SQLReader.GetInt32(0)
                    ElseIf TypeOf returnValue Is String Then
                        returnValue = SQLReader.GetString(0)
                    ElseIf TypeOf returnValue Is Decimal Then
                        returnValue = SQLReader.GetDecimal(0)
                    ElseIf TypeOf returnValue Is Double Then
                        returnValue = SQLReader.GetDouble(0)
                    ElseIf TypeOf returnValue Is DateTime Then
                        returnValue = SQLReader.GetDateTime(0)
                    ElseIf TypeOf returnValue Is Boolean Then
                        returnValue = SQLReader.GetBoolean(0)
                    ElseIf TypeOf returnValue Is Byte Then
                        returnValue = SQLReader.GetByte(0)
                    ElseIf TypeOf returnValue Is Single Then
                        returnValue = SQLReader.GetFloat(0)
                    ElseIf TypeOf returnValue Is Int64 Then
                        returnValue = SQLReader.GetInt64(0)
                    End If
                End While
            End If


        Catch ex As Exception
            Console.Write(ex.Message)


        Finally
            If SQLReader Is Nothing = False Then
                SQLReader.Close()
                SQLReader = Nothing
            End If

            sCloseConnection()
        End Try
    End Sub

    ''' <summary>
    ''' Select command to fill in Dataset to return dataset.
    ''' Applicable for single table select command if parameter isManyTable is not set to true.
    ''' Modified Hazrin on 15022018 to include the execution of BeginTransaction and MissingSchemaAction
    ''' If isManyTable is set to True, this function is applicable for many tables for update. 
    ''' But have to call function sConnBeginTrans() before calling this function. 
    ''' Modified Hazrin on 03042018
    ''' </summary>
    ''' <param name="strSQL"></param>
    ''' <param name="DatasetName"></param>
    ''' <param name="isManyTable">set to True</param>
    ''' <returns></returns>
    Public Function fSelectCommand(ByVal strSQL As String, ByVal DatasetName As String, Optional ByVal isManyTable As Boolean = False) As DataSet
        Dim dbDataset = New DataSet
        Try
            If dbConn.State = ConnectionState.Closed Then
                sCreateConnection()
            End If

            If Not isManyTable Then
                'start the transaction
                sConnBeginTrans()
            End If

            dbComm = New SqlCommand(strSQL, dbConn, dbSQLTxn)
            dbAdapter.SelectCommand = dbComm

            'dbAdapter.FillSchema(dbDataset, SchemaType.Source)

            Dim cmb As New SqlCommandBuilder(dbAdapter)

            ' generate updating logic for command objects
            cmb.GetDeleteCommand()
            cmb.GetInsertCommand()
            cmb.GetUpdateCommand()

            'MissingSchemaAction specifies the action to take when adding data 
            'to the DataSet and the required DataTable Or DataColumn Is missing. 
            'The AddWithKey option adds the necessary columns And primary key Information 
            'to complete the schema.
            dbAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey

            dbAdapter.Fill(dbDataset, DatasetName)
            dbDataset.DataSetName = DatasetName

            'associate transaction with command builder command objects
            cmb.GetDeleteCommand().Transaction = dbSQLTxn
            cmb.GetInsertCommand().Transaction = dbSQLTxn
            cmb.GetUpdateCommand().Transaction = dbSQLTxn
        Catch ex As Exception
            Console.Write(ex.Message)
        End Try

        Return dbDataset
    End Function

    ''' <summary>
    ''' Get the dataset by passing query string, dataset name and also pass the parameters.
    ''' Applicable for single table select command if parameter isManyTable is not set to true.
    ''' Modified Hazrin on 15022018 to include the execution of BeginTransaction and MissingSchemaAction
    ''' If isManyTable is set to True, this function is applicable for many tables for update. 
    ''' But have to call function sConnBeginTrans() before calling this function. 
    ''' Modified Hazrin on 03042018
    ''' </summary>
    ''' <param name="strSQL"></param>
    ''' <param name="DatasetName"></param>
    ''' <param name="param"></param>
    ''' <param name="isManyTable">set to True</param>
    ''' <returns>Dataset</returns>
    Public Function fSelectCommand(ByVal strSQL As String, ByVal DatasetName As String, ByVal param() As SqlParameter, Optional ByVal isManyTable As Boolean = False) As DataSet
        Dim dbDataset = New DataSet
        Try
            If dbConn.State = ConnectionState.Closed Then
                sCreateConnection()
            End If

            If Not isManyTable Then
                'start the transaction
                sConnBeginTrans()
            End If

            dbAdapter.SelectCommand = New SqlCommand(strSQL, dbConn, dbSQLTxn)
            dbDataset = New DataSet
            'dbAdapter.FillSchema(dbDataset, SchemaType.Source)

            If Not param Is Nothing Then
                For Each p In param
                    dbAdapter.SelectCommand.Parameters.Add(p)
                Next
            End If

            Dim cmb As New SqlCommandBuilder(dbAdapter)
            ' generate updating logic for command objects
            cmb.GetDeleteCommand()
            cmb.GetInsertCommand()
            cmb.GetUpdateCommand()

            'MissingSchemaAction specifies the action to take when adding data 
            'to the DataSet and the required DataTable Or DataColumn Is missing. 
            'The AddWithKey option adds the necessary columns And primary key Information 
            'to complete the schema.
            dbAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey

            dbAdapter.Fill(dbDataset, DatasetName)
            dbDataset.DataSetName = DatasetName

            'associate transaction with command builder command objects
            cmb.GetDeleteCommand().Transaction = dbSQLTxn
            cmb.GetInsertCommand().Transaction = dbSQLTxn
            cmb.GetUpdateCommand().Transaction = dbSQLTxn

            Return dbDataset
        Catch ex As Exception
            Console.Write(ex.Message)
        Finally
            dbAdapter.SelectCommand.Parameters.Clear()
        End Try
        Return dbDataset
    End Function

    '''Return 1 column value based On the query
    Public Function fSelectCount(ByVal strSQL As String) As Int32
        Dim ResultCnt As Int32
        Dim SQLReader As SqlDataReader = Nothing
        'Dim OdbcReader As OdbcDataReader = Nothing
        Try
            If dbConn.State = ConnectionState.Closed Then
                sCreateConnection()
            End If
            dbComm = New SqlCommand(strSQL, dbConn, dbSQLTxn)
            SQLReader = dbComm.ExecuteReader
            While SQLReader.Read()
                ResultCnt = SQLReader.GetInt32(0)
            End While

            'If OdbcReader Is Nothing = False Then
            '    OdbcReader.Close()
            '    OdbcReader = Nothing
            'End If
        Catch ex As Exception
            Console.Write(ex.Message)
            Return -1
        Finally
            If SQLReader IsNot Nothing Then
                SQLReader.Close()
                SQLReader = Nothing
            End If
            sCloseConnection()
        End Try
        Return ResultCnt
    End Function


    ''' <summary>
    ''' Execute select command and return dataset
    ''' </summary>
    ''' <param name="strSql">sql string</param>
    ''' <returns>dbDataset</returns>
    Public Function fSelectCommand(ByVal strSql As String) As DataSet
        Dim dbDataset = New DataSet
        Try
            If dbConn.State = ConnectionState.Closed Then
                sCreateConnection()
            End If

            dbComm = New SqlCommand(strSql, dbConn)
            dbComm.CommandTimeout = 600000
            dbAdapter.SelectCommand = dbComm

            dbAdapter.Fill(dbDataset)
        Catch ex As Exception
            Console.Write(ex.Message)
        Finally
            sCloseConnection()
        End Try

        Return dbDataset
    End Function

    ''' <summary>
    ''' Execute select command and return datatable
    ''' Created on 8/11/2018
    ''' </summary>
    ''' <param name="strSql"></param>
    ''' <returns>Datatable</returns>
    Public Function fSelectCommandDt(ByVal strSql As String) As DataTable
        Dim dbDatatable As New DataTable
        Try
            If dbConn.State = ConnectionState.Closed Then
                sCreateConnection()
            End If

            dbComm = New SqlCommand(strSql, dbConn)
            dbComm.CommandTimeout = 600000
            dbAdapter.SelectCommand = dbComm

            dbAdapter.Fill(dbDatatable)
        Catch ex As Exception
            Console.Write(ex.Message)
        Finally
            sCloseConnection()
        End Try

        Return dbDatatable
    End Function

    'for executing update statement by passing in a SQL statement
    Public Function fUpdateCommand(ByVal strSQL As String) As Long
        Dim intRec As Long
        Try

            If dbConn.State = ConnectionState.Closed Then
                sCreateConnection()
            End If

            dbComm = New SqlCommand(strSQL, dbConn, dbSQLTxn)

            intRec = dbComm.ExecuteNonQuery
            Return intRec
        Catch ex As Exception
            Console.Write(ex.Message)

            Return 0
        Finally
            'sCloseConnection()
        End Try
    End Function


    ''' <summary>
    ''' Update record to database using sqlcommand parameter to avoid sql injection
    ''' Hanafi,02554,26/12/2017, modified to suit commit rollback transaction 
    ''' </summary>
    ''' <param name="strSQL"></param>
    ''' <param name="param"></param>
    ''' <returns></returns>
    Public Function fUpdateCommand(ByVal strSQL As String, ByVal param() As SqlParameter) As Integer
        Dim intRec As Int32
        Try
            If dbConn.State = ConnectionState.Closed Then
                sCreateConnection()
            End If

            dbComm = New SqlCommand(strSQL, dbConn, dbSQLTxn)

            If Not param Is Nothing Then
                For Each p In param
                    dbComm.Parameters.Add(p)
                Next
            End If

            intRec = dbComm.ExecuteNonQuery
            Return intRec
        Catch ex As Exception
            Console.Write(ex.Message)
            Return 0
        Finally
            ' sCloseConnection()
        End Try
    End Function

    Public Sub sUpdateCommand(ByVal strSQL As String, ByVal param() As SqlParameter, Optional ByRef bResultCommit As Boolean = False)

        Try
            If dbConn.State = ConnectionState.Closed Then
                sCreateConnection()
            End If

            sConnBeginTrans()

            dbComm = New SqlCommand(strSQL, dbConn, dbSQLTxn)

            If Not param Is Nothing Then
                For Each p In param
                    dbComm.Parameters.Add(p)
                Next
            End If

            'submit changes, commit or rollback, and close the connection
            Dim intRec As Int32 = dbComm.ExecuteNonQuery

            If intRec > 0 Then
                'commit If successful
                sConnCommitTrans()
                bResultCommit = True
            Else
                'Restore if otherwise
                sConnRollbackTrans()
                bResultCommit = False
            End If


        Catch ex As Exception
            Console.Write(ex.Message)
        Finally
            dbComm.Parameters.Clear()
            sCloseConnection()
        End Try
    End Sub

    Public Sub sUpdateCommand2(ByVal strSQL As String, ByVal param() As SqlParameter, Optional ByVal manyTable As Boolean = False, Optional ByVal lastTable As Boolean = True, Optional ByRef bResultUpdate As Boolean = False, Optional ByRef bResultCommit As Boolean = False)

        Try
            If dbConn.State = ConnectionState.Closed Then
                sCreateConnection()
                sConnBeginTrans()
            End If

            If Not manyTable Then
                sConnBeginTrans()
            End If

            dbComm = New SqlCommand(strSQL, dbConn, dbSQLTxn)

            If Not param Is Nothing Then
                For Each p In param
                    dbComm.Parameters.Add(p)
                Next
            End If

            'submit changes, commit or rollback, and close the connection
            Dim intRec As Int32 = dbComm.ExecuteNonQuery
            If intRec > 0 Then
                bResultUpdate = True
                If lastTable Then
                    'commit If successful
                    sConnCommitTrans()
                    bResultCommit = True
                End If
            Else
                'Restore if otherwise
                sConnRollbackTrans()
                bResultCommit = False
                bResultUpdate = False
            End If

        Catch ex As Exception
            Console.Write(ex.Message)
        Finally
            If lastTable Then
                sCloseConnection()
            End If
        End Try
    End Sub


    ''' <summary>
    ''' For executing update statement by passing in a dataset.
    ''' SqldataAdapter can recognize Insert,Update,Delete Command by adding SqlCommandBuilder,
    ''' But it is only applicable for one table, not multiple tables.
    ''' Need to have Primary Key in order to UpdateCommand
    ''' Changed by: Hazrin 14/02/2018
    ''' Modified Hazrin on 15022018 to include the execution of Commit and rollback transaction and MissingSchemaAction
    ''' 
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <param name="strSQL"></param>
    ''' <param name="bResultUpdate">true if Update is successful</param>
    ''' <param name="lastTable">True if last or only one table is executing for update</param>
    ''' <param name="bResultCommit">True if commit transaction is true</param>
    Public Sub sUpdateCommand(ByVal ds As DataSet, ByVal strSQL As String, Optional ByRef bResultUpdate As Boolean = False, Optional ByVal lastTable As Boolean = True, Optional ByRef bResultCommit As Boolean = False)
        Try
            If dbConn.State = ConnectionState.Closed Then
                sCreateConnection()
            End If

            If ds.HasChanges Then
                Dim s = ds.DataSetName

                'submit changes, commit or rollback, and close the connection
                dbAdapter.Update(ds, s)
                bResultUpdate = True

                If lastTable Then
                    'commit If successful
                    sConnCommitTrans()
                    bResultCommit = True
                End If
            Else
                If lastTable Then
                    'commit the first dataset
                    sConnCommitTrans()
                    bResultCommit = True
                End If
            End If
        Catch ex As Exception
            Console.Write(ex.Message)

            'Restore if otherwise
            sConnRollbackTrans()
            bResultUpdate = False
            bResultCommit = False
        Finally
            If lastTable Then
                sCloseConnection()
            End If
        End Try
    End Sub

    Public Function fGetRunningID()
        Dim strSQL = "SELECT @@IDENTITY AS 'Identity'"
        dbComm = New SqlCommand(strSQL, dbConn, dbSQLTxn)
        Dim newID = CInt(dbComm.ExecuteScalar())
        Return newID
    End Function


    '''' <summary>
    '''' For executing update statement by passing in a dataset.
    '''' SqldataAdapter can recognize Insert,Update,Delete Command by adding SqlCommandBuilder,
    '''' But it is only applicable for one table, not multiple tables.
    '''' Need to have Primary Key in order to UpdateCommand
    '''' for executing update statement by passing in a dataset
    '''' pass dataset, string SQL statement, and parameter
    '''' Changed by: Hazrin 14/02/2018
    '''' Modified by Hazrin on 15022018 to include the execution of Commit and rollback transaction and MissingSchemaAction
    '''' </summary>
    '''' <param name="ds"></param>
    '''' <param name="strSQL"></param>
    'Public Sub sUpdateCommand(ByVal ds As DataSet, ByVal strSQL As String)
    '    Try
    '        If dbConn.State = ConnectionState.Closed Then
    '            sCreateConnection()
    '        End If

    '        If ds.HasChanges Then
    '            Dim s = ds.DataSetName

    '            'submit changes, commit or rollback, and close the connection
    '            dbAdapter.Update(ds, s)

    '            'commit If successful
    '            sConnCommitTrans()
    '        End If
    '    Catch ex As Exception
    '        Console.Write(ex.Message)

    '        'Restore if otherwise
    '        sConnRollbackTrans()
    '    Finally
    '        dbAdapter.SelectCommand.Parameters.Clear()
    '        sCloseConnection()
    '    End Try
    'End Sub

    'for executing insert statements by passing in a SQL statement
    Public Function fInsertCommand(ByVal strSQL As String, ByRef NewID As Long) As Integer
        Dim intRec As Integer
        Dim dbDataset As DataSet
        'Dim sqlReader As SqlDataReader
        'Dim odbcReader As OdbcDataReader
        Try
            'If dbConn.State = ConnectionState.Closed Then
            '    sCreateConnection()
            'End If
            'sConnBeginTrans()
            dbComm = New SqlCommand(strSQL, dbConn, dbSQLTxn)
            intRec = dbComm.ExecuteNonQuery
            strSQL = "SELECT @@IDENTITY AS 'Identity'"
            'dbComm.CommandText = strSQL
            'If strDBConnType = "SQL" Then
            '    dbComm.CommandText = strSQL
            '    dbComm = New SqlCommand(strSQL, dbConn, dbSQLTxn)
            '    sqlReader = dbComm.ExecuteReader()
            '    sqlReader.Read()
            '    NewID = CLng(sqlReader("Identity"))
            'ElseIf strDBConnType = "ODBC" Then
            '    dbComm = New OdbcCommand(strSQL, dbConn, dbODBCTxn)
            '    odbcReader = dbComm.ExecuteReader()
            '    odbcReader.Read()
            '    NewID = CLng(odbcReader("Identity"))
            'End If
            dbComm = New SqlCommand(strSQL, dbConn, dbSQLTxn)
            dbAdapter.SelectCommand = dbComm
            dbDataset = New DataSet
            dbAdapter.Fill(dbDataset, "Tbl")
            dbDataset.DataSetName = "Tbl"
            NewID = dbDataset.Tables("Tbl").Rows(0)("Identity")
            Return intRec
        Catch ex As Exception
            Console.Write(ex.Message)
            Return 0
        Finally
            'sCloseConnection()
        End Try
    End Function

    Public Function fInsertCommand(ByVal strSQL As String) As Integer
        Dim intRec As Integer
        Try
            If dbConn.State = ConnectionState.Closed Then
                sCreateConnection()
            End If
            'sConnBeginTrans()
            dbComm = New SqlCommand(strSQL, dbConn, dbSQLTxn)
            intRec = dbComm.ExecuteNonQuery
            Return intRec
        Catch ex As Exception
            Console.Write(ex.Message)
            Return 0
        Finally
            'sCloseConnection()
        End Try
    End Function

    ''' <summary>
    ''' Insert record to database using sqlcommand parameter to avoid sql injection.
    ''' Return count of record as integer.
    ''' </summary>
    ''' <param name="strSQL"></param>
    ''' <param name="param"></param>
    ''' <returns>count of record </returns>
    Public Function fInsertCommand(ByVal strSQL As String, ByVal param() As SqlParameter) As Integer
        Dim intRec As Integer
        Try
            If dbConn.State = ConnectionState.Closed Then
                sCreateConnection()
            End If

            dbComm = New SqlCommand(strSQL, dbConn, dbSQLTxn)

            If Not param Is Nothing Then
                For Each p In param
                    dbComm.Parameters.Add(p)
                Next
            End If

            intRec = dbComm.ExecuteNonQuery
            Return intRec

        Catch ex As Exception
            fErrorLog("fInsertCommand - Err: " & ex.Message.ToString)
            Return 0
        Finally
            'sCloseConnection()
        End Try
    End Function


    Public Function fInsertCommand(ByVal strSQL As String, ByVal param() As SqlParameter, ByRef newid As Integer) As Integer
        Dim intRec As Integer
        Try
            If dbConn.State = ConnectionState.Closed Then
                sCreateConnection()
            End If

            dbComm = New SqlCommand(strSQL, dbConn, dbSQLTxn)

            If Not param Is Nothing Then
                For Each p In param
                    dbComm.Parameters.Add(p)
                Next
            End If

            intRec = dbComm.ExecuteNonQuery

            strSQL = "SELECT @@IDENTITY AS 'Identity'"

            dbComm = New SqlCommand(strSQL, dbConn, dbSQLTxn)
            dbAdapter.SelectCommand = dbComm
            Dim dbDataset = New DataSet
            dbAdapter.Fill(dbDataset, "Tbl")
            dbDataset.DataSetName = "Tbl"
            newid = dbDataset.Tables("Tbl").Rows(0)("Identity")
            Return intRec

        Catch ex As Exception
            fErrorLog(ex.Message)
            Console.Write(ex.Message)
            Return 0
        Finally
            'sCloseConnection()
        End Try
    End Function

    Public Function fInsertBulk(ByVal strTable As String, dt As DataTable) As Boolean
        Try
            Using sqlConn As SqlConnection = New SqlConnection(ConnectionString)
                Using bulkCopy As SqlBulkCopy = New SqlBulkCopy(sqlConn)
                    sqlConn.Open()
                    bulkCopy.DestinationTableName = "dbo.TMP_Bina_Akaun"
                    Try
                        bulkCopy.WriteToServer(dt)
                    Catch ex As Exception
                        sqlConn.Close()
                    End Try
                End Using
                sqlConn.Close()
            End Using

        Catch ex As Exception

        Finally

        End Try


        Try
            Using sqlConn As New SqlConnection(ConnectionString)
                Try
                    sqlConn.Open()
                    Using sqlTransaction As SqlTransaction = sqlConn.BeginTransaction()
                        Using sqlBulkCopy As New SqlBulkCopy(sqlConn, SqlBulkCopyOptions.Default, sqlTransaction)
                            sqlBulkCopy.DestinationTableName = strTable

                            Try
                                sqlBulkCopy.WriteToServer(dt)
                                sqlTransaction.Commit()
                                Return True
                            Catch
                                sqlTransaction.Rollback()
                                sqlConn.Close()
                                Return False
                            End Try
                        End Using
                    End Using
                    sqlConn.Close()
                Catch ex As Exception
                    sqlConn.Close()
                End Try

            End Using
        Catch ex As Exception

        End Try
    End Function


    ''' <summary>
    ''' Hanafi,02554,26/12/2017 
    ''' executes stored procedure update transaction
    ''' </summary>
    ''' <param name="SPName">nama stored proc</param>
    ''' <param name="param">parameter collection</param>
    ''' <returns></returns>
    Public Function fSP_Update(ByVal SPName As String, ByVal param() As SqlParameter) As Integer
        Dim intRec As Long
        Dim ExeSP As SqlCommand

        Try
            ExeSP = New SqlCommand
            ExeSP.CommandType = CommandType.StoredProcedure
            ExeSP.CommandText = SPName
            ExeSP.Connection = dbConn
            If Not param Is Nothing Then
                For Each p In param
                    ExeSP.Parameters.Add(p)
                Next
            End If
            intRec = ExeSP.ExecuteNonQuery()
            ' NewID = ExeSP.Parameters("@NEWPROFILEID").Value

            Return intRec
        Catch ex As Exception
            Console.Write(ex.Message)
            Return 0
        Finally
            'sCloseConnection()
        End Try
    End Function


    ''' <summary>
    ''' Hanafi,02554,26/12/2017 
    ''' executes stored procedure insert transaction
    ''' </summary>
    ''' <param name="SPName">Stored Proc name</param>
    ''' <param name="param">parameter collection</param>
    ''' <returns></returns>
    Public Function fSP_Insert(ByVal SPName As String, ByVal param() As SqlParameter, ByRef strOutput As String) As Integer
        'to hazrin, if nak modify function tlg bagitau aku -napi

        Dim intRec As Long
        Dim ExeSP As SqlCommand
        Try
            ExeSP = New SqlCommand
            ExeSP.CommandType = CommandType.StoredProcedure
            ExeSP.CommandText = SPName
            ExeSP.Connection = dbConn
            If Not param Is Nothing Then
                For Each p In param
                    ExeSP.Parameters.Add(p)
                Next
            End If
            ExeSP.Parameters("@Err").Direction = ParameterDirection.Output
            ExeSP.CommandTimeout = 0
            intRec = ExeSP.ExecuteNonQuery()
            strOutput = ExeSP.Parameters("@Err").Value.ToString
            Return intRec
        Catch ex As Exception
            Console.Write(ex.Message)
            Return 0
        Finally
            'sCloseConnection()
        End Try
    End Function

    ''' <summary>
    ''' Execute Store Procedures and get one of output parameters.
    ''' </summary>
    ''' <param name="SPName">Store Precedure's Name</param>
    ''' <param name="param"> Collection of parameters</param>
    ''' <param name="ParamOutput"> Parameter that has direction Output/Return</param>
    ''' <param name="strOutput"> Get the Output/Return Value</param>
    ''' <returns></returns>
    Public Function fExecuteSP(ByVal SPName As String, ByVal param() As SqlParameter, ByVal ParamOutput As SqlParameter, ByRef strOutput As String) As Integer
        Dim intRec As Long
        Dim ExeSP As New SqlCommand
        Try
            ExeSP.CommandType = CommandType.StoredProcedure
            ExeSP.CommandText = SPName
            ExeSP.Connection = dbConn

            Dim lastIndex = param.Length - 1
            If Not param Is Nothing Then
                For Each p In param
                    ExeSP.Parameters.Add(p)
                Next
            End If

            ExeSP.CommandTimeout = 0
            intRec = ExeSP.ExecuteNonQuery()
            strOutput = ParamOutput.Value.ToString
            Return intRec
        Catch ex As Exception
            Console.Write(ex.Message)
            Return 0
        Finally
            'sCloseConnection()
        End Try
    End Function


    'to create the database connection
    Private Sub sCreateConnection()
        Try
            dbConn = New SqlConnection
            dbAdapter = New SqlDataAdapter

            'dbSQLTxn = New SqlTransaction

            dbConn.ConnectionString = ConnectionString '"DSN=KJTEST; UID=sa; PWD=1293; persist security info=False;"
            dbConn.Open()

        Catch ex As Exception
            Console.Write(ex.Message)

        End Try
    End Sub

    'to test the connection if is open or not
    Public Function fChkDBConn() As Boolean
        'Try
        If dbConn.State = ConnectionState.Open Then
            Return True
        Else
            Return False
        End If
        'Catch ex As Exception
        'Console.Write(ex.Message)

        'End Try
    End Function

    'to close the database connection
    Public Sub sCloseConnection()
        Try
            If Not dbAdapter Is Nothing Then
                dbAdapter.Dispose()
            End If
            If Not dbComm Is Nothing Then
                dbComm.Dispose()
            End If
            'If Not dbConn Is Nothing Then
            '    dbConn.Close()
            'End If
            If dbConn.State = ConnectionState.Open Then
                dbConn.Close()
            End If
        Catch ex As Exception
            Console.Write(ex.Message)

        End Try
    End Sub

    'Initialize the Connection String based on the DB Type
    Private Sub sInitializeConnStr()
        Dim strDeDBPassword
        Dim clsCrypto As New clsCrypto
        strDeDBPassword = clsCrypto.fDecrypt(strDBPassword)

        ConnectionString = "Server=" & strDbServer & ";Database=" & strDbName & ";Uid=" & strDBUser & ";Pwd=" & strDeDBPassword & ";Pooling=False;"

    End Sub


    'Begin the database transaction
    Public Sub sConnBeginTrans()
        Try
            dbSQLTxn = dbConn.BeginTransaction()

        Catch ex As Exception
            Console.Write(ex.Message)

        End Try
    End Sub

    'Commit the database transaction
    Public Sub sConnCommitTrans()
        Try
            If Not dbSQLTxn Is Nothing Then
                dbSQLTxn.Commit()
            End If

        Catch ex As Exception
            Console.Write(ex.Message)

        End Try
    End Sub

    'Commit the database transaction
    Public Sub sConnRollbackTrans()
        Try
            dbSQLTxn.Rollback()

        Catch ex As Exception
            Console.Write(ex.Message)

        End Try
    End Sub

    ''' <summary>
    ''' Select Statement 'Parameteried Query
    ''' </summary>
    ''' <param name="strSql"></param>
    ''' <param name="param"></param>
    ''' <returns></returns>
    Public Function fSelectCommand(ByVal strSql As String, ByVal param() As SqlParameter) As DataSet
        Try
            If dbConn.State = ConnectionState.Closed Then
                sCreateConnection()
            End If

            dbComm = New SqlCommand(strSql, dbConn, dbSQLTxn)

            If Not param Is Nothing Then
                For Each p In param
                    dbComm.Parameters.Add(p)
                Next
            End If

            dbComm.CommandTimeout = 600000
            dbAdapter.SelectCommand = dbComm

            Dim dbDataset = New DataSet
            dbAdapter.Fill(dbDataset)

            If Not dbDataset Is Nothing Then
                Return dbDataset
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Console.Write(ex.Message)
            Return Nothing
        Finally
            sCloseConnection()
        End Try
    End Function

    'AIDA 10/02/2020
    ''' <summary>
    ''' Select command to fill in Dataset to return dataset.
    ''' Applicable for single table select command if parameter isManyTable is not set to true.
    ''' Modified Hazrin on 15022018 to include the execution of BeginTransaction and MissingSchemaAction
    ''' If isManyTable is set to True, this function is applicable for many tables for update. 
    ''' But have to call function sConnBeginTrans() before calling this function. 
    ''' Modified Hazrin on 03042018
    ''' </summary>
    ''' <param name="strSQL"></param>
    ''' <param name="isFirstTable">set to True</param>
    ''' <returns></returns>
    Public Function fSelectCommandDs(ByVal strSQL As String, ByVal isFirstTable As Boolean) As DataSet

        If dbConn.State = ConnectionState.Closed Then
            sCreateConnection()
        End If

        If isFirstTable Then
            'start the transaction
            dbSQLTxn = dbConn.BeginTransaction()
        End If

        Using dbComm = New SqlCommand(strSQL, dbConn, dbSQLTxn)
            dbComm.CommandTimeout = 600000
            dbAdapter.SelectCommand = dbComm
            Dim dbDataset = New DataSet
            Dim cmb As New SqlCommandBuilder(dbAdapter)
            ' generate updating logic for command objects
            cmb.GetDeleteCommand()
            cmb.GetInsertCommand()
            cmb.GetUpdateCommand()

            'MissingSchemaAction specifies the action to take when adding data 
            'to the DataSet and the required DataTable Or DataColumn Is missing. 
            'The AddWithKey option adds the necessary columns And primary key Information 
            'to complete the schema.
            dbAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey

            dbAdapter.Fill(dbDataset)

            'associate transaction with command builder command objects
            cmb.GetDeleteCommand().Transaction = dbSQLTxn
            cmb.GetInsertCommand().Transaction = dbSQLTxn
            cmb.GetUpdateCommand().Transaction = dbSQLTxn

            Return dbDataset
        End Using
    End Function

    ''' <summary>
    ''' Execute select command and return dataset
    ''' </summary>
    ''' <param name="strSql">sql string</param>
    ''' <returns>dbDataset</returns>
    Public Function fselectCommandDs(ByVal strSql As String) As DataSet
        Using dbconn = New SqlConnection(ConnectionString)
            dbconn.Open()
            Using dbComm = New SqlCommand(strSql, dbconn)
                dbComm.CommandTimeout = 600000
                Using dbAdapter = New SqlDataAdapter(dbComm)
                    dbComm.CommandTimeout = 600000
                    Dim dbDataset As New DataSet
                    dbAdapter.Fill(dbDataset)
                    Return dbDataset
                End Using
            End Using
        End Using
    End Function

    'Edit By Arif
    ''' <summary>
    ''' Select command to fill in Dataset to return dataset.
    ''' Applicable for single table select command if parameter isManyTable is not set to true.
    ''' Modified Hazrin on 15022018 to include the execution of BeginTransaction and MissingSchemaAction
    ''' If isManyTable is set to True, this function is applicable for many tables for update. 
    ''' But have to call function sConnBeginTrans() before calling this function. 
    ''' Modified Hazrin on 03042018
    ''' </summary>
    ''' <param name="strSQL"></param>
    ''' <param name="isFirstTable">set to True</param>
    ''' <returns></returns>
    Public Function fSelectCommandDs1(ByVal strSQL As String, ByVal isFirstTable As Boolean) As DataSet

        If dbConn.State = ConnectionState.Closed Then
            sCreateConnection()
        End If

        If isFirstTable Then
            'start the transaction
            dbSQLTxn = dbConn.BeginTransaction()
        End If

        Using dbComm = New SqlCommand(strSQL, dbConn, dbSQLTxn)
            dbComm.CommandTimeout = 600000
            dbAdapter.SelectCommand = dbComm
            Dim dbDataset = New DataSet
            Dim cmb As New SqlCommandBuilder(dbAdapter)
            ' generate updating logic for command objects
            cmb.GetDeleteCommand()
            cmb.GetInsertCommand()
            cmb.GetUpdateCommand()

            'MissingSchemaAction specifies the action to take when adding data 
            'to the DataSet and the required DataTable Or DataColumn Is missing. 
            'The AddWithKey option adds the necessary columns And primary key Information 
            'to complete the schema.
            dbAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey

            dbAdapter.Fill(dbDataset)

            'associate transaction with command builder command objects
            cmb.GetDeleteCommand().Transaction = dbSQLTxn
            cmb.GetInsertCommand().Transaction = dbSQLTxn
            cmb.GetUpdateCommand().Transaction = dbSQLTxn
            dbConn.Close()

            Return dbDataset
        End Using

    End Function

    'Edit By Arif
    Public Function fselectCommandDt1(ByVal strSql As String) As DataTable
        Using dbconn = New SqlConnection(ConnectionString)
            dbconn.Open()
            Using dbComm = New SqlCommand(strSql, dbconn)
                dbComm.CommandTimeout = 600000
                Using dbAdapter = New SqlDataAdapter(dbComm)
                    Dim dbDatatable As New DataTable
                    dbAdapter.Fill(dbDatatable)
                    Return dbDatatable
                End Using
            End Using
            dbconn.Close()
        End Using
    End Function

    ''' <summary>
    ''' For executing update statement by passing in a dataset.
    ''' SqldataAdapter can recognize Insert,Update,Delete Command by adding SqlCommandBuilder,
    ''' But it is only applicable for one table, not multiple tables.
    ''' Need to have Primary Key in order to UpdateCommand
    ''' for executing update statement by passing in a dataset
    ''' pass dataset, string SQL statement, and parameter
    ''' Changed by: Hazrin 14/02/2018
    ''' Modified by Hazrin on 15022018 to include the execution of Commit and rollback transaction and MissingSchemaAction
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <param name="bResultUpdate">true if Update is successful</param>
    ''' <param name="lastTable">True if last or only one table is executing for update</param>
    ''' <param name="bResultCommit">True if commit transaction is true</param>
    Public Sub sUpdateCommandDs(ByVal ds As DataSet, Optional ByRef bResultUpdate As Boolean = False, Optional ByVal lastTable As Boolean = True, Optional ByRef bResultCommit As Boolean = False)
        Try
            If ds.HasChanges Then
                'submit changes, commit or rollback, and close the connection
                dbAdapter.Update(ds)
                bResultUpdate = True
            End If

            If lastTable Then
                'commit If successful
                dbSQLTxn.Commit()
                bResultCommit = True
            End If
        Catch ex As Exception
            'Restore if otherwise
            dbSQLTxn.Rollback()
            bResultUpdate = False
            bResultCommit = False
        Finally
            If lastTable Then
                dbConn.Close()
                dbAdapter.Dispose()
                dbConn.Dispose()
                dbSQLTxn.Dispose()
            End If
        End Try
    End Sub

    'Edit By Arif
    ''' <summary>
    ''' For executing update statement by passing in a dataset.
    ''' SqldataAdapter can recognize Insert,Update,Delete Command by adding SqlCommandBuilder,
    ''' But it is only applicable for one table, not multiple tables.
    ''' Need to have Primary Key in order to UpdateCommand
    ''' for executing update statement by passing in a dataset
    ''' pass dataset, string SQL statement, and parameter
    ''' Changed by: Hazrin 14/02/2018
    ''' Modified by Hazrin on 15022018 to include the execution of Commit and rollback transaction and MissingSchemaAction
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <param name="bResultUpdate">true if Update is successful</param>
    ''' <param name="lastTable">True if last or only one table is executing for update</param>
    ''' <param name="bResultCommit">True if commit transaction is true</param>
    Public Sub sUpdateCommandDs1(ByVal ds As DataSet, Optional ByRef bResultUpdate As Boolean = False, Optional ByVal lastTable As Boolean = True, Optional ByRef bResultCommit As Boolean = False)
        Try

            If dbConn.State = ConnectionState.Closed Then
                sCreateConnection()
            End If

            If ds.HasChanges Then
                'submit changes, commit or rollback, and close the connection
                dbSQLTxn = dbConn.BeginTransaction()
                dbAdapter.Update(ds)
                bResultUpdate = True
            End If

            If lastTable Then
                'commit If successful
                dbSQLTxn.Commit()
                bResultCommit = True
            End If
        Catch ex As Exception
            'Restore if otherwise
            dbSQLTxn.Rollback()
            bResultUpdate = False
            bResultCommit = False
        Finally
            If lastTable Then
                dbConn.Close()
                dbAdapter.Dispose()
                dbConn.Dispose()
                dbSQLTxn.Dispose()
            Else
                dbConn.Close()
            End If
            'dbSQLTxn.Commit()

        End Try
    End Sub

    Public Sub sUpdateCommandDt(ByVal dt As DataTable, Optional ByRef bResultUpdate As Boolean = False, Optional ByVal lastTable As Boolean = True, Optional ByRef bResultCommit As Boolean = False)

        'submit changes, commit or rollback, and close the connection
        Try
            dbAdapter.Update(dt)
            bResultUpdate = True

            If lastTable Then
                'commit If successful
                dbSQLTxn.Commit()
                bResultCommit = True
            End If

        Catch ex As Exception
            'Restore if otherwise
            dbSQLTxn.Rollback()
            bResultUpdate = False
            bResultCommit = False
        Finally
            If lastTable Then
                dbConn.Close()
                dbAdapter.Dispose()
                dbConn.Dispose()
                dbSQLTxn.Dispose()
            End If
        End Try
    End Sub

    'Edit By Arif
    Public Sub sUpdateCommandDt1(ByVal dt As DataTable, Optional ByRef bResultUpdate As Boolean = False, Optional ByVal lastTable As Boolean = True, Optional ByRef bResultCommit As Boolean = False)

        'submit changes, commit or rollback, and close the connection
        Try
            If dbConn.State = ConnectionState.Closed Then
                sCreateConnection()
            End If
            dbAdapter.Update(dt)
            bResultUpdate = True

            If lastTable Then
                'commit If successful
                dbSQLTxn = dbConn.BeginTransaction()
                dbSQLTxn.Commit()
                bResultCommit = True
            End If

        Catch ex As Exception
            Console.Write(ex.Message)
            'Restore if otherwise
            dbSQLTxn.Rollback()
            bResultUpdate = False
            bResultCommit = False
        Finally
            If lastTable Then
                dbConn.Close()
                dbAdapter.Dispose()
                dbConn.Dispose()
                dbSQLTxn.Dispose()
            Else
                dbConn.Close()
            End If

        End Try
    End Sub

    Public Sub sUpdateCommandNonQuery(ByVal strSQL As String, ByVal param() As SqlParameter, Optional ByRef rCommit As Boolean = False)
        Using dbconn = New SqlConnection(ConnectionString)
            dbconn.Open()
            Using dbComm = New SqlCommand(strSQL, dbconn)
                dbComm.CommandTimeout = 600000
                If Not param Is Nothing Then
                    For Each p In param
                        dbComm.Parameters.Add(p)
                    Next
                End If

                'submit changes, commit or rollback, and close the connection
                Dim intRec As Int32 = dbComm.ExecuteNonQuery
                If intRec > 0 Then
                    rCommit = True
                Else
                    rCommit = False
                End If
                'dbComm.Parameters.Clear()
            End Using
        End Using
    End Sub

    'Edit By Arif
    Public Sub sUpdateCommandNonQuery1(ByVal strSQL As String, ByVal param() As SqlParameter, Optional ByVal isFirstTable As Boolean = True, Optional ByVal lastTable As Boolean = True, Optional ByRef bResultUpdate As Boolean = False, Optional ByRef bResultCommit As Boolean = False)
        Try
            If dbConn.State = ConnectionState.Closed Then
                sCreateConnection()
            End If

            'If lastTable Then
            dbSQLTxn = dbConn.BeginTransaction()
            'End If

            Using dbComm = New SqlCommand(strSQL, dbConn, dbSQLTxn)
                dbComm.CommandTimeout = 600000

                If Not param Is Nothing Then
                    For Each p In param
                        dbComm.Parameters.Add(p)
                    Next
                End If

                'submit changes, commit or rollback, and close the connection
                Dim intRec As Int32 = dbComm.ExecuteNonQuery
                If intRec > 0 Then
                    bResultUpdate = True
                    If lastTable Then
                        'commit If successful
                        dbSQLTxn.Commit()
                        bResultCommit = True
                    End If
                Else
                    'Restore if otherwise
                    dbSQLTxn.Rollback()
                    bResultCommit = False
                    bResultUpdate = False
                End If
                'dbComm.Parameters.Clear()
            End Using
        Catch ex As Exception
            Console.Write(ex.Message)
            'fErrorLog("fUpdateCommand - " & ex.Message & " - " & strSQL)
        Finally
            If lastTable Then
                dbConn.Close()
                dbAdapter.Dispose()
                dbConn.Dispose()
                dbSQLTxn.Dispose()
            Else
                dbConn.Close()
            End If

        End Try
    End Sub

    Public Function fSelectCommandDt(ByVal strSQL As String, ByVal isFirstTable As Boolean, ByVal param() As SqlParameter) As DataTable
        If dbConn.State = ConnectionState.Closed Then
            sCreateConnection()
        End If

        If isFirstTable Then
            'start the transaction
            dbSQLTxn = dbConn.BeginTransaction()
        End If

        Using dbComm = New SqlCommand(strSQL, dbConn, dbSQLTxn)
            dbComm.CommandTimeout = 600000
            dbAdapter.SelectCommand = dbComm

            If Not param Is Nothing Then
                For Each p In param
                    dbAdapter.SelectCommand.Parameters.Add(p)
                Next
            End If
            'dbAdapter.FillSchema(dbDataset, SchemaType.Source)

            Dim cmb As New SqlCommandBuilder(dbAdapter)
            ' generate updating logic for command objects
            cmb.GetDeleteCommand()
            cmb.GetInsertCommand()
            cmb.GetUpdateCommand()

            'MissingSchemaAction specifies the action to take when adding data 
            'to the DataSet and the required DataTable Or DataColumn Is missing. 
            'The AddWithKey option adds the necessary columns And primary key Information 
            'to complete the schema.
            dbAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey
            Dim dbDatatable As New DataTable
            dbAdapter.Fill(dbDatatable)

            'associate transaction with command builder command objects
            cmb.GetDeleteCommand().Transaction = dbSQLTxn
            cmb.GetInsertCommand().Transaction = dbSQLTxn
            cmb.GetUpdateCommand().Transaction = dbSQLTxn
            Return dbDatatable
        End Using
    End Function

    'Edit By Arif
    Public Function fSelectCommandDt(ByVal strSQL As String, ByVal isFirstTable As Boolean) As DataTable
        If dbConn.State = ConnectionState.Closed Then
            sCreateConnection()
        End If

        If isFirstTable Then
            'start the transaction
            dbSQLTxn = dbConn.BeginTransaction()
        End If

        Using dbComm = New SqlCommand(strSQL, dbConn, dbSQLTxn)
            dbComm.CommandTimeout = 600000
            dbAdapter.SelectCommand = dbComm
            Dim dbDatatable = New DataTable
            Dim cmb As New SqlCommandBuilder(dbAdapter)
            ' generate updating logic for command objects
            cmb.GetDeleteCommand()
            cmb.GetInsertCommand()
            cmb.GetUpdateCommand()

            'MissingSchemaAction specifies the action to take when adding data 
            'to the DataSet and the required DataTable Or DataColumn Is missing. 
            'The AddWithKey option adds the necessary columns And primary key Information 
            'to complete the schema.
            dbAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey
            dbAdapter.Fill(dbDatatable)

            'associate transaction with command builder command objects
            cmb.GetDeleteCommand().Transaction = dbSQLTxn
            cmb.GetInsertCommand().Transaction = dbSQLTxn
            cmb.GetUpdateCommand().Transaction = dbSQLTxn
            dbConn.Close()
            Return dbDatatable
        End Using
    End Function

    Public Sub sUpdateCommandNonQuery(ByVal strSQL As String, ByVal param() As SqlParameter, Optional ByVal isFirstTable As Boolean = True, Optional ByVal lastTable As Boolean = True, Optional ByRef bResultUpdate As Boolean = False, Optional ByRef bResultCommit As Boolean = False)
        Try
            If dbConn.State = ConnectionState.Closed Then
                sCreateConnection()
            End If

            If isFirstTable Then
                dbSQLTxn = dbConn.BeginTransaction()
            End If

            Using dbComm = New SqlCommand(strSQL, dbConn, dbSQLTxn)
                dbComm.CommandTimeout = 600000

                If Not param Is Nothing Then
                    For Each p In param
                        dbComm.Parameters.Add(p)
                    Next
                End If

                'submit changes, commit or rollback, and close the connection
                Dim intRec As Int32 = dbComm.ExecuteNonQuery
                If intRec > 0 Then
                    bResultUpdate = True
                    If lastTable Then
                        'commit If successful
                        dbSQLTxn.Commit()
                        bResultCommit = True
                    End If
                Else
                    'Restore if otherwise
                    dbSQLTxn.Rollback()
                    bResultCommit = False
                    bResultUpdate = False
                End If
                'dbComm.Parameters.Clear()
            End Using
        Catch ex As Exception
            Console.Write(ex.Message)
            'fErrorLog("fUpdateCommand - " & ex.Message & " - " & strSQL)
        Finally
            If lastTable Then
                dbConn.Close()
                dbAdapter.Dispose()
                dbConn.Dispose()
                dbSQLTxn.Dispose()
            End If
        End Try
    End Sub

    Private Sub RunQueryDB(query As Object, param As List(Of SqlParameter), type As CommandType)
        Try
            Using mycon As New SqlConnection()
                mycon.ConnectionString = ConnectionString
                mycon.Open()
                dbComm = New SqlCommand(query, mycon)
                dbComm.CommandType = CommandType.Text
                If param IsNot Nothing Then
                    param.ForEach(Function(x)
                                      dbComm.Parameters.Add(x)
                                  End Function)
                End If

                If type = 1 Then
                    If dbComm.ExecuteNonQuery().Equals(1) Then
                        'dbcmd = New SqlCommand("SELECT SCOPE_IDENTITY();", mycon)
                        'current_id = dbcmd.ExecuteScalar()
                        ProcStatus = "OK"
                    Else
                        ProcStatus = "X"
                    End If
                ElseIf type = 0 Then
                    dt = New DataTable
                    dr = dbComm.ExecuteReader
                    dt.Load(dr)
                    dr.Close()
                End If
                mycon.Close()
            End Using
        Catch ex As SqlException
            ProcStatus = ex.Message.ToString
        End Try
    End Sub
End Class




