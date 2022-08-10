<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="mon_image.aspx.vb" Inherits="MDS.mon_image" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>



<%@ Register Src="~/uc_header.ascx" TagPrefix="uc1" TagName="uc_header" %>
<%@ Register Src="~/uc_footer.ascx" TagPrefix="uc1" TagName="uc_footer" %>





<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:uc_header runat="server" ID="uc_header" />

    <style type="text/css">
        .gambar {
            max-width: 100%;
            max-height: 100%;
        }
    </style>

    <dx:ASPxUploadControl Width="280px" runat="server" ID="auc_files" ShowAddRemoveButtons="True" ShowProgressPanel="True" ShowUploadButton="True">
    </dx:ASPxUploadControl>
    <br />

    <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False">
        <Columns>
            <dx:GridViewDataTextColumn FieldName="id_supir" VisibleIndex="0" Width="100px" Caption="ID. Driver">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="driver_id" VisibleIndex="1" Width="100px" Caption="NIK Driver">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Nama Driver" FieldName="nama" VisibleIndex="2" Width="300px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Image" FieldName="sta_img" VisibleIndex="3" Width="100px">
            </dx:GridViewDataTextColumn>
        </Columns>
        <Templates>
            <PreviewRow>
                <table>
                    <tr>
                        <td rowspan="4">
                            <div class="block">
                                <div class="thumbnail" style="width: 120px">
                                    <a href='<%#Eval("alamat_img")%>' class="thumb-zoom lightbox" title='<%#Eval("nama")%>' runat="server" id="a_supir">
                                        <img src='<%#Eval("alamat_img")%>' alt="" runat="server" id="img_supir" class="gambar" />
                                    </a>
                                </div>
                            </div>
                        </td>
                        <td style="width: 40px"></td>
                        <td>Jabatan : <%#Eval("nm_jbtn") %></td>
                    </tr>
                    <tr>
                        <td style="width: 40px"></td>
                        <td>Lokasi :</td>
                    </tr>
                    <tr>
                        <td style="width: 40px"></td>
                        <td>Masa Kerja : </td>
                    </tr>
                    <tr>
                        <td style="width: 40px"></td>
                        <td>Tanggal Bergabung :</td>
                    </tr>
                </table>

            </PreviewRow>
        </Templates>
    </dx:ASPxGridView>

    <uc1:uc_footer runat="server" ID="uc_footer" />
</asp:Content>
