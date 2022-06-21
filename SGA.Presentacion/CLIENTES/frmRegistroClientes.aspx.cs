using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGA.Utilitarios;
using SGA.ENTIDADES;
using SGA.LogicaNegocio;
using Microsoft.Reporting.WebForms;

namespace SGA.Presentacion.CLIENTES
{
    public partial class frmRegistroClientes : System.Web.UI.Page
    {
        #region Variables globales

        GEN.CapaNegocio.clsCNUbigeo ListadoUbigeo = new GEN.CapaNegocio.clsCNUbigeo();
        GEN.CapaNegocio.clsCNTipoPersona ListadoTipoPersona = new GEN.CapaNegocio.clsCNTipoPersona();
        GEN.CapaNegocio.clsCNTipDocumento LisTipDocFil = new GEN.CapaNegocio.clsCNTipDocumento();
        GEN.CapaNegocio.clsCNTipoVia ListaTipoVia = new GEN.CapaNegocio.clsCNTipoVia();
        GEN.CapaNegocio.clsCNSexo ListaSexo = new GEN.CapaNegocio.clsCNSexo();
        GEN.CapaNegocio.clsCNEstadoCivil ListaEstCivil = new GEN.CapaNegocio.clsCNEstadoCivil();
        GEN.CapaNegocio.clsCNOcupacion ListaOcu = new GEN.CapaNegocio.clsCNOcupacion();
        CLI.CapaNegocio.clsCNListaActivEco objActEco = new CLI.CapaNegocio.clsCNListaActivEco();
        GEN.CapaNegocio.clsCNNivInstruccion listaNivInst = new GEN.CapaNegocio.clsCNNivInstruccion();
        GEN.CapaNegocio.clsCNProfesion listaProf = new GEN.CapaNegocio.clsCNProfesion();
        GEN.CapaNegocio.clsCNDirecCli ListaDirCli = new GEN.CapaNegocio.clsCNDirecCli();
        clsWebJScript script = new clsWebJScript();

        #endregion

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
                cargarControles();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void cargarControles()
        {
            cargarDepartametoDir();
            cboDepartamento.SelectedValue = "13";
            cargarProvinciaDir();
            cboProvincia.SelectedValue = "133";
            cargarDistritoDir();
            cboDistrito.SelectedValue = "1233";
            cargarTipoPersona();
            cboTipPersona.SelectedValue = "1";
            cargarTipoDocumento();
            cboTipDocumento.SelectedValue = "1";
            cargarTipoVia();
            cboTipoVia.SelectedValue = "6";
            cargarCondicionVivienda();
            cboCondicionVivienda.SelectedValue = "3";
            cargarMaterialConstruccion();
            cboMaterialConstruccion.SelectedValue = "1";
            cargarTipoDireccion();
            cboTipoDireccion.SelectedValue = "1";
            cargarTipoSexo();
            cboSexo.SelectedValue = "1";
            cargarEstadoCivil();
            cboEstadoCivil.SelectedValue = "1";
            cargarOcupacion();
            cargarActividad();
            cargarNivelIntruccion();
            cboNivInstruc.SelectedValue = "5";
            cargarProfesion();
            cargarDepartametoNac();
            cboDepartamentoNac.SelectedValue = "13";
            cargarProvinciaNac();
            cboProvinciaNac.SelectedValue = "133";
            cargarDistritoNac();
            cboDistritoNac.SelectedValue = "1233";
            cargarDirecciones(0);
            dtFecNac.SeleccionarFecha = DateTime.Now.AddYears(-30);
        }

        private void cargarTipoPersona()
        {            
            DataTable dt = ListadoTipoPersona.listarTipoPersona();
            this.cboTipPersona.DataSource = dt;
            this.cboTipPersona.DataValueField = dt.Columns[0].ToString();
            this.cboTipPersona.DataTextField = dt.Columns[1].ToString();
            cboTipPersona.DataBind();
        }

        private void cargarTipoDocumento()
        {            
            DataTable tbTipDoc = LisTipDocFil.ListaTipDocFiltro("N");
            cboTipDocumento.DataSource = tbTipDoc;
            cboTipDocumento.DataValueField = tbTipDoc.Columns[0].ToString();
            cboTipDocumento.DataTextField = tbTipDoc.Columns[1].ToString();
            cboTipDocumento.DataBind();
        }

        private void cargarTipoVia()
        {            
            DataTable dt = ListaTipoVia.ListpoVia();
            this.cboTipoVia.DataSource = dt;
            this.cboTipoVia.DataValueField = "idTipoVia";
            this.cboTipoVia.DataTextField = "cDescripcion";
            cboTipoVia.DataBind();
        }

        private void cargarCondicionVivienda()
        {
            DataTable dtConViv = new GEN.CapaNegocio.clsCNVivienda().CNCondicionVivienda();
            this.cboCondicionVivienda.DataSource = dtConViv;
            this.cboCondicionVivienda.DataValueField = "idCondicionVivienda";
            this.cboCondicionVivienda.DataTextField = "cCondicionVivienda";
            cboCondicionVivienda.DataBind();
        }

        private void cargarMaterialConstruccion()
        {
            DataTable dtMatViv = new GEN.CapaNegocio.clsCNVivienda().CNMaterialVivienda();
            this.cboMaterialConstruccion.DataSource = dtMatViv;
            this.cboMaterialConstruccion.DataValueField = "idMaterialConstruccion";
            this.cboMaterialConstruccion.DataTextField = "cMaterialContruccion";
            cboMaterialConstruccion.DataBind();
        }

        private void cargarTipoDireccion()
        {
            var dtbTipDir = new GEN.CapaNegocio.clsCNTipoDireccion().ListaTipDireccion();
            cboTipoDireccion.DataSource = dtbTipDir;
            cboTipoDireccion.DataValueField = "idTipoDireccion";
            cboTipoDireccion.DataTextField = "cTipoDir";
            cboTipoDireccion.DataBind();
        }

