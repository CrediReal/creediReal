using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGA.ENTIDADES;
using SGA.Utilitarios;
using Helper.Conector;

namespace SGA.AccesoDatos
{
    public class clsADConfiguracion
    {
        public List<clsConfiguracion> ListarConfiguraciones(int id, string cConfiguracion, bool lSoloVig)
        {
            try
            {
                DataTable dtResult = new clsGENEjeSP().EjecSP("SGA_ListarConfiguraciones_Sp", id, cConfiguracion, lSoloVig);
                return (from DataRow row in dtResult.Rows select new clsConfiguracion(row)).ToList();
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
                string xmlConfiguracion = objConfiguracion.GetXml();
                DataTable dtResult = new clsGENEjeSP().EjecSP("SGA_SaveConfiguracion_Sp", xmlConfiguracion);
                return new clsDBResp(dtResult);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
