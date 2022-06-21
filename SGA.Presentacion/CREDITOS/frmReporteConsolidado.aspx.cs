using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

namespace SGA.Presentacion.CREDITOS
{
    public partial class frmReporteConsolidado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["usuario"] != null)
            {
                Session["DatosUsuarioSession"] = new SGA.LogicaNegocio.clsCNUsuario().GetDatosUsuario(Request.QueryString["usuario"].ToString());
            }
            if (IsPostBack) return;
        }

        protected void btnResumen_Click(object sender, EventArgs e)
        {
            List<ReportParameter> ListaParametros = new List<ReportParameter>();
            List<ReportDataSource> ListaDataSource = new List<ReportDataSource>();

            DateTime dFecha = dtpFechaProceso.SeleccionarFecha;
            
            string reportpath = "";

                ListaDataSource.Add(new ReportDataSource("dsConsolidado", new SGA.LogicaNegocio.clsCNCredito().rptConsolidado(dFecha)));
                ListaParametros.Add(new ReportParameter("dFecha", dFecha.ToString("dd/MM/yyyy"), false));
            reportpath = "rptConsolidado.rdlc";
            

            Session["ListaParametros"] = ListaParametros;
            Session["ListaDataSource"] = ListaDataSource;
            Session["lModal"] = true;

            var cReporte = reportpath;

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../frmGenReporte.aspx?cNomReporte=" + cReporte + "');", true);

        }
    }
}