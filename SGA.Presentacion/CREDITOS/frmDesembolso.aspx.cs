using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGA.Utilitarios;
using Microsoft.Reporting.WebForms;
using SGA.ENTIDADES;
using SGA.LogicaNegocio;

namespace SGA.Presentacion.CREDITOS
{
    public partial class frmDesembolso : System.Web.UI.Page
    {
        GEN.CapaNegocio.clsCNSolicitud Solicitud = new GEN.CapaNegocio.clsCNSolicitud();
        clsWebJScript script = new clsWebJScript();
        int idProducto = 0;

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

                if (this.ValidarInicioOpe() != "A")
                {
                    pnlDesembolso.Visible = false;
                    BotonGrabar1.Visible = false;
                    BotonCancelar1.Visible = false;
                    conBuscarCliente1.Visible = false;
                    BotonConsultar1.Visible = false;
                    return;
                }

                cargarMoneda();
                cargarModalidadDesembolso();
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void BotonGrabar1_Click(object sender, EventArgs e)
        {
            if (!validar())
            {
                return;
            }
            insertar();
        }

        protected void BotonCancelar1_Click(object sender, EventArgs e)
        {

        }

        private void insertar()
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

            var Sol = (DataTable)ViewState["Sol"];
            if (Sol.Rows.Count == 0)
            {
                script.Mensaje("Solicitud no tiene Plan de Pagos generado");
                return;
            }
            //GRAVAMEN
            //////if (new CRE.CapaNegocio.clsCNGarantia().CNSaldoGravamen(Convert.ToInt32(hIdSolicitud.Value), Convert.ToDouble(this.txtMontoCapital.Text), Convert.ToInt32(Sol.Rows[0]["idProducto"])))
            //////{
            //////    script.Mensaje("Monto de Garantia no es suficiente");
            //////    return;
            //////}
            //FIN VALIDA GRAVAMEN

            CRE.CapaNegocio.clsCNCredito Credito = new CRE.CapaNegocio.clsCNCredito();

            Int32 idCli = Convert.ToInt32(Sol.Rows[0]["idCli"]);
            DataTable dtNumCre = new DataTable();
            dtNumCre = Credito.NumeroCreditos(idCli);

           
            if (Convert.ToInt32(dtNumCre.Rows[0]["nNumeroCreditos"]) > 0)
            {
                Sol.Rows[0]["idTipoCliente"] = 2;
            }
            else
            {
                Sol.Rows[0]["idTipoCliente"] = 1;
            }

            DataSet ds = new DataSet("dscredito");
            ds.Tables.Add(Sol);
            String XmlCredito = ds.GetXml();
            XmlCredito = GEN.CapaNegocio.clsCNFormatoXML.EncodingXML(XmlCredito);

            DataTable dtCre = new DataTable();
            dtCre = Credito.Desembolsa(XmlCredito, objUsuario.idUsuario, objUsuario.dFecSystem, objUsuario.nIdAgencia, Convert.ToInt32(cboModDesemb1.SelectedValue));
            hIdCuenta.Value = Convert.ToString(dtCre.Rows[0]["nCuenta"]);
            int idKarDep = Convert.ToInt32(dtCre.Rows[0]["nKarDep"]);
            script.Mensaje("Crédito desembolsado correctamente, por favor imprima el cronograma.");
            this.BotonGrabar1.Visible = false;
            this.BotonImprimir1.Visible = true;
            //////////btnConograma.Enabled = true;
            //////////btnBlanco1.Enabled = true;
            //////////btnContrato.Enabled = true;

            
            CRE.CapaNegocio.clsCNPlanPago Desmbolso = new CRE.CapaNegocio.clsCNPlanPago();
            //Emisión de Voucher
            //DataTable dtCobro = new clsRPTCNDeposito().CNDetalleTransaccion(idKarDep);
            //EmitirVoucher(dtCobro);

            DataTable dtDesembolso = Desmbolso.CNGetCobro((Int32)dtCre.Rows[0]["Kardex"], (Int32)dtCre.Rows[0]["nCuenta"]);
            DataTable detDesembolso = Desmbolso.CNGetDetCobro((Int32)dtCre.Rows[0]["Kardex"], (Int32)dtCre.Rows[0]["nCuenta"]);

