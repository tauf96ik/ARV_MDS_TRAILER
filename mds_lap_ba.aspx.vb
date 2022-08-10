Imports DevExpress.Web
Imports System.Reflection.MethodBase

Public Class mds_lap_ba
    Inherits System.Web.UI.Page

    Dim dr_user As DataRow
    Dim str As String
    Dim salah As er_custom
    Dim waktu_query As New Stopwatch
    Dim waktu_page As New Stopwatch
    Dim dt As New DataTable
    Dim dr As DataRow
    Dim cb As GridViewDataComboBoxColumn
    Dim str_menu As String = ",35,"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.isi_data()
    End Sub

    Protected Sub bt_refresh_ServerClick(sender As Object, e As EventArgs)

    End Sub

    Private Sub mds_lap_ba_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Me.waktu_page.Start()
    End Sub

    Private Sub mds_lap_ba_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        Me.waktu_page.Stop()
        Mod_Utama.master_waktu(Me.waktu_query, Me.waktu_page, uc_footer)
    End Sub

    Private Sub mds_lap_ba_Init(sender As Object, e As EventArgs) Handles Me.Init
        dr_user = Session("dr_user")
        Me.uc_header.pivot = Me.ASPxPivotGrid1  'penting

        str = "<li><a href='home.aspx'>HOME</a></li>"
        str = str & "<li class='active'>LAPORAN</li>"
        str = str & "<li><a href='mds_lap_ba.aspx'>Laporan Berita Acara</a></li>"
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
        Dim awal As String = Format(Me.s_date.Value, "dd MMM yyyy")
        Dim akhir As String = Format(Me.e_date.Value, "dd MMM yyyy")

        str = "select A.*, "
        str = str & "(select nama from mds_mst_ba where id_ba = A.id_ba) as ba, "
        str = str & "(select nama from tms_mst_driver where id_driver = A.id_driver) as supir, "
        str = str & "(select nopol from tms_mst_nopol where id_nopol = A.id_nopol) as nopol, "
        str = str & "(select '') as thnbln, "
        str = str & "(select 1) as qty "
        str = str & "from mds_trs_ba A "
        str = str & "where A.tgl_kejadian >= '" & Format(Me.s_date.Value, "yyyy-MM-dd") & "' "
        str = str & "and A.tgl_kejadian <= '" & Format(Me.e_date.Value, "yyyy-MM-dd") & "' "
        Me.salah = Mod_Utama.isi_data(Me.dt, str, "", waktu_query)
        If salah.er_hasil <> "" Then
            Me.Jika_Error(str, salah.er_hasil, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
            Exit Sub
        End If

        For Each dtr As DataRow In Me.dt.Rows
            dtr("thnbln") = Format(dtr("tgl_kejadian"), "yyyy-MM")
        Next
        Me.dt.AcceptChanges()

        Me.ASPxPivotGrid1.DataSource = Me.dt
        Me.ASPxPivotGrid1.Caption = awal & " s/d " & akhir
        Me.ASPxPivotGrid1.DataBind()
        Mod_Utama.Atur_pivot(Me.ASPxPivotGrid1)
    End Sub
End Class