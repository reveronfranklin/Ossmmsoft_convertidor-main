namespace Convertidor.Dtos.Presupuesto
{
    public class PreCompromisosResponseDto
    {
        public int CodigoCompromiso { get; set; }
        public int Ano { get; set; }
        public int CodigoSolicitud { get; set; }
        public string NumeroSolicitud { get; set; } = string.Empty;
        public string NumeroCompromiso { get; set; } = string.Empty;
        public DateTime FechaCompromiso { get; set; }
        public string FechaCompromisoString { get; set; }
        public FechaDto FechaCompromisoObj { get; set; }
        public int CodigoProveedor { get; set; }
        public string NombreProveedor { get; set; }
        public int CodigoDirEntrega { get; set; }
        public string Motivo { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string DescripcionStatus { get; set; } = string.Empty;
        public int CodigoPresupuesto { get; set; }
        public int OrigenId { get; set; }
        public string OrigenDescripcion { get; set; }
        
        public decimal Monto { get; set; }
       

    }
}
