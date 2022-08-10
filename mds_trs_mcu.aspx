<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="mds_trs_mcu.aspx.vb" Inherits="MDS.mds_trs_mcu" %>

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
                    <CalendarProperties Columns="1">
                    </CalendarProperties>
                    <ButtonStyle BackColor="Transparent">
                    </ButtonStyle>
                    <Paddings Padding="6px" />
                    <Border BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
                </dx:ASPxDateEdit>
            </div>
            <div class="col-sm-2">
                <dx:ASPxDateEdit ID="e_date" runat="server" EnableTheming="False" Height="20px" DisplayFormatString="yyyy-MM-dd" EditFormat="Custom" EditFormatString="yyyy-MM-dd">
                    <CalendarProperties Columns="1">
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
    <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" Theme="Office365">
        <Columns>
            <dx:GridViewCommandColumn VisibleIndex="0" Width="100px" ShowEditButton="true" ShowDeleteButton="true" ShowClearFilterButton="true" ShowNewButtonInHeader="true">
            </dx:GridViewCommandColumn>

            <dx:GridViewDataTextColumn Caption="ID" FieldName="id_mcu" Width="80px" EditFormSettings-Visible="False" VisibleIndex="1">
                <CellStyle HorizontalAlign="Center" />
                <EditFormSettings Visible="False"></EditFormSettings>
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataDateColumn Caption="Tanggal" FieldName="tgl" VisibleIndex="2" Width="100px">
                <CellStyle HorizontalAlign="Center" />
                <PropertiesDateEdit>
                    <ValidationSettings>
                        <RequiredField IsRequired="true" />
                    </ValidationSettings>
                </PropertiesDateEdit>
            </dx:GridViewDataDateColumn>

            <dx:GridViewDataComboBoxColumn Caption="No Polisi" FieldName="id_nopol" VisibleIndex="3" Width="100px">
                <CellStyle HorizontalAlign="Center" />
                <PropertiesComboBox>
                    <ValidationSettings>
                        <RequiredField IsRequired="true" />
                    </ValidationSettings>
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>

            <dx:GridViewDataComboBoxColumn Caption="Tujuan" FieldName="id_kab" VisibleIndex="4" Width="100px">
                <CellStyle HorizontalAlign="Center" />
                <PropertiesComboBox>
                    <ValidationSettings>
                        <RequiredField IsRequired="true" />
                    </ValidationSettings>
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>

            <dx:GridViewDataComboBoxColumn Caption="Driver" FieldName="id_spr" VisibleIndex="5" Width="180px">
                <CellStyle HorizontalAlign="Center" />
                <PropertiesComboBox>
                    <ValidationSettings>
                        <RequiredField IsRequired="true" />
                    </ValidationSettings>
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>

            <dx:GridViewDataTextColumn Caption="Asal Driver" Width="100px" FieldName="asal_driver" VisibleIndex="6">
                <CellStyle HorizontalAlign="Center" />
                <EditFormSettings Visible="False" />
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Tekanan Darah (mmHg)" FieldName="tekanan_drh" VisibleIndex="6" Width="100px" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true" CellStyle-HorizontalAlign="Center" CellStyle-VerticalAlign="Middle">
                <PropertiesTextEdit DisplayFormatInEditMode="True" DisplayFormatString="# 'mmHg'">
                    <MaskSettings IncludeLiterals="None" />
                    <ValidationSettings>
                        <RequiredField IsRequired="True" />
                     </ValidationSettings>
                </PropertiesTextEdit>
                <CellStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Status Tekanan Darah" FieldName="nm_sta_tekanan_drh" Visible="true" VisibleIndex="7" Width="80px" EditFormSettings-Visible="False" CellStyle-HorizontalAlign="Center" CellStyle-VerticalAlign="Middle">
                <PropertiesTextEdit Style-HorizontalAlign="Center" Style-VerticalAlign="Middle">
                    <Style HorizontalAlign="Center" VerticalAlign="Middle" />
                </PropertiesTextEdit>
                <EditFormSettings Visible="False" />
                <CellStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Suhu Badan" FieldName="suhu_bdn" VisibleIndex="8" Width="80px" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true" CellStyle-HorizontalAlign="Center" CellStyle-VerticalAlign="Middle">
                <PropertiesTextEdit DisplayFormatInEditMode="true" DisplayFormatString="0.0 °C">
                    <MaskSettings IncludeLiterals="None" />
                    <ValidationSettings>
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesTextEdit>
                <CellStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Status Suhu Badan" FieldName="nm_sta_suhu_bdn" VisibleIndex="9" Visible="true" Width="80px" EditFormSettings-Visible="False" CellStyle-HorizontalAlign="Center" CellStyle-VerticalAlign="Middle">
                <PropertiesTextEdit Style-HorizontalAlign="Center" Style-VerticalAlign="Middle">
                    <Style HorizontalAlign="Center" VerticalAlign="Middle" />
                </PropertiesTextEdit>
                <EditFormSettings Visible="False" />
                <CellStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataMemoColumn Caption="Ket." FieldName="ket" Visible="true" VisibleIndex="10" Width="100px" PropertiesMemoEdit-Width="250px" PropertiesMemoEdit-Height="150px">
                <PropertiesMemoEdit Width="250px" Height="150px"></PropertiesMemoEdit>
            </dx:GridViewDataMemoColumn>

            <dx:GridViewDataTextColumn Caption="Updated" FieldName="u_date" VisibleIndex="12" Width="130px" EditFormSettings-Visible="False">
                <PropertiesTextEdit DisplayFormatString="yyyy-MM-dd HH:mm:ss">
                </PropertiesTextEdit>
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="By" FieldName="u_user" VisibleIndex="13" Width="80px" EditFormSettings-Visible="False">
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="By" FieldName="u_user" VisibleIndex="14" Width="80px" EditFormSettings-Visible="False">
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataTextColumn>
        </Columns>
    </dx:ASPxGridView>
    <uc1:uc_footer runat="server" ID="uc_footer" />
</asp:Content>
