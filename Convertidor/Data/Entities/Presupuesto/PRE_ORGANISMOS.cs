namespace Convertidor.Data.Entities.Presupuesto
{
    public class PRE_ORGANISMOS
    {
        public int CODIGO_ORGANISMO { get; set; }
        public int ANO { get; set; }
        public string DENOMINACION { get; set; }=string.Empty;
        public string NUMERO_REGISTRO { get; set; } = string.Empty;
        public string ACTIVIDAD { get; set; } = string.Empty;
        public string UBICACION_GEOGRAFICA { get; set; } = string.Empty;
        public int TIPO_ORGANISMO_ID { get; set; }
        public int CAPITAL_SOCIAL { get; set; }
        public int MONTO1 { get; set; }
        public int MONTO2 { get; set; }
        public int MONTO4 { get; set; }
        public string EXTRA1 { get; set; } = string.Empty;
        public string EXTRA2 { get; set; } = string.Empty;
        public string EXTRA3 { get; set; } = string.Empty;
        public int USUARIO_INS { get; set; }
        public DateTime FECHA_INS { get; set; }
        public int USUARIO_UPD { get; set; }
        public DateTime FECHA_UPD { get; set; }
        public int CODIGO_EMPRESA { get; set; }
        public int MONTO3 { get; set; }
        public int CODIGO_PRESUPUESTO { get; set; }

    }
}
