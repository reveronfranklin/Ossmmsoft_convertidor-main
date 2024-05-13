namespace Convertidor.Dtos.Presupuesto
{
    public class PreEjecucionPresupuestariaUpdateDto
    {
        public int CodigoEjePresupuestaria { get; set; }
        public string CodigoGrupo { get; set; }
        public string CodigoNivel1 { get; set; }
        public string CodigoNivel2 { get; set; }
        public string CodigoNivel3 { get; set; }
        public string CodigoNivel4 { get; set; }
        public decimal IReal { get; set; }
        public decimal IProyectado { get; set; }
        public decimal IiReal { get; set; }
        public decimal IiProyectado { get; set; }
        public decimal IiiReal { get; set; }
        public decimal IiiProyectado { get; set; }
        public decimal IvReal { get; set; }
        public decimal IvProyectado { get; set; }
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
        public int CodigoPresupuesto { get; set; }
    }
}
