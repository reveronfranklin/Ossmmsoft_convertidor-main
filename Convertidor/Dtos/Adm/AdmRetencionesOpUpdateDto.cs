namespace Convertidor.Dtos.Adm
{
    public class AdmRetencionesOpUpdateDto
    {
        public int CodigoRetencionOp { get; set; }
        public int CodigoOrdenPago { get; set; }
        public int TipoRetencionId { get; set; }
        public int CodigoRetencion { get; set; }
        public decimal PorRetencion { get; set; }
        public decimal MontoRetencion { get; set; }
        public decimal BaseImponible { get; set; }
        public int CodigoPresupuesto { get; set; }
    }
}