        private void cargarTipoSexo()
        {
            DataTable tbSexo = ListaSexo.ListarSexo();
            this.cboSexo.DataSource = tbSexo;
            this.cboSexo.DataValueField = tbSexo.Columns[0].ToString();
            this.cboSexo.DataTextField = tbSexo.Columns[1].ToString();
            cboSexo.DataBind();
        }

        private void cargarEstadoCivil()
        {
            DataTable tbEstCivil = ListaEstCivil.ListaEstadoCivil();
            this.cboEstadoCivil.DataSource = tbEstCivil;
            this.cboEstadoCivil.DataValueField = tbEstCivil.Columns[0].ToString();
            this.cboEstadoCivil.DataTextField = tbEstCivil.Columns[1].ToString();
            cboEstadoCivil.DataBind();
        }

        private void cargarOcupacion()
        {            
            DataTable tbOcup = ListaOcu.ListarOcupacion();
            cboOcupacion.DataSource = tbOcup;
            cboOcupacion.DataValueField = tbOcup.Columns[0].ToString();
            cboOcupacion.DataTextField = tbOcup.Columns[1].ToString();
            cboOcupacion.DataBind();
        }

        private void cargarActividad()
        {            
            DataTable dtbActEco = objActEco.ListaActividadEco();
            this.cboActividadEco1.DataSource = dtbActEco;
            this.cboActividadEco1.DataValueField = dtbActEco.Columns[0].ToString();
            this.cboActividadEco1.DataTextField = dtbActEco.Columns[1].ToString();
            cboActividadEco1.DataBind();
        }

        private void cargarNivelIntruccion()
        {            
            DataTable tbNivInst = listaNivInst.ListarNivInstruccion();
            cboNivInstruc.DataSource = tbNivInst;
            cboNivInstruc.DataValueField = tbNivInst.Columns[0].ToString();
            cboNivInstruc.DataTextField = tbNivInst.Columns[1].ToString();
            cboNivInstruc.DataBind();
        }

        private void cargarProfesion()
        {            
            DataTable tbProf = listaProf.ListarProfesion();
            cboProfesion.DataSource = tbProf;
            cboProfesion.DataValueField = tbProf.Columns[0].ToString();
            cboProfesion.DataTextField = tbProf.Columns[1].ToString();
            cboProfesion.DataBind();
        }

        private void cargarDepartametoDir()
        {
            DataTable dt = ListadoUbigeo.listarUbigeo(1);
            cboDepartamento.DataSource = dt;
            cboDepartamento.DataValueField = dt.Columns[0].ToString();
            cboDepartamento.DataTextField = dt.Columns[2].ToString();
            cboDepartamento.DataBind();
        }

        private void cargarProvinciaDir()
        {
            DataTable dt = ListadoUbigeo.listarUbigeo(Convert.ToInt32(cboDepartamento.SelectedValue));
            this.cboProvincia.DataSource = dt;
            cboProvincia.DataValueField = dt.Columns[0].ToString();
            cboProvincia.DataTextField = dt.Columns[2].ToString();
            cboProvincia.DataBind();
            cboProvincia.SelectedIndex = 1;
            cargarDistritoDir();
        }

        private void cargarDistritoDir()
        {
            DataTable dt = ListadoUbigeo.listarUbigeo(Convert.ToInt32(cboProvincia.SelectedValue));
            this.cboDistrito.DataSource = dt;
            cboDistrito.DataValueField = dt.Columns[0].ToString();
            cboDistrito.DataTextField = dt.Columns[2].ToString();
            cboDistrito.DataBind();
            cboDistrito.SelectedIndex = 1;
        }

        private void cargarDepartametoNac()
        {
            DataTable dt = ListadoUbigeo.listarUbigeo(1);
            cboDepartamentoNac.DataSource = dt;
            cboDepartamentoNac.DataValueField = dt.Columns[0].ToString();
            cboDepartamentoNac.DataTextField = dt.Columns[2].ToString();
            cboDepartamentoNac.DataBind();
        }

        private void cargarProvinciaNac()
        {
            DataTable dt = ListadoUbigeo.listarUbigeo(Convert.ToInt32(cboDepartamentoNac.SelectedValue));
            this.cboProvinciaNac.DataSource = dt;
            cboProvinciaNac.DataValueField = dt.Columns[0].ToString();
            cboProvinciaNac.DataTextField = dt.Columns[2].ToString();
            cboProvinciaNac.DataBind();
            //cboProvinciaNac.SelectedIndex = 1;
            cargarDistritoNac();
        }

        private void cargarDistritoNac()
        {
            DataTable dt = ListadoUbigeo.listarUbigeo(Convert.ToInt32(cboProvinciaNac.SelectedValue));
            this.cboDistritoNac.DataSource = dt;
            cboDistritoNac.DataValueField = dt.Columns[0].ToString();
            cboDistritoNac.DataTextField = dt.Columns[2].ToString();
            cboDistritoNac.DataBind();
            //cboDistritoNac.SelectedIndex = 1;
        }

        private void cargarDirecciones(int idCli)
        {
            var dtDirecciones = ListaDirCli.ListaDirCli(idCli);
            ViewState["dtDirecciones"] = dtDirecciones;
            dtgDireccion.DataSource = null;
            dtgDireccion.DataSource = dtDirecciones;
            dtgDireccion.DataBind();
        }

