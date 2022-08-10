Imports DevExpress.Web
Imports System.Reflection.MethodBase
Imports DevExpress.XtraCharts
Imports DevExpress.Web.ASPxPivotGrid

Public Class mds_lap_training
    Inherits System.Web.UI.Page

    Dim dr_user As DataRow
    Dim str As String
    Dim salah As er_custom
    Dim waktu_query As New Stopwatch
    Dim waktu_page As New Stopwatch
    Dim dt As New DataTable
    Dim dr As DataRow
    Dim cb As GridViewDataComboBoxColumn
    Dim str_menu As String = ",28,"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.isi_data()
    End Sub

    Protected Sub bt_refresh_ServerClick(sender As Object, e As EventArgs)

    End Sub

    Private Sub mds_lap_training_Init(sender As Object, e As EventArgs) Handles Me.Init
        dr_user = Session("dr_user")
        Me.uc_header.pivot = Me.ASPxPivotGrid1  'penting

        str = "<li><a href='home.aspx'>HOME</a></li>"
        str = str & "<li class='active'>LAPORAN</li>"
        str = str & "<li><a href='mds_lap_training.aspx'>Laporan Training</a></li>"
        Me.uc_header.list_menu.InnerHtml = str

        If IsPostBack = False Then
            Me.s_date.Date = Format(Now.AddMonths(-1), "yyyy-MM-dd")
            Me.e_date.Date = Format(Now, "yyyy-MM-dd")
        End If
    End Sub

    Private Sub mds_lap_training_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Me.waktu_page.Start()
    End Sub

    Private Sub mds_lap_training_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        Me.waktu_page.Stop()
        Mod_Utama.master_waktu(Me.waktu_query, Me.waktu_page, uc_footer)
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
        str = "SELECT *, "
        str = str & "(select '') as minggu_act, "
        str = str & "(select nama from tms_mst_driver where id_driver = A.id_spr) as supir, "
        str = str & "(select tgl from mds_trs_train where id_train = A.id_train) as tgl_aktual, "
        str = str & "(select tgl_exp_train from mds_trs_train where id_train = A.id_train) as tgl_exp, "
        str = str & "(select materi from mds_trs_train where id_train = A.id_train) as materi, "
        str = str & "(select minggu from mds_rtt_train where id_rtt in (select id_rtt from mds_trs_train where id_train = A.id_train)) as planning "
        str = str & "FROM mds_trs_train_dtl A where c_date >= '" & Format(Me.s_date.Value, "yyyy-MM-dd") & "' and c_date <= '" & Format(Me.e_date.Value, "yyyy-MM-dd") & "' "

        salah = Mod_Utama.isi_data(Me.dt, str, "id_train_supir", Me.waktu_query)
        If salah.er_hasil <> "" Then
            Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
            Exit Sub
        End If

        Dim bulan As String
        Dim nBulan As String
        For Each dtr As DataRow In Me.dt.Rows
            bulan = Format(dtr("tgl_aktual"), "MMMM")
            Select Case bulan
                Case "January"
                    nBulan = "Januari"
                Case "February"
                    nBulan = "Februari"
                Case "March"
                    nBulan = "Maret"
                Case "April"
                    nBulan = "April"
                Case "May"
                    nBulan = "Mei"
                Case "June"
                    nBulan = "Juni"
                Case "July"
                    nBulan = "Juli"
                Case "August"
                    nBulan = "Agustus"
                Case "September"
                    nBulan = "September"
                Case "October"
                    nBulan = "Oktober"
                Case "November"
                    nBulan = "November"
                Case "December"
                    nBulan = "Desember"
            End Select

            Select Case CDbl(CType(dtr("tgl_aktual"), Date).Day / 7)
                Case Is <= 1
                    dtr("minggu_act") = "Minggu Ke 1 " & nBulan
                Case Is <= 2
                    dtr("minggu_act") = "Minggu Ke 2 " & nBulan
                Case Is <= 3
                    dtr("minggu_act") = "Minggu Ke 3 " & nBulan
                Case Is <= 4
                    dtr("minggu_act") = "Minggu Ke 4 " & nBulan
                Case Is <= 5
                    dtr("minggu_act") = "Minggu Ke 5 " & nBulan
            End Select

        Next

        Me.dt.AcceptChanges()

        Me.dt.DefaultView.RowFilter = Session("rowfilter")
        Me.ASPxPivotGrid1.DataSource = Me.dt
        Me.ASPxPivotGrid1.DataBind()
        Mod_Utama.Atur_pivot(Me.ASPxPivotGrid1)
        Me.ASPxPivotGrid1.OptionsView.ShowColumnTotals = False
        Me.ASPxPivotGrid1.OptionsView.ShowGrandTotalsForSingleValues = False
        Me.ASPxPivotGrid1.OptionsView.ShowColumnGrandTotals = False
        Me.ASPxPivotGrid1.OptionsView.ShowTotalsForSingleValues = False
        Me.ASPxPivotGrid1.OptionsView.ShowRowGrandTotals = False
    End Sub
End Class