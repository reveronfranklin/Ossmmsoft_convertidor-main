using NPOI.SS.Formula.Functions;

namespace Convertidor.Dtos.Adm.ReporteSolicitudCompromiso
{
    public class ReporteSolicitudCompromisoDto
    {
        public EncabezadoReporte Encabezado {  get; set; }
        public CuerpoReporte Cuerpo { get; set; }
    }

    public class EncabezadoReporte 
    {
        public int NumeroSolicitud { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public string FechaSolicitudString { get; set; }
        public FechaDto FechaSolicitudObj { get; set; }
        public int CodigoSector { get; set; }
        public int CodigoPrograma { get; set; }
        public int CodigoSubPrograma { get; set; }
        public int CodigoProyecto { get; set; }
        public int CodigoActividad { get; set; }
        public string Denominacion { get; set; } = string.Empty;
        public string UnidadEjecutora { get; set; }
        public int CodigoSolicitante { get; set; }
        public string NombreProveedor { get; set; } = string.Empty;
        public string Rif { get; set; } = string.Empty;
        public string Vialidad { get; set; } = string.Empty;
        public string Extra1 { get; set; } = string.Empty;
        public string Vivienda { get; set; } = string.Empty;
        public string CodigoArea { get; set; }=string.Empty;
        public string LineaComunicacion { get; set; } = string.Empty;
        public string DireccionProveedor { get { return $"{Vialidad}-{Extra1}-{Vivienda}"; } }
    }

    public class CuerpoReporte
    {
        public decimal Cantidad { get; set; }
        public string DescripcionUdm { get; set; } = string.Empty;
        public string DescripcionArticulo { get; set; } = string.Empty;
        public decimal PrecioUnitario { get; set; }
        public decimal TotalBolivares { get { return Cantidad * PrecioUnitario ; }} 
        public  decimal SubTotal { get; set; }
        public decimal TotalMontoImpuesto { get  { return TotalBolivares * PorImpuesto / 100; } } 
        public string Motivo { get; set; }
        public decimal Total { get { return TotalMontoImpuesto + TotalBolivares ; } }
        public decimal PorImpuesto { get; set; }
        public decimal MontoImpuesto { get; set; }
        public  string Status { get; set; }

    }


}
