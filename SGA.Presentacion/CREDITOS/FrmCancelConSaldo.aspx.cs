using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGA.ENTIDADES;
using SGA.LogicaNegocio;
using SGA.Utilitarios;

namespace SGA.Presentacion.CREDITOS
{
    public partial class FrmCancelConSaldo : System.Web.UI.Page
    {
        GEN.CapaNegocio.clsCNRetornsCuentaSolCliente RetornaCuentaSolCliente = new GEN.CapaNegocio.clsCNRetornsCuentaSolCliente();
        clsWebJScript script = new clsWebJScript();
        clsCNCronograma cnocronograma = new clsCNCronograma();

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
        }

        protected void BotonConsultar1_Click(object sender, EventArgs e)
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
                dtgCreditos.DataSource = dtDatosCuentaSolCliente;
                dtgCreditos.DataBind();
            }
        }

        protected void dtgCreditos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dtgCreditos.Rows.Count > 0)
            {
                hIdCuenta.Value = dtgCreditos.SelectedRow.Cells[0].Text;
                cargadatos();
            }
        }

        private void cargadatos()
        {
            if (hIdCuenta.Value == "")
            {
                this.BotonGrabar1.Enabled = false;
                this.LimpiarDatos();
                return;
            }
            int nNumCredito = Convert.ToInt32(hIdCuenta.Value);

            CRE.CapaNegocio.clsCNCredito Credito = new CRE.CapaNegocio.clsCNCredito();
            var dtCredito = Credito.CNdtDataCreditoCobro(nNumCredito);
            ViewState["dtCredito"] = dtCredito;

            var dt = Credito.GetCancConDeuda(nNumCredito);
            txtCap.Text = dt.Rows[0]["NSALCAP"].ToString();
            txtInt.Text = dt.Rows[0]["NSALINT"].ToString();
            txtOtro.Text = dt.Rows[0]["NSALOTR"].ToString();
            txtMora.Text = dt.Rows[0]["NSALMOR"].ToString();
            txtTotal.Text = dt.Rows[0]["NSALTOT"].ToString();
            txtMot.Focus();
        }

        private void LimpiarDatos()
        {
            cboMot.SelectedIndex = -1;
            txtCap.Text = "";
            txtInt.Text = "";
            txtOtro.Text = "";
            txtMora.Text = "";
            txtTotal.Text = "";
            txtMot.Text = "";
            this.conBuscarCliente1.LimpiarControl();
            dtgCreditos.DataSource = null;
            dtgCreditos.DataBind();
        }

        protected void BotonCancelar1_Click(object sender, EventArgs e)
        {
            LimpiarDatos();
            conBuscarCliente1.Habilitar(true);
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

            if (hIdCuenta.Value == "" || hIdCuenta.Value=="0")
            {
                script.Mensaje("Debe asignar una cuenta valida");
                return;
            }
            if (cboMot.SelectedItem == null)
            {
                script.Mensaje("Debe seleccionar un motivo");
                return;
            }
            if (Convert.ToDouble(txtTotal.Text) < 0.0f)
            {
                script.Mensaje("El Monto a Pagar debe ser mayor a 0");
                return;
            }
            if (txtMot.Text.Length < 11)
            {
                script.Mensaje("Debe asignar una breve descripción para el Motivo (Mín. 10 caracteres)");
                return;
            }
            new CRE.CapaNegocio.clsCNCredito().CancConDeuda(Convert.ToInt32(hIdCuenta.Value), objUsuario.nIdAgencia, objUsuario.dFecSystem.ToString("yyyy-MM-dd"), Convert.ToInt32(cboMot.SelectedValue), objUsuario.idUsuario, txtMot.Text);
            script.Mensaje("Cancelación realizada exitosamente");
            LimpiarDatos();
            LiberarCuenta();
            this.BotonGrabar1.Enabled = false;
        }

        public void LiberarCuenta()
        {
            new GEN.CapaNegocio.clsCNRetornaNumCuenta().UpdEstCuenta(Convert.ToInt32(hIdCuenta.Value), 0);
            this.conBuscarCliente1.Habilitar(true);
        }

    }
}