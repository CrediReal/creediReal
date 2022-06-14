using SGA.AccesoDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.LogicaNegocio
{
    public class clsCNAlmacen
    {
        clsADAlmacen adalmacen = new clsADAlmacen();

        public DataTable ListarAlmacen()
        {
            try
            {
                return adalmacen.ListarAlmacen();
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
                return adalmacen.InsertarAlmacen(cAlmacen, cDireccion, cRefDirec, cFono, idUsuRes, idUsuReg, lVigente, cCodExterno);
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
                return adalmacen.ActualizarAlmacen(idAlmacen, cAlmacen, cDireccion, cRefDirec, cFono, idUsuRes, idUsuReg, lVigente, cCodExterno);
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
                return adalmacen.EliminarAlmacen(idAlmacen);
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
                return adalmacen.ListarAlmacenXid(idAlmacen);
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
                return adalmacen.ListarEntrada(dFecIni, dFecFin);
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
                return adalmacen.ListarEntradaPorId(idEntrada);
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
                return adalmacen.InsertarEntrada(cNroDocumento, idAlmacen, idUsuario, idProveedor, idTipoEntradaSalida, nTotal, xmlDetalle, lAveria);
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
                return adalmacen.ActualizarEntrada(idEntrada, cNroDocumento, idAlmacen, idUsuario, idProveedor, idTipoEntradaSalida, nTotal, xmlDetalle);
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
                return adalmacen.ListarDetalleEntrada(idEntrada);
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
                return adalmacen.RptEntradaAlamcen(idEntrada);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        public DataTable ListarTipoEntrada()
        {
            try
            {
                var dtTipoEntrada = adalmacen.ListarTipoEntradaSalida();
                var dtEntrada = dtTipoEntrada.Clone();

                dtTipoEntrada.AsEnumerable().Where(x => Convert.ToBoolean(x["lEntrada"]) == true).CopyToDataTable(dtEntrada, LoadOption.OverwriteChanges);

                return dtEntrada;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ListarTipoSalida()
        {
            try
            {
                var dtTipoSalida = adalmacen.ListarTipoEntradaSalida();
                var dtSalida = dtTipoSalida.Clone();

                dtTipoSalida.AsEnumerable().Where(x => Convert.ToBoolean(x["lEntrada"]) == false).CopyToDataTable(dtSalida, LoadOption.OverwriteChanges);

                return dtSalida;
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
                return adalmacen.InsertarSalida(cOrdenTrabajo, idAlmacen, idUsuario, idResponsable, idTipoEntradaSalida, nTotal, xmlDetalle);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
