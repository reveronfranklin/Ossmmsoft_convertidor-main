namespace Convertidor.Data.Entities.Adm
{
    public class ADM_SOL_COMPROMISO
    {
        public int CODIGO_SOL_COMPROMISO { get; set; }
        public int TIPO_SOL_COMPROMISO_ID { get; set; }
        public DateTime FECHA_SOLICITUD { get; set; }
        public string NUMERO_SOLICITUD { get; set; } = string.Empty;
        public int CODIGO_SOLICITANTE { get; set; }
        public int CODIGO_PROVEEDOR { get; set; }
        public string MOTIVO { get; set; } = string.Empty;
        public string STATUS { get; set; } = string.Empty;
        public int CODIGO_PRESUPUESTO { get; set; }
        public int ANO { get; set; }
        public string EXTRA1 { get; set; } = string.Empty;
        public string EXTRA2 { get; set; } = string.Empty;
        public string EXTRA3 { get; set; } = string.Empty;
        public int USUARIO_INS { get; set; }
        public DateTime FECHA_INS { get; set; }
        public int USUARIO_UPD { get; set; }
        public DateTime FECHA_UPD { get; set; }
        public int CODIGO_EMPRESA { get; set; }
    }
}
