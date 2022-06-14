using Microsoft.Reporting.WebForms;
using SGA.ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGA.Presentacion.CREDITOS
{
    public partial class frmDetalleOperaciones : System.Web.UI.Page
    {
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
                cargarAgencia();
                dtpFecFin.SeleccionarFecha = objUsuario.dFecSystem;
                dtpFecIni.SeleccionarFecha = objUsuario.dFecSystem;
                cboAgencia1.SelectedValue = objUsuario.nIdAgencia.ToString();
                if (objUsuario.idUsuario.In(1, 2, 3))
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

        protected void btnDetalle_Click(object sender, EventArgs e)
        {
            List<ReportParameter> ListaParametros = new List<ReportParameter>();
            List<ReportDataSource> ListaDataSource = new List<ReportDataSource>();

            var pdFecIni = this.dtpFecIni.SeleccionarFecha.Date;
            var pdFecFin = this.dtpFecFin.SeleccionarFecha.Date;
            var idAgencia = Convert.ToInt32(cboAgencia1.SelectedValue);

            ListaDataSource.Add(new ReportDataSource("dtsRutaLogo", new RPT.CapaNegocio.clsRPTCNAgencia().CNRutaLogo()));
            ListaDataSource.Add(new ReportDataSource("dtsCobranza", new RPT.CapaNegocio.clsRPTCNCredito().CNDetalleCobAse(pdFecIni, pdFecFin, idAgencia)));

            var reportpath = "RptDetalleOperaciones.rdlc";

            Session["ListaParametros"] = ListaParametros;
            Session["ListaDataSource"] = ListaDataSource;
            Session["lModal"] = true;

            var cReporte = reportpath;

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../frmGenReporte.aspx?cNomReporte=" + cReporte + "');", true);

        }

        protected void btnAnalista_Click(object sender, EventArgs e)
        {
            List<ReportParameter> ListaParametros = new List<ReportParameter>();
            List<ReportDataSource> ListaDataSource = new List<ReportDataSource>();

            var pdFecIni = this.dtpFecIni.SeleccionarFecha.Date;
            var pdFecFin = this.dtpFecFin.SeleccionarFecha.Date;
            var idAgencia = Convert.ToInt32(cboAgencia1.SelectedValue);


            ListaDataSource.Add(new ReportDataSource("dtsRutaLogo", new RPT.CapaNegocio.clsRPTCNAgencia().CNRutaLogo()));
            ListaDataSource.Add(new ReportDataSource("dtsRecAsesor", new RPT.CapaNegocio.clsRPTCNCredito().CNResumenCobAse(pdFecIni, pdFecFin, idAgencia)));

           var reportpath = "RptDetalleOperacionesAsesor.rdlc";

            Session["ListaParametros"] = ListaParametros;
            Session["ListaDataSource"] = ListaDataSource;
            Session["lModal"] = true;

            var cReporte = reportpath;

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../frmGenReporte.aspx?cNomReporte=" + cReporte + "');", true);

        }

        protected void btnResumen_Click(object sender, EventArgs e)
        {
            List<ReportParameter> ListaParametros = new List<ReportParameter>();
            List<ReportDataSource> ListaDataSource = new List<ReportDataSource>();

            var pdFecIni = this.dtpFecIni.SeleccionarFecha.Date;
            var pdFecFin = this.dtpFecFin.SeleccionarFecha.Date;
            var idAgencia = Convert.ToInt32(cboAgencia1.SelectedValue);



            ListaDataSource.Add(new ReportDataSource("dtsRutaLogo", new RPT.CapaNegocio.clsRPTCNAgencia().CNRutaLogo()));
            ListaDataSource.Add(new ReportDataSource("dtsOperaciones", new RPT.CapaNegocio.clsRPTCNCredito().CNResuCob(pdFecIni, pdFecFin, idAgencia)));

           var reportpath = "RptResumenOperaciones.rdlc";

           Session["ListaParametros"] = ListaParametros;
           Session["ListaDataSource"] = ListaDataSource;
           Session["lModal"] = true;

           var cReporte = reportpath;

           ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../frmGenReporte.aspx?cNomReporte=" + cReporte + "');", true);

        }

        protected void btnConsolidado_Click(object sender, EventArgs e)
        {
            List<ReportParameter> ListaParametros = new List<ReportParameter>();
            List<ReportDataSource> ListaDataSource = new List<ReportDataSource>();

            var pdFecIni = this.dtpFecIni.SeleccionarFecha.Date;
            var pdFecFin = this.dtpFecFin.SeleccionarFecha.Date;
            var idAgencia = Convert.ToInt32(cboAgencia1.SelectedValue);


            ListaDataSource.Add(new ReportDataSource("dtsRutaLogo", new RPT.CapaNegocio.clsRPTCNAgencia().CNRutaLogo()));
            ListaDataSource.Add(new ReportDataSource("dtsRecAsesor", new RPT.CapaNegocio.clsRPTCNCredito().CNResumenCobAse(pdFecIni, pdFecFin, idAgencia)));

            //var reportpath = "RptConsolidadoOperacionesAsesor.rdlc";
            //var reportpath = "RptDetalleOperacionesAsesor.rdlc";
            var reportpath = "RptConsoOperacionesAsesor.rdlc";

            Session["ListaParametros"] = ListaParametros;
            Session["ListaDataSource"] = ListaDataSource;
            Session["lModal"] = true;

            var cReporte = reportpath;

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../frmGenReporte.aspx?cNomReporte=" + cReporte + "');", true);
        }
    }
}