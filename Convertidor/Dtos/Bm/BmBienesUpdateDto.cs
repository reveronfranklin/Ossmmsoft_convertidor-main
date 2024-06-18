namespace Convertidor.Dtos.Bm
{
    public class BmBienesUpdateDto
    {

        public int CodigoBien { get; set; }
        public int CodigoArticulo { get; set; }
        public int CodigoProveedor { get; set; }
        public int CodigoOrdenCompra { get; set; }
        public int OrigenId { get; set; }
        public DateTime FechaFabricacion { get; set; }
        public string NumeroOrdenCompra { get; set; } = string.Empty;
        public DateTime FechaCompra { get; set; }
        public string NumeroPlaca { get; set; } = string.Empty;
        public string NumeroLote { get; set; } = string.Empty;
        public int ValorInicial { get; set; }
        public int ValorActual { get; set; }
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
        public string NumeroFactura { get; set; } = string.Empty;
        public DateTime FechaFactura { get; set; }
        public int TipoImpuestoId { get; set; }
    }

	
}

