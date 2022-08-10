Imports DevExpress.Web
Imports System.Reflection.MethodBase
Imports DevExpress.Web.Data

Public Class mds_trs_training
    Inherits System.Web.UI.Page

    Dim dr_user As DataRow
    Dim str As String
    Dim salah As er_custom
    Dim waktu_query As New Stopwatch
    Dim waktu_page As New Stopwatch
    Dim dt, dt2 As New DataTable
    Dim cb As GridViewDataComboBoxColumn
    Dim str_menu As String = ",26,"
    Dim dr As DataRow

    Dim dt_training, dt_training2, dt_lokasi, dt_rtt As New DataTable
    Dim dt_supir As New DataTable
    Dim listbox_train As ASPxListBox

    Dim dr_focus As DataRow

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub mds_trs_training_Init(sender As Object, e As EventArgs) Handles Me.Init
        dr_user = Session("dr_user")
        Me.uc_header.grid = Me.ASPxGridView1 'penting
        Me.uc_header.a_filter.Visible = False

        str = "<li><a href='home.aspx'>HOME</a></li>"
        str = str & "<li class='active'>EXISTING</li>"
        str = str & "<li><a href='trs_training.aspx'>Driver Training</a></li>"

        If CStr(dr_user("lihat")).Contains(str_menu) = False Then
            Response.Redirect("~/page_no_auth.aspx")
        End If
        Me.uc_header.list_menu.InnerHtml = str

        Me.Isi_Filter()
        Me.isi_training()
        Me.isi_lokasi()
        Me.isi_rtt()
        Me.isi_data()
    End Sub

    Private Sub mds_trs_training_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Me.waktu_page.Start()
    End Sub

    Private Sub mds_trs_training_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
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

    Private Sub isi_training()
        str = "select * from mds_mst_training order by id_training"
        Me.salah = Mod_Utama.isi_data(Me.dt_training, str, "id_training", waktu_query)
        If salah.er_hasil <> "" Then Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)

        cb = Me.ASPxGridView1.Columns("id_training")
        cb.PropertiesComboBox.DataSource = Me.dt_training
        cb.PropertiesComboBox.ValueField = "id_training"
        cb.PropertiesComboBox.TextField = "nama"
        cb.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains
    End Sub

    Private Sub isi_lokasi()
        str = "select * from mst_lokasi order by nama"
        Me.salah = Mod_Utama.isi_data(Me.dt_lokasi, str, "id_lokasi", waktu_query)

        If salah.er_hasil <> "" Then Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)

        cb = Me.ASPxGridView1.Columns("lokasi")
        cb.PropertiesComboBox.DataSource = Me.dt_lokasi
        cb.PropertiesComboBox.ValueField = "nama"
        cb.PropertiesComboBox.TextField = "nama"
        cb.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains
    End Sub

    Private Sub isi_rtt()
        str = "select * from mds_rtt_train"
        Me.salah = Mod_Utama.isi_data(Me.dt_rtt, str, "id_rtt", waktu_query)
        If salah.er_hasil <> "" Then Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)

        cb = Me.ASPxGridView1.Columns("id_rtt")
        cb.PropertiesComboBox.DataSource = Me.dt_rtt
        cb.PropertiesComboBox.ValueField = "id_rtt"
        cb.PropertiesComboBox.TextField = "minggu"
        cb.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains
    End Sub

    Private Sub isi_data()
        str = "select *, "
        str = str & "(select '') as minggu_plan, "
        str = str & "(select '') as minggu_act, "
        str = str & "(select nama + ',' from tms_mst_driver where id_driver in (select id_spr from mds_trs_train_dtl where id_train = A.id_train ) for xml path('')) as list_peserta, "
        str = str & "(select COUNT(id_files) from mds_log_file where id_sumber = A.id_train and sumber = 'TRAINING') as ttl_upload, "
        str = str & "isnull((select count(id_spr) from mds_trs_train_dtl where id_train = A.id_train),0) as peserta "
        str = str & "from mds_trs_train A  "
        str = str & "order by id_train desc "
        salah = Mod_Utama.isi_data(Me.dt, str, "id_train", Me.waktu_query)
        If salah.er_hasil <> "" Then
            Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
            Exit Sub
        End If

        Dim bulan As String
        Dim nBulan As String
        For Each dtr As DataRow In Me.dt.Rows
            bulan = Format(dtr("tgl"), "MMMM")
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

            Select Case CDbl(CType(dtr("tgl"), Date).Day / 7)
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

        Me.ASPxGridView1.DataSource = Me.dt
        Me.ASPxGridView1.KeyFieldName = "id_train"
        Mod_Utama.Atur_Grid(Me.ASPxGridView1)
        Me.ASPxGridView1.DataBind()
        Me.ASPxGridView1.Settings.ShowPreview = True
    End Sub

    Private Sub ASPxGridView1_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles ASPxGridView1.CellEditorInitialize
        Select Case e.Column.FieldName
            Case "id_training"
                Dim cb As ASPxComboBox = e.Editor
                cb.DataSource = Me.dt_training
                cb.ValueField = "id_training"
                cb.TextField = "nama"
                cb.IncrementalFilteringMode = IncrementalFilteringMode.Contains
                cb.DataBind()
            Case "lokasi"
                Dim cb As ASPxComboBox = e.Editor
                cb.DataSource = Me.dt_lokasi
                cb.ValueField = "nama"
                cb.TextField = "nama"
                cb.IncrementalFilteringMode = IncrementalFilteringMode.Contains
                cb.DataBind()
            Case "id_rtt"
                Dim cb As ASPxComboBox = e.Editor
                cb.DataSource = Me.dt_rtt
                cb.ValueField = "id_rtt"
                cb.TextField = "tgl_rtt"
                cb.Columns.Add("id_rtt", "ID RTT Planing", 120)
                cb.Columns.Add("tgl_rtt", "Tanggal Planing", 150)
                cb.Columns.Add("minggu", "Minggu Planing", 150)
                cb.IncrementalFilteringMode = IncrementalFilteringMode.Contains
                cb.DataBind()
        End Select
    End Sub

    Private Sub ASPxGridView1_CommandButtonInitialize(sender As Object, e As ASPxGridViewCommandButtonEventArgs) Handles ASPxGridView1.CommandButtonInitialize
        Select Case e.ButtonType
            Case ColumnCommandButtonType.Edit
                If CStr(dr_user("ubah")).Contains(str_menu) = False Then e.Visible = False
            Case ColumnCommandButtonType.Delete
                If CStr(dr_user("hapus")).Contains(str_menu) = False Then e.Visible = False
        End Select
    End Sub

    Private Sub ASPxGridView1_CustomErrorText(sender As Object, e As ASPxGridViewCustomErrorTextEventArgs) Handles ASPxGridView1.CustomErrorText
        e.ErrorText = salah.er_hasil
    End Sub

    Private Sub ASPxGridView1_RowInserting(sender As Object, e As ASPxDataInsertingEventArgs) Handles ASPxGridView1.RowInserting
        If e.NewValues("tgl") > e.NewValues("tgl_exp_train") Then
            salah.er_hasil = "Tanggal training tidak boleh lebih besar dari Tanggal Expired Training"
            Return
        End If

        str = "INSERT INTO mds_trs_train ("
        str = str & "id_train, id_training, id_rtt, tgl, jamawal, jamakhir, trainer, materi, lokasi, tgl_exp_train, "
        str = str & "c_date, c_user, u_date, u_user) VALUES ("
        str = str & "(select isnull(max(id_train),0) + 1 from mds_trs_train), "
        str = str & "'" & e.NewValues("id_training") & "', "
        str = str & "'" & e.NewValues("id_rtt") & "', "
        str = str & "'" & e.NewValues("tgl") & "', "
        str = str & "'" & e.NewValues("jamawal") & "', "
        str = str & "'" & e.NewValues("jamakhir") & "', "
        str = str & "'" & e.NewValues("trainer") & "', "
        str = str & "'" & e.NewValues("materi") & "', "
        str = str & "'" & e.NewValues("lokasi") & "', "
        str = str & "'" & e.NewValues("tgl_exp_train") & "', "
        str = str & "getdate(), "
        str = str & "'" & dr_user("nama") & "', "
        str = str & "getdate(), "
        str = str & "'" & dr_user("nama") & "') "

        salah.er_hasil = Mod_Utama.exec_sql(str, dr_user, "Training/Trs-update")
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
        If e.NewValues("tgl") > e.NewValues("tgl_exp_train") Then
            salah.er_hasil = "Tanggal training tidak boleh lebih besar dari Tanggal Expired Training"
            Return
        End If

        str = "update mds_trs_train set "
        str = str & "id_training = '" & e.NewValues("id_training") & "', "
        str = str & "id_rtt = '" & e.NewValues("id_rtt") & "', "
        str = str & "tgl = '" & e.NewValues("tgl") & "', "
        str = str & "jamawal = '" & e.NewValues("jamawal") & "', "
        str = str & "jamakhir = '" & e.NewValues("jamakhir") & "', "
        str = str & "trainer = '" & e.NewValues("trainer") & "', "
        str = str & "materi = '" & e.NewValues("materi") & "', "
        str = str & "lokasi = '" & e.NewValues("lokasi") & "', "
        str = str & "tgl_exp_train = '" & e.NewValues("tgl_exp_train") & "', "
        str = str & "u_date = getdate(), "
        str = str & "u_user = '" & dr_user("nama") & "' "
        str = str & "where id_train = " & e.Keys("id_train")

        salah.er_hasil = Mod_Utama.exec_sql(str, dr_user, "Training/Trs-update")
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

    Private Sub ASPxGridView1_RowDeleting(sender As Object, e As ASPxDataDeletingEventArgs) Handles ASPxGridView1.RowDeleting
        Mod_Utama.log_delete("select * From mds_trs_train where id_train = " & e.Keys("id_train"), "mds_trs_train", dr_user)
        Mod_Utama.log_delete("select * From mds_trs_train_dtl where id_train = " & e.Keys("id_train"), "mds_trs_train_dtl", dr_user)
        Mod_Utama.log_delete("select * From mds_log_file where sumber = 'TRAINING' and id_sumber = " & e.Keys("id_train"), "mds_log_file", dr_user)
        str = "delete mds_trs_train "
        str = str & "where id_train = " & e.Keys("id_train")

        str = str & "delete mds_trs_train_dtl "
        str = str & "where id_train = " & e.Keys("id_train")

        str = str & "delete mds_log_file "
        str = str & "where sumber = 'TRAINING' and id_sumber = " & e.Keys("id_train")

        salah.er_hasil = Mod_Utama.exec_sql(str, dr_user, "Training/Trs-delete")
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