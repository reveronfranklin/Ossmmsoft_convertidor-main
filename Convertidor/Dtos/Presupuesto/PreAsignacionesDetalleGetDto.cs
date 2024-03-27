namespace Convertidor.Dtos.Presupuesto;

public class PreAsignacionesDetalleGetDto
{
    public int CodigoAsignacion { get; set; }
    public int CodigoAsignacionDetalle { get; set; }
    public DateTime FechaDesembolso { get; set; }
    public string FechaDesembolsoString { get; set; }
    public FechaDto FechaDesembolsoObj { get; set; }
    public decimal Monto { get; set; }
    public string Notas { get; set; } = string.Empty;
}