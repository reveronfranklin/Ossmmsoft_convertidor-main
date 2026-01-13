namespace Convertidor.Dtos.Cnt
{
    public class CntPeriodosResponseDto
    {
        public int CodigoPeriodo { get; set; }
        public string NombrePeriodo { get; set; } = string.Empty;
        public DateTime FechaDesde { get; set; }
        public string FechaDesdeString { get; set; }
        public FechaDto FechaDesdeObj { get; set; }
        public DateTime FechaHasta { get; set; }
        public string FechaHastaString { get; set; }
        public FechaDto FechaHastaObj { get; set; }
        public int AnoPeriodo { get; set; }
        public int NumeroPeriodo { get; set; }
        public int UsuarioPreCierre { get; set; }
        public DateTime FechaPreCierre { get; set; }
        public string FechaPreCierreString { get; set; }
        public FechaDto FechaPreCierreObj { get; set; }
        public int UsuarioCierre { get; set; }
        public DateTime FechaCierre { get; set; }
        public string FechaCierreString { get; set; }
        public FechaDto FechaCierreObj { get; set; }
        public int UsuarioPreCierreConc { get; set; }
        public DateTime FechaPreCierreConc { get; set; }
        public string FechaPreCierreConcString { get; set; }
        public FechaDto FechaPreCierreConcObj { get; set; }
        public int UsuarioCierreConc { get; set; }
        public DateTime FechaCierreConc { get; set; }
        public string FechaCierreConcString { get; set; }
        public FechaDto FechaCierreConcObj { get; set; }
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;

    }
}
