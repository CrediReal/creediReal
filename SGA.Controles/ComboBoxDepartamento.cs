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
using SGA.LogicaNegocio;
using System.Data;

namespace SGA.Controles
{
    [ToolboxBitmap(typeof(DropDownList))]
    [ToolboxData("<{0}:ComboBoxDepartamento runat=server></{0}:ComboBoxDepartamento>")]
    public class ComboBoxDepartamento : ComboBoxBase
    {
        public bool lBracko { get; set; }

        public void CargarDepartamentos()
        {
            if (!Page.IsPostBack)
            {
                var dt = lBracko ? new clsCNUbigeo().ListarDepartamentoBracko() : new clsCNUbigeo().ListarDepartamento();

                DataValueField = dt.Columns[0].ColumnName;
                DataTextField = dt.Columns[1].ColumnName;
                DataSource = dt;
                DataBind();
                AgregarTodos();
            }
        }

    }
}
