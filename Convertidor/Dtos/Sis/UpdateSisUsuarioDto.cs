namespace Convertidor.Dtos.Sis
{
	public class UpdateSisUsuarioDto
	{
        public int CODIGO_USUARIO { get; set; }
        public string USUARIO { get; set; } = string.Empty;
        public string LOGIN { get; set; } = string.Empty;
        public Byte[]? PASSWORD { get; set; }
        public int CEDULA { get; set; }
        public int DEPARTAMENTO_ID { get; set; }
        public int CARGO_ID { get; set; }
        public int SISTEMA_ID { get; set; }
        public DateTime FECHA_INGRESO { get; set; }
        public DateTime FECHA_EGRESO { get; set; }
        public int PRIORIDAD { get; set; }
        public string STATUS { get; set; } = string.Empty;
        public string EXTRA1 { get; set; } = string.Empty;
        public string EXTRA2 { get; set; } = string.Empty;
        public string EXTRA3 { get; set; } = string.Empty;
        public int USUARIO_INS { get; set; }
        public DateTime FECHA_INS { get; set; }
        public int USUARIO_UPD { get; set; }
        public DateTime FECHA_UPD { get; set; }
        public int CODIGO_EMPRESA { get; set; }
        public int CODIGO_SUCURSAL { get; set; }
        public string EXTRA4 { get; set; } = string.Empty;
        public string EXTRA5 { get; set; } = string.Empty;
        public string EXTRA6 { get; set; } = string.Empty;
        public string EXTRA7 { get; set; } = string.Empty;
        public string EXTRA8 { get; set; } = string.Empty;
        public string EXTRA9 { get; set; } = string.Empty;
        public string EXTRA10 { get; set; } = string.Empty;
        public string EXTRA11 { get; set; } = string.Empty;
        public string EXTRA12 { get; set; } = string.Empty;
        public string EXTRA13 { get; set; } = string.Empty;
        public string EXTRA14 { get; set; } = string.Empty;
        public string EXTRA15 { get; set; } = string.Empty;

        public string PASSWORDSTRING { get; set; } = string.Empty;
        public string REFRESHTOKEN { get; set; } = string.Empty;
        public DateTime TOKENCREATED { get; set; }
        public DateTime TOKENEXPIRES { get; set; }
    }
}

