Imports System.Data.SqlClient

'Imports MySql.Data.MySqlClient


Module Mod_Data
    Public Structure Hasil_Sql
        Public hasil_str As String
        Public hasil_id As Int64
    End Structure

    Public Function isi_data(dt As DataTable, str As String, clmn_nm As String, sw As Stopwatch) As Hasil_Sql
        Dim sql_cont As New SqlConnection(Mod_Utama.sql_str)
        Dim sql_adpt As New SqlDataAdapter(str, sql_cont)

        dt.Clear()
        Dim hasil As Hasil_Sql
        Try
            sw.Start()
            sql_cont.Open()
            sql_adpt.Fill(dt)
        Catch ex As Exception
            hasil.hasil_str = ex.ToString
            hasil.hasil_id = 0
            Return hasil
        Finally
            sql_cont.Close()
            sw.Stop()
        End Try

        If Not clmn_nm = "" Then
            dt.PrimaryKey = New DataColumn() {dt.Columns(clmn_nm)}
        End If

        hasil.hasil_str = ""
        hasil.hasil_id = 0
        Return hasil
    End Function
    Public Function isi_data_noclear(dt As DataTable, str As String, clmn_nm As String, sw As Stopwatch) As Hasil_Sql
        Dim sql_cont As New SqlConnection(Mod_Utama.sql_str)
        Dim sql_adpt As New SqlDataAdapter(str, sql_cont)

        Dim hasil As Hasil_Sql
        Try
            sw.Start()
            sql_cont.Open()
            sql_adpt.Fill(dt)
        Catch ex As Exception
            hasil.hasil_str = ex.ToString
            hasil.hasil_id = 0
            Return hasil
        Finally
            sql_cont.Close()
            sw.Stop()
        End Try

        If Not clmn_nm = "" Then
            dt.PrimaryKey = New DataColumn() {dt.Columns(clmn_nm)}
        End If

        hasil.hasil_str = ""
        hasil.hasil_id = 0
        Return hasil
    End Function
    Public Function isi_datahr(dt As DataTable, str As String, clmn_nm As String, sw As Stopwatch) As Hasil_Sql
        Dim sql_cont As New SqlConnection(Mod_Utama.sql_tran_branch)
        Dim sql_adpt As New SqlDataAdapter(str, sql_cont)


        dt.Clear()
        Dim hasil As Hasil_Sql
        Try
            sw.Start()
            sql_cont.Open()
            sql_adpt.Fill(dt)
        Catch ex As Exception
            hasil.hasil_str = ex.ToString
            hasil.hasil_id = 0
            Return hasil
        Finally
            sql_cont.Close()
            sw.Stop()
        End Try

        If Not clmn_nm = "" Then
            dt.PrimaryKey = New DataColumn() {dt.Columns(clmn_nm)}
        End If

        hasil.hasil_str = ""
        hasil.hasil_id = 0
        Return hasil
    End Function
    Public Function isi_datahr_noclear(dt As DataTable, str As String, clmn_nm As String, sw As Stopwatch) As Hasil_Sql
        Dim sql_cont As New SqlConnection(Mod_Utama.sql_tran_branch)
        Dim sql_adpt As New SqlDataAdapter(str, sql_cont)

        Dim hasil As Hasil_Sql
        Try
            sw.Start()
            sql_cont.Open()
            sql_adpt.Fill(dt)
        Catch ex As Exception
            hasil.hasil_str = ex.ToString
            hasil.hasil_id = 0
            Return hasil
        Finally
            sql_cont.Close()
            sw.Stop()
        End Try

        If Not clmn_nm = "" Then
            dt.PrimaryKey = New DataColumn() {dt.Columns(clmn_nm)}
        End If

        hasil.hasil_str = ""
        hasil.hasil_id = 0
        Return hasil
    End Function


    Public Function exec_sql(str As String, show_error As Boolean) As String
        Dim cont As New SqlConnection(Mod_Utama.sql_str)
        Dim cmnd As New SqlCommand(str, cont)

        Try
            cont.Open()
            cmnd.ExecuteScalar()
        Catch ex As Exception
            If show_error = True Then MsgBox(str & Chr(13) & Chr(13) & ex.ToString)
            Return ex.Message
        Finally
            cont.Close()
        End Try

        Return ""
    End Function
    Public Function exec_mysql(str As String, show_error As Boolean) As String
        Dim cont As New SqlConnection(Mod_Utama.sql_tran_branch)
        Dim cmnd As New SqlCommand(str, cont)

        Try
            cont.Open()
            cmnd.ExecuteScalar()
        Catch ex As Exception
            If show_error = True Then MsgBox(str & Chr(13) & Chr(13) & ex.ToString)
            Return ex.Message
        Finally
            cont.Close()
        End Try

        Return ""
    End Function
    Public Function exec_sqlhr(str As String, show_error As Boolean) As String
        Dim cont As New SqlConnection(Mod_Utama.sql_tran_branch)
        Dim cmnd As New SqlCommand(str, cont)

        Try
            cont.Open()
            cmnd.ExecuteScalar()
        Catch ex As Exception
            If show_error = True Then MsgBox(str & Chr(13) & Chr(13) & ex.ToString)
            Return ex.Message
        Finally
            cont.Close()
        End Try

        Return ""
    End Function

    Public Function exec_sql_id(str As String, show_error As Boolean) As Hasil_Sql
        Dim cont As New SqlConnection(Mod_Utama.sql_str)
        Dim cmnd As New SqlCommand(str, cont)

        Dim nilai As Hasil_Sql
        Try
            cont.Open()
            nilai.hasil_id = cmnd.ExecuteScalar()
            nilai.hasil_str = ""
        Catch ex As Exception
            If show_error = True Then MsgBox(ex.ToString)
            nilai.hasil_str = ex.ToString
            nilai.hasil_id = -1
            Return nilai
        Finally
            cont.Close()
        End Try

        Return nilai
    End Function


End Module
