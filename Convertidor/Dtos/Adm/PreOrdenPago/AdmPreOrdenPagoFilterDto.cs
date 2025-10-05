namespace Convertidor.Dtos.Adm.PreOrdenPago;

public class AdmPreOrdenPagoFilterDto
{
    public int PageSize { get; set; } 
    public int PageNumber { get; set; }
    public int UsuarioConectado { get; set; }
    public string SearchText { get; set; } 
  
}