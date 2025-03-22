namespace Convertidor.Dtos.Adm;

public class AdmChequeFilterDto
{
    public int PageSize { get; set; } 
    public int PageNumber { get; set; }
    public string SearchText { get; set; } 
    public int CodigoLote { get; set; }
}