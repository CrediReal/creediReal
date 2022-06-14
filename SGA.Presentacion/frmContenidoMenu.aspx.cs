using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using SGA.ENTIDADES;
using SGA.LogicaNegocio;

namespace SGA.Presentacion
{
    public partial class frmContenidoMenu : System.Web.UI.Page
    {
        DataTable dtMenu;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == true) return;
            //------------------Valida Session Vigente -------------------------------------->
            if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor"); }
            //------------------------------------------------------------------------------>
            Session["cOpcion"] = "";
            clsUsuario ObjetoUsuario = new clsUsuario();
            ObjetoUsuario = (clsUsuario)(Session["DatosUsuarioSession"]);
            hUsuario.Value = ObjetoUsuario.cUsuario;
            hPerfil.Value = ObjetoUsuario.idPerfil.ToString();

            cargarModulos();
            cargarMenu(1);
            cargarDatosUsuario();
           
            //" - Oficina: " + ObjetoUsuario.cNombreAge;
            lblInfoFecha.Text = ObjetoUsuario.dFecSystem.ToLongDateString();
            lblInfoAgencia.Text = "Oficina: " + ObjetoUsuario.cNombreAge;
        }

        private void cargarMenu(int idModulo)
        {
            clsUsuario usuarioSession = new clsUsuario();
            if (Session["DatosUsuarioSession"] == null)
            {
                Session["DatosUsuarioSession"] = new clsCNUsuario().GetDatosUsuario(hUsuario.Value);
                usuarioSession = (clsUsuario)Session["DatosUsuarioSession"];
            }
            GEN.CapaNegocio.clsCNMenu Menu = new GEN.CapaNegocio.clsCNMenu();

            usuarioSession = (clsUsuario)Session["DatosUsuarioSession"];

            var dtMenuTotal = new GEN.CapaNegocio.clsCNMenu().LisTreeMenByPer(Convert.ToInt32(hPerfil.Value));
            dtMenu = FiltrarDT(dtMenuTotal, idModulo);

            ViewState["dtMenu"] = dtMenu;
            HtmlGenericControl html_ul;
            HtmlGenericControl html_li;
            HtmlGenericControl html_a;
            HtmlGenericControl html_i;
            HtmlGenericControl html_span;
            HtmlGenericControl html_span2;
            HtmlGenericControl html_i2;
            if (dtMenu.Rows.Count > 0)
            {
                foreach (DataRow item in dtMenu.Rows)
                {
                    if (item["idTipoMenu"].ToString()=="2")
                    {
                        html_li = new HtmlGenericControl("li");
                        html_a = new HtmlGenericControl("a");
                        html_i = new HtmlGenericControl("i");
                        html_span = new HtmlGenericControl("span");
                        html_span2 = new HtmlGenericControl("span");
                        html_i2 = new HtmlGenericControl("i");
                        html_ul = new HtmlGenericControl("ul");
                        html_li.Attributes.Add("class", "treeview");
                        html_a.Attributes.Add("href", "#");
                        html_i.Attributes.Add("class", "fa fa-folder-open");
                        html_span.InnerText = item["cMenu"].ToString();
                        html_span2.Attributes.Add("class", "pull-right-container");
                        html_i2.Attributes.Add("class", "fa fa-angle-left pull-right");
                        html_ul.Attributes.Add("class", "treeview-menu");
                        html_span2.Controls.Add(html_i2);

                        html_a.Controls.Add(html_i);
                        html_a.Controls.Add(html_span);
                        html_a.Controls.Add(html_span2);

                        html_li.Controls.Add(html_a);

                        cargarSubMenu(Convert.ToInt32(item["idMenu"]), html_ul);

                        html_li.Controls.Add(html_ul);
                        ulMenu.Controls.Add(html_li);
                    }                    
                }                
            }
        }

        private void cargarSubMenu(int idMenu, HtmlGenericControl html_ul)
        {
            var dtMenu = (DataTable)ViewState["dtMenu"];
            var dtsubMenu = dtMenu.AsEnumerable().Where(x => (int)x["idMenuPadre"] == idMenu).CopyToDataTable();
            HtmlGenericControl html_li;
            HtmlGenericControl html_a;
            HtmlGenericControl html_i;
            HtmlGenericControl html_span;

            foreach (DataRow item in dtsubMenu.Rows)
            {
                html_li = new HtmlGenericControl("li");
                html_a = new HtmlGenericControl("a");
                html_i = new HtmlGenericControl("i");
                html_span = new HtmlGenericControl("span");
                html_i.Attributes.Add("class", "fa fa-tag");
                html_a.Attributes.Add("href", "#");
                html_a.Attributes.Add("onclick", "go('" + item["cFormMenu"].ToString() + "?usuario=" + hUsuario.Value + "&perfil=" + hPerfil.Value + "','" + item["cMenu"].ToString() + "')");
                html_span.Attributes.Add("class", "pull-right-container");
                html_span.Controls.Add(html_i);
                html_a.InnerText = item["cMenu"].ToString();
                html_a.Controls.Add(html_span);
                html_li.Controls.Add(html_a);
                html_ul.Controls.Add(html_li);
            }
        }

        private void cargarDatosUsuario()
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

            lblNombreUser.Text = objUsuario.cNombre + " " + objUsuario.cApellidoPaterno;
            lblNombreUserMenu.Text = objUsuario.cNombre + " " + objUsuario.cApellidoPaterno;
            lblNombreUserCargo.Text = " Colaborador de CrediReal";
            lblFechaInicio.Text = "Perfil: " + objUsuario.cPerfil;
            var cImgUsuario = @"~/Imagenes/usuarios/" + objUsuario.cNombre.ToLower() + "." + objUsuario.cApellidoPaterno.ToLower() + ".jpg";
            var cRuta = MapPath(cImgUsuario);
            if (File.Exists(cRuta))
            {
                imgUsuario.Attributes["src"] = ResolveUrl(cImgUsuario);
                imgUsuario2.Attributes["src"] = ResolveUrl(cImgUsuario);
                imgUsuario3.Attributes["src"] = ResolveUrl(cImgUsuario);
                imgUsuario4.Attributes["src"] = ResolveUrl(cImgUsuario);

            }
            else
            {
                imgUsuario.Attributes["src"] = ResolveUrl(@"~/Imagenes/usuarios/user.jpg");
                imgUsuario2.Attributes["src"] = ResolveUrl(@"~/Imagenes/usuarios/user.jpg");
                imgUsuario3.Attributes["src"] = ResolveUrl(@"~/Imagenes/usuarios/user.jpg");
                imgUsuario4.Attributes["src"] = ResolveUrl(@"~/Imagenes/usuarios/user.jpg");
            }
        }
        
        protected void btnSalir_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Session.RemoveAll();
            string strScript = "";
            Response.Redirect("frmInicioBoot.aspx", true);
        }

        private DataTable FiltrarDT(DataTable dt, int idModulo)
        {
            try
            {
                DataTable dtnew;
                //dtnew = dt.Rows.OfType<DataRow>().Where((item) => Convert.ToInt32(item["idModulo"]) == idModulo).CopyToDataTable();

                //dtnew = dt.Clone();
                var query = from d in dt.AsEnumerable()
                            where Convert.ToInt32(d["idModulo"]) == idModulo
                            select d;
                if (query.Count() > 0)
                {
                    dtnew = query.CopyToDataTable();
                }
                else
                {
                    dtnew = new DataTable();
                }
                return dtnew;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void cargarModulos()
        {
            GEN.CapaNegocio.clsCNModulo cnmodulo = new GEN.CapaNegocio.clsCNModulo();
            var dtModulo = cnmodulo.LisModulo();
            cboModulo.DataValueField = "idModulo";
            cboModulo.DataTextField = "cModulo";
            cboModulo.DataSource = dtModulo;
            cboModulo.DataBind();
            cboModulo.SelectedValue = "1";
        }

        protected void cboModulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboModulo.SelectedIndex > -1)
            {
                var idModulo = Convert.ToInt32(cboModulo.SelectedValue);
                cargarMenu(idModulo);
            }
        }
    }
}