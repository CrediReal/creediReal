﻿using Helper.Conector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.AccesoDatos
{
    public class clsADMoneda
    {
        clsGENEjeSP objEjeSp = new clsGENEjeSP();

        public DataTable ListarMoneda()
        {
            try
            {
                return objEjeSp.EjecSP("SGA_ListarMoneda_SP");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
