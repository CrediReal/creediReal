using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using SGA.ENTIDADES;
using SGA.Utilitarios;
using Helper.Conector;

namespace SGA.AccesoDatos
{
    public class clsADMarca
    {
        public List<clsMarca> ListarMarcas(int id, string cMarca, bool lSoloVig)
        {
            try
            {
                DataTable dtResult = new clsGENEjeSP().EjecSP("SGA_ListarMarcas_Sp", id, cMarca, lSoloVig);
                return (from DataRow row in dtResult.Rows select new clsMarca(row)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public clsDBResp SaveMarca(clsMarca objMarca)
        {
            try
            {
                string xmlMarca = objMarca.GetXml();
                DataTable dtResult = new clsGENEjeSP().EjecSP("SGA_SaveMarca_Sp", xmlMarca);
                return new clsDBResp(dtResult);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
