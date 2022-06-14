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
    public partial class frmHabilitaciones : System.Web.UI.Page
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
            if (this.ValidarInicioOpe() != "A")
            {
                this.pnlHabilita.Visible = false; ;
                return;
            }

            cargarMoneda();
            DatosUsuario();
            

            CargarTipoHab();
            //==================================================
            //Validar si es usuario de Boveda
            //==================================================
            if (ValidaRespBoveda() != "0")
            {
                this.cboTipoHab.SelectedValue = "1";
                cargarResHab(5, "V");
                this.cboTipoHab.Enabled = false;
            }
            else
            {
                cargarResHab(5, "V");
            }

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

        public string ValidarInicioOpe()
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
            clsCNControlOpe ValidaOpe = new clsCNControlOpe();
            string cEstCie = ValidaOpe.ValidaIniOpe(objUsuario.dFecSystem, objUsuario.idUsuario, objUsuario.nIdAgencia);
            // Si Estado es: F--> Falta Iniciar, A--> Caja Abierta, C--> Caja Cerrada
            //string cRpta = this.ValidarInicioOpe();
            switch (cEstCie) // Si Estado es: F--> Falta Iniciar, A--> Caja Abierta, C--> Caja Cerrada  
            {
                case "F":
                    script.Mensaje("Falta Realizar el Inicio de sus Operaciones");
                    //MessageBox.Show("Falta Realizar el Inicio de sus Operaciones", "Validar Inicio de Operaciones", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //this.Dispose();
                    break;
                case "A":
                    break;
                case "C":
                    script.Mensaje("El Usuario ya Cerro Sus Operaciones");
                    //MessageBox.Show("El Usuario ya Cerro Sus Operaciones", "Validar Cierre de Operaciones", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //this.Dispose();
                    break;
                default:
                    script.Mensaje(cEstCie);
                    //MessageBox.Show(cEstCie, "Error al Validar Estado de Operaciones", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //this.Dispose();
                    break;
            }
            return cEstCie;
        }
        private void CargarTipoHab()
        {
            string msg = "";
            clsCNControlOpe LisTipHab = new clsCNControlOpe();
            DataTable tbTipHab = LisTipHab.LisTipHab(ref msg);
            if (msg == "OK")
            {
                this.cboTipoHab.DataSource = tbTipHab;
                this.cboTipoHab.DataValueField = tbTipHab.Columns[0].ToString();
                this.cboTipoHab.DataTextField = tbTipHab.Columns[1].ToString();
                this.cboTipoHab.DataBind();
                this.cboTipoHab.SelectedValue = "1";

            }
            else
            {
                script.Mensaje(msg );
                //MessageBox.Show(msg, "Tipos de habilitación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        private string ValidaRespBoveda()
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

        private void cargarResHab(int idCargo, string cTipRes)
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
            string msg = "";
            clsCNControlOpe LisResHab = new clsCNControlOpe();
            DataTable tbTipRes = LisResHab.LisRespHab(idCargo, objUsuario.nIdAgencia, cTipRes, ref msg);
            if (msg == "OK")
            {
                this.cboUsuario.DataSource = tbTipRes;
                this.cboUsuario.DataValueField = tbTipRes.Columns["idUsuario"].ToString();
                this.cboUsuario.DataTextField = tbTipRes.Columns["cNombre"].ToString();
                this.cboUsuario.DataBind();
                //this.cboUsuario.SelectedValue = "1";
            }
            else
            {
                script.Mensaje(msg);
               // MessageBox.Show(msg, "Responsables de habilitación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        protected void BotonGrabar1_Click(object sender, EventArgs e)
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
            if (ValidadatIng() == "ERROR")
            {
                return;
            }
            if (Convert.ToDouble(this.txtMonHab.Text) <= 0)
            {
                script.Mensaje("Ingrese Monto de habilitacion Mayor Cero(0)");
                //MessageBox.Show("Ingrese Monto de habilitacion Mayor Cero(0)", "Registro de Habilitaciones", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //this.txtMonHab.Select();
                this.txtMonHab.Focus();
                return;
            }
            if (this.cboUsuario.SelectedValue.ToString().Trim() == objUsuario.idUsuario.ToString().Trim())
            {
                script.Mensaje("El Mismo Usuario No Puede Habilitarse");
                //MessageBox.Show("El Mismo Usuario No Puede Habilitarse", "Registro de Habilitaciones", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //this.cboUsuario.Select();
                this.cboUsuario.Focus();
                return;
            }
            //==================================================
            //--Datos de la Habilitación
            //==================================================
            int nTiphab = Convert.ToInt32(this.cboTipoHab.SelectedValue);
            int nTipMon = Convert.ToInt32(this.cboMoneda.SelectedValue);
            int nidUsuOri = Convert.ToInt32(this.txtCodUsu.Text);
            int nidUsuDes = Convert.ToInt32(this.cboUsuario.SelectedValue);
            double nMonHab = Convert.ToDouble(this.txtMonHab.Text);
            string cSust = this.txtSustento.Text.Trim();

            //==================================================
            //--Grabar Habilitacion
            //==================================================
            string msge = "";
            clsCNControlOpe GuardarHab = new clsCNControlOpe();
            string nidHab = GuardarHab.GuardarHabilitacion(Convert.ToDateTime(this.dtpFechaSis.SeleccionarFecha), objUsuario.nIdAgencia, nTiphab,
                                                        nTipMon, nMonHab, cSust, nidUsuOri, nidUsuDes, ref msge);
            if (msge == "OK")
            {
                script.Mensaje("La Habilitación se Registro Correctamente, NRO HABILITACION: " + nidHab);
                //MessageBox.Show("La Habilitación se Registro Correctamente, NRO HABILITACION: " + nidHab, "Registro de Habilitaciones", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                script.Mensaje(msge);
                //MessageBox.Show(msge, "Error al Registrar la Habilitación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.btnNuevo.Enabled = true;
            this.btnGrabar.Enabled = false;
            this.cboTipoHab.Enabled = false;
            desabilitaCtr(false);
        }

        private string ValidadatIng()
        {
            if (string.IsNullOrEmpty(this.cboTipoHab.SelectedValue.ToString().Trim()))
            {
                script.Mensaje("Debe Seleccionar el Tipo de Habilitación");
                //MessageBox.Show("Debe Seleccionar el Tipo de Habilitación", "Registro de Habilitaciones", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //this.cboTipoHab.Select();
                this.cboTipoHab.Focus();
                return "ERROR";
            }
            if (string.IsNullOrEmpty(this.cboMoneda.SelectedValue.ToString().Trim()))
            {
                script.Mensaje("Debe Seleccionar el Tipo de Moneda");
                //MessageBox.Show("Debe Seleccionar el Tipo de Moneda", "Registro de Habilitaciones", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //this.cboMoneda.Select();
                this.cboMoneda.Focus();
                return "ERROR";
            }
            if (string.IsNullOrEmpty(this.cboUsuario.SelectedValue.ToString().Trim()))
            {
                script.Mensaje("Debe Seleccionar Usuario Destino");
                //MessageBox.Show("Debe Seleccionar Usuario Destino", "Registro de Habilitaciones", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //this.cboUsuario.Select();
                this.cboUsuario.Focus();
                return "ERROR";
            }

            if (string.IsNullOrEmpty(this.txtMonHab.Text.Trim()))
            {
                script.Mensaje("Debe Ingresar el Monto de la habilitación");
                //MessageBox.Show("Debe Ingresar el Monto de la habilitación", "Registro de Habilitaciones", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //this.txtMonHab.Select();
                this.txtMonHab.Focus();
                return "ERROR";
            }

            if (string.IsNullOrEmpty(this.txtSustento.Text.Trim()))
            {
                script.Mensaje("Debe Ingresar el Sustento de la habilitación");
                //MessageBox.Show("Debe Ingresar el Sustento de la habilitación", "Registro de Habilitaciones", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //this.txtSustento.Select();
                this.txtSustento.Focus();
                return "ERROR";
            }

            return "OK";
        }

        private void desabilitaCtr(Boolean val)
        {
            this.cboMoneda.Enabled = val;
            this.cboUsuario.Enabled = val;
            this.txtMonHab.Enabled = val;
            this.txtSustento.Enabled = val;
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarControles();
            desabilitaCtr(true);
            if (ValidaRespBoveda() != "0")
            {
                this.cboTipoHab.Enabled = false;
            }
            else
            {
                this.cboTipoHab.Enabled = true;
            }

            this.btnNuevo.Enabled = false;
            this.btnGrabar.Enabled = true;
            this.cboTipoHab.Focus();
            cargarMoneda();
            CargarTipoHab();
            cargarResHab(4,"V");
        }

        private void LimpiarControles()
        {
            this.txtMonHab.Text = "";
            this.txtSustento.Text = "";
        }

        

        private void cargarMoneda()
        {
            GEN.CapaNegocio.clsCNMoneda moneda = new GEN.CapaNegocio.clsCNMoneda();
            var dt = moneda.listarMoneda();
            this.cboMoneda.DataSource = dt;
            this.cboMoneda.DataValueField = dt.Columns[0].ToString();
            this.cboMoneda.DataTextField = dt.Columns[1].ToString();
            cboMoneda.DataBind();
            this.cboMoneda.SelectedValue = "1";
        }

        protected void cboMoneda_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void cboTipoHab_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (this.cboTipoHab.SelectedValue.ToString() == "1")
            {
                cargarResHab(5, "V");
            }
            else
            {
                cargarResHab(5, "B");
            }
        }
       
        
    }
}