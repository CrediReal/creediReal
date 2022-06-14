using SGA.ENTIDADES;
using SGA.LogicaNegocio;
using SGA.Utilitarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGA.Presentacion
{
    public partial class frmTipoMeta : System.Web.UI.Page
    {
        #region Variables_Globales

        clsWebJScript Script = new clsWebJScript();

        public clsTipoMeta ObjTipoMeta
        {
            get
            {
                clsTipoMeta objTipoMeta = ViewState["ObjTipoMeta"] as clsTipoMeta;
                return objTipoMeta ?? new clsTipoMeta();
            }
            set
            {
                ViewState["ObjTipoMeta"] = value;
            }
        }

        public List<clsTipoMeta> LstTipoMetas
        {
            get
            {
                List<clsTipoMeta> lstTipoMetas = ViewState["lstTipoMetas"] as List<clsTipoMeta>;
                return lstTipoMetas ?? new List<clsTipoMeta>();
            }
            set
            {
                ViewState["lstTipoMetas"] = value;
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
                LstTipoMetas = new clsCNTipoMeta().ListarTipoMetas(0);

                if (!LstTipoMetas.Any())
                {
                    Script.Mensaje("No se encontraron resultados.");
                }

                grvTipoMetas.DataSource = LstTipoMetas;
                grvTipoMetas.DataBind();
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void BotonEditar_Click(object sender, EventArgs e)
        {
            int idTipoMeta = Convert.ToInt32(((Button)sender).CommandArgument);

            ObjTipoMeta = LstTipoMetas.FirstOrDefault(x => x.idTipoMeta == idTipoMeta);
            if (ObjTipoMeta == null)
            {
                Script.Mensaje("No se puede editar el registro.");
                return;
            }

            pnlBotones.Visible = false;
            pnlBusqueda.Visible = true;
            pnlForm.Visible = true;

            FillControls(ObjTipoMeta);
            HabilitarControles(true);

        }

        protected void BotonNuevo_Click(object sender, EventArgs e)
        {
            LimpiarControles();

            ObjTipoMeta = new clsTipoMeta();
            FillControls(ObjTipoMeta);
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


                ObjTipoMeta.cTipoMeta = txtTipoMeta.Text.Trim().ToUpper();
                ObjTipoMeta.lVigente = chcVigente.Checked;
                ObjTipoMeta.idUsuario = usuarioSession.idUsuario;

                clsDBResp objResp = new clsCNTipoMeta().SaveTipoMeta(ObjTipoMeta);
                if (objResp.nMsje == 0)
                {
                    pnlBotones.Visible = true;
                    pnlBusqueda.Visible = true;
                    pnlForm.Visible = false;

                    LimpiarControles();

                    LstTipoMetas = new clsCNTipoMeta().ListarTipoMetas(0);
                    grvTipoMetas.DataSource = LstTipoMetas;
                    grvTipoMetas.DataBind();

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
            txtTipoMeta.Text = string.Empty;
            chcVigente.Checked = false;
        }

        private bool CamposSonValidos()
        {
            if (string.IsNullOrEmpty(txtTipoMeta.Text.Trim()))
            {
                Script.Mensaje("Ingrese la descripción de la TipoMeta.");
                return false;
            }
            return true;
        }

        private void FillControls(clsTipoMeta objTipoMeta)
        {
            txtCodigo.Text = objTipoMeta.idTipoMeta == 0 ? string.Empty : objTipoMeta.idTipoMeta.ToString();
            txtTipoMeta.Text = objTipoMeta.cTipoMeta;
            chcVigente.Checked = objTipoMeta.lVigente;
        }

        private void HabilitarControles(bool lHabil)
        {
            txtCodigo.Enabled = false;
            txtTipoMeta.Enabled = lHabil;
            chcVigente.Enabled = lHabil;
        }

        #endregion
    }
}