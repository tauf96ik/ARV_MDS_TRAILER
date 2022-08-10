<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="home.aspx.vb" Inherits="MDS.home" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Src="~/uc_header.ascx" TagPrefix="uc1" TagName="uc_header" %>
<%@ Register Src="~/uc_footer.ascx" TagPrefix="uc1" TagName="uc_footer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:uc_header runat="server" ID="uc_header" />

    <div class="row">
        <div class="col-md-6">
            <div class="panel panel-danger">
                <div class="panel-heading">
                    <img src="img/card.png" style="margin: 10px" width="100px" height="100px" id="ctl00_photo_user">
                </div>
                <div class="panel-body">
                    <div class="col-md-12">
                        <ul class="list-group" runat="server" id="ul_approve">
                        </ul>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-6">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <img src="img/driver.png" style="margin: 10px" width="100px" height="100px" id="ctl00_photo_user">
                </div>
                <div class="panel-body">
                    <div class="col-md-12">
                        <ul class="list-group" runat="server" id="ul_supir">
                        </ul>
                    </div>
                </div>
            </div>
        </div>

    </div>
    <div class="row" runat="server" visible="false">
        <div class="col-md-6">
            <div class="panel panel-success">
                <div class="panel-heading">
                    <img src="img/truck.png" style="margin: 10px" width="100px" height="100px" id="ctl00_photo_user">
                </div>
                <div class="panel-body">
                    <div class="col-md-12">
                        <ul class="list-group" runat="server" id="ul_cc">
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>

<%--    <div class="row">
        <div class="col-md-6">
            <ul class="list-group" runat="server" id="ul_approve"></ul>
        </div>

        <div class="col-md-6">
            <ul class="list-group" runat="server" id="ul_cc"></ul>
        </div>
    </div>--%>

    <hr />
    <div class="row">
        <div class="col-md-6">
            <ul class="list-group" runat="server" id="ul_info"></ul>
        </div>

        <div class="col-md-6">
            <ul class="list-group" runat="server" id="ul_tmc"></ul>
        </div>
        <div class="col-md-6">
            <ul class="list-group" runat="server" id="ul_ultah"></ul>
        </div>
    </div>


    <uc1:uc_footer runat="server" id="uc_footer" />
</asp:Content>
