namespace Convertidor.Dtos.Sis;

public class SisCuentaBancoResponseDto
{
    public int CodigoCuentaBanco { get; set; }
    
    public int CodigoBanco { get; set; }
    public string DescripcionBanco { get; set; } = string.Empty;
    
    public int TipoCuentaId { get; set; }
    public string DescripcionTipoCuenta { get; set; } = string.Empty;
    public string NoCuenta { get; set; } = string.Empty;
    
    public string FormatoMascara { get; set; } = string.Empty;
    
    public int DenominacionFuncionalId { get; set; }
    
    public string DescripcionDenominacionFuncional { get; set; } = string.Empty;

    
    public string Codigo { get; set; } = string.Empty;
    
    public bool Principal { get; set; }
    
    public bool Recaudadora { get; set; }
    
    public int CodigoMayor { get; set; }
    
    public int CodigoAuxiliar { get; set; }
    public string SearchText { get; set; } = null!;
   
}