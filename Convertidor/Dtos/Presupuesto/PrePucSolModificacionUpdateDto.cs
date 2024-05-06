namespace Convertidor.Dtos.Presupuesto;

public class PrePucSolModificacionUpdateDto
{
    public int CodigoPresupuesto { get; set; }
    public int CodigoPucSolModificacion { get; set; }
    public int CodigoSolModificacion { get; set; }
    public int CodigoSaldo { get; set; }
    public int FinanciadoId { get; set; }
    public int CodigoIcp { get; set; }
    public int CodigoPuc { get; set; }
    public decimal Monto { get; set; }
    public string DePara { get; set; }
}