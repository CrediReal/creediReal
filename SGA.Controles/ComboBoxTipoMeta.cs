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
    [ToolboxData("<{0}:ComboBoxTipoMeta runat=server></{0}:ComboBoxTipoMeta>")]
    public class ComboBoxTipoMeta : ComboBoxBase
    {
        public void ListarTipoMetas()
        {
            List<clsTipoMeta> LstTipoMeta = new clsCNTipoMeta().ListarTipoMetas(0, true);
            DataValueField = "idTipoMeta";
            DataTextField = "cTipoMeta";
            DataSource = LstTipoMeta;
            DataBind();
            AgregarTodos();
            SelectedIndex = -1;
        }
    }
}
