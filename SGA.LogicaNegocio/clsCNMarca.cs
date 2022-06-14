using System;
using System.Collections.Generic;
using System.Linq;
using SGA.AccesoDatos;
using SGA.ENTIDADES;

namespace SGA.LogicaNegocio
{
    public class clsCNMarca
    {
        public List<clsMarca> ListarMarcas(int id, string cMarca, bool lSoloVig = false)
        {
            try
            {
                return new clsADMarca().ListarMarcas(id, cMarca,lSoloVig);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public clsMarca GetMarcaById(int id)
        {
            try
            {
                return ListarMarcas(id, string.Empty).First();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<clsMarca> GetMarcaByName(string cMarca)
        {
            try
            {
                return ListarMarcas(0, cMarca);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } 

        public clsDBResp SaveMarca(clsMarca objMarca)
        {
            try
            {
                return new clsADMarca().SaveMarca(objMarca);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
