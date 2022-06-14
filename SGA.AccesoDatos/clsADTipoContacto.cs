using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGA.ENTIDADES;
using System.Data;
using Helper.Conector;

namespace SGA.AccesoDatos
{
    public class clsADTipoContacto
    {
        public List<clsTipoContacto> GetTipoContacto(int id, bool lSoloVig)
        {
            try
            {
                DataTable dtResult = new clsGENEjeSP().EjecSP("SGA_ListarTipoContacto_Sp", id, lSoloVig);
                return (from DataRow row in dtResult.Rows select new clsTipoContacto(row)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
