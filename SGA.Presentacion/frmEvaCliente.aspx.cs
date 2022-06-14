using SGA.ENTIDADES;
using SGA.LogicaNegocio;
using System;
using System.Collections;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

namespace SGA.Presentacion
{
    public partial class frmEvaCliente : System.Web.UI.Page
    {
        clsCNEvaluacion cneva = new clsCNEvaluacion();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["usuario"] != null)
            {
                Session["DatosUsuarioSession"] = new clsCNUsuario().GetDatosUsuario(Request.QueryString["usuario"].ToString());
            }
            if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor " + this.ToString()); }

            if (IsPostBack) return;
            hUsuario.Value = ((clsUsuario)Session["DatosUsuarioSession"]).cUsuario;
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
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
            hidCli.Value = idCliente.ToString();
            var dtEva = cneva.ListarEvaluacionCreditoCliente(idCliente);
            GridViewUser.DataSource = dtEva;
            GridViewUser.DataBind();
        }
        
        protected void lnkAccion_Click(object sender, EventArgs e)
        {
            var dtEva = cneva.ListarEvaluacionCreditoCliente(Convert.ToInt32(hidCli.Value));

            

            hidSolicitud.Value=((LinkButton)sender).CommandArgument;
            var drEva = dtEva.AsEnumerable().Where(x => x["idSolicitud"].ToString() == hidSolicitud.Value).FirstOrDefault();
            if (Convert.ToBoolean(drEva["lTieneEvaluacion"]))
            {
                verReporte(Convert.ToInt32(drEva["idEvaluacion"]));
            }
            else
            {
                Response.Redirect("CREDITOS/frmEvaluacionConsumo.aspx?usuario=" + hUsuario.Value + "&idSolicitud=" + hidSolicitud.Value, false);
            }

        }

        protected void lnkAccionPyme_Click(object sender, EventArgs e)
        {
            var dtEva = cneva.ListarEvaluacionCreditoCliente(Convert.ToInt32(hidCli.Value));

            hidSolicitud.Value = ((LinkButton)sender).CommandArgument;
            var drEva = dtEva.AsEnumerable().Where(x => x["idSolicitud"].ToString() == hidSolicitud.Value).FirstOrDefault();
            if (Convert.ToBoolean(drEva["lTieneEvaluacion"]))
            {
                verReporte(Convert.ToInt32(drEva["idEvaluacion"]));
            }
            else
            {
                Response.Redirect("CREDITOS/frmEvaluacion.aspx?usuario=" + hUsuario.Value + "&idSolicitud=" + hidSolicitud.Value, false);
            }
           
        }

        protected void btnReporteHori_Click(object sender, EventArgs e)
        {

        }

        private void verReporte(int idEvaluacion)
        {
            List<ReportParameter> ListaParametros = new List<ReportParameter>();
            List<ReportDataSource> ListaDataSource = new List<ReportDataSource>();
            
            ListaDataSource.Add(new ReportDataSource("dsEvaluacion", cneva.ListarEvaluacionCreditoidEvaluacion(idEvaluacion)));
            ListaDataSource.Add(new ReportDataSource("dsEvaConsumo", cneva.ListarEvaluacionConsumoidEvaluacion(idEvaluacion)));
            ListaDataSource.Add(new ReportDataSource("dsEvaConCreComercial", cneva.ListarEvaluacionConsumoCreditoComercialidEvaluacion(idEvaluacion)));
            ListaDataSource.Add(new ReportDataSource("dsEvaConCreDirecto", cneva.ListarEvaluacionConsumoCreditoDirectoid(idEvaluacion)));
            ListaDataSource.Add(new ReportDataSource("dsEvaConCreIndirecto", cneva.ListarEvaluacionConsumoCreditoIndirectoidEvaluacion(idEvaluacion)));

            ListaParametros.Add(new ReportParameter("idEvaluacion", idEvaluacion.ToString(), false));

            string reportpath = "rptEvaluacionConsumo.rdlc";

            Session["ListaParametros"] = ListaParametros;
            Session["ListaDataSource"] = ListaDataSource;
            Session["lModal"] = true;

            var cReporte = reportpath;

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../frmGenReporte.aspx?cNomReporte=" + cReporte + "');", true);

        }


    }
}