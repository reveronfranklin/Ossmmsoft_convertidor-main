namespace Convertidor.Dtos.Catastro
{
    public class CatDocumentosRamoUpdateDto
    {
        public int CodigoDocuRamo { get; set; }
        public int CodigoContribuyente { get; set; }
        public int CodigoContribuyenteFk { get; set; }
        public int Tributo { get; set; }
        public int CodigoIdentificador { get; set; }
        public int CodigoEstado { get; set; }
        public int CodigoMunicipio { get; set; }
        public int TipoDocumentoId { get; set; }
        public string NumeroDocumento { get; set; }
        public DateTime FechaDocumento { get; set; }
        public string Observacion { get; set; }
        public string Descripcion { get; set; }
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
        public string Origen { get; set; } = string.Empty;
        public string TipoTransaccion { get; set; } = string.Empty;
        public int CodigoAplicacion { get; set; }
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
        public int CodigoFicha { get; set; }
    }
}
