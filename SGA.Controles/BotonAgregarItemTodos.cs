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
    [ToolboxData("<{0}:BotonAgregarItemTodos runat=server></{0}:BotonAgregarItemTodos>")]
    public class BotonAgregarItemTodos : Button
    {
        protected override void Render(HtmlTextWriter writer)
        {
            this.Text = "Agregar Todos";
            this.CssClass = "btnBase_AgregarItemTodos";

            base.Render(writer);
        }
    }
}
