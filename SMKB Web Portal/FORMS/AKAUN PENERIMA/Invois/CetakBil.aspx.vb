Imports System
Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Globalization
Imports System.IO
Imports System.Collections
Imports System.Web.HttpFileCollection
Imports Microsoft.Office.Interop.Excel

Public Class CetakBil
    Inherits System.Web.UI.Page
    Dim dbconn As New DBKewConn
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim nobil As String = Request.QueryString("bilid")
        If nobil IsNot Nothing Then
            fBindTransaksi(nobil)
            fBindTransaksi_Details(nobil)
        End If
    End Sub

    Private Sub fBindTransaksi(bil As String)
        If bil <> "" Then
            Dim strSql As String
            strSql = $"SELECT A.No_Bil,A.Kod_Penghutang,UPPER(B.Nama_Penghutang) AS Nama_Penghutang,A.Kod_Urusniaga,C.Butiran, A.Kontrak,A.Tujuan,Kod_Status_Dok,
                                CASE WHEN Tkh_Lulus <> '' THEN FORMAT(Tkh_Lulus, 'dd/MM/yyyy') END AS Tkh_Lulus,
                                CASE WHEN Tkh_Tamat <> '' THEN FORMAT(Tkh_Tamat, 'dd/MM/yyyy') END AS Tkh_Tamat,
                                (SELECT SUM(a.Jumlah) FROM SMKB_Bil_Dtl as a where No_Bil = '{bil}' and status = 1 ) AS Jumlah, 
                                (SELECT SUM(a.Diskaun) FROM SMKB_Bil_Dtl as a where No_Bil = '{bil}' and status = 1 ) AS Diskaun,
                                (SELECT SUM(a.Cukai) FROM SMKB_Bil_Dtl as a where No_Bil = '{bil}' and status = 1 ) AS Cukai,
                                Jumlah as JumlahSebenar,
								UPPER(B.Alamat_1) +','+UPPER(B.Alamat_2)  AS ALAMAT_1,
								B.Poskod+','+UPPER(G.Butiran)  AS ALAMAT_2,
								UPPER(D.Butiran)+','+UPPER(F.Butiran) AS ALAMAT_3,B.Emel,B.Tel_Bimbit
                                FROM SMKB_Bil_Hdr A
                                LEFT JOIN SMKB_Penghutang_Master B ON A.Kod_Penghutang=B.Kod_Penghutang
                                LEFT JOIN SMKB_Kod_Urusniaga C ON A.Kod_Urusniaga=C.Kod_Urusniaga
								LEFT JOIN SMKB_Lookup_Detail D ON B.Kod_Negeri=D.Kod_Detail AND D.Kod='0002'
								LEFT JOIN SMKB_Lookup_Detail F ON B.Kod_Negara=F.Kod_Detail AND F.Kod='0001'
								LEFT JOIN SMKB_Lookup_Detail G ON B.Bandar = G.Kod_Detail AND G.Kod = '0003'
                                WHERE No_Bil = '{bil}'  AND A.Status='1'"
            Dim ds = dbconn.fSelectCommand(strSql)
            Using dt = ds.Tables(0)
                If dt.Rows.Count > 0 Then
                    Nama.InnerText = dt.Rows(0)("Nama_Penghutang").ToString
                    Alamat1.InnerText = dt.Rows(0)("ALAMAT_1").ToString
                    Alamat2.InnerText = dt.Rows(0)("ALAMAT_2").ToString
                    Alamat3.InnerText = dt.Rows(0)("ALAMAT_3").ToString
                    txtnobil.InnerText = dt.Rows(0)("No_Bil").ToString
                    tkhBil.InnerText = dt.Rows(0)("Tkh_Lulus").ToString
                    'tkhTamat.InnerText = dt.Rows(0)("Tkh_Tamat").ToString
                    txtJumlah.InnerText = dt.Rows(0)("Jumlah").ToString
                    txtJumlahCukai.InnerText = dt.Rows(0)("Cukai").ToString
                    txtJumlahDiskaun.InnerText = dt.Rows(0)("Diskaun").ToString
                    txtJumlahSebenar.InnerText = dt.Rows(0)("JumlahSebenar").ToString
                End If
            End Using
        End If
    End Sub
    Private Sub fBindTransaksi_Details(bil As String)
        If bil <> "" Then
            Dim strSql As String
            strSql = $"select a.No_Bil,Perkara ,Kuantiti, Kadar_Harga, Jumlah, Diskaun, Cukai
                                from SMKB_Bil_Dtl as a
                                where No_Bil = '{bil}'
                                and status = 1
                                order by No_Item"
            Dim ds = dbconn.fSelectCommand(strSql)
            If ds IsNot Nothing Then
                Dim dt = ds.Tables(0)

                gvTransaksi.DataSource = dt
                gvTransaksi.DataBind()

            End If
        End If
    End Sub

End Class