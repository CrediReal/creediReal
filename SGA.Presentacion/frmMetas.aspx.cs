using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGA.Controles;
using SGA.ENTIDADES;
using SGA.LogicaNegocio;
using SGA.Utilitarios;

namespace SGA.Presentacion
{
    public partial class frmMetas : PageBase
    {
        #region Variables_Globales

        clsWebJScript Script = new clsWebJScript();

        public clsMeta ObjMeta
        {
            get
            {
                clsMeta objMeta = ViewState["ObjMeta"] as clsMeta;
                return objMeta ?? new clsMeta();
            }
            set
            {
                ViewState["ObjMeta"] = value;
            }
        }

        public List<clsMeta> LstMetas
        {
            get
            {
                List<clsMeta> lstMetas = ViewState["lstMetas"] as List<clsMeta>;
                return lstMetas ?? new List<clsMeta>();
            }
            set
            {
                ViewState["lstMetas"] = value;
            }
        }

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (IsPostBack) return;

                lblOpcion.Text = Session["cOpcion"].ToString().ToUpper() + ":";

                CargarCombos();

                cboAniosBus.SelectedValue = DateTime.Now.Year.ToString();
                cboMesesBus.SelectedValue = DateTime.Now.Month.ToString();
                if (cboOficinaBus.Items.Count > 0)
                    cboOficinaBus.SelectedIndex = 0;

                if (cboTipoMetaBus.Items.Count > 0)
                    cboTipoMetaBus.SelectedIndex = 0;

                BuscarMetas();
                pnlBotones.Visible = true;
                pnlResultados.Visible = true;
                pnlForm.Visible = false;

            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarMetas();
        }

        protected void BotonEditar_Click(object sender, EventArgs e)
        {
            int idMeta = Convert.ToInt32(((Button)sender).CommandArgument);

            ObjMeta = LstMetas.FirstOrDefault(x => x.idMeta == idMeta);
            if (ObjMeta == null)
            {
                Script.Mensaje("No se puede editar el registro.");
                return;
            }

            pnlBotones.Visible = false;
            pnlResultados.Visible = true;
            pnlForm.Visible = true;

            FillControls(ObjMeta);
            HabilitarControles(true);

        }

        protected void BotonNuevo_Click(object sender, EventArgs e)
        {
            LimpiarControles();

            ObjMeta = new clsMeta()
            {
                idMeta = 0,
                nAnio = DateTime.Now.Year,
                nMes = DateTime.Now.Month,
                idOficina = 1,
                idUsuario = 0,
                nValor = 0m
            };
            FillControls(ObjMeta);
            HabilitarControles(true);

            pnlBotones.Visible = false;
            pnlResultados.Visible = true;
            pnlForm.Visible = true;
        }

        protected void BotonGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                var usuarioSession = (clsUsuario)Session["DatosUsuarioSession"];
                if (CamposSonValidos() == false) return;


                ObjMeta.nAnio = Convert.ToInt32(cboAnios.SelectedValue);
                ObjMeta.nMes = Convert.ToInt32(cboMeses.SelectedValue);
                ObjMeta.idOficina = Convert.ToInt32(cboOficina.SelectedValue);
                ObjMeta.idUsuario = Convert.ToInt32(cboUsuario.SelectedValue);
                ObjMeta.idTipoMeta = Convert.ToInt32(cboTipoMeta.SelectedValue);
                ObjMeta.nValor = Convert.ToDecimal(txtValor.Text.Trim());
                int idUsuReg = usuarioSession.idUsuario;

                clsDBResp objResp = new clsCNMeta().SaveMetas(ObjMeta,idUsuReg);
                if (objResp.nMsje == 0)
                {
                    pnlBotones.Visible = true;
                    pnlResultados.Visible = true;
                    pnlForm.Visible = false;

                    LimpiarControles();

                    BuscarMetas();

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
            pnlResultados.Visible = true;
            pnlForm.Visible = false;
        }

