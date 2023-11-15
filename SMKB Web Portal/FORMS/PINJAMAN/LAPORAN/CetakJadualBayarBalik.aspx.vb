Imports System
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text
'Imports System.Data.SqlClient

Partial Class CetakJadualBayarBalik
    Inherits System.Web.UI.Page

    Public dsMaklumatKorporat As New DataSet
    Public dvMaklumatKorporat As New DataView
    Dim dbconn As New DBKewConn

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not IsPostBack Then

            Dim script As New StringBuilder()
            script.AppendLine("<script type='text/javascript'>")
            script.AppendLine("window.onload = function() {")
            script.AppendLine("    setTimeout(function() {")
            script.AppendLine("        window.print();")
            script.AppendLine("        window.onafterprint = function() { window.close(); };")
            script.AppendLine("    }, 100);")
            script.AppendLine("};")
            script.AppendLine("</script>")

            ClientScript.RegisterStartupScript(Me.GetType(), "Print", script.ToString(), False)

            tblByrBalikPinjaman.NoPinj = Session("NoPinj")
            loadMakPeminjam()
            LoadMakHeader()

        End If
    End Sub

    Private Sub LoadMakHeader()
        Dim strSql As String = "select a.Nama_Sing, a.Nama, a.Almt1, a.Almt2, a.Bandar, a.Poskod, b.Butiran as Negeri, a.Kod_Negara, a.No_Tel1, a.No_Tel2, 
                            a.No_Faks1, a.No_Faks2, a.Laman_Web, a.Logo, a.Emel, a.Kategori, a.No_GST
                            from SMKB_Korporat a, SMKB_Lookup_Detail b
                            where b.Kod_Detail = a.Kod_Negeri
                            and b.Kod = '0002'
                            and a.status = 1"

        Dim connection As New SqlConnection(strCon)
        connection.Open()

        Dim command As New SqlCommand(strSql, connection)
        Dim dbread As SqlDataReader = command.ExecuteReader()

        If dbread.Read() Then
            Dim imageData As Byte() = DirectCast(dbread("Logo"), Byte())
            Dim base64String As String = Convert.ToBase64String(imageData)
            imgMyImage.ImageUrl = String.Format("data:image/jpg;base64,{0}", base64String)

            lblNamaKorporat.Text = dbread("Nama")
            lblAlamatKorporat.Text = dbread("Almt1") & ", " & dbread("Almt1") & ", " & dbread("Almt2") & ", " & dbread("Poskod") & ", " & dbread("Bandar") & ", " & dbread("Negeri")
            lblNoTelFaks.Text = "No Tel: " & dbread("No_Tel1") & " Fax: " & dbread("No_Faks1")
            lblEmailKorporat.Text = dbread("Emel")
        End If

        dbread.Close()
        connection.Close()
    End Sub

    Protected Sub Page_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        ' Retrieve the content from the session variable and display it
        Dim content As String = TryCast(Session("PrintContent"), String)
        'lblContent.Text = content
    End Sub
    Function ConvertFileToBinary(ByVal fileStream As Stream) As Byte()
        ' Convert the file to binary data
        Using binaryReader As New BinaryReader(fileStream)
            Return binaryReader.ReadBytes(CInt(fileStream.Length))
        End Using
    End Function


    Protected Sub loadMakPeminjam()

        Dim Sql As String

        Sql = "select a.No_Pinj, a.No_Staf, c.MS01_Nama,  e.pejabat, a.Amaun, a.Tempoh_Pinjaman, isnull(a.No_Jilid,'-') as No_Jilid, a.Amaun, a.Tempoh_Pinjaman, e.pejabat
                from SMKB_Pinjaman_Master AS a, [devmis\sql_ins01].dbstaf.dbo.MS01_Peribadi AS c,
                [devmis\sql_ins01].dbstaf.dbo.MS08_Penempatan AS d, [devmis\sql_ins01].dbstaf.dbo.MS_pejabat AS e
                where 1 = 1
                and a.No_Pinj = '" & Session("noPinj") & "'
                and a.No_Staf = c.MS01_NoStaf
                and a.No_Staf = d.MS01_NoStaf
                and d.MS08_StaTerkini = 1
                and SUBSTRING(d.MS08_Unit,1,2) = e.kodpejabat"

        Dim dt As New DataTable
        dt = dbconn.fSelectCommandDt(Sql)

        If dt.Rows.Count > 0 Then
            lblNama.Text = dt.Rows(0)("MS01_Nama")
            lblNoJilid.Text = dt.Rows(0)("No_Jilid")
            lblNoPinj.Text = dt.Rows(0)("No_Pinj")
            lblJumlah.Text = dt.Rows(0)("Amaun")
            lblNoStaf.Text = dt.Rows(0)("No_Staf")
            lblTempoh.Text = dt.Rows(0)("Tempoh_Pinjaman")
            lblNamaPTj.Text = dt.Rows(0)("pejabat")
            lblKeuntungan.Text = "4%"
        End If

    End Sub

End Class

