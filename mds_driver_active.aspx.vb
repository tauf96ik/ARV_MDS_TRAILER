Imports System.Reflection.MethodBase
Imports DevExpress.Web
Imports DevExpress.Web.Data

Public Class mds_driver_active
    Inherits System.Web.UI.Page

    Dim dr_user As DataRow
    Dim str, dari As String
    Dim salah As er_custom
    Dim waktu_query As New Stopwatch
    Dim waktu_page As New Stopwatch
    Dim cb As New GridViewDataComboBoxColumn
    Dim str_menu As String = ",3,"
    Dim str_app As String = ",1,"
    Dim dr As DataRow
    Dim dt_lokasi, dt_sim, dt_pendidikan, dt_kel, dt_kec, dt As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub mds_driver_active_Init(sender As Object, e As EventArgs) Handles Me.Init
        dr_user = Session("dr_user")
        Me.uc_header.grid = Me.ASPxGridView1 'penting

        Try
            Me.dari = Request.QueryString("dari")
        Catch ex As Exception
            Me.dari = ""
        End Try

        str = "<li><a href='home.aspx'>HOME</a></li>"
        str = str & "<li class='active'>EXISTING</li>"
        str = str & "<li><a href='mds_driver_act.aspx'>Daftar Driver Active</a></li>"

        If CStr(dr_user("lihat")).Contains(str_menu) = False Then
            Response.Redirect("~/page_no_auth.aspx")
        End If
        Me.uc_header.list_menu.InnerHtml = str
        Me.Isi_Filter()
        Me.isi_pendidikan()
        Me.isi_lokasi()
        Me.isi_jns_sim()

        Me.isi_data()
    End Sub

    Private Sub mds_driver_active_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Me.waktu_page.Start()

    End Sub

    Private Sub mds_driver_active_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
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

    Private Sub Isi_Filter()
        uc_header.filter_cb3.Items.Clear()
        uc_header.filter_cb3.Items.Add("UPDATED")
        'VALUE
        uc_header.filter_cb3.Items(0).Value = "u_date"
    End Sub

    Private Sub isi_pendidikan()
        str = "select * from mst_pendidikan"
        Me.salah = Mod_Utama.isi_data(Me.dt_pendidikan, str, "id_pendidikan", waktu_query)

        cb = Me.ASPxGridView1.Columns("pendidikan")
        cb.PropertiesComboBox.DataSource = Me.dt_pendidikan
        cb.PropertiesComboBox.ValueField = "nama"
        cb.PropertiesComboBox.TextField = "nama"
        cb.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains
    End Sub

    Private Sub isi_jns_sim()
        str = "select * from mst_jenis_sim"
        Me.salah = Mod_Utama.isi_data(Me.dt_sim, str, "id_jns_sim", waktu_query)

        cb = Me.ASPxGridView1.Columns("id_jns_sim")
        cb.PropertiesComboBox.DataSource = Me.dt_sim
        cb.PropertiesComboBox.ValueField = "id_jns_sim"
        cb.PropertiesComboBox.TextField = "nama"
        cb.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains
    End Sub

    Private Sub isi_lokasi()
        str = "select * from mst_lokasi"
        Me.salah = Mod_Utama.isi_data(Me.dt_lokasi, str, "id_lokasi", waktu_query)

        cb = Me.ASPxGridView1.Columns("id_lokasi")
        cb.PropertiesComboBox.DataSource = Me.dt_lokasi
        cb.PropertiesComboBox.ValueField = "id_lokasi"
        cb.PropertiesComboBox.TextField = "nama"
        cb.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains
    End Sub

    Private Sub isi_data()
        Dim fi_lim As uc_header.filter_limit = uc_header.filtertext
        str = "select " & fi_lim.str_limit & " *, "
        str = str & "(select isnull(count(*), 0) from mds_log_file where id_sumber = tms_mst_driver.id_driver and sumber = 'DOCSUPIR') as upload, "
        str = str & "(select isnull(count(*), 0) from mds_drv_fam where id_spr = tms_mst_driver.id_driver) as jml_fam, "
        str = str & "(select '') as masakerja, "
        str = str & "(select '') as umur, "
        str = str & "(select '') as masakerja_bln "
        str = str & "from tms_mst_driver where "
        If dari = "exp2bln" Then
            str = str & "exp_sim < DATEADD(m, 2, convert(date,getdate())) and "
        ElseIf dari = "exp1bln" Then
            str = str & "exp_sim < DATEADD(m, 1, convert(date,getdate())) and "
        ElseIf dari = "exp1mgg" Then
            str = str & "exp_sim < DATEADD(w, 1, convert(date,getdate())) and "
        ElseIf dari = "ultah" Then
            str = str & "MONTH(tgl_lahir) = MONTH(GETDATE()) and DAY(tgl_lahir) = Day(getdate()) and "
        ElseIf dari = "norek" Then
            str = str & "len(rek_no) < 3 and "
        ElseIf dari = "min_doc" Then
            str = str & "(Select count(id_sumber) from mds_log_file where sumber = 'DOCSUPIR' and id_sumber = tms_mst_driver.id_driver) < 4 and "
        End If
        str = str & "aktif_sta = 1 "
        str = str & fi_lim.str_filter
        str = str & "order by id_driver desc "

        Me.salah = Mod_Utama.isi_data(dt, str, "id_driver", waktu_query)

        If salah.er_hasil <> "" Then
            Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
            Exit Sub
        End If

        Dim bln As Integer
        Dim thn As Integer
        Dim bln_lhr As Integer
        Dim thn_lhr As Integer
        For Each dtr As DataRow In Me.dt.Rows
            If IsDBNull(dtr("tgl_masuk")) Then
                dtr("masakerja") = ""
                dtr("masakerja_bln") = ""
            Else
                bln = DateDiff(DateInterval.Month, dtr("tgl_masuk"), Now)
                thn = Math.Floor(bln / 12)
                dtr("masakerja") = thn.ToString & " | " & bln Mod 12
                dtr("masakerja_bln") = bln
            End If
            If IsDBNull(dtr("tgl_lahir")) Then
                dtr("umur") = ""
            Else
                bln_lhr = DateDiff(DateInterval.Month, dtr("tgl_lahir"), Now)
                thn_lhr = Math.Floor(bln_lhr / 12)
                dtr("umur") = thn_lhr.ToString & " | " & bln_lhr Mod 12
            End If
        Next
        Me.dt.AcceptChanges()

        If IsPostBack = False Then Session("rowFilter") = ""
        Me.dt.DefaultView.RowFilter = Session("rowFilter")
        Me.ASPxGridView1.DataSource = dt
        Me.ASPxGridView1.KeyFieldName = "id_driver"
        Mod_Utama.Atur_Grid(Me.ASPxGridView1, True)
        Me.ASPxGridView1.DataBind()

        Me.ASPxGridView1.SettingsEditing.Mode = GridViewEditingMode.PopupEditForm
        Me.ASPxGridView1.SettingsPopup.EditForm.VerticalAlign = PopupVerticalAlign.WindowCenter
        Me.ASPxGridView1.SettingsPopup.EditForm.HorizontalAlign = PopupHorizontalAlign.WindowCenter
        Me.ASPxGridView1.SettingsPopup.EditForm.Width = 1000
        Me.ASPxGridView1.SettingsPopup.EditForm.Height = 600
        Me.ASPxGridView1.SettingsPopup.EditForm.AllowResize = True
    End Sub

    Private Sub ASPxGridView1_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles ASPxGridView1.CellEditorInitialize
        Select Case e.Column.FieldName
            Case "id_lokasi"
                Dim cb As ASPxComboBox = e.Editor
                cb.DataSource = Me.dt_lokasi
                cb.ValueField = "id_lokasi"
                cb.TextField = "nama"
                cb.IncrementalFilteringMode = IncrementalFilteringMode.Contains
                cb.DataBind()

            Case "id_jns_sim"
                Dim cb As ASPxComboBox = e.Editor
                cb.DataSource = Me.dt_sim
                cb.ValueField = "id_jns_sim"
                cb.TextField = "nama"
                cb.IncrementalFilteringMode = IncrementalFilteringMode.Contains
                cb.DataBind()

            Case "pendidikan"
                Dim cb As ASPxComboBox = e.Editor
                cb.DataSource = Me.dt_pendidikan
                cb.ValueField = "nama"
                cb.TextField = "nama"
                cb.IncrementalFilteringMode = IncrementalFilteringMode.Contains
                cb.DataBind()
        End Select
    End Sub

    Private Sub ASPxGridView1_CustomErrorText(sender As Object, e As ASPxGridViewCustomErrorTextEventArgs) Handles ASPxGridView1.CustomErrorText
        e.ErrorText = salah.er_hasil

    End Sub

    Private Sub ASPxGridView1_RowInserting(sender As Object, e As ASPxDataInsertingEventArgs) Handles ASPxGridView1.RowInserting
        str = "INSERT INTO tms_mst_driver ("
        str = str & "id_driver, nik, no_absen, nama, no_ktp, alamat, alamat_domisili, no_hp, tmpt_lhr, tgl_lahir, jk, agama, warga, goldar, "
        str = str & "id_jns_sim, no_sim, exp_sim, status_kerja, lamar_date, tgl_masuk, awal_kontrak, akhir_kontrak, id_lokasi, perusahaan, dept, kategori, "
        str = str & "jabatan, rek_bank, rek_no, no_bpjs_tk, bpjs_no, garda_no, tgl_garda, npwp, pendidikan, pengalaman_kerja, email, perkawinan, tanggungan, "
        str = str & "size_seragam, size_sepatu, suhu_badan, tekanan_darah, reff_masuk, ket, aktif_sta, aktif_date, aktif_user, c_date, c_user, u_date, u_user) VALUES ("
        str = str & "(Select isnull(max(id_driver),0) + 1 from tms_mst_driver), "
        'str = str & "'" & e.NewValues("nik") & "', "
        str = str & "(select format(getdate(), 'yyyyMM') + right(('0000' + ltrim(str(isnull(right(max(nik),4),0) + 1))),4) from tms_mst_driver), "
        str = str & "'" & e.NewValues("no_absen") & "', "
        str = str & "'" & e.NewValues("nama") & "', "
        str = str & "'" & e.NewValues("no_ktp") & "', "
        str = str & "'" & e.NewValues("alamat") & "', "
        str = str & "'" & e.NewValues("alamat_domisili") & "', "
        str = str & "'" & e.NewValues("no_hp") & "', "
        str = str & "'" & e.NewValues("tmpt_lhr") & "', "
        str = str & "'" & Format(e.NewValues("tgl_lahir"), "yyyy-MM-dd") & "', "
        str = str & "'" & e.NewValues("jk") & "', "
        str = str & "'" & e.NewValues("agama") & "', "
        str = str & "'" & e.NewValues("warga") & "', "
        str = str & "'" & e.NewValues("goldar") & "', "
        str = str & "" & e.NewValues("id_jns_sim") & ", "
        str = str & "'" & e.NewValues("no_sim") & "', "
        str = str & "'" & Format(e.NewValues("exp_sim"), "yyyy-MM-dd") & "', "
        str = str & "'" & e.NewValues("status_kerja") & "', "
        str = str & "'" & Format(e.NewValues("lamar_date"), "yyyy-MM-dd") & "', "
        str = str & "'" & Format(e.NewValues("tgl_masuk"), "yyyy-MM-dd") & "', "
        str = str & "'" & Format(e.NewValues("awal_kontrak"), "yyyy-MM-dd") & "', "
        str = str & "'" & Format(e.NewValues("akhir_kontrak"), "yyyy-MM-dd") & "', "
        str = str & "" & e.NewValues("id_lokasi") & ", "
        str = str & "'" & e.NewValues("perusahaan") & "', "
        str = str & "'" & e.NewValues("dept") & "', "
        str = str & "'" & e.NewValues("kategori") & "', "
        str = str & "'" & e.NewValues("jabatan") & "', "
        str = str & "'" & e.NewValues("rek_bank") & "', "
        str = str & "'" & e.NewValues("rek_no") & "', "
        str = str & "'" & e.NewValues("no_bpjs_tk") & "', "
        str = str & "'" & e.NewValues("bpjs_no") & "', "
        str = str & "'" & e.NewValues("garda_no") & "', "
        str = str & "'" & Format(e.NewValues("tgl_garda"), "yyyy-MM-dd") & "', "
        str = str & "'" & e.NewValues("npwp") & "', "
        str = str & "'" & e.NewValues("pendidikan") & "', "
        str = str & "'" & e.NewValues("pengalaman_kerja") & "', "
        str = str & "'" & e.NewValues("email") & "', "
        str = str & "'" & e.NewValues("perkawinan") & "', "
        str = str & "'" & e.NewValues("tanggungan") & "', "
        str = str & "UPPER('" & e.NewValues("size_seragam") & "'), "
        str = str & "'" & e.NewValues("size_sepatu") & "', "
        str = str & "'" & e.NewValues("suhu_badan") & "', "
        str = str & "'" & e.NewValues("tekanan_darah") & "', "
        str = str & "'" & e.NewValues("reff_masuk") & "', "
        str = str & "'" & e.NewValues("ket") & "', "
        str = str & "'1', "
        str = str & "getdate(), "
        str = str & "'" & dr_user("nama") & "', "
        str = str & "getdate(), "
        str = str & "'" & dr_user("nama") & "', "
        str = str & "getdate(), "
        str = str & "'" & dr_user("nama") & "') "

        salah.er_hasil = Mod_Utama.exec_sql(str, dr_user, "input Driver Active")
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

    Private Sub ASPxGridView1_HtmlDataCellPrepared(sender As Object, e As ASPxGridViewTableDataCellEventArgs) Handles ASPxGridView1.HtmlDataCellPrepared
        dr = Me.ASPxGridView1.GetDataRow(e.VisibleIndex)
        Select Case e.DataColumn.FieldName
            Case "exp_sim"
                If CDate(e.CellValue) < Now.AddMonths(1).Date Then
                    e.Cell.BackColor = Drawing.Color.LightPink
                End If
                'Case "masakerja"
                '    If dr("masakerja_bln") <= 6 Then
                '        e.Cell.HorizontalAlign = HorizontalAlign.Center
                '        e.Cell.BackColor = Drawing.Color.Red
                '        e.Cell.ForeColor = Drawing.Color.White
                '    ElseIf dr("masakerja_bln") >= 6 And dr("masakerja_bln") <= 12 Then
                '        e.Cell.HorizontalAlign = HorizontalAlign.Center
                '        e.Cell.BackColor = Drawing.Color.Yellow
                '    ElseIf dr("masakerja_bln") >= 12 And dr("masakerja_bln") <= 60 Then
                '        e.Cell.HorizontalAlign = HorizontalAlign.Center
                '        e.Cell.BackColor = Drawing.Color.Green
                '        e.Cell.ForeColor = Drawing.Color.White
                '    ElseIf dr("masakerja_bln") >= 60 Then
                '        e.Cell.HorizontalAlign = HorizontalAlign.Center
                '        e.Cell.BackColor = Drawing.Color.Blue
                '        e.Cell.ForeColor = Drawing.Color.White
                'End If
        End Select
    End Sub

    Private Sub ASPxGridView1_RowUpdating(sender As Object, e As ASPxDataUpdatingEventArgs) Handles ASPxGridView1.RowUpdating
        Dim v_db As New DataView(Me.dt)
        v_db.RowFilter = "id_driver = " & e.Keys("id_driver") & " "
        Dim dr_db As DataRowView
        dr_db = v_db.Item(0)

        str = "update tms_mst_driver set "
        'str = str & "nik = '" & e.NewValues("nik") & "', "
        If IsDBNull(dr_db("nik")) = True Then
            str = str & "nik = concat(format(tgl_masuk,'yyyyMM'),stuff('0000',(4-len(id_driver))+1,len(id_driver),id_driver)), "
        End If
        str = str & "no_absen = '" & e.NewValues("no_absen") & "', "
        str = str & "nama = '" & e.NewValues("nama") & "', "
        str = str & "no_ktp = '" & e.NewValues("no_ktp") & "', "
        str = str & "alamat = '" & e.NewValues("alamat") & "', "
        str = str & "alamat_domisili = '" & e.NewValues("alamat_domisili") & "', "
        str = str & "no_hp = '" & e.NewValues("no_hp") & "', "
        str = str & "tmpt_lhr = '" & e.NewValues("tmpt_lhr") & "', "
        str = str & "tgl_lahir = '" & Format(e.NewValues("tgl_lahir"), "yyyy-MM-dd") & "', "
        str = str & "jk = '" & e.NewValues("jk") & "', "
        str = str & "agama = '" & e.NewValues("agama") & "', "
        str = str & "warga = '" & e.NewValues("warga") & "', "
        str = str & "goldar = '" & e.NewValues("goldar") & "', "
        str = str & "id_jns_sim = " & e.NewValues("id_jns_sim") & ", "
        str = str & "no_sim = '" & e.NewValues("no_sim") & "', "
        If IsNothing(e.NewValues("exp_sim")) = True Then
            str = str & "exp_sim = null, "
        Else
            str = str & "exp_sim = '" & Format(e.NewValues("exp_sim"), "yyyy-MM-dd") & "', "
        End If
        str = str & "status_kerja = '" & e.NewValues("status_kerja") & "', "
        str = str & "lamar_date = '" & Format(e.NewValues("lamar_date"), "yyyy-MM-dd") & "', "
        str = str & "tgl_masuk = '" & Format(e.NewValues("tgl_masuk"), "yyyy-MM-dd") & "', "
        str = str & "awal_kontrak = '" & Format(e.NewValues("awal_kontrak"), "yyyy-MM-dd") & "', "
        str = str & "akhir_kontrak = '" & Format(e.NewValues("akhir_kontrak"), "yyyy-MM-dd") & "', "
        str = str & "id_lokasi = '" & e.NewValues("id_lokasi") & "', "
        str = str & "perusahaan = '" & e.NewValues("perusahaan") & "', "
        str = str & "dept = '" & e.NewValues("dept") & "', "
        str = str & "kategori = '" & e.NewValues("kategori") & "', "
        str = str & "jabatan = '" & e.NewValues("jabatan") & "', "
        str = str & "rek_bank = '" & e.NewValues("rek_bank") & "', "
        str = str & "rek_no = '" & e.NewValues("rek_no") & "', "
        str = str & "no_bpjs_tk = '" & e.NewValues("no_bpjs_tk") & "', "
        str = str & "bpjs_no = '" & e.NewValues("bpjs_no") & "', "
        str = str & "garda_no = '" & e.NewValues("garda_no") & "', "
        str = str & "tgl_garda = '" & Format(e.NewValues("tgl_garda"), "yyyy-MM-dd") & "', "
        str = str & "npwp = '" & e.NewValues("npwp") & "', "
        str = str & "pendidikan = '" & e.NewValues("pendidikan") & "', "
        str = str & "pengalaman_kerja = '" & e.NewValues("pengalaman_kerja") & "', "
        str = str & "email = '" & e.NewValues("email") & "', "
        str = str & "perkawinan = '" & e.NewValues("perkawinan") & "', "
        str = str & "tanggungan = '" & e.NewValues("tanggungan") & "', "
        str = str & "size_seragam = UPPER('" & e.NewValues("size_seragam") & "'), "
        str = str & "size_sepatu = '" & e.NewValues("size_sepatu") & "', "
        str = str & "suhu_badan = '" & e.NewValues("suhu_badan") & "', "
        str = str & "tekanan_darah = '" & e.NewValues("tekanan_darah") & "', "
        str = str & "reff_masuk = '" & e.NewValues("reff_masuk") & "', "
        str = str & "ket = '" & e.NewValues("ket") & "', "
        str = str & "u_date = getdate(), "
        str = str & "u_user = '" & dr_user("nama") & "' "
        str = str & "where id_driver = " & e.Keys("id_driver")

        salah.er_hasil = Mod_Utama.exec_sql(str, dr_user, "update Driver active")
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
        str = "delete tms_mst_driver "
        str = str & "where id_driver = " & e.Keys("id_driver")

        salah.er_hasil = Mod_Utama.exec_sql(str, dr_user, "delete Driver active")
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

    Private Sub ASPxGridView1_CustomButtonInitialize(sender As Object, e As ASPxGridViewCustomButtonEventArgs) Handles ASPxGridView1.CustomButtonInitialize
        dr = Me.ASPxGridView1.GetDataRow(e.VisibleIndex)
        If dr Is Nothing Then Return

        e.Image.Height = 30
        e.Image.Width = 30

        Select Case e.ButtonID
            Case "bt_aktif"
                If dr("aktif_sta") = 0 Then
                    e.Image.Url = "~/img/no.png"
                    If CStr(dr_user("oth").Contains(str_app)) = False Then e.Enabled = True
                Else
                    If CStr(dr_user("oth").Contains(str_app)) = False Then e.Enabled = False
                    e.Image.Url = "~/img/yes.png"
                End If
        End Select
    End Sub

    Private Sub ASPxGridView1_CommandButtonInitialize(sender As Object, e As ASPxGridViewCommandButtonEventArgs) Handles ASPxGridView1.CommandButtonInitialize
        Select Case e.ButtonType
            Case ColumnCommandButtonType.Edit
                If CStr(dr_user("ubah")).Contains(str_menu) = False Then e.Visible = False
            Case ColumnCommandButtonType.Delete
                If CStr(dr_user("hapus")).Contains(str_menu) = False Then e.Visible = False
            Case ColumnCommandButtonType.New
                If CStr(dr_user("baru")).Contains(str_menu) = False Then e.Visible = False
        End Select
    End Sub

    Private Sub ASPxGridView1_CustomButtonCallback(sender As Object, e As ASPxGridViewCustomButtonCallbackEventArgs) Handles ASPxGridView1.CustomButtonCallback
        dr = Me.ASPxGridView1.GetDataRow(e.VisibleIndex)
        If dr Is Nothing Then Return

        Select Case e.ButtonID
            Case "bt_aktif"
                If dr("aktif_sta") = False Then
                    str = "update tms_mst_driver set "
                    If IsDBNull(dr("nik")) = True Then
                        str = str & "nik = concat(format(tgl_masuk,'yyyyMM'),stuff('0000',(4-len(id_driver))+1,len(id_driver),id_driver)), "
                    End If
                    str = str & "aktif_user = '" & dr_user("nama") & "', "
                    str = str & "aktif_sta = 1, "
                    str = str & "aktif_date = getdate() "
                    str = str & "where id_driver = " & dr("id_driver")
                Else
                    str = "update tms_mst_driver set "
                    str = str & "aktif_user = '" & dr_user("nama") & "', "
                    str = str & "aktif_sta = 0, "
                    str = str & "aktif_date = getdate() "
                    str = str & "where id_driver = " & dr("id_driver")
                End If
                salah.er_hasil = Mod_Utama.exec_sql(str)
                If salah.er_hasil <> "" Then Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
                Me.isi_data()
        End Select
    End Sub
End Class