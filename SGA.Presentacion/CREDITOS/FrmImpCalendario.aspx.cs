using Microsoft.Reporting.WebForms;
using SGA.ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGA.LogicaNegocio;

namespace SGA.Presentacion.CREDITOS
{
    public partial class FrmImpCalendario : System.Web.UI.Page
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
            int nNumCredito = Convert.ToInt32(txtCuenta.Text);
            var dtPlanpago = new RPT.CapaNegocio.clsRPTCNPlanPagos().CNCronogramaCredito(nNumCredito);

            if (dtPlanpago.Rows.Count==0)
            {
                script.Mensaje("No existe cronograma para la cuenta ingresada");
                return;
            }

            List<ReportParameter> ListaParametros = new List<ReportParameter>();
            List<ReportDataSource> ListaDataSource = new List<ReportDataSource>();

            ListaDataSource.Add(new ReportDataSource("dtsAgencia", new RPT.CapaNegocio.clsRPTCNAgencia().CNDatoAgencia(objUsuario.nIdAgencia)));
            ListaDataSource.Add(new ReportDataSource("dtsPPG", dtPlanpago));
            ListaDataSource.Add(new ReportDataSource("dtsCuenta", new RPT.CapaNegocio.clsRPTCNCredito().CNDatosCuenta(nNumCredito)));
            ListaDataSource.Add(new ReportDataSource("dtsCliente", new RPT.CapaNegocio.clsRPTCNCliente().CNDireccion(nNumCredito)));
            
            Session["ListaParametros"] = ListaParametros;
            Session["ListaDataSource"] = ListaDataSource;
            Session["lModal"] = true;

            var cReporte = "rptCalendarioCreditoDiario.rdlc";

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../frmGenReporte.aspx?cNomReporte=" + cReporte + "');", true);
        }
    }

   
}