<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="mds_lap_izin.aspx.vb" Inherits="MDS.mds_lap_izin" %>

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
            <dx:PivotGridField ID="fielddriver" Area="RowArea" AreaIndex="1" Caption="Driver" FieldName="supir" Options-ShowCustomTotals="True">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldjenis" Area="RowArea" AreaIndex="2" Caption="Jenis Izin" FieldName="jenis_izin">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldhari" Area="DataArea" AreaIndex="1" Caption="Jumlah Hari" FieldName="hari">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldtglmulai" Area="FilterArea" AreaIndex="1" Caption="Tanggal Mulai" FieldName="tgl_mulai">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldtglselesai" Area="FilterArea" AreaIndex="2" Caption="Tanggal Selesai" FieldName="tgl_selesai">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldket" Area="FilterArea" AreaIndex="3" Caption="Keterangan" FieldName="ket">
            </dx:PivotGridField>
        </Fields>
        <OptionsPager PagerAlign="Right">
            <PageSizeItemSettings Visible="true" ShowAllItem="true" Position="Right">
            </PageSizeItemSettings>
        </OptionsPager>
    </dx:ASPxPivotGrid>
    <uc1:uc_footer runat="server" ID="uc_footer" />
</asp:Content>
