<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="mds_lap_supir.aspx.vb" Inherits="MDS.mds_lap_supir" %>

<%@ Register Assembly="DevExpress.XtraCharts.v18.2.Web, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dx" %>

<%@ Register Src="~/uc_header.ascx" TagPrefix="uc1" TagName="uc_header" %>
<%@ Register Src="~/uc_footer.ascx" TagPrefix="uc1" TagName="uc_footer" %>
<%@ Register Src="~/uc_chart.ascx" TagPrefix="uc1" TagName="uc_chart" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:uc_header runat="server" ID="uc_header" />
    <dx:ASPxPivotGrid ID="ASPxPivotGrid1" runat="server">
        <Fields>
            <dx:PivotGridField ID="id_supir" AreaIndex="0" Caption="ID">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fielddriverid" AreaIndex="1" Caption="No.Supir" FieldName="driver_id">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldnama" AreaIndex="2" Caption="Nama Supir" FieldName="nama">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldjabatan" Area="RowArea" AreaIndex="0" Caption="Jabatan" FieldName="kategori">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldrekbank" AreaIndex="3" Caption="Rek.Bank" FieldName="rek_bank">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldlokasi" Area="ColumnArea" AreaIndex="0" Caption="Lokasi" FieldName="lokasi">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldumur" AreaIndex="4" Caption="Umur" FieldName="umur">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldmasakerja" AreaIndex="5" Caption="Masa Kerja" FieldName="masakerja">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldaktif" AreaIndex="6" Caption="Status Aktif" FieldName="aktif">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldsiap" AreaIndex="7" Caption="Status Siap" FieldName="siap">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldpendidikan" AreaIndex="8" Caption="Pendidikan" FieldName="nmpendidikan">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldsim" AreaIndex="9" Caption="SIM" FieldName="sim">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldqty" Area="DataArea" AreaIndex="0" Caption="Qty" FieldName="qty">
            </dx:PivotGridField>
        </Fields>
    </dx:ASPxPivotGrid>
    <br />
    <uc1:uc_chart runat="server" ID="uc_chart" />
    <br />
    <dx:WebChartControl ID="WebChartControl1" runat="server">

    </dx:WebChartControl>
    <uc1:uc_footer runat="server" ID="uc_footer" />
</asp:Content>
