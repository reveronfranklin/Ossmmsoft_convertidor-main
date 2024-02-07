namespace Convertidor.Dtos.Rh
{
	public class RhExpLaboralResponseDto
    {
        public int CodigoExpLaboral { get; set; }
        public int CodigoPersona { get; set; }
        public string NombreEmpresa { get; set; }
        public string TipoEmpresa { get; set; } 
        public int RamoId { get; set; }
        public string Cargo { get; set; } 
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        public string FechaDesdeString { get; set; }
        public string FechaHastaString { get; set; }
        public FechaDto FechaDesdeObj { get; set; }
        public FechaDto FechaHastaObj { get; set; }
        public int UltimoSueldo { get; set; }
        public string Supervisor { get; set; } 
        public string CargoSupervisor { get; set; }
        public string Telefono { get; set; } 
        public string Descripcion { get; set; }
     

    }
}

