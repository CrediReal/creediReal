using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using SGA.AccesoDatos;
using SGA.Utilitarios;

namespace SGA.LogicaNegocio
{
    public class clsCNCronograma
    {
        clsADCronograma cncrgonograma= new clsADCronograma();

        public DataTable CalculaPpgFlat(double nMonDesemb, double nTasEfeMen, DateTime dFecDesemb, int nNumCuoCta, int nDiaGraCta, short nTipPerPag, int nDiaFecPag, int nNumsolicitud)
        {
            int nDiaAcumul = 0;
            DateTime dFecNewCuo = dFecDesemb.AddDays(Convert.ToDouble(nDiaGraCta));
            clsFuncionAritmetica FunAritmetic = new clsFuncionAritmetica();
            double nPeriodo = 0.0;
            if (nDiaFecPag==7)
            {
                nPeriodo = 7.5;
            }
            else
            {
                nPeriodo = nDiaFecPag;
            }
            double nTasEfeFin = FunAritmetic.RedxDefecto((((nDiaGraCta + (nNumCuoCta * nPeriodo)) / 30.00) * nTasEfeMen), 4);
            if (nTasEfeFin < nTasEfeMen)
            {
                nTasEfeFin = nTasEfeMen;
            }
            double nMonInteres = FunAritmetic.RedxDefecto(nMonDesemb * nTasEfeFin, 1);
            double nMonDevolver = FunAritmetic.RedxDefecto(nMonDesemb + nMonInteres, 1);
            double nMontoCuota = FunAritmetic.RedxExceso(nMonDevolver / nNumCuoCta, 1);
            double nMonCapCuota = FunAritmetic.RedxDefecto(nMonDesemb / nNumCuoCta, 1);
            double nMonCuoAcum = 0;
            double nMonCapAcum = 0;

            int nDiaFecAux = nDiaFecPag;

            DataTable ppg = new DataTable("dtPlanPago");
            ppg.Columns.Add("cuota", typeof(int));
            ppg.Columns.Add("fecha", typeof(DateTime));
            ppg.Columns.Add("dias", typeof(int));
            ppg.Columns.Add("dias_acu", typeof(int));
            ppg.Columns.Add("capital", typeof(double));
            ppg.Columns.Add("interes", typeof(double));
            ppg.Columns.Add("imp_cuota", typeof(double));
            ppg.Columns.Add("nIdSolicitud", typeof(int));

            //Cargando la tabla de feriados
            DataTable dtFeriado = new DataTable("dtFeriado");
            
            dtFeriado = cncrgonograma.ADdtFeriado();
            int nDiaAdicionalFeriado = 0;
            int nFeriadoAcumulado = 0;

            nFeriadoAcumulado = 0;
            for (int i = 1; i <= nNumCuoCta; i++)
            {
                DataRow fila = ppg.NewRow();
                fila["cuota"] = i;
                if (i == 1)
                {
                    nDiaAcumul = nDiaAcumul + nDiaGraCta + nDiaFecPag;
                    dFecNewCuo = dFecNewCuo.AddDays(Convert.ToDouble(nDiaAcumul - nDiaGraCta));
                    //Si frecuencia de pago es diaria y la fecha de la cuota cae en día feriado
                    if (nDiaFecPag == 1)
                    {
                        for (int j = 0; j < dtFeriado.Rows.Count; j++)
                        {
                            if (dFecNewCuo == Convert.ToDateTime(dtFeriado.Rows[j]["dferiado"]))
                            {
                                nDiaAcumul = nDiaAcumul + 1;
                                dFecNewCuo = dFecNewCuo.AddDays(Convert.ToDouble(1));
                                nDiaAdicionalFeriado = nDiaAdicionalFeriado + 1;
                                nFeriadoAcumulado++;
                            }
                        }
                    }
                    fila["fecha"] = dFecDesemb.AddDays(Convert.ToDouble(nDiaAcumul));
                    fila["dias"] = nDiaFecPag + nDiaGraCta + nDiaAdicionalFeriado;
                    fila["dias_acu"] = nDiaAcumul;
                    nDiaAdicionalFeriado = 0;
                }
                else
                {
                    nDiaAcumul = nDiaAcumul + nDiaFecPag;
                    dFecNewCuo = dFecNewCuo.AddDays(Convert.ToDouble(nDiaFecPag));
                    if (nDiaFecPag == 1)
                    {
                        for (int j = 0; j < dtFeriado.Rows.Count; j++)
                        {
                            if (dFecNewCuo == Convert.ToDateTime(dtFeriado.Rows[j]["dferiado"]))
                            {
                                nDiaAcumul = nDiaAcumul + 1;
                                dFecNewCuo = dFecNewCuo.AddDays(Convert.ToDouble(1));
                                nDiaAdicionalFeriado = nDiaAdicionalFeriado + 1;
                                nFeriadoAcumulado++;
                            }
                        }
                    }

                    fila["fecha"] = dFecNewCuo;
                    fila["dias"] = nDiaFecPag + nDiaAdicionalFeriado;
                    fila["dias_acu"] = nDiaAcumul;
                    nDiaAdicionalFeriado = 0;
                }
                if (i < nNumCuoCta)
                {
                    fila["capital"] = nMonCapCuota;
                    fila["interes"] = FunAritmetic.RedxDefecto(nMontoCuota - nMonCapCuota, 1);
                    fila["imp_cuota"] = nMontoCuota;
                    fila["nIdSolicitud"] = nNumsolicitud;
                    nMonCuoAcum = nMonCuoAcum + nMontoCuota;
                    nMonCapAcum = nMonCapAcum + nMonCapCuota;
                }
                else // Ajuste de última cuota
                {
                    fila["capital"] = FunAritmetic.RedxDefecto(nMonDesemb - nMonCapAcum, 1);
                    fila["interes"] = FunAritmetic.RedxDefecto((nMonDevolver - nMonCuoAcum) - (nMonDesemb - nMonCapAcum), 1);
                    fila["imp_cuota"] = FunAritmetic.RedxDefecto(nMonDevolver - nMonCuoAcum, 1);
                    fila["nIdSolicitud"] = nNumsolicitud;
                }
                ppg.Rows.Add(fila);
            }

            ppg.AcceptChanges();
            return ppg;


        }

