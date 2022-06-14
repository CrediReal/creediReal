using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using SGA.ENTIDADES;
using SGA.LogicaNegocio;
using SGA.Utilitarios;

namespace SGA.Presentacion.CREDITOS
{
    public partial class frmPropuesta : System.Web.UI.Page
    {
        clsWebJScript script = new clsWebJScript();
        DataTable dtSolicitud = new DataTable("dtSolicitud");
        clsCNEvaluacion cnevaluacion = new clsCNEvaluacion();
        int IdSolicitud = 0;

        public frmPropuesta()
        {

        }


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["usuario"] != null)
                {
                    Session["DatosUsuarioSession"] = new clsCNUsuario().GetDatosUsuario(Request.QueryString["usuario"].ToString());
                }
                if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor " + this.ToString()); }

                if (IsPostBack) return;
                hUsuario.Value = ((clsUsuario)Session["DatosUsuarioSession"]).cUsuario;

                if (Request.QueryString["IdSolicitud"] != null)
                {
                    cargarPropuesta2(Convert.ToInt32(Request.QueryString["IdSolicitud"].ToString()));
                }
                

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void BotonConsultar1_Click(object sender, EventArgs e)
        {
            limpiarControles();
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
            GEN.CapaNegocio.clsCNRetornsCuentaSolCliente RetornaCuentaSolCliente = new GEN.CapaNegocio.clsCNRetornsCuentaSolCliente();
            DataTable dtDatosCuentaSolCliente = RetornaCuentaSolCliente.RetornaCuentaSolCliente(idCliente, "S", "[1]");
            if (dtDatosCuentaSolCliente.Rows.Count == 1)
            {
                divRegistro.Visible = true;
                cargarPropuesta(dtDatosCuentaSolCliente);
                
            }
            else
            {
                script.Mensaje("No existe solicitud de crédito en estado solicitado");
            }
        }
        private void cargarPropuesta(DataTable dtSolicitud)
        {

            var idSolicitud = Convert.ToInt32(dtSolicitud.Rows[0][0]);
            hidSolicitud.Value = idSolicitud.ToString();
            GEN.CapaNegocio.clsCNBuscarCli objBusCli = new GEN.CapaNegocio.clsCNBuscarCli();
            DataTable nidCli = objBusCli.DatosClientexNumSol(idSolicitud);
            var dtPropuesta = cnevaluacion.ListarPropuestaCreditoidSolicitud(idSolicitud);
            if (dtPropuesta.Rows.Count>0)
            {
                txtConclusion.Text = dtPropuesta.Rows[0]["cConclusion"].ToString();
                txtEntorno.Text = dtPropuesta.Rows[0]["cEntornoFamiliar"].ToString();
                txtEvaAnalisisFinanciero.Text = dtPropuesta.Rows[0]["cEvaAnalisisFinanciero"].ToString();
                txtExperienciaCrediticia.Text = dtPropuesta.Rows[0]["cExperienciaCrediticia"].ToString();
                txtGarantia.Text = dtPropuesta.Rows[0]["cGarantia"].ToString();
                txtGiroUbicacion.Text = dtPropuesta.Rows[0]["cGiroUbicacion"].ToString();
                txtProveedores.Text = dtPropuesta.Rows[0]["cProveedores"].ToString();
                txtReferencia.Text = dtPropuesta.Rows[0]["cReferencia"].ToString();
                hidPropuesta.Value = dtPropuesta.Rows[0]["idPropuesta"].ToString();
                btnImprimir.Visible = true;
            }            
        }

