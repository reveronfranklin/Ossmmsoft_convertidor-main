﻿namespace Convertidor.Data.Entities.Cnt
{
    public class CNT_ESTADO_CUENTAS
    {
        public int CODIGO_ESTADO_CUENTA { get; set; }
        public int CODIGO_CUENTA_BANCO { get; set; }
        public string NUMERO_ESTADO_CUENTA { get; set; } = string.Empty;
        public DateTime FECHA_DESDE { get; set; }
        public DateTime FECHA_HASTA { get; set; }
        public int SALDO_INICIAL { get; set; }
        public int SALDO_FINAL { get; set; }
        public string EXTRA1 { get; set; } = string.Empty;
        public string EXTRA2 { get; set; } = string.Empty;
        public string EXTRA3 { get; set; } = string.Empty;
        public int USUARIO_INS { get; set; }
        public DateTime FECHA_INS { get; set; }
        public int USUARIO_UPD { get; set; }
        public DateTime FECHA_UPD { get; set; }
        public int CODIGO_EMPRESA { get; set; }


    }
}
