namespace Convertidor.Dtos.Adm
{
    public class AdmDetalleSolCompromisoUpdateDto
    {
        public int CodigoDetalleSolicitud { get; set; }
        public int CodigoPucSolicitud { get; set; }
        public int Cantidad { get; set; }
        public int UdmId { get; set; }
        public string Denominacion { get; set; } = string.Empty;
        public decimal PrecioUnitario { get; set; }
        public int TipoImpuestoId { get; set; }
        public int PORIMPUESTO { get; set; }
        public int CantidadAprobada { get; set; }
        public int CantidadAnulada { get; set; }
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
        public int CodigoPresupuesto { get; set; }
    }
}
