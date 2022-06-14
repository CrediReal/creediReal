using SGA.AccesoDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.LogicaNegocio
{
    public class clsCNBanco
    {
        clsADBanco adbanco = new clsADBanco();

        public DataTable ListarBanco()
        {
            try
            {
                return adbanco.ListarBanco();
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
                return adbanco.ListarBancoidBanco(idBanco);
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
                return adbanco.InsertarBanco(cBanco, lVigente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ActualizarBanco(int idBanco, string cBanco, bool lVigente)
        {
            try
            {
                return adbanco.ActualizarBanco(idBanco, cBanco, lVigente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
