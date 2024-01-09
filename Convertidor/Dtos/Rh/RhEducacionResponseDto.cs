namespace Convertidor.Dtos.Rh
{
	public class RhEducacionResponseDto
    {
        public int CodigoEducacion { get; set; }
        public int CodigoPersona { get; set; }
        public int NivelId { get; set; } 
        public string DescripcionNivel{ get; set; } 
        public string NombreInstituto { get; set; }
        public string LocalidadInstituto { get; set; } 
        public int ProfesionID { get; set; }
        public DateTime FechaIni { get; set; }
        public string FechaIniString { get; set; }
        public DateTime FechaFin { get; set; }
        public string FechaFinString { get; set; }
        
        public FechaDto FechaIniObj { get; set; }
        
        public FechaDto FechaFinObj { get; set; }
        public int UltimoAñoAprobado{ get; set; }
        public string Graduado { get; set; } 
        public int TituloId { get; set; }
        public int MencionEspecialidadId { get; set; }
       

    }

}

