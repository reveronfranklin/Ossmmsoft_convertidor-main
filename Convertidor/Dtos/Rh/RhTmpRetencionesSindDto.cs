﻿namespace Convertidor.Dtos.Rh
{
    public class RhTmpRetencionesSindDto
    {
        public int Id { get; set; }
        public int CodigoRetencionAporte { get; set; }
        public int Secuencia { get; set; }
        public string UnidadEjecutora { get; set; }
        public string CedulaTexto { get; set; }
        public string NombresApellidos { get; set; }
        public string DescripcionCargo { get; set; }
        public DateTime FechaIngreso { get; set; }
        public decimal MontoSindTrabajador { get; set; }
        public decimal MontoSindPatrono { get; set; }
        public decimal MontoTotalRetencion { get; set; }
        public string FechaNomina { get; set; }
        public string SiglasTipoNomina { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        public int CodigoTipoNomina { get; set; }

    }
}