        public void LimpiarControles()
        {
            cargarControles();
            //-----------------------------------------------
            // Limpiar Datos Generales
            //-----------------------------------------------
            this.txtCBDNI.Text="";
            this.txtCBRUC.Text="";
            this.txtCBNroTel.Text="";
            txtCelular.Text="";            
            this.txtDireccion.Text = "";
            this.txtReferencia.Text = "";
            //-----------------------------------------------
            // Limpiar Datos Persona Natural
            //-----------------------------------------------
            this.txtApePat.Text = "";
            this.txtApeMat.Text = "";
            this.txtApeCasado.Text = "";
            this.txtNomCli.Text = "";
            txtAnioResidencia.Text = "";
            dtFecNac.SeleccionarFecha = DateTime.Now;
            txtCBCorreoElectronico.Email = "";

            txtNroHijos.Text = "";
            txtNroPerDep.Text = "";
            //-----------------------------------------------
            // Limpiar Datos Persona Jurídica
            //-----------------------------------------------
            ////////this.txtRazSocial.Text = "";
            ////////this.txtNomComercial.Text = "";
            ////////dtpFecCons.Value = DateTime.Now;
            //-----------------------------------------------
            // Limpiar Datos Vinculados
            //-----------------------------------------------
            //////////cboTipVinculo.SelectedValue = 1;
            //////////this.dtgVinculo.ClearSelection();
            //////////conBusCliVin.txtCodCli.Clear();
            //////////conBusCliVin.txtNombre.Clear();
            //////////conBusCliVin.txtNroDoc.Clear();
        }

        private string ValidarDatos_Generales()
        {
            if (cboTipDocumento.Text.Trim() == "")
            {
                script.Mensaje("Debe Seleccionar el tipo de documento"); 
                cboTipDocumento.Focus();
                return "ERROR";
            }

            if (cboTipPersona.Text.Trim() == "")
            {
                script.Mensaje("Debe seleccionar el tipo de persona");                
                cboTipPersona.Focus();
                return "ERROR";
            }

            if (dtgDireccion.Rows.Count == 0)
            {
                script.Mensaje("Debe agregar al menos una dirección del cliente");                
                return "ERROR";
            }

            if (this.cboDistrito.SelectedItem.Text == "")
            {
                script.Mensaje("Debe de Seleccionar el Ubigeo de la Dirección del Cliente: Distrito");                
                cboDistrito.Focus();
                return "ERROR";
            }
           
            //Validamos la Direccion Principal
            var tbDirCli = (DataTable)ViewState["dtDirecciones"];
            int cont = 0;
            for (int i = 0; i < tbDirCli.Rows.Count; i++)
            {
                if (Convert.ToInt32(tbDirCli.Rows[i]["idTipoDireccion"]) == 1 && tbDirCli.Rows[i]["Estado"].ToString() != "E")
                {
                    cont = cont + 1;
                }

            }
            if (cont < 1)
            {

                script.Mensaje("Debe de Seleccionar la Direccion Principal");
                return "ERROR";
            }
            return "OK";
        }

