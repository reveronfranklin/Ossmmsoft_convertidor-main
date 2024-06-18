namespace Convertidor.Dtos.Presupuesto
{
    public class PreEquiposUpdateDto
    {
        public int CodigoEquipo { get; set; }
        public int Ano { get; set; }
        public int Escenario { get; set; }
        public int CodigoIcp { get; set; }
        public int Principal { get; set; }
        public string Denominacion { get; set; } = string.Empty;
        public int Reemplazos { get; set; }
        public int Deficiencias { get; set; }
        public decimal CostoUnitario { get; set; }
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
        public int CodigoPresupuesto { get; set; }
    }
}
