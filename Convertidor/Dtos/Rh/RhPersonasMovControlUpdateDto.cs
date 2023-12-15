namespace Convertidor.Dtos.Rh;

public class RhPersonasMovControlUpdateDto
{
    public int CodigoPersonaMovCtrl { get; set; }
    public int CodigoPersona { get; set; }
    public int CodigoConcepto { get; set; }
    public int ControlAplica { get; set; }
    public string Extra1 { get; set; } = string.Empty;
    public string Extra2 { get; set; } = string.Empty;
    public string Extra3 { get; set; } = string.Empty;
    public int UsuarioIns { get; set; }
    public DateTime FechaIns { get; set; }
    public int UsuarioUpd { get; set; }
    public DateTime FechaUpd { get; set; }
    public int CodigoEmpresa { get; set; }

}