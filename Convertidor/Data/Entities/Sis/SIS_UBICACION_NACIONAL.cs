using System;
using static NPOI.HSSF.Record.UnicodeString;

namespace Convertidor.Data.Entities.Sis
{
	public class SIS_UBICACION_NACIONAL
	{
        public int PAIS { get; set; }
        public int ENTIDAD { get; set; }
        public int MUNICIPIO { get; set; }
        public int CIUDAD { get; set; }
        public int PARROQUIA { get; set; }
        public int SECTOR { get; set; }
        public int URBANIZACION { get; set; }
        public int MANZANA { get; set; }
        public int PARCELA { get; set; }
        public string DESCRIPCION { get; set; } = string.Empty;
        public string EXTRA1 { get; set; } = string.Empty;
        public string EXTRA2 { get; set; } = string.Empty;
        public string EXTRA3 { get; set; } = string.Empty;
        public int? USUARIO_INS { get; set; }
        public DateTime? FECHA_INS { get; set; }
        public int? USUARIO_UPD { get; set; }
        public DateTime? FECHA_UPD { get; set; }

    }
}

