namespace Convertidor.Dtos.Rh;

public class RhComunicacionUpdate
{
    public int CodigoComunicacion { get; set; }
    public int CodigoPersona { get; set; }
    public int TipoComunicacionId { get; set; }  
    public string CodigoArea { get; set; } = string.Empty;
    public string LineaComunicacion { get; set; } = string.Empty;
    public int Extencion { get; set; }
    public int Principal { get; set; } 
}