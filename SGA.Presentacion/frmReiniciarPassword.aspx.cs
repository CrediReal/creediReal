using SGA.ENTIDADES;
using SGA.LogicaNegocio;
using SGA.Utilitarios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGA.Presentacion
{
    public partial class frmReiniciarPassword : System.Web.UI.Page
    {
        #region Variables_Globales
        clsCNUsuario Usuario = new clsCNUsuario();
        clsWebJScript Script = new clsWebJScript();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["usuario"] != null)
                {
                    Session["DatosUsuarioSession"] = new SGA.LogicaNegocio.clsCNUsuario().GetDatosUsuario(Request.QueryString["usuario"].ToString());
                }
                if (Request.QueryString["perfil"] != null)
                {
                    hPerfil.Value=Request.QueryString["perfil"].ToString();
                }
                if (IsPostBack == true) return;

                //------------------Valida Session Vigente -------------------------------------->
                if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor"); }
                //------------------------------------------------------------------------------>


                //----------------- TITULO ------------------------>
                lblOpcion.Text = Session["cOpcion"].ToString().ToUpper() + ":";
                //-------------------------------------------------->
                pnlDetalle.Visible = false;

                hTipoOperacion.Value = "0"; //1:Nuevo
                //2:Edición

                //-- Cargar Proyectos a los que está relacionado el ADMINISTRADOR --------------> 
                var usuarioSession = (clsUsuario)Session["DatosUsuarioSession"];

                int idPerfil = Convert.ToInt32(hPerfil.Value);

                
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void cboProyecto_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                pnlDetalle.Visible = false;
                LimpiarControles();
                hTipoOperacion.Value = "0";

                //--------- Búsqueda usuario  -------------->
                txtNombreUsuario.Enabled = true;
                BotonConsultar1.Enabled = true;
                BotonEditar1.Visible = false;
                txtNombreUsuario.Focus();
                //------------------------------------------>

                txtNombreUsuario.Text = "";
                txtNombreUsuario.Focus();
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void BotonConsultar1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtNombreUsuario.Text))
                {
                    Script.Mensaje("Debe Ingresar Apellidos de Usuario a Buscar.");
                    lstUsuarios.DataSource = null;
                    lstUsuarios.Items.Clear();
                    return;
                }

                //------------------Valida Session Vigente -------------------------------------->
                if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor"); }
                //------------------------------------------------------------------------------>
                
                //Buscar al usuario por apellido paterno por sus apellidoS
                DataTable dtUsuario = Usuario.BuscarUsuarioPorApellido(txtNombreUsuario.Text.Trim().ToUpper());

                if (dtUsuario.Rows.Count == 0)
                {
                    Script.Mensaje("No se ha encontrado Usuarios con el apellido paterno: " + txtNombreUsuario.Text.Trim().ToUpper());
                    lstUsuarios.DataSource = null;
                    lstUsuarios.Items.Clear();
                    return;
                }

                Session["dtUsuario"] = dtUsuario;
                lstUsuarios.DataSource = dtUsuario;
                lstUsuarios.DataTextField = "cNombres";
                lstUsuarios.DataValueField = "idUsuario";
                lstUsuarios.DataBind();

                lstUsuarios.Visible = true;
                BotonEditar1.Visible = true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void BotonEditar1_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstUsuarios.SelectedItem == null)
                {
                    Script.Mensaje("Debe seleccionar el usuario al que desea reiniciar su contraseña");
                    return;
                }

                pnlDetalle.Visible = true;

                //Ubicar al usuario específico
                int indice = lstUsuarios.SelectedIndex;

                DataTable dtUsuario = (DataTable)(Session["dtUsuario"]);

                txtCodigo.Text = dtUsuario.Rows[indice]["idUsuario"].ToString();
                txtUsuario.Text = dtUsuario.Rows[indice]["cUsuario"].ToString();
                txtApellidoPaterno.Text = dtUsuario.Rows[indice]["cApellidoPaterno"].ToString();
                txtApellidoMaterno.Text = dtUsuario.Rows[indice]["cApellidoMaterno"].ToString();
                txtNombre1.Text = dtUsuario.Rows[indice]["cNombre"].ToString();
                txtNombre2.Text = dtUsuario.Rows[indice]["cNombreSeg"].ToString();
                NumberBox1.Text = dtUsuario.Rows[indice]["cDNI"].ToString();
                ComboBoxBaseSexo2.SelectedValue = dtUsuario.Rows[indice]["idSexo"].ToString();
                chcVigente.Checked = Convert.ToBoolean(dtUsuario.Rows[indice]["lVigente"]);

                BotonEditar1.Visible = false;

                txtDNI.Enabled = false;
                hTipoOperacion.Value = "2";

                lstUsuarios.Enabled = false;
                BotonConsultar1.Enabled = false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void BotonCancelar1_Click(object sender, EventArgs e)
        {
            pnlDetalle.Visible = false;

            LimpiarControles();

            hTipoOperacion.Value = "0";

            //--------- Búsqueda usuario  -------------->
            txtNombreUsuario.Enabled = true;
            BotonConsultar1.Enabled = true;
            BotonEditar1.Visible = true;
            txtNombreUsuario.Focus();
            //------------------------------------------>

            lstUsuarios.Enabled = true;
            BotonConsultar1.Enabled = true;
        }

        private void LimpiarControles()
        {
            txtCodigo.Text = "";
            txtNombre1.Text = "";
            txtNombre2.Text = "";
            txtApellidoPaterno.Text = "";
            txtApellidoMaterno.Text = "";
            NumberBox1.Text = "0";
            chcVigente.Checked = false;

            txtUsuario.Text = "";

            txtDNI.Enabled = true;
        }

        protected void OnConfirm(object sender, EventArgs e)
        {
            try
            {
                string confirmValue = Request.Form["confirm_value"];
                if (confirmValue == "Si")
                {
                    //------------------Valida Session Vigente -------------------------------------->
                    if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor"); }
                    //------------------------------------------------------------------------------>

                    DataTable dtUsuario = (DataTable)Session["dtUsuario"];

                    if (dtUsuario.Rows.Count == 0)
                    {
                        Script.Mensaje("Problemas al seleccionar el usuario, intente otra vez.");
                        return;
                    }

                    int indice = lstUsuarios.SelectedIndex;

                    int idUsuario = Convert.ToInt32(dtUsuario.Rows[indice]["idUsuario"]);
                    string cNombre = dtUsuario.Rows[indice]["cNombre"].ToString();
                    string cApellidoPaterno = dtUsuario.Rows[indice]["cApellidoPaterno"].ToString();
                    string cDNI = dtUsuario.Rows[indice]["cDNI"].ToString();

                    //--------- Campos de Auditoría --------------------->
                    var DatosUsuarioSession = (clsUsuario)Session["DatosUsuarioSession"];

                    int idUsuarioReg = DatosUsuarioSession.idUsuario;
                    string cNombrePc = DatosUsuarioSession.cNamePc;
                    string cMacPc = DatosUsuarioSession.cMacPc;
                    //--------------------------------------------------->

                    var cPassXDefecto = Usuario.crearLoginUsuario(cNombre, cApellidoPaterno, cDNI);
                    cPassXDefecto = clsCriptografia.EncriptarPassword(cPassXDefecto);

                    pnlDetalle.Visible = false;
                    LimpiarControles();

                    //--------- Búsqueda usuario  -------------->
                    txtNombreUsuario.Enabled = true;
                    BotonConsultar1.Enabled = true;
                    lstUsuarios.Visible = false;
                    BotonEditar1.Visible = false;
                    txtNombreUsuario.Text = "";
                    txtNombreUsuario.Focus();
                    //------------------------------------------>

                    //Resetear la contraseña
                    DataTable dtRpta = Usuario.ReiniciarClaveXDefecto(idUsuario, cPassXDefecto, idUsuarioReg, cNombrePc, cMacPc);
                    Script.Mensaje("La contraseña ha sido reiniciada correctamente...");

                    lstUsuarios.Enabled = true;
                    BotonConsultar1.Enabled = true;
                }
                else
                {
                   
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}