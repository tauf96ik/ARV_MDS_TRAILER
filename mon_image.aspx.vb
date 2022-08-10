Imports DevExpress.Web
Imports System.Reflection.MethodBase
Imports System.IO

Public Class mon_image
    Inherits System.Web.UI.Page

    Dim dr_user As DataRow
    Dim str As String
    Dim salah As er_custom
    Dim waktu_query As New Stopwatch
    Dim waktu_page As New Stopwatch
    Dim dt, dt_photo As New DataTable
    Dim cb As GridViewDataComboBoxColumn
    Dim str_menu As String = ",6,"
    Dim dr As DataRow

    Private Sub page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Me.waktu_page.Start()
    End Sub
    Private Sub page_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        Me.waktu_page.Stop()
        Mod_Utama.master_waktu(Me.waktu_query, Me.waktu_page, uc_footer)
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

    Private Sub page_Init(sender As Object, e As EventArgs) Handles Me.Init
        dr_user = Session("dr_user")
        Me.uc_header.grid = Me.ASPxGridView1 'penting

        str = "<li><a href='home.aspx'>HOME</a></li>"
        str = str & "<li class='active'>EXISTING</li>"
        str = str & "<li><a href='mds_driver_act.aspx'>Daftar Driver Active</a></li>"
        If CStr(dr_user("baru")).Contains(str_menu) Then
            str = str & "<li><a href='#' id='new' onclick='baru()' style='color: #f00'>New Record</a></li>"
        End If
        Me.uc_header.list_menu.InnerHtml = str
        Me.Isi_Filter()
        Me.isi_photo()
        Me.isi_data()

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub isi_photo()
        str = "SELECT * "
        str = str & "FROM mds_driver_doc "
        str = str & "where jenis = 'Photo' "
        str = str & "order by id_supir_doc desc "
        salah = Mod_Utama.isi_data(Me.dt_photo, str, "id_supir_doc", Me.waktu_query)
        If salah.er_hasil <> "" Then Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)

        'If dt.Rows.Count <> 0 Then
        '    dr = Me.dt.Rows(0)

        'img_supir.ImageUrl = "doc_supir\" & dr("file_nm")
        'End If

    End Sub
    Private Sub isi_data()

        str = "select *, "
        str = str & "(select nama from mst_pendidikan where id_pendidikan = A.id_pendidikan) as pendidikan, "
        str = str & "(select 0) as masakerja_bln, "
        str = str & "(select '') as masakerja, "
        str = str & "(select '') as umur, "
        str = str & "(select '') as sta_img, "
        str = str & "(select '') as alamat_img, "
        str = str & "(select nama from mds_mst_jabatan where id_jbtn = A.jbtn) as nm_jbtn "
        str = str & "from opr_mst_supir A "
        str = str & "where aktif_sta = 1 "
        str = str & "order by id_supir desc "
        Me.salah = Mod_Utama.isi_data(dt, str, "id_supir", waktu_query)

        If salah.er_hasil <> "" Then
            Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
            Exit Sub
        End If

        Dim bln As Integer
        Dim thn As Integer
        Dim bln_lhr As Integer
        Dim thn_lhr As Integer

        Dim dv As DataView = New DataView(Me.dt_photo)
        Dim dtpoto As New DataTable
        Dim drpoto As DataRow
        For Each dtr As DataRow In Me.dt.Rows
            bln = DateDiff(DateInterval.Month, dtr("tgl_masuk"), Now)
            thn = Math.Floor(bln / 12)
            bln_lhr = DateDiff(DateInterval.Month, dtr("tgl_lahir"), Now)
            thn_lhr = Math.Floor(bln_lhr / 12)
            dtr("masakerja_bln") = bln
            dtr("masakerja") = thn.ToString & " | " & bln Mod 12
            dtr("umur") = thn_lhr.ToString & " | " & bln_lhr Mod 12
            'dtr("alamat_img") = "~\img_supir\" & dtr("driver_id") & ".jpg"

            dv.RowFilter = "id_supir = " & dtr("id_supir")
            dtpoto = dv.ToTable
            If dtpoto.Rows.Count <> 0 Then
                drpoto = dtpoto.Rows(0)
                dtr("alamat_img") = "~\doc_supir\" & drpoto("file_nm")
                dtpoto.Clear()
            End If


            If File.Exists(Server.MapPath(dtr("alamat_img"))) = False Then
                dtr("sta_img") = "NO IMAGE"
            End If
        Next
        Me.dt.AcceptChanges()

        If IsPostBack = False Then Session("rowfilter") = ""

        Me.dt.DefaultView.RowFilter = Session("rowfilter")
        Me.ASPxGridView1.DataSource = dt
        Me.ASPxGridView1.KeyFieldName = "id_supir"
        Me.ASPxGridView1.DataBind()
        Mod_Utama.Atur_Grid(Me.ASPxGridView1)
        Me.ASPxGridView1.Settings.ShowPreview = True


    End Sub

    Private Sub ASPxGridView1_HtmlRowPrepared(sender As Object, e As ASPxGridViewTableRowEventArgs) Handles ASPxGridView1.HtmlRowPrepared
        'If e.RowType = GridViewRowType.Data Then
        '    dr = Me.ASPxGridView1.GetDataRow(e.VisibleIndex)

        '    Dim img As HtmlImage = Me.ASPxGridView1.FindPreviewRowTemplateControl(e.VisibleIndex, "img_supir")
        '    img.Width = 200
        '    img.Src = "img\34.jpg"

        '    Dim ahref As HtmlAnchor = Me.ASPxGridView1.FindPreviewRowTemplateControl(e.VisibleIndex, "a_supir")
        '    ahref.HRef = "img\34.jpg"
        '    ahref.Title = dr("nama").ToString.ToUpper
        'End If


    End Sub


    Private Sub auc_files_FileUploadComplete(sender As Object, e As DevExpress.Web.FileUploadCompleteEventArgs) Handles auc_files.FileUploadComplete
        Dim nmfile As String = String.Format("~/img_supir/{0}", e.UploadedFile.FileName)
        e.UploadedFile.SaveAs(MapPath(nmfile), True)
    End Sub
End Class