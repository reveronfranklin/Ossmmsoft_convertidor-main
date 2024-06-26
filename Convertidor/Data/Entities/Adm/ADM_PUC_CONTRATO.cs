﻿namespace Convertidor.Data.Entities.Adm
{
    public class ADM_PUC_CONTRATO
    {
        public int CODIGO_PUC_CONTRATO { get; set; }
        public int CODIGO_CONTRATO { get; set; }
        public int CODIGO_ICP { get; set; }
        public int CODIGO_PUC { get; set; }
        public int FINANCIADO_ID { get; set; }
        public int? CODIGO_FINANCIADO { get; set; }
        public int CODIGO_SALDO { get; set; }
        public int MONTO { get; set; }
        public int MONTO_CAUSADO { get; set; }
        public int MONTO_ANULADO { get; set; }
        public string? EXTRA1 { get; set; } = string.Empty;
        public string? EXTRA2 { get; set; } = string.Empty;
        public string? EXTRA3 { get; set; } = string.Empty;
        public int USUARIO_INS { get; set; }
        public DateTime FECHA_INS { get; set; }
        public int USUARIO_UPD { get; set; }
        public DateTime FECHA_UPD { get; set; }
        public int CODIGO_EMPRESA { get; set; }
        public int CODIGO_PRESUPUESTO { get; set; }
    }
}
