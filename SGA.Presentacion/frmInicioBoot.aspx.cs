using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGA.ENTIDADES;
using SGA.LogicaNegocio;
using SGA.Utilitarios;

namespace SGA.Presentacion
{
    public partial class frmInicioBoot : System.Web.UI.Page
    {
        #region Variables Globales
        clsCNUsuario usuario = new clsCNUsuario();
        clsWebJScript script = new clsWebJScript();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (hModulo.Value == "0")
            {
                liPanel.Attributes["class"] = "";
                //liErp.Attributes["class"] = "";
            }
            else if (hModulo.Value == "1")
            {
                liPanel.Attributes["class"] = "active";
               // liErp.Attributes["class"] = "";
            }
            else
            {
                liPanel.Attributes["class"] = "";
                //liErp.Attributes["class"] = "active";
            }

            if (IsPostBack == true) return;
            this.txtUsuario.Focus();


        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(this.txtUsuario.Text.Trim()))
            //{
            //    script.Mensaje("Ingrese su Usuario.");
            //    return;
            //}

            //if (string.IsNullOrEmpty(this.txtClave.Text.Trim()))
            //{
            //    script.Mensaje("Ingrese su Password.");
            //    return;
            //}

            //string cUsuario = txtUsuario.Text.Trim();

            //DataTable dtDatUsuario = usuario.ListarDatosUsuario(cUsuario);

            //if (dtDatUsuario.Rows.Count <= 0)
            //{
            //    script.Mensaje("Ingrese valores correctos.");
            //    return;
            //}

            //string cPassword = clsCriptografia.DesencriptarPassword(dtDatUsuario.Rows[0]["cPassword"].ToString());

            //if (cPassword.Equals(txtClave.Text.Trim()))
            //{
            //    int idUsuario = Convert.ToInt32(dtDatUsuario.Rows[0]["idUsuario"]);
            //    Session["TmpIdUsuario"] = idUsuario;

            //    //-------------- Verificar que el usuario haya cambiado su contraseña ---------->
            //    if (Convert.ToBoolean(dtDatUsuario.Rows[0]["lCambiarClave"]))
            //    {
            //        script.Mensaje("Debe cambiar su contraseña por primera vez...");
            //        txtClaveAnterior.Enabled = false;
            //        txtClaveAnterior.Text = cUsuario;
            //        hClaveAnterior.Value = cUsuario;
            //        HabilitarPanelNuevoPassword();
            //        return;
            //    }
            //    //----------------------------------------------------------------------------->                

            //    PermitirIngresoPorPerfil(idUsuario);

            //}
            //else
            //{
            //    script.Mensaje("El usuario o la contraseña no son Correctos.");
            //    this.txtClave.Focus();
            //}

            if (string.IsNullOrEmpty(this.txtUsuario.Text.Trim()))
            {
                script.Mensaje("Ingrese su Usuario.");
                txtUsuario.Focus();
                return;
            }

            if (string.IsNullOrEmpty(this.txtClave.Text.Trim()))
            {
                script.Mensaje("Ingrese su Password.");
                txtClave.Focus();
                return;
            }

            string cUsuario = txtUsuario.Text.Trim();

            DataTable dtDatUsuario = usuario.ListarDatosUsuario(cUsuario);

            if (dtDatUsuario.Rows.Count <= 0)
            {
                script.Mensaje("Ingrese valores correctos.");
                return;
            }

            string cPassword = dtDatUsuario.Rows[0]["cPassword"].ToString();
            EntityLayer.clsVarGlobal.cConexString = ConfigurationManager.ConnectionStrings["conexionSGA"].ConnectionString;

