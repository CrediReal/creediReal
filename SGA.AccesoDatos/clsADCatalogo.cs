using Helper.Conector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.AccesoDatos
{
    public class clsADCatalogo
    {
        clsGENEjeSP objEjeSp = new clsGENEjeSP();

        public DataTable ListarTipoBien()
        {
            try
            {
                return objEjeSp.EjecSP("SGA_ListarTipoBien_SP");
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
                return objEjeSp.EjecSP("SGA_ListarCatalogoXid_SP", idCatalogo);
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
                return objEjeSp.EjecSP("SGA_BuscarCatalogoPorCodExterno_SP", cCodigoProducto);
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
                return objEjeSp.EjecSP("SGA_InsertarCatalogo_SP", cProducto, idTipoBien, idGrupo, lVigente, idUnidadCompra, idUnidadAlmacenaje,
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
                return objEjeSp.EjecSP("SGA_ActualizarCatalogo_SP",idCatalogo,  cProducto, idTipoBien, idGrupo, lVigente, idUnidadCompra, idUnidadAlmacenaje,
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
                return objEjeSp.EjecSP("SGA_EliminarCatalogo_SP", idCatalogo);
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
                return objEjeSp.EjecSP("SGA_ListarCatalogoidGrupo_SP", idGrupo);
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
                return objEjeSp.EjecSP("SGA_ListarUnidad_SP");
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
                return objEjeSp.EjecSP("SGA_BuscarCatalogoPorNombre_SP", cProducto);
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
                return objEjeSp.EjecSP("SGA_ListarCatalogoInstala_SP");
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
                return objEjeSp.EjecSP("SGA_ListarCatalogoPaquete_SP");
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
                return objEjeSp.EjecSP("SGA_BuscarCatalogoPorSerie_SP", cSerie, idTipoSalida);
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
                return objEjeSp.EjecSP("SGA_BuscarProductosxResponsable_SP", idResponsable);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
