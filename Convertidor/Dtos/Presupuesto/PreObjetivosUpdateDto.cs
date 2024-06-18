namespace Convertidor.Dtos.Presupuesto
{
    public class PreObjetivosUpdateDto
    {
        public int CodigoObjetivo { get; set; }
        public int CodigoIcp { get; set; }
        public int NumeroObjetivo { get; set; }
        public string DenominacionObjetivo { get; set; } = string.Empty;
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
        public int CodigoPresupuesto { get; set; }
    }
}
