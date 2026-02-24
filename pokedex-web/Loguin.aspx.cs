using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace pokedex_web
{
    public partial class Loguin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //lo que esta adentro de este btn me lo paso el chat gpt esta prolijo el que habia mostrado
        //maxi es un video viejo y dio error por eso necesite cambiarlo por ahora quedo asi y compila
        //joya. sigamos......
        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                UsuarioNegocio negocio = new UsuarioNegocio();
                Usuario usuario = new Usuario(txtUser.Text, txtPassword.Text, false);

                if (negocio.Loguear(usuario))
                {
                    Session["usuario"] = usuario;
                    Redirigir("MenuLogin1Ejemplo.aspx");
                }
                else
                {
                    Session["error"] = "Usuario o contraseña incorrectos";
                    Redirigir("Error.aspx");
                }
            }
            catch (Exception ex)
            {
                Session["error"] = ex.Message;
                Redirigir("Error.aspx");
            }
        }

        private void Redirigir(string url)
        {
            Response.Redirect(url, false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}