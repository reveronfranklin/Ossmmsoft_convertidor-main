using Convertidor.Utility;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NPOI.SS.Formula.Functions;

namespace Convertidor.Dtos.Adm.ReporteSolicitudCompromiso
{
    public class ReporteSolicitudCompromisoDto
    {
        public EncabezadoReporteDto Encabezado {  get; set; }
        public CuerpoReporteDto Cuerpo { get; set; }
    }

    public class EncabezadoReporteDto
    {
        public int CodigoSolicitud { get; set; }
        public int? Ano { get; set; }
        public string NumeroSolicitud { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public string FechaSolicitudString { get; set; }
        public FechaDto FechaSolicitudObj { get; set; }
        public string Denominacion { get; set; } = string.Empty;
        public string UnidadEjecutora { get; set; }
        public int CodigoSolicitante { get; set; }
        public int CodigoIcp { get; set; }
        public int CodigoProveedor { get; set; }
        public string NombreProveedor { get; set; } = string.Empty;
        public string Rif { get; set; } = string.Empty;
        public string Vialidad { get; set; } = string.Empty;
        public string Extra1 { get; set; } = string.Empty;
        public string Vivienda { get; set; } = string.Empty;
        public string CodigoArea { get; set; }=string.Empty;
        public string LineaComunicacion { get; set; } = string.Empty;
        public string DireccionProveedor { get { return $"{Vialidad}-{Extra1}-{Vivienda}"; } }
    }

    public class CuerpoReporteDto
    {
        public decimal Cantidad { get; set; }
        public string DescripcionUdm { get; set; } = string.Empty;
        public string DescripcionArticulo { get; set; } = string.Empty;
        public decimal PrecioUnitario { get; set; }
        public decimal TotalBolivares { get { return Cantidad * PrecioUnitario ; }} 
        public  decimal SubTotal { get; set; }
        public decimal TotalMontoImpuesto { get  { return Math.Round( TotalBolivares * PorImpuesto / 100); } } 
        public string Motivo { get; set; }
        public decimal Total { get { return TotalMontoImpuesto + TotalBolivares ; } }
        public string TotalEnletras { get; set; }
        public decimal PorImpuesto { get; set; }
        public decimal MontoImpuesto { get; set; }
        public  string Status { get; set; }

         
    }


}
