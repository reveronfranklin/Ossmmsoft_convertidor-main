namespace Convertidor.Data.Entities
{
    public class PRE_INDICE_CAT_PRG 
    {
        public int CODIGO_ICP { get; set; }
        public int ANO { get; set; }
        public int ESCENARIO { get; set; }
        public string CODIGO_SECTOR { get; set; } = string.Empty;
        public string CODIGO_PROGRAMA { get; set; } = string.Empty;
        public string CODIGO_SUBPROGRAMA { get; set; } = string.Empty;
        public string CODIGO_PROYECTO { get; set; } = string.Empty;
        public string CODIGO_ACTIVIDAD { get; set; } = string.Empty;
        public string DENOMINACION { get; set; } = string.Empty;
        public string UNIDAD_EJECUTORA { get; set; } = string.Empty;
        public string DESCRIPCION { get; set; } = string.Empty;
        public int CODIGO_FUNCIONARIO { get; set; }
        public DateTime FECHA_INI { get; set; }
        public DateTime FECHA_FIN { get; set; }
        public string EXTRA1 { get; set; } = string.Empty;
        public string EXTRA2 { get; set; } = string.Empty;
        public string EXTRA3 { get; set; } = string.Empty;
        public int USUARIO_INS { get; set; }
        public DateTime FECHA_INS { get; set; }
        public int USUARIO_UPD { get; set; }
        public DateTime FECHA_UPD { get; set; }
        public int CODIGO_EMPRESA { get; set; }
        public string CODIGO_OFICINA { get; set; } = string.Empty;
        public int CODIGO_PRESUPUESTO { get; set; }


    }
}
