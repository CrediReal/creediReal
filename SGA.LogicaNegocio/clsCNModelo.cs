using System.Collections.Generic;
using SGA.AccesoDatos;
using SGA.ENTIDADES;

namespace SGA.LogicaNegocio
{
    public class clsCNModelo
    {
        public List<clsModelo> GetModelos(int idMarca,bool lSoloVig = false)
        {
            return new clsADModelo().GetModelos(idMarca, lSoloVig);
        }

        public clsDBResp SaveModelo(clsModelo objModelo)
        {
            return new clsADModelo().SaveModelo(objModelo);
        }
    }
}
