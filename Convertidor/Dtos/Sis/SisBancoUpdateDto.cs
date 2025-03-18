namespace Convertidor.Dtos.Sis;

public class SisBancoUpdateDto
{
    public int CodigoBanco { get; set; }
    public string Nombre { get; set; } = null!;
    public string CodigoInterbancario { get; set; } = null!;
   
}