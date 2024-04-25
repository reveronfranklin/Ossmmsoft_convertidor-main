namespace Convertidor.Data.Entities.Presupuesto
{
    public class PRE_NIVELES_PUC
    {
        public int CODIGO_GRUPO { get; set; }
        public string  NIVEL1 { get; set; }= string.Empty;
        public string NIVEL2 { get; set; }= string.Empty;
        public string NIVEL3 { get; set; } = string.Empty;
        public string NIVEL4 { get; set; }=string.Empty;
        public string NIVEL5 { get; set; } = string.Empty;
        public string NIVEL6 { get; set; } =string.Empty;
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
