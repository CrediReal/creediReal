using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Helper.Conector
{
    public class clsGENEjeSP
    {
        #region Listas
        Dictionary<string, SqlDbType> dicSQLDBType = new Dictionary<string, SqlDbType>
            {   { "bit", SqlDbType.Bit},
                { "bigint", SqlDbType.BigInt}, 
                { "binary", SqlDbType.Binary}, 
                { "char", SqlDbType.Char},
                { "date", SqlDbType.Date}, 
                { "datetime", SqlDbType.DateTime},
                { "datetime2", SqlDbType.DateTime2},
                { "datetimeoffset", SqlDbType.DateTimeOffset},
                { "decimal", SqlDbType.Decimal},
                { "numeric", SqlDbType.Decimal},
                { "float", SqlDbType.Float},
                { "image", SqlDbType.Image},                 
                { "int", SqlDbType.Int}, 
                { "money", SqlDbType.Money},                 
                { "nchar", SqlDbType.NChar},                
                { "nvarchar", SqlDbType.NVarChar},
                { "ntext", SqlDbType.NText},
                { "real", SqlDbType.Real},
                { "smalldatetime", SqlDbType.SmallDateTime}, 
                { "smallint", SqlDbType.SmallInt},
                { "time", SqlDbType.Time}, 
                { "timestamp", SqlDbType.Timestamp}, 
                { "tinyint", SqlDbType.TinyInt}, 
                { "uniqueidentifier", SqlDbType.UniqueIdentifier}, 
                { "text", SqlDbType.Text},
                { "varbinary", SqlDbType.VarBinary}, 
                { "varchar", SqlDbType.VarChar},
                { "xml", SqlDbType.Xml} };

        Dictionary<string, ParameterDirection> dicParamDir = new Dictionary<string, ParameterDirection> 
            {   { "IN", ParameterDirection.Input}, 
                { "INOUT", ParameterDirection.Output}};
        #endregion

        private List<clsGENParams> lstParametros = new List<clsGENParams>();

        /// <summary>
        /// Carga los parametros de un Store Procedure determinado
        /// </summary>
        /// <param name="NombreSP">Nombre del Store Proccedure</param>
        /// <param name="con">Conexión a la base de datos</param>
        /// <returns>Listado de parametros del Store Procedure</returns>
        public List<clsGENParams> CargaParametroSP(string NombreSP, SqlConnection con)
        {
            List<clsGENParams> lstParamsSP = new List<clsGENParams>();
            using (SqlCommand lstParams = new SqlCommand())
            {
                lstParams.Connection = con;

                //lstParams.CommandText = "SGA_LstParamsxSP_SP";
                lstParams.CommandText = "GEN_LstParamsxSP_SP";
                lstParams.CommandType = CommandType.StoredProcedure;
                //lstParams.Parameters.Add(new SqlParameter("@cNombreSP", NombreSP));
                lstParams.Parameters.Add(new SqlParameter("@x_cNombreSP", NombreSP));
                SqlDataReader parametersReader = lstParams.ExecuteReader();

                {
                    while (parametersReader.Read())
                    {
                        clsGENParams ParamSP = new clsGENParams();
                        SqlParameter Parametro = new SqlParameter();
                        string paramInfo = string.Format(
                          "Orden: {0} NombreParametro: {1} Tipo: {2} Longitud: {3} Modo: {4} ",
                          parametersReader["ORDINAL_POSITION"],
                          parametersReader["PARAMETER_NAME"],
                          parametersReader["DATA_TYPE"],
                          parametersReader["CHARACTER_MAXIMUM_LENGTH"],
                          parametersReader["PARAMETER_MODE"]);

                        ParamSP.nPosicion = Convert.ToInt32(parametersReader["ORDINAL_POSITION"]);
                        ParamSP.Tipodatos = parametersReader["DATA_TYPE"].ToString();
                        Parametro.ParameterName = parametersReader["PARAMETER_NAME"].ToString();
                        Parametro.SqlDbType = dicSQLDBType[parametersReader["DATA_TYPE"].ToString()];
                        Parametro.Direction = dicParamDir[parametersReader["PARAMETER_MODE"].ToString()];
                        if (parametersReader["DATA_TYPE"].ToString() == "char")
                            Parametro.Size = Convert.ToInt32(parametersReader["CHARACTER_MAXIMUM_LENGTH"]);
                        ParamSP.Parametro = Parametro;
                        lstParamsSP.Add(ParamSP);
                    }
                }
                lstParams.Dispose();
                parametersReader.Close();
                return lstParamsSP;
            }
        }

        /// <summary>
        /// Ejecuta un Store Procedure retornando una estructura tabla de la bd
        /// </summary>
        /// <param name="NombreSP">Nombre del Store Proccedure</param>
        /// <param name="parametros">Parametros del Store Procedure</param>
        /// <returns>Tabla seleccionada por el Store Procedure</returns>
        public DataTable EjecSP(string NombreSP, params object[] parametros)
        {

            DataTable dt = new DataTable();
            using (SqlConnection Conexion = new SqlConnection())
            {
                try
                {
                    Conexion.ConnectionString = ConfigurationManager.ConnectionStrings["conexionSGA"].ToString();
                    Conexion.Open();
                    lstParametros = CargaParametroSP(NombreSP, Conexion);
                    SqlCommand ComandoSP = new SqlCommand();
                    ComandoSP.CommandText = NombreSP;
                    ComandoSP.CommandType = CommandType.StoredProcedure;
                    ComandoSP.Connection = Conexion;
                    for (int i = 0; i < lstParametros.Count; i++)
                    {
                        if (lstParametros[i].Parametro.SqlDbType == SqlDbType.Xml)
                            lstParametros[i].Parametro.Value = new SqlXml(new XmlTextReader(parametros[i].ToString(), XmlNodeType.Document, null));
                        else
                            lstParametros[i].Parametro.Value = parametros[i];
                        ComandoSP.Parameters.Add(lstParametros[i].Parametro);
                    }
                    SqlDataReader reader = ComandoSP.ExecuteReader();
                    dt.Load(reader);
                    reader.Close();
                    Conexion.Close();
                    Conexion.Dispose();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            return dt;
        }

        /// <summary>
        /// Ejecuta un Store Procedure retornando un conjunto de tablas de la bd llenados en un dataset 
        /// </summary>
        /// <param name="NombreSP">Nombre del Store Proccedure</param>
        /// <param name="parametros">Parametros del Store Procedure</param>
        /// <returns>DataSrt con las tablas seleccionada por el Store Procedure</returns>
        public DataSet DSEjecSP(string NombreSP, params object[] parametros)
        {
            DataSet ds = new DataSet();
            using (SqlConnection Conexion = new SqlConnection())
            {
                try
                {
                    if (string.IsNullOrEmpty(ConfigurationManager.ConnectionStrings["conexionSGA"].ToString()))
                    {
                        return new DataSet();
                    }

                    Conexion.ConnectionString = ConfigurationManager.ConnectionStrings["conexionSGA"].ToString();
                    Conexion.Open();
                    lstParametros = CargaParametroSP(NombreSP, Conexion);
                    SqlCommand ComandoSP = new SqlCommand();
                    ComandoSP.CommandText = NombreSP;
                    ComandoSP.CommandType = CommandType.StoredProcedure;
                    ComandoSP.Connection = Conexion;
                    for (int i = 0; i < lstParametros.Count; i++)
                    {
                        if (lstParametros[i].Parametro.SqlDbType == SqlDbType.Xml)
                            lstParametros[i].Parametro.Value = new SqlXml(new XmlTextReader(parametros[i].ToString(), XmlNodeType.Document, null));
                        else
                            lstParametros[i].Parametro.Value = parametros[i];
                        ComandoSP.Parameters.Add(lstParametros[i].Parametro);
                    }
                    SqlDataAdapter adapter = new SqlDataAdapter(ComandoSP);
                    adapter.Fill(ds);
                    adapter.Dispose();
                    Conexion.Close();
                    Conexion.Dispose();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            return ds;
        }

        /// <summary>
        /// Ejecuta un Store Procedure con parametros de salida
        /// </summary>
        /// <param name="NombreSP">Nombre del Store Procedure</param>
        /// <param name="parametros">Parametros del Store Procedure</param>
        /// <returns>Kistado de parametros de salida(Output)</returns>
        public List<clsGENParams> EjecSPOut(string NombreSP, params object[] parametros)
        {


            if (string.IsNullOrEmpty(ConfigurationManager.ConnectionStrings["conexionSGA"].ToString()))
            {
                return new List<clsGENParams>();
            }

            using (SqlConnection Conexion = new SqlConnection())
            {
                Conexion.ConnectionString = ConfigurationManager.ConnectionStrings["conexionSGA"].ToString();
                Conexion.Open();
                lstParametros = CargaParametroSP(NombreSP, Conexion);
                SqlCommand ComandoSP = new SqlCommand();
                ComandoSP.CommandText = NombreSP;
                ComandoSP.CommandType = CommandType.StoredProcedure;
                ComandoSP.Connection = Conexion;
                for (int i = 0; i < lstParametros.Count; i++)
                {
                    if (lstParametros[i].Parametro.SqlDbType == SqlDbType.Xml)
                        lstParametros[i].Parametro.Value = new SqlXml(new XmlTextReader(parametros[i].ToString(), XmlNodeType.Document, null));
                    else
                        lstParametros[i].Parametro.Value = parametros[i];
                    ComandoSP.Parameters.Add(lstParametros[i].Parametro);
                }
                ComandoSP.ExecuteNonQuery();
                Conexion.Close();
            }
            lstParametros.RemoveAll(x => x.Parametro.Direction != ParameterDirection.Output);
            return lstParametros;
        }

        /// <summary>
        /// Ejecuta un Store Procedure que no retorna ningun tipo de tabla y/o parametro
        /// </summary>
        /// <param name="NombreSP">Nombre del Store Procedure</param>
        /// <param name="parametros">Parametros del Store Procedure</param>
        public void EjecSPAlm(string NombreSP, params object[] parametros)
        {


            if (string.IsNullOrEmpty(ConfigurationManager.ConnectionStrings["conexionSGA"].ToString()))
            {
                return;
            }

            using (SqlConnection Conexion = new SqlConnection())
            {
                Conexion.ConnectionString = ConfigurationManager.ConnectionStrings["conexionSGA"].ToString();
                Conexion.Open();
                lstParametros = CargaParametroSP(NombreSP, Conexion);
                SqlCommand ComandoSP = new SqlCommand();
                ComandoSP.CommandText = NombreSP;
                ComandoSP.CommandType = CommandType.StoredProcedure;
                ComandoSP.Connection = Conexion;
                for (int i = 0; i < lstParametros.Count; i++)
                {
                    if (lstParametros[i].Parametro.SqlDbType == SqlDbType.Xml)
                        lstParametros[i].Parametro.Value = new SqlXml(new XmlTextReader(parametros[i].ToString(), XmlNodeType.Document, null));
                    else
                        lstParametros[i].Parametro.Value = parametros[i];
                    ComandoSP.Parameters.Add(lstParametros[i].Parametro);
                }
                ComandoSP.ExecuteNonQuery();
                Conexion.Close();
            }
        }

        private DataTable estructuraVacia()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("col1");
            dt.Columns.Add("col2");
            dt.Columns.Add("col3");
            dt.Columns.Add("col4");
            dt.Columns.Add("col5");
            return dt;
        }
    }
}
