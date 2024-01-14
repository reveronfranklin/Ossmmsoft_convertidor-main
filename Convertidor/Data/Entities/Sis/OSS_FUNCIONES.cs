namespace Convertidor.Data.Entities.Sis
{
	public class OSS_FUNCIONES
	{
		public int ID { get; set; }
		public string FUNCION { get; set; }=String.Empty;
		public string DESCRIPCION { get; set; }=String.Empty;
        public int? USUARIO_INS { get; set; }
        public DateTime? FECHA_INS { get; set; }
        public int? USUARIO_UPD { get; set; }
        public DateTime? FECHA_UPD { get; set; }
        public int CODIGO_EMPRESA { get; set; }
        public int MODULO_ID { get; set; }
        public int ES_SQL { get; set; }

    }
}

