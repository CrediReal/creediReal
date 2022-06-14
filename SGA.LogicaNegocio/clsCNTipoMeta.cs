using SGA.AccesoDatos;
using SGA.ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.LogicaNegocio
{
    public class clsCNTipoMeta
    {
        public List<clsTipoMeta> ListarTipoMetas(int id, bool lSoloVig = false)
        {
            try
            {
                return new clsADTipoMeta().ListarTipoMetas(id, lSoloVig);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public clsTipoMeta GetTipoMetaById(int id)
        {
            try
            {
                return ListarTipoMetas(id).First();
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
                return new clsADTipoMeta().SaveTipoMeta(objTipoMeta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
