using SGA.ENTIDADES;
using SGA.LogicaNegocio;
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

namespace SGA.Controles
{
    [ToolboxBitmap(typeof(DropDownList))]
    [ToolboxData("<{0}:ComboBoxClasificacion runat=server></{0}:ComboBoxClasificacion>")]
    public class ComboBoxClasificacion : ComboBoxBase
    {
        public void ListarSoloVigentes()
        {
            List<clsClasificacion> LstClasificaciones = new clsCNClasificacion().ListarClasificaciones(0, true);
            DataValueField = "idClasificacion";
            DataTextField = "cClasificacion";
            DataSource = LstClasificaciones;
            DataBind();
            AgregarTodos();
            SelectedIndex = -1;
        }

    }
}
