using Npgsql.EntityFrameworkCore.PostgreSQL.Storage.Internal.Mapping;

namespace Convertidor.Data.Entities.Catastro
{
    public class CAT_DIRECCIONES
    {
        public int CODIGO_DIRECCION { get; set; }
        public int CODIGO_CONTRIBUYENTE { get; set; }
        public int CODIGO_CUENTA { get; set; }
        public int CODIGO_INMUEBLE { get; set; }
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
        public string VIALIDAD { get; set; }=string.Empty;
        public int TIPO_VIVIENDA_ID { get; set; }
        public string VIVIENDA { get; set; }
        public int TIPO_NIVEL_ID { get; set; }
        public string NIVEL { get; set; }
        public int TIPO_UNIDAD_ID { get; set; }
        public string NUMERO_UNIDAD { get; set; } = string.Empty;
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
        public string TIPO_TRANSACCION { get; set; } = string.Empty;
        public string EXTRA4 { get; set; } = string.Empty;
        public string EXTRA5 { get; set; } = string.Empty;
        public string EXTRA6 { get; set; } = string.Empty;
        public string EXTRA7 { get; set; } = string.Empty;
        public string EXTRA8 { get; set; } = string.Empty;
        public string EXTRA9 { get; set; } = string.Empty;
        public string EXTRA10 { get; set; } = string.Empty;
        public string EXTRA11 { get; set; } = string.Empty;
        public string EXTRA12 { get; set; } = string.Empty;
        public string EXTRA13 { get; set; } = string.Empty;
        public string EXTRA14 { get; set; } = string.Empty;
        public string EXTRA15 { get; set; } = string.Empty;
        public int CODIGO_FICHA { get; set; }
    }
}
