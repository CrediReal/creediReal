using Helper.Conector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.AccesoDatos
{
    public class clsADUbigeo
    {
        clsGENEjeSP objEjeSp = new clsGENEjeSP();

        #region Departamentos

        public DataTable ListarDepartamentoBracko()
        {
            try
            {
                return objEjeSp.EjecSP("SGA_ListarDepartamentoBracko_SP");
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
                return objEjeSp.EjecSP("SGA_ListarDepartamento_SP");
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
                return objEjeSp.EjecSP("SGA_ListarProvincia_SP", cCodDepartamento);
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
                return objEjeSp.EjecSP("SGA_ListarDistrito_SP", cCodDepartamento, cCodProvincia);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
