<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="mds_driver_active.aspx.vb" Inherits="MDS.mds_driver_active" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Src="~/uc_header.ascx" TagPrefix="uc1" TagName="uc_header" %>
<%@ Register Src="~/uc_footer.ascx" TagPrefix="uc1" TagName="uc_footer" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <uc1:uc_header runat="server" ID="uc_header" />
    <dx:ASPxGridView ID="ASPxGridView1" runat="server" Theme="Office365" AutoGenerateColumns="False">
        <Columns>
            <dx:GridViewCommandColumn VisibleIndex="0" Width="100px" ShowDeleteButton="true" ShowEditButton="true" ShowNewButtonInHeader="true">
            </dx:GridViewCommandColumn>

            <dx:GridViewCommandColumn ButtonType="Image" Caption="Aktif" VisibleIndex="1" Width="60px" ShowClearFilterButton="true">
                <CustomButtons>
                    <dx:GridViewCommandColumnCustomButton ID="bt_aktif">
                    </dx:GridViewCommandColumnCustomButton>
                </CustomButtons>
            </dx:GridViewCommandColumn>

            <%--<dx:GridViewDataHyperLinkColumn Caption="ID" FieldName="id_driver" VisibleIndex="2" Width="80px" ReadOnly="True">
                <PropertiesHyperLinkEdit NavigateUrlFormatString="mds_data_driver.aspx?id_supir={0}" TextField="id_driver" TextFormatString="{0}">
                </PropertiesHyperLinkEdit>
                <EditFormSettings Visible="False" />
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataHyperLinkColumn>--%>

            <dx:GridViewDataTextColumn Caption="ID" FieldName="id_driver" VisibleIndex="2" Width="80px">
                <EditFormSettings Visible="False" />
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="NIK" FieldName="nik" VisibleIndex="3" Width="100px">
                <EditFormSettings Visible="False" />
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataColumn Caption="No. Regis Absen" FieldName="no_absen" Visible="false" VisibleIndex="4" Width="80px">
                <CellStyle HorizontalAlign="Center" />
                <EditFormSettings Visible="true" />
            </dx:GridViewDataColumn>

            <dx:GridViewDataTextColumn Caption="Nama Driver" FieldName="nama" VisibleIndex="5" Width="150px">
                <PropertiesTextEdit DisplayFormatString="{0}">
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="No. KTP" FieldName="no_ktp" VisibleIndex="6" Width="150px">
                <CellStyle HorizontalAlign="Center" />
                <PropertiesTextEdit>
                    <ValidationSettings><RequiredField IsRequired="true" /></ValidationSettings>
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataMemoColumn FieldName="alamat" Visible="false" VisibleIndex="7" Caption="Alamat KTP" Width="200px" >
                <PropertiesMemoEdit>
                    <ValidationSettings>
                        <RequiredField IsRequired="true" />
                    </ValidationSettings>
                </PropertiesMemoEdit>
                <EditFormSettings Visible="True" />
            </dx:GridViewDataMemoColumn>

            <dx:GridViewDataMemoColumn FieldName="alamat_domisili" Visible="false" VisibleIndex="8" Caption="Alamat Domisili" Width="200px" >
                <PropertiesMemoEdit>
                    <ValidationSettings>
                        <RequiredField IsRequired="true" />
                    </ValidationSettings>
                </PropertiesMemoEdit>
                <EditFormSettings Visible="True" />
            </dx:GridViewDataMemoColumn>

            <dx:GridViewDataTextColumn FieldName="no_hp" ShowInCustomizationForm="True" Width="120px" Caption="No. HP" VisibleIndex="9">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Tempat Lahir" FieldName="tmpt_lhr"  VisibleIndex="10" Width="100px">
                <CellStyle HorizontalAlign="Center" />
                <EditFormSettings Visible="True" />
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataDateColumn Caption="Tgl. Lahir" FieldName="tgl_lahir" VisibleIndex="11" Width="100px">
                <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd">
                    <ValidationSettings>
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesDateEdit>
            </dx:GridViewDataDateColumn>

            <dx:GridViewDataTextColumn Caption="Usia (Thn|Bln)" FieldName="umur" ReadOnly="True" VisibleIndex="12" Width="80px">
                <EditFormSettings Visible="False" />
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataComboBoxColumn Caption="Jenis Kelamin" FieldName="jk" VisibleIndex="13" Width="100px">
                <PropertiesComboBox>
                    <Items>
                        <dx:ListEditItem Text="L" Value="Pria" />
                        <dx:ListEditItem Text="P" Value="Wanita" />
                    </Items>
                </PropertiesComboBox>
                <CellStyle HorizontalAlign="Center" />
                <EditFormSettings Visible="True" />
            </dx:GridViewDataComboBoxColumn>

            <dx:GridViewDataComboBoxColumn Caption="Agama" FieldName="agama" Visible="True" VisibleIndex="14" Width="100px">
                <PropertiesComboBox>
                    <Items>
                        <dx:ListEditItem Text="Islam" Value="Islam" />
                        <dx:ListEditItem Text="Protestan" Value="Protestan" />
                        <dx:ListEditItem Text="Katolik" Value="Katolik" />
                        <dx:ListEditItem Text="Buddha" Value="Buddha" />
                        <dx:ListEditItem Text="Hindu" Value="Hindu" />
                        <dx:ListEditItem Text="Konghucu" Value="Konghucu" />
                    </Items>
                </PropertiesComboBox>
                <CellStyle HorizontalAlign="Center" />
                <EditFormSettings Visible="True" />
            </dx:GridViewDataComboBoxColumn>

            <dx:GridViewDataComboBoxColumn Caption="Kewarganegaraan" FieldName="warga" VisibleIndex="15" Width="120px">
                <PropertiesComboBox>
                    <Items>
                        <dx:ListEditItem Selected="True" Text="WNI" Value="WNI" />
                        <dx:ListEditItem Text="WNA" Value="WNA" />
                    </Items>
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>

            <dx:GridViewDataComboBoxColumn Caption="Golongan Darah" FieldName="goldar" VisibleIndex="16" Width="120px">
                <PropertiesComboBox ClearButton-DisplayMode="OnHover">
                    <Items>
                        <dx:ListEditItem Text="A" Value="A" />
                        <dx:ListEditItem Text="B" Value="B" />
                        <dx:ListEditItem Text="AB" Value="AB" />
                        <dx:ListEditItem Text="O" Value="O" />
                    </Items>
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>

            <dx:GridViewDataComboBoxColumn Caption="Jenis SIM" FieldName="id_jns_sim" VisibleIndex="17" Width="100px">
                <PropertiesComboBox>
                    <ValidationSettings>
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>

            <dx:GridViewDataTextColumn FieldName="no_sim" VisibleIndex="18" Width="120px" Caption="No. SIM">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataDateColumn Caption="Expired SIM" FieldName="exp_sim" VisibleIndex="19" Width="100px">
                <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd">
                    <ValidationSettings>
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesDateEdit>
            </dx:GridViewDataDateColumn>

            <dx:GridViewDataTextColumn Caption="Status Driver" FieldName="status_kerja" VisibleIndex="20" Width="100px">
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataDateColumn Caption="Tgl. Melamar" FieldName="lamar_date" VisibleIndex="21" Width="100px">
                <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd">
                </PropertiesDateEdit>
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataDateColumn>

            <dx:GridViewDataDateColumn Caption="Tgl. Masuk" FieldName="tgl_masuk" VisibleIndex="22" Width="100px">
                <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd">
                </PropertiesDateEdit>
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataDateColumn>

            <dx:GridViewDataDateColumn Caption="Tgl. Awal Kontrak" FieldName="awal_kontrak" VisibleIndex="23" Width="100px">
                <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd">
                </PropertiesDateEdit>
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataDateColumn>

            <dx:GridViewDataDateColumn Caption="Tgl. Akhir Kontrak" FieldName="akhir_kontrak" VisibleIndex="24" Width="100px">
                <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd">
                </PropertiesDateEdit>
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataDateColumn>

            <dx:GridViewDataTextColumn Caption="Masa Kerja (Thn|Bln)" FieldName="masakerja" VisibleIndex="25" Width="80px" ReadOnly="True">
                <EditFormSettings Visible="False" />
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataComboBoxColumn Caption="Lokasi" FieldName="id_lokasi" VisibleIndex="26" Width="100px">
                <PropertiesComboBox>
                    <ValidationSettings>
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesComboBox>
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataComboBoxColumn>

            <dx:GridViewDataTextColumn Caption="Perusahaan" FieldName="perusahaan" VisibleIndex="27" Width="100px">
                <EditFormSettings Visible="true" />
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Departement" Visible="true" FieldName="dept" VisibleIndex="28" Width="100px">
                <CellStyle HorizontalAlign="Center" />
                <EditFormSettings Visible="true" />
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataComboBoxColumn Caption="Jabatan" FieldName="kategori" VisibleIndex="29" Width="100px">
                <PropertiesComboBox>
                    <Items>
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

            <dx:GridViewDataComboBoxColumn Caption="Bank Rek" FieldName="rek_bank" VisibleIndex="30" Width="80px">
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
                    <ValidationSettings>
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>

            <dx:GridViewBandColumn Caption="No. Kartu" VisibleIndex="31">
                <Columns>
                    <dx:GridViewDataTextColumn Caption="No. Rek" FieldName="rek_no" VisibleIndex="31" Width="120px">
                        <CellStyle HorizontalAlign="Center" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="BPJS TK" FieldName="no_bpjs_tk" VisibleIndex="31" Width="100px">
                        <CellStyle HorizontalAlign="Center" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="BPJS Kes" FieldName="bpjs_no" VisibleIndex="31" Width="100px">
                        <CellStyle HorizontalAlign="Center" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="No. Garda" FieldName="garda_no" VisibleIndex="31" Width="100px">
                        <CellStyle HorizontalAlign="Center" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataDateColumn Caption="Tanggal Garda" FieldName="tgl_garda" VisibleIndex="31" Width="100px">
                        <CellStyle HorizontalAlign="Center" />
                    </dx:GridViewDataDateColumn>
                    <dx:GridViewDataTextColumn Caption="NPWP" FieldName="npwp" VisibleIndex="31" Width="150px">
                        <CellStyle HorizontalAlign="Center" />
                    </dx:GridViewDataTextColumn>
                </Columns>
            </dx:GridViewBandColumn>
            
            <dx:GridViewDataComboBoxColumn Caption="Pendidikan Terakhir" FieldName="pendidikan" VisibleIndex="32" Width="100px">
                <PropertiesComboBox>
                    <ValidationSettings>
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesComboBox>
                <EditFormSettings Visible="True" />
            </dx:GridViewDataComboBoxColumn>
            
            <dx:GridViewDataMemoColumn Caption="Pengalaman Kerja" Visible="false" FieldName="pengalaman_kerja" VisibleIndex="33" Width="100px">
                <EditFormSettings Visible="True" />
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataMemoColumn>

            <dx:GridViewDataTextColumn Caption="E-Mail" FieldName="email" Visible="False" VisibleIndex="34" Width="100px">
                <CellStyle HorizontalAlign="Center" />
                <EditFormSettings Visible="True" />
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataComboBoxColumn Caption="Status Menikah" FieldName="perkawinan"  Width="100px" VisibleIndex="35">
                <PropertiesComboBox>
                    <Items>
                        <dx:ListEditItem Text="Single" Value="Single" />
                        <dx:ListEditItem Text="Menikah" Value="Menikah" />
                    </Items>
                </PropertiesComboBox>
                <EditFormSettings Visible="True" />
            </dx:GridViewDataComboBoxColumn>

            <dx:GridViewDataComboBoxColumn Caption="Jml. Tanggungan" Width="100px" FieldName="tanggungan" VisibleIndex="36">
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

            <dx:GridViewDataTextColumn Caption="Size Seragam" Width="90px" FieldName="size_seragam" VisibleIndex="37">
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Size Sepatu" Width="90px" FieldName="size_sepatu" VisibleIndex="38">
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Suhu Badan" Width="90px" FieldName="suhu_badan" VisibleIndex="39">
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Tekanan Darah" Width="90px" FieldName="tekanan_darah" VisibleIndex="40">
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Reff Masuk" Width="90px" FieldName="reff_masuk" VisibleIndex="41">
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataTextColumn>
            
            <dx:GridViewDataHyperLinkColumn Caption="Doc" FieldName="id_driver" VisibleIndex="42" Width="100px">
                <DataItemTemplate>
                    <dx:ASPxHyperLink ID="ASPxHyperLink1" Target="_blank" runat="server" Text='<%#String.Concat(Eval("upload"), " Doc. Upload")%>' NavigateUrl='<%#String.Format("~/mds_trs_file.aspx?idrec={0}&sumber=DOCSUPIR", Eval("id_driver")) %>' >
                    </dx:ASPxHyperLink>
                </DataItemTemplate>
                <EditFormSettings Visible="False" />
            </dx:GridViewDataHyperLinkColumn>

            <dx:GridViewDataHyperLinkColumn Caption="Keluarga" FieldName="id_driver" VisibleIndex="43" Width="100px">
                <DataItemTemplate>
                    <dx:ASPxHyperLink ID="ASPxHyperLink2" Target="_blank" runat="server" Text='<%#String.Concat(Eval("jml_fam"), " Keluarga")%>' NavigateUrl='<%#String.Format("~/mds_driver_family.aspx?idspr={0}", Eval("id_driver")) %>' >
                    </dx:ASPxHyperLink>
                </DataItemTemplate>
                <CellStyle HorizontalAlign="Center" />
                <EditFormSettings Visible="False" />
            </dx:GridViewDataHyperLinkColumn>

            <dx:GridViewDataTextColumn Caption="Keterangan" FieldName="ket" VisibleIndex="44" Width="100px">
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataTextColumn>
        </Columns>
        <Settings ShowPreview="true" />
        <Templates>
            <PreviewRow>
                <table style="width: 100%">
                    <tr>
                        <td>Alamat KTP : <%#Eval("alamat") %>
                            <br />
                            Alamat Domisili : <%#Eval("alamat_domisili") %>
                            <br />
                            Keterangan : <%#Eval("ket") %>
                        </td>
                        <td>
                            Pengalaman Kerja : <%#Eval("pengalaman_kerja") %>
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
