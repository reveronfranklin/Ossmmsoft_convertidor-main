using System;
using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;

namespace Convertidor.Dtos.Bm
{
	public class BmDirBienResponseDto
	{
        public int CodigoDirBien { get; set; }
        public int CodigoIcp { get; set; }
        public int PaisId { get; set; }
        public int EstadoId { get; set; }
        public int MunicipioId { get; set; }
        public int CiudadId { get; set; }
        public int ParroquiaId { get; set; }
        public int SectorId { get; set; }
        public int UrbanizacionId { get; set; }
        public int ManzanaId { get; set; }
        public int ParcelaId { get; set; }
        public int VialidadId { get; set; }
        public string Vialidad { get; set; } = string.Empty;
        public int TipoViviendaId { get; set; }
        public string Vivienda { get; set; } = string.Empty;
        public int TipoNivelId { get; set; }
        public string Nivel { get; set; } = string.Empty;
        public int TipoUnidadId { get; set; }
        public string NumeroUnidad { get; set; } = string.Empty;
        public string ComplementoDir { get; set; } = string.Empty;
        public int TenenciaId { get; set; }
        public int CodigoPostal { get; set; }
        public DateTime FechaIni { get; set; }
        public string FechaIniString { get; set; }
        public FechaDto FechaIniObj { get; set; }
        public DateTime FechaFin { get; set; }
        public string FechaFinString { get; set; }
        public FechaDto FechaFinObj { get; set; }
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
        public int UnidadTrabajoId { get; set; }
        
    }

    }

   

 


