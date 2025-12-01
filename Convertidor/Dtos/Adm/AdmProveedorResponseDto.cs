namespace Convertidor.Dtos.Adm;

public class AdmProveedorResponseDto
{
    public int CodigoProveedor  { get; set; }	
    public string NombreProveedor  { get; set; } = String.Empty;
    public int TipoProveedorId  { get; set; }	
    public string TipoProveeedor  { get; set; } = String.Empty;
    public string Nacionalidad  { get; set; } = String.Empty; //V o E
    public int Cedula  { get; set; }	
    public string Rif  { get; set; } = String.Empty;
    public DateTime? FechaRif  { get; set; }	 
    public FechaDto? FechaRifObj  { get; set; }	 
    public string FechaRifString  { get; set; }	 = String.Empty;
    public string Nit  { get; set; } = String.Empty;
    public DateTime? FechaNit  { get; set; }	
    public string FechaNitString  { get; set; }	
    public FechaDto? FechaNitObj  { get; set; }	
    public string? NumeroRegistroContraloria  { get; set; } = String.Empty;
    public DateTime? FechaRegistroContraloria  { get; set; }	
    public string? FechaRegistroContraloriaString  { get; set; }	 = String.Empty;
    public FechaDto? FechaRegistroContraloriaObj  { get; set; }	
    public decimal? CapitalPagado  { get; set; }	
    public decimal? CapitalSuscrito  { get; set; }	 

    public string? Status  { get; set; } = String.Empty;
    public int? EstatusFisicoId  { get; set; }
    public string? EstatusFisico  { get; set; } = String.Empty;
    public string? NumeroCuenta  { get; set; } = String.Empty;
}