namespace Convertidor.Dtos.Presupuesto
{
    public class PreEscalaUpdateDto
    {
        public int CodigoEscala { get; set; }
        public int Ano { get; set; }
        public int Escenario { get; set; }
        public string NumeroEscala { get; set; } = string.Empty;
        public string CodigoGrupo { get; set; }
        public decimal ValorIni { get; set; }
        public decimal ValorFin { get; set; }
        public int CodigoPresupuesto { get; set; }
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
    }
}
