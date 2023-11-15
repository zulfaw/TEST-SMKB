Imports System.Data.SqlClient
Public Class db
    Private mycon As SqlConnection
    Private dbcmd As SqlCommand
    Private dr As SqlDataReader
    Public strCon As String = "Data Source=10.115.10.73;Initial Catalog=dbtest;Persist Security Info=True;User ID=dbtable;Password=dbtable1234"
    Dim cmd_status As Boolean = False
    Public code As String = ""
    Private status_ As String
    Private lastid_ As String
    Public dt As DataTable

    Public Sub New()
        MyBase.New()
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
    Public Property LastID As String
        Get
            Return lastid_
        End Get
        Set(value As String)
            lastid_ = value
        End Set
    End Property

    Public Property ProcStatus As String
        Get
            Return status_
        End Get
        Set(value As String)
            status_ = value
        End Set
    End Property

End Class
