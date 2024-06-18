namespace Convertidor.Data.Entities.Adm
{
    public class ADM_CONTRATOS
    {
        public int CODIGO_CONTRATO { get; set; }
        public int ANO { get; set; }
        public DateTime FECHA_CONTRATO { get; set; }
        public string NUMERO_CONTRATO { get; set; }=string.Empty;
        public int CODIGO_SOLICITANTE { get; set; }
        public int CODIGO_PROVEEDOR { get; set; }
        public DateTime FECHA_APROBACION { get; set; }
        public string NUMERO_APROBACION { get; set; } = string.Empty;
        public string OBRA { get; set; } = string.Empty;
        public string DESCRIPCION { get; set; } = string.Empty;
        public int PARROQUIA_ID { get; set; }
        public DateTime FECHA_INI_OBRA { get; set; }
        public DateTime FECHA_FIN_OBRA { get; set; }
        public int MONTO_CONTRATO { get; set; }
        public string EXTRA1 { get; set; } = string.Empty;
        public string EXTRA2 { get; set; } = string.Empty;
        public string EXTRA3 { get; set; } = string.Empty;
        public string STATUS { get; set; } = string.Empty;
        public int USUARIO_INS { get; set; }
        public DateTime FECHA_INS { get; set; }
        public int USUARIO_UPD { get; set; }
        public DateTime FECHA_UPD { get; set; }
        public int CODIGO_EMPRESA { get; set; }
        public int CODIGO_PRESUPUESTO { get; set; }
        public int MONTO_ORIGINAL { get; set; }
        public int TIPO_MODIFICACION_ID { get; set; }
    }
}
