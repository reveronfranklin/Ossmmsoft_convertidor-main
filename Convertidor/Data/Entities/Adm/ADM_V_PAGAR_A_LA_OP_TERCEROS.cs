namespace Convertidor.Data.Entities.Adm;

public class ADM_V_PAGAR_A_LA_OP_TERCEROS
{

    public int CodigoProveedor { get; set; }
    public int CodigoContactoProveedor { get; set; }
    public string? PagarALaOrdenDe { get; set; } = string.Empty;
    public string NombreProveedor { get; set; }
    public int? CodigoEmpresa { get; set; }
        
    
}
