using dominio;
using negocio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace pokedex_web
{
    public partial class FormularioPokemon : System.Web.UI.Page
    {
        public bool ConfirmaEliminacion { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            txtId.Enabled = false;
            ConfirmaEliminacion = false;
            try
            {

                //configuracion inicial de la pantalla
                if (!IsPostBack)
                {
                    ElementoNegocio negocio = new ElementoNegocio();
                    List<Elemento> lista = negocio.listar(); //Se crea una variable tipo lista, se crea la lista para guardar en memoria la lista de objetos que vas a usar para cargar los DropDownList, NO se crea directamente una lista nueva con new List<Elemento>() desde afuera, porque esa lista ya se creó y se llenó adentro del método listar()

                    ddlTipo.DataSource = lista;
                    ddlTipo.DataValueField = "id"; // Aca el sistema guarda El nombre de la propiedad de la clase Pero cuando el usuario seleccione una opción, el valor que se guarda internamente es el id
                    ddlTipo.DataTextField = "Descripcion"; // aca el sistema guarda el nombre de la propiedad de la clase y Mostrale al usuario el texto de la propiedad
                    ddlTipo.DataBind();

                    ddlDebilidad.DataSource = lista;
                    ddlDebilidad.DataValueField = "id";  // Aca el sistema guarda El nombre de la propiedad de la clase Pero cuando el usuario seleccione una opción, el valor que se guarda internamente es el id
                    ddlDebilidad.DataTextField = "Descripcion"; // Aca el sistema guarda El nombre de la propiedad de la clase y Mostrale al usuario el texto de la propiedad
                    ddlDebilidad.DataBind();


                }

                //configuracion si estamos modificando
                string id = Request.QueryString["id"] != null ? Request.QueryString["id"].ToString() : ""; // <--ESTO TE LO EXPLICO EN AYUDA MEMORIA WORD SE LLAMA "OPERADOR TERNARIO MAXI CURSO 3" pero tecnicamente pregunta, si request.querystring es distinto de nulo, entonces le voy a guardar lo que tiene el queristring.tostring, sino le guardamos vacio ""
                if (id != "" && !IsPostBack) //aca preguntamos si tengo un id, osea si es distinto de null es porque esta viniendo un dato informado osea cuando carga la pagina si trajo un id tengo que ir a la base de datos a traerme ese pokemon.
                {
                    // traer la lista completa filtrar la lista aca y quedarme con pkmon que quiero metodo(listar) ver que hace maxi ahi 
                    PokemonNegocio negocio = new PokemonNegocio();
                    //List<Pokemon> lista = negocio.listar(id);
                    //Pokemon seleccionado = lista[0];

                    Pokemon seleccionado = (negocio.listar(id))[0]; //// entonces aca se le pasa el id a listar? ¡Sí, mono, exacto! //Llama al método que DEVUELVE!!! una lista de Pokémon filtrada por ese id, y solo trae el pokemon con ese id. Con o sin paréntesis, hace lo mismo, Se ponen por legibilidad, para remarcar el orden. EL [0] SIGNIFICA "dame el primer elemento de la lista" -- Como estás buscando por ID, la base solo debería devolver un único registro
                    //aclaracion Solo uso new Pokemon() cuando quiero crear un pokemon desde cero

                    //guardo pokemon seleccionado en sesion 
                    Session.Add("pokeSeleccionado", seleccionado);

                    //pre cargar todos los campos...
                    //Una vez que tiene el id "pokemon" seleccionado, se asignan sus propiedades a los campos de la página:
                    txtId.Text = id;
                    txtNombre.Text = seleccionado.Nombre;
                    txtDescripcion.Text = seleccionado.Descripcion;
                    txtImagenUrl.Text = seleccionado.UrlImagen;
                    txtNumero.Text = seleccionado.Numero.ToString();

                    ddlTipo.SelectedValue = seleccionado.Tipo.Id.ToString();
                    ddlDebilidad.SelectedValue = seleccionado.Debilidad.Id.ToString();
                    txtImagenUrl_TextChanged(sender, e); //aca esta forzando el evento porque no se cargaba la imagen 

                    //34 Esto precarga los datos para que el usuario pueda ver o editar la información del Pokémon.

                    //configurar acciones

                    if (!seleccionado.Activo) //si seleccionado no esta activo osea si esta en false entonces ponele reactivar si esta en true queda como viene
                        btnInactivar.Text = "Reactivar";



                }

            }
            catch (Exception ex)
            {

                Session.Add("Error", ex);
                Response.Redirect("Error.aspx");
            }
        }


        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                Pokemon nuevo = new Pokemon();
                PokemonNegocio negocio = new PokemonNegocio(); 

                nuevo.Numero = int.Parse(txtNumero.Text);
                nuevo.Nombre = txtNombre.Text;
                nuevo.Descripcion = txtDescripcion.Text;
                nuevo.UrlImagen = txtImagenUrl.Text;

                nuevo.Tipo = new Elemento(); //recordemos que tipo es un objeto entonces yo voy a tener que agregar aca un nuevo tipo primeroo antes de poder cargar el dato, hay que crear el new elemento para que tenga un objeto y yo pueda decir nuevo.Tipo.id = ddltipo.selectedvalue
                nuevo.Tipo.Id = int.Parse(ddlTipo.SelectedValue); //Guardame el Id que está oculto detrás de la opción que el usuario seleccionó, aca es donde obtenés el ID que corresponde a "Fuego", por ejemplo 2
                nuevo.Debilidad = new Elemento();
                nuevo.Debilidad.Id = int.Parse(ddlDebilidad.SelectedValue); //aca es donde obtenés el ID que corresponde a debilidad es igual a "agua", por ejemplo 3

                if (Request.QueryString["id"] != null)
                {
                    nuevo.Id = int.Parse(txtId.Text);
                    negocio.modificarConSP(nuevo);              //El evento aceptar lo tengo que modificar para que no solo agregue sino que tambien modifique        

                }
                else
                    negocio.agregarConSP(nuevo);

                Response.Redirect("PokemonLista.aspx", false);


            }
            catch (Exception ex)
            {
                Session.Add("Error", ex);
                Response.Redirect("Error.aspx");
            }
        }

        protected void txtImagenUrl_TextChanged(object sender, EventArgs e)
        {
            imgPokemon.ImageUrl = txtImagenUrl.Text;
        }

        protected void BtnEliminar_Click(object sender, EventArgs e)
        {
            ConfirmaEliminacion = true;
        }


        protected void btnConfirmaEliminacion_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkConfirmaEliminacion.Checked)//si se confirma el check entra 
                {
                    PokemonNegocio negocio = new PokemonNegocio();
                    negocio.eliminar(int.Parse(txtId.Text)); //El id lo consigue a travez de la caja de texto monoooo!!!
                    Response.Redirect("pokemonLista.aspx");
                }
            }
            catch (Exception ex)
            {

                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        protected void btnInactivar_Click(object sender, EventArgs e)
        {
            try
            {
                PokemonNegocio negocio = new PokemonNegocio();
                Pokemon seleccionado = (Pokemon)Session["pokeSeleccionado"];
                negocio.eliminarLogico(seleccionado.Id, !seleccionado.Activo);//seleccionado.id que ya tiene un entero y seleccionado.activo que tambien tiene cargado el valor porque todos esos datos se seleccionaron y se guardaron cuando yo recupere el elementoo en el load en el pageload
                Response.Redirect("PokemonLista.aspx");

            }
            catch (Exception ex)
            {

                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }
    }
}

// Los DropDownList o desplegables son controles que funcionan como pares clave-valor.
// Es decir, tienen un dato visible que se muestra en pantalla (por ejemplo: "Aire", "Fuego", "Tierra"),
// y un valor oculto detrás que es el Value, generalmente el Id correspondiente a ese texto.
// Ese Id es el que vamos a querer capturar cuando el usuario haga una selección.

