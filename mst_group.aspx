<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="mst_group.aspx.vb" Inherits="MDS.mst_group" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Src="~/uc_header.ascx" TagPrefix="uc1" TagName="uc_header" %>
<%@ Register Src="~/uc_footer.ascx" TagPrefix="uc1" TagName="uc_footer" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:uc_header runat="server" ID="uc_header" />
    <script src="js/autho_combo.js"></script>

    <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" Theme="Office365">
        <Columns>
            <dx:GridViewCommandColumn Caption="Actions" VisibleIndex="0" Width="100px" ShowDeleteButton="true" ShowEditButton="true" ShowNewButtonInHeader="true">
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn FieldName="id_user_grp" ReadOnly="True" Visible="False" VisibleIndex="1" Caption="ID" Width="60px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Nama" FieldName="nama" VisibleIndex="2" Width="130px">
                <EditFormSettings VisibleIndex="1" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Updated" FieldName="u_date" ReadOnly="True" VisibleIndex="13" Width="100px">
                <PropertiesTextEdit DisplayFormatString="yyyy-MM-dd HH:mm:ss">
                </PropertiesTextEdit>
                <EditFormSettings VisibleIndex="20" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="By" FieldName="u_user" ReadOnly="True" VisibleIndex="14" Width="100px">
                <EditFormSettings VisibleIndex="21" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewBandColumn Caption="AUTHORIZE" VisibleIndex="3">
                <Columns>

                    <dx:GridViewDataComboBoxColumn Caption="View" FieldName="lihat" VisibleIndex="0" Width="80px">
                        <EditFormSettings VisibleIndex="3" />
                        <EditItemTemplate>
                            <dx:ASPxDropDownEdit ID="dde_lihat" runat="server" ClientInstanceName="ccb_lihat" OnInit="ASPxDropDownEdit_Init" Width="100%">
                                <DropDownWindowStyle BackColor="#EDEDED" />
                                <DropDownWindowTemplate>
                                    <dx:ASPxListBox ID="listBox_lihat" runat="server" ClientInstanceName="clb_lihat" Height="300px" OnInit="listBox_Init" Rows="20" SelectionMode="CheckColumn" TextField="User" Width="100%">
                                        <Border BorderStyle="None" />
                                        <BorderBottom BorderColor="#DCDCDC" BorderStyle="Solid" BorderWidth="1px" />
                                        <ClientSideEvents SelectedIndexChanged="function(s, e){ OnListBoxSelectionChanged(s, e, ccb_lihat); }" />
                                    </dx:ASPxListBox>
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="padding: 4px">
                                                <dx:ASPxButton ID="ASPxButton_ubah" runat="server" AutoPostBack="False" Style="float: right" Text="Close">
                                                    <ClientSideEvents Click="function(s, e){ ccb_lihat.HideDropDown(); }" />
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </DropDownWindowTemplate>
                                <ClientSideEvents DropDown="function(s, e){ SynchronizeListBoxValues(s, e, clb_lihat); }" TextChanged="function(s, e){ SynchronizeListBoxValues(s, e, clb_lihat); }" />
                            </dx:ASPxDropDownEdit>
                        </EditItemTemplate>
                    </dx:GridViewDataComboBoxColumn>

                    <dx:GridViewDataComboBoxColumn Caption="New" FieldName="baru" VisibleIndex="1" Width="80px">
                        <EditFormSettings VisibleIndex="5" />
                        <EditItemTemplate>
                            <dx:ASPxDropDownEdit ID="dde_baru" runat="server" ClientInstanceName="ccb_baru" OnInit="ASPxDropDownEdit_Init" Width="100%">
                                <DropDownWindowStyle BackColor="#EDEDED" />
                                <DropDownWindowTemplate>
                                    <dx:ASPxListBox ID="listBox_baru" runat="server" ClientInstanceName="clb_baru" Height="300px" OnInit="listBox_Init" Rows="20" SelectionMode="CheckColumn" TextField="User" Width="100%">
                                        <Border BorderStyle="None" />
                                        <BorderBottom BorderColor="#DCDCDC" BorderStyle="Solid" BorderWidth="1px" />
                                        <ClientSideEvents SelectedIndexChanged="function(s, e){ OnListBoxSelectionChanged(s, e, ccb_baru); }" />
                                    </dx:ASPxListBox>
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="padding: 4px">
                                                <dx:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="False" Style="float: right" Text="Close">
                                                    <ClientSideEvents Click="function(s, e){ ccb_baru.HideDropDown(); }" />
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </DropDownWindowTemplate>
                                <ClientSideEvents DropDown="function(s, e){ SynchronizeListBoxValues(s, e, clb_baru); }" TextChanged="function(s, e){ SynchronizeListBoxValues(s, e, clb_baru); }" />
                            </dx:ASPxDropDownEdit>
                        </EditItemTemplate>
                    </dx:GridViewDataComboBoxColumn>

                    <dx:GridViewDataComboBoxColumn Caption="Change" FieldName="ubah" VisibleIndex="2" Width="80px">
                        <EditFormSettings VisibleIndex="7" />
                        <EditItemTemplate>
                            <dx:ASPxDropDownEdit ID="dde_ubah" runat="server" ClientInstanceName="ccb_ubah" OnInit="ASPxDropDownEdit_Init" Width="100%">
                                <DropDownWindowStyle BackColor="#EDEDED" />
                                <DropDownWindowTemplate>
                                    <dx:ASPxListBox ID="listBox_ubah" runat="server" ClientInstanceName="clb_ubah" Height="300px" OnInit="listBox_Init" Rows="20" SelectionMode="CheckColumn" TextField="User" Width="100%">
                                        <Border BorderStyle="None" />
                                        <BorderBottom BorderColor="#DCDCDC" BorderStyle="Solid" BorderWidth="1px" />
                                        <ClientSideEvents SelectedIndexChanged="function(s, e){ OnListBoxSelectionChanged(s, e, ccb_ubah); }" />
                                    </dx:ASPxListBox>
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="padding: 4px">
                                                <dx:ASPxButton ID="ASPxButton_ubah" runat="server" AutoPostBack="False" Style="float: right" Text="Close">
                                                    <ClientSideEvents Click="function(s, e){ ccb_ubah.HideDropDown(); }" />
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </DropDownWindowTemplate>
                                <ClientSideEvents DropDown="function(s, e){ SynchronizeListBoxValues(s, e, clb_ubah); }" TextChanged="function(s, e){ SynchronizeListBoxValues(s, e, clb_ubah); }" />
                            </dx:ASPxDropDownEdit>
                        </EditItemTemplate>
                    </dx:GridViewDataComboBoxColumn>

                    <dx:GridViewDataComboBoxColumn Caption="Delete" FieldName="hapus" VisibleIndex="3" Width="80px">
                        <EditFormSettings VisibleIndex="9" />
                        <EditItemTemplate>
                            <dx:ASPxDropDownEdit ID="dde_hapus" runat="server" ClientInstanceName="ccb_hapus" OnInit="ASPxDropDownEdit_Init" Width="100%">
                                <DropDownWindowStyle BackColor="#EDEDED" />
                                <DropDownWindowTemplate>
                                    <dx:ASPxListBox ID="listBox_hapus" runat="server" ClientInstanceName="clb_hapus" Height="300px" OnInit="listBox_Init" Rows="20" SelectionMode="CheckColumn" TextField="User" Width="100%">
                                        <Border BorderStyle="None" />
                                        <BorderBottom BorderColor="#DCDCDC" BorderStyle="Solid" BorderWidth="1px" />
                                        <ClientSideEvents SelectedIndexChanged="function(s, e){ OnListBoxSelectionChanged(s, e, ccb_hapus); }" />
                                    </dx:ASPxListBox>
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="padding: 4px">
                                                <dx:ASPxButton ID="ASPxButton_hapus" runat="server" AutoPostBack="False" Style="float: right" Text="Close">
                                                    <ClientSideEvents Click="function(s, e){ ccb_hapus.HideDropDown(); }" />
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </DropDownWindowTemplate>
                                <ClientSideEvents DropDown="function(s, e){ SynchronizeListBoxValues(s, e, clb_hapus); }" TextChanged="function(s, e){ SynchronizeListBoxValues(s, e, clb_hapus); }" />
                            </dx:ASPxDropDownEdit>
                        </EditItemTemplate>
                    </dx:GridViewDataComboBoxColumn>

                </Columns>
            </dx:GridViewBandColumn>
            <dx:GridViewBandColumn Caption="APPROVE" VisibleIndex="8">
                <Columns>
                    
                    <dx:GridViewDataComboBoxColumn FieldName="app_spv" ShowInCustomizationForm="True" Width="80px" Caption="Supervisor" VisibleIndex="5">
                        <EditFormSettings VisibleIndex="2" />
                        <EditItemTemplate>
                            <dx:ASPxDropDownEdit ID="dde_spv" runat="server" ClientInstanceName="ccb_spv" OnInit="ASPxDropDownEdit_Init" Width="100%">
                                <DropDownWindowStyle BackColor="#EDEDED" />
                                <DropDownWindowTemplate>
                                    <dx:ASPxListBox ID="listBox_spv" runat="server" ClientInstanceName="clb_spv" Height="300px" OnInit="listBox_Init" Rows="20" SelectionMode="CheckColumn" TextField="User" Width="100%">
                                        <Border BorderStyle="None" />
                                        <BorderBottom BorderColor="#DCDCDC" BorderStyle="Solid" BorderWidth="1px" />
                                        <ClientSideEvents SelectedIndexChanged="function(s, e){ OnListBoxSelectionChanged(s, e, ccb_spv); }" />
                                    </dx:ASPxListBox>
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="padding: 4px">
                                                <dx:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="False" Style="float: right" Text="Close">
                                                    <ClientSideEvents Click="function(s, e){ ccb_spv.HideDropDown(); }" />
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </DropDownWindowTemplate>
                                <ClientSideEvents DropDown="function(s, e){ SynchronizeListBoxValues(s, e, clb_spv); }" TextChanged="function(s, e){ SynchronizeListBoxValues(s, e, clb_spv); }" />
                            </dx:ASPxDropDownEdit>
                        </EditItemTemplate>
                    </dx:GridViewDataComboBoxColumn>

                    <dx:GridViewDataComboBoxColumn Caption="Manager" FieldName="app_mgr" ShowInCustomizationForm="True" VisibleIndex="6" Width="80px">
                        <EditFormSettings VisibleIndex="4" />
                        <EditItemTemplate>
                            <dx:ASPxDropDownEdit ID="dde_mgr" runat="server" ClientInstanceName="ccb_mgr" OnInit="ASPxDropDownEdit_Init" Width="100%">
                                <DropDownWindowStyle BackColor="#EDEDED" />
                                <DropDownWindowTemplate>
                                    <dx:ASPxListBox ID="listBox_mgr" runat="server" ClientInstanceName="clb_mgr" Height="300px" OnInit="listBox_Init" Rows="20" SelectionMode="CheckColumn" TextField="User" Width="100%">
                                        <Border BorderStyle="None" />
                                        <BorderBottom BorderColor="#DCDCDC" BorderStyle="Solid" BorderWidth="1px" />
                                        <ClientSideEvents SelectedIndexChanged="function(s, e){ OnListBoxSelectionChanged(s, e, ccb_mgr); }" />
                                    </dx:ASPxListBox>
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="padding: 4px">
                                                <dx:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="False" Style="float: right" Text="Close">
                                                    <ClientSideEvents Click="function(s, e){ ccb_mgr.HideDropDown(); }" />
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </DropDownWindowTemplate>
                                <ClientSideEvents DropDown="function(s, e){ SynchronizeListBoxValues(s, e, clb_mgr); }" TextChanged="function(s, e){ SynchronizeListBoxValues(s, e, clb_mgr); }" />
                            </dx:ASPxDropDownEdit>
                        </EditItemTemplate>
                    </dx:GridViewDataComboBoxColumn>

                    <dx:GridViewDataComboBoxColumn Caption="FIN/HR/GA" FieldName="app_fin" ShowInCustomizationForm="True" VisibleIndex="7" Width="100px">
                        <EditFormSettings VisibleIndex="6" />
                        <EditItemTemplate>
                            <dx:ASPxDropDownEdit ID="dde_fin" runat="server" ClientInstanceName="ccb_fin" OnInit="ASPxDropDownEdit_Init" Width="100%">
                                <DropDownWindowStyle BackColor="#EDEDED" />
                                <DropDownWindowTemplate>
                                    <dx:ASPxListBox ID="listBox_fin" runat="server" ClientInstanceName="clb_fin" Height="300px" OnInit="listBox_Init" Rows="20" SelectionMode="CheckColumn" TextField="User" Width="100%">
                                        <Border BorderStyle="None" />
                                        <BorderBottom BorderColor="#DCDCDC" BorderStyle="Solid" BorderWidth="1px" />
                                        <ClientSideEvents SelectedIndexChanged="function(s, e){ OnListBoxSelectionChanged(s, e, ccb_fin); }" />
                                    </dx:ASPxListBox>
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="padding: 4px">
                                                <dx:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="False" Style="float: right" Text="Close">
                                                    <ClientSideEvents Click="function(s, e){ ccb_fin.HideDropDown(); }" />
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </DropDownWindowTemplate>
                                <ClientSideEvents DropDown="function(s, e){ SynchronizeListBoxValues(s, e, clb_fin); }" TextChanged="function(s, e){ SynchronizeListBoxValues(s, e, clb_fin); }" />
                            </dx:ASPxDropDownEdit>
                        </EditItemTemplate>
                    </dx:GridViewDataComboBoxColumn>

                    <dx:GridViewDataComboBoxColumn Caption="GM" FieldName="app_gm" ShowInCustomizationForm="True" VisibleIndex="8" Width="80px">
                        <EditFormSettings VisibleIndex="8" />
                        <EditItemTemplate>
                            <dx:ASPxDropDownEdit ID="dde_gm" runat="server" ClientInstanceName="ccb_gm" OnInit="ASPxDropDownEdit_Init" Width="100%">
                                <DropDownWindowStyle BackColor="#EDEDED" />
                                <DropDownWindowTemplate>
                                    <dx:ASPxListBox ID="listBox_gm" runat="server" ClientInstanceName="clb_gm" Height="300px" OnInit="listBox_Init" Rows="20" SelectionMode="CheckColumn" TextField="User" Width="100%">
                                        <Border BorderStyle="None" />
                                        <BorderBottom BorderColor="#DCDCDC" BorderStyle="Solid" BorderWidth="1px" />
                                        <ClientSideEvents SelectedIndexChanged="function(s, e){ OnListBoxSelectionChanged(s, e, ccb_gm); }" />
                                    </dx:ASPxListBox>
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="padding: 4px">
                                                <dx:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="False" Style="float: right" Text="Close">
                                                    <ClientSideEvents Click="function(s, e){ ccb_gm.HideDropDown(); }" />
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </DropDownWindowTemplate>
                                <ClientSideEvents DropDown="function(s, e){ SynchronizeListBoxValues(s, e, clb_gm); }" TextChanged="function(s, e){ SynchronizeListBoxValues(s, e, clb_gm); }" />
                            </dx:ASPxDropDownEdit>
                        </EditItemTemplate>
                    </dx:GridViewDataComboBoxColumn>

                    <dx:GridViewDataComboBoxColumn Caption="COO" FieldName="app_coo" ShowInCustomizationForm="True" VisibleIndex="8" Width="80px">
                        <EditFormSettings VisibleIndex="8" />
                        <EditItemTemplate>
                            <dx:ASPxDropDownEdit ID="dde_dir" runat="server" ClientInstanceName="ccb_dir" OnInit="ASPxDropDownEdit_Init" Width="100%">
                                <DropDownWindowStyle BackColor="#EDEDED" />
                                <DropDownWindowTemplate>
                                    <dx:ASPxListBox ID="listBox_dir" runat="server" ClientInstanceName="clb_dir" Height="300px" OnInit="listBox_Init" Rows="20" SelectionMode="CheckColumn" TextField="User" Width="100%">
                                        <Border BorderStyle="None" />
                                        <BorderBottom BorderColor="#DCDCDC" BorderStyle="Solid" BorderWidth="1px" />
                                        <ClientSideEvents SelectedIndexChanged="function(s, e){ OnListBoxSelectionChanged(s, e, ccb_dir); }" />
                                    </dx:ASPxListBox>
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="padding: 4px">
                                                <dx:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="False" Style="float: right" Text="Close">
                                                    <ClientSideEvents Click="function(s, e){ ccb_dir.HideDropDown(); }" />
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </DropDownWindowTemplate>
                                <ClientSideEvents DropDown="function(s, e){ SynchronizeListBoxValues(s, e, clb_dir); }" TextChanged="function(s, e){ SynchronizeListBoxValues(s, e, clb_dir); }" />
                            </dx:ASPxDropDownEdit>
                        </EditItemTemplate>
                    </dx:GridViewDataComboBoxColumn>

                    <dx:GridViewDataComboBoxColumn Caption="OTH" FieldName="app_oth" ShowInCustomizationForm="True" VisibleIndex="8" Width="80px">
                        <EditFormSettings VisibleIndex="8" />
                        <EditItemTemplate>
                            <dx:ASPxDropDownEdit ID="dde_oth" runat="server" ClientInstanceName="ccb_oth" OnInit="ASPxDropDownEdit_Init" Width="100%">
                                <DropDownWindowStyle BackColor="#EDEDED" />
                                <DropDownWindowTemplate>
                                    <dx:ASPxListBox ID="listBox_oth" runat="server" ClientInstanceName="clb_oth" Height="300px" OnInit="listBox_Init" Rows="20" SelectionMode="CheckColumn" TextField="User" Width="100%">
                                        <Border BorderStyle="None" />
                                        <BorderBottom BorderColor="#DCDCDC" BorderStyle="Solid" BorderWidth="1px" />
                                        <ClientSideEvents SelectedIndexChanged="function(s, e){ OnListBoxSelectionChanged(s, e, ccb_oth); }" />
                                    </dx:ASPxListBox>
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="padding: 4px">
                                                <dx:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="False" Style="float: right" Text="Close">
                                                    <ClientSideEvents Click="function(s, e){ ccb_oth.HideDropDown(); }" />
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </DropDownWindowTemplate>
                                <ClientSideEvents DropDown="function(s, e){ SynchronizeListBoxValues(s, e, clb_oth); }" TextChanged="function(s, e){ SynchronizeListBoxValues(s, e, clb_oth); }" />
                            </dx:ASPxDropDownEdit>
                        </EditItemTemplate>
                    </dx:GridViewDataComboBoxColumn>

                </Columns>
            </dx:GridViewBandColumn>
        </Columns>
    </dx:ASPxGridView>

    <uc1:uc_footer runat="server" ID="uc_footer" />
</asp:Content>
