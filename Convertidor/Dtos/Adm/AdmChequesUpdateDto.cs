namespace Convertidor.Dtos.Adm
{
    public class AdmChequesUpdateDto
    {
        public int CodigoLote { get; set; }         //Relacion con ADM_LOTE_PAGOS
        public int CodigoCheque { get; set; }       // CLAVE DE REGISTRO, EN CERO EN LA CREACION
        public int Ano { get; set; }                //?
        public int CodigoCuentaBanco { get; set; }  //Codigo de Cuenta, clave foranea con ADM_CUENTA_BANCO
        public int NumeroChequera { get; set; }     //?
        public int NumeroCheque { get; set; }       //?
        public DateTime FechaCheque { get; set; }   //Fecha del Cheque
        public int CodigoProveedor { get; set; }    //Codigo de proveedor,clave foranea con ADM_PROVEEDORES
        public Int16 PrintCount { get; set; }       //?
        public string Motivo { get; set; } = string.Empty;  //Motivo texto explicativo del pago
        public string Status { get; set; } = string.Empty;  //PE,AP,AN 
        public string Endoso { get; set; } = string.Empty; //?
        public int CodigoPresupuesto { get; set; }  // Codigo de Presupuesto Afectado
        public int TipoChequeID { get; set; }       //Clave Foranea con ADM_DESCRIPTIVA

    }
}
