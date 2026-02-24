<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Pagina2LoguinAdmin.aspx.cs" Inherits="pokedex_web.Pagina2LoguinAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-md-6 text-center">

                <h2 class="mb-3">¡Esa!</h2>
                <p class="lead">Tenés perfil admin, sino no podrías estar acá.</p>

                <asp:Button
                    ID="btnRegresar"
                    runat="server"
                    Text="Regresar"
                    CssClass="btn btn-primary mt-3"
                    OnClick="btnRegresar_Click" />

            </div>
        </div>
    </div>
</asp:Content>
