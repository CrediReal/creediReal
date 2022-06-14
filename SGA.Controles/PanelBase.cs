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
    [System.Drawing.ToolboxBitmap(typeof(Panel))]
    [ToolboxData("<{0}:PanelBase runat=server></{0}:PanelBase>")]
    public class PanelBase : Panel
    {
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            this.CssClass = "pnlBase";
        }

    }
}