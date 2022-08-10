<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="mds_trs_training.aspx.vb" Inherits="MDS.mds_trs_training" %>

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

            <dx:GridViewDataHyperLinkColumn Caption="ID" FieldName="id_train" ReadOnly="True" VisibleIndex="1" Width="80px">
                <CellStyle HorizontalAlign="Center" />
                <PropertiesHyperLinkEdit NavigateUrlFormatString="mds_trs_train_dtl.aspx?id={0}" TextFormatString="No. {0}">
                </PropertiesHyperLinkEdit>
                <EditFormSettings Visible="False" />
            </dx:GridViewDataHyperLinkColumn>

            <dx:GridViewDataHyperLinkColumn Caption="Print" FieldName="id_train" ReadOnly="True" VisibleIndex="2" Width="80px">
                <PropertiesHyperLinkEdit TextFormatString="Print" NavigateUrlFormatString="page_print.aspx?mode=train&amp;id={0}" Target="_blank">
                </PropertiesHyperLinkEdit>
                <EditFormSettings Visible="False" />
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataHyperLinkColumn>

            <dx:GridViewDataComboBoxColumn Caption="Minggu Planing RTT" FieldName="id_rtt" VisibleIndex="3" Width="100px">
                <PropertiesComboBox DropDownStyle="DropDown" ClearButton-DisplayMode="OnHover">
                </PropertiesComboBox>
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataComboBoxColumn>


            <dx:GridViewDataDateColumn Caption="Tanggal Aktual" FieldName="tgl" VisibleIndex="3" Width="120px">
                <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd">
                    <ValidationSettings>
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesDateEdit>
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataDateColumn>

            <dx:GridViewDataTextColumn Caption="Minggu Aktual" FieldName="minggu_act" VisibleIndex="3" Width="100px">
                <CellStyle HorizontalAlign="Center" />
                <EditFormSettings Visible="False" />
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Jam Awal" FieldName="jamawal" VisibleIndex="4" Width="100px">
                <CellStyle HorizontalAlign="Center" />
                <PropertiesTextEdit>
                    <ValidationSettings><RequiredField IsRequired="true" /></ValidationSettings>
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Jam Akhir" FieldName="jamakhir" VisibleIndex="5" Width="100px">
                <CellStyle HorizontalAlign="Center" />
                <PropertiesTextEdit>
                    <ValidationSettings><RequiredField IsRequired="true" /></ValidationSettings>
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataDateColumn Caption="Tgl Exp. Training" FieldName="tgl_exp_train" VisibleIndex="6" Width="140px">
                <CellStyle HorizontalAlign="Center" />
                <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd">
                    <ValidationSettings RequiredField-IsRequired="true">
                        <RequiredField IsRequired="True"></RequiredField>
                    </ValidationSettings>
                </PropertiesDateEdit>
            </dx:GridViewDataDateColumn>

            <dx:GridViewDataTextColumn Caption="Trainer" FieldName="trainer" VisibleIndex="7" Width="140px">
                <CellStyle HorizontalAlign="Center" />
                <PropertiesTextEdit>
                    <ValidationSettings><RequiredField IsRequired="true" /></ValidationSettings>
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataComboBoxColumn Caption="Lokasi" FieldName="lokasi" VisibleIndex="8" Width="100px">
                <PropertiesComboBox DropDownStyle="DropDown">
                    <ValidationSettings>
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesComboBox>
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataComboBoxColumn>

            <dx:GridViewDataComboBoxColumn Caption="Jenis Training" FieldName="id_training" VisibleIndex="9" Width="120px">
                <PropertiesComboBox>
                    <ValidationSettings>
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesComboBox>
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataComboBoxColumn>

            <dx:GridViewDataTextColumn Caption="Materi Training" FieldName="materi" VisibleIndex="10" Width="100px">
                <CellStyle HorizontalAlign="Center" />
                <PropertiesTextEdit>
                    <ValidationSettings><RequiredField IsRequired="true" /></ValidationSettings>
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Jumlah Peserta" FieldName="peserta" ReadOnly="True" VisibleIndex="11" Width="80px">
                <CellStyle HorizontalAlign="Center" />
                <EditFormSettings Visible="False" />
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataHyperLinkColumn Caption="Image" FieldName="id_train" VisibleIndex="13" Visible="true" Width="80">
                <DataItemTemplate>
                    <dx:ASPxHyperLink ID="ASPxHyperLink1" Width="80" runat="server" Text='<%# String.Concat(Eval("ttl_upload"), " Foto")%>' NavigateUrl='<%# String.Format("~/mds_trs_file.aspx?idrec={0}&sumber=TRAINING", Eval("id_train"))%>'>
                    </dx:ASPxHyperLink>
                </DataItemTemplate>
                <EditFormSettings Visible="False" />
                <PropertiesHyperLinkEdit NavigateUrlFormatString="~/mds_trs_file.aspx?idrec={0}&sumber=TRAINING" Text="">
                </PropertiesHyperLinkEdit>
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataHyperLinkColumn>

            <dx:GridViewDataTextColumn Caption="By" FieldName="u_user" ReadOnly="True" VisibleIndex="15" Width="80px">
                <EditFormSettings Visible="False" />
            </dx:GridViewDataTextColumn>
        </Columns>
        <Settings ShowPreview="true" />
        <Templates>
            <PreviewRow>
                <table style="width: 100%">
                    <tr>
                        <td>
                            List Peserta : <%#Eval("list_peserta") %>
                            <br />
                            Updated : <%#Eval("u_date")%><b> By </b><%#Eval("u_user")%>
                        </td>
                    </tr>
                </table>
            </PreviewRow>
        </Templates>
    </dx:ASPxGridView>
    <uc1:uc_footer runat="server" ID="uc_footer" />
</asp:Content>
