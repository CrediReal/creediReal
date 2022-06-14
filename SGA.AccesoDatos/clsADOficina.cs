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
    public class clsADOficina
    {
        public List<clsOficina> ListarOficinas(int id, bool lSoloVig)
        {
            try
            {
                DataTable dtResult = new clsGENEjeSP().EjecSP("SGA_ListarOficinas_Sp", id, lSoloVig);
                return (from DataRow row in dtResult.Rows select new clsOficina(row)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public clsDBResp SaveOficina(clsOficina objOficina)
        {
            try
            {
                string xmlOficina = objOficina.GetXml();
                DataTable dtResult = new clsGENEjeSP().EjecSP("SGA_SaveOficina_Sp", xmlOficina);
                return new clsDBResp(dtResult);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
