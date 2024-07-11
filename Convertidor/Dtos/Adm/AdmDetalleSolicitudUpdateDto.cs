namespace Convertidor.Dtos.Adm
{
    public class AdmDetalleSolicitudUpdateDto
    {
        public int CodigoDetalleSolicitud { get; set; }
        public int CodigoSolicitud { get; set; }
        public decimal Cantidad { get; set; }
        public int UdmId { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public decimal PrecioUnitario { get; set; }
        public int TipoImpuestoId { get; set; }
        public int CodigoProducto { get; set; }
        
    }
}


