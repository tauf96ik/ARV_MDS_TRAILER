Imports DevExpress.Web
Imports System.Reflection.MethodBase
Imports System.Web.HttpPostedFile
Imports System.Drawing
Imports System.IO

Public Class mds_driver_doc
    Inherits System.Web.UI.Page

    Dim dr_user As DataRow
    Dim str As String
    Dim salah As er_custom
    Dim waktu_query As New Stopwatch
    Dim waktu_page As New Stopwatch
    Dim dt As New DataTable
    Dim cb As GridViewDataComboBoxColumn
    'Dim str_menu As String = ",40,"
    'Dim str_app As String = ",5,"
    Dim dr As DataRow

    Dim id_supir As Int64
    Dim dt_head As New DataTable

    Private Sub page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Me.waktu_page.Start()
    End Sub
    Private Sub page_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
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

    Private Sub page_Init(sender As Object, e As EventArgs) Handles Me.Init
        Try
            id_supir = Request.QueryString("id_supir")
        Catch ex As Exception
            Response.Redirect("mds_driver_act.aspx")
        End Try

        dr_user = Session("dr_user")
        'Me.uc_header.grid = Me.ASPxGridView1

        str = "<li><a href='home.aspx'>HOME</a></li>"
        str = str & "<li class='active'>EXISTING</li>"
        str = str & "<li><a href='mds_driver_act.aspx'>Daftar Driver Active</a></li>"
        str = str & "<li><a href='mds_driver_doc.aspx?id_supir = " & Me.id_supir & "' style='color: #f00'>Driver No. " & Me.id_supir & "</a></li>"
        Me.uc_header.list_menu.InnerHtml = str

        Me.uc_header.div_search.Visible = False
        Me.uc_header.a_filter.Visible = False

        Me.isi_head()
        Me.isi_data()
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub isi_head()
        str = "select * "
        str = str & "from opr_mst_supir "
        str = str & "where id_supir = " & Me.id_supir & " "
        salah = Mod_Utama.isi_data(Me.dt_head, str, "id_supir", Me.waktu_query)
        If salah.er_hasil <> "" Then Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)

        dr = Me.dt_head.Rows(0)
        'Me.tx_bpk.Text = dr("no_bpk")
        'Me.tx_pemohon.Text = dr("pemohon_nm")

        'If dr("mgr_sta") = True Then Me.bt_img.Attributes("class") = "btn btn-primary disabled"
    End Sub
    Private Sub isi_data()
        str = "SELECT * "
        str = str & "FROM mds_driver_doc "
        str = str & "where id_supir = " & Me.id_supir & " "
        str = str & "order by id_supir_doc desc "
        salah = Mod_Utama.isi_data(Me.dt, str, "id_supir_doc", Me.waktu_query)
        If salah.er_hasil <> "" Then Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)

        Me.ASPxGridView1.DataSource = Me.dt
        Me.ASPxGridView1.KeyFieldName = "id_supir_doc"
        Me.ASPxGridView1.DataBind()
        Mod_Utama.Atur_Grid(Me.ASPxGridView1)
        Me.ASPxGridView1.Settings.ShowFooter = True
    End Sub

    Protected Sub bt_img_ServerClick(sender As Object, e As EventArgs)
        Dim filePath As String = tx_fileimg.PostedFile.FileName
        Dim filename As String = Path.GetFileName(filePath)
        Dim ext As String = Path.GetExtension(filename)

        If ext.ToLower <> ".jpg" And ext.ToLower <> ".jpeg" Then
            Mod_Utama.tampil_error(Me, "File bukan berupa image *.jpg | *.jpeg")
            Return
        End If

        Dim filepost As HttpPostedFile = tx_fileimg.PostedFile
        Dim filekb As Integer = filepost.ContentLength

        Dim filecurr As HttpPostedFile
        If filekb > 800000 Then
            resize_img(tx_fileimg.PostedFile)
            Return
        Else
            filecurr = tx_fileimg.PostedFile
        End If

        Dim title As String = Me.id_supir.ToString & Format(Now, "_yyyyMMddHHmmss")
        Try
            tx_fileimg.PostedFile.SaveAs(Server.MapPath("~\doc_supir\" & title & ".jpg"))
        Catch ex As Exception
            Mod_Utama.tampil_error(Me, "Simpan Standart Gambar User Tidak Berhasil")
            Return
        End Try

        If Me.insert_data(title) = False Then
            Mod_Utama.tampil_error(Me, "Simpan Standart Gambar User Tidak Berhasil")
            Exit Sub
        End If

        Mod_Utama.tampil_sukses(Me, "Simpan Gambar User Terbaru Telah Berhasil")
        Me.isi_data()
    End Sub
    Private Sub resize_img(filenm As HttpPostedFile)
        Dim stream As Stream = filenm.InputStream
        Dim oriimg As Bitmap = New Bitmap(stream)

        Dim newheight As Integer = CInt(oriimg.Height * (CSng(600) / CSng(oriimg.Width)))
        Dim newimg As Bitmap = New Bitmap(600, newheight)
        Dim graf As Graphics = Graphics.FromImage(newimg)
        graf.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
        graf.DrawImage(oriimg, 0, 0, 600, newheight)

        Dim title As String = Me.id_supir.ToString & Format(Now, "_yyyyMMddHHmmss")
        Try
            newimg.Save(Server.MapPath("~\doc_supir\" & title & ".jpg"))
        Catch ex As Exception
            Mod_Utama.tampil_error(Me, "Simpan BIG Gambar User Tidak Berhasil")
            Exit Sub
        End Try

        If Me.insert_data(title) = False Then
            Mod_Utama.tampil_error(Me, "Simpan BIG Gambar User Tidak Berhasil")
            Exit Sub
        End If

        Mod_Utama.tampil_sukses(Me, "Simpan Gambar User Terbaru Telah Berhasil")
        Me.isi_data()
    End Sub
    Private Function insert_data(nmfile As String) As Boolean
        str = "insert mds_driver_doc ("
        str = str & "id_supir_doc, id_supir, file_nm, title_file, jenis, ket, "
        str = str & "c_date, c_user) VALUES ("
        str = str & "(select isnull(max(id_supir_doc),0) + 1 from mds_driver_doc), "
        str = str & "'" & Me.id_supir & "', "
        str = str & "'" & nmfile & ".jpg" & "', "
        str = str & "'" & Me.tx_fileimg.Value & "', "
        str = str & "'" & Me.cb_doc.Value & "', "
        str = str & "'" & Me.tx_ket.Value & "', "
        str = str & "getdate(), "
        str = str & "'" & dr_user("nama") & "') "
        salah.er_hasil = Mod_Utama.exec_sql(str)
        If salah.er_hasil <> "" Then
            MsgBox(salah.er_hasil)
            Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
            Return False
        End If

        Return True
    End Function

    Private Sub ASPxGridView1_CommandButtonInitialize(sender As Object, e As ASPxGridViewCommandButtonEventArgs) Handles ASPxGridView1.CommandButtonInitialize
        dr = Me.dt_head.Rows(0)
        If dr Is Nothing Then Return

        'If dr("mgr_sta") = True Then e.Visible = False
    End Sub

    Private Sub ASPxGridView1_RowDeleting(sender As Object, e As DevExpress.Web.Data.ASPxDataDeletingEventArgs) Handles ASPxGridView1.RowDeleting
        File.Delete("c:\doc_supir\" & e.Keys("file_nm"))

        str = "DELETE mds_driver_doc "
        str = str & "WHERE id_supir_doc = " & e.Keys("id_supir_doc")

        salah.er_hasil = Mod_Utama.exec_sql(str, dr_user, "Driver-Act/Doc")
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