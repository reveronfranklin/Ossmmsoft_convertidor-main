namespace Convertidor.Data.Entities.Bm
{
	public class BM_SOL_MOV_BIENES
    {
        public int CODIGO_MOV_BIEN { get; set; }
        public int CODIGO_BIEN { get; set; }
        public string TIPO_MOVIMIENTO { get; set; } = string.Empty;
        public DateTime FECHA_MOVIMIENTO { get; set; }
        public int CODIGO_DIR_BIEN { get; set; }
        public string EXTRA1 { get; set; } = string.Empty;
        public string EXTRA2 { get; set; } = string.Empty;
        public string EXTRA3 { get; set; } = string.Empty;
        public int USUARIO_INS { get; set; }
        public DateTime FECHA_INS { get; set; }
        public int USUARIO_UPD { get; set; }
        public DateTime FECHA_UPD { get; set; } 
        public int CODIGO_EMPRESA { get; set; }
        public int CONCEPTO_MOV_ID { get; set; }
        public int CODIGO_SOL_MOV_BIEN { get; set; }
        public string NUMERO_SOLICITUD { get; set; } = string.Empty;
        public int APROBADO { get; set; }
        public int USUARIO_SOLICITA { get; set; }
        public DateTime FECHA_SOLICITA { get; set; }









    }
}

