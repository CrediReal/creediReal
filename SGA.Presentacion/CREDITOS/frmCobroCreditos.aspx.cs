using SGA.ENTIDADES;
using SGA.Utilitarios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using SGA.LogicaNegocio;

namespace SGA.Presentacion.CREDITOS
{
    public partial class frmCobroCreditos : System.Web.UI.Page
    {
        GEN.CapaNegocio.clsCNRetornsCuentaSolCliente RetornaCuentaSolCliente = new GEN.CapaNegocio.clsCNRetornsCuentaSolCliente();
        clsWebJScript script = new clsWebJScript();
        clsCNCronograma cnocronograma = new clsCNCronograma();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["usuario"] != null)
                {
                    Session["DatosUsuarioSession"] = new SGA.LogicaNegocio.clsCNUsuario().GetDatosUsuario(Request.QueryString["usuario"].ToString());
                }
                if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor " + this.ToString()); }

                if (IsPostBack) return;

               // lblOpcion.Text = Session["cOpcion"].ToString().ToUpper() + ":";

                hUsuario.Value = ((clsUsuario)Session["DatosUsuarioSession"]).cUsuario;

                if (this.ValidarInicioOpe() != "A")
                {
                    pnInfoCredito.Visible = false;
                    conBuscarCliente1.Visible = false;
                    BotonGrabar1.Visible = false;
                    BotonCancelar1.Visible = false;
                    BotonConsultar1.Visible = false;
                    pnlDetalle.Visible = false;
                    return;
                }
                CargarCboMedioPago();
                this.txtMontoMora.Attributes.Add("onfocus", "this.select()");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void CargarCboMedioPago()
        {
            CRE.CapaNegocio.clsCNCredito Credito = new CRE.CapaNegocio.clsCNCredito();
            DataTable dtMediosPago = Credito.ListaMediosPago();
            this.cboMedioPago.DataSource = dtMediosPago;
            this.cboMedioPago.DataValueField = dtMediosPago.Columns[0].ToString();
            this.cboMedioPago.DataTextField = dtMediosPago.Columns[1].ToString();
            cboMedioPago.DataBind();
            this.cboMedioPago.SelectedValue = "";
        }

        protected void BotonGrabar1_Click(object sender, EventArgs e)
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

            if (this.txtCuotasPendientes.Text == "")
            {
                BotonGrabar1.Enabled = false;
                this.script.Mensaje("Por favor busque un crédito e ingrese el monto a pagar");
                return;
            }
            if (Session["idCliente"] == null)
            {
                BotonGrabar1.Enabled = false;
                this.script.Mensaje("Seleccione un cliente para el cobro");
                return;
            }

            if (this.txtMontoNeto.Text.Trim() == "")
            {
                this.BotonGrabar1.Enabled = false;
                return;
            }
            if (this.txtMoraPag.Text.Trim() == "")
            {
                this.BotonGrabar1.Enabled = false;
                return;
            }
            if (Convert.ToDouble(this.txtMontoNeto.Text) > 0)
            {
                double nSumMonDistrib = 0;
                if (!string.IsNullOrEmpty(this.txtCapitalPag.Text.Trim()))
                    nSumMonDistrib = nSumMonDistrib + Convert.ToDouble(this.txtCapitalPag.Text.Trim());

                if (!string.IsNullOrEmpty(this.txtInteresPag.Text.Trim()))
                    nSumMonDistrib = nSumMonDistrib + Convert.ToDouble(this.txtInteresPag.Text.Trim());

                if (!string.IsNullOrEmpty(this.txtOtrosPag.Text.Trim()))
                    nSumMonDistrib = Math.Round(nSumMonDistrib + Convert.ToDouble(this.txtOtrosPag.Text.Trim()), 2);

                //if (!string.IsNullOrEmpty(this.txtMoraPag.Text.Trim()))
                //    nSumMonDistrib = Math.Round(nSumMonDistrib + Convert.ToDouble(this.txtMoraPag.Text.Trim()), 2);

                if (Convert.ToDouble(this.txtMontoNeto.Text) != nSumMonDistrib)
                {
                    this.BotonGrabar1.Enabled = false;
                    this.script.Mensaje("El Monto Distribuido no es igual al monto ingresado. Debe presionar ENTER para distribuir el pago");
                    return;
                }
            }

