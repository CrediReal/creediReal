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
    public partial class frmProveedor : System.Web.UI.Page
    {
        #region Variables Globales

        clsCNProveedor cnproveedor = new clsCNProveedor();
        clsWebJScript script = new clsWebJScript();
        clsCNUsuario cnusuario = new clsCNUsuario();

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == true) return;

            lblOpcion.Text = Session["cOpcion"].ToString().ToUpper() + ":";

            hTipoOperacion.Value = "0"; //1:Nuevo;2:Edición

            //------------------Valida Session Vigente -------------------------------------->
            if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor"); }
            //------------------------------------------------------------------------------>
            cargarProveedores();
            cargarTipoProveedor();
            habilitarControles(false);

            cargarDatosProveedor();
        }

        protected void cboProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarDatosProveedor();
        }

        protected void BotonNuevo1_Click(object sender, EventArgs e)
        {
            LimpiarControles();
            habilitarControles(true);
            BotonEditar1.Visible = false;
            BotonGrabar1.Visible = true;
            BotonCancelar1.Visible = true;
            BotonNuevo1.Visible = false;
            hTipoOperacion.Value = "1";
        }

        protected void BotonEditar1_Click(object sender, EventArgs e)
        {
            habilitarControles(true);
            BotonEditar1.Visible = false;
            BotonGrabar1.Visible = true;
            BotonCancelar1.Visible = true;
            BotonNuevo1.Visible = false;
            hTipoOperacion.Value = "2";
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
                    int idProveedor = 0;
                    var cDocumento = txtDocumento.Text.Trim();
                   var cProveedor=txtProveedor.Text.Trim();
                    var idTipoDocumento = Convert.ToInt32(this.cboTipoProveedor.SelectedValue);

                    if (hTipoOperacion.Value.Equals("2"))
                    {
                        idProveedor = Convert.ToInt32(txtCodigoProveedor.Text.Trim());
                        cnproveedor.ActualizarProveedor(idProveedor,cProveedor, cDocumento,  idTipoDocumento, usuarioSession.idUsuario,DateTime.Now, true);
                    }
                    else
                    {
                        cnproveedor.InsertarProveedor(cProveedor, cDocumento, idTipoDocumento, usuarioSession.idUsuario, DateTime.Now, true);
                    }

                    script.Mensaje("Los datos se registraron correctamente.");

                    BotonEditar1.Visible = false;
                    BotonGrabar1.Visible = false;
                    BotonCancelar1.Visible = false;
                    BotonNuevo1.Visible = true;
                    cargarDatosProveedor();
                    habilitarControles(false);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void BotonCancelar1_Click(object sender, EventArgs e)
        {
            if (this.cboProveedor.Items.Count > 0)
            {
                BotonEditar1.Visible = true;
            }
            else
            {
                BotonEditar1.Visible = false;
            }

            BotonGrabar1.Visible = false;
            BotonCancelar1.Visible = false;
            BotonNuevo1.Visible = true;
            cargarDatosProveedor();
            habilitarControles(false);
        }

        #endregion

        #region Metodos

        private void cargarProveedores()
        {
            var dtAlmacen = cnproveedor.ListarProveedor();
            if (dtAlmacen.Rows.Count > 0)
            {
                cboProveedor.DataSource = dtAlmacen;
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

        private void LimpiarControles()
        {
            txtCodigoProveedor.Text = "";
            txtDocumento.Text = "";
            txtProveedor.Text = "";
        }

        private void cargarTipoProveedor()
        {
            var dtTipoProveedor = cnproveedor.ListarTipoProveedor();
            if (dtTipoProveedor.Rows.Count > 0)
            {
                this.cboTipoProveedor.DataSource = dtTipoProveedor;
                cboTipoProveedor.DataValueField = dtTipoProveedor.Columns[0].ToString();
                cboTipoProveedor.DataTextField = dtTipoProveedor.Columns[1].ToString();
                cboTipoProveedor.DataBind();
            }
            else
            {
                cboProveedor.Enabled = false;
                BotonEditar1.Visible = false;
            }
        }

        private bool validar()
        {
            bool lval = false;

            if (string.IsNullOrEmpty(this.txtDocumento.Text))
            {
                script.Mensaje("Ingrese el número de documento.");
                this.txtDocumento.Focus();
                return lval;
            }
            if (string.IsNullOrEmpty(this.txtProveedor.Text))
            {
                script.Mensaje("Ingrese el nombre del proveedor.");
                this.txtProveedor.Focus();
                return lval;
            }

            if (this.cboTipoProveedor.SelectedIndex < 0)
            {
                script.Mensaje("Seleccione tipo de proveedor");
                this.cboTipoProveedor.Focus();
                return lval;
            }

            lval = true;
            return lval;
        }

        private void habilitarControles(bool lEstado)
        {
            txtDocumento.Enabled = lEstado;
            cboTipoProveedor.Enabled = lEstado;
            txtProveedor.Enabled = lEstado;
        }

        private void cargarDatosProveedor()
        {
            if (cboProveedor.SelectedIndex >= 0)
            {
                if (cboProveedor.Items.Count > 0)
                {
                    int idProveedor = Convert.ToInt32(this.cboProveedor.SelectedValue);
                    var drAlmacen = cnproveedor.ListarProveedorId(idProveedor).Rows[0];
                    txtCodigoProveedor.Text = idProveedor.ToString();
                    txtDocumento.Text = drAlmacen["cDocumento"].ToString();
                    cboTipoProveedor.SelectedValue = drAlmacen["idTipo"].ToString();
                    this.txtProveedor.Text = drAlmacen["cProveedor"].ToString();
                }

            }
        }

        #endregion
    }
}