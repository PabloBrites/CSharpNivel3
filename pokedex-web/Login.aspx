  <%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="pokedex_web.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
    <div class="col-4">
        <h2>Login</h2> 
        <div class="mb-3">
            <label class="form-label"> Email</label> <%--//class es el puente entre el HTML comunacho y Bootstrap fachero--%>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtEmail" />
        </div>
        <div class="mb-3">
            <label class="form-label">Password</label> <%--//class es el puente entre el HTML comunacho y Bootstrap fachero--%>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtPassword" textMode="Password"/>
        </div>
        <asp:button Text="Ingresar" cssclass="btn btn-primary" Onclick="btnLogin_Click" ID="btnLogin" runat="server" />
        <a href="Default.aspx">Cancelar</a>
    </div>
</div>
</asp:Content>
