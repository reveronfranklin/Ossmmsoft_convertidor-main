namespace Convertidor.Dtos.Sis;

public class OssFormulaResponseDto
{
    public int Id { get; set; }
    public int IdVariable { get; set; }
    public string CodeVariable { get; set; } = null!;
    public string? Formula { get; set; }
    public string? FormulaDescripcion { get; set; }
    public int? OrdenCalculo { get; set; }
    public string CodigoExterno { get; set; } = null!;
    public int? ModuloId { get; set; }
}