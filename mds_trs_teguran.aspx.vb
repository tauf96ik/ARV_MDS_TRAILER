Imports DevExpress.Web
Imports System.Reflection.MethodBase
Imports DevExpress.Web.Data

Public Class mds_trs_teguran
    Inherits System.Web.UI.Page

    Dim dr_user As DataRow
    Dim str As String
    Dim salah As er_custom
    Dim waktu_query As New Stopwatch
    Dim waktu_page As New Stopwatch
    Dim dt As New DataTable
    Dim cb As GridViewDataComboBoxColumn
    Dim str_menu As String = ",23,"
    Dim str_app As String = ",1,"
    Dim dr As DataRow

    Dim dt_jns As New DataTable
    Dim dt_supir, dt_ba As New DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.isi_data()
    End Sub

    Protected Sub bt_refresh_ServerClick(sender As Object, e As EventArgs)

    End Sub

    Private Sub mds_trs_teguran_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Me.waktu_page.Start()
    End Sub

    Private Sub mds_trs_teguran_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        Me.waktu_page.Stop()
        Mod_Utama.master_waktu(Me.waktu_query, Me.waktu_page, uc_footer)
    End Sub

    Private Sub mds_trs_teguran_Init(sender As Object, e As EventArgs) Handles Me.Init
        dr_user = Session("dr_user")
        Me.uc_header.grid = Me.ASPxGridView1 'penting
        Me.uc_header.a_filter.Visible = False

        str = "<li><a href='home.aspx'>HOME</a></li>"
        str = str & "<li class='active'>EXISTING</li>"
        str = str & "<li><a href='mds_trs_teguran.aspx'>Teguran & Peringatan</a></li>"

        If CStr(dr_user("lihat")).Contains(str_menu) = False Then
            Response.Redirect("~/page_no_auth.aspx")
        End If
        Me.uc_header.list_menu.InnerHtml = str

        If IsPostBack = False Then
            Me.s_date.Date = Format(Now.AddYears(-1), "yyyy-MM-dd")
            Me.e_date.Date = Format(Now, "yyyy-MM-dd")
        End If

        Me.Isi_Filter()

        Me.isi_jns()
        Me.isi_supir()
        Me.isi_ba()
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

    Private Sub isi_jns()
        str = "select * from mds_mst_teguran_jns "
        Me.salah = Mod_Utama.isi_data(Me.dt_jns, str, "id_teguran_jns", waktu_query)
        If salah.er_hasil <> "" Then Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)

        cb = Me.ASPxGridView1.Columns("id_teguran_jns")
        cb.PropertiesComboBox.DataSource = Me.dt_jns
        cb.PropertiesComboBox.ValueField = "id_teguran_jns"
        cb.PropertiesComboBox.TextField = "nama"
        cb.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains
    End Sub

    Private Sub isi_supir()
        str = "select * from tms_mst_driver where aktif_sta = 1 order by nama "
        Me.salah = Mod_Utama.isi_data(Me.dt_supir, str, "id_driver", waktu_query)
        If salah.er_hasil <> "" Then Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)

        cb = Me.ASPxGridView1.Columns("id_spr")
        cb.PropertiesComboBox.DataSource = Me.dt_supir
        cb.PropertiesComboBox.ValueField = "id_driver"
        cb.PropertiesComboBox.TextField = "nama"
        cb.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains
    End Sub

    Private Sub isi_ba()
        str = "select *,"
        str = str & "(select nama from tms_mst_driver where id_driver = mds_trs_ba.id_driver) as spr, "
        str = str & "(select nama from mds_mst_ba where id_ba = mds_trs_ba.id_ba) as jns_ba "
        str = str & " from mds_trs_ba "

        salah = Mod_Utama.isi_data(Me.dt_ba, str, "id_trs_ba", waktu_query)

        cb = Me.ASPxGridView1.Columns("id_trs_ba")
        cb.PropertiesComboBox.DataSource = Me.dt_ba
        cb.PropertiesComboBox.TextField = "no_ba"
        cb.PropertiesComboBox.ValueField = "id_trs_ba"
        cb.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains
    End Sub

    Private Sub isi_data()
        str = "select *, "
        str = str & "(select kategori from tms_mst_driver where id_driver = mds_trs_teguran.id_spr) as nm_jbtn, "
        str = str & "(select '') as status "
        str = str & "from mds_trs_teguran where tgl >= '" & Format(Me.s_date.Date, "yyyy-MM-dd") & "' and tgl <= '" & Format(Me.e_date.Date, "yyyy-MM-dd") & "' "
        str = str & "order by id_teguran desc "
        Me.salah = Mod_Utama.isi_data(Me.dt, str, "id_teguran", waktu_query)
        If salah.er_hasil <> "" Then
            Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
            Exit Sub
        End If

        Me.dt.AcceptChanges()
        Me.dt.DefaultView.RowFilter = Session("rowfilter")
        Me.ASPxGridView1.DataSource = Me.dt
        Me.ASPxGridView1.KeyFieldName = "id_teguran"
        Me.ASPxGridView1.DataBind()
        Mod_Utama.Atur_Grid(Me.ASPxGridView1)
        Me.ASPxGridView1.Settings.ShowPreview = True
        Me.ASPxGridView1.Settings.HorizontalScrollBarMode = ScrollBarMode.Auto
    End Sub

    Private Sub ASPxGridView1_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles ASPxGridView1.CellEditorInitialize
        Select Case e.Column.FieldName
            Case "id_spr"
                Dim cb As ASPxComboBox = e.Editor
                cb.DataSource = Me.dt_supir
                cb.ValueField = "id_driver"
                cb.TextField = "nama"
                cb.Columns.Add("nama", "Nama", 200)
                cb.Columns.Add("kategori", "Jabatan", 200)
                cb.IncrementalFilteringMode = IncrementalFilteringMode.Contains
                cb.DataBind()

            Case "id_teguran_jns"
                Dim cb As ASPxComboBox = e.Editor
                cb.DataSource = Me.dt_jns
                cb.ValueField = "id_teguran_jns"
                cb.TextField = "nama"
                cb.IncrementalFilteringMode = IncrementalFilteringMode.Contains
                cb.DataBind()

            Case "id_trs_ba"
                Dim cb As ASPxComboBox = e.Editor
                cb.DataSource = Me.dt_ba
                cb.ValueField = "id_trs_ba"
                cb.TextField = "no_ba"
                cb.Columns.Add("id_trs_ba", "ID", 60)
                cb.Columns.Add("no_ba", "No", 150)
                cb.Columns.Add("spr", "Driver", 150)
                cb.Columns.Add("jns_ba", "Jenis", 150)
                cb.DataBind()
                cb.IncrementalFilteringMode = IncrementalFilteringMode.Contains
        End Select
    End Sub

    Private Sub ASPxGridView1_CommandButtonInitialize(sender As Object, e As ASPxGridViewCommandButtonEventArgs) Handles ASPxGridView1.CommandButtonInitialize
        Select Case e.ButtonType
            Case ColumnCommandButtonType.[New]
                If CStr(dr_user("baru")).Contains(str_menu) = False Then e.Visible = False
            Case ColumnCommandButtonType.Edit
                If CStr(dr_user("ubah")).Contains(str_menu) = False Then e.Visible = False
            Case ColumnCommandButtonType.Delete
                If CStr(dr_user("hapus")).Contains(str_menu) = False Then e.Visible = False
        End Select
    End Sub

    Private Sub ASPxGridView1_CustomErrorText(sender As Object, e As ASPxGridViewCustomErrorTextEventArgs) Handles ASPxGridView1.CustomErrorText
        e.ErrorText = salah.er_hasil
    End Sub

    Private Sub ASPxGridView1_InitNewRow(sender As Object, e As ASPxDataInitNewRowEventArgs) Handles ASPxGridView1.InitNewRow
        e.NewValues("tgl") = Now.Date
    End Sub

    Private Sub ASPxGridView1_RowDeleting(sender As Object, e As ASPxDataDeletingEventArgs) Handles ASPxGridView1.RowDeleting
        Mod_Utama.log_delete("select * From mds_trs_teguran where id_teguran = " & e.Keys("id_teguran"), "mds_trs_teguran", dr_user)
        str = "delete mds_trs_teguran "
        str = str & "where id_teguran = " & e.Keys("id_teguran")

        salah.er_hasil = Mod_Utama.exec_sql(str, dr_user, "Delete Teguran Supir")
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

    Private Sub ASPxGridView1_RowUpdating(sender As Object, e As ASPxDataUpdatingEventArgs) Handles ASPxGridView1.RowUpdating
        str = "update mds_trs_teguran set "
        str = str & "tgl = '" & e.NewValues("tgl") & "', "
        str = str & "id_spr = '" & e.NewValues("id_spr") & "', "
        str = str & "id_teguran_jns = '" & e.NewValues("id_teguran_jns") & "', "
        str = str & "alasan = '" & e.NewValues("alasan") & "', "
        str = str & "tgl_berlaku = '" & e.NewValues("tgl_berlaku") & "', "
        str = str & "tgl_selesai = '" & e.NewValues("tgl_selesai") & "', "
        str = str & "catatan = '" & e.NewValues("catatan") & "', "
        str = str & "id_trs_ba = '" & e.NewValues("id_trs_ba") & "', "
        str = str & "u_date = getdate(), "
        str = str & "u_user = '" & dr_user("nama") & "' "
        str = str & "where id_teguran = " & e.Keys("id_teguran")

        salah.er_hasil = Mod_Utama.exec_sql(str, dr_user, "Driver Ijin")
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

    Private Sub ASPxGridView1_RowInserting(sender As Object, e As ASPxDataInsertingEventArgs) Handles ASPxGridView1.RowInserting
        str = "insert into mds_trs_teguran ("
        str = str & "id_teguran, tgl, id_spr, id_teguran_jns, alasan, tgl_berlaku, tgl_selesai, catatan, id_trs_ba, "
        str = str & "c_date, c_user, u_date, u_user ) "
        str = str & "VALUES ( "
        str = str & "(select isnull(max(id_teguran), 0) + 1 from mds_trs_teguran),  "
        str = str & "'" & e.NewValues("tgl") & "', "
        str = str & "'" & e.NewValues("id_spr") & "', "
        str = str & "'" & e.NewValues("id_teguran_jns") & "', "
        str = str & "'" & e.NewValues("alasan") & "', "
        str = str & "'" & e.NewValues("tgl_berlaku") & "', "
        str = str & "'" & e.NewValues("tgl_selesai") & "', "
        str = str & "'" & e.NewValues("catatan") & "', "
        str = str & "'" & e.NewValues("id_trs_ba") & "', "
        str = str & "getdate(), "
        str = str & "'" & dr_user("nama") & "', "
        str = str & "getdate(), "
        str = str & "'" & dr_user("nama") & "') "

        salah.er_hasil = Mod_Utama.exec_sql(str, dr_user, "Insert Teguran Supir")
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
End Class