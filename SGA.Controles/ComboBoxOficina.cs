using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGA.ENTIDADES;
using SGA.LogicaNegocio;

namespace SGA.Controles
{
    [ToolboxBitmap(typeof(DropDownList))]
    [ToolboxData("<{0}:ComboBoxOficina runat=server></{0}:ComboBoxOficina>")]
    public class ComboBoxOficina : ComboBoxBase
    {
        public void ListarSoloVigentes()
        {
            List<clsOficina> LstOficinas = new clsCNOficina().ListarOficinas(0, true);
            DataValueField = "idOficina";
            DataTextField = "cNomOficina";
            DataSource = LstOficinas;
            DataBind();
            AgregarTodos();
            SelectedIndex = -1;
        }

        public void ListarOficinas()
        {
            List<clsOficina> LstOficinas = new clsCNOficina().ListarOficinas(0);
            DataValueField = "idOficina";
            DataTextField = "cNomOficina";
            DataSource = LstOficinas;
            DataBind();
            AgregarTodos();
            SelectedIndex = -1;
        }
    }
}
