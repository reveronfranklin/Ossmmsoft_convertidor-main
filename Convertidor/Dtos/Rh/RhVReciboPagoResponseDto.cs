namespace Convertidor.Dtos.Rh;

public class RhVReciboPagoResponseDto
{
    public DateTime FechaPeriodoNomina { get; set; }
    public string FechaPeriodoNominaString { get; set; }
    public FechaDto FechaPeriodoNominaObj { get; set; }
    public DateTime FechaNomina { get; set; }
    public string FechaNominaString { get; set; }
    public FechaDto FechaNominaObj { get; set; }
    public int CodigoPeriodo { get; set; }
    public int CodigoTipoNomina { get; set; }
    public string CodigoIcpConcat { get; set; } = string.Empty;
    public string Denominacion { get; set; } = string.Empty;
    public string DenominacionCargo { get; set; } = string.Empty;
    public List< ResumenRhVReciboPagoResponseDto> ResumenReciboPago { get; set; }
    public int Cedula { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string NoCuenta { get; set; } = string.Empty;
    public string DescripcionTipoNomina { get; set; } = string.Empty;
    public string CodigoConcepto { get; set; } = string.Empty;
    public int CodigoConceptoId { get; set; }
    public string TipoMovConcepto { get; set; } = string.Empty;
    public string DenominacionConcepto { get; set; } = string.Empty;
    public string TipoConcepto { get; set; } = string.Empty;
    public string Porcentaje { get; set; } = string.Empty;
    public decimal Asignacion { get; set; }
    public decimal Deduccion { get; set; }
    public decimal Monto { get; set; }
    public decimal SueldoBase { get; set; }
    public string TipoSueldo { get; set; } = string.Empty;
    public decimal Sueldo { get; set; }
    public string CodigoTipoSueldoDesc { get; set; } = string.Empty;
    public string ComplementoConcepto { get; set; } = string.Empty;
    public int CodigoPersona { get; set; }
    public string Modulo { get; set; } = string.Empty;
    public string CodigoIdentificador { get; set; } = string.Empty;
   
}
