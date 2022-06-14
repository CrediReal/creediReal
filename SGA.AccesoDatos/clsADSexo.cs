using System;
using System.Text;
using Helper.Conector;
using System.Data;

namespace SGA.AccesoDatos
{
    public class clsADSexo
    {
        clsGENEjeSP objEjeSp = new clsGENEjeSP();

        public DataTable ListaSexo()
        {
            try
            {
                return objEjeSp.EjecSP("SGA_ListaSexo_Sp");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
