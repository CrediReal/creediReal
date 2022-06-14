using SGA.AccesoDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.LogicaNegocio
{
    public class clsCNMenu
    {
        clsADMenu objadmenu = new clsADMenu();

        public DataTable ListarMenuPerfil(int idUsuario, int idPerfil)
        {
            try
            {
                return objadmenu.ListarMenuPerfil(idUsuario, idPerfil);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ListarMenuPorPerfilEnGeneral(int idPerfil)
        {
            try
            {
                return objadmenu.ListarMenuPorPerfilEnGeneral(idPerfil);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
