using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using SGA.AccesoDatos;

namespace SGA.LogicaNegocio
{
    public class clsCNEvaluacion
    {
        clsADEvaluacion adevaluacion = new clsADEvaluacion();
        public DataTable ListarPropuestaCredito()
        {
            try
            {
                return adevaluacion.ListarPropuestaCredito();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ListarPropuestaCreditoidPropuesta(int idPropuesta)
        {
            try
            {
                return adevaluacion.ListarPropuestaCreditoidPropuesta(idPropuesta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ListarPropuestaCreditoidCli(int idCli)
        {
            try
            {
                return adevaluacion.ListarPropuestaCreditoidPropuesta( idCli);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ListarPropuestaCreditoidSolicitud(int idSolicitud)
        {
            try
            {
                return adevaluacion.ListarPropuestaCreditoidSolicitud(idSolicitud);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable InsertarPropuestaCredito(int idSolicitud, int idCli, string cEntornoFamiliar, string cGiroUbicacion,
            string cExperienciaCrediticia, string cEvaAnalisisFinanciero, string cGarantia, string cConclusion, string cReferencia, string cProveedores,
            DateTime dFechaReg, int idUsuReg, bool lVigente)
        {
            try
            {
                return adevaluacion.InsertarPropuestaCredito(idSolicitud, idCli, cEntornoFamiliar, cGiroUbicacion,
            cExperienciaCrediticia, cEvaAnalisisFinanciero, cGarantia, cConclusion, cReferencia, cProveedores, dFechaReg, idUsuReg, lVigente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ActualizarPropuestaCredito(int idPropuesta, int idSolicitud, int idCli, string cEntornoFamiliar, string cGiroUbicacion,
            string cExperienciaCrediticia, string cEvaAnalisisFinanciero, string cGarantia, string cConclusion, string cReferencia, string cProveedores,
            DateTime dFechaReg, int idUsuReg, bool lVigente)
        {
            try
            {
                return adevaluacion.ActualizarPropuestaCredito(idPropuesta, idSolicitud, idCli, cEntornoFamiliar, cGiroUbicacion,
            cExperienciaCrediticia, cEvaAnalisisFinanciero, cGarantia, cConclusion, cReferencia, cProveedores, dFechaReg, idUsuReg, lVigente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable EliminarPropuestaCredito(int idPropuesta)
        {
            try
            {
                return adevaluacion.EliminarPropuestaCredito(idPropuesta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Evaluacion

        public DataTable ListarEvaluacionCreditoCliente(int idCli)
        {
            try
            {
                return adevaluacion.ListarEvaluacionCreditoCliente(idCli);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ListarEvaluacionCredito()
        {
            try
            {
                return adevaluacion.ListarEvaluacionCredito();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ListarEvaluacionCreditoidEvaluacion(int idEvaluacion)
        {
            try
            {
                return adevaluacion.ListarEvaluacionCreditoidEvaluacion(idEvaluacion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable InsertarEvaluacionCredito(int idSolicitud, DateTime dFechaReg, int idTipoEvaluacion)
        {
            try
            {
                return adevaluacion.InsertarEvaluacionCredito(idSolicitud, dFechaReg, idTipoEvaluacion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ActualizarEvaluacionCredito(int idEvaluacion, int idSolicitud, DateTime dFechaReg, int idTipoEvaluacion)
        {
            try
            {
                return adevaluacion.ActualizarEvaluacionCredito(idEvaluacion, idSolicitud, dFechaReg, idTipoEvaluacion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable EliminarEvaluacionCredito(int idEvaluacion)
        {
            try
            {
                return adevaluacion.EliminarEvaluacionCredito( idEvaluacion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Evaluacion Consumo

        public DataTable ListarEvaluacionConsumo()
        {
            try
            {
                return adevaluacion.ListarEvaluacionConsumo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ListarEvaluacionConsumoidEvaluacion(int idEvaluacion)
        {
            try
            {
                return adevaluacion.ListarEvaluacionConsumoidEvaluacion(idEvaluacion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable InsertarEvaluacionConsumo(int idEvaluacion,
         decimal nIngresoBruto, decimal nIngresoConyuge, decimal nComisiones, decimal nOtrosIngresos, decimal nTotalIngresoBruto,
         decimal nIngresoNeto, decimal nIngresoNetoConyuge, decimal nComisionesNeto, decimal nOtrosIngresosNeto, decimal nTotalIngresoNeto,
         decimal nAlimentacion, decimal nEducacion, decimal nTransporte, decimal nAlquiler, decimal nServicios, decimal nImprevistos,
         decimal nTotalgastoFamiliar, int nNumHijos, int nDependientes, string cEdadHijo, decimal nColegio, decimal nUniversidad, decimal nMontoPension, string cObservaciones
            )
        {
            try
            {
                return adevaluacion.InsertarEvaluacionConsumo(idEvaluacion,
          nIngresoBruto, nIngresoConyuge, nComisiones, nOtrosIngresos, nTotalIngresoBruto,
          nIngresoNeto, nIngresoNetoConyuge, nComisionesNeto, nOtrosIngresosNeto, nTotalIngresoNeto,
          nAlimentacion, nEducacion, nTransporte, nAlquiler, nServicios, nImprevistos,
          nTotalgastoFamiliar, nNumHijos, nDependientes, cEdadHijo, nColegio, nUniversidad, nMontoPension, cObservaciones);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable ActualizarEvaluacionConsumo(int idEvaluacion,
         decimal nIngresoBruto, decimal nIngresoConyuge, decimal nComisiones, decimal nOtrosIngresos, decimal nTotalIngresoBruto,
         decimal nIngresoNeto, decimal nIngresoNetoConyuge, decimal nComisionesNeto, decimal nOtrosIngresosNeto, decimal nTotalIngresoNeto,
         decimal nAlimentacion, decimal nEducacion, decimal nTransporte, decimal nAlquiler, decimal nServicios, decimal nImprevistos,
         decimal nTotalgastoFamiliar, int nNumHijos, int nDependientes, string cEdadHijo, decimal nColegio, decimal nUniversidad, decimal nMontoPension, string cObservaciones
            )
        {
            try
            {
                return adevaluacion.ActualizarEvaluacionConsumo( idEvaluacion,
                  nIngresoBruto, nIngresoConyuge, nComisiones, nOtrosIngresos, nTotalIngresoBruto,
                  nIngresoNeto, nIngresoNetoConyuge, nComisionesNeto, nOtrosIngresosNeto, nTotalIngresoNeto,
                  nAlimentacion, nEducacion, nTransporte, nAlquiler, nServicios, nImprevistos,
                  nTotalgastoFamiliar, nNumHijos, nDependientes, cEdadHijo, nColegio, nUniversidad, nMontoPension, cObservaciones);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable ListarEvaluacionConsumoCreditoComercialidEvaluacion(int idEvaluacion)
        {
            try
            {
                return adevaluacion.ListarEvaluacionConsumoCreditoComercialidEvaluacion(idEvaluacion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable InsertarEvaluacionConsumoCreditoComercial(int idEvaluacion, string cBanco, decimal nMonto)
        {
            try
            {
                return adevaluacion.InsertarEvaluacionConsumoCreditoComercial(idEvaluacion, cBanco, nMonto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ActualizarEvaluacionConsumoCreditoComercial(int id, int idEvaluacion, string cBanco, decimal nMonto)
        {
            try
            {
                return adevaluacion.ActualizarEvaluacionConsumoCreditoComercial( id, idEvaluacion, cBanco, nMonto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public DataTable ListarEvaluacionConsumoCreditoDirectoid(int idEvaluacion)
        {
            try
            {
                return adevaluacion.ListarEvaluacionConsumoCreditoDirectoid(idEvaluacion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable InsertarEvaluacionConsumoCreditoDirecto(int idEvaluacion, string cBanco, decimal nMonto)
        {
            try
            {
                return adevaluacion.InsertarEvaluacionConsumoCreditoDirecto(idEvaluacion, cBanco, nMonto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ActualizarEvaluacionConsumoCreditoDirecto(int id, int idEvaluacion, string cBanco, decimal nMonto)
        {
            try
            {
                return adevaluacion.ActualizarEvaluacionConsumoCreditoDirecto(id, idEvaluacion, cBanco, nMonto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable ListarEvaluacionConsumoCreditoIndirectoidEvaluacion(int idEvaluacion)
        {
            try
            {
                return adevaluacion.ListarEvaluacionConsumoCreditoIndirectoidEvaluacion(idEvaluacion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable InsertarEvaluacionConsumoCreditoIndirecto(int idEvaluacion, string cBanco, decimal nMonto)
        {
            try
            {
                return adevaluacion.InsertarEvaluacionConsumoCreditoIndirecto( idEvaluacion, cBanco, nMonto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ActualizarEvaluacionConsumoCreditoIndirecto(int id, int idEvaluacion, string cBanco, decimal nMonto)
        {
            try
            {
                return adevaluacion.ActualizarEvaluacionConsumoCreditoIndirecto(id, idEvaluacion, cBanco, nMonto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Pyme

        public DataTable ListarEvaluacionPyme()
        {
            try
            {
                return adevaluacion.ListarEvaluacionPyme();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ListarEvaluacionPymeidEvaluacion(int idEvaluacion)
        {
            try
            {
                return adevaluacion.ListarEvaluacionPymeidEvaluacion(idEvaluacion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable InsertarEvaluacionPyme(int idEvaluacion, decimal nCaja, decimal nOtrosActivos,
            decimal nOtroPasivoCorriente, decimal nOtroPasivoNoCorriente, decimal nPatrimonio,
            decimal nCostoOperativo, decimal nTributo, decimal nTransporte, decimal nAlquiler, decimal nServicios, decimal nOtros)
        {
            try
            {
                return adevaluacion.InsertarEvaluacionPyme(idEvaluacion, nCaja, nOtrosActivos,
                                nOtroPasivoCorriente, nOtroPasivoNoCorriente, nPatrimonio, nCostoOperativo, nTributo, nTransporte, nAlquiler, nServicios, nOtros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ActualizarEvaluacionPyme(int idEvaluacion, decimal nCaja, decimal nOtrosActivos,
            decimal nOtroPasivoCorriente, decimal nOtroPasivoNoCorriente, decimal nPatrimonio)
        {
            try
            {
                return adevaluacion.ActualizarEvaluacionPyme(idEvaluacion, nCaja, nOtrosActivos,
                                nOtroPasivoCorriente, nOtroPasivoNoCorriente, nPatrimonio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ListarEvaluacionPymeDatosBalance()
        {
            try
            {
                return adevaluacion.ListarEvaluacionPymeDatosBalance();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ListarEvaluacionPymeDatosBalanceid(int idTipoDatoBalance)
        {
            try
            {
                return adevaluacion.ListarEvaluacionPymeDatosBalanceid(idTipoDatoBalance);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable InsertarEvaluacionPymeDatosBalance(int idEvaluacion, string
                        cDescripcion, decimal nMonto, int idTipoDatoBalance, bool lVigente)
        {
            try
            {
                return adevaluacion.InsertarEvaluacionPymeDatosBalance(idEvaluacion,
                        cDescripcion, nMonto, idTipoDatoBalance, lVigente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ActualizarEvaluacionPymeDatosBalance(int id, int idEvaluacion, string
                       cDescripcion, decimal nMonto, int idTipoDatoBalance, bool lVigente)
        {
            try
            {
                return adevaluacion.ActualizarEvaluacionPymeDatosBalance(id, idEvaluacion,
                        cDescripcion, nMonto, idTipoDatoBalance, lVigente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable EliminarEvaluacionPymeDatosBalance(int id)
        {
            try
            {
                return adevaluacion.EliminarEvaluacionPymeDatosBalance(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable ListarEvaluacionPymeEstado()
        {
            try
            {
                return adevaluacion.ListarEvaluacionPymeEstado();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ListarEvaluacionPymeEstadoid(int idEvaluacion)
        {
            try
            {
                return adevaluacion.ListarEvaluacionPymeEstadoid(idEvaluacion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable InsertarEvaluacionPymeEstado(int idEvaluacion, string cDescripcion,
            decimal nCantidad, decimal nPrecio, decimal nMonto, int idTipoDatoBalance, bool lVigente)
        {
            try
            {
                return adevaluacion.InsertarEvaluacionPymeEstado(idEvaluacion, cDescripcion,
             nCantidad, nPrecio, nMonto, idTipoDatoBalance, lVigente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ActualizarEvaluacionPymeEstado(int id, int idEvaluacion, string cDescripcion,
            decimal nCantidad, decimal nPrecio, decimal nMonto, int idTipoDatoBalance, bool lVigente)
        {
            try
            {
                return adevaluacion.ActualizarEvaluacionPymeEstado(id, idEvaluacion, cDescripcion,
             nCantidad, nPrecio, nMonto, idTipoDatoBalance, lVigente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable EliminarEvaluacionPymeEstado(int id)
        {
            try
            {
                return adevaluacion.EliminarEvaluacionPymeEstado(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
