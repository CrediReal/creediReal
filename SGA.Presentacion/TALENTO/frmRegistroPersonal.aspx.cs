using SGA.Utilitarios;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GEN.CapaNegocio;
using EntityLayer;

namespace SGA.Presentacion.TALENTO
{
    public partial class frmRegistroPersonal : System.Web.UI.Page
    {

        #region Variables Globales

        clsWebJScript script = new clsWebJScript();
        public string pcTipoOpe = "N";//Puede ser N-->Nuevo o A-->Actualizar
        public DataTable dtbBuscaPersonal;
        public int nIndRegistro = 0;
        public bool modFecCese = true;
        public int idUsuario = 0;

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
            
            cargarAgencia();
            CargoPersonal();
            EstPersonal();
            this.CleanData();
            HabilitarControles(true);
            this.cboAgencia1.Enabled = false;
            this.cboCargoPersonal.Enabled = false;
            this.dtpInicioPersonal.Enabled = false;


        }

        protected void BotonConsultar1_Click(object sender, EventArgs e)
        {
            if (Session["idCliente"] == null)
            {
                return;
            }
            var idCliente = Convert.ToInt32(Session["idCliente"]);

            clsCNBuscaPersonal BuscaPersonal = new clsCNBuscaPersonal();
            dtbBuscaPersonal = BuscaPersonal.BuscaCliente(idCliente);
            //========================================================================
            //--Asignando Valores
            //========================================================================
            if (dtbBuscaPersonal.Rows.Count > 0)
            {
                this.cboCargoPersonal.SelectedValue = dtbBuscaPersonal.Rows[0]["idCargo"].ToString();
                this.cboEstPersonal.SelectedValue = dtbBuscaPersonal.Rows[0]["idEstado"].ToString();
                this.cboAgencia1.SelectedValue = (string)dtbBuscaPersonal.Rows[0]["idAgencia"].ToString();
                this.txtidUsuario.Text = dtbBuscaPersonal.Rows[0]["cWinUser"].ToString();
                idUsuario = (int)dtbBuscaPersonal.Rows[0]["idUsuario"];
                this.btnEditar.Enabled = true;
                this.btnCancelar.Enabled = true;
                this.btnEditar.Focus();
                if ((string)cboEstPersonal.SelectedValue == "1")
                {
                    btnCancelar.Enabled = true;
                    this.btnEditar.Enabled = true;
                    this.lblBase1.Visible = true;
                    this.dtpInicioPersonal.Visible = true;
                    this.lblBase4.Visible = false;
                    this.dtpCesePersonal.Visible = false;
                    this.dtpInicioPersonal.SeleccionarFecha = Convert.ToDateTime(dtbBuscaPersonal.Rows[0]["dFechaIngreso"].ToString());
                    this.dtpCesePersonal.SeleccionarFecha = Convert.ToDateTime(dtbBuscaPersonal.Rows[0]["dFechaCese"].ToString());
                }
                if ((string)cboEstPersonal.SelectedValue == "2")
                {
                    modFecCese = false;
                    btnCancelar.Enabled = true;
                    this.btnEditar.Enabled = true;
                    this.lblBase1.Visible = true;
                    this.dtpInicioPersonal.Visible = true;
                    this.lblBase4.Visible = true;
                    this.dtpCesePersonal.Visible = true;
                    this.dtpInicioPersonal.SeleccionarFecha = Convert.ToDateTime(dtbBuscaPersonal.Rows[0]["dFechaIngreso"].ToString());
                    this.dtpCesePersonal.SeleccionarFecha = Convert.ToDateTime(dtbBuscaPersonal.Rows[0]["dFechaCese"].ToString());
                }
            }
            else
            {
                pcTipoOpe = "N";
                this.HabilitarControles(true);
                this.CleanData();
                dtpCesePersonal.SeleccionarFecha = Convert.ToDateTime("01/01/1900");
                cboEstPersonal.SelectedValue = "1";
                this.btnGrabar.Enabled = true;
                this.btnEditar.Enabled = false;
                this.btnCancelar.Enabled = true;
            }
           
        }

        public void HabilitarControles(Boolean bVal)
        {
 
            this.cboCargoPersonal.Enabled = bVal;
            this.cboAgencia1.Enabled = bVal;
            this.cboEstPersonal.Enabled = !bVal;
            this.btnEditar.Enabled = !bVal;
            this.btnGrabar.Enabled = bVal;
            this.btnCancelar.Enabled = bVal;
        }

        private void CleanData()
        {
            SGA.ENTIDADES.clsUsuario objUsuario2;
            if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor"); }
            else
            {
                objUsuario2 = (SGA.ENTIDADES.clsUsuario)Session["DatosUsuarioSession"];
            }
            this.cboAgencia1.SelectedValue = "0";
            this.cboCargoPersonal.SelectedValue = "0";
            this.cboEstPersonal.SelectedValue = "0";
            this.dtpInicioPersonal.SeleccionarFecha = objUsuario2.dFecSystem;
            this.dtpCesePersonal.SeleccionarFecha = objUsuario2.dFecSystem;
            this.txtidUsuario.Text = "";
            this.dtpInicioPersonal.Visible = false;
            this.dtpCesePersonal.Visible = false;
            this.lblBase1.Visible = false;
            this.lblBase4.Visible = false;
        }

