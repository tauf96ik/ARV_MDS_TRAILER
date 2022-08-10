Imports DevExpress.Web
Imports System.Reflection.MethodBase
Imports DevExpress.Web.Data

Public Class mds_trs_izin
    Inherits System.Web.UI.Page

    Dim dr_user As DataRow
    Dim str As String
    Dim salah As er_custom
    Dim waktu_query As New Stopwatch
    Dim waktu_page As New Stopwatch
    Dim dt As New DataTable
    Dim cb As GridViewDataComboBoxColumn
    Dim str_menu As String = ",24,"
    Dim dr As DataRow

    Dim dt_supir As New DataTable
    Dim dt_jns As New DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub mds_trs_izin_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        Me.waktu_page.Stop()
        Mod_Utama.master_waktu(Me.waktu_query, Me.waktu_page, uc_footer)
    End Sub

    Private Sub mds_trs_izin_Init(sender As Object, e As EventArgs) Handles Me.Init
        dr_user = Session("dr_user")
        Me.uc_header.grid = Me.ASPxGridView1 'penting

        str = "<li><a href='home.aspx'>HOME</a></li>"
        str = str & "<li class='active'>EXISTING</li>"
        str = str & "<li><a href='mds_trs_izin.aspx'>Izin Driver</a></li>"

        Me.uc_header.list_menu.InnerHtml = str
        If CStr(dr_user("lihat")).Contains(str_menu) = False Then
            Response.Redirect("~/page_no_auth.aspx")
        End If
        Me.Isi_Filter()

        Me.isi_supir()
        Me.isi_jns()

        Me.isi_data()
    End Sub

    Private Sub mds_trs_izin_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Me.waktu_page.Start()
    End Sub

    Private Sub Isi_Filter()
        uc_header.filter_cb3.Items.Clear()
        uc_header.filter_cb3.Items.Add("UPDATED")
        'VALUE
        uc_header.filter_cb3.Items(0).Value = "u_date"
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

    Private Sub isi_jns()
        str = "select *, "
        str = str & "(select '') as status "
        str = str & "from mds_mst_izin_jns "
        str = str & "order by nama "
        Me.salah = Mod_Utama.isi_data(Me.dt_jns, str, "id_izin_jns", waktu_query)
        If salah.er_hasil <> "" Then
            Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
            Return
        End If

        cb = Me.ASPxGridView1.Columns("id_izin_jns")
        cb.PropertiesComboBox.DataSource = Me.dt_jns
        cb.PropertiesComboBox.ValueField = "id_izin_jns"
        cb.PropertiesComboBox.TextField = "nama"
        cb.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains
    End Sub

    Private Sub isi_supir()
        str = "select id_driver, nama, aktif_sta, "
        str = str & "(select '') as status "
        str = str & "from tms_mst_driver "
        str = str & "order by nama "
        Me.salah = Mod_Utama.isi_data(Me.dt_supir, str, "id_driver", waktu_query)
        If salah.er_hasil <> "" Then
            Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
            Return
        End If

        cb = Me.ASPxGridView1.Columns("id_driver")
        cb.PropertiesComboBox.DataSource = Me.dt_supir
        cb.PropertiesComboBox.ValueField = "id_driver"
        cb.PropertiesComboBox.TextField = "nama"
        cb.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains
    End Sub

    Private Sub isi_data()
        Dim fi_lim As uc_header.filter_limit = uc_header.filtertext
        str = "select *, "
        str = str & "(datediff(day, tgl_mulai, tgl_akhir) + 1) as hari "
        str = str & "from mds_trs_izin "
        str = str & "where "
        str = str & fi_lim.str_filter
        str = str & "id_izin is not null order by id_izin desc "
        Me.salah = Mod_Utama.isi_data(Me.dt, str, "id_izin", waktu_query)
        If salah.er_hasil <> "" Then
            Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
            Return
        End If

        Me.dt.AcceptChanges()

        Me.ASPxGridView1.DataSource = dt
        Me.ASPxGridView1.KeyFieldName = "id_izin"
        Me.ASPxGridView1.DataBind()
        Mod_Utama.Atur_Grid(Me.ASPxGridView1)
        Me.ASPxGridView1.Settings.ShowFooter = True
    End Sub

    Private Sub ASPxGridView1_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles ASPxGridView1.CellEditorInitialize
        Select Case e.Column.FieldName
            Case "id_driver"
                Me.dt_supir.DefaultView.RowFilter = "aktif_sta = 1"
                Dim cb As ASPxComboBox = e.Editor
                cb.DataSource = Me.dt_supir
                cb.ValueField = "id_driver"
                cb.TextField = "nama"
                cb.IncrementalFilteringMode = IncrementalFilteringMode.Contains
                cb.DataBind()

            Case "id_izin_jns"
                Dim cb As ASPxComboBox = e.Editor
                cb.DataSource = Me.dt_jns
                cb.ValueField = "id_izin_jns"
                cb.TextField = "nama"
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

    Private Sub ASPxGridView1_CustomButtonCallback(sender As Object, e As ASPxGridViewCustomButtonCallbackEventArgs) Handles ASPxGridView1.CustomButtonCallback
        dr = Me.ASPxGridView1.GetDataRow(e.VisibleIndex)
        If dr Is Nothing Then Return

        Select Case e.ButtonID
            Case "bt_kmbl"
                If dr("kmbl_sta") = False Then
                    str = "update mds_trs_izin set "
                    str = str & "kmbl_user = '" & dr_user("nama") & "', "
                    str = str & "kmbl_sta = 1, "
                    str = str & "kmbl_date = getdate() "
                    str = str & "where id_izin = " & dr("id_izin") & " "
                Else
                    str = "update mds_trs_izin set "
                    str = str & "kmbl_user = '" & dr_user("nama") & "', "
                    str = str & "kmbl_sta = 0, "
                    str = str & "kmbl_date = getdate() "
                    str = str & "where id_izin = " & dr("id_izin") & " "
                End If

                salah.er_hasil = Mod_Utama.exec_sql(str)
        End Select

        If salah.er_hasil <> "" Then Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
        Me.isi_data()
    End Sub

    Private Sub ASPxGridView1_CustomButtonInitialize(sender As Object, e As ASPxGridViewCustomButtonEventArgs) Handles ASPxGridView1.CustomButtonInitialize
        dr = Me.ASPxGridView1.GetDataRow(e.VisibleIndex)
        If dr Is Nothing Then Return

        e.Image.Height = 30
        e.Image.Width = 30
        Select Case e.ButtonID
            Case "bt_kmbl"
                If dr("kmbl_sta") = False Then
                    e.Image.Url = "~/img/no.png"
                Else
                    e.Image.Url = "~/img/yes.png"
                End If
        End Select
    End Sub

    Private Sub ASPxGridView1_CustomErrorText(sender As Object, e As ASPxGridViewCustomErrorTextEventArgs) Handles ASPxGridView1.CustomErrorText
        e.ErrorText = salah.er_hasil
    End Sub

    Private Sub ASPxGridView1_RowInserting(sender As Object, e As ASPxDataInsertingEventArgs) Handles ASPxGridView1.RowInserting
        str = "INSERT INTO mds_trs_izin ("
        str = str & "id_izin, id_izin_jns, id_driver, tgl_mulai, tgl_akhir, ket, "
        str = str & "c_date, c_user, u_date, u_user) VALUES ("
        str = str & "(select isnull(max(id_izin),0) + 1 from mds_trs_izin), "
        str = str & "'" & e.NewValues("id_izin_jns") & "', "
        str = str & "'" & e.NewValues("id_driver") & "', "
        str = str & "'" & Format(e.NewValues("tgl_mulai"), "yyyy-MM-dd") & "', "
        str = str & "'" & Format(e.NewValues("tgl_akhir"), "yyyy-MM-dd") & "', "
        str = str & "'" & e.NewValues("ket") & "', "
        str = str & "getdate(), "
        str = str & "'" & dr_user("nama") & "', "
        str = str & "getdate(), "
        str = str & "'" & dr_user("nama") & "') "

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

    Private Sub ASPxGridView1_RowUpdating(sender As Object, e As ASPxDataUpdatingEventArgs) Handles ASPxGridView1.RowUpdating
        str = "UPDATE mds_trs_izin SET "
        str = str & "id_izin_jns = '" & e.NewValues("id_izin_jns") & "', "
        str = str & "id_driver = '" & e.NewValues("id_driver") & "', "
        str = str & "tgl_mulai = '" & Format(e.NewValues("tgl_mulai"), "yyyy-MM-dd") & "', "
        str = str & "tgl_akhir = '" & Format(e.NewValues("tgl_akhir"), "yyyy-MM-dd") & "', "
        str = str & "ket = '" & e.NewValues("ket") & "', "
        str = str & "u_date = getdate(), "
        str = str & "u_user = '" & dr_user("nama") & "' "
        str = str & "where id_izin = " & e.Keys("id_izin") & " "

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

    Private Sub ASPxGridView1_RowDeleting(sender As Object, e As ASPxDataDeletingEventArgs) Handles ASPxGridView1.RowDeleting
        Mod_Utama.log_delete("select * From mds_trs_izin where id_izin = " & e.Keys("id_izin"), "mds_trs_izin", dr_user)
        str = "DELETE mds_trs_izin "
        str = str & "where id_izin = " & e.Keys("id_izin") & " "

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
End Class