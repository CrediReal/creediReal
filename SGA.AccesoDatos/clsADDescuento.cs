using SGA.ENTIDADES;
using Helper.Conector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGA.Utilitarios;

namespace SGA.AccesoDatos
{
    public class clsADDescuento
    {
        public List<clsDescuento> ListarDescuentosCli(int idCli,bool lSoloVig)
        {
            try
            {
                DataTable dtResult = new clsGENEjeSP().EjecSP("SGA_ListarDescuentosCli_Sp", idCli, lSoloVig);
                return (from DataRow row in dtResult.Rows select new clsDescuento(row)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public clsDBResp SaveDescuento(clsDescuento objDescuento,int idUsuReg)
        {
            try
            {
                string xmlDescuento = objDescuento.GetXml();
                DataTable dtResult = new clsGENEjeSP().EjecSP("SGA_SaveDescuentoCli_Sp", xmlDescuento, idUsuReg);
                return new clsDBResp(dtResult);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
