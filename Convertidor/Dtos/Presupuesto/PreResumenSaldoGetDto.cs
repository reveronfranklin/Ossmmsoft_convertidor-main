namespace Convertidor.Dtos.Presupuesto;

public class PreResumenSaldoGetDto
{
    public int CodigoPresupuesto { get; set; }
    public string DenominacionPresupuesto { get; set; }
    public string Titulo { get; set; }
    public string CodigoIcpConcat { get; set; }
    public string Partida { get; set; }
    public decimal Presupuestado { get; set; }
    public decimal Modificacion { get; set; }
    public decimal AsignacionModificada { get; set; }
    public decimal Comprometido { get; set; }
    public decimal Causado { get; set; }
    public decimal Pagado { get; set; }
}