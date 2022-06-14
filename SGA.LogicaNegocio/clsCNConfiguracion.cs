using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGA.AccesoDatos;
using SGA.ENTIDADES;

namespace SGA.LogicaNegocio
{
    public class clsCNConfiguracion
    {
        public List<clsConfiguracion> ListarConfiguraciones(int id, string cConfiguracion, bool lSoloVig = false)
        {
            try
            {
                return new clsADConfiguracion().ListarConfiguraciones(id, cConfiguracion, lSoloVig);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public clsConfiguracion GetConfiguracionById(int id)
        {
            try
            {
                return ListarConfiguraciones(id, string.Empty).First();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<clsConfiguracion> GetConfiguracionByName(string cConfiguracion)
        {
            try
            {
                return ListarConfiguraciones(0, cConfiguracion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public clsDBResp SaveConfiguracion(clsConfiguracion objConfiguracion)
        {
            try
            {
                return new clsADConfiguracion().SaveConfiguracion(objConfiguracion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
