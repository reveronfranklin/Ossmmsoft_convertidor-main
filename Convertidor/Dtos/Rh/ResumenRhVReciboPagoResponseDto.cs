namespace Convertidor.Dtos.Rh;

public class ResumenRhVReciboPagoResponseDto
{
    public string CodigoIcpConcat { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public int CodigoTipoNomina { get; set; }
    public int Cedula { get; set; }
    public decimal Sueldo { get; set; }
    public DateTime FechaNomina { get; set; }
    public string FechaNominaString { get; set; }
    public string CodigoConcepto { get; set; } = string.Empty;
    public string DenominacionConcepto { get; set; } = string.Empty;
    public string ComplementoConcepto { get; set; } = string.Empty;
    public string Porcentaje { get; set; } = string.Empty;
    public FechaDto FechaNominaObj { get; set; }
    public decimal Asignacion { get; set; }
    public decimal Deduccion { get; set; }
    public decimal SueldoBase { get; set; }
    public int CodigoPersona { get; set; }

}

