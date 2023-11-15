Imports System.Data.SqlClient
Public Class Kadar_Eot
    Inherits System.Web.UI.Page


    Public dsKodKadar As New DataSet
    Public dvKodKadar As New DataView

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not IsPostBack Then
                fBindgrdOT()
                fNcmbVotTetap()
                fNcmbVotBTetap()
                fnCounterKod()


            End If
        Catch ex As Exception

        End Try

    End Sub
    Private Function NoAkhirEOT()

        Dim indexMT As Integer = 0

        Dim strSql = $"SELECT MAX(KOD) AS NoAKhir from SMKB_GAJI_OT"
        Dim dbconn As New DBKewConn
        Using dt = dbconn.fSelectCommandDt(strSql)
            If dt.Rows.Count = 1 Then
                indexMT = dt.Rows(0)("NoAKhir") + 1
            Else
                indexMT = 1
            End If
        End Using

        Return indexMT

    End Function

    Private Function fnCounterKod()
        Dim indx = NoAkhirEOT()
        txtKod.Value = indx.ToString

    End Function


    Private Function fBindgrdOT()


        Try
            Dim dt As New DataTable
            dt = fCreateDtKadar()

            If dt.Rows.Count = 0 Then
                grdOT.DataSource = New List(Of String)
            Else
                grdOT.DataSource = dt
            End If
            grdOT.DataBind()
            'lblJumRekod.InnerText = dt.Rows.Count


            'Required for jQuery DataTables to work.
            grdOT.UseAccessibleHeader = True
            grdOT.HeaderRow.TableSection = TableRowSection.TableHeader

        Catch ex As Exception

        End Try
    End Function

    Private Function fCreateDtKadar() As DataTable

        Try



            Dim dt As New DataTable
            dt.Columns.Add("kod", GetType(String))
            dt.Columns.Add("Butiran", GetType(String))
            dt.Columns.Add("kadar", GetType(String))
            dt.Columns.Add("Min_OT", GetType(String))
            dt.Columns.Add("Max_OT", GetType(String))
            dt.Columns.Add("Kira_Kwsp", GetType(String))
            dt.Columns.Add("Kira_Perkeso", GetType(String))
            dt.Columns.Add("Kira_Cukai", GetType(String))
            dt.Columns.Add("Kira_Pencen", GetType(String))
            dt.Columns.Add("Vot_Tetap", GetType(String))
            dt.Columns.Add("Vot_Bukan_Tetap", GetType(String))

            Dim strKodKadar As String
            Dim strButiranKadar As String
            Dim strkadar As String
            Dim strMinOT As String
            Dim strMaxOT As String
            Dim strKwsp As String
            Dim strPerkeso As String
            Dim strPencen As String
            Dim strCukai As String
            Dim strVotTetap As String
            Dim strVotBTetap As String
            Dim KwspStatus As String
            Dim PerkesoStatus As String
            Dim CukaiStatus As String
            Dim PencenStatus As String

            Dim strSql As String = "select kod, Butiran, kadar, Min_OT, Max_OT, Kira_Kwsp, Kira_Perkeso, Kira_Cukai, Kira_Pencen, Vot_Tetap, Vot_Bukan_Tetap from SMKB_Gaji_OT order by kod"

            Dim dbconn As New DBKewConn
            dsKodKadar = dbconn.fSelectCommand(strSql)
            dvKodKadar = New DataView(dsKodKadar.Tables(0))

            For i As Integer = 0 To dsKodKadar.Tables(0).Rows.Count - 1




                strKodKadar = dsKodKadar.Tables(0).Rows(i)(0).ToString
                strButiranKadar = dsKodKadar.Tables(0).Rows(i)(1).ToString
                strkadar = dsKodKadar.Tables(0).Rows(i)(2).ToString
                strMinOT = dsKodKadar.Tables(0).Rows(i)(3).ToString
                strMaxOT = dsKodKadar.Tables(0).Rows(i)(4).ToString
                KwspStatus = dsKodKadar.Tables(0).Rows(i)(5).ToString
                PerkesoStatus = dsKodKadar.Tables(0).Rows(i)(6).ToString
                CukaiStatus = dsKodKadar.Tables(0).Rows(i)(7).ToString
                PencenStatus = dsKodKadar.Tables(0).Rows(i)(8).ToString

                If KwspStatus = True Then
                    strKwsp = "Aktif"
                Else
                    strKwsp = "Tidak Aktif"
                End If
                If PerkesoStatus = True Then
                    strPerkeso = "Aktif"
                Else
                    strPerkeso = "Tidak Aktif"
                End If
                If CukaiStatus = True Then
                    strCukai = "Aktif"
                Else
                    strCukai = "Tidak Aktif"
                End If
                If PencenStatus = True Then
                    strPencen = "Aktif"
                Else
                    strPencen = "Tidak Aktif"
                End If
                strVotTetap = dsKodKadar.Tables(0).Rows(i)(9).ToString
                strVotBTetap = dsKodKadar.Tables(0).Rows(i)(10).ToString

                dt.Rows.Add(strKodKadar, strButiranKadar, strkadar, strMinOT, strMaxOT, strKwsp, strPerkeso, strCukai, strPencen, strVotTetap, strVotBTetap)

            Next

            Return dt

        Catch ex As Exception

        End Try

    End Function
    Private Sub grdOT_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grdOT.RowCommand
        Try
            If e.CommandName = "Select" Then


                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim selectedRow As GridViewRow = grdOT.Rows(index)

                Dim strKod As String = selectedRow.Cells(0).Text
                Dim strButiran As String = selectedRow.Cells(1).Text
                Dim strKadarjam As String = selectedRow.Cells(2).Text
                Dim strMinOT As String = selectedRow.Cells(3).Text
                Dim strMaxOT As String = selectedRow.Cells(4).Text
                Dim strKWSP As String = selectedRow.Cells(5).Text
                Dim strPerkeso As String = selectedRow.Cells(6).Text
                Dim strCukai As String = selectedRow.Cells(7).Text
                Dim strPencen As String = selectedRow.Cells(8).Text
                Dim strVotTetap As String = selectedRow.Cells(9).Text
                Dim strVotBTetap As String = selectedRow.Cells(10).Text
                If strKWSP = "Aktif" Then rblKwsp.SelectedValue = 1 Else rblKwsp.SelectedValue = 0
                If strPerkeso = "Aktif" Then rblPerkeso.SelectedValue = 1 Else rblPerkeso.SelectedValue = 0
                If strCukai = "Aktif" Then rblCukai.SelectedValue = 1 Else rblCukai.SelectedValue = 0
                If strPencen = "Aktif" Then rblPencen.SelectedValue = 1 Else rblPencen.SelectedValue = 0


                txtKod.Value = strKod
                txtButiran.Value = strButiran
                txtKadarEOT.Value = strKadarjam
                txtMax.Value = strMaxOT
                txtMin.Value = strMinOT
                Me.cmbVotBTetap.SelectedValue = strVotBTetap.TrimEnd
                Me.cmbVotTetap.SelectedValue = strVotTetap.TrimEnd
                ViewState("Kod") = strKod

                ClientScript.RegisterStartupScript([GetType](), "alert", "ShowPopup('1');", True)


            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub fNcmbVotTetap()
        Dim strSql As String = $"select kod_vot,Butiran from SMKB_Vot where kod_vot = '14101' AND Kod_Klasifikasi = 'D' order by kod_vot"



        Dim ds As New DataSet
        Dim dbconn As New DBKewConn
        Using dt = dbconn.fSelectCommandDt(strSql)

            cmbVotTetap.DataSource = dt
            cmbVotTetap.DataTextField = "Butiran"
            cmbVotTetap.DataValueField = "kod_vot"
            cmbVotTetap.DataBind()

            cmbVotTetap.Items.Insert(0, New ListItem("-SILA PILIH-", "0"))

            cmbVotTetap.SelectedIndex = 0
        End Using
    End Sub

    Private Sub fNcmbVotBTetap()
        Dim strSql As String = $"select kod_vot,Butiran from SMKB_Vot where kod_vot = '29302' AND Kod_Klasifikasi = 'D' order by kod_vot"

        Dim ds As New DataSet
        Dim dbconn As New DBKewConn
        Using dt = dbconn.fSelectCommandDt(strSql)

            cmbVotBTetap.DataSource = dt
            cmbVotBTetap.DataTextField = "Butiran"
            cmbVotBTetap.DataValueField = "kod_vot"
            cmbVotBTetap.DataBind()

            cmbVotBTetap.Items.Insert(0, New ListItem("-SILA PILIH-", "0"))

            cmbVotBTetap.SelectedIndex = 0
        End Using
    End Sub


    'Private Sub fNListSenarai()
    '    Try

    '        Dim strSql As String = $"Select kod,Butiran,kadar,Min_OT,Max_OT,Kira_Kwsp,Kira_Perkeso,Kira_Cukai,Kira_Pencen,Vot_Tetap,Vot_Bukan_Tetap from SMKB_Gaji_OT order by kod"
    '        ' dbcomm = New oledbCommand(strSql, dbconn)
    '        Using dt = dbconn.fSelectCommandDt(strSql)
    '            Dim ds As dataset = GetData(strSql)
    '            If (ds.Tables.Count > 0) Then
    '                grdOT.DataSource = dt
    '                grdOT.DataBind()
    '            End If
    '        End Using
    '    Catch ex As Exception

    '    End Try

    'End Sub

    Private Sub grdOT_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles grdOT.PageIndexChanging
        grdOT.PageIndex = e.NewPageIndex
        fBindgrdOT()
    End Sub


    Protected Sub lbtnSimpan_Click(sender As Object, e As EventArgs) Handles lbtnSimpan.ServerClick

        Dim dbconn As New DBKewConn
        Dim blnSuccess As Boolean = True
        Try
            Dim intKod = CInt(Trim(txtKod.Value.TrimEnd))
            Dim strButiran As String = txtButiran.Value.TrimEnd
            Dim strKadar As String = txtKadarEOT.Value.TrimEnd
            Dim strMinOT As String = txtMin.Value.TrimEnd
            Dim strMaxOT As String = txtMax.Value
            Dim strKirakwsp As String = rblKwsp.SelectedValue
            Dim strkiracukai As String = rblCukai.SelectedValue
            Dim strkiraperkeso As String = rblPerkeso.SelectedValue
            Dim strkirapencen As String = rblPencen.SelectedValue
            Dim strcmbVotTetap As String = cmbVotTetap.SelectedValue
            Dim strcmbVotBTetap As String = cmbVotBTetap.SelectedValue
            Dim strSql As String
            dbconn.sConnBeginTrans()

            If intKod = ViewState("Kod") Then
                strSql = "update SMKB_GAJI_OT set Butiran = @Butiran ,Kadar = @Kadar, Min_OT = @Min_OT, Max_OT = @Max_OT, 
                            kira_kwsp = @kira_kwsp , kira_Cukai = @kira_cukai,
                            kira_perkeso = @kira_perkeso ,kira_pencen =  @kira_pencen,
                            Vot_Tetap = @Vot_Tetap , Vot_Bukan_Tetap = @Vot_Bukan_Tetap where kod = @kod"

                Dim paramSQL2() As SqlParameter = {
                        New SqlParameter("@Butiran", strButiran),
                        New SqlParameter("@Kadar", strKadar),
                        New SqlParameter("@Min_OT", strMinOT),
                        New SqlParameter("@Max_OT", strMaxOT),
                        New SqlParameter("@kira_kwsp", strKirakwsp),
                        New SqlParameter("@kira_cukai", strkiracukai),
                        New SqlParameter("@kira_perkeso", strkiraperkeso),
                        New SqlParameter("@kira_pencen", strkirapencen),
                        New SqlParameter("@Vot_Tetap", strcmbVotTetap),
                        New SqlParameter("@Vot_Bukan_Tetap", strcmbVotBTetap),
                        New SqlParameter("@kod", ViewState("Kod"))
                }

                If Not dbconn.fUpdateCommand(strSql, paramSQL2) > 0 Then
                    dbconn.sConnRollbackTrans()
                    blnSuccess = False
                    Exit Try

                Else
                    lblModalMessaage.Text = "Rekod telah dikemaskini." 'message di modal
                    ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
                    dbconn.sConnCommitTrans()
                    fBindgrdOT()

                End If

            ElseIf ViewState("Kod") = String.Empty Then
                strSql = "insert into SMKB_GAJI_OT(kod,Butiran,Kadar,Min_OT,Max_OT,kira_kwsp,kira_Cukai,kira_perkeso,kira_pencen,Vot_Tetap,Vot_Bukan_Tetap) values " _
                 & "(@kod,@Butiran,@Kadar,@Min_OT,@Max_OT,@kira_kwsp,@kira_cukai,@kira_perkeso,@kira_pencen,@Vot_Tetap,@Vot_Bukan_Tetap)"

                Dim paramSQL2() As SqlParameter = {
                        New SqlParameter("@kod", intKod),
                        New SqlParameter("@Butiran", strButiran),
                        New SqlParameter("@Kadar", strKadar),
                        New SqlParameter("@Min_OT", strMinOT),
                        New SqlParameter("@Max_OT", strMaxOT),
                        New SqlParameter("@kira_kwsp", strKirakwsp),
                        New SqlParameter("@kira_cukai", strkiracukai),
                        New SqlParameter("@kira_perkeso", strkiraperkeso),
                        New SqlParameter("@kira_pencen", strkirapencen),
                        New SqlParameter("@Vot_Tetap", strcmbVotTetap),
                        New SqlParameter("@Vot_Bukan_Tetap", strcmbVotBTetap)
                }



                If Not dbconn.fInsertCommand(strSql, paramSQL2) > 0 Then
                    dbconn.sConnRollbackTrans()
                    blnSuccess = False
                    Exit Try

                Else
                    lblModalMessaage.Text = "Rekod telah disimpan" 'message di modal
                    ClientScript.RegisterStartupScript([GetType](), "alert", "SaveSucces();", True)
                    dbconn.sConnCommitTrans()
                    fBindgrdOT()

                End If

            End If
        Catch ex As Exception
            blnSuccess = False
        End Try
    End Sub

End Class