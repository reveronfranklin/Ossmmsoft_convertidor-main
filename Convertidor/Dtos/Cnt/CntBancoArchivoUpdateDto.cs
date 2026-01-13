namespace Convertidor.Dtos.Cnt
{
    public class CntBancoArchivoUpdateDto
    {
        public int CodigoBancoArchivo { get; set; }
        public int CodigoBancoArchivoControl { get; set; }
        public string NumeroBanco { get; set; } = string.Empty;
        public string NumeroCuenta { get; set; } = string.Empty;
        public DateTime FechaTransaccion { get; set; } 
        public string NumeroTransaccion { get; set; } = string.Empty;
        public int TipoTransaccionId { get; set; }
        public string TipoTransaccion { get; set; } = string.Empty;
        public string DescripcionTransaccion { get; set; } = string.Empty;
        public int MontoTransaccion { get; set; }
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
        public int CodigoDetalleEdoCta { get; set; }
    }
}
