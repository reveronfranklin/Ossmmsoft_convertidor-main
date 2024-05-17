namespace Convertidor.Dtos.Adm
{
    public class AdmSolicitudesResponseDto
    {
        public int CodigoSolicitud { get; set; }
        public int? Ano { get; set; }
        public string NumeroSolicitud { get; set; } = string.Empty;
        public DateTime FechaSolicitud { get; set; }
        public string FechaSolicitudString { get; set; }
        public FechaDto FechaSolicitudObj { get; set; }
        public int CodigoSolicitante { get; set; }
        public int? TipoSolicitudId { get; set; }
        public int? CodigoProveedor { get; set; }
        public string? Motivo { get; set; } = string.Empty;
        public string? Nota { get; set; } = string.Empty;
        public string? Status { get; set; } = string.Empty;
        public int? CodigoPresupuesto { get; set; }
        
        public string? DescripcionStatus { get; set; } = string.Empty;
        
        public string? NombreProveedor { get; set; }
        
        public string? DenominacionSolicitante { get; set; }
        public string? DescripcionTipoSolicitud { get; set; }
        
        public string SearchText { get { return $"{CodigoSolicitud}-{NumeroSolicitud}-{FechaSolicitudString}-{Motivo}-{Nota}-{DescripcionStatus}-{NombreProveedor}-{DenominacionSolicitante}-{DescripcionTipoSolicitud}"; } }

        
        
    }
}
