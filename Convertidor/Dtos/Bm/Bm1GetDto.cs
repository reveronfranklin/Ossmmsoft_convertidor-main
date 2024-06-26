namespace Convertidor.Dtos.Bm;

public class Bm1GetDto
{
    public int CodigoIcp { get; set; }
    public string UnidadTrabajo { get; set; } = string.Empty;
    public string CodigoGrupo { get; set; } = string.Empty;
    public string CodigoNivel1 { get; set; } = string.Empty;
    public string CodigoNivel2 { get; set; } = string.Empty;
    public string NumeroLote { get; set; } = string.Empty;
    public string ConsecutivoPlaca { get; set; } = string.Empty;
    public int Cantidad { get; set; }
    public string NumeroPlaca { get; set; } = string.Empty;
    public decimal ValorActual { get; set; } 
    public string Articulo { get; set; } = string.Empty;
    public string Especificacion { get; set; } = string.Empty;
    public string Servicio { get; set; } = string.Empty;
    public string ResponsableBien { get; set; } = string.Empty;
    public string SearchText { get { return $"{CodigoBien}-{UnidadTrabajo}-{CodigoGrupo}-{NumeroPlaca}-{Articulo}-{ResponsableBien}-{Year.ToString()}-{Month.ToString()}"; } }
   
    public string linkData { get; set; } = string.Empty;
    
    public int CodigoBien { get; set; }
    public int CodigoMovBien { get; set; }
    public DateTime FechaMovimiento { get; set; }
    public DateTime? FechaMovimientoFiltro { get; set; }
    
    public int CantidadFotos { get; set; }
    public int Year { get; set; }
    public int Month { get; set; }
    public string NroPlaca { get; set; } = string.Empty;


}
public class Bm1ExcelGetDto
{
    public string UnidadTrabajo { get; set; } = string.Empty;
 
    public int Cantidad { get; set; }
    public string NumeroPlaca { get; set; } = string.Empty;
    public int CodigoBien { get; set; }
    public decimal ValorActual { get; set; } 
    public string Articulo { get; set; } = string.Empty;
    public string Especificacion { get; set; } = string.Empty;
    public string Servicio { get; set; } = string.Empty;
    public string ResponsableBien { get; set; } = string.Empty;
 
    public DateTime FechaMovimiento { get; set; }
    public DateTime FechaMovimientoFiltro { get; set; }
    public int Year { get; set; }
    public int Month { get; set; }
    public string NroPlaca { get; set; } = string.Empty;
  
    
}