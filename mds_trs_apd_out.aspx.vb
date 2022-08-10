Imports DevExpress.Web
Imports System.Reflection.MethodBase
Imports DevExpress.Web.Data

Public Class mds_trs_apd_out
    Inherits System.Web.UI.Page

    Dim dr_user As DataRow
    Dim str As String
    Dim salah As er_custom
    Dim waktu_query As New Stopwatch
    Dim waktu_page As New Stopwatch
    Dim dt As New DataTable
    Dim cb As GridViewDataComboBoxColumn
    Dim str_menu As String = ",22,"
    Dim dr As DataRow

    Dim dt_head As New DataTable
    Dim dt_supir, dt_usr As New DataTable
    Dim dt_cc As New DataTable

    Dim id_trs_apd As Int64 = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub mds_trs_apd_out_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        Me.waktu_page.Stop()
        Mod_Utama.master_waktu(Me.waktu_query, Me.waktu_page, uc_footer)
    End Sub

    Private Sub mds_trs_apd_out_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Me.waktu_page.Start()
    End Sub

    Private Sub mds_trs_apd_out_Init(sender As Object, e As EventArgs) Handles Me.Init
        Try
            Me.id_trs_apd = Request.QueryString("id")
        Catch ex As Exception
            Response.Redirect("trs_apd.aspx")
        End Try

        dr_user = Session("dr_user")
        Me.uc_header.grid = Me.ASPxGridView1 'penting
        Me.uc_header.a_filter.Visible = False
        Me.uc_header.div_search.Visible = False

        str = "<li><a href='home.aspx'>HOME</a></li>"
        str = str & "<li class='active'>EXISTING</li>"
        str = str & "<li><a href='mds_trs_apd.aspx'>Pembagian APD</a></li>"
        str = str & "<li><a href='mds_trs_apd_out.aspx?id=" & Me.id_trs_apd & "' style='color: #f00'>Pembagian APD ID. " & Me.id_trs_apd & "</a></li>"
        Me.uc_header.list_menu.InnerHtml = str
        If CStr(dr_user("lihat")).Contains(str_menu) = False Then
            Response.Redirect("~/page_no_auth.aspx")
        End If
        Me.Isi_Filter()

        Me.isi_nopol()
        Me.isi_head()
        Me.isi_supir()

        Me.isi_data()
    End Sub

    Private Sub Jika_Error(er_str As String, er_hasil As String, er_menu As String, nopesan As Integer)
        salah.er_str = er_str
        salah.er_menu = er_menu
        salah.er_waktu = Mod_Utama.str_waktu(Me.waktu_query, Me.waktu_page)
        Session("error") = salah

        Select Case nopesan
            Case 1
                Mod_Utama.tampil_error(Me, "Terjadi kesalahan pada Query, harap kirim laporan ke MIS via email")
            Case Else
                Mod_Utama.tampil_error(Me, "Terjadi kesalahan pada proses, harap kirim laporan ke MIS via email")
        End Select
    End Sub

    Private Sub Isi_Filter()
        uc_header.filter_cb3.Items.Clear()
        uc_header.filter_cb3.Items.Add("UPDATED")
        'VALUE
        uc_header.filter_cb3.Items(0).Value = "u_date"
    End Sub

    Private Sub isi_head()
        str = "select *, "
        str = str & "(select nama from mds_mst_apd where id_apd = mds_trs_apd.id_apd) as apd "
        str = str & "from mds_trs_apd "
        str = str & "where id_trs_apd = " & Me.id_trs_apd
        Me.salah = Mod_Utama.isi_data(Me.dt_head, str, "id_trs_apd", waktu_query)
        If salah.er_hasil <> "" Then
            Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
        End If

        dr = Me.dt_head.Rows(0)
        If dr Is Nothing Then Return

        Me.lb_apd.InnerText = dr("apd")
        Me.lb_id.InnerText = Me.id_trs_apd
        Me.lb_jml.InnerText = dr("jml")
        Me.lb_tgl.InnerText = Format(dr("tgl"), "yyyy-MM-dd")
    End Sub

    Private Sub isi_supir()
        str = "select * from tms_mst_driver order by nama "
        Me.salah = Mod_Utama.isi_data(Me.dt_supir, str, "id_driver", waktu_query)
        If salah.er_hasil <> "" Then
            Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
        End If

        cb = Me.ASPxGridView1.Columns("id_supir")
        cb.PropertiesComboBox.DataSource = Me.dt_supir
        cb.PropertiesComboBox.ValueField = "id_driver"
        cb.PropertiesComboBox.TextField = "nama"
        cb.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains
    End Sub

    Private Sub isi_nopol()
        str = "select * from tms_mst_nopol where active = 1  order by nopol"
        Me.salah = Mod_Utama.isi_data(Me.dt_cc, str, "nopol", waktu_query)
        If salah.er_hasil <> "" Then
            Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
        End If

        cb = Me.ASPxGridView1.Columns("nopol")
        cb.PropertiesComboBox.DataSource = Me.dt_cc
        cb.PropertiesComboBox.ValueField = "nopol"
        cb.PropertiesComboBox.TextField = "nopol"
        cb.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains
        cb.PropertiesComboBox.DropDownStyle = DropDownStyle.DropDown
    End Sub

    Private Sub isi_data()
        str = "select *, "
        str = str & "(select nama from mst_lokasi where id_lokasi = CASE "
        str = str & "WHEN id_user = '' THEN (select id_lokasi from tms_mst_driver where id_driver = mds_trs_apd_out.id_supir) "
        str = str & "ELSE (select id_lokasi from mst_user where id_user = mds_trs_apd_out.id_user) "
        str = str & "END "
        str = str & ") as lokasi, "
        str = str & "(select nama from mst_user where id_user = mds_trs_apd_out.id_user) as nama_kar, "
        str = str & "(select nama from tms_mst_driver where id_driver = mds_trs_apd_out.id_supir) as supir "
        str = str & "from mds_trs_apd_out "
        str = str & "where id_trs_apd = " & Me.id_trs_apd & " "
        str = str & "order by id_out_apd desc "
        Me.salah = Mod_Utama.isi_data(dt, str, "id_out_apd", waktu_query)
        If salah.er_hasil <> "" Then
            Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
            Exit Sub
        End If

        Me.ASPxGridView1.DataSource = dt
        Me.ASPxGridView1.KeyFieldName = "id_out_apd"
        Me.ASPxGridView1.DataBind()
        Mod_Utama.Atur_Grid(Me.ASPxGridView1)
        Me.ASPxGridView1.Settings.ShowPreview = True

    End Sub

    Private Sub ASPxGridView1_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles ASPxGridView1.CellEditorInitialize
        Select Case e.Column.FieldName
            Case "id_supir"
                Dim cb As ASPxComboBox = e.Editor
                cb.DataSource = Me.dt_supir
                cb.ValueField = "id_driver"
                cb.TextField = "nama"
                cb.IncrementalFilteringMode = IncrementalFilteringMode.Contains
                cb.DataBind()

            Case "nopol"
                Dim cb As ASPxComboBox = e.Editor
                cb.DataSource = Me.dt_cc
                cb.ValueField = "nopol"
                cb.TextField = "nopol"
                cb.IncrementalFilteringMode = IncrementalFilteringMode.Contains
                cb.DropDownStyle = DropDownStyle.DropDown
                cb.DataBind()
        End Select
    End Sub

    Private Sub ASPxGridView1_CommandButtonInitialize(sender As Object, e As ASPxGridViewCommandButtonEventArgs) Handles ASPxGridView1.CommandButtonInitialize
        Select Case e.ButtonType
            Case ColumnCommandButtonType.New
                If CStr(dr_user("baru")).Contains(str_menu) = False Then e.Visible = False
                Return
            Case ColumnCommandButtonType.Edit
                If CStr(dr_user("ubah")).Contains(str_menu) = False Then e.Visible = False
            Case ColumnCommandButtonType.Delete
                If CStr(dr_user("hapus")).Contains(str_menu) = False Then e.Visible = False
        End Select

        dr = Me.ASPxGridView1.GetDataRow(e.VisibleIndex)
        If dr Is Nothing Then Return
        If dr("u_date") < Now.AddDays(-10) Then e.Visible = False
    End Sub

    Private Sub ASPxGridView1_CustomErrorText(sender As Object, e As ASPxGridViewCustomErrorTextEventArgs) Handles ASPxGridView1.CustomErrorText
        e.ErrorText = salah.er_hasil
    End Sub

    Private Sub ASPxGridView1_InitNewRow(sender As Object, e As ASPxDataInitNewRowEventArgs) Handles ASPxGridView1.InitNewRow
        e.NewValues("tgl") = Now.Date
        e.NewValues("status") = "Dibagi"
        e.NewValues("qty") = 1
    End Sub

    Private Sub ASPxGridView1_RowInserting(sender As Object, e As ASPxDataInsertingEventArgs) Handles ASPxGridView1.RowInserting
        dr = dt_head.Rows(0)
        Dim dr_cek_supir As DataRow = dt_supir.Rows.Find(e.NewValues("id_supir"))

        str = "INSERT INTO mds_trs_apd_out ("
        str = str & "id_out_apd, id_trs_apd, id_supir, id_user, qty, ukuran, nopol, status, tgl, ket, "
        str = str & "c_date, c_user, u_date, u_user) VALUES ("
        str = str & "(select isnull(max(id_out_apd),0) + 1 from mds_trs_apd_out), "
        str = str & "'" & Me.id_trs_apd & "', "
        str = str & "'" & e.NewValues("id_supir") & "', "
        str = str & "'" & dr_user("id_user") & "', "
        str = str & "'" & e.NewValues("qty") & "', "
        If dr("id_apd") = 2 Or dr("id_apd") = 5 Then
            If IsDBNull(dr_cek_supir("size_seragam")) Then
                str = str & "'" & e.NewValues("ukuran") & "', "
            Else
                str = str & "'" & dr_cek_supir("size_seragam") & "', "
            End If
        ElseIf dr("id_apd") = 11 Then
            If IsDBNull(dr_cek_supir("size_sepatu")) Then
                str = str & "'" & e.NewValues("ukuran") & "', "
            Else
                str = str & "'" & dr_cek_supir("size_sepatu") & "', "
            End If
        Else
            str = str & "'" & e.NewValues("ukuran") & "', "
        End If
        str = str & "'" & e.NewValues("nopol") & "', "
        str = str & "'" & e.NewValues("status") & "', "
        str = str & "'" & e.NewValues("tgl") & "', "
        str = str & "'" & e.NewValues("ket") & "', "
        str = str & "getdate(), "
        str = str & "'" & dr_user("nama") & "', "
        str = str & "getdate(), "
        str = str & "'" & dr_user("nama") & "') "

        salah.er_hasil = Mod_Utama.exec_sql(str, dr_user, "DETAILS APD")
        If salah.er_hasil = "" Then
            Dim g As ASPxGridView = TryCast(sender, ASPxGridView)
            g.CancelEdit()
            Me.isi_data()
            e.Cancel = True
        Else
            salah.er_str = str
            salah.er_menu = Me.Page.ToString & " // " & GetCurrentMethod.Name
            salah.er_waktu = Mod_Utama.str_waktu(Me.waktu_query, Me.waktu_page)
            Session("error") = salah
        End If
    End Sub

    Private Sub ASPxGridView1_RowUpdating(sender As Object, e As ASPxDataUpdatingEventArgs) Handles ASPxGridView1.RowUpdating
        str = "UPDATE mds_trs_apd_out SET "
        str = str & "id_supir = '" & e.NewValues("id_supir") & "', "
        str = str & "id_user = '" & dr_user("id_user") & "', "
        str = str & "qty = '" & e.NewValues("qty") & "', "
        str = str & "ukuran = '" & e.NewValues("ukuran") & "', "
        str = str & "nopol = '" & e.NewValues("nopol") & "', "
        str = str & "status = '" & e.NewValues("status") & "', "
        str = str & "tgl = '" & e.NewValues("tgl") & "', "
        str = str & "ket = '" & e.NewValues("ket") & "', "
        str = str & "u_date = getdate(), "
        str = str & "u_user = '" & dr_user("nama") & "' "
        str = str & "where id_out_apd = '" & e.Keys("id_out_apd") & "' "

        salah.er_hasil = Mod_Utama.exec_sql(str, dr_user, "DETAILS APD")
        If salah.er_hasil = "" Then
            Dim g As ASPxGridView = TryCast(sender, ASPxGridView)
            g.CancelEdit()
            Me.isi_data()
            e.Cancel = True
        Else
            salah.er_str = str
            salah.er_menu = Me.Page.ToString & " // " & GetCurrentMethod.Name
            salah.er_waktu = Mod_Utama.str_waktu(Me.waktu_query, Me.waktu_page)
            Session("error") = salah
        End If
    End Sub

    Private Sub ASPxGridView1_RowDeleting(sender As Object, e As ASPxDataDeletingEventArgs) Handles ASPxGridView1.RowDeleting
        Mod_Utama.log_delete("select * From mds_trs_apd_out where id_out_apd = " & e.Keys("id_out_apd"), "mds_trs_apd_out", dr_user)
        str = "delete mds_trs_apd_out "
        str = str & "where id_out_apd = " & e.Keys("id_out_apd") & " "

        salah.er_hasil = Mod_Utama.exec_sql(str, dr_user, "DETAILS APD")
        If salah.er_hasil = "" Then
            Dim g As ASPxGridView = TryCast(sender, ASPxGridView)
            g.CancelEdit()
            Me.isi_data()
            e.Cancel = True
        Else
            salah.er_str = str
            salah.er_menu = Me.Page.ToString & " // " & GetCurrentMethod.Name
            salah.er_waktu = Mod_Utama.str_waktu(Me.waktu_query, Me.waktu_page)
            Session("error") = salah
        End If
    End Sub
End Class