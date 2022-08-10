Imports DevExpress.Web
Imports System.Reflection.MethodBase
Imports System.Web.HttpPostedFile
Imports System.Drawing
Imports System.IO

Public Class mds_dashboard
    Inherits System.Web.UI.Page
    Dim dr_user As DataRow
    Dim str As String
    Dim salah As er_custom
    Dim waktu_query As New Stopwatch
    Dim waktu_page As New Stopwatch
    Dim dt, dt_kategori, dt_lokasi, dt_temp, dt_supir As New DataTable
    Dim cb As GridViewDataComboBoxColumn
    Dim dr As DataRow

    Private Sub mds_dashboard_Init(sender As Object, e As EventArgs) Handles Me.Init
        dr_user = Session("dr_user")
        Me.uc_header.grid = Me.ASPxGridView1 'penting

        str = "<li><a href='home.aspx'>HOME</a></li>"
        str = str & "<li><a href='mds_.aspx'>DASHBOARD</a></li>"
        Me.uc_header.list_menu.InnerHtml = str
        'If CStr(dr_user("lihat")).Contains(str_menu) = False Then
        '    Response.Redirect("~/page_no_auth.aspx")
        'End If
        Me.uc_header.div_search.Visible = False
        Me.uc_header.a_filter.Visible = False


        Me.isi_data()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub mds_dashboard_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        Me.waktu_page.Stop()

    End Sub

    Private Sub mds_dashboard_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
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
        str = str & "from izzi_gps "
        str = str & "where geo_last like '%REST%' "
        str = str & "ORDER BY nopol, tgl"

        salah = Mod_Utama.isi_data(Me.dt, str, "", Me.waktu_query)
        If salah.er_hasil <> "" Then Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)

        Me.ASPxGridView1.DataSource = Me.dt
        Me.ASPxGridView1.KeyFieldName = ""
        Me.ASPxGridView1.DataBind()
        Mod_Utama.Atur_Grid(Me.ASPxGridView1)
    End Sub
End Class