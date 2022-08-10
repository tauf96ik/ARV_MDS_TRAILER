<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="mds_surat_kontrak.aspx.vb" Inherits="MDS.mds_surat_kontrak" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Src="~/uc_header.ascx" TagPrefix="uc1" TagName="uc_header" %>
<%@ Register Src="~/uc_footer.ascx" TagPrefix="uc1" TagName="uc_footer" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:uc_header runat="server" ID="uc_header" />
    <div class="form-group">
        <div class="row">
            <div class="col-sm-4">
                <a runat="server" id="fil_2_bln" onserverclick="fil_2_bln_ServerClick" style="font-weight: bold; color: #000000">
                    <span class="label label-danger" runat="server" id="span_2bln">31</span>
                    Kontrak Exp 2 Bulan
                </a>
            </div>
            <div class="col-sm-4">
                <a runat="server" id="fil_1_bln" onserverclick="fil_1_bln_ServerClick" style="font-weight: bold; color: #000000">
                    <span class="label label-danger" runat="server" id="span_1bln">31</span>
                    Kontrak Exp 1 Bulan
                </a>
            </div>
            <div class="col-sm-4">
                <a runat="server" id="fil_1min" onserverclick="fil_1min_ServerClick" style="font-weight: bold; color: #000000">
                    <span class="label label-danger" runat="server" id="span_1min">31</span>
                    Kontrak Exp 1 Minggu
                </a>
            </div>
        </div>
    </div>

    <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="false" Theme="Office365">
        <Columns>
            <dx:GridViewCommandColumn ShowNewButtonInHeader="false" ShowDeleteButton="false" ShowEditButton="True" ShowInCustomizationForm="True" VisibleIndex="0" Width="100px">
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn Caption="No" readonly="true" FieldName="norut" VisibleIndex="1" Width="60px">
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataHyperLinkColumn Caption="Surat Kontrak" FieldName="link" ReadOnly="True" VisibleIndex="2" Width="100px">
                <PropertiesHyperLinkEdit TextFormatString="Print" Text="Print" NavigateUrlFormatString="{0}" Target="_blank">
                </PropertiesHyperLinkEdit>
                <CellStyle HorizontalAlign="Center" />
                <EditFormSettings Visible="False" />
            </dx:GridViewDataHyperLinkColumn>
            <%--<dx:GridViewDataHyperLinkColumn Caption="Surat Administrasi" FieldName="link2" ReadOnly="True" VisibleIndex="3" Width="150px">
                <PropertiesHyperLinkEdit TextFormatString="Print" Text="Print" NavigateUrlFormatString="{0}" Target="_blank">
                </PropertiesHyperLinkEdit>
                <CellStyle HorizontalAlign="Center" />
                <EditFormSettings Visible="False" />
            </dx:GridViewDataHyperLinkColumn>--%>
            <dx:GridViewDataTextColumn Caption="Driver" FieldName="nama" ReadOnly="True" VisibleIndex="3" Width="150px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Alamat" FieldName="alamat" ReadOnly="True" VisibleIndex="4" Width="300px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="No. KTP" FieldName="no_ktp" ReadOnly="True" VisibleIndex="5" Width="150px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Asal Pool" FieldName="lokasi" ReadOnly="True" VisibleIndex="6" Width="100px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataDateColumn Caption="Tanggal Bergabung" FieldName="tgl_masuk" VisibleIndex="7" Width="100px">
                <CellStyle HorizontalAlign="Center" />
                <EditFormSettings Visible="True" />
                <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd">
                    <ClearButton DisplayMode="OnHover" />
                </PropertiesDateEdit>
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataDateColumn Caption="Tanggal Awal Kontrak" FieldName="awal_kontrak" VisibleIndex="8" Width="100px">
                <CellStyle HorizontalAlign="Center" />
                <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd">
                    <ClearButton DisplayMode="OnHover" />
                </PropertiesDateEdit>
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataDateColumn Caption="Tanggal Akhir Kontrak" FieldName="akhir_kontrak" VisibleIndex="9" Width="100px">
                <CellStyle HorizontalAlign="Center" />
                <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd">
                    <ClearButton DisplayMode="OnHover" />
                </PropertiesDateEdit>
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataTextColumn Caption="Updated" FieldName="u_date" ReadOnly="True" Visible="true" VisibleIndex="10" Width="0px">
                <EditFormSettings Visible="False" />
            </dx:GridViewDataTextColumn>
        </Columns>
    </dx:ASPxGridView>
    <dx:ASPxLabel ID="lb_query" runat="server" ForeColor="#CCCCCC" Text=""></dx:ASPxLabel>
    <uc1:uc_footer runat="server" ID="uc_footer" />
</asp:Content>