        protected void cboOficina_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cboOficina.SelectedValue))
                return;
            int idOficina = Convert.ToInt32(cboOficina.SelectedValue);
            cboUsuario.ListarUsuarios(idOficina);
        }

        protected void grvMetas_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    clsMeta objMeta = e.Row.DataItem as clsMeta;
            //    NumberBox txtValor = e.Row.FindControl("txtValor") as NumberBox;
            //    if (txtValor != null && objMeta != null)
            //        txtValor.Text = objMeta.nValor.ToString("#,0.00");
            //}
        }

        #endregion

        #region Metodos

        private void LimpiarControles()
        {
            txtCodigo.Text = string.Empty;
            cboAnios.SelectedIndex = -1;
            cboMeses.SelectedIndex = -1;
            cboOficina.SelectedIndex = -1;
            cboUsuario.SelectedIndex = -1;
            cboTipoMeta.SelectedIndex = -1;
            txtValor.Text = string.Empty;
        }

        private bool CamposSonValidos()
        {
            if (string.IsNullOrEmpty(cboAnios.SelectedValue))
            {
                Script.Mensaje("Seleccione el año.");
                return false;
            }
            if (string.IsNullOrEmpty(cboMeses.SelectedValue))
            {
                Script.Mensaje("Seleccione el mes.");
                return false;
            }
            if (string.IsNullOrEmpty(cboOficina.SelectedValue))
            {
                Script.Mensaje("Seleccione la oficina.");
                return false;
            }
            if (string.IsNullOrEmpty(cboUsuario.SelectedValue))
            {
                Script.Mensaje("Seleccione el usuario.");
                return false;
            }
            if (string.IsNullOrEmpty(cboTipoMeta.SelectedValue))
            {
                Script.Mensaje("Seleccione el tipo de meta.");
                return false;
            }
            if (string.IsNullOrEmpty(txtValor.Text.Trim()))
            {
                Script.Mensaje("Ingrese el valor de la meta.");
                return false;
            }

            return true;
        }

        private void FillControls(clsMeta objMeta)
        {
            txtCodigo.Text = objMeta.idMeta == 0 ? string.Empty : objMeta.idMeta.ToString();
            if (cboAnios.Items.FindByValue(objMeta.nAnio.ToString()) != null)
                cboAnios.SelectedValue = objMeta.nAnio.ToString();

            if (cboMeses.Items.FindByValue(objMeta.nMes.ToString()) != null)
                cboMeses.SelectedValue = objMeta.nMes.ToString();

            if (cboOficina.Items.FindByValue(objMeta.idOficina.ToString()) != null)
                cboOficina.SelectedValue = objMeta.idOficina.ToString();

            cboOficina_OnSelectedIndexChanged(this, null);

            if (cboUsuario.Items.FindByValue(objMeta.idUsuario.ToString()) != null)
                cboUsuario.SelectedValue = objMeta.idUsuario.ToString();

            if (cboTipoMeta.Items.FindByValue(objMeta.idTipoMeta.ToString()) != null)
                cboTipoMeta.SelectedValue = objMeta.idTipoMeta.ToString();

            txtValor.Text = objMeta.nValor.ToString("#,0.00");
        }

        private void HabilitarControles(bool lHabil)
        {
            txtCodigo.Enabled = false;
            cboAnios.Enabled = lHabil;
            cboMeses.Enabled = lHabil;
            cboOficina.Enabled = lHabil;
            cboUsuario.Enabled = lHabil;
            cboTipoMeta.Enabled = lHabil;
            txtValor.Enabled = lHabil;
        }

        private void BuscarMetas()
        {
            if (!ValidarBusqueda())
                return;

            clsMeta objMeta = new clsMeta()
            {
                nAnio = Convert.ToInt32(cboAniosBus.SelectedValue),
                nMes = Convert.ToInt32(cboMesesBus.SelectedValue),
                idOficina = Convert.ToInt32(cboOficinaBus.SelectedValue),
                idTipoMeta = Convert.ToInt32(cboTipoMetaBus.SelectedValue)
            };
            List<clsMeta> lstMetas = new clsCNMeta().GetMetas(objMeta);
            grvMetas.DataSource = lstMetas;
            grvMetas.DataBind();

            pnlResultados.Visible = true;
        }

        private bool ValidarBusqueda()
        {
            if (string.IsNullOrEmpty(cboMesesBus.SelectedValue))
            {
                Script.Mensaje("Seleccione el año para la busqueda.");
                return false;
            }
            if (string.IsNullOrEmpty(cboMesesBus.SelectedValue))
            {
                Script.Mensaje("Seleccione el mes para la busqueda.");
                return false;
            }
            if (string.IsNullOrEmpty(cboOficinaBus.SelectedValue))
            {
                Script.Mensaje("Seleccione la oficina para la busqueda.");
                return false;
            }
            if (string.IsNullOrEmpty(cboTipoMetaBus.SelectedValue))
            {
                Script.Mensaje("Seleccione el tipo de meta para la busqueda.");
                return false;
            }
            return true;
        }

        private void CargarCombos()
        {
            cboAniosBus.ListarAnios();
            cboMesesBus.CargarMeses();
            cboOficinaBus.ListarSoloVigentes();
            cboTipoMetaBus.ListarTipoMetas();

            cboAnios.ListarAnios();
            cboMeses.CargarMeses();
            cboOficina.ListarSoloVigentes();
            cboTipoMeta.ListarTipoMetas();
        }

        #endregion
   
    }
}