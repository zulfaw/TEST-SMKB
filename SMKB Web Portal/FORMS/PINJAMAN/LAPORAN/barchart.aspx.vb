Imports System.Data.SqlClient

Public Class barchart
    Inherits System.Web.UI.Page
    Public labl As String = ""
    Public val As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Request.QueryString("tahun") IsNot Nothing Then
        ' Retrieve the value of the "tahun" parameter
        Dim parTahun As String = Request.QueryString("tahun")

            Dim rowIndex As Integer = 0
            Using con As New SqlConnection(dbSMKB.strCon)
                Using cmd As SqlCommand = New SqlCommand()

                cmd.CommandText = "SELECT a.Butiran as bulan,
                        cast((select COUNT(*) from SMKB_Pinjaman_Master x where year(x.Tkh_Mohon) = '" & Session("tahunChart") & "' and MONTH(x.Tkh_Mohon) = a.Kod_Detail) AS VARCHAR(10)) as bil
                        FROM SMKB_Lookup_Detail as a
                        WHERE kod = '0147'
                        ORDER BY CONVERT(INT, Kod_Detail)"

                cmd.Connection = con

                    Dim dt2 As DataTable = New DataTable()
                    Using sda As SqlDataAdapter = New SqlDataAdapter(cmd)
                        sda.Fill(dt2)
                        Dim dt As New DataTable()

                        dt.Columns.Add("bulan", GetType(String))
                        dt.Columns.Add("bil", GetType(String))
                        For i = 0 To dt2.Rows.Count - 1
                            Dim dr As DataRow = dt.NewRow()
                            rowIndex += 1

                            labl += "'" + dt2.Rows(i).Item(0) + "', "
                            val += "'" + dt2.Rows(i).Item(1) + "', "
                        Next

                        labl = Left(labl, labl.Length - 1)
                        val = Left(val, val.Length - 1)
                    End Using
                End Using
            End Using
        'End If
    End Sub

End Class