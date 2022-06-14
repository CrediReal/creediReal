using SGA.ENTIDADES;
using SGA.Utilitarios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGA.Presentacion.ADMINISTRACION
{
    public partial class frmSolExtorno : System.Web.UI.Page
    {
        GEN.CapaNegocio.clsCNGenAdmOpe ListaOpe = new GEN.CapaNegocio.clsCNGenAdmOpe();
        clsWebJScript script = new clsWebJScript();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["usuario"] != null)
                {
                    Session["DatosUsuarioSession"] = new SGA.LogicaNegocio.clsCNUsuario().GetDatosUsuario(Request.QueryString["usuario"].ToString());
                }
                if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor"); }

                if (IsPostBack) return;

                lblOpcion.Text = Session["cOpcion"].ToString().ToUpper() + ":";
                cargarControles();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void cargarControles()
        {
            var dtOperacion = ListaOpe.ListadoTipoOpe();
            this.cboTipoOperacion.DataSource = dtOperacion;
            this.cboTipoOperacion.DataValueField = dtOperacion.Columns["idTipoOperacion"].ToString();
            this.cboTipoOperacion.DataTextField = dtOperacion.Columns["cTipoOperacion"].ToString();
            cboTipoOperacion.DataBind();

            GEN.CapaNegocio.clsCNMoneda moneda = new GEN.CapaNegocio.clsCNMoneda();
            var dt = moneda.listarMoneda();
            this.cboMoneda.DataSource = dt;
            this.cboMoneda.DataValueField = dt.Columns[0].ToString();
            this.cboMoneda.DataTextField = dt.Columns[1].ToString();
            cboMoneda.DataBind();

            CRE.CapaNegocio.clsCNMotivoExtorno ListarMotivoExtorno = new CRE.CapaNegocio.clsCNMotivoExtorno();
            DataTable dtMotivo = ListarMotivoExtorno.ListaMotivioExtrono();
            this.cboMotivoExtorno.DataSource = dtMotivo;
            this.cboMotivoExtorno.DataValueField = dtMotivo.Columns[0].ToString();
            this.cboMotivoExtorno.DataTextField = dtMotivo.Columns[1].ToString();
            cboMotivoExtorno.DataBind();
        }

        protected void BotonCancelar1_Click(object sender, EventArgs e)
        {
            ViewState["tbTrx"] = null;
            LimpiarControles();
            this.nudNroKardex.Enabled = false;
            this.BotonConsultar1.Enabled = false;
            this.BotonGrabar1.Enabled = false;
            this.BotonCancelar1.Enabled = false;
            this.cboMotivoExtorno.Enabled = false;
            this.txtSustento.Enabled = false;
            this.nudNroKardex.Value = 0;
        }

        protected void BotonGrabar1_Click(object sender, EventArgs e)
        {
            clsUsuario objUsuario;
            if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor"); }
            else
            {
                objUsuario = (clsUsuario)Session["DatosUsuarioSession"];
            }
            if (string.IsNullOrEmpty(this.nudNroKardex.Value.ToString()))
            {
                script.Mensaje("El Numero de Kardex, esta Vacio, por Favor Registrar ");
                this.nudNroKardex.Focus();
                return;
            }
            if (string.IsNullOrEmpty(this.txtModulo.Text.Trim()))
            {
                script.Mensaje("La Operación no Pertenece a Ningun Módulo, No Puede Registrar la Solicitud");
                this.nudNroKardex.Focus();
                return;
            }

            if (this.cboMotivoExtorno.SelectedIndex == -1)
            {
                script.Mensaje("Debe Seleccionar el Motivo del Extorno");
                this.cboMotivoExtorno.Focus();
                return;
            }

            if (string.IsNullOrEmpty(this.cboMotivoExtorno.Text))
            {
                script.Mensaje("Debe Seleccionar el Motivo del Extorno");
                this.cboMotivoExtorno.Focus();
                return;
            }

            if (string.IsNullOrEmpty(this.txtSustento.Text.Trim()))
            {
                script.Mensaje("Debe Ingresar el Sustento del Extorno");
                this.nudNroKardex.Focus();
                return;
            }

            //===================================================================
            //--Guardar Pago del Giro
            //===================================================================
            int idKar = Convert.ToInt32(this.nudNroKardex.Text.ToString());
            int idTipOpe = Convert.ToInt32(this.cboTipoOperacion.SelectedValue.ToString());
            int idMon = Convert.ToInt32(this.cboMoneda.SelectedValue.ToString());
            double nMontoOpe = Convert.ToDouble(this.txtMonOpe.Text);
            int idMotExt = Convert.ToInt32(this.cboMotivoExtorno.SelectedValue.ToString());
            string cSust = this.txtSustento.Text.Trim();
            var tbTrx = (DataTable)ViewState["tbTrx"];
            var idProd = Convert.ToInt32(tbTrx.Rows[0]["idProducto"].ToString());

            GEN.CapaNegocio.clsCNAprobacion dtSolApr = new GEN.CapaNegocio.clsCNAprobacion();
            DataTable tbSolApr = dtSolApr.GuardarSolicitudAprobac(objUsuario.nIdAgencia, 0, Convert.ToInt32(cboTipoOperacion.SelectedValue), 2,
                                                                Convert.ToInt16(cboMoneda.SelectedValue), idProd, Convert.ToDouble(txtMonOpe.Text),
                                                               Convert.ToInt32(nudNroKardex.Value), objUsuario.dFecSystem, Convert.ToInt16(cboMotivoExtorno.SelectedValue),
                                                                txtSustento.Text, objUsuario.idUsuario);

            if (Convert.ToInt32(tbSolApr.Rows[0]["idSolAproba"].ToString()) != 0)
            {
                script.Mensaje(tbSolApr.Rows[0]["cMensaje"].ToString() + ". Nro Solicitud: " + tbSolApr.Rows[0]["idSolAproba"].ToString());
                this.nudNroKardex.Enabled = false;
                this.BotonConsultar1.Enabled = true;
                this.BotonGrabar1.Enabled = false;
                this.BotonCancelar1.Enabled = false;
                this.cboMotivoExtorno.Enabled = false;
                this.txtSustento.Enabled = false;
            }
            else
            {
                script.Mensaje(tbSolApr.Rows[0]["cMensaje"].ToString());
            }
        }

        protected void BotonConsultar1_Click(object sender, EventArgs e)
        {
            clsUsuario objUsuario;
            if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor"); }
            else
            {
                objUsuario = (clsUsuario)Session["DatosUsuarioSession"];
            }
            LimpiarControles();
            BotonGrabar1.Enabled = false;
            var idMod = 0;
            int idKar = Convert.ToInt32(this.nudNroKardex.Value);

            if (string.IsNullOrEmpty(idKar.ToString()))
            {
                script.Mensaje("Debe Ingresar el Número de Operación");
                this.nudNroKardex.Focus();
                this.nudNroKardex.Focus();
                return;
            }

            if (idKar <= 0)
            {
                script.Mensaje("El Kardex Ingresado No Es Válido");
                this.nudNroKardex.Focus();
                this.nudNroKardex.Focus();
                return;
            }

            GEN.CapaNegocio.clsCNGenAdmOpe dtDatTrx = new GEN.CapaNegocio.clsCNGenAdmOpe();
            DataTable tbTrx = dtDatTrx.RetDatosOperacion(idKar, objUsuario.dFecSystem, objUsuario.idUsuario, objUsuario.nIdAgencia);
            ViewState["tbTrx"]= tbTrx;
            if (Convert.ToInt32(tbTrx.Rows[0]["idRpta"].ToString()) == 0)
            {
                this.txtModulo.Text = tbTrx.Rows[0]["cModulo"].ToString();
                this.cboTipoOperacion.SelectedValue = tbTrx.Rows[0]["idTipoOperacion"].ToString();
                this.cboMoneda.SelectedValue = tbTrx.Rows[0]["idMoneda"].ToString();
                this.txtMonOpe.Text = tbTrx.Rows[0]["nMontoOperacion"].ToString();
                idMod = Convert.ToInt32(tbTrx.Rows[0]["idModulo"].ToString());
                var idProd = Convert.ToInt32(tbTrx.Rows[0]["idProducto"].ToString());
                var idEstKar = Convert.ToInt32(tbTrx.Rows[0]["idEstadoKardex"].ToString());
                this.nudNroKardex.Enabled = false;
                this.BotonConsultar1.Enabled = false;
                this.BotonGrabar1.Enabled = true;
                this.BotonCancelar1.Enabled = true;
                this.cboMotivoExtorno.Enabled = true;
                this.txtSustento.Enabled = true;
                this.cboMotivoExtorno.Focus();
            }
            else
            {
                script.Mensaje(tbTrx.Rows[0]["cMensaje"].ToString());
                this.nudNroKardex.Focus();
                this.nudNroKardex.Focus();
            }
        }
        private void LimpiarControles()
        {
            this.txtModulo.Text="";
            this.cboMoneda.SelectedIndex = -1;
            this.cboTipoOperacion.SelectedIndex = -1;
            this.txtMonOpe.Text = "0.00";
            this.cboMotivoExtorno.SelectedIndex = -1;
            this.txtSustento.Text = "";
        }
    }
}