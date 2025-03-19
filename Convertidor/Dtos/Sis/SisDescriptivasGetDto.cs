namespace Convertidor.Dtos.Sis;

public class SisDescriptivasGetDto
{
    public int DescripcionId { get; set; }
    public int DescripcionTituloId { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public string CodigoDescripcion { get; set; } = string.Empty;
}