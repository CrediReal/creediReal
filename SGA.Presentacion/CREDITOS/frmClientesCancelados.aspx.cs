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
    public partial class frmClientesCancelados : System.Web.UI.Page
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
                clsUsuario objUsuario;
                if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor"); }
                else
                {
                    objUsuario = (clsUsuario)Session["DatosUsuarioSession"];
                }
                if (IsPostBack) return;

                lblOpcion.Text = Session["cOpcion"].ToString().ToUpper() + ":";
                dtpFecFin.SeleccionarFecha = objUsuario.dFecSystem;
                dtpFecIni.SeleccionarFecha = objUsuario.dFecSystem;
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

            if (!Validar())
            {
                return;
            }

            List<ReportParameter> ListaParametros = new List<ReportParameter>();
            List<ReportDataSource> ListaDataSource = new List<ReportDataSource>();
            
            DateTime dFecIni = dtpFecIni.SeleccionarFecha;
            DateTime dFecFin = dtpFecFin.SeleccionarFecha;

            DataTable dtCreCancel = new RPT.CapaNegocio.clsRPTCNCredito().CNCancelados(objUsuario.nIdAgencia, dFecIni, dFecFin);

            ListaDataSource.Add(new ReportDataSource("dtsRutaLogo", new RPT.CapaNegocio.clsRPTCNAgencia().CNRutaLogo()));
            ListaDataSource.Add(new ReportDataSource("dtsCreCancelados", dtCreCancel));

            ListaParametros.Add(new ReportParameter("dFecIni", dtpFecIni.SeleccionarFecha.ToString("dd/MM/yyyy"), false));
            ListaParametros.Add(new ReportParameter("dFecFin", dtpFecFin.SeleccionarFecha.ToString("dd/MM/yyyy"), false));

            string reportpath = "RptCreditosCancelados.rdlc";
            Session["ListaParametros"] = ListaParametros;
            Session["ListaDataSource"] = ListaDataSource;
            Session["lModal"] = true;

            var cReporte = reportpath;

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../frmGenReporte.aspx?cNomReporte=" + cReporte + "');", true);

        }

        private bool Validar()
        {
            var lEstado = false;
            if (dtpFecIni.SeleccionarFecha > dtpFecFin.SeleccionarFecha)
            {
                script.Mensaje("La Fecha Final debe ser mayor o igual a la Fecha inicial");
                lEstado = false;
            }
            else
            {
                lEstado = true;
            }
            return lEstado;
        }
    }
}