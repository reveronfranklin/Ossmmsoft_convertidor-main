﻿namespace Convertidor.Data.Entities.Catastro
{
    public class CAT_CALC_X_TRIANGULACION
    {
        public int CODIGO_TRIANGULACION { get; set; }
        public int CODIGO_FICHA { get; set; }
        public int CODIGO_AVALUO_CONSTRUCCION { get; set; }
        public decimal CATETO_A { get; set; }
        public decimal CATETO_B { get; set; }
        public decimal CATETO_C { get; set; }
        public decimal AREA_PARCIAL { get; set; }
        public decimal AREA_COMPLEMENTARIA { get; set; }
        public string OBSERVACIONES { get; set; } = string.Empty;
        public string EXTRA1 { get; set; } = string.Empty;
        public string EXTRA2 { get; set; } = string.Empty;
        public string EXTRA3 { get; set; } = string.Empty;
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
        public int USUARIO_INS { get; set; }
        public DateTime FECHA_INS { get; set; }
        public int USUARIO_UPD { get; set; }
        public DateTime FECHA_UPD { get; set; }
        public int CODIGO_EMPRESA { get; set; }
    }
}