            //if (autentica.Autenticar(cUsuario, TxtPassword.Text.Trim()))
            if (txtClave.Text.Trim() == cPassword)
            {
                int idUsuario = Convert.ToInt32(dtDatUsuario.Rows[0]["idUsuario"]);


                //---- Rellenando los datos de USUARIO --------------------------------->
                clsUsuario ObjetoUsuario = new clsUsuario();
                ObjetoUsuario.idCli = Convert.ToInt32(dtDatUsuario.Rows[0]["idCli"]);
                ObjetoUsuario.idUsuario = Convert.ToInt32(dtDatUsuario.Rows[0]["idUsuario"]);
                ObjetoUsuario.cUsuario = Convert.ToString(dtDatUsuario.Rows[0]["cWinUser"]);
                ObjetoUsuario.cNombres = Convert.ToString(dtDatUsuario.Rows[0]["cNombre"]) + " " + Convert.ToString(dtDatUsuario.Rows[0]["cApellidoPaterno"]) + " " + Convert.ToString(dtDatUsuario.Rows[0]["cApellidoMaterno"]);
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
                cboSede.SelectedValue = ObjetoUsuario.nIdAgencia.ToString();
                //lblOficina.Text = "Usted ingresará a: " + ObjetoUsuario.cNombreAge;

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

                //lnkCambioClave.Visible = false;
            }
            else
            {
                script.Mensaje("El usuario o la contraseña no son Correctos.");
                this.txtClave.Focus();
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            Ingresar();
        }

        private void PermitirIngresoPorPerfil(int idUsuario)
        {
            //if (hModulo.Value == "1")
            //{
            //    liPanel.Attributes["class"] = "active";
            //}
            //else if (hModulo.Value == "2")
            //{
            //    //liErp.Attributes["class"] = "active";
            //}
            //else
            //{
            //    liPanel.Attributes["class"] = "";
            //    //liErp.Attributes["class"] = "";
            //}
            //txtUsuario.Enabled = false;
            //this.txtClave.Enabled = false;
            //btnIngresar.Visible = false;

            //cboPerfil.Visible = true;
            //btnAceptar.Visible = true;
            //cboSede.Visible = true;


            //DataTable dtPerfiles = usuario.ObtenerPerfilesUsuarioPorId(idUsuario);
            //cboPerfil.DataSource = dtPerfiles;
            //cboPerfil.DataValueField = dtPerfiles.Columns[0].ToString();//idPerfil
            //cboPerfil.DataTextField = dtPerfiles.Columns[1].ToString();//cPerfil
            //cboPerfil.DataBind();

            //if (dtPerfiles.Rows.Count == 0)
            //{
            //    script.Mensaje("Usted aún no tiene ningún PERFIL ...");
            //    btnAceptar.Visible = false;
            //    return;
            //}
            //this.btnAceptar.Focus();

            txtUsuario.Enabled = false;
            txtClave.Enabled = false;
            btnIngresar.Visible = false;
            cboPerfil.Visible = true;
            btnAceptar.Visible = true;
            cboSede.Visible = true;
            cboSede.Enabled = false;
            //pnlDatosUsuario.Visible = true;

            //lblMensajeError.Text = "";


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
                script.Mensaje( "Usted aún no tiene ningún PERFIL ...");
                this.btnAceptar.Visible = false;
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
            this.btnAceptar.Focus();
        }

