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
    public partial class frmDescuentos : PageBase
    {
        #region Variables_Globales

        clsWebJScript Script = new clsWebJScript();

        public clsDescuento ObjDescuento
        {
            get
            {
                clsDescuento objDescuento = ViewState["ObjDescuento"] as clsDescuento;
                return objDescuento ?? new clsDescuento();
            }
            set
            {
                ViewState["ObjDescuento"] = value;
            }
        }

        public List<clsDescuento> LstDescuentos
        {
            get
            {
                List<clsDescuento> lstDescuentos = ViewState["lstDescuentos"] as List<clsDescuento>;
                return lstDescuentos ?? new List<clsDescuento>();
            }
            set
            {
                ViewState["lstDescuentos"] = value;
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

                cboClasificacion.ListarSoloVigentes();
                pnlBusCli.Visible = true;
                pnlBotones.Visible = true;
                pnlBusqueda.Visible = true;
                pnlForm.Visible = false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void BotonEditar_Click(object sender, EventArgs e)
        {
            int idDescuento = Convert.ToInt32(((Button)sender).CommandArgument);

            ObjDescuento = LstDescuentos.FirstOrDefault(x => x.idDescuentoCli == idDescuento);
            if (ObjDescuento == null)
            {
                Script.Mensaje("No se puede editar el registro.");
                return;
            }

            pnlBusCli.Visible = true;
            pnlBotones.Visible = false;
            pnlBusqueda.Visible = true;
            pnlForm.Visible = true;

            FillControls(ObjDescuento);
            HabilitarControles(true);

        }

        protected void BotonNuevo_Click(object sender, EventArgs e)
        {
            LimpiarControles();

            ObjDescuento = new clsDescuento();
            FillControls(ObjDescuento);
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

                ObjDescuento.idCli = ConBusCli.IdCli;
                ObjDescuento.idClasificacion = Convert.ToInt32(cboClasificacion.SelectedValue);
                ObjDescuento.nDescuento =  Convert.ToDecimal(txtDescuento.Text.Trim());
                ObjDescuento.nMaxDescuento = Convert.ToDecimal(txtMaxDescuento.Text.Trim());
                int idUsuReg = usuarioSession.idUsuario;

                clsDBResp objResp = new clsCNDescuento().SaveDescuento(ObjDescuento, idUsuReg);
                if (objResp.nMsje == 0)
                {
                    pnlBotones.Visible = true;
                    pnlBusqueda.Visible = true;
                    pnlForm.Visible = false;

                    LimpiarControles();

                    LstDescuentos = new clsCNDescuento().ListarDescuentosCli(ConBusCli.IdCli);
                    grvDescuentos.DataSource = LstDescuentos;
                    grvDescuentos.DataBind();

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
            ConBusCli.ActivarBusqueda(true);

            pnlBusCli.Visible = true;
            pnlBotones.Visible = true;
            pnlBusqueda.Visible = true;
            pnlForm.Visible = false;
        }

        #endregion

        #region Metodos

        private void LimpiarControles()
        {
            txtCodigo.Text = string.Empty;
            cboClasificacion.SelectedIndex = -1;
            txtDescuento.Text = string.Empty;
            txtMaxDescuento.Text = string.Empty;
        }

        private bool CamposSonValidos()
        {
            if (ConBusCli.IdCli == 0)
            {
                Script.Mensaje("Cliente no válido.");
                return false;
            }
            if (string.IsNullOrEmpty(cboClasificacion.SelectedValue.Trim()))
            {
                Script.Mensaje("Seleccione la clasificación.");
                return false;
            }

            if (string.IsNullOrEmpty(txtDescuento.Text.Trim()))
            {
                Script.Mensaje("Ingrese el descuento.");
                return false;
            }
            if (string.IsNullOrEmpty(txtMaxDescuento.Text.Trim()))
            {
                Script.Mensaje("Ingrese el valor del máximo descuento.");
                return false;
            }
            return true;
        }

        private void FillControls(clsDescuento objDescuento)
        {
            txtCodigo.Text = objDescuento.idDescuentoCli == 0 ? string.Empty : objDescuento.idDescuentoCli.ToString();
            if (cboClasificacion.Items.FindByValue(objDescuento.idClasificacion.ToString()) != null)
                cboClasificacion.SelectedValue = objDescuento.idClasificacion.ToString();
            txtDescuento.Text = objDescuento.nDescuento.ToString("#,0.00");
            txtMaxDescuento.Text = objDescuento.nMaxDescuento.ToString("#,0.00");
        }

        private void HabilitarControles(bool lHabil)
        {
            txtCodigo.Enabled = false;
            txtDescuento.Enabled = lHabil;
            txtMaxDescuento.Enabled = lHabil;
        }

        #endregion

        protected void ConBusCli_ClienteChanged(object sender, EventArgs e)
        {
            if (ConBusCli.IdCli == 0)
                return;

            LstDescuentos = new clsCNDescuento().ListarDescuentosCli(ConBusCli.IdCli);
            grvDescuentos.DataSource = LstDescuentos;
            grvDescuentos.DataBind();
        }
    }
}