namespace Convertidor.Dtos.Presupuesto
{
    public class PreMetasUpdateDto
    {
        public int CodigoMeta { get; set; }
        public int CodigoProyecto { get; set; }
        public int NumeroMeta { get; set; }
        public string DenominacionMeta { get; set; } = string.Empty;
        public int UnidadMedidaId { get; set; }
        public int CantidadMeta { get; set; }
        public int CostoMeta { get; set; }
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
        public int CodigoPresupuesto { get; set; }
        public int CantidadPrimerTrimestre { get; set; }
        public int CantidadSegundoTrimestre { get; set; }
        public int CantidadTercerTrimestre { get; set; }
        public int CantidadCuartoTrimestre { get; set; }
    }
}
