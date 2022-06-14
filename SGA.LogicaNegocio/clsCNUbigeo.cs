using SGA.AccesoDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.LogicaNegocio
{
    public class clsCNUbigeo
    {
        clsADUbigeo adubigeo = new clsADUbigeo();

        #region Departamentos

        public DataTable ListarDepartamentoBracko()
        {
            try
            {
                return adubigeo.ListarDepartamentoBracko();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ListarDepartamento()
        {
            try
            {
                return adubigeo.ListarDepartamento();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Provincias

        public DataTable ListarProvincia(string cCodDepartamento)
        {
            try
            {
                return adubigeo.ListarProvincia(cCodDepartamento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Distritos

        public DataTable ListarDistrito(string cCodDepartamento,string cCodProvincia)
        {
            try
            {
                return adubigeo.ListarDistrito(cCodDepartamento,cCodProvincia);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
