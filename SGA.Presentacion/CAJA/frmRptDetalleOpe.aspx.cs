using SGA.LogicaNegocio;
using SGA.Utilitarios;
using RPT.CapaNegocio;
using SGA.ENTIDADES;
using GEN.CapaNegocio;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using CAJ.CapaNegocio;

namespace SGA.Presentacion.CAJA
{
    public partial class frmRptDetalleOpe : System.Web.UI.Page
    {
        #region Variables Globales
        clsWebJScript script = new clsWebJScript();
        clsCNControlOpe ControlOpe = new clsCNControlOpe();

                #endregion
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
            clsUsuario objUsuario;
            if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor"); }
            else
            {
                objUsuario = (clsUsuario)Session["DatosUsuarioSession"];
            }
            DatosUsuario();
            this.dtpFecProc.SeleccionarFecha = objUsuario.dFecSystem;

            //--Validar si ya Realizó su corte Fraccionario
            if (ValidarCorteFracc() == "ERROR")
            {
                this.Dispose();
                return;
            }

        }

        private void DatosUsuario()
        {
            clsUsuario objUsuario;
            if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor"); }
            else
            {
                objUsuario = (clsUsuario)Session["DatosUsuarioSession"];
            }
            this.dtpFechaSis.SeleccionarFecha = objUsuario.dFecSystem;
            this.txtCodUsu.Text = objUsuario.idUsuario.ToString();
            txtUsuario.Text = objUsuario.cWinuser.ToString();
            int nidCli = objUsuario.idCli;
            CLI.CapaNegocio.clsCNRetDatosCliente RetDatCli = new CLI.CapaNegocio.clsCNRetDatosCliente();
            DataTable DatosCli = RetDatCli.ListarDatosCli(nidCli, "D");
            if (DatosCli.Rows.Count > 0)
            {
                txtNomUsu.Text = DatosCli.Rows[0]["cNombre"].ToString();
            }
            else
            {
                txtNomUsu.Text = "";
            }
        }

        private string ValidarCorteFracc()
        {
            clsUsuario objUsuario;
            if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor"); }
            else
            {
                objUsuario = (clsUsuario)Session["DatosUsuarioSession"];
            }
            string cRpta = "OK";
            string msge = "";
            clsCNControlOpe ValCorFra = new clsCNControlOpe();
            string cCorFra = ValCorFra.ValidaCorteFracc(this.dtpFechaSis.SeleccionarFecha, objUsuario.idUsuario, objUsuario.nIdAgencia, ref msge);
            if (msge == "OK")
            {
                if (cCorFra == "0")
                {
                    script.Mensaje("Primero debe Realizar su Corte Fraccionario... por Favor..");
                    //MessageBox.Show("Primero debe Realizar su Corte Fraccionario... por Favor..", "Validar Corte Fraccionario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cRpta = "ERROR";
                }
            }
            else
            {
                script.Mensaje(msge);
                //MessageBox.Show(msge, "Error al Validar Corte Fraccionario", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cRpta = "ERROR";
            }
            return cRpta;
        }

        protected void BotonImprimir1_Click(object sender, EventArgs e)
        {
            clsUsuario objUsuario;
            if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor"); }
            else
            {
                objUsuario = (clsUsuario)Session["DatosUsuarioSession"];
            }
            List<ReportDataSource> ListaDataSource = new List<ReportDataSource>();
            DateTime dFecProc = this.dtpFecProc.SeleccionarFecha;
            int idUsu = objUsuario.idUsuario;
            int idAge = objUsuario.nIdAgencia;

            ListaDataSource.Add(new ReportDataSource("dtsCobranza", new clsRPTCNCredito().CNOperacionesCredito(dFecProc, idAge, idUsu)));
            ListaDataSource.Add(new ReportDataSource("dtsDetHabil", new clsRPTCNCaja().CNDetalleHabilita(dFecProc, idAge, idUsu)));
            ListaDataSource.Add(new ReportDataSource("dtsDetRecibos", new clsRPTCNCaja().CNDetalleRecibos(dFecProc, idAge, idUsu)));
            ListaDataSource.Add(new ReportDataSource("dtsGenReport", new clsRPTCNAgencia().CNAgenciaUsuario(idAge, idUsu)));

            Session["ListaDataSource"] = ListaDataSource;
            Session["lModal"] = true;
            var cReporte = "rptDetalleCuadreOpe.rdlc";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../frmGenReporte.aspx?cNomReporte=" + cReporte + "');", true);


        }
    }
}