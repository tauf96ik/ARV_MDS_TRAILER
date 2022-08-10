Imports DevExpress.Web
Imports System.Reflection.MethodBase
Imports DevExpress.Web.Data

Public Class mds_surat_kontrak
    Inherits System.Web.UI.Page

    Dim dr_user As DataRow
    Dim str As String
    Dim salah As er_custom
    Dim waktu_query As New Stopwatch
    Dim waktu_page As New Stopwatch
    Dim dt, dt2, dt_supir As New DataTable
    Dim cb As GridViewDataComboBoxColumn
    Dim str_menu As String = ",27,"
    Dim dr As DataRow
    Dim idsupir As String

    Dim dari As String

    Dim fil2bln As String = "akhir_kontrak < '" & Format(Now.Date.AddMonths(2), "yyyy-MM-dd") & "' and aktif_sta = 1 "
    Dim fil1bln As String = "akhir_kontrak < '" & Format(Now.Date.AddMonths(1), "yyyy-MM-dd") & "' and aktif_sta = 1 "
    Dim fil1min As String = "akhir_kontrak < '" & Format(Now.Date.AddDays(-7), "yyyy-MM-dd") & "' and aktif_sta = 1 "

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.span_2bln.InnerText = Me.dt.Compute("count(id_driver)", Me.fil2bln)
        Me.span_1bln.InnerText = Me.dt.Compute("count(id_driver)", Me.fil1bln)
        Me.span_1min.InnerText = Me.dt.Compute("count(id_driver)", Me.fil1min)
    End Sub

    Protected Sub fil_2_bln_ServerClick(sender As Object, e As EventArgs)
        Me.dt.DefaultView.RowFilter = Me.fil2bln
        Session("rowfilter") = Me.dt.DefaultView.RowFilter
        Me.ASPxGridView1.DataBind()
    End Sub

    Protected Sub fil_1_bln_ServerClick(sender As Object, e As EventArgs)
        Me.dt.DefaultView.RowFilter = Me.fil1bln
        Session("rowfilter") = Me.dt.DefaultView.RowFilter
        Me.ASPxGridView1.DataBind()
    End Sub

    Protected Sub fil_1min_ServerClick(sender As Object, e As EventArgs)
        Me.dt.DefaultView.RowFilter = Me.fil1min
        Session("rowfilter") = Me.dt.DefaultView.RowFilter
        Me.ASPxGridView1.DataBind()
    End Sub

    Private Sub mds_surat_kontrak_Init(sender As Object, e As EventArgs) Handles Me.Init
        dr_user = Session("dr_user")
        Me.uc_header.grid = Me.ASPxGridView1 'penting

        Try
            Me.dari = Request.QueryString("dari")
        Catch ex As Exception
            Me.dari = ""
        End Try

        dr_user = Session("dr_user")

        str = "<li><a href='home.aspx'>HOME</a></li>"
        str = str & "<li class='active'>EXISTING</li>"
        str = str & "<li><a href='mds_surat_kontrak.aspx'>Surat Kontrak</a></li>"
        Me.uc_header.list_menu.InnerHtml = str

        Me.isi_data()
    End Sub

    Private Sub mds_surat_kontrak_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        Me.waktu_page.Stop()
        Mod_Utama.master_waktu(Me.waktu_query, Me.waktu_page, uc_footer)
    End Sub

    Private Sub mds_surat_kontrak_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
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
        Dim fi_lim As uc_header.filter_limit = uc_header.filtertext

        str = "select " & fi_lim.str_limit & " id_driver, nama, alamat, alamat_domisili, no_ktp, akhir_kontrak, aktif_sta, tgl_masuk, awal_kontrak, kategori, u_date, "
        str = str & "(select CONCAT('/page_print.aspx?idrec=',id_driver,'&sumber=kontrak')) as link, "
        'str = str & "(select CONCAT('/page_print.aspx?idrec=',id_driver,'&sumber=admin')) as link2, "
        str = str & "(select nama from mst_lokasi where id_lokasi = tms_mst_driver.id_lokasi) as lokasi, "
        str = str & "(select 0) as norut "
        str = str & "from tms_mst_driver where "
        If dari = "exp2bln" Then
            str = str & "akhir_kontrak < DATEADD(m, 2, convert(date,getdate())) and "
        ElseIf dari = "exp1bln" Then
            str = str & "akhir_kontrak < DATEADD(m, 1, convert(date,getdate())) and "
        ElseIf dari = "exp1mgg" Then
            str = str & "akhir_kontrak < DATEADD(w, 1, convert(date,getdate())) and "
        End If
        str = str & fi_lim.str_filter
        str = str & "aktif_sta = 1 order by nama asc "

        Me.salah = Mod_Utama.isi_data(dt, str, "id_driver", waktu_query)
        If salah.er_hasil <> "" Then
            Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
            Exit Sub
        End If

        Dim norut As Integer = 1
        For Each dtr As DataRow In Me.dt.Rows
            dtr("norut") = norut
            norut = norut + 1
        Next

        Me.dt.DefaultView.RowFilter = Session("rowfilter")
        Me.ASPxGridView1.DataSource = dt
        Me.ASPxGridView1.KeyFieldName = "id_driver"
        Me.ASPxGridView1.DataBind()
        Mod_Utama.Atur_Grid(Me.ASPxGridView1, True)

        Me.lb_query.Text = "Select With Filter : " & fi_lim.str_filter & " & Limit : " & fi_lim.str_limit
    End Sub

    Private Sub ASPxGridView1_CustomErrorText(sender As Object, e As ASPxGridViewCustomErrorTextEventArgs) Handles ASPxGridView1.CustomErrorText
        e.ErrorText = salah.er_hasil
    End Sub

    Private Sub ASPxGridView1_RowUpdating(sender As Object, e As ASPxDataUpdatingEventArgs) Handles ASPxGridView1.RowUpdating
        str = "update tms_mst_driver set "
        If IsNothing(e.NewValues("tgl_masuk")) = True Then
            str = str & "tgl_masuk = null, "
        Else
            str = str & "tgl_masuk = '" & Format(e.NewValues("tgl_masuk"), "yyyy-MM-dd") & "', "
        End If
        If IsNothing(e.NewValues("akhir_kontrak")) = True Then
            str = str & "akhir_kontrak = null, "
        Else
            str = str & "akhir_kontrak = '" & Format(e.NewValues("akhir_kontrak"), "yyyy-MM-dd") & "', "
        End If
        If IsNothing(e.NewValues("awal_kontrak")) = True Then
            str = str & "awal_kontrak = null, "
        Else
            str = str & "awal_kontrak = '" & Format(e.NewValues("awal_kontrak"), "yyyy-MM-dd") & "', "
        End If
        str = str & "u_date = getdate(), "
        str = str & "u_user = '" & dr_user("nama") & "' "
        str = str & "where id_driver = " & e.Keys("id_driver")

        salah.er_hasil = Mod_Utama.exec_sql(str, dr_user, "Surat Kontrak")
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