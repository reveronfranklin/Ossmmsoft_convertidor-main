namespace Convertidor.Dtos.Adm
{
    public class AdmChequesResponseDto
    {
        public int CodigoLote { get; set; }
        public int CodigoCheque { get; set; }
        public int Ano { get; set; }
        public int CodigoCuentaBanco { get; set; }
        public string NroCuenta { get; set; } = string.Empty;
        public int CodigoBanco { get; set; } 
        public string NombreBanco { get; set; } = string.Empty;
        public int NumeroChequera { get; set; }
        public int NumeroCheque { get; set; }
        public int TipoChequeID { get; set; }
        public string DescripcionTipoCheque { get; set; } = string.Empty;
        public DateTime FechaCheque { get; set; }
        public string FechaChequeString { get; set; }
        public FechaDto FechaChequeObj { get; set; }
        public int CodigoProveedor { get; set; }
        public string NombreProveedor { get; set; }
        public int PrintCount { get; set; }
        public string Motivo { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Endoso { get; set; } = string.Empty;
        
        public int CodigoPresupuesto { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public string? FechaEntregaString { get; set; }
        public FechaDto? FechaEntregaObj { get; set; }
        public int UsuarioEntrega { get; set; }
    }
}
