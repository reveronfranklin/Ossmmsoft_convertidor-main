namespace Convertidor.Dtos.Adm
{
    public class AdmDetalleSolicitudResponseDto
    {
        public int CodigoDetalleSolicitud { get; set; }
        public int CodigoSolicitud { get; set; }
        public decimal Cantidad { get; set; }
        public decimal CantidadComprada { get; set; }
        public decimal CantidadAnulada { get; set; }
        public int UdmId { get; set; }
        
        public string DescripcionUnidad { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal PrecioUnitario { get; set; }
        
        public decimal PrecioTotal { get; set; }
        
        public decimal? PorDescuento { get; set; }
        public decimal? MontoDescuento { get; set; }
        public int TipoImpuestoId { get; set; }
        public decimal PorImpuesto { get; set; }
        public decimal? MontoImpuesto { get; set; }
        public int CodigoPresupuesto { get; set; }
        
        public decimal? CodigoProducto { get; set; }
        
        public string  DescripcionProducto  { get; set; }
        public decimal? Total { get; set; }
        public decimal? TotalMasImpuesto { get; set; }

    }
}
