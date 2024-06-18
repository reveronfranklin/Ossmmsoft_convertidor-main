namespace Convertidor.Dtos.Rh;

public class RhDocumentosDetallesUpdate
{
    public int CodigoDocumentoDetalle { get; set; }
    public int CodigoDocumento { get; set; }
    public string Descripcion { get; set; }
    public DateTime FechaFinal { get; set; }
    public DateTime FechaInicial { get; set; }

}