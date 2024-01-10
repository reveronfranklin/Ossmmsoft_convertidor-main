namespace Convertidor.Data.Entities.Rh
{
	public class RH_ADMINISTRATIVOS
	{

        public int CODIGO_ADMINISTRATIVO { get; set; }
        public int CODIGO_PERSONA { get; set; } 
        public int CARGO_ID { get; set; }
        public DateTime FECHA_INGRESO { get; set; }
        public string TIPO_PAGO { get; set; } = string.Empty;
        public int BANCO_ID { get; set; } 
        public int TIPO_CUENTA_ID { get; set; }
        public string NO_CUENTA { get; set; } = string.Empty;
        public string TURNO { get; set; } = string.Empty;
        public int PROFESION_ID { get; set; }
        public string STATUS { get; set; } = string.Empty;
        public int USUARIO_INS { get; set; }
        public DateTime FECHA_INS { get; set; }
        public int USUARIO_UPD { get; set; }
        public DateTime FECHA_UPD { get; set; }
        public int CODIGO_EMPRESA { get; set; }
        public string EXTRA1 { get; set; } = string.Empty;
        public string EXTRA2 { get; set; } = string.Empty;
        public string EXTRA3 { get; set; } = string.Empty;
        public string EXTRA4 { get; set; } = string.Empty;
      

    }
}

