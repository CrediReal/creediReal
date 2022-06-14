using Helper.Conector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.AccesoDatos
{
    public class clsADDocumento
    {
        clsGENEjeSP objEjeSp = new clsGENEjeSP();

        public DataTable ListarTipoDocumento()
        {
            try
            {
                return objEjeSp.EjecSP("SGA_ListarTipoDocumento_SP");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
