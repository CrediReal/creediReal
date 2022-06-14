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
    [ToolboxData("<{0}:BotonAsignar runat=server></{0}:BotonAsignar>")]
    public class BotonAsignar : Button
    {
        protected override void Render(HtmlTextWriter writer)
        {
            this.Text = "Asignar Valor";
            this.CssClass = "btnBase_Aceptar";

            base.Render(writer);
        }
    }
}
