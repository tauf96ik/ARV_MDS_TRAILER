<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="mds_trs_train_dtl.aspx.vb" Inherits="MDS.mds_trs_train_dtl" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Src="~/uc_header.ascx" TagPrefix="uc1" TagName="uc_header" %>
<%@ Register Src="~/uc_footer.ascx" TagPrefix="uc1" TagName="uc_footer" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:uc_header runat="server" ID="uc_header" />
    <table>
        <tr>
            <td style="width: 100px">No. Training</td>
            <td style="width: 10px">:</td>
            <td><label runat="server" id="lb_id"></label></td>
        </tr>
        <tr>
            <td>Jenis Training</td>
            <td>:</td>
            <td><label runat="server" id="lb_training"></label></td>
        </tr>
        <tr>
            <td>Tanggal</td>
            <td>:</td>
            <td><label runat="server" id="lb_tgl"></label></td>
        </tr>
        <tr>
            <td>Trainer</td>
            <td>:</td>
            <td><label runat="server" id="lb_trainer"></label></td>
        </tr>
    </table>
    <br />

    <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="false" Theme="Office365">
        <Columns>
            <dx:GridViewCommandColumn VisibleIndex="0" Width="105px" ShowEditButton="true" ShowDeleteButton="true" ShowClearFilterButton="true" ShowNewButtonInHeader="true">
            </dx:GridViewCommandColumn>

            <dx:GridViewDataComboBoxColumn Caption="Supir" FieldName="id_spr" VisibleIndex="1" Width="200px">
                <PropertiesComboBox DropDownStyle="DropDown">
                    <ValidationSettings>
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesComboBox>
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataComboBoxColumn>

            <dx:GridViewDataTextColumn Caption="Pre Test" FieldName="nilai_awal" VisibleIndex="2" Width="100px">
                <PropertiesTextEdit DisplayFormatString="#.##" DisplayFormatInEditMode="true" NullDisplayText="0" NullText="0">
                    <MaskSettings IncludeLiterals="None" />
                </PropertiesTextEdit>
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Post Test" FieldName="nilai_akhir" VisibleIndex="3" Width="100px">
                <PropertiesTextEdit DisplayFormatString="#.##" DisplayFormatInEditMode="true" NullDisplayText="0" NullText="0">
                    <MaskSettings IncludeLiterals="None" />
                </PropertiesTextEdit>
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Nilai Akhir" FieldName="nilai" VisibleIndex="6" Width="100px">
                <PropertiesTextEdit DisplayFormatString="#.##" DisplayFormatInEditMode="true" NullDisplayText="0" NullText="0">
                    <MaskSettings IncludeLiterals="None" />
                </PropertiesTextEdit>
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Keterangan" FieldName="ket" VisibleIndex="7" Width="200px">
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Updated" FieldName="u_date" ReadOnly="True" VisibleIndex="8" Width="130px">
                <PropertiesTextEdit DisplayFormatString="yyyy-MM-dd HH:mm:ss">
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="By" FieldName="u_user" ReadOnly="True" VisibleIndex="9" Width="80px">
            </dx:GridViewDataTextColumn>
        </Columns>
    </dx:ASPxGridView>
    <uc1:uc_footer runat="server" ID="uc_footer" />
</asp:Content>
