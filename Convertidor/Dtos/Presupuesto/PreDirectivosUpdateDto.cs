namespace Convertidor.Dtos.Presupuesto
{
    public class PreDirectivosUpdateDto
    {
        public int CodigoDirectivo { get; set; }
        public int CodigoIdentificacion { get; set; }
        public int TipoDirectivoId { get; set; }
        public int TituloId { get; set; }
        public int Cedula { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Cargo { get; set; } = string.Empty;
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
        public int CodigoPresupuesto { get; set; }
    }
}
