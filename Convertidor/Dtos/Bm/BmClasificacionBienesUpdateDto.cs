using System;
namespace Convertidor.Dtos.Bm
{
	public class BmClasificacionBienesUpdateDto
	{

        public int CodigoClasificacionBien { get; set; }
        public string CodigoGrupo { get; set; } = string.Empty;
        public string CodigoNivel1 { get; set; }= string.Empty;
        public string CodigoNivel2 { get; set; } = string.Empty;
        public string CodigoNivel3 { get; set; }= string.Empty;
        public string Denominacion { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaIni { get; set; }
        public DateTime FechaFin { get; set; }
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
 

    }
}

