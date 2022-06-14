using Helper.Conector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.AccesoDatos
{
    public class clsADBanco
    {
        clsGENEjeSP objEjeSp = new clsGENEjeSP();

        public DataTable ListarBanco()
        {
            try
            {
                return objEjeSp.EjecSP("SGA_ListarBanco_SP");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public DataTable ListarBancoidBanco(int idBanco)
        {
            try
            {
                return objEjeSp.EjecSP("SGA_ListarBancoidBanco_SP", idBanco);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable InsertarBanco(string cBanco, bool lVigente)
        {
            try
            {
                return objEjeSp.EjecSP("SGA_InsertarBanco_SP", cBanco, lVigente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ActualizarBanco(int idBanco,string cBanco, bool lVigente)
        {
            try
            {
                return objEjeSp.EjecSP("SGA_ActualizarBanco_SP",idBanco, cBanco, lVigente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
