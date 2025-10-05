namespace Convertidor.Data.EntitiesDestino.ADM
{
    public class ADM_PRE_ORDEN_PAGO
    {
        public int Id { get; set; }
        public string NombreEmisor { get; set; }
        public string DireccionEmisor { get; set; }
        public string Rif { get; set; }
        public string NumeroFactura { get; set; }
        public DateTime FechaEmision { get; set; }
        public decimal BaseImponible { get; set; }
        public decimal PorcentajeIva { get; set; }
        public decimal Iva { get; set; }
        public decimal MontoTotal { get; set; }
        public decimal Excento { get; set; }
        public int USUARIO_INS { get; set; }
        public DateTime FECHA_INS { get; set; }
        public int CODIGO_EMPRESA { get; set; }
        public string SEARCH_TEXT { get; set; }
      
    }
}
