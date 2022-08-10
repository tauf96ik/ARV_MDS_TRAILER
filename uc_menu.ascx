<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="uc_menu.ascx.vb" Inherits="MDS.uc_menu" %>

<%  
    Dim id_master As String = ""
    Dim id_autho As String = ""
    Dim id_trans As String = ""
    Dim id_finance As String = ""
    Dim id_lap As String = ""
    Dim id_doc As String = ""
    Dim id_mon As String = ""
    Dim id_help As String = ""
    
    Select Case Request.ServerVariables("SCRIPT_NAME")
        Case "/mst_group.aspx", "/mst_user.aspx"
            id_autho = "id='second-level'"
            
        Case "/opr_mst_moda.aspx", "/opr_mst_bbm.aspx", "/opr_mst_addcost.aspx", "/opr_mst_moda.aspx", "/opr_mst_lokasi.aspx", _
            "/opr_mst_cc.aspx", "/opr_mst_driver.aspx", "/opr_mst_tarif.aspx", "/tmc_mst_status.aspx", "/opr_mst_kend_type.aspx"
            id_master = "id='second-level'"
            
        Case "/opr_trs_costan.aspx", "/opr_trs_ti.aspx", "/opr_trs_spk.aspx", "/opr_trs_spk_close.aspx", "/opr_trs_unit_close.aspx"
            id_trans = "id='second-level'"
                      
        Case "/opr_lap_unit.aspx", "/opr_lap_cc_uti.aspx"
            id_lap = "id='second-level'"
            
        Case "/doc_driver.aspx"
            id_doc = "id='second-level'"
            
        Case "/opr_mon_cc.aspx"
            id_mon = "id='second-level"
            
    End Select
    
    Dim hal As String = Request.ServerVariables("SCRIPT_NAME")
    If hal Like "*help*" Then id_help = "id='second-level'"
%>

