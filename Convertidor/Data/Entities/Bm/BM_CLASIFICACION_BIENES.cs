using System;
using static NPOI.HSSF.Record.UnicodeString;
using static NPOI.HSSF.Util.HSSFColor;

namespace Convertidor.Data.Entities.Bm
{
    public class BM_CLASIFICACION_BIENES
    {

        public int CODIGO_CLASIFICACION_BIEN { get; set; }
        public string CODIGO_GRUPO { get; set; }= string.Empty;
        public string CODIGO_NIVEL1 { get; set; }=string.Empty;
        public string CODIGO_NIVEL2 { get; set; } =string.Empty; 
        public string CODIGO_NIVEL3 { get; set; } =string.Empty;
        public string DENOMINACION { get; set; } = string.Empty;
        public string DESCRIPCION { get; set; } = string.Empty;
        public DateTime FECHA_INI { get; set; } 
        public DateTime FECHA_FIN { get; set; } 
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

