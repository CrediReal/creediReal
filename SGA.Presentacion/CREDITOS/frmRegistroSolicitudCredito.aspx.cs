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

namespace SGA.Presentacion.CREDITOS
{
    public partial class frmRegistroSolicitudCredito : System.Web.UI.Page
    {
        clsWebJScript script = new clsWebJScript();
        GEN.CapaNegocio.clsCNSolicitud Solicitud = new GEN.CapaNegocio.clsCNSolicitud();
        GEN.CapaNegocio.clsCNRetornsCuentaSolCliente RetornaCuentaSolCliente = new GEN.CapaNegocio.clsCNRetornsCuentaSolCliente();
        int nEstado = 0;
        DataTable dtDatosCuentaSolCliente = null;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["usuario"] != null)
                {
                    Session["DatosUsuarioSession"] = new SGA.LogicaNegocio.clsCNUsuario().GetDatosUsuario(Request.QueryString["usuario"].ToString());
                }
                if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor " + this.ToString()); }

                if (IsPostBack) return;

                hUsuario.Value = ((clsUsuario)Session["DatosUsuarioSession"]).cUsuario;

                lblOpcion.Text = Session["cOpcion"].ToString().ToUpper() + ":";
                cargarControles();
                cboTipoCre.SelectedValue = "1";
                cargarSubTipoCre();
                cboSubTipoCre.SelectedValue = "2";
                cargarProducto();
                cboProducto.SelectedValue = "3";
                cargarSubProducto();
                cboSubProducto.SelectedValue = "4";
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void cargarControles()
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
            cargarOperacion();
            cargarTipoCre();
            cargarMoneda();
            cargarPeriodo();
            cargarEstado();
            cboEstado.SelectedValue = "1";
            cargarDestino();
            cargarAsesor();
            cargarTipoCalculo();
            this.cboTipoCalculo.SelectedValue = "1";
            this.dtpFechaDesembolso.SeleccionarFecha = objUsuario.dFecSystem;
            txtDiasGracia.Text = "0";
            txtMontoCapital.Text = "0.00";
            txtTasaMora.Text = "0.00";
            cboPeriodo.SelectedValue = "2";
            cboTipoCalculo.SelectedValue = "2";
        }

        private void cargarOperacion()
        {
            GEN.CapaNegocio.clsCNOperacionCre ListaOperacionCre = new GEN.CapaNegocio.clsCNOperacionCre();
            DataTable dt = ListaOperacionCre.ListarOperacionCre();
            this.cboOperacion.DataSource = dt;
            this.cboOperacion.DataValueField = dt.Columns[0].ToString();
            this.cboOperacion.DataTextField = dt.Columns[1].ToString();
            this.cboOperacion.DataBind();
        }

        private void cargarTipoCre()
        {
            GEN.CapaNegocio.clsCNProducto ListaProducto = new GEN.CapaNegocio.clsCNProducto();
            var dt = ListaProducto.listarProducto(0);
            this.cboTipoCre.DataSource = dt;
            this.cboTipoCre.DataValueField = dt.Columns[0].ToString();
            this.cboTipoCre.DataTextField = dt.Columns[1].ToString();
            cboTipoCre.DataBind();
        }

        private void cargarSubTipoCre()
        {
            GEN.CapaNegocio.clsCNProducto ListaProducto = new GEN.CapaNegocio.clsCNProducto();
            var idProducto=Convert.ToInt32(this.cboTipoCre.SelectedValue);
            var dt = ListaProducto.listarProducto(idProducto);
            this.cboSubTipoCre.DataSource = dt;
            this.cboSubTipoCre.DataValueField = dt.Columns[0].ToString();
            this.cboSubTipoCre.DataTextField = dt.Columns[1].ToString();
            cboSubTipoCre.DataBind();
        }

        private void cargarProducto()
        {
            GEN.CapaNegocio.clsCNProducto ListaProducto = new GEN.CapaNegocio.clsCNProducto();
            var idProducto = Convert.ToInt32(this.cboSubTipoCre.SelectedValue);
            var dt = ListaProducto.listarProducto(idProducto);
            this.cboProducto.DataSource = dt;
            this.cboProducto.DataValueField = dt.Columns[0].ToString();
            this.cboProducto.DataTextField = dt.Columns[1].ToString();
            cboProducto.DataBind();
        }

        private void cargarSubProducto()
        {
            GEN.CapaNegocio.clsCNProducto ListaProducto = new GEN.CapaNegocio.clsCNProducto();
            var idProducto = Convert.ToInt32(this.cboProducto.SelectedValue);
            var dt = ListaProducto.listarProducto(idProducto);
            this.cboSubProducto.DataSource = dt;
            this.cboSubProducto.DataValueField = dt.Columns[0].ToString();
            this.cboSubProducto.DataTextField = dt.Columns[1].ToString();
            cboSubProducto.DataBind();
        }

        private void cargarMoneda()
        {
            GEN.CapaNegocio.clsCNMoneda moneda = new GEN.CapaNegocio.clsCNMoneda();
            var dt = moneda.listarMoneda();
            this.cboMoneda.DataSource = dt;
            this.cboMoneda.DataValueField = dt.Columns[0].ToString();
            this.cboMoneda.DataTextField = dt.Columns[1].ToString();
            cboMoneda.DataBind();
        }

        private void cargarPeriodo()
        {
            CRE.CapaNegocio.clsCNTipoPeriodo TipoPerido = new CRE.CapaNegocio.clsCNTipoPeriodo();

            DataTable dt = TipoPerido.dtListaTipoPeriodo();
            this.cboPeriodo.DataSource = dt;
            cboPeriodo.DataValueField = dt.Columns[0].ToString();
            cboPeriodo.DataTextField = dt.Columns[1].ToString();
            cboPeriodo.DataBind();
        }

        private void cargarEstado()
        {
            GEN.CapaNegocio.clsCNEstadoCredito ListarEstado = new GEN.CapaNegocio.clsCNEstadoCredito();
            DataTable dt = ListarEstado.ListarEstado(12);

            this.cboEstado.DataSource = dt;
            cboEstado.DataValueField = dt.Columns[0].ToString();
            cboEstado.DataTextField = dt.Columns[1].ToString();
            cboEstado.DataBind();
        }

        private void cargarDestino()
        {
            CRE.CapaNegocio.clsCNDestinoCredito ListaDestino = new CRE.CapaNegocio.clsCNDestinoCredito();
            DataTable dt = ListaDestino.ListaDestino();
            this.cboDestino.DataSource = dt;
            this.cboDestino.DataValueField = dt.Columns[0].ToString();
            this.cboDestino.DataTextField = dt.Columns[1].ToString();
            cboDestino.DataBind();
        }

        private void cargarTipoCalculo()
        {
            DataTable dtTipoCalculo = new GEN.CapaNegocio.clsCNTipoCalculo().CNTipoCalculoPPG();
            this.cboTipoCalculo.DataSource = dtTipoCalculo;
            cboTipoCalculo.DataValueField = dtTipoCalculo.Columns["idTipoCalculo"].ToString();
            cboTipoCalculo.DataTextField = dtTipoCalculo.Columns["cTipoCalculo"].ToString();
            cboTipoCalculo.DataBind();
        }

        private void cargarAsesor()
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

            GEN.CapaNegocio.clsCNPersonalCreditos ListaPersonalCre = new GEN.CapaNegocio.clsCNPersonalCreditos();
            DataTable dt = ListaPersonalCre.ListarPersonalCre(objUsuario.nIdAgencia, 0, 0);
            this.cboAsesor.DataSource = dt;
            cboAsesor.DataValueField = dt.Columns[0].ToString();
            cboAsesor.DataTextField = dt.Columns[1].ToString();
            cboAsesor.DataBind();
        }

        public void CargarTasa(Int32 nPlazo, Int32 idProducto, Decimal nMonto, Int32 idMoneda, Int32 idAgencia, int idTipoCalculo)
        {
            DataTable dttasaaux = new DataTable();
            dttasaaux.Columns.Add("idTasa", typeof(int));
            dttasaaux.Columns.Add("cTasa", typeof(string));
            DataRow drtasa;
            if (cboFrecuencia.SelectedValue=="1")
            {
                dttasaaux.Rows.Clear();
                drtasa = dttasaaux.NewRow();
                drtasa[0] = "1";
                drtasa[1] = "6";
                dttasaaux.Rows.Add(drtasa);
                drtasa = dttasaaux.NewRow();
                drtasa[0] = "2";
                drtasa[1] = "6";
                dttasaaux.Rows.Add(drtasa);
                dttasaaux.AcceptChanges();
            }

            else if (cboFrecuencia.SelectedValue == "7")
            {
                dttasaaux.Rows.Clear();
                drtasa = dttasaaux.NewRow();
                drtasa[0] = "3";
                drtasa[1] = "6";
                dttasaaux.Rows.Add(drtasa);
                drtasa = dttasaaux.NewRow();
                drtasa[0] = "4";
                drtasa[1] = "6";
                dttasaaux.Rows.Add(drtasa);
                dttasaaux.AcceptChanges();
            }

            else if (cboFrecuencia.SelectedValue == "15")
            {
                dttasaaux.Rows.Clear();
                drtasa = dttasaaux.NewRow();
                drtasa[0] = "5";
                drtasa[1] = "6";
                dttasaaux.Rows.Add(drtasa);
                drtasa = dttasaaux.NewRow();
                drtasa[0] = "6";
                drtasa[1] = "6";
                dttasaaux.Rows.Add(drtasa);
                dttasaaux.AcceptChanges();
            }

            else if (cboFrecuencia.SelectedValue == "30")
            {
                dttasaaux.Rows.Clear();
                drtasa = dttasaaux.NewRow();
                drtasa[0] = "7";
                drtasa[1] = "6.00";
                dttasaaux.Rows.Add(drtasa);
                drtasa = dttasaaux.NewRow();
                drtasa[0] = "8";
                drtasa[1] = "9.00";
                dttasaaux.Rows.Add(drtasa);
                dttasaaux.AcceptChanges();
            }
            else
            {
                dttasaaux.Rows.Clear();
                drtasa = dttasaaux.NewRow();
                drtasa[0] = "7";
                drtasa[1] = "6.00";
                dttasaaux.Rows.Add(drtasa);
                drtasa = dttasaaux.NewRow();
                drtasa[0] = "8";
                drtasa[1] = "9.00";
                dttasaaux.Rows.Add(drtasa);
                dttasaaux.AcceptChanges();
            }
            //GEN.CapaNegocio.clsCNTasaCredito TasaCredito = new GEN.CapaNegocio.clsCNTasaCredito();
            //var dt = TasaCredito.ListaTasaCredito(nPlazo, idProducto, nMonto, idMoneda, idAgencia, idTipoCalculo);
            //this.cboTasaInteres.DataSource = dt;
            cboTasaInteres.DataSource = dttasaaux;
            cboTasaInteres.DataValueField = dttasaaux.Columns[0].ToString();
            cboTasaInteres.DataTextField = dttasaaux.Columns[1].ToString();
            cboTasaInteres.DataBind();
        }

        private void Tasa()
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
            Int32 nPlazo = Convert.ToInt32(this.txtCuotas.Value);
            Int32 idProducto = cboSubProducto.SelectedValue == "" ? 0 : Convert.ToInt32(cboSubProducto.SelectedValue);
            Decimal nMonto = Convert.ToDecimal(this.txtMontoCapital.Value);
            Int32 idMoneda = Convert.ToInt32(cboMoneda.SelectedValue);
            int idTipoCalculo = Convert.ToInt32(cboTipoCalculo.SelectedValue);

            CargarTasa(nPlazo, idProducto, nMonto, idMoneda, objUsuario.nIdAgencia, idTipoCalculo);
        }
        
        protected void cboTipoCre_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarSubTipoCre();
        }

        protected void cboSubTipoCre_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarProducto();
        }

        protected void cboProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarSubProducto();
        }

        protected void cboSubProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            Tasa();
            if (cboSubProducto.SelectedItem.Text.ToUpper()=="DIARIA")
            {
                txtFrecuencia.Text = "1";
            }
            if (cboSubProducto.SelectedItem.Text.ToUpper() == "SEMANAL")
            {
                txtFrecuencia.Text = "7";
            }
        }

        protected void BotonGrabar1_Click(object sender, EventArgs e)
        {
            validaActualizacion();
            if (nEstado == 0)
            {
                Insertar();
                limpiar();
            }
        }

        private void Insertar()
        {
            try
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
                if (Session["idCliente"] == null)
                {
                    return;
                }
                var idCliente = Convert.ToInt32(Session["idCliente"]);
                if (idCliente == 0)
                {
                    return;
                }

                DataTable Sol = new DataTable("dtSolicitud");
                GEN.CapaNegocio.clsCNSolicitud Solicitud = new GEN.CapaNegocio.clsCNSolicitud();
                Sol = Solicitud.ConsultaSolicitud(0, 0);
                DataRow dr = Sol.NewRow();
                dr["nCapitalSolicitado"] = this.txtMontoCapital.Text;
                dr["IdMoneda"] = cboMoneda.SelectedValue;
                dr["idOperacion"] = this.cboOperacion.SelectedValue;
                dr["nCuotas"] = this.txtCuotas.Value;
                dr["nPlazoCuota"] = this.txtFrecuencia.Value;
                dr["nDiasGracia"] = this.txtDiasGracia.Value;
                dr["dFechaDesembolsoSugerido"] = this.dtpFechaDesembolso.SeleccionarFecha.Date;
                dr["idEstado"] = cboEstado.SelectedValue;
                dr["idUsuario"] = this.cboAsesor.SelectedValue;
                dr["nTipCre"] = cboTipoCre.SelectedValue;
                dr["nSubTip"] = cboSubTipoCre.SelectedValue;
                dr["nProdu"] = cboProducto.SelectedValue;
                dr["nSubPro"] = cboSubProducto.SelectedValue;
                dr["idProducto"] = cboSubProducto.SelectedValue;
                dr["nTasaCompensatoria"] = this.cboTasaInteres.SelectedItem.Text;
                dr["nTasaMoratoria"] = txtTasaMora.Text;
                dr["dFechaRegistro"] = objUsuario.dFecSystem;
                dr["idCli"] = idCliente.ToString();
                dr["tObservacion"] = txtObservacion.Text;
                dr["idDestino"] = this.cboDestino.SelectedValue;
                dr["idTipoCli"] = 1;
                dr["idTasa"] = this.cboTasaInteres.SelectedValue;
                dr["idEvaluacion"] = 0;
                dr["idTipoPeriodo"] = cboPeriodo.SelectedValue;
                dr["lRequiereGarantia"] = 0;
                Sol.Rows.Add(dr);
                DataSet ds = new DataSet("dssolici");
                ds.Tables.Add(Sol);
                String XmlSoli = ds.GetXml();
                XmlSoli = GEN.CapaNegocio.clsCNFormatoXML.EncodingXML(XmlSoli);

                int idtipoOpera = Convert.ToInt32(this.cboOperacion.SelectedValue);
                int idcuenta = 0;

                //DATOS DE CUENTAS A REPROGRAMAR
                String XmlRepro;
                if (cboOperacion.SelectedValue.In("2","3"))
                {
                    DataSet dsRep = new DataSet("dsreprog");

                    DataTable dt = new DataTable("repro");
                    for (int i = 0; i < dtgCreditos.Columns.Count; i++)
                    {
                        //dt.Columns.Add("column" + i.ToString());
                        dt.Columns.Add(dtgCreditos.Columns[i].ToString().Replace(" ", ""));
                        //dtgCreditos.Columns[i]
                    }
                    foreach (GridViewRow row in dtgCreditos.Rows)
                    {
                        if (((CheckBox)row.FindControl("P")).Checked)
                        {
                            DataRow drow = dt.NewRow();
                            for (int j = 0; j < dtgCreditos.Columns.Count; j++)
                            {
                                drow[j] = row.Cells[j].Text;
                            }
                            dt.Rows.Add(drow);
                        }
                    }

                    dsRep.Tables.Add(dt);
                    XmlRepro = dsRep.GetXml();
                    XmlRepro = GEN.CapaNegocio.clsCNFormatoXML.EncodingXML(XmlRepro);
                }
                else
                {
                    XmlRepro = "<?xml version='1.0' encoding='ISO-8859-1' standalone='no' ?><dsreprog><repro></repro></dsreprog>";
                }

                DataTable InsSol = Solicitud.InsertaActualizaSolicitud(XmlSoli, XmlRepro, objUsuario.nIdAgencia, idcuenta, idtipoOpera);

                script.Mensaje("Los datos se guardaron correctamente");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void BotonCancelar1_Click(object sender, EventArgs e)
        {
            cargarControles();
            BotonGrabar1.Visible = false;
            pnlSolicitud.Visible = false;
            limpiar();
        }

        protected void cboTipoCalculo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Tasa();
        }

        private void validaActualizacion()
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
            if (Session["idCliente"] == null)
            {
                script.Mensaje("Seleccione un cliente por favor");
                nEstado = 1;
                return;
            }

            if (string.IsNullOrEmpty(this.txtMontoCapital.Text) || this.txtMontoCapital.Value==0)
            {
                script.Mensaje("El monto solicitado debe ser mayor a CERO");
                nEstado = 1;
                return;
            }

            if (this.txtCuotas.Value == 0)
            {
                script.Mensaje("Número de cuotas debe ser mayor a CERO");
                nEstado = 1;
                return;
            }

            if (this.txtFrecuencia.Value == 0)
            {
                script.Mensaje("Plazo debe ser mayor a CERO");
                nEstado = 1;
                return;
            }

            if (cboEstado.SelectedIndex == 0)
            {

                script.Mensaje("Solicitud no tiene un estado valido");
                nEstado = 1;
                return;
            }

            if (cboAsesor.SelectedIndex < 0)
            {

                script.Mensaje("Debe seleccionar a una responsable del crédito");
                nEstado = 1;
                return;
            }

            if (this.cboTasaInteres.SelectedValue == "")
            {

                script.Mensaje("No se encuentra Tasa Compensatoria");
                nEstado = 1;
                return;
            }

            if (Convert.ToDecimal(this.cboTasaInteres.SelectedValue) == 0)
            {
                script.Mensaje("No se encuentra Tasa Compensatoria");
                nEstado = 1;
                return;
            }

            if (this.dtpFechaDesembolso.SeleccionarFecha < objUsuario.dFecSystem)
            {

                script.Mensaje("La Fecha de Desembolso no puede ser menor a la del sistema");
                nEstado = 1;
                return;
            }
            if (Convert.ToInt32(cboSubProducto.SelectedValue) == 0)
            {
                script.Mensaje("Debe seleccionar un Sub Producto");
                nEstado = 1;
                return;
            }

            //En caso de reprogramación o refinanciamiento
            //if (cboOperacion.SelectedValue.In("2","3"))
            //{
            //    if (dtgCreditos.Rows.Count > 0)
            //    {
            //        bool x = false;
            //        foreach (GridViewRow row in dtgCreditos.Rows)
            //        {
            //            if (((CheckBox)row.Cells[7].Controls[1]).Checked)
            //            {
            //                x = true;
            //            }
            //        }
            //        if (!x)
            //        {
            //            script.Mensaje("Debe seleccionar alguna cuenta de crédito para la " + cboOperacion.SelectedItem.Text);
            //            nEstado = 1;
            //            return;
            //        }
            //    }
                
            //}

            Int32 CodCli = Convert.ToInt32(Session["idCliente"]);
            GEN.CapaNegocio.clsCNSolicitud Solicitud = new GEN.CapaNegocio.clsCNSolicitud();
            Int16 Producto = Convert.ToInt16(cboSubProducto.SelectedValue);
            DataTable Validasol = Solicitud.CNdtValidaRegSol(CodCli, Producto);
            string Valida = Validasol.Rows[0][0].ToString();
            if (Valida.Trim() != "OK")
            {
                script.Mensaje(Validasol.Rows[0][0].ToString());
                nEstado = 1;
                return;
            }
            nEstado = 0;
        }

       private void limpiar()
        {
            this.txtMontoCapital.Text = "0.00";
            txtMontoCapital.Enabled = true;
            dtpFechaDesembolso.Enabled = true;
            this.txtCuotas.Value = 30;
            this.txtDiasGracia.Value = 0;
            this.txtFrecuencia.Value = 0;
            cboEstado.SelectedValue = "1";
            txtObservacion.Text = "";
            Session["idCliente"] = null;
            conBuscarCliente1.LimpiarControl();
            dtDatosCuentaSolCliente = null;
            this.dtgCreditos.DataSource = null;
            this.dtgCreditos.DataBind();

            cboTipoCre.SelectedValue = "1";
            cargarSubTipoCre();
            cboSubTipoCre.SelectedValue = "2";
            cargarProducto();

        }

       protected void txtMontoCapital_TextChanged(object sender, EventArgs e)
       {
           Tasa();
       }

       protected void txtCuotas_TextChanged(object sender, EventArgs e)
       {
           Tasa();
       }

       protected void txtFrecuencia_TextChanged(object sender, EventArgs e)
       {
           Tasa();
       }

       protected void BotonConsultar1_Click(object sender, EventArgs e)
       {
           if (Session["idCliente"] != null)
           {
               var idCliente = Convert.ToInt32(Session["idCliente"]);
               if (idCliente == 0)
               {
                   return;
               }
               BuscarSoliciidCli(idCliente);

               pnlSolicitud.Visible = true;
               
           }
           else
           {
               
               pnlSolicitud.Visible = false;
               BotonGrabar1.Visible=false;
           }
       }


       private void BuscarSoliciidCli(int idCli)
       {
           DataTable dtSolCli, dtPreSol;
           dtSolCli = Solicitud.SolicitudClienteEstado(idCli, 1);
           dtPreSol = Solicitud.SolicitudClienteEstado(idCli, 12);
          
           Int32 idSoli;
           if (dtSolCli.Rows.Count == 0 && dtPreSol.Rows.Count == 0)
           {
               BuscarSolicitud(0);
               BotonGrabar1.Visible = true;
           }
           if (dtSolCli.Rows.Count == 1)
           {
               idSoli = Convert.ToInt32(dtSolCli.Rows[0]["idSolicitud"]);
               BuscarSolicitud(idSoli);
               BotonGrabar1.Visible = false;
               script.Mensaje("El cliente tiene una solicitud en estado solicitado, lista para su aprobación o rechazo");
           }
           if (dtPreSol.Rows.Count == 1)
           {
               script.Mensaje("El cliente tiene una solicitud en estado solicitado, lista para su aprobación o rechazo");
               limpiar();
               return;
           }
           //if (dtSolCli.Rows.Count > 1)
           //{
           //    GEN.ControlesBase.FrmBuscaCuentaCliente frmBusCuenCli = new GEN.ControlesBase.FrmBuscaCuentaCliente();
           //    string pcTIpBus = "S";
           //    string pcEstado = "[1]";
           //    frmBusCuenCli.CargarDatos(idCli, pcTIpBus, pcEstado);
           //    frmBusCuenCli.ShowDialog();
           //    nValBusqueda = Convert.ToInt32(frmBusCuenCli.cIdCreSol);
           //    BuscarSolicitud(nValBusqueda);
           //}

       }

       private void BuscarSolicitud(Int32 CodigoSol)
       {
          var Sol = Solicitud.ConsultaSolicitud(CodigoSol, 0);
          ViewState["Sol"] = Sol;

           if (Sol.Rows.Count > 0)
           {
               if (Convert.ToInt32(Sol.Rows[0]["idEstado"]) != 1)
               {

                   script.Mensaje("Estado de Solicitud diferente a SOLICITADO");
                   Transaccion.Value = "X";
                   this.limpiar();
                   this.BotonGrabar1.Enabled = false;
                   this.conBuscarCliente1.LimpiarControl();
                   this.conBuscarCliente1.Habilitar(true);
                   return;
               }
               else
               {
                   this.hIdSolciitud.Value = Sol.Rows[0]["idSolicitud"].ToString();
                   txtMontoCapital.Text = Sol.Rows[0]["nCapitalSolicitado"].ToString();
                   this.cboMoneda.SelectedValue = Convert.ToString(Sol.Rows[0]["IdMoneda"]);
                   txtCuotas.Value = Convert.ToDouble(Sol.Rows[0]["nCuotas"]);
                   dtpFechaDesembolso.SeleccionarFecha = Convert.ToDateTime(Sol.Rows[0]["dFechaRegistro"]);
                   //cboEstadoCredito1.SelectedValue = Convert.ToInt32(Sol.Rows[0]["idEstado"]);
                   this.cboAsesor.SelectedValue = Convert.ToString(Sol.Rows[0]["idUsuario"]);
                   cboTipoCre.SelectedValue = Convert.ToString(Sol.Rows[0]["nTipCre"]);
                   cargarSubTipoCre();
                   cboSubTipoCre.SelectedValue = Convert.ToString(Sol.Rows[0]["nSubTip"]);
                   cargarProducto();
                   cboProducto.SelectedValue = Convert.ToString(Sol.Rows[0]["nProdu"]);
                   cargarSubProducto();
                   cboSubProducto.SelectedValue = Convert.ToString(Sol.Rows[0]["nSubPro"]);
                   txtObservacion.Text = Convert.ToString(Sol.Rows[0]["tObservacion"]);
                   this.hIdSolciitud.Value = Convert.ToString(Sol.Rows[0]["idCli"]);
                   this.cboDestino.SelectedValue = Convert.ToString(Sol.Rows[0]["idDestino"]);
                   this.cboTasaInteres.SelectedValue = Convert.ToString(Sol.Rows[0]["idTasa"]);
                   this.cboTipoCalculo.SelectedValue = Convert.ToString(Sol.Rows[0]["idTipoCalculo"]);
                   txtFrecuencia.Value = Convert.ToInt32(Sol.Rows[0]["nPlazoCuota"]);
                   txtDiasGracia.Value = Convert.ToInt32(Sol.Rows[0]["nDiasGracia"]);
                   cboPeriodo.SelectedValue = Convert.ToString(Sol.Rows[0]["idTipoPeriodo"]);
                   Transaccion.Value = "U";
                   this.BotonGrabar1.Enabled = false;
                   //if (Convert.ToInt32(Sol.Rows[0]["idOperacion"]) == 1) nContador++;
               }
           }
           else
           {
               Transaccion.Value = "I";

               clsUsuario objUsuario = new clsUsuario();
               if (Session["DatosUsuarioSession"]==null)
               {
                   Session["DatosUsuarioSession"] = new clsCNUsuario().GetDatosUsuario(hUsuario.Value);
                   objUsuario = (clsUsuario)Session["DatosUsuarioSession"];
               }
               objUsuario = (clsUsuario)Session["DatosUsuarioSession"];
               dtpFechaDesembolso.SeleccionarFecha = objUsuario.dFecSystem;
               this.BotonGrabar1.Enabled = true;

           }
       }

       protected void cboFrecuencia_SelectedIndexChanged(object sender, EventArgs e)
       {
           cambiarFrecuencia();
       }

        private void cambiarFrecuencia()
        {
           txtFrecuencia.Text = cboFrecuencia.SelectedValue;
           if (cboFrecuencia.SelectedValue=="0")
           {
               txtFrecuencia.Enabled = true;
               txtFrecuencia.Focus();
           }
           else
           {
               txtFrecuencia.Enabled = false;
           }

           Tasa();
        }

       protected void dtgCreditos_SelectedIndexChanged(object sender, EventArgs e)
       {
           if (dtgCreditos.Rows.Count > 0)
           {
               //hIdCuenta.Value = dtgCreditos.SelectedRow.Cells[0].Text;
               //cargadatos();
           }
       }

       protected void cboOperacion_SelectedIndexChanged(object sender, EventArgs e)
       {
           if (cboOperacion.SelectedValue == "2" || cboOperacion.SelectedValue == "3")
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

               if (Session["idCliente"] == null)
               {
                   return;
               }
               var idCliente = Convert.ToInt32(Session["idCliente"]);
               dtDatosCuentaSolCliente = RetornaCuentaSolCliente.RetornaCuentaSolCliente(idCliente, "C", "[5]");
               dtgCreditos.DataSource = dtDatosCuentaSolCliente;
               dtgCreditos.DataBind();

               txtMontoCapital.Enabled = false;
               dtpFechaDesembolso.Enabled = false;
           }
           else
           {
               txtMontoCapital.Enabled = true;
               dtpFechaDesembolso.Enabled = true;
           }
           txtMontoCapital.Text = "0.00";
            
       }

       protected void P_CheckedChanged(object sender, EventArgs e)
       {
           try
           {
               GridViewRow row = ((GridViewRow)((CheckBox)sender).NamingContainer);
               int index = row.RowIndex;
               CheckBox cb1 = (CheckBox)dtgCreditos.Rows[index].FindControl("P");
               
               //verificar que no haya otro seleccionado
               foreach (GridViewRow r in dtgCreditos.Rows)
               {
                   if (r.RowIndex != index)
                   {
                       ((CheckBox)r.FindControl("P")).Checked = false;                       
                   }
               }
               
               double CapitalReprog = 0.00;
               //obtener todos los datos del grid
               foreach (GridViewRow r in dtgCreditos.Rows)
               {
                   double saldoCreditoSel = Convert.ToDouble(r.Cells[5].Text) + 
                            Convert.ToDouble(r.Cells[6].Text) + 
                            Convert.ToDouble(r.Cells[7].Text);
                   if (((CheckBox)r.Cells[10].Controls[1]).Checked)
                   {
                       cboFrecuencia.SelectedValue = r.Cells[9].Text;
                       cambiarFrecuencia();
                       CapitalReprog = CapitalReprog + saldoCreditoSel;
                   }
               }
               txtMontoCapital.Text = CapitalReprog.ToString();
           }
           catch (Exception ex)
           {
               throw;
           }
       }
    }
}