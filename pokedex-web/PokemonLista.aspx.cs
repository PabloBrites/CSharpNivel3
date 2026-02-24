using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;
using dominio;


namespace pokedex_web
{
    public partial class PokemonLista : System.Web.UI.Page
    {
        public bool FiltroAvanzado { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Seguridad.esAdmin(Session["trainee"]))
            {
                Session.Add("error","Se requiere permisos de admin para acceder a esta pantalla");
                Response.Redirect("Error.aspx");
            }

            FiltroAvanzado = chkAvanzado.Checked;
            if (!IsPostBack) //aca este ispostback no c cuando maxi lo creo lo puse porque justo lo vi que lo tenia 
            {
                PokemonNegocio negocio = new PokemonNegocio();
                Session.Add("listaPokemons", negocio.listarConSP());
                dgvPokemons.DataSource = Session["listaPokemons"]; /*DataSource significa fuente de datos*/
                dgvPokemons.DataBind(); //esto se pone para enlazar los datos mono. 

                ddlCampo_SelectedIndexChanged(null, null); //ESTA LINEA ME LA PASO CHATGPT PARA ARREGLAR FILTRO ACORDATE NO SE PODIA ELEGIR NOMBRE CON ESTO SE ARREGLA 
            }
            
        }

        protected void dgvPokemons_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvPokemons.PageIndex = e.NewPageIndex;
            dgvPokemons.DataSource = Session["listaPokemons"]; //esta linea me la paso el chat gpt para poder volver a ver el paginado acordate 
            dgvPokemons.DataBind();
        }

        protected void dgvPokemons_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = dgvPokemons.SelectedDataKey.Value.ToString();//el selectedDatakey obtiene el valor de la fila seleccionada del datagridview
            Response.Redirect("FormularioPokemon.aspx?id=" + id); //aca enviamos el id a la url
        }

        protected void filtro_TextChanged(object sender, EventArgs e)
        {
            //ver que es lambdaexpreshion
            List<Pokemon> lista = (List<Pokemon>)Session["listaPokemons"];
            List<Pokemon> listaFiltrada = lista.FindAll(x => x.Nombre.ToUpper().Contains(txtFiltro.Text.ToUpper()));
            dgvPokemons.DataSource = listaFiltrada;
            dgvPokemons.DataBind();
        }

        protected void chkAvanzado_CheckedChanged(object sender, EventArgs e)
        {
            FiltroAvanzado = chkAvanzado.Checked;
            txtFiltro.Enabled = !FiltroAvanzado;

            if (!chkAvanzado.Checked) //estas tres lineas me las paso el chat gpt para arreglar el filtro y volver hacia atras cuando se le saca el check
            {
                dgvPokemons.DataSource = Session["listaPokemons"]; 
                dgvPokemons.DataBind();
            }

        }

        protected void ddlCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCriterio.Items.Clear();
            if (ddlCampo.SelectedItem.ToString() == "Número")
            {
                ddlCriterio.Items.Add("Igual a");
                ddlCriterio.Items.Add("Mayor a");
                ddlCriterio.Items.Add("Menor a");
            }
            else
            {
                ddlCriterio.Items.Add("Contiene");
                ddlCriterio.Items.Add("Comienza con");
                ddlCriterio.Items.Add("Termina con");         
                               
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                PokemonNegocio negocio = new PokemonNegocio();
                dgvPokemons.DataSource = negocio.filtrar(
                    ddlCampo.SelectedItem.ToString(),
                    ddlCriterio.SelectedItem.ToString(),
                    txtFiltroAvanzado.Text,
                    ddlEstado.SelectedItem.ToString());
                dgvPokemons.DataBind();

            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                throw;
            }
        }

        //drop dawn list y update panel 
        //configuremos nuestra pantalla para que no c actualice todo cada vez quehcaemos un postback 
    }
}