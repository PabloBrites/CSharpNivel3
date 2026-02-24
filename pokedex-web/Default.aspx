<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="pokedex_web.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style> /*efecto brillo en la tarjeta mono*/
        .glow-card {
            transition: transform 0.3s ease, box-shadow 0.3s ease;
        }

        .glow-card:hover {
            transform: translateY(-8px);
            box-shadow: 0 0 20px rgba(0, 123, 255, 0.6);
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1>Hola!</h1>
    <p>Llegaste al Pokedex Web, tu lugar Pokemon...</p>



    <div class="row row-cols-1 row-cols-md-3 g-4">


        <%--        <%
            foreach (dominio.Pokemon poke in ListaPokemon)//Está diciendo: “Usá la clase Pokemon que está en el proyecto nombre dominio, y "poke" es el nombre de la variable(objeto) que representa a cada objeto pokemon dentro de la lista, se declara una sola vez y toma el valor de cada elemento de la lista uno por uno a medida que el bucle recorre la coleccion 
            {
        %>
            
                 <div class="col">
                    <div class="card">
                        <img src="<%:poke.UrlImagen %>" class="card-img-top" alt="...">
                        <div class="card-body">
                            <h5 class="card-title"> <%:poke.Nombre %></h5>
                            <p class="card-text"><%:poke.Descripcion %></p>
                            <a href="DetallePokemon.aspx?id=<%:poke.Id %>">Ver Detalle</a>
                        </div>
                    </div>
                </div>

        
         <% } %>--%>

        <asp:Repeater runat="server" ID="repRepetidor">
            <%--<-- aca reune los valores con repRepetidor en el codebehind--%> <%--<%# Eval("Id") %> significa que estás enlazando datos--%>
            <ItemTemplate>
                <div class="col">
                    <div class="card - glow-card">
                        <img src="<%#Eval("UrlImagen")%>" class="card-img-top" alt="...">
                        <div class="card-body">
                            <h5 class="card-title"><%#Eval("Nombre") %></h5>
                            <p class="card-text"><%#Eval("Descripcion") %></p>
                            <a href="DetallePokemon.aspx?id=<%#Eval("Id") %>">Ver Detalle</a>
                            <asp:Button Text="Ejemplo" CssClass="btn btn-primary" runat="server" ID="btnEjemplo" CommandArgument='<%#Eval("Id") %>' CommandName="PokemonId" OnClick="btnEjemplo_Click" />
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <br><br><br><br />
</asp:Content>

<%--El signo de pregunta (?) en una URL tiene un papel clave: marca el inicio de los parámetros que se van a enviar por la URL.--%>