        private string ValidarDatos_PerNatural()
        {
            if (Session["idCliente"] == null && hcOperacion.Value=="A")
            {
                this.script.Mensaje("Seleccione un cliente para el cobro");
                return"ERROR";
            }

            if (txtCBDNI.Text.Trim() == "")
            {
                script.Mensaje("Debe Registrar eL Número de Documento");                
                txtCBDNI.Focus();
                return "ERROR";
            }
            string tcDocIde = this.txtCBDNI.Text.Trim();
            if (tcDocIde != "")
            {
                var idCli = Convert.ToInt32(Session["idCliente"]);
                CLI.CapaNegocio.clsCNRetDatosCliente xRetDatCli = new CLI.CapaNegocio.clsCNRetDatosCliente();
                string cValidacion = xRetDatCli.RetDatVal(idCli, tcDocIde, "D");
                if (cValidacion == "ERROR")
                {
                    script.Mensaje("Ya Existe Registrado un Cliente con el Número de Documento Ingresado");
                    return "ERROR";
                }

            }
            if (cboTipDocumento.SelectedValue.ToString().Trim() == "1" && txtCBDNI.Text.Trim().Length != 8)
            {
                script.Mensaje("El DNI del Cliente debe ser de 8 Dígitos");
                txtCBDNI.Focus();
                return "ERROR";
            }
            if (txtApePat.Text.Trim() == "")
            {
                script.Mensaje("Debe Registrar el Apellido Paterno ");
                txtApePat.Focus();
                return "ERROR";
            }

            if (string.IsNullOrEmpty(txtApeMat.Text.Trim()) && string.IsNullOrEmpty(txtApeCasado.Text.Trim()))
            {
                script.Mensaje("Debe Registrar el Apellido Materno o de Casado");                
                txtApeMat.Focus();
                return "ERROR";
            }

            if (txtNomCli.Text.Trim() == "")
            {
                script.Mensaje("Debe Registrar el Nombre del Cliente");
                txtNomCli.Focus();
                return "ERROR";
            }

            if (cboEstadoCivil.SelectedItem.Text=="")
            {
                script.Mensaje("Debe Seleccionar el Estado Civil del Cliente");
                cboEstadoCivil.Focus();
                return "ERROR";
            }

            if (cboSexo.SelectedItem.Text == "")
            {
                script.Mensaje("Debe Seleccionar el Sexo del Cliente");                
                cboSexo.Focus();
                return "ERROR";
            }

            if (cboDistritoNac.SelectedItem.Text=="")
            {
                script.Mensaje("Debe Seleccionar el Ubigeo de Nacimiento del Cliente: Distrito");
                return "ERROR";
            }

            if (cboNivInstruc.SelectedItem.Text == "")
            {
                script.Mensaje("Debe Seleccionar el Nivel de Instrucción del Cliente");
                cboNivInstruc.Focus();
                return "ERROR";
            }

            if (cboActividadEco1.Text.Trim() == "")
            {
                script.Mensaje("Debe Seleccionar la Actividad Económica del Cliente");
                cboActividadEco1.Focus();
                return "ERROR";
            }
            return "OK";
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
            if (ValidarDatos_Generales()=="ERROR")
            {
                return;
            }
            
            //======================================================================
            //Obtener Datos Generales
            //======================================================================
            string tcTipDoc = cboTipDocumento.SelectedValue.ToString().Trim();
            string tcDocIde = txtCBDNI.Text.Trim();
            string tcDocAdi = txtCBRUC.Text.Trim();
            string tcTipPer = cboTipPersona.SelectedValue.ToString().Trim();
            string tcDirCli = txtDireccion.Text.Trim();
            string tcRefer = txtReferencia.Text.Trim();
            string tcTelef = txtCBNroTel.Text.Trim();
            string tcCelular = txtCelular.Text.Trim();
            string tcCorreo = txtCBCorreoElectronico.Email.Trim();

            //===================================================================
            //Guardar Datos de Direcciones Mediante XML
            //===================================================================  

            DataSet dsDir = new DataSet("dsDireccion");
            var tbDirCli = (DataTable)ViewState["dtDirecciones"];
            dsDir.Tables.Add(tbDirCli);
            string xmlDirec = dsDir.GetXml();
            xmlDirec = GEN.CapaNegocio.clsCNFormatoXML.EncodingXML(xmlDirec);
            dsDir.Tables.Clear();

            //===================================================================
            //Guardar Datos de Vinculos Mediante XML
            //===================================================================
            DataSet dsVin = new DataSet("dsVinculo");
            //dsVin.Tables.Add(tbClienteVinculado);
            string xmlVinc = dsVin.GetXml();
            xmlVinc = GEN.CapaNegocio.clsCNFormatoXML.EncodingXML(xmlVinc);
            dsVin.Tables.Clear();
             //===================================================================
            //Guardar Datos del Cliente
            //===================================================================
            if (cboTipPersona.SelectedValue.ToString().Trim() == "1")
            {
                //ValidarDatos_PerNatural();
                if (ValidarDatos_PerNatural() == "ERROR")
                {
                    return;
                }
                //=======================================================================
                //Obtener Datos de Persona Natural
                //=======================================================================
                string tcApePat = txtApePat.Text.Trim();
                string tcApeMat = txtApeMat.Text.Trim();
                string tcNomCli = txtNomCli.Text.Trim();
                string tcApeCas = txtApeCasado.Text.Trim();

                if (string.IsNullOrEmpty(tcApeMat))
                {
                    tcApeMat = "";
                }
                
                if (string.IsNullOrEmpty(tcApeCas))
                {
                    tcApeCas = "";
                }
                
                string tcNombCliente = tcApePat + " " + tcApeMat + " " + tcApeCas + " " + tcNomCli;

                int tnCodUbi;
                DateTime tdFecNac;
                string tcSexo;
                string tcEstCiv;
                string tcProf;
                string tcNivInst;
                string tcOcup;
                string tcIdActEcoN;
                string tcNumHijos;
                string tcNumPerDepend;
                bool lDatoBasico = false;// chcDatosBasicos.Checked;

                if (lDatoBasico)
                {
                    tnCodUbi = Convert.ToInt32(this.cboDistrito.SelectedValue) > 0 ? Convert.ToInt32(this.cboDistrito.SelectedValue) : 1224;
                    tdFecNac = dtFecNac.SeleccionarFecha != objUsuario.dFecSystem ? dtFecNac.SeleccionarFecha : objUsuario.dFecSystem;
                    tcSexo = cboSexo.SelectedValue.ToString();
                    tcEstCiv = cboEstadoCivil.SelectedValue.ToString();
                    tcProf = Convert.ToInt32(cboProfesion.SelectedValue) > 0 ? cboProfesion.SelectedValue.ToString() : "0";
                    tcNivInst = Convert.ToInt32(cboNivInstruc.SelectedValue) > 0 ? cboNivInstruc.SelectedValue.ToString() : "0";
                    tcOcup = Convert.ToInt32(cboOcupacion.SelectedValue) > 0 ? cboOcupacion.SelectedValue.ToString() : "0";
                    tcIdActEcoN = Convert.ToInt32(cboActividadEco1.SelectedValue) > 0 ? cboActividadEco1.SelectedValue.ToString() : "0";
                    tcNumHijos = string.IsNullOrEmpty(txtNroHijos.Text) ? "0" : txtNroHijos.Text;
                    tcNumPerDepend = string.IsNullOrEmpty(txtNroPerDep.Text) ? "0" : txtNroPerDep.Text;
                }
                else
                {
                    tnCodUbi = Convert.ToInt32(this.cboDistrito.SelectedValue);
                    tdFecNac = dtFecNac.SeleccionarFecha;
                    tcSexo = cboSexo.SelectedValue.ToString().Trim();
                    tcEstCiv = cboEstadoCivil.SelectedValue.ToString().Trim();
                    tcProf = cboProfesion.SelectedValue.ToString().Trim();
                    tcNivInst = cboNivInstruc.SelectedValue.ToString().Trim();
                    tcOcup = cboOcupacion.SelectedValue.ToString().Trim();
                    tcIdActEcoN = cboActividadEco1.SelectedValue.ToString().Trim();
                    tcNumHijos = txtNroHijos.Text.Trim();
                    tcNumPerDepend = txtNroPerDep.Text.Trim();
                }

                if (string.IsNullOrEmpty(tcNumHijos))
                {
                    tcNumHijos = "0";
                }

                if (string.IsNullOrEmpty(tcNumPerDepend))
                {
                    tcNumPerDepend = "0";
                }
                //=======================================================================
                //Validar Ingreso de Datos
                //=======================================================================           
                if (Convert.ToDateTime(dtFecNac.SeleccionarFecha.ToShortDateString()) > objUsuario.dFecSystem)
                {
                    this.script.Mensaje("La Fecha de Nacimiento del Cliente no Puede ser Mayor a la fecha de HOY");
                    dtFecNac.Focus();
                    dsDir.Dispose();
                    dsVin.Dispose();
                    return; 
                }

                if (cboNivInstruc.SelectedValue.ToString().Trim() == "4" && cboProfesion.SelectedValue.ToString().Trim()=="1")
                {
                    script.Mensaje("Debe Seleccionar la Profesión del Cliente");
                    cboProfesion.Focus();
                    dsDir.Dispose();
                    dsVin.Dispose();
                    return;
                }
                
                //=======================================================================
                //Guardar Datos de Persona Natural
                //=======================================================================
                GEN.CapaNegocio.clsCNGuardaCliPerNat GuardaCliNat = new GEN.CapaNegocio.clsCNGuardaCliPerNat();
                
                if (hcOperacion.Value == "N")
                {
                    DataTable dtIdCliente = GuardaCliNat.GuardarCliPerNat(tcNombCliente, Convert.ToInt32(tcTipDoc), Convert.ToInt32(tcTipPer), tcDocIde, tcDocAdi,
                                                     tcTelef, tcCelular, tcCorreo, Convert.ToInt32(tcIdActEcoN), xmlDirec, tcApePat,
                                                     tcApeMat, tcNomCli, tcApeCas, tdFecNac, tnCodUbi, Convert.ToInt32(tcSexo),
                                                     Convert.ToInt32(tcEstCiv), Convert.ToInt32(tcProf), Convert.ToInt32(tcNivInst),
                                                     Convert.ToInt32(tcOcup), xmlVinc, Convert.ToInt32(tcNumHijos), Convert.ToInt32(tcNumPerDepend), lDatoBasico);

                    script.Mensaje("Los Datos del Cliente se Registraron Correctamente");
                }
                else if (hcOperacion.Value == "A")
                {
                    if (Session["idCliente"]==null)
                    {
                        script.Mensaje("Debe Primero Buscar los Datos del Cliente");
                        dsDir.Dispose();
                        dsVin.Dispose();
                        return;
                    }
                    int xidCli = Convert.ToInt32(Session["idCliente"]);
                    
                    //=====================================================================
                    //--Actualizar Datos del Cliente (NATURAL)
                    //=====================================================================
                    GuardaCliNat.ActualizarCliPerNat(xidCli, tcNombCliente, Convert.ToInt32(tcTipDoc), Convert.ToInt32(tcTipPer), tcDocIde, tcDocAdi,
                                                    tcTelef, tcCelular, tcCorreo, Convert.ToInt32(tcIdActEcoN), xmlDirec, tcApePat,
                                                    tcApeMat, tcNomCli, tcApeCas, tdFecNac, tnCodUbi, Convert.ToInt32(tcSexo),
                                                    Convert.ToInt32(tcEstCiv), Convert.ToInt32(tcProf), Convert.ToInt32(tcNivInst),
                                                    Convert.ToInt32(tcOcup), xmlVinc, Convert.ToInt32(tcNumHijos), Convert.ToInt32(tcNumPerDepend), lDatoBasico);
                    script.Mensaje("Los Datos del Cliente se Actualizaron Correctamente");
                }
            }

            #region Persona juridica
            else if (cboTipPersona.SelectedValue.ToString().Trim() == "2")
            {
                ////ValidarDatos_PerJuridica();
                //if (ValidarDatos_PerJuridica() == "ERROR")
                //{
                //    tbcCliente.SelectedIndex = 2;
                //    return;
                //}
                ////=======================================================================
                ////Validar Ingreso de Datos
                ////=======================================================================           
                //if (Convert.ToDateTime(dtpFecCons.Value.ToShortDateString()) > Convert.ToDateTime(pdFecSistem.ToShortDateString()))
                //{
                //    MessageBox.Show("La Fecha de Constitución de la Empresa, no Puede ser Mayor a la fecha de HOY", "Registro de Clientes", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //    dtpFecCons.Focus();
                //    dsDir.Dispose();
                //    dsVin.Dispose();
                //    return;
                //}
                ////=======================================================================
                ////Obtener Datos de Persona Jurídica
                ////=======================================================================
                //string tcRazSoc = txtRazSocial.Text.Trim();
                //string tcNomCom = txtNomComercial.Text.Trim();
                //string tcIdActEcoJ = cboActividadEco2.SelectedValue.ToString().Trim(); ;
                //DateTime tdFecConst = dtpFecCons.Value;

                ////=======================================================================
                ////Guardar Datos de Persona Jurídica
                ////=======================================================================
                //clsCNGuardaCliPerJur GuardarCliJur=new clsCNGuardaCliPerJur();
                //if (pcTipOpe == "N")
                //{
                //    DataTable dtIdCliente= GuardarCliJur.GuardarCliPerJur(tcRazSoc, Convert.ToInt32(tcTipDoc), Convert.ToInt32(tcTipPer), tcDocIde, tcDocAdi,
                //                                    tcTelef, tcCorreo, Convert.ToInt32(tcIdActEcoJ), xmlDirec, tcRazSoc,
                //                                    tcNomCom, tdFecConst, xmlVinc);
                //    MessageBox.Show("Los Datos del Cliente se Registraron Correctamente", "Registro de Clientes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    this.conBusCliente.txtCodCli.Text = dtIdCliente.Rows[0]["IdCliente"].ToString();
                //    this.conBusCliente.txtNombre.Text = tcRazSoc;
                //    this.conBusCliente.txtNroDoc.Text = tcDocAdi;
                //}
                //else if (pcTipOpe == "A")
                //{
                //    int xidCli = Convert.ToInt32(conBusCliente.txtCodCli.Text);
                //    if (conBusCliente.txtCodCli.Text.Trim() == "")
                //    {
                //        MessageBox.Show("Debe Primero Buscar los Datos del Cliente", "Registro de Clientes", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //        dsDir.Dispose();
                //        dsVin.Dispose();
                //        return;
                //    }
                //    GuardarCliJur.ActualizarCliPerJur(xidCli,tcRazSoc, Convert.ToInt32(tcTipDoc), Convert.ToInt32(tcTipPer), tcDocIde, tcDocAdi,
                //                                    tcTelef, tcCorreo, Convert.ToInt32(tcIdActEcoJ), xmlDirec, tcRazSoc,
                //                                    tcNomCom, tdFecConst, xmlVinc);
                //    MessageBox.Show("Los Datos del Cliente se Actualizaron Correctamente", "Registro de Clientes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}

            }
            #endregion
            
            Buscar();
            //=====================================================
            //--Liberar Variables
            //=====================================================
            dsDir.Clear();
            dsDir.Dispose();
            dsVin.Clear();
            dsVin.Dispose();

            //=====================================================
            //--Habilitar y Desabilitar Controles
            //=====================================================
            //pcTipOpe = "N";
            //conBusCliente.btnBusCliente.Enabled = true;
            //HabilitarControles_Gen(false);
            //HabilitarControles_PerNat(false);
            //HabilitarControles_PerJur(false);
            //HabilitarControles_Vinculo(false);
            //HabilitarGridDireccion(false);
            //conBusCliVin.btnBusCliente.Enabled = false;

            this.BotonNuevo1.Enabled = true;
            this.BotonEditar1.Enabled = true;
            this.BotonCancelar1.Enabled = true;
            this.BotonGrabar1.Enabled = false;
            this.BotonImprimir1.Enabled = true;
        }

