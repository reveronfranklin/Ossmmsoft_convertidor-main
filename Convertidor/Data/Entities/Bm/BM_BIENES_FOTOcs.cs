using System;
using static NPOI.HSSF.Record.UnicodeString;
using static NPOI.HSSF.Util.HSSFColor;

namespace Convertidor.Data.Entities.Bm
{
    public class BM_BIENES_FOTO
    {
        public int CODIGO_BIEN_FOTO { get; set; }
        public int CODIGO_BIEN { get; set; }
        public string NUMERO_PLACA { get; set; } = string.Empty;
        public string FOTO { get; set; } = string.Empty;
        public string TITULO { get; set; } = string.Empty;
        public int USUARIO_INS { get; set; }
        public DateTime FECHA_INS { get; set; }
        public int USUARIO_UPD { get; set; }
        public DateTime FECHA_UPD { get; set; }
        public int CODIGO_EMPRESA { get; set; }
      
      

    }
}

