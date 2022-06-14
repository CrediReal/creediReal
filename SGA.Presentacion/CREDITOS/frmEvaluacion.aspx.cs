using SGA.LogicaNegocio;
using SGA.Utilitarios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGA.Presentacion.CREDITOS
{
    public partial class frmEvaluacion : System.Web.UI.Page
    {
        clsWebJScript script = new clsWebJScript();
        clsCNEvaluacion cneva = new clsCNEvaluacion();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //if (Request.QueryString["usuario"] != null)
                //{
                //    Session["DatosUsuarioSession"] = new SGA.LogicaNegocio.clsCNUsuario().GetDatosUsuario(Request.QueryString["usuario"].ToString());
                //}
               // if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor"); }

                if (IsPostBack) return;

                //lblOpcion.Text = Session["cOpcion"].ToString().ToUpper() + ":";
                cargarControles();
                if (Request.QueryString["idSolicitud"] != null)
                {
                    hidSolicitud.Value = Request.QueryString["idSolicitud"].ToString();
                }
                else
                {
                    hidSolicitud.Value = "0";
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void cargarControles()
        {
            var dtAdelanto = estructuraTabla();
            ViewState["dtAdelanto"] = dtAdelanto;
            dtgAdelanto.DataSource = dtAdelanto;
            dtgAdelanto.DataBind();

            var dtAdelantoProv = estructuraTabla();
            ViewState["dtAdelantoProv"] = dtAdelantoProv;
            dtgAdelantoProv.DataSource = dtAdelantoProv;
            dtgAdelantoProv.DataBind();

            var dtBanco = estructuraTabla();
            ViewState["dtBanco"] = dtBanco;
            dtgBancos.DataSource = dtBanco;
            dtgBancos.DataBind();

            var dtCtaCobrar = estructuraTabla();
            ViewState["dtCtaCobrar"] = dtCtaCobrar;
            dtgCtaCobrar.DataSource = dtCtaCobrar;
            dtgCtaCobrar.DataBind();

            var dtDeuda = estructuraTabla();
            ViewState["dtDeuda"] = dtDeuda;
            dtgDeuda.DataSource = dtDeuda;
            dtgDeuda.DataBind();

            var dtDeudaLargo = estructuraTabla();
            ViewState["dtDeudaLargo"] = dtDeudaLargo;
            dtgDeudaLargo.DataSource = dtDeudaLargo;
            dtgDeudaLargo.DataBind();

            var dtInmueble = estructuraTabla();
            ViewState["dtInmueble"] = dtInmueble;
            dtgInmueble.DataSource = dtInmueble;
            dtgInmueble.DataBind();

            var dtInventario = estructuraTabla();
            ViewState["dtInventario"] = dtInventario;
            dtgInventario.DataSource = dtInventario;
            dtgInventario.DataBind();

            var dtMueble = estructuraTabla();
            ViewState["dtMueble"] = dtMueble;
            dtgMueble.DataSource = dtMueble;
            dtgMueble.DataBind();

            var dtCreditodirecto = estructuraTabla();
            ViewState["dtCreditodirecto"] = dtCreditodirecto;
            this.dtgCreditoDirecto.DataSource = dtCreditodirecto;
            dtgCreditoDirecto.DataBind();

            var dtCreditoindirecto = estructuraTabla();
            ViewState["dtCreditoindirecto"] = dtCreditoindirecto;
            this.dtgCreditosIndirectos.DataSource = dtCreditoindirecto;
            dtgCreditosIndirectos.DataBind();

            var dtVentaServicio = estructuraTablaEstResul();
            ViewState["dtVentaServicio"] = dtVentaServicio;
            this.dtgVentaServicio.DataSource = dtVentaServicio;
            dtgVentaServicio.DataBind();

            var dtVentaProd = estructuraTablaEstResul();
            ViewState["dtVentaProd"] = dtVentaProd;
            this.dtgVentaProd.DataSource = dtVentaProd;
            dtgVentaProd.DataBind();

        }

        protected void btnActCorriente_Click(object sender, EventArgs e)
        {
            pnlActivoCorriente.Visible = true;
            pnlActNoCorriente.Visible = false;
            pnlPasivo.Visible = false;
            pnlPasivoNoCor.Visible = false;
            pnlPatrimonio.Visible = false;
            pnlEstadoresultado.Visible = false;
        }

        protected void btnActNoCorriente_Click(object sender, EventArgs e)
        {
            pnlActivoCorriente.Visible = false;
            pnlActNoCorriente.Visible = true;
            pnlPasivo.Visible = false;
            pnlPasivoNoCor.Visible = false;
            pnlPatrimonio.Visible = false;
            pnlEstadoresultado.Visible = false;
        }

        protected void btnPasCorriente_Click(object sender, EventArgs e)
        {
            pnlActivoCorriente.Visible = false;
            pnlActNoCorriente.Visible = false;
            pnlPasivo.Visible = true;
            pnlPasivoNoCor.Visible = false;
            pnlPatrimonio.Visible = false;
            pnlEstadoresultado.Visible = false;
        }

        protected void btnPasNoCorriente_Click(object sender, EventArgs e)
        {
            pnlActivoCorriente.Visible = false;
            pnlActNoCorriente.Visible = false;
            pnlPasivo.Visible = false;
            pnlPasivoNoCor.Visible = true;
            pnlPatrimonio.Visible = false;
            pnlEstadoresultado.Visible = false;
        }

        protected void btnPatrimonio_Click(object sender, EventArgs e)
        {
            pnlActivoCorriente.Visible = false;
            pnlActNoCorriente.Visible = false;
            pnlPasivo.Visible = false;
            pnlPasivoNoCor.Visible = false;
            pnlPatrimonio.Visible = true;
            pnlEstadoresultado.Visible = false;
        }

        protected void dtgBancos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int nIndex = e.RowIndex;

            DataTable dtBanco = (DataTable)ViewState["dtBanco"];
            DataRow drBanco = dtBanco.Rows[nIndex];
            dtBanco.Rows.Remove(drBanco);
            dtBanco.AcceptChanges();

            ViewState["dtBanco"] = dtBanco;
            dtgBancos.DataSource = dtBanco;
            dtgBancos.DataBind();
        }

        protected void dtgCtaCobrar_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int nIndex = e.RowIndex;

            DataTable dtCtaCobrar = (DataTable)ViewState["dtCtaCobrar"];
            DataRow dr = dtCtaCobrar.Rows[nIndex];
            dtCtaCobrar.Rows.Remove(dr);
            dtCtaCobrar.AcceptChanges();

            ViewState["dtCtaCobrar"] = dtCtaCobrar;
            dtgCtaCobrar.DataSource = dtCtaCobrar;
            dtgCtaCobrar.DataBind();
        }

        protected void dtgAdelanto_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int nIndex = e.RowIndex;

            DataTable dtAdelanto = (DataTable)ViewState["dtAdelanto"];
            DataRow dr = dtAdelanto.Rows[nIndex];
            dtAdelanto.Rows.Remove(dr);
            dtAdelanto.AcceptChanges();

            ViewState["dtAdelanto"] = dtAdelanto;
            dtgAdelanto.DataSource = dtAdelanto;
            dtgAdelanto.DataBind();
        }

        protected void dtgInventario_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int nIndex = e.RowIndex;

            DataTable dtInventario = (DataTable)ViewState["dtInventario"];
            DataRow dr = dtInventario.Rows[nIndex];
            dtInventario.Rows.Remove(dr);
            dtInventario.AcceptChanges();

            ViewState["dtInventario"] = dtInventario;
            dtgInventario.DataSource = dtInventario;
            dtgInventario.DataBind();
        }

        protected void dtgMueble_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int nIndex = e.RowIndex;

            DataTable dtMueble = (DataTable)ViewState["dtMueble"];
            DataRow dr = dtMueble.Rows[nIndex];
            dtMueble.Rows.Remove(dr);
            dtMueble.AcceptChanges();

            ViewState["dtMueble"] = dtMueble;
            dtgMueble.DataSource = dtMueble;
            dtgMueble.DataBind();
        }

        protected void dtgInmueble_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int nIndex = e.RowIndex;

            DataTable dtInmueble = (DataTable)ViewState["dtInmueble"];
            DataRow dr = dtInmueble.Rows[nIndex];
            dtInmueble.Rows.Remove(dr);
            dtInmueble.AcceptChanges();

            ViewState["dtInmueble"] = dtInmueble;
            dtgInmueble.DataSource = dtInmueble;
            dtgInmueble.DataBind();
        }

        protected void dtgDeuda_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int nIndex = e.RowIndex;

            DataTable dtDeuda = (DataTable)ViewState["dtDeuda"];
            DataRow dr = dtDeuda.Rows[nIndex];
            dtDeuda.Rows.Remove(dr);
            dtDeuda.AcceptChanges();

            ViewState["dtDeuda"] = dtDeuda;
            dtgDeuda.DataSource = dtDeuda;
            dtgDeuda.DataBind();
        }

        protected void dtgAdelantoProv_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int nIndex = e.RowIndex;

            DataTable dtAdelantoProv = (DataTable)ViewState["dtAdelantoProv"];
            DataRow dr = dtAdelantoProv.Rows[nIndex];
            dtAdelantoProv.Rows.Remove(dr);
            dtAdelantoProv.AcceptChanges();

            ViewState["dtAdelantoProv"] = dtAdelantoProv;
            dtgAdelantoProv.DataSource = dtAdelantoProv;
            dtgAdelantoProv.DataBind();
        }

        protected void dtgDeudaLargo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int nIndex = e.RowIndex;

            DataTable dtDeudaLargo = (DataTable)ViewState["dtDeudaLargo"];
            DataRow dr = dtDeudaLargo.Rows[nIndex];
            dtDeudaLargo.Rows.Remove(dr);
            dtDeudaLargo.AcceptChanges();

            ViewState["dtDeudaLargo"] = dtDeudaLargo;
            dtgDeudaLargo.DataSource = dtDeudaLargo;
            dtgDeudaLargo.DataBind();
        }

        protected void botonAgregaBanco_Click(object sender, EventArgs e)
        {
            var dtBanco = (DataTable)ViewState["dtBanco"];
            dtBanco.Rows.Add(dtBanco.Rows.Count + 1, txtBanco.Text, txtBancoVal.Value);
            ViewState["dtBanco"] = dtBanco;
            dtgBancos.DataSource = dtBanco;
            dtgBancos.DataBind();
        }

        protected void BotonAgregarCtaCobrar_Click(object sender, EventArgs e)
        {
            var dtCtaCobrar = (DataTable)ViewState["dtCtaCobrar"];
            dtCtaCobrar.Rows.Add(dtCtaCobrar.Rows.Count + 1, this.txtCtaCobrar.Text, this.txtCtaCobrarVal.Value);
            ViewState["dtCtaCobrar"] = dtCtaCobrar;
            dtgCtaCobrar.DataSource = dtCtaCobrar;
            dtgCtaCobrar.DataBind();
        }

        protected void BotonAgregarAdelanto_Click(object sender, EventArgs e)
        {
            var dtAdelanto = (DataTable)ViewState["dtAdelanto"];
            dtAdelanto.Rows.Add(dtAdelanto.Rows.Count + 1, this.txtAdelanto.Text, this.txtAdelantoVal.Value);
            ViewState["dtAdelanto"] = dtAdelanto;
            dtgAdelanto.DataSource = dtAdelanto;
            dtgAdelanto.DataBind();
        }

        protected void BotonAgregarInventario_Click(object sender, EventArgs e)
        {
            var dtInventario = (DataTable)ViewState["dtInventario"];
            dtInventario.Rows.Add(dtInventario.Rows.Count + 1, this.txtInventario.Text, this.txtInventarioVal.Value);
            ViewState["dtInventario"] = dtInventario;
            dtgInventario.DataSource = dtInventario;
            dtgInventario.DataBind();
        }

        protected void BotonAgregarMueble_Click(object sender, EventArgs e)
        {
            var dtMueble = (DataTable)ViewState["dtMueble"];
            dtMueble.Rows.Add(dtMueble.Rows.Count + 1, this.txtMueble.Text, this.txtMuebleVal.Value);
            ViewState["dtMueble"] = dtMueble;
            dtgMueble.DataSource = dtMueble;
            dtgMueble.DataBind();
        }

        protected void BotonAgregarInmueble_Click(object sender, EventArgs e)
        {
            var dtInmueble = (DataTable)ViewState["dtInmueble"];
            dtInmueble.Rows.Add(dtInmueble.Rows.Count + 1, this.txtInmueble.Text, this.txtInmuebleVal.Value);
            ViewState["dtInmueble"] = dtInmueble;
            dtgInmueble.DataSource = dtInmueble;
            dtgInmueble.DataBind();
        }

        protected void BotonAgregarDeuda_Click(object sender, EventArgs e)
        {
            var dtDeuda = (DataTable)ViewState["dtDeuda"];
            dtDeuda.Rows.Add(dtDeuda.Rows.Count + 1, this.txtDeuda.Text, this.txtDeudaVal.Value);
            ViewState["dtDeuda"] = dtDeuda;
            dtgDeuda.DataSource = dtDeuda;
            dtgDeuda.DataBind();
        }

        protected void BotonAgregarAdelantoProv_Click(object sender, EventArgs e)
        {
            var dtAdelantoProv = (DataTable)ViewState["dtAdelantoProv"];
            dtAdelantoProv.Rows.Add(dtAdelantoProv.Rows.Count + 1, this.txtAdelanto.Text, this.txtAdelantoVal.Value);
            ViewState["dtAdelantoProv"] = dtAdelantoProv;
            dtgAdelantoProv.DataSource = dtAdelantoProv;
            dtgAdelantoProv.DataBind();
        }

        protected void BotonAgregarDeudaLargo_Click(object sender, EventArgs e)
        {
            var dtDeudaLargo = (DataTable)ViewState["dtDeudaLargo"];
            dtDeudaLargo.Rows.Add(dtDeudaLargo.Rows.Count + 1, this.txtDeudaLargo.Text, this.txtDeudaLargoVal.Value);
            ViewState["dtDeudaLargo"] = dtDeudaLargo;
            this.dtgDeudaLargo.DataSource = dtDeudaLargo;
            dtgDeudaLargo.DataBind();
        }

        private DataTable estructuraTabla()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id",typeof(int));
            dt.Columns.Add("cDescripcion", typeof(string));
            dt.Columns.Add("nMonto", typeof(double));
            return dt;
        }

        private DataTable estructuraTablaEstResul()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("cDescripcion", typeof(string));
            dt.Columns.Add("nCantidad", typeof(double));
            dt.Columns.Add("nPrecio", typeof(double));
            dt.Columns.Add("nMonto", typeof(double));
            return dt;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            int idSolicitud = Convert.ToInt32(hidSolicitud.Value);
            DateTime dFechaReg=DateTime.Now.Date;
            int idTipoEvaluacion=1;
            
            var dtResultado =cneva.InsertarEvaluacionCredito(idSolicitud,dFechaReg,idTipoEvaluacion);
            if (dtResultado.Rows.Count > 0)
            {
                var idEvaluacion = Convert.ToInt32(dtResultado.Rows[0]["idEvaluacion"]);
                decimal nCaja = Convert.ToDecimal(txtCajaEfectivo.Value);
                decimal nOtrosActivos = Convert.ToDecimal(txtOtrosActVal.Value);
                decimal nOtroPasivoCorriente = Convert.ToDecimal(txtOtrosPasCorVal.Value);
                decimal nOtroPasivoNoCorriente = Convert.ToDecimal(txtOtrosPasNoCorVal.Value);
                decimal nPatrimonio = Convert.ToDecimal(txtPatrimonioVal.Value);
                decimal nCostoOperativo=0;
                decimal nTributo=0;
                decimal nTransporte=0;
                decimal nAlquiler=0;
                decimal nServicios=0;
                decimal nOtros=0;
                cneva.InsertarEvaluacionPyme(idEvaluacion, nCaja, nOtrosActivos, nOtroPasivoCorriente, nOtroPasivoNoCorriente, nPatrimonio,nCostoOperativo,nTributo,nTransporte,nAlquiler,nServicios,nOtros);

                var dtAdelanto = (DataTable)ViewState["dtAdelanto"];
                var dtAdelantoProv = (DataTable)ViewState["dtAdelantoProv"];
                var dtBanco = (DataTable)ViewState["dtBanco"];
                var dtCtaCobrar = (DataTable)ViewState["dtCtaCobrar"];
                var dtDeuda = (DataTable)ViewState["dtDeuda"];
                var dtDeudaLargo = (DataTable)ViewState["dtDeudaLargo"];
                var dtInmueble = (DataTable)ViewState["dtInmueble"];
                var dtInventario = (DataTable)ViewState["dtInventario"];
                var dtMueble = (DataTable)ViewState["dtMueble"];
                var dtCreditodirecto = (DataTable)ViewState["dtCreditodirecto"];
                var dtCreditoindirecto = (DataTable)ViewState["dtCreditoindirecto"];
                var dtVentaServicio = (DataTable)ViewState["dtVentaServicio"];
                var dtVentaProd = (DataTable)ViewState["dtVentaProd"];

                foreach (DataRow item in dtAdelanto.Rows)
                {
                    cneva.InsertarEvaluacionPymeDatosBalance(idEvaluacion, item["cDescripcion"].ToString(),Convert.ToDecimal(item["nMonto"]), 1, true);
                }

                foreach (DataRow item in dtAdelantoProv.Rows)
                {
                    cneva.InsertarEvaluacionPymeDatosBalance(idEvaluacion, item["cDescripcion"].ToString(), Convert.ToDecimal(item["nMonto"]), 2, true);
                }

                foreach (DataRow item in dtBanco.Rows)
                {
                    cneva.InsertarEvaluacionPymeDatosBalance(idEvaluacion, item["cDescripcion"].ToString(), Convert.ToDecimal(item["nMonto"]), 3, true);
                }


                foreach (DataRow item in dtCtaCobrar.Rows)
                {
                    cneva.InsertarEvaluacionPymeDatosBalance(idEvaluacion, item["cDescripcion"].ToString(), Convert.ToDecimal(item["nMonto"]), 4, true);
                }

                foreach (DataRow item in dtDeuda.Rows)
                {
                    cneva.InsertarEvaluacionPymeDatosBalance(idEvaluacion, item["cDescripcion"].ToString(), Convert.ToDecimal(item["nMonto"]), 5, true);
                }

                foreach (DataRow item in dtDeudaLargo.Rows)
                {
                    cneva.InsertarEvaluacionPymeDatosBalance(idEvaluacion, item["cDescripcion"].ToString(), Convert.ToDecimal(item["nMonto"]), 6, true);
                }


                foreach (DataRow item in dtInmueble.Rows)
                {
                    cneva.InsertarEvaluacionPymeDatosBalance(idEvaluacion, item["cDescripcion"].ToString(), Convert.ToDecimal(item["nMonto"]), 7, true);
                }

                foreach (DataRow item in dtInventario.Rows)
                {
                    cneva.InsertarEvaluacionPymeDatosBalance(idEvaluacion, item["cDescripcion"].ToString(), Convert.ToDecimal(item["nMonto"]), 8, true);
                }

                foreach (DataRow item in dtMueble.Rows)
                {
                    cneva.InsertarEvaluacionPymeDatosBalance(idEvaluacion, item["cDescripcion"].ToString(), Convert.ToDecimal(item["nMonto"]), 9, true);
                }

                foreach (DataRow item in dtCreditodirecto.Rows)
                {
                    cneva.InsertarEvaluacionPymeDatosBalance(idEvaluacion, item["cDescripcion"].ToString(), Convert.ToDecimal(item["nMonto"]), 10, true);
                }

                foreach (DataRow item in dtCreditoindirecto.Rows)
                {
                    cneva.InsertarEvaluacionPymeDatosBalance(idEvaluacion, item["cDescripcion"].ToString(), Convert.ToDecimal(item["nMonto"]), 11, true);
                }

                foreach (DataRow item in dtVentaServicio.Rows)
                {
                    cneva.InsertarEvaluacionPymeEstado(idEvaluacion,item["cDescripcion"].ToString(), Convert.ToDecimal(item["nCantidad"]), Convert.ToDecimal(item["nPrecio"]), Convert.ToDecimal(item["nMonto"]),12,true);
                }

                foreach (DataRow item in dtVentaProd.Rows)
                {
                    cneva.InsertarEvaluacionPymeEstado(idEvaluacion, item["cDescripcion"].ToString(), Convert.ToDecimal(item["nCantidad"]), Convert.ToDecimal(item["nPrecio"]), Convert.ToDecimal(item["nMonto"]), 13, true);
                }

                btnImprimir.Visible = true;
                script.Mensaje("Los datos se guardaron correctamente");

            }
            
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {

        }

        protected void btnEstadoResultado_Click(object sender, EventArgs e)
        {
            pnlActivoCorriente.Visible = false;
            pnlActNoCorriente.Visible = false;
            pnlPasivo.Visible = false;
            pnlPasivoNoCor.Visible = false;
            pnlPatrimonio.Visible = false;
            pnlEstadoresultado.Visible = true;
        }

        protected void btnAgregarVentaServicio_Click(object sender, EventArgs e)
        {
            var dtVentaServicio = (DataTable)ViewState["dtVentaServicio"];
            var nMonto = Convert.ToDecimal(txtCantidad.Text) * Convert.ToDecimal(this.txtPrecio.Value);

            dtVentaServicio.Rows.Add(dtVentaServicio.Rows.Count + 1, this.txtDescripcion.Text, txtCantidad.Text, this.txtPrecio.Value, nMonto);
            ViewState["dtVentaServicio"] = dtVentaServicio;
            this.dtgVentaServicio.DataSource = dtVentaServicio;
            dtgVentaServicio.DataBind();
        }

        protected void btnAgregarProd_Click(object sender, EventArgs e)
        {
            var dtVentaProd = (DataTable)ViewState["dtVentaProd"];
            var nMonto = Convert.ToDecimal(txtCantidadProd.Text) * Convert.ToDecimal(this.txtPrecioProd.Value);

            dtVentaProd.Rows.Add(dtVentaProd.Rows.Count + 1, this.txtDescProd.Text, this.txtCantidadProd.Text, this.txtPrecioProd.Value, nMonto);
            ViewState["dtVentaProd"] = dtVentaProd;
            this.dtgVentaProd.DataSource = dtVentaProd;
            dtgVentaProd.DataBind();
        }

        protected void dtgVentaProd_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int nIndex = e.RowIndex;

            DataTable dtVentaProd = (DataTable)ViewState["dtVentaProd"];
            DataRow drVentaProd = dtVentaProd.Rows[nIndex];
            dtVentaProd.Rows.Remove(drVentaProd);
            dtVentaProd.AcceptChanges();

            ViewState["dtVentaProd"] = dtVentaProd;
            this.dtgVentaProd.DataSource = dtVentaProd;
            dtgVentaProd.DataBind();
        }

        protected void dtgVentaServicio_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int nIndex = e.RowIndex;

            DataTable dtVentaServicio = (DataTable)ViewState["dtVentaServicio"];
            DataRow drVentaServicio = dtVentaServicio.Rows[nIndex];
            dtVentaServicio.Rows.Remove(drVentaServicio);
            dtVentaServicio.AcceptChanges();

            ViewState["dtVentaServicio"] = dtVentaServicio;
            this.dtgVentaServicio.DataSource = dtVentaServicio;
            dtgVentaServicio.DataBind();
        }

        protected void btnAgregarFinanciera_Click(object sender, EventArgs e)
        {
            var dtCreditodirecto = (DataTable)ViewState["dtCreditodirecto"];
            dtCreditodirecto.Rows.Add(dtCreditodirecto.Rows.Count + 1, this.txtEntidad.Text, this.txtMontoEntidad.Value);
            ViewState["dtCreditodirecto"] = dtCreditodirecto;
            this.dtgCreditoDirecto.DataSource = dtCreditodirecto;
            dtgCreditoDirecto.DataBind();
        }

        protected void dtgCreditoDirecto_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int nIndex = e.RowIndex;

            DataTable dtCreditoindirecto = (DataTable)ViewState["dtCreditoindirecto"];
            DataRow drCreditoindirecto = dtCreditoindirecto.Rows[nIndex];
            dtCreditoindirecto.Rows.Remove(drCreditoindirecto);
            dtCreditoindirecto.AcceptChanges();

            ViewState["dtCreditoindirecto"] = dtCreditoindirecto;
            this.dtgCreditosIndirectos.DataSource = dtCreditoindirecto;
            dtgCreditosIndirectos.DataBind();
        }

        protected void btnAgregarEntidadIndirecto_Click(object sender, EventArgs e)
        {
            var dtCreditoindirecto = (DataTable)ViewState["dtCreditoindirecto"];
            dtCreditoindirecto.Rows.Add(dtCreditoindirecto.Rows.Count + 1, this.txtEntidadIndirecto.Text, this.txtMontoIndirecto.Value);
            ViewState["dtCreditoindirecto"] = dtCreditoindirecto;
            this.dtgCreditosIndirectos.DataSource = dtCreditoindirecto;
            dtgCreditosIndirectos.DataBind();
        }

        protected void dtgCreditosIndirectos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int nIndex = e.RowIndex;

            DataTable dtCreditoindirecto = (DataTable)ViewState["dtCreditoindirecto"];
            DataRow drCreditoindirecto = dtCreditoindirecto.Rows[nIndex];
            dtCreditoindirecto.Rows.Remove(drCreditoindirecto);
            dtCreditoindirecto.AcceptChanges();

            ViewState["dtCreditoindirecto"] = dtCreditoindirecto;
            this.dtgCreditosIndirectos.DataSource = dtCreditoindirecto;
            dtgCreditosIndirectos.DataBind();
        }
    }
}