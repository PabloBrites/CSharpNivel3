 <%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="MiPerfil.aspx.cs" Inherits="pokedex_web.MiPerfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .validacion{
            color: red;
            font-size: 15px;
        }
    </style>
    <%--<script>
        function validar() {

            //capturar el control. 
            const txtApellido = document.getElementById("txtApellido");
            const txtNombre = document.getElementById("txtNombre");
            if (txtApellido.value == "") {
                txtApellido.classList.add("is-invalid");
                txtApellido.classList.remove("is-valid");
                txtNombre.classList.add("is-valid");
                return false;
            }
            txtApellido.classList.remove("is-invalid");
            txtApellido.classList.add("is-valid");
            return true;
        }
    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Mi Perfil</h2>
    <div class="row">
        <div class="col-md-4">
            <div class="mb-3">
                <label class="form-label">Email</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtEmail" />
            </div>
            <div class="mb-3">
                <label class="form-label">Nombre</label>
                <asp:TextBox runat="server" CssClass="form-control" ClientIDMode="Static" ID="txtNombre" />
                <asp:RequiredFieldValidator CssClass="validacion" ErrorMessage="El nombre es requerido" ControlToValidate="txtNombre" runat="server" />
            </div>
            <div class="mb-3">
                <label class="form-label">Apellido</label>
                <asp:TextBox ID="txtApellido" ClientIDMode="Static" runat="server" CssClass="form-control" MaxLength="12">
                </asp:TextBox>
                <%--<asp:RangeValidator ErrorMessage="Fuera de rango.." ControlToValidate="txtApellido" Type="Integer" MinimumValue="1" MaximumValue="20" runat="server" />--%>
                <%--<asp:RegularExpressionValidator ErrorMessage="Formato email por favor" ControlToValidate="txtApellido" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" runat="server" />--%>

                <%--<asp:RegularExpressionValidator ErrorMessage="Formato incorrecto..." ControlToValidate="txtApellido" ValidationExpression="^[0-9]+$" runat="server"/>--%>
            </div>
            <div class="mb-3">
                <label class="form-label">Fecha de Nacimiento</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtFechaNacimiento" TextMode="Date" />
            </div>

        </div>
        <div class="col-md-4">
            <div class="mb-3">
                <label class="form-label">Imagen Perfil</label>
                <input type="file" id="txtImagen" runat="server" class="form-control" />
            </div>
            <asp:Image ID="imgNuevoPerfil" ImageUrl="https://www.palomacornejo.com/wp-content/uploads/2021/08/no-image.jpg"
                runat="server" CssClass="img-fluid mb-3" />

        </div>

    </div>
    <div class="row">
        <div class="col-md-4">
            <asp:Button Text="Guardar" CssClass="btn btn-primary" OnClientClick="return validar()" OnClick="btnGuardar_Click" ID="btnGuardar" runat="server" />
            <a href="/">Regresar</a>
        </div>
    </div>
</asp:Content>

<%--Bootstrap usa un sistema de grilla de 12 columnas.
Una row es una fila y dentro se colocan columnas (col-*).
Cada col-md-4 ocupa 4 de las 12 columnas totales.
Si la suma de las columnas es menor o igual a 12, se muestran una al lado de la otra.
El prefijo md indica que el diseño horizontal se aplica en pantallas medianas o grandes;
en pantallas chicas, las columnas se apilan automáticamente.--%>

<%--queda así:

12 columnas totales
┌────4────┬────4────┬────4────┐
│Form     │Imagen   │ vacío   │


Primeras 4 columnas → Email, Nombre, Apellido, etc.

Siguientes 4 columnas → Imagen de perfil y botón de archivo para traer imagen de pc .

Últimas 4 columnas → quedan vacías, no pasa nada, simplemente no se usan.

Frase clave (guardala) vos no entendias porque btn guardar y regresar quedaron en la primer columna

y eso es porque Cada row empieza de nuevo desde la columna 1.--%>

<%--Cuando Bootstrap lee esto:

<div class="row">
    <div class="col-md-4">...</div>
    <div class="col-md-4">...</div>
</div>


razona así (mentalmente):

“Estoy en la misma row.
El primer col-md-4 ya ocupó las primeras 4 columnas.
Todavía quedan columnas libres.
Entonces este segundo col-md-4 se coloca a la derecha, ocupando las siguientes 4 columnas.”--%>