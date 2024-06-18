namespace Convertidor.Data.Entities.Rh;

public class RH_CONCEPTOS_PUC
{
    public int CODIGO_CONCEPTO_PUC { get; set; }
    public int CODIGO_CONCEPTO { get; set; }
    public int CODIGO_PUC { get; set; }
    public int CODIGO_PRESUPUESTO { get; set; }
    public int ESTATUS { get; set; }
    public string EXTRA1 { get; set; } = string.Empty;
    public string EXTRA2 { get; set; } = string.Empty;
    public string EXTRA3 { get; set; } = string.Empty;
    public string EXTRA4 { get; set; } = string.Empty;
    public string EXTRA5 { get; set; } = string.Empty;
    public int USUARIO_INS { get; set; }
    public DateTime FECHA_INS { get; set; }
    public int USUARIO_UPD { get; set; }
    public DateTime FECHA_UPD { get; set; }
    public int CODIGO_EMPRESA { get; set; }

}