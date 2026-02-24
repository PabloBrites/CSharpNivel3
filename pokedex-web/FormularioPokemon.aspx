<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="FormularioPokemon.aspx.cs" Inherits="pokedex_web.FormularioPokemon" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div class="row">
        <div class="col-6">
            <div class="mb-3">
                <label for="txtId" class="form-label">Id</label>
                <%--form-label no cambia cómo se ve el texto, cambia el espaciado y la disposición del label según Bootstrap/ lo que hay que entender es que class pertenece a html para dar estilos y cssclass hace loo mismo pero para controles asp--%>
                <asp:TextBox runat="server" ID="txtId" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label for="txtNombre" class="form-label">Nombre</label>
                <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label for="txtNumero" class="form-label">Numero</label>
                <asp:TextBox runat="server" ID="txtNumero" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label for="ddlTipo" class="form-label">Tipo</label>
                <asp:DropDownList ID="ddlTipo" CssClass="form-select" runat="server"></asp:DropDownList>
            </div>
            <div class="mb-3">
                <label for="ddlDebilidad" class="form-label">Debilidad</label>
                <asp:DropDownList ID="ddlDebilidad" CssClass="form-select" runat="server"></asp:DropDownList>
            </div>

            <div class="mb-3">
                <asp:Button Text="Aceptar" ID="btnAceptar" CssClass="btn btn-primary" OnClick="btnAceptar_Click" runat="server" />
                <a href="PokemonLista.aspx">Cancelar</a>
                <asp:Button Text="Inactivar" ID="btnInactivar" OnClick="btnInactivar_Click" CssClass="btn btn-warning" runat="server" />

            </div>
        </div>



        <div class="col-6">
            <div class="mb-3">
                <label for="txtDescripcion" class="form-label">Descripcion</label>
                <asp:TextBox runat="server" TextMode="MultiLine" ID="txtDescripcion" CssClass="form-control" />
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="mb-3">
                        <label for="txtImagenUrl" class="form-label">Url Imagen</label>
                        <asp:TextBox runat="server" ID="txtImagenUrl" CssClass="form-control"
                            AutoPostBack="true" OnTextChanged="txtImagenUrl_TextChanged" />
                    </div>
                    <asp:Image ImageUrl="https://developers.elementor.com/docs/assets/img/elementor-placeholder-image.png"
                        runat="server" ID="imgPokemon" Width="60%" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="row">
        <div class="col-6">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="mb-3">
                        <asp:Button Text="Eliminar" ID="BtnEliminar" OnClick="BtnEliminar_Click" CssClass="btn btn-danger" runat="server" />
                    </div>

                    <%if (ConfirmaEliminacion)
                        { %>
                    <div class="mb-3">
                        <asp:CheckBox Text="Confirmar Eliminacion?" ID="chkConfirmaEliminacion" runat="server" />
                        <asp:Button Text="Eliminar" ID="btnConfirmaEliminacion" OnClick="btnConfirmaEliminacion_Click" CssClass="btn btn-outline-danger" runat="server" />
                    </div>
                    <%} %>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>





</asp:Content>

<%--¿Entonces cuál uso?

Si el control empieza con <asp:...> → usa CssClass para estilos 

Si es un control HTML normal → usa class para estilos --%>

<%--"form-label" → es una clase CSS de Bootstrap aplicada a un control HTML puro, el estilo es siempre de css
pero vos usas clases de boostrap que "utilizan" css, (porque Bootstrap es un conjunto de clases CSS ya hechas)--%>

<%--------------------------------------------------------------------------------------------------------%>

<%--condicion bool de if(confirmaEliminacion)

if (ConfirmaEliminacion) es lo mismo que poner - if (ConfirmaEliminacion == true)

if (ConfirmaEliminacion) es la condición.
Como es booleano, no hace falta poner == true.
Si vale true el mensaje aparece; si vale false, no se muestra nada.

vos te preguntabas poorque esta oculto el mensaje de "confirmar eliminacion" al momento de 
apretar el btnEliminar, --%> 

<%--esta oculto primero porque vos pusiste la propiedad con un - bool public bool ConfirmaEliminacion { get; set; }
despues tenes este codigo y mientras ConfirmaEliminacion sea  == false,  ese HTML NO se genera en pantalla 
NO existe, no se va a moostrar, la pagina dice: 

"Solo agrego este mensaje a la pantalla si el booleano es verdadero" 
osea haciendo clic en el botón que dispara la eliminación.
<div class="mb-3">
    <asp:CheckBox Text="Confirmar Eliminacion?" --%>