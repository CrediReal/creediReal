using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Helper.Conector;

namespace SGA.AccesoDatos
{
    public class clsADEvaluacion
    {
        clsGENEjeSP objEjeSp = new clsGENEjeSP();

        public DataTable ListarPropuestaCredito()
        {
            try
            {
                return objEjeSp.EjecSP("sgc_ListarPropuestaCredito_SP");
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
                return objEjeSp.EjecSP("sgc_ListarPropuestaCreditoidPropuesta_SP", idPropuesta);
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
                return objEjeSp.EjecSP("sgc_ListarPropuestaCreditoidCli_SP", idCli);
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
                return objEjeSp.EjecSP("sgc_ListarPropuestaCreditoidSolicitud_SP", idSolicitud);
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
                return objEjeSp.EjecSP("sgc_InsertarPropuestaCredito_SP", idSolicitud, idCli, cEntornoFamiliar, cGiroUbicacion,
            cExperienciaCrediticia, cEvaAnalisisFinanciero, cGarantia, cConclusion, cReferencia, cProveedores,dFechaReg, idUsuReg, lVigente);
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
                return objEjeSp.EjecSP("sgc_ActualizarPropuestaCredito_SP", idPropuesta,idSolicitud, idCli, cEntornoFamiliar, cGiroUbicacion,
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
                return objEjeSp.EjecSP("sgc_EliminarPropuestaCredito_SP", idPropuesta);
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
                return objEjeSp.EjecSP("sgc_ListarEvaluacionCreditoCliente_SP", idCli);
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
                return objEjeSp.EjecSP("sgc_ListarEvaluacionCredito_SP");
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
                return objEjeSp.EjecSP("sgc_ListarEvaluacionCreditoidEvaluacion_SP", idEvaluacion);
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
                return objEjeSp.EjecSP("sgc_InsertarEvaluacionCredito_SP",idSolicitud, dFechaReg, idTipoEvaluacion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ActualizarEvaluacionCredito(int idEvaluacion,int idSolicitud, DateTime dFechaReg, int idTipoEvaluacion)
        {
            try
            {
                return objEjeSp.EjecSP("sgc_ActualizarEvaluacionCredito_SP",idEvaluacion, idSolicitud, dFechaReg, idTipoEvaluacion);
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
                return objEjeSp.EjecSP("sgc_EliminarEvaluacionCredito_SP", idEvaluacion);
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
                return objEjeSp.EjecSP("sgc_ListarEvaluacionConsumo_SP");
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
                return objEjeSp.EjecSP("sgc_ListarEvaluacionConsumoidEvaluacion_SP", idEvaluacion);
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
                return objEjeSp.EjecSP("sgc_InsertarEvaluacionConsumo_SP", idEvaluacion,
          nIngresoBruto,  nIngresoConyuge,  nComisiones,  nOtrosIngresos,  nTotalIngresoBruto,
          nIngresoNeto,  nIngresoNetoConyuge,  nComisionesNeto,  nOtrosIngresosNeto,  nTotalIngresoNeto,
          nAlimentacion,  nEducacion,  nTransporte,  nAlquiler,  nServicios,  nImprevistos,
          nTotalgastoFamiliar,  nNumHijos,  nDependientes,  cEdadHijo,  nColegio,  nUniversidad,  nMontoPension,  cObservaciones);
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
                return objEjeSp.EjecSP("sgc_ActualizarEvaluacionConsumo_SP",  idEvaluacion,
                  nIngresoBruto,  nIngresoConyuge,  nComisiones,  nOtrosIngresos,  nTotalIngresoBruto,
                  nIngresoNeto,  nIngresoNetoConyuge,  nComisionesNeto,  nOtrosIngresosNeto,  nTotalIngresoNeto,
                  nAlimentacion,  nEducacion,  nTransporte,  nAlquiler,  nServicios,  nImprevistos,
                  nTotalgastoFamiliar,  nNumHijos,  nDependientes,  cEdadHijo,  nColegio,  nUniversidad,  nMontoPension,  cObservaciones);
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
                return objEjeSp.EjecSP("sgc_ListarEvaluacionConsumoCreditoComercialidEvaluacion_SP", idEvaluacion);
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
                return objEjeSp.EjecSP("sgc_InsertarEvaluacionConsumoCreditoComercial_SP", idEvaluacion, cBanco, nMonto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ActualizarEvaluacionConsumoCreditoComercial(int id	,int idEvaluacion, string cBanco, decimal nMonto)
        {
            try
            {
                return objEjeSp.EjecSP("sgc_ActualizarEvaluacionConsumoCreditoComercial_SP", id	,idEvaluacion, cBanco,  nMonto);
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
                return objEjeSp.EjecSP("sgc_ListarEvaluacionConsumoCreditoDirectoid_SP", idEvaluacion);
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
                return objEjeSp.EjecSP("sgc_InsertarEvaluacionConsumoCreditoDirecto_SP", idEvaluacion,cBanco, nMonto);
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
                return objEjeSp.EjecSP("sgc_ActualizarEvaluacionConsumoCreditoDirecto_SP", id, idEvaluacion,  cBanco,  nMonto);
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
                return objEjeSp.EjecSP("sgc_ListarEvaluacionConsumoCreditoIndirectoidEvaluacion_SP", idEvaluacion);
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
                return objEjeSp.EjecSP("sgc_InsertarEvaluacionConsumoCreditoIndirecto_SP", idEvaluacion, cBanco, nMonto);
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
                return objEjeSp.EjecSP("sgc_ActualizarEvaluacionConsumoCreditoIndirecto_SP", id, idEvaluacion, cBanco, nMonto);
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
                return objEjeSp.EjecSP("sgc_ListarEvaluacionPyme_SP");
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
                return objEjeSp.EjecSP("sgc_ListarEvaluacionPymeidEvaluacion_SP", idEvaluacion);
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
                return objEjeSp.EjecSP("sgc_InsertarEvaluacionPyme_SP",idEvaluacion, nCaja, nOtrosActivos,
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
                return objEjeSp.EjecSP("sgc_ActualizarEvaluacionPyme_SP", idEvaluacion, nCaja, nOtrosActivos,
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
                return objEjeSp.EjecSP("sgc_ListarEvaluacionPymeDatosBalance_SP");
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
                return objEjeSp.EjecSP("sgc_ListarEvaluacionPymeDatosBalanceid_SP", idTipoDatoBalance);
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
                return objEjeSp.EjecSP("sgc_InsertarEvaluacionPymeDatosBalance_SP", idEvaluacion,  
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
                return objEjeSp.EjecSP("sgc_ActualizarEvaluacionPymeDatosBalance_SP",id, idEvaluacion,
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
                return objEjeSp.EjecSP("sgc_EliminarEvaluacionPymeDatosBalance_SP", id);
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
                return objEjeSp.EjecSP("sgc_ListarEvaluacionPymeEstado_SP");
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
                return objEjeSp.EjecSP("sgc_ListarEvaluacionPymeEstadoid_SP", idEvaluacion);
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
                return objEjeSp.EjecSP("sgc_InsertarEvaluacionPymeEstado_SP",idEvaluacion, cDescripcion, 
             nCantidad,  nPrecio,  nMonto,  idTipoDatoBalance,  lVigente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ActualizarEvaluacionPymeEstado(int id,int idEvaluacion, string cDescripcion,
            decimal nCantidad, decimal nPrecio, decimal nMonto, int idTipoDatoBalance, bool lVigente)
        {
            try
            {
                return objEjeSp.EjecSP("sgc_ActualizarEvaluacionPymeEstado_SP",id, idEvaluacion, cDescripcion,
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
                return objEjeSp.EjecSP("sgc_EliminarEvaluacionPymeEstado_SP", id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion
    }
}
