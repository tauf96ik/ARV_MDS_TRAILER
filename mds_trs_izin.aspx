<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="mds_trs_izin.aspx.vb" Inherits="MDS.mds_trs_izin" %>

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
            <dx:GridViewDataTextColumn Caption="ID" FieldName="id_izin" ReadOnly="True" VisibleIndex="1" Width="60px">
                <CellStyle HorizontalAlign="Center" />
                <EditFormSettings Visible="False" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataComboBoxColumn Caption="Nama Supir" FieldName="id_driver" VisibleIndex="2" Width="200px">
                <CellStyle HorizontalAlign="Center" />
                <PropertiesComboBox>
                    <ValidationSettings>
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>
            <dx:GridViewDataComboBoxColumn Caption="Jenis Izin" FieldName="id_izin_jns" VisibleIndex="3" Width="120px">
                <CellStyle HorizontalAlign="Center" />
                <PropertiesComboBox>
                    <ValidationSettings>
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>
            <dx:GridViewDataDateColumn Caption="Tgl. Mulai" FieldName="tgl_mulai" VisibleIndex="4" Width="100px">
                <CellStyle HorizontalAlign="Center" />
                <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd">
                    <ValidationSettings>
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesDateEdit>
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataDateColumn Caption="Tgl. Akhir" FieldName="tgl_akhir" VisibleIndex="5" Width="100px">
                <CellStyle HorizontalAlign="Center" />
                <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd">
                    <ValidationSettings>
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesDateEdit>
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataTextColumn Caption="Jml." FieldName="hari" ReadOnly="True" VisibleIndex="6" Width="60px">
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Keterangan" FieldName="ket" VisibleIndex="7" Width="200px">
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewCommandColumn ButtonType="Image" Caption="Kembali" VisibleIndex="8" Width="100px">
                <CustomButtons>
                    <dx:GridViewCommandColumnCustomButton ID="bt_kmbl">
                    </dx:GridViewCommandColumnCustomButton>
                </CustomButtons>
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn Caption="Updated" FieldName="u_date" ReadOnly="True" Visible="false" VisibleIndex="9" Width="100px">
                <CellStyle HorizontalAlign="Center" />
                <EditFormSettings Visible="False" />
                <PropertiesTextEdit DisplayFormatString="yyyy-MM-dd HH:mm:ss">
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
        </Columns>
        <Styles>
            <AlternatingRow BackColor="#CCFFCC" />
        </Styles>
        <Templates>
            <PreviewRow>
                <table border="0">
                    <tr>
                        <td>Approve By: <%#Eval("kmbl_user")%>, <%#Eval("kmbl_date")%></td>
                        <td>&nbsp &nbsp &nbsp &nbsp</td>
                        <td>Updated By: <%#Eval("u_user")%>, <%#Eval("u_date")%></td>
                    </tr>
                </table>
            </PreviewRow>
        </Templates>
    </dx:ASPxGridView>
    <uc1:uc_footer runat="server" ID="uc_footer" />
</asp:Content>
