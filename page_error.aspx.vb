Public Class page_error
    Inherits System.Web.UI.Page

    Dim salah As er_custom
    Dim dr_user As DataRow
    Dim str As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        dr_user = Session("dr_user")
        salah = Session("error")

        If salah.er_hasil = "" Then
            Me.ASPxLabel1.Text = "Tidak ditemukan Kesalahan !"
            Me.ASPxLabel2.Text = ""
            Me.ASPxLabel3.Text = ""
            Me.ASPxLabel4.Text = ""
            Me.ASPxLabel5.Text = ""
            Me.uc_header.list_menu.InnerHtml = ""

        Else
            str = "<li><a href='home.aspx'>HOME</a></li>"
            'str = str & "<li><a href='#' id='new' onclick='gocallbackemail()' style='color: #f00'>Click here for Send Email Automatic to MIS</a></li>"
            str = str & "<li><a href='#' id='new' onclick='gocallbackemail()' style='color: #f00'>Click here for Send Email Automatic to MIS</a></li>"
            Me.uc_header.list_menu.InnerHtml = str

            Me.ASPxLabel1.Text = "Message : " & salah.er_hasil
            Me.ASPxLabel2.Text = "Location : " & salah.er_menu & " // " & salah.er_page
            Me.ASPxLabel3.Text = "Timing : " & salah.er_waktu
            Me.ASPxLabel4.Text = "Query : " & salah.er_str
            Me.ASPxLabel5.Text = "User : " & dr_user("nama").ToString & " // " & Format(Now, "yyyy-MM-dd HH:mm:ss")
        End If
    End Sub

    Protected Sub ASPxCallback1_Callback(source As Object, e As DevExpress.Web.CallbackEventArgs)
        e.Result = "23123"

        Dim error_cls As er_custom = Session("error")
        If error_cls.er_hasil = "" Then
            e.Result = "123"
            Exit Sub
        End If

        str = Me.ASPxLabel5.Text & Chr(13) & Chr(10)
        str = str & Me.ASPxLabel2.Text & Chr(13) & Chr(10)
        str = str & Me.ASPxLabel3.Text & Chr(13) & Chr(10)
        str = str & Me.ASPxLabel4.Text & Chr(13) & Chr(10)
        str = str & Me.ASPxLabel1.Text & Chr(13) & Chr(10)

        If Mod_Utama.send_mail("system.noreply@actrans.co.id", "ismulyawan@agungcartrans.co.id", "MDS Error System", str) = False Then
            e.Result = "gagal"
        Else
            e.Result = "berhasil"
        End If
    End Sub

    Protected Sub SendMail_ServerClick(sender As Object, e As EventArgs)
        Dim error_cls As er_custom = Session("error")
        If error_cls.er_hasil = "" Then
            Mod_Utama.tampil_pesan(Me, "Tidak ada kesalahan yang dapat dikirim")
            Exit Sub
        End If

        str = Me.ASPxLabel5.Text & Chr(13) & Chr(10)
        str = str & Me.ASPxLabel2.Text & Chr(13) & Chr(10)
        str = str & Me.ASPxLabel3.Text & Chr(13) & Chr(10)
        str = str & Me.ASPxLabel4.Text & Chr(13) & Chr(10)
        str = str & Me.ASPxLabel1.Text & Chr(13) & Chr(10)

        If Mod_Utama.send_mail("system.noreply@actrans.co.id", "ismulyawan@agungcartrans.co.id", "MDS Error System", str) = False Then
            Mod_Utama.tampil_error(Me, "Email gagal dikirim, harap dicoba kembali")
        Else
            Mod_Utama.tampil_sukses(Me, "Email telah berhasil dikirim, terima kasih atas partisipasi anda")
        End If
    End Sub
End Class