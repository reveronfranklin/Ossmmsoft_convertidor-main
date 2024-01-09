namespace Convertidor.Dtos.Presupuesto
{
	public class ListPreMtrUnidadEjecutora
	{
        public int Id { get; set; }

        public int CodigoPresupuesto { get; set; }

        public int CodigoIcp { get; set; }

        public string CodigoIcpConcat { get; set; } = string.Empty;

        public string UnidadEjecutora { get; set; } = string.Empty;

        public string Dercripcion { get { return $"{ CodigoIcp} {UnidadEjecutora} "; } }
    }
}

