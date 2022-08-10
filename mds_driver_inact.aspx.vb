Imports DevExpress.Web
Imports System.Reflection.MethodBase

Public Class mds_driver_inact
    Inherits System.Web.UI.Page

    Dim dr_user As DataRow
    Dim str As String
    Dim salah As er_custom
    Dim waktu_query As New Stopwatch
    Dim waktu_page As New Stopwatch
    Dim dt As New DataTable
    Dim cb As GridViewDataComboBoxColumn
    Dim str_menu As String = ",4,"
    Dim str_app As String = ",1,"
    Dim dr As DataRow

    Dim dt_pool As New DataTable
    Dim dt_sim As New DataTable
    Dim dt_pendidikan, dt_lokasi As New DataTable

    Private Sub page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Me.waktu_page.Start()
    End Sub
    Private Sub page_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        Me.waktu_page.Stop()
        Mod_Utama.master_waktu(Me.waktu_query, Me.waktu_page, uc_footer)
    End Sub
    Private Sub page_Init(sender As Object, e As EventArgs) Handles Me.Init
        dr_user = Session("dr_user")
        Me.uc_header.grid = Me.ASPxGridView1 'penting

        str = "<li><a href='home.aspx'>HOME</a></li>"
        str = str & "<li class='active'>EXISTING</li>"
        str = str & "<li><a href='mds_driver_inact.aspx'>Daftar Driver Non Active</a></li>"

        Me.uc_header.list_menu.InnerHtml = str
        If CStr(dr_user("lihat")).Contains(str_menu) = False Then
            Response.Redirect("~/page_no_auth.aspx")
        End If
        If IsPostBack = False Then
            Me.Isi_Filter()
        End If

        Me.isi_pendidikan()
        Me.isi_jns_sim()
        Me.isi_lokasi()
        Me.isi_data()
    End Sub
    Private Sub Isi_Filter()
        'uc_header.filter_cb1.Items.Clear()
        'uc_header.filter_cb1.Items.Add("NAME")
        'uc_header.filter_cb1.Items.Add("PROPINSI")
        'uc_header.filter_cb1.Items.Add("KABUPATEN")
        ''VALUE
        'uc_header.filter_cb1.Items(0).Value = "nama"
        'uc_header.filter_cb1.Items(1).Value = "id_prop"
        'uc_header.filter_cb1.Items(1).Value = "id_prop"

        'uc_header.filter_cb2.Items.Clear()
        'uc_header.filter_cb2.Items.Add("NAME")
        'uc_header.filter_cb2.Items.Add("FULL NAME")
        ''VALUE
        'uc_header.filter_cb2.Items(0).Value = "nama"
        'uc_header.filter_cb2.Items(1).Value = "fullname"

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

    Private Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub

    Private Sub isi_pendidikan()
        str = "select * from mst_pendidikan"
        Me.salah = Mod_Utama.isi_data(Me.dt_pendidikan, str, "id_pendidikan", waktu_query)

        If salah.er_hasil <> "" Then Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)

        cb = Me.ASPxGridView1.Columns("pendidikan")
        cb.PropertiesComboBox.DataSource = Me.dt_pendidikan
        cb.PropertiesComboBox.ValueField = "nama"
        cb.PropertiesComboBox.TextField = "nama"
        cb.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains
    End Sub

    Private Sub isi_jns_sim()
        str = "select * from mst_jenis_sim"
        Me.salah = Mod_Utama.isi_data(Me.dt_sim, str, "id_jns_sim", waktu_query)

        cb = Me.ASPxGridView1.Columns("id_jns_sim")
        cb.PropertiesComboBox.DataSource = Me.dt_sim
        cb.PropertiesComboBox.ValueField = "id_jns_sim"
        cb.PropertiesComboBox.TextField = "nama"
        cb.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains
    End Sub

    Private Sub isi_lokasi()
        str = "select * from mst_lokasi"
        Me.salah = Mod_Utama.isi_data(Me.dt_lokasi, str, "id_lokasi", waktu_query)

        cb = Me.ASPxGridView1.Columns("id_lokasi")
        cb.PropertiesComboBox.DataSource = Me.dt_lokasi
        cb.PropertiesComboBox.ValueField = "id_lokasi"
        cb.PropertiesComboBox.TextField = "nama"
        cb.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains
    End Sub

    Private Sub isi_data()
        Dim fi_lim As uc_header.filter_limit = uc_header.filtertext
        str = "select " & fi_lim.str_limit & " *, "
        str = str & "(select '') as masakerja, "
        str = str & "(select '') as umur, "
        str = str & "(select '') as masakerja_bln "
        str = str & "from tms_mst_driver where "
        str = str & "sta = 0 "
        str = str & fi_lim.str_filter
        str = str & "order by id_driver desc "

        Me.salah = Mod_Utama.isi_data(dt, str, "id_driver", waktu_query)

        If salah.er_hasil <> "" Then
            Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
            Exit Sub
        End If

        Dim bln As Integer
        Dim thn As Integer
        Dim bln_lhr As Integer
        Dim thn_lhr As Integer
        For Each dtr As DataRow In Me.dt.Rows
            If IsDBNull(dtr("tgl_masuk")) Then
                dtr("masakerja") = ""
                dtr("masakerja_bln") = ""
            Else
                bln = DateDiff(DateInterval.Month, dtr("tgl_masuk"), Now)
                thn = Math.Floor(bln / 12)
                dtr("masakerja") = thn.ToString & " | " & bln Mod 12
                dtr("masakerja_bln") = bln
            End If
            If IsDBNull(dtr("tgl_lahir")) Then
                dtr("umur") = ""
            Else
                bln_lhr = DateDiff(DateInterval.Month, dtr("tgl_lahir"), Now)
                thn_lhr = Math.Floor(bln_lhr / 12)
                dtr("umur") = thn_lhr.ToString & " | " & bln_lhr Mod 12
            End If
        Next
        Me.dt.AcceptChanges()

        Me.ASPxGridView1.DataSource = dt
        Me.ASPxGridView1.KeyFieldName = "id_driver"
        Me.ASPxGridView1.DataBind()
        Mod_Utama.Atur_Grid(Me.ASPxGridView1, True)
        Me.ASPxGridView1.Settings.ShowPreview = True
        Me.ASPxGridView1.SettingsEditing.Mode = GridViewEditingMode.PopupEditForm
        Me.ASPxGridView1.SettingsPopup.EditForm.VerticalAlign = PopupVerticalAlign.WindowCenter
        Me.ASPxGridView1.SettingsPopup.EditForm.HorizontalAlign = PopupHorizontalAlign.WindowCenter
        Me.ASPxGridView1.SettingsPopup.EditForm.Width = 1000
        Me.ASPxGridView1.SettingsPopup.EditForm.Height = 900
        Me.ASPxGridView1.SettingsPopup.EditForm.AllowResize = True

        Me.lb_query.Text = "Select With Filter : " & fi_lim.str_filter & " & Limit : " & fi_lim.str_limit

    End Sub

    Private Sub ASPxGridView1_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles ASPxGridView1.CellEditorInitialize
        Select Case e.Column.FieldName
            Case "id_lokasi"
                Dim cb As ASPxComboBox = e.Editor
                cb.DataSource = Me.dt_lokasi
                cb.ValueField = "id_lokasi"
                cb.TextField = "nama"
                cb.IncrementalFilteringMode = IncrementalFilteringMode.Contains
                cb.DataBind()

            Case "id_jns_sim"
                Dim cb As ASPxComboBox = e.Editor
                cb.DataSource = Me.dt_sim
                cb.ValueField = "id_jns_sim"
                cb.TextField = "nama"
                cb.IncrementalFilteringMode = IncrementalFilteringMode.Contains
                cb.DataBind()

            Case "pendidikan"
                Dim cb As ASPxComboBox = e.Editor
                cb.DataSource = Me.dt_pendidikan
                cb.ValueField = "nama"
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
            Case ColumnCommandButtonType.New
                If CStr(dr_user("baru")).Contains(str_menu) = False Then e.Visible = False
        End Select
    End Sub

    Private Sub ASPxGridView1_CustomButtonInitialize(sender As Object, e As ASPxGridViewCustomButtonEventArgs) Handles ASPxGridView1.CustomButtonInitialize
        dr = Me.ASPxGridView1.GetDataRow(e.VisibleIndex)
        If dr Is Nothing Then Return

        e.Image.Height = 30
        e.Image.Width = 30

        Select Case e.ButtonID
            Case "bt_aktif"
                If dr("sta") = 0 Then
                    e.Image.Url = "~/img/no.png"
                    If CStr(dr_user("oth").Contains(str_app)) = False Then e.Enabled = True
                Else
                    If CStr(dr_user("oth").Contains(str_app)) = False Then e.Enabled = False
                    e.Image.Url = "~/img/yes.png"
                End If
        End Select
    End Sub
    Private Sub ASPxGridView1_CustomErrorText(sender As Object, e As ASPxGridViewCustomErrorTextEventArgs) Handles ASPxGridView1.CustomErrorText
        e.ErrorText = salah.er_hasil
    End Sub
    Private Sub ASPxGridView1_HtmlDataCellPrepared(sender As Object, e As ASPxGridViewTableDataCellEventArgs) Handles ASPxGridView1.HtmlDataCellPrepared
        dr = Me.ASPxGridView1.GetDataRow(e.VisibleIndex)
        Select Case e.DataColumn.FieldName
            Case "exp_sim"
                If CDate(e.CellValue) < Now.AddMonths(1).Date Then
                    e.Cell.BackColor = Drawing.Color.LightPink
                End If
        End Select
    End Sub
    Private Sub ASPxGridView1_RowDeleting(sender As Object, e As DevExpress.Web.Data.ASPxDataDeletingEventArgs) Handles ASPxGridView1.RowDeleting
        str = "delete tms_mst_driver "
        str = str & "where id_driver = " & e.Keys("id_driver")

        salah.er_hasil = Mod_Utama.exec_sql(str, dr_user, "delete Driver nonactive")
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
    Private Sub ASPxGridView1_RowInserting(sender As Object, e As DevExpress.Web.Data.ASPxDataInsertingEventArgs) Handles ASPxGridView1.RowInserting

    End Sub
    Private Sub ASPxGridView1_RowUpdating(sender As Object, e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs) Handles ASPxGridView1.RowUpdating

    End Sub

    Protected Sub rc_performa_ItemClick(sender As Object, e As DevExpress.Web.RatingControlItemEventArgs)

    End Sub
    Protected Function gettgl(container) As String
        Dim values() As Object = CType(container.Grid.GetRowValues(container.VisibleIndex, New String() {"id_supir", "aktif_sta", "aktif_date", "aktif_user"}), Object())

        'If values(1) = False Then Return ""

        Try
            Return Format(values(2), "yyyy-MM-dd HH:mm:ss") & " <b>By</b> " & values(3)
        Catch ex As Exception
            Return ""
        End Try

    End Function

    Private Sub ASPxGridView1_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles ASPxGridView1.CustomCallback
        Dim grid As ASPxGridView = TryCast(sender, ASPxGridView)

        If e.Parameters.StartsWith("bt") Then
            Dim isi() As String
            Dim idkey As Integer
            Try
                isi = Split(e.Parameters, ";")
                idkey = Convert.ToInt32(isi(1))
                dr = Me.dt.Rows.Find(idkey)
            Catch ex As Exception
                grid.JSProperties("cpTitle") = "gagal"
                grid.JSProperties("cpContent") = "Gagal mendapat data yang akan diproses"
                Return
            End Try

            Select Case isi(0)
                Case "bt_aktif"
                    If dr("aktif_sta") = 0 Then
                        str = "update tms_mst_driver set "
                        str = str & "aktif_sta = 1, "
                        str = str & "aktif_user = '" & dr_user("nama") & "', "
                        str = str & "aktif_date = getdate() "
                        str = str & "where id_driver = " & dr("id_driver")
                        Mod_Utama.exec_sql(str, dr_user, "approve active driver")
                    Else
                        str = "update tms_mst_driver set "
                        str = str & "aktif_sta = 0, "
                        str = str & "aktif_user = '" & dr_user("nama") & "', "
                        str = str & "aktif_date = getdate() "
                        str = str & "where id_driver = " & dr("id_driver")
                        Mod_Utama.exec_sql(str, dr_user, "unapprove active driver")
                    End If
            End Select

            salah.er_hasil = Mod_Utama.exec_sql(str)
            If salah.er_hasil = "" Then

            Else
                salah.er_str = str
                salah.er_menu = "Proses Send Records pada " & Me.Page.ToString & " #" & GetCurrentMethod.Name
                salah.er_waktu = Mod_Utama.str_waktu(Me.waktu_query, Me.waktu_page)
                salah.er_tquery = Me.waktu_query
                salah.er_tpage = Me.waktu_page
                Session("error") = salah
            End If

            Me.isi_data()
        End If
    End Sub
End Class