using System;
using System.Net.NetworkInformation;

namespace Convertidor.Dtos.Rh
{
	public class RhEducacionResponseDto
    {
        public int CodigoEducacion { get; set; }
        public int CodigoPersona { get; set; }
        public int NivelId{ get; set; } 
        public string NombreInstituto { get; set; }
        public string LocalidadInstituto { get; set; } 
        public int ProfesionID { get; set; }
        public DateTime FechaIni { get; set; }
        public DateTime FechaFin { get; set; }
        public int UltimoAñoAprobado{ get; set; }
        public string Graduado { get; set; } 
        public int TituloId { get; set; }
        public int MencionEspecialidadId { get; set; }
        public string Extra1 { get; set; }
        public string Extra2 { get; set; }
        public string Extra3 { get; set; }
        public int USuarioIns { get; set; }
        public DateTime FechaIns { get; set; }
        public int UsuarioUpd { get; set; }
        public DateTime FechaUpd { get; set; }
        public int CodigoEmpresa { get; set; }

    }

}