        private void cargarAgencia()
        {
            GEN.CapaNegocio.clsCNAgencia Agencia = new GEN.CapaNegocio.clsCNAgencia();
            var dt = Agencia.LisAgen();
            this.cboAgencia1.DataSource = dt;
            this.cboAgencia1.DataValueField = dt.Columns[0].ToString();
            this.cboAgencia1.DataTextField = dt.Columns[1].ToString();
            cboAgencia1.DataBind();


        }

        public void CargoPersonal()
        {
            clsCNListaCargoPersonal objCargoPersonal = new clsCNListaCargoPersonal();
            DataTable dtCargoPersonal = objCargoPersonal.ListacargoPersonal();
            this.cboCargoPersonal.DataSource = dtCargoPersonal;
            this.cboCargoPersonal.DataValueField = dtCargoPersonal.Columns[0].ToString();
            this.cboCargoPersonal.DataTextField = dtCargoPersonal.Columns[1].ToString();
            cboCargoPersonal.DataBind();

        }

        public void EstPersonal()
        {

            clsCNEstPersonal ListaEstPersonal = new clsCNEstPersonal();
            DataTable dtListaEstPersonal = ListaEstPersonal.ListaEstPersonal();
            this.cboEstPersonal.DataSource = dtListaEstPersonal;
            this.cboEstPersonal.DataValueField = dtListaEstPersonal.Columns[0].ToString();
            this.cboEstPersonal.DataTextField = dtListaEstPersonal.Columns[1].ToString();
            cboEstPersonal.DataBind();

        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            SGA.ENTIDADES.clsUsuario objUsuario2;
            if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor"); }
            else
            {
                objUsuario2 = (SGA.ENTIDADES.clsUsuario)Session["DatosUsuarioSession"];
            }
            pcTipoOpe = "A";
            this.HabilitarControles(true);
            this.cboEstPersonal.Enabled = true;
            if ((string)cboEstPersonal.SelectedValue == "2")
            {
                this.dtpInicioPersonal.SeleccionarFecha = objUsuario2.dFecSystem;
                this.dtpCesePersonal.Visible = false;
                this.lblBase4.Visible = false;
                this.dtpCesePersonal.SeleccionarFecha = Convert.ToDateTime("01/01/1900");
                this.cboEstPersonal.Enabled = false;
                this.cboEstPersonal.SelectedValue = "1";
            }
        }

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            this.GuardarPersonal();

        }

        private void GuardarPersonal()
        {
            SGA.ENTIDADES.clsUsuario objUsuario2;
            if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor"); }
            else
            {
                objUsuario2 = (SGA.ENTIDADES.clsUsuario)Session["DatosUsuarioSession"];
            }
            if (!ValidarData())
                return;
            //===================================================================
            //Obtener Datos Generales
            //======================================================================
            if (Session["idCliente"] == null)
            {
                return;
            }
            string tcIdCliente = Convert.ToString(Session["idCliente"]);
            DateTime tdFechInicio = dtpInicioPersonal.SeleccionarFecha;
            Nullable<DateTime> tdFechaCese = dtpCesePersonal.SeleccionarFecha;
            int nidAgencia = Convert.ToInt32(cboAgencia1.SelectedValue);
            string tcCargoPer = cboCargoPersonal.SelectedValue.ToString().Trim();
            string tcEstadoPer = cboEstPersonal.SelectedValue.ToString().Trim();
            //===================================================================
            //Guardar Datos del Cliente
            //===================================================================
            clsCNGuardaPersonal GuardaPersonal = new clsCNGuardaPersonal();
            if (pcTipoOpe == "N")
            {

                DataTable dtbPersonal = GuardaPersonal.GuardaPersonal(Convert.ToInt32(tcIdCliente), tdFechInicio, tdFechaCese, nidAgencia,
                                              Convert.ToInt32(tcEstadoPer), Convert.ToInt32(tcCargoPer));
                script.Mensaje("Los Datos se Registraron Correctamente");
                //MessageBox.Show("Los Datos se Registraron Correctamente", "Registro de Personal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.txtidUsuario.Text = dtbPersonal.Rows[0]["idPersonal"].ToString();


            }
            else if (pcTipoOpe == "A")
            {

                tdFechaCese = ((string)cboEstPersonal.SelectedValue == "2" & modFecCese) ? objUsuario2.dFecSystem : dtpCesePersonal.SeleccionarFecha;
                GuardaPersonal.ActualizaPersonal(idUsuario, tdFechInicio, tdFechaCese, Convert.ToInt32(tcEstadoPer),
                                                 Convert.ToInt32(tcCargoPer), nidAgencia);

                script.Mensaje("Los Datos se Actualizaron Correctamente");

                //MessageBox.Show("Los Datos se Actualizaron Correctamente", "Registro de Personal", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            this.cargardato();
            btnCancelar.Enabled = true;
            btnGrabar.Enabled = false;
            //grbDatosRegistro.Enabled = false;
            //this.conBusCli.btnBusCliente.Enabled = true;
            modFecCese = true;
        }

        private bool ValidarData()
        {
            bool res = true;
            if (cboAgencia1.SelectedValue == null || (string)cboAgencia1.SelectedValue == "0")
            {
                script.Mensaje("Debe asignar la Agencia");
                //MessageBox.Show("Debe asignar la Agencia", "Registro de Personal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                res = false;
                return res;
            }
            if (cboCargoPersonal.SelectedValue == null)
            {
                script.Mensaje("Debe asignar la cargo");
                //MessageBox.Show("Debe asignar el Cargo", "Registro de Personal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                res = false;
                return res;
            }
            if (cboEstPersonal.SelectedValue == null)
            {
                script.Mensaje("Debe asignar la estado");
                //MessageBox.Show("Debe asignar el Estado", "Registro de Personal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                res = false;
                return res;
            }
            return res;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            this.CleanData();

            this.HabilitarControles(false);
            this.btnEditar.Enabled = false;
            this.cboEstPersonal.Enabled = false;

        }

        private void cargardato()
        {
        if (Session["idCliente"] == null)
            {
                return;
            }
            var idCliente = Convert.ToInt32(Session["idCliente"]);

            clsCNBuscaPersonal BuscaPersonal = new clsCNBuscaPersonal();
            dtbBuscaPersonal = BuscaPersonal.BuscaCliente(idCliente);
            //========================================================================
            //--Asignando Valores
            //========================================================================
            if (dtbBuscaPersonal.Rows.Count > 0)
            {
                this.cboCargoPersonal.SelectedValue = dtbBuscaPersonal.Rows[0]["idCargo"].ToString();
                this.cboEstPersonal.SelectedValue = dtbBuscaPersonal.Rows[0]["idEstado"].ToString();
                this.cboAgencia1.SelectedValue = (string)dtbBuscaPersonal.Rows[0]["idAgencia"].ToString();
                this.txtidUsuario.Text = dtbBuscaPersonal.Rows[0]["cWinUser"].ToString();
                idUsuario = (int)dtbBuscaPersonal.Rows[0]["idUsuario"];
                this.btnEditar.Enabled = true;
                this.btnCancelar.Enabled = true;
                this.btnEditar.Focus();
                if ((string)cboEstPersonal.SelectedValue == "1")
                {
                    btnCancelar.Enabled = true;
                    this.btnEditar.Enabled = true;
                    this.lblBase1.Visible = true;
                    this.dtpInicioPersonal.Visible = true;
                    this.lblBase4.Visible = false;
                    this.dtpCesePersonal.Visible = false;
                    this.dtpInicioPersonal.SeleccionarFecha = Convert.ToDateTime(dtbBuscaPersonal.Rows[0]["dFechaIngreso"].ToString());
                    this.dtpCesePersonal.SeleccionarFecha = Convert.ToDateTime(dtbBuscaPersonal.Rows[0]["dFechaCese"].ToString());
                    this.cboAgencia1.Enabled = false;
                    this.cboCargoPersonal.Enabled = false;
                    this.dtpInicioPersonal.Enabled = false;
                    this.cboEstPersonal.Enabled = false;
                }
                if ((string)cboEstPersonal.SelectedValue == "2")
                {
                    modFecCese = false;
                    btnCancelar.Enabled = true;
                    this.btnEditar.Enabled = true;
                    this.lblBase1.Visible = true;
                    this.dtpInicioPersonal.Visible = true;
                    this.lblBase4.Visible = true;
                    this.dtpCesePersonal.Visible = true;
                    this.dtpInicioPersonal.SeleccionarFecha = Convert.ToDateTime(dtbBuscaPersonal.Rows[0]["dFechaIngreso"].ToString());
                    this.dtpCesePersonal.SeleccionarFecha = Convert.ToDateTime(dtbBuscaPersonal.Rows[0]["dFechaCese"].ToString());
                    this.cboAgencia1.Enabled = false;
                    this.cboCargoPersonal.Enabled = false;
                    this.dtpInicioPersonal.Enabled = false;
                    this.cboEstPersonal.Enabled = false;
                }
            }
            else
            {
                pcTipoOpe = "N";
                this.HabilitarControles(true);
                this.CleanData();
                dtpCesePersonal.SeleccionarFecha = Convert.ToDateTime("01/01/1900");
                cboEstPersonal.SelectedValue = "1";
                this.btnGrabar.Enabled = true;
                this.btnEditar.Enabled = false;
                this.btnCancelar.Enabled = true;
                this.cboAgencia1.Enabled = false;
                this.cboCargoPersonal.Enabled = false;
                this.dtpInicioPersonal.Enabled = false;
            }
        }

    }
}