        public void Buscar()
        {
            if (Session["idCliente"] == null)
            {
                LimpiarControles();
                this.BotonCancelar1.Enabled = true;                
                return;
            }

            //---Limpiar Controles
            LimpiarControles();
            
            //========================================================================
            //--Traer los datos del Cliente Buscado
            //========================================================================
            int nidCli = Convert.ToInt32(Session["idCliente"]);
            pnidCli.Value = nidCli.ToString();
            CLI.CapaNegocio.clsCNRetDatosCliente RetTipCli = new CLI.CapaNegocio.clsCNRetDatosCliente();
            DataTable DatosTipCli = RetTipCli.ListarDatosCli(nidCli, "O");
            if (DatosTipCli.Rows[0]["idTipoPersona"].ToString() == "1")
            {
                CLI.CapaNegocio.clsCNRetDatosCliente RetCliNat = new CLI.CapaNegocio.clsCNRetDatosCliente();
                DataTable DatosCliNat = RetTipCli.ListarDatosCli(nidCli, "N");
                //===================================================================================
                //--Asignando Valores: Datos Generales
                //===================================================================================
                ////////this.conBusCliente.txtCodCli.Text = Convert.ToString(nidCli);
                ////////this.conBusCliente.txtNombre.Text = DatosCliNat.Rows[0]["cApellidoPaterno"].ToString().Trim() + " " + DatosCliNat.Rows[0]["cApellidoMaterno"].ToString().Trim() + " " + DatosCliNat.Rows[0]["cApellidoCasado"].ToString().Trim() + " " + DatosCliNat.Rows[0]["cNombre"].ToString().Trim();
                ////////this.conBusCliente.txtNroDoc.Text = DatosCliNat.Rows[0]["cDocumentoID"].ToString();
                //===================================================================================
                //--Asignando Valores: Datos Generales
                //===================================================================================
                cboTipDocumento.SelectedValue = DatosCliNat.Rows[0]["idTipoDocumento"].ToString();
                cboTipPersona.SelectedValue = DatosCliNat.Rows[0]["IdTipoPersona"].ToString();
                txtCBDNI.Text = DatosCliNat.Rows[0]["cDocumentoID"].ToString();
                txtCBRUC.Text = DatosCliNat.Rows[0]["cDocumentoAdicional"].ToString();
                txtCelular.Text = DatosCliNat.Rows[0]["cNumeroCelular"].ToString();
                txtCBNroTel.Text = DatosCliNat.Rows[0]["nNumeroTelefono"].ToString();
                txtCBCorreoElectronico.Email = DatosCliNat.Rows[0]["cCorreoCli"].ToString();
                //===================================================================================
                //--Asignando Valores: Datos Persona Natural
                //===================================================================================
                txtApePat.Text = DatosCliNat.Rows[0]["cApellidoPaterno"].ToString();
                txtApeMat.Text = DatosCliNat.Rows[0]["cApellidoMaterno"].ToString();
                txtApeCasado.Text = DatosCliNat.Rows[0]["cApellidoCasado"].ToString();
                txtNomCli.Text = DatosCliNat.Rows[0]["cNombre"].ToString();
                dtFecNac.SeleccionarFecha = Convert.ToDateTime(DatosCliNat.Rows[0]["dFechaNacimiento"].ToString());
                cboEstadoCivil.SelectedValue = DatosCliNat.Rows[0]["idEstadoCivil"].ToString();
                cboSexo.SelectedValue = DatosCliNat.Rows[0]["idSexo"].ToString();
                cboNivInstruc.SelectedValue = DatosCliNat.Rows[0]["idNivelInstruccion"].ToString();
                cboProfesion.SelectedValue = DatosCliNat.Rows[0]["idProfesion"].ToString();
                cboOcupacion.SelectedValue = DatosCliNat.Rows[0]["idOcupacion"].ToString();
                cboActividadEco1.SelectedValue = DatosCliNat.Rows[0]["nIdActivEco"].ToString();
                txtNroHijos.Text = DatosCliNat.Rows[0]["nNumHijos"].ToString();
                txtNroPerDep.Text = DatosCliNat.Rows[0]["nNumPerDepend"].ToString();
                //chcDatosBasicos.Checked = Convert.ToBoolean(DatosCliNat.Rows[0]["lDatoBasico"]);
                //===============================================================================
                //---Retorna Ubigeo
                //===============================================================================

                Int32 idUbigeo = Convert.ToInt32(DatosCliNat.Rows[0]["idUbigeoNacimiento"]);
                DataTable tbDatUbi = RetCliNat.RetUbiCli(idUbigeo);


                //this.cboDepartamentoNac.SelectedValue = tbDatUbi.Rows[3]["idUbigeo"].ToString();
                this.cboDepartamentoNac.SelectedValue = tbDatUbi.Rows[2]["idUbigeo"].ToString();
                cargarProvinciaNac();
                this.cboProvinciaNac.SelectedValue = tbDatUbi.Rows[1]["idUbigeo"].ToString();
                cargarDistritoNac();
                this.cboDistritoNac.SelectedValue = tbDatUbi.Rows[0]["idUbigeo"].ToString();
                //---cagar Datos de Direcciones y Conyuge
                cargarDirecciones(nidCli);
                /////CargarVinculados(nidCli);
                //---Liberar Variables
                DatosCliNat.Dispose();
                tbDatUbi.Dispose();
            }
            #region Persona juridica
            else if (DatosTipCli.Rows[0]["idTipoPersona"].ToString() == "2")
            {
                //clsCNRetDatosCliente RetCliJur = new clsCNRetDatosCliente();
                //DataTable DatosCliJur = RetTipCli.ListarDatosCli(nidCli, "J");
                //this.conBusCliente.txtCodCli.Text = Convert.ToString(nidCli);
                //this.conBusCliente.txtNombre.Text = DatosCliJur.Rows[0]["cRazonSocial"].ToString();
                //this.conBusCliente.txtNroDoc.Text = DatosCliJur.Rows[0]["cDocumentoAdicional"].ToString();
                ////===============================================================================
                ////--Asignando Valores: Datos Generales
                ////===============================================================================
                //cboTipDocumento.SelectedValue = DatosCliJur.Rows[0]["idTipoDocumento"].ToString();
                //cboTipPersona.SelectedValue = DatosCliJur.Rows[0]["IdTipoPersona"].ToString();
                //txtCBDNI.Text = DatosCliJur.Rows[0]["cDocumentoID"].ToString();
                //txtCBRUC.Text = DatosCliJur.Rows[0]["cDocumentoAdicional"].ToString();
                //txtCBNroTel.Text = DatosCliJur.Rows[0]["nNumeroTelefono"].ToString();
                //txtCelular.Text = DatosCliJur.Rows[0]["cNumeroCelular"].ToString();
                //txtCBCorreoElectronico.Text = DatosCliJur.Rows[0]["cCorreoCli"].ToString();
                ////===============================================================================
                ////--Asignando Valores: Datos Persona Juridica
                ////===============================================================================
                //txtRazSocial.Text = DatosCliJur.Rows[0]["cRazonSocial"].ToString();
                //txtNomComercial.Text = DatosCliJur.Rows[0]["cNombreComercial"].ToString();
                //dtpFecCons.Value = Convert.ToDateTime(DatosCliJur.Rows[0]["dFechaConstitucion"].ToString());
                //cboActividadEco2.SelectedValue = DatosCliJur.Rows[0]["nIdActivEco"].ToString();
                ////---cagar Datos de Direcciones y Conyuge
                //CargarDirCli(nidCli);
                //CargarVinculados(nidCli);
                ////---Liberar Variables
                //DatosCliJur.Dispose();

            }
            #endregion
            //---Liberar Variables
            DatosTipCli.Dispose();

            //=====================================================
            //--Habilitar y Desabilitar Controles
            //=====================================================
            ////conBusCliente.btnBusCliente.Enabled = true;
            ////HabilitarControles_Gen(false);
            ////HabilitarControles_PerNat(false);
            ////HabilitarControles_PerJur(false);
            ////HabilitarControles_Vinculo(false);
            ////conBusCliVin.btnBusCliente.Enabled = false;

            BotonConsultar1.Visible = false;
            BotonEditar1.Visible = false;
            BotonImprimir1.Visible = false;
            BotonNuevo1.Visible = false;
            BotonGrabar1.Visible = false;
            BotonCancelar1.Visible = true;
            pnInfoBasico.Visible = true;
            pnPerNatural.Visible = true;
            conBuscarCliente1.Habilitar(false);
        }

