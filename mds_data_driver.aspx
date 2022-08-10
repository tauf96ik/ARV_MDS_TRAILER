<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="mds_data_driver.aspx.vb" Inherits="MDS.mds_data_driver" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Src="~/uc_header.ascx" TagPrefix="uc1" TagName="uc_header" %>
<%@ Register Src="~/uc_footer.ascx" TagPrefix="uc1" TagName="uc_footer" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:uc_header runat="server" ID="uc_header" />
    <br />
    <div class="col-lg-12 col-md-12 col-sm-12">
        <div class="row">
            <div class="panel panel-primary">
		                <div class="panel-heading">
	                    	<h6 class="panel-title"><i class="icon-accessibility"></i> Data Driver</h6>
	                    	<div class="dropdown pull-right">
		                    	<a href="#" class="dropdown-toggle btn btn-link btn-icon" data-toggle="dropdown">
			                    	<i class="icon-cog3"></i> 
			                    	<b class="caret"></b>
		                    	</a>
								<ul class="dropdown-menu icons-right dropdown-menu-right">
									<li><a href="#"><i class="icon-cogs"></i> This is</a></li>
									<li><a href="#"><i class="icon-grid3"></i> Dropdown</a></li>
									<li><a href="#"><i class="icon-spinner7"></i> With right</a></li>
									<li><a href="#"><i class="icon-link"></i> Aligned icons</a></li>
								</ul>
	                    	</div>
                    	</div>
                <div class="container">
                    <div class="panel-body">
	                    	
                            <div class="row">
                                <table style="width:100%;">
                                    <tr>
                                        <td rowspan="8">

                                            <dx:ASPxImage ID="img_supir" Width="200px" Height="350px" EmptyImage-Height="300px" EmptyImage-Width="200px"
                                                 runat="server" AlternateText="Photo" ImageUrl="doc_supir\{0}"></dx:ASPxImage>

                                        </td>
                                        <td>Nama Lengkap : </td>
                                        <td><dx:ASPxTextBox ReadOnly="true" ID="tx_nama" runat="server" Width="170px"></dx:ASPxTextBox></td>
                                    </tr>
                                    <tr>
                                        <td>Tanggal Lahir : </td>
                                        <td>
                                            <%--<dx:ASPxTextBox ReadOnly="true" ID="tx_tempat_lhr" runat="server" Width="100px"></dx:ASPxTextBox>--%>
                                            <dx:ASPxDateEdit ReadOnly="true" ID="tx_tgl_lhr" runat="server"></dx:ASPxDateEdit>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Jenis Kelamin : </td>
                                        <td><dx:ASPxComboBox ID="cb_jk" ReadOnly="true" runat="server" SelectedIndex="0">
                                            <Items>
                                                <dx:ListEditItem Selected="True" Text="Pria" Value="Pria" />
                                                <dx:ListEditItem Text="Wanita" Value="Wanita" />
                                            </Items>
                                            </dx:ASPxComboBox></td>
                                    </tr>
                                    <tr>
                                        <td>No. KTP : </td>
                                        <td><dx:ASPxTextBox ReadOnly="true" ID="tx_no_ktp" runat="server" Width="170px"></dx:ASPxTextBox></td>
                                    </tr>
                                   <%-- <tr>
                                        <td>Agama : </td>
                                        <td><dx:ASPxComboBox ID="cb_agama" ReadOnly="true" runat="server" SelectedIndex="0">
                                            <Items>
                                                <dx:ListEditItem Selected="True" Text="Islam" Value="Islam" />
                                                <dx:ListEditItem Text="Protestan" Value="Protestan" />
                                                <dx:ListEditItem Text="Katolik" Value="Katolik" />
                                                <dx:ListEditItem Text="Hindu" Value="Hindu" />
                                                <dx:ListEditItem Text="Buddha" Value="Buddha" />
                                                <dx:ListEditItem Text="Kong Huchu" Value="Kong Huchu" />
                                                <dx:ListEditItem Text="Kejawen" Value="Kejawen" />
                                                <dx:ListEditItem Text="Animisme" Value="Animisme" />
                                                <dx:ListEditItem Text="Dinamisme" Value="Dinamisme" />
                                            </Items>
                                            </dx:ASPxComboBox></td>
                                    </tr>--%>
                                    <%--<tr>
                                        <td>Status Perkawinan : </td>
                                        <td><dx:ASPxComboBox ReadOnly="true" ID="cb_perkawinan" runat="server" SelectedIndex="0">
                                            <Items>
                                                <dx:ListEditItem Selected="True" Text="Kawin" Value="Kawin" />
                                                <dx:ListEditItem Text="Janda" Value="Janda" />
                                                <dx:ListEditItem Text="Duda" Value="Duda" />
                                                <dx:ListEditItem Text="Single" Value="Single" />
                                            </Items>
                                            </dx:ASPxComboBox></td>
                                    </tr>--%>
                                    <%--<tr>
                                        <td>Kewarganegaraan : </td>
                                        <td><dx:ASPxComboBox ReadOnly="true" ID="cb_kewarganegaraan" runat="server" SelectedIndex="0">
                                            <Items>
                                                <dx:ListEditItem Selected="True" Text="WNI" Value="WNI" />
                                                <dx:ListEditItem Text="WNA" Value="WNA" />
                                            </Items>
                                            </dx:ASPxComboBox></td>
                                    </tr>--%>
                                    <tr>
                                        <td>No. Telp / HP : </td>
                                        <td><dx:ASPxTextBox ReadOnly="true" ID="tx_phone" runat="server" Width="170px"></dx:ASPxTextBox></td>
                                    </tr>
                                </table>
                            </div>
                            <hr />
                            <div class="row">

                                <table style="width:100%;">
                                    <tr>
                                        <th colspan="2">Alamat</th>
                                    </tr>
                                    <tr>
                                        <td>Alamat : </td>
                                        <td><dx:ASPxTextBox ReadOnly="true" ID="tx_alamat" runat="server" Width="350px"></dx:ASPxTextBox></td>
                                    </tr>
                                    <tr>
                                         <td>Pendidikan Terakhir : </td>
                                        <td>
                                            <dx:ASPxTextBox ReadOnly="true" ID="tx_pendidikan" runat="server" Width="170px"></dx:ASPxTextBox>
                                               </td>
                                    </tr>
                                   <%-- <tr>
                                        <td>RT / RW : </td>
                                        <td><dx:ASPxTextBox ReadOnly="true" ID="tx_rtrw" runat="server" Width="350px"></dx:ASPxTextBox></td>
                                    </tr>
                                    <tr>
                                        <td>Kelurahan / Desa : </td>
                                        <td><dx:ASPxTextBox ReadOnly="true" ID="tx_kelurahan" runat="server" Width="350px"></dx:ASPxTextBox></td>
                                    </tr>
                                    <tr>
                                        <td>Kecamatan : </td>
                                        <td><dx:ASPxTextBox ReadOnly="true" ID="tx_kecamatan" runat="server" Width="350px"></dx:ASPxTextBox></td>
                                    </tr>--%>
                                </table>

                            </div>
                            <hr />
                            <div class="row">

                                <table style="width:100%;">
                                    <tr>
                                        <th colspan="4">Keterangan Mitra Kerja</th>
                                    </tr>
                                    <tr>
                                        <%--<td>Jabatan : </td>
                                        <td><dx:ASPxTextBox ReadOnly="true" ID="tx_jbtn" runat="server" Width="170px"></dx:ASPxTextBox></td>--%>

                                        <%--<td><dx:ASPxComboBox ID="cb_jabatan" runat="server" SelectedIndex="0">
                                            <Items>
                                                <dx:ListEditItem Text="Driver 1" Value="1" />
                                                <dx:ListEditItem Text="Driver 2" Value="2" />
                                                <dx:ListEditItem Text="Asisten" Value="3" />
                                                <dx:ListEditItem Text="Driver Towing" Value="4" />
                                                <dx:ListEditItem Text="Self Driver" Value="5" />

                                            </Items>
                                            </dx:ASPxComboBox></td>--%>
                                        <td>Lama Bekerja : </td>
                                        <td><dx:ASPxTextBox ReadOnly="true" ID="tx_lama_kerja" runat="server" Width="170px"></dx:ASPxTextBox></td>
                                    </tr>
                                    <tr>
                                        <td>SIM : </td>
                                        <td><dx:ASPxTextBox ReadOnly="true" ID="tx_jns_sim" runat="server" Width="170px"></dx:ASPxTextBox></td>

