using SGA.ENTIDADES;
using SGA.LogicaNegocio;
using SGA.Utilitarios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGA.Presentacion
{
    public partial class frmUsuario : System.Web.UI.Page
    {
        #region Variables_Globales

        clsCNUsuario Usuario = new clsCNUsuario();
        clsWebJScript Script = new clsWebJScript();
        clsCNPerfil Perfil = new clsCNPerfil();
        clsCNSexo Sexo = new clsCNSexo();
        clsCNUbigeo cnubigeo = new clsCNUbigeo();

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (IsPostBack == true) return;

                //----------------- TITULO ------------------------>
                lblOpcion.Text = Session["cOpcion"].ToString().ToUpper() + ":";
                //-------------------------------------------------->
                pnlDetalle.Visible = false;

                hTipoOperacion.Value = "0"; //1:Nuevo
                //2:Edición

                //------------------Valida Session Vigente -------------------------------------->
                if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor"); }
                //------------------------------------------------------------------------------>


                //Cargar el combo de PERFILES---------------->
                DataTable dPerfil = Perfil.ListarPerfilesVigentes();

                if (dPerfil.Rows.Count == 0)
                {
                    Script.Mensaje("No existen perfiles ..");
                    BotonConsultar1.Visible = false;
                    BotonNuevo1.Visible = false;
                    return;
                }
                //------------------------------------------->

                //-- Cargar Proyectos a los que está relacionado el ADMINISTRADOR --------------> 
                var usuarioSession = (clsUsuario)Session["DatosUsuarioSession"];
                int idPerfil = usuarioSession.idPerfil;

      
                cboPerfil.DataSource = dPerfil;

                cboPerfil.DataValueField = dPerfil.Columns[0].ToString(); //idPerfil
                cboPerfil.DataTextField = dPerfil.Columns[1].ToString();  //cPerfil
                cboPerfil.DataBind();
                //--------------------------------------------->

                txtNombreUsuario.Focus();

                BotonConsultar1.Visible = true;
                BotonNuevo1.Visible = true;

                cargarListaSexo();
                cargarDepartamentos();
                cargarProvincias();
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void BotonConsultar1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtNombreUsuario.Text))
                {
                    Script.Mensaje("Debe Ingresar Apellidos de Usuario a Buscar.");
                    lstUsuarios.DataSource = null;
                    lstUsuarios.Items.Clear();
                    return;
                }

                //------------------Valida Session Vigente -------------------------------------->
                if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor"); }
                //------------------------------------------------------------------------------>

                //Buscar al usuario por apellido paterno
                DataTable dtUsuario = Usuario.BuscarUsuarioPorApellido(txtNombreUsuario.Text.Trim().ToUpper());

                if (dtUsuario.Rows.Count == 0)
                {
                    Script.Mensaje("No se ha encontrado Usuarios con el apellido paterno: " + txtNombreUsuario.Text.Trim().ToUpper());
                    lstUsuarios.DataSource = null;
                    lstUsuarios.Items.Clear();
                    return;
                }

                Session["dtUsuario"] = dtUsuario;
                lstUsuarios.DataSource = dtUsuario;
                lstUsuarios.DataTextField = "cNombres";
                lstUsuarios.DataValueField = "idUsuario";
                lstUsuarios.DataBind();

                lstUsuarios.Visible = true;
                BotonEditar1.Visible = true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void BotonEditar1_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstUsuarios.SelectedItem == null)
                {
                    Script.Mensaje("Debe seleccionar el usuario al que desea editar sus datos");
                    return;
                }

                txtNombreUsuario.Enabled = false;
                BotonConsultar1.Enabled = false;

                pnlDetalle.Visible = true;

                //Ubicar al usuario específico
                int indice = lstUsuarios.SelectedIndex;

                DataTable dtUsuario = (DataTable)(Session["dtUsuario"]);

                txtCodigo.Text = dtUsuario.Rows[indice]["idUsuario"].ToString();
                txtUsuario.Text = dtUsuario.Rows[indice]["cUsuario"].ToString();
                txtApellidoPaterno.Text = dtUsuario.Rows[indice]["cApellidoPaterno"].ToString();
                txtApellidoMaterno.Text = dtUsuario.Rows[indice]["cApellidoMaterno"].ToString();
                txtNombre1.Text = dtUsuario.Rows[indice]["cNombre"].ToString();
                txtNombre2.Text = dtUsuario.Rows[indice]["cNombreSeg"].ToString();
                NumberBox1.Text = dtUsuario.Rows[indice]["cDNI"].ToString();
                ComboBoxBase2.SelectedValue = dtUsuario.Rows[indice]["idSexo"].ToString();
                this.cboDepartamento.SelectedValue = dtUsuario.Rows[indice]["cCodDepartamento"].ToString();
                cargarProvincias();
                this.cboProvincia.SelectedValue = dtUsuario.Rows[indice]["cCodProvincia"].ToString();
                //-------- IdentificAr el perfil del Usuario -------->
                DataTable dtPerfilUsuarioSelecc = Perfil.BuscaPerfilDeUsuario(Convert.ToInt32(txtCodigo.Text));
                cboPerfil.SelectedValue = dtPerfilUsuarioSelecc.Rows[0]["idPerfil"].ToString();
                //-------------------------------------------------->
                chcVigente.Checked = Convert.ToBoolean(dtUsuario.Rows[indice]["lVigente"]);

                BotonNuevo1.Visible = false;
                BotonEditar1.Visible = false;

                txtDNI.Enabled = false;
                hTipoOperacion.Value = "2"; //1:Nuevo
                //2:Edición                
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void BotonNuevo1_Click(object sender, EventArgs e)
        {
            LimpiarControles();

            //--------- Búsqueda usuario  -------------->
            txtNombreUsuario.Enabled = false;
            BotonConsultar1.Enabled = false;
            lstUsuarios.Visible = false;
            BotonEditar1.Visible = false;
            //------------------------------------------>

            pnlDetalle.Visible = true;
            BotonNuevo1.Visible = false;

            txtCodigo.Text = "0";
            hTipoOperacion.Value = "1"; //1:Nuevo
            //2:Edición  
        }

        protected void cboProyecto_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                pnlDetalle.Visible = false;
                LimpiarControles();
                BotonNuevo1.Visible = true;
                hTipoOperacion.Value = "0";

                //--------- Búsqueda usuario  -------------->
                txtNombreUsuario.Enabled = true;
                BotonConsultar1.Enabled = true;
                BotonEditar1.Visible = false;
                txtNombreUsuario.Focus();
                //------------------------------------------>

                txtNombreUsuario.Text = "";
                txtNombreUsuario.Focus();
            }
            catch (Exception)
            {
                throw;
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
                if (CamposSonValidos() == false) return;

                #region Estructura Tabla Usuario
                DataTable dtNuevoUsuario = new DataTable("dtUsuario");
                dtNuevoUsuario.Columns.Add("idUsuario", typeof(int));
                dtNuevoUsuario.Columns.Add("cNombre", typeof(string));
                dtNuevoUsuario.Columns.Add("cNombreSeg", typeof(string));
                dtNuevoUsuario.Columns.Add("cApellidoPaterno", typeof(string));
                dtNuevoUsuario.Columns.Add("cApellidoMaterno", typeof(string));
                dtNuevoUsuario.Columns.Add("cDNI", typeof(string));
                dtNuevoUsuario.Columns.Add("idSexo", typeof(int));
                dtNuevoUsuario.Columns.Add("idPerfil", typeof(int));
                dtNuevoUsuario.Columns.Add("cUsuario", typeof(string));
                dtNuevoUsuario.Columns.Add("cPassword", typeof(string));
                dtNuevoUsuario.Columns.Add("lVigente", typeof(bool));

                //------------ CAMPOS DE AUDITORIA ------------------------>
                dtNuevoUsuario.Columns.Add("idUsuarioReg", typeof(int));
                dtNuevoUsuario.Columns.Add("cNombrePc", typeof(string));
                dtNuevoUsuario.Columns.Add("cMacPc", typeof(string));
                dtNuevoUsuario.Columns.Add("cCodDepartamento", typeof(string));
                dtNuevoUsuario.Columns.Add("cCodProvincia", typeof(string));
                //-------------------------------------------------------->
                #endregion


                DataRow dr = dtNuevoUsuario.NewRow();
                if (hTipoOperacion.Value.Equals("1"))//Nuevo
                {
                    Boolean lExisteDNI = Usuario.ExisteDNI(NumberBox1.Text);

                    if (usuarioSession.idPerfil == 1)//ADMINISTRADOR GENERAL
                    {
                        if (lExisteDNI)
                        {
                            Script.Mensaje("El DNI ya existe.");
                            return;
                        }
                    }

                    dr["idUsuario"] = 0;
                    string cLogin = Usuario.crearLoginUsuario(txtNombre1.Text.Trim(), txtApellidoPaterno.Text.Trim(), NumberBox1.Text.Trim());
                    dr["cUsuario"] = cLogin;
                    dr["cPassword"] = clsCriptografia.EncriptarPassword(cLogin);

                    if (usuarioSession.idPerfil == 2)//ADMINISTRADOR
                    {
                        if (lExisteDNI)//El Usuario con ése DNI ya trabajo anteriormente
                        {
                            Script.Mensaje("El trabajador ya existe en el Sistema");

                            DataTable dtUsuarioBuscado = Usuario.BuscarUsuarioPorDNI(NumberBox1.Text);
                            dr["idUsuario"] = Convert.ToInt32(dtUsuarioBuscado.Rows[0]["idUsuario"]);
                            cLogin = dtUsuarioBuscado.Rows[0]["cUsuario"].ToString();
                            dr["cUsuario"] = cLogin;
                            dr["cPassword"] = clsCriptografia.EncriptarPassword(cLogin);
                        }
                    }
                }
                if (hTipoOperacion.Value.Equals("2")) //Edición
                {
                    dr["idUsuario"] = Convert.ToInt32(txtCodigo.Text.Trim());
                    dr["cUsuario"] = "";
                    dr["cPassword"] = "";
                }

                dr["cNombre"]           = txtNombre1.Text.Trim().ToUpper();
                dr["cNombreSeg"]        = txtNombre2.Text.Trim().ToUpper();
                dr["cApellidoPaterno"]  = txtApellidoPaterno.Text.Trim().ToUpper();
                dr["cApellidoMaterno"]  = txtApellidoMaterno.Text.Trim().ToUpper();
                dr["cDNI"]              = NumberBox1.Text.Trim();
                dr["idSexo"]            = Convert.ToInt32(ComboBoxBase2.SelectedItem.Value);
                dr["idPerfil"]          = Convert.ToInt32(cboPerfil.SelectedItem.Value);
                dr["lVigente"]          = chcVigente.Checked;
                //------------ CAMPOS DE AUDITORIA ------------------------>
                dr["idUsuarioReg"]      = usuarioSession.idUsuario;
                dr["cNombrePc"]         = usuarioSession.cNamePc;
                dr["cMacPc"]            = usuarioSession.cMacPc;
                dr["cCodDepartamento"]  = cboDepartamento.SelectedValue.ToString();
                dr["cCodProvincia"]     = cboProvincia.SelectedValue.ToString();
                //-------------------------------------------------------->

                dtNuevoUsuario.Rows.Add(dr);
                DataSet ds = new DataSet("dsUsuario");
                ds.Tables.Add(dtNuevoUsuario);

                string xmlUsuario = ds.GetXml();

                DataTable rptaUsuario = Usuario.InsUpdUsuario(xmlUsuario);

                pnlDetalle.Visible = false;
                txtDNI.Enabled = true;
                BotonNuevo1.Visible = true;

                LimpiarControles();

                //--------- Búsqueda usuario  -------------->
                txtNombreUsuario.Enabled = true;
                BotonConsultar1.Enabled = true;
                lstUsuarios.Visible = false;
                BotonEditar1.Visible = false;
                txtNombreUsuario.Focus();
                //------------------------------------------>           

                Script.Mensaje("Los datos de registraron correctamente.");
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void BotonCancelar1_Click(object sender, EventArgs e)
        {
            pnlDetalle.Visible = false;

            LimpiarControles();

            BotonNuevo1.Visible = true;

            hTipoOperacion.Value = "0";

            //--------- Búsqueda usuario  -------------->
            txtNombreUsuario.Enabled = true;
            BotonConsultar1.Enabled = true;
            BotonEditar1.Visible = false;
            txtNombreUsuario.Text = "";
            txtNombreUsuario.Focus();
        }

        protected void cboDepartamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarProvincias();
        }

        #endregion

        #region Metodos

        private void LimpiarControles()
        {
            txtCodigo.Text = "";
            txtNombre1.Text = "";
            txtNombre2.Text = "";
            txtApellidoPaterno.Text = "";
            txtApellidoMaterno.Text = "";
            NumberBox1.Text = "0";
            chcVigente.Checked = false;

            txtUsuario.Text = "";

            txtDNI.Enabled = true;
            lstUsuarios.Items.Clear();
        }

        private Boolean CamposSonValidos()
        {
            if (string.IsNullOrEmpty(this.txtApellidoPaterno.Text))
            {
                Script.Mensaje("Ingrese el Apellido Paterno.");
                return false;
            }
            if (string.IsNullOrEmpty(this.txtApellidoMaterno.Text))
            {
                Script.Mensaje("Ingrese el Apellido Materno.");
                return false;
            }
            if (string.IsNullOrEmpty(this.txtNombre1.Text))
            {
                Script.Mensaje("Ingrese el Primer Nombre.");
                return false;
            }
            if (string.IsNullOrEmpty(this.NumberBox1.Text))
            {
                Script.Mensaje("Ingrese el DNI.");
                return false;
            }
            if (NumberBox1.Text.Length < 8)
            {
                Script.Mensaje("El DNI debe tener 8 dígitos");
                return false;
            }
            return true;
        }

        private void cargarDepartamentos()
        {
            var dtDepartamentos = cnubigeo.ListarDepartamentoBracko();
            cboDepartamento.DataSource = dtDepartamentos;
            cboDepartamento.DataValueField = dtDepartamentos.Columns[0].ColumnName;
            cboDepartamento.DataTextField = dtDepartamentos.Columns[1].ColumnName;
            cboDepartamento.DataBind();
        }

        private void cargarProvincias()
        {
            var dtProvincias = cnubigeo.ListarProvincia(cboDepartamento.SelectedValue.ToString());
            this.cboProvincia.DataSource = dtProvincias;
            cboProvincia.DataValueField = dtProvincias.Columns[0].ColumnName;
            cboProvincia.DataTextField = dtProvincias.Columns[1].ColumnName;
            cboProvincia.DataBind();
        }

        private void cargarListaSexo()
        {
            //Cargar Sexo------------------------------------------------------->
            DataTable dtSexo = Sexo.ListarSexo();
            ComboBoxBase2.DataSource = dtSexo;

            ComboBoxBase2.DataValueField = dtSexo.Columns[0].ToString(); //idsexo
            ComboBoxBase2.DataTextField = dtSexo.Columns[1].ToString();  //cSexo
            ComboBoxBase2.DataBind();
            //------------------------------------------------------------------>
        }

        #endregion

    }
}