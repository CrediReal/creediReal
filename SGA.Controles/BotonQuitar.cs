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
    [ToolboxData("<{0}:BotonQuitar runat=server></{0}:BotonQuitar>")]
    public class BotonQuitar : Button
    {
        protected override void Render(HtmlTextWriter writer)
        {
            this.Text = "Quitar";
            this.CssClass = "btnBase_Quitar";

            base.Render(writer);
        }
    }
}
