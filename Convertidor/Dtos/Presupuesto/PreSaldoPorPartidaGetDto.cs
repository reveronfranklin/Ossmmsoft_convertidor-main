using System;
using System.Globalization;

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

                return ConvertMoneda(Presupuestado);

            }
        }

        public string AsignacionFormat
        {
            get
            {
                return ConvertMoneda(Asignacion);
            }
        }
        public string ModificadoFormat
        {
            get
            {

       

                return ConvertMoneda(Modificado);
            }
        }
        public string BloqueadoFormat
        {
            get
            {
                return ConvertMoneda(Bloqueado);
            }
        }
        public string ComprometidoFormat
        {
            get
            {

                return ConvertMoneda(Comprometido);
            }
        }
        public string CausadoFormat
        {
            get
            {

                return ConvertMoneda(Causado);
            }
        }

        public string PagadoFormat
        {
            get
            {

               return ConvertMoneda(Pagado);
            }
        }

        public string ConvertMoneda(decimal value)
        {
            string literal = "";
            if (value == 0)
            {
                literal = "Bs. 0,0";
            }
            else
            {
                literal = "Bs. " + value.ToString("#,#", CultureInfo.InvariantCulture);
            }


            return literal;
        }





    }
}

