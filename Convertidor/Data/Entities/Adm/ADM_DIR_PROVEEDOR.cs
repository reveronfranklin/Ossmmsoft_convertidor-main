using System.ComponentModel.DataAnnotations;

namespace Convertidor.Data.Entities.ADM
{
	public class ADM_DIR_PROVEEDOR
	{
		
        [Key]
        public int CODIGO_DIR_PROVEEDOR  { get; set; } 
        public int CODIGO_PROVEEDOR  { get; set; } 
        public int TIPO_DIRECCION_ID  { get; set; } 
        public int PAIS_ID  { get; set; } 
        public int ESTADO_ID  { get; set; } 
        public int MUNICIPIO_ID  { get; set; } 
        public int CIUDAD_ID  { get; set; } 
        public int PARROQUIA_ID  { get; set; } 
        public int SECTOR_ID  { get; set; } 
        public int URBANIZACION_ID  { get; set; } 
        public int MANZANA_ID  { get; set; } 
        public int PARCELA_ID  { get; set; } 
        public int VIALIDAD_ID  { get; set; } 
        public string VIALIDAD  { get; set; } = String.Empty;
        public int TIPO_VIVIENDA_ID  { get; set; } 
        public string VIVIENDA  { get; set; } = String.Empty;
        public int TIPO_NIVEL_ID  { get; set; } 
        public string NIVEL  { get; set; } = String.Empty;
        public int TIPO_UNIDAD_ID  { get; set; } 
        public string NUMERO_UNIDAD  { get; set; } = String.Empty;
        public string COMPLEMENTO_DIR  { get; set; } = String.Empty;   
        public int TENENCIA_ID  { get; set; } 
        public int CODIGO_POSTAL  { get; set; } 
        public int PRINCIPAL  { get; set; }    
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

