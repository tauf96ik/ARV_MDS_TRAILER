<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="mds_mst_plan_train.aspx.vb" Inherits="MDS.mds_mst_plan_train" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Src="~/uc_header.ascx" TagPrefix="uc1" TagName="uc_header" %>
<%@ Register Src="~/uc_footer.ascx" TagPrefix="uc1" TagName="uc_footer" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:uc_header runat="server" ID="uc_header" />
    <script src="js/autho_combo.js"></script>
    <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" Theme="Office365" Width="100">
        <Columns>
            <dx:GridViewCommandColumn VisibleIndex="0" Width="100px"  ShowEditButton="true" ShowDeleteButton="true" ShowClearFilterButton="true" ShowNewButtonInHeader="true">
            </dx:GridViewCommandColumn>

            <dx:GridViewDataTextColumn Caption="ID Planing" FieldName="id_plan_train" VisibleIndex="1" Width="100px">
                <CellStyle HorizontalAlign="Center" />
                <EditFormSettings Visible="False" />
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataDateColumn Caption="Tanggal Planing" FieldName="tgl" VisibleIndex="2" Width="120px">
                <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd">
                    <ValidationSettings>
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesDateEdit>
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataDateColumn>

            <dx:GridViewDataTextColumn Caption="Minggu Ke" FieldName="minggu" VisibleIndex="3" Width="100px">
                <CellStyle HorizontalAlign="Center" />
                <EditFormSettings Visible="False" />
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataComboBoxColumn Caption="Driver" FieldName="driver" VisibleIndex="4" Width="200px">
                <EditItemTemplate>
                    <dx:ASPxDropDownEdit ID="dd_driver" runat="server" ClientInstanceName="cb_driver" OnInit="dd_driver_Init" Width="100%" EnableTheming="true" Theme="Office365">
                        <DropDownWindowStyle BackColor="#EDEDED" />
                        <DropDownWindowTemplate>
                            <dx:ASPxListBox ID="listBox_driver" runat="server" FilteringSettings-ShowSearchUI="true" ClientInstanceName="clb_driver" Height="300px" OnInit="listBox_driver_Init" Rows="20" SelectionMode="CheckColumn" TextField="User" Width="100%" EnableTheming="true" Theme="Office365">
                                <Border BorderStyle="None" />
                                <BorderBottom BorderColor="#DCDCDC" BorderStyle="Solid" BorderWidth="1px" />
                                <ClientSideEvents SelectedIndexChanged="function(s, e){ OnListBoxSelectionChanged(s, e, cb_driver); }" />
                            </dx:ASPxListBox>
                            <table style="width: 100%">
                                <tr>
                                    <td style="padding: 4px">
                                        <dx:ASPxButton ID="ASPxButton_driver" runat="server" AutoPostBack="False" Style="float: right" Text="Close" EnableTheming="true" Theme="Office365">
                                            <ClientSideEvents Click="function(s, e){ cb_driver.HideDropDown(); }" />
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </DropDownWindowTemplate>
                        <ClientSideEvents DropDown="function(s, e){ SynchronizeListBoxValues(s, e, clb_driver); }" TextChanged="function(s, e){ SynchronizeListBoxValues(s, e, clb_driver); }" />
                    </dx:ASPxDropDownEdit>
                </EditItemTemplate>
            </dx:GridViewDataComboBoxColumn>

            <dx:GridViewDataTextColumn Caption="Jumlah Driver" FieldName="jumlah" VisibleIndex="5" Width="100px">
                <CellStyle HorizontalAlign="Center" />
                <EditFormSettings Visible="False" />
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataComboBoxColumn Caption="Materi Training" FieldName="materi" VisibleIndex="6" Width="180px">
                <PropertiesComboBox ValidationSettings-RequiredField-IsRequired="true" />
                <EditItemTemplate>
                    <dx:ASPxDropDownEdit ID="dd_train" runat="server" ClientInstanceName="cb_train" OnInit="dd_train_Init" Width="100%" EnableTheming="true" Theme="Office365">
                        <DropDownWindowStyle BackColor="#EDEDED" />
                        <DropDownWindowTemplate>
                            <dx:ASPxListBox ID="listBox_train" runat="server" FilteringSettings-ShowSearchUI="true" ClientInstanceName="clb_train" Height="300px" OnInit="listBox_train_Init" Rows="20" SelectionMode="CheckColumn" TextField="User" Width="100%" EnableTheming="true" Theme="Office365">
                                <Border BorderStyle="None" />
                                <BorderBottom BorderColor="#DCDCDC" BorderStyle="Solid" BorderWidth="1px" />
                                <ClientSideEvents SelectedIndexChanged="function(s, e){ OnListBoxSelectionChanged(s, e, cb_train); }" />
                            </dx:ASPxListBox>
                            <table style="width: 100%">
                                <tr>
                                    <td style="padding: 4px">
                                        <dx:ASPxButton ID="ASPxButton_train" runat="server" AutoPostBack="False" Style="float: right" Text="Close" EnableTheming="true" Theme="Office365">
                                            <ClientSideEvents Click="function(s, e){ cb_train.HideDropDown(); }" />
                                        </dx:ASPxButton>
                                    </td>
                                </tr>      
                            </table>
                        </DropDownWindowTemplate>
                        <ClientSideEvents DropDown="function(s, e){ SynchronizeListBoxValues(s, e, clb_train); }" TextChanged="function(s, e){ SynchronizeListBoxValues(s, e, clb_train); }" />
                    </dx:ASPxDropDownEdit>
                </EditItemTemplate>
            </dx:GridViewDataComboBoxColumn>

            <dx:GridViewDataTextColumn Caption="Updated" FieldName="u_date" VisibleIndex="7" Width="100px">
                <EditFormSettings Visible="False" />
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="By" FieldName="u_user" VisibleIndex="8" Width="100px">
                <EditFormSettings Visible="False" />
            </dx:GridViewDataTextColumn>
        </Columns>
    </dx:ASPxGridView>
    <uc1:uc_footer runat="server" ID="uc_footer" />
</asp:Content>
