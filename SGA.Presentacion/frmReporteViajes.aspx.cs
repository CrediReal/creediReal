using Microsoft.Reporting.WebForms;
using SGA.LogicaNegocio;
using SGA.Utilitarios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace SGA.Presentacion
{
    public partial class frmReporteViajes : System.Web.UI.Page
    {
        #region Variables Globales

        clsWebJScript script = new clsWebJScript();
        clsCNViaje cnviaje = new clsCNViaje();

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == true) return;
            //----------------- TITULO ------------------------>
            lblOpcion.Text = Session["cOpcion"].ToString().ToUpper() + ":";
            //-------------------------------------------------->
            //------------------Valida Session Vigente -------------------------------------->
            if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor"); }
            calInicio.SeleccionarFecha = new DateTime(DateTime.Now.Year,DateTime.Now.Month,1);
            calFin.SeleccionarFecha = DateTime.Now.AddMonths(1).AddDays(-1);
            //------------------------------------------------------------------------------>
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

                var dtRpt = cnviaje.RptViajes(dFecIni, dFecfin);

                ListaDataSource.Add(new ReportDataSource("dsViajes", dtRpt));

                Session["ListaParametros"] = ListaParametros;
                Session["ListaDataSource"] = ListaDataSource;
                Session["lModal"] = true;

                var cReporte = "rptViajes.rdlc";

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