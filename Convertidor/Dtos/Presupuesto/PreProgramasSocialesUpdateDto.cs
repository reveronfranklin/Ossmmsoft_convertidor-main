namespace Convertidor.Dtos.Presupuesto
{
    public class PreProgramasSocialesUpdateDto
    {
        public int CodigoPrgSocial { get; set; }
        public int CodigoIcp { get; set; }
        public string Denominacion { get; set; } = string.Empty;
        public int OrganismoId { get; set; }
        public int AsignacionAnual { get; set; }
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
        public int Ano { get; set; }
        public int CodigoPresupuesto { get; set; }
    }
}