<div class="sidebar-content">
    <ul class="navigation yamm-fw">
        <li><a href="mds_dashboard.aspx"><span>DASHBOARD</span> <i class="icon-dashboard"></i></a></li>
        <li><a href="#" class="expand"><span>AUTHORIZE</span> <i class="icon-settings"></i></a>
            <ul style="background-color: transparent">
                <li runat="server" id="a_autho_grp"><a href="mst_group.aspx">- Group Authorize</a></li>
                <li runat="server" id="a_autho_user"><a href="mst_user.aspx">- User Authorize</a></li>
            </ul>
        </li>
        <li><a href="#" class="expand"><span>MASTER</span> <i class="icon-settings"></i></a>
            <ul style="background-color: transparent">
                <li runat="server" id="a_mst_apd"><a href="mds_mst_apd.aspx">- Master APD</a></li>
                <li runat="server" id="a_mst_ba"><a href="mds_mst_ba.aspx">- Master Berita Acara</a></li>
                <li runat="server" id="a_mst_izin_jns"><a href="mds_mst_izin.aspx">- Master Jenis Izin</a></li>
                <li runat="server" id="a_mst_kateg"><a href="mds_mst_kategori.aspx">- Master Kategori</a></li>
                <li runat="server" id="a_mst_limit"><a href="mds_mst_limit.aspx">- Master Limit</a></li>
                <li runat="server" id="a_mst_rtt"><a href="mds_mst_rtt.aspx">- Master Planing RTT Training</a></li>
                <li runat="server" id="a_mst_templ"><a href="mds_mst_template.aspx">- Master Template</a></li>
                <li runat="server" id="a_mst_training"><a href="mds_mst_training.aspx">- Master Training</a></li>
                <%--<li runat="server" id="a_mst_plan_train"><a href="mds_mst_plan_train.aspx">- Master Training Planing</a></li>--%>
            </ul>
        </li>
        <li><a href="#" class="expand" <%=id_master%>><span>RECRUITMENT</span> <i class="icon-menu3"></i></a>
            <ul style="background-color: transparent">
                <li runat="server" id="a_rec_applicant"><a href="mds_rec_apl.aspx">- Driver Applicants</a></li>
            </ul>
        </li>
        <li><a href="#" class="expand" <%=id_trans%>><span>EXISTING</span> <i class="icon-list"></i></a>
            <ul style="background-color: transparent">
                <li runat="server" id="a_trs_acc_inc"><a href="mds_acc_inc.aspx">- Accident / Incident</a></li>
                <li runat="server" id="a_trs_ba"><a href="mds_berita_acara.aspx">- Berita Acara</a></li>
                <li runat="server" id="a_driver_act"><a href="mds_driver_active.aspx">- Driver Active</a></li>
                <li runat="server" id="a_driver_inact"><a href="mds_driver_inact.aspx">- Driver Non Active</a></li>
                <li runat="server" id="a_trs_training"><a href="mds_trs_training.aspx">- Driver Training</a></li>
                <li runat="server" id="a_trs_izin"><a href="mds_trs_izin.aspx">- Izin Driver</a></li>
                <li runat="server" id="a_trs_mcu"><a href="mds_trs_mcu.aspx">- Medical Check Up</a></li>
                <li runat="server" id="a_trs_apd"><a href="mds_trs_apd.aspx">- Pembagian APD</a></li>
                <li runat="server" id="a_skontrak"><a href="mds_surat_kontrak.aspx">- Surat Kontrak</a></li>
                <li runat="server" id="a_trs_tegur"><a href="mds_trs_teguran.aspx">- Teguran & Peringatan</a></li>
                <%--<li runat="server" id="a_trs_railing"><a href="mds_trs_railing.aspx">- Driver Railing</a></li>
                <li runat="server" id="a_trs_bpjs"><a href="mds_trs_bpjs.aspx">- Iuran BPJS TK</a></li>
                <%--<li runat="server" id="a_trs_garda"><a href="#">- Pembayaran Garda</a></li>--%>
                <%--<li runat="server" id="a_trs_cc"><a href="trs_cc_driver.aspx">- Driver Dedicated CC</a></li>
                <li runat="server" id="a_driver_punish"><a href="mds_driver_punish.aspx">- Driver Punishment</a></li>
                <li runat="server" id="a_trs_tegur"><a href="trs_teguran.aspx">- Teguran & Peringatan</a></li>
                <li runat="server" id="a_trs_couching"><a href="trs_form_couching.aspx">- Form Couching</a></li>
                <li runat="server" id="a_trs_tab"><a href="mds_trs_tabungan.aspx">- Transaksi Tabungan</a></li>
                <li runat="server" id="a_trs_template"><a href="trs_template.aspx">- Kuesioner</a></li>--%>
            </ul>
        </li>
        <li><a href="#" class="expand" <%=id_mon%>><span>MONITORING</span> <i class="icon-stats2"></i></a>
            <ul style="background-color: transparent">
                <li runat="server" id="a_mon_supir"><a href="mon_spk_driver.aspx">- Monitoring Surat Jalan Driver</a></li>
                <li runat="server" id="a_mon_pass"><a href="mds_mon_pass.aspx">- Password Driver</a></li>
                <%--<li runat="server" id="a_mon_standby"><a href="mon_standby.aspx">- Driver Standby</a></li>
                <li runat="server" id="a_mon_supir"><a href="mon_supir.aspx">- SPK Driver</a></li>
                <li runat="server" id="a_mon_supir_cycle"><a href="mon_supir_cycle.aspx">- Cycle Time Driver</a></li>
                <li runat="server" id="a_mon_utilitas"><a href="mon_utilitas.aspx">- Utilitas Driver</a></li>
                <li runat="server" id="a_mon_img"><a href="mon_image.aspx">- Images Driver</a></li>--%>
            </ul>
        </li>
        <li><a href="#" class="expand" <%=id_lap%>><span>LAPORAN / REPORT</span> <i class="icon-stats2"></i></a>
            <ul style="background-color: transparent">
                <li runat="server" id="a_lap_apd"><a href="mds_lap_apd.aspx">- Lapopran A.P.D</a></li>
                <li runat="server" id="a_lap_accident"><a href="mds_lap_accinc.aspx">- Laporan Accident Incident</a></li>
                <li runat="server" id="a_lap_ba"><a href="mds_lap_ba.aspx">- Laporan Berita Acara</a></li>
                <li runat="server" id="a_lap_supir"><a href="mds_lap_supir.aspx">- Laporan Data Supir</a></li>
                <li runat="server" id="a_lap_izin"><a href="mds_lap_izin.aspx">- Laporan Izin Driver</a></li>
                <li runat="server" id="a_lap_mcu"><a href="mds_lap_mcu.aspx">- Laporan Medical Check Up</a></li>
                <li runat="server" id="a_lap_sj"><a href="mds_lap_sj.aspx">- Laporan SJ Driver</a></li>
                <li runat="server" id="a_lap_teg"><a href="mds_lap_teg.aspx">- Laporan Teguran</a></li>
                <li runat="server" id="a_lap_training"><a href="mds_lap_training.aspx">- Laporan Training</a></li>
                <%--<li runat="server" id="a_lap_pot"><a href="mds_lap_pot.aspx">- Lap. Potongan Driver</a></li>--%>
                <%--<li runat="server" id="a_lap_standby"><a href="lap_standby.aspx">- Lap. Standby Supir</a></li>--%>
                <%--<li runat="server" id="a_lap_point"><a href="lap_point_supir.aspx">- Lap. Point Supir</a></li>
                <li runat="server" id="a_lap_raport_point"><a href="lap_raport_point_driver.aspx">- Lap. Raport Point Supir</a></li>
                <li runat="server" id="a_lap_raport_pivot"><a href="lap_raport_pivot.aspx">- Lap. Pivot Raport Supir</a></li>
                <li runat="server" id="a_lap_ritase"><a href="lap_ritase.aspx">- Lap. Ritase</a></li>
                <li runat="server" id="a_lap_mcu"><a href="lap_mcu.aspx">- Lap. Medical Check Up</a></li>
                <li runat="server" id="a_pivot_tab"><a href="lap_trs_tab.aspx">- Lap. Tabungan Driver</a></li>
                <li runat="server" id="a_lap_couching"><a href="lap_couching.aspx">- Lap. Form Couching</a></li>
                <li runat="server" id="a_lap_kuesioner"><a href="lap_kuesioner.aspx">- Lap. Kuesioner</a></li>--%>
            </ul>
        </li>
        <li><a href="#" class="expand" <%=id_help%>><span>HELP</span> <i class="icon-stats2"></i></a>
            <ul style="background-color: transparent">
                <li runat="server" id="a_help_userguide"><a href="help_userguide.aspx">- User Guide</a></li>
                <li runat="server" id="a_help_flow"><a href="help_flow.aspx">- Flow Documents</a></li>
                <li runat="server" id="a_help_uat"><a href="help_uat.aspx">- U.A.T</a></li>
                <%--<li runat="server" id="Li1"><a href="fp_driver_pool.aspx">- Finger Print Driver Sumur Batu</a></li>
                <li runat="server" id="Li2"><a href="fp_karyawan_pool.aspx">- Finger Print Karyawan Sumur Batu</a></li>
                <li runat="server" id="Li3"><a href="fp_karyawan_cutmutiah.aspx">- Finger Print Karyawan Cut Mutiah</a></li>--%>
            </ul>
        </li>
    </ul>
</div>