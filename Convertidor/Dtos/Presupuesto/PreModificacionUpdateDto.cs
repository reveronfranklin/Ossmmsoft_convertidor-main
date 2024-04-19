namespace Convertidor.Dtos.Presupuesto
{
    public class PreModificacionUpdateDto
    {
        public int CodigoModificacion { get; set; }
        public int CodigoSolModificacion { get; set; }
        public int TipoModificacionId { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int Ano { get; set; }
        public string NumeroModificacion { get; set; } = string.Empty;
        public string NoResAct { get; set; } = string.Empty;
        public string CodigoOficio { get; set; } = string.Empty;
        public int CodigoSolicitante { get; set; }
        public string Motivo { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
        public int CodigoPresupuesto { get; set; }
    }
}
