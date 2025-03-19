namespace Convertidor.Dtos.Adm;

public class AdmLotePagoFilterDto
{
    public int PageSize { get; set; } 
    public int PageNumber { get; set; }
    public string SearchText { get; set; } 
    public int CodigoPresupuesto { get; set; } 
    public int CodigEmpresa { get; set; } 
}