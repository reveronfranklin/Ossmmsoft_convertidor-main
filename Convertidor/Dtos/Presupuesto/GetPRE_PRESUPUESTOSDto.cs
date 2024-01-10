namespace Convertidor.Dtos.Presupuesto
{
	public class GetPRE_PRESUPUESTOSDto
	{
        public int CodigoPresupuesto { get; set; }
        public string Denominacion { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public int Ano { get; set; }
        public decimal MontoPresupuesto { get; set; }
        
        public DateTime FechaDesde { get; set; } 
        public DateTime FechaHasta { get; set; } 
        public string FechaDesdeString { get; set; } = string.Empty;
        public string FechaHastaString { get; set; } = string.Empty;
        public FechaDto FechaDesdeObj { get; set; }
        public FechaDto FechaHastaObj { get; set; }
        
        public DateTime FechaAprobacion { get; set; }
        public string FechaAprobacionString { get; set; } = string.Empty; 
        public FechaDto FechaAprobacionObj { get; set; }
        
        public string NumeroOrdenanza { get; set; } = string.Empty;
        public DateTime FechaOrdenanza { get; set; }
        public string FechaOrdenanzaString { get; set; } = string.Empty;
        public FechaDto FechaOrdenanzaObj { get; set; }
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;

        public decimal TotalDisponible { get; set; }
        public decimal TotalPresupuesto { get; set; }
        public string TotalDisponibleString { get; set; } =string.Empty;
        public string TotalPresupuestoString { get; set; } = string.Empty;

        public decimal TotalModificacion { get; set; }
        public string TotalModificacionString { get; set; } = string.Empty;

        public decimal TotalVigente { get; set; }
        public string TotalVigenteString { get; set; } = string.Empty;


      
   
    
        public List<PreFinanciadoDto>? PreFinanciadoDto { get; set; }
        public List<GetPRE_V_DENOMINACION_PUCDto>? PreDenominacionPuc  { get; set; }
        public List<GetPreDenominacionPucResumenAnoDto>? PreDenominacionPucResumen { get; set; }

    }


}

