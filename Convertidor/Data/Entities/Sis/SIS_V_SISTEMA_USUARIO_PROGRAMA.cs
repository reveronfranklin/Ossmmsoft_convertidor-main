using System;
namespace CConvertidor.Data.Entities.Sis
{
	public class SIS_V_SISTEMA_USUARIO_PROGRAMA
	{
        public int CODIGO_EMPRESA { get; set; }
        public int CODIGO_USUARIO { get; set; }
        public string SISTEMA { get; set; } = string.Empty;
        public string DESCRIPCION { get; set; } = string.Empty;
  
    }
}

