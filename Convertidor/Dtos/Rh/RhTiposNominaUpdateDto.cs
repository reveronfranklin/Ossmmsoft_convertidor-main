namespace Convertidor.Dtos.Rh;

public class RhTiposNominaUpdateDto
{
    public int CodigoTipoNomina { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public string SiglasTipoNomina { get; set; } = string.Empty;
    public int FrecuenciaPagoId { get; set; }
    public decimal SueldoMinimo { get; set; }

}