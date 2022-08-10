Imports DevExpress.Web
Imports System.Reflection.MethodBase

Public Class mst_user
    Inherits System.Web.UI.Page

    Dim dr_user As DataRow
    Dim str As String
    Dim salah As er_custom
    Dim waktu_query As New Stopwatch
    Dim waktu_page As New Stopwatch
    Dim dt As New DataTable
    Dim cb As GridViewDataComboBoxColumn
    Dim str_menu As String = ",2,"
    Dim dr As DataRow

    Dim dt_user As New DataTable
    Dim dt_group As New DataTable

    Private Sub page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Me.waktu_page.Start()
    End Sub
    Private Sub page_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        Me.waktu_page.Stop()
        Mod_Utama.master_waktu(Me.waktu_query, Me.waktu_page, uc_footer)
    End Sub
    Private Sub page_Init(sender As Object, e As EventArgs) Handles Me.Init
        dr_user = Session("dr_user")
        Me.uc_header.grid = Me.ASPxGridView1

        str = "<li><a href='home.aspx'>HOME</a></li>"
        str = str & "<li class='active'>AUTHORIZE</li>"
        str = str & "<li><a href='mst_user.aspx'>Authorize User</a></li>" '

        Me.uc_header.list_menu.InnerHtml = str
        If CStr(dr_user("lihat")).Contains(str_menu) = False Then
            Response.Redirect("~/page_no_auth.aspx")
        End If
        Me.isi_group()
        Me.isi_user()

        Me.isi_data()
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Private Sub isi_user()
        str = "select * from mst_user"
        salah = Mod_Utama.isi_data(Me.dt_user, str, "id_user", Me.waktu_query)
        If salah.er_hasil <> "" Then Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)

        cb = Me.ASPxGridView1.Columns("id_user")
        cb.PropertiesComboBox.DataSource = Me.dt_user
        cb.PropertiesComboBox.ValueField = "id_user"
        cb.PropertiesComboBox.TextField = "nama"
        cb.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains
    End Sub
    Private Sub isi_group()
        str = "select * from mds_user_grp"
        salah = Mod_Utama.isi_data(Me.dt_group, str, "id_user_grp", Me.waktu_query)
        If salah.er_hasil <> "" Then Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)

        cb = Me.ASPxGridView1.Columns("id_user_grp")
        cb.PropertiesComboBox.DataSource = Me.dt_group
        cb.PropertiesComboBox.ValueField = "id_user_grp"
        cb.PropertiesComboBox.TextField = "nama"
        cb.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains
    End Sub
    Private Sub isi_data()
        str = "select * "
        str = str & "from mds_user"
        salah = Mod_Utama.isi_data(Me.dt, str, "id_user", Me.waktu_query)
        If salah.er_hasil <> "" Then Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)

        Me.ASPxGridView1.DataSource = Me.dt
        Me.ASPxGridView1.KeyFieldName = "id_user"
        Me.ASPxGridView1.DataBind()
        Mod_Utama.Atur_Grid(Me.ASPxGridView1)

    End Sub

    Private Sub ASPxGridView1_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles ASPxGridView1.CellEditorInitialize
        Select Case e.Column.FieldName
            Case "id_user"
                Dim cmb As ASPxComboBox = TryCast(e.Editor, ASPxComboBox)
                cmb.Items.Clear()
                cmb.DataSource = dt_user
                cmb.ValueField = "id_user"
                cmb.TextField = "nama"
                cmb.DataBind()

            Case "id_user_grp"
                Dim cmb As ASPxComboBox = TryCast(e.Editor, ASPxComboBox)
                cmb.Items.Clear()
                cmb.DataSource = dt_group
                cmb.ValueField = "id_user_grp"
                cmb.TextField = "nama"
                cmb.DataBind()

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
    Private Sub ASPxGridView1_RowDeleting(sender As Object, e As DevExpress.Web.Data.ASPxDataDeletingEventArgs) Handles ASPxGridView1.RowDeleting
        str = "DELETE mds_user "
        str = str & "where id_user = " & e.Keys("id_user")

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
    Private Sub ASPxGridView1_RowInserting(sender As Object, e As DevExpress.Web.Data.ASPxDataInsertingEventArgs) Handles ASPxGridView1.RowInserting
        str = "INSERT INTO mds_user ("
        str = str & "id_user, id_user_grp, "
        str = str & "c_date, c_user, u_date, u_user) VALUES ("
        str = str & "'" & e.NewValues("id_user") & "', "
        str = str & "'" & e.NewValues("id_user_grp") & "', "
        str = str & "getdate(), "
        str = str & "'" & dr_user("nama") & "', "
        str = str & "getdate(), "
        str = str & "'" & dr_user("nama") & "') "

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
    Private Sub ASPxGridView1_RowUpdating(sender As Object, e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs) Handles ASPxGridView1.RowUpdating
        str = "UPDATE mds_user set "
        str = str & "id_user = '" & e.NewValues("id_user") & "', "
        str = str & "id_user_grp = '" & e.NewValues("id_user_grp") & "', "
        str = str & "u_date = getdate(), "
        str = str & "u_user = '" & dr_user("nama") & "' "
        str = str & "where id_user = " & e.Keys("id_user")

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
End Class