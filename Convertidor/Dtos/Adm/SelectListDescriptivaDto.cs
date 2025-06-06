using NPOI.SS.Formula.Functions;

namespace Convertidor.Dtos.Adm;

public class SelectListDescriptivaDto
{
    public int  Id { get; set; }
    public string Descripcion {
        get;
        set;
    }  
    
    public decimal  Value { get; set; }
    public string  Titulo { get; set; }
    
}