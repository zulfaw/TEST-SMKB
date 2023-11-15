Imports System
Imports System.Data.SqlClient
Imports System.Diagnostics.Eventing.Reader
Imports System.Web.UI.HtmlControls
Imports System.Web
Imports System.Web.UI.WebControls

Public Class TableImbanganDuga
    Inherits System.Web.UI.UserControl

    Public Property KodKwFrom As String
    Public Property KodKwTo As String
    Public Property Tajuk As String
    Public Property TotalAmaun As Decimal
    Public Property TotalAmaun2 As Decimal
    Public Property CustomClass As String
    Public Property Bulan As String
    Public Property Tahun As String
    Public Property PTj As String
    Public Property Syarikat As String
    Public Property KodKw As String

    Dim listBulan As String() = {"Januari", "Februari", "Mac", "April", "Mei", "Jun", "Julai", "Ogos", "September", "Oktober", "November", "Disember"}
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblTajuk.InnerHtml = Tajuk
        'tableData.Attributes("class") += " " + CustomClass
        'LoadDataTable(KodKwFrom, KodKwTo, "tableData")
        rptone.DataSource = GetKW()
        rptone.DataBind()
    End Sub

    Private Function GetKW() As DataTable

        Dim connection As New SqlConnection(strCon)
        Dim optionalWhere As String = ""

        If Session("kodkw") = "00" Then
            optionalWhere = "WHERE Kod_Kump_Wang >= 1 And Kod_Kump_Wang <= 11"
        Else
            optionalWhere = "WHERE Kod_Kump_Wang = @KodKw "
        End If

        Dim query As String = $"SELECT Kod_Kump_Wang, CONCAT(Kod_Kump_Wang, ' - ', Butiran) AS Butiran
                            FROM SMKB_Kump_Wang
                            {optionalWhere}
                            ORDER BY Kod_Kump_Wang"


        Dim command As New SqlCommand(query, connection)
        command.Parameters.Add(New SqlParameter("@KodKw", Session("kodkw")))
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

    Private Function LoadDataTable(KodKw As String) As DataTable

        Dim query As String
        Dim optionalWhere As String = ""

        optionalWhere = " And A.Kod_Kump_Wang = @KodKw "

        If Session("ptj") = "00" Then

            query = "SELECT KodKW_RptTwo , kodvot , ButiranDetail , ButiranHeader , Kod_Jenis, 
                            CASE WHEN amaunTerkumpul > 0 THEN amaunTerkumpul ELSE '0.00' END AS amaunTerkumpulDebit, 
                            CASE WHEN amaunTerkumpul < 0 THEN Replace(amaunTerkumpul,'-','') ELSE '0.00' END AS amaunTerkumpulKredit, 
                            CASE WHEN amaunSemasa > 0 THEN amaunSemasa ELSE '0.00'  END AS amaunSemasaDebit, 
                            CASE WHEN amaunSemasa < 0 THEN Replace(amaunSemasa,'-','') ELSE '0.00'  END AS amaunSemasaKredit
 
                     FROM 
                            (
                                SELECT A.Kod_Kump_Wang as KodKW_RptTwo,
                                    A.Kod_Vot AS kodvot,
                                    (SELECT Butiran FROM SMKB_Vot WHERE Kod_Vot = A.Kod_Vot) as ButiranDetail,
                                    A.Kod_Vot AS kodvotH,
                                    (SELECT Butiran FROM SMKB_Vot WHERE Kod_Vot = A.Kod_Vot) as ButiranHeader,
                                    B.Kod_Jenis, 
                                    
                                        SUM(
                                            CASE 
                                                WHEN @bulan = 1 THEN (A.Dr_1 - A.Cr_1)
                                                WHEN @bulan = 2 THEN (A.Dr_1 + A.Dr_2) - (A.Cr_1 + A.Cr_2)
                                                WHEN @bulan = 3 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3) - (A.Cr_1 + A.Cr_2 + A.Cr_3)
                                                WHEN @bulan = 4 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4)
                                                WHEN @bulan = 5 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4  + A.Cr_5)
                                                WHEN @bulan = 6 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4  + A.Cr_5 + A.Cr_6)
                                                WHEN @bulan = 7 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4  + A.Cr_5 + A.Cr_6 + A.Cr_7)
                                                WHEN @bulan = 8 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4  + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8)	
                                                WHEN @bulan = 9 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4  + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9)
                                                WHEN @bulan = 10 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4  + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10)
                                                WHEN @bulan = 11 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4  + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11)
                                                WHEN @bulan = 12 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4  + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)
                                                ELSE 0 
                                            END
                                        
                                    ) AS amaunTerkumpul ,
                                    
                                        SUM(
                                            CASE 
                                                WHEN @bulan = 1 THEN (A.Dr_1) - (A.Cr_1)
                                                WHEN @bulan = 2 THEN (A.Dr_2) - (A.Cr_2)
                                                WHEN @bulan = 3 THEN (A.Dr_3) - (A.Cr_3)
                                                WHEN @bulan = 4 THEN (A.Dr_4) - (A.Cr_4)
                                                WHEN @bulan = 5 THEN (A.Dr_5) - (A.Cr_5)
                                                WHEN @bulan = 6 THEN (A.Dr_6) - (A.Cr_6)
                                                WHEN @bulan = 7 THEN (A.Dr_7) - (A.Cr_7)
                                                WHEN @bulan = 8 THEN (A.Dr_8) - (A.Cr_8)
                                                WHEN @bulan = 9 THEN (A.Dr_9) - (A.Cr_9)
                                                WHEN @bulan = 10 THEN (A.Dr_10) - (A.Cr_10)
                                                WHEN @bulan = 11 THEN (A.Dr_11) - (A.Cr_11)
                                                WHEN @bulan = 12 THEN (A.Dr_12) - (A.Cr_12)
                                                ELSE 0 
                                            END
                                       
                                    ) AS amaunSemasa
                                FROM
                                    SMKB_Lejar_Am A
                                JOIN SMKB_Vot B ON A.Kod_Vot=B.Kod_Vot
                                WHERE
                                    A.Tahun = @tahun 
                                    --AND B.Kod_Jenis IN ('A','L','C')
                                     " + optionalWhere + "
                                    --And A.Kod_Kump_Wang >= @kodkw
                                    --And A.Kod_Kump_Wang <= @kodkw
                                    AND A.Kod_PTJ >= '010000'
                                    AND A.Kod_PTJ <= 'Y00005'
                                GROUP BY
                                    A.Kod_Kump_Wang,A.Kod_Vot, B.Kod_Jenis
                            ) mainTbl 
                            WHERE (CAST(amaunTerkumpul AS DECIMAL) <> 0.00 OR CAST(amaunSemasa AS DECIMAL) <> 0.00)
                            ORDER BY
                                kodvot"

        Else query = "SELECT KodKW_RptTwo , kodvot , ButiranDetail , ButiranHeader , Kod_Jenis, 
                        CASE WHEN amaunTerkumpul > 0 THEN amaunTerkumpul ELSE '0.00'  END AS amaunTerkumpulDebit, 
                        CASE WHEN amaunTerkumpul < 0 THEN Replace(amaunTerkumpul,'-','') ELSE '0.00'  END AS amaunTerkumpulKredit, 
                        CASE WHEN amaunSemasa > 0 THEN amaunSemasa ELSE '0.00'  END AS amaunSemasaDebit, 
                        CASE WHEN amaunSemasa < 0 THEN Replace(amaunSemasa,'-','') ELSE '0.00'  END AS amaunSemasaKredit
 
                        FROM 
                            (
                                SELECT A.Kod_Kump_Wang as KodKW_RptTwo,
                                    A.Kod_Vot AS kodvot,
                                    (SELECT Butiran FROM SMKB_Vot WHERE Kod_Vot = A.Kod_Vot) as ButiranDetail,
                                    A.Kod_Vot AS kodvotH,
                                    (SELECT Butiran FROM SMKB_Vot WHERE Kod_Vot = A.Kod_Vot) as ButiranHeader,
                                    B.Kod_Jenis, 
                                    
                                        SUM(
                                            CASE 
                                                WHEN @bulan = 1 THEN (A.Dr_1 - A.Cr_1)
                                                WHEN @bulan = 2 THEN (A.Dr_1 + A.Dr_2) - (A.Cr_1 + A.Cr_2)
                                                WHEN @bulan = 3 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3) - (A.Cr_1 + A.Cr_2 + A.Cr_3)
                                                WHEN @bulan = 4 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4)
                                                WHEN @bulan = 5 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4  + A.Cr_5)
                                                WHEN @bulan = 6 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4  + A.Cr_5 + A.Cr_6)
                                                WHEN @bulan = 7 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4  + A.Cr_5 + A.Cr_6 + A.Cr_7)
                                                WHEN @bulan = 8 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4  + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8)	
                                                WHEN @bulan = 9 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4  + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9)
                                                WHEN @bulan = 10 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4  + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10)
                                                WHEN @bulan = 11 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4  + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11)
                                                WHEN @bulan = 12 THEN (A.Dr_1 + A.Dr_2 + A.Dr_3 + A.Dr_4  + A.Dr_5 + A.Dr_6 + A.Dr_7 + A.Dr_8 + A.Dr_9 + A.Dr_10 + A.Dr_11 + A.Dr_12) - (A.Cr_1 + A.Cr_2 + A.Cr_3 + A.Cr_4  + A.Cr_5 + A.Cr_6 + A.Cr_7 + A.Cr_8 + A.Cr_9 + A.Cr_10 + A.Cr_11 + A.Cr_12)
                                                ELSE 0 
                                            END
                                        
                                    ) AS amaunTerkumpul ,
                                    
                                        SUM(
                                            CASE 
                                                WHEN @bulan = 1 THEN (A.Dr_1) - (A.Cr_1)
                                                WHEN @bulan = 2 THEN (A.Dr_2) - (A.Cr_2)
                                                WHEN @bulan = 3 THEN (A.Dr_3) - (A.Cr_3)
                                                WHEN @bulan = 4 THEN (A.Dr_4) - (A.Cr_4)
                                                WHEN @bulan = 5 THEN (A.Dr_5) - (A.Cr_5)
                                                WHEN @bulan = 6 THEN (A.Dr_6) - (A.Cr_6)
                                                WHEN @bulan = 7 THEN (A.Dr_7) - (A.Cr_7)
                                                WHEN @bulan = 8 THEN (A.Dr_8) - (A.Cr_8)
                                                WHEN @bulan = 9 THEN (A.Dr_9) - (A.Cr_9)
                                                WHEN @bulan = 10 THEN (A.Dr_10) - (A.Cr_10)
                                                WHEN @bulan = 11 THEN (A.Dr_11) - (A.Cr_11)
                                                WHEN @bulan = 12 THEN (A.Dr_12) - (A.Cr_12)
                                                ELSE 0 
                                            END
                                       
                                    ) AS amaunSemasa
                                FROM
                                    SMKB_Lejar_Am A
                                JOIN SMKB_Vot B ON A.Kod_Vot=B.Kod_Vot
                                WHERE
                                    A.Tahun = @tahun 
                                    --AND B.Kod_Jenis IN ('A','L','C')
                                     " + optionalWhere + "
                                    --And A.Kod_Kump_Wang >= @kodkw
                                    --And A.Kod_Kump_Wang <= @kodkw
                                    And A.Kod_PTJ >= @ptj
                                    And A.Kod_PTJ <=@ptj
                                GROUP BY
                                    A.Kod_Kump_Wang,A.Kod_Vot, B.Kod_Jenis
                            ) mainTbl 
                            WHERE (CAST(amaunTerkumpul AS DECIMAL) <> 0.00 OR CAST(amaunSemasa AS DECIMAL) <> 0.00)
                            ORDER BY
                                kodvot"
        End If


        Dim dataTable As New DataTable()

        If Session("bulan") = "" Then
            Session("bulan") = "-"
        End If

        If Session("tahun") = "" Then
            Session("tahun") = "-"
        End If

        If Session("ptj") = "" Then
            Session("ptj") = "-"
        End If

        If Session("kodkw") = "" Then
            Session("kodkw") = "-"
        End If


        Using connection As New SqlConnection(strCon)
            Using command As New SqlCommand(query, connection)
                command.Parameters.Add(New SqlParameter("@KodKw", KodKw))
                command.Parameters.Add(New SqlParameter("@bulan", Session("bulan")))
                command.Parameters.Add(New SqlParameter("@tahun", Session("tahun")))
                command.Parameters.Add(New SqlParameter("@ptj", Session("ptj")))

                connection.Open()
                dataTable.Load(command.ExecuteReader())
            End Using
        End Using
        Return dataTable
    End Function

    Protected Sub rptone_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
        Dim rptItem As RepeaterItem = TryCast(e.Item, RepeaterItem)
        Dim kodKW As String = rptItem.DataItem("Kod_Kump_Wang")

        Dim rptTwo As Repeater = TryCast(rptItem.FindControl("repeaterDetail"), Repeater)
        rptTwo.DataSource = LoadDataTable(kodKW)
        rptTwo.DataBind()

    End Sub
End Class