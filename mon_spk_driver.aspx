<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="mon_spk_driver.aspx.vb" Inherits="MDS.mon_spk_driver" %>

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
    <dx:ASPxGridView ID="ASPxGridView1" runat="server" Theme="Office365">
        <Columns>
            <dx:GridViewDataTextColumn Caption="No" FieldName="norut" ReadOnly="True" VisibleIndex="0" Width="60px">
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="No. Surat Jalan" FieldName="no_sj" ReadOnly="True" VisibleIndex="1" Width="200px">
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataDateColumn Caption="Tanggal Berangkat" FieldName="tgl" ReadOnly="True" VisibleIndex="3" Width="100px">
                <CellStyle HorizontalAlign="Center" />
                <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd" />
            </dx:GridViewDataDateColumn>
            <%--<dx:GridViewDataTextColumn Caption="SPK Mode" FieldName="spk_mode" ReadOnly="True" VisibleIndex="4" Width="100px">
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataTextColumn>--%>
            <dx:GridViewDataTextColumn Caption="Sumber SJ" FieldName="sumber" ReadOnly="True" VisibleIndex="5" Width="100px">
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Trayek" FieldName="trayek" ReadOnly="True" VisibleIndex="6" Width="100px">
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Asal" FieldName="asal" ReadOnly="True" VisibleIndex="7" Width="100px">
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Tujuan" FieldName="tujuan" ReadOnly="True" VisibleIndex="8" Width="100px">
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Customer" FieldName="customer_nm" ReadOnly="True" VisibleIndex="9" Width="150px">
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="No Polisi" FieldName="nopol" ReadOnly="True" VisibleIndex="10" Width="100px">
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Supir 1" FieldName="supir1" ReadOnly="True" VisibleIndex="11" Width="150px">
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataTextColumn>
            <%--<dx:GridViewDataTextColumn Caption="Asal Pool" FieldName="lok" ReadOnly="True" VisibleIndex="12" Width="100px">
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataTextColumn>--%>
            <dx:GridViewDataTextColumn Caption="Harga Jual" FieldName="harga_jual" ReadOnly="True" VisibleIndex="13" Width="120px">
                <PropertiesTextEdit DisplayFormatString="#,###">
                </PropertiesTextEdit>
                <EditFormSettings Visible="False" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Harga Jual" FieldName="harga_jual" ReadOnly="True" VisibleIndex="13" Width="120px">
                <PropertiesTextEdit DisplayFormatString="#,###">
                </PropertiesTextEdit>
                <EditFormSettings Visible="False" />
            </dx:GridViewDataTextColumn>
        </Columns>
    </dx:ASPxGridView>
    <uc1:uc_footer runat="server" ID="uc_footer" />
</asp:Content>
