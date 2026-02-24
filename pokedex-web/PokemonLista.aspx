<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="PokemonLista.aspx.cs" Inherits="pokedex_web.PokemonLista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Lista de Pokemons</h1>
    <div class="row">
        <div class="col-6">
            <div class="mb-3">
                <asp:Label Text="Filtrar" runat="server" />
                <asp:TextBox runat="server" ID="txtFiltro" CssClass="form-control" AutoPostBack="true" OnTextChanged="filtro_TextChanged" />
            </div>
        </div>
    <%-- //filtro avanzado--%>
    <%--El atributo class pertenece a HTML y permite asignar clases CSS--%>
    <div class="col-6" style="display: flex; flex-direction: column; justify-content: flex-end;">
        <div class="mb-3">
            <asp:CheckBox Text="Filtro Avanzado"
                CssClass="" ID="chkAvanzado" runat="server"
                AutoPostBack="true"
                OnCheckedChanged="chkAvanzado_CheckedChanged" />
        </div>
    </div>
</div>
    <%--min 5:55--%>
   <%if(chkAvanzado.Checked)
       { %>
    <div class="row">
        <div class="col-3">
            <div class="mb-3">
                <asp:Label Text="Campo" ID="lblCampo" runat="server" />
                <asp:DropDownList runat="server" AutoPostBack="true" CssClass="form-control" ID="ddlCampo" OnSelectedIndexChanged="ddlCampo_SelectedIndexChanged">
                    <asp:ListItem Text="Nombre" />
                    <asp:ListItem Text="Tipo" />
                    <asp:ListItem Text="Número" />
                </asp:DropDownList>
            </div>
        </div>
        <div class="col-3">
            <div class="mb-3">
                <asp:Label Text="Criterio" runat="server" />
                <asp:DropDownList runat="server" ID="ddlCriterio" CssClass="form-control"></asp:DropDownList>
            </div>
        </div>
        <div class="col-3">
            <div class="mb-3">
                <asp:Label Text="Filtro" runat="server" />
                <asp:TextBox runat="server" ID="txtFiltroAvanzado" CssClass="form-control"/>
            </div>
        </div>
        <div class="col-3">
            <div class="mb-3">
                <asp:Label Text="Estado" runat="server" />
                <asp:DropDownList runat="server" ID="ddlEstado" CssClass="form-control">
                    <asp:ListItem Text="Todos" />
                    <asp:ListItem Text="Activo" />
                    <asp:ListItem Text="Inactivo" />
                </asp:DropDownList>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-6">
            <div class="mb-3">
                <asp:Button Text="Buscar" runat="server" CssClass="btn btn-primary" ID="btnBuscar" OnClick="btnBuscar_Click" />
            </div>
        </div>
    </div>
    <%} %>

    <asp:GridView ID="dgvPokemons" runat="server" DataKeyNames="Id"
        CssClass="table" AutoGenerateColumns="false"
        OnSelectedIndexChanged="dgvPokemons_SelectedIndexChanged"
        OnPageIndexChanging="dgvPokemons_PageIndexChanging"
        AllowPaging="true" PageSize="5">
        <Columns>
            <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
            <asp:BoundField HeaderText="Número" DataField="Numero" />
            <asp:BoundField HeaderText="Tipo" DataField="Tipo.Descripcion" />
            <asp:CheckBoxField HeaderText="Activo" DataField="Activo" />
            <asp:CommandField HeaderText="Acción" ShowSelectButton="true" SelectText="📋" />
        </Columns>
    </asp:GridView>
    <a href="FormularioPokemon.aspx" class="btn btn-primary">Agregar</a>
</asp:Content>


<%--cundo maxi dice el id de un "registro" registro es una fila de la clase de datos, cada fila tiene un id --%>

<%--DataKeyNames="Id" Le estás diciendo al GridView, Guarda el Id de cada fila (aunque no lo muestre en pantalla), porque lo va a necesitar si el usuario toca algo como "seleccionar"."--%>

