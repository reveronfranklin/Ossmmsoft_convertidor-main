using System.ComponentModel.DataAnnotations;

namespace Convertidor.Data.Entities.ADM
{
	public class ADM_PROVEEDORES
	{


        [Key]
        public int CODIGO_PROVEEDOR  { get; set; }	
        public string NOMBRE_PROVEEDOR  { get; set; } = String.Empty;
        public int TIPO_PROVEEDOR_ID  { get; set; }	
        public string NACIONALIDAD  { get; set; } = String.Empty;
        public int CEDULA  { get; set; }	
        public string RIF  { get; set; } = String.Empty;
        public DateTime FECHA_RIF  { get; set; }	 
        public string NIT  { get; set; } = String.Empty;
        public DateTime FECHA_NIT  { get; set; }	
        public string NUMERO_REGISTRO_CONTRALORIA  { get; set; } = String.Empty;
        public DateTime FECHA_REGISTRO_CONTRALORIA  { get; set; }	
        public decimal CAPITAL_PAGADO  { get; set; }	
        public decimal CAPITAL_SUSCRITO  { get; set; }	 
        public int TIPO_IMPUESTO_ID  { get; set; }	
        public string STATUS  { get; set; } = String.Empty;
        public int CODIGO_PERSONA  { get; set; }	
        public int CODIGO_AUXILIAR_GASTO_X_PAGAR  { get; set; }
        public int CODIGO_AUXILIAR_ORDEN_PAGO  { get; set; }
        public int ESTATUS_FISCO_ID  { get; set; }
        public string NUMERO_CUENTA  { get; set; } = String.Empty;
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

