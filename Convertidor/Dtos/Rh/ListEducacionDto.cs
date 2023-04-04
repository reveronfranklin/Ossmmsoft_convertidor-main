using System;
namespace Convertidor.Dtos.Rh
{
	public class ListEducacionDto
	{
        public int CodigoEducacion { get; set; }
        public int CodigoPersona { get; set; }
        public int NivelId { get; set; }
        public string DescripcionNivel { get; set; } = string.Empty;
        public string NombreInstituto { get; set; } = string.Empty;
        public string LocalidadInstituto { get; set; } = string.Empty;
        public int ProfesionId { get; set; }
        public string DescripcionProfesion { get; set; } = string.Empty;
        public DateTime FechaIni { get; set; }
        public DateTime FechaFin { get; set; }
        public int UltimoAnoAprobado { get; set; }
        public string Graduado { get; set; } = string.Empty;
        public int TituloId { get; set; }
        public string DescripcionTitulo { get; set; } = string.Empty;
        public int MencionEspecialidadId { get; set; }
        public string DescripcionMencionEspecialidad { get; set; } = string.Empty;
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
     
        public int CodigoEmpresa { get; set; }
    }
}

