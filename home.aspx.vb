Imports DevExpress.Web
Imports System.Reflection.MethodBase

Public Class home
    Inherits System.Web.UI.Page

    Dim dr_user As DataRow
    Dim str As String
    Dim salah As er_custom
    Dim waktu_query As New Stopwatch
    Dim waktu_page As New Stopwatch
    Dim dt As New DataTable
    Dim cb As GridViewDataComboBoxColumn

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
    Private Sub page_Init(sender As Object, e As EventArgs) Handles Me.Init
        dr_user = Session("dr_user")

        str = "<li><a href='home.aspx'>HOME</a></li>"
        Me.uc_header.list_menu.InnerHtml = str
        'Me.div_approval.InnerHtml = ""
        Me.ul_approve.InnerHtml = ""
        Me.ul_cc.InnerHtml = ""
        Me.ul_info.InnerHtml = ""
    End Sub
    Protected Sub isi_supir(title As String, jml As Integer, pg As String, Optional inputlog As Boolean = False)
        If jml = 0 Then
            Me.ul_supir.InnerHtml &= "<li class='list-group-item'><span class='badge'>" & jml & "</span><a href='" & pg & "' style='color: #000000'><i class='icon-checkmark'></i>" & title & "</a></li>"
        Else
            Me.ul_supir.InnerHtml &= "<li class='list-group-item'><span class='badge bg-danger'>" & jml & "</span><a href='" & pg & "' style='color: #000000'><i class='icon-checkmark'></i>" & title & "</a></li>"
        End If
    End Sub
    Protected Sub isi_approve(title As String, jml As Integer, pg As String, Optional inputlog As Boolean = False)
        If jml = 0 Then
            Me.ul_approve.InnerHtml &= "<li class='list-group-item'><span class='badge'>" & jml & "</span><a href='" & pg & "' style='color: #000000'><i class='icon-checkmark'></i>" & title & "</a></li>"
        Else
            Me.ul_approve.InnerHtml &= "<li class='list-group-item'><span class='badge bg-danger'>" & jml & "</span><a href='" & pg & "' style='color: #000000'><i class='icon-checkmark'></i>" & title & "</a></li>"
        End If
    End Sub
    Protected Sub isi_cc(title As String, jml As Integer, pg As String, Optional inputlog As Boolean = False)
        If jml = 0 Then
            Me.ul_cc.InnerHtml &= "<li class='list-group-item'><span class='badge'>" & jml & "</span><a href='" & pg & "' style='color: #000000'><i class='icon-bubble-notification2'></i>" & title & "</a></li>"
        Else
            Me.ul_cc.InnerHtml &= "<li class='list-group-item'><span class='badge bg-danger'>" & jml & "</span><a href='" & pg & "' style='color: #000000'><i class='icon-bubble-notification2'></i>" & title & "</a></li>"
        End If
    End Sub
    Protected Sub isi_info(title As String, jml As Integer, pg As String, Optional inputlog As Boolean = False)
        If jml = 0 Then
            Me.ul_info.InnerHtml &= "<li class='list-group-item'><span class='badge'>" & jml & "</span><a href='" & pg & "' style='color: #000000'><i class='icon-spam'></i>" & title & "</a></li>"
        Else
            Me.ul_info.InnerHtml &= "<li class='list-group-item'><span class='badge bg-danger'>" & jml & "</span><a href='" & pg & "' style='color: #000000'><i class='icon-spam'></i>" & title & "</a></li>"
        End If
    End Sub
    Protected Sub isi_tmc(title As String, jml As Integer, pg As String, Optional inputlog As Boolean = False)
        If jml = 0 Then
            Me.ul_tmc.InnerHtml &= "<li class='list-group-item'><span class='badge'>" & jml & "</span><a href='" & pg & "' style='color: #000000'><i class='icon-spam'></i>" & title & "</a></li>"
        Else
            Me.ul_tmc.InnerHtml &= "<li class='list-group-item'><span class='badge bg-danger'>" & jml & "</span><a href='" & pg & "' style='color: #000000'><i class='icon-spam'></i>" & title & "</a></li>"
        End If
    End Sub

    Protected Sub isi_ultah(title As String, jml As Integer, pg As String, Optional inputlog As Boolean = False)
        If jml = 0 Then
            Me.ul_supir.InnerHtml &= "<li class='list-group-item'><span class='badge'>" & jml & "</span><a href='" & pg & "' style='color: #000000'><i class='icon-balloon'></i>" & title & "</a></li>"
        Else
            Me.ul_supir.InnerHtml &= "<li class='list-group-item'><span class='badge bg-danger'>" & jml & "</span><a href='" & pg & "' style='color: #000000'><i class='icon-balloon'></i>" & title & "</a></li>"
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.driver_sim_2bulan()
        Me.driver_sim_1bulan()
        Me.driver_sim_1minggu()
        Me.driver_rek()
        Me.ultah()
        'Me.driver_img()
        'Me.driver_blm_siap()
        'Me.driver_blm_railing()
        'Me.driver_blm_training()
        'Me.driver_no_noabsen()

        'Me.cc_no_supir()
        'Me.standby_driver()
    End Sub

    Protected Sub driver_sim_2bulan()
        Dim dt As New DataTable
        str = "select count(id_driver) as jml from tms_mst_driver "
        str = str & "where exp_sim < DATEADD(m, 2, convert(date,getdate())) "
        Me.salah = Mod_Utama.isi_data(dt, str, "id_driver", waktu_query)
        If salah.er_hasil <> "" Then
            Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
            Exit Sub
        End If

        Me.isi_approve("Driver yang Sim akan Expired 2 Bulan", dt.Rows(0)("jml"), "mds_driver_active.aspx?dari=exp2bln")
    End Sub
    Protected Sub driver_sim_1bulan()
        Dim dt As New DataTable
        str = "select count(id_driver) as jml from tms_mst_driver "
        str = str & "where exp_sim < DATEADD(m, 1, convert(date,getdate())) "
        Me.salah = Mod_Utama.isi_data(dt, str, "id_driver", waktu_query)
        If salah.er_hasil <> "" Then
            Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
            Exit Sub
        End If

        Me.isi_approve("Driver yang Sim akan Expired 1 Bulan", dt.Rows(0)("jml"), "mds_driver_active.aspx?dari=exp1bln")
    End Sub
    Protected Sub driver_sim_1minggu()
        Dim dt As New DataTable
        str = "select count(id_driver) as jml from tms_mst_driver "
        str = str & "where exp_sim < DATEADD(w, 1, convert(date,getdate())) "
        Me.salah = Mod_Utama.isi_data(dt, str, "id_driver", waktu_query)
        If salah.er_hasil <> "" Then
            Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
            Exit Sub
        End If

        Me.isi_approve("Driver yang Sim akan/telah Expired 1 Minggu", dt.Rows(0)("jml"), "mds_driver_active.aspx?dari=exp1mgg")
    End Sub
    Protected Sub driver_rek()
        Dim dt As New DataTable
        str = "select count(id_driver) as jml from tms_mst_driver "
        str = str & "where len(rek_no) < 3 "
        Me.salah = Mod_Utama.isi_data(dt, str, "id_driver", waktu_query)
        If salah.er_hasil <> "" Then
            Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
            Exit Sub
        End If

        Me.isi_approve("Driver/Assisten yang belum punya No.Rekening", dt.Rows(0)("jml"), "mds_driver_active.aspx?dari=norek")
    End Sub
    Protected Sub driver_img()
        Dim dt As New DataTable
        str = "with contoh as (select id_driver, "
        str = str & "(select count(id_supir_doc) from mds_driver_doc where id_supir = opr_mst_supir.id_supir) as jml "
        str = str & "from opr_mst_supir "
        str = str & "where aktif_sta = 1) "
        str = str & "select count(jml) as jml from contoh "
        str = str & "where jml < 4 "
        Me.salah = Mod_Utama.isi_data(dt, str, "id_supir", waktu_query)
        If salah.er_hasil <> "" Then
            Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
            Exit Sub
        End If

        Me.isi_supir("Driver/Assisten Belum Mencapai Minimal Upload 4 Image", dt.Rows(0)("jml"), "mds_driver_active.aspx")
    End Sub
    Protected Sub driver_blm_siap()
        Dim dt As New DataTable
        str = "select count(id_supir) as jml from opr_mst_supir "
        str = str & "where siap_sta = 0 "
        str = str & "and aktif_sta = 1 "
        Me.salah = Mod_Utama.isi_data(dt, str, "id_supir", waktu_query)
        If salah.er_hasil <> "" Then
            Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
            Exit Sub
        End If

        Me.isi_supir("Driver/Assisten yang belum Siap Pakai", dt.Rows(0)("jml"), "mds_driver_active.aspx")
    End Sub
    Protected Sub driver_blm_training()
        Dim dt As New DataTable
        str = "select A.id_supir, "
        str = str & "isnull((select count(id_train_supir) from mds_trs_train_supir where id_supir = A.id_supir),0) as jml_train "
        str = str & "from opr_mst_supir A "
        str = str & "where A.siap_sta = 0 "
        str = str & "and aktif_sta = 1 "
        Me.salah = Mod_Utama.isi_data(dt, str, "id_supir", waktu_query)
        If salah.er_hasil <> "" Then
            Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
            Exit Sub
        End If

        Dim jml As Integer = dt.Compute("count(id_supir)", "jml_train < 2")
        Me.isi_supir("Driver/Assisten yang belum Min 2 Training", jml, "mds_driver_active.aspx")
    End Sub
    Protected Sub driver_blm_railing()
        Dim dt As New DataTable
        str = "select A.id_supir, "
        str = str & "isnull((select count(id_railing) from mds_trs_railing where id_supir = A.id_supir),0) as jml "
        str = str & "from opr_mst_supir A "
        str = str & "where A.siap_sta = 0 "
        str = str & "and aktif_sta = 1 "
        Me.salah = Mod_Utama.isi_data(dt, str, "id_supir", waktu_query)
        If salah.er_hasil <> "" Then
            Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
            Exit Sub
        End If

        Dim jml As Integer = dt.Compute("count(id_supir)", "jml < 3")
        Me.isi_supir("Driver/Assisten yang belum Min 3 Railing", jml, "mds_driver_active.aspx")
    End Sub

    Protected Sub driver_no_noabsen()
        Dim dt As New DataTable
        str = "select count(id_supir) as jml from opr_mst_supir "
        str = str & "where no_absen = '' "
        str = str & "and aktif_sta = 1 "
        str = str & "and jbtn in (1, 2, 3, 5) "
        Me.salah = Mod_Utama.isi_data(dt, str, "id_supir", waktu_query)
        If salah.er_hasil <> "" Then
            Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
            Exit Sub
        End If

        Me.isi_supir("Driver/Assisten yang belum memiliki No. Absen", dt.Rows(0)("jml"), "mst_absen.aspx")
    End Sub

    Protected Sub cc_no_supir()
        Dim dt As New DataTable
        str = "select id_cc, id_supir1, id_supir2 "
        str = str & "from opr_mst_cc A "
        str = str & "where (id_supir1 = 0 or id_supir2 = 0) "
        str = str & "and off_sta = 0 "
        Me.salah = Mod_Utama.isi_data(dt, str, "id_cc", waktu_query)
        If salah.er_hasil <> "" Then
            Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
            Exit Sub
        End If

        Dim jml As Integer = dt.Compute("count(id_cc)", "id_supir1=0")
        Me.isi_cc("CC yang belum memiliki Supir 1", jml, "trs_cc_driver.aspx?spr=1")

        jml = dt.Compute("count(id_cc)", "id_supir2=0")
        Me.isi_cc("CC yang belum memiliki Supir 2", jml, "trs_cc_driver.aspx?spr=2")

    End Sub

    Protected Sub standby_driver()
        Dim dt As New DataTable
        str = "select id_standby, status "
        str = str & "from pool_trs_standby A "
        str = str & "where spk_list is null "
        str = str & "and tgl = '" & Format(Now, "yyyy-MM-dd") & "' "
        Me.salah = Mod_Utama.isi_data(dt, str, "id_standby", waktu_query)
        If salah.er_hasil <> "" Then
            Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
            Exit Sub
        End If

        Dim jml As Integer = dt.Compute("count(id_standby)", "status=1")
        Me.isi_cc("Driver 1 Standby Hari Ini", jml, "mon_standby.aspx")

        jml = dt.Compute("count(id_standby)", "status=2")
        Me.isi_cc("Driver 2 Standby Hari Ini", jml, "mon_standby.aspx")
    End Sub

    Protected Sub ultah()
        Dim dt As New DataTable
        str = "select count(*) as jml from tms_mst_driver "
        str = str & "where MONTH(tgl_lahir) = MONTH(GETDATE()) and DAY(tgl_lahir) = Day(getdate()) "
        'str = str & "and sta = 1 "
        Me.salah = Mod_Utama.isi_data(dt, str, "id_supir", waktu_query)
        If salah.er_hasil <> "" Then
            Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
            Exit Sub
        End If

        Me.isi_ultah("Driver yang berulang tahun hari ini", dt.Rows(0)("jml"), "mds_driver_active.aspx?dari=ultah")
    End Sub

End Class