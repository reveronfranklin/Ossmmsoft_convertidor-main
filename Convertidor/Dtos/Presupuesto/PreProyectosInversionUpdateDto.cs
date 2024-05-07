namespace Convertidor.Dtos.Presupuesto
{
    public class PreProyectosInversionUpdateDto
    {
        public int CodigoProyectoInv { get; set; }
        public int Ano { get; set; }
        public int Escenario { get; set; }
        public int CodigoIcp { get; set; }
        public int FinanciadoId { get; set; }
        public string CodigoObra { get; set; } = string.Empty;
        public string Denominacion { get; set; } = string.Empty;
        public int CodigoFuncionario { get; set; }
        public DateTime FechaIni { get; set; }
        public DateTime FechaFin { get; set; }
        public int SituacionId { get; set; }
        public int Costo { get; set; }
        public int CompromisoAnterior { get; set; }
        public int CompromisoVigente { get; set; }
        public int EjecutadoAnterior { get; set; }
        public int EjecutadoVigente { get; set; }
        public int EstimadaPresupuesto { get; set; }
        public int EstimadaPosterior { get; set; }
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
        public int CodigoPresupuesto { get; set; }
        public string Funcionario { get; set; } = string.Empty;
    }
}
