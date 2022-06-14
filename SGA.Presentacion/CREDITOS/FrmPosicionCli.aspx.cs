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
    public partial class FrmPosicionCli : System.Web.UI.Page
    {
        CRE.CapaNegocio.clsCNCredito Credito = new CRE.CapaNegocio.clsCNCredito();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["usuario"]!=null)
                {
                    Session["DatosUsuarioSession"] = new clsCNUsuario().GetDatosUsuario(Request.QueryString["usuario"].ToString());
                }
                if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor " + this.ToString()); }

                if (IsPostBack) return;
                hUsuario.Value = ((clsUsuario)Session["DatosUsuarioSession"]).cUsuario;
                lblOpcion.Text = Session["cOpcion"].ToString().ToUpper() + ":";

            }
            catch (Exception)
            {
                throw;
            }
        }

        private void cargarCreditos()
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
            else
            {
                var dtLisCredxCli = Credito.CNdtLisCrexCli(idCliente);
                this.dtgCreditos.DataSource = dtLisCredxCli;
                this.dtgCreditos.DataBind();
            }            
        }

        protected void BotonConsultar1_Click(object sender, EventArgs e)
        {
            cargarCreditos();
        }

        protected void CheckBoxBase1_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void BotonImprimir1_Click(object sender, EventArgs e)
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

            string param = ((LinkButton)sender).CommandArgument;
            string[] array_param = param.Split(';');


            int idCuenta = Convert.ToInt32(array_param[0].ToString());
            int idSolicitud = Convert.ToInt32(array_param[1].ToString());

            //int idCuenta = Convert.ToInt32(((LinkButton)sender).CommandArgument);


            List<ReportParameter> ListaParametros = new List<ReportParameter>();
            List<ReportDataSource> ListaDataSource = new List<ReportDataSource>();            

            //int idCuenta = Convert.ToInt32(((LinkButton)sender).CommandArgument);   

            ListaDataSource.Add(new ReportDataSource("dtsPPG", new RPT.CapaNegocio.clsRPTCNPlanPagos().CNCronogramaPagos(idCuenta,idSolicitud)));
            ListaDataSource.Add(new ReportDataSource("dtsAgencia", new RPT.CapaNegocio.clsRPTCNAgencia().CNAgenciaUsuario(objUsuario.nIdAgencia, objUsuario.idUsuario)));
            
            Session["ListaParametros"] = ListaParametros;
            Session["ListaDataSource"] = ListaDataSource;
            Session["lModal"] = true;

            var cReporte = "rptPlanPagoPosInt.rdlc";

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../frmGenReporte.aspx?cNomReporte=" + cReporte + "');", true);

        }

        protected void btnKardex_Click(object sender, EventArgs e)
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
            int idCuenta = Convert.ToInt32(((LinkButton)sender).CommandArgument);


            List<ReportParameter> ListaParametros = new List<ReportParameter>();
            List<ReportDataSource> ListaDataSource = new List<ReportDataSource>();

            ListaDataSource.Add(new ReportDataSource("dtsKardexPagoCre", new RPT.CapaNegocio.clsRPTCNPlanPagos().CNKardexPagos(idCuenta)));
            ListaDataSource.Add(new ReportDataSource("dtsAgencia", new RPT.CapaNegocio.clsRPTCNAgencia().CNAgenciaUsuario(objUsuario.nIdAgencia, objUsuario.idUsuario)));


            Session["ListaParametros"] = ListaParametros;
            Session["ListaDataSource"] = ListaDataSource;
            Session["lModal"] = true;

            var cReporte = "rptKardexPagoCre.rdlc";

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../frmGenReporte.aspx?cNomReporte=" + cReporte + "');", true);

        }
    }
}