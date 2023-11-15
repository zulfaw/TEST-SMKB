Imports Newtonsoft.Json
Imports System
Imports System.Data.SqlClient

Public Class TokenResponseModel
    <JsonProperty("access_token")>
    Public Property AccessToken As String

    <JsonProperty("token_type")>
    Public Property TokenType As String

    <JsonProperty("expires_in")>
    Public Property ExpiresIn As Integer

    <JsonProperty("userName")>
    Public Property Username As String

    <JsonProperty(".issued")>
    Public Property IssuedAt As String

    <JsonProperty(".expires")>
    Public Property ExpiresAt As String


    'Namespace WindowsFormsApp8.Models
    'Class TokenResponseModel

    'End Class
    'End Namespace

    Public Function GetTicket(mymodule As String, userid As String) As String
        Dim myStr As String = ""
        Dim dbase_dbmobile As String = "Data Source='V-SQL14.utem.edu.my\SQL_INS04';Initial Catalog='DbMobile';User ID='MobApp';Password='m0bile@pp2018'"

        Try
            Using sqlConn As New SqlConnection(dbase_dbmobile)
                Using cmd As New SqlCommand()
                    Dim rnd As New Random()
                    Dim myrnd As Integer = rnd.Next(100000, 10000000)
                    cmd.CommandText = "insert into AspNetInstantTicket (TicketId, module, userid) values (@myticket,@mymodule, @userid)"
                    cmd.Connection = sqlConn
                    cmd.Parameters.AddWithValue("@myticket", myrnd.ToString())
                    cmd.Parameters.AddWithValue("@mymodule", mymodule)
                    cmd.Parameters.AddWithValue("@userid", userid)

                    Try
                        sqlConn.Open()
                        cmd.ExecuteNonQuery()
                        myStr = myrnd.ToString()
                    Catch ex As SqlException
                        myStr = ex.Message ' "Capaian ke database bermasalah. Sila cuba lagi"
                    End Try
                End Using
            End Using
        Catch ex As Exception
            myStr = ex.Message ' "Capaian ke database bermasalah. Sila cuba lagi"
        End Try
        Return myStr
    End Function

End Class
