Imports DevExpress.Web
Imports System.Reflection.MethodBase
Imports System.Web.HttpPostedFile
Imports System.Drawing
Imports DevExpress.Web.Data

Public Class mds_trs_mcu
    Inherits System.Web.UI.Page

    Dim dr_user As DataRow
    Dim str As String
    Dim salah As er_custom
    Dim waktu_query As New Stopwatch
    Dim waktu_page As New Stopwatch
    Dim dt As New DataTable
    Dim dt_2 As New DataTable
    Dim cb As GridViewDataComboBoxColumn
    Dim str_menu As String = ",25,"
    Dim dr As DataRow

    Dim dt_supir, dt_supir2, dt_stck, dt_limit As New DataTable
    Dim dt_lokasi As New DataTable
    Dim dt_cc As New DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.isi_data()
    End Sub

    Protected Sub bt_refresh_ServerClick(sender As Object, e As EventArgs)

    End Sub

    Private Sub mds_trs_mcu_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Me.waktu_page.Start()
    End Sub

    Private Sub mds_trs_mcu_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        Me.waktu_page.Stop()
        Mod_Utama.master_waktu(Me.waktu_query, Me.waktu_page, uc_footer)
    End Sub

    Private Sub mds_trs_mcu_Init(sender As Object, e As EventArgs) Handles Me.Init
        dr_user = Session("dr_user")
        Me.uc_header.grid = Me.ASPxGridView1

        str = "<li><a href='home.aspx'>HOME</a></li>"
        str = str & "<li class='active'>EXISTING</li>"
        str = str & "<li><a href='trs_mcu.aspx'>Medical Check Up</a></li>"

        Me.uc_header.list_menu.InnerHtml = str

        If IsPostBack = False Then
            Me.s_date.Date = Format(Now.AddMonths(-1), "yyyy-MM-dd")
            Me.e_date.Date = Format(Now, "yyyy-MM-dd")
        End If

        Me.uc_header.a_filter.Visible = False
        If CStr(dr_user("lihat")).Contains(str_menu) = False Then
            Response.Redirect("~/page_no_auth.aspx")
        End If

        Me.isi_supir()
        Me.isi_nopol()
        Me.isi_tujuan()
        Me.isi_limit()
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

    Private Sub isi_limit()
        str = "select * from mds_mst_limit"

        salah = Mod_Utama.isi_data(Me.dt_limit, str, "id_limit", waktu_query)
        If salah.er_hasil <> "" Then
            Me.Jika_Error(str, salah.er_hasil, Me.Page.ToString & "//" & GetCurrentMethod.Name, 1)
        End If
    End Sub

    Private Sub isi_supir()
        str = "select id_driver, nama, kategori "
        str = str & "from tms_mst_driver where aktif_sta = 1 order by nama "
        Me.salah = Mod_Utama.isi_data(Me.dt_supir, str, "id_driver", waktu_query)
        If salah.er_hasil <> "" Then Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)

        cb = Me.ASPxGridView1.Columns("id_spr")
        cb.PropertiesComboBox.DataSource = Me.dt_supir
        cb.PropertiesComboBox.ValueField = "id_driver"
        cb.PropertiesComboBox.TextField = "nama"
        cb.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains
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

    Private Sub isi_tujuan()
        str = "select *, "
        str = str & "(select nama from opr_mst_prop where id_prop = opr_mst_kab.id_prop ) as nm_prop "
        str = str & "from opr_mst_kab order by nama "
        Me.salah = Mod_Utama.isi_data(Me.dt_lokasi, str, "id_kab", waktu_query)
        If salah.er_hasil <> "" Then Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)

        cb = Me.ASPxGridView1.Columns("id_kab")
        cb.PropertiesComboBox.DataSource = Me.dt_lokasi
        cb.PropertiesComboBox.ValueField = "id_kab"
        cb.PropertiesComboBox.TextField = "nama"
        cb.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains
    End Sub

    Private Sub isi_data()
        str = "select *, "
        str = str & "(select '') nm_sta_suhu_bdn, "
        str = str & "(select '') nm_sta_tekanan_drh, "
        str = str & "(select nama from mst_lokasi where id_lokasi in (select id_lokasi from tms_mst_driver where id_driver = A.id_spr)) as asal_driver "
        str = str & "from mds_trs_mcu A where tgl >= '" & Format(Me.s_date.Date, "yyyy-MM-dd") & "' and tgl <= '" & Format(Me.e_date.Date, "yyyy-MM-dd") & "' "
        str = str & "order by id_mcu desc"

        salah = Mod_Utama.isi_data(Me.dt, str, "id_mcu", waktu_query)
        If salah.er_hasil <> "" Then
            Me.Jika_Error(str, salah.er_hasil, Me.Page.ToString & "//" & GetCurrentMethod.Name, 1)
        End If

        For Each dr_cek As DataRow In Me.dt.Rows

            If dr_cek("sta_suhu") = True Or dr_cek("sta_suhu") = 1 Then
                dr_cek("nm_sta_suhu_bdn") = "Good"
            Else
                dr_cek("nm_sta_suhu_bdn") = "Not Good"
            End If

            If dr_cek("sta_tekanan") = True Or dr_cek("sta_tekanan") = 1 Then
                dr_cek("nm_sta_tekanan_drh") = "Good"
            Else
                dr_cek("nm_sta_tekanan_drh") = "Not Good"
            End If
        Next
        Me.dt.AcceptChanges()

        Me.ASPxGridView1.DataSource = Me.dt
        Me.ASPxGridView1.KeyFieldName = "id_mcu"
        Me.ASPxGridView1.DataBind()
        Mod_Utama.Atur_Grid(Me.ASPxGridView1)
        'Me.ASPxPopupControl1.PopupAnimationType = AnimationType.Slide
        'Me.ASPxPopupControl1.PopupHorizontalAlign = PopupHorizontalAlign.Center
        'Me.ASPxPopupControl1.PopupVerticalAlign = PopupVerticalAlign.WindowCenter
    End Sub

    Private Sub ASPxGridView1_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles ASPxGridView1.CellEditorInitialize
        Select Case e.Column.FieldName
            Case "id_spr"
                Dim cb As ASPxComboBox = e.Editor
                cb.DataSource = Me.dt_supir
                cb.ValueField = "id_driver"
                cb.TextField = "nama"
                cb.Columns.Add("nama", "Nama", 250)
                cb.Columns.Add("kategori", "Jabatan", 200)
                cb.DataBind()
                cb.IncrementalFilteringMode = IncrementalFilteringMode.Contains
            Case "id_kab"
                Dim cb As ASPxComboBox = e.Editor
                cb.DataSource = Me.dt_lokasi
                cb.ValueField = "id_kab"
                cb.TextField = "nama"
                cb.Columns.Add("id_kab", "ID", 80)
                cb.Columns.Add("nama", "Kabupaten/Kota", 200)
                cb.Columns.Add("nm_prop", "Provinsi", 180)
                cb.IncrementalFilteringMode = IncrementalFilteringMode.Contains
                cb.DataBind()
            Case "id_nopol"
                Dim cb As ASPxComboBox = e.Editor
                cb.DataSource = Me.dt_cc
                cb.ValueField = "id_nopol"
                cb.TextField = "nopol"
                cb.DataBind()
                cb.IncrementalFilteringMode = IncrementalFilteringMode.Contains
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

    Private Sub ASPxGridView1_CustomErrorText(sender As Object, e As ASPxGridViewCustomErrorTextEventArgs) Handles ASPxGridView1.CustomErrorText
        e.ErrorText = salah.er_hasil
    End Sub

    Private Sub ASPxGridView1_InitNewRow(sender As Object, e As ASPxDataInitNewRowEventArgs) Handles ASPxGridView1.InitNewRow
        e.NewValues("tgl") = Now.Date
    End Sub

    Private Sub ASPxGridView1_RowDeleting(sender As Object, e As ASPxDataDeletingEventArgs) Handles ASPxGridView1.RowDeleting
        Mod_Utama.log_delete("select * From mds_trs_mcu where id_mcu = " & e.Keys("id_mcu"), "mds_trs_mcu", dr_user)
        str = "delete from mds_trs_mcu where id_mcu = " & e.Keys("id_mcu") & " "

        salah.er_hasil = Mod_Utama.exec_sql(str)
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

        Me.isi_data()
    End Sub

    Private Sub ASPxGridView1_RowInserting(sender As Object, e As ASPxDataInsertingEventArgs) Handles ASPxGridView1.RowInserting
        Dim s As String = e.NewValues("tekanan_drh")
        Dim arr_s = s.Split("/")

        str = "insert into mds_trs_mcu ( "
        str = str & "id_mcu, id_spr, tgl, id_nopol, id_kab, tekanan_drh, suhu_bdn, sta_tekanan, sta_suhu, ket, "
        str = str & "c_date, c_user, u_date, u_user) values ( "
        str = str & "(select isnull(max(id_mcu), 0) + 1 from mds_trs_mcu),"
        str = str & "'" & e.NewValues("id_spr") & "', "
        str = str & "'" & e.NewValues("tgl") & "', "
        str = str & "'" & e.NewValues("id_nopol") & "', "
        str = str & "'" & e.NewValues("id_kab") & "', "
        str = str & "'" & e.NewValues("tekanan_drh") & "', "
        str = str & "'" & e.NewValues("suhu_bdn") & "', "
        If CDbl(arr_s(0).Trim) >= "110" And CDbl(arr_s(0).Trim) <= "200" And CDbl(arr_s(1).Trim) >= "70" And CDbl(arr_s(1).Trim) <= "200" Then
            str = str & "1, "
        Else
            str = str & "0, "
        End If

        If CDbl(e.NewValues("suhu_bdn")) >= "35" And CDbl(e.NewValues("suhu_bdn")) <= "38" Then
            str = str & "1, "
        Else
            str = str & "0, "
        End If
        str = str & "'" & e.NewValues("ket") & "', "
        str = str & "getdate(), "
        str = str & "'" & dr_user("nama") & "', "
        str = str & "getdate(), "
        str = str & "'" & dr_user("nama") & "')"

        salah.er_hasil = Mod_Utama.exec_sql(str)
        If salah.er_hasil = "" Then
            Dim g As ASPxGridView = Me.ASPxGridView1
            g.CancelEdit()
            e.Cancel = True
            Me.isi_data()
        Else
            salah.er_str = str
            salah.er_menu = Me.Page.ToString & " // " & GetCurrentMethod.Name
            salah.er_waktu = Mod_Utama.str_waktu(Me.waktu_query, Me.waktu_page)
            Session("error") = salah
        End If
    End Sub

    Private Sub ASPxGridView1_RowUpdating(sender As Object, e As ASPxDataUpdatingEventArgs) Handles ASPxGridView1.RowUpdating
        Dim s As String = e.NewValues("tekanan_drh")
        Dim arr_s = s.Split("/")

        str = "update mds_trs_mcu set "
        str = str & "id_spr = " & e.NewValues("id_spr") & ", "
        str = str & "tgl = '" & e.NewValues("tgl") & "', "
        str = str & "id_nopol = " & e.NewValues("id_nopol") & ", "
        str = str & "id_kab = " & e.NewValues("id_kab") & ", "
        str = str & "tekanan_drh = '" & e.NewValues("tekanan_drh") & "', "
        str = str & "suhu_bdn = " & e.NewValues("suhu_bdn") & ", "
        If CDbl(arr_s(0).Trim) >= "110" And CDbl(arr_s(0).Trim) <= "200" And CDbl(arr_s(1).Trim) >= "70" And CDbl(arr_s(1).Trim) <= "200" Then
            str = str & "sta_tekanan = 1, "
        Else
            str = str & "sta_tekanan = 0, "
        End If

        If CDbl(e.NewValues("suhu_bdn")) >= "35" And CDbl(e.NewValues("suhu_bdn")) <= "38" Then
            str = str & "sta_suhu = 1, "
        Else
            str = str & "sta_suhu = 0, "
        End If
        str = str & "ket = '" & e.NewValues("ket") & "', "
        str = str & "u_date = getdate(), "
        str = str & "U_user = '" & dr_user("nama") & "' "
        str = str & "where id_mcu = " & e.Keys("id_mcu")

        salah.er_hasil = Mod_Utama.exec_sql(str)
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

    Private Sub ASPxGridView1_HtmlDataCellPrepared(sender As Object, e As ASPxGridViewTableDataCellEventArgs) Handles ASPxGridView1.HtmlDataCellPrepared
        dr = Me.ASPxGridView1.GetDataRow(e.VisibleIndex)
        Select Case e.DataColumn.FieldName
            Case "nm_sta_tekanan_drh"
                If dr("nm_sta_tekanan_drh") = "Good" Then
                    e.Cell.ForeColor = Color.White
                    e.Cell.BackColor = Color.Green
                Else
                    e.Cell.ForeColor = Color.White
                    e.Cell.BackColor = Color.Red
                End If
            Case "nm_sta_suhu_bdn"
                If dr("nm_sta_suhu_bdn") = "Good" Then
                    e.Cell.ForeColor = Color.White
                    e.Cell.BackColor = Color.Green
                Else
                    e.Cell.ForeColor = Color.White
                    e.Cell.BackColor = Color.Red
                End If
        End Select
    End Sub
End Class