using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using System.Drawing;
using System.Web.Services.Description;
using System.Security.Policy;
using System.Security.Cryptography;


namespace pokedex_web
{
    public partial class Default : System.Web.UI.Page
    {
        public List<Pokemon> ListaPokemon = new List<Pokemon>();
        protected void Page_Load(object sender, EventArgs e)
        {
            PokemonNegocio negocio = new PokemonNegocio();
            ListaPokemon = negocio.listarConSP();

            if (!IsPostBack)
            {
                repRepetidor.DataSource = ListaPokemon;
                repRepetidor.DataBind();
            }
            


        }

        protected void btnEjemplo_Click(object sender, EventArgs e)
        {
            string valor = ((Button)sender).CommandArgument; //<--Acá está el Id que vino desde el Eval("Id")

        }



        //<%--Cuando usás object como tipo de parámetro(como en sender), estás diciendo:
        //"Acepto cualquier cosa", pero si necesitás trabajar con ese “algo”, tenés que
        //convertirlo al tipo correcto(por ejemplo, Button).--%>
        //object es la clase mama y papa de todas las clases 

        //Si algo se declara como object, solo podés acceder a las propiedades y métodos de 
        //la clase object, NO a las del tipo real(por ejemplo Button) hasta que lo castées.

        //Una vez que la persona genera el estímulo en el botón, btnEjemplo_Click recibe 
        //argumentos a través de sus parámetros.El sender es un parámetro del tipo object,
        //y en tiempo de ejecución recibe como argumento al control que disparó el evento, 
        //por ejemplo, un <asp:Button 

        //El parámetro sender del método es de tipo object, pero lo que realmente Le llega al sander
        //como argumento es el botón que disparó el evento (<asp:Button>).

        //Como sender es object, no podés usar propiedades específicas del botón(como CommandArgument)
        //sin hacer un casteo.

    }
}