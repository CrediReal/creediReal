using SGA.ENTIDADES;
using EntityLayer;
using SGA.LogicaNegocio;
using SGA.Utilitarios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGA.Presentacion.AHORROS
{
    public partial class frmAperturaCuentaAhorro : System.Web.UI.Page
    {
        clsWebJScript script = new clsWebJScript();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //
            }
            catch (Exception ex)
            {
                this.script.Mensaje("Error al cargar formulario" + ex.Message);
            }
        }

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                RegistrarCuenta();
            }
            catch (Exception ex)
            {
                this.script.Mensaje("Error al intentar registrar la cuenta: " + ex.Message);
            }
        }

        private void RegistrarCuenta()
        {
            SGA.ENTIDADES.clsUsuario objUsuario;
            if (Session["DatosUsuarioSession"] == null)
            {
                Session["DatosUsuarioSession"] = new clsCNUsuario().GetDatosUsuario(hUsuario.Value);
                objUsuario = (SGA.ENTIDADES.clsUsuario)Session["DatosUsuarioSession"];
            }
            else
            {
                objUsuario = (SGA.ENTIDADES.clsUsuario)Session["DatosUsuarioSession"];
            }

            if (Session["idCliente"] == null)
            {
                this.script.Mensaje("Debe seleccionar un cliente");
                return;
            }
            var idCliente = Convert.ToInt32(Session["idCliente"]);

            if (this.txtMonto.Text == "")
            {
                btnGrabar.Enabled = false;
                this.script.Mensaje("Ingrese Monto en soles para la apertura");
                return;
            }

            AHO.CapaNegocio.AHO.AHOCNCuentaAhorros oCNCuentaAhorros = new AHO.CapaNegocio.AHO.AHOCNCuentaAhorros();
            
            //seteamos variables para el registro
            
            int idCli = conBuscarCliente.idCliente;
            int idTipoCuenta = Convert.ToInt32(cbTipoCuenta.SelectedValue.ToString());            
            double montoApertura = Convert.ToDouble(txtMonto.Text);
            double interesPactado = 0.00;
            int plazoDias = Convert.ToInt32(txtPlazo.Text);
            DataTable TablaRegistroCuentaAhorros = oCNCuentaAhorros.RegistrarAperturaCuentaPLazoFijo(idCliente, idTipoCuenta, montoApertura, interesPactado, plazoDias);
            this.script.Mensaje("Registro correcto. Cuenta de ahorros N°: " + TablaRegistroCuentaAhorros.Rows[0][0].ToString());
            //this..Enabled = false;
        }

        protected void BotonConsultar1_Click(object sender, EventArgs e)
        {
            try
            {
            
                SGA.ENTIDADES.clsUsuario objUsuario;
                if (Session["DatosUsuarioSession"] == null)
                {
                    Session["DatosUsuarioSession"] = new clsCNUsuario().GetDatosUsuario(hUsuario.Value);
                    objUsuario = (SGA.ENTIDADES.clsUsuario)Session["DatosUsuarioSession"];
                }
                else
                {

                    objUsuario = (SGA.ENTIDADES.clsUsuario)Session["DatosUsuarioSession"];
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
            }
            catch (Exception ex)
            {
                this.script.Mensaje("Error al intentar registrar la cuenta: " + ex.Message);
            }
            //DataTable dtDatosCuentaSolCliente = RetornaCuentaSolCliente.RetornaCuentaSolCliente(idCliente, "C", "[5]");
        }
    }
}