<%--                                        <td><dx:ASPxComboBox ReadOnly="true" ID="cb_sim" runat="server" ValueType="System.String"></dx:ASPxComboBox></td>--%>
                                       
                                    </tr>
                                    <tr>
                                        <td>No. SIM : </td>
                                        <td><dx:ASPxTextBox ReadOnly="true" ID="tx_no_sim" runat="server" Width="170px"></dx:ASPxTextBox></td>
                                        
                                    </tr>
                                    <tr>
<%--                                        <td>Dedicated : </td>
                                        <td><dx:ASPxTextBox ReadOnly="true" ID="tx_dedicated" runat="server" Width="170px"></dx:ASPxTextBox></td>--%>

                                        <%--<td><dx:ASPxComboBox ID="cb_lokasi_kerja" runat="server" TextField="lokasi" ValueField="id_lokasi">
                                            <Columns>
                                                <dx:ListBoxColumn Caption="ID" FieldName="id_lokasi" Name="id_lokasi" />
                                                <dx:ListBoxColumn Caption="Lokasi" FieldName="nama" Name="Lokasi" />
                                            </Columns>
                                            </dx:ASPxComboBox></td>--%>
                                        <td>No. Rek : </td>
                                        <td><dx:ASPxTextBox ReadOnly="true" ID="tx_no_rek" runat="server" Width="170px"></dx:ASPxTextBox></td>
                                    </tr>
                                    <tr>
                                        <td>Bank : </td>
                                        <td><dx:ASPxComboBox ReadOnly="true" ID="cb_bank" runat="server" SelectedIndex="0">
                                            <Items>
                                                <dx:ListEditItem Selected="True" Text="BNI 46" Value="1" />
                                                <dx:ListEditItem Text="Mandiri" Value="2" />
                                                <dx:ListEditItem Text="BCA" Value="3" />
                                                <dx:ListEditItem Text="MEGA" Value="4" />
                                                <dx:ListEditItem Text="Permata" Value="5" />
                                                <dx:ListEditItem Text="Niaga" Value="6" />
                                                <dx:ListEditItem Text="Panin" Value="7" />
                                                <dx:ListEditItem Text="HSBC" Value="8" />
                                                <dx:ListEditItem Text="CIMB" Value="9" />
                                                <dx:ListEditItem Text="Bukopin" Value="10" />
                                                <dx:ListEditItem Text="BRI" Value="11" />
                                                <dx:ListEditItem Text="UOB" Value="12" />
                                                <dx:ListEditItem Text="Sinarmas" Value="13" />
                                                <dx:ListEditItem Text="OCBC NISP" Value="14" />
                                            </Items>
                                            </dx:ASPxComboBox></td>
                                    </tr>
                                    <tr>
                                        <td>Tgl Bergabung : </td>
                                        <td><dx:ASPxDateEdit ReadOnly="true" ID="tx_tgl_bergabung" runat="server"></dx:ASPxDateEdit></td>
                        
