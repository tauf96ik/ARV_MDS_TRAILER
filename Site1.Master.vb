Imports System.Reflection.MethodBase

Public Class Site1
    Inherits System.Web.UI.MasterPage

    Dim waktu_query As New Stopwatch
    Dim waktu_page As New Stopwatch
    Dim salah As er_custom
    Dim str As String
    Dim dr_user As DataRow

    Private Sub Jika_Error(er_str As String, er_hasil As String, er_menu As String, nopesan As Integer)
        salah.er_str = er_str
        salah.er_menu = er_menu
        salah.er_waktu = Mod_Utama.str_waktu(Me.waktu_query, Me.waktu_page)
        Session("error") = salah

        Select Case nopesan
            Case 1
                Mod_Utama.tampil_error(Me.Page, "Terjadi kesalahan pada Query, harap kirim laporan ke MIS via email")
            Case Else
                Mod_Utama.tampil_error(Me.Page, "Terjadi kesalahan pada proses, harap kirim laporan ke MIS via email")
        End Select
    End Sub
    Private Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        dr_user = Session("dr_user")

        Dim usernm As String = ""
        Dim userid As String = ""
        userid = Request.QueryString("UserId")
        'userid = 436

        If userid <> "" Then
            Try
                usernm = Mod_encryp.DecryptData(Request.QueryString("ID").Replace(" ", "+"))
                userid = Mod_encryp.DecryptData(Request.QueryString("UserId").Replace(" ", "+"))
            Catch ex As Exception
                Session("pesan") = "Waktu Session Anda telah berakhir, harap login ulang"
            End Try
        Else
            If dr_user Is Nothing Then
                Session("login") = "Session anda telah berakhir"
                Session("login") &= Chr(13) & Chr(10)
                Session("login") &= "Harap anda LOGIN kembali !"
                Response.Redirect("~/page_nomaster.aspx")
            Else
                GoTo LANJUT
            End If
        End If

        Dim dtuser As New DataTable
        str = "Select *, "
        str = str & "(select id_dept from mst_user where id_user = mds_user.id_user) as id_dept, "
        str = str & "(select gm_dept from mst_user where id_user = mds_user.id_user) as gm_dept, "
        str = str & "(select nama from mst_user where id_user = mds_user.id_user) as nama, "
        str = str & "isnull((select nama from mds_user_grp where id_user_grp = mds_user.id_user_grp),'0') as grup, "
        str = str & "isnull((select lihat from mds_user_grp where id_user_grp = mds_user.id_user_grp),'0') as lihat, "
        str = str & "isnull((select baru from mds_user_grp where id_user_grp = mds_user.id_user_grp),'0') as baru, "
        str = str & "isnull((select ubah from mds_user_grp where id_user_grp = mds_user.id_user_grp),'0') as ubah, "
        str = str & "isnull((select hapus from mds_user_grp where id_user_grp = mds_user.id_user_grp),'0') as hapus, "
        str = str & "isnull((select app_spv from mds_user_grp where id_user_grp = mds_user.id_user_grp),'0') as spv, "
        str = str & "isnull((select app_mgr from mds_user_grp where id_user_grp = mds_user.id_user_grp),'0') as mgr, "
        str = str & "isnull((select app_fin from mds_user_grp where id_user_grp = mds_user.id_user_grp),'0') as fin, "
        str = str & "isnull((select app_gm from mds_user_grp where id_user_grp = mds_user.id_user_grp),'0') as gm, "
        str = str & "isnull((select app_coo from mds_user_grp where id_user_grp = mds_user.id_user_grp),'0') as coo, "
        str = str & "isnull((select app_oth from mds_user_grp where id_user_grp = mds_user.id_user_grp),'0') as oth, "
        str = str & "('" & userid & "' + ',' + (select LTRIM(str(id_user)) + ',' from mst_user_head where id_user_head = '" & userid & "' for xml path(''))) as staf "
        str = str & "from mds_user "
        str = str & "where id_user = '" & userid & "' "

        salah = Mod_Utama.isi_data(dtuser, str, "id_user", waktu_query)
        If salah.er_hasil <> "" Then Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)

        If dtuser.Rows.Count > 0 Then
            dtuser.Rows(0).BeginEdit()
            dtuser.Rows(0)("lihat") = "," & Replace(dtuser.Rows(0)("lihat"), " ", "") & ","
            dtuser.Rows(0)("baru") = "," & Replace(dtuser.Rows(0)("baru"), " ", "") & ","
            dtuser.Rows(0)("ubah") = "," & Replace(dtuser.Rows(0)("ubah"), " ", "") & ","
            dtuser.Rows(0)("hapus") = "," & Replace(dtuser.Rows(0)("hapus"), " ", "") & ","
            If IsDBNull(dtuser.Rows(0)("staf")) Then dtuser.Rows(0)("staf") = "," & userid & ","
            dtuser.Rows(0)("spv") = "," & Replace(dtuser.Rows(0)("spv"), " ", "") & ","
            dtuser.Rows(0)("mgr") = "," & Replace(dtuser.Rows(0)("mgr"), " ", "") & ","
            dtuser.Rows(0)("fin") = "," & Replace(dtuser.Rows(0)("fin"), " ", "") & ","
            dtuser.Rows(0)("gm") = "," & Replace(dtuser.Rows(0)("gm"), " ", "") & ","
            dtuser.Rows(0)("coo") = "," & Replace(dtuser.Rows(0)("coo"), " ", "") & ","
            dtuser.Rows(0)("oth") = "," & Replace(dtuser.Rows(0)("oth"), " ", "") & ","
            dtuser.Rows(0)("gm_dept") = "," & Replace(dtuser.Rows(0)("gm_dept"), " ", "") & ","
            dtuser.Rows(0).EndEdit()

            Mod_Utama.log_user(dtuser.Rows(0), "MDS", "Open Web", "Login")
            Session("dr_user") = dtuser.Rows(0)
        Else
            Session("login") = "ID anda tidak terdaftar"
            Session("login") &= Chr(13) & Chr(10)
            Session("login") &= "Harap anda LOGIN kembali !"
            Response.Redirect("~/page_nomaster.aspx")
        End If

LANJUT:
        dr_user = Session("dr_user")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lb_user.InnerText = dr_user("nama")
        photo_user.Src = "~\Img_User\" & dr_user("id_user") & ".jpg"

        a_publish.InnerText = "Publish Date : " & Format(System.IO.File.GetLastWriteTime(System.Reflection.Assembly.GetExecutingAssembly().Location).Date, "yyyy-MM-dd")
        'a_logout.HRef = Mod_Utama.page_login
    End Sub

    Public Sub hide_menu()
        Me.body_site.Attributes.Add("class", "sidebar-wide")
        Me.div_container.Attributes.Add("class", "page-container sidebar-hidden")
    End Sub
    Public Sub small_menu()
        Me.body_site.Attributes.Add("class", "sidebar-narrow")
        Me.div_container.Attributes.Add("class", "page-container")
    End Sub

    Protected Sub a_logout_ServerClick(sender As Object, e As EventArgs)
        HttpContext.Current.Session.Clear()
        HttpContext.Current.Session.Abandon()
        HttpContext.Current.Response.Cookies.Add(New HttpCookie("ASP.NET_SessionId", ""))
        Response.Redirect("~/page_nomaster.aspx")
    End Sub
End Class