namespace Convertidor.Dtos.Adm;

public class AdmProveedorContactoUpdateDto
{
    public int CodigoContactoProveedor  { get; set; } 
    public int CodigoProveedor  { get; set; } 
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public int IdentificacionId  { get; set; } 
    public string Identificacion { get; set; } = string.Empty;
    public string Sexo { get; set; } = string.Empty;
    public int TipoContactoId  { get; set; } 
    public int Principal  { get; set; }    
}