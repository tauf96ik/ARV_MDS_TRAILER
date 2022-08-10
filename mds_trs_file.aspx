<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="mds_trs_file.aspx.vb" Inherits="MDS.mds_trs_file" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Src="~/uc_header.ascx" TagPrefix="uc1" TagName="uc_header" %>
<%@ Register Src="~/uc_footer.ascx" TagPrefix="uc1" TagName="uc_footer" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:uc_header runat="server" ID="uc_header" />

    <div class="panel-body">
        <div class="form-group">
            <div class="col-sm-1">
                <label class="control-label">Upload File Image : </label>
            </div>
            <div class="col-sm-3">
                <input type="file" class="styled" runat="server" id="tx_fileimg" />
            </div>
            <div class="col-sm-3">
                <dx:ASPxComboBox ID="cb_doc" runat="server" EnableTheming="False" Height="32px" Width="300px" SelectedIndex="0">
                    <ButtonStyle BackColor="Transparent">
                    </ButtonStyle>
                    <Paddings Padding="6px" />
                    <Border BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
                    <Items>
                        <dx:ListEditItem Text="Photo" Value="Photo" Selected="True" />
                        <dx:ListEditItem Text="KTP" Value="KTP" />
                        <dx:ListEditItem Text="KTP Istri" Value="KTP Istri" />
                        <dx:ListEditItem Text="KK" Value="KK" />
                        <dx:ListEditItem Text="Buku Nikah" Value="Buku Nikah" />
                        <dx:ListEditItem Text="SIM A" Value="SIM A" />
                        <dx:ListEditItem Text="SIM A Umum" Value="SIM A Umum" />
                        <dx:ListEditItem Text="SIM B1" Value="SIM B1" />
                        <dx:ListEditItem Text="SIM B1 Umum" Value="SIM B1 Umum" />
                        <dx:ListEditItem Text="SIM B2" Value="SIM B2" />
                        <dx:ListEditItem Text="SIM B2 Umum" Value="SIM B2 Umum" />
                        <dx:ListEditItem Text="SIO Kartu" Value="SIO Kartu" />
                        <dx:ListEditItem Text="SIO Sertifikat" Value="SIO Sertifikat" />
                        <dx:ListEditItem Text="BPJS" Value="BPJS" />
                        <dx:ListEditItem Text="NPWP" Value="NPWP" />
                        <dx:ListEditItem Text="Driver Punishment" Value="Driver Punishment" />
                        <dx:ListEditItem Text="Defect" Value="Defect" />
                        <dx:ListEditItem Text="Pihak 1" Value="Pihak 1" />
                        <dx:ListEditItem Text="Pihak 2" Value="Pihak 2" />
                        <dx:ListEditItem Text="Absensi" Value="Absensi" />
                        <dx:ListEditItem Text="In Class & Out Class" Value="ICOC" />
                    </Items>
                </dx:ASPxComboBox>
            </div>
            <div class="col-sm-3">
                <input type="text" class="form-control" placeholder="Keterangan ..." runat="server" id="tx_ket"/>
            </div>
            <div class="col-sm-2">
                <input type="button" class="btn btn-primary" value="Upload & Save" runat="server" id="bt_img" onserverclick="bt_img_ServerClick" />
            </div>
        </div>
    </div>
    
    <br />

    <b>>>></b> File Image Upload harus type JPG
    <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" Theme="Office365">
        <Columns>
            <dx:GridViewCommandColumn VisibleIndex="0" Width="80px" ShowDeleteButton="true">
            </dx:GridViewCommandColumn>
            <dx:GridViewDataImageColumn Caption="Image" FieldName="file_nm" VisibleIndex="1" Width="200px">
                <PropertiesImage ImageHeight="100px" ImageUrlFormatString="Files/{0}">
                </PropertiesImage>
            </dx:GridViewDataImageColumn>
            <dx:GridViewDataHyperLinkColumn Caption="File Image" FieldName="file_nm" VisibleIndex="2" Width="250px">
                <PropertiesHyperLinkEdit NavigateUrlFormatString="Files\{0}" Target="_blank" TextField="file_nm">
                </PropertiesHyperLinkEdit>
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataHyperLinkColumn>
            <dx:GridViewDataTextColumn Caption="Jenis" FieldName="jenis" VisibleIndex="3" Width="100px">
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="title_nm" ShowInCustomizationForm="True" Width="100px" Caption="Source File" VisibleIndex="4">
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Keterangan" FieldName="ket" VisibleIndex="5" Width="100px">
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Inserted" FieldName="c_date" VisibleIndex="6" Width="80px">
                <PropertiesTextEdit DisplayFormatString="yyyy-MM-dd HH:mm:ss">
                </PropertiesTextEdit>
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="By" FieldName="c_user" VisibleIndex="7" Width="80px">
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataTextColumn>
        </Columns>
    </dx:ASPxGridView>

    <uc1:uc_footer runat="server" ID="uc_footer" />
</asp:Content>
