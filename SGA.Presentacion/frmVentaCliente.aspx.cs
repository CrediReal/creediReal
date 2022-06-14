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
    public partial class frmVentaCliente : PageBase
    {
        #region Variables Globales

        clsWebJScript script = new clsWebJScript();
        clsCNVenta CNVenta = new clsCNVenta();

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;
            //----------------- TITULO ------------------------>
            lblOpcion.Text = Session["cOpcion"].ToString().ToUpper() + ":";
        }


        protected void BotonImprimir1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!validar()) return;

                List<ReportParameter> ListaParametros = new List<ReportParameter>();
                List<ReportDataSource> ListaDataSource = new List<ReportDataSource>();

                int idCli = ConBusCli.IdCli;
                var dtRpt = CNVenta.RptVentasCliente(idCli);

                ListaDataSource.Add(new ReportDataSource("dsData", dtRpt));

                Session["ListaParametros"] = ListaParametros;
                Session["ListaDataSource"] = ListaDataSource;
                Session["lModal"] = true;

                var cReporte = "RptVentaCliente.rdlc";

                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('frmGenReporte.aspx?cNomReporte=" + cReporte + "');", true);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        private bool validar()
        {
            if (ConBusCli.IdCli == 0)
            {
                script.Mensaje("Seleccione el cliente para la busqueda.");
                return false;
            }

            return true;
        }
    }
}