using Org.BouncyCastle.Bcpg.OpenPgp;

namespace Convertidor.Data.Entities.Presupuesto
{
    public class PRE_DIRECTIVOS
    {
        public int CODIGO_DIRECTIVO { get; set; }
        public int CODIGO_IDENTIFICACION { get; set; }
        public int TIPO_DIRECTIVO_ID { get; set; }
        public int TITULO_ID { get; set; }
        public int CEDULA { get; set; }
        public string NOMBRE { get; set; } = string.Empty;
        public string APELLIDO { get; set; } = string.Empty;
        public string CARGO { get; set; } = string.Empty;
        public string EXTRA1 { get; set; } = string.Empty;
        public string EXTRA2 { get; set; } = string.Empty;
        public string EXTRA3 { get; set; } = string.Empty;
        public int USUARIO_INS { get; set; }
        public DateTime FECHA_INS { get; set; }
        public int USUARIO_UPD { get; set; }
        public DateTime FECHA_UPD { get; set; }
        public int CODIGO_EMPRESA { get; set; }
        public int CODIGO_PRESUPUESTO { get; set; }

    }
}
