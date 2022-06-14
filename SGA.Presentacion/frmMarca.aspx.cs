using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGA.ENTIDADES;
using SGA.LogicaNegocio;
using SGA.Utilitarios;

namespace SGA.Presentacion
{
    public partial class frmMarca : Page
    {
        #region Variables_Globales

        clsWebJScript Script = new clsWebJScript();

        public clsMarca ObjMarca
        {
            get
            {
                clsMarca objMarca = ViewState["ObjMarca"] as clsMarca;
                return objMarca ?? new clsMarca();
            }
            set
            {
                ViewState["ObjMarca"] = value;
            }
        }

        public List<clsMarca> LstMarcas
        {
            get
            {
                List<clsMarca> lstMarcas = ViewState["lstMarcas"] as List<clsMarca>;
                return lstMarcas ?? new List<clsMarca>();
            }
            set
            {
                ViewState["lstMarcas"] = value;
            }
        }

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor"); }

                if (IsPostBack) return;

                lblOpcion.Text = Session["cOpcion"].ToString().ToUpper() + ":";
                pnlBotones.Visible = true;
                pnlBusqueda.Visible = true;
                pnlForm.Visible = false;

                //Buscar al usuario por nombre
                LstMarcas = new clsCNMarca().ListarMarcas(0,string.Empty);

                if (!LstMarcas.Any())
                {
                    Script.Mensaje("No se encontraron resultados.");
                }

                grvMarcas.DataSource = LstMarcas;
                grvMarcas.DataBind();
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void BotonEditar_Click(object sender, EventArgs e)
        {
            int idMarca = Convert.ToInt32(((Button)sender).CommandArgument);

            ObjMarca = LstMarcas.FirstOrDefault(x => x.idMarca == idMarca);
            if (ObjMarca == null)
            {
                Script.Mensaje("No se puede editar el registro.");
                return;
            }

            pnlBotones.Visible = false;
            pnlBusqueda.Visible = true;
            pnlForm.Visible = true;

            FillControls(ObjMarca);
            HabilitarControles(true);

        }

        protected void BotonNuevo_Click(object sender, EventArgs e)
        {
            LimpiarControles();

            ObjMarca = new clsMarca();
            FillControls(ObjMarca);
            HabilitarControles(true);

            pnlBotones.Visible = false;
            pnlBusqueda.Visible = true;
            pnlForm.Visible = true;
        }

        protected void BotonGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                //------------------Valida Session Vigente -------------------------------------->
                if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor"); }
                //------------------------------------------------------------------------------>

                var usuarioSession = (clsUsuario)Session["DatosUsuarioSession"];
                if (CamposSonValidos() == false) return;


                ObjMarca.cMarca = txtMarca.Text.Trim().ToUpper();
                ObjMarca.lVigente = chcVigente.Checked;
                ObjMarca.idUsuario = usuarioSession.idUsuario;

                clsDBResp objResp = new clsCNMarca().SaveMarca(ObjMarca);
                if (objResp.nMsje == 0)
                {
                    pnlBotones.Visible = true;
                    pnlBusqueda.Visible = true;
                    pnlForm.Visible = false;

                    LimpiarControles();

                    LstMarcas = new clsCNMarca().ListarMarcas(0,String.Empty);
                    grvMarcas.DataSource = LstMarcas;
                    grvMarcas.DataBind();

                    Script.Mensaje(objResp.cMsje);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void BotonCancelar_Click(object sender, EventArgs e)
        {
            LimpiarControles();

            pnlBotones.Visible = true;
            pnlBusqueda.Visible = true;
            pnlForm.Visible = false;
        }

        #endregion

        #region Metodos

        private void LimpiarControles()
        {
            txtCodigo.Text = string.Empty;
            txtMarca.Text = string.Empty;
            chcVigente.Checked = false;
        }

        private bool CamposSonValidos()
        {
            if (string.IsNullOrEmpty(txtMarca.Text.Trim()))
            {
                Script.Mensaje("Ingrese la descripción de la marca.");
                return false;
            }
            return true;
        }

        private void FillControls(clsMarca objMarca)
        {
            txtCodigo.Text = objMarca.idMarca == 0 ? string.Empty : objMarca.idMarca.ToString();
            txtMarca.Text = objMarca.cMarca;
            chcVigente.Checked = objMarca.lVigente;
        }

        private void HabilitarControles(bool lHabil)
        {
            txtCodigo.Enabled = false;
            txtMarca.Enabled = lHabil;
            chcVigente.Enabled = lHabil;
        }

        #endregion

    }
}