﻿namespace Convertidor.Dtos.Rh
{
    public class RhTmpRetencionesSindDto
    {
        public int CodigoRetencionAporte { get; set; }
        public int Secuencia { get; set; }
        public string UnidadEjecutora { get; set; }
        public string CedulaTexto { get; set; }
        public string NombresApellidos { get; set; }
        public string DescripcionCargo { get; set; }
        public DateTime FechaIngreso { get; set; }
        public int MontoSindTrabajador { get; set; }
        public int MontoSindPatrono { get; set; }
        public int MontoTotalRetencion { get; set; }
        public string FechaNomina { get; set; }
        public string SiglasTipoNomina { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        public int CodigoTipoNomina { get; set; }

    }
}
