<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="uc_chart.ascx.vb" Inherits="MDS.uc_chart" %>

<strong>Click Bottom Button For Refresh Chart</strong>
<br />
<a href="#" class="btn btn-primary" runat="server" id="a_bar" onserverclick="a_bar_ServerClick"><i class="icon-bars"></i>BAR</a>
<a href="#" class="btn btn-primary" runat="server" id="a_line" onserverclick="a_line_ServerClick"><i class="icon-stats"></i>LINE</a>
<a href="#" class="btn btn-primary" runat="server" id="a_pai" onserverclick="a_pai_ServerClick"><i class="icon-pie2"></i>PIE</a>
<a href="#" class="btn btn-primary" runat="server" id="a_turner" onserverclick="a_turner_ServerClick"><i class="icon-database"></i>FUNNEL</a>
<a href="#" class="btn btn-primary" runat="server" id="a_radar" onserverclick="a_radar_ServerClick"><i class="icon-brightness-high"></i>RADAR</a>
<a href="#" class="btn btn-primary" runat="server" id="a_donut" onserverclick="a_donut_ServerClick"><i class="icon-spinner3"></i>DOUGHNUT</a>
<a href="#" class="btn btn-primary" runat="server" id="a_spline" onserverclick="a_spline_ServerClick"><i class="icon-factory"></i>SPLINE</a>
<a href="#" class="btn btn-success" runat="server" id="a_label" onserverclick="a_label_ServerClick"><i class="icon-location3"></i>SHOW / HIDE LABEL</a>
<a href="#" class="btn btn-success" runat="server" id="a_legend" onserverclick="a_legend_ServerClick"><i class="icon-menu2"></i>SHOW / HIDE LEGEND</a>
<br />
<br />