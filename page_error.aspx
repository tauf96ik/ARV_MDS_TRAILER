<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="page_error.aspx.vb" Inherits="MDS.page_error" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Src="~/uc_header.ascx" TagPrefix="uc1" TagName="uc_header" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:uc_header runat="server" ID="uc_header" />

    <br />
    <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="ASPxLabel"></dx:ASPxLabel>
    <a runat="server" id="SendMail" onserverclick="SendMail_ServerClick">Send Error via Email</a>
    <br />
    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="ASPxLabel"></dx:ASPxLabel>

    <br />
    <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="ASPxLabel"></dx:ASPxLabel>

    <br />
    <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="ASPxLabel"></dx:ASPxLabel>

    <br />
    <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="ASPxLabel"></dx:ASPxLabel>



    <script type="text/javascript">
        function sendemail() {
            document.cookie = "skip=sendemail";
        }

        function hasil_email(s, e) {
            if (e.result == "berhasil") {
                $.jGrowl('Email telah berhasil dikirim', { theme: 'growl-success', header: 'Success !', life: 10000 });
            } else if (e.result == "gagal") {
                $.jGrowl('Email gagal terkirim, harap coba kembali', { theme: 'growl-error', header: 'Error !', life: 10000 });
            } else {
                $.jGrowl('Tidak ditemukan kesalahan pada sistem', { header: 'Notification !', life: 10000 });
            }
        }

        function gocallbackemail() {
            callback_email.PerformCallback();
        }
    </script>

    <dx:ASPxCallback ID="ASPxCallback1" runat="server" ClientInstanceName="callback_email" OnCallback="ASPxCallback1_Callback">
        <ClientSideEvents CallbackComplete="function(s, e) { hasil_email(s, e); }" />
    </dx:ASPxCallback>
</asp:Content>
