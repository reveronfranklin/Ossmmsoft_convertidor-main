namespace Convertidor.Data.Entities.Sis
{
	public class OSS_FORMULAS
	{
		public int ID { get; set; }
		public int ID_VARIABLE { get; set; }
		public string CODE_VARIABLE { get; set; }=String.Empty;
		public string FORMULA { get; set; }=String.Empty;
		public string FORMULA_DESCRIPCION { get; set; }=String.Empty;
		public int ORDEN_CALCULO { get; set; }
		public string CODIGO_EXTERNO { get; set; }=String.Empty;
        public int? USUARIO_INS { get; set; }
        public DateTime? FECHA_INS { get; set; }
        public int? USUARIO_UPD { get; set; }
        public DateTime? FECHA_UPD { get; set; }
        public int CODIGO_EMPRESA { get; set; }
        public int MODULO_ID { get; set; }
    }
}

