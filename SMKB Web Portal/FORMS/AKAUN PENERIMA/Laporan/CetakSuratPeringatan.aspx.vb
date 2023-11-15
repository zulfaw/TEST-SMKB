Imports System
Imports System.Data.SqlClient
Imports AjaxControlToolkit

Public Class CetakSuratPeringatan
    Inherits System.Web.UI.Page
    Dim dt As DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim noBil As String = Request.QueryString("nobil")
            Dim kodPenghutang As String = Request.QueryString("kod")

            If noBil <> "" Then
                LoadHeader()
                fAlamat(kodPenghutang)

                Dim currentYear As Integer = DateTime.Now.Year
                ' Set the label text
                lblBilYear.Text = "TUNTUTAN BAYARAN BAGI INVOIS TAHUN " & currentYear

                dt = LoadBil(noBil)

                GridView1.DataSource = dt
                GridView1.DataBind()

                ' Create a new footer row
                Dim footerRow As New GridViewRow(0, 0, DataControlRowType.Footer, DataControlRowState.Normal)

                Dim cell As New TableCell()
                cell.ColumnSpan = 4
                cell.Text = "Jumlah Keseluruhan"

                cell.Style.Add("text-align", "center")
                cell.Style.Add("font-weight", "bold")

                Dim total As Decimal = CalculateTotal()
                Dim cell2 As New TableCell()
                cell2.ColumnSpan = 1 ' Since this cell will be in its own column
                cell2.Text = total ' Display the calculated total
                cell2.Style.Add("text-align", "right")
                cell2.Style.Add("font-weight", "bold")

                footerRow.Cells.Add(cell)
                footerRow.Cells.Add(cell2)

                ' Add the footer row to the GridView
                GridView1.Controls(0).Controls.Add(footerRow)
            Else
                ' Handle the case where nobil is not provided in the query string.
                ' You might want to show an error message or take appropriate action.
            End If
        End If
    End Sub
    Protected Function CalculateTotal() As Decimal
        Dim total As Decimal = 0

        For Each row As DataRow In dt.Rows
            Dim jumlahValue As Decimal
            If Decimal.TryParse(row("Jumlah").ToString(), jumlahValue) Then
                total += jumlahValue
            End If
        Next

        Return total
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

        Dim strSql As String = "select a.Nama_Sing, a.Nama, a.Almt1, a.Almt2, a.Bandar, a.Poskod, b.Butiran as Negara, c.Butiran as Negeri, a.Kod_Negara, a.No_Tel1, a.No_Tel2, 
                            a.No_Faks1, a.No_Faks2, a.Laman_Web, a.Logo, a.Emel, a.Kategori, a.No_GST
                            from SMKB_Korporat a 
                            INNER JOIN SMKB_Lookup_Detail b ON b.Kod='0001' AND a.Kod_Negara=b.Kod_Detail
                            INNER JOIN SMKB_Lookup_Detail c ON c.Kod='0002' AND a.Kod_Negeri=c.Kod_Detail"

        Dim connection As New SqlConnection(strCon)
        connection.Open()

        Dim command As New SqlCommand(strSql, connection)
        Dim dbread As SqlDataReader = command.ExecuteReader()

        If dbread.Read() Then
            Dim imageData As Byte() = DirectCast(dbread("Logo"), Byte())
            Dim base64String As String = Convert.ToBase64String(imageData)
            imgMyImage.ImageUrl = String.Format("data:image/jpg;base64,{0}", base64String)

            lblNamaKorporat.Text = ToTitleCase(dbread("Nama"))
            'lblAlamatKorporat.Text = dbread("Almt1") & ", " & dbread("Poskod") & ", " & dbread("Bandar") & ", " & dbread("Negeri")
            lblAlamatKorporat.Text = ToTitleCase(dbread("Almt1"))
            lblPoskod.Text = ToTitleCase(dbread("Poskod")) & " " & ToTitleCase(dbread("Bandar"))
            lblNegara.Text = ToTitleCase(dbread("Negeri")) & ", " & ToTitleCase(dbread("Negara"))
            lblNoTelFaks.Text = "Tel : +" & dbread("No_Tel1") & "&nbsp; &nbsp; &nbsp; &nbsp; Faks : +" & dbread("No_Faks1")
            lblEmailKorporat.Text = "Email: " & dbread("Emel").ToLower()
        End If

        dbread.Close()
        connection.Close()
    End Sub

    Private Sub fAlamat(kodPenghutang As String)
        Dim strSql As String = "SELECT A.Nama_Penghutang as Nama, A.Kod_Negara,B.Butiran AS Negara,C.Butiran AS Negeri,D.Butiran AS Bandar,A.* FROM SMKB_Penghutang_Master A
                                INNER JOIN SMKB_Lookup_Detail B ON B.Kod='0001' AND A.Kod_Negara=B.Kod_Detail
                                INNER JOIN SMKB_Lookup_Detail C ON C.Kod='0002' AND A.Kod_Negeri=C.Kod_Detail
                                INNER JOIN SMKB_Lookup_Detail D ON D.Kod='0003' AND A.Bandar=D.Kod_Detail
                                WHERE Kod_Penghutang='" & kodPenghutang & "'"

        Dim connection As New SqlConnection(strCon)
        connection.Open()

        Dim command As New SqlCommand(strSql, connection)
        Dim dbread As SqlDataReader = command.ExecuteReader()

        If dbread.Read() Then
            lblNamaPenghutang.Text = ToTitleCase(dbread("Nama"))
            lblAlamatPenghutang.Text = ToTitleCase(dbread("Alamat_1"))
            lblBandarPenghutang.Text = ToTitleCase(dbread("Alamat_2")) & ", " & ToTitleCase(dbread("Bandar"))
            lblNegaraPenghutang.Text = ToTitleCase(dbread("Poskod")) & " " & ToTitleCase(dbread("Negeri")) & ", " & ToTitleCase(dbread("Negara"))
        End If

        dbread.Close()
        connection.Close()
    End Sub

    Private Function LoadBil(nobil As String) As DataTable
        Dim strSql As String = "SELECT No_Bil, CONVERT(NVARCHAR(10), A.Tkh_Bil, 103) AS Tkh_Bil, Tujuan, Jumlah FROM SMKB_Bil_Hdr A
                                WHERE No_Bil='" & nobil & "'"

        Dim connection As New SqlConnection(strCon)
        connection.Open()

        Dim command As New SqlCommand(strSql, connection)

        Dim dt As New DataTable()
        dt.Columns.Add("bil", GetType(Integer))  ' Add the 'bil' column here
        dt.Columns.Add("No_Bil")
        dt.Columns.Add("Tkh_Bil")
        dt.Columns.Add("Tujuan")
        dt.Columns.Add("Jumlah")

        Try
            Dim adapter As New SqlDataAdapter(command)
            adapter.Fill(dt)
            If dt.Columns.Contains("bil") Then
                If dt.Rows.Count > 0 Then
                    For i As Integer = 0 To dt.Rows.Count - 1
                        dt.Rows(i)("bil") = i + 1
                    Next
                End If
            End If
        Catch ex As Exception
            ' Handle any exceptions here (e.g., log the error, show a message to the user, etc.)
        Finally
            connection.Close() ' Always remember to close the connection
        End Try
        Return dt
    End Function

    Protected Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound
        Dim counter As Integer = 1

        If e.Row.RowType = DataControlRowType.DataRow Then
            ' Find the index of the "bil" column
            Dim bilIndex As Integer = GetColumnIndexByName(GridView1.HeaderRow, "bil")

            ' Set the "bil" value to the counter variable and increment it
            e.Row.Cells(bilIndex).Text = counter.ToString()
            counter += 1

            Dim jumlahCell As TableCell = e.Row.Cells.OfType(Of TableCell)().FirstOrDefault(Function(cell) cell.Text = "Jumlah")

            If Not IsNothing(jumlahCell) Then
                ' Apply the style to the cell
                jumlahCell.Style.Add("text-align", "right")
            End If
        End If
    End Sub

    Private Function GetColumnIndexByName(headerRow As GridViewRow, columnName As String) As Integer
        For i As Integer = 0 To headerRow.Cells.Count - 1
            If headerRow.Cells(i).Text.ToLower() = columnName.ToLower() Then
                Return i
            End If
        Next
        Return -1
    End Function

End Class