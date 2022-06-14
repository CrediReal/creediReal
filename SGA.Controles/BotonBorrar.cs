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
    [ToolboxData("<{0}:BotonBorrar runat=server></{0}:BotonBorrar>")]
    public class BotonBorrar : Button
    {
        protected override void Render(HtmlTextWriter writer)
        {
            this.Text = "Borrar";
            this.CssClass = "btnBase_Quitar";

            base.Render(writer);
        }
    }
}
