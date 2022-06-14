using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGA.Controles;
using SGA.ENTIDADES;
using SGA.LogicaNegocio;
using SGA.Utilitarios;

namespace SGA.Presentacion
{
    public partial class frmVentas : PageBase
    {
        #region Variables_Globales

        clsWebJScript Script = new clsWebJScript();

        public clsVenta ObjVenta
        {
            get
            {
                clsVenta objVenta = ViewState["ObjVenta"] as clsVenta;
                return objVenta ?? new clsVenta();
            }
            set
            {
                ViewState["ObjVenta"] = value;
            }
        }

        public List<clsVenta> LstVentas
        {
            get
            {
                List<clsVenta> lstVentas = ViewState["lstVentas"] as List<clsVenta>;
                return lstVentas ?? new List<clsVenta>();
            }
            set
            {
                ViewState["lstVentas"] = value;
            }
        }         

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (IsPostBack) return;

                lblOpcion.Text = Session["cOpcion"].ToString().ToUpper() + ":";

                CargarCombos();

                BuscarVentas(0);
                pnlBotones.Visible = true;
                pnlResultados.Visible = true;
                pnlForm.Visible = false;

            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            int idOficina = Convert.ToInt32(string.IsNullOrEmpty(cboOficinaBus.SelectedValue) ? "0" : cboOficinaBus.SelectedValue);
            BuscarVentas(idOficina);
        }

        protected void BotonEditar_Click(object sender, EventArgs e)
        {
            int idVenta = Convert.ToInt32(((Button)sender).CommandArgument);

            ObjVenta = LstVentas.FirstOrDefault(x => x.idVenta == idVenta);
            if (ObjVenta == null)
            {
                Script.Mensaje("No se puede editar el registro.");
                return;
            }

            pnlBotones.Visible = false;
            pnlResultados.Visible = true;
            pnlForm.Visible = true;

            List<clsDetVenta> DetVenta = new clsCNVenta().GetDetalleVentas(ObjVenta.idVenta);
            ObjVenta.DetalleVenta = DetVenta;
            FillControls(ObjVenta);
            HabilitarControles(true);

        }

        protected void BotonNuevo_Click(object sender, EventArgs e)
        {
            LimpiarControles();

            ObjVenta = new clsVenta()
            {
                idVenta = 0,
                idOficina = 1,
                idEstado =  1,
                DetalleVenta = new List<clsDetVenta>()
            };

            FillControls(ObjVenta);
            HabilitarControles(true);

            pnlBotones.Visible = false;
            pnlResultados.Visible = true;
            pnlForm.Visible = true;
        }

        protected void BotonGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                var usuarioSession = (clsUsuario)Session["DatosUsuarioSession"];
                if (CamposSonValidos() == false) return;

                ObjVenta.idOficina = Convert.ToInt32(cboOficina.SelectedValue);
                ObjVenta.idCli = Convert.ToInt32(Session["idCliente"]);
                ObjVenta.idEstado = 1;
                

                foreach (GridViewRow row in grvDetalleVenta.Rows)
                {
                    LabelBase lblIdInterno = row.FindControl("lblIdInterno") as LabelBase;
                    if (lblIdInterno != null)
                        GetDataRow(Convert.ToInt32(lblIdInterno.Text), row);
                }

                ObjVenta.nTotalVenta = ObjVenta.DetalleVenta.Sum(x => x.nCantidad * x.nPrecio);
                
                int idUsuReg = usuarioSession.idUsuario;

                clsDBResp objResp = new clsCNVenta().SaveVentas(ObjVenta, idUsuReg);
                if (objResp.nMsje == 0)
                {
                    pnlBotones.Visible = true;
                    pnlResultados.Visible = true;
                    pnlForm.Visible = false;

                    LimpiarControles();

                    BuscarVentas(0);

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
            pnlResultados.Visible = true;
            pnlForm.Visible = false;
        }

        protected void grvDetVentas_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                clsDetVenta objDetVenta = e.Row.DataItem as clsDetVenta;
                LabelBase lblIdInterno = e.Row.FindControl("lblIdInterno") as LabelBase;
                NumberBox txtCantidad = e.Row.FindControl("txtCantidad") as NumberBox;
                NumberBox txtPrecio = e.Row.FindControl("txtPrecio") as NumberBox;
                NumberBox txtTotal = e.Row.FindControl("txtTotal") as NumberBox;

                ComboBoxBaseMarca cboMarca = e.Row.FindControl("cboMarca") as ComboBoxBaseMarca;
                ComboBoxModelo cboModelo = e.Row.FindControl("cboModelo") as ComboBoxModelo;

