namespace Convertidor.Dtos.Adm.ReporteSolicitudCompromiso
{
    public class ReporteSolicitudCompromisoDto
    {
        public SolicitudcompromisoDto SolicitudCompromiso { get; set; }
        public List<DetalleSolicitudcompromisoDto> DetalleSolicitud { get; set; }
        public List<PucSolCompromisoDto> PucSolicitudCompromiso { get; set; }

    }

    public class SolicitudcompromisoDto 
    {
        public int CodigoSolCompromiso { get; set; }
        public int TipoSolCompromisoId { get; set; }
        public int DescripcionTipoCompromiso { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public string FechaSolicitudString { get; set; }
        public FechaDto FechaSolicitudObj { get; set; }
        public string NumeroSolicitud { get; set; } = string.Empty;
        public int CodigoSolicitante { get; set; }
        public int CodigoProveedor { get; set; }
        public string Motivo { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int CodigoPresupuesto { get; set; }
        public int Ano { get; set; }
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
        public string SearchText { get { return $"{DescripcionTipoCompromiso}-{FechaSolicitudString}-{Ano}-{NumeroSolicitud}-{Motivo}-"; } }
    }

   public class DetalleSolicitudcompromisoDto 
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

   public class PucSolCompromisoDto
   {
        public int CodigoPucSolicitud { get; set; }
        public int CodigoSolicitud { get; set; }
        public int CodigoSaldo { get; set; }
        public int CodigoIcp { get; set; }
        public int CodigoPuc { get; set; }
        public int FinanciadoId { get; set; }
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
