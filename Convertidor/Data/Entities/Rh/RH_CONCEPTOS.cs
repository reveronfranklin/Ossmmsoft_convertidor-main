namespace Convertidor.Data.Entities.Rh
{
	public class RH_CONCEPTOS
	{

        public int CODIGO_CONCEPTO { get; set; }
        public string CODIGO { get; set; } = string.Empty;
        public int CODIGO_TIPO_NOMINA { get; set; }
        public string DENOMINACION { get; set; } = string.Empty;
        public string DESCRIPCION { get; set; } = string.Empty;
        public string TIPO_CONCEPTO { get; set; } = string.Empty;
        public int MODULO_ID { get; set; }
        public int CODIGO_PUC { get; set; }
        public string STATUS { get; set; } = string.Empty;
        public string EXTRA1 { get; set; } = string.Empty;
        public string EXTRA2 { get; set; } = string.Empty;
        public string EXTRA3 { get; set; } = string.Empty;
        public int USUARIO_INS { get; set; }
        public DateTime FECHA_INS { get; set; }
        public int USUARIO_UPD { get; set; }
        public DateTime FECHA_UPD { get; set; }
        public int CODIGO_EMPRESA { get; set; }
        public int FRECUENCIA_ID { get; set; }
        public int DEDUSIBLE { get; set; }
        public int AUTOMATICO { get; set; }
        
        //public int ID_MODELO_CALCULO { get; set; }



        

    }
}

