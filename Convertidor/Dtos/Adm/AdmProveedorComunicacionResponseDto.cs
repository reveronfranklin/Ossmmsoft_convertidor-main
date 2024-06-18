namespace Convertidor.Dtos.Adm;

public class AdmProveedorComunicacionResponseDto
{
    public int CodigoComProveedor  { get; set; } 
    public int CodigoProveedor  { get; set; } 
    public int TipoComunicacionId  { get; set; } 
    public string TipoComunicacionDescripcion { get; set; } = string.Empty;
    public string CodigoArea { get; set; } = string.Empty;
    public string LineaComunicacion { get; set; } = string.Empty;
    public int Extension  { get; set; } 
    public int Principal  { get; set; }  
    public int CodigoPresupuesto { get; set; }
}