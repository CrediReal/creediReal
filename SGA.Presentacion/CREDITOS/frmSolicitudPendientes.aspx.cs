using Microsoft.Reporting.WebForms;
using SGA.ENTIDADES;
using SGA.Utilitarios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGA.Presentacion.CREDITOS
{
    public partial class frmSolicitudPendientes : System.Web.UI.Page
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
                if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor"); }

                if (IsPostBack) return;

                lblOpcion.Text = Session["cOpcion"].ToString().ToUpper() + ":";

            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void BotonImprimir1_Click(object sender, EventArgs e)
        {
            clsUsuario objUsuario;
            if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor"); }
            else
            {
                objUsuario = (clsUsuario)Session["DatosUsuarioSession"];
            }

            int idAgencia = objUsuario.nIdAgencia;
            DataTable dtRutaLogo = new RPT.CapaNegocio.clsRPTCNAgencia().CNRutaLogo();
            DataTable dtSolici = new RPT.CapaNegocio.clsRPTCNCredito().CNSolicitudesPendientes(idAgencia);

            if (dtSolici.Rows.Count > 0)
            {
                List<ReportParameter> ListaParametros = new List<ReportParameter>();
                List<ReportDataSource> ListaDataSource = new List<ReportDataSource>();
                
                ListaDataSource.Add(new ReportDataSource("dtsRutaLogo", dtRutaLogo));
                ListaDataSource.Add(new ReportDataSource("dtsSolPendiente", dtSolici));

                string reportpath = "RptSolicitudPendiente.rdlc";

                Session["ListaParametros"] = ListaParametros;
                Session["ListaDataSource"] = ListaDataSource;
                Session["lModal"] = true;

                var cReporte = reportpath;

                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../frmGenReporte.aspx?cNomReporte=" + cReporte + "');", true);


            }
            else
            {
                script.Mensaje("No existen solicitudes pendientes");
            }
        }
    }
}