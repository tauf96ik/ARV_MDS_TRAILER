<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="mds_driver_inact.aspx.vb" Inherits="MDS.mds_driver_inact" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Src="~/uc_footer.ascx" TagPrefix="uc1" TagName="uc_footer" %>
<%@ Register Src="~/uc_header.ascx" TagPrefix="uc1" TagName="uc_header" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:uc_header runat="server" ID="uc_header" />

    <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" Theme="Office365">
        <StylesPopup>
            <EditForm>
                <Header BackColor="DarkBlue"></Header>
            </EditForm>
        </StylesPopup>
        <Styles>
            <AlternatingRow Enabled="True"></AlternatingRow>
        </Styles>
        <Columns>
            <dx:GridViewCommandColumn VisibleIndex="0" Width="100px" ShowEditButton="false" ShowDeleteButton="true" ShowNewButtonInHeader="false">
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn Caption="Nama Driver" FieldName="nama" VisibleIndex="2" Width="100px">
                <PropertiesTextEdit>
                    <ValidationSettings>
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Bank Rek" FieldName="rek_bank" VisibleIndex="4" Width="80px">
                <PropertiesTextEdit>
                    <ValidationSettings>
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="rek_no" VisibleIndex="5" Width="120px" Caption="No. Rek">
                <PropertiesTextEdit>
                    <ValidationSettings>
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="no_sim" VisibleIndex="6" Width="180px" Caption="No. SIM">
                <PropertiesTextEdit>
                    <ValidationSettings>
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="no_hp" VisibleIndex="9" Caption="No. Phone" Width="120px">
                <PropertiesTextEdit>
                    <ValidationSettings>
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataMemoColumn Visible="false" Caption="Alamat" FieldName="alamat" VisibleIndex="21" PropertiesMemoEdit-Height="100px" PropertiesMemoEdit-Width="350px">
                <PropertiesMemoEdit>
                    <ValidationSettings>
                        <RequiredField IsRequired="true" />
                    </ValidationSettings>
                </PropertiesMemoEdit>
                <EditFormSettings Visible="True" />
            </dx:GridViewDataMemoColumn>
            <dx:GridViewDataTextColumn Caption="Keterangan" FieldName="ket" VisibleIndex="14" Width="80px" Visible="False">
                <EditFormSettings Visible="True" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Updated" FieldName="u_date" VisibleIndex="16" Width="80px" ReadOnly="True" Visible="False">
                <PropertiesTextEdit DisplayFormatString="yyyy-MM-dd HH:mm:ss">
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="By" FieldName="u_user" VisibleIndex="21" Width="80px" ReadOnly="True" Visible="False">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="ID" FieldName="id_driver" ReadOnly="True" VisibleIndex="1" Width="60px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataDateColumn Caption="Expired SIM" FieldName="exp_sim" VisibleIndex="8" Width="100px">
                <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd">
                    <ValidationSettings>
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesDateEdit>
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataComboBoxColumn Caption="Lokasi" FieldName="id_lokasi" VisibleIndex="10" Width="120px">
                <PropertiesComboBox>
                    <ValidationSettings>
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>
            <dx:GridViewDataComboBoxColumn Caption="Kategori" FieldName="kategori" VisibleIndex="11" Width="80px" ReadOnly="True" Visible="False">
            </dx:GridViewDataComboBoxColumn>
            <dx:GridViewDataDateColumn Caption="Tgl. Bergabung" FieldName="tgl_masuk" VisibleIndex="3" Width="120px">
                <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd">
                    <ValidationSettings>
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesDateEdit>
            </dx:GridViewDataDateColumn>
            <dx:GridViewCommandColumn ButtonType="Image" Caption="Aktif" VisibleIndex="17" Width="80px">
                <CustomButtons>
                    <dx:GridViewCommandColumnCustomButton ID="bt_aktif">
                    </dx:GridViewCommandColumnCustomButton>
                </CustomButtons>
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn Caption="Masa Kerja (Thn|Bln)" FieldName="masakerja" VisibleIndex="12" Width="80px" Visible="true">
                <EditFormSettings Visible="False" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataComboBoxColumn Caption="Jenis SIM" FieldName="id_jns_sim" VisibleIndex="7" Width="80px">
                <PropertiesComboBox>
                    <ValidationSettings>
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>
            <dx:GridViewDataComboBoxColumn Caption="Pendidikan Terakhir" FieldName="pendidikan" Visible="False" VisibleIndex="13" Width="80px">
                <PropertiesComboBox>
                    <ValidationSettings>
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesComboBox>
                <EditFormSettings Visible="True" />
            </dx:GridViewDataComboBoxColumn>
            <dx:GridViewDataTextColumn Caption="Tgl Non Aktif" FieldName="aktif_date" ReadOnly="True" VisibleIndex="18" Width="80px">
                <EditFormSettings Visible="False" />
                <PropertiesTextEdit DisplayFormatString="yyyy-MM-dd HH:mm:ss">
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="By Non Aktif" FieldName="aktif_user" ReadOnly="True" VisibleIndex="19" Width="80px">
                <EditFormSettings Visible="False" />
            </dx:GridViewDataTextColumn>
        </Columns>
        <Styles>
            <AlternatingRow BackColor="#CCFF99">
            </AlternatingRow>
        </Styles>
        <Templates>
            <PreviewRow>
                <table style="width: 100%">
                    <tr>
                        <td>
                            <b>Alamat</b> : <%#Eval("alamat")%>
                            <br />
                            <b>Keterangan</b> : <%#Eval("ket")%>
                    </tr>
                </table>
            </PreviewRow>
        </Templates>
    </dx:ASPxGridView>
    <dx:ASPxLabel ID="lb_query" runat="server" ForeColor="#CCCCCC" Text=""></dx:ASPxLabel>
    <br />

    <uc1:uc_footer runat="server" ID="uc_footer" />
</asp:Content>