<%--                                        <td>Atas Nama : </td>
                                        <td><dx:ASPxTextBox ReadOnly="true" ID="tx_atas_nama" runat="server" Width="170px"></dx:ASPxTextBox></td>    --%>                    
                                    </tr>
                                    <tr>
                                        <%--<td>Status Karyawan : </td>
                                        <td><dx:ASPxComboBox ReadOnly="true" ID="cb_status_kar" runat="server" ValueType="System.String"></dx:ASPxComboBox></td>--%>
                                        <td>No. BPJS TK : </td>
                                        <td><dx:ASPxTextBox ReadOnly="true" ID="tx_no_bpjs" runat="server" Width="170px"></dx:ASPxTextBox></td>
                                    </tr>
                                    <tr>
                                       <%-- <td>Status Pekerjaan : </td>
                                        <td><dx:ASPxComboBox ReadOnly="true" ID="cb_status_pek" runat="server" ValueType="System.String"></dx:ASPxComboBox></td>--%>
                                        <%--<td>No. Garda Medika : </td>
                                        <td><dx:ASPxTextBox ReadOnly="true" ID="tx_no_garda" runat="server" Width="170px"></dx:ASPxTextBox></td>--%>
                                    </tr>
                                </table>

                            </div>
                            <hr />
                            <div class="row">

                                <%--<table style="width:100%;">
                                    <tr>
                                        <th colspan="4">Kelengkapan Administrasi</th>
                                    </tr>
                                    <tr>
                                        <td>Photo : </td>
                                        <td><%#Eval("file_photo")%></td>
                                        <td>Garda Medika : </td>
                                        <td><%#Eval("file_garda")%></td>
                                    </tr>
                                    <tr>
                                        <td>KTP : </td>
                                        <td><%#Eval("file_ktp")%></td>
                                        <td>SKCK : </td>
                                        <td><%#Eval("file_skck")%></td>
                                    </tr>
                                    <tr>
                                        <td>SIM : </td>
                                        <td><%#Eval("file_sim")%></td>
                                        <td>Surat Sehat : </td>
                                        <td><%#Eval("file_surat_sehat")%></td>
                                    </tr>
                                    <tr>
                                        <td>CV : </td>
                                        <td><%#Eval("file_cv")%></td>
                                        <td>Paklaring : </td>
                                        <td><%#Eval("file_paklaring")%></td>
                                    </tr>
                                    <tr>
                                        <td>Kartu Keluarga : </td>
                                        <td><%#Eval("file_kk")%></td>
                                        <td>Ijazah : </td>
                                        <td><%#Eval("file_ijazah")%></td>
                                    </tr>
                                    <tr>
                                        <td>Surat Nikah : </td>
                                        <td><%#Eval("file_surat_nikah")%></td>
                                        <td>SKK : </td>
                                        <td><%#Eval("file_skk")%></td>
                                    </tr>
                                    <tr>
                                        <td>Surat Domisili : </td>
                                        <td><%#Eval("file_surat_domisili")%></td>
                                        <td>Form Interview : </td>
                                        <td><%#Eval("file_form_interview")%></td>
                                    </tr>
                                    <tr>
                                        <td>BPJS TK : </td>
                                        <td><%#Eval("file_bpjs")%></td>
                                        <td> </td>
                                        <td> </td>
                                    </tr>
                                </table>--%>

                            </div>

                    	</div>
                </div>
                    	
         </div>
        </div>

    </div>


    <uc1:uc_footer runat="server" ID="uc_footer" />
</asp:Content>
