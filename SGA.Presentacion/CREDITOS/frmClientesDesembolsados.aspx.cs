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

namespace SGA.Presentacion.CREDITOS
{
    public partial class frmClientesDesembolsados : System.Web.UI.Page
    {
        clsWebJScript script = new clsWebJScript();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["usuario"] != null)
                {
                    Session["DatosUsuarioSession"] = new SGA.LogicaNegocio.clsCNUsuario().GetDatosUsuario(Request.QueryString["usuario"].ToString());
                }
                clsUsuario objUsuario;
                if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor"); }
                else
                {
                    objUsuario = (clsUsuario)Session["DatosUsuarioSession"];
                }
                if (IsPostBack) return;

                lblOpcion.Text = Session["cOpcion"].ToString().ToUpper() + ":";
                cargarTipoCliente();
                cargarAgencia();
                dtpFecFin.SeleccionarFecha = objUsuario.dFecSystem;
                dtpFecIni.SeleccionarFecha = objUsuario.dFecSystem;
                cboAgencia1.SelectedValue = objUsuario.nIdAgencia.ToString();
                if (objUsuario.idUsuario.In(1,2,3))
                {
                    cboAgencia1.Enabled = true;
                }
                else
                {
                    cboAgencia1.Enabled = false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void cargarAgencia()
        {
            GEN.CapaNegocio.clsCNAgencia Agencia = new GEN.CapaNegocio.clsCNAgencia();
            var dt = Agencia.LisAgen();
            this.cboAgencia1.DataSource = dt;
            this.cboAgencia1.DataValueField = dt.Columns[0].ToString();
            this.cboAgencia1.DataTextField = dt.Columns[1].ToString();
            cboAgencia1.DataBind();
            this.cboAgencia1.SelectedValue = "1";

        }

        private void cargarTipoCliente()
        {
            GEN.CapaNegocio.clsCNTipoCliente ListaTipo = new GEN.CapaNegocio.clsCNTipoCliente();
            DataTable dt = ListaTipo.Lista();
            this.cboTipoCliente.DataSource = dt;
            this.cboTipoCliente.DataValueField = dt.Columns[0].ToString();
            this.cboTipoCliente.DataTextField = dt.Columns[1].ToString();
            this.cboTipoCliente.DataBind();
        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            if (!Validar())
            {
                return;
            }

            List<ReportParameter> ListaParametros = new List<ReportParameter>();
            List<ReportDataSource> ListaDataSource = new List<ReportDataSource>();


            var idAgencia = Convert.ToInt32(cboAgencia1.SelectedValue);
            int idTipCli = Convert.ToInt32(cboTipoCliente.SelectedValue);
            DateTime dFecIni = dtpFecIni.SeleccionarFecha;
            DateTime dFecFin = dtpFecFin.SeleccionarFecha;
            ListaDataSource.Add(new ReportDataSource("dsRutaLogo", new RPT.CapaNegocio.clsRPTCNAgencia().CNRutaLogo()));
            ListaDataSource.Add(new ReportDataSource("dtsLisDesembolso", new RPT.CapaNegocio.clsRPTCNCredito().CNDesembolso(idAgencia, idTipCli, dFecIni, dFecFin)));

            ListaParametros.Add(new ReportParameter("dFecIni", dtpFecIni.SeleccionarFecha.ToString("dd/MM/yyyy"), false));
            ListaParametros.Add(new ReportParameter("dFecFin", dtpFecFin.SeleccionarFecha.ToString("dd/MM/yyyy"), false));
            ListaParametros.Add(new ReportParameter("cTipoCli", cboTipoCliente.Text, false));

            string reportpath = "rptClientesDesembolsados.rdlc";
            Session["ListaParametros"] = ListaParametros;
            Session["ListaDataSource"] = ListaDataSource;
            Session["lModal"] = true;

            var cReporte = reportpath;

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../frmGenReporte.aspx?cNomReporte=" + cReporte + "');", true);

        }

        protected void btnDesembolso_Click(object sender, EventArgs e)
        {
            if (!Validar())
            {
                return;
            }
            List<ReportParameter> ListaParametros = new List<ReportParameter>();
            List<ReportDataSource> ListaDataSource = new List<ReportDataSource>();

            
            var idAgencia = Convert.ToInt32(cboAgencia1.SelectedValue);
            int idTipCli = Convert.ToInt32(cboTipoCliente.SelectedValue);
            DateTime dFecIni = dtpFecIni.SeleccionarFecha;
            DateTime dFecFin = dtpFecFin.SeleccionarFecha;
            ListaDataSource.Add(new ReportDataSource("dtsRutaLogo", new RPT.CapaNegocio.clsRPTCNAgencia().CNRutaLogo()));
            ListaDataSource.Add(new ReportDataSource("dtsDesembolso", new RPT.CapaNegocio.clsRPTCNCredito().CNTotalDesembolso(dFecIni, dFecFin, idAgencia)));

            ListaParametros.Add(new ReportParameter("dFecIni", dFecIni.ToString("dd/MM/yyyy"), false));
            ListaParametros.Add(new ReportParameter("dFecFin", dFecFin.ToString("dd/MM/yyyy"), false));
            string reportpath = "rptDesembolso.rdlc";

            Session["ListaParametros"] = ListaParametros;
            Session["ListaDataSource"] = ListaDataSource;
            Session["lModal"] = true;

            var cReporte = reportpath;

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../frmGenReporte.aspx?cNomReporte=" + cReporte + "');", true);

        }

        protected void btnImpResumen_Click(object sender, EventArgs e)
        {
            if (!Validar())
            {
                return;
            }
            List<ReportParameter> ListaParametros = new List<ReportParameter>();
            List<ReportDataSource> ListaDataSource = new List<ReportDataSource>();

            int idTipCli = Convert.ToInt32(cboTipoCliente.SelectedValue);
            DateTime dFecIni = dtpFecIni.SeleccionarFecha;
            DateTime dFecFin = dtpFecFin.SeleccionarFecha;
            var idAgencia = Convert.ToInt32(cboAgencia1.SelectedValue);

            ListaDataSource.Add(new ReportDataSource("dtsRutaLogo", new RPT.CapaNegocio.clsRPTCNAgencia().CNRutaLogo()));


            string reportpath = "";
            if (idAgencia == 0)
            {
                reportpath = "rptDesembolsoResumen.rdlc";
                ListaDataSource.Add(new ReportDataSource("dtsDesembolso", new RPT.CapaNegocio.clsRPTCNCredito().CNConsolidadoDesembolso(dFecIni, dFecFin)));
            }
            else
            {
                reportpath = "rptDesembolsoResumenAge.rdlc";
                ListaDataSource.Add(new ReportDataSource("dtsDesembolso", new RPT.CapaNegocio.clsRPTCNCredito().CNConsolidadoDesembolso(dFecIni, dFecFin, idAgencia)));
                ListaDataSource.Add(new ReportDataSource("dtsAgencia", new RPT.CapaNegocio.clsRPTCNAgencia().CNDatoAgencia(idAgencia)));
            }

            ListaParametros.Add(new ReportParameter("dFecIni", dFecIni.ToString(), false));
            ListaParametros.Add(new ReportParameter("dFecFin", dFecFin.ToString(), false));
            
            Session["ListaParametros"] = ListaParametros;
            Session["ListaDataSource"] = ListaDataSource;
            Session["lModal"] = true;

            var cReporte = reportpath;

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../frmGenReporte.aspx?cNomReporte=" + cReporte + "');", true);

        }

        private bool Validar()
        {
            var lEstado = false;
            if (dtpFecIni.SeleccionarFecha > dtpFecFin.SeleccionarFecha)
            {
                script.Mensaje("La Fecha Final debe ser mayor o igual a la Fecha inicial");
                lEstado = false;
            }
            else
            {
                lEstado = true;
            }
            return lEstado;
        }
    }
}