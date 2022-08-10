﻿Imports DevExpress.Web
Imports System.Reflection.MethodBase
Imports DevExpress.Web.Data

Public Class mds_mst_ba
    Inherits System.Web.UI.Page

    Dim dr_user As DataRow
    Dim str As String
    Dim salah As er_custom
    Dim waktu_query As New Stopwatch
    Dim waktu_page As New Stopwatch
    Dim dt As New DataTable
    Dim cb As GridViewDataComboBoxColumn
    Dim str_menu As String = ",7,"
    Dim dr As DataRow

    Dim dt_nopol As New DataTable
    Dim dt_supir As New DataTable
    Dim dt_lokasi As New DataTable
    Dim dt_ba As New DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub mds_mst_ba_Init(sender As Object, e As EventArgs) Handles Me.Init
        dr_user = Session("dr_user")
        Me.uc_header.grid = Me.ASPxGridView1 'penting

        str = "<li><a href='home.aspx'>HOME</a></li>"
        str = str & "<li class='active'>MASTER</li>"
        str = str & "<li><a href='mds_mst_ba.aspx'>Master Berita Acara</a></li>"

        If CStr(dr_user("baru")).Contains(str_menu) Then
            str = str & "<li><a href='#' id='new' onclick='baru()' style='color: #f00'>New Record</a></li>"
        End If

        Me.uc_header.list_menu.InnerHtml = str
        If CStr(dr_user("lihat")).Contains(str_menu) = False Then
            Response.Redirect("~/page_no_auth.aspx")
        End If
        Me.isi_data()
    End Sub

    Private Sub mds_mst_ba_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        Me.waktu_page.Stop()
        Mod_Utama.master_waktu(Me.waktu_query, Me.waktu_page, uc_footer)
    End Sub

    Private Sub mds_mst_ba_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
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

        str = "select * "
        str = str & "from mds_mst_ba "
        Me.salah = Mod_Utama.isi_data(dt, str, "id_ba", waktu_query)

        If salah.er_hasil <> "" Then
            Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
            Exit Sub
        End If

        Me.dt.AcceptChanges()

        Me.ASPxGridView1.DataSource = dt
        Me.ASPxGridView1.KeyFieldName = "id_ba"
        Mod_Utama.Atur_Grid(Me.ASPxGridView1)
        Me.ASPxGridView1.DataBind()
    End Sub

    Private Sub ASPxGridView1_CommandButtonInitialize(sender As Object, e As ASPxGridViewCommandButtonEventArgs) Handles ASPxGridView1.CommandButtonInitialize
        dr = Me.ASPxGridView1.GetDataRow(e.VisibleIndex)

        Select Case e.ButtonType
            Case ColumnCommandButtonType.Edit
                If CStr(dr_user("ubah")).Contains(str_menu) = False Then e.Visible = False
            Case ColumnCommandButtonType.[New]
                If dr("id_ba") < 0 Then
                    e.Visible = False
                End If
            Case ColumnCommandButtonType.Delete
                If CStr(dr_user("hapus")).Contains(str_menu) = False Then e.Visible = False
                If dr("id_ba") < 0 Then e.Visible = False
        End Select
    End Sub

    Private Sub ASPxGridView1_CustomErrorText(sender As Object, e As ASPxGridViewCustomErrorTextEventArgs) Handles ASPxGridView1.CustomErrorText
        e.ErrorText = salah.er_hasil
    End Sub

    Private Sub ASPxGridView1_RowInserting(sender As Object, e As ASPxDataInsertingEventArgs) Handles ASPxGridView1.RowInserting
        str = "INSERT INTO mds_mst_ba ("
        str = str & "id_ba, nama, ket, inisial, point, bobot, dimensi, rencana, indikator, "
        str = str & "c_date, c_user, u_date, u_user) VALUES ("
        str = str & "(select isnull(max(id_ba),0) + 1 from mds_mst_ba), "
        str = str & "'" & e.NewValues("nama") & "', "
        str = str & "'" & e.NewValues("ket") & "', "
        str = str & "'" & e.NewValues("inisial") & "', "
        str = str & "'" & e.NewValues("point") & "', "
        str = str & "'" & e.NewValues("bobot") & "', "
        str = str & "'" & e.NewValues("dimensi") & "', "
        str = str & "'" & e.NewValues("rencana") & "', "
        str = str & "'" & e.NewValues("indikator") & "', "
        str = str & "getdate(), "
        str = str & "'" & dr_user("nama") & "', "
        str = str & "getdate(), "
        str = str & "'" & dr_user("nama") & "') "

        salah.er_hasil = Mod_Utama.exec_sql(str, dr_user, "BA/Master-insert")
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
        str = "update mds_mst_ba set "
        str = str & "nama = '" & e.NewValues("nama") & "', "
        str = str & "inisial = '" & e.NewValues("inisial") & "', "
        str = str & "point = '" & e.NewValues("point") & "', "
        str = str & "bobot = '" & e.NewValues("bobot") & "', "
        str = str & "dimensi = '" & e.NewValues("dimensi") & "', "
        str = str & "rencana = '" & e.NewValues("rencana") & "', "
        str = str & "indikator = '" & e.NewValues("indikator") & "', "
        str = str & "ket = '" & e.NewValues("ket") & "', "
        str = str & "u_date = getdate(), "
        str = str & "u_user = '" & dr_user("nama") & "' "
        str = str & "where id_ba = " & e.Keys("id_ba")

        salah.er_hasil = Mod_Utama.exec_sql(str, dr_user, "BA/Master-update")
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
        Mod_Utama.log_delete("select * From mds_mst_ba where id_ba = " & e.Keys("id_ba"), "mds_mst_ba", dr_user)
        str = "delete mds_mst_ba "
        str = str & "where id_ba = " & e.Keys("id_ba")

        salah.er_hasil = Mod_Utama.exec_sql(str, dr_user, "BA/Master-delete")
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