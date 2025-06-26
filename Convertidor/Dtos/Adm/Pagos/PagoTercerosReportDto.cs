namespace Convertidor.Dtos.Adm.Pagos;

public class PagoTercerosReportDto
{
    public int CodigoLotePago { get; set; }
    public int CodigoCheque { get; set; }
    public int NumeroCheque { get; set; }
    public DateTime FechaCheque { get; set; }
    public  string Nombre { get; set; }
    public  string NoCuenta { get; set; }
    public  string PagarALaOrdenDe { get; set; }
    public  string Motivo { get; set; }
    public  decimal Monto { get; set; }
    public  string Endoso { get; set; }
    public  string UsuarioIns { get; set; }  
    public int CodigoProveedor { get; set; }
    public  string DetalleOpIcpPuc { get; set; }  
    public  decimal MontoOpIcpPuc { get; set; }
    public  string DetalleImpRet { get; set; }  
    public  decimal MontoImpRet { get; set; }
    public int CodigoPresupuesto { get; set; } 
}