Public Class uc_chart
    Inherits System.Web.UI.UserControl

    Public chart As DevExpress.XtraCharts.Web.WebChartControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        chart.SeriesTemplate.Label.PointOptions.ValueNumericOptions.Format = DevExpress.XtraCharts.NumericFormat.Number
        chart.SeriesTemplate.Label.PointOptions.ValueNumericOptions.Precision = 1
    End Sub

    Protected Sub a_bar_ServerClick(sender As Object, e As EventArgs)
        chart.SeriesTemplate.View = New DevExpress.XtraCharts.SideBySideBarSeriesView
    End Sub
    Protected Sub a_line_ServerClick(sender As Object, e As EventArgs)
        chart.SeriesTemplate.View = New DevExpress.XtraCharts.LineSeriesView
    End Sub
    Protected Sub a_pai_ServerClick(sender As Object, e As EventArgs)
        chart.SeriesTemplate.View = New DevExpress.XtraCharts.Pie3DSeriesView
        chart.SeriesTemplate.Label.PointOptions.ValueNumericOptions.Format = DevExpress.XtraCharts.NumericFormat.Percent
        chart.SeriesTemplate.Label.PointOptions.ValueNumericOptions.Precision = 1
    End Sub
    Protected Sub a_turner_ServerClick(sender As Object, e As EventArgs)
        chart.SeriesTemplate.View = New DevExpress.XtraCharts.Funnel3DSeriesView
    End Sub
    Protected Sub a_label_ServerClick(sender As Object, e As EventArgs)
        If chart.SeriesTemplate.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True Then
            chart.SeriesTemplate.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False
        Else
            chart.SeriesTemplate.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True
        End If
    End Sub
    Protected Sub a_legend_ServerClick(sender As Object, e As EventArgs)
        If chart.Legend.Visible = True Then
            chart.Legend.Visible = False
        Else
            chart.Legend.Visible = True
        End If
    End Sub
    Protected Sub a_radar_ServerClick(sender As Object, e As EventArgs)
        chart.SeriesTemplate.View = New DevExpress.XtraCharts.RadarAreaSeriesView
    End Sub
    Protected Sub a_donut_ServerClick(sender As Object, e As EventArgs)
        chart.SeriesTemplate.View = New DevExpress.XtraCharts.Doughnut3DSeriesView
        chart.SeriesTemplate.Label.PointOptions.ValueNumericOptions.Format = DevExpress.XtraCharts.NumericFormat.Percent
        chart.SeriesTemplate.Label.PointOptions.ValueNumericOptions.Precision = 1
    End Sub
    Protected Sub a_spline_ServerClick(sender As Object, e As EventArgs)
        chart.SeriesTemplate.View = New DevExpress.XtraCharts.SplineAreaSeriesView
    End Sub

End Class