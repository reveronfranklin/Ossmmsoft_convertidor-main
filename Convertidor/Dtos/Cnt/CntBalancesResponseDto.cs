namespace Convertidor.Dtos.Cnt
{
    public class CntBalancesResponseDto
    {
        public int CodigoBalance { get; set; }
        public string NumeroBalance { get; set; } = string.Empty;
        public string Denominacion { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
        public int CodigoRubro { get; set; }
    }
}
