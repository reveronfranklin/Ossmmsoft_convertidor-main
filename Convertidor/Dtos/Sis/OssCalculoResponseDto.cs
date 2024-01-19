namespace Convertidor.Dtos.Sis;

public class OssCalculoResponseDto
{
    public int Id { get; set; }
    public int? IdCalculo { get; set; }
    public int? IdVariable { get; set; }
    public string? CodeVariable { get; set; }
    public string? Formula { get; set; }
    public string? FormulaValor { get; set; }
    public string? Valor { get; set; }
    public string? Query { get; set; }
    public int? OrdenCalculo { get; set; }
    public string? CodeVariableExterno { get; set; }
    public string? IdCalculoExterno { get; set; }
    public int? ModuloId { get; set; }
    public int? IdModeloCalculo { get; set; }
    public int AcumulaAlTotal { get; set; }

}