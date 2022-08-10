Public Class page_nomaster
    Inherits System.Web.UI.Page

    Dim salah As er_custom

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.ASPxLabel1.Text = Session("login")
        a_login.HRef = Mod_Utama.page_login
    End Sub

End Class