﻿namespace Convertidor.Dtos.Presupuesto
{
    public class PreSolModificacionResponseDto
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
        public decimal TotalAportar { get; set; }
        public decimal TotalDescontar { get; set; }
        public string SearchText { get { return $"{DescripcionTipoModificacion}-{FechaSolicitudString}-{Ano}-{NumeroSolModificacion}-{Motivo}-{DescripcionEstatus}-{NumeroCorrelativo}"; } }

    }
}
