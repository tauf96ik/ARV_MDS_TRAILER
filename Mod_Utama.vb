Imports System.Data.SqlClient
Imports DevExpress.Web
Imports System.Net.Mail
Imports DevExpress.Web.ASPxPivotGrid

Module Mod_Utama

    Public Structure er_custom
        Public er_str As String
        Public er_hasil As String
        Public er_menu As String
        Public er_page As String
        Public er_tquery As Stopwatch
        Public er_tpage As Stopwatch
        Public er_id As Int64
        Public er_waktu As String
        Public er_sql As Integer
    End Structure

    Public Structure user_autho
        Public lihat As String
        Public baru As String
        Public ubah As String
        Public hapus As String
        Public spv As String
        Public mgr As String
        Public fin As String
        Public coo As String
        Public oth As String
        Public staf As String
    End Structure

    '---SERVER
    Public sql_str = "Data Source=192.168.0.18;Initial Catalog=GS;Persist Security Info=True;User ID=sa;Password=Pass53rv3r"
    Public sql_cms_arv = "Data Source=192.168.4.15;Initial Catalog=CMS_ART;Persist Security Info=True;User ID=sa;Password=Pass53rv3r"
    'Public sql_cms_arv = "Data Source=192.168.0.18;Initial Catalog=CMS_ART;Persist Security Info=True;User ID=sa;Password=Pass53rv3r"

    '---LOCAL
    'Public sql_str = "Data Source=LAPTOP-LO733MK3\SQLEXPRESS;Initial Catalog=GS_ARV;Persist Security Info=True;User ID=sa;Password=123456"

    Public rc_max As Integer = 1000
    Public rc_day As Integer = 7
    Public page_login As String = "http://agungcartrans.co.id:1025/"

    Private Property str As String

    Public Function isi_data(dt As DataTable, str As String, clmn_nm As String, sw As Stopwatch) As er_custom
        Dim sql_cont As New SqlConnection(Mod_Utama.sql_str)
        Dim sql_adpt As New SqlDataAdapter(str, sql_cont)
        Dim swtime As Integer = sw.Elapsed.TotalMilliseconds

        dt.Clear()
        Dim hasil As New er_custom
        hasil.er_str = str
        hasil.er_id = 0

        Try
            If Not sw Is Nothing Then sw.Start()
            sql_cont.Open()
            sql_adpt.Fill(dt)
        Catch ex As Exception
            hasil.er_hasil = ex.Message
        Finally
            sql_cont.Close()
            If Not sw Is Nothing Then sw.Stop()
        End Try

        If Not clmn_nm = "" Then
            dt.PrimaryKey = New DataColumn() {dt.Columns(clmn_nm)}
        End If

        HttpContext.Current.Session("time_query") &= dt.TableName & " <" & sw.Elapsed.TotalMilliseconds - swtime & ">" & Chr(13) & Chr(10)

        Return hasil
    End Function
    Public Function isi_data_notime(dt As DataTable, str As String, clmn_nm As String) As String
        Dim sql_cont As New SqlConnection(Mod_Utama.sql_str)
        Dim sql_adpt As New SqlDataAdapter(str, sql_cont)

        dt.Clear()
        Try
            sql_cont.Open()
            sql_adpt.Fill(dt)
        Catch ex As Exception
            Return ex.ToString
        Finally
            sql_cont.Close()
        End Try

        If Not clmn_nm = "" Then
            dt.PrimaryKey = New DataColumn() {dt.Columns(clmn_nm)}
        End If

        Return ""
    End Function
    Public Function isi_data_noclear(dt As DataTable, str As String, clmn_nm As String, sw As Stopwatch) As er_custom
        Dim sql_cont As New SqlConnection(Mod_Utama.sql_str)
        Dim sql_adpt As New SqlDataAdapter(str, sql_cont)

        Dim hasil As New er_custom
        hasil.er_str = str
        hasil.er_id = 0

        Try
            sw.Start()
            sql_cont.Open()
            sql_adpt.Fill(dt)
        Catch ex As Exception
            hasil.er_hasil = ex.Message
        Finally
            sql_cont.Close()
            sw.Stop()
        End Try

        If Not clmn_nm = "" Then
            dt.PrimaryKey = New DataColumn() {dt.Columns(clmn_nm)}
        End If

        Return hasil
    End Function
    Public Function exec_sql(str As String, Optional ByVal druser As DataRow = Nothing, Optional ByVal hal As String = "") As String
        Dim cont As New SqlConnection(Mod_Utama.sql_str)
        Dim cmnd As New SqlCommand(str, cont)

        Try
            cont.Open()
            cmnd.ExecuteScalar()
        Catch ex As Exception
            Return ex.Message
        Finally
            cont.Close()
        End Try

        If Not druser Is Nothing Then
            Mod_Utama.log_user(druser, "TMS", "Exec : " & str, hal)
        End If

        Return ""
    End Function
    Public Function exec_cms_arv(str As String, Optional ByVal druser As DataRow = Nothing, Optional ByVal hal As String = "") As String
        Dim cont As New SqlConnection(Mod_Utama.sql_cms_arv)
        Dim cmnd As New SqlCommand(str, cont)

        Try
            cont.Open()
            cmnd.ExecuteScalar()
        Catch ex As Exception
            Return ex.Message
        Finally
            cont.Close()
        End Try

        Return ""
    End Function
    Public Function exec_sql_id(str As String, Optional ByVal druser As DataRow = Nothing, Optional ByVal hal As String = "") As er_custom
        Dim cont As New SqlConnection(Mod_Utama.sql_str)
        Dim cmnd As New SqlCommand(str, cont)

        Dim hasil As New er_custom
        hasil.er_str = str
        hasil.er_id = 0

        Try
            cont.Open()
            hasil.er_id = cmnd.ExecuteScalar()
            hasil.er_hasil = ""
        Catch ex As Exception
            hasil.er_hasil = ex.ToString
            hasil.er_id = -1
            Return hasil
        Finally
            cont.Close()
        End Try

        If Not druser Is Nothing Then
            Mod_Utama.log_user(druser, "TMS", "Exec : " & str, hal)
        End If

        Return hasil
    End Function

    Public Function hasil_gambar(dr As DataRow) As String
        Try
            Dim bytes As Byte() = DirectCast(dr("photo"), Byte())
            Dim base64String As String = Convert.ToBase64String(bytes, 0, bytes.Length)
            Return Convert.ToString("data:image/png;base64,") & base64String
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Sub tampil_error(hal As Page, pesan As String)
        Dim js As String = "$.jGrowl('" & pesan & "', { theme: 'growl-error', header: 'Error !', life: 10000 });"
        hal.ClientScript.RegisterStartupScript(GetType(String), "tampil_error", js, True)
    End Sub
    Public Sub tampil_sukses(hal As Page, pesan As String)
        Dim js As String = "$.jGrowl('" & pesan & "', { theme: 'growl-success', header: 'Success !', life: 10000 });"
        hal.ClientScript.RegisterStartupScript(GetType(String), "tampil_sukses", js, True)
    End Sub
    Public Sub tampil_pesan(hal As Page, pesan As String)
        Dim js As String = "$.jGrowl('" & pesan & "', { header: 'Notification !', life: 10000 });"
        hal.ClientScript.RegisterStartupScript(GetType(String), "tampil_pesan", js, True)
    End Sub

    Public Function master_waktu(sw_query As Stopwatch, sw_page As Stopwatch, hal As UserControl) As String
        Dim str As String = ""

        Try
            str = " Q: <font color='red'><b>"
            str = str & String.Format("{0:00}:{1:00}.{2:000}", sw_query.Elapsed.TotalMinutes, _
                                sw_query.Elapsed.Seconds, _
                                 sw_query.Elapsed.Milliseconds)
            str = str & "</b></font> Sec "
            str = str & " | P: <font color='red'><b>"
            str = str & String.Format("{0:00}:{1:00}.{2:000}", sw_page.Elapsed.TotalMinutes, _
                                sw_page.Elapsed.Seconds, _
                                 sw_page.Elapsed.Milliseconds)
            str = str & "</b></font> Sec "
            str = "2016. MIS ACTrans Company [" & str & "]"
        Catch ex As Exception
            str = "[ No Time ]"
        End Try

        Dim pagehand As Page = HttpContext.Current.Handler
        If pagehand Is Nothing Then GoTo SKIP
        If pagehand.IsPostBack = True Or pagehand.IsCallback = True Then GoTo SKIP
        If sw_query.Elapsed.Seconds > 4 Or sw_page.Elapsed.Seconds > 6 Then
            Dim strqry As String = ""
            Dim druser As DataRow = HttpContext.Current.Session("dr_user")
            strqry = "insert into log_query ("
            strqry = strqry & "waktu, web_nm, page_nm, query_scnd, page_scnd, "
            strqry = strqry & "c_user) "
            strqry = strqry & "values ("
            strqry = strqry & "getdate(), "
            strqry = strqry & "'TMS', "
            strqry = strqry & "'" & HttpContext.Current.Request.FilePath & "', "
            strqry = strqry & "'" & sw_query.Elapsed.Seconds & "', "
            strqry = strqry & "'" & sw_page.Elapsed.Seconds & "', "
            strqry = strqry & "'" & druser("nama") & "') "
            Dim hasil As String = exec_sql(strqry)
        End If

