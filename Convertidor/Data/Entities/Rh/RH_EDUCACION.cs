using System;
namespace Convertidor.Data.Entities.Rh
{
	public class RH_EDUCACION
	{

        public int CODIGO_EDUCACION { get; set; }
        public int CODIGO_PERSONA { get; set; }
        public int NIVEL_ID { get; set; }
        public string NOMBRE_INSTITUTO { get; set; } = string.Empty;
        public string LOCALIDAD_INSTITUTO { get; set; } = string.Empty;
        public int PROFESION_ID { get; set; }
        public DateTime FECHA_INI { get; set; }
        public DateTime FECHA_FIN { get; set; }
        public int ULTIMO_ANO_APROBADO { get; set; }
        public string GRADUADO { get; set; } = string.Empty;
        public int TITULO_ID { get; set; }
        public int MENCION_ESPECIALIDAD_ID { get; set; }
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

