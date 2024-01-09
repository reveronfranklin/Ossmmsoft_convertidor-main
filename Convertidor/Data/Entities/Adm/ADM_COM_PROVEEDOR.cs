using System.ComponentModel.DataAnnotations;

namespace Convertidor.Data.Entities.ADM
{
	public class ADM_COM_PROVEEDOR
	{
		
        [Key]
        public int CODIGO_COM_PROVEEDOR  { get; set; } 
        public int CODIGO_PROVEEDOR  { get; set; } 
        public int TIPO_COMUNICACION_ID  { get; set; } 
        public string CODIGO_AREA { get; set; } = string.Empty;
        public string LINEA_COMUNICACION { get; set; } = string.Empty;
        public int EXTENSION  { get; set; } 
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