            double nMonPagado = Convert.ToDouble(this.txtTotalPago.Text);
            if (nMonPagado <= 0)
            {
                script.Mensaje("El Monto a Pagar debe ser mayor a 0");
                return;
            }


            if (txtMonEfectivo.Value == 0)
            {
                script.Mensaje("El Monto recibido debe ser mayor a 0");
                txtMonEfectivo.Focus();
                return;
            }

            if (txtMonEfectivo.Value < txtTotalPago.Value)
            {
                script.Mensaje("El Monto recibido debe ser mayor o igual al monto total");
                txtMonEfectivo.Focus();
                return;
            }

            //DATOS DE MEDIOS DE PAGO
            int IdMedioPago;
            string cNroOperacion;

            IdMedioPago = Convert.ToInt32(cboMedioPago.SelectedValue);
            cNroOperacion = txtNroOperacion.Text.Trim();

            if (IdMedioPago == 0)
            {
                script.Mensaje("Seleccione un medio de pago");
                cboMedioPago.Focus();
                return;
            }

            if (IdMedioPago > 1 &&  cNroOperacion.Length < 5)
            {
                script.Mensaje("Ingrese un número de operación válido");
                txtNroOperacion.Focus();
                return;
            }

            DataSet ds = new DataSet("dsPlanPagos");
            var dtPlanPago = (DataTable)ViewState["dtPlanPago"];
            ds.Tables.Add(dtPlanPago);
            string xmlPpg = ds.GetXml();
            CRE.CapaNegocio.clsCNPlanPago PlanPago = new CRE.CapaNegocio.clsCNPlanPago();
            double nMoraPagada = this.txtMontoMora.Value;
            int nNumCredito = Convert.ToInt32(hIdCuenta.Value);




