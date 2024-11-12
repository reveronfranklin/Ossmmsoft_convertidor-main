namespace Convertidor.Dtos.Adm
{
    public class AdmCompromisoOpResponseDto
    {
        public int CodigoCompromisoOp { get; set; }
        public int OrigenCompromisoId { get; set; }
        public string OrigenDescripcion { get; set; }
        public int CodigoIdentificador { get; set; }
        public string Numero { get; set; }
        public string Fecha { get; set; }
        
        public int CodigoOrdenPago { get; set; }
        public int CodigoProveedor { get; set; }
        public int CodigoPresupuesto { get; set; }
        public int CodigoValContrato { get; set; }
        public decimal Monto { get; set; }
        public string Descripcion { get; set; }
    }
}
