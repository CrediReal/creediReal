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
    public class clsADModelo
    {
        public List<clsModelo> GetModelos(int idMarca, bool lSoloVig)
        {
            DataTable dtResult = new clsGENEjeSP().EjecSP("SGA_ListarModelos_Sp", idMarca, lSoloVig);
            return (from DataRow row in dtResult.Rows select new clsModelo(row)).ToList();
        }

        public clsDBResp SaveModelo(clsModelo objModelo)
        {
            string xmlModelo = objModelo.GetXml();
            DataTable dtResult = new clsGENEjeSP().EjecSP("SGA_SaveModelo_Sp", xmlModelo);
            return new clsDBResp(dtResult);
        }
    }
}
