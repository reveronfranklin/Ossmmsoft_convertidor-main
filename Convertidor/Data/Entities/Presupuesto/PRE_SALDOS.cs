namespace Convertidor.Data.Entities.Presupuesto;

public class PRE_SALDOS
{
    

public int CODIGO_SALDO { get; set; }
public int ANO { get; set; }
public int CODIGO_ICP { get; set; }
public int CODIGO_PUC { get; set; }
public int FINANCIADO_ID { get; set; }
public int CODIGO_FINANCIADO { get; set; }
public decimal ASIGNACION { get; set; } 
public decimal BLOQUEADO { get; set; } 
public decimal MODIFICADO { get; set; } 
public decimal COMPROMETIDO { get; set; } 
public decimal CAUSADO { get; set; } 
public decimal PAGADO { get; set; } 
public decimal AJUSTADO { get; set; } 
public decimal PRESUPUESTADO { get; set; } 
public string? EXTRA1 { get; set; }= string.Empty;
public string? EXTRA2 { get; set; } = string.Empty;
public string? EXTRA3 { get; set; } = string.Empty;
public int? USUARIO_INS { get; set; }
public DateTime? FECHA_INS { get; set; }
public int? USUARIO_UPD { get; set; }
public DateTime? FECHA_UPD { get; set; }
public int? CODIGO_PRESUPUESTO { get; set; }
public int? CODIGO_EMPRESA { get; set; }
    
}