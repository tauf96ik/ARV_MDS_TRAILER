Imports DevExpress.Web
Imports System.Reflection.MethodBase
Imports DevExpress.Web.Data

Public Class mds_mst_rtt
    Inherits System.Web.UI.Page

    Dim dr_user As DataRow
    Dim str As String
    Dim salah As er_custom
    Dim waktu_query As New Stopwatch
    Dim waktu_page As New Stopwatch
    Dim dt As New DataTable
    Dim cb As GridViewDataComboBoxColumn
    Dim str_menu As String = ",8,"
    Dim dr As DataRow

    Dim dt_train, dt_supir As New DataTable

    Dim listbox_train As ASPxListBox

    Dim dr_focus As DataRow

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub dd_train_Init(sender As Object, e As EventArgs)
        If dr_focus Is Nothing Then Exit Sub
        Dim hasil As String = ""

        Select Case TryCast(sender, ASPxDropDownEdit).ID
            Case "dd_train"
                hasil = dr_focus("materi")
        End Select

        TryCast(sender, ASPxDropDownEdit).Text = hasil
    End Sub

    Protected Sub listBox_train_Init(sender As Object, e As EventArgs)
        Select Case TryCast(sender, ASPxListBox).ID
            Case "listBox_train"
                listbox_train = TryCast(sender, ASPxListBox)
                isi_menu(TryCast(sender, ASPxListBox), "materi")
        End Select
    End Sub

    Private Sub mds_mst_rtt_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        Me.waktu_page.Stop()
        Mod_Utama.master_waktu(Me.waktu_query, Me.waktu_page, uc_footer)
    End Sub

    Private Sub mds_mst_rtt_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Me.waktu_page.Start()
    End Sub

    Private Sub mds_mst_rtt_Init(sender As Object, e As EventArgs) Handles Me.Init
        dr_user = Session("dr_user")
        Me.uc_header.grid = Me.ASPxGridView1

        str = "<li><a href='home.aspx'>HOME</a></li>"
        str = str & "<li class='active'>MASTER</li>"
        str = str & "<li><a href='mds_mst_rtt.aspx'>RTT Training</a></li>"
        If CStr(dr_user("lihat")).Contains(str_menu) = False Then
            Response.Redirect("~/page_no_auth.aspx")
        End If
        Me.uc_header.list_menu.InnerHtml = str

        Me.isi_data()
        Me.isi_supir()
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

    Private Sub isi_supir()
        str = "select * "
        str = str & "from tms_mst_driver where aktif_sta = 1 "
        str = str & "order by nama asc "

        salah = Mod_Utama.isi_data(Me.dt_supir, str, "id_driver", Me.waktu_query)

        cb = Me.ASPxGridView1.Columns("id_spr")
        cb.PropertiesComboBox.DataSource = Me.dt_supir
        cb.PropertiesComboBox.TextField = "nama"
        cb.PropertiesComboBox.ValueField = "id_driver"
        cb.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains
    End Sub

    Private Sub isi_data()
        salah = Mod_Utama.isi_data(Me.dt_train, "select * from mds_mst_training order by id_training", "nama", Me.waktu_query)
        If salah.er_hasil <> "" Then Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)

        str = "select * "
        str = str & "from mds_rtt_train"
        salah = Mod_Utama.isi_data(Me.dt, str, "id_rtt", Me.waktu_query)
        If salah.er_hasil <> "" Then Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)

        Me.ASPxGridView1.DataSource = Me.dt
        Me.ASPxGridView1.KeyFieldName = "id_rtt"
        Me.ASPxGridView1.DataBind()
        Mod_Utama.Atur_Grid(Me.ASPxGridView1)
    End Sub

    Private Sub isi_menu(lb As ASPxListBox, fieldnm As String)
        lb.DataSource = Me.dt_train
        lb.TextField = "nama"
        lb.ValueField = "nama"
        lb.Columns.Add("id_training", "ID", 60)
        lb.Columns.Add("nama", "Materi Training", 200)
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

    Private Sub ASPxGridView1_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles ASPxGridView1.CellEditorInitialize
        dr_focus = ASPxGridView1.GetDataRow(e.VisibleIndex)
        Select Case e.Column.FieldName
            Case "id_spr"
                Dim cb As ASPxComboBox = e.Editor
                cb.DataSource = Me.dt_supir
                cb.ValueField = "id_driver"
                cb.TextField = "nama"
                cb.Columns.Add("nama", "Nama", 250)
                cb.Columns.Add("nik", "NIK", 200)
                cb.DataBind()
                cb.IncrementalFilteringMode = IncrementalFilteringMode.Contains
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

    Private Sub ASPxGridView1_RowDeleting(sender As Object, e As ASPxDataDeletingEventArgs) Handles ASPxGridView1.RowDeleting
        Mod_Utama.log_delete("select * From mds_rtt_train where id_rtt = " & e.Keys("id_rtt"), "mds_rtt_train", dr_user)
        str = "DELETE mds_rtt_train "
        str = str & "where id_rtt = " & e.Keys("id_rtt")

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

    Private Sub ASPxGridView1_RowInserting(sender As Object, e As ASPxDataInsertingEventArgs) Handles ASPxGridView1.RowInserting
        Dim materi As String = nilai_list(listbox_train, "materi", e.NewValues("id_rtt"))

        Dim nomor As String = ""
        Dim bulan As String
        bulan = Format(e.NewValues("tgl_rtt"), "MMMM")

        Dim nBulan As String
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

        Select Case CDbl(CType(e.NewValues("tgl_rtt"), Date).Day / 7)

            Case Is <= 1
                nomor = "Minggu Ke 1 " & nBulan
            Case Is <= 2
                nomor = "Minggu Ke 2 " & nBulan
            Case Is <= 3
                nomor = "Minggu Ke 3 " & nBulan
            Case Is <= 4
                nomor = "Minggu Ke 4 " & nBulan
            Case Is <= 5
                nomor = "Minggu Ke 5 " & nBulan
        End Select

        str = "INSERT INTO mds_rtt_train ("
        str = str & "id_rtt, id_spr, tgl_rtt, materi, minggu, "
        str = str & "c_date, c_user, u_date, u_user) VALUES ("
        str = str & "(select isnull(max(id_rtt),0) + 1 from mds_rtt_train), "
        str = str & "'" & e.NewValues("id_spr") & "', "
        str = str & "'" & e.NewValues("tgl_rtt") & "', "
        str = str & "'" & materi & "', "
        str = str & "'" & nomor & "', "
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

    Private Sub ASPxGridView1_RowUpdating(sender As Object, e As ASPxDataUpdatingEventArgs) Handles ASPxGridView1.RowUpdating
        Dim materi As String = nilai_list(listbox_train, "materi", e.NewValues("id_rtt"))

        Dim nomor As String = ""
        Dim bulan As String
        bulan = Format(e.NewValues("tgl_rtt"), "MMMM")


        Dim nBulan As String
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

        Select Case CDbl(CType(e.NewValues("tgl_rtt"), Date).Day / 7)

            Case Is <= 1
                nomor = "Minggu Ke 1 " & nBulan
            Case Is <= 2
                nomor = "Minggu Ke 2 " & nBulan
            Case Is <= 3
                nomor = "Minggu Ke 3 " & nBulan
            Case Is <= 4
                nomor = "Minggu Ke 4 " & nBulan
            Case Is <= 5
                nomor = "Minggu Ke 5 " & nBulan
        End Select

        str = "UPDATE mds_rtt_train set "
        str = str & "id_spr = '" & e.NewValues("id_spr") & "', "
        str = str & "tgl_rtt = '" & e.NewValues("tgl_rtt") & "', "
        str = str & "materi = '" & materi & "', "
        str = str & "minggu = '" & nomor & "', "
        str = str & "u_date = getdate(), "
        str = str & "u_user = '" & dr_user("nama") & "' "
        str = str & "where id_rtt = " & e.Keys("id_rtt")

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

    Private Function nilai_list(listb As ASPxListBox, fieldnm As String, id_rtt As Int64) As String
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

End Class