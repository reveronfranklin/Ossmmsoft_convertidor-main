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
    public string Extra1 { get; set; } = string.Empty;
    public string Extra2 { get; set; } = string.Empty;
    public string Extra3 { get; set; } = string.Empty;

}