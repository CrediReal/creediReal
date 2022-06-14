using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGA.ENTIDADES;
using SGA.LogicaNegocio;

namespace SGA.Controles
{
    [ToolboxBitmap(typeof(DropDownList))]
    [ToolboxData("<{0}:ComboBoxModelo runat=server></{0}:ComboBoxModelo>")]
    public class ComboBoxModelo : ComboBoxBase
    {
        public void ListarModelos()
        {
            List<clsModelo> LstModelos = new clsCNModelo().GetModelos(0, true);
            DataValueField = "idModelo";
            DataTextField = "cModelo";
            DataSource = LstModelos;
            DataBind();
            AgregarTodos();
            SelectedIndex = -1;
        }
    }
}
