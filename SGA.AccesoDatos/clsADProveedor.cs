using Helper.Conector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.AccesoDatos
{
    public class clsADProveedor
    {
        clsGENEjeSP objEjeSp = new clsGENEjeSP();

        public DataTable ListarProveedor()
        {
            try
            {
                return objEjeSp.EjecSP("SGA_ListarProveedor_SP");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ListarProveedorId(int idProveedor)
        {
            try
            {
                return objEjeSp.EjecSP("SGA_ListarProveedoridProveedor_SP", idProveedor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public DataTable InsertarProveedor(string cProveedor, string cDocumento, int idTipo, int idUsuReg, DateTime dFechaReg, bool lVigente)
        {
            try
            {
                return objEjeSp.EjecSP("SGA_InsertarProveedor_SP", cProveedor, cDocumento, idTipo, idUsuReg, dFechaReg, lVigente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ActualizarProveedor(int idProveedor, string cProveedor, string cDocumento, int idTipo, int idUsuMod, DateTime dFechaMod, bool lVigente)
        {
            try
            {
                return objEjeSp.EjecSP("SGA_ActualizarProveedor_SP", idProveedor, cProveedor, cDocumento, idTipo, idUsuMod, dFechaMod, lVigente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable EliminarProveedor(int idProveedor)
        {
            try
            {
                return objEjeSp.EjecSP("SGA_EliminarProveedor_SP", idProveedor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ListarTipoProveedor()
        {
            try
            {
                return objEjeSp.EjecSP("SGA_ListarTipoProveedor_SP");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