        public DataTable dtCNPagoDistribuido(DataTable Ppg, double nMontoPagado, bool lPagaMora)
        {
            DataTable dtPagoDist = new DataTable("dtPagoDist");
            dtPagoDist.Columns.Add("nCapitalPag", typeof(double));
            dtPagoDist.Columns.Add("nInteresPag", typeof(double));
            dtPagoDist.Columns.Add("nMoraPag", typeof(double));
            dtPagoDist.Columns.Add("nOtrosPag", typeof(double));
            dtPagoDist.Columns.Add("nTotalPag", typeof(double));
            dtPagoDist.Rows.Add(dtPagoDist.NewRow());
            dtPagoDist.Rows[0]["nOtrosPag"] = 0.0;
            dtPagoDist.Rows[0]["nMoraPag"] = 0.0;
            dtPagoDist.Rows[0]["nInteresPag"] = 0.0;
            dtPagoDist.Rows[0]["nCapitalPag"] = 0.0;
            dtPagoDist.Rows[0]["nTotalPag"] = 0.0;

            for (int i = 0; i < Ppg.Rows.Count; i++)
            {
                

                if (lPagaMora)
                {
                    //Pagando la Mora
                    if ((Convert.ToDouble(Ppg.Rows[i]["nMora"]) - Convert.ToDouble(Ppg.Rows[i]["nMoraPagada"])) > nMontoPagado)
                    {
                        dtPagoDist.Rows[0]["nMoraPag"] = Convert.ToDouble(dtPagoDist.Rows[0]["nMoraPag"]) + nMontoPagado;
                        Ppg.Rows[i]["nMoraPagada"] = nMontoPagado;
                        nMontoPagado = 0.00;
                        Ppg.Rows[i]["nInteresPagado"] = 0.00;
                        Ppg.Rows[i]["nCapitalPagado"] = 0.00;
                        Ppg.Rows[i]["dFechaPago"] = DateTime.Today; // Debe guardar la fecha del sistema
                        break;
                    }
                    else
                    {
                        nMontoPagado = nMontoPagado - (Convert.ToDouble(Ppg.Rows[i]["nMora"]) - Convert.ToDouble(Ppg.Rows[i]["nMoraPagada"]));
                        dtPagoDist.Rows[0]["nMoraPag"] = Convert.ToDouble(dtPagoDist.Rows[0]["nMoraPag"]) +
                                                         (Convert.ToDouble(Ppg.Rows[i]["nMora"]) - Convert.ToDouble(Ppg.Rows[i]["nMoraPagada"]));
                        Ppg.Rows[i]["nMoraPagada"] = Convert.ToDouble(Ppg.Rows[i]["nMora"]) - Convert.ToDouble(Ppg.Rows[i]["nMoraPagada"]);
                        nMontoPagado = Math.Round(nMontoPagado, 2);
                    }
                }

                

                //Pagando el Interés
                if ((Convert.ToDouble(Ppg.Rows[i]["nInteres"]) - Convert.ToDouble(Ppg.Rows[i]["nInteresPagado"])) > nMontoPagado)
                {
                    dtPagoDist.Rows[0]["nInteresPag"] = Convert.ToDouble(dtPagoDist.Rows[0]["nInteresPag"]) + nMontoPagado;
                    Ppg.Rows[i]["nInteresPagado"] = nMontoPagado;
                    nMontoPagado = 0.00;
                    Ppg.Rows[i]["nCapitalPagado"] = 0.00;
                    Ppg.Rows[i]["dFechaPago"] = DateTime.Today; // Debe guardar la fecha del sistema
                    break;
                }
                else
                {
                    nMontoPagado = nMontoPagado - (Convert.ToDouble(Ppg.Rows[i]["nInteres"]) - Convert.ToDouble(Ppg.Rows[i]["nInteresPagado"]));
                    dtPagoDist.Rows[0]["nInteresPag"] = Convert.ToDouble(dtPagoDist.Rows[0]["nInteresPag"]) +
                                                     (Convert.ToDouble(Ppg.Rows[i]["nInteres"]) - Convert.ToDouble(Ppg.Rows[i]["nInteresPagado"]));
                    Ppg.Rows[i]["nInteresPagado"] = Convert.ToDouble(Ppg.Rows[i]["nInteres"]) - Convert.ToDouble(Ppg.Rows[i]["nInteresPagado"]);
                    nMontoPagado = Math.Round(nMontoPagado, 2);
                }

                //Pagando el Capital
                if ((Convert.ToDouble(Ppg.Rows[i]["nCapital"]) - Convert.ToDouble(Ppg.Rows[i]["nCapitalPagado"])) > nMontoPagado)
                {
                    dtPagoDist.Rows[0]["nCapitalPag"] = Convert.ToDouble(dtPagoDist.Rows[0]["nCapitalPag"]) + nMontoPagado;
                    Ppg.Rows[i]["nCapitalPagado"] = nMontoPagado;
                    nMontoPagado = 0.00;
                    Ppg.Rows[i]["dFechaPago"] = DateTime.Today; // Debe guardar la fecha del sistema
                    break;
                }
                else
                {
                    nMontoPagado = nMontoPagado - (Convert.ToDouble(Ppg.Rows[i]["nCapital"]) - Convert.ToDouble(Ppg.Rows[i]["nCapitalPagado"]));
                    dtPagoDist.Rows[0]["nCapitalPag"] = Convert.ToDouble(dtPagoDist.Rows[0]["nCapitalPag"]) +
                                                     (Convert.ToDouble(Ppg.Rows[i]["nCapital"]) - Convert.ToDouble(Ppg.Rows[i]["nCapitalPagado"]));
                    Ppg.Rows[i]["nCapitalPagado"] = Convert.ToDouble(Ppg.Rows[i]["nCapital"]) - Convert.ToDouble(Ppg.Rows[i]["nCapitalPagado"]);
                    nMontoPagado = Math.Round(nMontoPagado, 2);
                }

                // Pagando Otros
                if ((Convert.ToDouble(Ppg.Rows[i]["nOtros"]) - Convert.ToDouble(Ppg.Rows[i]["nOtrosPagado"])) > nMontoPagado)
                {
                    dtPagoDist.Rows[0]["nOtrosPag"] = Convert.ToDouble(dtPagoDist.Rows[0]["nOtrosPag"]) + nMontoPagado;
                    Ppg.Rows[i]["nOtrosPagado"] = nMontoPagado;
                    nMontoPagado = 0.00;
                    Ppg.Rows[i]["nMoraPagada"] = 0.00;
                    Ppg.Rows[i]["nInteresPagado"] = 0.00;
                    Ppg.Rows[i]["nCapitalPagado"] = 0.00;
                    Ppg.Rows[i]["dFechaPago"] = DateTime.Today; // Debe guardar la fecha del sistema
                    break;
                }
                else
                {
                    nMontoPagado = nMontoPagado - (Convert.ToDouble(Ppg.Rows[i]["nOtros"]) - Convert.ToDouble(Ppg.Rows[i]["nOtrosPagado"]));
                    dtPagoDist.Rows[0]["nOtrosPag"] = Convert.ToDouble(dtPagoDist.Rows[0]["nOtrosPag"]) +
                                                     (Convert.ToDouble(Ppg.Rows[i]["nOtros"]) - Convert.ToDouble(Ppg.Rows[i]["nOtrosPagado"]));
                    Ppg.Rows[i]["nOtrosPagado"] = Convert.ToDouble(Ppg.Rows[i]["nOtros"]) - Convert.ToDouble(Ppg.Rows[i]["nOtrosPagado"]);
                    nMontoPagado = Math.Round(nMontoPagado, 2);
                }
                
                Ppg.Rows[i]["dFechaPago"] = DateTime.Today; // Debe guardar la fecha del sistema
            }

            dtPagoDist.Rows[0]["nTotalPag"] = Convert.ToDouble(dtPagoDist.Rows[0]["nCapitalPag"]) +
                                                   Convert.ToDouble(dtPagoDist.Rows[0]["nInteresPag"]) +
                                                   Convert.ToDouble(dtPagoDist.Rows[0]["nMoraPag"]) +
                                                   Convert.ToDouble(dtPagoDist.Rows[0]["nOtrosPag"]);
            Ppg.AcceptChanges();
            dtPagoDist.AcceptChanges();
            return dtPagoDist;
        }

        public DataTable RegistrarCronogramaCanasta(String xCronograma, DateTime dFecdesemb, int idUsuario, decimal nMonto, int nCuotas, decimal nTasa)
        {
            return cncrgonograma.RegistrarCronogramaCanasta(xCronograma, dFecdesemb, idUsuario, nMonto, nCuotas, nTasa);
        }

        public DataTable BuscarCanasta(int idCli, int nPeriodo)
        {
            return cncrgonograma.BuscarCanasta(idCli, nPeriodo);
        }

        public DataTable CronogramaCanasta(int idCli, int nPeriodo)
        {
            return cncrgonograma.CronogramaCanasta(idCli, nPeriodo);
        }

    }
}
