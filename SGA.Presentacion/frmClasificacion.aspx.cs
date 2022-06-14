using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGA.ENTIDADES;
using SGA.LogicaNegocio;
using SGA.Utilitarios;

namespace SGA.Presentacion
{
    public partial class frmClasificacion : System.Web.UI.Page
    {
        #region Variables_Globales

        clsWebJScript Script = new clsWebJScript();

        public clsClasificacion ObjClasificacion
        {
            get
            {
                clsClasificacion objClasificacion = ViewState["ObjClasificacion"] as clsClasificacion;
                return objClasificacion ?? new clsClasificacion();
            }
            set
            {
                ViewState["ObjClasificacion"] = value;
            }
        }

        public List<clsClasificacion> LstClasificacions
        {
            get
            {
                List<clsClasificacion> lstClasificacions = ViewState["lstClasificacions"] as List<clsClasificacion>;
                return lstClasificacions ?? new List<clsClasificacion>();
            }
            set
            {
                ViewState["lstClasificacions"] = value;
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
                LstClasificacions = new clsCNClasificacion().ListarClasificaciones(0);

                if (!LstClasificacions.Any())
                {
                    Script.Mensaje("No se encontraron resultados.");
                }

                grvClasificacions.DataSource = LstClasificacions;
                grvClasificacions.DataBind();
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void BotonEditar_Click(object sender, EventArgs e)
        {
            int idClasificacion = Convert.ToInt32(((Button)sender).CommandArgument);

            ObjClasificacion = LstClasificacions.FirstOrDefault(x => x.idClasificacion == idClasificacion);
            if (ObjClasificacion == null)
            {
                Script.Mensaje("No se puede editar el registro.");
                return;
            }

            pnlBotones.Visible = false;
            pnlBusqueda.Visible = true;
            pnlForm.Visible = true;

            FillControls(ObjClasificacion);
            HabilitarControles(true);

        }

        protected void BotonNuevo_Click(object sender, EventArgs e)
        {
            LimpiarControles();

            ObjClasificacion = new clsClasificacion();
            FillControls(ObjClasificacion);
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


                ObjClasificacion.cClasificacion = txtClasificacion.Text.Trim().ToUpper();
                ObjClasificacion.lVigente = chcVigente.Checked;
                ObjClasificacion.idUsuario = usuarioSession.idUsuario;

                clsDBResp objResp = new clsCNClasificacion().SaveClasificacion(ObjClasificacion);
                if (objResp.nMsje == 0)
                {
                    pnlBotones.Visible = true;
                    pnlBusqueda.Visible = true;
                    pnlForm.Visible = false;

                    LimpiarControles();

                    LstClasificacions = new clsCNClasificacion().ListarClasificaciones(0);
                    grvClasificacions.DataSource = LstClasificacions;
                    grvClasificacions.DataBind();

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
            txtClasificacion.Text = string.Empty;
            chcVigente.Checked = false;
        }

        private bool CamposSonValidos()
        {
            if (string.IsNullOrEmpty(txtClasificacion.Text.Trim()))
            {
                Script.Mensaje("Ingrese la descripción de la Clasificacion.");
                return false;
            }
            return true;
        }

        private void FillControls(clsClasificacion objClasificacion)
        {
            txtCodigo.Text = objClasificacion.idClasificacion == 0 ? string.Empty : objClasificacion.idClasificacion.ToString();
            txtClasificacion.Text = objClasificacion.cClasificacion;
            chcVigente.Checked = objClasificacion.lVigente;
        }

        private void HabilitarControles(bool lHabil)
        {
            txtCodigo.Enabled = false;
            txtClasificacion.Enabled = lHabil;
            chcVigente.Enabled = lHabil;
        }

        #endregion
    }
}