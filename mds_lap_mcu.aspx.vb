Imports DevExpress.Web
Imports System.Reflection.MethodBase

Public Class mds_lap_mcu
    Inherits System.Web.UI.Page

    Dim dr_user As DataRow
    Dim str As String
    Dim salah As er_custom
    Dim waktu_query As New Stopwatch
    Dim waktu_page As New Stopwatch
    Dim dt As New DataTable
    Dim cb As GridViewDataComboBoxColumn
    Dim str_menu As String = ",30,"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.isi_data()
    End Sub

    Protected Sub bt_refresh_ServerClick(sender As Object, e As EventArgs)

    End Sub

    Private Sub mds_lap_mcu_Init(sender As Object, e As EventArgs) Handles Me.Init
        dr_user = Session("dr_user")
        Me.uc_header.pivot = Me.ASPxPivotGrid1  'penting

        str = "<li><a href='home.aspx'>HOME</a></li>"
        str = str & "<li class='active'>LAPORAN</li>"
        str = str & "<li><a href='mds_lap_mcu.aspx'>Laporan Medical Check Up</a></li>"
        Me.uc_header.list_menu.InnerHtml = str



        If IsPostBack = False Then
            Me.s_date.Date = Format(Now.AddMonths(-1), "yyyy-MM-dd")
            Me.e_date.Date = Format(Now, "yyyy-MM-dd")
        End If
    End Sub

    Private Sub mds_lap_mcu_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        Me.waktu_page.Stop()
        Mod_Utama.master_waktu(Me.waktu_query, Me.waktu_page, uc_footer)
    End Sub

    Private Sub mds_lap_mcu_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Me.waktu_page.Start()
    End Sub

    Private Sub Jika_Error(er_str As String, er_hasil As String, er_menu As String, nopesan As Integer)
        salah.er_str = er_str
        salah.er_menu = er_menu
        salah.er_hasil = er_hasil
        salah.er_tpage = Me.waktu_page
        salah.er_tquery = Me.waktu_query
        Session("error") = salah

        Select Case nopesan
            Case 1
                Mod_Utama.tampil_error(Me, "Terjadi kesalahan pada Query, harap kirim laporan ke MIS via email")
            Case Else
                Mod_Utama.tampil_error(Me, "Terjadi kesalahan pada proses, harap kirim laporan ke MIS via email")
        End Select
    End Sub

    Private Sub isi_data()
        str = "select *, "
        str = str & "(select 1 ) as qty, "
        str = str & "(select CASE sta_suhu WHEN 1 THEN 'Good' ELSE 'Not Good' End) as kondisi_suhu, "
        str = str & "(select CASE sta_tekanan WHEN 1 THEN 'Good' ELSE 'Not Good' End) as kondisi_tekanan, "
        str = str & "(select nama from tms_mst_driver where id_driver = mds_trs_mcu.id_spr ) as driver, "
        str = str & "(select nama from mst_lokasi where id_lokasi in (select id_lokasi from tms_mst_driver where id_driver = mds_trs_mcu.id_spr)) as lokasi, "
        str = str & "(select kategori from tms_mst_driver where id_driver = mds_trs_mcu.id_spr ) as job_title, "
        str = str & "(select nama from opr_mst_kab where id_kab = mds_trs_mcu.id_kab ) as tujuan, "
        str = str & "(select nopol from tms_mst_nopol where id_nopol = mds_trs_mcu.id_nopol ) as nopol "
        str = str & "from mds_trs_mcu "
        str = str & "WHERE tgl "
        str = str & "between '" & Format(Me.s_date.Date, "yyyy-MM-dd") & "' "
        str = str & "and '" & Format(Me.e_date.Date, "yyyy-MM-dd") & "' "

        salah = Mod_Utama.isi_data(Me.dt, str, "id_mcu", Me.waktu_query)

        If salah.er_hasil <> "" Then
            Me.Jika_Error(str, salah.er_hasil, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
            Exit Sub
        End If

        Me.ASPxPivotGrid1.DataSource = Me.dt
        Me.ASPxPivotGrid1.OptionsView.ShowColumnGrandTotals = False
        Me.ASPxPivotGrid1.DataBind()
    End Sub

End Class