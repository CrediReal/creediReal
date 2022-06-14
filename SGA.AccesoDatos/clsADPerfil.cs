using System;
using System.Text;
using System.Data;
using Helper.Conector;

namespace SGA.AccesoDatos
{
    public class clsADPerfil
    {
        clsGENEjeSP objEjeSp = new clsGENEjeSP();

        public DataTable ADListarPerfilesVigentes()
        {
            try
            {
                return objEjeSp.EjecSP("SGA_ListarPerfilesVigentes_SP");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable InsVinculaPerfilMenu(string xmlPerfilMenu, int idPerfil, int idUsuarioReg, string cNombrePc, string cMacPc)
        {
            try
            {
                return objEjeSp.EjecSP("SGA_InsVinculaPerfilMenu_SP", xmlPerfilMenu, idPerfil, idUsuarioReg, cNombrePc, cMacPc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable BuscaPerfilDeUsuario(int idUsuario)
        {
            try
            {
                return objEjeSp.EjecSP("SGA_BuscaPerfilDeUsuario_SP", idUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
