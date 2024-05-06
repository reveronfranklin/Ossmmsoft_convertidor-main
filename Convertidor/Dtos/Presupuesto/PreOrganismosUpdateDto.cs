namespace Convertidor.Dtos.Presupuesto
{
    public class PreOrganismosUpdateDto
    {
        public int CodigoOrganismo { get; set; }
        public int Ano { get; set; }
        public string Denominacion { get; set; } = string.Empty;
        public string NumeroRegistro { get; set; } = string.Empty;
        public string Actividad { get; set; } = string.Empty;
        public string UbicacionGeografica { get; set; } = string.Empty;
        public int TipoOrganismoId { get; set; }
        public int CapitalSocial { get; set; }
        public int Monto1 { get; set; }
        public int Monto2 { get; set; }
        public int Monto4 { get; set; }
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
        public int Monto3 { get; set; }
        public int CodigoPresupuesto { get; set; }
    }
}
