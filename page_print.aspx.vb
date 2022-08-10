Imports DevExpress.Web
Imports System.Reflection.MethodBase

Public Class page_print
    Inherits System.Web.UI.Page

    Dim dr_user As DataRow
    Dim str As String
    Dim salah As er_custom
    Dim waktu_query As New Stopwatch
    Dim waktu_page As New Stopwatch
    Dim dt As New DataTable
    Dim dr As DataRow

    Dim idrec, idsupir As Int64
    Dim mode, sdrsta As String

    Dim dt_teguran As New DataTable
    Dim dt_mcu As New DataTable
    Dim dt_couch As New DataTable
    Dim dt_acc, dt_train As New DataTable
    Dim dt_acc_img, dt_supir_img, dt_supir, dt_head As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Select Case mode
            Case "train"
                Me.print_train()
            Case "BA"
                Me.print_ba()
            Case "ACCINC"
                Me.acc_inc()
            Case "teguran"
                Me.teguran()
        End Select
    End Sub

    Private Sub page_print_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Me.waktu_page.Start()
    End Sub

    Private Sub page_print_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        Me.waktu_page.Stop()
    End Sub

    Private Sub page_print_Init(sender As Object, e As EventArgs) Handles Me.Init
        Try
            Me.idrec = Request.QueryString("id")
            Me.mode = Request.QueryString("mode")
        Catch ex As Exception
            Return
        End Try

        dr_user = Session("dr_user")

        str = "<li><a href='home.aspx'>HOME</a></li>"
        str = str & "<li class='active'>Page Print</li>"
        str = str & "<li><a href='page_print.aspx?id=" & Me.idrec & "&mode=" & Me.mode & "' style='color: #f00'>Print View " & Me.mode & " ID. " & Me.idrec & "</a></li>"
        Me.uc_header.list_menu.InnerHtml = str
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

    Private Sub print_train()
        Dim rpt As New rpt_train

        str = "select *,"
        str = str & "(select nama from mds_mst_training where id_training = mds_trs_train.id_training) as jns_train, "
        str = str & "(select format(tgl, 'dd MMMM yyyy', 'id-ID')) as tgl_train, "
        str = str & "isnull((select count(id_spr) from mds_trs_train_dtl where id_train = mds_trs_train.id_train),0) as peserta "
        str = str & "from mds_trs_train where id_train = " & Me.idrec & " "
        Me.salah = Mod_Utama.isi_data(Me.dt_train, str, "id_train", waktu_query)
        If salah.er_hasil <> "" Then Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)

        dr = Me.dt_train.Rows(0)
        If dr("id_training") = 9 Then
            rpt.XrLabel1.Text = "Report Induction"
        End If
        Dim tgl, hasil As String
        tgl = dr("tgl")
        hasil = Weekday(tgl)
        Select Case hasil
            Case 1
                hasil = "Minggu"
            Case 2
                hasil = "Senin"
            Case 3
                hasil = "Selasa"
            Case 4
                hasil = "Rabu"
            Case 5
                hasil = "Kamis"
            Case 6
                hasil = "Jum'at"
            Case 7
                hasil = "Sabtu"
        End Select
        rpt.lbhari.Text = hasil
        rpt.lb_date.Text = dr("tgl_train")
        rpt.lbjamawal.Text = dr("jamawal")
        rpt.lbjamahir.Text = dr("jamakhir")
        rpt.lblok.Text = dr("lokasi")
        rpt.lbagenda.Text = dr("jns_train")
        rpt.lbjml.Text = dr("peserta")
        rpt.lbtrainer.Text = dr("trainer")
        rpt.lbmateri.Text = dr("materi")

        'gambar
        Dim dt_gbr As New DataTable
        str = "select * from mds_log_file where sumber = 'TRAINING' and id_sumber = " & Me.idrec
        salah = Mod_Utama.isi_data(dt_gbr, str, "id_files", Me.waktu_query)

        For Each dr_cek As DataRow In dt_gbr.Rows
            If dr_cek("jenis") = "Absensi" Then
                Dim dtimg As New DataTable
                str = "select * from mds_log_file where sumber = 'TRAINING' and id_sumber = " & Me.idrec & " and jenis = 'Absensi' "
                salah = Mod_Utama.isi_data(dtimg, str, "id_files", Me.waktu_query)

                Dim lebar As Integer = rpt.pnl_gbr1.WidthF - 10
                Dim jml As Integer = dtimg.Rows.Count
                If jml <> 0 Then
                    Dim kiri As Integer = 10
                    Dim kelip As Integer = 3
                    Dim lbr_gbr As Integer = lebar / kelip

                    If jml < kelip Then lbr_gbr = lebar / jml

                    lbr_gbr -= 6
                    Dim jml_y As Integer = jml \ kelip
                    Dim sisa As Integer = jml Mod kelip

                    If sisa > 0 Then jml_y += 1

                    Dim atas As Integer = 0
                    Dim tinggi As Integer = 250

                    If jml_y > 1 Then tinggi = 150

                    For i = 0 To jml - 1

                        dr = dtimg.Rows(i)

                        Dim gbr As New DevExpress.XtraReports.UI.XRPictureBox
                        gbr.Visible = True
                        gbr.ImageUrl = "http://" & HttpContext.Current.Request.Url.Host & ":" & HttpContext.Current.Request.Url.Port & "/Files/" & dr("file_nm")
                        gbr.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage
                        gbr.Size = New System.Drawing.Size(lbr_gbr, tinggi)
                        gbr.LocationF = New DevExpress.Utils.PointFloat(kiri, atas)
                        gbr.BorderColor = Drawing.Color.White
                        gbr.BorderWidth = 10
                        gbr.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid
                        rpt.pnl_gbr1.Controls.Add(gbr)

                        kiri += lbr_gbr + 6
                        If kiri + lbr_gbr - 5 > lebar Then
                            atas += tinggi + 6
                            kiri = 10
                        End If
                    Next
                    rpt.pnl_gbr1.HeightF = tinggi * jml_y + 10
                End If

            ElseIf dr_cek("jenis") = "ICOC" Then

                Dim dtimg As New DataTable
                str = "select * from mds_log_file where sumber = 'TRAINING' and id_sumber = " & Me.idrec & " and jenis = 'ICOC' "
                salah = Mod_Utama.isi_data(dtimg, str, "id_img", Me.waktu_query)

                Dim lebar As Integer = rpt.pnl_gbr2.WidthF - 10
                Dim jml As Integer = dtimg.Rows.Count
                If jml <> 0 Then
                    Dim kiri As Integer = 10
                    Dim kelip As Integer = 3
                    Dim lbr_gbr As Integer = lebar / kelip

                    If jml < kelip Then lbr_gbr = lebar / jml

                    lbr_gbr -= 6
                    Dim jml_y As Integer = jml \ kelip
                    Dim sisa As Integer = jml Mod kelip

                    If sisa > 0 Then jml_y += 1

                    Dim atas As Integer = 0
                    Dim tinggi As Integer = 250

                    If jml_y > 1 Then tinggi = 150

                    For i = 0 To jml - 1
                        dr = dtimg.Rows(i)
                        Dim gbr = New DevExpress.XtraReports.UI.XRPictureBox
                        gbr.Visible = True
                        gbr.ImageUrl = "http://" & HttpContext.Current.Request.Url.Host & ":" & HttpContext.Current.Request.Url.Port & "/Files/" & dr("file_nm")
                        gbr.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage
                        gbr.Size = New System.Drawing.Size(lbr_gbr, tinggi)
                        gbr.LocationF = New DevExpress.Utils.PointFloat(kiri, atas)
                        gbr.BorderColor = Drawing.Color.White
                        gbr.BorderWidth = 10
                        gbr.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid
                        rpt.pnl_gbr2.Controls.Add(gbr)

                        kiri += lbr_gbr + 6
                        If kiri + lbr_gbr - 5 > lebar Then
                            atas += tinggi + 6
                            kiri = 10
                        End If
                    Next
                    rpt.pnl_gbr2.HeightF = tinggi * jml_y + 10
                End If

            End If

        Next

        Dim dt_supir As New DataTable
        str = "Select *, "
        str = str & "(select nama from tms_mst_driver where id_driver = A.id_spr) supir "
        str = str & "From mds_trs_train_dtl A Where id_train = " & Me.idrec & " order by id_train_dtl "

        Me.salah = Mod_Utama.isi_data(dt_supir, str, "id_train_dtl", waktu_query)
        'dr = dt_supir.Rows(0)
        If salah.er_hasil <> Nothing Then
            Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
            Exit Sub
        End If

        Me.salah = Mod_Utama.isi_data(rpt.DS_rpt1.train_supir, str, "", waktu_query)

        Me.ASPxDocumentViewer1.Report = rpt
    End Sub

    Protected Sub print_ba()
        Dim rpt As New rpt_ba

        str = "select *, "
        str = str & "(ISNULL((select nama from mst_jenis_sim where id_jns_sim = (select id_jns_sim from tms_mst_driver where id_driver = mds_trs_ba.id_driver)), 'BII')) as sim,  "
        str = str & "(select exp_sim from tms_mst_driver where id_driver = mds_trs_ba.id_driver) as sim_exp, "
        str = str & "(select nama from tms_mst_driver where id_driver = mds_trs_ba.id_driver) as supir, "
        str = str & "(ISNULL((select dept from tms_mst_driver where id_driver = mds_trs_ba.id_driver), 'Management Driver')) as dep, "
        str = str & "(ISNULL((select kategori from tms_mst_driver where id_driver = mds_trs_ba.id_driver), 'Trailer')) as jbtn, "
        str = str & "(ISNULL((select tgl_masuk from tms_mst_driver where id_driver = mds_trs_ba.id_driver), '2021-01-01')) as tgl_masuk, "
        str = str & "(select nama from mds_mst_ba where id_ba = mds_trs_ba.id_ba) as ba, "
        str = str & "FORMAT (tgl, 'dd MMMM yyyy', 'id-ID') as tgl1, "
        str = str & "(select nopol from tms_mst_nopol where id_nopol = mds_trs_ba.id_nopol) as nopol "
        str = str & "from mds_trs_ba "
        str = str & "where id_trs_ba = " & Me.idrec & " "
        Me.salah = Mod_Utama.isi_data(Me.dt, str, "id_trs_ba", waktu_query)
        If salah.er_hasil <> "" Then Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)

        dr = Me.dt.Rows(0)

        rpt.lb_ba.Text = dr("ba")

        rpt.lb_clmn_b.Text = dr("clmn_b")
        rpt.lb_clmn_c.Text = dr("clmn_c")
        rpt.lb_clmn_d.Text = dr("clmn_d")
        rpt.lb_clmn_e.Text = dr("clmn_e")
        rpt.lb_clmn_f.Text = dr("clmn_f")

        Select Case dr("id_ba")
            Case 1, 10
                rpt.lb_b.Text = "B. TEMPAT ACCIDENT/INCIDENT"
                rpt.lb_b.Text = "C. ACCIDENT/INCIDENT"
                rpt.lb_c.Text = "D. KRONOLOGIS"
                rpt.lb_d.Visible = False
                rpt.lb_clmn_d.Visible = False
                rpt.lb_e.Visible = False
                rpt.lb_clmn_e.Visible = False
            Case 2
                rpt.lb_b.Text = "B. TEMPAT DAN TANGGAL KEJADIAN"
                rpt.lb_b.Text = "C. BERITA ACARA"
                rpt.lb_c.Visible = False
                rpt.lb_clmn_c.Visible = False
                rpt.lb_d.Visible = False
                rpt.lb_clmn_d.Visible = False
                rpt.lb_e.Visible = False
                rpt.lb_clmn_e.Visible = False
            Case 3
                rpt.lb_b.Text = "B. DESKRIPSI DEFECT"
                rpt.lb_c.Text = "C. KRONOLOGIS"
                rpt.lb_d.Text = "D. ANALISIS & PENANGANAN"
                rpt.lb_e.Text = "E. REALISASI KLAIM"
                rpt.lb_f.Text = "F. PUNISHMENT"
            Case -1
                rpt.lb_a.Text = "A. Identitas Peserta Sharing Session"
                rpt.lb_b.Text = "B. Hal Yang Diperoleh Dari Sharing Session"
                rpt.lb_c.Text = "C. Hasil Dari Sharing Session Yang Dapat Diimplementasikan Dalam Pekerjaan"
                rpt.lb_d.Visible = False
                rpt.lb_clmn_d.Visible = False
                rpt.lb_e.Visible = False
                rpt.lb_clmn_e.Visible = False
                rpt.lb_f.Visible = False
                rpt.lb_clmn_f.Visible = False
            Case Else
                rpt.lb_b.Text = "B."
                rpt.lb_b.Text = "C."
                rpt.lb_c.Text = "D."
                rpt.lb_d.Text = "E."
                rpt.lb_e.Text = "F."
        End Select

        rpt.lb_no.Text = dr("no_ba")
        rpt.lb_tgl.Text = dr("tgl1")
        Dim tgl, hasil As String
        tgl = dr("tgl")
        hasil = Weekday(tgl)
        Select Case hasil
            Case 1
                hasil = "Minggu"
            Case 2
                hasil = "Senin"
            Case 3
                hasil = "Selasa"
            Case 4
                hasil = "Rabu"
            Case 5
                hasil = "Kamis"
            Case 6
                hasil = "Jum'at"
            Case 7
                hasil = "Sabtu"
        End Select
        rpt.lb_hari.Text = hasil

        rpt.lb_supir.Text = dr("supir")
        rpt.lb_jbtn.Text = dr("jbtn")
        rpt.lb_dep.Text = dr("dep")
        rpt.lb_pic.Text = dr("c_user")
        Dim bulan_masakerja As Integer = DateDiff(DateInterval.Month, dr("tgl_masuk"), Now.Date)
        Dim tahun_masakerja As Integer = 0
        If bulan_masakerja < 12 Then
            tahun_masakerja = 0
        Else
            tahun_masakerja = bulan_masakerja / 12
        End If
        rpt.lb_masakerja.Text = tahun_masakerja & " Tahun " & (bulan_masakerja Mod 12).ToString & " Bulan"

        rpt.tglttd.Text = "Bekasi, " & dr("tgl1")
        rpt.ttddrv.Text = dr("supir")
        rpt.ttdpic.Text = dr("c_user")
        rpt.XrBarCode1.Text = dr("no_ba").ToString.Replace("\", "").Replace("-", "")

        Me.ASPxDocumentViewer1.Report = rpt
    End Sub

    Private Sub acc_inc()
        Dim rpt As New rpt_acc_inc
        Dim drimg As DataRow

        str = "select *, "
        str = str & "(select no_ba from mds_trs_ba where id_trs_ba =  mds_trs_acc_inc.id_trs_ba) as no_ba, "
        str = str & "(select exp_sim from tms_mst_driver where id_driver = mds_trs_acc_inc.id_driver) as sim_exp, "
        str = str & "(SELECT DATEDIFF(year, tgl_lahir, getdate()) from tms_mst_driver where id_driver = mds_trs_acc_inc.id_driver) As tgl_lhr_thn, "
        str = str & "(SELECT DATEDIFF(month, tgl_lahir, getdate()) from tms_mst_driver where id_driver = mds_trs_acc_inc.id_driver) % 12 as tgl_lhr_bln, "
        str = str & "(SELECT DATEDIFF(month, tgl_masuk, getdate()) from tms_mst_driver where id_driver = mds_trs_acc_inc.id_driver) / 12 As kerja_thn, "
        str = str & "(SELECT DATEDIFF(month, tgl_masuk, getdate()) from tms_mst_driver where id_driver = mds_trs_acc_inc.id_driver) % 12 as kerja_bln, "
        str = str & "(select nama from tms_mst_driver where id_driver = mds_trs_acc_inc.id_driver) As supir1, "
        str = str & "(Select nopol from tms_mst_nopol where id_nopol = mds_trs_acc_inc.id_nopol) As nopol, "
        str = str & "(Select nama from mst_jenis_sim where id_jns_sim = (Select id_jns_sim from tms_mst_driver where id_driver = mds_trs_acc_inc.id_driver)) As jns_sim "
        str = str & "from mds_trs_acc_inc where id_acc_inc = " & Me.idrec
        Me.salah = Mod_Utama.isi_data(Me.dt_acc, str, "id_acc_inc", waktu_query)
        If salah.er_hasil <> "" Then Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)

        dr = Me.dt_acc.Rows(0)

        str = "Select top 1 file_nm as file1 "
        str = str & "FROM mds_log_file "
        str = str & "where id_sumber = " & dr("id_driver") & " "
        str = str & "And jenis = 'Photo' and sumber = 'DOCSUPIR' "
        str = str & "order by id_files desc "
        salah = Mod_Utama.isi_data(Me.dt_supir_img, str, "id_files", Me.waktu_query)
        If salah.er_hasil <> "" Then Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)

        Select Case dr("id_jenis")
            Case "1"
                rpt.lb_jenis.Text = "ACCIDENT"
            Case "2"
                rpt.lb_jenis.Text = "INCIDENT"
            Case "3"
                rpt.lb_jenis.Text = "NEARMISS"
        End Select

        Select Case dr("id_cuaca")
            Case "1"
                rpt.lb_cuaca.Text = "GERIMIS"
            Case "2"
                rpt.lb_cuaca.Text = "CERAH"
            Case "3"
                rpt.lb_cuaca.Text = "HUJAN"
        End Select

        Select Case dr("id_status")
            Case "1"
                rpt.lb_status.Text = "Arah Berangkat"
            Case "2"
                rpt.lb_status.Text = "Arah Pulang"
            Case "3"
                rpt.lb_status.Text = "Loading"
            Case "4"
                rpt.lb_status.Text = "Unloading"
        End Select

        rpt.lb_lokasi.Text = "" & dr("lokasi") & ""
        rpt.lb_no.Text = "" & dr("no_trs") & ""
        rpt.lb_tgl.Text = "" & Format(dr("u_date"), "dd-MM-yyyy") & ""
        rpt.rc_krono.Text = "" & dr("krono") & ""
        rpt.rc_analis.Text = "" & dr("cause_analis") & ""
        rpt.rc_rusak.Text = "" & dr("rusak") & ""
        rpt.rc_biaya.Text = "Rp. " & dr("biaya") & ""
        rpt.lb_supir.Text = "" & dr("supir1") & ""
        rpt.lb_exp_sim.Text = "" & dr("sim_exp") & ""
        rpt.lb_tgl_kejadian.Text = "" & Format(dr("tgl_kejadian"), "dd-MM-yyyy") & ""
        rpt.lb_jam_kejadian.Text = "" & dr("jam") & ""
        rpt.lb_nopol.Text = "" & dr("nopol") & ""
        rpt.lb_jenis_sim.Text = "" & dr("jns_sim") & ""
        rpt.tgl_ttd.Text = "Jakarta, " & Format(dr("u_date"), "dd-MM-yyyy")
        rpt.lb_umur.Text = "" & dr("tgl_lhr_thn") & " Tahun, " & dr("tgl_lhr_bln") & " Bulan"
        rpt.lb_kerja.Text = "" & dr("kerja_thn") & " Tahun, " & dr("kerja_bln") & " Bulan"
        rpt.ttdspr.Text = "" & dr("supir1") & ""
        If IsDBNull("no_ba") = True Then
            rpt.lb_noba.Text = ""
        Else
            rpt.lb_noba.Text = "" & dr("no_ba") & ""
        End If

        If Me.dt_supir_img.Rows.Count <> 0 Then
            drimg = Me.dt_supir_img.Rows(0)

            rpt.pic_supir1.ImageUrl = "http://" & HttpContext.Current.Request.Url.Host & ":" & HttpContext.Current.Request.Url.Port & "/Files/" & drimg("file1")
            rpt.pic_supir1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage

        End If

        'gambar
        Dim dt_gbr As New DataTable
        str = "select * from mds_log_file where sumber = 'ACCINC' and id_sumber = " & Me.idrec
        salah = Mod_Utama.isi_data(dt_gbr, str, "id_files", Me.waktu_query)
        Dim client As System.Net.WebClient

        For Each dr_cek As DataRow In dt_gbr.Rows
            If dr_cek("jenis") = "Pihak 1" Then

                Dim dtimg As New DataTable
                str = "select * from mds_log_file where sumber = 'ACCINC' and id_sumber = " & Me.idrec & " and jenis = 'Pihak 1' "
                salah = Mod_Utama.isi_data(dtimg, str, "id_files", Me.waktu_query)

                Dim lebar As Integer = rpt.pnl_gbr1.WidthF - 10
                Dim jml As Integer = dtimg.Rows.Count
                If jml <> 0 Then
                    Dim kiri As Integer = 10
                    Dim kelip As Integer = 3
                    Dim lbr_gbr As Integer = lebar / kelip

                    If jml < kelip Then lbr_gbr = lebar / jml

                    lbr_gbr -= 6
                    Dim jml_y As Integer = jml \ kelip
                    Dim sisa As Integer = jml Mod kelip

                    If sisa > 0 Then jml_y += 1

                    Dim atas As Integer = 0
                    Dim tinggi As Integer = 250

                    If jml_y > 1 Then tinggi = 150

                    For i = 0 To jml - 1

                        dr = dtimg.Rows(i)
                        'client.DownloadData("http://localhost:34472" & dr("imgurl"))

                        Dim gbr As New DevExpress.XtraReports.UI.XRPictureBox
                        gbr.Visible = True

                        gbr.ImageUrl = "http://" & HttpContext.Current.Request.Url.Host & ":" & HttpContext.Current.Request.Url.Port & "/Files/" & dr("file_nm")
                        gbr.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage
                        gbr.Size = New System.Drawing.Size(lbr_gbr, tinggi)
                        'gbr.HeightF = 1000
                        'gbr.WidthF = 2000
                        gbr.LocationF = New DevExpress.Utils.PointFloat(kiri, atas)
                        gbr.BorderColor = Drawing.Color.White
                        gbr.BorderWidth = 10
                        gbr.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid
                        rpt.pnl_gbr1.Controls.Add(gbr)

                        kiri += lbr_gbr + 6
                        If kiri + lbr_gbr - 5 > lebar Then
                            atas += tinggi + 6
                            kiri = 10
                        End If
                    Next
                    rpt.pnl_gbr1.HeightF = tinggi * jml_y + 10
                End If

            ElseIf dr_cek("jenis") = "Pihak 2" Then

                Dim dtimg As New DataTable
                str = "select * from mds_log_file where sumber = 'ACCINC' and id_sumber = " & Me.idrec & " and jenis = 'Pihak 2' "
                salah = Mod_Utama.isi_data(dtimg, str, "id_files", Me.waktu_query)

                Dim lebar As Integer = rpt.pnl_gbr2.WidthF - 10
                Dim jml As Integer = dtimg.Rows.Count
                If jml <> 0 Then
                    Dim kiri As Integer = 10
                    Dim kelip As Integer = 3
                    Dim lbr_gbr As Integer = lebar / kelip

                    If jml < kelip Then lbr_gbr = lebar / jml

                    lbr_gbr -= 6
                    Dim jml_y As Integer = jml \ kelip
                    Dim sisa As Integer = jml Mod kelip

                    If sisa > 0 Then jml_y += 1

                    Dim atas As Integer = 0
                    Dim tinggi As Integer = 250

                    If jml_y > 1 Then tinggi = 150

                    For i = 0 To jml - 1
                        dr = dtimg.Rows(i)
                        Dim gbr = New DevExpress.XtraReports.UI.XRPictureBox
                        gbr.Visible = True
                        gbr.ImageUrl = "http://" & HttpContext.Current.Request.Url.Host & ":" & HttpContext.Current.Request.Url.Port & "/Files/" & dr("file_nm")
                        gbr.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage
                        gbr.Size = New System.Drawing.Size(lbr_gbr, tinggi)
                        gbr.LocationF = New DevExpress.Utils.PointFloat(kiri, atas)
                        gbr.BorderColor = Drawing.Color.White
                        gbr.BorderWidth = 10
                        gbr.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid
                        rpt.pnl_gbr2.Controls.Add(gbr)

                        kiri += lbr_gbr + 6
                        If kiri + lbr_gbr - 5 > lebar Then
                            atas += tinggi + 6
                            kiri = 10
                        End If
                    Next
                    rpt.pnl_gbr2.HeightF = tinggi * jml_y + 10
                End If


            End If
        Next

        Me.ASPxDocumentViewer1.Report = rpt

    End Sub

    Protected Sub teguran()
        Dim rpt As New rpt_teguran

        str = "select *, "
        str = str & "(select format(tgl_berlaku, 'dd MMMM yyyy', 'ID-id')) as berlaku, "
        str = str & "(select format(tgl_selesai, 'dd MMMM yyyy', 'ID-id')) as selesai, "
        str = str & "(select nama from tms_mst_driver where id_driver = mds_trs_teguran.id_spr) as nm_supir, "
        str = str & "(select kategori from tms_mst_driver where id_driver = mds_trs_teguran.id_spr) as jabatan, "
        str = str & "(select nama from mst_lokasi where id_lokasi in (select id_lokasi from tms_mst_driver where id_driver = mds_trs_teguran.id_spr)) as unit_kerja "
        str = str & "from mds_trs_teguran "
        str = str & "where id_teguran = " & Me.idrec & " "

        Me.salah = Mod_Utama.isi_data(Me.dt_teguran, str, "id_teguran", waktu_query)
        If salah.er_hasil <> "" Then Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)

        dr = Me.dt_teguran.Rows(0)

        rpt.lb_nm_supir.Text = dr("nm_supir")
        rpt.lb_jabatan.Text = dr("jabatan")
        rpt.lb_unit_kerja.Text = dr("unit_kerja")
        rpt.lb_catatan.Text = dr("catatan")
        rpt.lb_alasan.Text = dr("alasan")
        rpt.lb_berlaku.Text = dr("berlaku")
        rpt.lb_selesai_berlaku.Text = dr("selesai")
        rpt.XrLabel15.Text = dr("unit_kerja") & ", " & Format(dr("tgl"), "yyyy-MM-dd")
        rpt.lb_driver.Text = dr("nm_supir")

        Select Case dr("id_teguran_jns")
            Case 1
                rpt.cb_teguran.Checked = True
            Case 2
                rpt.cb_sp1.Checked = True
                rpt.cb_skorsing1.Checked = True
            Case 3
                rpt.cb_sp2.Checked = True
                rpt.cb_skorsing2.Checked = True
            Case 4
                rpt.cb_sp3.Checked = True
                rpt.cb_phk.Checked = True
        End Select

        'Me.ReportViewer1.Report = rpt
        Me.ASPxDocumentViewer1.Report = rpt
    End Sub

End Class