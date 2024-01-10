using System;
using System.Net.NetworkInformation;

namespace Convertidor.Dtos.Bm
{
	public class BmDirBienUpdateDto
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
        public DateTime FechaFin { get; set; }
        public int UnidadTrabajoId { get; set; }

    }

    }

   

 