        protected void BotonCancelar1_Click(object sender, EventArgs e)
        {
            hcOperacion.Value = "N";
            Session["idCliente"] = null;
            
            LimpiarControles();

            conBuscarCliente1.LimpiarControl();
            conBuscarCliente1.Habilitar(true);
            conBuscarCliente1.Visible = true;
            BotonConsultar1.Visible = true;
            BotonEditar1.Visible = false;
            BotonImprimir1.Visible = false;
            BotonNuevo1.Visible = true;
            BotonGrabar1.Visible = false;
            BotonCancelar1.Visible = true;
            pnInfoBasico.Visible = false;
            pnPerNatural.Visible = false;
        }

        protected void BotonConsultar1_Click(object sender, EventArgs e)
        {
            Buscar();
            if (Session["idCliente"]!=null)
            {
                BotonEditar1.Visible = true;
                BotonConsultar1.Visible = false;
                BotonImprimir1.Visible = true;
            }
        }

        protected void btnAgregarDireccion_Click(object sender, EventArgs e)
        {
            if (this.cboTipoVia.Text.Trim() == "")
            {
                script.Mensaje("Debe Seleccionar la Vía de Ubicación del Cliente");
                this.cboTipoVia.Focus();
                return;
            }

            if (cboDistrito.SelectedItem.Text=="")
            {
                script.Mensaje("Debe Seleccionar la Ubicación del Cliente");
                return;
            }

            if (txtDireccion.Text.Trim() == "")
            {
                script.Mensaje("Debe Ingresar la Dirección del Cliente");
                txtDireccion.Focus();
                return;
            }

            if (txtReferencia.Text.Trim() == "")
            {
                script.Mensaje("Debe Ingresar la Referencia de la Dirección");
                txtReferencia.Focus();
                return;
            }

            var tbDirCli = (DataTable)ViewState["dtDirecciones"];
            DataRow dr = tbDirCli.NewRow();
            if (Session["idCliente"]==null)
            {
                dr["idCli"] = 0;
            }
            else
            {
                dr["idCli"] = Session["idCliente"].ToString();
            }
            dr["idUbigeo"] =this.cboDistrito.SelectedValue.ToString().Trim();
            dr["cDireccion"] = txtDireccion.Text.Trim();
            dr["cReferenciaDireccion"] = txtReferencia.Text.Trim();
            dr["idTipoDireccion"] = Convert.ToInt32(cboTipoDireccion.SelectedValue);
            dr["idDireccion"] = cboTipoVia.SelectedValue.ToString().Trim();
            dr["idTipoVia"] = this.cboTipoVia.SelectedValue.ToString().Trim();
            dr["idCondicionVivienda"] = cboCondicionVivienda.SelectedValue;
            dr["idMaterialConstruccion"] = cboMaterialConstruccion.SelectedValue;
            dr["idEstadoConstruccion"] = 0; //cboEstadoConstruccion.SelectedValue;
            dr["idTipoPiso"] = 0; // cboTipoPiso.SelectedValue;
            dr["idTipoPared"] = 0; // cboTipoPared.SelectedValue;
            dr["idTipoCombustible"] = 0;
            dr["nAreaTotal"] = 0;// string.IsNullOrEmpty(txtAreaTotal.Text) ? "0" : txtAreaTotal.Text;
            dr["nAreaConstruida"] = 0;// string.IsNullOrEmpty(txtAreaCons.Text) ? "0" : txtAreaCons.Text;
            dr["nAniosResidencia"] = string.IsNullOrEmpty(txtAnioResidencia.Text) ? "0" : txtAnioResidencia.Text;
            dr["Estado"] = 'N';
            dr["cTipoDir"] = cboTipoDireccion.SelectedItem.Text;
            tbDirCli.Rows.Add(dr);
            this.txtDireccion.Text="";
            this.txtReferencia.Text = "";

            ViewState["dtDirecciones"] = tbDirCli;
            this.dtgDireccion.DataSource = tbDirCli;
            dtgDireccion.DataBind();
        }

