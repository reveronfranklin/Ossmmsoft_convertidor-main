using System;
namespace Convertidor.Data.Entities.RentasMunicipales
{
	public class RM_CONTRIBUYENTES
	{
		

        public int CODIGO_CONTRIBUYENTE { get; set; }

        public int CODIGO_CONTRIBUYENTE_FK { get; set; }

        public int IDENTIFICACION_ID { get; set; }

        public int NUMERO_IDENTIFICACION { get; set; }

        public int NIT { get; set; }

        public string NOMBRE_RAZON_SOCIAL { get; set; } = string.Empty;

        public string APELLIDO_ACRONIMO { get; set; } = string.Empty;

        public DateTime FECHA_INGRESO { get; set; }

        public int NACIONALIDAD_ID { get; set; }

        public int SEXO_ID { get; set; }

        public int ESTADO_CIVIL_ID { get; set; }

        public string REGISTRO_MERCANTIL { get; set; } = string.Empty;

        public int APARTADO_POSTAL { get; set; }

        public int CODIGO_POSTAL { get; set; }

        public int CONTRIBUYE { get; set; }

        public int ESTATUS_ID { get; set; }


        public string EXTRA1 { get; set; } = string.Empty;

        public string EXTRA2 { get; set; } = string.Empty;

        public string EXTRA3 { get; set; } = string.Empty;

        public int USUARIO_INS { get; set; }

        public DateTime FECHA_INS { get; set; }

        public int USUARIO_UPD { get; set; }

        public DateTime FECHA_UPD { get; set; }

        public int CODIGO_EMPRESA { get; set; }

        public DateTime FECHA_NACIMIENTO { get; set; }

        public string NUMERO_IDENTIFICACION_PN { get; set; } = string.Empty;

        public string EXTRA4 { get; set; } = string.Empty;

        public string EXTRA5 { get; set; } = string.Empty;

        public string EXTRA6 { get; set; } = string.Empty;

        public string EXTRA7 { get; set; } = string.Empty;

        public string EXTRA8 { get; set; } = string.Empty;

        public string EXTRA9 { get; set; } = string.Empty;

        public string EXTRA10 { get; set; } = string.Empty;

        public string EXTRA11 { get; set; } = string.Empty;

        public string EXTRA13 { get; set; } = string.Empty;

        public string EXTRA14 { get; set; } = string.Empty; 

        public string EXTRA15 { get; set; } = string.Empty;

        public string TIPO_TRANSACCION { get; set; } = string.Empty;

    }
}

