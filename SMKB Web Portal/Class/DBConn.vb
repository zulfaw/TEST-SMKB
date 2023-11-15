Imports System.Data.Odbc
Imports System.Data.SqlClient
Imports System.Web.Configuration

Public Class DBConn

#Region "Declares"
    Private dbConn As New SqlConnection
    Private dbAdapter As New SqlDataAdapter
    Private dbSQLTxn As SqlTransaction
    Public ConnectionString As String
    Private dbComm As SqlCommand

#End Region

    Public Sub New(dbName As String)
        sReadConfig(dbName)
        sInitializeConnStr()
    End Sub

    Public Sub sReadConfig(dbName As String)
        Select Case dbName
            Case "DBKew"
                strDbServer = WebConfigurationManager.AppSettings("DBKewServerName")
                strDbName = WebConfigurationManager.AppSettings("DBKewName")
                strDBPassword = WebConfigurationManager.AppSettings("DBKewPassword")
                strDBUser = WebConfigurationManager.AppSettings("DBKewUserID")
            Case "DBCLM"
                strDbServer = WebConfigurationManager.AppSettings("DBClmServerName")
                strDbName = WebConfigurationManager.AppSettings("DBClmName")
                strDBPassword = WebConfigurationManager.AppSettings("DBClmPassword")
                strDBUser = WebConfigurationManager.AppSettings("DBClmUserID")
            Case "DBSMSM"
                strDbServer = WebConfigurationManager.AppSettings("DBSMServerName")
                strDbName = WebConfigurationManager.AppSettings("DBSMName")
                strDBUser = WebConfigurationManager.AppSettings("DBSMUserID")
                strDBPassword = WebConfigurationManager.AppSettings("DBSMPassword")
            Case "DBVen"
                strDbServer = WebConfigurationManager.AppSettings("DBVenServerName")
                strDbName = WebConfigurationManager.AppSettings("DBVenName")
                strDBPassword = WebConfigurationManager.AppSettings("DBVenPassword")
                strDBUser = WebConfigurationManager.AppSettings("DBVenUserID")
        End Select
    End Sub

    ''' <summary>
    ''' Get datatable by executing command reader. Very fast to retrieve the data compare using SqlDataAdapter.
    ''' The datatable is used for display only.
    ''' </summary>
    ''' <param name="strSQL"></param>
    ''' <returns></returns>
    Public Function fSelectCommandReaderDt(ByVal strSQL As String) As DataTable
        Using dbconn = New SqlConnection(ConnectionString)
            dbconn.Open()
            Using dbComm = New SqlCommand(strSQL, dbconn)
                dbComm.CommandTimeout = 600000
                Dim dbDatatable = New DataTable
                Using SQLReader = dbComm.ExecuteReader
                    If SQLReader.HasRows Then
                        dbDatatable.Load(SQLReader)
                    End If
                End Using
                Return dbDatatable
            End Using
        End Using
    End Function

    '''' <summary>
    '''' Select sql to retrieve the velue of a specific column.
    '''' Please write one Column name in the sql string.
    '''' </summary>
    '''' <param name="strSQL">Get one column in the string</param>
    '''' <param name="returnValue">Datatype of column</param>
    'Public Sub sSelectCommand(ByVal strSQL As String, ByRef returnValue As Object)
    '    Using dbconn = New SqlConnection(ConnectionString)
    '        dbconn.Open()
    '        Using dbComm = New SqlCommand(strSQL, dbconn)
    '            dbComm.CommandTimeout = 600000

    '            Using SQLReader = dbComm.ExecuteReader
    '                If SQLReader.HasRows Then
    '                    While SQLReader.Read()
    '                        If TypeOf returnValue Is Integer Then
    '                            returnValue = SQLReader.GetInt32(0)
    '                        ElseIf TypeOf returnValue Is String Then
    '                            returnValue = SQLReader.GetString(0)
    '                        ElseIf TypeOf returnValue Is Decimal Then
    '                            returnValue = SQLReader.GetDecimal(0)
    '                        ElseIf TypeOf returnValue Is Double Then
    '                            returnValue = SQLReader.GetDouble(0)
    '                        ElseIf TypeOf returnValue Is DateTime Then
    '                            returnValue = SQLReader.GetDateTime(0)
    '                        ElseIf TypeOf returnValue Is Boolean Then
    '                            returnValue = SQLReader.GetBoolean(0)
    '                        ElseIf TypeOf returnValue Is Byte Then
    '                            returnValue = SQLReader.GetByte(0)
    '                        ElseIf TypeOf returnValue Is Single Then
    '                            returnValue = SQLReader.GetFloat(0)
    '                        ElseIf TypeOf returnValue Is Int64 Then
    '                            returnValue = SQLReader.GetInt64(0)
    '                        End If
    '                    End While
    '                End If
    '            End Using
    '        End Using
    '    End Using
    'End Sub


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
            Return dbDatatable
        End Using
    End Function

    'Edit By Arif
    Public Function fSelectCommandDt1(ByVal strSQL As String, ByVal isFirstTable As Boolean) As DataTable
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
    ''' Get the dataset by passing query string, dataset name and also pass the parameters.
    ''' Applicable for single table select command if parameter isManyTable is not set to true.
    ''' Modified Hazrin on 15022018 to include the execution of BeginTransaction and MissingSchemaAction
    ''' If isManyTable is set to True, this function is applicable for many tables for update. 
    ''' But have to call function sConnBeginTrans() before calling this function. 
    ''' Modified Hazrin on 03042018
    ''' </summary>
    ''' <param name="strSQL"></param>
    ''' <param name="param"></param>
    ''' <param name="isFirstTable">set to True</param>
    ''' <returns>Dataset</returns>
    Public Function fSelectCommandDs(ByVal strSQL As String, ByVal isFirstTable As Boolean, ByVal param() As SqlParameter) As DataSet

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
            Dim dbDataset As New DataSet
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

    Public Function fselectCommandDt(ByVal strSql As String) As DataTable
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
        End Using
    End Function

    Public Function fselectCommandDsParam(ByVal strSql As String, ByVal param() As SqlParameter) As DataSet
        Using dbconn = New SqlConnection(ConnectionString)
            dbconn.Open()
            Using dbComm = New SqlCommand(strSql, dbconn)
                dbComm.CommandTimeout = 600000
                If Not param Is Nothing Then
                    For Each p In param
                        dbComm.Parameters.Add(p)
                    Next
                End If

                Using dbAdapter = New SqlDataAdapter(dbComm)
                    Dim dbDataset As New DataSet
                    dbAdapter.Fill(dbDataset)
                    'dbComm.Parameters.Clear()
                    Return dbDataset
                End Using
            End Using
        End Using
    End Function

    Public Function fselectCommandDtParam(ByVal strSql As String, ByVal param() As SqlParameter) As DataTable
        Using dbconn = New SqlConnection(ConnectionString)
            dbconn.Open()
            Using dbComm = New SqlCommand(strSql, dbconn)
                dbComm.CommandTimeout = 600000
                If Not param Is Nothing Then
                    For Each p In param
                        dbComm.Parameters.Add(p)
                    Next
                End If

                Using dbAdapter = New SqlDataAdapter(dbComm)
                    Dim dbDatatable As New DataTable
                    dbAdapter.Fill(dbDatatable)
                    'dbComm.Parameters.Clear()
                    Return dbDatatable
                End Using
            End Using
        End Using
    End Function


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


    Public Function fUpdateCommand(ByVal strSQL As String) As Integer
        Using dbconn = New SqlConnection(ConnectionString)
            dbconn.Open()
            Using dbComm = New SqlCommand(strSQL, dbconn)
                dbComm.CommandTimeout = 600000
                Dim intRec = dbComm.ExecuteNonQuery
                Return intRec
            End Using
        End Using
    End Function

    ''' <summary>
    ''' Insert record to database using sqlcommand parameter to avoid sql injection.
    ''' Return count of record as integer.
    ''' </summary>
    ''' <param name="strSQL"></param>
    ''' <param name="param"></param>
    ''' <returns>count of record </returns>
    Public Function fUpdateCommand(ByVal strSQL As String, ByVal param() As SqlParameter) As Integer
        Using dbconn = New SqlConnection(ConnectionString)
            dbconn.Open()
            Using dbComm = New SqlCommand(strSQL, dbconn)
                dbComm.CommandTimeout = 600000
                If Not param Is Nothing Then
                    For Each p In param
                        dbComm.Parameters.Add(p)
                    Next
                End If
                Dim intRec = dbComm.ExecuteNonQuery
                'dbComm.Parameters.Clear()
                Return intRec
            End Using
        End Using
    End Function


    ''' <summary>
    ''' Hanafi,02554,26/12/2017 
    ''' executes stored procedure update transaction
    ''' </summary>
    ''' <param name="SPName">nama stored proc</param>
    ''' <param name="param">parameter collection</param>
    ''' <returns></returns>
    Public Function fSP_Update(ByVal SPName As String, ByVal param() As SqlParameter) As Integer
        Using dbconn = New SqlConnection(ConnectionString)
            dbconn.Open()
            Using dbComm = New SqlCommand(SPName, dbconn)
                dbComm.CommandType = CommandType.StoredProcedure
                dbComm.CommandTimeout = 600000
                If Not param Is Nothing Then
                    For Each p In param
                        dbComm.Parameters.Add(p)
                    Next
                End If
                Dim intRec = dbComm.ExecuteNonQuery
                'dbComm.Parameters.Clear()
                Return intRec
            End Using
        End Using
    End Function

    Friend Shared Function fSelectCommand(strSql As String) As DataSet
        Throw New NotImplementedException()
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
        Using dbconn = New SqlConnection(ConnectionString)
            dbconn.Open()
            Using dbComm = New SqlCommand(SPName, dbconn)
                dbComm.CommandType = CommandType.StoredProcedure
                dbComm.CommandTimeout = 600000
                If Not param Is Nothing Then
                    For Each p In param
                        dbComm.Parameters.Add(p)
                    Next
                End If
                Dim intRec = dbComm.ExecuteNonQuery
                strOutput = ParamOutput.Value.ToString
                'dbComm.Parameters.Clear()
                Return intRec
            End Using
        End Using
    End Function

    Public Function fCountRecord(strSql As String) As Int32
        Using dbconn = New SqlConnection(ConnectionString)
            dbconn.Open()
            Dim count As Int32 = 0
            Using dbComm As New SqlCommand(strSql, dbconn)
                dbComm.CommandTimeout = 600000
                count = Convert.ToInt32(dbComm.ExecuteScalar())
            End Using
            Return count
        End Using
    End Function

    Public Function fCountRecord(strSql As String, param() As SqlParameter) As Integer
        Using dbconn = New SqlConnection(ConnectionString)
            dbconn.Open()
            Dim count As Integer = 0
            Using dbComm As New SqlCommand(strSql, dbconn)
                dbComm.CommandTimeout = 600000
                If Not param Is Nothing Then
                    For Each p In param
                        dbComm.Parameters.Add(p)
                    Next
                End If
                count = Convert.ToInt32(dbComm.ExecuteScalar())
                'dbComm.Parameters.Clear()
            End Using
            Return count
        End Using
    End Function

    Public Sub sSP_GetItem(ByVal SPName As String, ByVal param() As SqlParameter, ByRef item As Object)
        Using dbconn = New SqlConnection(ConnectionString)
            dbconn.Open()

            Using dbComm As New SqlCommand(SPName, dbconn)
                dbComm.CommandTimeout = 600000
                dbComm.CommandType = CommandType.StoredProcedure

                If Not param Is Nothing Then
                    For Each p In param
                        dbComm.Parameters.Add(p)
                    Next
                End If

                If TypeOf item Is String Then
                    item = dbComm.ExecuteScalar().ToString
                ElseIf TypeOf item Is Integer Then
                    item = Convert.ToInt32(dbComm.ExecuteScalar())
                End If
            End Using
        End Using
    End Sub


    'Private Sub sCreateConnection()
    '    dbConn = New SqlConnection(ConnectionString)
    '    dbConn.Open()
    'End Sub

    Private Sub sInitializeConnStr()
        Dim strDeDBPassword
        Dim clsCrypto As New clsCrypto
        'strDeDBPassword = clsCrypto.fDecryptHashingMD5(strDBPassword)

        'ConnectionString = "Server=" & strDbServer & ";Database=" & strDbName & ";Uid=" & strDBUser & ";Pwd=" & strDeDBPassword & ";Pooling=False;"
    End Sub

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
End Class



