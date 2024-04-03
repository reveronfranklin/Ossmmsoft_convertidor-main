﻿namespace Convertidor.Dtos.Adm
{
    public class AdmContratosUpdateDto
    {
        public int CodigoContrato { get; set; }
        public int ANO { get; set; }
        public DateTime FechaContrato { get; set; }
        public string NumeroContrato { get; set; }=string.Empty;
        public int CodigoSolicitante { get; set; }
        public int CodigoProveedor { get; set; }
        public DateTime FechaAprobacion { get; set; }
        public string NumeroAprobacion { get; set; } = string.Empty;
        public string Obra { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public int Parroquiaid { get; set; }
        public DateTime FechaIniObra { get; set; }
        public DateTime FechaFinObra { get; set; }
        public int MontoContrato { get; set; }
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int CodigoPresupuesto { get; set; }
        public int MontoOriginal { get; set; }
        public int TipoModificacionId { get; set; }
    }
}
