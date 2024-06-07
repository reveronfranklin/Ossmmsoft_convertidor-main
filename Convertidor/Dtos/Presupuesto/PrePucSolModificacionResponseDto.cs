namespace Convertidor.Dtos.Presupuesto;

public class PrePucSolModificacionResponseDto
{
    public int CodigoPucSolModificacion { get; set; }
    public int CodigoSolModificacion { get; set; }
    public int CodigoSaldo { get; set; }
    public string FinanciadoId { get; set; }
    public string DescripcionFinanciado { get; set; }
    public int CodigoFinanciado { get; set; }
    public int CodigoIcp { get; set; }
    public string CodigoIcpConcat { get; set; }
    public string DenominacionIcp { get; set; }
    public int CodigoPuc { get; set; }
    public string CodigoPucConcat { get; set; }
    public string DenominacionPuc { get; set; }
    public decimal Monto { get; set; }
    public decimal Descontar { get; set; }
    public decimal Aportar { get; set; }
    public string DePara { get; set; }
    public decimal MontoModificado { get; set; }
    public decimal MontoAnulado { get; set; }
    public int CodigoPresupuesto { get; set; }
}