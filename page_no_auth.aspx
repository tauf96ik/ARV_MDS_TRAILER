<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="page_no_auth.aspx.vb" Inherits="MDS.page_no_auth" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .tengah {
  display: block;
  margin-left: auto;
  margin-right: auto;
  width: 50%;
}
    </style>
    <div class="container">
        <div class="row">
            <img src="img/not_auth.jpg" class="tengah" />
        </div>
        <div class="row">
            <div class="col-sm-12">
            <a class="btn btn-primary btn-md tengah" href="home.aspx">Back to Home</a>

            </div>
        </div>
    </div>
</asp:Content>
