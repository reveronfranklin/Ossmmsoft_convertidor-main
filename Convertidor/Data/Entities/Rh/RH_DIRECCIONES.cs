namespace Convertidor.Data.Entities.Rh
{
	public class RH_DIRECCIONES
	{
        public int CODIGO_DIRECCION { get; set; }
        public int CODIGO_PERSONA { get; set; }
        public int DIRECCION_ID { get; set; }
        public int PAIS_ID { get; set; }
        public int ESTADO_ID { get; set; }
        public int MUNICIPIO_ID { get; set; }
        public int CIUDAD_ID { get; set; }
        public int PARROQUIA_ID { get; set; }
        public int SECTOR_ID { get; set; }
        public int URBANIZACION_ID { get; set; }
        public int MANZANA_ID { get; set; }
        public int PARCELA_ID { get; set; }
        public int VIALIDAD_ID { get; set; }
        public string VIALIDAD { get; set; } = string.Empty;
        public int TIPO_VIVIENDA_ID { get; set; }
        public int VIVIENDA_ID { get; set; }
        public string VIVIENDA { get; set; } = string.Empty;
        public int TIPO_NIVEL_ID { get; set; }
        public string NIVEL { get; set; } = string.Empty;
        public string NRO_VIVIENDA { get; set; } = string.Empty;
        public string COMPLEMENTO_DIR { get; set; } = string.Empty;
        public int TENENCIA_ID { get; set; }
        public int CODIGO_POSTAL { get; set; }
        public int PRINCIPAL { get; set; }
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

