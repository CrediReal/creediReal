using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGA.ENTIDADES;
using SGA.LogicaNegocio;
using SGA.Utilitarios;

namespace SGA.Presentacion
{
    public partial class frmConfiguracion : System.Web.UI.Page
    {
        #region Variables_Globales

        clsWebJScript Script = new clsWebJScript();

        public clsConfiguracion ObjConfiguracion
        {
            get
            {
                clsConfiguracion objConfiguracion = ViewState["objConfiguracion"] as clsConfiguracion;
                return objConfiguracion ?? new clsConfiguracion();
            }
            set
            {
                ViewState["objConfiguracion"] = value;
            }
        }

        public List<clsConfiguracion> LstConfiguraciones
        {
            get
            {
                List<clsConfiguracion> lstConfiguraciones = ViewState["lstConfiguraciones"] as List<clsConfiguracion>;
                return lstConfiguraciones ?? new List<clsConfiguracion>();
            }
            set
            {
                ViewState["lstConfiguraciones"] = value;
            }
        }

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor"); }

                if (IsPostBack) return;

                lblOpcion.Text = Session["cOpcion"].ToString().ToUpper() + ":";
                pnlBotones.Visible = true;
                pnlBusqueda.Visible = true;
                pnlForm.Visible = false;

                //Buscar al usuario por nombre
                LstConfiguraciones = new clsCNConfiguracion().ListarConfiguraciones(0, string.Empty);

                if (!LstConfiguraciones.Any())
                {
                    Script.Mensaje("No se encontraron resultados.");
                }

                grvConfiguraciones.DataSource = LstConfiguraciones;
                grvConfiguraciones.DataBind();
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void BotonEditar_Click(object sender, EventArgs e)
        {
            int idConfiguracion = Convert.ToInt32(((Button)sender).CommandArgument);

            ObjConfiguracion = LstConfiguraciones.FirstOrDefault(x => x.idConfiguracion == idConfiguracion);
            if (ObjConfiguracion == null)
            {
                Script.Mensaje("No se puede editar el registro.");
                return;
            }

            pnlBotones.Visible = false;
            pnlBusqueda.Visible = true;
            pnlForm.Visible = true;

            FillControls(ObjConfiguracion);
            HabilitarControles(true);

        }

        protected void BotonNuevo_Click(object sender, EventArgs e)
        {
            LimpiarControles();

            ObjConfiguracion = new clsConfiguracion();
            FillControls(ObjConfiguracion);
            HabilitarControles(true);

            pnlBotones.Visible = false;
            pnlBusqueda.Visible = true;
            pnlForm.Visible = true;
        }

        protected void BotonGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                //------------------Valida Session Vigente -------------------------------------->
                if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor"); }
                //------------------------------------------------------------------------------>

                var usuarioSession = (clsUsuario)Session["DatosUsuarioSession"];
                if (CamposSonValidos() == false) return;


                ObjConfiguracion.cConfiguracion = txtConfiguracion.Text.Trim().ToUpper();
                ObjConfiguracion.lVigente = chcVigente.Checked;
                ObjConfiguracion.idUsuario = usuarioSession.idUsuario;

                clsDBResp objResp = new clsCNConfiguracion().SaveConfiguracion(ObjConfiguracion);
                if (objResp.nMsje == 0)
                {
                    pnlBotones.Visible = true;
                    pnlBusqueda.Visible = true;
                    pnlForm.Visible = false;

                    LimpiarControles();

                    LstConfiguraciones = new clsCNConfiguracion().ListarConfiguraciones(0, String.Empty);
                    grvConfiguraciones.DataSource = LstConfiguraciones;
                    grvConfiguraciones.DataBind();

                    Script.Mensaje(objResp.cMsje);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void BotonCancelar_Click(object sender, EventArgs e)
        {
            LimpiarControles();

            pnlBotones.Visible = true;
            pnlBusqueda.Visible = true;
            pnlForm.Visible = false;
        }

        #endregion

        #region Metodos

        private void LimpiarControles()
        {
            txtCodigo.Text = string.Empty;
            txtConfiguracion.Text = string.Empty;
            chcVigente.Checked = false;
        }

        private bool CamposSonValidos()
        {
            if (string.IsNullOrEmpty(txtConfiguracion.Text.Trim()))
            {
                Script.Mensaje("Ingrese la descripción de la configuración.");
                return false;
            }
            return true;
        }

        private void FillControls(clsConfiguracion ObjConfiguracion)
        {
            txtCodigo.Text = ObjConfiguracion.idConfiguracion == 0 ? string.Empty : ObjConfiguracion.idConfiguracion.ToString();
            txtConfiguracion.Text = ObjConfiguracion.cConfiguracion;
            chcVigente.Checked = ObjConfiguracion.lVigente;
        }

        private void HabilitarControles(bool lHabil)
        {
            txtCodigo.Enabled = false;
            txtConfiguracion.Enabled = lHabil;
            chcVigente.Enabled = lHabil;
        }

        #endregion
    }
}