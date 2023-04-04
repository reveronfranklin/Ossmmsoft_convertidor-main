using System;
namespace Convertidor.Dtos.Rh
{
	public class ListDireccionesDto
	{
        public int CodigoDireccion { get; set; }
        public int CodigoPersona { get; set; }
        public int DireccionId { get; set; }
        public string DescripcionDireccion { get; set; } = string.Empty;
        public int PaisId { get; set; }
        public string DescripcionPais { get; set; } = string.Empty;
        public int EstadoId { get; set; }
        public string DescripcionEsatado { get; set; } = string.Empty;
        public int MunicipioId { get; set; }
        public string DescripcionMunicipio { get; set; } = string.Empty;
        public int CiudadId { get; set; }
        public string DescripcionCiudad { get; set; } = string.Empty;
        public int ParroquiaId { get; set; }
        public string DescripcionParroquia { get; set; } = string.Empty;
        public int SectorId { get; set; }
        public string DescripcionSector { get; set; } = string.Empty;
        public int UrbanizacionId { get; set; }
        public string DescripcionUrbanizacion { get; set; } = string.Empty;
        public int ManzanaId { get; set; }
        public string DescripcionManzana { get; set; } = string.Empty;
        public int ParcelaId { get; set; }
        public string DescripcionParcela{ get; set; } = string.Empty;
        public int VialidadId { get; set; }
        public string DescripcionVialidad { get; set; } = string.Empty;
        public string Vialidad { get; set; } = string.Empty;
        public int TipoViviendaId { get; set; }
        public string DescripcionTipovivienda { get; set; } = string.Empty;
        public int ViviendaId { get; set; }
        public string DescripcionVivienda { get; set; } = string.Empty;
        public string Vivienda { get; set; } = string.Empty;
        public int TipoNivelId { get; set; }
        public string DescripcionTipoNivel{ get; set; } = string.Empty;
        public string Nivel { get; set; } = string.Empty;
        public string NroVivienda { get; set; } = string.Empty;
        public string ComplementoDir { get; set; } = string.Empty;
        public int TenenciaId { get; set; }
        public string DescripcionTenencia { get; set; } = string.Empty;
        public int CodigoPostal { get; set; }
        public int Principal { get; set; }
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
        public int CodigoEmpresa { get; set; }
    }
}

