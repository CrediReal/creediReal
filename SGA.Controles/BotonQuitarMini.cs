using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

[assembly: TagPrefix("ControlesBase", "FastSolutions")]
namespace SGA.Controles
{
    [ToolboxData("<{0}:BotonQuitarMini runat=server></{0}:BotonQuitarMini>")]
    public class BotonQuitarMini : Button
    {
        protected override void Render(HtmlTextWriter writer)
        {
            this.Text = "";
            this.CssClass = "btnBase_QuitarMini";

            base.Render(writer);
        }
    }
}
