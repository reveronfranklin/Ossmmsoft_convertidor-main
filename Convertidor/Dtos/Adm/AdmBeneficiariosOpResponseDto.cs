namespace Convertidor.Dtos.Adm
{
    public class AdmBeneficiariosOpResponseDto
    {
        public int CodigoBeneficiarioOp { get; set; }
        public int CodigoOrdenPago { get; set; }
        public int CodigoProveedor { get; set; }
        public string NombreProveedor { get; set; }
        public decimal Monto { get; set; }
        public decimal MontoPagado { get; set; }
        public decimal MontoAnulado { get; set; }
        public int CodigoPresupuesto { get; set; }
        
    }
}