        private void Ingresar()
        {
            if (hModulo.Value == "0")
            {
                script.Mensaje("Seleccione el módulo a ingresar: PANEL COMERCIAL o ERP");
                return;
            }

            //string cUsuario = txtUsuario.Text.Trim();//.Equals("") ? txtClaveAnterior.Text.Trim() : TxtUsuario.Text.Trim();
            //DataTable dtDatUsuario = usuario.ListarDatosUsuario(cUsuario);

            //int idUsuario = Convert.ToInt32(dtDatUsuario.Rows[0]["idUsuario"]);

            //int idPerfil = Convert.ToInt32(cboPerfil.SelectedItem.Value);
            //string cPerfil = cboPerfil.SelectedItem.Text;

            ////---- Rellenando los datos de USUARIO --------------------------------->
            //clsUsuario ObjetoUsuario = new clsUsuario();
            //ObjetoUsuario.idUsuario = idUsuario;
            //ObjetoUsuario.cUsuario = dtDatUsuario.Rows[0]["cUsuario"].ToString();

            //ObjetoUsuario.cNombres = dtDatUsuario.Rows[0]["cNombres"].ToString();
            //ObjetoUsuario.cApellidoPaterno = dtDatUsuario.Rows[0]["cApellidoPaterno"].ToString();
            //ObjetoUsuario.cApellidoMaterno = dtDatUsuario.Rows[0]["cApellidoMaterno"].ToString();
            //ObjetoUsuario.cNombre = dtDatUsuario.Rows[0]["cNombre"].ToString();
            //ObjetoUsuario.cNombreSeg = dtDatUsuario.Rows[0]["cNombreSeg"].ToString();
            //ObjetoUsuario.cDNI = dtDatUsuario.Rows[0]["cDNI"].ToString();
            //ObjetoUsuario.idSexo = Convert.ToInt32(dtDatUsuario.Rows[0]["idSexo"].ToString());

            //ObjetoUsuario.idPerfil = idPerfil;

            //ObjetoUsuario.cPerfil = cPerfil;
            //ObjetoUsuario.cNamePc = "Local";
            //ObjetoUsuario.cMacPc = "00000000000";
            //ObjetoUsuario.nIdAgencia = Convert.ToInt32(dtDatUsuario.Rows[0]["idOficina"]);

            //Session["TmpIdUsuario"] = idUsuario;
            //Session["idOficina"] = cboSede.SelectedValue;
            //Session["cOficina"] = cboSede.SelectedItem.Text;
            //Session["cOpcion"] = "";

            //Session["DatosUsuarioSession"] = ObjetoUsuario;

            //if (hModulo.Value == "1")
            //{
            //    Response.Redirect("frmPrincipal.aspx");
            //}
            //else
            //{
            //    //Response.Redirect("frmErp.aspx");
            //    Response.Redirect("frmDetalleIndicador.aspx");
            //}

            clsUsuario ObjetoUsuario = new clsUsuario();
            ObjetoUsuario = (clsUsuario)Session["DatosUsuarioSession"];

            int idPerfil = Convert.ToInt32(cboPerfil.SelectedItem.Value);
            string cPerfil = cboPerfil.SelectedItem.Text;
            ObjetoUsuario.idPerfil = idPerfil;
            ObjetoUsuario.cPerfil = cPerfil;
            ObjetoUsuario.cNamePc = "Local";
            ObjetoUsuario.cMacPc = "00000000000";

            Session["DatosUsuarioSession"] = ObjetoUsuario;
            //Response.Redirect("frmPrincipal.aspx");
            Response.Redirect("frmContenidoMenu.aspx");
            
        }

        protected void BotonAceptar0_Click(object sender, EventArgs e)
        {
            try
            {
                if (CamposSonValidos() == false) return;

                string cClaveAnterior = this.txtClaveAnterior.Text.Trim();
                string cNuevoPassord = txtNuevaClave.Text.Trim();
                string cNuevoPassord1 = txtNuevaClave1.Text.Trim();

                string cUsuario = this.txtClaveAnterior.Text.Trim();

                //cNuevoPassord = clsCriptografia.EncriptarPassword(cNuevoPassord);

                DataTable dtRpta = usuario.ActualizarNuevoPassword(cUsuario,cNuevoPassord);

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
            this.txtUsuario.Enabled = true;
            this.txtClave.Enabled = true;
            this.txtUsuario.Text = "";
            this.txtClave.Text = "";

            txtUsuario.Focus();

            btnIngresar.Visible = true;
        }

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
                return false;
            }
            if (string.IsNullOrEmpty(this.txtNuevaClave1.Text))
            {
                script.Mensaje("Vuelva a escribir la nueva contraseña.");
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

        private void HabilitarPanelNuevoPassword()
        {
            //lnkCambioClave.Visible = false;
            this.txtUsuario.Enabled = false;
            this.txtClave.Enabled = false;
            pnlCambioContrasenia.Visible = true;
            txtUsuario.Text = hClaveAnterior.Value;
            btnIngresar.Visible = false;
        }
    }
}