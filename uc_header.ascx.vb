Imports DevExpress.Web
Imports DevExpress.Web.ASPxPivotGrid

Public Class uc_header
    Inherits System.Web.UI.UserControl

    Public idpage As Integer = 0
    Public grid As ASPxGridView = Nothing
    Public pivot As ASPxPivotGrid = Nothing

    Dim kalimatfilter As String = ""
    Dim str As String
    Dim dt As New DataTable

    Private Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        Me.div_max.InnerText = "Data hanya menampilkam maksimal " & Mod_Utama.rc_max & " RECORDS terakhir, gunakan -FILTER- untuk menampilkan seluruh data."
        Me.div_max.Visible = False

        If IsPostBack = False Then
            Dim cari As String = ""
            Try
                cari = Request.QueryString("cari")
            Catch ex As Exception
                cari = ""
            End Try

            If cari <> "" Then Me.tx_search.Value = cari
        End If

    End Sub

    Private Sub isi_help()
        str = "select * from opr_help "
        str = str & "where id_menu = " & Me.idpage
        Mod_Utama.isi_data_notime(Me.dt, str, "id_help")

        str = ""
        For Each dtr As DataRow In Me.dt.Rows
            str = str & dtr("desc") & "<br>"
        Next
        Me.lb_isi.InnerHtml = str
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If filter_cb1.Items.Count = 0 Then filter_tx1.Disabled = True
        If filter_cb2.Items.Count = 0 Then filter_tx2.Disabled = True
        If filter_cb3.Items.Count = 0 Then filter_de1.Enabled = False
        If filter_cb3.Items.Count = 0 Then filter_de2.Enabled = False

        If Not grid Is Nothing Then
            grid.ClientInstanceName = "grid"
            Me.ASPxGridViewExporter1.GridViewID = grid.ID
            grid.SettingsBehavior.EnableCustomizationWindow = True

            If grid.VisibleRowCount >= Mod_Utama.rc_max And kalimatfilter = "" Then
                div_max.Visible = True
            Else
                div_max.Visible = False
            End If

        Else
            Me.a_column.Visible = False
            Me.a_export.Visible = False
            Me.a_filter.Visible = False
            Me.div_search.Visible = False

        End If

        If Not pivot Is Nothing Then
            pivot.ClientInstanceName = "pivot"
            Me.ASPxPivotGridExporter1.ASPxPivotGridID = pivot.ID
        Else
            Me.pivot_export.Visible = False
            Me.a_field.Visible = False
        End If

        Me.isi_help()
    End Sub

    Private Sub bt_pdf_ServerClick(sender As Object, e As EventArgs) Handles bt_pdf.ServerClick
        If grid Is Nothing Then Exit Sub

        Me.ASPxGridViewExporter1.RightMargin = 1
        Me.ASPxGridViewExporter1.LeftMargin = 1
        Me.ASPxGridViewExporter1.WritePdfToResponse()
    End Sub

    Private Sub bt_excel_ServerClick(sender As Object, e As EventArgs) Handles bt_excel.ServerClick
        If grid Is Nothing Then Exit Sub

        Me.ASPxGridViewExporter1.WriteXlsToResponse()
    End Sub

    Public Sub bt_search_ServerClick(sender As Object, e As EventArgs) Handles bt_search.ServerClick
        If grid Is Nothing Then Exit Sub

        Dim searchString As String = Me.tx_search.Value

        If searchString = "" Then
            grid.FilterExpression = ""
        Else
            Dim fExpr As String = ""
            For Each column As GridViewColumn In grid.Columns
                If TypeOf column Is GridViewDataColumn Then
                    If fExpr = "" Then
                        fExpr = "[" & (TryCast(column, GridViewDataColumn)).FieldName & "] Like '%" & searchString & "%'"
                    Else
                        fExpr = fExpr & "OR " & "[" & (TryCast(column, GridViewDataColumn)).FieldName & "] Like '%" & searchString & "%'"
                    End If
                End If
            Next column

            grid.FilterExpression = fExpr
        End If

        Session("searchText") = searchString
    End Sub

    Public Structure filter_limit
        Public str_limit As String
        Public str_filter As String
    End Structure
    Public Function filtertext() As filter_limit
        Dim radioinfo As String = ""
        If IsPostBack = True Then
            filter_cb1.Text = Request.Form(filter_cb1.UniqueID)
            filter_tx1.Value = Request.Form(filter_tx1.UniqueID)

            filter_cb2.Text = Request.Form(filter_cb2.UniqueID)
            filter_tx2.Value = Request.Form(filter_tx2.UniqueID)

            filter_cb3.Text = Request.Form(filter_cb3.UniqueID)
            filter_de1.Value = Request.Form(filter_de1.UniqueID)
            filter_de2.Value = Request.Form(filter_de2.UniqueID)

            If Request.Form("radioinfo") = "radio_or" Then
                radioinfo = "OR "
            Else
                radioinfo = "AND "
            End If
        End If

        Dim hasil As filter_limit
        Dim filter As String = ""

        If filter_tx1.Value <> "" Then
            filter = filter & radioinfo & filter_cb1.SelectedValue & " like '%" & filter_tx1.Value & "%' "
        End If
        If filter_tx2.Value <> "" Then
            filter = filter & radioinfo & filter_cb2.SelectedValue & " like '%" & filter_tx2.Value & "%' "
        End If
        If filter_de1.Text <> "" Then
            'filter = filter & radioinfo & filter_cb3.SelectedValue & " >= '" & filter_de1.Value & "' "
            filter = filter_cb3.SelectedValue & " >= '" & filter_de1.Value & "' " & radioinfo & ""
        End If
        If filter_de2.Text <> "" Then
            ''filter = filter & radioinfo & filter_cb3.SelectedValue & " <= '" & filter_de2.Value & "' "
            filter = filter & filter_cb3.SelectedValue & " <= '" & filter_de2.Value & "' " & radioinfo & ""
        End If

        Dim limit As String = ""
        If filter = "" Then
            limit = " TOP " & Mod_Utama.rc_max
        Else
            a_filter.Attributes("style") = "background-color: #00FF00"
        End If

        hasil.str_limit = limit
        hasil.str_filter = filter
        kalimatfilter = filter

        Return hasil
    End Function

    Private Sub pivot_excel_ServerClick(sender As Object, e As EventArgs) Handles pivot_excel.ServerClick
        If pivot Is Nothing Then Exit Sub

        Me.ASPxPivotGridExporter1.ExportXlsToResponse("Actual Report")
    End Sub
    Private Sub pivot_pdf_ServerClick(sender As Object, e As EventArgs) Handles pivot_pdf.ServerClick
        If pivot Is Nothing Then Exit Sub

        Me.ASPxPivotGridExporter1.ExportPdfToResponse("Actual Report")
    End Sub

    'Private Sub ASPxGridViewExporter1_RenderBrick(sender As Object, e As DevExpress.Web.ASPxGridViewExportRenderingEventArgs) Handles ASPxGridViewExporter1.RenderBrick
    '    If e.RowType <> GridViewRowType.Data Then
    '        Return
    '    End If
    '    If e.RowType = GridViewRowType.Header Then
    '        Return
    '    End If


    '    'Dim gridrow As GridViewRow = TryCast(grid.GetRow(e.VisibleIndex), GridViewRow)
    '    Dim gridrow As GridViewRow = grid.
    '    e.BrickStyle.BackColor = gridrow.Cells(e.Column.Index).BackColor
    'End Sub
End Class