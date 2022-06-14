using SGA.ENTIDADES;
using SGA.Utilitarios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGA.LogicaNegocio;

namespace SGA.Presentacion.CREDITOS
{
    public partial class frmCancelaAnticipada : System.Web.UI.Page
    {
        GEN.CapaNegocio.clsCNRetornsCuentaSolCliente RetornaCuentaSolCliente = new GEN.CapaNegocio.clsCNRetornsCuentaSolCliente();
        clsWebJScript script = new clsWebJScript();

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
                hUsuario.Value = ((clsUsuario)Session["DatosUsuarioSession"]).cUsuario;
                lblOpcion.Text = Session["cOpcion"].ToString().ToUpper() + ":";

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
            }
            catch (Exception)
            {
                throw;
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
            }
        }

        private void cargadatos()
        {
            if (hIdCuenta.Value == "")
            {
                this.BotonGrabar1.Enabled = false;
                this.LimpiarDatos();
                return;
            }
            int nNumCredito = Convert.ToInt32(hIdCuenta.Value);

            if (nNumCredito <= 0)
            {
                this.BotonGrabar1.Enabled = false;
                this.LimpiarDatos();
                return;
            }

            pnInfoCredito.Visible = true;
            pnlDetalle.Visible = true;
            BotonGrabar1.Visible = true;
            BotonCancelar1.Visible = true;

            CRE.CapaNegocio.clsCNCredito Credito = new CRE.CapaNegocio.clsCNCredito();
            var dtCredito = Credito.CNdtDataCreditoCobro(nNumCredito);

            h_saldoInteresFecha.Value = ((decimal)(dtCredito.Rows[0]["saldoInteresCancelacion"])).ToString(); //((decimal)(dtCredito.Rows[0]["nInteresDia"]) - (decimal)(dtCredito.Rows[0]["nInteresPagado"])).ToString(); ;
            h_saldoInteresPactado.Value = ((decimal)(dtCredito.Rows[0]["nInteresPactado"]) - (decimal)(dtCredito.Rows[0]["nInteresPagado"])).ToString(); ;

            //hvv(20211120): Se reemplaza interes por saldoInteresCancelación
            h_totalFecha.Value = ((decimal)(dtCredito.Rows[0]["nCapitalDesembolso"]) - (decimal)(dtCredito.Rows[0]["nCapitalPagado"]) +
                            (decimal)(dtCredito.Rows[0]["saldoInteresCancelacion"]) +
                            (decimal)(dtCredito.Rows[0]["nMoraGenerado"]) - (decimal)(dtCredito.Rows[0]["nMoraPagada"]) +
                            (decimal)(dtCredito.Rows[0]["nOtrosGenerado"]) - (decimal)(dtCredito.Rows[0]["nOtrosPagado"])).ToString();

            h_totalPactado.Value = ((decimal)(dtCredito.Rows[0]["nCapitalDesembolso"]) - (decimal)(dtCredito.Rows[0]["nCapitalPagado"]) +
                            (decimal)(dtCredito.Rows[0]["nInteresPactado"]) - (decimal)(dtCredito.Rows[0]["nInteresPagado"]) +
                            (decimal)(dtCredito.Rows[0]["nMoraGenerado"]) - (decimal)(dtCredito.Rows[0]["nMoraPagada"]) +
                            (decimal)(dtCredito.Rows[0]["nOtrosGenerado"]) - (decimal)(dtCredito.Rows[0]["nOtrosPagado"])).ToString();

            this.txtSalCap.Text = ((decimal)(dtCredito.Rows[0]["nCapitalDesembolso"]) - (decimal)(dtCredito.Rows[0]["nCapitalPagado"])).ToString();
            this.txtSalInt.Text = h_saldoInteresFecha.Value;
            this.txtSalMor.Text = ((decimal)(dtCredito.Rows[0]["nMoraGenerado"]) - (decimal)(dtCredito.Rows[0]["nMoraPagada"])).ToString();
            this.txtSalGas.Text = ((decimal)(dtCredito.Rows[0]["nOtrosGenerado"]) - (decimal)(dtCredito.Rows[0]["nOtrosPagado"])).ToString();
            this.txtTotPag.Text = h_totalFecha.Value;
            txtMonEfectivo.Text = this.txtTotPag.Text;
            FormatoDeuda();
            this.BotonGrabar1.Enabled = true;

            CultureInfo culture;
            if (dtCredito.Rows[0]["idMoneda"].ToString() == "1")
            {
                culture = CultureInfo.GetCultureInfo("es-PE");
            }
            else
            {
                culture = CultureInfo.GetCultureInfo("en-US");
            }

            Session["culture"] = culture;
            txtMonDiferencia.Text = (txtMonEfectivo.Value - this.txtTotPag.Value).ToString();
        }

        private void FormatoDeuda()
        {
             var culture=(CultureInfo)Session["culture"];

            if (txtSalCap.Text != "")
            {
                this.txtSalCap.Text = string.Format("{0:0.00}", Convert.ToDouble(txtSalCap.Text));
                this.txtSalInt.Text = string.Format("{0:0.00}", Convert.ToDouble(txtSalInt.Text));
                this.txtSalMor.Text = string.Format("{0:0.00}", Convert.ToDouble(txtSalMor.Text));
                this.txtSalGas.Text = string.Format("{0:0.00}", Convert.ToDouble(txtSalGas.Text));
            }

            if (culture == null)
            {
                this.txtTotPag.Text = string.Format("{0:0.00}", txtTotPag.Value);
                txtMonDiferencia.Text = string.Format("{0:0.00}", txtMonDiferencia.Value);
            }
            //else
            //{
            //    this.txtTotPag.Text = string.Format(culture, "{0:c}", txtTotPag.Value);
            //    txtMonDiferencia.Text = string.Format(culture, "{0:c}", txtMonDiferencia.Value);
            //}
        }

        private void LimpiarDatos()
        {
            this.txtSalCap.Text = "0.00";
            this.txtSalInt.Text = "0.00";
            this.txtSalMor.Text = "0.00";
            this.txtSalGas.Text = "0.00";
            this.txtTotPag.Text = "0.00";
            txtMonEfectivo.Text = "0.00";
            txtMonDiferencia.Text = "0.00";
            chbPagoTotal.Checked = false;

            this.conBuscarCliente1.LimpiarControl();

            this.BotonGrabar1.Enabled = false;
            hIdCuenta.Value = "";
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
            if (validar())
            {
                CRE.CapaNegocio.clsCNPlanPago PlanPago = new CRE.CapaNegocio.clsCNPlanPago();
                int idCuenta = Convert.ToInt32(hIdCuenta.Value);
                int nNumCredito = Convert.ToInt32(hIdCuenta.Value);
                decimal nCapital = Convert.ToDecimal(txtSalCap.Text);
                decimal nInte = Convert.ToDecimal(txtSalInt.Text);
                decimal nMora = Convert.ToDecimal(txtSalMor.Text);
                decimal nOtros = Convert.ToDecimal(txtSalGas.Text);
                decimal nTotPag = Convert.ToDecimal(txtTotPag.Text);
                DataTable TablaUpPpg = PlanPago.UpCancelAnti(idCuenta, objUsuario.dFecSystem, objUsuario.idUsuario, nTotPag, nCapital, nInte, nMora, nOtros, objUsuario.nIdAgencia,chbPagoTotal.Checked);
                script.Mensaje("Cobro satisfactorio con kardex N°: " + TablaUpPpg.Rows[0][0].ToString());
                this.BotonCancelar1.Enabled = false;
                //Emisión de Voucher
                if (TablaUpPpg != null)
                {
                    DataTable dtCobro = PlanPago.CNGetCobro(Convert.ToInt32(TablaUpPpg.Rows[0][0]), nNumCredito);
                    DataTable detCobro = PlanPago.CNGetDetCobro(Convert.ToInt32(TablaUpPpg.Rows[0][0]), nNumCredito);
                    for (int i = 0; i < 1; i++)
                    {
                        EmitirVoucher(dtCobro, detCobro);
                    }
                }
                LiberarCuenta();
                this.LimpiarDatos();                
            }
        }

        private bool validar()
        {
            if (hIdCuenta.Value == "")
            {
                script.Mensaje("El cobro debe corresponder a algún crédito. Busque un crédito a cancelar");
                this.BotonGrabar1.Enabled = false;
                return false;
            }
            if (Convert.ToDouble(this.txtTotPag.Text) <= 0)
            {
                script.Mensaje("El Monto a Pagar debe ser mayor a 0");
                return false;
            }
            return true;
        }

        protected void BotonCancelar1_Click(object sender, EventArgs e)
        {
            LiberarCuenta();
            pnInfoCredito.Visible = true;
            pnlDetalle.Visible = true;
            this.LimpiarDatos();
            this.BotonGrabar1.Enabled = false;            
        }

        protected void btnDistribuir_Click(object sender, EventArgs e)
        {
           
        }

        protected void txtMonEfectivo_TextChanged(object sender, EventArgs e)
        {
            txtMonDiferencia.Text = (txtMonEfectivo.Value - this.txtTotPag.Value).ToString();
        }

        protected void btnRecibido_Click(object sender, EventArgs e)
        {

        }

        public void LiberarCuenta()
        {
            new GEN.CapaNegocio.clsCNRetornaNumCuenta().UpdEstCuenta(Convert.ToInt32(hIdCuenta.Value), 0);
            this.conBuscarCliente1.Habilitar(true);
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
            cvou += "<center>CREDITOS - CANCELACIÓN</center><br>";
            cvou += "----------------------------------------<br>";
            cvou += "Cuenta: " + dtDatosCobro.Rows[0]["idCuenta"] + " Cod. Cliente: " + dtDatosCobro.Rows[0]["idCli"] + "<br>";
            cvou += "Cliente: " + dtDatosCobro.Rows[0]["cNombre"] + "<br>";
            //if (detCobro.Rows[0]["dFecProxPag"] != DBNull.Value)
            //{
            //    cvou += "Próxima Fecha de Pago: " + string.Format("{0:dd/MM/yy}", Convert.ToDateTime(detCobro.Rows[0]["dFecProxPag"])) + "<br>";
            //}
            //cvou += "Cuota: " + cCuoPagTot + "       Resta:" + dtDatosCobro.Rows[0]["nTotPendientes"] + "/" + dtDatosCobro.Rows[0]["nTotCuotas"] + " <br>";
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

        protected void chbPagoTotal_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chbPagoTotal.Checked)
                {
                    this.txtSalInt.Text = h_saldoInteresPactado.Value;
                    this.txtTotPag.Text = h_totalPactado.Value;
                }
                else
                {
                    this.txtSalInt.Text = h_saldoInteresFecha.Value;
                    this.txtTotPag.Text = h_totalFecha.Value;
                }
                txtMonEfectivo.Text = this.txtTotPag.Text;
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }
    }
}