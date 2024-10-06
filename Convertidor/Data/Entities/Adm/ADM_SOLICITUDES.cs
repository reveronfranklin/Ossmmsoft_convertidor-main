namespace Convertidor.Data.Entities.ADM
{
	public class ADM_SOLICITUDES
	{
        public int CODIGO_SOLICITUD { get; set; }	
        public int? ANO { get; set; }
        public string NUMERO_SOLICITUD { get; set; } = string.Empty;	
        public DateTime FECHA_SOLICITUD { get; set; } 
        public int CODIGO_SOLICITANTE { get; set; }
        public int? TIPO_SOLICITUD_ID { get; set; }
        public int? CODIGO_PROVEEDOR { get; set; }
        public string? MOTIVO { get; set; } = string.Empty;
        public string? NOTA { get; set; } = string.Empty;
        public string? STATUS { get; set; } = string.Empty;
        public string? EXTRA1 { get; set; } = string.Empty;
        public string? EXTRA2 { get; set; } = string.Empty;
        public string? EXTRA3 { get; set; } = string.Empty;
        public int? USUARIO_INS { get; set; }
        public DateTime? FECHA_INS { get; set; }
        public int? USUARIO_UPD { get; set; }
        public DateTime? FECHA_UPD { get; set; }
        public int? CODIGO_EMPRESA { get; set; }
        public int? CODIGO_PRESUPUESTO { get; set; }
        
        public string? SEARCH_TEXT { get; set; } = string.Empty;
        public string? MONTO_LETRAS { get; set; } = string.Empty;
        public string? FIRMANTE { get; set; } = string.Empty;
        
     

    }
}

