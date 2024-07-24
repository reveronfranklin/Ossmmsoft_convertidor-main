using Convertidor.Utility;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NPOI.SS.Formula.Functions;

namespace Convertidor.Dtos.Adm.ReporteSolicitudCompromiso
{
    public class ReporteSolicitudCompromisoDto
    {
        public EncabezadoReporteDto Encabezado {  get; set; }
        public List<CuerpoReporteDto> Cuerpo { get; set; }
    }

    public class EncabezadoReporteDto
    {
        public int CodigoSolicitud { get; set; }
        public int Ano { get; set; }
        public string NumeroSolicitud { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public string FechaSolicitudString { get; set; }
        public FechaDto FechaSolicitudObj { get; set; }
        public string Denominacion { get; set; } = string.Empty;
        public string UnidadEjecutora { get; set; }
        public int CodigoSolicitante { get; set; }
        public int CodigoProveedor { get; set; }
        public string NombreProveedor { get; set; } = string.Empty;
        public string Rif { get; set; } = string.Empty;
        public string Vialidad { get; set; } = string.Empty;
        public string Extra1 { get; set; } = string.Empty;
        public string Vivienda { get; set; } = string.Empty;
        public string CodigoArea { get; set; } = string.Empty;
        public string LineaComunicacion { get; set; } = string.Empty;
        public string DireccionProveedor { get { return $"{Vialidad}  {Extra1}  {Vivienda}"; } }
        public string TelefonoProveedor { get { return $"{CodigoArea}{LineaComunicacion}"; } }
        public int CodigoDetallesolicitud { get; set; }
        public string Motivo { get; set; }
        public string Nota { get; set; }
        public string DenominacionSolicitante { get; set; }
        public string DescripcionStatus { get; set; }
        public string DescripcionTipoSolicitud { get; set; }
        public string SearchText { get { return $"{CodigoSolicitud}-{NumeroSolicitud}-{FechaSolicitudString}-{Motivo}-{Nota}-{DescripcionStatus}-{NombreProveedor}-{DenominacionSolicitante}-{DescripcionTipoSolicitud}"; } }

    }

    public class CuerpoReporteDto
    {
        public decimal Cantidad { get; set; }
        public string DescripcionUdmId { get; set; } 
        public string DescripcionArticulo { get; set; } = string.Empty;
        public decimal PrecioUnitario { get; set; }
        public decimal TotalBolivares { get; set;} 
        public  decimal SubTotal { get; set; }
        public decimal TotalMontoImpuesto { get; set; } 
        public string Motivo { get; set; }
        public decimal Total { get; set; }
        public string TotalEnletras { get; set; }
        public decimal PorImpuesto { get; set; }
        public decimal MontoImpuesto { get; set; }
        public  string Status { get; set; }

        public string MontoLetras { get; set; } = string.Empty;
        public string Firmante { get; set; } = string.Empty;

    }


}
