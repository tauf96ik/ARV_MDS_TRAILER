<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="mds_mst_training.aspx.vb" Inherits="MDS.mds_mst_training" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Src="~/uc_header.ascx" TagPrefix="uc1" TagName="uc_header" %>
<%@ Register Src="~/uc_footer.ascx" TagPrefix="uc1" TagName="uc_footer" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:uc_header runat="server" ID="uc_header" />
    <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" Theme="Office365">
        <Columns>
            <dx:GridViewCommandColumn VisibleIndex="0" Width="100px" ShowEditButton="true" ShowDeleteButton="true" ShowClearFilterButton="true" ShowNewButtonInHeader="true">
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn Caption="ID" FieldName="id_training" VisibleIndex="1" Width="80px">
                <CellStyle HorizontalAlign="Center" />
                <EditFormSettings Visible="False" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Nama" FieldName="nama" VisibleIndex="1" Width="150px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Lokasi" FieldName="lokasi" VisibleIndex="2" Width="130px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataComboBoxColumn Caption="Trainer" FieldName="trainer" VisibleIndex="3" Width="100px">
                <PropertiesComboBox>
                    <Items>
                        <dx:ListEditItem Selected="True" Text="Internal" Value="Internal" />
                        <dx:ListEditItem Text="External" Value="External" />
                    </Items>
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>
            <dx:GridViewDataCheckColumn Caption="Blok Status" FieldName="sta_blok" VisibleIndex="4" Width="130px">
            </dx:GridViewDataCheckColumn>
            <dx:GridViewDataTextColumn Caption="Updated" FieldName="u_date" VisibleIndex="5" Width="130px">
                <EditFormSettings Visible="False" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="By" FieldName="u_user" VisibleIndex="6" Width="100px">
                <EditFormSettings Visible="False" />
            </dx:GridViewDataTextColumn>
        </Columns>
    </dx:ASPxGridView>
    <uc1:uc_footer runat="server" ID="uc_footer" />
</asp:Content>
