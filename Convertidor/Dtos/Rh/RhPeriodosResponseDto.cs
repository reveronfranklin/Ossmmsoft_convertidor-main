namespace Convertidor.Dtos.Rh
{
    public class RhPeriodosResponseDto
    {
        public int CodigoPeriodo { get; set; }
        public int CodigoTipoNomina { get; set; }
        public DateTime FechaNomina { get; set; }
        public string FechaNominaString { get; set; }
        public FechaDto FechaNominaObj { get; set; }
        public int Periodo { get; set; }
        public string DescripcionPeriodo { get; set; }
        public string TipoNomina { get; set; }
        public string DescripcionTipoNomina{ get; set; }
        public string EXTRA1 { get; set; } = string.Empty;
        public string EXTRA2 { get; set; } = string.Empty;
        public string EXTRA3 { get; set; } = string.Empty;
        public int UsuarioPreCierre { get; set; }
        public DateTime FechaPreCierre { get; set; }
        public string FechaPreCierreString { get; set; }
        public FechaDto FechaPreCierreObj { get; set; }
        public int UsuarioCierre { get; set; }
        public DateTime FechaCierre { get; set; }
        public string FechaCierreString { get; set; }
        public FechaDto FechaCierreObj { get; set; }
        public int CodigoCuentaEmpresa { get; set; }
        public int UsuarioPreNomina { get; set; }
        public DateTime FechaPrenomina { get; set; }
        public string FechaPrenominaString { get; set; }
        public FechaDto FechaPrenominaObj { get; set; }
        public int CodigoPresupuesto { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public string SearchText { get { return $"{CodigoTipoNomina}-{FechaNominaString}-{DescripcionPeriodo}-{DescripcionTipoNomina}"; } }
        public int Year { get { return FechaNomina.Year; } }


    }
}

