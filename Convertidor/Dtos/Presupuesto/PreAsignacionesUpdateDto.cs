namespace Convertidor.Dtos.Presupuesto;

public class PreAsignacionesUpdateDto
{
    public int CodigoAsignacion { get; set; }
    public int CodigoPresupuesto { get; set; }
    public int Año { get; set; }
    public int Escenario { get; set; }
    public int CodigoIcp { get; set; }
    public int CodigoPuc { get; set; }
    public decimal Presupuestado { get; set; }
    public decimal Ordinario { get; set; }
    public decimal Coordinado { get; set; }
    public decimal Laee { get; set; }
    public decimal Fides { get; set; }
}