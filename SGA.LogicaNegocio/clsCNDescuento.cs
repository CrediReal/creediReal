using SGA.AccesoDatos;
using SGA.ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.LogicaNegocio
{
    public class clsCNDescuento
    {
        public List<clsDescuento> ListarDescuentosCli(int idCli, bool lSoloVig = true)
        {
            try
            {
                return new clsADDescuento().ListarDescuentosCli(idCli, lSoloVig);
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
                return new clsADDescuento().SaveDescuento(objDescuento, idUsuReg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
