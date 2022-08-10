<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="page_print.aspx.vb" Inherits="MDS.page_print" %>

<%@ Register Assembly="DevExpress.XtraReports.v18.2.Web.WebForms, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>

<%@ Register Src="~/uc_header.ascx" TagPrefix="uc1" TagName="uc_header" %>
<%@ Register Src="~/uc_footer.ascx" TagPrefix="uc1" TagName="uc_footer" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:uc_header runat="server" ID="uc_header" />

    <script type="text/javascript">
        function init_docviewer(s) {
            var createFrameElement = s.viewer.printHelper.createFrameElement;
            s.viewer.printHelper.createFrameElement = function (name) {
                var frameElement = createFrameElement.call(this, name);
                var isChrome = /Chrome/.test(navigator.userAgent) && /Google Inc/.test(navigator.vendor);
                if (isChrome) {
                    frameElement.addEventListener("load", function (e) {
                        if (frameElement.contentDocument && frameElement.contentDocument.contentType !== "text/html")
                            frameElement.contentWindow.print();
                    });
                }
                return frameElement;
            }
        }
    </script>
    <br />
    <dx:ASPxDocumentViewer ID="ASPxDocumentViewer1" runat="server" Width="100%">
        <ClientSideEvents Init="function(s, e) { init_docviewer(s); }" />
    </dx:ASPxDocumentViewer>

    <uc1:uc_footer runat="server" ID="uc_footer" />
</asp:Content>
