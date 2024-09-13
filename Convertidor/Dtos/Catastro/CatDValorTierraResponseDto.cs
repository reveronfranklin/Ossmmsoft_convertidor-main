namespace Convertidor.Dtos.Catastro
{
    public class CatDValorTierraResponseDto
    {
        public int CodigoValorTierra { get; set; }
        public int CodigoDValorTierraUrbFk { get; set; }
        public int PaisId { get; set; }
        public int EstadoId { get; set; }
        public int MunicipioId { get; set; }
        public int ParroquiaId { get; set; }
        public int SectorId { get; set; }
        public DateTime FechaIniVigValor { get; set; }
        public string FechaIniVigValorString { get; set; }
        public FechaDto FechaIniVigValorObj { get; set; }
        public DateTime FechaFinVigValor { get; set; }
        public string FechaFinVigValorString { get; set; }
        public FechaDto FechaFinVigValorObj { get; set; }
        public int VialidadPrincipalId { get; set; }
        public int VialidadDesdeId { get; set; }
        public int VialidadHastaId { get; set; }
        public decimal ValorTierra { get; set; }
        public string Observaciones { get; set; } = string.Empty;
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
        public int CodigoZonificacionId { get; set; }
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
