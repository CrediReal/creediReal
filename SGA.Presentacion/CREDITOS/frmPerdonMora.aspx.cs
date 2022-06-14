using SGA.ENTIDADES;
using SGA.LogicaNegocio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace SGA.Presentacion.CREDITOS
{
    public partial class frmPerdonMora : System.Web.UI.Page
    {
        SGA.Utilitarios.clsWebJScript script = new Utilitarios.clsWebJScript();
        CRE.CapaNegocio.clsCNCredito cncredito = new CRE.CapaNegocio.clsCNCredito();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["usuario"] != null)
            {
                Session["DatosUsuarioSession"] = new clsCNUsuario().GetDatosUsuario(Request.QueryString["usuario"].ToString());
            }
            if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor " + this.ToString()); }

            if (IsPostBack) return;
            hUsuario.Value = ((clsUsuario)Session["DatosUsuarioSession"]).cUsuario;
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            buscarCreditos();
        }

        private void buscarCreditos()
        {
            if (Session["idCliente"] == null)
            {
                return;
            }
            var idCliente = Convert.ToInt32(Session["idCliente"]);
            if (idCliente == 0)
            {
                return;
            }
            hidCli.Value = idCliente.ToString();

            var dtCreditos = cncredito.CNdtLisCrexCli(idCliente);

            var dtCreActivo = dtCreditos.AsEnumerable().Where(x => x["cEstado"].ToString() == "ACTIVO").CopyToDataTable();
            GridViewUser.DataSource = dtCreActivo;
            GridViewUser.DataBind();
        }

        protected void lnkAccion_Click(object sender, EventArgs e)
        {
            var idCuenta = ((LinkButton)sender).CommandArgument;
            hidCuenta.Value = idCuenta;
            var datos = cncredito.CNdtDataCreditoCobro(Convert.ToInt32(idCuenta));

            divRegistro.Visible = true;
            txtMoraGenerado.Text = datos.Rows[0]["nMoraGenerado"].ToString();
            txtMoraPagada.Text = datos.Rows[0]["nMoraPagada"].ToString();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            var nSaldoMora = Convert.ToDecimal(txtMoraGenerado.Text) - Convert.ToDecimal(txtMoraPagada.Text);
            if (txtMoraDescontar.Value>(double)nSaldoMora)
            {
                script.Mensaje("La mora a descontar debe de ser menor al saldo de mora");
                return;
            }

            SGA.LogicaNegocio.clsCNCredito cncre = new clsCNCredito();
            cncre.RegPerdonMora(Convert.ToInt32(hidCuenta.Value), Convert.ToDecimal(txtMoraDescontar.Value));

            buscarCreditos();
            hidCuenta.Value = "0";
            txtMoraGenerado.Text = "0.00";
            txtMoraPagada.Text = "0.00";
            txtMoraDescontar.Text = "0.00";
            divRegistro.Visible = false;

            script.Mensaje("Los datos se actualizaron correctamente");

           
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            hidCuenta.Value = "0";         
            txtMoraGenerado.Text = "0.00";
            txtMoraPagada.Text = "0.00";
            txtMoraDescontar.Text = "0.00";
            divRegistro.Visible = false;
        }
    }
}