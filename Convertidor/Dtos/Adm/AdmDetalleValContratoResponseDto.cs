namespace Convertidor.Dtos.Adm
{
    public class AdmDetalleValContratoResponseDto
    {
        public int CodigoDetalleValContrato { get; set; }
        public int CodigoContrato { get; set; }
        public int CodigoValContrato { get; set; }
        public int ConceptoID { get; set; }
        public string ComplementoConcepto { get; set; }
        public int PorConcepto { get; set; } 
        public int MontoConcepto { get; set; }
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
        public int CodigoPresupuesto { get; set; }
        
    }
}
