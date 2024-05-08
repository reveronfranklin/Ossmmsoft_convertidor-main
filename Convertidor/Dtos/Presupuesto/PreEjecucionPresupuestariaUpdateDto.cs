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
        public int IReal { get; set; }
        public int IProyectado { get; set; }
        public int IiReal { get; set; }
        public int IiProyectado { get; set; }
        public int IiiReal { get; set; }
        public int IiiProyectado { get; set; }
        public int IvReal { get; set; }
        public int IvProyectado { get; set; }
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
        public int CodigoPresupuesto { get; set; }
    }
}
