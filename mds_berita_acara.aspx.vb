Imports DevExpress.Web
Imports System.Reflection.MethodBase
Imports DevExpress.Web.Data

Public Class mds_berita_acara
    Inherits System.Web.UI.Page

    Dim dr_user As DataRow
    Dim str As String
    Dim salah As er_custom
    Dim waktu_query As New Stopwatch
    Dim waktu_page As New Stopwatch
    Dim dt As New DataTable
    Dim cb As GridViewDataComboBoxColumn
    Dim str_menu As String = ",19,"
    Dim dr As DataRow

    Dim dt_nopol, dt_stck, dt_ba_sdr, dt_sdr As New DataTable
    Dim dt_lokasi As New DataTable
    Dim dt_driver As New DataTable
    Dim dt_ba As New DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.isi_data()
    End Sub

    Protected Sub bt_refresh_ServerClick(sender As Object, e As EventArgs)

    End Sub

    Private Sub mds_berita_acara_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        Me.waktu_page.Stop()
        Mod_Utama.master_waktu(Me.waktu_query, Me.waktu_page, uc_footer)
    End Sub

    Private Sub mds_berita_acara_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Me.waktu_page.Start()
    End Sub

    Private Sub mds_berita_acara_Init(sender As Object, e As EventArgs) Handles Me.Init
        dr_user = Session("dr_user")
        Me.uc_header.grid = Me.ASPxGridView1 'penting
        Me.uc_header.a_filter.Visible = False

        str = "<li><a href='home.aspx'>HOME</a></li>"
        str = str & "<li class='active'>EXISTING</li>"
        str = str & "<li><a href='mds_berita_acara.aspx'>Berita Acara</a></li>"
        Me.uc_header.list_menu.InnerHtml = str

        If IsPostBack = False Then
            Me.de_start.Date = Format(Now.AddMonths(-1), "yyyy-MM-dd")
            Me.de_end.Date = Format(Now, "yyyy-MM-dd")
        End If

        Me.Isi_Filter()

        Me.isi_ba()
        Me.isi_nopol()
        Me.isi_driver()
        Me.isi_tujuan()
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

    Private Sub isi_ba()
        str = "select * from mds_mst_ba "
        str = str & "where id_ba not in (1, 10) "
        str = str & " order by nama "
        Me.salah = Mod_Utama.isi_data(Me.dt_ba, str, "id_ba", waktu_query)
        If salah.er_hasil <> "" Then Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)

        cb = Me.ASPxGridView1.Columns("id_ba")
        cb.PropertiesComboBox.DataSource = Me.dt_ba
        cb.PropertiesComboBox.ValueField = "id_ba"
        cb.PropertiesComboBox.TextField = "nama"
        cb.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains
    End Sub

    Private Sub isi_nopol()
        str = "select * from tms_mst_nopol order by nopol"
        Me.salah = Mod_Utama.isi_data(Me.dt_nopol, str, "id_nopol", waktu_query)
        If salah.er_hasil <> "" Then Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)

        cb = Me.ASPxGridView1.Columns("id_nopol")
        cb.PropertiesComboBox.DataSource = Me.dt_nopol
        cb.PropertiesComboBox.ValueField = "id_nopol"
        cb.PropertiesComboBox.TextField = "nopol"
        cb.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains
    End Sub

    Private Sub isi_driver()
        str = "select * from tms_mst_driver order by nama "
        Me.salah = Mod_Utama.isi_data(Me.dt_driver, str, "id_driver", waktu_query)
        If salah.er_hasil <> "" Then Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)

        cb = Me.ASPxGridView1.Columns("id_driver")
        cb.PropertiesComboBox.DataSource = Me.dt_driver
        cb.PropertiesComboBox.ValueField = "id_driver"
        cb.PropertiesComboBox.TextField = "nama"
        cb.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains

        cb = Me.ASPxGridView1.Columns("id_driver2")
        cb.PropertiesComboBox.DataSource = Me.dt_driver
        cb.PropertiesComboBox.ValueField = "id_driver"
        cb.PropertiesComboBox.TextField = "nama"
        cb.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains
    End Sub

    Private Sub isi_tujuan()
        str = "select *, "
        str = str & "(select nama from opr_mst_prop where id_prop = opr_mst_kab.id_prop ) as nm_prop "
        str = str & "from opr_mst_kab order by nama "
        Me.salah = Mod_Utama.isi_data(Me.dt_lokasi, str, "id_kab", waktu_query)
        If salah.er_hasil <> "" Then Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)

        cb = Me.ASPxGridView1.Columns("id_kab")
        cb.PropertiesComboBox.DataSource = Me.dt_lokasi
        cb.PropertiesComboBox.ValueField = "id_kab"
        cb.PropertiesComboBox.TextField = "nama"
        cb.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains
    End Sub

    Private Sub isi_data()
        str = "select *, "
        str = str & "(select nama from mst_lokasi where id_lokasi in (select id_lokasi from tms_mst_driver where id_driver = mds_trs_ba.id_driver)) as lokasi, "
        str = str & "isnull((Select count(id_files) from mds_log_file where id_sumber = mds_trs_ba.id_trs_ba And sumber = 'BA'),0) as jml_img, "
        str = str & "(select 'Column B') as clmnB, "
        str = str & "(select 'Column C') as clmnC, "
        str = str & "(select 'Column D') as clmnD, "
        str = str & "(select 'Column E') as clmnE, "
        str = str & "(select 'Column F') as clmnF "
        str = str & "from mds_trs_ba "
        str = str & "where FORMAT(tgl_kejadian,'yyyy-MM-dd') >= '" & Format(Me.de_start.Value, "yyyy-MM-dd") & "' and FORMAT(tgl_kejadian,'yyyy-MM-dd') <= '" & Format(Me.de_end.Value, "yyyy-MM-dd") & "' "
        str = str & "order by id_trs_ba desc "
        Me.salah = Mod_Utama.isi_data(dt, str, "id_trs_ba", waktu_query)
        If salah.er_hasil <> "" Then
            Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
            Exit Sub
        End If

        For Each dtr As DataRow In Me.dt.Rows
            Select Case dtr("id_ba")
                Case 1, 10
                    dtr("clmnB") = "B. TEMPAT ACCIDENT / INCIDENT"
                    dtr("clmnC") = "C. ACCIDENT / INCIDENT"
                    dtr("clmnC") = "D. KRONOLOGIS"
                Case 2 'KEHILANGAN
                    dtr("clmnB") = "B. TEMPAT DAN TANGGAL KEJADIAN"
                    dtr("clmnC") = "C. BERITA ACARA KEHILANGAN"
                Case 3 'DEFECT
                    dtr("clmnB") = "B. DESKRIPSI DEFECT"
                    dtr("clmnC") = "C. KRONOLOGIS"
                    dtr("clmnD") = "D. ANALISIS & PENANGANAN"
                    dtr("clmnE") = "E. REALISASI KLAIM"
                    dtr("clmnF") = "F. PUNISHMENT"
            End Select
        Next
        Me.dt.AcceptChanges()
        Me.dt.DefaultView.RowFilter = Session("rowfilter")
        Me.ASPxGridView1.DataSource = dt
        Me.ASPxGridView1.KeyFieldName = "id_trs_ba"
        Mod_Utama.Atur_Grid(Me.ASPxGridView1)
        Me.ASPxGridView1.DataBind()
        Me.ASPxGridView1.Settings.ShowPreview = True
    End Sub

    Private Sub ASPxGridView1_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles ASPxGridView1.CellEditorInitialize
        Select Case e.Column.FieldName
            Case "id_ba"
                Dim cb As ASPxComboBox = e.Editor
                cb.DataSource = Me.dt_ba
                cb.ValueField = "id_ba"
                cb.TextField = "nama"
                cb.IncrementalFilteringMode = IncrementalFilteringMode.Contains
                cb.DataBind()

            Case "id_driver"
                Dim cb As ASPxComboBox = e.Editor
                cb.DataSource = Me.dt_driver
                cb.ValueField = "id_driver"
                cb.TextField = "nama"
                cb.IncrementalFilteringMode = IncrementalFilteringMode.Contains
                cb.DataBind()

            Case "id_driver2"
                Dim cb As ASPxComboBox = e.Editor
                cb.DataSource = Me.dt_driver
                cb.ValueField = "id_driver"
                cb.TextField = "nama"
                cb.IncrementalFilteringMode = IncrementalFilteringMode.Contains
                cb.DataBind()

            Case "id_nopol"
                Dim cb As ASPxComboBox = e.Editor
                cb.DataSource = Me.dt_nopol
                cb.ValueField = "id_nopol"
                cb.TextField = "nopol"
                cb.IncrementalFilteringMode = IncrementalFilteringMode.Contains
                cb.DataBind()

            Case "id_kab"
                Dim cb As ASPxComboBox = e.Editor
                cb.DataSource = Me.dt_lokasi
                cb.ValueField = "id_kab"
                cb.TextField = "nama"
                cb.Columns.Add("id_kab", "ID", 80)
                cb.Columns.Add("nama", "Kabupaten/Kota", 200)
                cb.Columns.Add("nm_prop", "Provinsi", 180)
                cb.IncrementalFilteringMode = IncrementalFilteringMode.Contains
                cb.DataBind()
        End Select
    End Sub

    Private Sub ASPxGridView1_CommandButtonInitialize(sender As Object, e As ASPxGridViewCommandButtonEventArgs) Handles ASPxGridView1.CommandButtonInitialize
        Select Case e.ButtonType
            Case ColumnCommandButtonType.New
                If CStr(dr_user("baru")).Contains(str_menu) = False Then e.Visible = False
            Case ColumnCommandButtonType.Edit
                If CStr(dr_user("ubah")).Contains(str_menu) = False Then e.Visible = False
            Case ColumnCommandButtonType.Delete
                If CStr(dr_user("hapus")).Contains(str_menu) = False Then e.Visible = False
        End Select
    End Sub

    Private Sub ASPxGridView1_CustomErrorText(sender As Object, e As ASPxGridViewCustomErrorTextEventArgs) Handles ASPxGridView1.CustomErrorText
        e.ErrorText = salah.er_hasil
    End Sub

    Private Sub ASPxGridView1_InitNewRow(sender As Object, e As ASPxDataInitNewRowEventArgs) Handles ASPxGridView1.InitNewRow
        e.NewValues("tgl") = Now.Date
    End Sub

    Private Sub ASPxGridView1_RowDeleting(sender As Object, e As ASPxDataDeletingEventArgs) Handles ASPxGridView1.RowDeleting
        Mod_Utama.log_delete("select * From mds_trs_ba where id_trs_ba = " & e.Keys("id_trs_ba"), "mds_trs_ba", dr_user)
        str = "delete mds_trs_ba "
        str = str & "where id_trs_ba = " & e.Keys("id_trs_ba")

        salah.er_hasil = Mod_Utama.exec_sql(str, dr_user, "Berita Acara")
        If salah.er_hasil = "" Then
            Dim g As ASPxGridView = TryCast(sender, ASPxGridView)
            g.CancelEdit()
            Me.isi_data()
            e.Cancel = True
        Else
            salah.er_str = str
            salah.er_menu = Me.Page.ToString & " // " & GetCurrentMethod.Name
            salah.er_waktu = Mod_Utama.str_waktu(Me.waktu_query, Me.waktu_page)
            Session("Error") = salah
        End If
    End Sub

    Private Sub ASPxGridView1_RowInserting(sender As Object, e As ASPxDataInsertingEventArgs) Handles ASPxGridView1.RowInserting
        str = "INSERT INTO mds_trs_ba ("
        str = str & "id_trs_ba, nourut, no_ba, id_ba, point, id_driver, id_driver2, id_nopol, "
        str = str & "unit_nm, unit_frame, unit_defect, no_reff, id_kab, "
        str = str & "tgl, tgl_kejadian, clmn_b, clmn_c, clmn_d, clmn_e, clmn_f, "
        str = str & "c_date, c_user, u_date, u_user) VALUES ("
        str = str & "(Select isnull(max(id_trs_ba),0) + 1 from mds_trs_ba), "
        str = str & "(Select isnull(max(nourut),0) + 1 from mds_trs_ba where year(tgl) = year(getdate())), "
        str = str & "(Select 'ACT\BA-' + format(getdate(), 'yyyyMM') + right('000000' + ltrim(str(isnull(max(nourut),0) + 1)),6) from mds_trs_ba where year(tgl) = year(getdate())), "
        str = str & "'" & e.NewValues("id_ba") & "', "
        str = str & "(select point from mds_mst_ba where id_ba = " & e.NewValues("id_ba") & "), "
        str = str & "'" & e.NewValues("id_driver") & "', "
        str = str & "'" & e.NewValues("id_driver2") & "', "
        str = str & "'" & e.NewValues("id_nopol") & "', "
        str = str & "'" & e.NewValues("unit_nm") & "', "
        str = str & "'" & e.NewValues("unit_frame") & "', "
        str = str & "'" & e.NewValues("unit_defect") & "', "
        str = str & "'" & e.NewValues("no_reff") & "', "
        str = str & "'" & e.NewValues("id_kab") & "', "
        str = str & "'" & e.NewValues("tgl") & "', "
        str = str & "'" & e.NewValues("tgl_kejadian") & "', "
        str = str & "'" & e.NewValues("clmn_b") & "', "
        str = str & "'" & e.NewValues("clmn_c") & "', "
        str = str & "'" & e.NewValues("clmn_d") & "', "
        str = str & "'" & e.NewValues("clmn_e") & "', "
        str = str & "'" & e.NewValues("clmn_f") & "', "
        str = str & "getdate(), "
        str = str & "'" & dr_user("nama") & "', "
        str = str & "getdate(), "
        str = str & "'" & dr_user("nama") & "') "

        salah.er_hasil = Mod_Utama.exec_sql(str, dr_user, "Berita Acara")
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
        str = "update mds_trs_ba set "
        str = str & "id_ba = '" & e.NewValues("id_ba") & "', "
        str = str & "point = (select point from mds_mst_ba where id_ba = " & e.NewValues("id_ba") & "), "
        str = str & "id_driver = '" & e.NewValues("id_driver") & "', "
        str = str & "id_driver2 = '" & e.NewValues("id_driver2") & "', "
        str = str & "id_nopol = '" & e.NewValues("id_nopol") & "', "
        str = str & "unit_nm = '" & e.NewValues("unit_nm") & "', "
        str = str & "unit_frame = '" & e.NewValues("unit_frame") & "', "
        str = str & "unit_defect = '" & e.NewValues("unit_defect") & "', "
        str = str & "no_reff = '" & e.NewValues("no_reff") & "', "
        str = str & "id_kab = '" & e.NewValues("id_kab") & "', "
        str = str & "tgl = '" & e.NewValues("tgl") & "', "
        str = str & "tgl_kejadian = '" & e.NewValues("tgl_kejadian") & "', "
        str = str & "clmn_b = '" & e.NewValues("clmn_b") & "', "
        str = str & "clmn_c = '" & e.NewValues("clmn_c") & "', "
        str = str & "clmn_d = '" & e.NewValues("clmn_d") & "', "
        str = str & "clmn_e = '" & e.NewValues("clmn_e") & "', "
        str = str & "clmn_f = '" & e.NewValues("clmn_f") & "', "
        str = str & "u_date = getdate(), "
        str = str & "u_user = '" & dr_user("nama") & "' "
        str = str & "where id_trs_ba = " & e.Keys("id_trs_ba")

        salah.er_hasil = Mod_Utama.exec_sql(str, dr_user, "Berita Acara")
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