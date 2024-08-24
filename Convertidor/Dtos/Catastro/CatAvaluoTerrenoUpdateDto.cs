namespace Convertidor.Dtos.Catastro
{
    public class CatAvaluoTerrenoUpdateDto
    {
        public int CodigoAvaluoTerreno { get; set; }
        public int CodigoFicha { get; set; }
        public DateTime AnoAvaluo { get; set; }
        public int UnidadMedidaId { get; set; }
        public int AreaM2 { get; set; }
        public decimal ValorUnitario { get; set; }
        public int ValorAjustado { get; set; }
        public int FactorAjuste { get; set; }
        public int FactorFrente { get; set; }
        public int FactorForma { get; set; }
        public int FactorEsquina { get; set; }
        public int FactorProf { get; set; }
        public int FactorArea { get; set; }
        public decimal ValorModificado { get; set; }
        public int AreaTotal { get; set; }
        public decimal MontoAvaluo { get; set; }
        public string Observaciones { get; set; } = string.Empty;
        public int IncrementoEsquina { get; set; }
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
        public int CodigoZonificacion { get; set; }
        public int FrenteParcela { get; set; }
        public int CodigoVialidadPrincipal { get; set; }
        public int CodigoVialidadAdyacente1 { get; set; }
        public int CodigoVialidadAdyacente2 { get; set; }
        public int Vialidad1 { get; set; }
        public int Vialidad2 { get; set; }
        public int Vialidad3 { get; set; }
        public int Vialidad4 { get; set; }
        public int UbicacionTerreno { get; set; }
        public int CodigoVialidad1 { get; set; }
        public int CodigoVialidad2 { get; set; }
        public int CodigoVialidad3 { get; set; }
        public int CodigoVialidad4 { get; set; }
        public int FactorProfundidad { get; set; }
        public decimal MontoTotalAvaluo { get; set; }
    }
}
