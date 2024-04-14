namespace Convertidor.Dtos.Rh;

public class RhReporteNominaResponseDto
{
   
    public DateTime FechaNomina { get; set; }
    public string FechaNominaString { get; set; }
    public FechaDto FechaNominaObj { get; set; }
    public int CodigoPeriodo { get; set; }
    public int CodigoTipoNomina { get; set; }
    public string CodigoIcpConcat { get; set; }
    public int CodigoIcp { get; set; }
    public string Denominacion { get; set; }
    public string DenominacionCargo { get; set; }
    public int Cedula { get; set; }
    public string Nombre { get; set; }
    public string NoCuenta { get; set; }
    public string NumeroConcepto { get; set; }
    public string TipoMovConcepto { get; set; }
    public string DenominacionConcepto { get; set; }
    public string ComplementoConcepto { get; set; }
    public string Porcentaje { get; set; }
    public string TipoConcepto { get; set; }
    public decimal Monto { get; set; }
    public decimal Asignacion { get; set; }
    public decimal Deduccion { get; set; }
    public string Status { get; set; }
    public string DescripcionStatus { get; set; }
    public int CodigoPersona { get; set; }
    public DateTime FechaIngreso { get; set; }
    public string FechaIngresoString { get; set; }
    public FechaDto FechaIngresoObj { get; set; }
    public string CargoCodigo { get; set; }
    public string Banco { get; set; }
    public string CodigoConcepto { get; set; }
    public string Modulo { get; set; }
    public string CodigoIdentificador { get; set; }
    public int CodigoEmpresa { get; set; }
    public string Descripcion { get; set; }
    public decimal Sueldo { get; set; }
    public string DenominacionTipoNomina { get; set; }
}