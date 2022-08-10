Imports DevExpress.Web
Imports System.Reflection.MethodBase
Imports DevExpress.Web.Data

Public Class mds_rec_apl
    Inherits System.Web.UI.Page

    Dim dr_user As DataRow
    Dim str As String
    Dim salah As er_custom
    Dim waktu_query As New Stopwatch
    Dim waktu_page As New Stopwatch
    Dim dt As New DataTable
    Dim cb As GridViewDataComboBoxColumn
    Dim str_menu As String = ",39,"
    Dim dr As DataRow

    Dim dt_sim, dt_moda As New DataTable
    Dim dt_lokasi, dt_limit As New DataTable
    Dim dt_pendidikan As New DataTable
    Dim dt_supir As New DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub mds_rec_apl_Init(sender As Object, e As EventArgs) Handles Me.Init
        dr_user = Session("dr_user")
        Me.uc_header.grid = Me.ASPxGridView1 'penting

        str = "<li><a href='home.aspx'>HOME</a></li>"
        str = str & "<li class='active'>RECRUITMENT</li>"
        str = str & "<li><a href='mds_rec_apl.aspx'>DRIVER APPLICANT</a></li>"

        Me.uc_header.list_menu.InnerHtml = str

        Me.isi_sim()
        Me.isi_lokasi()
        Me.isi_pendidikan()
        Me.isi_data()
    End Sub

    Private Sub mds_rec_apl_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Me.waktu_page.Start()
    End Sub

    Private Sub mds_rec_apl_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        Me.waktu_page.Stop()
        Mod_Utama.master_waktu(Me.waktu_query, Me.waktu_page, uc_footer)
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

    Function cek_supir(no_ktp As String)
        str = "select * from tms_mst_driver "
        Me.salah = Mod_Utama.isi_data(dt_supir, str, "id_driver", waktu_query)
        If salah.er_hasil <> "" Then
            Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
            Exit Function
        End If

        Dim dv As New DataView(Me.dt_supir)
        dv.RowFilter = "no_ktp='" & no_ktp & "'"

        Me.dt_supir = dv.ToTable
        Me.dt_supir.AcceptChanges()
        Return dt_supir
    End Function

    Private Sub isi_lokasi()
        str = "select * from mst_lokasi "
        Me.salah = Mod_Utama.isi_data(Me.dt_lokasi, str, "id_lokasi", waktu_query)

        If salah.er_hasil <> "" Then Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)

        cb = Me.ASPxGridView1.Columns("id_lokasi")
        cb.PropertiesComboBox.DataSource = Me.dt_lokasi
        cb.PropertiesComboBox.ValueField = "id_lokasi"
        cb.PropertiesComboBox.TextField = "nama"
        cb.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains
    End Sub

    Private Sub isi_sim()
        str = "select * from mst_jenis_sim"
        Me.salah = Mod_Utama.isi_data(Me.dt_sim, str, "id_jns_sim", waktu_query)

        If salah.er_hasil <> "" Then Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)

        cb = Me.ASPxGridView1.Columns("jns_sim")
        cb.PropertiesComboBox.DataSource = Me.dt_sim
        cb.PropertiesComboBox.ValueField = "id_jns_sim"
        cb.PropertiesComboBox.TextField = "nama"
        cb.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains
    End Sub

    Private Sub isi_pendidikan()
        str = "select * from mst_pendidikan"
        Me.salah = Mod_Utama.isi_data(Me.dt_pendidikan, str, "id_pendidikan", waktu_query)

        If salah.er_hasil <> "" Then Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)

        cb = Me.ASPxGridView1.Columns("id_pendidikan")
        cb.PropertiesComboBox.DataSource = Me.dt_pendidikan
        cb.PropertiesComboBox.ValueField = "id_pendidikan"
        cb.PropertiesComboBox.TextField = "nama"
        cb.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains
    End Sub

    Private Sub isi_data()
        str = "select * from mds_rec_apl order by id_rec desc "
        Me.salah = Mod_Utama.isi_data(dt, str, "id_rec", waktu_query)
        If salah.er_hasil <> "" Then
            Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
            Exit Sub
        End If

        Me.ASPxGridView1.DataSource = dt
        Me.ASPxGridView1.KeyFieldName = "id_rec"
        Mod_Utama.Atur_Grid(Me.ASPxGridView1)
        Me.ASPxGridView1.DataBind()
        Me.ASPxGridView1.Settings.ShowPreview = True
        Me.ASPxGridView1.Settings.ShowFooter = True
    End Sub

    Private Sub ASPxGridView1_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles ASPxGridView1.CellEditorInitialize
        Select Case e.Column.FieldName
            Case "jns_sim"
                Dim cb As ASPxComboBox = e.Editor
                cb.DataSource = Me.dt_sim
                cb.ValueField = "id_jns_sim"
                cb.TextField = "nama"
                cb.IncrementalFilteringMode = IncrementalFilteringMode.Contains
                cb.DataBind()

            Case "id_lokasi"
                Dim cb As ASPxComboBox = e.Editor
                cb.DataSource = Me.dt_lokasi
                cb.ValueField = "id_lokasi"
                cb.TextField = "nama"
                cb.IncrementalFilteringMode = IncrementalFilteringMode.Contains
                cb.DataBind()

            Case "id_pendidikan"
                Dim cb As ASPxComboBox = e.Editor
                cb.DataSource = Me.dt_pendidikan
                cb.ValueField = "id_pendidikan"
                cb.TextField = "nama"
                cb.IncrementalFilteringMode = IncrementalFilteringMode.Contains
                cb.DataBind()
        End Select
    End Sub

    Private Sub ASPxGridView1_CommandButtonInitialize(sender As Object, e As ASPxGridViewCommandButtonEventArgs) Handles ASPxGridView1.CommandButtonInitialize
        dr = Me.ASPxGridView1.GetDataRow(e.VisibleIndex)
        If dr Is Nothing Then Return

        Select Case e.ButtonType
            Case ColumnCommandButtonType.New
                If CStr(dr_user("baru")).Contains(str_menu) = False Then e.Visible = False
            Case ColumnCommandButtonType.Edit
                If CStr(dr_user("ubah")).Contains(str_menu) = False Then e.Visible = False
            Case ColumnCommandButtonType.Delete
                If CStr(dr_user("hapus")).Contains(str_menu) = False Then e.Visible = False
                If dr("terima_sta") = True Then e.Visible = False
        End Select
    End Sub

    Private Sub ASPxGridView1_CustomButtonCallback(sender As Object, e As ASPxGridViewCustomButtonCallbackEventArgs) Handles ASPxGridView1.CustomButtonCallback
        dr = Me.ASPxGridView1.GetDataRow(e.VisibleIndex)
        If dr Is Nothing Then Return

        Select Case e.ButtonID
            Case "bt_trm"
                If dr("terima_sta") = False Then
                    str = "update mds_rec_apl set "
                    str = str & "terima_user = '" & dr_user("nama") & "', "
                    str = str & "terima_sta = 1, "
                    str = str & "terima_date = getdate() "
                    str = str & "where id_rec = " & dr("id_rec") & " "

                    str = str & "INSERT INTO tms_mst_driver ("
                    str = str & "id_driver, nama, nik, no_ktp, tmpt_lhr, tgl_lahir, alamat, alamat_domisili, no_sim, id_jns_sim, exp_sim, no_hp, rek_bank, rek_no, pendidikan, agama, id_lokasi, "
                    str = str & "kategori, jabatan, jk, pengalaman_kerja, email, perkawinan, npwp, goldar, bpjs_no, perusahaan, dept, status_kerja, "
                    str = str & "no_bpjs_tk, reff_masuk, lamar_date, tgl_masuk, tanggungan, warga, pass, aktif_sta, aktif_date, aktif_user, "
                    str = str & "c_date, c_user, u_date, u_user) VALUES ("
                    str = str & "(select isnull(max(id_driver),0) + 1 from tms_mst_driver), "
                    str = str & "'" & dr("nama") & "', "
                    str = str & "(select format(getdate(), 'yyMMdd') + right(('000' + ltrim(str(isnull(right(max(id_driver),3),0) + 1))),3) from tms_mst_driver), "
                    str = str & "'" & dr("no_ktp") & "', "
                    str = str & "'" & dr("tmpt_lhr") & "', "
                    str = str & "'" & dr("tgl_lahir") & "', "
                    str = str & "'" & dr("alamat_ktp") & "', "
                    str = str & "'" & dr("alamat_dom") & "', "
                    str = str & "'" & dr("no_sim") & "', "
                    str = str & "'" & dr("jns_sim") & "', "
                    If IsDBNull(dr("exp_sim")) = True Then
                        str = str & "null, "
                    Else
                        str = str & "'" & Format(dr("exp_sim"), "yyyy-MM-dd") & "', "
                    End If
                    str = str & "'" & dr("no_hp") & "', "
                    str = str & "'" & dr("rek_bank") & "', "
                    str = str & "'" & dr("rek_no") & "', "
                    str = str & "(select nama from mst_pendidikan where id_pendidikan = '" & dr("id_pendidikan") & "'), "
                    str = str & "'" & dr("agama") & "', "
                    str = str & "" & dr("id_lokasi") & ", "
                    str = str & "'" & dr("kategori") & "', "
                    str = str & "'" & dr("jabatan") & "', "
                    str = str & "'" & dr("jk") & "', "
                    str = str & "'" & dr("pengalaman_kerja") & "', "
                    str = str & "'" & dr("email") & "', "
                    str = str & "'" & dr("status_menikah") & "', "
                    str = str & "'" & dr("npwp") & "', "
                    str = str & "'" & dr("goldar") & "', "
                    str = str & "'" & dr("bpjs_no") & "', "
                    str = str & "'" & dr("perusahaan") & "', "
                    str = str & "'" & dr("dept") & "', "
                    str = str & "'MAGANG', "
                    str = str & "'" & dr("no_bpjs_tk") & "', "
                    str = str & "'" & dr("reff_masuk") & "', "
                    str = str & "'" & Format(dr("lamar_date"), "yyyy-MM-dd") & "', "
                    str = str & "getdate(), "
                    str = str & "'" & dr("tanggungan") & "', "
                    str = str & "'" & dr("warga") & "', "
                    str = str & "'TC_CIBITUNG', "
                    str = str & "'" & True & "', "
                    str = str & "getdate(), "
                    str = str & "'" & dr_user("nama") & "', "
                    str = str & "getdate(), "
                    str = str & "'" & dr_user("nama") & "', "
                    str = str & "getdate(), "
                    str = str & "'" & dr_user("nama") & "') "
                Else
                    str = "update mds_rec_apl set "
                    str = str & "terima_user = '" & dr_user("nama") & "', "
                    str = str & "terima_sta = 0, "
                    str = str & "terima_date = getdate() "
                    str = str & "where id_rec = " & dr("id_rec") & " "
                End If

                salah.er_hasil = Mod_Utama.exec_sql(str)
                If salah.er_hasil <> "" Then Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
                Me.isi_data()
        End Select
    End Sub

    Private Sub ASPxGridView1_CustomButtonInitialize(sender As Object, e As ASPxGridViewCustomButtonEventArgs) Handles ASPxGridView1.CustomButtonInitialize
        dr = Me.ASPxGridView1.GetDataRow(e.VisibleIndex)
        If dr Is Nothing Then Return

        e.Image.Height = 30
        e.Image.Width = 30

        Select Case e.ButtonID
            Case "bt_trm"
                If dr("terima_sta") = 0 Or dr("terima_sta") = False Then
                    e.Image.Url = "~/img/no.png"
                Else
                    e.Image.Url = "~/img/yes.png"
                End If
                If dr("terima_sta") = True Then e.Enabled = False
        End Select
    End Sub

    Private Sub ASPxGridView1_CustomErrorText(sender As Object, e As ASPxGridViewCustomErrorTextEventArgs) Handles ASPxGridView1.CustomErrorText
        e.ErrorText = salah.er_hasil
    End Sub

    Private Sub ASPxGridView1_RowDeleting(sender As Object, e As ASPxDataDeletingEventArgs) Handles ASPxGridView1.RowDeleting
        Mod_Utama.log_delete("select * From mds_rec_apl where id_rec = " & e.Keys("id_rec"), "mds_rec_apl", dr_user)
        str = "delete mds_rec_apl "
        str = str & "where id_rec = " & e.Keys("id_rec") & " "

        salah.er_hasil = Mod_Utama.exec_sql(str, dr_user, "Applicant/Trs-insert")
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

    Private Sub ASPxGridView1_RowInserting(sender As Object, e As ASPxDataInsertingEventArgs) Handles ASPxGridView1.RowInserting
        Me.cek_supir(e.NewValues("no_ktp"))

        If Me.dt_supir.Rows.Count > 0 Then
            salah.er_hasil = "No. KTP sudah ada. !"
            Return
        End If

        str = "INSERT INTO mds_rec_apl ("
        str = str & "id_rec, nama, no_ktp, tmpt_lhr, tgl_lahir, alamat_ktp, alamat_dom, no_sim, jns_sim, exp_sim, no_hp, rek_bank, rek_no, id_pendidikan, agama, id_lokasi, "
        str = str & "kategori, jabatan, jk, pengalaman_kerja, email, status_menikah, npwp, goldar, bpjs_no, perusahaan, dept, "
        str = str & "no_bpjs_tk, reff_masuk, lamar_date, tanggungan, warga, "
        str = str & "c_date, c_user, u_date, u_user) VALUES ("
        str = str & "(select isnull(max(id_rec),0) + 1 from mds_rec_apl), "
        str = str & "'" & e.NewValues("nama") & "', "
        str = str & "'" & e.NewValues("no_ktp") & "', "
        str = str & "'" & e.NewValues("tmpt_lhr") & "', "
        str = str & "'" & Format(e.NewValues("tgl_lahir"), "yyyy-MM-dd") & "', "
        str = str & "'" & e.NewValues("alamat_ktp") & "', "
        str = str & "'" & e.NewValues("alamat_dom") & "', "
        str = str & "'" & e.NewValues("no_sim") & "', "
        str = str & "" & e.NewValues("jns_sim") & ", "
        If IsNothing(e.NewValues("exp_sim")) = True Then
            str = str & "null, "
        Else
            str = str & "'" & Format(e.NewValues("exp_sim"), "yyyy-MM-dd") & "', "
        End If
        str = str & "'" & e.NewValues("no_hp") & "', "
        str = str & "'" & e.NewValues("rek_bank") & "', "
        str = str & "'" & e.NewValues("rek_no") & "', "
        str = str & "'" & e.NewValues("id_pendidikan") & "', "
        str = str & "'" & e.NewValues("agama") & "', "
        str = str & "'" & e.NewValues("id_lokasi") & "', "
        str = str & "'" & e.NewValues("kategori") & "', "
        str = str & "'" & e.NewValues("jabatan") & "', "
        str = str & "'" & e.NewValues("jk") & "', "
        str = str & "'" & e.NewValues("pengalaman_kerja") & "', "
        str = str & "'" & e.NewValues("email") & "', "
        str = str & "'" & e.NewValues("status_menikah") & "', "
        str = str & "'" & e.NewValues("npwp") & "', "
        'str = str & "'" & e.NewValues("no_sio") & "', "
        'If IsNothing(e.NewValues("exp_sio")) = True Then
        '    str = str & "null, "
        'Else
        '    str = str & "'" & Format(e.NewValues("exp_sio"), "yyyy-MM-dd") & "', "
        'End If
        str = str & "'" & e.NewValues("goldar") & "', "
        str = str & "'" & e.NewValues("bpjs_no") & "', "
        str = str & "'" & e.NewValues("perusahaan") & "', "
        str = str & "'" & e.NewValues("dept") & "', "
        str = str & "'" & e.NewValues("no_bpjs_tk") & "', "
        str = str & "'" & e.NewValues("reff_masuk") & "', "
        str = str & "'" & Format(e.NewValues("lamar_date"), "yyyy-MM-dd") & "', "
        str = str & "'" & e.NewValues("tanggungan") & "', "
        str = str & "'" & e.NewValues("warga") & "', "
        str = str & "getdate(), "
        str = str & "'" & dr_user("nama") & "', "
        str = str & "getdate(), "
        str = str & "'" & dr_user("nama") & "') "

        salah.er_hasil = Mod_Utama.exec_sql(str, dr_user, "Applicant/Trs-insert")
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
        str = "UPDATE mds_rec_apl SET "
        str = str & "nama = '" & e.NewValues("nama") & "', "
        str = str & "no_ktp = '" & e.NewValues("no_ktp") & "', "
        str = str & "tmpt_lhr = '" & e.NewValues("tmpt_lhr") & "', "
        str = str & "tgl_lahir = '" & Format(e.NewValues("tgl_lahir"), "yyyy-MM-dd") & "', "
        str = str & "alamat_ktp = '" & e.NewValues("alamat_ktp") & "', "
        str = str & "alamat_dom = '" & e.NewValues("alamat_dom") & "', "
        str = str & "no_sim = '" & e.NewValues("no_sim") & "', "
        str = str & "jns_sim = " & e.NewValues("jns_sim") & ", "
        If IsNothing(e.NewValues("exp_sim")) = True Then
            str = str & "exp_sim = null, "
        Else
            str = str & "exp_sim = '" & Format(e.NewValues("exp_sim"), "yyyy-MM-dd") & "', "
        End If
        str = str & "no_hp = '" & e.NewValues("no_hp") & "', "
        str = str & "rek_bank = '" & e.NewValues("rek_bank") & "', "
        str = str & "rek_no = '" & e.NewValues("rek_no") & "', "
        str = str & "id_pendidikan = '" & e.NewValues("id_pendidikan") & "', "
        str = str & "agama = '" & e.NewValues("agama") & "', "
        str = str & "id_lokasi = '" & e.NewValues("id_lokasi") & "', "
        str = str & "kategori = '" & e.NewValues("kategori") & "', "
        str = str & "jabatan = '" & e.NewValues("jabatan") & "', "
        str = str & "jk = '" & e.NewValues("jk") & "', "
        str = str & "pengalaman_kerja = '" & e.NewValues("pengalaman_kerja") & "', "
        str = str & "email = '" & e.NewValues("email") & "', "
        str = str & "status_menikah = '" & e.NewValues("status_menikah") & "', "
        str = str & "npwp = '" & e.NewValues("npwp") & "', "
        'str = str & "no_sio = '" & e.NewValues("no_sio") & "', "
        'If IsNothing(e.NewValues("exp_sio")) = True Then
        '    str = str & "exp_sio = null, "
        'Else
        '    str = str & "exp_sio = '" & Format(e.NewValues("exp_sio"), "yyyy-MM-dd") & "', "
        'End If
        str = str & "goldar = '" & e.NewValues("goldar") & "', "
        str = str & "bpjs_no = '" & e.NewValues("bpjs_no") & "', "
        str = str & "perusahaan = '" & e.NewValues("perusahaan") & "', "
        str = str & "dept = '" & e.NewValues("dept") & "', "
        str = str & "no_bpjs_tk = '" & e.NewValues("no_bpjs_tk") & "', "
        str = str & "reff_masuk = '" & e.NewValues("reff_masuk") & "', "
        str = str & "lamar_date = '" & Format(e.NewValues("lamar_date"), "yyyy-MM-dd") & "', "
        str = str & "tanggungan = '" & e.NewValues("tanggungan") & "', "
        str = str & "warga = '" & e.NewValues("warga") & "', "
        str = str & "u_date = getdate(), "
        str = str & "u_user = '" & dr_user("nama") & "' "
        str = str & "where id_rec = " & e.Keys("id_rec") & " "

        salah.er_hasil = Mod_Utama.exec_sql(str, dr_user, "Applicant/Trs-insert")
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