using System;
using System.Collections.Generic;
using System.Text;
using SGA.AccesoDatos;
using System.Data;

namespace SGA.LogicaNegocio
{
    public class clsCNSexo
    {
        clsADSexo objlistaSexo = new clsADSexo();

        public DataTable ListarSexo()
        {
            try
            {
                return objlistaSexo.ListaSexo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
