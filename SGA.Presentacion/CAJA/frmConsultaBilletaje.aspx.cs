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

namespace SGA.Presentacion.CAJA
{
    public partial class frmConsultaBilletaje : System.Web.UI.Page
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
            ListarColAgencia(objUsuario.nIdAgencia);
            this.cboColaborador.SelectedValue = objUsuario.idUsuario.ToString();
            if (ValidaRespBoveda() != "0")
            {
                this.cboColaborador.Enabled = true;
            }
            else
            {
                this.cboColaborador.Enabled = true;
            }
            this.dtpProceso.SeleccionarFecha = objUsuario.dFecSystem;

        }

        protected void BotonProcesar1_Click(object sender, EventArgs e)
        {
            if (this.cboColaborador.SelectedIndex < 0)
            {
                script.Mensaje("Debe Seleccionar un Colaborador");
                //MessageBox.Show("Debe Seleccionar un Colaborador", "Consultar Billetaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(this.cboColaborador.SelectedValue.ToString().Trim()))
            {
                script.Mensaje("Debe Seleccionar un Colaborador");
                //MessageBox.Show("Debe Seleccionar un Colaborador", "Consultar Billetaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //--Reiniciar Valores
            this.txtMonMoneda.Text = "0.00";
            this.txtMonBillete.Text = "0.00";
            this.txtMonTotal.Text = "0.00";
            //--Cargar Datos del Billetaje
            ListarMonedaSoles(2);
            ListarBilleteSoles(2);
            SumaMonSol();

            if (this.dtgMonedas.Rows.Count > 0)
            {
                this.btnImprimir.Enabled = true;
            }
            if (this.dtgBilletes.Rows.Count > 0)
            {
                this.btnImprimir.Enabled = true;
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
            int nidCli = objUsuario.idUsuario;

            CLI.CapaNegocio.clsCNRetDatosCliente RetDatCli = new CLI.CapaNegocio.clsCNRetDatosCliente();
            DataTable DatosCli = RetDatCli.ListarDatosCli(nidCli, "D");

            if (DatosCli.Rows.Count > 0)
            {
                this.txtNomUsu.Text = DatosCli.Rows[0]["cNombre"].ToString();
            }
            else
            {
                txtNomUsu.Text = "";
            }
        }

        private void ListarColAgencia(int idAge)
        {
            clsCNControlOpe LisColAge = new clsCNControlOpe();
            DataTable tbColAge = LisColAge.ListarColabAgencias(idAge);
            this.cboColaborador.DataSource = tbColAge;
            this.cboColaborador.DataValueField = tbColAge.Columns[0].ToString();
            this.cboColaborador.DataTextField = tbColAge.Columns[1].ToString();
            this.cboColaborador.DataBind();
            if (tbColAge.Rows.Count > 0)
            {
                this.cboColaborador.Enabled = true;
            }
            else
            {
                this.cboColaborador.Enabled = false;
            }
        }


        private string ValidaRespBoveda()
        {
            clsUsuario objUsuario;
            if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor"); }
            else
            {
                objUsuario = (clsUsuario)Session["DatosUsuarioSession"];
            }
            if (string.IsNullOrEmpty(this.txtCodUsu.Text.Trim()))
            {
                script.Mensaje("No Existe Usuario");
                //MessageBox.Show("No Existe Usuario", "Validar Usuario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return "ERROR";
            }
            clsCNControlOpe ValidaResBov = new clsCNControlOpe();
            string cValUsu = ValidaResBov.RetRespBoveda(Convert.ToInt32(this.txtCodUsu.Text.Trim().ToString()), objUsuario.nIdAgencia);
            // Si valor es: 0--> usuario no Es Responsable de Boveda, 1 u otro Valor--> Es responsable de Boveda
            return cValUsu;
        }


        private void ListarMonedaSoles(int nOpc)
        {
            clsUsuario objUsuario;
            if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor"); }
            else
            {
                objUsuario = (clsUsuario)Session["DatosUsuarioSession"];
            }
            if (this.cboColaborador.SelectedIndex < 0)
            {
                return;
            }
            string msge = "";
            clsCNControlOpe LisMonSol = new clsCNControlOpe();
            DataTable tbMonSol = LisMonSol.ListarCorteFrac(Convert.ToDateTime(this.dtpProceso.SeleccionarFecha.ToShortDateString()), Convert.ToInt32(this.cboColaborador.SelectedValue.ToString()), objUsuario.nIdAgencia, 1, 1, ref msge);
            if (msge == "OK")
            {
                this.dtgMonedas.DataSource = tbMonSol;
                this.dtgMonedas.DataBind();
            }
            else
            {
                script.Mensaje(msge);
                //MessageBox.Show(msge, "Error al Extraer Datos de Monedas", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.txtMonMoneda.Text = tbMonSol.Compute("SUM(nTotal)", "").ToString();
        }

        private void ListarBilleteSoles(int nOpc)
        {
            clsUsuario objUsuario;
            if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor"); }
            else
            {
                objUsuario = (clsUsuario)Session["DatosUsuarioSession"];
            }
            if (this.cboColaborador.SelectedIndex < 0)
            {
                return;
            }
            string msge = "";
            clsCNControlOpe LisBillSol = new clsCNControlOpe();
            DataTable tbBillSol = LisBillSol.ListarCorteFrac(Convert.ToDateTime(this.dtpProceso.SeleccionarFecha.ToShortDateString()), Convert.ToInt32(this.cboColaborador.SelectedValue.ToString()), objUsuario.nIdAgencia, 1, 2, ref msge);
            if (msge == "OK")
            {
                this.dtgBilletes.DataSource = tbBillSol;
                this.dtgBilletes.DataBind();
            }
            else
            {
                script.Mensaje(msge);
                //MessageBox.Show(msge, "Error al Extraer Datos de Billetes", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.txtMonBillete.Text = tbBillSol.Compute("SUM(nTotal)", "").ToString();
        }

        private void SumaMonSol()
        {
            this.txtMonMoneda.Text = (this.txtMonMoneda.Text.Trim().Equals("")) ? "0.00" : this.txtMonMoneda.Text;
            this.txtMonBillete.Text = (this.txtMonBillete.Text.Trim().Equals("")) ? "0.00" : this.txtMonBillete.Text;
            this.txtMonTotal.Text = Convert.ToString(Math.Round((Convert.ToDouble(this.txtMonMoneda.Text) + Convert.ToDouble(this.txtMonBillete.Text)), 2));
        }

        
    }
}