<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="mds_rec_apl.aspx.vb" Inherits="MDS.mds_rec_apl" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Src="~/uc_header.ascx" TagPrefix="uc1" TagName="uc_header" %>
<%@ Register Src="~/uc_footer.ascx" TagPrefix="uc1" TagName="uc_footer" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:uc_header runat="server" ID="uc_header" />
    <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" Theme="Office365">
        <Columns>
            <dx:GridViewCommandColumn VisibleIndex="0" Width="100px"  ShowEditButton="true" ShowDeleteButton="true" ShowClearFilterButton="true" ShowNewButtonInHeader="true">
            </dx:GridViewCommandColumn>

            <dx:GridViewDataTextColumn Caption="ID" FieldName="id_rec" ReadOnly="True" VisibleIndex="1" Width="80px">
                <EditFormSettings Visible="False" />
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Nama" FieldName="nama" VisibleIndex="2" Width="150px">
                <PropertiesTextEdit>
                    <ValidationSettings>
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesTextEdit>
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="No. KTP" FieldName="no_ktp" VisibleIndex="3" Width="150px">
                <PropertiesTextEdit>
                    <ValidationSettings>
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Tempat Lahir" FieldName="tmpt_lhr" VisibleIndex="4" Width="100px">
                <PropertiesTextEdit>
                    <ValidationSettings>
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesTextEdit>
                <EditFormSettings Visible="True" />
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataDateColumn Caption="Tgl. Lahir" FieldName="tgl_lahir" VisibleIndex="5" Width="100px">
                <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd">
                </PropertiesDateEdit>
                <EditFormSettings Visible="True" />
            </dx:GridViewDataDateColumn>

            <dx:GridViewDataMemoColumn Caption="Alamat KTP" FieldName="alamat_ktp" Visible="False" VisibleIndex="6" Width="80px">
                <EditFormSettings RowSpan="0" Visible="True" />
            </dx:GridViewDataMemoColumn>

            <dx:GridViewDataMemoColumn Caption="Alamat Domisili" FieldName="alamat_dom" Visible="False" VisibleIndex="7" Width="80px">
                <EditFormSettings RowSpan="0" Visible="True" />
            </dx:GridViewDataMemoColumn>

            <dx:GridViewDataComboBoxColumn Caption="SIM" FieldName="jns_sim" VisibleIndex="8" Width="100px">
                <PropertiesComboBox>
                    <ValidationSettings>
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>

            <dx:GridViewDataTextColumn Caption="No. SIM" FieldName="no_sim" VisibleIndex="9" Width="150px">
                <PropertiesTextEdit>
                    <ValidationSettings>
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataDateColumn Caption="Expired SIM" FieldName="exp_sim" VisibleIndex="10" Width="130px">
                <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd">
                    <ValidationSettings>
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesDateEdit>
            </dx:GridViewDataDateColumn>

            <%--<dx:GridViewDataTextColumn Caption="No. SIO" FieldName="no_sio" VisibleIndex="11" Width="150px">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataDateColumn Caption="Expired SIO" FieldName="exp_sio" VisibleIndex="12" Width="130px">
                <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd">
                </PropertiesDateEdit>
            </dx:GridViewDataDateColumn>--%>

            <dx:GridViewDataComboBoxColumn Caption="Pendidikan Terakhir" FieldName="id_pendidikan" VisibleIndex="13" Width="100px">
                <PropertiesComboBox>
                    <ValidationSettings>
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>

            <dx:GridViewDataComboBoxColumn Caption="Agama" FieldName="agama" VisibleIndex="14" Width="80px">
                <PropertiesComboBox>
                    <Items>
                        <dx:ListEditItem Selected="True" Text="Islam" Value="Islam" />
                        <dx:ListEditItem Text="Protestan" Value="Protestan" />
                        <dx:ListEditItem Text="Katolik" Value="Katolik" />
                        <dx:ListEditItem Text="Hindu" Value="Hindu" />
                        <dx:ListEditItem Text="Budha" Value="Budha" />
                    </Items>
                    <ValidationSettings>
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesComboBox>
                <EditFormSettings Visible="True" />
            </dx:GridViewDataComboBoxColumn>

            <dx:GridViewDataComboBoxColumn Caption="Jenis Kelamin" FieldName="jk" VisibleIndex="15" Width="120px">
                <PropertiesComboBox>
                    <Items>
                        <dx:ListEditItem Selected="True" Text="Pria" Value="Pria" />
                        <dx:ListEditItem Text="Wanita" Value="Wanita" />
                    </Items>
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>

            <dx:GridViewDataComboBoxColumn Caption="Status Nikah" FieldName="status_menikah" VisibleIndex="16" Width="120px">
                <PropertiesComboBox>
                    <Items>
                        <dx:ListEditItem Selected="True" Text="Menikah" Value="Menikah" />
                        <dx:ListEditItem Text="Single" Value="Single" />
                    </Items>
                    <ValidationSettings>
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>

            <dx:GridViewDataComboBoxColumn Caption="Jml. Tanggungan" Width="100px" FieldName="tanggungan" VisibleIndex="17">
                <CellStyle HorizontalAlign="Center" />
                <PropertiesComboBox>
                    <Items>
                        <dx:ListEditItem Text="TK" Value="TK" />
                        <dx:ListEditItem Text="K0" Value="K0" />
                        <dx:ListEditItem Text="K1" Value="K1" />
                        <dx:ListEditItem Text="K2" Value="K2" />
                        <dx:ListEditItem Text="K3" Value="K3" />
                    </Items>
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>

            <dx:GridViewDataComboBoxColumn Caption="Kewarganegaraan" FieldName="warga" VisibleIndex="18" Width="120px">
                <PropertiesComboBox>
                    <Items>
                        <dx:ListEditItem Selected="True" Text="WNI" Value="WNI" />
                        <dx:ListEditItem Text="WNA" Value="WNA" />
                    </Items>
                    <ValidationSettings>
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>

            <dx:GridViewDataComboBoxColumn Caption="Golongan Darah" FieldName="goldar" VisibleIndex="19" Width="120px">
                <PropertiesComboBox ClearButton-DisplayMode="OnHover">
                    <Items>
                        <dx:ListEditItem Text="A" Value="A" />
                        <dx:ListEditItem Text="B" Value="B" />
                        <dx:ListEditItem Text="AB" Value="AB" />
                        <dx:ListEditItem Text="O" Value="O" />
                    </Items>
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>

            <dx:GridViewDataComboBoxColumn Caption="Lokasi" FieldName="id_lokasi" VisibleIndex="20" Width="90px">
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataComboBoxColumn>

            <dx:GridViewDataTextColumn Caption="Perusahaan" FieldName="perusahaan" VisibleIndex="21" Width="100px">
                <EditFormSettings Visible="true" />
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Departement" Visible="true" FieldName="dept" VisibleIndex="22" Width="100px">
                <CellStyle HorizontalAlign="Center" />
                <EditFormSettings Visible="true" />
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataComboBoxColumn Caption="Jabatan" FieldName="kategori" VisibleIndex="23" Width="100px">
                <PropertiesComboBox ValidationSettings-RequiredField-IsRequired="true">
                    <Items>
                        <dx:ListEditItem Text="BOX DRIVER" Value="BOX DRIVER" />
                        <dx:ListEditItem Text="Driver Wingbox" Value="Driver Wingbox" />
                        <dx:ListEditItem Text="Driver Trailer" Value="Driver Trailer" />
                    </Items>
                </PropertiesComboBox>
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataComboBoxColumn>

            <dx:GridViewDataComboBoxColumn Caption="Kategori" FieldName="jabatan" VisibleIndex="30" Width="100px">
                <PropertiesComboBox>
                    <Items>
                        <dx:ListEditItem Text="Reguler" Value="Reguler" />
                        <dx:ListEditItem Text="TMMIN" Value="TMMIN" />
                    </Items>
                </PropertiesComboBox>
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataComboBoxColumn>

            <dx:GridViewDataMemoColumn Caption="Pengalaman Kerja" Visible="false" FieldName="pengalaman_kerja" VisibleIndex="24" Width="100px">
                <EditFormSettings Visible="True" />
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataMemoColumn>

            <dx:GridViewDataTextColumn Caption="E-Mail" FieldName="email" Visible="False" VisibleIndex="25" Width="100px">
                <CellStyle HorizontalAlign="Center" />
                <EditFormSettings Visible="True" />
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn FieldName="no_hp" VisibleIndex="26" Caption="No. Phone" Width="120px">
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataComboBoxColumn Caption="Rekening Bank" FieldName="rek_bank" VisibleIndex="27" Width="100px">
                <PropertiesComboBox>
                    <Items>
                        <dx:ListEditItem Text="BCA" Value="BCA" />
                        <dx:ListEditItem Text="BNI" Value="BNI" />
                        <dx:ListEditItem Text="Mandiri" Value="Mandiri" />
                        <dx:ListEditItem Text="BRI" Value="BRI" />
                        <dx:ListEditItem Text="Permata" Value="Permata" />
                        <dx:ListEditItem Text="HSBC" Value="HSBC" />
                        <dx:ListEditItem Text="MEGA" Value="MEGA" />
                        <dx:ListEditItem Text="Bank DKI" Value="Bank DKI" />
                        <dx:ListEditItem Text="Bank Lampung" Value="Bank Lampung" />
                        <dx:ListEditItem Text="-" Value="-" />
                    </Items>
                </PropertiesComboBox>
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataComboBoxColumn>

            <dx:GridViewBandColumn Caption="No. Kartu" VisibleIndex="28">
                <Columns>
                    <dx:GridViewDataTextColumn Caption="No. Rek" FieldName="rek_no" VisibleIndex="28" Width="120px">
                        <CellStyle HorizontalAlign="Center" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="BPJS TK" FieldName="no_bpjs_tk" VisibleIndex="28" Width="100px">
                        <CellStyle HorizontalAlign="Center" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="BPJS Kes" FieldName="bpjs_no" VisibleIndex="28" Width="100px">
                        <CellStyle HorizontalAlign="Center" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="NPWP" FieldName="npwp" VisibleIndex="28" Width="150px">
                        <CellStyle HorizontalAlign="Center" />
                    </dx:GridViewDataTextColumn>
                </Columns>
            </dx:GridViewBandColumn>

            <dx:GridViewDataDateColumn Caption="Tgl. Melamar" FieldName="lamar_date" VisibleIndex="29" Width="100px">
                <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd">
                </PropertiesDateEdit>
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataDateColumn>

            <dx:GridViewDataTextColumn Caption="Reff Masuk" Width="90px" FieldName="reff_masuk" VisibleIndex="30">
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataTextColumn>

            <dx:GridViewCommandColumn ButtonType="Image" Caption="Terima" VisibleIndex="31" Width="80px" ShowClearFilterButton="true">
                <CustomButtons>
                    <dx:GridViewCommandColumnCustomButton ID="bt_trm">
                    </dx:GridViewCommandColumnCustomButton>
                </CustomButtons>
            </dx:GridViewCommandColumn>

            <dx:GridViewDataTextColumn Caption="Updated" Width="90px" FieldName="u_date" VisibleIndex="32">
                <CellStyle HorizontalAlign="Center" />
                <EditFormSettings Visible="false" />
            </dx:GridViewDataTextColumn>
        </Columns>
        <Templates>
            <PreviewRow>
                <table style="width: 100%">
                    <tr>
                        <td style="text-align: right">
                            <b>Updated </b><%#Eval("u_date")%><b> By </b><%#Eval("u_user")%>
                        </td>
                    </tr>
                </table>
                <table style="width: 100%">
                    <tr>
                        <td>
                            <b>Alamat KTP : </b><%#Eval("alamat_ktp")%>
                        </td>
                        <td style="text-align: right">
                            <b>Terima Sta </b><%#Eval("terima_date")%><b> By </b><%#Eval("terima_user")%>
                        </td>
                    </tr>
                </table>
                <table style="width: 100%">
                    <tr>
                        <td>
                            <b>Alamat Tinggal : </b><%#Eval("alamat_dom")%>
                        </td>
                    </tr>
                </table>
                <table style="width: 100%">
                    <tr>
                        <td>
                            <b>Pengalaman Kerja : </b><%#Eval("pengalaman_kerja")%>
                        </td>
                    </tr>
                </table>
            </PreviewRow>
        </Templates>
    </dx:ASPxGridView>
    <uc1:uc_footer runat="server" ID="uc_footer" />
</asp:Content>
