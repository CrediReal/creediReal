using System;
using System.Collections.Generic;
using System.Linq;
using SGA.AccesoDatos;
using SGA.ENTIDADES;


namespace SGA.LogicaNegocio
{
    public class clsCNClasificacion
    {
        public List<clsClasificacion> ListarClasificaciones(int id, bool lSoloVig = false)
        {
            try
            {
                return new clsADClasificacion().ListarClasificaciones(id, lSoloVig);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public clsClasificacion GetClasificacionById(int id)
        {
            try
            {
                return ListarClasificaciones(id).First();
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
                return new clsADClasificacion().SaveClasificacion(objClasificacion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
