using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using SGA.LogicaNegocio;
using SGA.Utilitarios;

namespace SGA.Presentacion
{
    public partial class frmReporteVisitasFecha : PageBase
    {
        #region Variables Globales

        clsWebJScript script = new clsWebJScript();
        clsCNHojaRuta CNHojaRuta = new clsCNHojaRuta();

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == true) return;
            //----------------- TITULO ------------------------>
            lblOpcion.Text = Session["cOpcion"].ToString().ToUpper() + ":";
            
            calInicio.SeleccionarFecha = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            calFin.SeleccionarFecha = DateTime.Now.AddMonths(1).AddDays(-1);
        }


        protected void BotonImprimir1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!validar()) return;

                List<ReportParameter> ListaParametros = new List<ReportParameter>();
                List<ReportDataSource> ListaDataSource = new List<ReportDataSource>();

                var dFecIni = calInicio.SeleccionarFecha;
                var dFecfin = calFin.SeleccionarFecha;

                var dtRpt = CNHojaRuta.RptVisitasFecha(dFecIni, dFecfin);

                ListaDataSource.Add(new ReportDataSource("dsData", dtRpt));
                ListaParametros.Add(new ReportParameter("dFecIni",dFecIni.Date.ToString("dd/MM/yyyy")));
                ListaParametros.Add(new ReportParameter("dFecFin", dFecfin.Date.ToString("dd/MM/yyyy")));

                Session["ListaParametros"] = ListaParametros;
                Session["ListaDataSource"] = ListaDataSource;
                Session["lModal"] = true;

                var cReporte = "RptVisitasFecha.rdlc";

                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('frmGenReporte.aspx?cNomReporte=" + cReporte + "');", true);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        private bool validar()
        {
            bool lVal = false;

            if ((calInicio.SeleccionarFecha - calFin.SeleccionarFecha).Days > 0)
            {
                script.Mensaje("La fecha de inicio debe de ser menor a la fecha final");
                return lVal;
            }

            lVal = true;
            return lVal;
        }
    }
}