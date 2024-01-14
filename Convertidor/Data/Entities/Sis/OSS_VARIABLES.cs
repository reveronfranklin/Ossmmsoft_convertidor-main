namespace Convertidor.Data.Entities.Sis
{
	public class OSS_VARIABLES
	{
		public int ID { get; set; }
		public string CODE { get; set; }=String.Empty;
		public string DESCRIPCION { get; set; }=String.Empty;
		public int LONGITUD { get; set; }
		public int LONGITUD_REDONDEO { get; set; }
		public int LONGITUD_TRUNCADO { get; set; }
		public int LONGITUD_DECIMAL { get; set; }
        public int? USUARIO_INS { get; set; }
        public DateTime? FECHA_INS { get; set; }
        public int? USUARIO_UPD { get; set; }
        public DateTime? FECHA_UPD { get; set; }
        public int CODIGO_EMPRESA { get; set; }
        public int MODULO_ID { get; set; }
        public int ES_SQL { get; set; }

    }
}

