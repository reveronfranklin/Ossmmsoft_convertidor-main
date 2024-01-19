namespace Convertidor.Dtos.Sis;

public class OssFormulaUpdateDto
{
    public int Id { get; set; }
    public int IdVariable { get; set; }
    public string CodeVariable { get; set; } = null!;
    public string? Formula { get; set; }
    public string? FormulaDescripcion { get; set; }
    public int? OrdenCalculo { get; set; }
    public int  IdModeloCalculo { get; set; } 
  
    public int ModuloId { get; set; }
    public int AcumulaAlTotal { get; set; }
    
    
}