<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="mds_trs_apd_out.aspx.vb" Inherits="MDS.mds_trs_apd_out" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Src="~/uc_header.ascx" TagPrefix="uc1" TagName="uc_header" %>
<%@ Register Src="~/uc_footer.ascx" TagPrefix="uc1" TagName="uc_footer" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:uc_header runat="server" ID="uc_header" />
    <table>
        <tr>
            <td style="width: 100px">ID</td>
            <td style="width: 10px">:</td>
            <td><label runat="server" id="lb_id"></label></td>
        </tr>
        <tr>
            <td>Tgl. Terima</td>
            <td>:</td>
            <td><label runat="server" id="lb_tgl"></label></td>
        </tr>
        <tr>
            <td>Jenis APD</td>
            <td>:</td>
            <td><label runat="server" id="lb_apd"></label></td>
        </tr>
        <tr>
            <td>Jumlah</td>
            <td>:</td>
            <td><label runat="server" id="lb_jml"></label></td>
        </tr>
    </table>
    <br />
    <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" Theme="Office365">
        <Columns>
            <dx:GridViewCommandColumn VisibleIndex="0" Width="110px" ShowEditButton="true" ShowDeleteButton="true" ShowClearFilterButton="true" ShowNewButtonInHeader="true">
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn Caption="ID" FieldName="id_out_apd" ReadOnly="True" VisibleIndex="1" Width="80px">
                <CellStyle HorizontalAlign="Center" />
                <EditFormSettings Visible="False" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataDateColumn Caption="Tanggal" FieldName="tgl" VisibleIndex="2" Width="100px">
                <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd">
                    <ValidationSettings>
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesDateEdit>
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataComboBoxColumn Caption="Nama Supir" FieldName="id_supir" VisibleIndex="3" Width="140px">
                <PropertiesComboBox>
                    <ClearButton DisplayMode="OnHover" />
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>
            <dx:GridViewDataTextColumn Caption="Karyawan Pembagi" FieldName="nama_kar" VisibleIndex="4" Width="120px">
                <EditFormSettings Visible="False" />
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Lokasi" FieldName="lokasi" VisibleIndex="5" Width="100px">
                <EditFormSettings Visible="False" />
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Qty" FieldName="qty" VisibleIndex="6" Width="80px">
                <CellStyle HorizontalAlign="Center" />
                <PropertiesTextEdit>
                    <ValidationSettings>
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Ukuran" FieldName="ukuran" VisibleIndex="7" Width="100px">
                <PropertiesTextEdit>
                    <ValidationSettings>
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataComboBoxColumn Caption="No.Polisi" FieldName="nopol" VisibleIndex="8" Width="90px">
            </dx:GridViewDataComboBoxColumn>
            <dx:GridViewDataComboBoxColumn Caption="Status" FieldName="status" VisibleIndex="9" Width="100px">
                <PropertiesComboBox>
                    <Items>
                        <dx:ListEditItem Text="Dibagi" Value="Dibagi" />
                        <dx:ListEditItem Text="Dibeli" Value="Dibeli" />
                    </Items>
                    <ValidationSettings>
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>
            <dx:GridViewDataTextColumn Caption="Keterangan" FieldName="ket" VisibleIndex="10" Width="160px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Updated" FieldName="u_date" ReadOnly="True" VisibleIndex="11" Width="130px">
                <PropertiesTextEdit DisplayFormatString="yyyy-MM-dd HH:mm:ss">
                </PropertiesTextEdit>
                <EditFormSettings Visible="False" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="By" FieldName="u_user" ReadOnly="True" VisibleIndex="12" Width="80px">
                <EditFormSettings Visible="False" />
            </dx:GridViewDataTextColumn>
        </Columns>
    </dx:ASPxGridView>
    <uc1:uc_footer runat="server" ID="uc_footer" />
</asp:Content>
