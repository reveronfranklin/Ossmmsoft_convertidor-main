namespace Convertidor.Dtos.Adm.PreOrdenPago;


public class AdmPreOdenPagoCreateDto
{
  
    public string NombreEmisor { get; set; }
    public string DireccionEmisor { get; set; }
    public string Rif { get; set; }
    public string NumeroFactura { get; set; }
    public DateTime FechaEmision { get; set; }
    public decimal BaseImponible { get; set; }
    public decimal PorcentajeIva { get; set; }
    public decimal Iva { get; set; }
    public decimal MontoTotal { get; set; }
    public decimal Excento { get; set; }
    public int UsuarioConectado { get; set; }
    
    public string SearchText => $"{NombreEmisor} {DireccionEmisor} {Rif} {NumeroFactura}";
    
   
}