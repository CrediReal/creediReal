using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGA.Controles
{
    [ToolboxData("<{0}:BotonMiniAceptar runat=server></{0}:BotonMiniAceptar>")]
    public class BotonMiniAceptar : Button
    {
        protected override void Render(HtmlTextWriter writer)
        {
            this.CssClass = "btnBase_Aceptar";

            base.Render(writer);
        }
    }
}
