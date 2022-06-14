using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGA.AccesoDatos;
using SGA.ENTIDADES;

namespace SGA.LogicaNegocio
{
    public class clsCNMeta
    {
        public List<clsMeta> GetMetas(clsMeta objBusMeta)
        {
            try
            {
                return new clsADMeta().GetMetas(objBusMeta);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public clsDBResp SaveMetas(clsMeta objMeta, int idUsuReg)
        {
            try
            {
                return new clsADMeta().SaveMetas(objMeta, idUsuReg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
