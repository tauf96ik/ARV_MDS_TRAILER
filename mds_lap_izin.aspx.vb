Imports DevExpress.Web
Imports System.Reflection.MethodBase

Public Class mds_lap_izin
    Inherits System.Web.UI.Page

    Dim dr_user As DataRow
    Dim str As String
    Dim salah As er_custom
    Dim waktu_query As New Stopwatch
    Dim waktu_page As New Stopwatch
    Dim dt As New DataTable
    Dim dr As DataRow
    Dim cb As GridViewDataComboBoxColumn
    Dim str_menu As String = ",29,"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.isi_data()
    End Sub

    Protected Sub bt_refresh_ServerClick(sender As Object, e As EventArgs)

    End Sub

    Private Sub mds_lap_izin_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        Me.waktu_page.Stop()
        Mod_Utama.master_waktu(Me.waktu_query, Me.waktu_page, uc_footer)
    End Sub

    Private Sub mds_lap_izin_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Me.waktu_page.Start()
    End Sub

    Private Sub mds_lap_izin_Init(sender As Object, e As EventArgs) Handles Me.Init
        dr_user = Session("dr_user")
        Me.uc_header.pivot = Me.ASPxPivotGrid1  'penting

        str = "<li><a href='home.aspx'>HOME</a></li>"
        str = str & "<li class='active'>LAPORAN</li>"
        str = str & "<li><a href='mds_lap_izin.aspx'>Laporan Izin Driver</a></li>"
        Me.uc_header.list_menu.InnerHtml = str

        If IsPostBack = False Then
            Me.s_date.Date = Format(Now.AddMonths(-1), "yyyy-MM-dd")
            Me.e_date.Date = Format(Now, "yyyy-MM-dd")
        End If
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
        Dim fi_lim As uc_header.filter_limit = uc_header.filtertext
        str = "select *, "
        str = str & "(datediff(day, tgl_mulai, tgl_akhir) + 1) as hari, "
        str = str & "(select nama from tms_mst_driver where id_driver = mds_trs_izin.id_driver) as supir, "
        str = str & "(select nama from mds_mst_izin_jns where id_izin_jns = mds_trs_izin.id_izin_jns) as jenis_izin "
        str = str & "from mds_trs_izin where tgl_mulai >= '" & Format(Me.s_date.Value, "yyyy-MM-dd") & "' and tgl_akhir <= '" & Format(Me.e_date.Value, "yyyy-MM-dd") & "' "
        str = str & "and id_izin is not null "
        str = str & fi_lim.str_filter
        str = str & "order by id_izin desc "
        Me.salah = Mod_Utama.isi_data(Me.dt, str, "id_izin", waktu_query)
        If salah.er_hasil <> "" Then
            Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
            Return
        End If

        Me.dt.AcceptChanges()

        Me.dt.DefaultView.RowFilter = Session("rowfilter")
        Me.ASPxPivotGrid1.DataSource = Me.dt
        Me.ASPxPivotGrid1.DataBind()
        Mod_Utama.Atur_pivot(Me.ASPxPivotGrid1)
    End Sub

End Class