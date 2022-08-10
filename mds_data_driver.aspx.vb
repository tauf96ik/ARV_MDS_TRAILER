Imports DevExpress.Web
Imports System.Reflection.MethodBase
Imports System.Web.HttpPostedFile
Imports System.Drawing
Imports System.IO

Public Class mds_data_driver
    Inherits System.Web.UI.Page

    Dim dr_user As DataRow
    Dim str As String
    Dim salah As er_custom
    Dim waktu_query As New Stopwatch
    Dim waktu_page As New Stopwatch
    Dim dt As New DataTable
    Dim cb As GridViewDataComboBoxColumn
    'Dim str_menu As String = ",40,"
    'Dim str_app As String = ",5,"
    Dim dr As DataRow

    Dim id_supir As Int64
    Dim dt_head As New DataTable
    Dim dr_head As DataRow
    Dim dt_lokasi As New DataTable

    Private Sub mds_data_driver_Init(sender As Object, e As EventArgs) Handles Me.Init
        Try
            id_supir = Request.QueryString("id_supir")
        Catch ex As Exception
            Response.Redirect("mds_driver_active.aspx")
        End Try

        dr_user = Session("dr_user")
        'Me.uc_header.grid = Me.ASPxGridView1

        str = "<li><a href='home.aspx'>HOME</a></li>"
        str = str & "<li class='active'>EXISTING</li>"
        str = str & "<li><a href='mds_driver_active.aspx'>Daftar Driver Active</a></li>"
        str = str & "<li><a href='mds_data_driver.aspx?id_supir = " & Me.id_supir & "' style='color: #f00'>Driver No. " & Me.id_supir & "</a></li>"
        Me.uc_header.list_menu.InnerHtml = str

        Me.uc_header.div_search.Visible = False
        Me.uc_header.a_filter.Visible = False

        Me.isi_head()
        Me.isi_lokasi()
        'Me.isi_data()
    End Sub

    Private Sub page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Me.waktu_page.Start()
    End Sub
    Private Sub page_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub isi_head()
        str = "select *, "
        str = str & "(select nama from opr_mst_jns_sim where id_jns_sim = A.id_jns_sim) as jns_sim, "
        str = str & "('') as nm_jbtn, "
        str = str & "(select nama from mst_pendidikan where id_pendidikan = A.id_pendidikan) as nm_pendidikan, "
        str = str & "(select '') as masakerja, "
        str = str & "(select '') as umur, "
        str = str & "(select '') as status, "
        str = str & "(select nama from mst_lokasi where id_lokasi = A.id_lokasi) as lokasi "
        str = str & "from box_mst_driver A "
        str = str & "where id_driver = " & Me.id_supir & " "
        salah = Mod_Utama.isi_data(Me.dt_head, str, "id_driver", Me.waktu_query)
        If salah.er_hasil <> "" Then Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)

        Dim bln As Integer
        Dim thn As Integer
        Dim bln_lhr As Integer
        Dim thn_lhr As Integer
        For Each dtr As DataRow In Me.dt_head.Rows
            bln = DateDiff(DateInterval.Month, dtr("tgl_masuk"), Now)
            thn = bln / 12
            bln_lhr = DateDiff(DateInterval.Month, dtr("tgl_lahir"), Now)
            thn_lhr = bln_lhr / 12
            dtr("masakerja") = thn.ToString & " | " & bln Mod 12
            dtr("umur") = thn_lhr.ToString & " | " & bln_lhr Mod 12
            If dtr("sta") = 0 Then
                dtr("status") = "OFF"
            Else
                dtr("status") = "ON"
            End If
        Next
        Me.dt_head.AcceptChanges()

        dr_head = Me.dt_head.Rows(0)

        'Profile
        tx_nama.Value = dr_head("nama")
        tx_tgl_lhr.Value = dr_head("tgl_lahir")
        tx_no_ktp.Value = dr_head("no_ktp")
        tx_phone.Value = dr_head("no_hp")

        'Alamat
        tx_alamat.Value = dr_head("alamat")

        'Keterangan Mitra Kercb_lokasi_kerjaja
        tx_lama_kerja.Value = dr_head("masakerja")
        tx_pendidikan.Value = dr_head("nm_pendidikan")
        tx_no_sim.Value = dr_head("no_sim")
        tx_jns_sim.Value = dr_head("jns_sim")
        tx_no_bpjs.Value = dr_head("bpjs_no")
        tx_no_rek.Value = dr_head("rek_no")
        tx_tgl_bergabung.Value = dr_head("tgl_masuk")
        'tx_jbtn.Value = dr_head("nm_jbtn")

        'cb_lokasi_kerja.DataSource = dr_head
        'cb_lokasi_kerja.ValueField = "id_lokasi"
        'cb_lokasi_kerja.TextField = "lokasi"
        'cb_lokasi_kerja.IncrementalFilteringMode = IncrementalFilteringMode.Contains


    End Sub
    Private Sub isi_data()
        str = "SELECT * "
        str = str & "FROM mds_driver_doc "
        str = str & "where id_supir = " & Me.id_supir & " "
        str = str & "and jenis = 'Photo' "
        str = str & "order by id_supir_doc desc "
        salah = Mod_Utama.isi_data(Me.dt, str, "id_supir_doc", Me.waktu_query)
        If salah.er_hasil <> "" Then Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)

        If dt.Rows.Count <> 0 Then
            dr = Me.dt.Rows(0)

            img_supir.ImageUrl = "doc_supir\" & dr("file_nm")
        End If

    End Sub
    Private Sub isi_lokasi()
        str = "select * "
        str = str & "from mst_lokasi"
        salah = Mod_Utama.isi_data(Me.dt_lokasi, str, "id_lokasi", Me.waktu_query)
        If salah.er_hasil <> "" Then Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)

        Me.dt_lokasi.DefaultView.Sort = "id_lokasi, nama"

        'cb_lokasi_kerja.DataSource = Me.dt_lokasi
        'cb_lokasi_kerja.ValueField = "id_lokasi"

        'cb_lokasi_kerja.TextField = "nama"
        'cb_lokasi_kerja.IncrementalFilteringMode = IncrementalFilteringMode.Contains

        'cb = Me.ASPxGridView1.Columns("id_cust_cab")
        'cb.PropertiesComboBox.DataSource = Me.dt_cust_cab
        'cb.PropertiesComboBox.ValueField = "id_cust"
        'cb.PropertiesComboBox.TextField = "cabang"
        'cb.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains

        'cb = Me.ASPxGridView1.Columns("id_cust_inv_cab")
        'cb.PropertiesComboBox.DataSource = Me.dt_cust_cab
        'cb.PropertiesComboBox.ValueField = "id_cust"
        'cb.PropertiesComboBox.TextField = "cabang"
        'cb.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains

    End Sub

End Class