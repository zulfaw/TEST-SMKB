Imports System
Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Globalization
Imports System.IO
Imports System.Collections
Imports System.Web.HttpFileCollection
Imports Microsoft.Office.Interop.Excel

Public Class CetakResit
    Inherits System.Web.UI.Page
    Dim dbconn As New DBKewConn
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim noResit As String = Request.QueryString("noResit")
        Dim noDok As String = ""
        If noResit IsNot Nothing Then
            fBindTransaksi(noResit, noDok)
            fBindTransaksi_Details(noDok)
        End If
    End Sub

    Private Sub fBindTransaksi(noResit As String, ByRef noDok As String)
        If noResit <> "" Then
            Dim strSql As String
            strSql = $"SELECT 
                        CASE WHEN A.No_Rujukan <> '' THEN A.No_Rujukan ELSE '-' END AS No_Rujukan,
                        A.No_Dok,
                        A.Kod_Penghutang,
                        UPPER(C.Nama_Penghutang) AS Nama_Penghutang,
                        D.Butiran AS Urusniaga,
                        D.Butiran,
                        A.Tujuan,
                        A.Kod_Status_Dok,
                        CASE WHEN A.Tkh_Daftar <> '' THEN FORMAT(A.Tkh_Daftar, 'dd/MM/yyyy') END AS Tkh_Lulus,
                        SUM(A.Jumlah_Bayar) AS Total_Jumlah_Bayar,
                        SUM(B.Jumlah_Diskaun) AS Total_Diskaun,
                        SUM(B.Jumlah_Cukai) AS Total_Cukai,
                        A.Jumlah_Sebenar as JumlahSebenar,
                        A.Jumlah_Bayar as JumlahBayar,
                        SUM(A.Jumlah_Bayar) AS TOTAL_PAID,
                        UPPER(C.Alamat_1) + ',' + UPPER(C.Alamat_2)  AS ALAMAT_1,
                        C.Poskod + ',' + UPPER(G.Butiran)  AS ALAMAT_2,
                        UPPER(D.Butiran) + ',' + UPPER(F.Butiran) AS ALAMAT_3,
                        C.Emel,
                        C.Tel_Bimbit
                        FROM SMKB_Terima_Hdr A
                        INNER JOIN SMKB_Terima_Dtl B ON A.No_Dok = B.No_Dok                            
                        LEFT JOIN SMKB_Penghutang_Master C ON A.Kod_Penghutang = C.Kod_Penghutang
                        LEFT JOIN SMKB_Kod_Urusniaga D ON A.Kod_Urusniaga = D.Kod_Urusniaga
                        LEFT JOIN SMKB_Lookup_Detail E ON C.Kod_Negeri = E.Kod_Detail AND E.Kod = '0002'
                        LEFT JOIN SMKB_Lookup_Detail F ON C.Kod_Negara = F.Kod_Detail AND F.Kod = '0001'
                        LEFT JOIN SMKB_Lookup_Detail G ON C.Bandar = G.Kod_Detail AND G.Kod = '0003' 
                        WHERE A.No_Dok = '{noResit}' AND A.Status = '1'
                        GROUP BY 
                            A.No_Rujukan,
                            A.No_Dok,
                            A.Kod_Penghutang,
                            C.Nama_Penghutang,
                            D.Butiran,
                            A.Tujuan,
                            A.Kod_Status_Dok,
                            A.Tkh_Daftar,
                            A.Jumlah_Sebenar,
                            A.Jumlah_Bayar,
                            C.Alamat_1,
                            C.Alamat_2,
                            C.Poskod,
                            G.Butiran,
                            D.Butiran,
                            F.Butiran,
                            C.Emel,
                            C.Tel_Bimbit
                        ORDER BY A.Tkh_Daftar DESC"
            'strSql = $"select No_Dok,A.No_Bil,A.Kod_Penghutang,UPPER(B.Nama_Penghutang) AS Nama_Penghutang,C.Butiran AS Urusniaga,C.Butiran, A.Kontrak,A.Tujuan,A.Kod_Status_Dok,
            '        CASE WHEN SMKB_Terima_Hdr.Tkh_Daftar <> '' THEN FORMAT(SMKB_Terima_Hdr.Tkh_Daftar, 'dd/MM/yyyy') END AS Tkh_Lulus,
            '        CASE WHEN Tkh_Tamat <> '' THEN FORMAT(Tkh_Tamat, 'dd/MM/yyyy') END AS Tkh_Tamat,
            '        (SELECT SUM(a.Amaun_Terima) FROM SMKB_Terima_Transaksi as a where No_Resit = SMKB_Terima_Hdr.No_Dok and status = 1 ) AS Jumlah, 
            '        (SELECT SUM(a.Amaun_Diskaun) FROM SMKB_Terima_Transaksi as a where No_Resit = SMKB_Terima_Hdr.No_Dok and status = 1 ) AS Diskaun,
            '        (SELECT SUM(a.Amaun_Cukai) FROM SMKB_Terima_Transaksi as a where No_Resit = SMKB_Terima_Hdr.No_Dok and status = 1 ) AS Cukai,
            '        SMKB_Terima_Hdr.Jumlah_Sebenar as JumlahSebenar, SMKB_Terima_Hdr.Jumlah_Bayar as JumlahBayar,
            '        UPPER(B.Alamat_1) +','+UPPER(B.Alamat_2)  AS ALAMAT_1,
            '        B.Poskod+','+UPPER(G.Butiran)  AS ALAMAT_2,
            '        UPPER(D.Butiran)+','+UPPER(F.Butiran) AS ALAMAT_3,B.Emel,B.Tel_Bimbit
            '        FROM SMKB_Terima_Hdr 
            '        LEFT JOIN SMKB_Bil_Hdr A ON SMKB_Terima_Hdr.No_Rujukan=A.No_Bil
            '        LEFT JOIN SMKB_Penghutang_Master B ON A.Kod_Penghutang=B.Kod_Penghutang
            '        LEFT JOIN SMKB_Kod_Urusniaga C ON A.Kod_Urusniaga=C.Kod_Urusniaga
            '        LEFT JOIN SMKB_Lookup_Detail D ON B.Kod_Negeri=D.Kod_Detail AND D.Kod='0002'
            '        LEFT JOIN SMKB_Lookup_Detail F ON B.Kod_Negara=F.Kod_Detail AND F.Kod='0001'
            '        LEFT JOIN SMKB_Lookup_Detail G ON B.Bandar = G.Kod_Detail AND G.Kod = '0003'
            '        WHERE SMKB_Terima_Hdr.No_Dok= '{noResit}' AND A.Status='1'
            '        ORDER BY SMKB_Terima_Hdr.Tkh_Daftar DESC"
            Dim ds = dbconn.fSelectCommand(strSql)
            Using dt = ds.Tables(0)
                If dt.Rows.Count > 0 Then
                    noDok = dt.Rows(0)("No_Dok").ToString
                    Nama.InnerText = dt.Rows(0)("Nama_Penghutang").ToString
                    Alamat1.InnerText = dt.Rows(0)("ALAMAT_1").ToString
                    Alamat2.InnerText = dt.Rows(0)("ALAMAT_2").ToString
                    Alamat3.InnerText = dt.Rows(0)("ALAMAT_3").ToString
                    txtnoResit.InnerText = dt.Rows(0)("No_Dok").ToString
                    txtNoBil.InnerText = dt.Rows(0)("No_Rujukan").ToString
                    tkhBil.InnerText = dt.Rows(0)("Tkh_Lulus").ToString
                    'tkhTamat.InnerText = dt.Rows(0)("Tkh_Tamat").ToString
                    'txtJumlah.InnerText = dt.Rows(0)("Jumlah").ToString
                    'txtJumlahCukai.InnerText = dt.Rows(0)("Cukai").ToString
                    'txtJumlahDiskaun.InnerText = dt.Rows(0)("Diskaun").ToString
                    'txtJumlahSebenar.InnerText = dt.Rows(0)("JumlahSebenar").ToString
                    txtDibayar.InnerText = dt.Rows(0)("JumlahBayar").ToString
                    'txtTotalPaid.InnerText = dt.Rows(0)("TOTAL_PAID").ToString
                    txtTujuan.InnerText = dt.Rows(0)("Tujuan").ToString
                End If
            End Using
        End If
    End Sub
    Private Sub fBindTransaksi_Details(bil As String)
        If bil <> "" Then
            Dim strSql As String
            ' strSql = $"select A.No_Dok,No_Bil,Perkara ,Kuantiti, Kadar_Harga, Jumlah, Diskaun, Cukai
            '             from SMKB_Terima_Hdr A
            '             LEFT JOIN SMKB_Terima_Transaksi B ON A.No_Dok=B.No_Resit
            '             LEFT JOIN SMKB_Bil_Dtl as C ON C.No_Bil=A.No_Rujukan
            '             where No_Bil = '{bil}'
            '             and B.status = 1
            '             order by B.No_Item "

            strSql = $"SELECT A.No_Dok,No_Bil,B.Butiran,b.Kod_Kump_Wang+B.Kod_Operasi+B.Kod_PTJ+B.Kod_Vot+B.Kod_Projek AS COA,Kuantiti, Kadar_Harga, Jumlah, Diskaun, Cukai,Kredit 
                    FROM SMKB_Terima_Hdr A
                    INNER JOIN SMKB_Terima_Dtl AS B ON A.No_Dok=B.No_Dok
                    INNER JOIN SMKB_Bil_Dtl as C ON C.No_Item=B.No_Item AND C.No_Bil=A.No_Rujukan
                    WHERE a.No_Dok = '{bil}' AND Kredit<>'0.00'"
            Dim ds = dbconn.fSelectCommand(strSql)
            If ds IsNot Nothing Then
                Dim dt = ds.Tables(0)

                gvTransaksi.DataSource = dt
                gvTransaksi.DataBind()

            End If
        End If
    End Sub

End Class