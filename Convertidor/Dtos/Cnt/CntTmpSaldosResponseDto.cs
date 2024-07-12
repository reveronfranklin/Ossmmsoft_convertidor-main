namespace Convertidor.Dtos.Cnt
{
    public class CntTmpSaldosResponseDto
    {
        public int CodigoTmpSaldo { get; set; }
        public int CodigoPeriodo { get; set; }
        public int CodigoMayor { get; set; }
        public int CodigoAuxiliar { get; set; }
        public int SaldoInicial { get; set; }
        public decimal Debitos { get; set; }
        public decimal Creditos { get; set; }
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
    }
}
