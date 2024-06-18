namespace Convertidor.Dtos.Presupuesto
{
	public class PreIndiceCategoriaProgramaticaUpdateDto
	{
        public int CodigoIcp { get; set; }
        public int CodigoIcpPadre { get; set; }
        public int Ano { get; set; }
        public int Escenario { get; set; }
        public string CodigoSector { get; set; } = string.Empty;
        public string CodigoPrograma { get; set; } = string.Empty;
        public string CodigoSubPrograma { get; set; } = string.Empty;
        public string CodigoProyecto { get; set; } = string.Empty;
        public string CodigoActividad { get; set; } = string.Empty;
        public string Denominacion { get; set; } = string.Empty;
        public string UnidadEjecutora { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public int CodigoFuncionario { get; set; }
     
        public string CodigoOficina { get; set; } = string.Empty;
        public int CodigoPresupuesto { get; set; }
        public string UsuarioConectado  { get; set; } = string.Empty;




        /*public string FechaIni { get; set; } = string.Empty;
        public string FechaFin { get; set; } = string.Empty;
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;*/
    }
}

