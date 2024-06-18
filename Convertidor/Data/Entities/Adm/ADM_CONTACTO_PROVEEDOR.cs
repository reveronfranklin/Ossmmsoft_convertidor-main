using System.ComponentModel.DataAnnotations;

namespace Convertidor.Data.Entities.ADM
{
	public class ADM_CONTACTO_PROVEEDOR
	{
		
        [Key]
        public int CODIGO_CONTACTO_PROVEEDOR  { get; set; } 
        public int CODIGO_PROVEEDOR  { get; set; } 
        public string NOMBRE { get; set; } = string.Empty;
        public string APELLIDO { get; set; } = string.Empty;
        public int IDENTIFICACION_ID  { get; set; } 
        public string IDENTIFICACION { get; set; } = string.Empty;
        public string SEXO { get; set; } = string.Empty;
        public int TIPO_CONTACTO_ID  { get; set; } 
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

