namespace Convertidor.Data.Entities.Sis
{
	public class OSS_USUARIO_ROL
	{
        public int CODIGO_USUARIO_ROL { get; set; }
        public int CODIGO_USUARIO { get; set; }
        public string USUARIO { get; set; }
        public string DESCRIPCION { get; set; } = string.Empty;
        public string JSON_MENU { get; set; } = string.Empty;
    }
}

