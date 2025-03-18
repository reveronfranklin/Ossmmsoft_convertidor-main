namespace Convertidor.Dtos.Sis;

public class SisBancoResponseDto
{
    public int CodigoBanco { get; set; }
    public string Nombre { get; set; } = null!;
    public string CodigoInterbancario { get; set; } = null!;
    
    public string SearchText { get; set; } = null!;
   
}