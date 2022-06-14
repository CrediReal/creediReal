using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using SGA.ENTIDADES;
using SGA.Utilitarios;
using Helper.Conector;

namespace SGA.AccesoDatos
{
    public class clsADClasificacion
    {
        public List<clsClasificacion> ListarClasificaciones(int id, bool lSoloVig)
        {
            try
            {
                DataTable dtResult = new clsGENEjeSP().EjecSP("SGA_ListarClasificaciones_Sp", id, lSoloVig);
                return (from DataRow row in dtResult.Rows select new clsClasificacion(row)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public clsDBResp SaveClasificacion(clsClasificacion objClasificacion)
        {
            try
            {
                string xmlClasificacion = objClasificacion.GetXml();
                DataTable dtResult = new clsGENEjeSP().EjecSP("SGA_SaveClasificacion_Sp", xmlClasificacion);
                return new clsDBResp(dtResult);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
