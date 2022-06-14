using Helper.Conector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SGA.AccesoDatos
{
    public class clsADAlmacen
    {
        clsGENEjeSP objEjeSp = new clsGENEjeSP();

        public DataTable ListarAlmacen()
        {
            try
            {
                return objEjeSp.EjecSP("SGA_ListarAlmacen_SP");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable InsertarAlmacen(string cAlmacen, string cDireccion, string cRefDirec, string cFono, int idUsuRes, int idUsuReg, bool lVigente, string cCodExterno)
        {
            try
            {
                return objEjeSp.EjecSP("SGA_InsertarAlmacen_SP", cAlmacen, cDireccion, cRefDirec, cFono, idUsuRes, idUsuReg, lVigente, cCodExterno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ActualizarAlmacen(int idAlmacen, string cAlmacen, string cDireccion, string cRefDirec, string cFono, int idUsuRes, int idUsuReg, bool lVigente, string cCodExterno)
        {
            try
            {
                return objEjeSp.EjecSP("SGA_ActualizarAlmacen_SP", idAlmacen, cAlmacen, cDireccion, cRefDirec, cFono, idUsuRes, idUsuReg, lVigente, cCodExterno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable EliminarAlmacen(int idAlmacen)
        {
            try
            {
                return objEjeSp.EjecSP("SGA_EliminarAlmacen_SP", idAlmacen);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ListarAlmacenXid(int idAlmacen)
        {
            try
            {
                return objEjeSp.EjecSP("SGA_ListarAlmacenXid_SP", idAlmacen);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Entrada a Almacen

        public DataTable ListarEntrada(DateTime dFecIni, DateTime dFecFin)
        {
            try
            {
                return objEjeSp.EjecSP("SGA_ListarEntrada_SP", dFecIni, dFecFin);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ListarEntradaPorId(int idEntrada)
        {
            try
            {
                return objEjeSp.EjecSP("SGA_ListarEntradaidEntrada_SP", idEntrada);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable InsertarEntrada(string cNroDocumento, int idAlmacen, int idUsuario, int idProveedor, int idTipoEntradaSalida, decimal nTotal, string xmlDetalle, bool lAveria)
        {
            try
            {
                return objEjeSp.EjecSP("SGA_InsertarEntrada_SP", cNroDocumento, idAlmacen, idUsuario, idProveedor, idTipoEntradaSalida, nTotal, xmlDetalle, lAveria);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ActualizarEntrada(int idEntrada, string cNroDocumento, int idAlmacen, int idUsuario, int idProveedor, int idTipoEntradaSalida, decimal nTotal, string xmlDetalle)
        {
            try
            {
                return objEjeSp.EjecSP("SGA_ActualizarEntrada_SP", idEntrada, cNroDocumento, idAlmacen, idUsuario, idProveedor, idTipoEntradaSalida, nTotal, xmlDetalle);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ListarDetalleEntrada(int idEntrada)
        {
            try
            {
                return objEjeSp.EjecSP("SGA_ListarEntradaidEntrada_SP", idEntrada);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable RptEntradaAlamcen(int idEntrada)
        {
            try
            {
                return objEjeSp.EjecSP("SGA_RptEntradaAlamcen_SP", idEntrada);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Salida de Almacen

        public DataTable ListarTipoEntradaSalida()
        {
            try
            {
                return objEjeSp.EjecSP("SGA_ListarTipoEntradaSalida_SP");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable InsertarSalida(string cOrdenTrabajo, int idAlmacen, int idUsuario, int idResponsable, int idTipoEntradaSalida, decimal nTotal, string xmlDetalle)
        {
            try
            {
                return objEjeSp.EjecSP("SGA_InsertarSalida_SP", cOrdenTrabajo, idAlmacen, idUsuario, idResponsable, idTipoEntradaSalida, nTotal, xmlDetalle);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
