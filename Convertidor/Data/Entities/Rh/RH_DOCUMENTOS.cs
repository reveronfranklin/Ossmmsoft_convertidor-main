using System;
namespace Convertidor.Data.Entities.Rh
{
	public class RH_DOCUMENTOS
    {
        public int CODIGO_PERSONA { get; set; }
        public int CODIGO_DOCUMENTO { get; set; }
        public int TIPO_DOCUMENTO_ID { get; set; } 
        public string NUMERO_DOCUMENTO { get; set; } 
        public DateTime FECHA_VENCIMIENTO { get; set; }
        public int TIPO_GRADO_ID { get; set; }
        public int GRADO_ID { get; set; }
        public string EXTRA1 { get; set; }
        public string EXTRA2 { get; set; }
        public string EXTRA3 { get; set; }
        public int USUARIO_INS { get; set; }
        public DateTime FECHA_INS { get; set; } 
        public int USUARIO_UPD { get; set; }
        public DateTime FECHA_UPD { get; set; }
        public int CODIGO_EMPRESA { get; set; }
        
    }
}

