﻿namespace Convertidor.Dtos.Adm
{
    public class AdmSolicitudesUpdateDto
    {
        public int CodigoSolicitud { get; set; }
        public string NumeroSolicitud { get; set; } = string.Empty;
        public DateTime FechaSolicitud { get; set; }
        public int CodigoSolicitante { get; set; }
        public int TipoSolicitudId { get; set; }
        public int CodigoProveedor { get; set; }
        public string Motivo { get; set; } = string.Empty;
        public string Nota { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int CodigoPresupuesto { get; set; }
    }
}