            for (int i = 0; i < 1; i++)
            {
                EmitirVoucher(dtDesembolso, detDesembolso);
            }
        }

        public void EmitirVoucher(DataTable dtDesembolso, DataTable detDesembolso)
        {
            //List<ReportDataSource> dtslist = new List<ReportDataSource>();
            //dtslist.Clear();
            //dtslist.Add(new ReportDataSource("dtsRutaLogo", new clsRPTCNAgencia().CNRutaLogo()));
            //dtslist.Add(new ReportDataSource("dtsCobro", dtDeposito));

            //List<ReportParameter> paramlist = new List<ReportParameter>();
            //string reportpath = "RptVouchers.rdlc";
            //new frmReporteLocal(dtslist, reportpath).ShowDialog();
        }

        private bool validar()
        {
            if (string.IsNullOrEmpty(hIdSolicitud.Value))
            {
                script.Mensaje("Debe ingresar un Número de solicitud");
                return false;
            }
            if (Session["idCliente"]==null)
            {
                script.Mensaje("No existen Datos de Cliente para esta solicitud");
                return false;
            }
            int idcli = Convert.ToInt32(Session["idCliente"]);
            CRE.CapaNegocio.clsCNCredito Credito = new CRE.CapaNegocio.clsCNCredito();
            DataTable dtMorcli = new DataTable();
            dtMorcli = Credito.CNdtSumMorCli(idcli);
            double nSalMorCli = Convert.ToDouble(dtMorcli.Rows[0][0]);
            if (nSalMorCli > 0)
            {
                script.Mensaje("El cliente tiene Créditos Vigentes en Mora, consulte Posición del Cliente");
            }
            return true;
        }

