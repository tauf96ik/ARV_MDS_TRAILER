Imports DevExpress.Web
Imports System.Reflection.MethodBase

Public Class mds_lap_supir
    Inherits System.Web.UI.Page

    Dim dr_user As DataRow
    Dim str As String
    Dim salah As er_custom
    Dim waktu_query As New Stopwatch
    Dim waktu_page As New Stopwatch
    Dim dt As New DataTable
    Dim cb As GridViewDataComboBoxColumn

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.isi_data()
    End Sub

    Private Sub mds_lap_supir_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        Me.waktu_page.Stop()
        Mod_Utama.master_waktu(Me.waktu_query, Me.waktu_page, uc_footer)
    End Sub

    Private Sub mds_lap_supir_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Me.waktu_page.Start()
    End Sub

    Private Sub mds_lap_supir_Init(sender As Object, e As EventArgs) Handles Me.Init
        dr_user = Session("dr_user")
        Me.uc_header.pivot = Me.ASPxPivotGrid1  'penting
        Me.uc_chart.chart = Me.WebChartControl1

        str = "<li><a href='home.aspx'>HOME</a></li>"
        str = str & "<li class='active'>LAPORAN</li>"
        str = str & "<li><a href='mds_lap_supir.aspx'>Laporan Data Driver</a></li>"
        Me.uc_header.list_menu.InnerHtml = str
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
        str = "select A.*, "
        str = str & "(select nama from mst_jenis_sim where id_jns_sim = A.id_jns_sim) as sim, "
        str = str & "(select nama from mst_lokasi where id_lokasi = A.id_lokasi) as lokasi, "
        str = str & "(select '') as masakerja, "
        str = str & "(select '') as umur, "
        str = str & "(select '') as aktif, "
        str = str & "(select '') as siap, "
        str = str & "(select '') as jabatan, "
        str = str & "(select 1) as qty "
        str = str & "from tms_mst_driver A where aktif_sta = 1 and id_driver <> 0 "
        Me.salah = Mod_Utama.isi_data(Me.dt, str, "", waktu_query)
        If salah.er_hasil <> "" Then
            Me.Jika_Error(str, salah.er_hasil, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
            Exit Sub
        End If

        Dim bln As Integer
        Dim thn As Integer
        Dim bln_lhr As Integer
        Dim thn_lhr As Integer
        For Each dtr As DataRow In Me.dt.Rows
            If IsDBNull(dtr("tgl_masuk")) = False Then
                bln = DateDiff(DateInterval.Month, dtr("tgl_masuk"), Now)
                thn = Math.Floor(bln / 12)

                dtr("masakerja") = thn.ToString & " | " & bln Mod 12
            End If

            If IsDBNull(dtr("tgl_lahir")) = False Then
                bln_lhr = DateDiff(DateInterval.Month, dtr("tgl_lahir"), Now)
                thn_lhr = Math.Floor(bln_lhr / 12)

                dtr("umur") = thn_lhr.ToString
            End If

            If dtr("aktif_sta") = 0 Then
                dtr("aktif") = "NON AKTIF"
            Else
                dtr("aktif") = "AKTIF"
            End If
        Next
        Me.dt.AcceptChanges()

        Me.ASPxPivotGrid1.DataSource = Me.dt
        Me.ASPxPivotGrid1.Caption = "DATA SUPIR"
        Me.ASPxPivotGrid1.DataBind()
        Mod_Utama.Atur_pivot(Me.ASPxPivotGrid1)
    End Sub
End Class