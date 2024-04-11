using NPOI.Util;

namespace Convertidor.Data.Entities.Presupuesto;

public class PRE_RESUMEN_SALDOS
{
    public int CODIGO_PRESUPUESTO { get; set; }
    public string TITULO { get; set; }
    public string CODIGO_ICP_CONCAT { get; set; }
    public string PARTIDA { get; set; }
    public decimal PRESUPUESTADO { get; set; }
    public decimal MODIFICACION { get; set; }
    public decimal ASIGNACION_MODIFICADA { get; set; }
    public decimal COMPROMETIDO { get; set; }
    public decimal CAUSADO { get; set; }
    public decimal PAGADO { get; set; }
    
}