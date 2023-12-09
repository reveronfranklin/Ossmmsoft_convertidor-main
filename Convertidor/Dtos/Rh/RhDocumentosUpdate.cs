namespace Convertidor.Dtos.Rh;

public class RhDocumentosUpdate
{
    public int CodigoPersona { get; set; }
    public int CodigoDocumento { get; set; }
    public int TipoDocumentoId { get; set; }
    public string NumeroDocumento { get; set; }
    public DateTime FechaVencimiento { get; set; } 
    public int TipoGradoId { get; set; }
    public int GradoId { get; set; }
    public int UsuarioIns { get; set; }
    public DateTime FechaIns { get; set; }
    public int UsuarioUpd { get; set; } 
    public DateTime FechaUpd { get; set; }
    public int CodigoEmpresa { get; set; }
    
}