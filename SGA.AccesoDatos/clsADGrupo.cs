using Helper.Conector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SGA.AccesoDatos
{
    public class clsADGrupo
    {
        clsGENEjeSP objEjeSp = new clsGENEjeSP();

        public DataTable ListarGrupoActivo()
        {
            try
            {
                return objEjeSp.EjecSP("SGA_ListarGrupoActivo_SP");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable ListarGrupoXidGrupo(int idGrupoActivo)
        {
            try
            {
                return objEjeSp.EjecSP("SGA_ListarGrupoActivoidGrupoActivo_SP", idGrupoActivo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable InsertarGrupoActivo(string cNombreGrupo, int idPadre, int idTipoBien, bool lvigente, string cCuentaContable)
        {
            try
            {
                return objEjeSp.EjecSP("SGA_InsertarGrupoActivo_SP", cNombreGrupo, idPadre, idTipoBien, lvigente, cCuentaContable);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ActualizarGrupoActivo(int idGrupoActivo,string cNombreGrupo, int idPadre, int idTipoBien, bool lvigente, string cCuentaContable)
        {
            try
            {
                return objEjeSp.EjecSP("SGA_ActualizarGrupoActivo_SP", idGrupoActivo, cNombreGrupo, idPadre, idTipoBien, lvigente, cCuentaContable);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable EliminarGrupoActivo(int idGrupoActivo)
        {
            try
            {
                return objEjeSp.EjecSP("SGA_EliminarGrupoActivo_SP", idGrupoActivo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
