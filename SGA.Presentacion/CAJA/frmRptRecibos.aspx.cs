using SGA.LogicaNegocio;
using SGA.Utilitarios;
using SGA.ENTIDADES;
using GEN.CapaNegocio;
using RPT.CapaNegocio;
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
    public partial class frmRptRecibos : System.Web.UI.Page
    {
        #region Variables Globales

        public string cValCarCol = "S";
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
            cargarAgencia();
            DatosUsuario();
            ListarColAgencia(objUsuario.nIdAgencia);

            dtpFechaRpt.SeleccionarFecha = objUsuario.dFecSystem;
            //=====================================================
            //----Validar cargo del Colaborador
            //=====================================================
            clsCNControlOpe ValCargo = new clsCNControlOpe();
            string ValCargoPer = ValCargo.ValidacargoPer(objUsuario.idUsuario);
            if (ValCargoPer == "0")
            {
                cValCarCol = "N";
                this.cboAgencias.Enabled = false;
                this.cboColaborador.Enabled = false;
                this.cboAgencias.SelectedValue = Convert.ToString(objUsuario.nIdAgencia);
                this.cboColaborador.SelectedValue = Convert.ToString(objUsuario.idUsuario);
            }

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

        private void cargarAgencia()
        {
            GEN.CapaNegocio.clsCNAgencia Agencia = new GEN.CapaNegocio.clsCNAgencia();
            var dt = Agencia.LisAgen();
            this.cboAgencias.DataSource = dt;
            this.cboAgencias.DataValueField = dt.Columns[0].ToString();
            this.cboAgencias.DataTextField = dt.Columns[1].ToString();
            cboAgencias.DataBind();
            this.cboAgencias.SelectedValue = "1";

        }

        private void ListarColAgencia(int idAge)
        {
            clsCNControlOpe LisColAge = new clsCNControlOpe();
            DataTable tbColAge = LisColAge.ListarColabAgencias(idAge);
            this.cboColaborador.DataSource = tbColAge;
            this.cboColaborador.DataValueField = tbColAge.Columns[0].ToString();
            this.cboColaborador.DataTextField = tbColAge.Columns[1].ToString();
            cboColaborador.DataBind();
            
            if (tbColAge.Rows.Count > 0)
            {
                this.cboColaborador.Enabled = true;
            }
            else
            {
                this.cboColaborador.Enabled = false;
            }
            if (cValCarCol == "N")
            {
                this.cboColaborador.Enabled = false;
            }
        }

        protected void cboAgencias_SelectedIndexChanged(object sender, EventArgs e)
        {
            int nItem;
            nItem = Convert.ToInt32(this.cboAgencias.SelectedValue.ToString());
            ListarColAgencia(nItem);
            if (cValCarCol == "N")
            {
                this.cboColaborador.Enabled = false;
            }
        }



        protected void BotonImprimir1_Click1(object sender, EventArgs e)
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
            //==================================================================
            //--Validar Datos del Reporte
            //==================================================================
            if (string.IsNullOrEmpty(this.cboAgencias.SelectedValue.ToString().Trim()))
            {
                script.Mensaje("Debe Seleccionar una Agencia");
                //MessageBox.Show("Debe Seleccionar una Agencia", "Reporte de Recibos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.cboAgencias.Focus();
                return;
            }
            if (this.cboColaborador.SelectedIndex < 0)
            {
                script.Mensaje("Debe Seleccionar el Colaborador");
                //MessageBox.Show("Debe Seleccionar el Colaborador", "Reporte de Recibos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.cboColaborador.Focus();
                return;
            }
            if (string.IsNullOrEmpty(this.cboColaborador.SelectedValue.ToString().Trim()))
            {
                script.Mensaje("Debe Seleccionar el Colaborador");
                //MessageBox.Show("Debe Seleccionar el Colaborador", "Reporte de Recibos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.cboColaborador.Focus();

                return;
            }



            string dFecha = this.dtpFechaRpt.SeleccionarFecha.ToShortDateString();
            string idUsu = this.cboColaborador.SelectedValue.ToString();
            string idAge = this.cboAgencias.SelectedValue.ToString();
            DateTime dFechaSis = objUsuario.dFecSystem;
            //string cVarVal = EntityLayer.clsVarGlobal.cRutaLogo;
            //string cVarVal = clsVarApl.dicVarGen["CRUTALOGO"];

            DataTable dtRecibos = new clsRPTCNCaja().CNRptRecibos(Convert.ToDateTime(dFecha), Convert.ToInt32(idUsu), Convert.ToInt32(idAge));
            if (dtRecibos.Rows.Count <= 0)
            {
                script.Mensaje("No existen registros de Recibos");
                //MessageBox.Show("No existen registros de Recibos", "Reporte de Habilitaciones", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                List<ReportParameter> ListaParametros = new List<ReportParameter>();
                List<ReportDataSource> ListaDataSource = new List<ReportDataSource>();
                ListaParametros.Add(new ReportParameter("dFecOpe", dFecha, false));
                ListaParametros.Add(new ReportParameter("idUsu", idUsu, false));
                ListaParametros.Add(new ReportParameter("idAge", idAge, false));
                //paramlist.Add(new ReportParameter("cNombreVariable", cVarVal, false));
                ListaParametros.Add(new ReportParameter("x_dFechaSis", dFechaSis.ToString("dd/MM/yyyy"), false));

                ListaDataSource.Add(new ReportDataSource("dsRecibos", new clsRPTCNCaja().CNRptRecibos(Convert.ToDateTime(dFecha), Convert.ToInt32(idUsu), Convert.ToInt32(idAge))));
                Session["ListaParametros"] = ListaParametros;
                Session["ListaDataSource"] = ListaDataSource;
                Session["lModal"] = true;
                var cReporte = "RptRecibos.rdlc";
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../frmGenReporte.aspx?cNomReporte=" + cReporte + "');", true);


                //ListaDataSource.Add(new ReportDataSource("dtsPPG", new RPT.CapaNegocio.clsRPTCNPlanPagos().CNCronogramaPagos(idCuenta)));
                //ListaDataSource.Add(new ReportDataSource("dtsAgencia", new RPT.CapaNegocio.clsRPTCNAgencia().CNAgenciaUsuario(EntityLayer.clsVarGlobal.nIdAgencia, EntityLayer.clsVarGlobal.User.idUsuario)));

                //Session["ListaParametros"] = ListaParametros;
                //Session["ListaDataSource"] = ListaDataSource;
                //Session["lModal"] = true;

                //var cReporte = "rptPlanPagoPosInt.rdlc";

                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../frmGenReporte.aspx?cNomReporte=" + cReporte + "');", true);
            }

        }
        


    }
}