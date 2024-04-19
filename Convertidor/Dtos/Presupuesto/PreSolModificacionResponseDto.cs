﻿namespace Convertidor.Dtos.Presupuesto
{
    public class PreSolModificacionResponseDto
    {
        public int CodigoSolModificacion { get; set; }
        public int TipoModificacionId { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public string FechaSolicitudString { get; set; }
        public FechaDto FechaSolicitudObj { get; set; }
        public int Ano { get; set; }
        public string NumeroSolModificacion { get; set; } = string.Empty;
        public string CodigoOficio { get; set; } = string.Empty;
        public int CodigoSolicitante { get; set; }
        public string Motivo { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
        public int NumeroCorrelativo { get; set; }
        public int CodigoPresupuesto { get; set; }
    }
}
