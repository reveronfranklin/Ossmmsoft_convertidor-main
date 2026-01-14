namespace Convertidor.Dtos.Adm;

public class AdmProveedorUpdateDto
{
    public int CodigoProveedor  { get; set; }	
    public string NombreProveedor  { get; set; } = String.Empty;
    public int TipoProveedorId  { get; set; }	
    public string Nacionalidad  { get; set; } = String.Empty;
    public int Cedula  { get; set; }	
    public string Rif  { get; set; } = String.Empty;
    public DateTime FechaRif  { get; set; }	 

    public string? Nit  { get; set; } = String.Empty;
    public DateTime? FechaNit  { get; set; }	

    public string? NumeroRegistroContraloria  { get; set; } = String.Empty;
    public DateTime? FechaRegistroContraloria  { get; set; }	

    public decimal CapitalPagado  { get; set; }	
    public decimal CapitalSuscrito  { get; set; }	 
  
    public Boolean Activo  { get; set; } = true;
    public int CodigoPersona  { get; set; }	
    public int CodigoAuxiliarGastoXPagar  { get; set; }
    public int CodigoAuxiliarOrdenPago  { get; set; }
    public int EstatusFisicoId  { get; set; }
    public string NumeroCuenta  { get; set; } = String.Empty;
}