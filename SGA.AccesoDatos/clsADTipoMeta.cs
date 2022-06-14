using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGA.ENTIDADES;
using System.Data;
using Helper.Conector;
using SGA.Utilitarios;

namespace SGA.AccesoDatos
{
    public class clsADTipoMeta
    {
        public List<clsTipoMeta> ListarTipoMetas(int id, bool lSoloVig)
        {
            try
            {
                DataTable dtResult = new clsGENEjeSP().EjecSP("SGA_ListarTipoMetas_Sp", id, lSoloVig);
                return (from DataRow row in dtResult.Rows select new clsTipoMeta(row)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public clsDBResp SaveTipoMeta(clsTipoMeta objTipoMeta)
        {
            try
            {
                string xmlTipoMeta = objTipoMeta.GetXml();
                DataTable dtResult = new clsGENEjeSP().EjecSP("SGA_SaveTipoMeta_Sp", xmlTipoMeta);
                return new clsDBResp(dtResult);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
