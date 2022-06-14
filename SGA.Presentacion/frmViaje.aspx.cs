using SGA.ENTIDADES;
using SGA.LogicaNegocio;
using SGA.Utilitarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGA.Presentacion
{
    public partial class frmViaje : System.Web.UI.Page
    {
        #region Variables Globales

        clsCNProveedor cnproveedor = new clsCNProveedor();
        clsWebJScript script = new clsWebJScript();
        clsCNUsuario cnusuario = new clsCNUsuario();
        clsCNViaje cnviaje = new clsCNViaje();

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == true) return;

            lblOpcion.Text = Session["cOpcion"].ToString().ToUpper() + ":";

            hTipoOperacion.Value = "0"; //1:Nuevo;2:Edición

            //------------------Valida Session Vigente -------------------------------------->
            if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor"); }
            //------------------------------------------------------------------------------>
            cargarProveedores();
            cargarEstados();
        }

        private void cargarProveedores()
        {
            var dtProveedor = cnproveedor.ListarProveedor();
            if (dtProveedor.Rows.Count > 0)
            {
                cboProveedor.DataSource = dtProveedor;
                cboProveedor.DataValueField = "idProveedor";
                cboProveedor.DataTextField = "cProveedor";
                cboProveedor.DataBind();
            }
            else
            {
                cboProveedor.Enabled = false;
                BotonEditar1.Visible = false;
            }
        }

        private void cargarEstados()
        {
            var dtEstado = cnviaje.ListarEstadoViaje();
            if (dtEstado.Rows.Count > 0)
            {
                this.cboEstado.DataSource = dtEstado;
                cboEstado.DataValueField = "idEstadoViaje";
                cboEstado.DataTextField = "cEstadoViaje";
                cboEstado.DataBind();
            }
            else
            {
                cboEstado.Enabled = false;
                BotonEditar1.Visible = false;
            }
        }

        private void LimpiarControles()
        {
            txtNumViaje.Text = "";
            txtDestino.Text = "";
        }

        protected void BotonNuevo1_Click(object sender, EventArgs e)
        {
            if (Session["idCliente"]==null)
            {
                script.Mensaje("Seleccione a un cliente para el registro de viaje.");
                return;
            }

            habilitarControles(true);
            cboEstado.Enabled = false;

            LimpiarControles();
            BotonEditar1.Visible = false;
            BotonGrabar1.Visible = true;
            BotonCancelar1.Visible = true;
            BotonNuevo1.Visible = false;
            hTipoOperacion.Value = "1";
            conBuscarCliente1.Habilitar(false);
        }

        private void habilitarControles(bool lEstado)
        {
            txtNumViaje.Enabled = lEstado;
            txtDestino.Enabled = lEstado;
            cboEstado.Enabled = lEstado;
            cboProveedor.Enabled = lEstado;
        }

        protected void BotonCancelar1_Click(object sender, EventArgs e)
        {            
            BotonEditar1.Visible = false;
            BotonGrabar1.Visible = false;
            BotonCancelar1.Visible = false;
            BotonNuevo1.Visible = true;
            habilitarControles(false);
            conBuscarCliente1.Habilitar(true);
            conBuscarCliente1.LimpiarControl();
            conBuscarCliente1.ActivarBusqueda(true);
            Session["idCliente"] = null;
        }

        protected void BotonEditar1_Click(object sender, EventArgs e)
        {

        }

        protected void BotonGrabar1_Click(object sender, EventArgs e)
        {
            try
            {
                //------------------Valida Session Vigente -------------------------------------->
                if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor"); }
                //------------------------------------------------------------------------------>
                var usuarioSession = (clsUsuario)Session["DatosUsuarioSession"];

                if (validar())
                {
                    int idviaje = 0;
                    var nNumViaje = Convert.ToInt32(txtNumViaje.Text);
                    var idProveedor = Convert.ToInt32(cboProveedor.SelectedValue);
                    var idCliente=Convert.ToInt32(Session["idCliente"]);

                    if (hTipoOperacion.Value.Equals("2"))
                    {
                       // idviaje = Convert.ToInt32(txtCodigoProveedor.Text.Trim());
                        cnviaje.ActualizarViaje(idviaje,nNumViaje, 1, idProveedor, idCliente, txtDestino.Text, usuarioSession.idUsuario, DateTime.Now, true);
                    }
                    else
                    {
                        cnviaje.InsertarViaje(nNumViaje, 1, idProveedor, idCliente, txtDestino.Text, usuarioSession.idUsuario, DateTime.Now, true);
                    }

                    script.Mensaje("Los datos se registraron correctamente.");

                    BotonEditar1.Visible = false;
                    BotonGrabar1.Visible = false;
                    BotonCancelar1.Visible = false;
                    BotonNuevo1.Visible = true;
                    habilitarControles(false);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private bool validar()
        {
            bool lval = false;

            if (string.IsNullOrEmpty(this.txtNumViaje.Text))
            {
                script.Mensaje("Ingrese el número de viaje.");
                this.txtNumViaje.Focus();
                return lval;
            }
            if (string.IsNullOrEmpty(this.txtDestino.Text))
            {
                script.Mensaje("Ingrese el destino de viaje.");
                this.txtDestino.Focus();
                return lval;
            }

            var dtExisteViaje = cnviaje.ListarViajeNroViaje(Convert.ToInt32(txtNumViaje.Text));
            if (dtExisteViaje.Rows.Count > 0)
            {
                script.Mensaje("El número de viaje ya fue registrado.");
                this.txtNumViaje.Focus();
                return lval;
            }

            lval = true;
            return lval;
        }

    }
}