        private void BuscarSolicitud(Int32 CodigoSol)
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
            DataTable Sol = new DataTable("dtCredito");
            Sol = Solicitud.SolicitudDesembolso(CodigoSol);
            ViewState["Sol"] = Sol;
            if (Sol.Rows.Count > 0)
            {
                //if (Convert.ToBoolean(Sol.Rows[0]["lAprobado"]) == false)
                //{
                //    script.Mensaje("Solicitud Aprobada no tiene Autorización para Desembolso");
                //    limpiar();
                //    return;
                //}

                if (Convert.ToInt32(Sol.Rows[0]["idEstado"]) != 2)
                {
                    script.Mensaje("Solicitud no esta aprobada");
                    limpiar();
                    return;
                }

                if (Convert.ToDateTime(Sol.Rows[0]["dFecSugerido"]) > objUsuario.dFecSystem)
                {
                    script.Mensaje("La fecha de desembolso no coincide con la fecha en el Cronograma de pago");
                    limpiar();
                    return;
                }

                dtpFechaSol.SeleccionarFecha = Convert.ToDateTime(Sol.Rows[0]["dFechaRegistro"]);
                this.txtMontoCapital.Text = Convert.ToString(Sol.Rows[0]["nCapitalSolicitado"]);
                cboMoneda.SelectedValue = Convert.ToString(Sol.Rows[0]["IdMoneda"]);
                txtNroCuotas.Text = Convert.ToString(Sol.Rows[0]["nCuotas"]);
                txtMonPriCuo.Text = Convert.ToString(Sol.Rows[0]["nMontoCuota"]);
                dtpFecPriCuo.SeleccionarFecha = Convert.ToDateTime(Sol.Rows[0]["dFechaProg"]);
                txtObservacion.Text = Convert.ToString(Sol.Rows[0]["tObservacion"]);
                idProducto = Convert.ToInt32(Sol.Rows[0]["idProducto"]);

                cboModDesemb1.Enabled = false;
                cboModDesemb1.SelectedValue = "1";

                DataTable dtGasto = new CRE.CapaNegocio.clsCNTipoGasto().CNDerechoDesembolso(CodigoSol);
                dtGasto.Columns["nMonAplica"].ReadOnly = false;

                //this.dtgGasto.DataSource = dtGasto;
                //this.dtgGasto.DataBind();
                
                //                DataTable dtCtaAporte = new clsCNTipoGasto().CNValidaCtaAporte(CodigoSol);
                Boolean lIndicaAporte = false;

                int nNumReg = dtGasto.Rows.Count;
                decimal nTotDes = Convert.ToDecimal(Sol.Rows[0]["nCapitalSolicitado"]);
                for (int i = 0; i < nNumReg; i++)
                {
                    if (Convert.ToInt32(dtGasto.Rows[i]["idTipoGasto"]) != 1)
                    {
                        nTotDes = nTotDes - Convert.ToDecimal(dtGasto.Rows[i]["nMonAplica"]);
                    }
                    if (Convert.ToInt32(dtGasto.Rows[i]["idTipoGasto"]) == 2 && Convert.ToDecimal(dtGasto.Rows[i]["nMonAplica"]) > 0)
                    {
                        lIndicaAporte = true;
                    }
                }
                txtTotEntrega.Text = nTotDes.ToString();
                //    if (lIndicaAporte && dtCtaAporte.Rows.Count==0)
                //    {
                //        script.Mensaje("Cliente no tiene cuenta de aportes debe APERTURAR CON MONTO CERO", "Desembolso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //        limpiar();
                //        return;
                //    }
            }
            else
            {
                script.Mensaje("Solicitud no tiene Plan de Pagos generado");
                limpiar();
            }
        }

        private void limpiar()
        {
            conBuscarCliente1.LimpiarControl();
            txtMonPriCuo.Text = "";
            this.txtMontoCapital.Text = "";
            txtNroCuotas.Text = "";
            txtTotEntrega.Text = "";
            dtgGasto.DataSource = null;
            txtObservacion.Text = "";
            //            txtCodigoCuenta.Text = "";
            this.BotonGrabar1.Enabled = true;
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
            DataTable dtDatosCuentaSolCliente = RetornaCuentaSolCliente.RetornaCuentaSolCliente(idCliente, "S", "[2]");
            if (dtDatosCuentaSolCliente.Rows.Count == 1)
            {
                pnlDesembolso.Visible = true;
                BotonGrabar1.Visible = true;
                BotonCancelar1.Visible = true;
                var nIdSolCta = Convert.ToInt32(dtDatosCuentaSolCliente.Rows[0][0]);
                hIdSolicitud.Value = nIdSolCta.ToString();
                BuscarSolicitud(nIdSolCta);
            }
            else
            {
                hIdSolicitud.Value = "";
                pnlDesembolso.Visible = false;
                BotonGrabar1.Visible = false;
                BotonCancelar1.Visible = true;
                script.Mensaje("No existe solicitud de crédito aprobada para desembolso");
            }
        }

        protected void BotonImprimir1_Click(object sender, EventArgs e)
        {
            imprimirReporte();

        }

        private void imprimirReporte()
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
            int nNumCredito = Convert.ToInt32(hIdCuenta.Value);
            List<ReportParameter> ListaParametros = new List<ReportParameter>();
            List<ReportDataSource> ListaDataSource = new List<ReportDataSource>();

            ListaDataSource.Add(new ReportDataSource("dtsAgencia", new RPT.CapaNegocio.clsRPTCNAgencia().CNDatoAgencia(objUsuario.nIdAgencia)));
            ListaDataSource.Add(new ReportDataSource("dtsPPG", new RPT.CapaNegocio.clsRPTCNPlanPagos().CNCronogramaCredito(nNumCredito)));
            ListaDataSource.Add(new ReportDataSource("dtsCuenta", new RPT.CapaNegocio.clsRPTCNCredito().CNDatosCuenta(nNumCredito)));
            ListaDataSource.Add(new ReportDataSource("dtsCliente", new RPT.CapaNegocio.clsRPTCNCliente().CNDireccion(nNumCredito)));


            Session["ListaParametros"] = ListaParametros;
            Session["ListaDataSource"] = ListaDataSource;
            Session["lModal"] = true;

            var cReporte = "rptCalendarioCreditoDiario.rdlc";

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../frmGenReporte.aspx?cNomReporte=" + cReporte + "');", true);
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

        private void cargarModalidadDesembolso()
        {
            CRE.CapaNegocio.clsCNModDesembolso ListaModaDese = new CRE.CapaNegocio.clsCNModDesembolso();

            DataTable tbModDes = ListaModaDese.ListaModDesem();
            this.cboModDesemb1.DataSource = tbModDes;
            this.cboModDesemb1.DataValueField = tbModDes.Columns[0].ToString();
            this.cboModDesemb1.DataTextField = tbModDes.Columns[1].ToString();
            cboModDesemb1.DataBind();
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

    }
}