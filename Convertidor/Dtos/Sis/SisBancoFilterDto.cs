namespace Convertidor.Dtos.Sis;

public class SisBancoFilterDto
{
    public int PageSize { get; set; } 
    public int PageNumber { get; set; }
    public string SearchText { get; set; } 
    public int CodigEmpresa { get; set; } 
   
}