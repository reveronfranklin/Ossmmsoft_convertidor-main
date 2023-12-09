using System;
namespace Convertidor.Dtos.Rh
{
	public class ListEducacionDto
	{
        public int CodigoEducacion { get; set; }
        public int CodigoPersona { get; set; }
        public int NivelId { get; set; }
        public string DescripcionNivel { get; set; } 
        public string NombreInstituto { get; set; }
        public string LocalidadInstituto { get; set; } 
        public int ProfesionId { get; set; }
        public string DescripcionProfesion { get; set; } 
        public DateTime FechaIni { get; set; }
        public DateTime FechaFin { get; set; }
        public int UltimoAnoAprobado { get; set; }
        public string Graduado { get; set; } 
        public int TituloId { get; set; }
        public string DescripcionTitulo { get; set; } 
        public int MencionEspecialidadId { get; set; }
        public string DescripcionMencionEspecialidad { get; set; } 
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
     
        public int CodigoEmpresa { get; set; }
    }
}

