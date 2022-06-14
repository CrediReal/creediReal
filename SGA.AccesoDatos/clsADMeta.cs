using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SGA.ENTIDADES;
using SGA.Utilitarios;
using Helper.Conector;

namespace SGA.AccesoDatos
{
    public class clsADMeta
    {
        public List<clsMeta> GetMetas(clsMeta objBusMeta)
        {
            try
            {
                string xmlBusMetas = objBusMeta.GetXml();
                DataTable dtResult = new clsGENEjeSP().EjecSP("SGA_ListarMetas_Sp", xmlBusMetas);
                return (from DataRow row in dtResult.Rows select new clsMeta(row)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public clsDBResp SaveMetas(clsMeta objMeta,int idUsuReg)
        {
            try
            {
                string xmlMeta = objMeta.GetXml();
                DataTable dtResult = new clsGENEjeSP().EjecSP("SGA_SaveMetasUsu_Sp", xmlMeta, idUsuReg);
                return new clsDBResp(dtResult);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
