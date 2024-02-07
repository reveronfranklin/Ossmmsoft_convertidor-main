namespace Convertidor.Dtos.Adm
{
    public class AdmReintegrosUpdateDto
    {
        public int CodigoReintegro { get; set; }
        public int ANO { get; set; }
        public int CodigoCompromiso { get; set; }
        public DateTime FechaReintegro { get; set; }
        public string NumeroReintegro { get; set; } = string.Empty;
        public int CodigoCuentaBanco { get; set; }
        public string NumeroDeposito { get; set; } = string.Empty;
        public DateTime FechaDeposito { get; set; }
        public string Motivo { get; set; } = string.Empty;
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int CodigoPresupuesto { get; set; }
        public int OrigenCompromisoId { get; set; }
    }
}
