Imports DevExpress.Web
Imports System.Reflection.MethodBase
Imports System.Web.HttpPostedFile
Imports System.Drawing
Imports System.IO
Imports DevExpress.Web.Data

Public Class mds_mst_template_dtl
    Inherits System.Web.UI.Page
    Dim dr_user As DataRow
    Dim str As String
    Dim salah As er_custom
    Dim waktu_query As New Stopwatch
    Dim waktu_page As New Stopwatch
    Dim dt As New DataTable
    Dim dt_2 As New DataTable
    Dim cb As GridViewDataComboBoxColumn
    Dim str_menu As String = ",14,"
    Dim dr As DataRow

    Dim id_templ As Integer
    Dim dt_head As New DataTable

    Dim dt_nilai As New DataTable
    Dim dt_limit As New DataTable
    Dim dt_supir As New DataTable
    Dim dr_nilai As DataRow

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub mds_mst_template_dtl_Init(sender As Object, e As EventArgs) Handles Me.Init
        Try
            Me.id_templ = Request.QueryString("id_template")
        Catch ex As Exception
            Response.Redirect("trs_form_couching.aspx")
        End Try
        dr_user = Session("dr_user")
        Me.uc_header.grid = Me.ASPxGridView1 'penting

        str = "<li><a href='home.aspx'>HOME</a></li>"
        str = str & "<li class='active'>EXISTING</li>"
        str = str & "<li><a href='mst_template.aspx'>Master Template Detail</a></li>"
        str = str & "<li><a href='mst_template_dtl.aspx?id_template=" & Me.id_templ & "' style='color: #f00'>Master Template Detail ID. " & Me.id_templ & "</a></li>"
        Me.uc_header.list_menu.InnerHtml = str

        Me.uc_header.div_search.Visible = False
        Me.uc_header.a_filter.Visible = False
        Me.isi_head()
        Me.isi_data()
    End Sub

    Private Sub mds_mst_template_dtl_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        Me.waktu_page.Stop()
    End Sub

    Private Sub mds_mst_template_dtl_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
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

    Private Sub isi_head()
        str = "select * "
        str = str & "from mds_mst_template where id_template = " & Me.id_templ

        salah = Mod_Utama.isi_data(Me.dt_head, str, "id_template", waktu_query)
        If salah.er_hasil <> "" Then
            Me.Jika_Error(str, salah.er_hasil, Me.Page.ToString & "//" & GetCurrentMethod.Name, 1)
        End If
    End Sub

    Private Sub isi_data()
        str = "select * "
        str = str & "from mds_mst_template_dtl where id_template = " & Me.id_templ

        salah = Mod_Utama.isi_data(Me.dt, str, "id_template_dtl", waktu_query)
        If salah.er_hasil <> "" Then
            Me.Jika_Error(str, salah.er_hasil, Me.Page.ToString & "//" & GetCurrentMethod.Name, 1)
        End If

        Me.ASPxGridView1.DataSource = Me.dt
        Me.ASPxGridView1.KeyFieldName = "id_template_dtl"
        Me.ASPxGridView1.DataBind()
        Mod_Utama.Atur_Grid(Me.ASPxGridView1)
    End Sub

    Private Sub ASPxGridView1_CommandButtonInitialize(sender As Object, e As ASPxGridViewCommandButtonEventArgs) Handles ASPxGridView1.CommandButtonInitialize
        Select Case e.ButtonType
            Case ColumnCommandButtonType.Edit
                If CStr(dr_user("ubah")).Contains(str_menu) = False Then e.Visible = False
            Case ColumnCommandButtonType.Delete
                If CStr(dr_user("hapus")).Contains(str_menu) = False Then e.Visible = False
        End Select
    End Sub

    Private Sub ASPxGridView1_RowInserting(sender As Object, e As ASPxDataInsertingEventArgs) Handles ASPxGridView1.RowInserting
        str = "insert into mds_mst_template_dtl (id_template_dtl, id_template, ket, "
        str = str & "u_date, u_user, c_date, c_user) values ( "
        str = str & "(select isnull(max(id_template_dtl), 0) + 1 from mds_mst_template_dtl), "
        str = str & "'" & Me.id_templ & "', "
        str = str & "'" & e.NewValues("ket") & "', "
        str = str & "getdate(), "
        str = str & "'" & dr_user("nama") & "', "
        str = str & "getdate(), "
        str = str & "'" & dr_user("nama") & "')"
        salah.er_hasil = Mod_Utama.exec_sql(str, dr_user, "MST Template Details Insert")

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
        str = "UPDATE mds_mst_template_dtl SET "
        str = str & "ket = '" & e.NewValues("ket") & "', "
        str = str & "u_date = getdate(), "
        str = str & "u_user = '" & dr_user("nama") & "' "
        str = str & "where id_template_dtl = " & e.Keys("id_template_dtl") & " "

        salah.er_hasil = Mod_Utama.exec_sql(str, dr_user, "MST Template Detail Update")
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
        Mod_Utama.log_delete("select * From mds_mst_template_dtl where id_template_dtl = " & e.Keys("id_template_dtl"), "mds_mst_template_dtl", dr_user)
        str = "delete mds_mst_template_dtl "
        str = str & "where id_template_dtl = " & e.Keys("id_template_dtl")
        salah.er_hasil = Mod_Utama.exec_sql(str, dr_user, "MST Template Details delete")

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