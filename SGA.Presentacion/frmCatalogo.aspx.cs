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
    public partial class frmCatalogo : System.Web.UI.Page
    {
        #region Variables Globales

        clsCNGrupo cngrupo = new clsCNGrupo();
        clsCNCatalogo cncatalogo = new clsCNCatalogo();
        clsWebJScript script = new clsWebJScript();

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == true) return;
            //------------------Valida Session Vigente -------------------------------------->
            if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor"); }
            //------------------------------------------------------------------------------>
            pnlDetalle.Visible = false;
            //----------------- TITULO --------------------------->
            lblOpcion.Text = Session["cOpcion"].ToString().ToUpper() + ":";
            //---------------------------------------------------->

            this.trvGrupoActivo.ForeColor = System.Drawing.Color.Black;
            cargarGrupoActivo();
            cargarTipoBien();
            cargarUnidadCompra();
            cargarUnidadAlmacenaje();
            txtNombreProducto.Focus();
        }

        private void cargarGrupoActivo()
        {
            try
            {
                var dtMenu = cngrupo.ListarGrupoActivo();

                if (dtMenu.Rows.Count > 0)
                {
                    trvGrupoActivo.Nodes.Clear();
                    TreeNode padre;
                    TreeNode hijo;
                    for (int i = 0; i < dtMenu.Rows.Count; i++)
                    {
                        hijo = new TreeNode();
                        hijo.Text = ".  " + dtMenu.Rows[i]["cNombreGrupo"].ToString();
                        hijo.Value = dtMenu.Rows[i]["idGrupoActivo"].ToString();


                        hijo.ToolTip = dtMenu.Rows[i]["cNombreGrupo"].ToString();
                        padre = buscarPadre(dtMenu.Rows[i]["idPadre"].ToString(), trvGrupoActivo.Nodes);
                        if (padre == null)
                            this.trvGrupoActivo.Nodes.Add(hijo);
                        else
                            padre.ChildNodes.Add(hijo);
                        padre = null;
                    }
                    trvGrupoActivo.CollapseAll();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private TreeNode buscarPadre(String NodoBusqueda, TreeNodeCollection nodos)
        {
            try
            {
                TreeNode padre = null;
                bool encontrado = false;
                int contador = 0;

                while (encontrado == false && contador < nodos.Count)
                {
                    if (Convert.ToInt32(nodos[contador].Value) == Convert.ToInt32(NodoBusqueda))
                    {
                        encontrado = true;
                        padre = nodos[contador];
                    }
                    else
                    {
                        if (nodos[contador].ChildNodes.Count > 0)
                        {
                            padre = buscarPadre(NodoBusqueda, nodos[contador].ChildNodes);
                            if (padre != null)
                            {
                                encontrado = true;
                            }
                        }
                    }
                    contador++;
                }
                return padre;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void cargarTipoBien()
        {
            var dtTipoBien = cncatalogo.ListarTipoBien();
            if (dtTipoBien.Rows.Count > 0)
            {
                this.cboTipoBien.DataSource = dtTipoBien;
                cboTipoBien.DataValueField = dtTipoBien.Columns[0].ToString();
                cboTipoBien.DataTextField = dtTipoBien.Columns[1].ToString();
                cboTipoBien.DataBind();
            }
            else
            {
                cboTipoBien.Enabled = false;
            }
        }

        private void cargarUnidadCompra()
        {
            var dtUnidadCompra = cncatalogo.ListarUnidad();
            if (dtUnidadCompra.Rows.Count > 0)
            {
                this.cboUniCompra.DataSource = dtUnidadCompra;
                cboUniCompra.DataValueField = dtUnidadCompra.Columns[0].ToString();
                cboUniCompra.DataTextField = dtUnidadCompra.Columns[1].ToString();
                cboUniCompra.DataBind();
            }
            else
            {
                cboTipoBien.Enabled = false;
            }
        }

        private void cargarUnidadAlmacenaje()
        {
            var dtUnidadAlmacena = cncatalogo.ListarUnidad();
            if (dtUnidadAlmacena.Rows.Count > 0)
            {
                this.cboUniAlmacenaje.DataSource = dtUnidadAlmacena;
                cboUniAlmacenaje.DataValueField = dtUnidadAlmacena.Columns[0].ToString();
                cboUniAlmacenaje.DataTextField = dtUnidadAlmacena.Columns[1].ToString();
                cboUniAlmacenaje.DataBind();
            }
            else
            {
                cboTipoBien.Enabled = false;
            }
        }

        private void habilitarControles(bool lEstado)
        {
            this.txtCantidad.Enabled = lEstado;
            this.txtObservaciones.Enabled = lEstado;
            this.txtPrecioUnitario.Enabled = lEstado;
            this.txtProducto.Enabled = lEstado;
            this.txtValorConversion.Enabled = lEstado;
            this.cboTipoBien.Enabled = lEstado;
            this.cboUniAlmacenaje.Enabled = lEstado;
            this.cboUniCompra.Enabled = lEstado;
            this.chcActivo.Enabled = lEstado;
            trvGrupoActivo.Enabled = lEstado;
            this.txtCodigoExterno.Enabled = lEstado;
        }

        private void LimpiarControles()
        {
            this.txtCantidad.Text = "0.00";
            this.txtCodigoCatalogo.Text = "";
            this.txtObservaciones.Text = "";
            this.txtPrecioUnitario.Text = "0.00";
            this.txtProducto.Text = "";
            this.txtValorConversion.Text = "0.00";
            this.txtCodigoExterno.Text = "";
        }

        private bool validar()
        {
            bool lval = false;

            if (string.IsNullOrEmpty(this.txtProducto.Text))
            {
                script.Mensaje("Ingrese el nombre del producto.");
                this.txtProducto.Focus();
                return lval;
            }

            if (string.IsNullOrEmpty(this.txtPrecioUnitario.Text))
            {
                script.Mensaje("Ingrese el precio del producto.");
                this.txtPrecioUnitario.Focus();
                return lval;
            }

            if (string.IsNullOrEmpty(this.trvGrupoActivo.SelectedValue))
            {
                script.Mensaje("Seleccione el grupo al que pertenece el producto.");
                this.trvGrupoActivo.Focus();
                return lval;
            }


            if (this.cboTipoBien.SelectedIndex < 0)
            {
                script.Mensaje("Seleccione el tipo de bien");
                this.cboTipoBien.Focus();
                return lval;
            }

            if (this.cboUniCompra.SelectedIndex < 0)
            {
                script.Mensaje("Seleccione unidad de compra");
                this.cboUniCompra.Focus();
                return lval;
            }

            if (this.cboUniAlmacenaje.SelectedIndex < 0)
            {
                script.Mensaje("Seleccione unidad de almacenaje");
                this.cboUniAlmacenaje.Focus();
                return lval;
            }

            lval = true;
            return lval;
        }

        protected void trvGrupoActivo_SelectedNodeChanged(object sender, EventArgs e)
        {
            trvGrupoActivo.CollapseAll();
            hIdGrupo.Value = trvGrupoActivo.SelectedNode.Value;
            var cNodos = trvGrupoActivo.SelectedNode.ValuePath.Split('/');
            trvGrupoActivo.FindNode(cNodos[0]).ExpandAll();
            trvGrupoActivo.SelectedNode.Select();
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

            pnlDetalle.Visible = true;

            //--------- Búsqueda usuario  -------------->
            this.txtNombreProducto.Enabled = false;
            BotonConsultar1.Enabled = false;
            this.lstProductos.Visible = false;
            BotonEditar1.Visible = false;
            //------------------------------------------>

        }
        
        protected void BotonEditar1_Click(object sender, EventArgs e)
        {
            if (this.lstProductos.SelectedItem == null)
            {
                script.Mensaje("Debe seleccionar el producto al que desea editar sus datos");
                return;
            }

            habilitarControles(true);
            BotonEditar1.Visible = false;
            BotonGrabar1.Visible = true;
            BotonCancelar1.Visible = true;
            BotonNuevo1.Visible = false;
            hTipoOperacion.Value = "2";
            pnlDetalle.Visible = true;           

            this.txtNombreProducto.Enabled = false;
            BotonConsultar1.Enabled = false;

            pnlDetalle.Visible = true;

            //Ubicar al producto específico
            this.hIdCatalogo.Value = this.lstProductos.SelectedValue;
            cargarCatalogo();
        }

        private void cargarCatalogo()
        {
            int idCatalogo = Convert.ToInt32(hIdCatalogo.Value);

            var dtProducto = cncatalogo.ListarCatalogoXid(idCatalogo);

            txtCodigoCatalogo.Text = dtProducto.Rows[0]["idCatalogo"].ToString();
            this.txtProducto.Text = dtProducto.Rows[0]["cProducto"].ToString();
            this.cboTipoBien.SelectedValue= dtProducto.Rows[0]["idTipoBien"].ToString();
            this.cboUniCompra.SelectedValue = dtProducto.Rows[0]["idUnidadCompra"].ToString();
            this.cboUniAlmacenaje.SelectedValue= dtProducto.Rows[0]["idUnidadAlmacenaje"].ToString();
            this.txtValorConversion.Text = dtProducto.Rows[0]["nValConversion"].ToString();
            chcActivo.Checked = Convert.ToBoolean(dtProducto.Rows[0]["lIndActivo"]);
            this.txtObservaciones.Text = dtProducto.Rows[0]["cObservacion"].ToString();
            this.txtCantidad.Text = dtProducto.Rows[0]["nCantidad"].ToString();
            this.txtPrecioUnitario.Text = dtProducto.Rows[0]["nPrecioUnit"].ToString();
            this.txtCodigoExterno.Text = dtProducto.Rows[0]["cCodigoProducto"].ToString();
            this.trvGrupoActivo.FindNode(dtProducto.Rows[0]["cRutaGrupo"].ToString()).ExpandAll();
            this.trvGrupoActivo.FindNode(dtProducto.Rows[0]["cRutaGrupo"].ToString()).Selected = true;

            var padre = this.trvGrupoActivo.FindNode(dtProducto.Rows[0]["cRutaGrupo"].ToString()).Parent;
            if (padre != null)
            {
                padre.ExpandAll();
                var padrepadre = this.trvGrupoActivo.FindNode(padre.ValuePath).Parent;
                if (padrepadre != null)
                {
                    padrepadre.ExpandAll();
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
                    int idCatalogo = 0;
                    var cProducto = this.txtProducto.Text.Trim();
                    var nCantidad = Convert.ToDecimal(txtCantidad.Text);
                    var cObservaciones = this.txtObservaciones.Text;
                    var nPrecio = Convert.ToDecimal(this.txtPrecioUnitario.Text);
                    var nValorConversion = Convert.ToDecimal(this.txtValorConversion.Text);

                    var idTipoBien = Convert.ToInt32(this.cboTipoBien.SelectedValue);
                    var idUniCompra = Convert.ToInt32(this.cboUniCompra.SelectedValue);
                    var idUniAlmacenaje = Convert.ToInt32(this.cboUniAlmacenaje.SelectedValue);
                    var cCodigoExterno = txtCodigoExterno.Text.Trim();

                    var idGrupo = Convert.ToInt32(this.hIdGrupo.Value);

                    if (hTipoOperacion.Value.Equals("2"))
                    {
                        idCatalogo = Convert.ToInt32(this.txtCodigoCatalogo.Text);
                        this.cncatalogo.ActualizarCatalogo(idCatalogo, cProducto, idTipoBien, idGrupo, true, idUniCompra, idUniAlmacenaje, nValorConversion, usuarioSession.idUsuario, chcActivo.Checked, cObservaciones, 1, nCantidad, nPrecio, cCodigoExterno);
                    }
                    else
                    {
                        this.cncatalogo.InsertarCatalogo(cProducto, idTipoBien, idGrupo, true, idUniCompra, idUniAlmacenaje, nValorConversion, usuarioSession.idUsuario, chcActivo.Checked, cObservaciones, 1, nCantidad, nPrecio, cCodigoExterno);
                    }

                    script.Mensaje("Los datos se registraron correctamente.");

                    BotonEditar1.Visible = true;
                    BotonGrabar1.Visible = false;
                    BotonCancelar1.Visible = false;
                    BotonNuevo1.Visible = true;
                    this.cargarGrupoActivo();
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
            pnlDetalle.Visible = false;
            BotonEditar1.Visible = false;
            BotonGrabar1.Visible = false;
            BotonCancelar1.Visible = false;
            BotonNuevo1.Visible = true;
            this.cargarGrupoActivo();
            habilitarControles(false);

            //--------- Búsqueda   -------------->
            this.txtNombreProducto.Enabled = true;
            BotonConsultar1.Enabled = true;
            BotonEditar1.Visible = false;
            txtNombreProducto.Text = "";
            txtNombreProducto.Focus();
        }

        protected void BotonConsultar1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.txtNombreProducto.Text))
                {
                    script.Mensaje("Debe ingresar nombre del producto.");
                    lstProductos.DataSource = null;
                    lstProductos.Items.Clear();
                    return;
                }

                //------------------Valida Session Vigente -------------------------------------->
                if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor"); }
                //------------------------------------------------------------------------------>

                //Buscar 
                DataTable dtProductos = cncatalogo.BuscarCatalogo(txtNombreProducto.Text.Trim());

                if (dtProductos.Rows.Count == 0)
                {
                    script.Mensaje("No se ha encontrado productos con dicha descripción");
                    this.lstProductos.DataSource = null;
                    lstProductos.Items.Clear();
                    return;
                }

                Session["dtUsuario"] = dtProductos;
                lstProductos.DataSource = dtProductos;
                lstProductos.DataTextField = "cProducto";
                lstProductos.DataValueField = "idCatalogo";
                lstProductos.DataBind();

                lstProductos.Visible = true;
                BotonEditar1.Visible = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}