namespace Convertidor.Dtos.Catastro
{
    public class CatDValorConstruccionResponseDto
    {
        public int CodigoParcela { get; set; }
        public int CodigoDValorConstruccion { get; set; }
        public int CodigoValorConstruccion { get; set; }
        public int CodigoInmueble { get; set; }
        public string CodigoCatastro { get; set; } = string.Empty;
        public int EstructuraNivel1Id { get; set; }
        public int EstructuraNivel2Id { get; set; }
        public int EstructuraNivel3Id { get; set; }
        public int EstructuraNivel4Id { get; set; }
        public string EstructuraDescriptiva { get; set; } = string.Empty;
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
        public int ValorComplementario { get; set; }
    }
}
