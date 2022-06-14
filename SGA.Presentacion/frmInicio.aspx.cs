using SGA.ENTIDADES;
using SGA.LogicaNegocio;
using SGA.Utilitarios;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

namespace SGA.Presentacion
{
    public partial class frmInicio : System.Web.UI.Page
    {
        #region Variables Globales
        clsCNUsuario usuario = new clsCNUsuario();
        clsWebJScript script = new clsWebJScript();
        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == true) return;
            this.TxtUsuario.Focus();
            Session.Clear();
            
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.TxtUsuario.Text.Trim()))
            {
                script.Mensaje("Ingrese su Usuario.");
                TxtUsuario.Focus();
                return;
            }

            if (string.IsNullOrEmpty(this.TxtPassword.Text.Trim()))
            {
                script.Mensaje("Ingrese su Password.");
                TxtPassword.Focus();
                return;
            }

            string cUsuario = TxtUsuario.Text.Trim();

            DataTable dtDatUsuario = usuario.ListarDatosUsuario(cUsuario);
            
            if (dtDatUsuario.Rows.Count <= 0)
            {
                script.Mensaje("Ingrese valores correctos.");
                return;
            }

            string cPassword = dtDatUsuario.Rows[0]["cPassword"].ToString();
            EntityLayer.clsVarGlobal.cConexString = ConfigurationManager.ConnectionStrings["conexionSGA"].ConnectionString;

            //if (autentica.Autenticar(cUsuario, TxtPassword.Text.Trim()))
             if (TxtPassword.Text.Trim()== cPassword)
            { 
                int idUsuario = Convert.ToInt32(dtDatUsuario.Rows[0]["idUsuario"]);
                 

                //---- Rellenando los datos de USUARIO --------------------------------->
                clsUsuario ObjetoUsuario = new clsUsuario();
                ObjetoUsuario.idCli = Convert.ToInt32(dtDatUsuario.Rows[0]["idCli"]);
                ObjetoUsuario.idUsuario = Convert.ToInt32(dtDatUsuario.Rows[0]["idUsuario"]);
                ObjetoUsuario.cUsuario = Convert.ToString(dtDatUsuario.Rows[0]["cWinUser"]);
                ObjetoUsuario.cNombres = Convert.ToString(dtDatUsuario.Rows[0]["cNombre"]) + " "+ Convert.ToString(dtDatUsuario.Rows[0]["cApellidoPaterno"]) +" "+ Convert.ToString(dtDatUsuario.Rows[0]["cApellidoMaterno"]);
                ObjetoUsuario.cApellidoPaterno = Convert.ToString(dtDatUsuario.Rows[0]["cApellidoPaterno"]);
                ObjetoUsuario.cApellidoMaterno = Convert.ToString(dtDatUsuario.Rows[0]["cApellidoMaterno"]);
                ObjetoUsuario.cNombre = Convert.ToString(dtDatUsuario.Rows[0]["cNombre"]);
                ObjetoUsuario.cWinuser = Convert.ToString(dtDatUsuario.Rows[0]["cWinUser"]); 
                ObjetoUsuario.cDNI = Convert.ToString(dtDatUsuario.Rows[0]["cDocumentoID"]);
                ObjetoUsuario.idSexo = Convert.ToInt32(dtDatUsuario.Rows[0]["idSexo"]);
                ObjetoUsuario.dFecSystem = Convert.ToDateTime(dtDatUsuario.Rows[0]["dFecSystem"]);
                ObjetoUsuario.nIdAgencia = Convert.ToInt32(dtDatUsuario.Rows[0]["idAgencia"]);
                ObjetoUsuario.lCambioClave = Convert.ToBoolean(dtDatUsuario.Rows[0]["lCambioClave"]);
                ObjetoUsuario.idEstado = Convert.ToInt32(dtDatUsuario.Rows[0]["idEstado"]);
                ObjetoUsuario.idCargo = Convert.ToInt32(dtDatUsuario.Rows[0]["idCargo"]);
                ObjetoUsuario.dFechaIngreso = Convert.ToDateTime(dtDatUsuario.Rows[0]["dFechaIngreso"]);
                ObjetoUsuario.dFechaCese = Convert.ToDateTime(dtDatUsuario.Rows[0]["dFechaCese"]);
                ObjetoUsuario.cNombreAge = Convert.ToString(dtDatUsuario.Rows[0]["cNombreAge"]);
                lblOficina.Text ="Usted ingresará a: " + ObjetoUsuario.cNombreAge;

                Session["DatosUsuarioSession"] = ObjetoUsuario;
                             
                Session["TmpIdUsuario"] = ObjetoUsuario.idUsuario;

                //-------------- Verificar que el usuario haya cambiado su contraseña ---------->
                if (ObjetoUsuario.lCambioClave)
                {
                    script.Mensaje("Debe cambiar su contraseña por primera vez...");
                    txtClaveAnterior.Enabled = false;
                    txtClaveAnterior.Text = cUsuario;
                    hClaveAnterior.Value = cUsuario;
                    HabilitarPanelNuevoPassword();
                    return;
                }
                //----------------------------------------------------------------------------->                

                PermitirIngresoPorPerfil(idUsuario);

                lnkCambioClave.Visible = false;
            }
            else
            {
                script.Mensaje("El usuario o la contraseña no son Correctos.");
                this.TxtPassword.Focus();
            }
        }

        protected void BotonAceptar_Click(object sender, EventArgs e)
        {
            Ingresar();
        }

        protected void BotonCancelar_Click(object sender, EventArgs e)
        {
            TxtUsuario.Enabled = true;
            TxtPassword.Enabled = true;
            btnIngresar.Visible = true;

            pnlDatosUsuario.Visible = false;
            BotonAceptar.Visible = true;
            TxtUsuario.Focus();
        }

        protected void BotonAceptar0_Click(object sender, EventArgs e)
        {
            try
            {
                if (CamposSonValidos() == false) return;
                
                string cNuevoPassord = txtNuevaClave.Text.Trim();
                string cUsuario = this.txtClaveAnterior.Text.Trim();
                
                DataTable dtRpta = usuario.ActualizarNuevoPassword(cUsuario, cNuevoPassord);

                if (dtRpta.Rows.Count == 0)
                {
                    script.Mensaje("no tiene permisos para poder cambiar su contraseña.");
                    return;
                }

                int idUsuario = Convert.ToInt32(dtRpta.Rows[0]["idUsuario"]);
                script.Mensaje("Su contraseña se ha cambiado correctamente...");

                txtClaveAnterior.Enabled = true;
                pnlCambioContrasenia.Visible = false;

                PermitirIngresoPorPerfil(idUsuario);

                Session["TmpIdUsuario"] = idUsuario;

                lnkCambioClave.Visible = false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void BotonCancelar0_Click(object sender, EventArgs e)
        {
            //Nuevo formulario
            pnlCambioContrasenia.Visible = false;
            txtClaveAnterior.Text = "";
            txtClaveAnterior.Enabled = true;
            txtNuevaClave.Text = "";
            txtNuevaClave1.Text = "";

            //anterior formulario
            TxtUsuario.Enabled = true;
            TxtPassword.Enabled = true;

            TxtUsuario.Focus();
        }

        #endregion

        #region Metodos

        private Boolean CamposSonValidos()
        {
            if (string.IsNullOrEmpty(this.txtClaveAnterior.Text))
            {
                script.Mensaje("Escriba su Usuario.");
                return false;
            }
            if (string.IsNullOrEmpty(this.txtNuevaClave.Text))
            {
                script.Mensaje("Escriba la nueva contraseña.");
                txtNuevaClave.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(this.txtNuevaClave1.Text))
            {
                script.Mensaje("Vuelva a escribir la nueva contraseña.");
                txtNuevaClave1.Focus();
                return false;
            }

            string cNuevoPassord = txtNuevaClave.Text.Trim();
            string cNuevoPassord1 = txtNuevaClave1.Text.Trim();

            if (cNuevoPassord.Equals(cNuevoPassord1) == false)
            {
                script.Mensaje("Las nuevas contraseñas no coinciden.");
                return false;
            }
            return true;
        }

        private void Ingresar()
        {
            //---- Rellenando los datos de USUARIO --------------------------------->
            clsUsuario ObjetoUsuario = new clsUsuario();
            ObjetoUsuario=(clsUsuario)Session["DatosUsuarioSession"];

            int idPerfil = Convert.ToInt32(cboPerfil.SelectedItem.Value);
            string cPerfil = cboPerfil.SelectedItem.Text;
            ObjetoUsuario.idPerfil = idPerfil;
            ObjetoUsuario.cPerfil = cPerfil;
            ObjetoUsuario.cNamePc = "Local";
            ObjetoUsuario.cMacPc = "00000000000";

            Session["DatosUsuarioSession"] = ObjetoUsuario;
            Response.Redirect("frmPrincipal.aspx");
        }

        private void PermitirIngresoPorPerfil(int idUsuario)
        {
            TxtUsuario.Enabled = false;
            TxtPassword.Enabled = false;
            btnIngresar.Visible = false;

            pnlDatosUsuario.Visible = true;

            lblMensajeError.Text = "";


            GEN.CapaNegocio.clsCNPerfilUsu Perfiles = new GEN.CapaNegocio.clsCNPerfilUsu();
            List<EntityLayer.clsPerfilUsu> lisPerfiles = new List<EntityLayer.clsPerfilUsu>();
            lisPerfiles = Perfiles.ListarPerUsu(idUsuario);

            //DataTable dtPerfiles = usuario.ObtenerPerfilesUsuarioPorId(idUsuario);
            cboPerfil.DataSource = lisPerfiles;//dtPerfiles;
            cboPerfil.DataValueField = "idPerfil";
            cboPerfil.DataTextField = "cPerfil";
            cboPerfil.DataBind();

            if (lisPerfiles.Count == 0)
            {
                lblMensajeError.Text = "Usted aún no tiene ningún PERFIL ...";
                BotonAceptar.Visible = false;
                return;
            }
            else
            {
                clsUsuario ObjetoUsuario = new clsUsuario();
                ObjetoUsuario = (clsUsuario)Session["DatosUsuarioSession"];
                ObjetoUsuario.idPerfil = lisPerfiles[0].idPerfil;
                ObjetoUsuario.cPerfil = lisPerfiles[0].cPerfil;
                Session["DatosUsuarioSession"] = ObjetoUsuario;
            }
            this.BotonAceptar.Focus();
        }

        private void HabilitarPanelNuevoPassword()
        {
            lnkCambioClave.Visible = false;
            TxtUsuario.Enabled = false;
            TxtPassword.Enabled = false;
            pnlCambioContrasenia.Visible = true;
            TxtUsuario.Text = hClaveAnterior.Value;
        }

        #endregion

        protected void cboPerfil_SelectedIndexChanged(object sender, EventArgs e)
        {
            var idPerfil = Convert.ToInt32(cboPerfil.SelectedValue);
            clsUsuario ObjetoUsuario = new clsUsuario();
            ObjetoUsuario = (clsUsuario)Session["DatosUsuarioSession"];
            GEN.CapaNegocio.clsCNPerfilUsu Perfiles = new GEN.CapaNegocio.clsCNPerfilUsu();
            List<EntityLayer.clsPerfilUsu> lisPerfiles = new List<EntityLayer.clsPerfilUsu>();
            lisPerfiles = Perfiles.ListarPerUsu(ObjetoUsuario.idUsuario);            
            ObjetoUsuario.idPerfil = lisPerfiles.Where(x => x.idPerfil == idPerfil).ToList()[0].idPerfil;
            ObjetoUsuario.cPerfil = lisPerfiles.Where(x => x.idPerfil == idPerfil).ToList()[0].cPerfil;
            Session["DatosUsuarioSession"] = ObjetoUsuario;
        }

        
    }
}