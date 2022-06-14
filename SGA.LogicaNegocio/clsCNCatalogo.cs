using SGA.AccesoDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.LogicaNegocio
{
    public class clsCNCatalogo
    {
        clsADCatalogo adcatalogo = new clsADCatalogo();

        public DataTable ListarTipoBien()
        {
            try
            {
                return adcatalogo.ListarTipoBien();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ListarCatalogoXid(int idCatalogo)
        {
            try
            {
                return adcatalogo.ListarCatalogoXid( idCatalogo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable BuscarCatalogoCodExterno(string cCodigoProducto)
        {
            try
            {
                return adcatalogo.BuscarCatalogoCodExterno(cCodigoProducto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable InsertarCatalogo(string cProducto, int idTipoBien, int idGrupo, bool lVigente, int idUnidadCompra, int idUnidadAlmacenaje,
            decimal nValConversion, int idUsuReg, bool lIndActivo, string cObservacion, int idEstado, decimal nCantidad, decimal nPrecioUnit, string cCodigoProducto)
        {
            try
            {
                return adcatalogo.InsertarCatalogo( cProducto, idTipoBien, idGrupo, lVigente, idUnidadCompra, idUnidadAlmacenaje,
            nValConversion, idUsuReg, lIndActivo, cObservacion, idEstado, nCantidad, nPrecioUnit, cCodigoProducto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ActualizarCatalogo(int idCatalogo,string cProducto, int idTipoBien, int idGrupo, bool lVigente, int idUnidadCompra, int idUnidadAlmacenaje,
            decimal nValConversion, int idUsuMod, bool lIndActivo, string cObservacion, int idEstado, decimal nCantidad, decimal nPrecioUnit, string cCodigoProducto)
        {
            try
            {
                return adcatalogo.ActualizarCatalogo(idCatalogo,cProducto, idTipoBien, idGrupo, lVigente, idUnidadCompra, idUnidadAlmacenaje,
            nValConversion, idUsuMod, lIndActivo, cObservacion, idEstado, nCantidad, nPrecioUnit, cCodigoProducto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable EliminarCatalogo(int idCatalogo)
        {
            try
            {
                return adcatalogo.EliminarCatalogo( idCatalogo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ListarCatalogoidGrupo(int idGrupo)
        {
            try
            {
                return adcatalogo.ListarCatalogoidGrupo( idGrupo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ListarUnidad()
        {
            try
            {
                return adcatalogo.ListarUnidad();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable BuscarCatalogo(string cProducto)
        {
            try
            {
                return adcatalogo.BuscarCatalogo(cProducto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ListarCatalogoInstalacion()
        {
            try
            {
                return adcatalogo.ListarCatalogoInstalacion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public DataTable ListarCatalogoPaquete()
        {
            try
            {
                return adcatalogo.ListarCatalogoPaquete();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable BuscarCatalogoPorSerie(string cSerie, int idTipoSalida)
        {
            try
            {
                return adcatalogo.BuscarCatalogoPorSerie(cSerie, idTipoSalida);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable BuscarProductosxResponsable(int idResponsable)
        {
            try
            {
                return adcatalogo.BuscarProductosxResponsable(idResponsable);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
