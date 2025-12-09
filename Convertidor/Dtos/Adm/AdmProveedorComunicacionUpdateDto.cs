namespace Convertidor.Dtos.Adm;

public class AdmProveedorComunicacionUpdateDto
{
    public int CodigoComProveedor  { get; set; } 
    public int CodigoProveedor  { get; set; } 
    public int TipoComunicacionId  { get; set; } 
    public string CodigoArea { get; set; } = string.Empty;
    public string LineaComunicacion { get; set; } = string.Empty;
    public int Extension  { get; set; } 
    public bool Principal  { get; set; }  
 
}