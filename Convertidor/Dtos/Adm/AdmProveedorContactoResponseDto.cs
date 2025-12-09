namespace Convertidor.Dtos.Adm;

public class AdmProveedorContactoResponseDto
{
    public int CodigoContactoProveedor  { get; set; } 
    public int CodigoProveedor  { get; set; } 
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public int IdentificacionId  { get; set; } 
        public string DescripcionIdentificacion { get; set; } = string.Empty;
    public string Identificacion { get; set; } = string.Empty;
    public string Sexo { get; set; } = string.Empty;
    public int TipoContactoId  { get; set; } 
    public string TipoContactoDescripcion { get; set; } = string.Empty;
    public bool Principal  { get; set; }    
}