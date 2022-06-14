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
    [ToolboxData("<{0}:BotonQuitarItem runat=server></{0}:BotonQuitarItem>")]
    public class BotonQuitarItem : Button
    {
        protected override void Render(HtmlTextWriter writer)
        {
            this.Text = "Quitar";
            this.CssClass = "btnBase_QuitarItem";

            base.Render(writer);
        }
    }
}
