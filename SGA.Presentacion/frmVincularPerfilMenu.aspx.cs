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
    public partial class frmVincularPerfilMenu : System.Web.UI.Page
    {
        #region Variables Globales

        clsWebJScript Script = new clsWebJScript();
        clsCNPerfil Perfil = new clsCNPerfil();
        clsCNMenu Menu = new clsCNMenu();
        DataTable dtPerfilMenu;

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (IsPostBack == true) return;

                //--- Añadir postBack a Treeview cuando hace check en un nodo  --------->
                TreeViewMenu.Attributes.Add("onclick", "postBackByObject()");
                //-------------------------------------------------------------------->

                //----------------- TITULO --------------------------->
                lblOpcion.Text = Session["cOpcion"].ToString().ToUpper() + ":";
                //---------------------------------------------------->

                this.TreeViewMenu.ForeColor = System.Drawing.Color.Black;

                PanelMenu.Visible = false;

                //---------- Cargar Todos los perfiles ---------------------------------------------------->
                DataTable dtPerfil = Perfil.ListarPerfilesVigentes();
                cboPerfil.DataSource = dtPerfil;

                cboPerfil.DataValueField = dtPerfil.Columns[0].ToString();//idPerfil
                cboPerfil.DataTextField = dtPerfil.Columns[1].ToString();//cPerfil
                cboPerfil.DataBind();
                if (dtPerfil.Rows.Count == 0)
                {
                    Script.Mensaje("No existen perfiles . . .");
                    return;
                }
                //---------------------------------------------------------------------------->

                PanelMenu.Visible = true;
                cargarMenu();
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void cboPerfil_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cargarMenu();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void check_changed(object sender, TreeNodeEventArgs e)
        {
            try
            {
                TreeNode Nodo = e.Node;

                DataTable dtMenu = (DataTable)(Session["dtMenuEdicion"]);

                int nIdOpcion = Convert.ToInt32(Nodo.Value);

                int nTipoMenu = 0;  //1: Nodo Hijo
                //2: Nodo Padre

                //---------------    Ubicar Nodo Padre   --------------------------------------------------->
                int idMenuPadre = -1;
                for (int i = 0; i < dtMenu.Rows.Count; i++)
                {
                    if (Convert.ToInt32(dtMenu.Rows[i]["idMenu"]) == nIdOpcion)//Ubicar Nodo en la tabla de Menú
                    {
                        nTipoMenu = Convert.ToInt32(dtMenu.Rows[i]["idTipoMenu"]);

                        //Ubicando al Nodo Padre
                        idMenuPadre = Convert.ToInt32(dtMenu.Rows[i]["idMenuPadre"]);
                        break;
                    }
                }
                //------------------------------------------------------------------------------------------->

                //----------- No se debe permitir checkear o descheckear al Nodo Padre directamnete, 
                //----------- debe ser en func. al check de los nodos hijos                       ------------->
                if (nTipoMenu == 2)
                {
                    //Script.Mensaje("No se permite acciones sobre el Nodo Padre.");
                    if (Nodo.Checked)
                    {
                        Nodo.Checked = false;
                    }
                    else
                    {
                        Nodo.Checked = true;
                    }
                    return;
                }
                //------------------------------------------------------------------------------------------->

                if (Nodo.Checked == true)
                {
                    //Todos los Nodo Padre deben Chequearse
                    TreeNode NodoPadre = Nodo;
                    while (NodoPadre.Parent != null)
                    {
                        NodoPadre = NodoPadre.Parent;
                        NodoPadre.Checked = true;
                    }
                }
                else
                {
                    if (idMenuPadre != -1)
                    {
                        // 'El administrador del Sistema' no puede desactivar la opción:
                        // 'Vincular Perfil con Menu' para su propio perfil, pero si a otros perfiles

                        //--------- Datos Usuario --------------------->
                        var usuario = (clsUsuario)Session["DatosUsuarioSession"];
                        //--------------------------------------------------->
                        if (nIdOpcion == 13 && Convert.ToInt32(cboPerfil.SelectedItem.Value) == 1)
                        {
                            Nodo.Checked = true;
                            Script.Mensaje("No debe desactivar la opción: " + Nodo.Text + " para el perfil: " + usuario.cPerfil);
                            return;
                        }

                        //----- Deschekear el Menu Padre (Nodo Padre) si todos los Nodos Hijos están descheckeados ------->
                        int nCantDescheckados = 0;//Cantidad de Hijos que están checkeados

                        TreeNode NodoPadre = Nodo;
                        while (NodoPadre.Parent != null)
                        {
                            NodoPadre = NodoPadre.Parent;
                            string a = NodoPadre.Text;
                            foreach (TreeNode Hijos in NodoPadre.ChildNodes)
                            {
                                if (Hijos.Checked == false)
                                    nCantDescheckados++;
                            }

                            if (nCantDescheckados == NodoPadre.ChildNodes.Count)//Todos los nodos hijos están descheckeados
                            {
                                NodoPadre.Checked = false;
                            }
                            else
                            {
                                break;
                            }
                            nCantDescheckados = 0;
                        }

                    }
                }//FIN ELSE
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void BotonGrabar1_Click(object sender, EventArgs e)
        {
            try
            {
                //-------- FORMAR TABLA MENU X PERFIL  --------------------------->
                dtPerfilMenu = new DataTable("dtPerfilMenu");

                dtPerfilMenu.Columns.Add("idMenu", typeof(int)); ;
                dtPerfilMenu.Columns.Add("cMenu", typeof(string));
                dtPerfilMenu.Columns.Add("lEsUsado", typeof(bool));

                foreach (TreeNode Nodo in TreeViewMenu.Nodes)
                {
                    AddTreNodeADataTable(Nodo);
                }
                //-------------------------------------------------------------->

                int idPerfil = Convert.ToInt32(cboPerfil.SelectedItem.Value);

                DataSet dsPerfilMenu = new DataSet("dsPerfilMenu");
                dsPerfilMenu.Tables.Add(dtPerfilMenu);

                string xmlPerfilMenu = dsPerfilMenu.GetXml();

                //------------------Valida Session Vigente -------------------------------------->
                if (Session["DatosUsuarioSession"] == null) { throw new Exception("La Sesión a terminado vuelva ingresar porfavor"); }
                //------------------------------------------------------------------------------>                

                //--------- Campos de Auditoría --------------------->
                var usuario = (clsUsuario)Session["DatosUsuarioSession"];
                int idUsuarioReg = usuario.idUsuario;
                string cNombrePc = usuario.cNamePc;
                string cMacPc = usuario.cMacPc;
                //--------------------------------------------------->

                DataTable Rpta = Perfil.InsVinculaPerfilMenu(xmlPerfilMenu, idPerfil, idUsuarioReg, cNombrePc, cMacPc);

                Script.Mensaje("La configuración se ha guardado correctamente...");

                BotonGrabar1.Visible = false;
                cargarMenu();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void BotonCancelar1_Click(object sender, EventArgs e)
        {
            try
            {
                cboPerfil.SelectedIndex = 0;
                cargarMenu();
                BotonGrabar1.Visible = true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void cargarMenu()
        {
            try
            {
                int idPerfil = Convert.ToInt32(cboPerfil.SelectedItem.Value);
                DataTable dtMenu = Menu.ListarMenuPorPerfilEnGeneral(idPerfil);
                Session["dtMenuEdicion"] = dtMenu;

                if (dtMenu.Rows.Count > 0)
                {
                    TreeViewMenu.Nodes.Clear();
                    TreeNode padre;
                    TreeNode hijo;
                    for (int i = 0; i < dtMenu.Rows.Count; i++)
                    {
                        hijo = new TreeNode();
                        hijo.Text = ".  " + dtMenu.Rows[i]["cMenu"].ToString();
                        hijo.Value = dtMenu.Rows[i]["idMenu"].ToString();

                        if (dtMenu.Rows[i]["idTipoMenu"].ToString() == "2")//MENU PADRE
                        {
                            hijo.ImageUrl = @"~/Imagenes/TreeView/t01.jpg";
                            if (Convert.ToBoolean(dtMenu.Rows[i]["lEsUsado"]))
                            {
                                hijo.Checked = true;
                            }
                            else
                            {
                                hijo.Checked = false;
                            }
                        }

                        if (dtMenu.Rows[i]["idTipoMenu"].ToString() == "1")//MENU HIJO
                        {
                            hijo.ImageUrl = @"~/Imagenes/TreeView/t04.jpg";
                            if (Convert.ToBoolean(dtMenu.Rows[i]["lEsUsado"]))
                            {
                                hijo.Checked = true;
                            }
                            else
                            {
                                hijo.Checked = false;
                            }
                        }

                        hijo.ToolTip = dtMenu.Rows[i]["cMenu"].ToString();
                        padre = buscarPadre(dtMenu.Rows[i]["idMenuPadre"].ToString(), TreeViewMenu.Nodes);
                        if (padre == null)
                            this.TreeViewMenu.Nodes.Add(hijo);
                        else
                            padre.ChildNodes.Add(hijo);
                        padre = null;
                    }

                    TreeViewMenu.Nodes[0].ChildNodes[0].Expand();
                    this.TreeViewMenu.Focus();
                    TreeViewMenu.ExpandAll();
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

        private void AddTreNodeADataTable(TreeNode Nodo)
        {
            try
            {
                DataRow dr = dtPerfilMenu.NewRow();
                dr["idMenu"] = Nodo.Value;
                dr["cMenu"] = Nodo.Text;
                dr["lEsUsado"] = Nodo.Checked;
                dtPerfilMenu.Rows.Add(dr);
                foreach (TreeNode NodoHijo in Nodo.ChildNodes)
                {
                    AddTreNodeADataTable(NodoHijo);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}