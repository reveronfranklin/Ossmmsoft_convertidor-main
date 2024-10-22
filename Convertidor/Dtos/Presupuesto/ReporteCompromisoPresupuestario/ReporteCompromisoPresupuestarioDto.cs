using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Convertidor.Dtos.Presupuesto.ReporteCompromisoPresupuestario
{
    public class ReporteCompromisoPresupuestarioDto
    {
        public EncabezadoReporteDto Encabezado;
        public List<CuerpoReporteDto> Cuerpo;
    }


    public class EncabezadoReporteDto
    {
        public string NumeroCompromiso { get; set; } = string.Empty;
        public DateTime FechaCompromiso { get; set; }
        public string FechaCompromisoString { get; set; }
        public FechaDto FechaCompromisoObj { get; set; }
        public string NumeroSolicitud { get; set; } = string.Empty;
        public int CodigoSolicitud { get; set; }
        public string UnidadEjecutora { get; set; } = string.Empty;
        public string NombreProveedor { get; set; } = string.Empty;
        public string Rif { get; set; }
        public string Direccion { get; set; }
        public string CodigoArea { get; set; } = string.Empty;
        public string LineaComunicacion { get; set; } = string.Empty;
        public string IcpConcat { get; set; } = string.Empty;
        public string Para { get; set; }
        public string codigoSector { get; set; }
        public string codigoPrograma { get; set; }
        public string codigoSubPrograma { get; set; }
        public string codigoProyecto { get; set; }
        public string codigoActividad { get; set; }
        public string CodigoIcpConcat { get { return $"{codigoSector}-{codigoPrograma}-{codigoSubPrograma}-{codigoProyecto}-{codigoActividad}"; } } 
        public string Denominacion { get; set; } = string.Empty;
        public string MontoEnLetras { get; set; } = string.Empty;
        public string Motivo { get; set; } = string.Empty;
        public string Firmante { get; set; } = string.Empty;
        public decimal Tolal { get; set; }
        public decimal Impuesto { get; set; }
        public decimal TolalMasImpuesto { get; set; }
        public decimal Base { get; set; }
        
        public decimal PorcentajeImpuesto { get; set; }
        public string Titulo { get; set; } = string.Empty;
        
        
        
        public string TelefonoProveedor { get { return $"{CodigoArea}{LineaComunicacion}"; } }
        public List<PrePucCompromisosResumenResponseDto> PucCompromisos { get; set; }
    }

    public class CuerpoReporteDto 
    {
        public int CodigoDetalleCompromiso { get; set; }
        public decimal Cantidad { get; set; }
        public string DescripcionUdm { get; set; } = string.Empty;
        public string DescripcionArticulo { get; set; } = string.Empty;
        public decimal PrecioUnitario { get; set; }
        public decimal TotalBolivares { get; set; }
        public string CodigoPucConcat { get; set; } = string.Empty;
        public string codigoIcpConcat { get; set; } = string.Empty;
        public string DescripcionFinanciado { get; set; } = string.Empty;
        public int FinanciadoId { get; set; }
        public decimal MontoImpuesto { get; set; }
        public decimal Monto { get; set; }
        //public List<PrePucCompromisosResumenResponseDto> PucCompromisos { get; set; }

    }
}
