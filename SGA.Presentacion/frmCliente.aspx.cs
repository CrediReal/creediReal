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
    public partial class frmCliente : System.Web.UI.Page
    {
        #region Variables Globales

        clsWebJScript script = new clsWebJScript();
        clsCNUsuario cnusuario = new clsCNUsuario();
        clsCNCliente cncliente = new clsCNCliente();
        clsCNDocumento cndocumento = new clsCNDocumento();
        clsCNPersona cnpersona = new clsCNPersona();
        clsCNUbigeo cnubigeo = new clsCNUbigeo();
        clsCNOficina cnoficina = new clsCNOficina();
        clsCNTipo cntipo = new clsCNTipo();

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == true) return;

            lblOpcion.Text = Session["cOpcion"].ToString().ToUpper() + ":";

            hTipoOperacion.Value = "0"; //1:Nuevo;2:Edición

            //------------------Valida Session Vigente -------------------------------------->
            if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor"); }
            //------------------------------------------------------------------------------>
            cargarControles();
            habilitarControles(false);
            calFechaNac.SeleccionarFecha = DateTime.Now.Date;
        }

        private void cargarControles()
        {
            var dtTipoPersona = cnpersona.ListarTipoPersona();
            cboTipoPersona.DataSource = dtTipoPersona;
            cboTipoPersona.DataValueField = "idTipoPersona";
            cboTipoPersona.DataTextField = "cTipoPersona";
            cboTipoPersona.DataBind();
            
            var dtTipoDocumento = cndocumento.ListarTipoDocumento();
            cboTipoDocumento.DataSource = dtTipoDocumento;
            cboTipoDocumento.DataValueField = "idTipoDocumento";
            cboTipoDocumento.DataTextField = "cTipoDocumento";
            cboTipoDocumento.DataBind();

            cargarDepartamentos();
            cboDepartamento.SelectedValue = "11";
            cargarProvincias();
            cargarDistritos();
            cargarDepartamentosAdi();
            cboDepartamentoAdi.SelectedValue = "11";
            cargarProvinciasAdi();
            cargarDistritosAdi();
            cargarAsesores();
            cargarOficinas();
            cargarTipo();
        }

        protected void BotonNuevo1_Click(object sender, EventArgs e)
        {
            this.conBuscarCliente1.Ocultar(true);
            pnlDetalle.Visible = true;
            LimpiarControles();
            habilitarControles(true);
            BotonEditar1.Visible = false;
            BotonGrabar1.Visible = true;
            BotonCancelar1.Visible = true;
            BotonNuevo1.Visible = false;
            hTipoOperacion.Value = "1"; 
            pnlDetalle.Visible = true;
        }

        protected void BotonEditar1_Click(object sender, EventArgs e)
        {
            if (Session["idCliente"]==null)
            {
                return;
            }
            var idCliente = Convert.ToInt32(Session["idCliente"]);
            if (idCliente == 0)
            {
                return;
            }
            this.conBuscarCliente1.Ocultar(true);
            habilitarControles(true);
            BotonEditar1.Visible = false;
            BotonGrabar1.Visible = true;
            BotonCancelar1.Visible = true;
            BotonNuevo1.Visible = false;
            hTipoOperacion.Value = "2";
            pnlDetalle.Visible = true;

            var dtDatosCli = cncliente.ListarClienteid(idCliente);
            if (dtDatosCli.Rows.Count == 1)
            {
                this.txtNombres.Text = dtDatosCli.Rows[0]["cNombres"].ToString().Trim();
                this.txtDocumento.Text = dtDatosCli.Rows[0]["cDocumento"].ToString().Trim();
                this.txtDireccion.Text = dtDatosCli.Rows[0]["cDireccion"].ToString().Trim();
                this.txtNombre.Text = dtDatosCli.Rows[0]["cNombre"].ToString().Trim();
                this.txtApePaterno.Text = dtDatosCli.Rows[0]["cApellidoPaterno"].ToString().Trim();
                this.txtApeMaterno.Text = dtDatosCli.Rows[0]["cApellidoMaterno"].ToString().Trim();
                this.txtdocumentoAdi.Text = dtDatosCli.Rows[0]["cDocumentoAdicional"].ToString().Trim();
                this.calFechaNac.SeleccionarFecha = Convert.ToDateTime(dtDatosCli.Rows[0]["dFechaNac"]);
                this.cboTipoDocumento.SelectedValue = dtDatosCli.Rows[0]["idTipoDocumento"].ToString().Trim();
                this.cboTipoPersona.SelectedValue = dtDatosCli.Rows[0]["idTipoPersona"].ToString().Trim();
                this.txtEmail.Text = dtDatosCli.Rows[0]["cCorreo"].ToString().Trim();
                this.txtTelefonos.Text = dtDatosCli.Rows[0]["cTelefono"].ToString().Trim();

                txtEmailAdi.Text = dtDatosCli.Rows[0]["cCorreoAdi"].ToString().Trim();
                txtDireccionAdi.Text = dtDatosCli.Rows[0]["cDireccionAdi"].ToString().Trim();

                cboDepartamento.SelectedValue = dtDatosCli.Rows[0]["cUbigeo"].ToString().Substring(0, 2);
                cargarProvincias();
                cboProvincia.SelectedValue = dtDatosCli.Rows[0]["cUbigeo"].ToString().Substring(2, 2);
                cargarDistritos();
                cboDistrito.SelectedValue = dtDatosCli.Rows[0]["cUbigeo"].ToString().Substring(4, 2);

                cboDepartamentoAdi.SelectedValue = dtDatosCli.Rows[0]["cUbigeoAdi"].ToString().Substring(0, 2);
                cargarProvinciasAdi();
                cboProvinciaAdi.SelectedValue = dtDatosCli.Rows[0]["cUbigeoAdi"].ToString().Substring(2, 2);
                cargarDistritosAdi();
                cboDistritoAdi.SelectedValue = dtDatosCli.Rows[0]["cUbigeoAdi"].ToString().Substring(4, 2);

                txtTelefonosAdi.Text = dtDatosCli.Rows[0]["cTelefonoAdi"].ToString().Trim();
                cboAsesor.SelectedValue = dtDatosCli.Rows[0]["idAsesor"].ToString();
                txtContacto.Text = dtDatosCli.Rows[0]["cContacto"].ToString().Trim();
                cboOficina.SelectedValue = dtDatosCli.Rows[0]["idOficina"].ToString();
                txtCargo.Text = dtDatosCli.Rows[0]["cCargoContacto"].ToString().Trim();
                cboTipo.SelectedValue = dtDatosCli.Rows[0]["idTipo"].ToString();

                if (cboTipoPersona.SelectedValue == "1")
                {
                    ActivarPersonaNatural(true);
                    lblNombres.Text = "Nombres";
                    lblFechaNac.Text = "Fecha de Nacimiento";
                }
                else
                {
                    ActivarPersonaNatural(false);
                    lblNombres.Text = "Razón Social";
                    lblFechaNac.Text = "Fecha de constitución";
                }
            }
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
                    int idClienteAct = Convert.ToInt32(Session["idCliente"]);
                    string cNombres = "", cNombre = "", cApellidoPaterno = "", cApellidoMaterno = "";
                    if (cboTipoPersona.SelectedValue == "1")
                    {
                        cNombres = txtApePaterno.Text.Trim() + " " + txtApeMaterno.Text.Trim() + " " + txtNombre.Text.Trim();
                        cNombre = txtNombre.Text.Trim();
                        cApellidoPaterno = txtApePaterno.Text.Trim();
                        cApellidoMaterno = txtApeMaterno.Text.Trim();
                    }
                    if (cboTipoPersona.SelectedValue == "2")
                    {
                        cNombres = txtNombres.Text.Trim();
                    }

                    var idTipoPersona = Convert.ToInt32(cboTipoPersona.SelectedValue);
                    var idTipoDocumento = Convert.ToInt32(cboTipoDocumento.SelectedValue);
                    var cDocumento = txtDocumento.Text.Trim();
                    var cDocumentoAdicional = txtdocumentoAdi.Text.Trim();
                    var dFechaNac = calFechaNac.SeleccionarFecha;
                    var cTelefono = txtTelefonos.Text.Trim();
                    var cCorreo = txtEmail.Text.Trim();
                    var cDireccion = txtDireccion.Text.Trim();
                    var idUsuario = usuarioSession.idUsuario;
                    var dFechaReg = DateTime.Now;
                    var lVigente = true;
                    var cCorreoAdi=txtEmailAdi.Text.Trim();
                    var cDireccionAdi=txtDireccionAdi.Text.Trim();
                    var cUbigeo = cboDepartamento.SelectedValue + cboProvincia.SelectedValue + cboDistrito.SelectedValue;
                    var cUbigeoAdi = cboDepartamentoAdi.SelectedValue + cboProvinciaAdi.SelectedValue + cboDistritoAdi.SelectedValue;
                    var cTelefonoAdi = txtTelefonosAdi.Text.Trim();
                    var idAsesor = Convert.ToInt32(cboAsesor.SelectedValue);
                    var cContacto = txtContacto.Text.Trim();
                    var cCargoContacto = txtCargo.Text.Trim();
                    var idOficina = Convert.ToInt32(cboOficina.SelectedValue == "" ? "0" : cboOficina.SelectedValue);
                    var idTipo = Convert.ToInt32(cboTipo.SelectedValue);

                    if (hTipoOperacion.Value.Equals("1"))
                    {
                        cncliente.InsertarCliente(cNombres, idTipoPersona, idTipoDocumento, cDocumento, cDocumentoAdicional, cNombre,
                            cApellidoPaterno, cApellidoMaterno, dFechaNac, cTelefono, cCorreo, cDireccion, idUsuario, dFechaReg, lVigente,
                            cCorreoAdi, cDireccionAdi, cUbigeo, cUbigeoAdi, cTelefonoAdi, idAsesor, cContacto, idOficina, cCargoContacto, idTipo);
                    }
                    else
                    {
                        //------------------Valida idCliente Vigente -------------------------------------->
                        if (Session["idCliente"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor"); }
                        //------------------------------------------------------------------------------>
                        cncliente.ActualizarCliente(idClienteAct,cNombres, idTipoPersona, idTipoDocumento, cDocumento, cDocumentoAdicional, cNombre,
                         cApellidoPaterno, cApellidoMaterno, dFechaNac, cTelefono, cCorreo, cDireccion, idUsuario, dFechaReg, lVigente,
                         cCorreoAdi, cDireccionAdi, cUbigeo, cUbigeoAdi, cTelefonoAdi, idAsesor, cContacto, idOficina, cCargoContacto, idTipo);
                    
                    }

                    script.Mensaje("Los datos se registraron correctamente.");

                    BotonEditar1.Visible = false;
                    BotonGrabar1.Visible = false;
                    BotonCancelar1.Visible = true;
                    BotonNuevo1.Visible = true;
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
            this.conBuscarCliente1.ActivarBusqueda(true);
            BotonEditar1.Visible = true;
            BotonGrabar1.Visible = false;
            BotonCancelar1.Visible = false;
            BotonNuevo1.Visible = true;
            LimpiarControles();
            habilitarControles(false);
            pnlDetalle.Visible = false;
            Session["idCliente"] = 0;
        }

        private void LimpiarControles()
        {
            this.txtApeMaterno.Text = "";
            this.txtApePaterno.Text = "";
            this.txtDireccion.Text = "";
            this.txtDocumento.Text = "";
            this.txtdocumentoAdi.Text = "";
            this.txtEmail.Text = "";
            this.txtNombre.Text = "";
            this.txtNombres.Text = "";
            this.txtTelefonos.Text = "";
            txtEmailAdi.Text = "";
            txtTelefonosAdi.Text = "";
            txtDireccionAdi.Text = "";
            txtContacto.Text = "";
            txtCargo.Text = "";
            calFechaNac.SeleccionarFecha = DateTime.Now.Date;
            cboTipo.SelectedValue = "1";
            cboOficina.SelectedValue = "1";            
        }

        private void habilitarControles(bool lEstado)
        {
            this.txtApeMaterno.Enabled = lEstado;
            this.txtApePaterno.Enabled = lEstado;
            this.txtDireccion.Enabled = lEstado;
            this.txtDocumento.Enabled = lEstado;
            this.txtdocumentoAdi.Enabled = lEstado;
            this.txtEmail.Enabled = lEstado;
            this.txtNombre.Enabled = lEstado;
            //this.txtNombres.Enabled = lEstado;
            this.txtTelefonos.Enabled = lEstado;
            this.cboTipoDocumento.Enabled = lEstado;
            this.cboTipoPersona.Enabled = lEstado;
            this.calFechaNac.Enabled = lEstado;
            this.txtEmailAdi.Enabled = lEstado;
            this.txtDireccionAdi.Enabled = lEstado;
            this.txtTelefonosAdi.Enabled = lEstado;
            this.cboDepartamento.Enabled = lEstado;
            this.cboProvincia.Enabled = lEstado;
            this.cboDistrito.Enabled = lEstado;
            this.txtContacto.Enabled = lEstado;
            this.txtCargo.Enabled = lEstado;
            this.cboDepartamentoAdi.Enabled = lEstado;
            this.cboProvinciaAdi.Enabled = lEstado;
            this.cboDistritoAdi.Enabled = lEstado;
            this.cboAsesor.Enabled = lEstado;
            this.cboOficina.Enabled = lEstado;
            this.cboTipo.Enabled = lEstado;
        }

        private void ActivarPersonaNatural(bool lEstado)
        {
            lblNombre.Visible = lEstado;
            lblApeParteno.Visible = lEstado;
            lblMaterno.Visible = lEstado;
            txtNombre.Visible = lEstado;
            txtApeMaterno.Visible = lEstado;
            txtApePaterno.Visible = lEstado;
            
        }

        protected void cboTipoPersona_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTipoPersona.SelectedValue=="1")
            {
                ActivarPersonaNatural(true);
                lblNombres.Text = "Nombres";
                lblFechaNac.Text = "Fecha de Nacimiento";
                cboTipoDocumento.SelectedValue = "1";
                txtNombres.Enabled = false;
            }
            else
            {
                ActivarPersonaNatural(false);
                lblNombres.Text = "Razón Social";
                lblFechaNac.Text = "Fecha de constitución";
                cboTipoDocumento.SelectedValue = "2";
                txtNombres.Enabled = true;
            }
            LimpiarControles();
        }

        private bool validar()
        {
            bool lval = false;

            if (cboTipoPersona.SelectedValue=="1")
            {
                if (string.IsNullOrEmpty(this.txtNombre.Text))
                {
                    script.Mensaje("Ingrese el nombre.");
                    this.txtApeMaterno.Focus();
                    return lval;
                }

                if (string.IsNullOrEmpty(this.txtApePaterno.Text))
                {
                    script.Mensaje("Ingrese el apellido paterno.");
                    this.txtApePaterno.Focus();
                    return lval;
                }

                if (string.IsNullOrEmpty(this.txtApeMaterno.Text))
                {
                    script.Mensaje("Ingrese el apellido materno.");
                    this.txtApeMaterno.Focus();
                    return lval;
                }
            }

            if (cboTipoPersona.SelectedValue == "2")
            {
                if (string.IsNullOrEmpty(this.txtNombres.Text))
                {
                    script.Mensaje("Ingrese la razón social.");
                    this.txtNombres.Focus();
                    return lval;
                }
            }

            if (string.IsNullOrEmpty(this.txtDocumento.Text))
            {
                script.Mensaje("Ingrese el documento.");
                this.txtDocumento.Focus();
                return lval;
            }

            if (string.IsNullOrEmpty(this.txtDireccion.Text))
            {
                script.Mensaje("Ingrese la dirección.");
                this.txtDireccion.Focus();
                return lval;
            }

            if (cboAsesor.Items.Count == 0)
            {
                script.Mensaje("No existe Asesores de ventas confgurados, comuníquese con el administrador del sistema");
                return lval;
            }

            lval = true;
            return lval;
        }

        protected void cboDepartamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarProvincias();
            cargarDistritos();
        }

        protected void cboProvincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarDistritos();
        }

        private void cargarDepartamentos()
        {
            var dtDepartamentos = cnubigeo.ListarDepartamento();
            cboDepartamento.DataSource = dtDepartamentos;
            cboDepartamento.DataValueField = dtDepartamentos.Columns[0].ColumnName;
            cboDepartamento.DataTextField = dtDepartamentos.Columns[1].ColumnName;
            cboDepartamento.DataBind();
        }

        private void cargarProvincias()
        {
            var dtProvincias = cnubigeo.ListarProvincia(cboDepartamento.SelectedValue);
            cboProvincia.DataSource = dtProvincias;
            cboProvincia.DataValueField = dtProvincias.Columns[0].ColumnName;
            cboProvincia.DataTextField = dtProvincias.Columns[1].ColumnName;
            cboProvincia.DataBind();
        }

        private void cargarDistritos()
        {
            var dtdistrito = cnubigeo.ListarDistrito(cboDepartamento.SelectedValue,cboProvincia.SelectedValue);
            cboDistrito.DataSource = dtdistrito;
            cboDistrito.DataValueField = dtdistrito.Columns[0].ColumnName;
            cboDistrito.DataTextField = dtdistrito.Columns[1].ColumnName;
            cboDistrito.DataBind();
        }

        private void cargarAsesores()
        {
            var dtAsesores = cnusuario.ListarUsuariosXPerfil(1);
            cboAsesor.DataSource = dtAsesores;
            cboAsesor.DataValueField = dtAsesores.Columns[0].ColumnName;
            cboAsesor.DataTextField = dtAsesores.Columns[1].ColumnName;
            cboAsesor.DataBind();
        }

        private void cargarDepartamentosAdi()
        {
            var dtDepartamentosAdi = cnubigeo.ListarDepartamento();
            cboDepartamentoAdi.DataSource = dtDepartamentosAdi;
            cboDepartamentoAdi.DataValueField = dtDepartamentosAdi.Columns[0].ColumnName;
            cboDepartamentoAdi.DataTextField = dtDepartamentosAdi.Columns[1].ColumnName;
            cboDepartamentoAdi.DataBind();
        }

        private void cargarProvinciasAdi()
        {
            var dtProvinciasAdi = cnubigeo.ListarProvincia(cboDepartamentoAdi.SelectedValue);
            cboProvinciaAdi.DataSource = dtProvinciasAdi;
            cboProvinciaAdi.DataValueField = dtProvinciasAdi.Columns[0].ColumnName;
            cboProvinciaAdi.DataTextField = dtProvinciasAdi.Columns[1].ColumnName;
            cboProvinciaAdi.DataBind();
        }

        private void cargarDistritosAdi()
        {
            var dtdistritoAdi = cnubigeo.ListarDistrito(cboDepartamentoAdi.SelectedValue, cboProvinciaAdi.SelectedValue);
            cboDistritoAdi.DataSource = dtdistritoAdi;
            cboDistritoAdi.DataValueField = dtdistritoAdi.Columns[0].ColumnName;
            cboDistritoAdi.DataTextField = dtdistritoAdi.Columns[1].ColumnName;
            cboDistritoAdi.DataBind();
        }

        private void cargarOficinas()
        {
            cboOficina.DataSource = cnoficina.ListarOficinas(0);
            cboOficina.DataValueField = "idOficina";
            cboOficina.DataTextField = "cNomOficina";
            cboOficina.DataBind();
        }

        private void cargarTipo()
        {
            cboTipo.DataSource = this.cntipo.ListarTipos(0,"",true);
            cboTipo.DataValueField = "idTipo";
            cboTipo.DataTextField = "cTipo";
            cboTipo.DataBind();
        }

        protected void cboDepartamentoAdi_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarProvinciasAdi();
            cargarDistritosAdi();
        }

        protected void cboProvinciaAdi_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarDistritosAdi();
        }
    }
}