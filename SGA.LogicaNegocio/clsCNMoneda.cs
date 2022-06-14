using SGA.AccesoDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.LogicaNegocio
{
    public class clsCNMoneda
    {
        clsADMoneda admoneda = new clsADMoneda();
        public DataTable ListarMoneda()
        {
            try
            {
                return admoneda.ListarMoneda();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
