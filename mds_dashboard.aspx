<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="mds_dashboard.aspx.vb" Inherits="MDS.mds_dashboard" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Src="~/uc_header.ascx" TagPrefix="uc1" TagName="uc_header" %>
<%@ Register Src="~/uc_footer.ascx" TagPrefix="uc1" TagName="uc_footer" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:uc_header runat="server" ID="uc_header" />
    <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False">
        <Columns>
            <dx:GridViewDataTextColumn Caption="Nopol" FieldName="nopol" VisibleIndex="0">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Lokasi" FieldName="lokasi" VisibleIndex="1">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Event" FieldName="event" VisibleIndex="2">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Geofence" FieldName="geo_last" VisibleIndex="4">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Tanggal Jam" FieldName="tgl" VisibleIndex="3">
            </dx:GridViewDataTextColumn>
        </Columns>

    </dx:ASPxGridView>
    
    <uc1:uc_footer runat="server" ID="uc_footer" />
</asp:Content>
