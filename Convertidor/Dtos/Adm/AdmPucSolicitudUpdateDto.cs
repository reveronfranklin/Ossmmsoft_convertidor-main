﻿namespace Convertidor.Dtos.Adm
{
    public class AdmPucSolicitudUpdateDto
    {
        public int CodigoPucSolicitud { get; set; }
        public int CodigoDetalleSolicitud { get; set; }
        public int CodigoSolicitud { get; set; }
        public int CodigoSaldo { get; set; }
        public int CodigoIcp { get; set; }
        public int CodigoPuc { get; set; }
        public int FinanciadoId { get; set; }
        public int CodigoFinanciado { get; set; }
        public int Monto { get; set; }
        public int MontoComprometido { get; set; }
        public int MontoAnulado { get; set; }
      
        public int CodigoPresupuesto { get; set; }
    }
}
