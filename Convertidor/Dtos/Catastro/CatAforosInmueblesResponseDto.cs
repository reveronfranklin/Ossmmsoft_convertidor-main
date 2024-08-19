namespace Convertidor.Dtos.Catastro
{
    public class CatAforosInmueblesResponseDto
    {
        public int CodigoAforoInmueble { get; set; }
        public int Tributo { get; set; }
        public int CodigoInmueble { get; set; }
        public decimal Monto { get; set; }
        public decimal MontoMinimo { get; set; }
        public int CodigoFormaLiquidacion { get; set; }
        public int CodigoFormaLiqMinimo { get; set; }
        public DateTime FechaLiquidacion { get; set; }
        public string FechaLiquidacionString { get; set; }
        public FechaDto FechaLiquidacionObj { get; set; }
        public DateTime FechaPeriodoIni { get; set; }
        public string FechaPeriodoIniString { get; set; }
        public FechaDto FechaPeriodoIniObj { get; set; }
        public DateTime FechaPeriodoFin { get; set; }
        public string FechaPeriodoFinString { get; set; }
        public FechaDto FechaPeriodoFinObj { get; set; }
        public int AplicadoId { get; set; }
        public int CodigoAplicado { get; set; }
        public int Estatus { get; set; }
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
        public DateTime FechaInicioExonera { get; set; }
        public string FechaInicioExoneraString { get; set; }
        public FechaDto FechaInicioExoneraObj { get; set; }
        public DateTime FechaFinExonera { get; set; }
        public string FechaFinExoneraString { get; set; }
        public FechaDto FechaFinExoneraObj { get; set; }
        public string Observacion { get; set; }
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
        public int Codigoficha { get; set; }
        public int CodigoAvaluoConstruccion { get; set; }
        public int CodigoAvaluoTerreno { get; set; }
    }
}
