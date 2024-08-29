namespace Convertidor.Dtos.Catastro
{
    public class CatDesgloseResponseDto
    {
        public int CodigoDesglose { get; set; }
        public int CodigoDesgloseFk { get; set; }
        public int CodigoDesglosePk { get; set; }
        public decimal CodigoParcela { get; set; }
        public string CodigoCatastro { get; set; } = string.Empty;
        public string Titulo { get; set; } = string.Empty;
        public int AreaTerrenoTotal { get; set; }
        public int AreaConTrucTotal { get; set; }
        public int AreaTrrTotalVendi { get; set; }
        public int AreaTerrComun { get; set; }
        public int AreaContrucComun { get; set; }
        public int AreaTerrSinCond { get; set; }
        public int AreaContrucSinCond { get; set; }
        public int Area { get; set; }
        public int EstacionaTerr { get; set; }
        public int EstacionaContruc { get; set; }
        public decimal PorcentajCondominio { get; set; }
        public int ManualTerreno { get; set; }
        public int ManualConstruccion { get; set; }
        public int MaleteroTerreno { get; set; }
        public int MaleteroConstruccion { get; set; }
        public string Observacion { get; set; } = string.Empty;
        public string NivelId { get; set; } = string.Empty;
        public string UnidadId { get; set; } = string.Empty;
        public int TipoOperacionId { get; set; }
        public string TipoTransaccion { get; set; } = string.Empty;
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
        public string Extra4 { get; set; } = string.Empty;
        public string Extra5 { get; set; } = string.Empty;
    }
}
