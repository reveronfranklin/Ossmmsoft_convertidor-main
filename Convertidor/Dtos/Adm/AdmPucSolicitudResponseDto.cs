namespace Convertidor.Dtos.Adm
{
    public class AdmPucSolicitudResponseDto
    {
        public int CodigoPucSolicitud { get; set; }
        public int CodigoDetalleSolicitud { get; set; }
        public int CodigoSolicitud { get; set; }
        public int CodigoSaldo { get; set; }
        public int CodigoIcp { get; set; }
        public string IcpConcat { get; set; } = string.Empty;
        public int CodigoPuc { get; set; }
        public string PucConcat { get; set; } = string.Empty;
        public int FinanciadoId { get; set; }
        
        public string DescripcionFinanciado { get; set; } = string.Empty;
        public int CodigoFinanciado { get; set; }
        public int Monto { get; set; }
        public int MontoComprometido { get; set; }
        public int MontoAnulado { get; set; }
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty; 
        public int CodigoPresupuesto { get; set; }
    }
}
