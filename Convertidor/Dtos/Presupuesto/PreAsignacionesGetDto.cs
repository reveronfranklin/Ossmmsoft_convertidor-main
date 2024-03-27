namespace Convertidor.Dtos.Presupuesto;

public class PreAsignacionesGetDto
{
    public int CodigoAsignacion { get; set; }
    public int CodigoPresupuesto { get; set; }
    public int Año { get; set; }
    public int Escenario { get; set; }
    public int CodigoIcp { get; set; }
    public string CodigoIcpConcat { get; set; }
    public string DenominacionIcp { get; set; }
    public int CodigoPuc { get; set; }
    public string CodigoPucConcat { get; set; }
    public string DenominacionPuc { get; set; }
    public decimal Presupuestado { get; set; }
    public decimal Ordinario { get; set; }
    public decimal Coordinado { get; set; }
    public decimal Laee { get; set; }
    public decimal Fides { get; set; }
    public decimal Total { get; set; }
    public decimal TotalDesembolso { get; set; }
    public string SearchText { get; set; }
    
    
}