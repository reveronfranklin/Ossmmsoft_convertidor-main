namespace Convertidor.Dtos.Catastro
{
    public class CatAvaluoConstruccionResponseDto
    {
        public int CodigoAvaluoConstruccion { get; set; }
        public int CodigoFicha { get; set; }
        public DateTime AnoAvaluo { get; set; }
        public string AnoAvaluoString { get; set; }
        public FechaDto AnoAvaluoObj { get; set; }
        public int PlantaId { get; set; }
        public int UnidadMedidaId { get; set; }
        public int ValorMedida { get; set; }
        public int FactorDePreciacion { get; set; }
        public int ValorModificado { get; set; }
        public int AreaTotal { get; set; }
        public int MontoAvaluo { get; set; }
        public string Observaciones { get; set; } = string.Empty;
        public int ValorReposicion { get; set; }
        public int AreaConstruccion { get; set; }
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
        public int CodigoParcela { get; set; }
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
        public string Tipologia { get; set; } = string.Empty;
        public int FrenteParcela { get; set; }
        public int MontoComplemento { get; set; }
        public int MontoComplementoUsuario { get; set; }
        public int MontoTotalAvaluo { get; set; }
    }
}
