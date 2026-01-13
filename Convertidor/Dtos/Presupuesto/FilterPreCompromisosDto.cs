namespace Convertidor.Dtos.Presupuesto
{
    public class FilterPreCompromisosDto
    {

        public int CodigoCompromiso { get; set; }
        public string NumeroCompromiso { get; set; } = string.Empty;
        public DateTime fechaCompromiso { get; set; }
    }
}
