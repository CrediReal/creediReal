using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GEN.CapaNegocio;
using SGA.ENTIDADES;
using SGA.Utilitarios;

namespace SGA.Presentacion.CREDITOS
{
    public partial class frmDesbCtas : System.Web.UI.Page
    {
        GEN.CapaNegocio.clsCNRetornsCuentaSolCliente RetornaCuentaSolCliente = new GEN.CapaNegocio.clsCNRetornsCuentaSolCliente();
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
        }

        protected void BotonConsultar1_Click(object sender, EventArgs e)
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
                this.btnLiberar.Enabled = false;
                this.LimpiarDatos();
                return;
            }
            int nNumCredito = Convert.ToInt32(hIdCuenta.Value);

            if (nNumCredito <= 0)
            {
                this.btnLiberar.Enabled = false;
                this.LimpiarDatos();
                return;
            }

            else if (nNumCredito > 0)
            {
                clsCNRetornaNumCuenta RetornaNumCuenta = new clsCNRetornaNumCuenta();
                DataTable dtEstCuenta = RetornaNumCuenta.CNVerifEstCuentaGen(Convert.ToInt32(hIdCuenta.Value), 1);
                if (dtEstCuenta.Rows.Count >0)
                {
                    DataTable dtUsu = RetornaNumCuenta.BusUsuBlo((int)dtEstCuenta.Rows[0]["nIdUsuBloq"]);
                    if (dtUsu.Rows.Count > 0)
                    {
                        txtUsuarioBlo.Text = dtUsu.Rows[0]["cNombre"].ToString();
                        this.txtAgeBlo.Text = dtUsu.Rows[0]["cNombreAge"].ToString();
                        this.btnLiberar.Enabled = true;
                        this.btnCancelar1.Enabled = true;
                    }
                    else
                    {
                        script.Mensaje("Cuenta no se encuentra Bloqueada.");
                    }
                }
                else
                {
                    script.Mensaje("Cuenta no se encuentra Bloqueada.");
                }
            }

        }

        private void LimpiarDatos()
        {
            this.conBuscarCliente1.LimpiarControl();
            dtgCreditos.DataSource = null;
            dtgCreditos.DataBind();
            this.btnLiberar.Enabled = false;
            hIdCuenta.Value = "";
            txtAgeBlo.Text = "";
            txtUsuarioBlo.Text = "";
        }

        protected void btnLiberar_Click(object sender, EventArgs e)
        {
            clsCNRetornaNumCuenta RetornaNumCuenta = new clsCNRetornaNumCuenta();
            RetornaNumCuenta.CNDesbloqueaCuenta(Convert.ToInt32(hIdCuenta.Value), 1);            
            LimpiarDatos();
            this.btnLiberar.Enabled = false;
            this.btnCancelar1.Enabled = false;
            conBuscarCliente1.Habilitar(true);
            script.Mensaje("Cuenta de crédito desbloqueada.");
        }

        protected void BotonCancelar1_Click(object sender, EventArgs e)
        {
            LimpiarDatos();
            conBuscarCliente1.Habilitar(true);
        }
    }
}