namespace Convertidor.Dtos.Adm
{
    public class AdmBeneficiariosOpUpdateDto
    {
        public int CodigoBeneficiarioOp { get; set; }
        public int CodigoOrdenPago { get; set; }
        public int CodigoProveedor { get; set; }
        public decimal Monto { get; set; }
        public decimal MontoPagado { get; set; }
        public decimal MontoAnulado { get; set; }
        public int CodigoPresupuesto { get; set; }
    }
    public class AdmBeneficiariosOpUpdateMontoDto
    {
        public int CodigoBeneficiarioOp { get; set; }
       
        public decimal Monto { get; set; }
       
    }
}
