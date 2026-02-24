using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace pokedex_web
{
    public partial class MiPerfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Seguridad.sesionActiva(Session["trainee"]))
                    {
                        Trainee user = (Trainee)Session["trainee"];
                        txtEmail.Text = user.Email;
                        txtEmail.ReadOnly = true;
                        txtNombre.Text = user.Nombre;
                        txtApellido.Text = user.Apellido;
                        if (user.FechaNacimiento.HasValue) //punto 1 esto me lo paso el chat para poder guardar la fecha 
                        {
                            txtFechaNacimiento.Text = user.FechaNacimiento.Value.ToString("yyyy-MM-dd");
                        }
                        if (!string.IsNullOrEmpty(user.ImagenPerfil))
                            imgNuevoPerfil.ImageUrl = "~/Images/" + user.ImagenPerfil;
                    }
                }

            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {

                Page.Validate();
                if (!Page.IsValid)
                    return;

                TraineeNegocio negocio = new TraineeNegocio();
                Trainee user = (Trainee)Session["trainee"];
                //Escribir img si se cargó algo.
                if (txtImagen.PostedFile.FileName != "")
                {
                    string ruta = Server.MapPath("./Images/");
                    txtImagen.PostedFile.SaveAs(ruta + "perfil-" + user.Id + ".jpg");
                    user.ImagenPerfil = "perfil-" + user.Id + ".jpg";
                }

                user.Nombre = txtNombre.Text;
                user.Apellido = txtApellido.Text;

                if (!string.IsNullOrEmpty(txtFechaNacimiento.Text))//punto 2 esto me lo paso el chat para que me tome la fecha la bd y dejar bien el btn 
                {
                    user.FechaNacimiento = DateTime.Parse(txtFechaNacimiento.Text);
                }
                else
                {
                    user.FechaNacimiento = null; 
                }

                //guardar datos perfil
                negocio.actualizar(user);
                Session["trainee"] = user;

                //leer img
                Image img = (Image)Master.FindControl("imgAvatar");
                img.ImageUrl = "~/Images/" + user.ImagenPerfil;

            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
            }
        }

    }
}
////Una Session es un espacio de tiempo que dura o empieza cuando:

//El usuario entra a la aplicación o al sitio web.

//Y termina cuando:

//El usuario cierra el navegador, o cuando se pierde el identificador de sesión como pasa aca
//cada ves que cerras la pagina y volves a abrirla nace una session nueva. 

//¿Qué ES Session?

//Una cajita en memoria del servidor, asociada a un usuario, que vive un tiempo limitado