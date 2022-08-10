Imports DevExpress.Web
Imports System.Reflection.MethodBase

Public Class mst_group
    Inherits System.Web.UI.Page

    Dim dr_user As DataRow
    Dim str As String
    Dim salah As er_custom
    Dim waktu_query As New Stopwatch
    Dim waktu_page As New Stopwatch
    Dim dt As New DataTable
    Dim cb As GridViewDataComboBoxColumn
    Dim str_menu As String = ",1,"
    Dim dr As DataRow

    Dim dt_app As New DataTable
    Dim dt_menu As New DataTable

    Dim listbox_baru As ASPxListBox
    Dim listbox_ubah As ASPxListBox
    Dim listbox_lihat As ASPxListBox
    Dim listbox_hapus As ASPxListBox
    Dim listbox_spv As ASPxListBox
    Dim listbox_mgr As ASPxListBox
    Dim listbox_fin As ASPxListBox
    Dim listbox_gm As ASPxListBox
    Dim listbox_dir As ASPxListBox
    Dim listbox_oth As ASPxListBox

    Dim dr_focus As DataRow

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
        str = str & "<li><a href='mst_group.aspx'>Authorize Group</a></li>"
        If CStr(dr_user("lihat")).Contains(str_menu) = False Then
            Response.Redirect("~/page_no_auth.aspx")
        End If
        Me.uc_header.list_menu.InnerHtml = str

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
    Private Sub isi_data()
        str = "select * from mds_app order by no_urut"
        salah = Mod_Utama.isi_data(Me.dt_app, str, "id_app", Me.waktu_query)
        If salah.er_hasil <> "" Then Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)

        salah = Mod_Utama.isi_data(Me.dt_menu, "select * from mds_menu order by no_urut", "id_menu", Me.waktu_query)
        If salah.er_hasil <> "" Then Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)

        str = "select * "
        str = str & "from mds_user_grp order by nama"
        salah = Mod_Utama.isi_data(Me.dt, str, "id_user_grp", Me.waktu_query)
        If salah.er_hasil <> "" Then Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)

        Me.ASPxGridView1.DataSource = Me.dt
        Me.ASPxGridView1.KeyFieldName = "id_user_grp"
        Me.ASPxGridView1.DataBind()
        Mod_Utama.Atur_Grid(Me.ASPxGridView1)
    End Sub

    'AUTO LISTBOX
    Protected Sub ASPxDropDownEdit_Init(sender As Object, e As EventArgs)
        If dr_focus Is Nothing Then Exit Sub
        Dim hasil As String = ""

        Select Case TryCast(sender, ASPxDropDownEdit).ID
            Case "dde_lihat"
                hasil = dr_focus("lihat")

            Case "dde_baru"
                hasil = dr_focus("baru")

            Case "dde_ubah"
                hasil = dr_focus("ubah")

            Case "dde_hapus"
                hasil = dr_focus("hapus")

            Case "dde_spv"
                hasil = dr_focus("app_spv")

            Case "dde_mgr"
                hasil = dr_focus("app_mgr")

            Case "dde_dir"
                hasil = dr_focus("app_coo")

            Case "dde_fin"
                hasil = dr_focus("app_fin")

            Case "dde_oth"
                hasil = dr_focus("app_oth")

            Case "dde_gm"
                hasil = dr_focus("app_gm")
        End Select

        TryCast(sender, ASPxDropDownEdit).Text = hasil
    End Sub
    Protected Sub listBox_Init(sender As Object, e As EventArgs)
        Select Case TryCast(sender, ASPxListBox).ID

            Case "listBox_lihat"
                listbox_lihat = TryCast(sender, ASPxListBox)
                isi_menu(TryCast(sender, ASPxListBox), "lihat")

            Case "listBox_baru"
                listbox_baru = TryCast(sender, ASPxListBox)
                isi_menu(TryCast(sender, ASPxListBox), "baru")

            Case "listBox_ubah"
                listbox_ubah = TryCast(sender, ASPxListBox)
                isi_menu(TryCast(sender, ASPxListBox), "ubah")

            Case "listBox_hapus"
                listbox_hapus = TryCast(sender, ASPxListBox)
                isi_menu(TryCast(sender, ASPxListBox), "hapus")

            Case "listBox_spv"
                listbox_spv = TryCast(sender, ASPxListBox)
                Isi_App(TryCast(sender, ASPxListBox), "app_spv")

            Case "listBox_mgr"
                listbox_mgr = TryCast(sender, ASPxListBox)
                Isi_App(TryCast(sender, ASPxListBox), "app_mgr")

            Case "listBox_fin"
                listbox_fin = TryCast(sender, ASPxListBox)
                Isi_App(TryCast(sender, ASPxListBox), "app_fin")

            Case "listBox_dir"
                listbox_dir = TryCast(sender, ASPxListBox)
                Isi_App(TryCast(sender, ASPxListBox), "app_coo")

            Case "listBox_gm"
                listbox_gm = TryCast(sender, ASPxListBox)
                Isi_App(TryCast(sender, ASPxListBox), "app_gm")

            Case "listBox_oth"
                listbox_oth = TryCast(sender, ASPxListBox)
                Isi_App(TryCast(sender, ASPxListBox), "app_oth")

        End Select
    End Sub
    Private Sub isi_menu(lb As ASPxListBox, fieldnm As String)
        lb.DataSource = Me.dt_menu
        lb.TextField = "nama"
        lb.ValueField = "id_menu"
        lb.Columns.Add("id_menu", "ID", 60)
        lb.Columns.Add("nama", "Descriptions", 200)
        lb.DataBind()
        lb.Rows = 20

        Dim nilai As String
        Dim pilihan() As String
        If dr_focus Is Nothing Then
            nilai = ""
        Else
            nilai = dr_focus(fieldnm)
        End If

        If Not nilai = "" Then
            pilihan = nilai.Replace(" ", "").Split(",")
            For i = 0 To pilihan.Length - 1
                Try
                    lb.Items.FindByValue(pilihan(i)).Selected = True
                Catch ex As Exception
                End Try
            Next
        End If
    End Sub
    Private Sub Isi_App(lb As ASPxListBox, fieldnm As String)
        lb.DataSource = Me.dt_app
        lb.TextField = "nama"
        lb.ValueField = "id_app"
        lb.Columns.Add("id_app", "ID", 60)
        lb.Columns.Add("nama", "Descriptions", 160)
        lb.Columns.Add("ket", "Keterangan", 300)
        lb.DataBind()
        lb.Rows = 20

        Dim nilai As String
        Dim pilihan() As String
        If dr_focus Is Nothing Then
            nilai = ""
        Else
            nilai = dr_focus(fieldnm)
        End If

        If Not nilai = "" Then
            pilihan = nilai.Replace(" ", "").Split(",")
            For i = 0 To pilihan.Length - 1
                Try
                    lb.Items.FindByValue(pilihan(i)).Selected = True
                Catch ex As Exception
                End Try
            Next
        End If
    End Sub
    Private Function nilai_list(listb As ASPxListBox, fieldnm As String, id_user_grp As Int64) As String
        Dim hasil As String = ""
        If listb.SelectedItems.Count = 0 Then
            hasil = ""
        Else
            For i = 0 To listb.SelectedItems.Count - 1
                If hasil = "" Then
                    hasil = listb.SelectedItems(i).Value
                Else
                    hasil = hasil & ", " & listb.SelectedItems(i).Value
                End If
            Next
        End If

        Return hasil
    End Function

    'GRID
    Private Sub ASPxGridView1_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles ASPxGridView1.CellEditorInitialize
        dr_focus = ASPxGridView1.GetDataRow(e.VisibleIndex)
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
    Private Sub ASPxGridView1_RowDeleting(sender As Object, e As DevExpress.Web.Data.ASPxDataDeletingEventArgs) Handles ASPxGridView1.RowDeleting
        str = "DELETE mds_user_grp "
        str = str & "where id_user_grp = " & e.Keys("id_user_grp")

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
        Dim lihat As String = nilai_list(listbox_lihat, "lihat", e.NewValues("id_user_grp"))
        Dim baru As String = nilai_list(listbox_baru, "baru", e.NewValues("id_user_grp"))
        Dim ubah As String = nilai_list(listbox_ubah, "ubah", e.NewValues("id_user_grp"))
        Dim hapus As String = nilai_list(listbox_hapus, "hapus", e.NewValues("id_user_grp"))
        Dim app_spv As String = nilai_list(listbox_spv, "app_spv", e.NewValues("id_user_grp"))
        Dim app_mgr As String = nilai_list(listbox_mgr, "app_mgr", e.NewValues("id_user_grp"))
        Dim app_fin As String = nilai_list(listbox_fin, "app_fin", e.NewValues("id_user_grp"))
        Dim app_gm As String = nilai_list(listbox_gm, "app_gm", e.NewValues("id_user_grp"))
        Dim app_dir As String = nilai_list(listbox_dir, "app_coo", e.NewValues("id_user_grp"))
        Dim app_oth As String = nilai_list(listbox_oth, "app_oth", e.NewValues("id_user_grp"))

        str = "INSERT INTO mds_user_grp ("
        str = str & "id_user_grp, nama, lihat, baru, ubah, hapus, app_spv, app_mgr, app_fin, app_gm, app_coo, app_oth, "
        str = str & "c_date, c_user, u_date, u_user) VALUES ("
        str = str & "(select isnull(max(id_user_grp),0) + 1 from mds_user_grp), "
        str = str & "'" & e.NewValues("nama") & "', "
        str = str & "'" & lihat & "', "
        str = str & "'" & baru & "', "
        str = str & "'" & ubah & "', "
        str = str & "'" & hapus & "', "
        str = str & "'" & app_spv & "', "
        str = str & "'" & app_mgr & "', "
        str = str & "'" & app_fin & "', "
        str = str & "'" & app_gm & "', "
        str = str & "'" & app_dir & "', "
        str = str & "'" & app_oth & "', "
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
        Dim lihat As String = nilai_list(listbox_lihat, "lihat", e.NewValues("id_user_grp"))
        Dim baru As String = nilai_list(listbox_baru, "baru", e.NewValues("id_user_grp"))
        Dim ubah As String = nilai_list(listbox_ubah, "ubah", e.NewValues("id_user_grp"))
        Dim hapus As String = nilai_list(listbox_hapus, "hapus", e.NewValues("id_user_grp"))
        Dim app_spv As String = nilai_list(listbox_spv, "app_spv", e.NewValues("id_user_grp"))
        Dim app_mgr As String = nilai_list(listbox_mgr, "app_mgr", e.NewValues("id_user_grp"))
        Dim app_fin As String = nilai_list(listbox_fin, "app_fin", e.NewValues("id_user_grp"))
        Dim app_gm As String = nilai_list(listbox_gm, "app_gm", e.NewValues("id_user_grp"))
        Dim app_dir As String = nilai_list(listbox_dir, "app_coo", e.NewValues("id_user_grp"))
        Dim app_oth As String = nilai_list(listbox_oth, "app_oth", e.NewValues("id_user_grp"))

        str = "UPDATE mds_user_grp set "
        str = str & "nama = '" & e.NewValues("nama") & "', "
        str = str & "lihat = '" & lihat & "', "
        str = str & "baru = '" & baru & "', "
        str = str & "ubah = '" & ubah & "', "
        str = str & "hapus = '" & hapus & "', "
        str = str & "app_spv = '" & app_spv & "', "
        str = str & "app_mgr = '" & app_mgr & "', "
        str = str & "app_fin = '" & app_fin & "', "
        str = str & "app_gm = '" & app_gm & "', "
        str = str & "app_coo = '" & app_dir & "', "
        str = str & "app_oth = '" & app_oth & "', "
        str = str & "u_date = getdate(), "
        str = str & "u_user = '" & dr_user("nama") & "' "
        str = str & "where id_user_grp = " & e.Keys("id_user_grp")

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