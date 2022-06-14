using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

[assembly: TagPrefix("ControlesBase", "FastSolutions")]
namespace SGA.Controles
{
    [System.Drawing.ToolboxBitmap(typeof(TreeView))]
    [ToolboxData("<{0}:TreeViewBase runat=server></{0}:TreeViewBase>")]
    public class TreeViewBase : TreeView
    {
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            this.ShowLines = true;
            this.SelectedNodeStyle.CssClass = "treeNodeSeleccionado";
            this.CssClass = "treeViewBase";
            this.NodeStyle.CssClass = "treeNodeBase";
        }
    }
}
