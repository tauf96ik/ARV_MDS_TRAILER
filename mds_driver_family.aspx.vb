Imports DevExpress.Web
Imports System.Reflection.MethodBase
Imports DevExpress.Web.Data

Public Class mds_driver_family
    Inherits System.Web.UI.Page

    Dim dr_user As DataRow
    Dim str As String
    Dim salah As er_custom
    Dim waktu_query As New Stopwatch
    Dim waktu_page As New Stopwatch
    Dim dt, dt_head, dt_head2 As New DataTable
    Dim cb As GridViewDataComboBoxColumn
    Dim str_menu As String = ",3,"
    Dim dr As DataRow
    Dim dt_supir As New DataTable
    Dim id_spr, service As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub mds_driver_family_Init(sender As Object, e As EventArgs) Handles Me.Init
        dr_user = Session("dr_user")
        Me.uc_header.grid = Me.ASPxGridView1 'penting

        Try
            id_spr = Request.QueryString("idspr")
        Catch ex As Exception
            Response.Redirect("mds_driver_wingbox.aspx")
        End Try

        str = "<li><a href='home.aspx'>HOME</a></li>"
        str = str & "<li class='active'>EXISTING</li>"
        str = str & "<li><a href='mds_driver_active.aspx'>Daftar Driver</a></li>"
        str = str & "<li><a href=''>Daftar Keluarga Driver</a></li>"
        Me.uc_header.list_menu.InnerHtml = str

        Me.isi_head()
        Me.isi_data()
    End Sub

    Private Sub mds_driver_family_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Me.waktu_page.Start()
    End Sub

    Private Sub mds_driver_family_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
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

    Private Sub isi_head()
        str = "select * from tms_mst_driver where id_driver = " & Me.id_spr & " "

        salah = Mod_Utama.isi_data(Me.dt_head, str, "", Me.waktu_query)
        If salah.er_hasil <> "" Then
            Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
            Return
        End If

        dr = Me.dt_head.Rows(0)
        Me.lb_spr.InnerText = dr("nama")
        Me.lb_nik.InnerText = dr("nik")
    End Sub

    Private Sub isi_data()
        str = "select * from mds_drv_fam where id_spr = " & id_spr & " "

        salah = Mod_Utama.isi_data(dt, str, "id_fam", waktu_query)
        If salah.er_hasil <> "" Then Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)

        Me.ASPxGridView1.DataSource = Me.dt
        Me.ASPxGridView1.KeyFieldName = "id_fam"
        Me.ASPxGridView1.DataBind()
        Mod_Utama.Atur_Grid(Me.ASPxGridView1)
    End Sub

    Private Sub ASPxGridView1_CustomErrorText(sender As Object, e As ASPxGridViewCustomErrorTextEventArgs) Handles ASPxGridView1.CustomErrorText
        e.ErrorText = salah.er_hasil
    End Sub

    Private Sub ASPxGridView1_RowInserting(sender As Object, e As ASPxDataInsertingEventArgs) Handles ASPxGridView1.RowInserting
        str = "insert into mds_drv_fam ("
        str = str & "id_fam, id_spr, nama_fam, tgl_lhr, jk, status, pekerjaan, no_bpjs, "
        str = str & "c_date, c_user, u_date, u_user) VALUES "
        str = str & "((select isnull(max(id_fam), 0) + 1 from mds_drv_fam), "
        str = str & "'" & id_spr & "', "
        str = str & "'" & e.NewValues("nama_fam") & "', "
        str = str & "'" & Format(e.NewValues("tgl_lhr"), "yyyy-MM-dd") & "', "
        str = str & "'" & e.NewValues("jk") & "', "
        str = str & "'" & e.NewValues("status") & "', "
        str = str & "'" & e.NewValues("pekerjaan") & "', "
        str = str & "'" & e.NewValues("no_bpjs") & "', "
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
        str = "update mds_drv_fam set "
        str = str & "nama_fam = '" & e.NewValues("nama_fam") & "', "
        str = str & "tgl_lhr = '" & Format(e.NewValues("tgl_lhr"), "yyyy-MM-dd") & "', "
        str = str & "jk = '" & e.NewValues("jk") & "', "
        str = str & "status = '" & e.NewValues("status") & "', "
        str = str & "pekerjaan = '" & e.NewValues("pekerjaan") & "', "
        str = str & "no_bpjs = '" & e.NewValues("no_bpjs") & "', "
        str = str & "u_date = getdate(), "
        str = str & "u_user = '" & dr_user("nama") & "' "
        str = str & "where id_fam = " & e.Keys("id_fam")

        salah.er_hasil = Mod_Utama.exec_sql(str, dr_user, "MDS Driver Family")
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
        Mod_Utama.log_delete("select * From mds_drv_fam where id_fam = " & e.Keys("id_fam"), "mds_drv_fam", dr_user)
        str = "delete mds_drv_fam "
        str = str & "where id_fam = " & e.Keys("id_fam")

        salah.er_hasil = Mod_Utama.exec_sql(str, dr_user, "MDS Driver Family")
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