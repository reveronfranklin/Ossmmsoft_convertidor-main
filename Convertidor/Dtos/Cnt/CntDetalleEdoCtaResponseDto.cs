namespace Convertidor.Dtos.Cnt
{
    public class CntDetalleEdoCtaResponseDto
    {
        public int CodigoDetalleEdoCta { get; set; }
        public int CodigoEstadoCuenta { get; set; }
        public int TipoTransaccionId { get; set; }
        public string NumeroTransaccion { get; set; } = string.Empty;
        public DateTime FechaTransaccion { get; set; }
        public string FechaTransaccionString { get; set; }
        public FechaDto FechaTransaccionObj { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public int Monto { get; set; }
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;

    }
}
