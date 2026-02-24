<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="MenuLogin1Ejemplo.aspx.cs" Inherits="pokedex_web.MenuLogin1Ejemplo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-12">
            <h3>Te logueaste correctamente.</h3>
            <hr />
        </div>
        <div class="col-4">
            <asp:Button Text="Pagina 1" ID="btnPagina1" OnClick="btnPagina1_Click" runat="server" CssClass="btn btn-primary" />
        </div>
        <div class="col-4">
            <% if (Session["usuario"] != null &&
         ((dominio.Usuario)Session["usuario"]).TipoUsuario == dominio.TipoUsuario.ADMIN)
                { %>

            <asp:Button Text="Pagina 2" ID="btnPagina2"
                OnClick="btnPagina2_Click"
                runat="server"
                CssClass="btn btn-primary" />

            <% }
            else
            { %>

            <p>Tenés que ser admin.</p>

            <% } %>
        </div>
    </div>
</asp:Content>
