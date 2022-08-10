<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="mds_driver_doc.aspx.vb" Inherits="MDS.mds_driver_doc" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Src="~/uc_header.ascx" TagPrefix="uc1" TagName="uc_header" %>
<%@ Register Src="~/uc_footer.ascx" TagPrefix="uc1" TagName="uc_footer" %>





<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
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
                        <Border BorderStyle="None" />
                    </ButtonStyle>
                    <Paddings Padding="6px" />
                    <Border BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
                    <Items>
                        <dx:ListEditItem Text="Photo" Value="Photo" Selected="True" />
                        <dx:ListEditItem Text="KTP" Value="KTP" />
                        <dx:ListEditItem Text="SIM A" Value="SIM A" />
                        <dx:ListEditItem Text="SIM A Umum" Value="SIM A Umum" />
                        <dx:ListEditItem Text="SIM B1" Value="SIM B1" />
                        <dx:ListEditItem Text="SIM B1 Umum" Value="SIM B1 Umum" />
                        <dx:ListEditItem Text="SIM B2" Value="SIM B2" />
                        <dx:ListEditItem Text="SIM B2 Umum" Value="SIM B2 Umum" />
                        <dx:ListEditItem Text="CV" Value="CV" />
                        <dx:ListEditItem Text="KK" Value="KK" />
                        <dx:ListEditItem Text="S. Nikah" Value="S. Nikah" />
                        <dx:ListEditItem Text="S. Domisili" Value="S. Domisili" />
                        <dx:ListEditItem Text="SKCK" Value="SKCK" />
                        <dx:ListEditItem Text="Fc. Garda" Value="Fc. Garda" />
                        <dx:ListEditItem Text="Fc. BPJS TK" Value="Fc. BPJS TK" />
                        <dx:ListEditItem Text="S. Kesehatan" Value="S. Kesehatan" />
                        <dx:ListEditItem Text="Paklaring" Value="Paklaring" />
                        <dx:ListEditItem Text="Ijazah" Value="Ijazah" />
                        <dx:ListEditItem Text="SKK" Value="SKK" />
                        <dx:ListEditItem Text="Form Interview" Value="Form Interview" />
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

    <asp:FileUpload ID="FileUpload1" runat="server" Visible="False" />
    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
    <br />

    <b>>>></b> File Image Upload harus type JPG
    <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False">
        <Columns>
            <dx:GridViewCommandColumn VisibleIndex="0" Width="80px">
                <DeleteButton Visible="True">
                </DeleteButton>
            </dx:GridViewCommandColumn>
            <dx:GridViewDataImageColumn Caption="Image" FieldName="file_nm" VisibleIndex="1" Width="200px">
                <PropertiesImage ImageHeight="100px" ImageUrlFormatString="doc_supir/{0}">
                </PropertiesImage>
            </dx:GridViewDataImageColumn>
            <dx:GridViewDataHyperLinkColumn Caption="File Image" FieldName="file_nm" VisibleIndex="2" Width="170px">
                <PropertiesHyperLinkEdit NavigateUrlFormatString="doc_supir\{0}" Target="_blank" TextField="file_nm">
                </PropertiesHyperLinkEdit>
            </dx:GridViewDataHyperLinkColumn>
            <dx:GridViewDataTextColumn Caption="Jenis" FieldName="jenis" VisibleIndex="3" Width="100px">
            </dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="title_file" ShowInCustomizationForm="True" Width="100px" Caption="Source File" VisibleIndex="4"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Keterangan" FieldName="ket" VisibleIndex="5" Width="100px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Inserted" FieldName="c_date" VisibleIndex="6" Width="80px">
                <PropertiesTextEdit DisplayFormatString="yyyy-MM-dd HH:mm:ss">
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="By" FieldName="c_user" VisibleIndex="7" Width="80px">
            </dx:GridViewDataTextColumn>
        </Columns>
    </dx:ASPxGridView>

    <uc1:uc_footer runat="server" ID="uc_footer" />
</asp:Content>

