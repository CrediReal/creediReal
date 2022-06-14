using SGA.ENTIDADES;
using SGA.LogicaNegocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGA.Presentacion
{
    public partial class frmMenu : System.Web.UI.Page
    {
        DataTable dtMenu;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == true) return;
            cargarModulos();
            cargarMenu(1);  
        }
        private void cargarMenu(int idModulo)
        {
            clsCNMenu Menu = new clsCNMenu();

            var usuarioSession = (clsUsuario)Session["DatosUsuarioSession"];
           var  dtMenuTotal = new GEN.CapaNegocio.clsCNMenu().LisTreeMenByPer(usuarioSession.idPerfil);
           dtMenu = FiltrarDT(dtMenuTotal, idModulo);

            //dtMenu = Menu.ListarMenuPerfil(usuarioSession.idUsuario, usuarioSession.idPerfil);

            Session["dtMenu"] = dtMenu;
            tvMenu.Nodes.Clear();
            if (dtMenu.Rows.Count > 0)
            {             
                TreeNode padre;
                TreeNode hijo;
                for (int i = 0; i < dtMenu.Rows.Count; i++)
                {
                    hijo = new TreeNode();
                    hijo.Text = ".  " + dtMenu.Rows[i]["cMenu"].ToString();
                    hijo.Value = dtMenu.Rows[i]["idMenu"].ToString();

                    if (dtMenu.Rows[i]["idTipoMenu"].ToString() == "2")
                    {
                        hijo.ImageUrl = @"~/Imagenes/TreeView/t02_.png";
                    }

                    if (dtMenu.Rows[i]["idTipoMenu"].ToString() == "1")
                    {
                        hijo.ImageUrl = @"~/Imagenes/TreeView/t03_.png";
                    }

                    hijo.ToolTip = dtMenu.Rows[i]["cMenu"].ToString();
                    padre = buscarPadre(dtMenu.Rows[i]["idMenuPadre"].ToString(), tvMenu.Nodes);
                    if (padre == null)
                        this.tvMenu.Nodes.Add(hijo);
                    else
                        padre.ChildNodes.Add(hijo);
                    padre = null;
                }

                if (tvMenu.Nodes.Count > 0)
                {
                    tvMenu.ExpandAll();
                }              
            }
        }

        private TreeNode buscarPadre(String NodoBusqueda, TreeNodeCollection nodos)
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

        protected void tvMenu_SelectedNodeChanged(object sender, EventArgs e)
        {
            //------------------Valida Session Vigente -------------------------------------->
            if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor"); }
            //------------------------------------------------------------------------------>

            var idMenu = tvMenu.SelectedNode.Value;
            dtMenu = (DataTable)Session["dtMenu"];
            if (dtMenu.Rows.Count > 0)
            {
                var cForm = (from item in dtMenu.AsEnumerable()
                             where item["idMenu"].ToString() == idMenu
                             select item).FirstOrDefault()["cFormMenu"].ToString();
                if (cForm != "")
                {
                    //----------------- TITULO ------------------------>
                    Session["cOpcion"] = tvMenu.SelectedNode.Text;
                    Session["cForm"] = cForm;
                    Session["idMenu"] = idMenu;
                    //-------------------------------------------------->

                    string strScript = "";
                    strScript = @"<script type=""text/javascript"">";
                    strScript += @"parent.window.frames[""Contenido""].location.href = ";
                    strScript += @"""" + cForm + @""";";
                    strScript += @"</script>";

                    if (cForm=="CREDITOS/FrmCobroMasivo.aspx")
                    {
                        clsUsuario objUsuario;
                        if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor"); }
                        else
                        {
                            objUsuario = (clsUsuario)Session["DatosUsuarioSession"];
                        }
                        new CRE.CapaNegocio.clsCNCredito().DesbMasByAse(objUsuario.idUsuario, objUsuario.idUsuario);
                    }

                    Response.Write(strScript);
                }
                else
                {
                    tvMenu.SelectedNode.Expand();
                }
                tvMenu.SelectedNode.Selected = true;
            }
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
            GEN.CapaNegocio.clsCNModulo cnmodulo= new GEN.CapaNegocio.clsCNModulo();
            var dtModulo = cnmodulo.LisModulo();
            cboModulo.DataValueField = "idModulo";
            cboModulo.DataTextField = "cModulo";
            cboModulo.DataSource = dtModulo;
            cboModulo.DataBind();
            cboModulo.SelectedValue = "1";
        }

        protected void cboModulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboModulo.SelectedIndex>-1)
            {
                var idModulo = Convert.ToInt32(cboModulo.SelectedValue);
                cargarMenu(idModulo);                 
            }
        }
    }
}