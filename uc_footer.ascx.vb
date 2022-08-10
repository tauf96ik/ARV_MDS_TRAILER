Public Class uc_footer
    Inherits System.Web.UI.UserControl

    Dim str As String
    Dim salah As er_custom

    Private Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        If HttpContext.Current.Request.FilePath <> "/page_error.aspx" Then HttpContext.Current.Session("time_query") = ""
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

End Class