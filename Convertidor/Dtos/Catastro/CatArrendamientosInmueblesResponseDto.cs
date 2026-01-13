namespace Convertidor.Dtos.Catastro
{
    public class CatArrendamientosInmueblesResponseDto
    {
        public int CodigoArrendamientoInmueble { get; set; }
        public int CodigoInmueble { get; set; }
        public string NumeroDeExpediente { get; set; } = string.Empty;
        public DateTime FechaDonacion { get; set; }
        public string FechaDonacionString { get; set; }
        public FechaDto FechaDonacionObj { get; set; }
        public string NumeroConsecionDeUso { get; set; } = string.Empty;
        public string NumeroResolucionXResicion { get; set; } = string.Empty;
        public DateTime FechaResolucionXResicion { get; set; }
        public string FechaResolucionXResicionString { get; set; }
        public FechaDto FechaResolucionXResicionObj { get; set; }
        public string NumeroDeInforme { get; set; }
        public DateTime FechaInicioContrato { get; set; }
        public string FechaInicioContratoString { get; set; }
        public FechaDto FechaInicioContratoObj { get; set; }
        public DateTime FechaFinContrato { get; set; }
        public string FechaFinContratoString { get; set; }
        public FechaDto FechaFinContratoObj { get; set; }
        public string NumeroResolucion { get; set; } = string.Empty;
        public DateTime FechaResolucion { get; set; }
        public string FechaResolucionString { get; set; }
        public FechaDto FechaResolucionObj { get; set; }
        public string NumeroNotificacion { get; set; } = string.Empty;
        public decimal Canon { get; set; }
        public string Tomo { get; set; } = string.Empty;
        public string Folio { get; set; } = string.Empty;
        public string Registro { get; set; } = string.Empty;
        public int CodigoContribuyente { get; set; }
        public string NumeroContrato { get; set; } = string.Empty;
        public string Observaciones { get; set; } = string.Empty;
        public string NumeroArrendamiento { get; set; } = string.Empty;
        public string TipoTransaccion { get; set; } = string.Empty;
        public decimal Tributo { get; set; }
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
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
        public decimal PrecioUnitario { get; set; }
        public decimal ValorTerreno { get; set; }
        public string NumeroAcuerdo { get; set; } = string.Empty;
        public DateTime FechaAcuerdo { get; set; }
        public string FechaAcuerdoString { get; set; }
        public FechaDto FechaAcuerdoObj { get; set; }
        public string CodigoCatastro { get; set; }
        public DateTime FechaNotificacion { get; set; }
        public string FechaNotificacionString { get; set; }
        public FechaDto FechaNotificacionObj { get; set; }
    }
}
