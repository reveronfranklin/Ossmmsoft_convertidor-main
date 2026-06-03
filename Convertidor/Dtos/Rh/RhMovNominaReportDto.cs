namespace Convertidor.Dtos.Rh
{
    public class RhMovNominaReportDto
    {
        public int CodigoTipoNomina { get; set; }
        public DateTime FechaPago { get; set; }
        public int TipoOperacion { get; set; }
        public int CodigoPeriodo { get; set; }
        public int Cedula { get; set; }
        public string Report { get; set; }
        public string Usuario { get; set; }
    }
}


