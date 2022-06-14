using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGA.ENTIDADES;
using SGA.LogicaNegocio;

namespace SGA.Presentacion.CREDITOS
{
    public partial class frmReimprimeVoucher : System.Web.UI.Page
    {
        SGA.Utilitarios.clsWebJScript script = new Utilitarios.clsWebJScript();

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
                hUsuario.Value = ((clsUsuario)Session["DatosUsuarioSession"]).cUsuario;
                lblOpcion.Text = Session["cOpcion"].ToString().ToUpper() + ":";

            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void BotonImprimir1_Click(object sender, EventArgs e)
        {
            if (txtCuenta.Text == "" || txtKardex.Text == "")
            {
                script.Mensaje("Ingrese los datos correctos por favor");
                return;
            }
            CRE.CapaNegocio.clsCNPlanPago PlanPago = new CRE.CapaNegocio.clsCNPlanPago();
            var dtDatosMaestro = PlanPago.CNGetCobro(Convert.ToInt32(txtKardex.Text), Convert.ToInt32(txtCuenta.Text));
            if (dtDatosMaestro.Rows.Count < 1)
            {
                script.Mensaje("No se encontraró el voucher con los datos ingresados, verifique por favor");
                return;
            }
            //if (dtDatosMaestro.Rows[0]["idTipoOperacion"].ToString() == "1")
            //{
            //    imprimirVoucherDesembolso();
            //}
            //else if (dtDatosMaestro.Rows[0]["idTipoOperacion"].ToString() == "2")
            //{
                imprimirVoucherCobro();
            //}
            //else
            //{
            //    script.Mensaje("La operación no pertenece al módulo de créditos");
            //}


        }

        private void imprimirVoucherCobro()
        {
            CRE.CapaNegocio.clsCNPlanPago PlanPago = new CRE.CapaNegocio.clsCNPlanPago();
            DataTable dtDatosCobro = PlanPago.CNGetCobro(Convert.ToInt32(txtKardex.Text), Convert.ToInt32(txtCuenta.Text));
            DataTable detCobro = PlanPago.CNGetDetCobro(Convert.ToInt32(txtKardex.Text), Convert.ToInt32(txtCuenta.Text));

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
                    if (dtDatosCobro.Rows.Count > 0)
                    {
                        cCuoPagPar += "A Cuenta de la Cuota N°: " + " " + detCobro.Rows[i]["idCuota"].ToString() + "  " + dtDatosCobro.Rows[0]["cMoneda"] + " ";
                        nConPagPar += Convert.ToDecimal(detCobro.Rows[i]["nMonPagCuo"].ToString());
                        cCuoPagPar += nConPagPar + "\r\n";
                    }

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
            cvou += "Fec Hor: " + ((DateTime)dtDatosCobro.Rows[0]["dFecHoraOpe"]).ToString("dd/MM/yy HH:mm") + "<br>";

            Session["cVoucher"] = cvou;
            string reportpath = "RptVouchers.rdlc";
            var cReporte = reportpath;

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../frmVoucher.aspx?cNomReporte=" + cReporte + "');", true);

        }

        private void imprimirVoucherDesembolso()
        {
            CRE.CapaNegocio.clsCNPlanPago Desmbolso = new CRE.CapaNegocio.clsCNPlanPago();

            DataTable dtDesembolso = Desmbolso.CNGetCobro(Convert.ToInt32(txtKardex.Text), Convert.ToInt32(txtCuenta.Text));
            DataTable detDesembolso = Desmbolso.CNGetDetCobro(Convert.ToInt32(txtKardex.Text), Convert.ToInt32(txtCuenta.Text));

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
            GEN.CapaNegocio.clsCNAgencia cnagencia = new GEN.CapaNegocio.clsCNAgencia();
            var cDatAge = cnagencia.DatosAgenciaDireccion(objUsuario.nIdAgencia).Rows[0];
            string cDirAge = cDatAge["cDirección"].ToString().Trim();
            string cvou = "", cMoneda = "";
            var nMonCuoPag = Convert.ToDecimal(detDesembolso.Rows[0]["nMonPagCuo"]);
            var nLongMax = dtDesembolso.Rows[0]["nMontoOperacion"].ToString().Length;
            var nMontoOperacion = Convert.ToDecimal(dtDesembolso.Rows[0]["nMontoOperacion"]);
            var nSaldCre = Convert.ToDecimal(dtDesembolso.Rows[0]["nSaldCre"]);
            var nInteres = nSaldCre - nMontoOperacion;
            cMoneda = dtDesembolso.Rows[0]["cMoneda"].ToString().Trim();

            cvou += "<center>..Financiamos</center>" + "<br>";
            cvou += "<center>..Tu Progreso</center>" + "<br>";
            cvou += "..N° Ope.: " + dtDesembolso.Rows[0]["IdKardex"] + "<br>";
            cvou += ".." + cDirAge + "<br>";
            cvou += "<center>..CREDITOS - DESEMBOLSO</center><br>";
            cvou += "----------------------------------------<br>";
            cvou += "..Cuenta: " + dtDesembolso.Rows[0]["idCuenta"] + " Cod. Cliente: " + dtDesembolso.Rows[0]["idCli"] + "<br>";
            cvou += "..Cliente: " + dtDesembolso.Rows[0]["cNombre"] + "<br>";
            cvou += "----------------------------------------<br>";
            cvou += "..CAPITAL: " + cMoneda + FormatoDerechaMonto(nMontoOperacion.ToString(), nLongMax) + "<br>";
            cvou += "..INTERES: " + cMoneda + FormatoDerechaMonto(nInteres.ToString(), nLongMax) + "<br>";

            cvou += "----------------------------------------<br>";
            cvou += "..TOTAL PAGAR:" + cMoneda + nSaldCre.ToString() + "<br>";
            cvou += "..CUOTA : " + cMoneda + nMonCuoPag.ToString() + "<br>";
            cvou += "..Usuario: " + dtDesembolso.Rows[0]["cNameUsu"].ToString() + "<br>";
            cvou += "..Fec Hor: " + DateTime.Now.ToString("dd/MM/yy HH:mm") + "<br>";

            Session["cVoucher"] = cvou;
            string reportpath = "RptVouchers.rdlc";
            var cReporte = reportpath;

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../frmVoucher.aspx?cNomReporte=" + cReporte + "');", true);

        }

        private string FormatoDerechaMonto(string cTexto, int nLongMax)
        {
            return cTexto.PadLeft(nLongMax, ' ');
        }
    }
}