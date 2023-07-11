using System;
using System.Globalization;
using Convertidor.Utility;

namespace Convertidor.Dtos.Presupuesto
{
	public class PreSaldoPorPartidaGetDto
	{
        public int Id { get; set; }
        public int CodigoPresupuesto { get; set; }
        public string CodigoPucConcat { get; set; } = string.Empty;
        public string DescripcionFinanciado { get; set; } = string.Empty;
        public decimal Presupuestado { get; set; }
        public decimal Asignacion { get; set; }
        public decimal Modificado { get; set; }
        public decimal Bloqueado { get; set; }
        public decimal Comprometido { get; set; }
        public decimal Causado { get; set; }
        public decimal Pagado { get; set; }

        public string PresupuestadoFormat
        {
            get
            {

                return ConvertidorMoneda.ConvertMoneda(Presupuestado);

            }
        }

        public string AsignacionFormat
        {
            get
            {
                return ConvertidorMoneda.ConvertMoneda(Asignacion);
            }
        }
        public string ModificadoFormat
        {
            get
            {

       

                return ConvertidorMoneda.ConvertMoneda(Modificado);
            }
        }
        public string BloqueadoFormat
        {
            get
            {
                return ConvertidorMoneda.ConvertMoneda(Bloqueado);
            }
        }
        public string ComprometidoFormat
        {
            get
            {

                return ConvertidorMoneda.ConvertMoneda(Comprometido);
            }
        }
        public string CausadoFormat
        {
            get
            {

                return ConvertidorMoneda.ConvertMoneda(Causado);
            }
        }

        public string PagadoFormat
        {
            get
            {

               return ConvertidorMoneda.ConvertMoneda(Pagado);
            }
        }

     





    }
}

