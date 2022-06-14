using SGA.AccesoDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.LogicaNegocio
{
    public class clsCNGrupo
    {
        clsADGrupo adgrupo = new clsADGrupo();

        public DataTable ListarGrupoActivo()
        {
            try
            {
                return adgrupo.ListarGrupoActivo();
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
                return adgrupo.ListarGrupoXidGrupo(idGrupoActivo);
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
                return adgrupo.InsertarGrupoActivo(cNombreGrupo, idPadre, idTipoBien, lvigente, cCuentaContable);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ActualizarGrupoActivo(int idGrupoActivo, string cNombreGrupo, int idPadre, int idTipoBien, bool lvigente, string cCuentaContable)
        {
            try
            {
                return adgrupo.ActualizarGrupoActivo(idGrupoActivo, cNombreGrupo, idPadre, idTipoBien, lvigente, cCuentaContable);
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
                return adgrupo.EliminarGrupoActivo(idGrupoActivo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
