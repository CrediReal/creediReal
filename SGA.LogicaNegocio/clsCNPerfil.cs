using System;
using System.Collections.Generic;
using System.Text;
using SGA.AccesoDatos;
using System.Data;

namespace SGA.LogicaNegocio
{
    public class clsCNPerfil
    {
        clsADPerfil objPerfil = new clsADPerfil();

        public DataTable ListarPerfilesVigentes()
        {
            try
            {
                return objPerfil.ADListarPerfilesVigentes();
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
                return objPerfil.InsVinculaPerfilMenu(xmlPerfilMenu, idPerfil, idUsuarioReg, cNombrePc, cMacPc);
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
                return objPerfil.BuscaPerfilDeUsuario(idUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
