using SGA.ENTIDADES;
using SGA.Utilitarios;
using CRE.CapaNegocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGA.Presentacion.CREDITOS
{
    public partial class frmCargarGastos : System.Web.UI.Page
    {
        GEN.CapaNegocio.clsCNRetornsCuentaSolCliente RetornaCuentaSolCliente = new GEN.CapaNegocio.clsCNRetornsCuentaSolCliente();
        public Int32 pnIdCliente;
        public DataTable dtbPlanPagos;
        public Int32 pnIdNumCuenta;
        public string pcTipoBusqueda;
        public string pcEstadoCredito;
        public Int32 nNumCuotas = 0;
        clsWebJScript script = new clsWebJScript();

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
            cargarMoneda();
            cboTipoGastos();
            string cTipoBusqueda = "C";
            string cEstado = "[5]";
            //pcTipoBusqueda = this.conBusCuentaCli.cTipoBusqueda;
            //pcEstadoCredito = this.conBusCuentaCli.cEstado;
        }

        protected void BotonProcesar2_Click(object sender, EventArgs e)
        {
            clsUsuario objUsuario;
            if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor"); }
            else
            {
                objUsuario = (clsUsuario)Session["DatosUsuarioSession"];
            }

            if (Session["idCliente"] == null)
            {
                return;
            }
            var idCliente = Convert.ToInt32(Session["idCliente"]);
            if (idCliente == 0)
            {
                return;
            }
            DataTable dtDatosCuentaSolCliente = RetornaCuentaSolCliente.RetornaCuentaSolCliente(idCliente, "C", "[5]");
            if (dtDatosCuentaSolCliente.Rows.Count == 1)
            {
                this.hIdCuenta.Value = dtDatosCuentaSolCliente.Rows[0][0].ToString();
                GEN.CapaNegocio.clsCNRetornaNumCuenta RetornaNumCuenta = new GEN.CapaNegocio.clsCNRetornaNumCuenta();
                DataTable dtDatosNumCuenta = RetornaNumCuenta.RetornaNumCuenta(Convert.ToInt32(hIdCuenta.Value), "C", "[5]");
                if (dtDatosNumCuenta.Rows.Count == 0)
                {
                    script.Mensaje("No se encontró Número de Cuenta");
                    this.hIdCuenta.Value = "";
                }
                else
                {
                    DataTable dtEstCuenta = RetornaNumCuenta.VerifEstCuenta(Convert.ToInt32(hIdCuenta.Value));
                    var nidUserBloqueo = (Nullable<int>)dtEstCuenta.Rows[0][0];
                    if (nidUserBloqueo != 0)
                    {
                        DataTable dtUsu = new DataTable();
                        dtUsu = RetornaNumCuenta.BusUsuBlo((int)nidUserBloqueo);
                        var cUserBloqueo = dtUsu.Rows[0][0].ToString();
                        script.Mensaje("Cuenta Bloqueada por usuario: " + cUserBloqueo);
                        this.hIdCuenta.Value = "";
                    }
                    else
                    {
                        conBuscarCliente1.Habilitar(false);
                        RetornaNumCuenta.UpdEstCuenta(Convert.ToInt32(hIdCuenta.Value), objUsuario.idUsuario);
                        cargadatos();
                    }
                }
            }
            else if (dtDatosCuentaSolCliente.Rows.Count > 1)
            {
                //dtgCreditos.DataSource = dtDatosCuentaSolCliente;
                //dtgCreditos.DataBind();
            }
           
            //pnIdNumCuenta = Convert.ToInt32(conBusCuentaCli.txtNroBusqueda.Text);
            //if (pnIdNumCuenta == 0)
            //{
            //    MessageBox.Show("Debe de Seleccionar una cuenta", "Carga de Gastos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    LimpiarControles();
            //    conBusCuentaCli.Focus();
            //    return;
            //}
            //else
            //{
            //    //===================================================================================
            //    //--Cargando Datos de la Cuenta Seleccionada
            //    //===================================================================================
            //    clsCNRetornsCuentaSolCliente objNumCreSol = new clsCNRetornsCuentaSolCliente();
            //    DataTable dtbNumCreSol = objNumCreSol.RetornaCuentaSolCliente(pnIdCliente, pcTipoBusqueda, pcEstadoCredito);
            //    this.txtTipoCredito.Text = dtbNumCreSol.Rows[0]["cProducto"].ToString();
            //    this.cboMoneda.SelectedValue = dtbNumCreSol.Rows[0]["IdMoneda"].ToString();
            //    this.txtMonto.Text = dtbNumCreSol.Rows[0]["nMonto"].ToString();
            //    this.txtCuotas.Text = dtbNumCreSol.Rows[0]["nCuotas"].ToString();
            //    //===================================================================================
            //    //--Cargando Datos del Plan de Pagos de la Cuenta Seleccionada
            //    //===================================================================================
            //    this.CargarPlanPagosCreditoCli(pnIdNumCuenta);
            //    this.HabilitarControles(true);
            //    this.HabilitarGridPlanPagos(true);

            //}
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

        private void cboTipoGastos()
        {

            clsCNTipoGasto objTipoGasto = new clsCNTipoGasto();
            DataTable dtbTipoGasto = objTipoGasto.ListaTipoGasto();
            this.cboTipoGasto.DataSource = dtbTipoGasto;
            this.cboTipoGasto.DataValueField = dtbTipoGasto.Columns[0].ToString();
            this.cboTipoGasto.DataTextField = dtbTipoGasto.Columns[1].ToString();
            cboTipoGasto.DataBind();
            this.cboTipoGasto.SelectedValue = "1";

        }

        private void cargadatos()
        {
            //===================================================================================
            //--Cargando Datos de la Cuenta Seleccionada
            //===================================================================================
            GEN.CapaNegocio.clsCNRetornsCuentaSolCliente objNumCreSol = new GEN.CapaNegocio.clsCNRetornsCuentaSolCliente();
            DataTable dtbNumCreSol = objNumCreSol.RetornaCuentaSolCliente(pnIdCliente, pcTipoBusqueda, pcEstadoCredito);
            this.txtTipoCredito.Text = dtbNumCreSol.Rows[0]["cProducto"].ToString();
            this.cboMoneda.SelectedValue = dtbNumCreSol.Rows[0]["IdMoneda"].ToString();
            this.txtMonto.Text = dtbNumCreSol.Rows[0]["nMonto"].ToString();
            this.txtCuotas.Text = dtbNumCreSol.Rows[0]["nCuotas"].ToString();
            //===================================================================================
            //--Cargando Datos del Plan de Pagos de la Cuenta Seleccionada
            //===================================================================================
            this.CargarPlanPagosCreditoCli(pnIdNumCuenta);
            //this.HabilitarControles(true);
            //this.HabilitarGridPlanPagos(true);
        }


        public void CargarPlanPagosCreditoCli(Int32 nNumCredito)
        {
            clsCNTipoGasto objTipoGasto = new clsCNTipoGasto();
            DataTable dtbTipoGasto = objTipoGasto.ListaTipoGasto();
            this.cboTipoGasto.DataSource = dtbTipoGasto;
            this.cboTipoGasto.DataValueField = dtbTipoGasto.Columns[0].ToString();
            this.cboTipoGasto.DataTextField = dtbTipoGasto.Columns[1].ToString();
            cboTipoGasto.DataBind();
            this.cboTipoGasto.SelectedValue = "1";

            clsCNPlanPago ObjPlanPagos = new clsCNPlanPago();
            DataTable dtbPlanPagos = ObjPlanPagos.CNdtPlanPago(nNumCredito);
            //dtbPlanPagos.Columns.Add("nMontoTotal", typeof(decimal));
            //dtbPlanPagos.Columns.Add("lAplicar", typeof(Boolean));
            
            this.dtgPlanPagos.DataSource = dtbPlanPagos;
            this.dtgPlanPagos.DataBind();

            //===================================================================================
            //--Asignando Valor a la Columna Aplicar Gasto
            //===================================================================================
            nNumCuotas = dtbPlanPagos.Rows.Count;
            if (nNumCuotas > 0)
            {
                for (int i = 0; i < nNumCuotas; i++)
                {
                    //dtgPlanPagos.Rows[i].Cells["lAplicar"].Value = false;
                }
            }
        }

    }
}