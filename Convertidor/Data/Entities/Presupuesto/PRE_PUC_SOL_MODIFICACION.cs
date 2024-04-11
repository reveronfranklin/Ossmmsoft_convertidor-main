namespace Convertidor.Data.Entities.Presupuesto;

public class PRE_PUC_SOL_MODIFICACION
{
 public int CODIGO_PUC_SOL_MODIFICACION { get; set; }
 public int CODIGO_SOL_MODIFICACION { get; set; }
 public int CODIGO_SALDO { get; set; }
 public string FINANCIADO_ID { get; set; }
 public int CODIGO_FINANCIADO { get; set; }
 public int CODIGO_ICP { get; set; }
 public int CODIGO_PUC { get; set; }
 public decimal MONTO { get; set; }
 public string DE_PARA { get; set; }
 public decimal EXTRA1 { get; set; }
 public string EXTRA2 { get; set; }
 public string EXTRA3 { get; set; }
 public int USUARIO_INS { get; set; }
 public DateTime FECHA_INS { get; set; }
 public int USUARIO_UPD { get; set; }
 public DateTime FECHA_UPD { get; set; }
 public int CODIGO_EMPRESA { get; set; }
 public decimal MONTO_MODIFICADO { get; set; }
 public decimal MONTO_ANULADO { get; set; }
 public int CODIGO_PRESUPUESTO { get; set; }
}