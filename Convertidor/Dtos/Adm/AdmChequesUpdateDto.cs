namespace Convertidor.Dtos.Adm
{
    public class AdmChequesUpdateDto
    {
        public int CodigoCheque { get; set; }
        public Int16 Ano { get; set; }
        public int CodigoCuentaBanco { get; set; }
        public int NumeroChequera { get; set; }
        public int NumeroCheque { get; set; }
        public DateTime FechaCheque { get; set; }
        public DateTime FechaConciliacion { get; set; }
        public DateTime FechaAnulacion { get; set; }
        public int CodigoProveedor { get; set; }
        public int CodigoContactoProveedor { get; set; }
        public Int16 PrintCount { get; set; }
        public string Motivo { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Endoso { get; set; } = string.Empty;
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
        public int UsuarioIns { get; set; }
        public DateTime FechaIns { get; set; }
        public int UsuarioUpd { get; set; }
        public DateTime FechaUpd { get; set; }
        public int CodigoEmpresa { get; set; }
        public int CodigoPresupuesto { get; set; }
        public string TipoBeneficiario { get; set; } = string.Empty;
        public int TipoChequeID { get; set; }
        public DateTime FechaEntrega { get; set; }
        public int UsuarioEntrega { get; set; }
    }
}
