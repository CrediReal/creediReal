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
    public partial class frmExtornaOpe : System.Web.UI.Page
    {
        GEN.CapaNegocio.clsCNRetornaNumCuenta RetornaNumCuenta = new GEN.CapaNegocio.clsCNRetornaNumCuenta();
        CRE.CapaNegocio.clsCNOperacion Operacion = new CRE.CapaNegocio.clsCNOperacion();
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
                cargarSolicitudes();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void cargarSolicitudes()
        {
            CargarDatosSolExt(1, "2");
        }
        private void CargarDatosSolExt(int idModulo, string cTipOpe)
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
            GEN.CapaNegocio.clsCNAprobacion objExt = new GEN.CapaNegocio.clsCNAprobacion();
            DataTable tbApr = objExt.ListarAprobacionExtorno(objUsuario.dFecSystem, objUsuario.nIdAgencia, objUsuario.idUsuario,
                                                       idModulo, cTipOpe);
            ViewState["tbApr"] = tbApr;
            this.dtgSolExt.DataSource = tbApr;
            this.dtgSolExt.DataBind();
        }

        protected void BotonConsultar1_Click(object sender, EventArgs e)
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
            int IdKardex = Convert.ToInt32(((LinkButton)sender).CommandArgument);
            //var tbApr=(DataTable)ViewState["tbApr"];
            //var query = tbApr.AsEnumerable().Where(x => (int)x["idSolAproba"] == idSolAproba).ToList()[0];
            HidKardex.Value = IdKardex.ToString();

            if (Convert.ToInt32(nudNroKardex.Value) > 0)
            {
                LiberarCuenta();
            }

            LimpiarControles();

            var idCuenta = 0;
            btnExtorno.Enabled = false;
            //frmBuscarSolExt frmExtPen = new frmBuscarSolExt();
            //frmExtPen.pidMod = 1;
            //frmExtPen.pidTipOpe = "2";
            //frmExtPen.ShowDialog();
            int nidKar = Convert.ToInt32(HidKardex.Value); ;

            if (nidKar > 0)
            {
                nudNroKardex.Value = nidKar;
                btnCancelar.Enabled = true;
            }
            else
            {
                nudNroKardex.Value = 0;
                return;
            }

            GEN.CapaNegocio.clsCNAprobacion objApr = new GEN.CapaNegocio.clsCNAprobacion();
            DataTable dtDatExt = objApr.RetornaDatosOperacionExt(Convert.ToInt32(nudNroKardex.Value), objUsuario.dFecSystem, objUsuario.nIdAgencia,
                                                                objUsuario.idUsuario, 1, "2");

            var nNumOperacion = Convert.ToInt32(this.nudNroKardex.Value);
            var dtOperacion = Operacion.CNdtOperacion(nNumOperacion); //Obtener los datos propios de la Cobranza que se quiere extornar

            if (dtOperacion.Rows.Count == 0)
            {
                script.Mensaje("No se encontró ninguna operación con ese número o Ya se encuentrá extornada");
                this.btnExtorno.Enabled = false;
                return;
            }

            if (dtOperacion.Rows.Count > 1)
            {
                script.Mensaje("Existen operaciones posteriores a la que desea extornar");
                this.btnExtorno.Enabled = false;
                return;
            }

            idCuenta = Convert.ToInt32(dtDatExt.Rows[0]["idCuenta"].ToString());

            //--===============================================================================
            //--Validar Si Cuenta esta Siendo Usada
            //--===============================================================================
            DataTable dtEstCuenta = RetornaNumCuenta.VerifEstCuenta(idCuenta);
           var  nidUserBloqueo = Convert.ToInt32(dtEstCuenta.Rows[0][0]);
            if (nidUserBloqueo != 0)
            {
                DataTable dtUsu = new DataTable();
                dtUsu = RetornaNumCuenta.BusUsuBlo(nidUserBloqueo);
                var cUserBloqueo = dtUsu.Rows[0][0].ToString();
                script.Mensaje("Cuenta Bloqueada por usuario: " + cUserBloqueo);
                btnExtorno.Enabled = false;
                //lbloqueo = false;
                LimpiarControles();
                return;
            }
            else
            {
                RetornaNumCuenta.UpdEstCuenta(idCuenta, objUsuario.idUsuario);//Usando la cuenta
                //lbloqueo = true;
            }

            //Operación
            this.txtBase1.Text = dtOperacion.Rows[0]["idCuenta"].ToString();
            this.txtBase2.Text = dtOperacion.Rows[0]["cNombre"].ToString();

            //Monto
            this.txtBase6.Text = dtOperacion.Rows[0]["nMontoCapital"].ToString();
            this.txtBase7.Text = dtOperacion.Rows[0]["nMontoInteres"].ToString();
            this.txtBase8.Text = dtOperacion.Rows[0]["nMontoMora"].ToString();
            this.txtBase9.Text = dtOperacion.Rows[0]["nMontoOtros"].ToString();
            this.txtBase10.Text = dtOperacion.Rows[0]["nMontoOperacion"].ToString();

            //Detalles
            this.txtBase3.Text = dtOperacion.Rows[0]["ctipooperacion"].ToString();
            this.txtBase4.Text = dtOperacion.Rows[0]["dFechaOpe"].ToString();
            this.txtBase5.Text = dtOperacion.Rows[0]["usuario"].ToString();

            btnExtorno.Enabled = true;
        }

        protected void BotonCancelar1_Click(object sender, EventArgs e)
        {
            nudNroKardex.Value = 0;
            LimpiarControles();
            LiberarCuenta();
            btnExtorno.Enabled = false;
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
            //Validando el Usuario y Fecha

            Int32 nUsusis = objUsuario.idUsuario;
            DateTime dfecsis = objUsuario.dFecSystem;

            //if (Convert.ToInt32(dtOperacion.Rows[0]["iduser"]) != nUsusis)
            //{
            //    MessageBox.Show("El Usuario que hizo la operación no es el mismo que él del extorno", "Extorno de Operacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    return;
            //}
            

            if (Convert.ToDateTime(txtBase4.Text) != dfecsis)
            {
                script.Mensaje("La Fecha en la que se hizo la operación no es el misma que la del extorno");
                return;
            }
            int nKarAExtornar = Convert.ToInt32(this.nudNroKardex.Value);
            Int32 nIdUsuario = objUsuario.idUsuario;
            CRE.CapaNegocio.clsCNOperacion Operacion = new CRE.CapaNegocio.clsCNOperacion();

            DataTable nKarQueExtorna = Operacion.CNdtExtornaOpe(nKarAExtornar, objUsuario.dFecSystem, nIdUsuario);

            CRE.CapaNegocio.clsCNPlanPago PlanPago = new CRE.CapaNegocio.clsCNPlanPago();

            //this.dtgBase1.DataSource = TablaInsPpg;
            script.Mensaje("El Extorno se realizón con éxito.");
            LiberarCuenta();
            //Emisión de Voucher
            ////////////////if (nKarQueExtorna != null)
            ////////////////{
            ////////////////    DataTable dtExtorno = PlanPago.CNGetCobro((Int32)nKarQueExtorna.Rows[0][0], (Int32)dtOperacion.Rows[0]["idCuenta"]);
            ////////////////    //for (int i = 0; i < 2; i++)
            ////////////////    //{
            ////////////////    //    EmitirVoucher(dtExtorno);
            ////////////////    //}
            ////////////////}
            this.btnExtorno.Enabled = false;
            cargarSolicitudes();
        }

        

        private void LiberarCuenta()
        {
            
                new GEN.CapaNegocio.clsCNRetornaNumCuenta().UpdEstCuenta(Convert.ToInt32(txtBase1.Value), 0);
            
        }

        private void LimpiarControles()
        {
            //Operación
            this.txtBase1.Text = "";
            this.txtBase2.Text = "";

            //Monto
            this.txtBase6.Text = "";
            this.txtBase7.Text = "";
            this.txtBase8.Text = "";
            this.txtBase9.Text = "";
            this.txtBase10.Text = "";

            //Detalles
            this.txtBase3.Text = "";
            this.txtBase4.Text = "";
            this.txtBase5.Text = "";
        }

    }
}