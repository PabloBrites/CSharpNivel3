<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="pokedex_web.Registro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-4">
            <h2>Crea tu perfil Trainee</h2> <%--class conecta el elemento HTML con CSS de Bootstrap)--%>
            <div class="mb-3">
                <label class="form-label"> Email</label> <%--//class es el puente entre el HTML comunacho y Bootstrap fachero--%>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtEmail" />
            </div>
            <div class="mb-3">
                <label class="form-label">Password</label> <%--//class es el puente entre el HTML comunacho y Bootstrap fachero--%>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtPassword" textMode="Password"/>
            </div>
            <asp:button Text="Registrarse" cssclass="btn btn-primary" Onclick="btnRegistrarse_Click" ID="btnRegistrarse" runat="server" />
            <a href="#">Cancelar</a>
        </div>
    </div>

</asp:Content>
