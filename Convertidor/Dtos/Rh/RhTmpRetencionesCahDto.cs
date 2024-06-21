namespace Convertidor.Dtos.Rh
{
    public class RhTmpRetencionesCahDto
    {
        public int Id { get; set; }
        public int CodigoRetencionAporte { get; set; }
        public int Secuencia { get; set; }
        public string UnidadEjecutora { get; set; }
        public string CedulaTexto { get; set; }
        public string NombresApellidos { get; set; }
        public string DescripcionCargo { get; set; }
        public DateTime FechaIngreso { get; set; }
        public decimal MontoCahTrabajador { get; set; }
        public decimal MontoCahPatrono { get; set; }
        public decimal MontoTotalRetencion { get; set; }
        public string FechaNomina { get; set; }
        public string SiglasTipoNomina { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        public int CodigoTipoNomina { get; set; }

        public string Avatar { get; set; } = string.Empty;
        public string SearchText { get { return $"{UnidadEjecutora}-{CedulaTexto}-{NombresApellidos}-{UnidadEjecutora}--{DescripcionCargo}"; } }
      
      
        
    }
}
