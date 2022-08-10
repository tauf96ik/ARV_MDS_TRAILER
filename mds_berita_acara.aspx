<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="mds_berita_acara.aspx.vb" Inherits="MDS.mds_berita_acara" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Src="~/uc_header.ascx" TagPrefix="uc1" TagName="uc_header" %>
<%@ Register Src="~/uc_footer.ascx" TagPrefix="uc1" TagName="uc_footer" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:uc_header runat="server" ID="uc_header" />

    <div class="panel-group">
        <div class="form-group">
            <div class="row">
                <div class="col-sm-2">
                    <label>Start Date : </label>
                    <dx:ASPxDateEdit ID="de_start" runat="server" EnableTheming="False" Height="20px" DisplayFormatString="yyyy-MM-dd" EditFormat="Custom" EditFormatString="yyyy-MM-dd">
                        <CalendarProperties Columns="1">
                        </CalendarProperties>
                        <ButtonStyle BackColor="Transparent">
                        </ButtonStyle>
                        <Paddings Padding="6px" />
                        <Border BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
                    </dx:ASPxDateEdit>
                </div>
                <div class="col-sm-2">
                    <label>End Date : </label>
                    <dx:ASPxDateEdit ID="de_end" runat="server" EnableTheming="False" Height="20px" DisplayFormatString="yyyy-MM-dd" EditFormat="Custom" EditFormatString="yyyy-MM-dd">
                        <CalendarProperties Columns="1">
                        </CalendarProperties>
                        <ButtonStyle BackColor="Transparent">
                        </ButtonStyle>
                        <Paddings Padding="6px" />
                        <Border BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
                    </dx:ASPxDateEdit>
                </div>
                <div class="col-sm-2">
                    <br />
                    <button class="btn btn-primary" type="button" runat="server" id="bt_refresh" onserverclick="bt_refresh_ServerClick"><i class="icon-search3"></i>Refresh Data</button>
                </div>
            </div>
        </div>
        <div class="panel-body">
            <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" Theme="Office365">
                <Columns>
                    <dx:GridViewCommandColumn VisibleIndex="0" Width="100px" ShowEditButton="true" ShowDeleteButton="true" ShowClearFilterButton="true" ShowNewButtonInHeader="true">
                    </dx:GridViewCommandColumn>

                    <dx:GridViewDataHyperLinkColumn Caption="ID" FieldName="id_trs_ba" ReadOnly="True" VisibleIndex="1" Width="80px">
                        <CellStyle HorizontalAlign="Center" />
                        <PropertiesHyperLinkEdit TextFormatString="View {0}" NavigateUrlFormatString="page_print.aspx?mode=BA&amp;id={0}&amp;sdr=0" Target="_blank">
                        </PropertiesHyperLinkEdit>
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataHyperLinkColumn>

                    <dx:GridViewDataTextColumn Caption="No. B.A" FieldName="no_ba" ReadOnly="True" VisibleIndex="2" Width="150px">
                        <PropertiesTextEdit DisplayFormatString="{0}">
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataDateColumn Caption="Tanggal" FieldName="tgl" ReadOnly="True" VisibleIndex="3" Width="100px">
                        <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd">
                            <ValidationSettings>
                                <RequiredField IsRequired="True" />
                            </ValidationSettings>
                        </PropertiesDateEdit>
                    </dx:GridViewDataDateColumn>

                    <dx:GridViewDataDateColumn Caption="Tgl. Kejadian" FieldName="tgl_kejadian" VisibleIndex="4" Width="110px">
                        <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd">
                            <ValidationSettings>
                                <RequiredField IsRequired="True" />
                            </ValidationSettings>
                        </PropertiesDateEdit>
                    </dx:GridViewDataDateColumn>

                    <dx:GridViewDataComboBoxColumn Caption="Jenis B.A" FieldName="id_ba" VisibleIndex="5" Width="90px">
                        <PropertiesComboBox>
                            <ValidationSettings>
                                <RequiredField IsRequired="True" />
                            </ValidationSettings>
                        </PropertiesComboBox>
                    </dx:GridViewDataComboBoxColumn>

                    <dx:GridViewDataComboBoxColumn Caption="Nama Supir" FieldName="id_driver" VisibleIndex="6" Width="130px">
                        <PropertiesComboBox>
                            <ValidationSettings>
                                <RequiredField IsRequired="True" />
                            </ValidationSettings>
                        </PropertiesComboBox>
                    </dx:GridViewDataComboBoxColumn>

                    <dx:GridViewDataComboBoxColumn Caption="Nama Supir 2" FieldName="id_driver2" VisibleIndex="7" Width="120px">
                        <EditFormSettings VisibleIndex="7" />
                    </dx:GridViewDataComboBoxColumn>

                    <dx:GridViewDataTextColumn Caption="Asal Pool" FieldName="lokasi" ReadOnly="True" VisibleIndex="8" Width="100px">
                        <CellStyle HorizontalAlign="Center" />
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataComboBoxColumn Caption="No. Polisi" FieldName="id_nopol" VisibleIndex="9" Width="100px">
                        <PropertiesComboBox>
                            <ValidationSettings>
                                <RequiredField IsRequired="True" />
                            </ValidationSettings>
                        </PropertiesComboBox>
                    </dx:GridViewDataComboBoxColumn>

                    <dx:GridViewBandColumn Caption="Info Unit" VisibleIndex="10">
                        <Columns>
                            <dx:GridViewDataTextColumn Caption="Model" FieldName="unit_nm" VisibleIndex="10" Width="100px">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Frame" FieldName="unit_frame" VisibleIndex="11" Width="170px">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Defect" FieldName="unit_defect" VisibleIndex="12" Width="100px">
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:GridViewBandColumn>

                    <dx:GridViewDataComboBoxColumn Caption="Tujuan" FieldName="id_kab" VisibleIndex="13" Width="100px">
                    </dx:GridViewDataComboBoxColumn>

                    <dx:GridViewDataTextColumn Caption="No. Referensi" FieldName="no_reff" VisibleIndex="14">
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataHyperLinkColumn Caption="Images" FieldName="id_trs_ba" VisibleIndex="15" Width="90px" ReadOnly="True">
                        <PropertiesHyperLinkEdit NavigateUrlFormatString="~/mds_trs_file.aspx?sumber=BA&idrec={0}" Target="_blank" TextField="jml_img" TextFormatString="{0} Image">
                        </PropertiesHyperLinkEdit>
                        <CellStyle HorizontalAlign="Center" />
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataHyperLinkColumn>

                    <dx:GridViewDataTextColumn Caption="By" FieldName="u_user" ReadOnly="True" VisibleIndex="16" Width="80px" Visible="False">
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn Caption="Updated" FieldName="u_date" ReadOnly="True" VisibleIndex="17" Width="80px" Visible="False">
                        <PropertiesTextEdit DisplayFormatString="yyyy-MM-dd HH:mm:ss">
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>

                    <%--<dx:GridViewDataTextColumn FieldName="clmnA" Caption="A" ReadOnly="True" Visible="False" VisibleIndex="18">
                        <EditFormSettings CaptionLocation="None" ColumnSpan="2" Visible="True" />
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataMemoColumn Caption="Column A" FieldName="clmn_a" VisibleIndex="19" Width="120px" Visible="False">
                        <EditFormSettings ColumnSpan="2" RowSpan="0" Visible="True" CaptionLocation="None" />
                    </dx:GridViewDataMemoColumn>--%>

                    <dx:GridViewDataTextColumn FieldName="clmnB" Caption="B" ReadOnly="True" Visible="False" VisibleIndex="20">
                        <EditFormSettings CaptionLocation="None" ColumnSpan="2" Visible="True" />
                    </dx:GridViewDataTextColumn>
                    
                    <dx:GridViewDataMemoColumn Caption="Column B" FieldName="clmn_b" VisibleIndex="21" Width="120px" Visible="False">
                        <EditFormSettings ColumnSpan="2" RowSpan="0" Visible="True" CaptionLocation="None" />
                    </dx:GridViewDataMemoColumn>

                    <dx:GridViewDataTextColumn FieldName="clmnC" Caption="C" ReadOnly="True" Visible="False" VisibleIndex="22">
                        <EditFormSettings CaptionLocation="None" ColumnSpan="2" Visible="True" />
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataMemoColumn Caption="Column C" FieldName="clmn_c" VisibleIndex="23" Width="120px" Visible="False">
                        <EditFormSettings ColumnSpan="2" RowSpan="0" Visible="True" CaptionLocation="None" />
                    </dx:GridViewDataMemoColumn>

                    <dx:GridViewDataTextColumn FieldName="clmnD" Caption="D" ReadOnly="True" Visible="False" VisibleIndex="24">
                        <EditFormSettings CaptionLocation="None" ColumnSpan="2" Visible="True" />
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataMemoColumn Caption="Column D" FieldName="clmn_d" Visible="False" VisibleIndex="25" Width="120px">
                        <PropertiesMemoEdit ClientInstanceName="clmn_d"></PropertiesMemoEdit>
                        <EditFormSettings ColumnSpan="2" RowSpan="0" Visible="True" CaptionLocation="None" />
                    </dx:GridViewDataMemoColumn>

                    <dx:GridViewDataTextColumn FieldName="clmnE" Caption="E" ReadOnly="True" Visible="False" VisibleIndex="26">
                        <EditFormSettings CaptionLocation="None" ColumnSpan="2" Visible="True" />
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataMemoColumn Caption="Column E" FieldName="clmn_e" Visible="False" VisibleIndex="27" Width="120px">
                        <EditFormSettings ColumnSpan="2" RowSpan="0" Visible="True" CaptionLocation="None" />
                    </dx:GridViewDataMemoColumn>

                    <dx:GridViewDataTextColumn FieldName="clmnF" Caption="F" ReadOnly="True" Visible="False" VisibleIndex="28">
                        <EditFormSettings CaptionLocation="None" ColumnSpan="2" Visible="True" />
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataMemoColumn Caption="Column F" FieldName="clmn_f" Visible="False" VisibleIndex="29" Width="120px">
                        <EditFormSettings ColumnSpan="2" RowSpan="0" Visible="True" CaptionLocation="None" />
                    </dx:GridViewDataMemoColumn>
                </Columns>
                <Templates>
                    <PreviewRow>
                        <b>B : </b><%#Eval("clmn_b")%><br />
                        <b>C : </b><%#Eval("clmn_c")%><br />
                        <b>D : </b><%#Eval("clmn_d")%><br />
                        <b>E : </b><%#Eval("clmn_e")%><br />
                        <b>F : </b><%#Eval("clmn_f")%><br />
                        <b>Updated </b><%#Eval("u_date")%><b> By </b><%#Eval("u_user")%>
                    </PreviewRow>
                </Templates>
            </dx:ASPxGridView>
        </div>
    </div>

    <uc1:uc_footer runat="server" ID="uc_footer" />
</asp:Content>