SKIP:

        Dim mpLabel As System.Web.UI.HtmlControls.HtmlGenericControl
        mpLabel = CType(hal.FindControl("lb_time"), System.Web.UI.HtmlControls.HtmlGenericControl)
        If Not mpLabel Is Nothing Then
            mpLabel.InnerHtml = str
        End If

        HttpContext.Current.Session("waktu_query") = sw_query
        HttpContext.Current.Session("waktu_page") = sw_page

        Return str
    End Function
    Public Function str_waktu(sw_query As Stopwatch, sw_page As Stopwatch) As String
        Dim str As String = ""

        Try
            str = " Query Time : "
            str = str & String.Format("{0:00}:{1:00}.{2:000}", sw_query.Elapsed.TotalMinutes, _
                                sw_query.Elapsed.Seconds, _
                                 sw_query.Elapsed.Milliseconds)
            str = str & " Seconds "
            str = str & " // Page Time : "
            str = str & String.Format("{0:00}:{1:00}.{2:000}", sw_page.Elapsed.TotalMinutes, _
                                sw_page.Elapsed.Seconds, _
                                 sw_page.Elapsed.Milliseconds)
            str = str & " Seconds "
            str = "[" & str & "]"
        Catch ex As Exception
            'str = "[ No Time ]"
            str = ex.ToString
        End Try

        Return str
    End Function

    Public Function send_mail(from_add As String, to_add As String, subject As String, body As String) As Boolean
        Dim mail As New System.Net.Mail.MailMessage
        Dim smtpClient As New System.Net.Mail.SmtpClient("mail.agungcartrans.co.id")

        Try
            mail.To.Add(to_add)
            mail.From = New Net.Mail.MailAddress(from_add)
            mail.Subject = subject
            mail.Body = body

            smtpClient.Send(mail)
        Catch ex As Exception
            'MsgBox(ex.ToString)
            Return False
        End Try

        Return True
    End Function

    Public Sub Atur_Grid(grid As ASPxGridView, Optional usecustomcallback As Boolean = False)
        If usecustomcallback = True Then
            grid.ClientSideEvents.CustomButtonClick = "function (s, e) { e.processOnServer = false; s.PerformCallback(e.buttonID+';'+s.GetRowKey(e.visibleIndex)); }"
            grid.ClientSideEvents.EndCallback = "function (s, e) { tampil_pesan(s.cpTitle, s.cpContent); delete s.cpTitle; }"
        End If

        grid.Width = 100

        grid.StylesEditors.ReadOnly.BackColor = Drawing.Color.LightPink
        grid.Styles.Header.HorizontalAlign = HorizontalAlign.Center
        grid.Styles.Header.Wrap = DevExpress.Utils.DefaultBoolean.True
        grid.Styles.Header.Paddings.PaddingLeft = 1
        grid.Styles.Header.Paddings.PaddingRight = 1
        grid.Styles.Header.Paddings.PaddingTop = 4
        grid.Styles.Header.Paddings.PaddingBottom = 4

        grid.SettingsAdaptivity.AdaptiveColumnPosition = GridViewAdaptiveColumnPosition.Left
        grid.Settings.ShowGroupPanel = True
        grid.Settings.ShowPreview = True
        grid.Settings.ShowFooter = True
        grid.Settings.ShowFilterRow = True
        grid.Settings.ShowHeaderFilterButton = True
        grid.Settings.ShowHeaderFilterBlankItems = True

        grid.SettingsText.ConfirmDelete = "Yakin untuk hapus record ini ?"
        grid.SettingsBehavior.ConfirmDelete = True

        grid.SettingsResizing.ColumnResizeMode = ColumnResizeMode.Control

        grid.SettingsPager.Visible = True
        grid.SettingsPager.PageSizeItemSettings.ShowAllItem = True
        grid.SettingsPager.PageSizeItemSettings.Visible = True
        grid.SettingsPager.Position = PagerPosition.TopAndBottom
        grid.SettingsPager.EnableAdaptivity = True
        grid.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Left

        grid.SettingsSearchPanel.Visible = True
        grid.Styles.SearchPanel.Width = 400
        grid.Styles.SearchPanel.HorizontalAlign = HorizontalAlign.Left

        grid.StylesPopup.EditForm.Header.BackColor = Drawing.Color.LightBlue
        grid.StylesPopup.EditForm.Header.ForeColor = Drawing.Color.Gray
        grid.StylesPopup.EditForm.MainArea.BackColor = Drawing.Color.WhiteSmoke

        'popup edit form
        grid.SettingsEditing.Mode = GridViewEditingMode.PopupEditForm
        grid.SettingsPopup.EditForm.HorizontalAlign = PopupHorizontalAlign.WindowCenter
        grid.SettingsPopup.EditForm.VerticalAlign = PopupVerticalAlign.WindowCenter
        grid.SettingsResizing.ColumnResizeMode = ColumnResizeMode.Control
        grid.Styles.Row.Height = 20
        grid.Width = Unit.Percentage(100)
        grid.SettingsEditing.Mode = GridViewEditingMode.PopupEditForm
        grid.SettingsPopup.EditForm.Height = 500
        grid.SettingsPopup.EditForm.Width = 1000
        grid.SettingsPopup.EditForm.HorizontalAlign = PopupHorizontalAlign.WindowCenter
        grid.SettingsPopup.EditForm.VerticalAlign = PopupVerticalAlign.WindowCenter
        grid.SettingsPopup.EditForm.Modal = True
        grid.Border.BorderStyle = WebControls.BorderStyle.Solid

        grid.Styles.AlternatingRow.BackColor = System.Drawing.Color.FromName("FFFFFF")
        AddHandler grid.HtmlRowPrepared, AddressOf Grid_HtmlRowPrepared

        Dim lebar As Integer = 0
        Dim clm As GridViewDataColumn
        For i = 0 To grid.Columns.Count - 1
            If grid.Columns(i).Visible = True Then
                lebar += grid.Columns(i).Width.Value
            End If

            Select Case grid.Columns(i).GetType
                Case GetType(GridViewCommandColumn)
                    Dim cmd As GridViewCommandColumn = grid.Columns(i)
                    cmd.ShowClearFilterButton = True
                    'cmd.ClearFilterButton.Visible = True
            End Select

            Try
                clm = grid.Columns(i)
            Catch ex As Exception
                GoTo SKIP
            End Try

            clm = grid.Columns(i)
            If clm.GetType = GetType(GridViewDataHyperLinkColumn) Then
                clm.Settings.FilterMode = ColumnFilterMode.DisplayText
                clm.Settings.AutoFilterCondition = AutoFilterCondition.Contains
            Else
                clm.Settings.FilterMode = ColumnFilterMode.Value
            End If

            clm.Settings.HeaderFilterMode = HeaderFilterMode.CheckedList
            clm.Settings.AutoFilterCondition = AutoFilterCondition.Contains
            clm.Settings.SortMode = DevExpress.XtraGrid.ColumnSortMode.DisplayText
            clm.HeaderStyle.Wrap = DevExpress.Utils.DefaultBoolean.True

