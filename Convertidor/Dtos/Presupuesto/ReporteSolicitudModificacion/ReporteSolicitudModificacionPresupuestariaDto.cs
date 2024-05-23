namespace Convertidor.Dtos.Presupuesto.ReporteSolicitudModificacion
{
    public class ReporteSolicitudModificacionPresupuestariaDto
    {
        public GeneralReporteSolicitudModificacionDto General { get; set; }
        public List<DetalleReporteSolicitudModificacionDto> DetalleDe { get; set; }
        public List<DetalleReporteSolicitudModificacionDto> DetallePara { get; set; }

    }

    public class GeneralReporteSolicitudModificacionDto 
    {
        public int CodigoSolModificacion { get; set; }
        public int TipoModificacionId { get; set; }
        public string DescripcionTipoModificacion { get; set; } = string.Empty;
        public bool Descontar { get; set; }
        public bool Aportar { get; set; }
        public bool OrigenPreSaldo { get; set; }

        public DateTime FechaSolicitud { get; set; }
        public string FechaSolicitudString { get; set; }
        public FechaDto FechaSolicitudObj { get; set; }
        public int Ano { get; set; }
        public string NumeroSolModificacion { get; set; } = string.Empty;
        public string CodigoOficio { get; set; } = string.Empty;
        public int CodigoSolicitante { get; set; }
        public string Motivo { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string DescripcionEstatus { get; set; } = string.Empty;
        public int NumeroCorrelativo { get; set; }
        public int CodigoPresupuesto { get; set; }
        public string StatusProceso { get; set; } = string.Empty;
        public string SearchText { get { return $"{DescripcionTipoModificacion}-{FechaSolicitudString}-{Ano}-{NumeroSolModificacion}-{Motivo}-{DescripcionEstatus}-{NumeroCorrelativo}"; } }
    }

   public class DetalleReporteSolicitudModificacionDto 
   {
        public int CodigoPucSolModificacion { get; set; }
        public int CodigoSolModificacion { get; set; }
        public int CodigoSaldo { get; set; }
        public string FinanciadoId { get; set; }
        public string DescripcionFinanciado { get; set; }
        public int CodigoFinanciado { get; set; }
        public int CodigoIcp { get; set; }
        public string CodigoIcpConcat { get; set; }
        public string DenominacionIcp { get; set; }
        public int CodigoPuc { get; set; }
        public string CodigoPucConcat { get; set; }
        public string DenominacionPuc { get; set; }
        public decimal Monto { get; set; }
        public decimal Descontar { get; set; }
        public decimal Aportar { get; set; }
        public string DePara { get; set; }
        public decimal MontoModificado { get; set; }
        public decimal MontoAnulado { get; set; }
    }
}
