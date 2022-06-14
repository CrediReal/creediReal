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
    [System.Drawing.ToolboxBitmap(typeof(Label))]
    [ToolboxData("<{0}:LabelBase runat=server></{0}:LabelBase>")]
    public class LabelBase : Label
    {
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            this.CssClass = "lblBase";
        }

    }
}