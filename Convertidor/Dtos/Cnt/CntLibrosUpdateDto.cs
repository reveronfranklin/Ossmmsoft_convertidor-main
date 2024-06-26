namespace Convertidor.Dtos.Cnt
{
    public class CntLibrosUpdateDto
    {
        public int CodigoLibro { get; set; }
        public int CodigoCuentaBanco { get; set; }
        public DateTime FechaLibro { get; set; }
        public string Status { get; set; }
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
    }
}