        private void cargarPropuesta2(int idSolicitud)
        {
            hidSolicitud.Value = idSolicitud.ToString();
            GEN.CapaNegocio.clsCNBuscarCli objBusCli = new GEN.CapaNegocio.clsCNBuscarCli();
            DataTable nidCli = objBusCli.DatosClientexNumSol(idSolicitud);
            var dtPropuesta = cnevaluacion.ListarPropuestaCreditoidSolicitud(idSolicitud);
            if (dtPropuesta.Rows.Count > 0)
            {
                txtConclusion.Text = dtPropuesta.Rows[0]["cConclusion"].ToString();
                txtEntorno.Text = dtPropuesta.Rows[0]["cEntornoFamiliar"].ToString();
                txtEvaAnalisisFinanciero.Text = dtPropuesta.Rows[0]["cEvaAnalisisFinanciero"].ToString();
                txtExperienciaCrediticia.Text = dtPropuesta.Rows[0]["cExperienciaCrediticia"].ToString();
                txtGarantia.Text = dtPropuesta.Rows[0]["cGarantia"].ToString();
                txtGiroUbicacion.Text = dtPropuesta.Rows[0]["cGiroUbicacion"].ToString();
                txtProveedores.Text = dtPropuesta.Rows[0]["cProveedores"].ToString();
                txtReferencia.Text = dtPropuesta.Rows[0]["cReferencia"].ToString();
                hidPropuesta.Value = dtPropuesta.Rows[0]["idPropuesta"].ToString();
                btnImprimir.Visible = true;
                btnGuardar.Visible = false;
                btnCancelar.Visible = false;
                divRegistro.Visible = true;
            }
            else
            {
                script.Mensaje("La solicitud Seleccionada no cuenta con Propuesta");
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
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

            int idSolicitud = Convert.ToInt32(hidSolicitud.Value);
            int idCli = Convert.ToInt32(hidCli.Value);
            string cEntornoFamiliar = txtEntorno.Text.Trim();
            string cGiroUbicacion = txtGiroUbicacion.Text.Trim();
            string cExperienciaCrediticia = txtExperienciaCrediticia.Text.Trim();
            string cEvaAnalisisFinanciero = txtEvaAnalisisFinanciero.Text.Trim();
            string cGarantia =txtGarantia.Text.Trim() ;
            string cConclusion = txtConclusion.Text.Trim();
            string cReferencia = txtReferencia.Text.Trim();
            string cProveedores = txtProveedores.Text.Trim();
            DateTime dFechaReg = DateTime.Now;
            int idUsuReg = objUsuario.idUsuario;
            bool lVigente =true;
            if (hidPropuesta.Value == "0")
            {
               var result=  cnevaluacion.InsertarPropuestaCredito(idSolicitud, idCli, cEntornoFamiliar, cGiroUbicacion, cExperienciaCrediticia,
                    cEvaAnalisisFinanciero, cGarantia, cConclusion, cReferencia, cProveedores, dFechaReg, idUsuReg, lVigente);
               script.Mensaje(result.Rows[0]["cMensaje"].ToString());
               hidPropuesta.Value = result.Rows[0]["idPropuesta"].ToString();
               btnImprimir.Visible = true;
            }
            else
            {
               var result= cnevaluacion.ActualizarPropuestaCredito(Convert.ToInt32(hidPropuesta.Value), idSolicitud, idCli, cEntornoFamiliar,
                    cGiroUbicacion, cExperienciaCrediticia, cEvaAnalisisFinanciero, cGarantia, cConclusion, cReferencia, cProveedores,
                    dFechaReg, idUsuReg, lVigente);
               script.Mensaje(result.Rows[0]["cMensaje"].ToString());
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {

            divRegistro.Visible = true;
            limpiarControles();
        }

        private void limpiarControles()
        {
            txtConclusion.Text = "";
            txtEntorno.Text = "";
            txtEvaAnalisisFinanciero.Text = "";
            txtExperienciaCrediticia.Text = "";
            txtGarantia.Text = "";
            txtGiroUbicacion.Text = "";
            txtProveedores.Text = "";
            txtReferencia.Text = ""; 
            hidPropuesta.Value = "0";
            hidSolicitud.Value = "0";
            hidCli.Value = "0";
            btnImprimir.Visible = false;
        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            imprimirReporte();
        }

        private void imprimirReporte()
        {            
            int idPropuesta = Convert.ToInt32(hidPropuesta.Value);
            List<ReportParameter> ListaParametros = new List<ReportParameter>();
            List<ReportDataSource> ListaDataSource = new List<ReportDataSource>();

            ListaDataSource.Add(new ReportDataSource("dsPropuesta", cnevaluacion.ListarPropuestaCreditoidPropuesta(idPropuesta)));
            ListaParametros.Add(new ReportParameter("idPropuesta",idPropuesta.ToString(),false));
            Session["ListaParametros"] = ListaParametros;
            Session["ListaDataSource"] = ListaDataSource;
            Session["lModal"] = true;

            var cReporte = "rptPropuesta.rdlc";

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../frmGenReporte.aspx?cNomReporte=" + cReporte + "');", true);
        }
    }
}