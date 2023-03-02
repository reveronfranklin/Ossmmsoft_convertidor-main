using System.ComponentModel.DataAnnotations;

namespace Convertidor.Data.Entities
{
    public class RH_HISTORICO_PERSONAL_CARGO
    {
        
        public int CODIGO_PERSONA { get; set; }
        public int CEDULA { get; set; }
        public string FOTO { get; set; } 
        public string NOMBRE { get; set; }
        public string APELLIDO { get; set; }
        public string NACIONALIDAD { get; set; }
        public string DESCRIPCION_NACIONALIDAD { get; set; }
        public string SEXO { get; set; }
        public string STATUS { get; set; }
        public string DESCRIPCION_STATUS   { get; set; }
        public int CODIGO_EMPRESA { get; set; }
        public string DESCRIPCION_SEXO { get; set; }
        public int CODIGO_RELACION_CARGO { get; set; }
        public int CODIGO_CARGO { get; set; }
        public string CARGO_CODIGO { get; set; }
        public int CODIGO_ICP { get; set; }
        public int CODIGO_ICP_UBICACION { get; set; }
        public decimal SUELDO { get; set; }
        public string DESCRIPCION_CARGO { get; set; }
        public int CODIGO_TIPO_NOMINA { get; set; }
        public string TIPO_NOMINA { get; set; }
        public int FRECUENCIA_PAGO_ID { get; set; }
        public string CODIGO_SECTOR { get; set; }
        public string CODIGO_PROGRAMA { get; set; }
        public int TIPO_CUENTA_ID { get; set; }
        public string DESCRIPCION_TIPO_CUENTA { get; set; }
        public int BANCO_ID { get; set; }
        public string DESCRIPCION_BANCO { get; set; }
        public string NO_CUENTA { get; set; }
        public string EXTRA1 { get; set; }
        public string EXTRA2 { get; set; }
        public string EXTRA3 { get; set; }
        public int USUARIO_INS { get; set; }
        public DateTime FECHA_INS { get; set; }
        public int USUARIO_UPD { get; set; }
        public DateTime FECHA_UPD { get; set; }
        public int CODIGO_PERIODO { get; set; }
        public DateTime FECHA_NOMINA { get; set; }
        public DateTime FECHA_INGRESO { get; set; }
       


    }
}
