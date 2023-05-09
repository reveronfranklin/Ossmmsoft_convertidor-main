using System;
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
        public string PresupuestadoFormat { get { return Presupuestado.ToString("C2"); } }       
        public string AsignacionFormat { get { return Asignacion.ToString("C2"); } }
        public string ModificadoFormat { get { return Modificado.ToString("C2"); } }
        public string BloqueadoFormat { get { return Bloqueado.ToString("C2"); } }
        public string ComprometidoFormat { get { return Comprometido.ToString("C2"); } }
        public string CausadoFormat { get { return Causado.ToString("C2"); } }
        public string PagadoFormat { get { return Pagado.ToString("C2"); } }


    }
}

