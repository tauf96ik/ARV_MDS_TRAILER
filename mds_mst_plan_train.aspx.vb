Imports DevExpress.Web
Imports System.Reflection.MethodBase
Imports DevExpress.Web.Data

Public Class mds_mst_plan_train
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
    Dim listbox_train, listbox_driver As ASPxListBox
    Dim dr_focus As DataRow

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub dd_driver_Init(sender As Object, e As EventArgs)
        If dr_focus Is Nothing Then Exit Sub
        Dim hasil As String = ""

        Select Case TryCast(sender, ASPxDropDownEdit).ID
            Case "dd_driver"
                hasil = dr_focus("driver")
        End Select

        TryCast(sender, ASPxDropDownEdit).Text = hasil
    End Sub

    Protected Sub listBox_driver_Init(sender As Object, e As EventArgs)
        Select Case TryCast(sender, ASPxListBox).ID
            Case "listBox_driver"
                listbox_driver = TryCast(sender, ASPxListBox)
                isi_menudriver(TryCast(sender, ASPxListBox), "driver")
        End Select
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

    Private Sub mds_mst_plan_train_Init(sender As Object, e As EventArgs) Handles Me.Init
        dr_user = Session("dr_user")
        Me.uc_header.grid = Me.ASPxGridView1

        str = "<li><a href='home.aspx'>HOME</a></li>"
        str = str & "<li class='active'>MASTER</li>"
        str = str & "<li><a href='mds_mst_plan_train.aspx'>Planing Training</a></li>"
        If CStr(dr_user("lihat")).Contains(str_menu) = False Then
            Response.Redirect("~/page_no_auth.aspx")
        End If
        Me.uc_header.list_menu.InnerHtml = str

        Me.isi_data()
    End Sub

    Private Sub mds_mst_plan_train_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        Me.waktu_page.Stop()
        Mod_Utama.master_waktu(Me.waktu_query, Me.waktu_page, uc_footer)
    End Sub

    Private Sub mds_mst_plan_train_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
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

    Private Sub isi_data()

        salah = Mod_Utama.isi_data(Me.dt_supir, "select * from tms_mst_driver where aktif_sta = 1 order by nama asc", "nama", Me.waktu_query)
        If salah.er_hasil <> "" Then Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)

        salah = Mod_Utama.isi_data(Me.dt_train, "select * from mds_mst_training order by id_training", "nama", Me.waktu_query)
        If salah.er_hasil <> "" Then Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)

        str = "select *, "
        str = str & "(select 0) as jumlah "
        str = str & "from mds_mst_plan_train "
        salah = Mod_Utama.isi_data(Me.dt, str, "id_plan_train", Me.waktu_query)
        If salah.er_hasil <> "" Then Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)

        For Each dtr As DataRow In Me.dt.Rows
            Dim kata() As String = dtr("driver").Split(", ")
            dtr("jumlah") = kata.Length & " "
        Next
        Me.dt.AcceptChanges()

        Me.ASPxGridView1.DataSource = Me.dt
        Me.ASPxGridView1.KeyFieldName = "id_plan_train"
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

    Private Sub isi_menudriver(lb As ASPxListBox, fieldnm As String)
        lb.DataSource = Me.dt_supir
        lb.TextField = "nama"
        lb.ValueField = "nama"
        lb.Columns.Add("id_driver", "ID", 60)
        lb.Columns.Add("nama", "Nama Driver", 200)
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

    Private Function nilai_list(listb As ASPxListBox, fieldnm As String, id_plan_train As Int64) As String
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

    Private Function nilai_listdriver(listb As ASPxListBox, fieldnm As String, id_plan_train As Int64) As String
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
        Dim materi As String = nilai_list(listbox_train, "materi", e.NewValues("id_plan_train"))
        Dim driver As String = nilai_listdriver(listbox_driver, "driver", e.NewValues("id_plan_train"))
        Dim nomor As String = ""
        Dim bulan As String
        bulan = Format(e.NewValues("tgl"), "MMMM")

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

        Select Case CDbl(CType(e.NewValues("tgl"), Date).Day / 7)

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

        str = "INSERT INTO mds_mst_plan_train ("
        str = str & "id_plan_train, tgl, materi, driver, minggu, "
        str = str & "c_date, c_user, u_date, u_user) VALUES ("
        str = str & "(select isnull(max(id_plan_train),0) + 1 from mds_mst_plan_train), "
        str = str & "'" & e.NewValues("tgl") & "', "
        str = str & "'" & materi & "', "
        str = str & "'" & driver & "', "
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
        Dim materi As String = nilai_list(listbox_train, "materi", e.NewValues("id_plan_train"))
        Dim driver As String = nilai_listdriver(listbox_driver, "driver", e.NewValues("id_plan_train"))

        Dim nomor As String = ""
        Dim bulan As String
        bulan = Format(e.NewValues("tgl"), "MMMM")


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

        Select Case CDbl(CType(e.NewValues("tgl"), Date).Day / 7)

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

        str = "UPDATE mds_mst_plan_train set "
        str = str & "tgl = '" & e.NewValues("tgl") & "', "
        str = str & "driver = '" & driver & "', "
        str = str & "materi = '" & materi & "', "
        str = str & "minggu = '" & nomor & "', "
        str = str & "u_date = getdate(), "
        str = str & "u_user = '" & dr_user("nama") & "' "
        str = str & "where id_plan_train = " & e.Keys("id_plan_train")

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

    Private Sub ASPxGridView1_RowDeleting(sender As Object, e As ASPxDataDeletingEventArgs) Handles ASPxGridView1.RowDeleting
        str = "DELETE mds_mst_plan_train "
        str = str & "where id_plan_train = " & e.Keys("id_plan_train")

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