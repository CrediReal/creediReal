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
    public partial class frmTipo : System.Web.UI.Page
    {
        #region Variables_Globales

        clsWebJScript Script = new clsWebJScript();

        public clsTipo ObjTipo
        {
            get
            {
                clsTipo objTipo = ViewState["obTipo"] as clsTipo;
                return objTipo ?? new clsTipo();
            }
            set
            {
                ViewState["obTipo"] = value;
            }
        }

        public List<clsTipo> LstTipos
        {
            get
            {
                List<clsTipo> lstTipos = ViewState["lstTipos"] as List<clsTipo>;
                return lstTipos ?? new List<clsTipo>();
            }
            set
            {
                ViewState["lstTipos"] = value;
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
                LstTipos = new clsCNTipo().ListarTipos(0,string.Empty);

                if (!LstTipos.Any())
                {
                    Script.Mensaje("No se encontraron resultados.");
                }

                grvTipos.DataSource = LstTipos;
                grvTipos.DataBind();
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void BotonEditar_Click(object sender, EventArgs e)
        {
            int idTipo = Convert.ToInt32(((Button)sender).CommandArgument);

            ObjTipo = LstTipos.FirstOrDefault(x => x.idTipo == idTipo);
            if (ObjTipo == null)
            {
                Script.Mensaje("No se puede editar el registro.");
                return;
            }

            pnlBotones.Visible = false;
            pnlBusqueda.Visible = true;
            pnlForm.Visible = true;

            FillControls(ObjTipo);
            HabilitarControles(true);

        }

        protected void BotonNuevo_Click(object sender, EventArgs e)
        {
            LimpiarControles();

            ObjTipo = new clsTipo();
            FillControls(ObjTipo);
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


                ObjTipo.cTipo = txtTipo.Text.Trim().ToUpper();
                ObjTipo.lVigente = chcVigente.Checked;
                ObjTipo.idUsuario = usuarioSession.idUsuario;

                clsDBResp objResp = new clsCNTipo().SaveTipo(ObjTipo);
                if (objResp.nMsje == 0)
                {
                    pnlBotones.Visible = true;
                    pnlBusqueda.Visible = true;
                    pnlForm.Visible = false;

                    LimpiarControles();

                    LstTipos = new clsCNTipo().ListarTipos(0, String.Empty);
                    grvTipos.DataSource = LstTipos;
                    grvTipos.DataBind();

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
            txtTipo.Text = string.Empty;
            chcVigente.Checked = false;
        }

        private bool CamposSonValidos()
        {
            if (string.IsNullOrEmpty(txtTipo.Text.Trim()))
            {
                Script.Mensaje("Ingrese la descripción del tipo.");
                return false;
            }
            return true;
        }

        private void FillControls(clsTipo objTipo)
        {
            txtCodigo.Text = objTipo.idTipo == 0 ? string.Empty : objTipo.idTipo.ToString();
            txtTipo.Text = objTipo.cTipo;
            chcVigente.Checked = objTipo.lVigente;
        }

        private void HabilitarControles(bool lHabil)
        {
            txtCodigo.Enabled = false;
            txtTipo.Enabled = lHabil;
            chcVigente.Enabled = lHabil;
        }

        #endregion
    }
}