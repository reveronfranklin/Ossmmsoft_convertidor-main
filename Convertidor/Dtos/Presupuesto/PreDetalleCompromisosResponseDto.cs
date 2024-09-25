namespace Convertidor.Dtos.Presupuesto
{
    public class PreDetalleCompromisosResponseDto
    {
        public int CodigoDetalleCompromiso { get; set; }
        public int CodigoCompromiso { get; set; }
        public int CodigoDetalleSolicitud { get; set; }
        public decimal Cantidad { get; set; }
        public decimal CantidadAnulada { get; set; }
        public int UdmId { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public decimal PrecioUnitario { get; set; }
        public decimal PorDescuento { get; set; }
        public decimal MontoDescuento { get; set; }
        public int TipoImpuestoId { get; set; }
        public decimal PorImpuesto { get; set; }
        public decimal MontoImpuesto { get; set; }
        public int CodigoPresupuesto { get; set; }
        public decimal Total { get; set; }
        public decimal TotalMasImpuesto { get; set; }
        
    }
}
