using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGA.LogicaNegocio;
using System.Data;
using System.Drawing;

namespace SGA.Controles
{
    [ToolboxBitmap(typeof(DropDownList))]
    [ToolboxData("<{0}:ComboBoxProvincia runat=server></{0}:ComboBoxProvincia>")]
    public class ComboBoxProvincia : ComboBoxBase
    {
        public void ListarProvincias(string cCodDepartamento)
        {
            DataTable dt = new clsCNUbigeo().ListarProvincia(cCodDepartamento);

            DataValueField = dt.Columns[0].ColumnName;
            DataTextField = dt.Columns[1].ColumnName;
            DataSource = dt;
            DataBind();
            AgregarTodos();
            SelectedIndex = -1;
        }
    }
}
