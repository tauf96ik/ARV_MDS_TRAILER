<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="mds_lap_accinc.aspx.vb" Inherits="MDS.mds_lap_absen" %>

<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Src="~/uc_header.ascx" TagPrefix="uc1" TagName="uc_header" %>
<%@ Register Src="~/uc_footer.ascx" TagPrefix="uc1" TagName="uc_footer" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:uc_header runat="server" ID="uc_header" />
    <div class="form-group">
        <label>Periode Date : </label>
        <div class="row">
            <div class="col-sm-2">
                <dx:ASPxDateEdit ID="s_date" runat="server" EnableTheming="False" Height="20px" DisplayFormatString="yyyy-MM-dd" EditFormat="Custom" EditFormatString="yyyy-MM-dd">
                    <CalendarProperties Columns="3">
                    </CalendarProperties>
                    <ButtonStyle BackColor="Transparent">
                    </ButtonStyle>
                    <Paddings Padding="6px" />
                    <Border BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
                </dx:ASPxDateEdit>
            </div>
            <div class="col-sm-2">
                <dx:ASPxDateEdit ID="e_date" runat="server" EnableTheming="False" Height="20px" DisplayFormatString="yyyy-MM-dd" EditFormat="Custom" EditFormatString="yyyy-MM-dd">
                    <CalendarProperties Columns="3">
                    </CalendarProperties>
                    <ButtonStyle BackColor="Transparent">
                    </ButtonStyle>
                    <Paddings Padding="6px" />
                    <Border BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
                </dx:ASPxDateEdit>
            </div>
            <div class="col-sm-2">
                <button class="btn btn-success" type="button" runat="server" id="bt_refresh" onserverclick="bt_refresh_ServerClick"><i class="icon-search3"></i>Filter</button>
            </div>
        </div>
    </div>
    <dx:ASPxPivotGrid ID="ASPxPivotGrid1" runat="server" ClientIDMode="AutoID">
        <Fields>
            <dx:PivotGridField ID="fieldsupir" Area="RowArea" AreaIndex="0" Caption="Nama Supir" FieldName="supir" Options-AllowExpand="False" ExpandedInFieldsGroup="False" Options-ShowCustomTotals="False" Options-ShowGrandTotal="False" Options-ShowInCustomizationForm="False" Options-ShowInExpressionEditor="False" Options-ShowInFilter="False" Options-ShowInPrefilter="False" Options-ShowTotals="False">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldqty" Area="DataArea" AreaIndex="0" Caption="Qty" FieldName="qty">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldthnbln" Area="ColumnArea" AreaIndex="0" Caption="Tahun Bulan" FieldName="thnbln">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldcause" Area="FilterArea" AreaIndex="0" Caption="Cause Analysis" FieldName="cause_analis">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldba" Area="RowArea" AreaIndex="4" Caption="Jenis" FieldName="jenis">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldcuaca" Area="RowArea" AreaIndex="3" Caption="Cuaca" FieldName="cuaca"  Options-ShowTotals="False" Options-AllowExpand="False" ExpandedInFieldsGroup="False">
            </dx:PivotGridField>
            <dx:PivotGridField ID="PivotGridField1" Area="RowArea" AreaIndex="2" Caption="Status" FieldName="status"  Options-ShowTotals="False" Options-AllowExpand="False" ExpandedInFieldsGroup="False">
            </dx:PivotGridField>
            <dx:PivotGridField ID="PivotGridField2" Area="RowArea" AreaIndex="1" Caption="Lokasi" FieldName="lokasi"  Options-ShowTotals="False" Options-AllowExpand="False" ExpandedInFieldsGroup="False">
            </dx:PivotGridField>
            <dx:PivotGridField ID="field" AreaIndex="0" Caption="Tgl. Kejadian" FieldName="tgl_kejadian"  Options-ShowTotals="False" Options-AllowExpand="False" ExpandedInFieldsGroup="False">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldnoba" AreaIndex="1" Caption="No. Trs" FieldName="no_trs"  Options-ShowTotals="False" Options-AllowExpand="False" ExpandedInFieldsGroup="False">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldnopol" AreaIndex="2" Caption="No. Polisi" FieldName="nopol"  Options-ShowTotals="False" Options-AllowExpand="False" ExpandedInFieldsGroup="False">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldjam" AreaIndex="3" Caption="Jam Kejadian" FieldName="jam" ValueFormat-FormatString="HH:mm"  Options-ShowTotals="False" Options-AllowExpand="False" ExpandedInFieldsGroup="False">
            </dx:PivotGridField>
        </Fields>
    </dx:ASPxPivotGrid>
    <uc1:uc_footer runat="server" ID="uc_footer" />
</asp:Content>
