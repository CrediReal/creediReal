using Microsoft.Reporting.WebForms;
using SGA.ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGA.LogicaNegocio;
using System.Data;
using EntityLayer;

namespace SGA.Presentacion.CREDITOS
{
    public partial class FrmConsultaSolicitudes : System.Web.UI.Page
    {
        //GEN.CapaNegocio.clsCNSolicitud Solicitud = new GEN.CapaNegocio.clsCNSolicitud();
        List<clsSolCtaCre> LstSolxCli;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["usuario"]!=null)
                {
                    Session["DatosUsuarioSession"] = new clsCNUsuario().GetDatosUsuario(Request.QueryString["usuario"].ToString());
                }
                if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor " + this.ToString()); }

                if (IsPostBack) return;
                hUsuario.Value = ((SGA.ENTIDADES.clsUsuario)Session["DatosUsuarioSession"]).cUsuario;
                lblOpcion.Text = Session["cOpcion"].ToString().ToUpper() + ":";

            }
            catch (Exception)
            {
                throw;
            }
        }

        private void cargarCreditos()
        {
            if (Session["idCliente"] == null)
            {
                return;
            }
            var idCliente = Convert.ToInt32(Session["idCliente"]);
            if (idCliente == 0)
            {
                return;
            }
            else
            {
                GEN.CapaNegocio.clsCNRetornsCuentaSolCliente RetornaCuentaSolCliente = new GEN.CapaNegocio.clsCNRetornsCuentaSolCliente();
                var dt = RetornaCuentaSolCliente.RetornaCuentaSolCliente(idCliente, "S", "%");
                LstSolxCli = new List<clsSolCtaCre>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //IdNum = A.idSolicitud, B.cEstado, C.cNombre, a.idCli, 	Fecha_Desembolso = a.dFechaDesembolsoSugerido, 
                    //Frecuencia = a.nPlazoCuota,	nMonto = a.nCapitalSolicitado, a.nCuotas, a.IdMoneda, a.idProducto, 
                    //D.cProducto, Monto_Cuota = 0, nAtraso = 0, c.cDocumentoID, mo.cMoneda, a.idoperacion
                    clsSolCtaCre SolxCli = new clsSolCtaCre();
                    SolxCli.Id = Convert.ToInt32(dt.Rows[i]["IdNum"]);
                    SolxCli.Estado = dt.Rows[i]["cEstado"].ToString();
                    SolxCli.Nombre = dt.Rows[i]["cNombre"].ToString();
                    SolxCli.IdCliente = Convert.ToInt32(dt.Rows[i]["idCli"]);
                    SolxCli.FechaDesembolso = Convert.ToDateTime(dt.Rows[i]["Fecha_Desembolso"].ToString());
                    SolxCli.FrecuenciaPago = Convert.ToInt32(dt.Rows[i]["Frecuencia"]);
                    SolxCli.MontoCapital = Convert.ToDecimal(dt.Rows[i]["nMonto"]);
                    SolxCli.NumeroCuotas = Convert.ToInt32(dt.Rows[i]["nCuotas"]);
                    SolxCli.TipoMoneda = Convert.ToInt32(dt.Rows[i]["IdMoneda"]);
                    SolxCli.IdProducto = Convert.ToInt32(dt.Rows[i]["idProducto"]);
                    SolxCli.Producto = dt.Rows[i]["cProducto"].ToString();
                    SolxCli.MontoCuota = Convert.ToDecimal(dt.Rows[i]["Monto_Cuota"]);
                    SolxCli.DiasAtraso = Convert.ToInt32(dt.Rows[i]["nAtraso"]);
                    SolxCli.IdDocumento = dt.Rows[i]["cDocumentoID"].ToString();
                    SolxCli.Moneda = dt.Rows[i]["cMoneda"].ToString();
                    SolxCli.IdOperacion = Convert.ToInt32(dt.Rows[i]["idoperacion"]);

                    LstSolxCli.Add(SolxCli);
                }  
                
                 
                //var dtLisSolxCli = Solicitud.CNdtLisSolxCli(idCliente);
                this.dtgCreditos.DataSource = LstSolxCli;
                this.dtgCreditos.DataBind();
            }            
        }

        protected void BotonConsultar1_Click(object sender, EventArgs e)
        {
            cargarCreditos();
        }

        protected void CheckBoxBase1_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void btnPropuesta_Click(object sender, EventArgs e)
        {
            SGA.ENTIDADES.clsUsuario objUsuario;
            if (Session["DatosUsuarioSession"] == null)
            {
                Session["DatosUsuarioSession"] = new clsCNUsuario().GetDatosUsuario(hUsuario.Value);
                objUsuario = (SGA.ENTIDADES.clsUsuario)Session["DatosUsuarioSession"];
            }
            else
            {
                objUsuario = (SGA.ENTIDADES.clsUsuario)Session["DatosUsuarioSession"];
            }
            int idSolicitud = Convert.ToInt32(((LinkButton)sender).CommandArgument);

            Response.Redirect("frmPropuesta.aspx?IdSolicitud=" + idSolicitud.ToString());
            //frmPropuesta oFrmPropuesta = new frmPropuesta(idSolicitud);
            //clsSolCtaCre oClsSoCre = new clsSolCtaCre();
            //oClsSoCre = LstSolxCli.Where(c => c.Id == idSolicitud).FirstOrDefault();

        }

        protected void btnEvaluacion_Click(object sender, EventArgs e)
        {
            SGA.ENTIDADES.clsUsuario objUsuario;
            if (Session["DatosUsuarioSession"] == null)
            {
                Session["DatosUsuarioSession"] = new clsCNUsuario().GetDatosUsuario(hUsuario.Value);
                objUsuario = (SGA.ENTIDADES.clsUsuario)Session["DatosUsuarioSession"];
            }
            else
            {
                objUsuario = (SGA.ENTIDADES.clsUsuario)Session["DatosUsuarioSession"];
            }
            int idSolicitud = Convert.ToInt32(((LinkButton)sender).CommandArgument);

            

        }
    }
}