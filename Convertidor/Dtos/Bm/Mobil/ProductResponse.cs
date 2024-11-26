namespace Convertidor.Dtos.Bm.Mobil;

public class ProductResponse
{
    public string Key { get; set; }
    public int Id { get; set; }
    public string Articulo { get; set; }
    public string Descripcion { get; set; }
    public string Responsable { get; set; }
    public string NroPlaca { get; set; }
    public int CodigoDepartamentoResponsable { get; set; }
    public string DescripcionDepartamentoResponsable { get; set; }
    public string[]  Images { get; set; }
}


public class ProductFilterDto
{
    
    public int PageSize { get; set; } 
    public int PageNumber { get; set; }
    public int? CodigoBien { get; set; } 
    public int CodigoDepartamentoResponsable { get; set; }
    public string SearhText { get; set; }
  
  
}