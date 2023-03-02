using System.ComponentModel.DataAnnotations;

namespace Convertidor.Data.EntitiesDestino
{
    public class HistoricoRetenciones
    {
        [Key]
        public long CODIGO_HISTORICO_NOMINA { get; set; }
        public string Titulo { get; set; }
        public int TIPO_NOMINA { get; set; }
        public string UNIDAD_EJECUTORA { get; set; } = string.Empty;
        public string DESCRIPCION_CARGO { get; set; } = string.Empty;
        public string NACIONALIDAD { get; set; }
        public int CEDULA { get; set; }
        public string NOMBRE { get; set; }
        public string APELLIDO { get; set; }
        public DateTime FECHA_NOMINA { get; set; }
        public DateTime FECHA_INGRESO { get; set; }
        public decimal MONTO { get; set; }
        public decimal SUELDO { get; set; }

        public DateTime FECHA_INS { get; set; }

        /* TABLAS:
           RH.RH_HISTORICO_PERSONAL_CARGO RVPC
           RH.RH_HISTORICO_NOMINA RMN
           PRE.PRE_INDICE_CAT_PRG PICP
           'FAOV' RETENCION ==> Titulo
           ,RVPC.TIPO_NOMINA 
           ,PICP.UNIDAD_EJECUTORA 
           ,RVPC.DESCRIPCION_CARGO 
           ,RVPC.NACIONALIDAD 
           ,RVPC.CEDULA 
           ,RVPC.NOMBRE 
           ,RVPC.APELLIDO 
           ,TO_CHAR(RVPC.FECHA_INGRESO,'DD/MM/RRRR') FECHA_INGRESO  
           ,TO_CHAR(RVPC.FECHA_NOMINA,'DD/MM/RRRR') FECHA_NOMINA  
           ,RMN.MONTO MONTO_RETENCION
           , RVPC.SUELDO*/

    }
}
