<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="mds_acc_inc.aspx.vb" Inherits="MDS.mds_acc_inc" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Src="~/uc_header.ascx" TagPrefix="uc1" TagName="uc_header" %>
<%@ Register Src="~/uc_footer.ascx" TagPrefix="uc1" TagName="uc_footer" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:uc_header runat="server" ID="uc_header" />
    <div class="tabbable page-tabs">
        <ul class="nav nav-tabs">
            <li class="active"><a href="#cc" class="hvr-icon-forward" data-toggle="tab"><i class="icon-truck hvr-icon"></i>Accident Incident</a></li>
        </ul>
        <div class="tab-content">
            <div class="tab-pane active fade in" id="cc">
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
                            <dx:ASPxDateEdit ID="de_end" runat="server"  EnableTheming="False" Height="20px" DisplayFormatString="yyyy-MM-dd" EditFormat="Custom" EditFormatString="yyyy-MM-dd">
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
                            <button class="btn btn-success" type="button" runat="server" id="bt_refresh" onserverclick="bt_refresh_ServerClick"><i class="icon-search3"></i>Refresh Data</button>
                        </div>
                    </div>
                </div>
                <dx:ASPxGridView ID="ASPxGridView1" runat="server" Theme="Office365" AutoGenerateColumns="False" Settings-ShowPreview="true">
                    <Columns>
                        <dx:GridViewCommandColumn VisibleIndex="0" Width="100px" ShowEditButton="true" ShowDeleteButton="true" ShowClearFilterButton="true" ShowNewButtonInHeader="true">
                        </dx:GridViewCommandColumn>

                        <dx:GridViewDataHyperLinkColumn Caption="Print" FieldName="id_acc_inc" ReadOnly="True" VisibleIndex="1" Width="80px">
                            <PropertiesHyperLinkEdit TextFormatString="View" NavigateUrlFormatString="page_print.aspx?mode=ACCINC&amp;id={0}" Target="_blank">
                            </PropertiesHyperLinkEdit>
                            <EditFormSettings Visible="False" />
                            <CellStyle HorizontalAlign="Center" />
                        </dx:GridViewDataHyperLinkColumn>

                        <dx:GridViewDataTextColumn Caption="ID" FieldName="id_acc_inc" VisibleIndex="2" Width="80">
                            <EditFormSettings Visible="False" />
                            <CellStyle HorizontalAlign="Center" />
                        </dx:GridViewDataTextColumn>

                        <dx:GridViewDataTextColumn Caption="No. Trs" readonly="true" FieldName="no_trs" VisibleIndex="3" Width="180">
                            <EditFormSettings Visible="true" />
                            <CellStyle HorizontalAlign="Center" />
                        </dx:GridViewDataTextColumn>

                        <dx:GridViewDataComboBoxColumn Caption="Driver" FieldName="id_driver" VisibleIndex="4" Width="130">
                            <PropertiesComboBox>
                                <ValidationSettings>
                                    <RequiredField IsRequired="True" />
                                </ValidationSettings>
                            </PropertiesComboBox>
                            <CellStyle HorizontalAlign="Center" />
                        </dx:GridViewDataComboBoxColumn>

                        <dx:GridViewDataTextColumn Caption="Asal Pool" FieldName="asalpool" VisibleIndex="6" Width="100px" Visible="true">
                            <EditFormSettings Visible="False" />
                            <CellStyle HorizontalAlign="Center" />
                        </dx:GridViewDataTextColumn>

                        <dx:GridViewDataDateColumn Caption="Tgl Kejadian" FieldName="tgl_kejadian" VisibleIndex="7" Width="100px">
                            <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd" EditFormatString="yyyy-MM-dd">
                                <ValidationSettings>
                                    <RequiredField IsRequired="True" />
                                </ValidationSettings>
                            </PropertiesDateEdit>
                            <CellStyle HorizontalAlign="Center" />
                        </dx:GridViewDataDateColumn>

                        <dx:GridViewDataTimeEditColumn Caption="Jam Kejadian" FieldName="jam" VisibleIndex="8" Width="100px">
                            <PropertiesTimeEdit DisplayFormatString="HH:mm" DisplayFormatInEditMode="true" EditFormat="Time" EditFormatString="HH:mm">
                            </PropertiesTimeEdit>
                            <CellStyle HorizontalAlign="Center" />
                        </dx:GridViewDataTimeEditColumn>

                        <dx:GridViewDataComboBoxColumn Caption="Jenis Kasus" FieldName="id_jenis" VisibleIndex="9" Width="100">
                            <PropertiesComboBox>
                                <Items>
                                    <dx:ListEditItem Text="Accident" Value="1" />
                                    <dx:ListEditItem Text="Incident" Value="2" />
                                    <dx:ListEditItem Text="Nearmiss" Value="3" />
                                </Items>
                                <ValidationSettings>
                                    <RequiredField IsRequired="True" />
                                </ValidationSettings>
                            </PropertiesComboBox>
                            <CellStyle HorizontalAlign="Center" />
                        </dx:GridViewDataComboBoxColumn>

                        <dx:GridViewDataComboBoxColumn Caption="Faktor Kecelakaan" FieldName="id_faktor" VisibleIndex="10" Width="100">
                            <PropertiesComboBox>
                                <Items>
                                    <dx:ListEditItem Text="Man" Value="1" />
                                    <dx:ListEditItem Text="Material" Value="2" />
                                    <dx:ListEditItem Text="Machine" Value="3" />
                                     <dx:ListEditItem Text="Methode" Value="4" />
                                    <dx:ListEditItem Text="Environment" Value="5" />
                                </Items>
                                <ValidationSettings>
                                    <RequiredField IsRequired="True" />
                                </ValidationSettings>
                            </PropertiesComboBox>
                            <CellStyle HorizontalAlign="Center" />
                        </dx:GridViewDataComboBoxColumn>

                        <dx:GridViewDataComboBoxColumn Caption="No. Pol" FieldName="id_nopol" VisibleIndex="11" Width="100">
                            <PropertiesComboBox>
                                <ValidationSettings>
                                    <RequiredField IsRequired="True" />
                                </ValidationSettings>
                            </PropertiesComboBox>
                            <CellStyle HorizontalAlign="Center" />
                        </dx:GridViewDataComboBoxColumn>

                        <dx:GridViewDataComboBoxColumn Caption="Status" FieldName="id_status" VisibleIndex="12" Width="100">
                            <PropertiesComboBox>
                                <Items>
                                    <dx:ListEditItem Text="Arah Berangkat" Value="1" />
                                    <dx:ListEditItem Text="Arah Pulang" Value="2" />
                                    <dx:ListEditItem Text="Loading" Value="3" />
                                    <dx:ListEditItem Text="Unloading" Value="4" />
                                </Items>
                                <ValidationSettings>
                                    <RequiredField IsRequired="True" />
                                </ValidationSettings>
                            </PropertiesComboBox>
                            <CellStyle HorizontalAlign="Center" />
                        </dx:GridViewDataComboBoxColumn>

                        <dx:GridViewDataComboBoxColumn Caption="Cuaca" FieldName="id_cuaca" VisibleIndex="13" Width="100">
                            <PropertiesComboBox>
                                <Items>
                                    <dx:ListEditItem Text="Gerimis" Value="1" />
                                    <dx:ListEditItem Text="Cerah" Value="2" />
                                    <dx:ListEditItem Text="Hujan" Value="3" />
                                </Items>
                                <ValidationSettings>
                                    <RequiredField IsRequired="True" />
                                </ValidationSettings>
                            </PropertiesComboBox>
                            <CellStyle HorizontalAlign="Center" />
                        </dx:GridViewDataComboBoxColumn>

                        <dx:GridViewDataMemoColumn Caption="Lokasi Kejadian" FieldName="lokasi" Width="100px" VisibleIndex="14">
                            <PropertiesMemoEdit Height="150px">
                                <ValidationSettings>
                                    <RequiredField IsRequired="true" />
                                </ValidationSettings>
                            </PropertiesMemoEdit>
                            <EditFormSettings Visible="True" />
                            <CellStyle HorizontalAlign="Center" />
                        </dx:GridViewDataMemoColumn>

                        <dx:GridViewDataMemoColumn Caption="Kronologi" FieldName="krono" VisibleIndex="15" Visible="false">
                            <PropertiesMemoEdit Height="150px">
                                <ValidationSettings>
                                    <RequiredField IsRequired="True" />
                                </ValidationSettings>
                            </PropertiesMemoEdit>
                            <EditFormSettings Visible="True" />
                            <CellStyle HorizontalAlign="Center" />
                        </dx:GridViewDataMemoColumn>

                        <dx:GridViewDataMemoColumn Caption="Cause Analysis & Countermeasure" FieldName="cause_analis" VisibleIndex="16" Visible="false">
                            <PropertiesMemoEdit Height="150px">
                                <ValidationSettings>
                                    <RequiredField IsRequired="True" />
                                </ValidationSettings>
                            </PropertiesMemoEdit>
                            <EditFormSettings Visible="True" />
                            <CellStyle HorizontalAlign="Center" />
                        </dx:GridViewDataMemoColumn>

                        <dx:GridViewDataMemoColumn Caption="Kerusakan" FieldName="rusak" VisibleIndex="17" Visible="false">
                            <PropertiesMemoEdit Height="150px">
                                <ValidationSettings>
                                    <RequiredField IsRequired="True" />
                                </ValidationSettings>
                            </PropertiesMemoEdit>
                            <EditFormSettings Visible="True" />
                            <CellStyle HorizontalAlign="Center" />
                        </dx:GridViewDataMemoColumn>

                        <dx:GridViewDataTextColumn Caption="Biaya Kepengurusan" Visible="false" FieldName="biaya" VisibleIndex="18" Width="125px">
                            <CellStyle HorizontalAlign="Center" />
                            <PropertiesTextEdit DisplayFormatString="0,###.##" DisplayFormatInEditMode="true" NullDisplayText="0" NullText="0">
                                <MaskSettings IncludeLiterals="None" />
                                <ValidationSettings>
                                    <RequiredField IsRequired="True" />
                                </ValidationSettings>
                            </PropertiesTextEdit>
                            <EditFormSettings Visible="True" />
                        </dx:GridViewDataTextColumn>

                        <dx:GridViewDataHyperLinkColumn Caption="Image" FieldName="id_acc_inc" VisibleIndex="19" Visible="true" Width="80">
                            <DataItemTemplate>
                                <dx:ASPxHyperLink ID="ASPxHyperLink1" Width="80" runat="server" Text='<%# String.Concat(Eval("ttl_upload"), " Foto")%>' NavigateUrl='<%# String.Format("~/mds_trs_file.aspx?idrec={0}&sumber=ACCINC", Eval("id_acc_inc"))%>'>
                                </dx:ASPxHyperLink>
                            </DataItemTemplate>
                            <EditFormSettings Visible="False" />
                            <PropertiesHyperLinkEdit NavigateUrlFormatString="~/mds_trs_file.aspx?idrec={0}&sumber=ACCINC" Text="">
                            </PropertiesHyperLinkEdit>
                        </dx:GridViewDataHyperLinkColumn>

                        <dx:GridViewDataComboBoxColumn Caption="" FieldName="" VisibleIndex="21" Width="0">
                            <EditFormSettings Visible="false" />
                        </dx:GridViewDataComboBoxColumn>
                    </Columns>

                    <Settings ShowPreview="True"></Settings>

                    <Templates>
                        <PreviewRow>
                            <div class="row">
                                <div class="col-md-12">
                                    <b>Jam & Tgl Kejadian : </b> <%#Format(Eval("tgl_kejadian"), "yyyy-MM-dd")%> , <%#Eval("jam")%><br />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-8">
                                    <b>Kronologi : </b><%#Eval("krono")%><br />
                                    <b>Kerusakan : </b><%#Eval("rusak")%><br />
                                    <b>Biaya Kepengurusan : </b>Rp. <%#Format(Eval("biaya"), "#,###.##")%><br />
                                    <b>Cause Analysis & Countermeasure : </b><%#Eval("cause_analis")%><br />
                                </div>
                                <div class="col-md-4 right">
                                    <b>Updated </b><%#Eval("u_date")%><b> By </b><%#Eval("u_user")%>
                                </div>
                            </div>
                        </PreviewRow>
                    </Templates>
                </dx:ASPxGridView>
            </div>
        </div>
    </div>
    <uc1:uc_footer runat="server" ID="uc_footer" />
</asp:Content>
