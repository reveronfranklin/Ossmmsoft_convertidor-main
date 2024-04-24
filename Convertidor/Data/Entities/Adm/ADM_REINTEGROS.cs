namespace Convertidor.Data.Entities.ADM
{
	public class ADM_REINTEGROS
    {
        public int CODIGO_REINTEGRO { get; set; }	
        public int ANO { get; set; }
        public int CODIGO_COMPROMISO { get; set; } 
        public DateTime FECHA_REINTEGRO { get; set; }
        public string NUMERO_REINTEGRO { get; set; } = string.Empty;
        public int CODIGO_CUENTA_BANCO { get; set; }
        public string NUMERO_DEPOSITO { get; set; } = string.Empty;
        public DateTime FECHA_DEPOSITO { get; set; }
        public string MOTIVO { get; set; } = string.Empty;
        public string EXTRA1 { get; set; } = string.Empty;
        public string EXTRA2 { get; set; } = string.Empty;
        public string EXTRA3 { get; set; } = string.Empty;
        public string STATUS { get; set; }=string.Empty;
        public int USUARIO_INS { get; set; }
        public DateTime FECHA_INS { get; set; }
        public int USUARIO_UPD { get; set; }
        public DateTime FECHA_UPD { get; set; }
        public int CODIGO_EMPRESA { get; set; }
        public int CODIGO_PRESUPUESTO { get; set; }
        public int ORIGEN_COMPROMISO_ID { get; set; }


    }
}

