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
        public string Extra3 { get; set; } = string.Empty;
        public int UsuarioIns { get; set; }
        public DateTime FechaIns { get; set; }
        public int UsuarioUpd { get; set; }
        public DateTime FechaUpd { get; set; }
        public int CodigoEmpresa { get; set; }

    }
}

