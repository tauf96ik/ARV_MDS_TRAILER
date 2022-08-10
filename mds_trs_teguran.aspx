<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="mds_trs_teguran.aspx.vb" Inherits="MDS.mds_trs_teguran" %>

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

            <dx:GridViewDataTextColumn Caption="ID" FieldName="id_teguran" VisibleIndex="1" Width="60px" ReadOnly="True">
                <CellStyle HorizontalAlign="Center" />
                <EditFormSettings Visible="False" />
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataDateColumn Caption="Tanggal" FieldName="tgl" VisibleIndex="2" Width="100px" ReadOnly="True">
                <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd">
                </PropertiesDateEdit>
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataDateColumn>

            <dx:GridViewDataComboBoxColumn Caption="Nama Supir" FieldName="id_spr" VisibleIndex="3" Width="150px">
                <CellStyle HorizontalAlign="Center" />
                <PropertiesComboBox>
                    <ValidationSettings>
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>

            <dx:GridViewDataTextColumn Caption="Jabatan" FieldName="nm_jbtn" VisibleIndex="4" Width="100px">
                <EditFormSettings Visible="False" />
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataComboBoxColumn Caption="No Berita Acara" FieldName="id_trs_ba" VisibleIndex="5" Width="150px">
                <CellStyle HorizontalAlign="Center" />
                <EditFormSettings Visible="True" />
                <PropertiesComboBox EnableCallbackMode="true">
                    <ClearButton DisplayMode="OnHover" />
                    <ValidationSettings>
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>

            <dx:GridViewDataComboBoxColumn Caption="Jenis" FieldName="id_teguran_jns" VisibleIndex="6" Width="150px">
                <CellStyle HorizontalAlign="Center" />
                <PropertiesComboBox>
                    <ValidationSettings>
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>

            <dx:GridViewDataMemoColumn Caption="Alasan" FieldName="alasan" VisibleIndex="7" Width="300px">
                <%--<EditFormSettings RowSpan="2" />--%>
            </dx:GridViewDataMemoColumn>

            <dx:GridViewDataMemoColumn Caption="Catatan" FieldName="catatan" VisibleIndex="8" Width="300px">
                <%--<EditFormSettings RowSpan="2" />--%>
            </dx:GridViewDataMemoColumn>

            <dx:GridViewDataDateColumn Caption="Tgl. Berlaku" FieldName="tgl_berlaku" VisibleIndex="9" Width="120px">
                <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd">
                    <ValidationSettings>
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesDateEdit>
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataDateColumn>

            <dx:GridViewDataDateColumn Caption="Tgl. Selesai" FieldName="tgl_selesai" VisibleIndex="10" Width="120px">
                <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd">
                    <ValidationSettings>
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesDateEdit>
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataDateColumn>

            <dx:GridViewDataHyperLinkColumn Caption="Print" PropertiesHyperLinkEdit-ClientInstanceName="Print" FieldName="id_teguran" VisibleIndex="11" Width="80px" Settings-AllowAutoFilter ="false">
                <PropertiesHyperLinkEdit Target="_blank" NavigateUrlFormatString="~/page_print.aspx?id={0}&amp;mode=teguran" Text="Print">
                </PropertiesHyperLinkEdit>
                <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                </CellStyle>
                <Settings AllowAutoFilter="False"></Settings>
                <EditFormSettings Visible="False" />
            </dx:GridViewDataHyperLinkColumn>

            <dx:GridViewDataDateColumn Caption="Updated" FieldName="u_date" ReadOnly="true" VisibleIndex="12">
                <PropertiesDateEdit DisplayFormatString="yyyy-mm-dd HH:mm:ss">
                </PropertiesDateEdit>
                <CellStyle HorizontalAlign="Center" />
                <EditFormSettings Visible="False" />
            </dx:GridViewDataDateColumn>

            <dx:GridViewDataTextColumn Caption="By" FieldName="u_user" ReadOnly="true" VisibleIndex="13">
                <CellStyle HorizontalAlign="Center" />
                <EditFormSettings Visible="False" />
            </dx:GridViewDataTextColumn>

            <dx:GridViewBandColumn Caption="Approve" VisibleIndex="10" Visible="false">
                <Columns>
                    <dx:GridViewCommandColumn ButtonType="Image" Caption="SPV" VisibleIndex="9" Width="50px" ShowClearFilterButton="true">
                        <CustomButtons>
                            <dx:GridViewCommandColumnCustomButton ID="spv_sta" Text="SPV">
                            </dx:GridViewCommandColumnCustomButton>
                        </CustomButtons>
                    </dx:GridViewCommandColumn>
                </Columns>
            </dx:GridViewBandColumn>
        </Columns>
    </dx:ASPxGridView>
    <uc1:uc_footer runat="server" ID="uc_footer" />
</asp:Content>
