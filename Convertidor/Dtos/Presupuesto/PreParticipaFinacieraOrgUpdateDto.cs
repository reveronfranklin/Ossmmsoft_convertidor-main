namespace Convertidor.Dtos.Presupuesto
{
    public class PreParticipaFinacieraOrgUpdateDto
    {
        public int CodigoParticipaFinancorg { get; set; }
        public int Ano { get; set; }
        public int CodigoOrganismo { get; set; }
        public int CodigoIcp { get; set; }
        public int CuotaParticipacion { get; set; }
        public string Observaciones { get; set; } = string.Empty;
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
        public int CodigoPuc { get; set; }
        public int CodigoPresupuesto { get; set; }
    }
}
