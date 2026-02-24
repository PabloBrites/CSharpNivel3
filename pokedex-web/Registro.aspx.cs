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
    public partial class Registro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Write(Session.SessionID); codigo para ver tu session no hace falta usarlo salvo que la quieras ver
        }

        protected void btnRegistrarse_Click(object sender, EventArgs e)
        {
            try
            {
                Trainee user = new Trainee();
                TraineeNegocio traineeNegocio = new TraineeNegocio();
                EmailService emailService = new EmailService();

                user.Email = txtEmail.Text;
                user.pass = txtPassword.Text;
                user.Id = traineeNegocio.insertarNuevo(user);
                Session.Add("trainee", user);

                emailService.armarCorreo(user.Email, "¡Bienvenido!", "Gracias por iniciar sesión, ¡nos alegra tenerte acá!");
                emailService.enviarEmail();
                Response.Redirect("Default.aspx" ,false);
            }
            catch (Exception ex)
            {

                Session.Add("error", ex.ToString());
            }
        }
    }
}