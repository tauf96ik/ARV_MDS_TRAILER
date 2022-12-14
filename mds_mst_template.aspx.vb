Imports DevExpress.Web
Imports System.Reflection.MethodBase
Imports DevExpress.Web.Data

Public Class mds_mst_template
    Inherits System.Web.UI.Page

    Dim dr_user As DataRow
    Dim str As String
    Dim salah As er_custom
    Dim waktu_query As New Stopwatch
    Dim waktu_page As New Stopwatch
    Dim dt As New DataTable
    Dim dt_kategori As New DataTable
    Dim cb As GridViewDataComboBoxColumn
    Dim str_menu As String = ",13,"
    Dim dr As DataRow

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub mds_mst_template_Init(sender As Object, e As EventArgs) Handles Me.Init
        dr_user = Session("dr_user")
        Me.uc_header.grid = Me.ASPxGridView1 'penting

        str = "<li><a href='home.aspx'>HOME</a></li>"
        str = str & "<li class='active'>MASTER</li>"
        str = str & "<li><a href='mst_template.aspx'>Master Template</a></li>"

        If CStr(dr_user("lihat")).Contains(str_menu) = False Then
            Response.Redirect("~/page_no_auth.aspx")
        End If
        Me.uc_header.list_menu.InnerHtml = str

        Me.isi_kategori()
        Me.isi_data()
    End Sub

    Private Sub mds_mst_template_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        Me.waktu_page.Stop()
        Mod_Utama.master_waktu(Me.waktu_query, Me.waktu_page, uc_footer)
    End Sub

    Private Sub mds_mst_template_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
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

    Private Sub isi_kategori()
        str = "select * "
        str = str & "from mds_mst_kategori "
        Me.salah = Mod_Utama.isi_data(Me.dt_kategori, str, "id_kategori", waktu_query)
        If salah.er_hasil <> "" Then
            Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
            Return
        End If

        cb = Me.ASPxGridView1.Columns("id_kategori")
        cb.PropertiesComboBox.DataSource = Me.dt_kategori
        cb.PropertiesComboBox.ValueField = "id_kategori"
        cb.PropertiesComboBox.TextField = "nama"
        cb.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains
    End Sub

    Private Sub isi_data()
        str = "select *, "
        str = str & "(select nama from mds_mst_kategori where id_kategori = A.id_kategori) as nm_kategori "
        str = str & "from mds_mst_template A "
        str = str & "order by id_template asc "
        Me.salah = Mod_Utama.isi_data(Me.dt, str, "id_template", waktu_query)
        If salah.er_hasil <> "" Then
            Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
            Return
        End If

        Me.ASPxGridView1.DataSource = dt
        Me.ASPxGridView1.KeyFieldName = "id_template"
        Me.ASPxGridView1.DataBind()
        Mod_Utama.Atur_Grid(Me.ASPxGridView1)
        Me.ASPxGridView1.Settings.ShowFooter = True
    End Sub

    Private Sub ASPxGridView1_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles ASPxGridView1.CellEditorInitialize
        dr = Me.ASPxGridView1.GetDataRow(e.VisibleIndex)
        Select Case e.Column.FieldName
            Case "id_kategori"
                Dim cb As ASPxComboBox = e.Editor
                cb.DataSource = Me.dt_kategori
                cb.TextField = "nama"
                cb.ValueField = "id_kategori"
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
        str = "insert into mds_mst_template (id_template, id_kategori, title, "
        str = str & "u_date, u_user, c_date, c_user) values ( "
        str = str & "(select isnull(max(id_template), 0) + 1 from mds_mst_template), "
        str = str & "'" & e.NewValues("id_kategori") & "', "
        str = str & "'" & String.Format(e.NewValues("title")).ToUpper & "', "
        str = str & "getdate(), "
        str = str & "'" & dr_user("nama") & "', "
        str = str & "getdate(), "
        str = str & "'" & dr_user("nama") & "')"
        salah.er_hasil = Mod_Utama.exec_sql(str, dr_user, "MST Template Insert")

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
        str = "UPDATE mds_mst_template SET "
        str = str & "title = '" & String.Format(e.NewValues("title")).ToUpper & "', "
        str = str & "id_kategori = '" & e.NewValues("id_kategori") & "', "
        str = str & "u_date = getdate(), "
        str = str & "u_user = '" & dr_user("nama") & "' "
        str = str & "where id_template = " & e.Keys("id_template") & " "

        salah.er_hasil = Mod_Utama.exec_sql(str, dr_user, "MST Template Update")
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
        Mod_Utama.log_delete("select * From mds_mst_template where id_template = " & e.Keys("id_template"), "mds_mst_template", dr_user)
        str = "delete mds_mst_template "
        str = str & "where id_template = " & e.Keys("id_template")

        salah.er_hasil = Mod_Utama.exec_sql(str, dr_user, "MST Template Delete")
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