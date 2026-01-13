namespace Convertidor.Dtos.Cnt
{
    public class CntReversoConciliacionResponseDto
    {
        public int CodigoHistConciliacion { get; set; }
        public int CodigoConciliacion { get; set; }
        public int CodigoPeriodo { get; set; }
        public int CodigoCuentaBanco { get; set; }
        public int CodigoDetalleLibro { get; set; }
        public int CodigoDetalleEdoCta { get; set; }
        public DateTime Fecha { get; set; }
        public string FechaString { get; set; }
        public FechaDto FechaObj { get; set; }
        public string Numero { get; set; } = string.Empty;
        public int Monto { get; set; }
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
    }
}
