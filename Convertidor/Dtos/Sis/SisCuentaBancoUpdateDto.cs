namespace Convertidor.Dtos.Sis;

public class SisCuentaBancoUpdateDto
{
    public int CodigoCuentaBanco { get; set; }
    
    public int CodigoBanco { get; set; }
    
    public int TipoCuentaId { get; set; }
    public string NoCuenta { get; set; } = string.Empty;
    
    public string FormatoMascara { get; set; } = string.Empty;
    
    public int DenominacionFuncionalId { get; set; }
    
    public string Codigo { get; set; } = string.Empty;
    
    public bool Principal { get; set; }
    
    public bool Recaudadora { get; set; }
    
    public int CodigoMayor { get; set; }
    
    public int CodigoAuxiliar { get; set; }
    
   
}