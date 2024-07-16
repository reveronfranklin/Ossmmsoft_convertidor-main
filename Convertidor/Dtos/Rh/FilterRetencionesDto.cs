namespace Convertidor.Dtos.Rh
{
    public class FilterRetencionesDto
    {
        public int TipoNomina { get; set; }
        public string FechaDesde { get; set; }
        public string FechaHasta { get; set; }
        public FechaDto FechaDesdeObj { get; set; }
        public FechaDto FechaHastaObj { get; set; }

    }
}
