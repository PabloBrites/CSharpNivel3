<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Loguin.aspx.cs" Inherits="pokedex_web.Loguin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-md-6">
        <div class="mb-3">
            <label class="form-label">User</label>
            <asp:TextBox runat="server" id="txtUser" placeholder="user name" CssClass="form-control" />
        </div>
        <div class="mb-3">
            <label class="form-label">Password</label>
            <asp:TextBox runat="server" placeholder="******" Id="txtPassword" CssClass="form-control" TextMode="Password" />  
        </div>
        <asp:Button Text="Ingresar" runat="server" ID="btnIngresar" OnClick="btnIngresar_Click" CssClass="btn btn-primary" />
    </div>






</asp:Content>
