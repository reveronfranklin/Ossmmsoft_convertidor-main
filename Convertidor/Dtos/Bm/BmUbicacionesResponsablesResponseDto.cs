namespace Convertidor.Dtos.Bm;

public class BmUbicacionesResponsablesResponseDto
{
   
    public int CodigoBmConteo { get; set; }
    public int Conteo { get; set; }
    public string Titulo { get; set; }
    public int CodigoDirBien { get; set; }
    public int CodigoIcp { get; set; }
    public string UnidadEjecutora { get; set; }
    public int CodigoUsuario { get; set; }
    public int CodigoPersona { get; set; }
    public string Login { get; set; }
    public int Cedula { get; set; }
    public string Descripcion
    {
        get
        {
            return $"Conteo:{Conteo}-{UnidadEjecutora}";
        }
    }
    public string KeyUbicacionResponsable
    {
        get
        {
            return $"{CodigoBmConteo}-{Conteo}-{CodigoDirBien}";
        }
    }
    
}