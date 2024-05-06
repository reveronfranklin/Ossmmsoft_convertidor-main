namespace Convertidor.Dtos.Presupuesto;

public class ListIcpPucConDisponible
{
    public int CodigoSaldo { get; set; }
    public int CodigoIcp { get; set; }
    public string CodigoIcpConcat { get; set; }
    public string DenominacionIcp { get; set; }
    
    public int CodigoPuc { get; set; }
    public string CodigoPucConcat { get; set; }
    public string DenominacionPuc { get; set; }
    
    public int FinanciadoId { get; set; }
    public string DenominacionFinanciado { get; set; }
    
    public decimal Disponible { get; set; }
    public string SearchText { get { return $"{CodigoIcpConcat}-{DenominacionIcp}-{CodigoPucConcat}-{DenominacionPuc}-{DenominacionFinanciado}"; } }

    
}