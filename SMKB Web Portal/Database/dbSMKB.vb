Imports System
Imports System.Data.SqlClient

Module dbSMKB
    Public dbconnNew As SqlConnection

    Public dbcomm As SqlCommand

    Public ds As DataSet

    Public dbread As SqlDataReader

    Public strCon As String = "Data Source=devmis12.utem.edu.my\sql_ins02;Initial Catalog=DbKewanganV4;Persist Security Info=True;User ID=Smkb;Password=Smkb@Dev2012"

    Public strCon2 As String = "Data Source=qa11.utem.edu.my\sql_ins01;Initial Catalog=dbstaf;Persist Security Info=True;User ID=smkb;Password=smkb*pwd"

    Public strConStaf As String = "Data Source=V-SQL11.utem.edu.my\sql_ins01;Initial Catalog=dbstaf;Persist Security Info=True;User ID=smkb;Password=smkb*pwd"

    Public strConSMP As String = "Data Source=V-SQL13.utem.edu.my\SQL_INS03;Initial Catalog=dbStudent;Persist Security Info=True;User ID=smkb;Password=smkb*pwd"

    Function NewGetDataPB(ByVal queryString As String, ByVal param() As SqlParameter, conn As SqlConnection, Optional db As String = "DbKewanganV4") As DataSet    'Utk gridview sahaja

        Dim ds As New DataSet()
        Dim strConnection As String = ""

        Try
            dbcomm = New SqlCommand(queryString, conn)
            Dim adapter As New SqlDataAdapter

            adapter.SelectCommand = dbcomm

            If Not param Is Nothing Then

                For Each p In param
                    dbcomm.Parameters.Add(p)
                Next
            End If

            '---fILL THE DATASET---
            adapter.Fill(ds)

        Catch ex As Exception

            '---THE CONNECTION FAILED. DISPLAY AN ERROR MESSAGE---
            MsgBox(ex.Message)

        Finally

            dbcomm = Nothing

        End Try
        Return ds
    End Function
End Module


