namespace Convertidor.Dtos.Sis;

public class OssVariableUpdateDto
{
    public int Id { get; set; }
    public string Code { get; set; } = null!;
    public string Descripcion { get; set; } = null!;
    public int? Longitud { get; set; }
    public int? LongitudRedondeo { get; set; }
    public int? LongitudTruncado { get; set; }
    public int? LongitudDecimal { get; set; }
    public int? CodigoEmpresa { get; set; }
    public int? ModuloId { get; set; }
}