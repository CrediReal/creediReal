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
    [System.Drawing.ToolboxBitmap(typeof(ListBox))]
    [ToolboxData("<{0}:ListBoxBase runat=server></{0}:ListBoxBase>")]
    public class ListBoxBase : ListBox
    {
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            this.CssClass = "listBoxBase";
        }
    }
}
