Imports System
Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.Net.NetworkInformation

Public Class Agensi
    Inherits System.Web.UI.Page
    Dim dbconn As New DBKewConn
    Dim dbconnSM As New DBSMConn
    Public dsKod As New DataSet
    Public dvKodKW As New DataView

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                fBindGvAgensi()
                'fBindDdlBank()
                ViewState("savemode") = 1


            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub fBindGvAgensi()


        Try
            Dim dt As New DataTable
            dt = fCreateDtAgensi()

            If dt.Rows.Count = 0 Then
                gvAgensi.DataSource = New List(Of String)
            Else
                gvAgensi.DataSource = dt
            End If
            gvAgensi.DataBind()
            'gvAgensi.InnerText = dt.Rows.Count


            'Required for jQuery DataTables to work.
            gvAgensi.UseAccessibleHeader = True
            gvAgensi.HeaderRow.TableSection = TableRowSection.TableHeader

        Catch ex As Exception

        End Try
    End Sub
    Private Function fCreateDtAgensi() As DataTable

        Try

            Dim dt As New DataTable
            dt.Columns.Add("Kod_Kerajaan", GetType(String))
            dt.Columns.Add("No_Kerajaan", GetType(String))
            dt.Columns.Add("Nama", GetType(String))
            dt.Columns.Add("Alamat_1", GetType(String))
            dt.Columns.Add("Alamat_2", GetType(String))
            dt.Columns.Add("Bandar", GetType(String))
            dt.Columns.Add("Poskod", GetType(String))
            dt.Columns.Add("Kod_Negeri", GetType(String))
            dt.Columns.Add("Kod_Negara", GetType(String))
            dt.Columns.Add("No_Tel_1", GetType(String))
            dt.Columns.Add("No_Tel_2", GetType(String))
            dt.Columns.Add("No_Faks", GetType(String))
            dt.Columns.Add("Kod_Bank", GetType(String))
            dt.Columns.Add("No_Akaun", GetType(String))
            dt.Columns.Add("Emel", GetType(String))


            Dim kod As String
            Dim no As String
            Dim nama As String
            Dim add1 As String
            Dim add2 As String
            Dim bandar As String
            Dim poskod As String
            Dim negeri As String
            Dim negara As String
            Dim tel1 As String
            Dim tel2 As String
            Dim nofaks As String
            Dim kodbank As String
            Dim noakaun As String
            Dim strSql As String
            Dim emel As String

            strSql = "SELECT Kod_Kerajaan,No_Kerajaan,Nama,Alamat_1,Alamat_2,Bandar,Poskod,Kod_Negeri,Kod_Negara,No_Tel_1,No_Tel_2,No_Faks,Kod_Bank,No_Akaun,Emel from SMKB_Gaji_Kerajaan order by Kod_Kerajaan"


                Dim dbconn As New DBKewConn
            dsKod = dbconn.fSelectCommand(strSql)
            dvKodKW = New DataView(dsKod.Tables(0))

            For i As Integer = 0 To dsKod.Tables(0).Rows.Count - 1

                kod = dsKod.Tables(0).Rows(i)(0).ToString
                no = dsKod.Tables(0).Rows(i)(1).ToString
                nama = dsKod.Tables(0).Rows(i)(2).ToString
                add1 = dsKod.Tables(0).Rows(i)(3).ToString
                add2 = dsKod.Tables(0).Rows(i)(4).ToString
                bandar = dsKod.Tables(0).Rows(i)(5).ToString
                poskod = dsKod.Tables(0).Rows(i)(6).ToString
                negeri = dsKod.Tables(0).Rows(i)(7).ToString
                negara = dsKod.Tables(0).Rows(i)(8).ToString
                tel1 = dsKod.Tables(0).Rows(i)(9).ToString
                tel2 = dsKod.Tables(0).Rows(i)(10).ToString
                nofaks = dsKod.Tables(0).Rows(i)(11).ToString
                kodbank = dsKod.Tables(0).Rows(i)(12).ToString
                noakaun = dsKod.Tables(0).Rows(i)(13).ToString
                emel = dsKod.Tables(0).Rows(i)(14).ToString

                dt.Rows.Add(kod, no, nama, add1, add1, bandar, poskod, negeri, negara, tel1, tel2, nofaks, kodbank, noakaun, emel)
            Next

            Return dt

        Catch ex As Exception

        End Try

    End Function

    'Private Sub fBindDdlBank()

    '    Dim strSql As String = "SELECT Nama, Kod FROM SMKB_Bank_Credit_Online ORDER BY Kod"
    '    Using dt = dbconn.fSelectCommandDt(strSql)


    '        ddlBank.DataSource = dt
    '        ddlBank.DataTextField = "Nama"
    '        ddlBank.DataValueField = "Kod"
    '        ddlBank.DataBind()

    '        ddlBank.Items.Insert(0, New ListItem("- SILA PILIH - ", ""))
    '        ddlBank.SelectedIndex = 0



    '    End Using
    'End Sub

    Private Sub gvAgensi_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvAgensi.RowCommand
        Try
            Dim strSql As String
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim selectedRow As GridViewRow = gvAgensi.Rows(index)


            If e.CommandName = "EditRow" Then

                'Dim kod As String = ViewState("kod")
                txtkod.Text = selectedRow.Cells(0).Text.ToString
                txtno.Text = selectedRow.Cells(1).Text.ToString
                txtButir.Text = selectedRow.Cells(2).Text.ToString

                strSql = $"SELECT Alamat_1,Alamat_2,Bandar,Poskod,Kod_Negeri,Kod_Negara,No_Tel_1,No_Tel_2,No_Faks,Kod_Bank,No_Akaun,Emel from SMKB_Gaji_Kerajaan WHERE Kod_Kerajaan = '{txtkod.Text}';"
                Dim ds = dbconn.fSelectCommand(strSql)

                Using dt = ds.Tables(0)
                    If dt.Rows.Count > 0 Then
                        txtAlamat1.Text = dt.Rows(0)("Alamat_1").ToString
                        txtAlamat2.Text = dt.Rows(0)("Alamat_2").ToString
                        txtbandar.Text = dt.Rows(0)("Bandar").ToString
                        txtposkod.Text = dt.Rows(0)("Poskod").ToString
                        txtnotel1.Text = dt.Rows(0)("No_Tel_1").ToString
                        txtnotel2.Text = dt.Rows(0)("No_Tel_2").ToString
                        txtnofaks.Text = dt.Rows(0)("No_Faks").ToString

                    End If
                End Using


                ViewState("SaveMode") = "2"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Pop", "ShowPopup('2');", True)

            ElseIf e.CommandName = "DeleteRow" Then
                ViewState("SaveMode") = "3"
                Dim kod As String = selectedRow.Cells(0).Text.ToString

                'If fCheckMaster(jenis) = False Then
                strSql = "delete from SMKB_Gaji_Kerajaan where Kod_Kerajaan = '" & kod & "'"
                dbconn.sConnBeginTrans()
                If dbconn.fUpdateCommand(strSql) > 0 Then
                    dbconn.sConnCommitTrans()

                    lblModalMessaage.Text = "Rekod telah dipadam!" 'message di modal
                    ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
                    fBindGvAgensi()
                Else
                    dbconn.sConnRollbackTrans()
                    lblModalMessaage.Text = "Rekod gagal dipadam!" 'message di modal
                    ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
                End If

                'Else

                '    lblModalMessaage.Text = "Rekod tidak dapat dipadam! Terdapat rekod potongan ini dalam master gaji!" 'message di modal
                '    ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
                'End If

            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Function fCheckMaster(ByVal strKod As String) As Boolean
        Dim strSql = "select count(*) from smkb_gaji_master  where kod_trans = '" & strKod & "'"
        Dim intCnt As Integer = dbconn.fSelectCount(strSql)
        If intCnt < 1 Then
            Return False
        Else
            Return True
        End If

    End Function
    Protected Sub lbtnSave_Click(sender As Object, e As EventArgs) Handles lbtnSave.ServerClick
        Dim strSql As String
        Dim dbconn As New DBKewConn
        Try
            Dim strNo As String = Trim(txtno.Text.ToUpper.TrimEnd)
            Dim strKod As String = Trim(txtkod.Text.ToUpper.TrimEnd)
            Dim strButir As String = Trim(txtButir.Text.ToUpper.TrimEnd)
            Dim stralamat1 As String = Trim(txtAlamat1.Text.ToUpper.TrimEnd)
            Dim stralamat2 As String = Trim(txtAlamat2.Text.ToUpper.TrimEnd)
            Dim strposkod As String = Trim(txtposkod.Text.ToUpper.TrimEnd)
            Dim strbandar As String = Trim(txtbandar.Text.ToUpper.TrimEnd)
            Dim strnotel1 As String = Trim(txtnotel1.Text.ToUpper.TrimEnd)
            Dim strnotel2 As String = Trim(txtnotel2.Text.ToUpper.TrimEnd)
            Dim strnegeri As String = Trim(txtnegeri.Text.ToUpper.TrimEnd)
            Dim strnegara As String = Trim(txtnegara.Text.ToUpper.TrimEnd)
            Dim stremel As String = Trim(txtemel.Text.ToUpper.TrimEnd)
            Dim strnofaks As String = Trim(txtnofaks.Text.ToUpper.TrimEnd)
            Dim strbank As String = Trim(txtbank.Text.ToUpper.TrimEnd)
            Dim strnoakaun As String = Trim(txtnoakaun.Text.ToUpper.TrimEnd)
            'Dim strDaripada As String = Trim(txtDaripada.Text.ToUpper.TrimEnd)
            Dim valSave As String = hdnSimpan.Value

            strSql = "select count(*) from SMKB_Gaji_Kerajaan where Kod_Kerajaan = '" & strKod & "'"
            If fCheckRec(strSql) = 0 Then
                strSql = "insert into SMKB_Gaji_Kerajaan (Kod_Kerajaan,No_Kerajaan,Nama,Alamat_1,Alamat_2,Bandar,Poskod,Kod_Negeri,Kod_Negara,No_Tel_1,No_Tel_2,No_Faks,Kod_Bank,No_Akaun,Emel) values (@Kod,@noagensi,@nama,@add1,@add2,@bandar,@poskod,@negeri,@negara,@notel1,@notel2,@nofaks,@emel,@bank,@noacc)"
                Dim paramSql() As SqlParameter = {
                        New SqlParameter("@Kod", strKod),
                        New SqlParameter("@noagensi", strNo),
                        New SqlParameter("@nama", strButir),
                        New SqlParameter("@add1", stralamat1),
                        New SqlParameter("@add2", stralamat2),
                        New SqlParameter("@bandar", strbandar),
                        New SqlParameter("@poskod", strposkod),
                        New SqlParameter("@negeri", strnegeri),
                        New SqlParameter("@negara", strnegara),
                        New SqlParameter("@notel1", strnotel1),
                        New SqlParameter("@notel2", strnotel2),
                        New SqlParameter("@nofaks", strnofaks),
                        New SqlParameter("@emel", stremel),
                        New SqlParameter("@bank", strbank),
                        New SqlParameter("@noacc", strnoakaun)
                    }

                dbconn.sConnBeginTrans()
                If dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                    dbconn.sConnCommitTrans()
                    'fGlobalAlert("Rekod baru telah disimpan!", Me.Page, Me.[GetType]())
                    fBindGvAgensi()
                    'fReset()
                    lblModalMessaage.Text = "Rekod baru telah disimpan." 'message di modal
                    ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
                Else
                    dbconn.sConnRollbackTrans()
                    'fGlobalAlert("Ralat!", Me.Page, Me.[GetType]())
                    lblModalMessaage.Text = "Ralat!" 'message di modal
                    ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
                End If
            Else
                strSql = "UPDATE SMKB_Gaji_Kerajaan SET No_Kerajaan=@noagensi,Nama=@nama,alamat_1=@add1,alamat_2=@add2,bandar=@bandar,poskod=@poskod,kod_negeri=@negeri,kod_negara=@negara,no_tel_1=@notel1,no_tel_2=@notel2,No_Faks=@nofaks,emel=@emel,Kod_Bank=@bank,no_akaun=@noacc WHERE Kod_Kerajaan=@strKod"

                Dim paramSql() As SqlParameter = {
                            New SqlParameter("@noagensi", strNo),
                            New SqlParameter("@nama", strButir),
                            New SqlParameter("@add1", stralamat1),
                            New SqlParameter("@add2", stralamat2),
                            New SqlParameter("@bandar", strbandar),
                            New SqlParameter("@poskod", strposkod),
                            New SqlParameter("@negeri", strnegeri),
                            New SqlParameter("@negara", strnegara),
                            New SqlParameter("@notel1", strnotel1),
                            New SqlParameter("@notel2", strnotel2),
                            New SqlParameter("@nofaks", strnofaks),
                            New SqlParameter("@emel", stremel),
                            New SqlParameter("@bank", strbank),
                            New SqlParameter("@noacc", strnoakaun),
                            New SqlParameter("@Kod", strKod)
                        }

                dbconn = New DBKewConn
                dbconn.sConnBeginTrans()
                If dbconn.fInsertCommand(strSql, paramSql) > 0 Then
                    dbconn.sConnCommitTrans()
                    'Alert("Rekod telah dikemaskini!")
                    fBindGvAgensi()
                    lblModalMessaage.Text = "Rekod telah dikemaskini." 'message di modal
                    ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
                Else
                    dbconn.sConnRollbackTrans()
                    'Alert("Rekod gagal dikemaskini!")
                    lblModalMessaage.Text = "Ralat!" 'message di modal
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub
End Class