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
    public partial class frmSaldoAnalistas : System.Web.UI.Page
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
                hUsuario.Value = ((clsUsuario)Session["DatosUsuarioSession"]).cUsuario;
                lblOpcion.Text = Session["cOpcion"].ToString().ToUpper() + ":";
                dtpFechaProceso.SeleccionarFecha = objUsuario.dFecSystem;
                cargarAgencia();

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

        protected void btnResumen_Click(object sender, EventArgs e)
        {
            List<ReportParameter> ListaParametros = new List<ReportParameter>();
            List<ReportDataSource> ListaDataSource = new List<ReportDataSource>();

            DateTime dFecha = dtpFechaProceso.SeleccionarFecha;
            Int32 idAgePar = Convert.ToInt32(cboAgencia1.SelectedValue);
            ListaDataSource.Add(new ReportDataSource("dtsRutaRep", new RPT.CapaNegocio.clsRPTCNAgencia().CNRutaLogo()));

            string reportpath = "";
            if (idAgePar > 0)
            {
                ListaDataSource.Add(new ReportDataSource("dtsSaldoDesembolso", new RPT.CapaNegocio.clsRPTCNCredito().CNSaldoCartera(dFecha, idAgePar)));
                reportpath = "RptSaldoCarteraAnalista.rdlc";
            }
            else
            {
                ListaDataSource.Add(new ReportDataSource("dtsSaldoDesembolso", new RPT.CapaNegocio.clsRPTCNCredito().CNSaldoCartera(dFecha)));
                
                reportpath = "RptSaldoCarteraOficina.rdlc";
            }

            Session["ListaParametros"] = ListaParametros;
            Session["ListaDataSource"] = ListaDataSource;
            Session["lModal"] = true;

            var cReporte = reportpath;

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../frmGenReporte.aspx?cNomReporte=" + cReporte + "');", true);

        }

        protected void btnDetalle_Click(object sender, EventArgs e)
        {
            List<ReportParameter> ListaParametros = new List<ReportParameter>();
            List<ReportDataSource> ListaDataSource = new List<ReportDataSource>();


            DateTime dFecha = dtpFechaProceso.SeleccionarFecha;
            Int32 idAgePar = Convert.ToInt32(cboAgencia1.SelectedValue);
            ListaDataSource.Add(new ReportDataSource("dtsRutaLogo", new RPT.CapaNegocio.clsRPTCNAgencia().CNRutaLogo()));

            string reportpath = "";
            ListaDataSource.Add(new ReportDataSource("dtsSaldo", new RPT.CapaNegocio.clsRPTCNCredito().CNSaldoDetalle(idAgePar, dFecha)));
            reportpath = "RptDetalleSaldo.rdlc";

            Session["ListaParametros"] = ListaParametros;
            Session["ListaDataSource"] = ListaDataSource;
            Session["lModal"] = true;

            var cReporte = reportpath;

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../frmGenReporte.aspx?cNomReporte=" + cReporte + "');", true);

        }
    }
}