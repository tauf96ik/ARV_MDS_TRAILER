<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="mds_lap_training.aspx.vb" Inherits="MDS.mds_lap_training" %>

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
            <dx:PivotGridField ID="fielddriver" Area="RowArea" AreaIndex="0" Caption="Driver" FieldName="supir" Options-ShowCustomTotals="True">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldpre" Area="DataArea" AreaIndex="1" Caption="Pre Test" FieldName="nilai_awal">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldpost" Area="DataArea" AreaIndex="2" Caption="Post Test" FieldName="nilai_akhir">
            </dx:PivotGridField>
            <%--<dx:PivotGridField ID="fieldprakpre" Area="DataArea" AreaIndex="3" Caption="Pra Praktek" FieldName="nilai_praktek_pre">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldprakpost" Area="DataArea" AreaIndex="4" Caption="Post Praktek" FieldName="nilai_praktek_post">
            </dx:PivotGridField>--%>
            <dx:PivotGridField ID="fieldnilai" Area="DataArea" AreaIndex="5" Caption="Nilai Akhir" FieldName="nilai">
            </dx:PivotGridField>
            <%--<dx:PivotGridField ID="fieldgrade" Area="DataArea" AreaIndex="6" Caption="Grade" FieldName="grade">
            </dx:PivotGridField>--%>
            <dx:PivotGridField ID="fieldtgl" Area="FilterArea" AreaIndex="2" Caption="Tanggal" FieldName="tgl_aktual">
                <CellStyle HorizontalAlign="Center" />
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldplanning" Area="FilterArea" AreaIndex="1" Caption="Planning" FieldName="planning">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldactual" Area="FilterArea" AreaIndex="2" Caption="Actual" FieldName="minggu_act">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldket" Area="FilterArea" AreaIndex="3" Caption="Predikat" FieldName="ket">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldmateri" Area="FilterArea" AreaIndex="4" Caption="Materi" FieldName="materi">
            </dx:PivotGridField>
            <dx:PivotGridField ID="fieldtglexp" Area="FilterArea" AreaIndex="5" Caption="Tanggal Expired" FieldName="tgl_exp">
            </dx:PivotGridField>
        </Fields>
    </dx:ASPxPivotGrid>
    <uc1:uc_footer runat="server" ID="uc_footer" />
</asp:Content>
