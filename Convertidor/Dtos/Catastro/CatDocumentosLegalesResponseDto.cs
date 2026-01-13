namespace Convertidor.Dtos.Catastro
{
    public class CatDocumentosLegalesResponseDto
    {
        public int CodigoDocumentosLegales { get; set; }
        public int CodigoFicha { get; set; }
        public int DocumentoNumero { get; set; }
        public string FolioNumero { get; set; } = string.Empty;
        public string TomoNumero { get; set; } = string.Empty;
        public string ProfNumero { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; }
        public string FechaRegistroString { get; set; }
        public FechaDto FechaRegistroObj { get; set; }
        public decimal AreaTerreno { get; set; }
        public decimal AreaConstruccion { get; set; }
        public string Protocolo { get; set; } = string.Empty;
        public decimal MontoRegistro { get; set; }
        public int ServTerreno { get; set; }
        public decimal PrecioTerreno { get; set; }
        public int NumeroCivico { get; set; }
        public DateTime FechaPrimeraVisita { get; set; }
        public string FechaPrimeraVisitaString { get; set; }
        public FechaDto FechaPrimeraVisitaObj { get; set; }
        public DateTime FechaLevantamiento { get; set; }
        public string FechaLevantamientoString { get; set; }
        public FechaDto FechaLevantamientoObj { get; set; }
        public int ControlArchivo { get; set; }
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
        public string Extra4 { get; set; } = string.Empty;
        public string Extra5 { get; set; } = string.Empty;
        public string Extra6 { get; set; } = string.Empty;
        public string Extra7 { get; set; } = string.Empty;
        public string Extra8 { get; set; } = string.Empty;
        public string Extra9 { get; set; } = string.Empty;
        public string Extra10 { get; set; } = string.Empty;
        public string Extra11 { get; set; } = string.Empty;
        public string Extra12 { get; set; } = string.Empty;
        public string Extra13 { get; set; } = string.Empty;
        public string Extra14 { get; set; } = string.Empty;
        public string Extra15 { get; set; } = string.Empty;
    }
}
