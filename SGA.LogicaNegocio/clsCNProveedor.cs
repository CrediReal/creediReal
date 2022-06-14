using SGA.AccesoDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.LogicaNegocio
{
    public class clsCNProveedor
    {
        clsADProveedor adproveedor = new clsADProveedor();

        public DataTable ListarProveedor()
        {
            try
            {
                return adproveedor.ListarProveedor();
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
                return adproveedor.ListarProveedorId(idProveedor);
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
                return adproveedor.InsertarProveedor(cProveedor, cDocumento, idTipo, idUsuReg, dFechaReg, lVigente);
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
                return adproveedor.ActualizarProveedor(idProveedor, cProveedor, cDocumento, idTipo, idUsuMod, dFechaMod, lVigente);
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
                return adproveedor.EliminarProveedor(idProveedor);
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
                return adproveedor.ListarTipoProveedor();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
