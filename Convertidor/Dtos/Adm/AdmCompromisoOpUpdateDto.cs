namespace Convertidor.Dtos.Adm
{
    public class AdmCompromisoOpUpdateDto
    {
        public int CodigoCompromisoOp { get; set; }
        public int OrigenCompromisoId { get; set; }
        public int CodigoIdentificador { get; set; }
        public int CodigoOrdenPago { get; set; }
        public int CodigoProveedor { get; set; }
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
        public int CodigoPresupuesto { get; set; }
        public int CodigoValContrato { get; set; }
    }
}
