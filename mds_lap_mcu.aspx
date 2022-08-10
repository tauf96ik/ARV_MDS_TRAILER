<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="mds_lap_mcu.aspx.vb" Inherits="MDS.mds_lap_mcu" %>

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
    <dx:ASPxPivotGrid ID="ASPxPivotGrid1" runat="server" ClientIDMode="AutoID">
        <Fields>
            <dx:PivotGridField ID="fielddriver1" Area="RowArea" AreaIndex="0" Caption="Driver" FieldName="driver" Width="200" RunningTotal="True" TotalsVisibility="None" Options-ShowTotals="True" Options-ShowGrandTotal="True" GrandTotalCellFormat-FormatType="Numeric" SortBySummaryInfo-FieldName="kondisi">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldjabatan" AreaIndex="0" Caption="Jabatan" FieldName="job_title" TotalsVisibility="None" Width="100">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldPool" AreaIndex="1" Caption="Asal Pool" FieldName="lokasi" TotalsVisibility="None" Width="100" CellFormat-FormatType="Custom" Options-ShowGrandTotal="False">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldtgl" AreaIndex="8" Caption="Tanggal" FieldName="tgl" TotalsVisibility="None" ValueFormat-FormatString="yyyy-MM-dd" ValueFormat-FormatType="Custom">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldtujuan" AreaIndex="2" Caption="Tujuan" FieldName="tujuan" TotalsVisibility="None" Width="180">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldnopol" AreaIndex="3" Caption="No. Pol." FieldName="nopol" TotalsVisibility="None">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldtekanandrh" AreaIndex="5" Caption="Tekanan Darah" FieldName="tekanan_drh" TotalsVisibility="None" Options-ShowGrandTotal="False" >
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldsuhubdn" AreaIndex="6" Caption="Suhu Badan" FieldName="suhu_bdn" TotalsVisibility="None" CellFormat-FormatType="Custom" Options-ShowGrandTotal="False">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldkondisi" Area="RowArea" AreaIndex="1" Caption="Kondisi Suhu" FieldName="kondisi_suhu" Options-ShowGrandTotal="False">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldkondisitkn" Area="RowArea" AreaIndex="1" Caption="Kondisi Tekanan Darah" FieldName="kondisi_tekanan" Options-ShowGrandTotal="False">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldqty" Area="DataArea" AreaIndex="0" Caption="Qty" FieldName="qty" TotalCellFormat-FormatType="Numeric" TotalValueFormat-FormatType="Numeric" Options-ShowGrandTotal="False">
            </dx:PivotGridField>
        </Fields>
        <OptionsPager PagerAlign="Right">
            <PageSizeItemSettings Visible="true" ShowAllItem="true" Position="Right">
            </PageSizeItemSettings>
        </OptionsPager>
    </dx:ASPxPivotGrid>
    <uc1:uc_footer runat="server" ID="uc_footer" />
</asp:Content>
