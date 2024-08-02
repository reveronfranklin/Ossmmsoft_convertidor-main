using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Convertidor.Dtos.Presupuesto.ReporteCompromisoPresupuestario
{
    public class ReporteCompromisoPresupuestarioDto
    {
        public EncabezadoReporteDto Encabezado;
        public CuerpoReporteDto CuerpoReporte;
    }


    public class EncabezadoReporteDto 
    {
        public string NumeroCompromiso { get; set; }= string.Empty;
        public DateTime FechaCompromiso { get; set; }
        public string FechaCompromisoString { get; set; }
        public FechaDto FechaCompromisoObj { get; set; }
        public string NumeroSolicitud { get; set; } = string.Empty;
        public int CodigoSolicitud { get; set; }
        public string UnidadEjecutora { get; set; } = string.Empty;
        public string NombreProveedor { get; set; } = string.Empty;
        public string Rif { get; set; }
        public string CodigoArea { get; set; } = string.Empty;
        public string LineaComunicacion { get; set; } = string.Empty;
        public string IcpConcat { get; set; } = string.Empty;
        public string Denominacion { get; set; } = string.Empty;
        public string MontoEnLetras { get; set; } = string.Empty;
        public string Motivo { get; set; } = string.Empty;
        public string Firmante { get; set; } = string.Empty;
    }

    public class CuerpoReporteDto 
    {
        public decimal Cantidad { get; set; }
        public string DescripcionUdm { get; set; } = string.Empty;
        public string DescripcionArticulo { get; set; } = string.Empty;
        public decimal PrecioUnitario { get; set; }
        public decimal TotalBolivares { get; set; }
        public string CodigoPucConcat { get; set; } = string.Empty;
        public string DescripcionFinanciado { get; set; } = string.Empty;
        public decimal Monto { get; set; }

    }
}
