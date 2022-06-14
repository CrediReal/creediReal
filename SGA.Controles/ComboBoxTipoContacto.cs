using SGA.ENTIDADES;
using SGA.LogicaNegocio;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGA.Controles
{
    [ToolboxBitmap(typeof(DropDownList))]
    [ToolboxData("<{0}:ComboBoxTipoContacto runat=server></{0}:ComboBoxTipoContacto>")]
    public class ComboBoxTipoContacto : ComboBoxBase
    {
        public void ListarTipoContactos()
        {
            List<clsTipoContacto> LstTipoContacto = new clsCNTipoContacto().GetTipoContacto(0, true);
            DataValueField = "idTipoContacto";
            DataTextField = "cTipoContacto";
            DataSource = LstTipoContacto;
            DataBind();
            AgregarTodos();
            SelectedIndex = -1;
        }
    }
}
