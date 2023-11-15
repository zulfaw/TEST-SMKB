
Public Class CRIM
    Inherits System.Web.UI.Page

    'Private Shared dtSpekAm As New DataTable
    'Private Shared decJumlahBesar As Decimal = 0.00
    'Private Shared decAngHrgSeunit As Decimal = 0.00
    'Private Shared decJumAngHrg As Decimal = 0.00

    'Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    '    If Not IsPostBack Then
    '        mvMohonBeli.ActiveViewIndex = 0
    '        LinkButton2.Enabled = False
    'fBindGVSpekAm()
    'End If
    'End Sub


    'Private Sub fBindGVSpekAm()

    '    Try
    '        Dim strSql As String = $"select ID, Perkara, Kuantiti, Unit, AngHargaSeunit, AngHargaTotal From PO1_SpekAmDt"
    '        Dim dbconn As New DBKewConn


    '        dbconn.fselectCommand(strSql)

    '        If dtSpekAm.Rows.Count = 0 Then
    '            'dtSpekAm.Rows.Add()
    '        End If


    '        'If ds.Tables(0).Rows.Count > 0 Then
    '        '    Dim strDate As DateTime = Nothing
    '        '    dt.Columns.Add("Bil", GetType(Int32))
    '        '    dt.Columns.Add("NoPermohonan", GetType(String))
    '        '    dt.Columns.Add("Program", GetType(String))
    '        '    dt.Columns.Add("TarikhMohon", GetType(String))
    '        '    dt.Columns.Add("AngBelanja", GetType(String))
    '        '    dt.Columns.Add("Status", GetType(String))

    '        '    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
    '        '        Dim tableRow = dt.NewRow()
    '        '        tableRow("Bil") = i + 1
    '        '        tableRow("NoPermohonan") = ds.Tables(0).Rows(i)(0).ToString
    '        '        strDate = ds.Tables(0).Rows(i)(1).ToString
    '        '        tableRow("TarikhMohon") = strDate.ToString("dd/MM/yyyy")

    '        '        tableRow("Program") = ds.Tables(0).Rows(i)(2).ToString
    '        '        tableRow("AngBelanja") = ds.Tables(0).Rows(i)(3).ToString

    '        '        For Each item In dicStatusDok
    '        '            If item.Key = ds.Tables(0).Rows(i)(4).ToString Then
    '        '                tableRow("Status") = item.Value
    '        '                Exit For
    '        '            End If
    '        '        Next

    '        '        dt.Rows.Add(tableRow)
    '        '    Next
    '        'Else
    '        '    fGlobalAlert("Tiada rekod dijumpai!", Me.Page, Me.[GetType]())
    '        'End If



    '    Catch ex As Exception

    '    End Try
    'End Sub
    'Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
    '    mvMohonBeli.ActiveViewIndex = 0
    'End Sub

    'Protected Sub LinkButton2_Click(sender As Object, e As EventArgs) Handles LinkButton2.Click
    '    mvMohonBeli.ActiveViewIndex = 1
    'End Sub


    'Protected Sub btnStep1_Click(sender As Object, e As EventArgs) Handles btnStep1.Click

    '    LinkButton2.Enabled = True
    '    'check validation
    '    'simpan

    '    'check jumlah harga, bagi mengelak pecah kecil pembelian
    '    If decJumlahBesar >= 50000 Then
    '        mvMohonBeli.ActiveViewIndex = 2
    '    Else
    '        mvMohonBeli.ActiveViewIndex = 1
    '    End If
    'End Sub

    Private Sub fbindGVRA()

    End Sub
End Class