<%-- 
       AutoGenerateColumns: si lo ponés en "true", vas a ver todas las columnas tal cual vienen de la 
       base de datos, sin que tengas que definir nada manualmente. Ahora, si lo ponés en "false", estás 
       diciendo que vas a definir vos manualmente las columnas que querés mostrar y cómo las querés 
       mostrar, usando <asp:BoundField>, <asp:CommandField>, <Columns>, etc.
--%>

<%--bueno DataKeyNames="Id" funciona asi, es un atributo del gridview, y lo que le dice al control
        osea al gridview es que "gaurde el "id" de cada fila" nada mas, ni nada menos, ahora bien vos tenes 
        un evento que se llama OnSelectedIndexChanged="dgvPokemons_SelectedIndexChanged" que se va a disparar
        cuando el usuario seleccione una fila osea haga click en el btn de seleccion de esa fila que en este caso
        es cuando se aprieta este icono 📋 <-- cuando se aprieta ese btn, el gridview llama al metodo que 
        vos pongas aca adentro OnSelectedIndexChanged="  " <-- en este caso esta --> "dgvPokemons_SelectedIndexChanged"
        pero podria ser cualquier otro, pero bueno aca se ejecuta el "dgvPokemons_SelectedIndexChanged" que esta en el
        code behind, en el momento en el que nosotros hicimos clic en 📋 el GridView selecciona esa fila y actualiza 
        su propiedad SelectedDataKey <-- "se ve en el codebehind" ahi "SelectedDataKey.Value" te devuelve el valor del campo
        que pusiste en DataKeyNames, es decir, el Id del Pokémon seleccionado. Con SelectedDataKey.Value obtenés el valor 
        del Id de la fila seleccionada y Usás ese valor para redireccionar al formulario con el parámetro id en la URL 
        (por ejemplo, FormularioPokemon.aspx?id=12) Sin DataKeyNames="Id", el GridView no sabría qué columna usar como clave, 
        y SelectedDataKey.Value sería null o te daría error.
        
        En resumen:
        Sí, tu DataKeyNames="Id" es lo que le permite al GridView guardar el Id de cada Pokémon y recuperarlo cuando se selecciona una fila.
        Es fundamental para que tu evento funcione correctamente.--%>


<%--El propósito principal del evento OnPageIndexChanging es permitirte actualizar el índice de la página cuando el usuario  
        hace click en un número de página (o en “siguiente/anterior”) en la paginación del GridView.--%>
<%--Como funciona: El GridView detecta que se hizo clic en un botón de paginación y dispara el evento OnPageIndexChanging.  
        El evento automáticamente llama a tu método manejador que definiste en el atributo, o sea: "dgvPokemons_PageIndexChanging",  
        y se ejecuta el método que tenés en el back donde:  
        - El parámetro `GridViewPageEventArgs e` recibe el índice de la página nueva y luego se accede a través de `e.NewPageIndex`.  
        - Actualizás el índice de página del GridView con `dgvPokemons.PageIndex = e.NewPageIndex;`.  
        - Volvés a enlazar los datos con `dgvPokemons.DataBind();` o recargando tu lista de datos, para que el GridView muestre los registros correctos de la nueva página.  
        - Como resultado, el GridView actualiza su contenido y muestra la página que el usuario seleccionó, manteniendo el paginado funcionando correctamente.--%>


<%--GridViewPageEventArgs e :incluye el índice de la página a la que el usuario quiere cambiar, 
        el indice arranca del cero como un vector, el cero es el uno el uno es el dos y asi..   --%>

<%--object sender: es el objeto que disparó el evento en este caso es el GridView (dgvPokemons).--%>

<%--AllowPaging="True" :Esto activa la paginación en el GridView, Si no lo activás, el GridView mostraría todas las filas de la fuente de datos en una sola página.--%>

<%--PageSize="5" me dice cuantas filas voy a tener en cada pagina--%> 

        
       
        