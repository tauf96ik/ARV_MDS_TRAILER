Imports DevExpress.Web
Imports System.Reflection.MethodBase

Public Class mon_spk_driver
    Inherits System.Web.UI.Page

    Dim dr_user As DataRow
    Dim str As String
    Dim salah As er_custom
    Dim waktu_query As New Stopwatch
    Dim waktu_page As New Stopwatch
    Dim dt As New DataTable
    Dim cb As GridViewDataComboBoxColumn
    Dim str_menu As String = ",33,"
    Dim dr As DataRow

    Dim dt_supir As New DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.isi_data()
    End Sub

    Protected Sub bt_refresh_ServerClick(sender As Object, e As EventArgs)

    End Sub

    Private Sub mon_spk_driver_Init(sender As Object, e As EventArgs) Handles Me.Init
        dr_user = Session("dr_user")
        Me.uc_header.grid = Me.ASPxGridView1 'penting

        str = "<li><a href='home.aspx'>HOME</a></li>"
        str = str & "<li class='active'>MONITORING</li>"
        str = str & "<li><a href='mon_spk_driver.aspx'>Monitoring SPK Driver</a></li>"
        Me.uc_header.list_menu.InnerHtml = str
        Dim now As DateTime = DateTime.Now
        Dim startDate = New DateTime(now.Year, now.Month, 1)
        If IsPostBack = False Then
            Me.s_date.Value = startDate
            Me.e_date.Value = now.Date
        Else
            Me.s_date.Value = Request.Form(Me.s_date.UniqueID)
            Me.e_date.Value = Request.Form(Me.e_date.UniqueID)
        End If
    End Sub

    Private Sub mon_spk_driver_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        Me.waktu_page.Stop()
        Mod_Utama.master_waktu(Me.waktu_query, Me.waktu_page, uc_footer)
    End Sub

    Private Sub mon_spk_driver_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Me.waktu_page.Start()
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

    Private Sub isi_data()
        If Me.s_date.Value > Me.e_date.Value Then
            Return
        End If

        str = "select *, "
        'str = str & "(select nopol from tms_mst_nopol where id_nopol = A.id_nopol) as nopol, "
        str = str & "(select nama from tms_mst_trayek where id_trayek = A.id_trayek) as trayek, "
        str = str & "(select '0') as norut "
        str = str & "from tms_trs_suratjalan A where FORMAT(tgl, 'yyyy-MM-dd') >= '" & Format(Me.s_date.Value, "yyyy-MM-dd") & "' and FORMAT(tgl, 'yyyy-MM-dd') <= '" & Format(Me.e_date.Value, "yyyy-MM-dd") & "' order by no_sj asc "

        Me.salah = Mod_Utama.isi_data(dt, str, "id_spj", waktu_query)
        If salah.er_hasil <> "" Then
            Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
            Return
        End If
        Dim norut As Integer = 1
        For Each dtr As DataRow In Me.dt.Rows
            dtr("norut") = norut
            norut = norut + 1
        Next

        'For Each dtr As DataRow In dt.Rows
        '    dtr.BeginEdit()
        '    dtr("ttl_cost") = dtr("uj_bbm") + dtr("uj_driver") + dtr("uj_makan") + dtr("uj_other")
        '    dtr.EndEdit()
        'Next

        Me.dt.AcceptChanges()

        Me.dt.DefaultView.RowFilter = Session("rowfilter")

        Me.ASPxGridView1.DataSource = Me.dt
        Me.ASPxGridView1.KeyFieldName = "id_sj"
        Me.ASPxGridView1.DataBind()
        Mod_Utama.Atur_Grid(Me.ASPxGridView1)
        Me.ASPxGridView1.Settings.ShowGroupPanel = False
    End Sub

End Class