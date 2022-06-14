using SGA.LogicaNegocio;
using SGA.Utilitarios;
using CAJ.CapaNegocio;
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
using RPT.CapaNegocio;

namespace SGA.Presentacion.CAJA
{
    public partial class frmConsultaCieOpe : System.Web.UI.Page
    {
        #region Variables Globales

        clsWebJScript script = new clsWebJScript();
        public DataTable tbIngSol;
        public DataTable tbEgrSol;
        public DataTable tbColAge;


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
                hUsuario.Value = ((clsUsuario)Session["DatosUsuarioSession"]).cUsuario;

                lblOpcion.Text = Session["cOpcion"].ToString().ToUpper() + ":";

            }
            catch (Exception)
            {
                throw;
            }
            clsUsuario objUsuario;
            if (Session["DatosUsuarioSession"] == null)
            {
                Session["DatosUsuarioSession"] = new SGA.LogicaNegocio.clsCNUsuario().GetDatosUsuario(hUsuario.Value);
                objUsuario = (clsUsuario)Session["DatosUsuarioSession"];
            }
            else
            {
                objUsuario = (clsUsuario)Session["DatosUsuarioSession"];
            }
            DatosUsuario();
            ListarColAgencia(objUsuario.nIdAgencia);
            this.cboColaborador.SelectedValue = Convert.ToString(objUsuario.idUsuario);
            this.dtpProceso.SeleccionarFecha = objUsuario.dFecSystem;

        }


        private void DatosUsuario()
        {
            clsUsuario objUsuario;
            if (Session["DatosUsuarioSession"] == null)
            {
                Session["DatosUsuarioSession"] = new SGA.LogicaNegocio.clsCNUsuario().GetDatosUsuario(hUsuario.Value);
                objUsuario = (clsUsuario)Session["DatosUsuarioSession"];
            }
            else
            {
                objUsuario = (clsUsuario)Session["DatosUsuarioSession"];
            }
            this.dtpFechaSis.SeleccionarFecha = objUsuario.dFecSystem;
            this.txtCodUsu.Text = objUsuario.idUsuario.ToString();
            txtUsuario.Text = objUsuario.cWinuser.ToString();
            int nidCli = objUsuario.idUsuario;
            
            CLI.CapaNegocio.clsCNRetDatosCliente RetDatCli = new CLI.CapaNegocio.clsCNRetDatosCliente();
            DataTable DatosCli = RetDatCli.ListarDatosCli(nidCli, "D");

            if (DatosCli.Rows.Count > 0)
            {
                txtUsuario.Text = DatosCli.Rows[0]["cNombre"].ToString();
            }
            else
            {
                txtUsuario.Text = "";
            }
        }


        private void ListarColAgencia(int idAge)
        {
            clsCNControlOpe LisColAge = new clsCNControlOpe();
            tbColAge = LisColAge.ListarColPorAgencias(idAge);
            this.cboColaborador.DataSource = tbColAge;
            this.cboColaborador.DataValueField = tbColAge.Columns[0].ToString();
            this.cboColaborador.DataTextField = tbColAge.Columns[1].ToString();
            this.cboColaborador.DataBind();
        }

        

        private void Limpiar()
        {
            this.txtSalIniSol.Text = "0.00";
            this.txtMonIngSol.Text = "0.00";
            this.txtMonEgrSol.Text = "0.00";
            this.txtSalFinSol.Text = "0.00";
            this.txtCortSoles.Text = "0.00";
            this.txtDifSoles.Text = "0.00";
            this.dtgEgrSoles.DataSource = "";
            this.dtgIngSoles.DataSource = "";
        }

        protected void btnProcesar_Click(object sender, EventArgs e)
        {

            if (this.cboColaborador.SelectedIndex < 0)
            {
                script.Mensaje("Debe Seleccionar un Colaborador");
                //MessageBox.Show("Debe Seleccionar un Colaborador", "Consultar Cierre Operaciones", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(this.cboColaborador.SelectedValue.ToString().Trim()))
            {
                script.Mensaje("Debe Seleccionar un Colaborador");
                //MessageBox.Show("Debe Seleccionar un Colaborador", "Consultar Cierre Operaciones", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //--Reiniciar Valores
            this.Limpiar();
            //---Procesar Consulta
            this.btnImprimir.Enabled = false;
            this.btnImprimir1.Enabled = false;
            ActualizarCierre();

        }

        private void ActualizarCierre()
        {
            CuadreOpe();
            SalIniOpe();
            SaldoFinal();
            //--Saldo de Corte Fraccionario
            SaldoCorteFraccionario();
        }

        private void CuadreOpe()
        {
            clsUsuario objUsuario;
            if (Session["DatosUsuarioSession"] == null)
            {
                Session["DatosUsuarioSession"] = new SGA.LogicaNegocio.clsCNUsuario().GetDatosUsuario(hUsuario.Value);
                objUsuario = (clsUsuario)Session["DatosUsuarioSession"];
            }
            else
            {
                objUsuario = (clsUsuario)Session["DatosUsuarioSession"];
            }
            string msge = "";
            int idUsu = Convert.ToInt32(this.cboColaborador.SelectedValue);
            clsCNControlOpe CuadreOpe = new clsCNControlOpe();
            //=====================================================================
            //---Ingresos en Soles
            //=====================================================================
            tbIngSol = CuadreOpe.ConsultaCuadreOpe(Convert.ToDateTime(this.dtpProceso.SeleccionarFecha.ToShortDateString()), idUsu, 1, objUsuario.nIdAgencia, 1, 1, ref msge);
            if (msge == "OK")
            {
                this.dtgIngSoles.DataSource = tbIngSol;
                this.dtgIngSoles.DataBind();
                if (tbIngSol.Rows.Count > 0)
                {
                    this.txtMonIngSol.Text = tbIngSol.Rows[0]["nTotal"].ToString();
                    this.btnImprimir.Enabled = true;
                    this.btnImprimir1.Enabled = true;
                }
            }
            else
            {
                script.Mensaje(msge);
                //MessageBox.Show(msge, "Error al Extraer Datos de las Operaciones", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //=====================================================================
            //---Egresos en Soles
            //=====================================================================
            tbEgrSol = CuadreOpe.ConsultaCuadreOpe(Convert.ToDateTime(this.dtpProceso.SeleccionarFecha.ToShortDateString()), idUsu, 1, objUsuario.nIdAgencia, 2, 1, ref msge);
            if (msge == "OK")
            {
                this.dtgEgrSoles.DataSource = tbEgrSol;
                this.dtgEgrSoles.DataBind();
                if (tbEgrSol.Rows.Count > 0)
                {
                    this.txtMonEgrSol.Text = tbEgrSol.Rows[0]["nTotal"].ToString();
                    this.btnImprimir.Enabled = true;
                    this.btnImprimir1.Enabled = true;
                }
            }
            else
            {
                script.Mensaje(msge);
                //MessageBox.Show(msge, "Error al Extraer Datos de las Operaciones", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void SalIniOpe()
        {
            clsUsuario objUsuario;
            if (Session["DatosUsuarioSession"] == null)
            {
                Session["DatosUsuarioSession"] = new SGA.LogicaNegocio.clsCNUsuario().GetDatosUsuario(hUsuario.Value);
                objUsuario = (clsUsuario)Session["DatosUsuarioSession"];
            }
            else
            {
                objUsuario = (clsUsuario)Session["DatosUsuarioSession"];
            }
            string msge = "";
            int idUsu = Convert.ToInt32(cboColaborador.SelectedValue);
            clsCNControlOpe saldoIniOpe = new clsCNControlOpe();
            //=====================================================================
            //---Ingresos en Soles
            //=====================================================================
            DataTable tbSalIniOpe = saldoIniOpe.SaldoinicialOpe(Convert.ToDateTime(this.dtpProceso.SeleccionarFecha.ToShortDateString()), idUsu, objUsuario.nIdAgencia, ref msge);
            if (msge == "OK")
            {
                if (tbSalIniOpe.Rows.Count > 0)
                {
                    this.txtSalIniSol.Text = tbSalIniOpe.Rows[0]["nSalIniSol"].ToString();

                }
            }
            else
            {
                script.Mensaje(msge);
                //MessageBox.Show(msge, "Error al Extraer El Saldo Inicial...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaldoFinal()
        {
            //====================
            //--SALDO FINA SOLES
            //====================
            this.txtSalFinSol.Text = Convert.ToString(Math.Round((Convert.ToDouble(this.txtSalIniSol.Text) + Convert.ToDouble(this.txtMonIngSol.Text) - Convert.ToDouble(this.txtMonEgrSol.Text)), 2));
        }

        private void SaldoCorteFraccionario()
        {
            clsUsuario objUsuario;
            if (Session["DatosUsuarioSession"] == null)
            {
                Session["DatosUsuarioSession"] = new SGA.LogicaNegocio.clsCNUsuario().GetDatosUsuario(hUsuario.Value);
                objUsuario = (clsUsuario)Session["DatosUsuarioSession"];
            }
            else
            {
                objUsuario = (clsUsuario)Session["DatosUsuarioSession"];
            }
            double nMonSoles = 0.00, nMonDolar = 0.00;
            DateTime dFecPro = Convert.ToDateTime(this.dtpProceso.SeleccionarFecha.ToShortDateString());
            clsCNControlOpe SalCorteFrac = new clsCNControlOpe();
            string cRpta = SalCorteFrac.RetMontoCorFracc(dFecPro, Convert.ToInt32(this.cboColaborador.SelectedValue), objUsuario.nIdAgencia, ref nMonSoles, ref nMonDolar);
            if (cRpta == "OK")
            {
                if (string.IsNullOrEmpty(nMonSoles.ToString()))
                {
                    nMonSoles = 0.00;
                }
                if (string.IsNullOrEmpty(nMonDolar.ToString()))
                {
                    nMonDolar = 0.00;
                }
                this.txtCortSoles.Text = nMonSoles.ToString();
                this.txtDifSoles.Text = Convert.ToString(Math.Round((Math.Round(Convert.ToDouble(this.txtSalFinSol.Text), 2) - Math.Round(nMonSoles, 2)), 2));
            }
            else
            {
                script.Mensaje(cRpta);
                //MessageBox.Show(cRpta, "Error al Extraer El Saldo Inicial...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected void cboColaborador_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnImprimir1_Click(object sender, EventArgs e)
        {
            clsUsuario objUsuario;
            if (Session["DatosUsuarioSession"] == null)
            {
                Session["DatosUsuarioSession"] = new SGA.LogicaNegocio.clsCNUsuario().GetDatosUsuario(hUsuario.Value);
                objUsuario = (clsUsuario)Session["DatosUsuarioSession"];
            }
            else
            {
                objUsuario = (clsUsuario)Session["DatosUsuarioSession"];
            }
            DateTime dFecha = this.dtpFechaSis.SeleccionarFecha;
            int idUsu = Convert.ToInt32(cboColaborador.SelectedValue);
            int idAge = objUsuario.nIdAgencia;

            List<ReportParameter> ListaParametros = new List<ReportParameter>();
            List<ReportDataSource> ListaDataSource = new List<ReportDataSource>();

            ListaDataSource.Add(new ReportDataSource("dsKardex", new clsRPTCNCaja().CNDetallOperaciones(dFecha, idUsu, idAge)));
            ListaParametros.Add(new ReportParameter("dFecOpe", dFecha.ToString(), false));

            Session["ListaDataSource"] = ListaDataSource;
            Session["ListaParametros"] = ListaParametros;
            Session["lModal"] = true;
            var cReporte = "rptDetalleOpe.rdlc";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../frmGenReporte.aspx?cNomReporte=" + cReporte + "');", true);

        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            clsUsuario objUsuario;
            if (Session["DatosUsuarioSession"] == null)
            {
                Session["DatosUsuarioSession"] = new SGA.LogicaNegocio.clsCNUsuario().GetDatosUsuario(hUsuario.Value);
                objUsuario = (clsUsuario)Session["DatosUsuarioSession"];
            }
            else
            {
                objUsuario = (clsUsuario)Session["DatosUsuarioSession"];
            }
            DateTime dFecha = this.dtpFechaSis.SeleccionarFecha;
            int idUsu = Convert.ToInt32(cboColaborador.SelectedValue);
            int idAge = objUsuario.nIdAgencia;

            List<ReportDataSource> ListaDataSource = new List<ReportDataSource>();

            ListaDataSource.Add(new ReportDataSource("rptResumenOpe", new clsRPTCNCaja().CNResumenOpeSol(dFecha, idUsu, idAge)));
            ListaDataSource.Add(new ReportDataSource("rptResumenDol", new clsRPTCNCaja().CNResumenOpeDol(dFecha, idUsu, idAge)));

            Session["ListaDataSource"] = ListaDataSource;
            Session["lModal"] = true;
            var cReporte = "rptResumenOpe.rdlc";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../frmGenReporte.aspx?cNomReporte=" + cReporte + "');", true);

        }

        

    }
}