            DataTable TablaUpPpg = PlanPago.UpCobroPpg(xmlPpg, objUsuario.dFecSystem, objUsuario.idUsuario, objUsuario.nIdAgencia, nMoraPagada, nNumCredito, IdMedioPago, cNroOperacion);
            this.script.Mensaje("Cobro satisfactorio con kardex N°: " + TablaUpPpg.Rows[0][0].ToString());
            this.BotonCancelar1.Enabled = false;
            //Emisión de Voucher
            if (TablaUpPpg != null)
            {
                DataTable dtCobro = PlanPago.CNGetCobro(Convert.ToInt32(TablaUpPpg.Rows[0][0]), nNumCredito);
                DataTable detCobro = PlanPago.CNGetDetCobro(Convert.ToInt32(TablaUpPpg.Rows[0][0]), nNumCredito);
                for (int i = 0; i < 1; i++)
                {
                    //                    DataTable dtCobro2 = PlanPago.CNGetCobro(Convert.ToInt32(TablaUpPpg.Rows[0][0]));
                    EmitirVoucher(dtCobro, detCobro);
                }
            }
            chbMora.Checked = false;
            LiberarCuenta();
            ds.Dispose();
            this.LimpiarDatos();
        }

        protected void BotonCancelar1_Click(object sender, EventArgs e)
        {
            LimpiarDatos();
            conBuscarCliente1.Habilitar(true);
        }

        protected void BotonConsultar1_Click(object sender, EventArgs e)
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

            if (Session["idCliente"] == null)
            {
                return;
            }
            var idCliente = Convert.ToInt32(Session["idCliente"]);
            if (idCliente == 0)
            {
                return;
            }
            DataTable dtDatosCuentaSolCliente = RetornaCuentaSolCliente.RetornaCuentaSolCliente(idCliente, "C", "[5]");
            if (dtDatosCuentaSolCliente.Rows.Count == 1)
            {
                this.hIdCuenta.Value = dtDatosCuentaSolCliente.Rows[0][0].ToString();
                GEN.CapaNegocio.clsCNRetornaNumCuenta RetornaNumCuenta = new GEN.CapaNegocio.clsCNRetornaNumCuenta();
                DataTable dtDatosNumCuenta = RetornaNumCuenta.RetornaNumCuenta(Convert.ToInt32(hIdCuenta.Value), "C", "[5]");
                if (dtDatosNumCuenta.Rows.Count == 0)
                {
                    script.Mensaje("No se encontró Número de Cuenta");
                    this.hIdCuenta.Value = "";
                }
                else
                {
                    DataTable dtEstCuenta = RetornaNumCuenta.VerifEstCuenta(Convert.ToInt32(hIdCuenta.Value));
                    var nidUserBloqueo = (Nullable<int>)dtEstCuenta.Rows[0][0];
                    if (nidUserBloqueo != 0)
                    {
                        DataTable dtUsu = new DataTable();
                        dtUsu = RetornaNumCuenta.BusUsuBlo((int)nidUserBloqueo);
                        var cUserBloqueo = dtUsu.Rows[0][0].ToString();
                        script.Mensaje("Cuenta Bloqueada por usuario: " + cUserBloqueo);
                        this.hIdCuenta.Value = "";
                        btnKardex.Visible = false;
                    }
                    else
                    {
                        conBuscarCliente1.Habilitar(false);
                        RetornaNumCuenta.UpdEstCuenta(Convert.ToInt32(hIdCuenta.Value), objUsuario.idUsuario);
                        cargadatos();
                        
                    }
                }                
            }
            else if (dtDatosCuentaSolCliente.Rows.Count > 1)
            {
                dtgCreditos.DataSource = dtDatosCuentaSolCliente;
                dtgCreditos.DataBind();
                btnKardex.Visible = false;
            }

        }

        protected void dtgCreditos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dtgCreditos.Rows.Count > 0)
            {
                hIdCuenta.Value = dtgCreditos.SelectedRow.Cells[0].Text;
                cargadatos();
            }
        }

        private void cargadatos()
        {
            if (hIdCuenta.Value=="")
            {
                this.BotonGrabar1.Enabled = false;
                this.LimpiarDatos();
                return;
            }
            int nNumCredito = Convert.ToInt32(hIdCuenta.Value);
            
            CRE.CapaNegocio.clsCNCredito Credito = new CRE.CapaNegocio.clsCNCredito();
            var dtCredito = Credito.CNdtDataCreditoCobro(nNumCredito);
            ViewState["dtCredito"] = dtCredito;
            //this.txtMoneda.Text = dtCredito.Rows[0]["cMoneda"].ToString();
            this.txtTotalCuotas.Text = dtCredito.Rows[0]["nCuotas"].ToString();
            this.txtDiasAtraso.Text = dtCredito.Rows[0]["nAtraso"].ToString();
            this.txtMoraPen.Text = dtCredito.Rows[0]["nMonSalMora"].ToString();
            txtMontoMora.Text = "0.00";
            ////this.txtFecDesembolso.Text = dtCredito.Rows[0]["dFechaDesembolso"].ToString().Substring(0, 10);
            ////this.txtTotDevolver.Text = dtCredito.Rows[0]["nMonTotDevolver"].ToString();
            ////this.txtTasaInteres.Text = dtCredito.Rows[0]["nTasaCompensatoria"].ToString();


            CRE.CapaNegocio.clsCNPlanPago PlanPago = new CRE.CapaNegocio.clsCNPlanPago();
            var dtPlanPago = PlanPago.CNdtPlanPago(nNumCredito);
            ViewState["dtPlanPago"] = dtPlanPago;
            this.txtPriCuotaPen.Text = PlanPago.nPrimeraCuotaPen(dtPlanPago).ToString();
            this.txtCuotasVencidas.Text = PlanPago.nCuotasVencidas(dtPlanPago).ToString();
            this.txtCuotasPendientes.Text = PlanPago.nNumCuotasPen(dtPlanPago).ToString();

            DataTable dtDeudaPendiente = new DataTable("dtDeudaPendiente");
            if (dtPlanPago.Rows.Count > 0)
            {
                dtDeudaPendiente = PlanPago.dtCNDeudaPendiente(dtPlanPago, Convert.ToInt32(dtCredito.Rows[0]["nAtraso"]));
                this.txtCapitalPen.Text = dtDeudaPendiente.Rows[0]["nCapitalPen"].ToString();
                this.txtInteresPen.Text = dtDeudaPendiente.Rows[0]["nInteresPen"].ToString();
                this.txtOtrosPen.Text = dtDeudaPendiente.Rows[0]["nOtrosPen"].ToString();
                this.txtTotalPen.Text = dtDeudaPendiente.Rows[0]["nTotalPen"].ToString();
                this.txtSubTotalDeuda.Text = Credito.nSaldoCredito(dtCredito).ToString();
                this.txtMontoNeto.Enabled = true;

                //FormatoDeuda();
                //cargarCronograma(nNumCredito);
                //cargarKardex(nNumCredito);
            }
            else
            {
                this.txtCapitalPen.Text = "0.00";
                this.txtInteresPen.Text = "0.00";
                this.txtOtrosPen.Text = "0.00";
                this.txtTotalPen.Text = "0.00";
                this.txtSubTotalDeuda.Text = "0.00";
                this.txtMontoNeto.Enabled = false;
            }
            //btnKardex.Visible = true;

            //DataTable dtLisCredxCli = new DataTable();

            //DataTable Listkardex = Credito.CNdtLisKardexCre(nNumCredito);
            //dtgBase2.DataSource = Listkardex;
            //this.FormatoKardexpago();

            //// Cargar el Plan de Pagos
            ////clsCNPlanPago PlanPago = new clsCNPlanPago();
            //DataTable ListPlanPago = PlanPago.CNdtPlanPagPosi(nNumCredito);
            //dtgBase1.DataSource = ListPlanPago;
            //this.FormatoPlanPagos();
            //this.btnCancelar1.Enabled = true;
            //this.txtMontoNeto.Focus();

        }

        private void distribuir()
        {
            int nNumCredito = Convert.ToInt32(hIdCuenta.Value);
            CRE.CapaNegocio.clsCNCredito Credito = new CRE.CapaNegocio.clsCNCredito();

            var dtCredito = Credito.CNdtDataCreditoCobro(nNumCredito);
            double nMonSalCre = Credito.nSaldoCredito(dtCredito);
            nMonSalCre = Convert.ToDouble(string.Format("{0:#,##0.##}", Convert.ToDouble(nMonSalCre.ToString())));
            if (Math.Round(nMonSalCre, 2) < Convert.ToDouble(this.txtMontoNeto.Value))
            {
                this.script.Mensaje("Monto a Pagar no puede exceder al Saldo del Crédito: " + nMonSalCre.ToString());
                this.BotonGrabar1.Enabled = false;
                return;
            }
            double nMontoaPagar = Convert.ToDouble(this.txtMontoNeto.Value);
            DataTable dtPlanPagado = new DataTable("dtPlanPagado");
            //bool lPagaMora = this.chcBase1.Checked;
            //dtPlanPagado = PlanPago.dtCNPagoDistribuido(dtPlanPago, nMontoaPagar, false);
            
            CRE.CapaNegocio.clsCNPlanPago PlanPago = new CRE.CapaNegocio.clsCNPlanPago();
            var dtPlanPago = PlanPago.CNdtPlanPago(nNumCredito);
            ViewState["dtPlanPago"] = dtPlanPago;


            dtPlanPagado = cnocronograma.dtCNPagoDistribuido(dtPlanPago, nMontoaPagar, false); // distribucion con la mora
            this.txtCapitalPag.Text = dtPlanPagado.Rows[0]["nCapitalPag"].ToString();
            this.txtInteresPag.Text = dtPlanPagado.Rows[0]["nInteresPag"].ToString();
            //this.txtBase13.Text = dtPlanPagado.Rows[0]["nMoraPag"].ToString();
            txtMoraPag.Text = dtPlanPagado.Rows[0]["nMoraPag"].ToString();
            this.txtOtrosPag.Text = dtPlanPagado.Rows[0]["nOtrosPag"].ToString();
            //this.txtNumRea4.Text = (Convert.ToDouble(this.txtNumRea1.Text) + Convert.ToDouble(this.txtNumRea3.Text)).ToString();
            
            this.txtTotalPago.Text = this.txtMontoNeto.Text;
            

            ViewState["dtPlanPago"] = dtPlanPago;
            this.BotonGrabar1.Enabled = true;
            txtMonEfectivo.Text = (txtMontoNeto.Value + txtMontoMora.Value).ToString();
            txtMonDiferencia.Text = (txtMonEfectivo.Value - txtTotalPago.Value).ToString();
            FormatoMonto();
        }

        private void FormatoMonto()
        {
            if (txtCapitalPag.Text != "")
            {
                this.txtCapitalPag.Text = string.Format("{0:0.00}", Convert.ToDouble(txtCapitalPag.Text));
                this.txtInteresPag.Text = string.Format("{0:0.00}", Convert.ToDouble(txtInteresPag.Text));
                this.txtMoraPag.Text = string.Format("{0:0.00}", Convert.ToDouble(txtMoraPag.Text));
                this.txtOtrosPag.Text = string.Format("{0:0.00}", Convert.ToDouble(txtOtrosPag.Text));
                this.txtTotalPago.Text = string.Format("{0:0.00}", txtTotalPago.Value);
                this.txtSubTotalDeuda.Text = string.Format("{0:0.00}", txtSubTotalDeuda.Value);
                txtMonDiferencia.Text = string.Format("{0:0.00}", txtMonDiferencia.Value);
            }
        }

        protected void btnDistribuir_Click(object sender, EventArgs e)
        {
            distribuir();
        }

        private void LimpiarDatos()
        {
            hIdCuenta.Value = "";
            this.cboMedioPago.SelectedValue = "0";
            this.txtNroOperacion.Text = "";
            this.txtTotalCuotas.Text = "";
            this.txtDiasAtraso.Text = "";
            this.txtPriCuotaPen.Text = "";
            this.txtCuotasVencidas.Text = "";
            this.txtCuotasPendientes.Text = "";
            this.txtCapitalPen.Text = "";
            this.txtInteresPen.Text = "";
            this.txtMoraPen.Text = "";
            this.txtOtrosPen.Text = "";
            this.txtTotalPen.Text = "";
            this.txtMontoNeto.Text = "0.00";
            this.txtMoraPag.Text = "0.00";
            this.txtTotalPago.Text = "0.00";
           
            this.txtCapitalPag.Text = "0.00";
            this.txtInteresPag.Text = "0.00";
            this.txtOtrosPag.Text = "0.00";
            txtMontoMora.Text = "0.00";
            txtSubTotalDeuda.Text = "0.00";

            this.conBuscarCliente1.LimpiarControl();

            this.txtOtrosPag.Text = "";
            //this.txtBase13.Text = "";
            this.txtInteresPag.Text = "";
            this.txtCapitalPag.Text = "";

            this.BotonGrabar1.Enabled = false;
            this.txtMoraPag.Enabled = false;
            this.txtCuotasPendientes.Enabled = false;
            this.dtgCreditos.DataSource = null;
            this.dtgCreditos.DataBind();

            txtMonEfectivo.Text = "0.00";
            ViewState["dtPlanPago"] = null;
        }

        public void LiberarCuenta()
        {
            new GEN.CapaNegocio.clsCNRetornaNumCuenta().UpdEstCuenta(Convert.ToInt32(hIdCuenta.Value), 0);
            this.conBuscarCliente1.Habilitar(true);
            this.txtMontoNeto.Text = "0.00";
        }

        public void EmitirVoucher(DataTable dtDatosCobro, DataTable detCobro)
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
            //Validando las Cuotas Pagadas en el kardex
            int nLongMax = 0;
            string cCuoPagTot = "", cMoneda = "", cvou = "", cCuoPagPar = "", cNumRecibo = "";
            decimal nCapital = 0M, nInteres = 0M, nMora = 0M, nOtros = 0M, nMonCuoPag = 0M, nConPagPar = 0M;
            GEN.CapaNegocio.clsCNAgencia cnagencia = new GEN.CapaNegocio.clsCNAgencia();

            var cDatAge = cnagencia.DatosAgenciaDireccion(objUsuario.nIdAgencia).Rows[0];
            //string cDirAge = cDatAge["cDepartamento"].ToString().Trim() + @"/" + cDatAge["cProvincia"].ToString().Trim()
            //                + @"/" + cDatAge["cDistrito"].ToString().Trim() + @"/" + cDatAge["cDirección"].ToString().Trim();
            string cDirAge = cDatAge["cDirección"].ToString().Trim();

            for (int i = 0; i < detCobro.Rows.Count; i++)
            {
                if (Convert.ToInt32(detCobro.Rows[i]["idEstadoCuota"].ToString()) == 2)
                {
                    cCuoPagTot += detCobro.Rows[i]["idCuota"].ToString() + ",";
                    if (i > 0 && i % 4 == 0)
                    {
                        cCuoPagTot += "\r\n";
                    }
                }
                else if (Convert.ToInt32(detCobro.Rows[i]["idEstadoCuota"].ToString()) == 1)
                {
                    cCuoPagPar += "A Cuenta de la Cuota N°: " + " " + detCobro.Rows[i]["idCuota"].ToString() + "  " + dtDatosCobro.Rows[0]["cMoneda"] + " ";
                    nConPagPar += Convert.ToDecimal(detCobro.Rows[i]["nMonPagCuo"].ToString());
                    cCuoPagPar += nConPagPar + "\r\n";
                }

                nCapital = nCapital + Convert.ToDecimal(detCobro.Rows[i]["nCapitalDet"]);
                nInteres = nInteres + Convert.ToDecimal(detCobro.Rows[i]["nInteresDet"]);
                nMora = nMora + Convert.ToDecimal(detCobro.Rows[i]["nMoraDet"]);
                nOtros = nOtros + Convert.ToDecimal(detCobro.Rows[i]["nOtrosDet"]);
            }

            nMonCuoPag = Convert.ToDecimal(dtDatosCobro.Rows[0]["nMontoOperacion"]) - nConPagPar;
            nLongMax = dtDatosCobro.Rows[0]["nMontoOperacion"].ToString().Length;
            cMoneda = dtDatosCobro.Rows[0]["cMoneda"].ToString().Trim();

            cNumRecibo = dtDatosCobro.Rows[0]["idAgencia"].ToString().PadLeft(4, '0') + "-" + dtDatosCobro.Rows[0]["nContador"].ToString().PadLeft(7, '0');

            //cvou += "Recibo:" + cNumRecibo + "\r\n";
            cvou += "CREDIREAL - N° Ope.: " + dtDatosCobro.Rows[0]["IdKardex"] + "<br>";
            cvou += cDirAge + "<br>";
            cvou += "<center>CREDITOS - COBRANZAS</center><br>";
            cvou += "----------------------------------------<br>";
            cvou += "Cuenta: " + dtDatosCobro.Rows[0]["idCuenta"] + " Cod. Cliente: " + dtDatosCobro.Rows[0]["idCli"] + "<br>";
            cvou += "Cliente: " + dtDatosCobro.Rows[0]["cNombre"] + "<br>";
            if (detCobro.Rows[0]["dFecProxPag"] != DBNull.Value)
            {
                cvou += "Próxima Fecha de Pago: " + string.Format("{0:dd/MM/yy}", Convert.ToDateTime(detCobro.Rows[0]["dFecProxPag"])) + "<br>";
            }
            cvou += "Cuota: " + cCuoPagTot + "       Resta:" + dtDatosCobro.Rows[0]["nTotPendientes"] + "/" + dtDatosCobro.Rows[0]["nTotCuotas"] + " <br>";
            cvou += "----------------------------------------<br>";
            if (nCapital != 0)
            {
                cvou += "CAPITAL : " + cMoneda + FormatoDerechaMonto(nCapital.ToString(), nLongMax) + "<br>";
            }
            if (nInteres != 0)
            {
                cvou += "INTERES : " + cMoneda + FormatoDerechaMonto(nInteres.ToString(), nLongMax) + "<br>";
            }
            if (nMora != 0)
            {
                cvou += "MORA    : " + cMoneda + FormatoDerechaMonto(nMora.ToString(), nLongMax) + "<br>";
            }
            if (nOtros != 0)
            {
                //            cvou += "SEG. DESGRAVAMEN  : " + cMoneda + FormatoDerechaMonto("0.00",nLongMax)+"<br>";
                cvou += "COMISIONES        : " + cMoneda + FormatoDerechaMonto(nOtros.ToString(), nLongMax) + "<br>";
            }
            cvou += "----------------------------------------<br>";
            cvou += "TOTAL           : " + cMoneda + dtDatosCobro.Rows[0]["nMontoOperacion"] + "<br>";
            cvou += "Usuario: " + dtDatosCobro.Rows[0]["cNameUsu"].ToString() + "<br>";
            cvou += "Fec Hor: " + DateTime.Now.ToString("dd/MM/yy HH:mm") + "<br>";

            Session["cVoucher"] = cvou;
            string reportpath = "RptVouchers.rdlc";
            var cReporte = reportpath;

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../frmVoucher.aspx?cNomReporte=" + cReporte + "');", true);

        }

        private string FormatoDerechaMonto(string cTexto, int nLongMax)
        {
            return cTexto.PadLeft(nLongMax, ' ');
        }

        public string ValidarInicioOpe()
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
            CAJ.CapaNegocio.clsCNControlOpe ValidaOpe = new CAJ.CapaNegocio.clsCNControlOpe();
            string cEstCie = ValidaOpe.ValidaIniOpe(objUsuario.dFecSystem, objUsuario.idUsuario, objUsuario.nIdAgencia);
            // Si Estado es: F--> Falta Iniciar, A--> Caja Abierta, C--> Caja Cerrada
            //string cRpta = this.ValidarInicioOpe();
            switch (cEstCie) // Si Estado es: F--> Falta Iniciar, A--> Caja Abierta, C--> Caja Cerrada  
            {
                case "F":
                    script.Mensaje("Falta Realizar el Inicio de sus Operaciones");
                    //MessageBox.Show("Falta Realizar el Inicio de sus Operaciones", "Validar Inicio de Operaciones", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //this.Dispose();
                    break;
                case "A":
                    break;
                case "C":
                    script.Mensaje("El Usuario ya Cerro Sus Operaciones");
                    //MessageBox.Show("El Usuario ya Cerro Sus Operaciones", "Validar Cierre de Operaciones", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //this.Dispose();
                    break;
                default:
                    script.Mensaje(cEstCie);
                    //MessageBox.Show(cEstCie, "Error al Validar Estado de Operaciones", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //this.Dispose();
                    break;
            }
            return cEstCie;
        }

        protected void btnRecibido_Click(object sender, EventArgs e)
        {
            script.Mensaje("hola");
        }

        protected void txtMonEfectivo_TextChanged(object sender, EventArgs e)
        {
            txtMonDiferencia.Text = (txtMonEfectivo.Value - txtTotalPago.Value).ToString();
        }

        protected void btnKardex_Click(object sender, EventArgs e)
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
            int idCuenta = Convert.ToInt32(hIdCuenta.Value);


            List<ReportParameter> ListaParametros = new List<ReportParameter>();
            List<ReportDataSource> ListaDataSource = new List<ReportDataSource>();

            ListaDataSource.Add(new ReportDataSource("dtsKardexPagoCre", new RPT.CapaNegocio.clsRPTCNPlanPagos().CNKardexPagos(idCuenta)));
            ListaDataSource.Add(new ReportDataSource("dtsAgencia", new RPT.CapaNegocio.clsRPTCNAgencia().CNAgenciaUsuario(objUsuario.nIdAgencia, objUsuario.idUsuario)));


            Session["ListaParametros"] = ListaParametros;
            Session["ListaDataSource"] = ListaDataSource;
            Session["lModal"] = true;

            var cReporte = "rptKardexPagoCre.rdlc";

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../frmGenReporte.aspx?cNomReporte=" + cReporte + "');", true);

        }

        protected void CheckBoxBase1_CheckedChanged(object sender, EventArgs e)
        {
            this.txtMontoMora.Enabled = this.chbMora.Checked;
            this.txtMontoMora.Focus();
            if (!this.chbMora.Checked)
            {
                this.txtMontoMora.Text = "0.00";
                if (this.txtMontoNeto.Text == "")
                {
                    return;
                }
                this.txtTotalPago.Text = (this.txtMontoNeto.Value + this.txtMontoMora.Value).ToString();
            }
            txtMonEfectivo.Text = (txtMontoNeto.Value + txtMontoMora.Value).ToString();
            txtMonDiferencia.Text = (txtMonEfectivo.Value - txtTotalPago.Value).ToString();
        }

        protected void btnDistribuiMora_Click(object sender, EventArgs e)
        {
            distribuir();

            double nMonSalMora = txtMoraPen.Value;
            if (nMonSalMora < txtMontoMora.Value)
            {
                script.Mensaje("Monto a Pagar de Mora no puede exceder al Saldo de Mora: " + nMonSalMora.ToString());
                this.txtMontoMora.Text = "0.00";                
                this.txtMontoMora.Focus();
                this.BotonGrabar1.Enabled = false;
                return;
            }
            this.txtTotalPago.Text = (Convert.ToDouble(this.txtMontoNeto.Value) + Convert.ToDouble(this.txtMontoMora.Text)).ToString();
            if (this.txtMontoNeto.Text=="")
            {
                this.txtMontoNeto.Text = "0.00";
            }
            FormatoMonto();

            txtMonEfectivo.Text = (txtMontoNeto.Value + txtMontoMora.Value).ToString();
            txtMonDiferencia.Text = (txtMonEfectivo.Value - txtTotalPago.Value).ToString();
            FormatoMonto();
        }

        private void cargarCronograma(int idCuenta)
        {
            var dtCronograma = new RPT.CapaNegocio.clsRPTCNPlanPagos().CNCronogramaPagos(idCuenta,0);
            dtgCronograma.DataSource=dtCronograma;
            dtgCronograma.DataBind();
        }

        private void cargarKardex(int idCuenta)
        {
            var dtKardex = new RPT.CapaNegocio.clsRPTCNPlanPagos().CNKardexPagos(idCuenta);
            this.dtgKardex.DataSource = dtKardex;
            dtgKardex.DataBind();
        }

        protected void btnCronograma_Click(object sender, EventArgs e)
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
            int idCuenta = Convert.ToInt32(hIdCuenta.Value);


            List<ReportParameter> ListaParametros = new List<ReportParameter>();
            List<ReportDataSource> ListaDataSource = new List<ReportDataSource>();

            ListaDataSource.Add(new ReportDataSource("dtsPPG", new RPT.CapaNegocio.clsRPTCNPlanPagos().CNCronogramaPagos(idCuenta,0)));
            ListaDataSource.Add(new ReportDataSource("dtsAgencia", new RPT.CapaNegocio.clsRPTCNAgencia().CNAgenciaUsuario(objUsuario.nIdAgencia, objUsuario.idUsuario)));
            

            Session["ListaParametros"] = ListaParametros;
            Session["ListaDataSource"] = ListaDataSource;
            Session["lModal"] = true;

            var cReporte = "rptPlanPagoPosInt.rdlc";

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../frmGenReporte.aspx?cNomReporte=" + cReporte + "');", true);

        }
    }
}