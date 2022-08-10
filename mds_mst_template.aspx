<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="mds_mst_template.aspx.vb" Inherits="MDS.mds_mst_template" %>

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
            <dx:GridViewDataTextColumn Caption="ID" FieldName="id_template" VisibleIndex="1" ReadOnly="true" Width="80px">
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Title" FieldName="title" VisibleIndex="2" Visible="false">
                <EditFormSettings Visible="True" />
                <PropertiesTextEdit>
                    <ValidationSettings RequiredField-IsRequired="true" />
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataHyperLinkColumn Caption="Title" FieldName="id_template" VisibleIndex="3" Width="300px">
                <CellStyle HorizontalAlign="Center" />
                <EditFormSettings Visible="False" />
                <PropertiesHyperLinkEdit NavigateUrlFormatString="~/mds_mst_template_dtl.aspx?id_template={0}" TextField="title">
                </PropertiesHyperLinkEdit>
                <Settings AutoFilterCondition="Contains" FilterMode="DisplayText" />
            </dx:GridViewDataHyperLinkColumn>
            <dx:GridViewDataComboBoxColumn Caption="Kategori" FieldName="id_kategori" VisibleIndex="4">
                <PropertiesComboBox>
                    <ValidationSettings RequiredField-IsRequired="true">
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>
            <dx:GridViewDataTextColumn Caption="Updated" FieldName="u_date" VisibleIndex="5" EditFormSettings-Visible="False" Width="150px">
                <EditFormSettings Visible="False" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="By" FieldName="u_user" VisibleIndex="6" EditFormSettings-Visible="False" Width="100px">
                <EditFormSettings Visible="False" />
            </dx:GridViewDataTextColumn>
        </Columns>
    </dx:ASPxGridView>
    <uc1:uc_footer runat="server" ID="uc_footer" />
</asp:Content>
