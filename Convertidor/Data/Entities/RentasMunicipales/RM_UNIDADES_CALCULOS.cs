namespace Convertidor.Data.Entities.RentasMunicipales
{
	public class RM_UNIDADES_CALCULOS
	{
        public long CODIGO_UNIDAD_CALCULO { get; set; }

        public long UNIDAD_CALCULO_ID { get; set; }

        public decimal Monto { get; set; }

        public string NUMERO_GACETA_OFICIAL { get; set; } = string.Empty;

        public DateTime FECHA_GACETA_OFICIAL { get; set; }

        public string EXTRA1 { get; set; } = string.Empty;

        public string EXTRA2 { get; set; } = string.Empty;

        public string EXTRA3 { get; set; } = string.Empty;

        public long USUARIO_INS { get; set; }

        public DateTime FECHA_INS { get; set; }

        public long USUARIO_UPD { get; set; }

        public DateTime FECHA_UPD { get; set; }

        public long CODIGO_EMPRESA { get; set; }

        public DateTime FECHA_INICIO { get; set; }

        public DateTime FECHA_FIN { get; set; }


    }
}

