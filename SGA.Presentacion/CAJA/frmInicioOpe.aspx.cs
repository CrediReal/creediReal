using SGA.LogicaNegocio;
using SGA.Utilitarios;
using CAJ.CapaNegocio;
using SGA.ENTIDADES;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGA.Presentacion.CAJA
{
    public partial class frmInicioOpe : System.Web.UI.Page
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
                Session["DatosUsuarioSession"] = new clsCNUsuario().GetDatosUsuario(hUsuario.Value);
                objUsuario = (clsUsuario)Session["DatosUsuarioSession"];
            }
            else
            {
                objUsuario = (clsUsuario)Session["DatosUsuarioSession"];
            }
            DataTable dtEstadoCajaAgencia = ControlOpe.CNValidaAgenciaApertura(objUsuario.dFecSystem, objUsuario.nIdAgencia);
            if (Convert.ToBoolean(dtEstadoCajaAgencia.Rows[0]["nApertura"]))
            {
                script.Mensaje("La agencia ya esta cerrada,no es posible Inciar Operaciones");
                //MessageBox.Show("La agencia ya esta cerrada," + "\n" + "no es posible Inciar Operaciones", "Inicio de Operaciones", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Dispose();
            }

            DatosUsuario();
            SaldosUsuario();
            //===========================================================
            //--Validar Inicio de Operaciones
            //===========================================================
            string cRpta = ValidaInicioOpe(); // ValidaInicioOpe();
            switch (cRpta) // Si Estado es: F--> Falta Iniciar, A--> Caja Abierta, C--> Caja Cerrada  
            {
                case "F":
                    break;
                case "A":
                    script.Mensaje("El Usuario ya Inicio sus Operaciones");
                    //MessageBox.Show("El Usuario ya Inicio sus Operaciones", "Validar Inicio de Operaciones", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Dispose();
                    return;
                case "C":
                    script.Mensaje("El Usuario ya Cerro sus Operaciones");
                    //MessageBox.Show("El Usuario ya Cerro sus Operaciones", "Validar Cierre de Operaciones", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Dispose();
                    return;
                default:
                    script.Mensaje("Error al Validar Estado de Operaciones");
                    //MessageBox.Show(cRpta, "Error al Validar Estado de Operaciones", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Dispose();
                    return;
            }


        }

        private string ValidaInicioOpe()
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
            string cEstCie = ControlOpe.ValidaIniOpe(this.dtpFechaSis.SeleccionarFecha, Convert.ToInt32(txtCodUsu.Text.Trim().ToString()), objUsuario.nIdAgencia);
            // Si Estado es: F--> Falta Iniciar, A--> Caja Abierta, C--> Caja Cerrada            
            return cEstCie;
        }

        private void DatosUsuario()
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

        private void SaldosUsuario()
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
            DataTable tbSaldos = ControlOpe.ListarSaldos(objUsuario.dFecSystem , Convert.ToInt32(txtCodUsu.Text.Trim().ToString()), objUsuario.nIdAgencia);
            if (tbSaldos.Rows.Count > 0)
            {
                txtMonSoles.Text = tbSaldos.Rows[0]["nMontoCieSol"].ToString();
                txtMonDolares.Text = tbSaldos.Rows[1]["nMontoCieDol"].ToString();
            }
            else
            {
                txtMonSoles.Text = "0.00";
                txtMonDolares.Text = "0.00";
            }
        }

        protected void BotonProcesar1_Click(object sender, EventArgs e)
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
            //var Msg = script.Mensaje("Esta seguro de Realizar el Inicio de Operaciones?...");// MessageBox.Show("Esta seguro de Realizar el Inicio de Operaciones?...", "Inicio de Operaciones", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            //if (Msg == DialogResult.Yes)
            //{
            string Rpta;
                double nMonSol = Convert.ToDouble(txtMonSoles.Text);
                double nMonDol = Convert.ToDouble(txtMonDolares.Text);
                Rpta = ControlOpe.GrabarIniOpe(this.dtpFechaSis.SeleccionarFecha, Convert.ToInt32(txtCodUsu.Text), nMonSol, nMonDol, objUsuario.nIdAgencia);
                if (Rpta == "OK")
                {
                    script.Mensaje("Inicio de operaciones exitosa.....");
                    //MessageBox.Show("El Inicio de Operaciones se Realizó Correctamente...", "Inicio de Operaciones", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BotonProcesar1.Enabled = false;
                }
                else
                {
                    script.Mensaje("No se puede iniciar operaciones..");
                    //MessageBox.Show(Rpta, "Inicio de Operaciones", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            //}
        }
    }
}