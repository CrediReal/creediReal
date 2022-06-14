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
    public partial class frmAlmacen : System.Web.UI.Page
    {
        #region Variables Globales

        clsCNAlmacen cnalmacen = new clsCNAlmacen();
        clsWebJScript script = new clsWebJScript();
        clsCNUsuario cnusuario = new clsCNUsuario();

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["usuario"] != null)
            {
                Session["DatosUsuarioSession"] = new SGA.LogicaNegocio.clsCNUsuario().GetDatosUsuario(Request.QueryString["usuario"].ToString());
            }
            if (IsPostBack == true) return;

            lblOpcion.Text = Session["cOpcion"].ToString().ToUpper() + ":";

            hTipoOperacion.Value = "0"; //1:Nuevo;2:Edición

            //------------------Valida Session Vigente -------------------------------------->
            if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor"); }
            //------------------------------------------------------------------------------>
            cargarAlmacenes();
            cargarResponsables();
            habilitarControles(false);

            cargarDatosAlmacen();
        }

        protected void cboAlmacen_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarDatosAlmacen();
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
                    int idAlmacen = 0;                    
                    var cAlmacen = txtNombreAlmacen.Text.Trim();
                    var cDireccion = txtDireccion.Text.Trim();
                    var cReferencia = txtRefDirecc.Text.Trim();
                    var cTelefono = txtTelefono.Text.Trim();
                    var idResponsable = Convert.ToInt32(this.cboResponsable.SelectedValue);
                    var cCodExterno = txtCodExterno.Text.Trim();

                    if (hTipoOperacion.Value.Equals("2"))
                    {
                        idAlmacen = Convert.ToInt32(txtCodigoAlmacen.Text.Trim());
                        cnalmacen.ActualizarAlmacen(idAlmacen, cAlmacen, cDireccion, cReferencia, cTelefono, idResponsable, usuarioSession.idUsuario, true, cCodExterno);
                    }
                    else
                    {
                        cnalmacen.InsertarAlmacen(cAlmacen, cDireccion, cReferencia, cTelefono, idResponsable, usuarioSession.idUsuario, true, cCodExterno);
                    }

                    script.Mensaje("Los datos se registraron correctamente.");
                   
                    BotonEditar1.Visible = false;
                    BotonGrabar1.Visible = false;
                    BotonCancelar1.Visible = false;
                    BotonNuevo1.Visible = true;
                    cargarDatosAlmacen();
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
            if (cboAlmacen.Items.Count > 0)
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
            cargarDatosAlmacen();
            habilitarControles(false);
        }

        #endregion

        #region Metodos

        private void cargarAlmacenes()
        {
            var dtAlmacen = cnalmacen.ListarAlmacen();
            if (dtAlmacen.Rows.Count > 0)
            {
                cboAlmacen.DataSource = dtAlmacen;
                cboAlmacen.DataValueField = dtAlmacen.Columns[0].ToString();
                cboAlmacen.DataTextField = dtAlmacen.Columns[1].ToString();
                cboAlmacen.DataBind();
            }
            else
            {
                cboAlmacen.Enabled = false;
                BotonEditar1.Visible = false;
            }
        }

        private void LimpiarControles()
        {
            txtCodigoAlmacen.Text = "";
            txtDireccion.Text = "";
            txtNombreAlmacen.Text = "";
            txtRefDirecc.Text = "";
            txtTelefono.Text = "";
        }

        private void cargarResponsables()
        {
            var dtResponsables = cnusuario.ListarUsuarios();
            if (dtResponsables.Rows.Count > 0)
            {
                this.cboResponsable.DataSource = dtResponsables;
                cboResponsable.DataValueField = dtResponsables.Columns[0].ToString();
                cboResponsable.DataTextField = dtResponsables.Columns[1].ToString();
                cboResponsable.DataBind();
            }
            else
            {
                cboAlmacen.Enabled = false;
                BotonEditar1.Visible = false;
            }
        }

        private bool validar()
        {
            bool lval = false;

            if (string.IsNullOrEmpty(this.txtNombreAlmacen.Text))
            {
                script.Mensaje("Ingrese el nombre del almacén.");
                this.txtNombreAlmacen.Focus();
                return lval;
            }

            if (string.IsNullOrEmpty(this.txtDireccion.Text))
            {
                script.Mensaje("Ingrese la dirección del almacén.");
                this.txtDireccion.Focus();
                return lval;
            }

            if (string.IsNullOrEmpty(this.txtRefDirecc.Text))
            {
                script.Mensaje("Ingrese la referencia de la dirección.");
                this.txtRefDirecc.Focus();
                return lval;
            }

            if (string.IsNullOrEmpty(this.txtCodExterno.Text))
            {
                script.Mensaje("Ingrese el código externo del almacén.");
                this.txtCodExterno.Focus();
                return lval;
            }

            if (this.cboResponsable.SelectedIndex < 0)
            {
                script.Mensaje("Seleccione responsable de almacén");
                this.cboResponsable.Focus();
                return lval;
            }

            lval = true;
            return lval;
        }

        private void habilitarControles(bool lEstado)
        {
            txtNombreAlmacen.Enabled = lEstado;
            txtDireccion.Enabled = lEstado;
            txtRefDirecc.Enabled = lEstado;
            txtTelefono.Enabled = lEstado;
            cboResponsable.Enabled = lEstado;
            txtCodExterno.Enabled = lEstado;

        }

        private void cargarDatosAlmacen()
        {
            if (cboAlmacen.SelectedIndex >= 0)
            {
                if (cboAlmacen.Items.Count > 0)
                {
                    int idAlmacen = Convert.ToInt32(cboResponsable.SelectedValue);
                    var drAlmacen = cnalmacen.ListarAlmacenXid(idAlmacen).Rows[0];
                    txtCodigoAlmacen.Text = idAlmacen.ToString();
                    txtNombreAlmacen.Text = drAlmacen["cAlmacen"].ToString();
                    txtDireccion.Text = drAlmacen["cDireccion"].ToString();
                    txtRefDirecc.Text = drAlmacen["cRefDirec"].ToString();
                    txtTelefono.Text = drAlmacen["cFono"].ToString();
                    cboResponsable.SelectedValue = drAlmacen["idUsuRes"].ToString();
                    txtCodExterno.Text = drAlmacen["cCodExterno"].ToString();
                }

            }
        }

        #endregion
    }
}