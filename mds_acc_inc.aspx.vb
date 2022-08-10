Imports DevExpress.Web
Imports System.Reflection.MethodBase
Imports DevExpress.Web.Data

Public Class mds_acc_inc
    Inherits System.Web.UI.Page

    Dim dr_user As DataRow
    Dim str As String
    Dim salah As er_custom
    Dim waktu_query As New Stopwatch
    Dim waktu_page As New Stopwatch
    Dim dt, dt_ba, dt_cc, dt2, dt_supir, dt_supir2 As New DataTable
    Dim cb As GridViewDataComboBoxColumn
    Dim str_menu As String = ",20,"
    Dim dr As DataRow

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.isi_data()
    End Sub

    Protected Sub bt_refresh_ServerClick(sender As Object, e As EventArgs)

    End Sub

    Private Sub mds_acc_inc_Init(sender As Object, e As EventArgs) Handles Me.Init
        dr_user = Session("dr_user")
        Me.uc_header.grid = Me.ASPxGridView1 'penting

        str = "<li><a href='home.aspx'>HOME</a></li>"
        str = str & "<li class='active'>EXISTING</li>"
        str = str & "<li><a href='mds_acc_inc.aspx'>Accident/Incident</a></li>"
        Me.uc_header.list_menu.InnerHtml = str

        Dim startDate = New DateTime(Now.Year, 1, 1)
        If IsPostBack = False Then
            Me.de_start.Value = startDate
            Me.de_end.Value = Now.Date
        End If

        If CStr(dr_user("lihat")).Contains(str_menu) = False Then
            Response.Redirect("~/page_no_auth.aspx")
        End If

        Me.Isi_Filter()
        Me.isi_nopol()
        Me.isi_supir()
    End Sub

    Private Sub mds_acc_inc_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Me.waktu_page.Start()
    End Sub

    Private Sub mds_acc_inc_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
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

    Private Sub Isi_Filter()
        uc_header.filter_cb3.Items.Clear()
        uc_header.filter_cb3.Items.Add("UPDATED")
        'VALUE
        uc_header.filter_cb3.Items(0).Value = "u_date"
    End Sub

    Private Sub isi_nopol()
        str = "select * from tms_mst_nopol order by nopol"
        Me.salah = Mod_Utama.isi_data(Me.dt_cc, str, "id_nopol", waktu_query)
        If salah.er_hasil <> "" Then Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)

        cb = Me.ASPxGridView1.Columns("id_nopol")
        cb.PropertiesComboBox.DataSource = Me.dt_cc
        cb.PropertiesComboBox.ValueField = "id_nopol"
        cb.PropertiesComboBox.TextField = "nopol"
        cb.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains
    End Sub

    Private Sub isi_supir()
        str = "select * from tms_mst_driver where aktif_sta = 1 order by nama "
        Me.salah = Mod_Utama.isi_data(Me.dt_supir, str, "id_spr", waktu_query)
        If salah.er_hasil <> "" Then Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)

        cb = Me.ASPxGridView1.Columns("id_driver")
        cb.PropertiesComboBox.DataSource = Me.dt_supir
        cb.PropertiesComboBox.ValueField = "id_driver"
        cb.PropertiesComboBox.TextField = "nama"
        cb.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains
    End Sub

    Private Sub isi_data()
        If Me.de_start.Value > Me.de_end.Value Then
            Return
        End If

        str = "select *, "
        str = str & "(select nama from mst_lokasi where id_lokasi in (select id_lokasi from tms_mst_driver where id_driver = A.id_driver)) as asalpool,  "
        str = str & "(select COUNT(id_sumber) from mds_log_file where id_sumber = A.id_acc_inc and sumber = 'ACCINC') as ttl_upload "
        str = str & "from mds_trs_acc_inc A where "
        str = str & "FORMAT(tgl_kejadian,'yyyy-MM-dd') >= '" & Format(Me.de_start.Value, "yyyy-MM-dd") & "' and FORMAT(tgl_kejadian,'yyyy-MM-dd') <= '" & Format(Me.de_end.Value, "yyyy-MM-dd") & "' "
        str = str & "order by id_acc_inc desc "
        Me.salah = Mod_Utama.isi_data(dt, str, "id_acc_inc", waktu_query)
        If salah.er_hasil <> "" Then
            Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
            Exit Sub
        End If
        Me.dt.DefaultView.RowFilter = Session("rowfilter")

        Me.ASPxGridView1.DataSource = Me.dt
        Me.ASPxGridView1.KeyFieldName = "id_acc_inc"
        Me.ASPxGridView1.DataBind()
        Mod_Utama.Atur_Grid(Me.ASPxGridView1, True)

        Me.ASPxGridView1.Settings.ShowGroupPanel = False
    End Sub

    Private Sub ASPxGridView1_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles ASPxGridView1.CellEditorInitialize
        Select Case e.Column.FieldName
            Case "id_driver"
                Dim g As ASPxComboBox = e.Editor
                g.DataSource = Me.dt_supir
                g.ValueField = "id_driver"
                g.TextField = "nama"
                g.IncrementalFilteringMode = IncrementalFilteringMode.Contains
                g.DataBind()
            Case "id_nopol"
                Dim g As ASPxComboBox = e.Editor
                g.DataSource = Me.dt_cc
                g.ValueField = "id_nopol"
                g.TextField = "nopol"
                g.IncrementalFilteringMode = IncrementalFilteringMode.Contains
                g.DataBind()
        End Select
    End Sub

    Private Sub ASPxGridView1_CommandButtonInitialize(sender As Object, e As ASPxGridViewCommandButtonEventArgs) Handles ASPxGridView1.CommandButtonInitialize
        Select Case e.ButtonType
            Case ColumnCommandButtonType.New
                If CStr(dr_user("baru")).Contains(str_menu) = False Then e.Visible = False
            Case ColumnCommandButtonType.Edit
                If CStr(dr_user("ubah")).Contains(str_menu) = False Then e.Visible = False
            Case ColumnCommandButtonType.Delete
                If CStr(dr_user("hapus")).Contains(str_menu) = False Then e.Visible = False
        End Select
    End Sub

    Private Sub ASPxGridView1_RowDeleting(sender As Object, e As ASPxDataDeletingEventArgs) Handles ASPxGridView1.RowDeleting
        Mod_Utama.log_delete("select * From mds_trs_acc_inc where id_acc_inc = " & e.Keys("id_acc_inc"), "mds_trs_acc_inc", dr_user)
        str = "delete mds_trs_acc_inc where id_acc_inc = " & e.Keys("id_acc_inc")

        salah.er_hasil = Mod_Utama.exec_sql(str, dr_user, "Delete Incident")
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
        str = "select isnull(max(nourut),0) from mds_trs_acc_inc "
        str = str & "where Year(tgl_kejadian) = Year('" & e.NewValues("tgl_kejadian") & "') "
        salah = Mod_Utama.exec_sql_id(str)
        If salah.er_id < 0 Then
            salah.er_hasil = "Terjadi kesalahan pada penentuan No. Trs"
            Return
        End If

        salah.er_id += 1
        Dim notrs As String = ""
        If e.NewValues("id_jenis") = 1 Then
            notrs = "ACT\ACCDNT\" & Format(Now.Date, "yyMM") & Right("000000" & salah.er_id, 6)
        ElseIf e.NewValues("id_jenis") = 2 Then
            notrs = "ACT\INCDNT\" & Format(Now.Date, "yyMM") & Right("000000" & salah.er_id, 6)
        ElseIf e.NewValues("id_jenis") = 3 Then
            notrs = "ACT\NRMS\" & Format(Now.Date, "yyMM") & Right("000000" & salah.er_id, 6)
        End If

        str = "insert into mds_trs_acc_inc ( "
        str = str & "id_acc_inc, nourut, no_trs, id_driver, lokasi, id_nopol, tgl_kejadian, jam, cause_analis, "
        str = str & "id_status, id_jenis, id_cuaca, krono, rusak, biaya, id_faktor, "
        str = str & "c_user, c_date, u_user, u_date) values ("
        str = str & "(select isnull(max(id_acc_inc),0) + 1 from mds_trs_acc_inc), "
        str = str & "'" & salah.er_id & "', "
        str = str & "'" & notrs & "', "
        str = str & "'" & e.NewValues("id_driver") & "', "
        str = str & "'" & e.NewValues("lokasi") & "', "
        str = str & "'" & e.NewValues("id_nopol") & "', "
        str = str & "'" & e.NewValues("tgl_kejadian") & "', "
        str = str & "'" & Format(CDate(e.NewValues("jam")), "HH:mm") & "', "
        str = str & "'" & e.NewValues("cause_analis") & "', "
        str = str & "'" & e.NewValues("id_status") & "', "
        str = str & "'" & e.NewValues("id_jenis") & "', "
        str = str & "'" & e.NewValues("id_cuaca") & "', "
        str = str & "'" & e.NewValues("krono") & "', "
        str = str & "'" & e.NewValues("rusak") & "', "
        str = str & "'" & e.NewValues("biaya") & "', "
        str = str & "'" & e.NewValues("id_faktor") & "', "
        str = str & "'" & dr_user("nama") & "', "
        str = str & "getdate(), "
        str = str & "'" & dr_user("nama") & "', "
        str = str & "getdate())"

        salah.er_hasil = Mod_Utama.exec_sql(str, dr_user, "Accident Incident")
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
        Dim notrs As String = ""

        If e.NewValues("tgl_kejadian") <> e.OldValues("tgl_kejadian") Or e.NewValues("id_jenis") <> e.OldValues("id_jenis") Then
            str = "select isnull(max(nourut),0) from mds_trs_acc_inc "
            str = str & "where Year(tgl_kejadian) = Year('" & e.NewValues("tgl_kejadian") & "') "
            salah = Mod_Utama.exec_sql_id(str)
            If salah.er_id < 0 Then
                salah.er_hasil = "Terjadi kesalahan pada penentuan No. Trs"
                Return
            End If
            salah.er_id += 1
            If e.NewValues("id_jenis") = 1 Then
                notrs = "ACT\ACCDNT\" & Format(Now.Date, "yyMM") & Right("000000" & salah.er_id, 6)
            ElseIf e.NewValues("id_jenis") = 2 Then
                notrs = "ACT\INCDNT\" & Format(Now.Date, "yyMM") & Right("000000" & salah.er_id, 6)
            ElseIf e.NewValues("id_jenis") = 3 Then
                notrs = "ACT\NRMS\" & Format(Now.Date, "yyMM") & Right("000000" & salah.er_id, 6)
            End If
        End If
        str = "update mds_trs_acc_inc set "
        If notrs <> "" Then
            str = str & "no_trs = '" & notrs & "', "
            str = str & "nourut = '" & salah.er_id & "', "
        End If
        str = str & "id_driver = '" & e.NewValues("id_driver") & "', "
        str = str & "lokasi = '" & e.NewValues("lokasi") & "', "
        str = str & "id_nopol = '" & e.NewValues("id_nopol") & "', "
        str = str & "tgl_kejadian = '" & e.NewValues("tgl_kejadian") & "', "
        str = str & "jam = '" & Format(CDate(e.NewValues("jam")), "HH:mm") & "', "
        str = str & "id_status = '" & e.NewValues("id_status") & "', "
        str = str & "id_jenis = '" & e.NewValues("id_jenis") & "', "
        str = str & "id_cuaca = '" & e.NewValues("id_cuaca") & "', "
        str = str & "krono = '" & e.NewValues("krono") & "', "
        str = str & "cause_analis = '" & e.NewValues("cause_analis") & "', "
        str = str & "rusak = '" & e.NewValues("rusak") & "', "
        str = str & "biaya = '" & e.NewValues("biaya") & "', "
        str = str & "id_faktor = '" & e.NewValues("id_faktor") & "', "
        str = str & "u_user = '" & dr_user("nama") & "', "
        str = str & "u_date = getdate() "
        str = str & "where id_acc_inc = " & e.Keys("id_acc_inc")

        salah.er_hasil = Mod_Utama.exec_sql(str)
        If salah.er_hasil = "" Then
            Dim g As ASPxGridView = TryCast(sender, ASPxGridView)
            g.CancelEdit()
            Me.isi_data()
            e.Cancel = True
        Else
            salah.er_str = str
            salah.er_menu = Me.Page.ToString & "//" & GetCurrentMethod.Name
            salah.er_waktu = Mod_Utama.str_waktu(Me.waktu_query, Me.waktu_page)
            Session("error") = salah
        End If
    End Sub

End Class