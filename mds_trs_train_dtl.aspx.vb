Imports DevExpress.Web
Imports System.Reflection.MethodBase
Imports DevExpress.Web.Data

Public Class mds_trs_train_dtl
    Inherits System.Web.UI.Page

    Dim dr_user As DataRow
    Dim str As String
    Dim salah As er_custom
    Dim waktu_query As New Stopwatch
    Dim waktu_page As New Stopwatch
    Dim dt As New DataTable
    Dim cb As GridViewDataComboBoxColumn
    Dim str_menu As String = ",26,"
    Dim dr As DataRow

    Dim dt_train As New DataTable
    Dim dt_supir As New DataTable
    Dim id_train As Int64 = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub mds_trs_train_dtl_Init(sender As Object, e As EventArgs) Handles Me.Init
        Try
            Me.id_train = Request.QueryString("id")
        Catch ex As Exception
            Response.Redirect("mds_trs_training.aspx")
        End Try

        dr_user = Session("dr_user")
        Me.uc_header.grid = Me.ASPxGridView1 'penting
        Me.uc_header.a_filter.Visible = False
        Me.uc_header.div_search.Visible = False

        str = "<li><a href='home.aspx'>HOME</a></li>"
        str = str & "<li class='active'>EXISTING</li>"
        str = str & "<li><a href='mds_trs_training.aspx'>Driver Training</a></li>"
        str = str & "<li><a href='mds_trs_train_dtl.aspx?id=" & Me.id_train & "' style='color: #f00'>Training ID. " & Me.id_train & "</a></li>"
        If CStr(dr_user("lihat")).Contains(str_menu) = False Then
            Response.Redirect("~/page_no_auth.aspx")
        End If
        Me.uc_header.list_menu.InnerHtml = str
        Me.Isi_Filter()

        Me.isi_train()
        Me.isi_supir()
        Me.isi_data()
    End Sub

    Private Sub mds_trs_train_dtl_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Me.waktu_page.Start()
    End Sub

    Private Sub mds_trs_train_dtl_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
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

    Private Sub isi_train()
        str = "select *, "
        str = str & "(select nama from mds_mst_training where id_training = mds_trs_train.id_training) as training "
        str = str & "from mds_trs_train where id_train = " & Me.id_train
        Me.salah = Mod_Utama.isi_data(Me.dt_train, str, "id_train", waktu_query)
        If salah.er_hasil <> "" Then
            Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
        End If

        dr = Me.dt_train.Rows(0)
        Me.lb_id.InnerText = dr("id_train")
        Me.lb_tgl.InnerText = Format(dr("tgl"), "yyyy MMMM dd")
        Me.lb_trainer.InnerText = dr("trainer")
        Me.lb_training.InnerText = dr("training")
    End Sub

    Private Sub isi_supir()
        str = "select * from tms_mst_driver where aktif_sta = 1 order by nama "
        Me.salah = Mod_Utama.isi_data(Me.dt_supir, str, "id_spr", waktu_query)
        If salah.er_hasil <> "" Then
            Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
        End If

        cb = Me.ASPxGridView1.Columns("id_spr")
        cb.PropertiesComboBox.DataSource = Me.dt_supir
        cb.PropertiesComboBox.ValueField = "id_driver"
        cb.PropertiesComboBox.TextField = "nama"
        cb.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains
    End Sub

    Private Sub isi_data()
        str = "select *, "
        str = str & "(select nama from tms_mst_driver where id_driver = mds_trs_train_dtl.id_spr) as supir "
        str = str & "from mds_trs_train_dtl "
        str = str & "where id_train = " & Me.id_train & " "
        str = str & "order by id_train_dtl desc "
        Me.salah = Mod_Utama.isi_data(dt, str, "id_train_dtl", waktu_query)
        If salah.er_hasil <> "" Then
            Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
            Exit Sub
        End If

        Me.ASPxGridView1.DataSource = dt
        Me.ASPxGridView1.KeyFieldName = "id_train_dtl"
        Me.ASPxGridView1.DataBind()
        Mod_Utama.Atur_Grid(Me.ASPxGridView1)
        Me.ASPxGridView1.Settings.ShowPreview = True
    End Sub

    Private Sub ASPxGridView1_CustomErrorText(sender As Object, e As ASPxGridViewCustomErrorTextEventArgs) Handles ASPxGridView1.CustomErrorText
        e.ErrorText = salah.er_hasil
    End Sub

    Private Sub ASPxGridView1_InitNewRow(sender As Object, e As ASPxDataInitNewRowEventArgs) Handles ASPxGridView1.InitNewRow
        e.NewValues("nilai_awal") = 0
        e.NewValues("nilai_akhir") = 0
        e.NewValues("nilai") = 0
    End Sub

    Private Sub ASPxGridView1_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles ASPxGridView1.CellEditorInitialize
        Select Case e.Column.FieldName
            Case "id_spr"
                Dim cb As ASPxComboBox = e.Editor
                cb.DataSource = Me.dt_supir
                cb.ValueField = "id_driver"
                cb.TextField = "nama"
                cb.IncrementalFilteringMode = IncrementalFilteringMode.Contains
                cb.DataBind()
        End Select
    End Sub

    Private Sub ASPxGridView1_RowInserting(sender As Object, e As ASPxDataInsertingEventArgs) Handles ASPxGridView1.RowInserting
        str = "INSERT INTO mds_trs_train_dtl ("
        str = str & "id_train_dtl, id_train, id_spr, nilai_awal, nilai_akhir, nilai, ket, "
        str = str & "c_date, c_user, u_date, u_user) VALUES ("
        str = str & "(select isnull(max(id_train_dtl),0) + 1 from mds_trs_train_dtl), "
        str = str & "'" & Me.id_train & "', "
        str = str & "'" & e.NewValues("id_spr") & "', "
        str = str & "'" & e.NewValues("nilai_awal") & "', "
        str = str & "'" & e.NewValues("nilai_akhir") & "', "
        str = str & "'" & e.NewValues("nilai") & "', "
        str = str & "'" & e.NewValues("ket") & "', "
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
        str = "update mds_trs_train_dtl set "
        str = str & "id_spr = '" & e.NewValues("id_spr") & "', "
        str = str & "nilai_awal = '" & e.NewValues("nilai_awal") & "', "
        str = str & "nilai_akhir = '" & e.NewValues("nilai_akhir") & "', "
        str = str & "nilai = '" & e.NewValues("nilai") & "', "
        str = str & "ket = '" & e.NewValues("ket") & "', "
        str = str & "u_date = getdate(), "
        str = str & "u_user = '" & dr_user("nama") & "' "
        str = str & "where id_train_dtl = '" & e.Keys("id_train_dtl") & "' "

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
        Mod_Utama.log_delete("select * From mds_trs_train_dtl where id_train_dtl = " & e.Keys("id_train_dtl"), "mds_trs_train_dtl", dr_user)
        str = "delete mds_trs_train_dtl "
        str = str & "where id_train_dtl = " & e.Keys("id_train_dtl") & " "

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