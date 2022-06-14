using SGA.ENTIDADES;
using SGA.Utilitarios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGA.LogicaNegocio;

namespace SGA.Presentacion.CREDITOS
{
    public partial class frmExtornoDesembolso : System.Web.UI.Page
    {
        GEN.CapaNegocio.clsCNKardex DatExtorno = new GEN.CapaNegocio.clsCNKardex();
        clsWebJScript script = new clsWebJScript();

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

            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void BotonConsultar1_Click(object sender, EventArgs e)
        {
            Int32 idCuenta = Convert.ToInt32(txtcuenta.Value);
            CargaDatosDesembolso(idCuenta);
        }

        private void CargaDatosDesembolso(Int32 idCuenta)
        {
            var dtExtorno = DatExtorno.DetalleDesembolso(idCuenta);
            ViewState["dtExtorno"] = dtExtorno;
            if (dtExtorno.Rows.Count > 0)
            {
                dtpFecDes.Text = Convert.ToDateTime(dtExtorno.Rows[0]["dFechaOpe"]).ToString("dd/MM/yyyy");
                txtMonto.Text = Convert.ToString(dtExtorno.Rows[0]["nMontoOperacion"]);
                txtUsuario.Text = Convert.ToString(dtExtorno.Rows[0]["cNombre"]);
            }
            else
            {
                script.Mensaje("No se encontro datos de la cuenta");
            }
        }

        private bool ValidaTrx(Int32 idCuenta)
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
            DataTable dtValida = DatExtorno.DetalleValida(idCuenta);
            var dtExtorno=(DataTable)ViewState["dtExtorno"];
            Int32 nCodUsu = Convert.ToInt32(objUsuario.idUsuario);
            DateTime dFecSis = objUsuario.dFecSystem;

            bool EstadoTrx = false;
            if (string.IsNullOrEmpty(txtcuenta.Text))
            {
                script.Mensaje("Debe registrar un numero de cuenta");
                return false;

            }

            if (dtValida.Rows.Count == 0)
            {
                script.Mensaje("No existe operacion a extornar");
                return EstadoTrx;
            }

            if (Convert.ToInt32(dtExtorno.Rows[0]["IdUsuario"]) != nCodUsu)
            {
                script.Mensaje("Usuario no realizo la transaccion");
                return EstadoTrx;
            }
            if (Convert.ToDateTime(dtExtorno.Rows[0]["dFechaOpe"]) != dFecSis)
            {
                script.Mensaje("Solo es posible el EXTORNO de desembolso en la fecha actual");
                return EstadoTrx;
            }
            if (Convert.ToInt32(dtExtorno.Rows[0]["IdKardex"]) != Convert.ToInt32(dtValida.Rows[0]["IdKardex"]))
            {
                script.Mensaje("Existen Transacciones posteriores en esta cuenta");
                return EstadoTrx;
            }
            EstadoTrx = true;
            return EstadoTrx;
        }


        protected void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        protected void btnExtorno_Click(object sender, EventArgs e)
        {
            Int32 idCuenta = Convert.ToInt32(txtcuenta.Value);
            ValidaTrx(idCuenta);
            var dtExtorno = (DataTable)ViewState["dtExtorno"];
            if (ValidaTrx(idCuenta))
            {
                DataSet ds = new DataSet("dsextorno");
                ds.Tables.Add(dtExtorno);
                String XmlCredito = ds.GetXml();
                XmlCredito = GEN.CapaNegocio.clsCNFormatoXML.EncodingXML(XmlCredito);
                ExtornaTrx(XmlCredito);
                script.Mensaje("Desembolso Extornado");
                LiberarCuenta();
                limpiar();
            }
        }

        private void ExtornaTrx(String tExtorno)
        {
            DataTable dtExtornar = DatExtorno.ExtDesembolso(tExtorno);
        }

        public void LiberarCuenta()
        {
            Int32 idCuenta = Convert.ToInt32(txtcuenta.Value);
            if (idCuenta > 0)
            {
                new GEN.CapaNegocio.clsCNRetornaNumCuenta().UpdEstCuenta(idCuenta, 0);
            }
        }

        public void limpiar()
        {
            txtcuenta.Text = "0";
            txtMonto.Text = "";
            txtUsuario.Text = "";
            this.btnExtorno.Enabled = true;
        }
    }
}