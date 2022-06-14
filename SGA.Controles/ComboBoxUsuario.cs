using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGA.LogicaNegocio;

namespace SGA.Controles
{
    [ToolboxBitmap(typeof(DropDownList))]
    [ToolboxData("<{0}:ComboBoxUsuario runat=server></{0}:ComboBoxUsuario>")]
    public class ComboBoxUsuario : ComboBoxBase
    {
        public int idOficina { get; set; }
        public string cPerfiles { get; set; }

        public void ListarUsuarios()
        {
            DataTable dt = new clsCNUsuario().ListarUsuariosOficinaPerfil(idOficina, cPerfiles);
            DataValueField = "idUsuario";
            DataTextField = "cUsuario";
            DataSource = dt;
            DataBind();
            AgregarTodos();
            SelectedIndex = -1;
        }
        public void ListarUsuarios(int idOfi, string cPerfiles)
        {
            DataTable dt = new clsCNUsuario().ListarUsuariosOficinaPerfil(idOfi, cPerfiles);
            DataValueField = "idUsuario";
            DataTextField = "cUsuario";
            DataSource = dt;
            DataBind();
            AgregarTodos();
            SelectedIndex = -1;
        }
        public void ListarUsuarios(int idOfi)
        {
            DataTable dt = new clsCNUsuario().ListarUsuariosOficinaPerfil(idOfi, cPerfiles);
            DataValueField = "idUsuario";
            DataTextField = "cUsuario";
            DataSource = dt;
            DataBind();
            AgregarTodos();
            SelectedIndex = -1;
        }
    }
}
