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
    public class clsADTipo
    {
        public List<clsTipo> ListarTipos(int id, string cTipo, bool lSoloVig)
        {
            try
            {
                DataTable dtResult = new clsGENEjeSP().EjecSP("SGA_ListarTipos_Sp", id, cTipo, lSoloVig);
                return (from DataRow row in dtResult.Rows select new clsTipo(row)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public clsDBResp SaveTipo(clsTipo objTipo)
        {
            try
            {
                string xmlTipo = objTipo.GetXml();
                DataTable dtResult = new clsGENEjeSP().EjecSP("SGA_SaveTipo_Sp", xmlTipo);
                return new clsDBResp(dtResult);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
