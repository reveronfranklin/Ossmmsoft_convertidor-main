﻿namespace Convertidor.Dtos.Bm
{
	public class BmMovBienesResponseDto
	{

        public int CodigoMovBien { get; set; }
        public int CodigoBien { get; set; }
        public string TipoMovimiento { get; set; } = string.Empty;
        public DateTime FechaMovimiento { get; set; }
        public string FechaMovimientoString { get; set; }
        public FechaDto FechaMovimientoObj { get; set; }
        public int CodigoDirBien { get; set; } 
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
        public int ConceptoMovId { get; set; }
        public int CodigoSolMovBien { get; set; }


    }
}

