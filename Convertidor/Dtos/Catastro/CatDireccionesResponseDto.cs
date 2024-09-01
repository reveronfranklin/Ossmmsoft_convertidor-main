namespace Convertidor.Dtos.Catastro
{
    public class CatDireccionesResponseDto
    {
        public int CodigoDireccion { get; set; }
        public int CodigoContribuyente { get; set; }
        public int CodigoCuenta { get; set; }
        public int CodigoInmueble { get; set; }
        public int DireccionId { get; set; }
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
        public string Vivienda { get; set; }
        public int TipoNivelId { get; set; }
        public string Nivel { get; set; }
        public int TipoUnidadId { get; set; }
        public string NumeroUnidad { get; set; } = string.Empty;
        public string ComplementoDir { get; set; } = string.Empty;
        public int TenenciaId { get; set; }
        public int CodigoPostal { get; set; }
        public int Principal { get; set; }
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
        public string TipoTransaccion { get; set; } = string.Empty;
        public string Extra4 { get; set; } = string.Empty;
        public string Extra5 { get; set; } = string.Empty;
        public string Extra6 { get; set; } = string.Empty;
        public string Extra7 { get; set; } = string.Empty;
        public string Extra8 { get; set; } = string.Empty;
        public string Extra9 { get; set; } = string.Empty;
        public string Extra10 { get; set; } = string.Empty;
        public string Extra11 { get; set; } = string.Empty;
        public string Extra12 { get; set; } = string.Empty;
        public string Extra13 { get; set; } = string.Empty;
        public string Extra14 { get; set; } = string.Empty;
        public string Extra15 { get; set; } = string.Empty;
        public int CodigoFicha { get; set; }
    }
}
