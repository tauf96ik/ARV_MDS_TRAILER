<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="mds_lap_sj.aspx.vb" Inherits="MDS.mds_lap_sj" %>

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
                <button class="btn btn-success" type="button" runat="server" id="bt_refresh" onserverclick="bt_refresh_ServerClick"><i class="icon-search3"></i>Resfresh Data</button>
            </div>
        </div>
    </div>
    <dx:ASPxPivotGrid ID="ASPxPivotGrid1" runat="server" ClientIDMode="AutoID">
        <Fields>
            <dx:PivotGridField ID="fieldsupir" FieldName="supir1" Caption="Supir 1" Area="RowArea" AreaIndex="1">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldsumber" FieldName="sumber" Caption="Sumber SJ" Area="RowArea" AreaIndex="2">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldqty" Area="DataArea" AreaIndex="0" Caption="Qty" FieldName="qty" TotalCellFormat-FormatType="Numeric" TotalValueFormat-FormatType="Numeric" Options-ShowGrandTotal="False">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldnosj" Caption="No. Surat Jalan" FieldName="no_sj" Area="FilterArea" AreaIndex="1">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldtgl" FieldName="tgl" Caption="Tanggal" Area="FilterArea" AreaIndex="2" ValueFormat-FormatString="yyyy-MM-dd">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldtrayek" Caption="Trayek" FieldName="trayek" Area="FilterArea" AreaIndex="2">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldasal" Caption="Asal" FieldName="asal" Area="FilterArea" AreaIndex="3">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldtujuan" Caption="Tujuan" FieldName="tujuan" Area="FilterArea" AreaIndex="4">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldcust" Caption="Customer" FieldName="customer_nm" Area="FilterArea" AreaIndex="5">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldnopol" Caption="No Polisi" FieldName="nopol" Area="FilterArea" AreaIndex="6">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldharga" Caption="Harga Jual" FieldName="harga_jual" Area="FilterArea" AreaIndex="7">
            </dx:PivotGridField>
        </Fields>
    </dx:ASPxPivotGrid>
    <uc1:uc_footer runat="server" ID="uc_footer" />
</asp:Content>
