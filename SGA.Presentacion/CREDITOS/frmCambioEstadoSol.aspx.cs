using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGA.Utilitarios;
using SGA.ENTIDADES;
using SGA.LogicaNegocio;

namespace SGA.Presentacion.CREDITOS
{
    public partial class frmCambioEstadoSol : System.Web.UI.Page
    {
        clsWebJScript script = new clsWebJScript();
        GEN.CapaNegocio.clsCNSolicitud Solicitud = new GEN.CapaNegocio.clsCNSolicitud();
        
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
                    hPerfil.Value = Request.QueryString["perfil"].ToString();
                }
                if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor " + this.ToString()); }

                if (IsPostBack) return;
                hUsuario.Value = ((clsUsuario)Session["DatosUsuarioSession"]).cUsuario;
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
            clsUsuario objUsuario;
            if (Session["DatosUsuarioSession"] == null)
            {
                Session["DatosUsuarioSession"] = new clsCNUsuario().GetDatosUsuario(hUsuario.Value);
                objUsuario = (clsUsuario)Session["DatosUsuarioSession"];
            }
            else
            {
                objUsuario = (clsUsuario)Session["DatosUsuarioSession"];
            }

            cargarTipoCre();
            cargarMoneda();
            cargarPeriodo();
            cargarEstado();
            cboEstado.SelectedValue = "1";
            cargarAsesor();
            cargarTipoCalculo();
            cargarFormaDesembolso();
            cboModDesemb1.SelectedValue="1";
            this.cboTipoCalculo.SelectedValue = "1";
            this.dtpFechaDesembolso.SeleccionarFecha = objUsuario.dFecSystem;
            this.dtpFechaSol.SeleccionarFecha = objUsuario.dFecSystem;
            txtDiasGracia.Text = "0";
            txtMonto.Text = "0.00";
            txtTasaInteres.Text = "0.00";
            txtTasaMora.Text = "0.00";
            this.conBuscarCliente1.Habilitar(true);
        }

        private void cargarTipoCre()
        {
            GEN.CapaNegocio.clsCNProducto ListaProducto = new GEN.CapaNegocio.clsCNProducto();
            var dt = ListaProducto.listarProducto(0);
            this.cboTipoCre.DataSource = dt;
            this.cboTipoCre.DataValueField = dt.Columns[0].ToString();
            this.cboTipoCre.DataTextField = dt.Columns[1].ToString();
            cboTipoCre.DataBind();
        }

        private void cargarSubTipoCre()
        {
            GEN.CapaNegocio.clsCNProducto ListaProducto = new GEN.CapaNegocio.clsCNProducto();
            var idProducto = Convert.ToInt32(this.cboTipoCre.SelectedValue);
            var dt = ListaProducto.listarProducto(idProducto);
            this.cboSubTipoCre.DataSource = dt;
            this.cboSubTipoCre.DataValueField = dt.Columns[0].ToString();
            this.cboSubTipoCre.DataTextField = dt.Columns[1].ToString();
            cboSubTipoCre.DataBind();
        }

        private void cargarProducto()
        {
            GEN.CapaNegocio.clsCNProducto ListaProducto = new GEN.CapaNegocio.clsCNProducto();
            var idProducto = Convert.ToInt32(this.cboSubTipoCre.SelectedValue);
            var dt = ListaProducto.listarProducto(idProducto);
            this.cboProducto.DataSource = dt;
            this.cboProducto.DataValueField = dt.Columns[0].ToString();
            this.cboProducto.DataTextField = dt.Columns[1].ToString();
            cboProducto.DataBind();
        }

        private void cargarSubProducto()
        {
            GEN.CapaNegocio.clsCNProducto ListaProducto = new GEN.CapaNegocio.clsCNProducto();
            var idProducto = Convert.ToInt32(this.cboProducto.SelectedValue);
            var dt = ListaProducto.listarProducto(idProducto);
            this.cboSubProducto.DataSource = dt;
            this.cboSubProducto.DataValueField = dt.Columns[0].ToString();
            this.cboSubProducto.DataTextField = dt.Columns[1].ToString();
            cboSubProducto.DataBind();
        }

        private void cargarMoneda()
        {
            GEN.CapaNegocio.clsCNMoneda moneda = new GEN.CapaNegocio.clsCNMoneda();
            var dt = moneda.listarMoneda();
            this.cboMoneda.DataSource = dt;
            this.cboMoneda.DataValueField = dt.Columns[0].ToString();
            this.cboMoneda.DataTextField = dt.Columns[1].ToString();
            cboMoneda.DataBind();
        }

        private void cargarPeriodo()
        {
            CRE.CapaNegocio.clsCNTipoPeriodo TipoPerido = new CRE.CapaNegocio.clsCNTipoPeriodo();

            DataTable dt = TipoPerido.dtListaTipoPeriodo();
            this.cboPeriodo.DataSource = dt;
            cboPeriodo.DataValueField = dt.Columns[0].ToString();
            cboPeriodo.DataTextField = dt.Columns[1].ToString();
            cboPeriodo.DataBind();
        }

        private void cargarEstado()
        {
            GEN.CapaNegocio.clsCNEstadoCredito ListarEstado = new GEN.CapaNegocio.clsCNEstadoCredito();
            DataTable dt = ListarEstado.ListarEstado(12);

            this.cboEstado.DataSource = dt;
            cboEstado.DataValueField = dt.Columns[0].ToString();
            cboEstado.DataTextField = dt.Columns[1].ToString();
            cboEstado.DataBind();
        }
        
        private void cargarTipoCalculo()
        {
            DataTable dtTipoCalculo = new GEN.CapaNegocio.clsCNTipoCalculo().CNTipoCalculoPPG();
            this.cboTipoCalculo.DataSource = dtTipoCalculo;
            cboTipoCalculo.DataValueField = dtTipoCalculo.Columns["idTipoCalculo"].ToString();
            cboTipoCalculo.DataTextField = dtTipoCalculo.Columns["cTipoCalculo"].ToString();
            cboTipoCalculo.DataBind();
        }

        private void cargarAsesor()
        {
            GEN.CapaNegocio.clsCNPersonalCreditos ListaPersonalCre = new GEN.CapaNegocio.clsCNPersonalCreditos();
            DataTable dt = ListaPersonalCre.ListarPersonalCre(0, 0, 0);
            this.cboAsesor.DataSource = dt;
            cboAsesor.DataValueField = dt.Columns[0].ToString();
            cboAsesor.DataTextField = dt.Columns[1].ToString();
            cboAsesor.DataBind();
        }
        
        private void cargarFormaDesembolso()
        {
            CRE.CapaNegocio.clsCNModDesembolso ListaModaDese = new CRE.CapaNegocio.clsCNModDesembolso();

            DataTable tbModDes = ListaModaDese.ListaModDesem();
            this.cboModDesemb1.DataSource = tbModDes;
            this.cboModDesemb1.DataValueField = tbModDes.Columns[0].ToString();
            this.cboModDesemb1.DataTextField= tbModDes.Columns[1].ToString();
            cboModDesemb1.DataBind();
        }

        protected void cboTipoCre_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarSubTipoCre();
        }

        protected void cboSubTipoCre_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarProducto();
        }

        protected void cboProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarSubProducto();
        }

        protected void cboSubProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void BotonGrabar1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validar())
                {
                    ActualizarEstado();

                    BuscarSolicitud(0);
                }
            }
            catch (Exception ex)
            {
                
                throw;
            }
            
        }

        private void ActualizarEstado()
        {
            clsUsuario objUsuario;
            if (Session["DatosUsuarioSession"] == null)
            {
                Session["DatosUsuarioSession"] = new clsCNUsuario().GetDatosUsuario(hUsuario.Value);
                objUsuario = (clsUsuario)Session["DatosUsuarioSession"];
            }
            else
            {
                objUsuario = (clsUsuario)Session["DatosUsuarioSession"];
            }

            var Sol = (DataTable)ViewState["dtSolicitud"];

            Sol.Columns["nCuotas"].ReadOnly = false;
            Sol.Columns["nDiasGracia"].ReadOnly = false;
            Sol.Columns["nTasaCompensatoria"].ReadOnly = false;
            Sol.Columns["nPlazoCuota"].ReadOnly = false;
            Sol.Columns["nCapitalSolicitado"].ReadOnly = false;

            Sol.Rows[0]["nTasaCompensatoria"] = this.txtTasaInteres.Text;
            Sol.Rows[0]["idEstado"] = cboNuevoEstado.SelectedValue;
            Sol.Rows[0]["nCapitalSolicitado"] = txtMonto.Text;
            Sol.Rows[0]["tObservacion"] = txtObservacion.Text;
            Sol.Rows[0]["idModalidadDes"] = cboModDesemb1.SelectedValue;
            Sol.Rows[0]["nCuotas"] = Convert.ToDecimal(this.txtCuotas.Text);
            Sol.Rows[0]["nDiasGracia"]= Convert.ToDecimal(this.txtDiasGracia.Text);
            Sol.Rows[0]["nPlazoCuota"] = Convert.ToInt32(txtFrecuencia.Text);
            
            int idtipoOpera = Convert.ToInt32(Sol.Rows[0]["idOperacion"]);
            DataSet ds = new DataSet("dssolici");
            ds.Tables.Add(Sol);
            String XmlSoli = ds.GetXml();
            XmlSoli = GEN.CapaNegocio.clsCNFormatoXML.EncodingXML(XmlSoli);
            string XmlRep = "<?xml version='1.0' encoding='ISO-8859-1' standalone='no' ?><dsreprog><repro></repro></dsreprog>";
            DataTable dtActualizarSolicitud = Solicitud.InsertaActualizaSolicitud(XmlSoli, XmlRep, objUsuario.nIdAgencia, 0, idtipoOpera);
            this.BotonGrabar1.Enabled = false;
            this.script.Mensaje(dtActualizarSolicitud.Rows[0]["cMensaje"].ToString());
        }

      
        protected void BotonCancelar1_Click(object sender, EventArgs e)
        {
            cargarControles();
            
        }

        protected void cboTipoCalculo_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void limpiar()
        {
            this.txtMonto.Text = "0.00";
            this.txtCuotas.Value = 0;
            this.txtDiasGracia.Value = 0;
            this.txtFrecuencia.Value = 0;
            cboEstado.SelectedValue = "1";
            cboTipoCre.SelectedValue = "1";
            txtObservacion.Text = "";
            Session["idCliente"] = null;
            ViewState["dtSolicitud"] = null;
            hdIdSolicitud.Value = "";
            conBuscarCliente1.LimpiarControl();
            cboTipoCalculo.SelectedValue = "0";
        }

        protected void BotonConsultar1_Click(object sender, EventArgs e)
        {
            if (Session["idCliente"] == null)
            {
                return;
            }
            var idCliente = Convert.ToInt32(Session["idCliente"]);
            if (idCliente == 0)
            {
                return;
            }
            GEN.CapaNegocio.clsCNRetornsCuentaSolCliente RetornaCuentaSolCliente = new GEN.CapaNegocio.clsCNRetornsCuentaSolCliente();
            DataTable dtDatosCuentaSolCliente = RetornaCuentaSolCliente.RetornaCuentaSolCliente(idCliente, "S", "[1]");
            if (dtDatosCuentaSolCliente.Rows.Count == 1)
            {
                this.pnlSolicitud.Visible = true;
                var nIdSolCta = Convert.ToInt32(dtDatosCuentaSolCliente.Rows[0][0]);
                this.conBuscarCliente1.Habilitar(false);
                BuscarSolicitud(nIdSolCta);
            }
            else
            {
                pnlSolicitud.Visible = false;
                this.conBuscarCliente1.Habilitar(true);
                script.Mensaje("No existe solicitud de crédito aprobada para desembolso");
            }
        }

        private void BuscarSolicitud(Int32 CodigoSol)
        {
            DataTable Sol = new DataTable("dtSolicitud");
            Sol = Solicitud.ConsultaSolicitud(CodigoSol, 0);
            ViewState["dtSolicitud"] = Sol;
            if (CodigoSol != 0)
            {
                DataTable dtInt = new CRE.CapaNegocio.clsCNIntervCre().CNdtIntervCre(CodigoSol);

                if (dtInt.Rows.Count == 0)
                {
                    this.pnlSolicitud.Visible = false;
                    conBuscarCliente1.Habilitar(true);
                    script.Mensaje("No registro intervinientes en su Solicitud");
                    limpiar();
                    return;
                }
            }
            if (Sol.Rows.Count > 0)
            {
                //if (Convert.ToInt32(Sol.Rows[0]["idEvaluacion"]) == 0)
                //{
                //    this.pnlSolicitud.Visible = false;
                //    conBuscarCliente1.Habilitar(true);
                //    script.Mensaje("Solicitud no tiene evaluación de crédito");
                //    limpiar();
                //    return;
                //}

                hdIdSolicitud.Value = Sol.Rows[0]["idSolicitud"].ToString();
                dtpFechaSol.SeleccionarFecha = (DateTime)Sol.Rows[0]["dFechaRegistro"];
                txtMonto.Text = Sol.Rows[0]["nCapitalSolicitado"].ToString();
                cboMoneda.SelectedValue = Convert.ToString(Sol.Rows[0]["IdMoneda"]);
                this.txtCuotas.Value = Convert.ToDouble(Sol.Rows[0]["nCuotas"]);
                this.txtFrecuencia.Value = Convert.ToDouble(Sol.Rows[0]["nPlazoCuota"]);
                this.txtDiasGracia.Value = Convert.ToDouble(Sol.Rows[0]["nDiasGracia"]);
                this.dtpFechaDesembolso.SeleccionarFecha = Convert.ToDateTime(Sol.Rows[0]["dFechaDesembolsoSugerido"]);
                this.cboEstado.SelectedValue = Convert.ToString(Sol.Rows[0]["idEstado"]);
                this.cboAsesor.SelectedValue = Convert.ToString(Sol.Rows[0]["idUsuario"]);
                this.cboTipoCre.SelectedValue = Convert.ToString(Sol.Rows[0]["nTipCre"]);
                cargarSubTipoCre();
                cboSubTipoCre.SelectedValue = Convert.ToString(Sol.Rows[0]["nSubTip"]);
                cargarProducto();
                cboProducto.SelectedValue = Convert.ToString(Sol.Rows[0]["nProdu"]);
                cargarSubProducto();
                cboSubProducto.SelectedValue = Convert.ToString(Sol.Rows[0]["nSubPro"]);
                this.txtTasaInteres.Text = Convert.ToString(Sol.Rows[0]["nTasaCompensatoria"]);
                txtTasaMora.Text = Convert.ToString(Sol.Rows[0]["nTasaMoratoria"]);
                txtObservacion.Text = Convert.ToString(Sol.Rows[0]["tObservacion"]);
                CargaEstado(Convert.ToInt32(Sol.Rows[0]["idEstado"]));
                CargaEstadoActual(Convert.ToInt32(Sol.Rows[0]["idEstado"]));
                chTasaEspecial.Enabled = true;
                this.cboTipoCalculo.SelectedValue = Convert.ToString(Sol.Rows[0]["idTipoCalculo"]);

                if (Convert.ToInt32(Sol.Rows[0]["idOperacion"]) == 2)
                {
                    this.txtMonto.Enabled = false;
                }
                else
                {
                    this.txtMonto.Enabled = true;
                }

            }
            else
            {
                limpiar();
            }
        }

        public void CargaEstado(Int32 nCodEstadoPad)
        {
            GEN.CapaNegocio.clsCNEstadoCredito ListarEstado = new GEN.CapaNegocio.clsCNEstadoCredito();
            DataTable dt = ListarEstado.ListarEstado(nCodEstadoPad);

            this.cboNuevoEstado.DataSource = dt;
            this.cboNuevoEstado.DataValueField = dt.Columns[0].ToString();
            this.cboNuevoEstado.DataTextField = dt.Columns[1].ToString();
            cboNuevoEstado.DataBind();
        }

        public void CargaEstadoActual(Int32 nEstadoActual)
        {
            GEN.CapaNegocio.clsCNEstadoCredito ListarEstado = new GEN.CapaNegocio.clsCNEstadoCredito();
            DataTable dt = ListarEstado.ListarEstadoActual(nEstadoActual);

            this.cboEstado.DataSource = dt;
            this.cboEstado.DataValueField = dt.Columns[0].ToString();
            this.cboEstado.DataTextField = dt.Columns[1].ToString();
            cboEstado.DataBind();
        }

        protected void chTasaEspecial_CheckedChanged(object sender, EventArgs e)
        {
            if (chTasaEspecial.Checked)
            {
                this.txtTasaInteres.Enabled = true;
            }
            else
            {
                txtTasaInteres.Enabled = false;
            }
        }

        private bool Validar()
        {
            clsUsuario objUsuario;
            if (Session["DatosUsuarioSession"] == null)
            {
                Session["DatosUsuarioSession"] = new clsCNUsuario().GetDatosUsuario(hUsuario.Value);
                objUsuario = (clsUsuario)Session["DatosUsuarioSession"];
            }
            else
            {
                objUsuario = (clsUsuario)Session["DatosUsuarioSession"];
            }

            if (String.IsNullOrWhiteSpace(hdIdSolicitud.Value))
            {
                this.script.Mensaje("Seleccione una solicitud");                
                return false;
            }

            var Sol=(DataTable)ViewState["dtSolicitud"];

            DataTable dtValidaPermisos = new GEN.CapaNegocio.clsCNAprobacion().CNValidarPermisosAprobacion(
                                        Convert.ToInt32(Sol.Rows[0]["idAgencia"]), 9, 1, 
                                        Convert.ToInt32(this.cboMoneda.SelectedValue), 
                                        Convert.ToInt32(cboProducto.SelectedValue), 
                                        Convert.ToDouble(txtMonto.Text),
                                        objUsuario.idUsuario,
                                        Convert.ToInt32(hPerfil.Value));
            if ((bool)dtValidaPermisos.Rows[0]["lPermisoApr"] == false)
            {
                script.Mensaje(dtValidaPermisos.Rows[0]["cMensaje"].ToString());
                return false;
            }

            if (Convert.ToDecimal(txtMonto.Text) > (Decimal)Sol.Rows[0]["nCapitalSolicitado"])
            {
                script.Mensaje("El Monto no puede ser mayor al solicitado");
                return false;
            }

            if (cboNuevoEstado.SelectedIndex == 0)
            {
                script.Mensaje("Debe seleccionar un cambio de estado valido");
                
                return false;
            }
            return true;
        }
    
    }
}