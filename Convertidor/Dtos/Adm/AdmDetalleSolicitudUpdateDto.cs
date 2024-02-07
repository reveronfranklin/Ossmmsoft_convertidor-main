namespace Convertidor.Dtos.Adm
{
    public class AdmDetalleSolicitudUpdateDto
    {
        public int CodigoDetalleSolicitud { get; set; }
        public int CodigoSolicitud { get; set; }
        public int Cantidad { get; set; }
        public int CantidadComprada { get; set; }
        public int CantidadAnulada { get; set; }
        public int UdmId { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public int PrecioUnitario { get; set; }
        public int PorDescuento { get; set; }
        public int MontoDescuento { get; set; }
        public int TipoImpuestoId { get; set; }
        public int PorImpuesto { get; set; }
        public int MontoImpuesto { get; set; }
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
        public int CodigoPresupuesto { get; set; }

    }
}
