<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="page_nomaster.aspx.vb" Inherits="MDS.page_nomaster" %>

<%@ Register assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />

    <title>Error Page</title>

    <link href="../css/bootstrap.min.css" type="text/css" rel="stylesheet" />
    <link href="../css/londinium-theme.css" type="text/css" rel="stylesheet" />
    <link href="../css/styles.min.css" type="text/css" rel="stylesheet" />
    <link href="../css/icons.min.css" type="text/css" rel="stylesheet" />

    <script type="text/javascript" src="../js/plugins/jquery.min.js"></script>
    <script type="text/javascript" src="../js/plugins/jquery-ui.min.js"></script>

    <script type="text/javascript" src="../js/plugins/charts/sparkline.min.js"></script>
    <script type="text/javascript" src="../js/plugins/forms/uniform.min.js"></script>
    <script type="text/javascript" src="../js/plugins/forms/select2.min.js"></script>
    <script type="text/javascript" src="../js/plugins/forms/inputmask.js"></script>
    <script type="text/javascript" src="../js/plugins/forms/autosize.js"></script>
    <script type="text/javascript" src="../js/plugins/forms/inputlimit.min.js"></script>
    <script type="text/javascript" src="../js/plugins/forms/listbox.js"></script>
    <script type="text/javascript" src="../js/plugins/forms/multiselect.js"></script>
    <script type="text/javascript" src="../js/plugins/forms/validate.min.js"></script>
    <script type="text/javascript" src="../js/plugins/forms/tags.min.js"></script>
    <script type="text/javascript" src="../js/plugins/forms/switch.min.js"></script>
    <script type="text/javascript" src="../js/plugins/forms/uploader/plupload.full.min.js"></script>
    <script type="text/javascript" src="../js/plugins/forms/uploader/plupload.queue.min.js"></script>
    <script type="text/javascript" src="../js/plugins/forms/wysihtml5/wysihtml5.min.js"></script>
    <script type="text/javascript" src="../js/plugins/forms/wysihtml5/toolbar.js"></script>
    
    <script type="text/javascript" src="../js/plugins/interface/daterangepicker.js"></script>
    <script type="text/javascript" src="../js/plugins/interface/fancybox.min.js"></script>
    <script type="text/javascript" src="../js/plugins/interface/moment.js"></script>
    <script type="text/javascript" src="../js/plugins/interface/jgrowl.min.js"></script>
    <script type="text/javascript" src="../js/plugins/interface/datatables.min.js"></script>
    <script type="text/javascript" src="../js/plugins/interface/colorpicker.js"></script>
    <script type="text/javascript" src="../js/plugins/interface/fullcalendar.min.js"></script>
    <script type="text/javascript" src="../js/plugins/interface/timepicker.min.js"></script>
    <script type="text/javascript" src="../js/plugins/interface/collapsible.min.js"></script>
    <script type="text/javascript" src="../js/bootstrap.min.js"></script>
    <script type="text/javascript" src="../js/application.js"></script>

</head>
<body class="full-width">
    <form id="form1" runat="server">

        <div class="navbar navbar-inverse" role="navigation" aria-expanded="false">
        </div>
        <div class="page-content">
            <div class="breadcrumb-line">
            </div>

            <br />
            <br />

            <div class="error-wrapper text-center">
                <dx:ASPxImage ID="ASPxImage1" runat="server" ImageUrl="~/img/logo_agungcartrans.png" Height="60px" ImageAlign="Middle"></dx:ASPxImage>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <dx:ASPxImage ID="ASPxImage2" runat="server" ImageUrl="~/img/logo_agungline.png" Height="60px" ImageAlign="Middle"></dx:ASPxImage>

                <br />
                <br />
                <br />
                <br />

                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="ASPxLabel" Font-Size="Medium">
                </dx:ASPxLabel>

                <br />
                <br />

                <div class="error-content">
                    <div class="row">
                        <div class="col-sd-6"><a href="#" class="btn btn-success btn-block" runat="server" id="a_login">Back to Login Page</a> </div>
                    </div>
                </div>
            </div>
        </div>

    </form>
</body>
</html>
