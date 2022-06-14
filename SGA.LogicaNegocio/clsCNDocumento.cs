using SGA.AccesoDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.LogicaNegocio
{
    public class clsCNDocumento
    {
        clsADDocumento addocumento = new clsADDocumento();

        public DataTable ListarTipoDocumento()
        {
            try
            {
                return addocumento.ListarTipoDocumento();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
