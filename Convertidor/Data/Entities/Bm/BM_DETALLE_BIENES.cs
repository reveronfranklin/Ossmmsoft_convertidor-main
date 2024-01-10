using System;
using static NPOI.HSSF.Record.UnicodeString;
using static NPOI.HSSF.Util.HSSFColor;

namespace Convertidor.Data.Entities.Bm
{
    public class BM_DETALLE_BIENES
    {

        public int CODIGO_DETALLE_BIEN { get; set; }
        public int CODIGO_BIEN { get; set; }
        public int TIPO_ESPECIFICACION_ID { get; set; }
        public int ESPECIFICACION_ID { get; set; } 
        public string ESPECIFICACION { get; set; } =string.Empty;
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

