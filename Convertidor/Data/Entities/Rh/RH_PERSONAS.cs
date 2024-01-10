namespace Convertidor.Data.Entities.Rh
{
	public class RH_PERSONAS
	{
        public int CODIGO_PERSONA { get; set; }
        public int CEDULA { get; set; }
        public string NOMBRE { get; set; } = string.Empty;
        public string APELLIDO { get; set; } = string.Empty;
        public string NACIONALIDAD { get; set; } = string.Empty;
        public string SEXO { get; set; } = string.Empty;
        public DateTime FECHA_NACIMIENTO { get; set; }
        public int PAIS_NACIMIENTO_ID { get; set; }
        public int ESTADO_NACIMIENTO_ID { get; set; }
        public string NUMERO_GACETA_NACIONAL { get; set; } = string.Empty;
        public int ESTADO_CIVIL_ID { get; set; }
        public decimal ESTATURA { get; set; }
        public decimal PESO { get; set; }
        public string MANO_HABIL { get; set; } = string.Empty;
        public string EXTRA1 { get; set; } = string.Empty;
        public string EXTRA2 { get; set; } = string.Empty;
        public string EXTRA3 { get; set; } = string.Empty;
        public int USUARIO_INS { get; set; }
        public DateTime FECHA_INS { get; set; }
        public int USUARIO_UPD { get; set; }
        public DateTime FECHA_UPD { get; set; }
        public int CODIGO_EMPRESA { get; set; }
        public string STATUS { get; set; } = string.Empty;
        public int IDENTIFICACION_ID { get; set; }
        public int NUMERO_IDENTIFICACION { get; set; }



    }
}

