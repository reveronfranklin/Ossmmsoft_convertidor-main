namespace Convertidor.Dtos.Adm
{
    public class AdmValContratoResponseDto
    {
        public int CodigoValContrato { get; set; }
        public int CodigoContrato { get; set; }
        public string TipoValuacion { get; set; }
        public DateTime FechaValuacion { get; set; }
        public string FechaValuacionString { get; set; }
        public FechaDto FechaValuacionObj { get; set; }
        public string NumeroValuacion { get; set; } = string.Empty;
        public DateTime FechaIni { get; set; }
        public string FechaIniString { get; set; }
        public FechaDto FechaIniObj { get; set; }
        public DateTime FechaFin { get; set; }
        public string FechaFinString { get; set; }
        public FechaDto FechaFinObj { get; set; }
        public DateTime FechaAprobacion { get; set; }
        public string FechaAprobacionString { get; set; }
        public FechaDto FechaAprobacionObj { get; set; }
        public string NumeroAprobacion { get; set; }
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int CodigoPresupuesto { get; set; }
        public int Monto { get; set; }
        public int MontoCausado { get; set; }
        public int MontoAnulado { get; set; }
    }
}
