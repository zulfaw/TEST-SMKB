Imports System.ComponentModel
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports Newtonsoft.Json
Imports System.Web.Script.Services
Imports System.Web.Script.Serialization
Imports System.Data.SqlClient
Imports System.Collections.Generic


Imports System.Drawing
Imports System.Globalization

Imports System.Net
Imports System.Net.Mail
Imports System.Web.Configuration
Imports Newtonsoft.Json.Linq
Imports Org.BouncyCastle.Asn1.Sec


' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class Laporan_WS
    Inherits System.Web.Services.WebService

    Dim sqlcmd As SqlCommand
    Dim sqlcon As SqlConnection
    Dim sqlread As SqlDataReader
    Dim dt As DataTable


    Public Function HelloWorld() As String
        Return "Hello World"
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function LoadRecord_CukaiBulanan(bulan As String, tahun As String, syarikat As String, ptj As String) As String
        Dim resp As New ResponseRepository
        If bulan = "" Or tahun = "" Or syarikat = "" Or ptj = "" Then
            Return JsonConvert.SerializeObject(New DataTable)
        End If
        Session("bulan") = bulan
        Session("tahun") = tahun
        Session("syarikat") = syarikat
        Session("ptj") = ptj
        dt = GetRecord_CukaiBulanan(bulan, tahun, syarikat, ptj)
        Dim totalRecords As Integer = dt.Rows.Count

        Return JsonConvert.SerializeObject(dt)
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Private Function GetRecord_CukaiBulanan(bulan As String, tahun As String, syarikat As String, ptj As String) As DataTable
        Dim db = New DBKewConn
        Dim dt As New DataTable

        'Dim connectionString As String = "server=v-sql12.utem.edu.my;database=DbKewangan;uid=smkb;pwd=smkb*pwd;"
        Dim connectionString As String = "server=devmis12.utem.edu.my;database=DbKewanganV4;uid=smkb;pwd=Smkb@Dev2012;"

        Using sqlconn As New SqlConnection(connectionString)
            Dim cmd As New SqlCommand
            sqlconn.Open()

            Dim query As String

            If ptj = "00" Then
                query = "SELECT A.No_Staf,C.MS01_Nama AS Nama,B.No_Cukai AS No_Cukai,C.MS01_KpB AS No_KP, B.Kategori_Cukai,A.Amaun,A.Jenis_Trans
                         FROM SMKB_Gaji_Lejar A
                         LEFT JOIN SMKB_Gaji_Staf B ON B.No_Staf = A.No_Staf
                         LEFT JOIN [DEVMIS\SQL_INS01].dbStaf.dbo.MS01_Peribadi AS C ON C.MS01_NoStaf = A.No_Staf
                         WHERE A.Jenis_Trans = 'T' AND A.Bulan = @bulan AND A.Tahun = @tahun"
            ElseIf ptj = "-0000" Then
                query = "SELECT A.No_Staf,C.MS01_Nama AS Nama,B.No_Cukai AS No_Cukai,C.MS01_KpB AS No_KP, B.Kategori_Cukai,A.Amaun,A.Jenis_Trans
                         FROM SMKB_Gaji_Lejar A
                         LEFT JOIN SMKB_Gaji_Staf B ON B.No_Staf = A.No_Staf
                         LEFT JOIN [DEVMIS\SQL_INS01].dbStaf.dbo.MS01_Peribadi AS C ON C.MS01_NoStaf = A.No_Staf
                         WHERE A.Jenis_Trans = 'T' AND A.Bulan = @bulan AND A.Tahun = @tahun AND A.Kod_PTJ= '-'"
            Else
                query = "SELECT A.No_Staf,C.MS01_Nama AS Nama,B.No_Cukai AS No_Cukai,C.MS01_KpB AS No_KP, B.Kategori_Cukai,A.Amaun,A.Jenis_Trans
                         FROM SMKB_Gaji_Lejar A
                         LEFT JOIN SMKB_Gaji_Staf B ON B.No_Staf = A.No_Staf
                         LEFT JOIN [DEVMIS\SQL_INS01].dbStaf.dbo.MS01_Peribadi AS C ON C.MS01_NoStaf = A.No_Staf
                         WHERE A.Jenis_Trans = 'T' AND A.Bulan = @bulan AND A.Tahun = @tahun AND A.Kod_PTJ= @ptj"
            End If


            cmd.Connection = sqlconn
            cmd.CommandText = query

            cmd.Parameters.Add(New SqlParameter("@tahun", tahun))
            cmd.Parameters.Add(New SqlParameter("@bulan", bulan))
            cmd.Parameters.Add(New SqlParameter("@ptj", ptj))
            dt.Load(cmd.ExecuteReader())
            Return dt
        End Using
    End Function

    <WebMethod(EnableSession:=True)>
	<ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
	Public Function LoadRecord_RingkasanGaji(bulan As String, tahun As String, syarikat As String, ptj As String) As String
		Dim resp As New ResponseRepository
		If bulan = "" Or tahun = "" Or syarikat = "" Or ptj = "" Then
			Return JsonConvert.SerializeObject(New DataTable)
		End If
		Session("bulan") = bulan
		Session("tahun") = tahun
		Session("syarikat") = syarikat
		Session("ptj") = ptj
		dt = GetRecord_RingkasanGaji(bulan, tahun, syarikat, ptj)
		Dim totalRecords As Integer = dt.Rows.Count

		Return JsonConvert.SerializeObject(dt)
	End Function

	<WebMethod()>
	<ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
	Private Function GetRecord_RingkasanGaji(bulan As String, tahun As String, syarikat As String, ptj As String) As DataTable
		Dim db = New DBKewConn
		Dim dt As New DataTable

		'Dim connectionString As String = "server=v-sql12.utem.edu.my;database=DbKewangan;uid=smkb;pwd=smkb*pwd;"
		Dim connectionString As String = "server=devmis12.utem.edu.my;database=DbKewanganV4;uid=smkb;pwd=Smkb@Dev2012;"

		Using sqlconn As New SqlConnection(connectionString)
			Dim cmd As New SqlCommand
			sqlconn.Open()

			Dim query As String
			Dim optWhere As String

			If ptj = "00" Then
				optWhere &= ""
			ElseIf ptj = "-0000" Then
				optWhere &= "AND A.Kod_PTJ = '-'"
			Else
				optWhere &= "AND A.Kod_PTJ = @ptj"
			End If

			query = "SELECT No_Staf,Nama,
	                            COALESCE([G],0.00) AS G,
	                            COALESCE([B],0.00) AS B,
	                            COALESCE([E],0.00) AS E,
	                            COALESCE([O],0.00) AS O,
	                            (COALESCE([G],0.00)+COALESCE([B],0.00)+COALESCE([E],0.00)+COALESCE([O],0.00)) AS Gaji_Kasar,
	                            COALESCE([P],0.00) AS P,
	                            COALESCE([C],0.00) AS C,
	                            COALESCE([KWSP],0.00) AS KWSP,
	                            COALESCE([SOCP],0.00) AS SOCP,
	                            COALESCE([T],0.00) AS T,
	                            (
			                        (COALESCE([G],0.00)+COALESCE([B],0.00)+COALESCE([E],0.00)+COALESCE([O],0.00)) -
			                        (COALESCE([P],0.00)+COALESCE([C],0.00)+COALESCE([KWSP],0.00)+COALESCE([SOCP],0.00)+COALESCE([T],0.00))
		                        ) AS Gaji_Bersih,
	                            COALESCE([KWSM],0.00) AS KWSM, 
		                        COALESCE([SOCM],0.00) AS SOCM
                        FROM (
	                        SELECT A.No_Staf,B.MS01_Nama AS Nama,A.Jenis_Trans,A.Amaun,A.Bulan,A.Tahun,KWSM.KWSM,KWSP.KWSP,SOCM.SOCM,SOCP.SOCP
	                        FROM SMKB_Gaji_Lejar A
	                        LEFT JOIN [DEVMIS\SQL_INS01].dbStaf.dbo.MS01_Peribadi AS B ON B.MS01_NoStaf = A.No_Staf
	                        LEFT JOIN (
		                        SELECT No_Staf, Amaun AS KWSM FROM SMKB_Gaji_Lejar WHERE Bulan = @bulan AND Tahun = @tahun AND Kod_Trans = 'KWSM'
	                        ) AS KWSM ON KWSM.No_Staf = A.No_Staf
	                        LEFT JOIN (
		                        SELECT No_Staf, Amaun AS KWSP FROM SMKB_Gaji_Lejar WHERE Bulan = @bulan AND Tahun = @tahun AND Kod_Trans = 'KWSP'
	                        ) AS KWSP ON KWSP.No_Staf = A.No_Staf
	                        LEFT JOIN (
		                        SELECT No_Staf, Amaun AS SOCM FROM SMKB_Gaji_Lejar WHERE Bulan = @bulan AND Tahun = @tahun AND Kod_Trans = 'SOCM'
	                        ) AS SOCM ON SOCM.No_Staf = A.No_Staf
	                        LEFT JOIN (
		                        SELECT No_Staf, Amaun AS SOCP FROM SMKB_Gaji_Lejar WHERE Bulan = @bulan AND Tahun = @tahun AND Kod_Trans = 'SOCP'
	                        ) AS SOCP ON SOCP.No_Staf = A.No_Staf
	                        WHERE A.Bulan = @bulan AND A.Tahun = @tahun " + optWhere + "
                        ) AS SourceTable
                        PIVOT(
	                        SUM(Amaun)
	                        FOR Jenis_Trans IN([B],[C],[E],[G],[O],[P],[T])
                        ) AS PivotTable;"


			cmd.Connection = sqlconn
			cmd.CommandText = query

			cmd.Parameters.Add(New SqlParameter("@tahun", tahun))
			cmd.Parameters.Add(New SqlParameter("@bulan", bulan))
			cmd.Parameters.Add(New SqlParameter("@ptj", ptj))
			dt.Load(cmd.ExecuteReader())
			Return dt
		End Using
	End Function

	<WebMethod(EnableSession:=True)>
	<ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
	Public Function LoadRecord_Transaksi(bulan As String, tahun As String, syarikat As String, ptj As String) As String
		Dim resp As New ResponseRepository
		If bulan = "" Or tahun = "" Or syarikat = "" Or ptj = "" Then
			Return JsonConvert.SerializeObject(New DataTable)
		End If
		Session("bulan") = bulan
		Session("tahun") = tahun
		Session("syarikat") = syarikat
		Session("ptj") = ptj
		dt = GetRecord_Transaksi(bulan, tahun, syarikat, ptj)
		Dim totalRecords As Integer = dt.Rows.Count

		Return JsonConvert.SerializeObject(dt)
	End Function

	<WebMethod()>
	<ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
	Private Function GetRecord_Transaksi(bulan As String, tahun As String, syarikat As String, ptj As String) As DataTable
		Dim db = New DBKewConn
		Dim dt As New DataTable

		'Dim connectionString As String = "server=v-sql12.utem.edu.my;database=DbKewangan;uid=smkb;pwd=smkb*pwd;"
		Dim connectionString As String = "server=devmis12.utem.edu.my;database=DbKewanganV4;uid=smkb;pwd=Smkb@Dev2012;"

		Using sqlconn As New SqlConnection(connectionString)
			Dim cmd As New SqlCommand
			sqlconn.Open()

			Dim query As String
			Dim optWhere As String

			If ptj = "00" Then
				optWhere &= ""
			ElseIf ptj = "-0000" Then
				optWhere &= "AND A.Kod_PTJ = '-'"
			Else
				optWhere &= "AND A.Kod_PTJ = @ptj"
			End If

			query = "SELECT No_Staf,Nama,KP,
						COALESCE([G],0.00) AS Gaji_Pokok,
						COALESCE([B],0.00) AS Bonus,
						COALESCE([E],0.00) AS Elaun,
						COALESCE([O],0.00) AS OT,
						(COALESCE([G],0.00)+COALESCE([B],0.00)+COALESCE([E],0.00)+COALESCE([O],0.00)) AS Gaji_Kasar,
						COALESCE([P],0.00) AS Potongan,
						COALESCE([C],0.00) AS Cuti,No_KWSP,
						COALESCE([KWSP],0.00) AS KWSP,
						COALESCE([KWSM],0.00) AS KWSM,
						No_Cukai, Kategori_Cukai,
						COALESCE([T],0.00) AS Cukai,
						(
							(COALESCE([G],0.00)+COALESCE([B],0.00)+COALESCE([E],0.00)+COALESCE([O],0.00)) -
							(COALESCE([P],0.00)+COALESCE([C],0.00)+COALESCE([KWSP],0.00)+COALESCE([SOCP],0.00)+COALESCE([T],0.00))
						) AS Gaji_Bersih,
						No_Perkeso,
						COALESCE([SOCP],0.00) AS SOCP,
						COALESCE([SOCM],0.00) AS SOCM,No_Pencen,
						COALESCE([N],0.00) AS Pencen
					FROM(
						SELECT A.No_Staf, C.MS01_Nama AS Nama, C.MS01_KpB AS KP, A.Jenis_Trans,A.Amaun,KWSM.KWSM,KWSP.KWSP,SOCM.SOCM,SOCP.SOCP, B.No_Cukai, B.Kategori_Cukai, C.MS01_NoPencen AS No_Pencen, C.MS01_NoKWSP AS No_KWSP, B.No_Perkeso
						FROM SMKB_Gaji_Lejar A
						LEFT JOIN SMKB_Gaji_Staf B ON B.No_Staf = A.No_Staf
						LEFT JOIN [DEVMIS\SQL_INS01].dbStaf.dbo.MS01_Peribadi AS C ON C.MS01_NoStaf = A.No_Staf
						LEFT JOIN (SELECT No_Staf, Amaun AS KWSM FROM SMKB_Gaji_Lejar WHERE Bulan = @bulan AND Tahun = @tahun AND Kod_Trans = 'KWSM') AS KWSM ON KWSM.No_Staf = A.No_Staf
						LEFT JOIN (SELECT No_Staf, Amaun AS KWSP FROM SMKB_Gaji_Lejar WHERE Bulan = @bulan AND Tahun = @tahun AND Kod_Trans = 'KWSP') AS KWSP ON KWSP.No_Staf = A.No_Staf
						LEFT JOIN (SELECT No_Staf, Amaun AS SOCM FROM SMKB_Gaji_Lejar WHERE Bulan = @bulan AND Tahun = @tahun AND Kod_Trans = 'SOCM') AS SOCM ON SOCM.No_Staf = A.No_Staf
						LEFT JOIN (SELECT No_Staf, Amaun AS SOCP FROM SMKB_Gaji_Lejar WHERE Bulan = @bulan AND Tahun = @tahun AND Kod_Trans = 'SOCP') AS SOCP ON SOCP.No_Staf = A.No_Staf
						WHERE A.Bulan = @bulan AND A.Tahun = @tahun " + optWhere + "
					)AS SourceTable
					PIVOT(
					Sum(Amaun)
					FOR Jenis_Trans IN ([B],[C],[E],[G],[O],[P],[T],[N])
					) AS PivotTable;"

			'query = "SELECT No_Staf,Nama,KP,No_Cukai, Kategori_Cukai,
			'			COALESCE([T],0.00) AS Cukai,
			'			COALESCE([G],0.00) AS Gaji,
			'			COALESCE([O],0.00) AS OT, No_Pencen,
			'			COALESCE([N],0.00) AS Pencen,No_KWSP,
			'			COALESCE([KWSP],0.00) AS KWSP,
			'			COALESCE([KWSM],0.00) AS KWSM,No_Perkeso,
			'			COALESCE([SOCP],0.00) AS SOCP,
			'			COALESCE([SOCM],0.00) AS SOCM
			'		FROM(
			'			SELECT A.No_Staf, C.MS01_Nama AS Nama, C.MS01_KpB AS KP, A.Jenis_Trans,A.Amaun,KWSM.KWSM,KWSP.KWSP,SOCM.SOCM,SOCP.SOCP, B.No_Cukai, B.Kategori_Cukai, C.MS01_NoPencen AS No_Pencen, C.MS01_NoKWSP AS No_KWSP, B.No_Perkeso
			'			FROM SMKB_Gaji_Lejar A
			'			LEFT JOIN SMKB_Gaji_Staf B ON B.No_Staf = A.No_Staf
			'			LEFT JOIN [DEVMIS\SQL_INS01].dbStaf.dbo.MS01_Peribadi AS C ON C.MS01_NoStaf = A.No_Staf
			'			LEFT JOIN (SELECT No_Staf, Amaun AS KWSM FROM SMKB_Gaji_Lejar WHERE Bulan = @bulan AND Tahun = @tahun AND Kod_Trans = 'KWSM') AS KWSM ON KWSM.No_Staf = A.No_Staf
			'			LEFT JOIN (SELECT No_Staf, Amaun AS KWSP FROM SMKB_Gaji_Lejar WHERE Bulan = @bulan AND Tahun = @tahun AND Kod_Trans = 'KWSP') AS KWSP ON KWSP.No_Staf = A.No_Staf
			'			LEFT JOIN (SELECT No_Staf, Amaun AS SOCM FROM SMKB_Gaji_Lejar WHERE Bulan = @bulan AND Tahun = @tahun AND Kod_Trans = 'SOCM') AS SOCM ON SOCM.No_Staf = A.No_Staf
			'			LEFT JOIN (SELECT No_Staf, Amaun AS SOCP FROM SMKB_Gaji_Lejar WHERE Bulan = @bulan AND Tahun = @tahun AND Kod_Trans = 'SOCP') AS SOCP ON SOCP.No_Staf = A.No_Staf
			'			WHERE A.Bulan = @bulan AND A.Tahun = @tahun " + optWhere + "
			'		)AS SourceTable
			'		PIVOT(
			'		Sum(Amaun)
			'		FOR Jenis_Trans IN ([T],[G],[O],[N])
			'		) AS PivotTable;
			'		"

			cmd.Connection = sqlconn
			cmd.CommandText = query

			cmd.Parameters.Add(New SqlParameter("@tahun", tahun))
			cmd.Parameters.Add(New SqlParameter("@bulan", bulan))
			cmd.Parameters.Add(New SqlParameter("@ptj", ptj))
			'cmd.Parameters.Add(New SqlParameter("@nostaf", nostaf))
			dt.Load(cmd.ExecuteReader())
			Return dt
		End Using
	End Function

	<WebMethod(EnableSession:=True)>
	<ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
	Public Function LoadRecord_TransaksiPotonganBulanan(bulan As String, tahun As String, syarikat As String, ptj As String, kodpotongan As String) As String
		Dim resp As New ResponseRepository
		If bulan = "" Or tahun = "" Or syarikat = "" Or ptj = "" Then
			Return JsonConvert.SerializeObject(New DataTable)
		End If
		Session("bulan") = bulan
		Session("tahun") = tahun
		Session("syarikat") = syarikat
		Session("ptj") = ptj
		Session("kodpotongan") = kodpotongan
		dt = GetRecord_TransaksiPotonganBulanan(bulan, tahun, syarikat, ptj, kodpotongan)
		Dim totalRecords As Integer = dt.Rows.Count

		Return JsonConvert.SerializeObject(dt)
	End Function

	<WebMethod()>
	<ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
	Private Function GetRecord_TransaksiPotonganBulanan(bulan As String, tahun As String, syarikat As String, ptj As String, kodpotongan As String) As DataTable
		Dim db = New DBKewConn
		Dim dt As New DataTable

		'Dim connectionString As String = "server=v-sql12.utem.edu.my;database=DbKewangan;uid=smkb;pwd=smkb*pwd;"
		Dim connectionString As String = "server=devmis12.utem.edu.my;database=DbKewanganV4;uid=smkb;pwd=Smkb@Dev2012;"

		Using sqlconn As New SqlConnection(connectionString)
			Dim cmd As New SqlCommand
			sqlconn.Open()

			Dim query As String
			Dim optWhere As String

			If ptj = "00" Then
				ptj = "%"
			ElseIf ptj = "-0000" Then
				ptj = "-"
			Else
				ptj = ptj + "%"
			End If

			If kodpotongan = "00" Then
				kodpotongan = "%"
			Else
				kodpotongan = kodpotongan + "%"
			End If

			query = "DECLARE @DynamicSQL NVARCHAR(MAX);
					DECLARE @ColumnList NVARCHAR(MAX);

					-- Create a comma-separated list of distinct Category values to be used as columns
					SELECT @ColumnList = COALESCE(@ColumnList + ', ', '') + QUOTENAME(Kod_Trans)
					FROM (SELECT DISTINCT Kod_Trans FROM SMKB_Gaji_Lejar WHERE Jenis_Trans = 'P' AND Bulan = @bulan AND Tahun = @tahun AND Kod_PTJ LIKE @ptj AND Kod_Trans LIKE @kodpotongan) AS Categories;

					-- Build the dynamic SQL query to pivot the table
					SET @DynamicSQL = N'
						SELECT *
						FROM (
							SELECT A.No_Staf AS No, B.MS01_Nama AS Nama, A.Kod_Trans, Amaun
							FROM SMKB_Gaji_Lejar A
							LEFT JOIN [DEVMIS\SQL_INS01].dbStaf.dbo.MS01_Peribadi AS B ON B.MS01_NoStaf = A.No_Staf
							WHERE Bulan = @bulan AND Tahun = @tahun AND Kod_PTJ LIKE @ptj AND Kod_Trans LIKE @kodpotongan
						) AS SourceTable
						PIVOT (
							SUM(Amaun)
							FOR Kod_Trans IN (' + @ColumnList + ')
						) AS PivotTable;';

					-- Execute the dynamic SQL query with the new parameter
					EXEC sp_executesql @DynamicSQL, N'@bulan INT, @tahun INT, @ptj NVARCHAR(50), @kodpotongan NVARCHAR(50)', @bulan, @tahun, @ptj, @kodpotongan;"

			cmd.Connection = sqlconn
			cmd.CommandText = query

			cmd.Parameters.Add(New SqlParameter("@tahun", tahun))
			cmd.Parameters.Add(New SqlParameter("@bulan", bulan))
			cmd.Parameters.Add(New SqlParameter("@ptj", ptj))
			cmd.Parameters.Add(New SqlParameter("@kodpotongan", kodpotongan))
			dt.Load(cmd.ExecuteReader())
			Return dt
		End Using
	End Function

	<WebMethod(EnableSession:=True)>
	<ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
	Public Function LoadRecord_TransaksiElaunBulanan(bulan As String, tahun As String, syarikat As String, ptj As String, kodelaun As String) As String
		Dim resp As New ResponseRepository
		If bulan = "" Or tahun = "" Or syarikat = "" Or ptj = "" Then
			Return JsonConvert.SerializeObject(New DataTable)
		End If
		Session("bulan") = bulan
		Session("tahun") = tahun
		Session("syarikat") = syarikat
		Session("ptj") = ptj
		Session("kodelaun") = kodelaun
		dt = GetRecord_TransaksiElaunBulanan(bulan, tahun, syarikat, ptj, kodelaun)
		Dim totalRecords As Integer = dt.Rows.Count

		Return JsonConvert.SerializeObject(dt)
	End Function

	<WebMethod()>
	<ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
	Private Function GetRecord_TransaksiElaunBulanan(bulan As String, tahun As String, syarikat As String, ptj As String, kodelaun As String) As DataTable
		Dim db = New DBKewConn
		Dim dt As New DataTable

		'Dim connectionString As String = "server=v-sql12.utem.edu.my;database=DbKewangan;uid=smkb;pwd=smkb*pwd;"
		Dim connectionString As String = "server=devmis12.utem.edu.my;database=DbKewanganV4;uid=smkb;pwd=Smkb@Dev2012;"

		Using sqlconn As New SqlConnection(connectionString)
			Dim cmd As New SqlCommand
			sqlconn.Open()

			Dim query As String
			Dim optWhere As String

			If ptj = "00" Then
				ptj = "%"
			ElseIf ptj = "-0000" Then
				ptj = "-"
			Else
				ptj = ptj + "%"
			End If

			If kodelaun = "00" Then
				kodelaun = "%"
			Else
				kodelaun = kodelaun + "%"
			End If

			query = "DECLARE @DynamicSQL NVARCHAR(MAX);
					DECLARE @ColumnList NVARCHAR(MAX);

					-- Create a comma-separated list of distinct Category values to be used as columns
					SELECT @ColumnList = COALESCE(@ColumnList + ', ', '') + QUOTENAME(Kod_Trans)
					FROM (SELECT DISTINCT Kod_Trans FROM SMKB_Gaji_Lejar WHERE Jenis_Trans = 'E' AND Bulan = @bulan AND Tahun = @tahun AND Kod_PTJ LIKE @ptj AND Kod_Trans LIKE @kodelaun) AS Categories;

					-- Build the dynamic SQL query to pivot the table
					SET @DynamicSQL = N'
						SELECT *
						FROM (
							SELECT A.No_Staf AS No, B.MS01_Nama AS Nama, A.Kod_Trans, Amaun
							FROM SMKB_Gaji_Lejar A
							LEFT JOIN [DEVMIS\SQL_INS01].dbStaf.dbo.MS01_Peribadi AS B ON B.MS01_NoStaf = A.No_Staf
							WHERE Bulan = @bulan AND Tahun = @tahun AND Kod_PTJ LIKE @ptj AND Kod_Trans LIKE @kodelaun
						) AS SourceTable
						PIVOT (
							SUM(Amaun)
							FOR Kod_Trans IN (' + @ColumnList + ')
						) AS PivotTable;';

					-- Execute the dynamic SQL query with the new parameter
					EXEC sp_executesql @DynamicSQL, N'@bulan INT, @tahun INT, @ptj NVARCHAR(50), @kodelaun NVARCHAR(50)', @bulan, @tahun, @ptj, @kodelaun;"

			cmd.Connection = sqlconn
			cmd.CommandText = query

			cmd.Parameters.Add(New SqlParameter("@tahun", tahun))
			cmd.Parameters.Add(New SqlParameter("@bulan", bulan))
			cmd.Parameters.Add(New SqlParameter("@ptj", ptj))
			cmd.Parameters.Add(New SqlParameter("@kodelaun", kodelaun))
			dt.Load(cmd.ExecuteReader())
			Return dt
		End Using
	End Function

	<WebMethod(EnableSession:=True)>
	<ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
	Public Function LoadRecord_PinjamanBulanan(syarikat As String, ptj As String, kodpinjaman As String) As String
		Dim resp As New ResponseRepository
		'If syarikat = "" Or ptj = "" Then
		'	Return JsonConvert.SerializeObject(New DataTable)
		'End If

		Session("syarikat") = syarikat
		Session("ptj") = ptj
		Session("kodpinjaman") = kodpinjaman

		If kodpinjaman = "PK01" Then
			Session("Pinjaman") = "Kenderaan"
		Else
			Session("Pinjaman") = "Komputer"
		End If

		dt = GetRecord_PinjamanBulanan(syarikat, ptj, kodpinjaman)
		Dim totalRecords As Integer = dt.Rows.Count

		Return JsonConvert.SerializeObject(dt)
	End Function

	<WebMethod()>
	<ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
	Private Function GetRecord_PinjamanBulanan(syarikat As String, ptj As String, kodpinjaman As String) As DataTable
		Dim db = New DBKewConn
		Dim dt As New DataTable

		'Dim connectionString As String = "server=v-sql12.utem.edu.my;database=DbKewangan;uid=smkb;pwd=smkb*pwd;"
		Dim connectionString As String = "server=devmis12.utem.edu.my;database=DbKewanganV4;uid=smkb;pwd=Smkb@Dev2012;"

		Using sqlconn As New SqlConnection(connectionString)
			Dim cmd As New SqlCommand
			sqlconn.Open()

			Dim query As String
			Dim optWhere As String

			If ptj = "00" Then
				ptj = "%"
			ElseIf ptj = "-0000" Then
				ptj = "-"
			Else
				ptj = ptj + "%"
			End If

			query = "SELECT A.Kod_Trans,A.Bulan, A.Tahun,B.No_Trans,A.No_Staf, E.MS01_Nama AS Nama, C.Amaun AS Pinjaman,A.Amaun AS Pot_bulanan, D.Pokok, (A.Amaun - D.Pokok) AS Untung
				FROM SMKB_Gaji_Lejar A
				INNER JOIN SMKB_Gaji_Master B ON B.No_Staf = A.No_Staf AND B.Kod_Trans = @kodpinjaman AND B.Tkh_Tamat > GETDATE()
				INNER JOIN SMKB_Pinjaman_Master C ON C.No_Pinj = B.No_Trans 
				INNER JOIN SMKB_Pinjaman_Jadual_Bayar_Balik D ON D.No_Pinj = B.No_Trans AND RIGHT(D.Bln_GJ, 2) = MONTH(GETDATE()) AND LEFT(D.Bln_GJ,4) = YEAR(GETDATE())
				LEFT JOIN [DEVMIS\SQL_INS01].dbStaf.dbo.MS01_Peribadi AS E ON E.MS01_NoStaf = A.No_Staf
				WHERE A.Tahun = YEAR(GETDATE()) AND A.Bulan = MONTH(GETDATE()) AND A.Kod_PTJ LIKE @ptj AND A.Kod_Trans = @kodpinjaman"

			'query = "SELECT A.Kod_Trans,A.Bulan, A.Tahun,B.No_Trans,A.No_Staf, E.MS01_Nama AS Nama, C.Amaun AS Pinjaman,A.Amaun AS Pot_bulanan, D.Pokok, (A.Amaun - D.Pokok) AS Untung
			'	FROM SMKB_Gaji_Lejar A
			'	INNER JOIN SMKB_Gaji_Master B ON B.No_Staf = A.No_Staf AND B.Kod_Trans = @kodpinjaman AND B.Tkh_Tamat > GETDATE()
			'	INNER JOIN SMKB_Pinjaman_Master C ON C.No_Pinj = B.No_Trans 
			'	INNER JOIN SMKB_Pinjaman_Jadual_Bayar_Balik D ON D.No_Pinj = B.No_Trans AND RIGHT(D.Bln_GJ, 2) = MONTH('2022-10-30 15:39:14.297') AND LEFT(D.Bln_GJ,4) = YEAR('2022-10-30 15:39:14.297')
			'	LEFT JOIN [DEVMIS\SQL_INS01].dbStaf.dbo.MS01_Peribadi AS E ON E.MS01_NoStaf = A.No_Staf
			'	WHERE A.Tahun = YEAR('2022-10-30 15:39:14.297') AND A.Bulan = MONTH('2022-10-30 15:39:14.297') AND A.Kod_PTJ LIKE @ptj AND A.Kod_Trans = @kodpinjaman"

			cmd.Connection = sqlconn
			cmd.CommandText = query

			cmd.Parameters.Add(New SqlParameter("@ptj", ptj))
			cmd.Parameters.Add(New SqlParameter("@kodpinjaman", kodpinjaman))
			dt.Load(cmd.ExecuteReader())
			Return dt
		End Using
	End Function

	''SQL to fetch top 10 result of staf list for dropdown
	'<WebMethod()>
	'   <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
	'   Public Function GetKodTransaksiList(ByVal q As String) As String
	'       Dim tmpDT As DataTable = LoadKodTransList(q)
	'       Return JsonConvert.SerializeObject(tmpDT)
	'   End Function

	'   Private Function LoadKodTransList(kod As String) As DataTable
	'       Dim db = New DBKewConn

	'       'Dim query As String = "SELECT TOP(10) Kod_Penghutang as kod, Nama_Penghutang as nama, No_Rujukan as id FROM SMKB_Penghutang_Master"
	'       Dim query As String = "SELECT 'OO' AS Kod_Trans, 'KESELURUHAN' AS Butiran UNION ALL
	'                              SELECT Kod_Trans, Butiran
	'                              FROM SMKB_Gaji_Kod_Trans
	'                              WHERE Jenis_Trans = @kod"
	'       Dim param As New List(Of SqlParameter)
	'       param.Add(New SqlParameter("@kod", kod))
	'       'If (kod <> "") Then
	'       '    query = query & " WHERE LOWER(Nama_Penghutang) LIKE '%' + @kod + '%' OR Kod_Penghutang LIKE '%' + @kod + '%' OR No_Rujukan LIKE '%' + @kod + '%'"
	'       '    param.Add(New SqlParameter("@kod", kod))
	'       'End If
	'       'query &= " ORDER BY Kod_Penghutang"
	'       Return db.Read(query, param)
	'   End Function
End Class