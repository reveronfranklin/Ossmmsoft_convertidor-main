﻿namespace Convertidor.Data.Entities.Cnt
{
    public class CNT_RELACION_DOCUMENTOS
    {
        public int CODIGO_RELACION_DOCUMENTO { get; set; }
        public int TIPO_DOCUMENTO_ID { get; set; }
        public int TIPO_TRANSACCION_ID { get; set; }
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
