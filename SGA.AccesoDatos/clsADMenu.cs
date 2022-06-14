using System;
using System.Text;
using Helper.Conector;
using System.Data;

namespace SGA.AccesoDatos
{
    public class clsADMenu
    {
        clsGENEjeSP objEjeSp = new clsGENEjeSP();

        public DataTable ListarMenuPerfil(int idUsuario, int idPerfil)
        {
            try
            {
                return objEjeSp.EjecSP("SGA_ListarMenuPerfil_SP", idUsuario, idPerfil);
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
                return objEjeSp.EjecSP("SGA_ListarMenuPorPerfilEnGeneral_SP", idPerfil);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
