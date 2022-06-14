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
    [ToolboxData("<{0}:BotonBuscar runat=server></{0}:BotonBuscar>")]
    public class BotonBuscar : Button
    {
        protected override void Render(HtmlTextWriter writer)
        {
            this.Text = "Buscar";
            this.CssClass = "btnBase_Consultar";
            base.Render(writer);
        }
    }
}
