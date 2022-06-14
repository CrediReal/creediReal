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
    [ToolboxData("<{0}:BotonAgregar runat=server></{0}:BotonAgregar>")]
    public class BotonAgregar : Button
    {
        protected override void Render(HtmlTextWriter writer)
        {
            this.Text = "Agregar";
            this.CssClass = "btnBase_Agregar";

            base.Render(writer);
        }
    }
}
