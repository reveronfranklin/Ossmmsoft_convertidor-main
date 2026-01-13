namespace Convertidor.Dtos.Adm;

public class AdmLotePagoUpdateDto
{
    public int CodigoLotePago { get;  set; }
    public int TipoPagoId { get;  set; }
    public DateTime FechaPago { get;  set; }
    public int CodigoCuentaBanco { get;  set; }
    public string Status { get; set; } = string.Empty;
    public string Titulo { get; set; } = string.Empty;
    public int CodigoPresupuesto { get; set; }
}