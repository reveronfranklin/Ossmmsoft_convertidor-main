using System;
namespace Convertidor.Data.Entities.Rh
{

    /*
     
    


CODIGO_CARGO
CARGO_CODIGO
CODIGO_ICP
CODIGO_ICP_UBICACION
SUELDO
DESCRIPCION_CARGO
CODIGO_TIPO_NOMINA
TIPO_NOMINA
FRECUENCIA_PAGO_ID
CODIGO_SECTOR
CODIGO_PROGRAMA
TIPO_CUENTA_ID
DESCRIPCION_TIPO_CUENTA
BANCO_ID
DESCRIPCION_BANCO
NO_CUENTA
EXTRA1
EXTRA2
EXTRA3
USUARIO_INS
FECHA_INS
USUARIO_UPD
FECHA_UPD
CODIGO_PERIODO
FECHA_NOMINA
FECHA_INGRESO
FECHA_NOMINA_MOV
COMPLEMENTO
TIPO
MONTO
ESTATUS_MOV
CODIGO
DENOMINACION
     */
    public class RH_V_HISTORICO_MOVIMIENTOS
	{
        public int CODIGO_PERSONA { get; set; }
        public int CEDULA { get; set; }
        public string FOTO { get; set; } = string.Empty;
        public string NOMBRE { get; set; } = string.Empty;
        public string APELLIDO { get; set; } = string.Empty;
        public string NACIONALIDAD { get; set; } = string.Empty;
        public string DESCRIPCION_NACIONALIDAD { get; set; } = string.Empty;
        public string SEXO { get; set; } = string.Empty;
        public string STATUS { get; set; } = string.Empty;
        public string DESCRIPCION_STATUS { get; set; } = string.Empty;
        public string DESCRIPCION_SEXO { get; set; } = string.Empty;
        public int CODIGO_RELACION_CARGO { get; set; }
        public int CODIGO_CARGO { get; set; }
        public string CARGO_CODIGO { get; set; } = string.Empty;
        public int CODIGO_ICP { get; set; }
        public int CODIGO_ICP_UBICACION { get; set; }
        public decimal SUELDO { get; set; }
        public string DESCRIPCION_CARGO { get; set; } = string.Empty;
        public int CODIGO_TIPO_NOMINA { get; set; }
        public string TIPO_NOMINA { get; set; } = string.Empty;
        public int FRECUENCIA_PAGO_ID { get; set; }
        public string CODIGO_SECTOR { get; set; } = string.Empty;
        public string CODIGO_PROGRAMA { get; set; } = string.Empty;
        public int TIPO_CUENTA_ID { get; set; }
        public string DESCRIPCION_TIPO_CUENTA { get; set; } = string.Empty;
        public int BANCO_ID { get; set; }
        public string DESCRIPCION_BANCO { get; set; } = string.Empty;
        public string NO_CUENTA { get; set; } = string.Empty;
        public string EXTRA1 { get; set; } = string.Empty;
        public string EXTRA2 { get; set; } = string.Empty;
        public string EXTRA3 { get; set; } = string.Empty;
        public int USUARIO_INS { get; set; }
        public DateTime FECHA_INS { get; set; }
        public int USUARIO_UPD { get; set; }
        public DateTime FECHA_UPD { get; set; }
        public int CODIGO_PERIODO { get; set; }
        public DateTime FECHA_NOMINA { get; set; }
        public DateTime FECHA_INGRESO { get; set; }
        public DateTime FECHA_NOMINA_MOV { get; set; }
        public string COMPLEMENTO  { get; set; }
        public string TIPO { get; set; }
        public decimal MONTO { get; set; }
        public string ESTATUS_MOV { get; set; }
        public string CODIGO { get; set; }
        public string DENOMINACION { get; set; }

        



    }
}

