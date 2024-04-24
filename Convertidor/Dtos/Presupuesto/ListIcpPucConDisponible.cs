namespace Convertidor.Dtos.Presupuesto;

public class ListIcpPucConDisponible
{
    public int CodigoIcp { get; set; }
    public string CodigoIcpConcat { get; set; }
    public string DenominacionIcp { get; set; }
    public int CodigoPuc { get; set; }
    public string CodigoPucConcat { get; set; }
    public string DenominacionPuc { get; set; }
    public decimal Disponible { get; set; }
    
}