namespace Convertidor.Data.Entities.Rh
{

    public class RH_V_HISTORICO_MOVIMIENTOS
	{




        public int CODIGO_HISTORICO_NOMINA { get; set; }
        public int CODIGO_PERSONA { get; set; }
        public int CODIGO_TIPO_NOMINA { get; set; }
        public int CODIGO_EMPRESA { get; set; }
        public int CODIGO_RELACION_CARGO { get; set; }
        public int CODIGO_CARGO { get; set; }
        public string CARGO_CODIGO { get; set; } = string.Empty;
        public int CODIGO_ICP { get; set; }
        public string UNIDAD_EJECUTORA { get; set; } = string.Empty;
        //public string CODIGO_SECTOR { get; set; } = string.Empty;
        //public string CODIGO_PROGRAMA { get; set; } = string.Empty;
        public int TIPO_CUENTA_ID { get; set; }
        public string DESCRIPCION_TIPO_CUENTA { get; set; } = string.Empty;
        public int BANCO_ID { get; set; }
        public string DESCRIPCION_BANCO { get; set; } = string.Empty;
        public string NO_CUENTA { get; set; } = string.Empty;
        public int CEDULA { get; set; }
        public string FOTO { get; set; } = string.Empty;
        public string NOMBRE { get; set; } = string.Empty;
        public string APELLIDO { get; set; } = string.Empty;
        public string NACIONALIDAD { get; set; } = string.Empty;
        public string DESCRIPCION_NACIONALIDAD { get; set; } = string.Empty;
        public string SEXO { get; set; } = string.Empty;
        public string ESTADO_CIVIL { get; set; } = string.Empty;
        
        public string STATUS { get; set; } = string.Empty;
        public string DESCRIPCION_STATUS { get; set; } = string.Empty;
 
        public decimal SUELDO { get; set; }
        public string DESCRIPCION_CARGO { get; set; } = string.Empty;
        public string TIPO_NOMINA { get; set; } = string.Empty;
        public DateTime FECHA_NOMINA_MOV { get; set; }
        public string COMPLEMENTO { get; set; } = string.Empty;
        public string TIPO { get; set; } = string.Empty;
        public decimal MONTO { get; set; } 
        public string ESTATUS_MOV { get; set; } = string.Empty;
        public string CODIGO { get; set; } = string.Empty;
        public string DENOMINACION { get; set; } = string.Empty;
        public int PERIODO { get; set; }
        public int CODIGO_PERIODO { get; set; }
        public int CODIGO_CONCEPTO { get; set; }
        









    }
}

