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
    [ToolboxData("<{0}:BotonQuitarItemTodos runat=server></{0}:BotonQuitarItemTodos>")]
    public class BotonQuitarItemTodos : Button
    {
        protected override void Render(HtmlTextWriter writer)
        {
            this.Text = "Quitar Todos";
            this.CssClass = "btnBase_QuitarItemTodos";

            base.Render(writer);
        }
    }
}
