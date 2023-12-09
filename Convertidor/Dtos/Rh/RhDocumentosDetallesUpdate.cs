namespace Convertidor.Dtos.Rh;

public class RhDocumentosDetallesUpdate
{
    public int CodigoDocumentoDetalle { get; set; }
    public int CodigoDocumento { get; set; }
    public string Descripcion { get; set; }
    public int UsuarioIns { get; set; }
    public DateTime FechaIns { get; set; }
    public int UsuarioUpd { get; set; }
    public DateTime FechaUpd { get; set; }
    public int CodigoEmpresa { get; set; }
    public DateTime FechaFinal { get; set; }
    public DateTime FechaInicial { get; set; }

}