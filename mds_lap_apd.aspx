<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="mds_lap_apd.aspx.vb" Inherits="MDS.mds_lap_apd" %>

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
                <button class="btn btn-success" type="button" runat="server" id="bt_refresh" onserverclick="bt_refresh_ServerClick"><i class="icon-search3"></i>Resfresh Data</button>
            </div>
        </div>
    </div>
    <dx:ASPxPivotGrid ID="ASPxPivotGrid1" runat="server" ClientIDMode="AutoID">
        <Fields>
            <dx:PivotGridField ID="fielddriver1" Area="RowArea" AreaIndex="0" Caption="Driver" FieldName="driver" Width="200" RunningTotal="True" TotalsVisibility="None" Options-ShowTotals="True" Options-ShowGrandTotal="True" GrandTotalCellFormat-FormatType="Numeric" SortBySummaryInfo-FieldName="kondisi">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldapd" Area="RowArea" AreaIndex="1" Caption="APD" FieldName="apd" Width="100">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldsize" Area="RowArea" AreaIndex="2" Caption="Ukuran" FieldName="ukuran" Width="100">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldtgl" AreaIndex="8" Caption="Tanggal" FieldName="tgl" ValueFormat-FormatString="yyyy-MM-dd">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldqty" Area="DataArea" AreaIndex="0" Caption="Qty" FieldName="qty" TotalCellFormat-FormatType="Numeric" TotalValueFormat-FormatType="Numeric" Options-ShowGrandTotal="False">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldnopol" AreaIndex="1" Caption="No. Polisi" FieldName="nopol" TotalsVisibility="None">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldstaf" AreaIndex="2" Caption="Staf Penyerah" FieldName="staf" TotalsVisibility="None">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldket" AreaIndex="3" Caption="Keterangan" FieldName="ket" TotalsVisibility="None">
            </dx:PivotGridField>
        </Fields>
        <OptionsPager PagerAlign="Right">
            <PageSizeItemSettings Visible="true" ShowAllItem="true" Position="Right">
            </PageSizeItemSettings>
        </OptionsPager>
    </dx:ASPxPivotGrid>
    <uc1:uc_footer runat="server" ID="uc_footer" />
</asp:Content>
