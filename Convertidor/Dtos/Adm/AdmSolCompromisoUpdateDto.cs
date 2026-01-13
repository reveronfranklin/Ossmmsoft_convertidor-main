namespace Convertidor.Dtos.Adm
{
    public class AdmSolCompromisoUpdateDto
    {
        public int CodigoSolCompromiso { get; set; }
        public int TipoSolCompromisoId { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public string NumeroSolicitud { get; set; } = string.Empty;
        public int CodigoSolicitante { get; set; }
        public int CodigoProveedor { get; set; }
        public string Motivo { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int CodigoPresupuesto { get; set; }
        public int Ano { get; set; }
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
    }
}
