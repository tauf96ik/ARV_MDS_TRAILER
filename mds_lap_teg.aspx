<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="mds_lap_teg.aspx.vb" Inherits="MDS.mds_lap_teg" %>

<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Src="~/uc_header.ascx" TagPrefix="uc1" TagName="uc_header" %>
<%@ Register Src="~/uc_footer.ascx" TagPrefix="uc1" TagName="uc_footer" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:uc_header runat="server" ID="uc_header" />
    <div class="form-group">
        <label>Periode Date : </label>
        <div class="row">
            <div class="col-sm-2">
                <dx:ASPxDateEdit ID="s_date" runat="server" EnableTheming="False" Height="20px" DisplayFormatString="yyyy-MM-dd" EditFormat="Custom" EditFormatString="yyyy-MM-dd">
                    <CalendarProperties Columns="3">
                    </CalendarProperties>
                    <ButtonStyle BackColor="Transparent">
                    </ButtonStyle>
                    <Paddings Padding="6px" />
                    <Border BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
                </dx:ASPxDateEdit>
            </div>
            <div class="col-sm-2">
                <dx:ASPxDateEdit ID="e_date" runat="server" EnableTheming="False" Height="20px" DisplayFormatString="yyyy-MM-dd" EditFormat="Custom" EditFormatString="yyyy-MM-dd">
                    <CalendarProperties Columns="3">
                    </CalendarProperties>
                    <ButtonStyle BackColor="Transparent">
                    </ButtonStyle>
                    <Paddings Padding="6px" />
                    <Border BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
                </dx:ASPxDateEdit>
            </div>
            <div class="col-sm-2">
                <button class="btn btn-success" type="button" runat="server" id="bt_refresh" onserverclick="bt_refresh_ServerClick"><i class="icon-search3"></i>Filter</button>
            </div>
        </div>
    </div>
    <dx:ASPxPivotGrid ID="ASPxPivotGrid1" runat="server">
        <Fields>
            <dx:PivotGridField ID="fieldsupir" Area="RowArea" AreaIndex="0" Caption="Nama Driver" FieldName="nama_driver">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldjabawan" Area="RowArea" AreaIndex="1" Caption="Jabatan" FieldName="nm_jbtn">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldjenis" Area="RowArea" AreaIndex="2" Caption="Jenis Teguran" FieldName="jenis">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldqty" Area="DataArea" AreaIndex="0" Caption="Qty" FieldName="qty">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldtgl" Area="FilterArea" AreaIndex="1" Caption="Tanggal" FieldName="tgl">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldtglawal" Area="FilterArea" AreaIndex="2" FieldName="tgl_berlaku" Caption="Tgl. Berlaku Hukuman">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldtglahir" Area="FilterArea" AreaIndex="3" FieldName="tgl_selesai" Caption="Tgl. Selesai Hukuman">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldalasan" Area="FilterArea" AreaIndex="4" Caption="Alasan" FieldName="alasan">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldcatatan" Area="FilterArea" AreaIndex="5" Caption="Catatan" FieldName="catatan">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldnoba" Area="FilterArea" AreaIndex="6" Caption="No. Berita Acara" FieldName="no_ba">
            </dx:PivotGridField>
        </Fields>
    </dx:ASPxPivotGrid>
    <uc1:uc_footer runat="server" ID="uc_footer" />
</asp:Content>
