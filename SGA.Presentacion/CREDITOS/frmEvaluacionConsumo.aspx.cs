using HtmlAgilityPack;
using SGA.ENTIDADES;
using SGA.LogicaNegocio;
using SGA.Utilitarios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SGA.Presentacion.CREDITOS
{
    public partial class frmEvaluacionConsumo : System.Web.UI.Page
    {
        clsWebJScript script = new clsWebJScript();
        clsCNEvaluacion cneva = new clsCNEvaluacion();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    if (Request.QueryString["usuario"] != null)
                    {
                        Session["DatosUsuarioSession"] = new clsCNUsuario().GetDatosUsuario(Request.QueryString["usuario"].ToString());
                    }
                    if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor " + this.ToString()); }

                    if (IsPostBack) return;
                    hUsuario.Value = ((clsUsuario)Session["DatosUsuarioSession"]).cUsuario;
                    CargarControles();

                    if (Request.QueryString["idSolicitud"] != null)
                    {
                        hidSolicitud.Value = Request.QueryString["idSolicitud"].ToString();
                    }
                    else
                    {
                        hidSolicitud.Value = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        protected void btnAgregarCreditoComercial_Click(object sender, EventArgs e)
        {
            var dtBanco = (DataTable)ViewState["dtCreditos"];
            var nMonto = Convert.ToDouble(txtMontoBanco.Text == "" ? "0" : txtMontoBanco.Text);
            dtBanco.Rows.Add(dtBanco.Rows.Count + 1, txtBanco.Text, nMonto);
            ViewState["dtCreditos"] = dtBanco;
            dtgCreditosComerciales.DataSource = dtBanco;
            dtgCreditosComerciales.DataBind();

            SumaTotalBruto();
            SumaTotalNeto();
            SumaTotalFamiliar();
            SumaCreditos();
            SumaCreditosDirecto();
            SumaCreditosInDirecto();
        }
        
        protected void dtgCreditosComerciales_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int nIndex = e.RowIndex;

            DataTable dtCreditos = (DataTable)ViewState["dtCreditos"];
            DataRow drBanco = dtCreditos.Rows[nIndex];
            dtCreditos.Rows.Remove(drBanco);
            dtCreditos.AcceptChanges();

            ViewState["dtCreditos"] = dtCreditos;
            dtgCreditosComerciales.DataSource = dtCreditos;
            dtgCreditosComerciales.DataBind();
            SumaCreditos();
            SumaCreditosDirecto();
            SumaCreditosInDirecto();
        }
        
        protected void DtgCreditoDirecto_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int nIndex = e.RowIndex;

            DataTable dtCreditosDirecto = (DataTable)ViewState["dtCreditosDirecto"];
            DataRow drBanco = dtCreditosDirecto.Rows[nIndex];
            dtCreditosDirecto.Rows.Remove(drBanco);
            dtCreditosDirecto.AcceptChanges();

            ViewState["dtCreditosDirecto"] = dtCreditosDirecto;
            DtgCreditoDirecto.DataSource = dtCreditosDirecto;
            DtgCreditoDirecto.DataBind();
            SumaCreditos();
            SumaCreditosDirecto();
            SumaCreditosInDirecto();
        }

        protected void BtnAgregarDirecto_Click(object sender, EventArgs e)
        {
            var dtBancoDirecto = (DataTable)ViewState["dtCreditosDirecto"];
            var nMonto = Convert.ToDouble(txtMontoDirecto.Text == "" ? "0" : txtMontoDirecto.Text);
            dtBancoDirecto.Rows.Add(dtBancoDirecto.Rows.Count + 1, txtBancoDirecto.Text, nMonto);
            ViewState["dtCreditosDirecto"] = dtBancoDirecto;
            DtgCreditoDirecto.DataSource = dtBancoDirecto;
            DtgCreditoDirecto.DataBind();

            SumaTotalBruto();
            SumaTotalNeto();
            SumaTotalFamiliar();
            SumaCreditos();
            SumaCreditosDirecto();
            SumaCreditosInDirecto();
        }

        protected void BtnAgregarIndirecto_Click(object sender, EventArgs e)
        {
            var dtBancoInDirecto = (DataTable)ViewState["dtCreditosInDirecto"];
            var nMonto = Convert.ToDouble(txtMontoIndirecto.Text == "" ? "0" : txtMontoIndirecto.Text);
            dtBancoInDirecto.Rows.Add(dtBancoInDirecto.Rows.Count + 1, txtBancoIndirecto.Text, nMonto);
            ViewState["dtCreditosInDirecto"] = dtBancoInDirecto;
            DtgCreditoInDirecto.DataSource = dtBancoInDirecto;
            DtgCreditoInDirecto.DataBind();

            SumaTotalBruto();
            SumaTotalNeto();
            SumaTotalFamiliar();
            SumaCreditos();
            SumaCreditosDirecto();
            SumaCreditosInDirecto();
        }

        protected void DtgCreditoInDirecto_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int nIndex = e.RowIndex;

            DataTable dtCreditosInDirecto = (DataTable)ViewState["dtCreditosInDirecto"];
            DataRow drBanco = dtCreditosInDirecto.Rows[nIndex];
            dtCreditosInDirecto.Rows.Remove(drBanco);
            dtCreditosInDirecto.AcceptChanges();

            ViewState["dtCreditosInDirecto"] = dtCreditosInDirecto;
            DtgCreditoInDirecto.DataSource = dtCreditosInDirecto;
            DtgCreditoInDirecto.DataBind();
            SumaCreditos();
            SumaCreditosDirecto();
            SumaCreditosInDirecto();
        }

        private void CargarControles()
        {

            var dtBanco = EstructuraTabla();
            ViewState["dtCreditos"] = dtBanco;
            dtgCreditosComerciales.DataSource = dtBanco;
            dtgCreditosComerciales.DataBind();

            var dtBancoDirecto = EstructuraTabla();
            ViewState["dtCreditosDirecto"] = dtBancoDirecto;
            DtgCreditoDirecto.DataSource = dtBancoDirecto;
            DtgCreditoDirecto.DataBind();

            var dtBancoInDirecto = EstructuraTabla();
            ViewState["dtCreditosInDirecto"] = dtBancoInDirecto;
            DtgCreditoInDirecto.DataSource = dtBancoInDirecto;
            DtgCreditoInDirecto.DataBind();
        }

        private DataTable EstructuraTabla()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("cDescripcion", typeof(string));
            dt.Columns.Add("nMonto", typeof(double));
            return dt;
        }

        private void SumaTotalBruto()
        {
            var a = Convert.ToDecimal(txtIngresoBruto.Text == "" ? "0" : txtIngresoBruto.Text);
            var b = Convert.ToDecimal(txtIngresoConyuge.Text == "" ? "0" : txtIngresoConyuge.Text);
            var c = Convert.ToDecimal(txtComisiones.Text == "" ? "0" : txtComisiones.Text);
            var d = Convert.ToDecimal(txtOtrosIngresos.Text == "" ? "0" : txtOtrosIngresos.Text);
            //var eb = Convert.ToDecimal(txtRemuneracionBruta.Text == "" ? "0" : txtRemuneracionBruta.Text);

            txtTotalIngresoBruto.Text = (a + b + c + d).ToString();
        }

        private void SumaTotalNeto()
        {
            var a = Convert.ToDecimal(txtIngresoNeto.Text == "" ? "0" : txtIngresoNeto.Text);
            var b = Convert.ToDecimal(txtIngresoNetoConyuge.Text == "" ? "0" : txtIngresoNetoConyuge.Text);
            var c = Convert.ToDecimal(txtComisionesNeto.Text == "" ? "0" : txtComisionesNeto.Text);
            var d = Convert.ToDecimal(txtOtrosIngresosNeto.Text == "" ? "0" : txtOtrosIngresosNeto.Text);

            txtTotalIngresoNeto.Text = (a + b + c + d).ToString();
        }

        private void SumaTotalFamiliar()
        {
            var a = Convert.ToDecimal(txtAlimentacion.Text == "" ? "0" : txtAlimentacion.Text);
            var b = Convert.ToDecimal(txtEducacion.Text == "" ? "0" : txtEducacion.Text);
            var c = Convert.ToDecimal(txtTransporte.Text == "" ? "0" : txtTransporte.Text);
            var d = Convert.ToDecimal(txtServicios.Text == "" ? "0" : txtServicios.Text);
            var eb = Convert.ToDecimal(txtImprevistos.Text == "" ? "0" : txtImprevistos.Text);
            var ec = Convert.ToDecimal(txtAlquiler.Text == "" ? "0" : txtAlquiler.Text);

            txtTotalgastoFamiliar.Text = (a + b + c + d + eb + ec).ToString();
        }

        private void SumaCreditos()
        {
            DataTable dtCreditos = (DataTable)ViewState["dtCreditos"];

            if (dtCreditos.Rows.Count > 0)
            {
                var Total = dtCreditos.AsEnumerable().Sum(x => (double)x["nMonto"]);
                txtTotalCreditos.Text = Total.ToString();
            }
            else
            {
                txtTotalCreditos.Text = "0.00";
            }
        }

        private void SumaCreditosDirecto()
        {
            DataTable dtCreditos = (DataTable)ViewState["dtCreditosDirecto"];

            if (dtCreditos.Rows.Count > 0)
            {
                var Total = dtCreditos.AsEnumerable().Sum(x => (double)x["nMonto"]);
                txtTotalCreditoDirecto.Text = Total.ToString();
            }
            else
            {
                txtTotalCreditoDirecto.Text = "0.00";
            }
        }

        private void SumaCreditosInDirecto()
        {
            DataTable dtCreditos = (DataTable)ViewState["dtCreditosInDirecto"];

            if (dtCreditos.Rows.Count > 0)
            {
                var Total = dtCreditos.AsEnumerable().Sum(x => (double)x["nMonto"]);
                txtTotalCreditoIndirecto.Text = Total.ToString();
            }
            else
            {
                txtTotalCreditoIndirecto.Text = "0.00";
            }
        }

        private void guardar()
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

            if (hidSolicitud.Value!="0")
            {
                int idSolicitud = Convert.ToInt32(hidSolicitud.Value);
               var dtResultado=  cneva.InsertarEvaluacionCredito(idSolicitud, objUsuario.dFecSystem, 2);//consumo
               if (dtResultado.Rows.Count > 0)
               {
                   var idEvaluacion = Convert.ToInt32(dtResultado.Rows[0]["idEvaluacion"]);

                   decimal nIngresoBruto = Convert.ToDecimal(txtIngresoBruto.Text);
                   decimal nIngresoConyuge = Convert.ToDecimal(txtIngresoConyuge.Text);
                   decimal nComisiones = Convert.ToDecimal(txtComisiones.Text);
                   decimal nOtrosIngresos = Convert.ToDecimal(txtOtrosIngresos.Text);
                   decimal nTotalIngresoBruto = Convert.ToDecimal(txtTotalIngresoBruto.Text);
                   decimal nIngresoNeto = Convert.ToDecimal(txtIngresoNeto.Text);
                   decimal nIngresoNetoConyuge = Convert.ToDecimal(txtIngresoNetoConyuge.Text);
                   decimal nComisionesNeto = Convert.ToDecimal(txtComisionesNeto.Text);
                   decimal nOtrosIngresosNeto = Convert.ToDecimal(txtOtrosIngresosNeto.Text);
                   decimal nTotalIngresoNeto = Convert.ToDecimal(txtTotalIngresoNeto.Text);
                   decimal nAlimentacion = Convert.ToDecimal(txtAlimentacion.Text);
                   decimal nEducacion = Convert.ToDecimal(txtEducacion.Text);
                   decimal nTransporte = Convert.ToDecimal(txtTransporte.Text);
                   decimal nAlquiler = Convert.ToDecimal(txtAlquiler.Text);
                   decimal nServicios = Convert.ToDecimal(txtServicios.Text);
                   decimal nImprevistos = Convert.ToDecimal(txtImprevistos.Text);
                   decimal nTotalgastoFamiliar = Convert.ToDecimal(txtTotalgastoFamiliar.Text);
                   int nNumHijos = Convert.ToInt32(txtNumHijos.Text);
                   int nDependientes = Convert.ToInt32(txtDependientes.Text);
                   string cEdadHijo = txtEdadHijo.Text;
                   decimal nColegio = Convert.ToDecimal(txtColegio.Text);
                   decimal nUniversidad = Convert.ToDecimal(txtUniversidad.Text);
                   decimal nMontoPension = Convert.ToDecimal(txtMontoPension.Text);
                   string cObservaciones = txtObservaciones.Text.Trim();

                   cneva.InsertarEvaluacionConsumo(idEvaluacion,
                      nIngresoBruto, nIngresoConyuge, nComisiones, nOtrosIngresos, nTotalIngresoBruto,
                      nIngresoNeto, nIngresoNetoConyuge, nComisionesNeto, nOtrosIngresosNeto, nTotalIngresoNeto,
                      nAlimentacion, nEducacion, nTransporte, nAlquiler, nServicios, nImprevistos,
                      nTotalgastoFamiliar, nNumHijos, nDependientes, cEdadHijo, nColegio, nUniversidad, nMontoPension, cObservaciones);

                   var dtComercial = (DataTable)ViewState["dtCreditos"];
                   foreach (DataRow item in dtComercial.Rows)
                   {
                       cneva.InsertarEvaluacionConsumoCreditoComercial(idEvaluacion, item["cDescripcion"].ToString(), Convert.ToDecimal(item["nMonto"]));
                   }

                   var dtBancoDirecto = (DataTable)ViewState["dtCreditosDirecto"];
                   foreach (DataRow item in dtBancoDirecto.Rows)
                   {
                       cneva.InsertarEvaluacionConsumoCreditoDirecto(idEvaluacion, item["cDescripcion"].ToString(), Convert.ToDecimal(item["nMonto"]));
                   }

                   var dtBancoInDirecto = (DataTable)ViewState["dtCreditosInDirecto"];
                   foreach (DataRow item in dtBancoInDirecto.Rows)
                   {
                       cneva.InsertarEvaluacionConsumoCreditoIndirecto(idEvaluacion, item["cDescripcion"].ToString(), Convert.ToDecimal(item["nMonto"]));
                   }

               }
            }
            
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            guardar();
            script.Mensaje("Los datos se guardaron correctamente.");
            Response.Redirect("frmEvaCliente.aspx");
        }

    }
}