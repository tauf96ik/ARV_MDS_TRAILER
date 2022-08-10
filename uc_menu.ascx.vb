Public Class uc_menu
    Inherits System.Web.UI.UserControl

    Dim dr_user As DataRow

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        dr_user = Session("dr_user")

        'Auth
        If CStr(dr_user("lihat")).Contains(",1,") = False Then a_autho_grp.Attributes("class") = "disabled"
        If CStr(dr_user("lihat")).Contains(",2,") = False Then a_autho_user.Attributes("class") = "disabled"

        'Master
        If CStr(dr_user("lihat")).Contains(",6,") = False Then a_mst_apd.Attributes("class") = "disabled"
        If CStr(dr_user("lihat")).Contains(",7,") = False Then a_mst_ba.Attributes("class") = "disabled"
        If CStr(dr_user("lihat")).Contains(",9,") = False Then a_mst_izin_jns.Attributes("class") = "disabled"
        If CStr(dr_user("lihat")).Contains(",11,") = False Then a_mst_kateg.Attributes("class") = "disabled"
        If CStr(dr_user("lihat")).Contains(",10,") = False Then a_mst_limit.Attributes("class") = "disabled"
        If CStr(dr_user("lihat")).Contains(",13,") = False Then a_mst_templ.Attributes("class") = "disabled"
        If CStr(dr_user("lihat")).Contains(",8,") = False Then a_mst_training.Attributes("class") = "disabled"
        If CStr(dr_user("lihat")).Contains(",8,") = False Then a_mst_rtt.Attributes("class") = "disabled"

        'Existing
        If CStr(dr_user("lihat")).Contains(",3,") = False Then a_driver_act.Attributes("class") = "disabled"
        If CStr(dr_user("lihat")).Contains(",4,") = False Then a_driver_inact.Attributes("class") = "disabled"
        If CStr(dr_user("lihat")).Contains(",19,") = False Then a_trs_ba.Attributes("class") = "disabled"
        If CStr(dr_user("lihat")).Contains(",20,") = False Then a_trs_acc_inc.Attributes("class") = "disabled"
        If CStr(dr_user("lihat")).Contains(",22,") = False Then a_trs_apd.Attributes("class") = "disabled"
        If CStr(dr_user("lihat")).Contains(",23,") = False Then a_trs_tegur.Attributes("class") = "disabled"
        If CStr(dr_user("lihat")).Contains(",24,") = False Then a_trs_izin.Attributes("class") = "disabled"
        If CStr(dr_user("lihat")).Contains(",25,") = False Then a_trs_mcu.Attributes("class") = "disabled"
        If CStr(dr_user("lihat")).Contains(",26,") = False Then a_trs_training.Attributes("class") = "disabled"
        If CStr(dr_user("lihat")).Contains(",27,") = False Then a_skontrak.Attributes("class") = "disabled"

        'Monitoring
        If CStr(dr_user("lihat")).Contains(",33,") = False Then a_mon_supir.Attributes("class") = "disabled"
        If CStr(dr_user("lihat")).Contains(",38,") = False Then a_mon_pass.Attributes("class") = "disabled"

        'Laporan
        If CStr(dr_user("lihat")).Contains(",28,") = False Then a_lap_training.Attributes("class") = "disabled"
        If CStr(dr_user("lihat")).Contains(",29,") = False Then a_lap_izin.Attributes("class") = "disabled"
        If CStr(dr_user("lihat")).Contains(",30,") = False Then a_lap_mcu.Attributes("class") = "disabled"
        If CStr(dr_user("lihat")).Contains(",31,") = False Then a_lap_apd.Attributes("class") = "disabled"
        If CStr(dr_user("lihat")).Contains(",34,") = False Then a_lap_accident.Attributes("class") = "disabled"
        If CStr(dr_user("lihat")).Contains(",35,") = False Then a_lap_ba.Attributes("class") = "disabled"
        If CStr(dr_user("lihat")).Contains(",36,") = False Then a_lap_sj.Attributes("class") = "disabled"
        If CStr(dr_user("lihat")).Contains(",37,") = False Then a_lap_teg.Attributes("class") = "disabled"
    End Sub

End Class