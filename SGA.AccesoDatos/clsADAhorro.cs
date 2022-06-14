using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Helper.Conector;

namespace SGA.AccesoDatos
{
    public class clsADAhorro
    {
        clsGENEjeSP objEjeSp = new clsGENEjeSP();

        public DataTable ListarAlmacen()
        {
            try
            {
                return objEjeSp.EjecSP("sgc_ListarAhorro_SP");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