                if (objDetVenta != null)
                {
                    if (lblIdInterno != null)
                        lblIdInterno.Text = objDetVenta.idInterno.ToString();
                    if (txtCantidad != null)
                            txtCantidad.Text = objDetVenta.nCantidad.ToString("#,0.00");
                    if (txtPrecio != null)
                        txtPrecio.Text = objDetVenta.nPrecio.ToString("#,0.00");
                    if (txtTotal != null)
                        txtTotal.Text = (objDetVenta.nCantidad*objDetVenta.nPrecio).ToString("#,0.00");
                    if (cboMarca != null)
                    {
                        cboMarca.Llenar();
                        if (cboMarca.Items.FindByValue(objDetVenta.idMarca.ToString()) != null)
                            cboMarca.SelectedValue = objDetVenta.idMarca.ToString();
                    }
                    if (cboModelo != null)
                    {
                        cboModelo.ListarModelos();
                        if (cboModelo.Items.FindByValue(objDetVenta.idModelo.ToString()) != null)
                            cboModelo.SelectedValue = objDetVenta.idModelo.ToString();
                    }
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                decimal nTotVenta = 0M;
                foreach (GridViewRow row in grvDetalleVenta.Rows)
                {
                    NumberBox txtTotal = row.FindControl("txtTotal") as NumberBox;
                    if (txtTotal != null)
                        nTotVenta += Convert.ToDecimal(txtTotal.Text);
                }
                NumberBox txtTotVenta = e.Row.FindControl("txtTotVenta") as NumberBox;
                if (txtTotVenta != null)
                    txtTotVenta.Text = nTotVenta.ToString("#,0.00");
            }
        }

        protected void BotonAgregar_OnClick(object sender, EventArgs e)
        {
            foreach (GridViewRow row in grvDetalleVenta.Rows)
            {
                LabelBase lblIdInterno = row.FindControl("lblIdInterno") as LabelBase;
                if (lblIdInterno != null)
                    GetDataRow(Convert.ToInt32(lblIdInterno.Text), row);
            }
            clsDetVenta objDetVenta = new clsDetVenta();
            objDetVenta.idInterno = ObjVenta.DetalleVenta.Any() ? ObjVenta.DetalleVenta.Max(x => x.idInterno) + 1 : 1;
            ObjVenta.DetalleVenta.Add(objDetVenta);
            grvDetalleVenta.DataSource = ObjVenta.DetalleVenta;
            DataBind();
        }

        protected void btnQuitarDetalle_OnClick(object sender, EventArgs e)
        {
            int idInterno = Convert.ToInt32(((Button)sender).CommandArgument);

            var objDetVenta = ObjVenta.DetalleVenta.FirstOrDefault(x => x.idInterno == idInterno);
            if (objDetVenta == null)
            {
                return;
            }

            ObjVenta.DetalleVenta.Remove(objDetVenta);
            grvDetalleVenta.DataSource = ObjVenta.DetalleVenta;
            DataBind();
        }

        #endregion

        #region Metodos

        private void LimpiarControles()
        {
            txtCodigo.Text = string.Empty;
            cboOficina.SelectedIndex = -1;
            grvDetalleVenta.DataSource = null;
            conBuscarCliente.ActivarBusqueda(true);
        }

        private bool CamposSonValidos()
        {
            if (Session["idCliente"] == null || (int)Session["idCliente"] == 0)
            {
                Script.Mensaje("No se ha seleccionado a ningun cliente.");
                return false;
            }
            if (string.IsNullOrEmpty(cboOficina.SelectedValue))
            {
                Script.Mensaje("Seleccione la oficina.");
                return false;
            }
            if (grvDetalleVenta.Rows.Count == 0)
            {
                Script.Mensaje("Ingrese el detalle de la venta");
                return false;
            }

            return true;
        }

        private void FillControls(clsVenta objVenta)
        {
            txtCodigo.Text = objVenta.idVenta == 0 ? string.Empty : objVenta.idVenta.ToString();

            if (cboOficina.Items.FindByValue(objVenta.idOficina.ToString()) != null)
                cboOficina.SelectedValue = objVenta.idOficina.ToString();
            else
                cboOficina.SelectedIndex = 0;

            grvDetalleVenta.DataSource = objVenta.DetalleVenta;
            grvDetalleVenta.DataBind();
        }

        private void HabilitarControles(bool lHabil)
        {
            txtCodigo.Enabled = false;
            cboOficina.Enabled = lHabil;
            grvDetalleVenta.Enabled = lHabil;
        }

        private void BuscarVentas(int idOficina)
        {
            if (!ValidarBusqueda())
                return;


            LstVentas = new clsCNVenta().GetVentas(idOficina);
            grvVentas.DataSource = LstVentas;
            grvVentas.DataBind();

            pnlResultados.Visible = true;
        }

        private bool ValidarBusqueda()
        {
            if (string.IsNullOrEmpty(cboOficinaBus.SelectedValue))
            {
                Script.Mensaje("Seleccione la oficina para la busqueda.");
                return false;
            }
            return true;
        }

        private void CargarCombos()
        {
            cboOficinaBus.ListarSoloVigentes();
            cboOficina.ListarSoloVigentes();

        }

        private void GetDataRow(int idInterno, GridViewRow row)
        {
            clsDetVenta objDetVenta = ObjVenta.DetalleVenta.FirstOrDefault(x => x.idInterno == idInterno);
            NumberBox txtCantidad = row.FindControl("txtCantidad") as NumberBox;
            NumberBox txtPrecio = row.FindControl("txtPrecio") as NumberBox;
            NumberBox txtTotal = row.FindControl("txtTotal") as NumberBox;

            ComboBoxBaseMarca cboMarca = row.FindControl("cboMarca") as ComboBoxBaseMarca;
            ComboBoxModelo cboModelo = row.FindControl("cboModelo") as ComboBoxModelo;

            if (objDetVenta != null)
            {
                if (txtCantidad != null)
                    objDetVenta.nCantidad = Convert.ToDecimal(txtCantidad.Text.Trim());
                if (txtPrecio != null)
                    objDetVenta.nPrecio = Convert.ToDecimal(txtPrecio.Text.Trim());
                if (txtTotal != null)
                    txtTotal.Text = (objDetVenta.nCantidad * objDetVenta.nPrecio).ToString("#,0.00");
                if (cboMarca != null)
                    objDetVenta.idMarca = Convert.ToInt32(cboMarca.SelectedValue);
                if (cboModelo != null)
                    objDetVenta.idModelo = Convert.ToInt32(cboModelo.SelectedValue);

                objDetVenta.nTotal = objDetVenta.nCantidad * objDetVenta.nPrecio;
            }
        }

        #endregion

    }
}