        protected void dtgDireccion_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int nIndex = e.RowIndex;

            DataTable dtDirecciones = (DataTable)ViewState["dtDirecciones"];
            DataRow dr = dtDirecciones.Rows[nIndex];
            dtDirecciones.Rows.Remove(dr);
            dtDirecciones.AcceptChanges();

            ViewState["dtDirecciones"] = dtDirecciones;
            dtgDireccion.DataSource = dtDirecciones;
            dtgDireccion.DataBind();
        }

        protected void cboDepartamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboDepartamento.SelectedIndex>-1)
            {
                cargarProvinciaDir();
            }
        }

        protected void cboProvincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboProvincia.SelectedIndex>-1)
            {
                cargarDistritoDir(); 
            }
        }

        protected void cboDepartamentoNac_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboDepartamentoNac.SelectedIndex>-1)
            {
                cargarProvinciaNac();
            }
        }

        protected void cboProvinciaNac_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboProvinciaNac.SelectedIndex>-1)
            {
                cargarDistritoNac();
            }
        }

        protected void BotonEditar1_Click(object sender, EventArgs e)
        {
            hcOperacion.Value = "A";
            var idCli = Convert.ToInt32(Session["idCliente"]);
            conBuscarCliente1.Visible = true;
            conBuscarCliente1.Habilitar(false);
            BotonConsultar1.Visible = false;
            BotonEditar1.Visible = false;
            BotonImprimir1.Visible = false;
            BotonNuevo1.Visible = false;
            BotonGrabar1.Visible = true;
            BotonCancelar1.Visible = true;
            pnInfoBasico.Visible = true;
            pnlPerJuridica.Visible = true;
        }

        protected void BotonNuevo1_Click(object sender, EventArgs e)
        {
            hcOperacion.Value = "N";
            Session["idCliente"] = null;
            conBuscarCliente1.Visible = false;
            BotonConsultar1.Visible = false;
            BotonEditar1.Visible = false;
            BotonImprimir1.Visible = false;
            BotonNuevo1.Visible = false;
            BotonGrabar1.Visible = true;
            BotonCancelar1.Visible = true;
            pnInfoBasico.Visible = true;
            pnPerNatural.Visible = true;
            BotonGrabar1.Enabled = true;
            BotonCancelar1.Enabled = true;
        }

        protected void BotonImprimir1_Click(object sender, EventArgs e)
        {

            List<ReportParameter> ListaParametros = new List<ReportParameter>();
            List<ReportDataSource> ListaDataSource = new List<ReportDataSource>();

                     
             string reportpath = "";

             int idCli = Convert.ToInt32( pnidCli.Value);
          

             ListaDataSource.Add(new ReportDataSource("CLI_DatosCliente_reporte_sp", new SGA.LogicaNegocio.clsCNCredito().rptFichaSocio(idCli)));
             ListaDataSource.Add(new ReportDataSource("Gen_ListaDirCli_Sp", new SGA.LogicaNegocio.clsCNCredito().rptFichaSocio_direccion(idCli)));

            ListaParametros.Add(new ReportParameter("pnidCli", idCli.ToString(), false));

            reportpath = "rptFichaSocio.rdlc";
           

            Session["ListaParametros"] = ListaParametros;
            Session["ListaDataSource"] = ListaDataSource;
            Session["lModal"] = true;

            var cReporte = reportpath;

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../frmGenReporte.aspx?cNomReporte=" + cReporte + "');", true);

        }

        protected void pnidCli_ValueChanged(object sender, EventArgs e)
        {

        }

        protected void hIdCuenta_ValueChanged(object sender, EventArgs e)
        {

        }      
    }
}