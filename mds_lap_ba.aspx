<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="mds_lap_ba.aspx.vb" Inherits="MDS.mds_lap_ba" %>

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
            <dx:PivotGridField ID="fieldjenis" Area="RowArea" AreaIndex="0" Caption="Jenis Berita Acara" FieldName="ba">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldqty" Area="DataArea" AreaIndex="0" Caption="Qty" FieldName="qty">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldthnbln" Area="ColumnArea" AreaIndex="0" Caption="Tahun Bulan" FieldName="thnbln">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldsupir" AreaIndex="0" Caption="Nama Supir" FieldName="supir">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldtgl" AreaIndex="1" Caption="Tanggal" FieldName="tgl">
            </dx:PivotGridField>
            <dx:PivotGridField ID="field" AreaIndex="2" FieldName="tgl_kejadian" Caption="Tgl. Kejadian">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldnoba" AreaIndex="3" Caption="No. Berita Acara" FieldName="no_ba">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldnopol" AreaIndex="4" Caption="No. Polisi" FieldName="nopol">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldpoint" AreaIndex="5" Caption="Point" FieldName="point">
            </dx:PivotGridField>
        </Fields>
    </dx:ASPxPivotGrid>
    <uc1:uc_footer runat="server" ID="uc_footer" />
</asp:Content>
