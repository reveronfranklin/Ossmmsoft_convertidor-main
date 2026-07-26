namespace Convertidor.Dtos.Adm
{
    public enum EstadoAsignacionComprobante
    {
        NoAplica = 0,
        Reutilizado = 1,
        Generado = 2,
        Error = 3
    }

    public class AsignacionComprobanteOpDto
    {
        public EstadoAsignacionComprobante Estado { get; set; }
        public int CodigoOrdenPago { get; set; }
        public decimal? NumeroComprobante { get; set; }
        public string NumeroComprobanteTexto { get; set; } = string.Empty;
        public int CantidadRetencionesIva { get; set; }
        public int CantidadRetencionesActualizadas { get; set; }
    }
}
