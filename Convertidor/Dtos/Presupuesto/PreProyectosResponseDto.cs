namespace Convertidor.Dtos.Presupuesto
{
    public class PreProyectosResponseDto
    {
        public int CodigoProyecto { get; set; }
        public int CodigoIcp { get; set; }
        public int NumeroProyecto { get; set; }
        public string DenominacionProyecto { get; set; } = string.Empty;
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
        public int CodigoPresupuesto { get; set; }
    }
}
