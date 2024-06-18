namespace Convertidor.Dtos.Presupuesto
{
	public class PreCargosUpdateDto
	{
        public int CodigoCargo { get; set; }
        public int TipoPersonalId { get; set; }
        public int TipoCargoId { get; set; } 
        public string Denominacion { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public int Grado { get; set; }
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
        public int CodigoPresupuesto { get; set; }
    }
}

