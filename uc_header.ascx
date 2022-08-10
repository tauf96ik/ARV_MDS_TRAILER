<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="uc_header.ascx.vb" Inherits="MDS.uc_header" %>
<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>


<div class="page-header">
    <div class="breadcrumb-line breadcrumb-top">
        <ul class="breadcrumb" runat="server" id="list_menu">
            <li><a href="home.aspx">Home</a></li>
        </ul>

        <div class="visible-xs breadcrumb-toggle"><a class="btn btn-link btn-lg btn-icon" data-toggle="collapse" data-target="#breadcrumb-buttons2"><i class="icon-menu2"></i></a></div>
        <ul class="breadcrumb-buttons collapse" id="breadcrumb-buttons2">

            <li class="dropdown">
                <a class="dropdown-toggle" data-toggle="collapse" data-target="#togglegroup2" runat="server" id="a_filter">
                    <i class="icon-search3"></i><span>Filter</span>
                </a>
            </li>
            <li class="dropdown">
                <a id="a_column" onclick="custom()" runat="server">
                    <i class="icon-table2"></i><span>Columns</span>
                </a>
            </li>
            <li class="dropdown">
                <a id="a_field" onclick="field()" runat="server">
                    <i class="icon-table2"></i><span>Fields</span>
                </a>
            </li>
            <li class="dropdown">
                <a class="dropdown-toggle" data-toggle="dropdown" runat="server" id="a_export">
                    <span>Exports</span><i class="icon-paragraph-justify"></i>
                </a>
                <ul class="dropdown-menu dropdown-menu-right icons-right">
                    <li><a href="#" runat="server" id="bt_pdf"><i class="icon-file-pdf"></i>Pdf</a></li>
                    <li><a href="#" runat="server" id="bt_excel"><i class="icon-file-excel"></i>Excel</a></li>
                </ul>
            </li>
            <li class="dropdown">
                <a class="dropdown-toggle" data-toggle="dropdown" runat="server" id="pivot_export">
                    <span>Exports Pivot</span><i class="icon-paragraph-justify"></i>
                </a>
                <ul class="dropdown-menu dropdown-menu-right icons-right">
                    <li><a href="#" runat="server" id="pivot_pdf"><i class="icon-file-pdf"></i>Pdf</a></li>
                    <li><a href="#" runat="server" id="pivot_excel"><i class="icon-file-excel"></i>Excel</a></li>
                </ul>
            </li>
            <li class="dropdown">
                <a class="dropdown-toggle" data-toggle="modal" role="button" href="#large_modal">
                    <span>Help</span><i class="icon-question4"></i>
                </a>
            </li>
        </ul>
    </div>
</div>

<%--RULE--%>
<div id="large_modal" class="modal fade" tabindex="-1" role="dialog">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title"><i class="icon-question4"></i>Help</h4>
            </div>
            <div class="modal-body with-padding">
                <p runat="server" id="lb_isi">No Details Found ...</p>
            </div>
            <div class="modal-footer">
                <button class="btn btn-warning" data-dismiss="modal">Close</button>
            </div>
        </div>
    </div>
</div>

<%--PANEL FILTER--%>
<div id="togglegroup2" class="panel-collapse collapse" style="background-image: url('../img/bg.jpg');">
    <div class="panel-body">
        <div class="form-group">
            <div class="col-sm-10">
                <div class="row">
                    <div class="col-sm-3">
                        <b>Filter Column 1 :</b>
                        <asp:DropDownList ID="filter_cb1" runat="server" CssClass="select">
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-3">
                        <b>Containt :</b>
                        <input type="text" class="form-control" runat="server" id="filter_tx1" name="filter_tx1">
                    </div>
                    <div class="col-sm-3">
                        <b>Filter Column 2 :</b>
                        <asp:DropDownList ID="filter_cb2" runat="server" CssClass="select">
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-3">
                        <b>Containt :</b>
                        <input type="text" class="form-control" runat="server" id="filter_tx2">
                    </div>
                    <div class="col-sm-3">
                    </div>
                </div>
            </div>
        </div>


        <div class="form-group">
            <%--<label class="col-sm-2 control-label">And</label>--%>

            <div class="block-inner">
                <label class="radio-inline radio-info">
                    <input type="radio" name="radioinfo" class="styled" value="radio_or">
                    <b>Or</b></label>
                <label class="radio-inline radio-info">
                    <input type="radio" name="radioinfo" class="styled" value="radio_and">
                    <b>And</b></label>
            </div>


            <div class="col-sm-10">
                <div class="row">
                    <div class="col-sm-3">
                        <b>Filter Date :</b>
                        <asp:DropDownList ID="filter_cb3" runat="server" CssClass="select">
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-3">
                        <b>From :</b>
                        <dx:ASPxDateEdit ID="filter_de1" runat="server" EnableTheming="False" Height="20px" DisplayFormatString="yyyy-MM-dd" EditFormat="Custom" EditFormatString="yyyy-MM-dd">
                            <ButtonStyle BackColor="Transparent">
                                <Border BorderStyle="None" />
                            </ButtonStyle>
                            <Paddings Padding="6px" />
                            <Border BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
                        </dx:ASPxDateEdit>
                    </div>
                    <div class="col-sm-3">
                        <b>To :</b>
                        <dx:ASPxDateEdit ID="filter_de2" runat="server" EnableTheming="False" Height="20px" DisplayFormatString="yyyy-MM-dd" EditFormat="Custom" EditFormatString="yyyy-MM-dd">
                            <ButtonStyle BackColor="Transparent">
                                <Border BorderStyle="None" />
                            </ButtonStyle>
                            <Paddings Padding="6px" />
                            <Border BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
                        </dx:ASPxDateEdit>
                    </div>
                    <div class="col-sm-3">
                    </div>
                </div>
            </div>
            <ul>
                <li style="color: #FFFFFF"></li>
                <li style="color: #FFFFFF">
                    <button class="btn btn-success" type="submit"><i class="icon-search3"></i>View</button>
                </li>
            </ul>
        </div>
    </div>
</div>

<dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server"></dx:ASPxGridViewExporter>
<dx:ASPxPivotGridExporter ID="ASPxPivotGridExporter1" runat="server"></dx:ASPxPivotGridExporter>

<%--MENU SEARCH--%>
<div class="bg-danger with-padding" runat="server" id="div_max">Data hanya menampilkam maksimal Records, gunakan Filter untuk menampilkan seluruh data.</div>
<br />
<div class="form-group" runat="server" id="div_search">
    <div class="row">
        <div class="col-md-4">
            <input type="text" class="form-control" runat="server" id="tx_search" onkeydown="searchKeyPress(event);">
        </div>
        <div class="col-md-8">
            <button class="btn btn-info" type="button" runat="server" id="bt_search"><i class="icon-search3"></i>Search</button>
        </div>
    </div>
</div>

<script type="text/javascript">
    function baru() {
        grid.AddNewRow();
    }
    function custom() {
        if (grid.IsCustomizationWindowVisible())
            grid.HideCustomizationWindow();
        else
            grid.ShowCustomizationWindow();
    }
    function field() {
        pivot.ChangeCustomizationFieldsVisibility();
    }
    function searchKeyPress(e) {
        if (e.keyCode == 13) {
            var mensagem = document.getElementById('<%=bt_search.ClientID%>');
            mensagem.click();
        }
    }
</script>
