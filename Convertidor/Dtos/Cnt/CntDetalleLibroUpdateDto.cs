namespace Convertidor.Dtos.Cnt
{
    public class CntDetalleLibroUpdateDto
    {
        public int CodigoDetalleLibro { get; set; }
        public int CodigoLibro { get; set; }
        public int TipoDocumentoId { get; set; }
        public int CodigoCheque { get; set; }
        public int CodigoIdentificador { get; set; }
        public int OrigenId { get; set; }
        public string NumeroDocumento { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public int Monto { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;


    }
}