SKIP:
        Next
        grid.Width = Unit.Percentage(100)
    End Sub
    Private Sub Grid_HtmlRowPrepared(sender As Object, e As ASPxGridViewTableRowEventArgs)
        Select Case e.RowType
            Case GridViewRowType.Preview
                e.Row.BackColor = HttpContext.Current.Session("warna")
            Case GridViewRowType.Data, GridViewRowType.Detail
                If Trim(e.Row.BackColor.ToString).Replace(" ", "") = "Color[Empty]" Then
                    HttpContext.Current.Session("warna") = System.Drawing.Color.White
                Else
                    HttpContext.Current.Session("warna") = e.Row.BackColor
                End If
        End Select
    End Sub
    Public Sub Atur_pivot(pivot As ASPxPivotGrid)
        pivot.OptionsPager.AllButton.Visible = True
        pivot.OptionsPager.ShowSeparators = True
        pivot.OptionsPager.PageSizeItemSettings.Visible = True
        pivot.OptionsPager.PageSizeItemSettings.Position = DevExpress.Web.PagerPageSizePosition.Right
        pivot.OptionsChartDataSource.MaxAllowedSeriesCount = 100
        pivot.OptionsView.DataHeadersDisplayMode = PivotDataHeadersDisplayMode.Popup
        pivot.OptionsChartDataSource.CurrentPageOnly = False
        'pivot.OptionsPager.RenderMode = DevExpress.Web.ControlRenderMode.Lightweight

        For i = 0 To pivot.Fields.Count - 1
            Select Case pivot.Fields(i).DataType
                Case GetType(Decimal)
                    pivot.Fields(i).CellFormat.FormatType = DevExpress.Utils.FormatType.Custom
                    pivot.Fields(i).CellFormat.FormatString = "#,###"
                Case GetType(Date)
                    pivot.Fields(i).ValueFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                    pivot.Fields(i).ValueFormat.FormatString = "yyyy-MM-dd"
            End Select
        Next
    End Sub
    Public Sub clear_pivot(pivot As ASPxPivotGrid)
        For i = 0 To pivot.Fields.Count - 1
            pivot.Fields(i).Area = DevExpress.XtraPivotGrid.PivotArea.FilterArea
        Next
    End Sub

    Public Function to_null(nilai) As String
        If nilai Is Nothing Then Return "NULL"
        If nilai Is DBNull.Value Then Return "NULL"
        If IsDBNull(nilai) Then Return "NULL"
        If nilai.ToString = "" Then Return "NULL"
        If nilai.ToString = "0" Then Return "NULL"

        Return nilai.ToString
    End Function

    Public Function log_user(druser As DataRow, web As String, ket As String, hal As String) As String
        Dim ket_str As String = Replace(ket, "'", "''")

        str = "insert into log_user ("
        str = str & "waktu, id_user, nm_user, web, module, ket) "
        str = str & "values ("
        str = str & "getdate(), "
        str = str & "'" & druser("id_user") & "', "
        str = str & "'" & druser("nama") & "', "
        str = str & "'" & web & "', "
        str = str & "'" & hal & "', "
        str = str & "'" & ket_str & "') "

        Dim cont As New SqlConnection(Mod_Utama.sql_str)
        Dim cmnd As New SqlCommand(str, cont)

        Try
            cont.Open()
            cmnd.ExecuteScalar()
        Catch ex As Exception
            Return ex.Message
        Finally
            cont.Close()
        End Try

        Return ""
    End Function

    Public Sub insert_error(msg As String, strquery As String)
        Dim ket_str As String = Replace(strquery, "'", "''")

        str = "insert into opr_logerror ("
        str = str & "tgl, msg, str, hal) "
        str = str & "values ("
        str = str & "getdate(), "
        str = str & "'" & msg & "', "
        str = str & "'" & ket_str & "', "
        str = str & "'" & HttpContext.Current.ToString & "') "

        Dim cont As New SqlConnection(Mod_Utama.sql_str)
        Dim cmnd As New SqlCommand(str, cont)

        Try
            cont.Open()
            cmnd.ExecuteScalar()
        Catch ex As Exception
            MsgBox(str)
        Finally
            cont.Close()
        End Try
    End Sub

    Function sql_tran_branch() As String
        Throw New NotImplementedException
    End Function

    Public Function dttbl_xml(dtt As DataTable) As String
        dtt.TableName = "teat"
        Dim xmlstr As New IO.StringWriter
        dtt.WriteXml(xmlstr)
        Return xmlstr.ToString
    End Function

    Public Function log_delete(sumbersql As String, tblnm As String, druser As DataRow) As String
        Dim method = New StackTrace().GetFrame(1).GetMethod().Name
        Dim str As String = ""
        Dim dtemp As New DataTable
        str = sumbersql
        Dim swload As New Stopwatch

        Mod_Utama.isi_data(dtemp, str, "", swload)
        Dim hasil As String = ""
        hasil = Mod_Utama.dttbl_xml(dtemp)

        str = "insert into log_delete "
        str = str & "(xml, table_nm, web, module, page, c_date, c_user) values ("
        str = str & "'" & hasil & "', "
        str = str & "'" & tblnm & "', "
        str = str & "'MDS-ACT', "
        str = str & "'" & method.ToString & "', "
        str = str & "'" & HttpContext.Current.CurrentHandler.ToString & "', "
        str = str & "getdate(), "
        str = str & "'" & druser("nama") & "') "

        Dim cont As New SqlConnection(Mod_Utama.sql_str)
        Dim cmnd As New SqlCommand(str, cont)

        Try
            cont.Open()
            cmnd.ExecuteScalar()
        Catch ex As Exception
            'MsgBox(ex.ToString)
            Return ex.Message
        Finally
            cont.Close()
        End Try

        Return ""
    End Function

End Module
