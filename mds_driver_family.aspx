<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="mds_driver_family.aspx.vb" Inherits="MDS.mds_driver_family" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Src="~/uc_header.ascx" TagPrefix="uc1" TagName="uc_header" %>
<%@ Register Src="~/uc_footer.ascx" TagPrefix="uc1" TagName="uc_footer" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:uc_header runat="server" ID="uc_header" />
    <div class="panel panel-primary">
        <div class="panel-heading">
            <h5 style="margin-left: 10px">Data Keluarga Driver</h5>
        </div>
        <div class="panel-body">
            <table id="Table1" class="tabel" runat="server">
                <tr>
                    <td style="padding: 10px">Nama Driver</td>
                    <td style="padding: 10px">: </td>
                    <td style="padding: 10px"><span runat="server" id="lb_spr"></span></td>
                </tr>
                <tr>
                    <td style="padding: 10px">NIK</td>
                    <td style="padding: 10px">: </td>
                    <td style="padding: 10px"><span runat="server" id="lb_nik"></span></td>
                </tr>
            </table>
        </div>
    </div>
    <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" Theme="Office365">
        <Columns>
            <dx:GridViewCommandColumn VisibleIndex="0" Width="100px" ShowEditButton="true" ShowDeleteButton="true" ShowClearFilterButton="true" ShowNewButtonInHeader="true">
            </dx:GridViewCommandColumn>

            <dx:GridViewDataTextColumn Caption="ID" FieldName="id_fam" Visible="false" VisibleIndex="1" Width="60px" EditFormSettings-Visible="False">
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Nama" FieldName="nama_fam" VisibleIndex="2" Width="200">
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataDateColumn Caption="Tanggal Lahir" FieldName="tgl_lhr"  VisibleIndex="3" Width="120px">
                <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd" />
                <CellStyle HorizontalAlign="Center" />
                <EditFormSettings Visible="True" />
            </dx:GridViewDataDateColumn>

            <dx:GridViewDataComboBoxColumn Caption="Jenis Kelamin" FieldName="jk" VisibleIndex="4" Width="125px">
                <PropertiesComboBox>
                    <Items>
                        <dx:ListEditItem Text="Laki-Laki" Value="Laki-Laki" />
                        <dx:ListEditItem Text="Perempuan" Value="Perempuan" />
                    </Items>
                </PropertiesComboBox>
                <CellStyle HorizontalAlign="Center" />
                <EditFormSettings Visible="True" />
            </dx:GridViewDataComboBoxColumn>

            <dx:GridViewDataComboBoxColumn Caption="Status" FieldName="status" VisibleIndex="5" Width="100">
                <PropertiesComboBox>
                    <Items>
                        <dx:ListEditItem Text="Istri" Value="Istri" />
                        <dx:ListEditItem Text="Anak Ke-1" Value="Anak Ke-1" />
                        <dx:ListEditItem Text="Anak Ke-2" Value="Anak Ke-2" />
                        <dx:ListEditItem Text="Anak Ke-3" Value="Anak Ke-3" />
                    </Items>
                </PropertiesComboBox>
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataComboBoxColumn>

            <dx:GridViewDataTextColumn Caption="Pekerjaan" FieldName="pekerjaan" VisibleIndex="6" Width="100">
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="No. BPJS Kes" FieldName="no_bpjs" VisibleIndex="7" Width="125px">
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Updated Date" FieldName="u_date" VisibleIndex="7" Width="100px">
                <EditFormSettings Visible="False" />
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="By" FieldName="u_user" VisibleIndex="8" Width="100px">
                <EditFormSettings Visible="False" />
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataTextColumn>
        </Columns>
    </dx:ASPxGridView>
    <uc1:uc_footer runat="server" ID="uc_footer" />
</asp:Content>
