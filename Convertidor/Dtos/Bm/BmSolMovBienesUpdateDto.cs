using System;
namespace Convertidor.Dtos.Bm
{
	public class BmSolMovBienesUpdateDto
	{

        public int CodigoMovBien { get; set; }
        public int CodigoBien { get; set; }
        public string TipoMovimiento { get; set; } = string.Empty;
        public DateTime FechaMovimiento { get; set; }
        public int CodigoDirBien { get; set; }
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
        public int ConceptoMovId { get; set; }
        public int CodigoSolMovBien { get; set; }
        public string NumeroSolicitud { get; set; } = string.Empty;
        public int Aprobado { get; set; }
        public int UsuarioSolicita { get; set; }
        public DateTime FechaSolicita { get; set; }


    }
}

