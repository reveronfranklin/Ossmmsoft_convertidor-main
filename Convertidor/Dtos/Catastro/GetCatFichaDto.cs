using System;
namespace Convertidor.Dtos.Catastro
{
	public class GetCatFichaDto
	{

             public int CODIGO_FICHA { get; set; }

        public string CODIGO_VIEJO_CAT { get; set; } = string.Empty;

        public string CODIGO_PARCELA { get; set; } = string.Empty;

        public int CODIGO_INMUEBLE { get; set; }

        public int ESTATUS_FICHA_ID { get; set; }


        public int AMBITO_ID { get; set; }

        public int MANZANA_ID { get; set; }

        public int SUB_SECTOR_ID { get; set; }

        public int PARCELA_ID { get; set; }

        public int SUB_PARCELA_ID { get; set; }

        public int NIVEL_ID { get; set; }

        public int UNIDAD_ID { get; set; }

        public int FICHA_CATASTRAL_NUM { get; set; }

        public DateTime FECHA_PRIMERA_VISITA { get; set; }

        public DateTime FECHA_LEVANTAMIENTO { get; set; }

        public int CONTROL_ARCHIVO { get; set; }

        public string INSCRIPCION_CATASTRAL_NUM { get; set; } = string.Empty;

        public DateTime FECHA_INSCRIPCION { get; set; }

        public int UBICACION_TERRENO { get; set; }

        public int TIPO_OPERACION_ID { get; set; }

        public string TIPO_TRANSACCION { get; set; } = string.Empty;

        public int CODIGO_ZONIFICACION { get; set; }

        public int AREA_PARCELA { get; set; }

        public int FRENTE_PARCELA { get; set; }

        public int AREA_INMUEBLE { get; set; }

        public int AREA_TIPO_PARCELA { get; set; }

        public int FRENTE_TIPO_PARCELA { get; set; }


        public string EXTRA1 { get; set; } = string.Empty;

        public string EXTRA2 { get; set; } = string.Empty;

        public string EXTRA3 { get; set; } = string.Empty;

        public int USUARIO_INS { get; set; }

        public DateTime FECHA_INS { get; set; }

      

        public int CODIGO_EMPRESA { get; set; }

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

        public string NUMERO_CIVICO { get; set; } = string.Empty;

        public DateTime FECHA_INS_HIST { get; set; }

        //Relate CAT_UBICACION_NAC
        public int CODIGO_UBICACION_NAC { get; set; }

        public int CODIGO_FICHA_FK { get; set; }

        public string CODIGO_CATASTRO { get; set; } = string.Empty;

    }
	
}

